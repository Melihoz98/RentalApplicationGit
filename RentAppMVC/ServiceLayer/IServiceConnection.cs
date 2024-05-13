﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAppMVC.ServiceLayer
{
    public interface IServiceConnection
    {
        string? BaseUrl { get; init; }
        string? UseUrl { get; set; }

        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete();

        
        Task<HttpResponseMessage?> GetById(int id);
        Task<HttpResponseMessage?> GetById(string id);
    }
}