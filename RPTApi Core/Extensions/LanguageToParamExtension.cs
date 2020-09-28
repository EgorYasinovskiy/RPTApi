using RPTApi.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPTApi.Extensions
{
    public static class LanguageToParamExtension
    {
        public static string ToParameter(this Language language)
        {
            switch (language)   
            {
                case Language.Russian:
                    return "RUS";
                case Language.English:
                    return "ENG";
                default:
                    return "RUS";
            }
        }
    }
}
