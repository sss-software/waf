﻿using Foundation;
using System;
using System.Globalization;
using System.Linq;
using Waf.NewsReader.Applications.Services;

namespace Waf.NewsReader.iOS.Services
{
    // See https://github.com/mono/mono/issues/16827
    public class LocalizationService : ILocalizationService
    {
        public void Initialize()
        {
            var preferredLanguage = NSLocale.PreferredLanguages.FirstOrDefault()?.Replace("_", "-", StringComparison.Ordinal);
            if (preferredLanguage == null) return;
            if (CultureInfo.CurrentCulture.Name == preferredLanguage) return;
            var cultureCode = string.Join('-', preferredLanguage.Split('-').Skip(1));
            var supportedCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            // Note: This is a very simple approach to find the first culture with the country code -> might come with the wrong language
            var culture = supportedCultures.FirstOrDefault(x => x.Name.Substring(3) == cultureCode);
            if (culture != null)
            {
                CultureInfo.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture = culture;
            }
        }
    }
}