﻿using RentalService.DTO;

namespace RentalService.Business
{
    public interface IPrivateCustomerData
    {
        List<PrivateCustomerDto?>? GetAllPrivateCustomers();
        PrivateCustomerDto? GetPrivateCustomerById(string id);
        void createPrivateCustomer(PrivateCustomerDto privateCustomerDto);
        void UpdatePrivateCustomer(PrivateCustomerDto privateCustomerDto);
        void DeletePrivateCustomer(string id);
    }
}