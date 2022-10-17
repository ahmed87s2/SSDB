﻿using System.Linq;
using SSDB.Shared.Constants.Localization;
using SSDB.Shared.Settings;

namespace SSDB.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}