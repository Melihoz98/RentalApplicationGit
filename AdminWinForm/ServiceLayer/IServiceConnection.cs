using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public interface IServiceConnection
    {
        string? BaseUrl { get; init; }
        string? UseUrl { get; set; }


        public HttpClient HttpEnabler { get; init; }

        Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest);


        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete(string deleteUrl);
        Task<HttpResponseMessage?> CallServiceDelete();
    }
}
