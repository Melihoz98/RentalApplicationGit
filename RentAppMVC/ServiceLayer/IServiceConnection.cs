﻿namespace RentAppMVC.ServiceLayer
{
    public interface IServiceConnection
    {
        string? BaseUrl { get; init; }
        string? UseUrl { get; set; }

        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete();

        // New method to get data by ID
        Task<HttpResponseMessage?> GetById(int id);
        Task<HttpResponseMessage?> GetById(string id);

        Task<HttpResponseMessage?> Get(string url);
       
    }
}