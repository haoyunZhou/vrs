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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using InterfaceFactory;
using VirtualRadar.Interface;
using VirtualRadar.Interface.PortableBinding;
using VirtualRadar.Interface.Presenter;
using VirtualRadar.Interface.Settings;
using VirtualRadar.Interface.View;

namespace VirtualRadar.Plugin.WebAdmin.View
{
    class SettingsView : BaseView, ISettingsView
    {
        #region Fields
        private ISettingsPresenter _Presenter;
        #endregion

        #region Interface properties
        public Configuration Configuration { get; set; }

        public NotifyList<IUser> Users { get; private set; }

        public string UserManager { get; set; }

        public string OpenOnPageTitle { get; set; }

        public object OpenOnRecord { get; set; }
        #endregion

        #region Other properties
        /// <summary>
        /// Gets or sets the most recent set of validation results.
        /// </summary>
        public WebValidationResults ValidationResults { get; set; }
        #endregion

        #region Interface events
        public event EventHandler SaveClicked;

        public event EventHandler<EventArgs<Receiver>> TestConnectionClicked;

        public event EventHandler TestTextToSpeechSettingsClicked;

        public event EventHandler UpdateReceiverLocationsFromBaseStationDatabaseClicked;

        public event EventHandler UseIcaoRawDecodingSettingsClicked;

        public event EventHandler UseRecommendedRawDecodingSettingsClicked;

        public event EventHandler FlightSimulatorXOnlyClicked;
        #endregion

        #region Ctor
        public SettingsView()
        {
            Users = new NotifyList<IUser>();
        }
        #endregion

        #region Interface methods
        public void PopulateTextToSpeechVoices(IEnumerable<string> voiceNames)
        {
            ;
        }

        public void ShowTestConnectionResults(string message, string title)
        {
            ;
        }

        public void ShowValidationResults(ValidationResults results)
        {
            ValidationResults = TranslateValidationResults(results);
        }

        protected override void TranslateValidationResultRecord(WebValidationResult result, ValidationResult originalResult)
        {
            if(originalResult.Record != null) {
                var receiver = originalResult.Record as Receiver;
                if(receiver != null) {
                    result.RecordType = "Receiver";
                    result.RecordId = receiver.UniqueId.ToString();
                }
            }
        }

        const string _FieldRoot = "Configuration";
        static readonly string _FieldBaseStation = String.Format("{0}.{1}", _FieldRoot, PropertyHelper.ExtractName<Configuration>(r => r.BaseStationSettings));

        protected override void TranslateValidationResultField(WebValidationResult result, ValidationResult originalResult)
        {
            string path = null;
            string property = null;

            switch(result.RecordType ?? "") {
                case "":
                    switch(originalResult.Field) {
                        case ValidationField.BaseStationDatabase:   path = _FieldBaseStation; property = PropertyHelper.ExtractName<BaseStationSettings>(r => r.DatabaseFileName); break;
                        case ValidationField.FlagsFolder:           path = _FieldBaseStation; property = PropertyHelper.ExtractName<BaseStationSettings>(r => r.OperatorFlagsFolder); break;
                        case ValidationField.PicturesFolder:        path = _FieldBaseStation; property = PropertyHelper.ExtractName<BaseStationSettings>(r => r.PicturesFolder); break;
                        case ValidationField.SilhouettesFolder:     path = _FieldBaseStation; property = PropertyHelper.ExtractName<BaseStationSettings>(r => r.SilhouettesFolder); break;
                        default:
                            break;
                    }
                    break;
                case "Receiver":
                    break;
            }

            if(!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(property)) {
                result.FieldId = String.Format("{0}.{1}", path, property);
            }
        }
        #endregion

        #region Base overrides
        public override System.Windows.Forms.DialogResult ShowView()
        {
            if(IsRunning) {
                var configuration = Factory.Singleton.Resolve<IConfigurationStorage>().Singleton.Load();
                if(configuration.DataVersion != Configuration.DataVersion) {
                    _Presenter.Dispose();
                    _Presenter = null;
                    IsRunning = false;
                }
            }

            if(!IsRunning) {
                _Presenter = Factory.Singleton.Resolve<ISettingsPresenter>();
                _Presenter.Initialise(this);
                _Presenter.ValidateView();
            }

            return base.ShowView();
        }
        #endregion
    }
}