﻿// Copyright © 2017 onwards, Andrew Whewell
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceFactory;
using VirtualRadar.Interface.Owin;

namespace VirtualRadar.Owin.Middleware
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    /// <summary>
    /// Default implementation of <see cref="IResponseStreamWrapper"/>.
    /// </summary>
    class ResponseStreamWrapper : IResponseStreamWrapper
    {
        private IStreamManipulator[] _StreamManipulators;

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="streamManipulators"></param>
        public void Initialise(IEnumerable<IStreamManipulator> streamManipulators)
        {
            _StreamManipulators = streamManipulators.ToArray();
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public Func<IDictionary<string, object>, Task> WrapResponseStream(Func<IDictionary<string, object>, Task> next)
        {
            AppFunc appFunc = async(IDictionary<string, object> environment) => {
                var context = PipelineContext.GetOrCreate(environment);
                var originalStream = context.Response.Body;

                using(var wrapperStream = new MemoryStream()) {
                    context.Response.Body = wrapperStream;

                    await next.Invoke(environment);

                    foreach(var streamManipulator in _StreamManipulators) {
                        streamManipulator.ManipulateResponseStream(environment);
                    }

                    if(originalStream != Stream.Null && wrapperStream != Stream.Null && wrapperStream.Length > 0) {
                        context.Response.ContentLength = wrapperStream.Length;
                        wrapperStream.Position = 0;
                        wrapperStream.CopyTo(originalStream);
                    }

                    context.Response.Body = originalStream;
                }
            };

            return appFunc;
        }
    }
}
