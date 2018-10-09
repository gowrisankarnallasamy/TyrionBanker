using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.FrontUI.Common
{
    public class TyrionBankerConfiguration : ITyrionBankerConfiguration
    {
        private static string GetConnectionString([CallerMemberName] string propertyName = null) =>
            ConfigurationManager.ConnectionStrings[propertyName]?.ConnectionString;

        private static string GetString([CallerMemberName] string propertyName = null, string defaultValue = null) =>
            ConfigurationManager.AppSettings[propertyName] ?? defaultValue;

        private static int GetInt32([CallerMemberName] string propertyName = null, int defaultValue = 0) =>
            ConfigurationManager.AppSettings[propertyName] == null
                ? defaultValue
                : int.Parse(ConfigurationManager.AppSettings[propertyName]);

        private static double GetDouble([CallerMemberName] string propertyName = null, double defaultValue = 0) =>
         ConfigurationManager.AppSettings[propertyName] == null
             ? defaultValue
             : double.Parse(ConfigurationManager.AppSettings[propertyName]);

        private static int? GetInt32Null([CallerMemberName] string propertyName = null) =>
            ConfigurationManager.AppSettings[propertyName] == null
                ? (int?)null
                : int.Parse(ConfigurationManager.AppSettings[propertyName]);

        private static bool GetBool([CallerMemberName] string propertyName = null) =>
            ConfigurationManager.AppSettings[propertyName]?.ToLower() == "true";

        private static TimeSpan? GetTimeSpanNull([CallerMemberName] string propertyName = null) =>
         ConfigurationManager.AppSettings[propertyName] == null
             ? (TimeSpan?)null
             : TimeSpan.Parse(ConfigurationManager.AppSettings[propertyName]);

        public string SourceId => GetString();

        public string ApiTyrionBankerBaseUri => GetString();

        public string ApiTyrionBankerGetOwinToken => GetCombinedUrl(GetString());

        public string ApiTyrionBankerGetRoles => GetCombinedUrl(GetString());

        private string GetCombinedUrl(string deriveUrl)
        {
            char pathSeparator = '/';
            string combinedUrl = ApiTyrionBankerBaseUri.Trim(pathSeparator) + pathSeparator + deriveUrl.Trim(pathSeparator);
            return combinedUrl;
        }
    }
}
