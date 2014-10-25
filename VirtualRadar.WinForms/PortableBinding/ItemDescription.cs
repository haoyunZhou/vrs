﻿// Copyright © 2014 onwards, Andrew Whewell
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
using System.Linq;
using System.Text;

namespace VirtualRadar.WinForms.PortableBinding
{
    /// <summary>
    /// Carries an item and its description.
    /// </summary>
    public class ItemDescription<T>
    {
        public T Item { get; private set; }

        public string Description { get; private set; }

        public ItemDescription(T item, string description)
        {
            Item = item;
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }

        public bool AreItemDescriptionEqual(T item, string description)
        {
            return Object.Equals(Item, item) && String.Equals(Description, description);
        }

        public override bool Equals(object obj)
        {
            var result = Object.ReferenceEquals(this, obj);
            if(!result) {
                var other = obj as ItemDescription<T>;
                result = other != null && Object.Equals(Item, other.Item) && String.Equals(Description, other.Description);
            }

            return result;
        }

        public override int GetHashCode()
        {
            var result = Object.Equals(Item, default(T)) ? 0 : Item.GetHashCode();
            return result;
        }
    }
}