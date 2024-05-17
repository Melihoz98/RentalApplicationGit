using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(string inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }

        public HttpClient HttpEnabler {  get; init; }
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public async Task<HttpResponseMessage?> CallServicePostJWT(HttpRequestMessage postRequest)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.SendAsync(postRequest);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceGet()
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.GetAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.PostAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePut(StringContent postJson)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.PutAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceDelete()
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.DeleteAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceDelete(string deleteUrl)
        {
            using (var client = new HttpClient())
            {
                string url = $"{BaseUrl}/{deleteUrl}";

                try
                {
                    return await client.DeleteAsync(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while making DELETE request: {ex.Message}");
                    return null;
                }
            }
        }

    }
}
