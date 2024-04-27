namespace RentAppMVC.ServiceLayer
{
    public class ServiceConnection : IServiceConnection
    {
        private readonly HttpClient HttpEnabler; // Consider making public with setter for flexibility
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }

        public ServiceConnection(string inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }

        public async Task<HttpResponseMessage?> CallServiceGet(string url)
        {
            HttpResponseMessage? hrm = null;
            if (url != null)
            {
                hrm = await HttpEnabler.GetAsync(url);
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
            // Basic implementation for Put. You might need to modify based on your API requirements
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.PutAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceDelete()
        {
            // Basic implementation for Delete. You might need to modify based on your API requirements
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.DeleteAsync(UseUrl);
            }
            return hrm;
        }
    }
}
