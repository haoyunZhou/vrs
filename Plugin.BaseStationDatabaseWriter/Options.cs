﻿// Copyright © 2010 onwards, Andrew Whewell
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

namespace VirtualRadar.Plugin.BaseStationDatabaseWriter
{
    /// <summary>
    /// Holds the options for the plugin.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the version of the options being saved.
        /// </summary>
        public long DataVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the plugin is to record information about received aircraft in the BaseStation database.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the ID of the receiver to maintain the database for.
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the plugin is allowed to update databases that it did not create. By default it will only
        /// update databases that it knows it created.
        /// </summary>
        public bool AllowUpdateOfOtherDatabases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that aircraft details downloaded from the Internet should be saved to BaseStation.sqb.
        /// </summary>
        public bool SaveDownloadedAircraftDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the cache should be saving both new / missing and out-of-date aircraft.
        /// </summary>
        public bool RefreshOutOfDateAircraft { get; set; }
    }
}
