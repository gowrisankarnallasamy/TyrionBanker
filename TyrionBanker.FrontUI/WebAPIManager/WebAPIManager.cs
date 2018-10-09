using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Threading;
using TyrionBanker.Core.Log;
using System.Threading.Tasks;
using TyrionBanker.FrontUI.Common;
using Unity.Attributes;

namespace TyrionBanker.FrontUI.WebAPIManager
{
    public class WebAPIManager : IWebAPIManager
    {
        [Dependency]
        public ITyrionBankerConfiguration TyrionBankerConfiguration { get; set; }
        public static string Token { get; set; }
        private const string EXECUTE_LOG_MESSAGE = "<API Call> [Type:{0}] [APIURL:{1}] [Parameter:{2}]";
        private const string LEAVE_LOG_MESSAGE = "<API Result> [Type:{0}] [APIURL:{1}] [Parameter:{2}]";
        public TyrionBankerLogger Log { get; }

        public WebAPIManager()
        {
            Log = new TyrionBankerLogger(typeof(WebAPIManager));
        }

        /// <summary>
        /// APIを呼び出してエンティティを取得します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ApiURL"></param>
        /// <returns></returns>
        async public Task<T> GetEntityAsync<T>(string ApiURL)
        {
            var client = createInstanceAndHeadder();

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "GET", ApiURL, string.Empty));
            HttpResponseMessage response = client.GetAsync(ApiURL).Result;

            if (response.IsSuccessStatusCode)
            {
                string resultstr = response.Content.ReadAsStringAsync().Result;
                Log.Info(string.Format("API RESULT:{0}", resultstr));
                T result = await response.Content.ReadAsAsync<T>();
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "GET", ApiURL, string.Empty));
                return result;
            }
            else
            {
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1}", response.RequestMessage.ToString(), response.ToString());
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage);
            }
        }

        async public Task<string> GetStringAsync(string ApiURL)
        {
            var client = createInstanceAndHeadder();

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "GET", ApiURL, string.Empty));
            HttpResponseMessage response = await client.GetAsync(ApiURL);

            if (response.IsSuccessStatusCode)
            {
                string resultstr = response.Content.ReadAsStringAsync().Result;
                Log.Info(string.Format("API RESULT:{0}", resultstr));
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "GET", ApiURL, string.Empty));
                return resultstr;
            }
            else
            {
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1}", response.RequestMessage.ToString(), response.ToString());
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage);
            }
        }

        async public Task<T> PostEntityAsync<T>(string ApiUrl, object parameter)
        {
            return await PostEntityAsync<T>(ApiUrl, parameter, default(HeaderOptions));
        }

        async public Task<T> PostEntityAsync<T>(string ApiUrl, object parameter, HeaderOptions headerOptions)
        {
            var client = createInstanceAndHeadder(headerOptions);

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "POST", ApiUrl, JsonConvert.SerializeObject(parameter)));
            HttpResponseMessage response = await client.PostAsJsonAsync(ApiUrl, parameter);

            if (response.IsSuccessStatusCode)
            {
                string resultstr = await response.Content.ReadAsStringAsync();
                Log.Info(string.Format("API RESULT:{0}", resultstr));
                T result = await response.Content.ReadAsAsync<T>();
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "POST", ApiUrl, string.Empty));
                return result;
            }
            else if (response.Content.ReadAsStringAsync().Result.Contains("Error code - 13515"))
            {
                var parameterValue = JsonConvert.SerializeObject(parameter);
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1} [Parameter]{2}", response.RequestMessage.ToString(), response.Content.ReadAsStringAsync().Result.ToString(), parameterValue);
                Log.Error(errorMessage);
                errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(errorMessage) { Source = response.Content.ReadAsStringAsync().Result };
            }

            else
            {

                var parameterValue = JsonConvert.SerializeObject(parameter);
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1} [Parameter]{2}", response.RequestMessage.ToString(), response.ToString(), parameterValue);
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage) { Source = response?.Content?.ReadAsStringAsync()?.Status.ToString() };
            }

        }

        async public Task<T> PostContentAsync<T>(string ApiUrl, IDictionary<string, Stream> files)
        {
            return await PostContentAsync<T>(ApiUrl, files, default(HeaderOptions));
        }

        async public Task<T> PostContentAsync<T>(string ApiUrl, IDictionary<string, Stream> files, HeaderOptions headerOptions)
        {
            var client = createInstanceAndHeadder(headerOptions);

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "POST", ApiUrl, null));
            var content = new MultipartFormDataContent();
            foreach (string fileName in files.Keys)
            {
                var fileContent = new StreamContent(files[fileName]);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                content.Add(fileContent);
            }
            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string resultstr = response.Content.ReadAsStringAsync().Result;
                Log.Info(string.Format("API RESULT:{0}", resultstr));
                T result = response.Content.ReadAsAsync<T>().Result;
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "POST", ApiUrl, string.Empty));
                return result;
            }
            else
            {
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1}", response.RequestMessage.ToString(), response.ToString());
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage);
            }
        }

        async public Task<T> PostContentAsync<T>(string ApiUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return await PostContentAsync<T>(ApiUrl, parameters, default(HeaderOptions));
        }

        async public Task<T> PostContentAsync<T>(string ApiUrl, IEnumerable<KeyValuePair<string, string>> parameters, HeaderOptions headerOptions)
        {
            var client = createInstanceAndHeadder(headerOptions);

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "POST", ApiUrl, null));
            HttpContent content = new FormUrlEncodedContent(parameters);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string resultstr = response.Content.ReadAsStringAsync().Result;
                Log.Info(string.Format("API RESULT:{0}", resultstr));
                T result = response.Content.ReadAsAsync<T>().Result;
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "POST", ApiUrl, string.Empty));
                return result;
            }
            else
            {
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1}", response.RequestMessage.ToString(), response.ToString());
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage);
            }
        }

        async public Task<Stream> GetContentAsync(string ApiURL)
        {
            var client = createInstanceAndHeadder();

            Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "GetContent", ApiURL, string.Empty));
            HttpResponseMessage response = await client.GetAsync(ApiURL);

            if (response.IsSuccessStatusCode)
            {
                System.IO.Stream result = await response.Content.ReadAsStreamAsync();
                Log.Info(string.Format(LEAVE_LOG_MESSAGE, "GetContent", ApiURL, string.Empty));
                return result;
            }
            else
            {
                string errorMessage = string.Format("<API Error> [RequestMessage]{0} [Response]{1}", response.RequestMessage.ToString(), response.ToString());
                Log.Error(errorMessage);
                errorMessage = "<API Error>";
                throw new HttpRequestException(errorMessage);
            }
        }

        async public Task<bool> TestUrlAsync(string ApiURL)
        {
            try
            {
                var client = createInstanceAndHeadder();

                Log.Info(string.Format(EXECUTE_LOG_MESSAGE, "GET", ApiURL, string.Empty));
                HttpResponseMessage response = await client.GetAsync(ApiURL);

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private HttpClient createInstanceAndHeadder(HeaderOptions headerOptions = default(HeaderOptions))
        {
            var webProxy = WebProxy.GetDefaultProxy();
            webProxy.UseDefaultCredentials = true;
            WebRequest.DefaultWebProxy = webProxy;

            var handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true,
                Proxy = webProxy,
            };
            HttpClient client = client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Add("X-UserName", "System");
            //client.DefaultRequestHeaders.Add("X-UserId", "System");
            //client.DefaultRequestHeaders.Add("X-Password", "nextscape");

            if (/*headerOptions.Has(HeaderOptions.OwinToken) &&*/ !string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                client.DefaultRequestHeaders.Add("SourceID", TyrionBankerConfiguration.SourceId);
            }

            //if (headerOptions.Has(HeaderOptions.TimeOut))
            //{
            //    double TimeOutInMinutes;
            //    if (!double.TryParse(ConfigurationManager.AppSettings["UploadTimeout"], out TimeOutInMinutes))
            //    {
            //        TimeOutInMinutes = 15;
            //    }
            //    client.Timeout = TimeSpan.FromMinutes(TimeOutInMinutes);
            //}
            //else
            //{
            //    double DefaultTimeOutInMinutes;
            //    if (!double.TryParse(ConfigurationManager.AppSettings["DefaultTimeOut"], out DefaultTimeOutInMinutes))
            //    {
            //        DefaultTimeOutInMinutes = 1;
            //    }
            //    client.Timeout = TimeSpan.FromMinutes(DefaultTimeOutInMinutes);
            //}

            return client;
        }
    }

    [Flags]
    public enum HeaderOptions : int
    {
        None = 0x0,
        TimeOut = 0x1,
        TimeZone = 0x2,
        KeepAlive = 0x4,
        OwinToken = 0x8
    }

    public static class Util
    {
        public static bool Has(this HeaderOptions source, HeaderOptions expected)
        {
            return (source & expected) == expected;
        }
    }
}
