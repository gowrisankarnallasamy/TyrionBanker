using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.FrontUI.Common;
using TyrionBanker.FrontUI.Models;
using Unity.Attributes;

namespace TyrionBanker.FrontUI.Services
{
    internal class BankerApiService : AbstractService, IBankerApiService
    {
        [Dependency]
        public ITyrionBankerConfiguration TyrionBankerConfiguration { get; set; }

        async public Task<OwinToken> GetBankerTokenAsync(string userName, string password)
        {
            OwinToken token = null;
            try
            {
                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("username", userName));
                parameters.Add(new KeyValuePair<string, string>("password", password));
                parameters.Add(new KeyValuePair<string, string>("grant_type", "password"));
                parameters.Add(new KeyValuePair<string, string>("SourceID", TyrionBankerConfiguration.SourceId));
                token = await webAPIManager.PostContentAsync<OwinToken>(TyrionBankerConfiguration.ApiTyrionBankerGetOwinToken, parameters);
                WebAPIManager.WebAPIManager.Token = token.AccessToken;
            }
            catch (Exception ex)
            {
                // Handle error
            }
            return token;
        }

        async public Task<IEnumerable<string>> GetRolesAsync()
        {
            IEnumerable<string> roles = null;
            try
            {
                roles = await webAPIManager.GetEntityAsync<IEnumerable<string>>(TyrionBankerConfiguration.ApiTyrionBankerGetRoles);
            }
            catch (Exception ex)
            {
                // Handle error
            }
            return roles;
        }
    }
}
