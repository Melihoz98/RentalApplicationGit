namespace RentAppMVC.ServiceLayer
{
    public interface IServiceConnection
    {
        string? BaseUrl { get; init; }
        string? UseUrl { get; set; }

        Task<HttpResponseMessage?> CallServiceGet(string url);
        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete();
    }
}
