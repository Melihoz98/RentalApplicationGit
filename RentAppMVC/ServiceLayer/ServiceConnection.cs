﻿namespace RentAppMVC.ServiceLayer
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(string inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }

        public HttpClient HttpEnabler { private get; init; }
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }

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

        public async Task<HttpResponseMessage?> GetById(string id)
        {
            if (UseUrl != null)
            {
                UseUrl = $"{BaseUrl}{id}";
                HttpResponseMessage? hrm = await HttpEnabler.GetAsync(UseUrl);
                return hrm;
            }
            return null;
        }
        public async Task<HttpResponseMessage?> GetById(int id)
        {
            if (UseUrl != null)
            {
                UseUrl = $"{BaseUrl}{id}";
                HttpResponseMessage? hrm = await HttpEnabler.GetAsync(UseUrl);
                return hrm;
            }
            return null;
        }

        public async Task<HttpResponseMessage?> Get(string url)
        {
            if (UseUrl != null)
            {
                HttpResponseMessage? hrm = await HttpEnabler.GetAsync(url);
                return hrm;
            }
            return null;
        }
    }
}