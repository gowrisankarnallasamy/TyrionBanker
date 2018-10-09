using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TyrionBanker.Core;
using TyrionBanker.FrontUI.Common;

namespace TyrionBanker.FrontUI.WebAPIManager
{
    public interface IWebAPIManager : ITyrionBankerBase
    {
        Task<T> GetEntityAsync<T>(string ApiURL);
        Task<string> GetStringAsync(string ApiURL);
        Task<T> PostEntityAsync<T>(string ApiUrl, object parameter);
        Task<T> PostEntityAsync<T>(string ApiUrl, object parameter, HeaderOptions headerOptions);
        Task<T> PostContentAsync<T>(string ApiUrl, IEnumerable<KeyValuePair<string, string>> parameters);
        Task<T> PostContentAsync<T>(string ApiUrl, IEnumerable<KeyValuePair<string, string>> parameters, HeaderOptions headerOptions);
        Task<T> PostContentAsync<T>(string ApiUrl, IDictionary<string, Stream> files);
        Task<T> PostContentAsync<T>(string ApiUrl, IDictionary<string, Stream> files, HeaderOptions headerOptions);
        Task<Stream> GetContentAsync(string ApiURL);
        Task<bool> TestUrlAsync(string ApiURL);

    }
}
