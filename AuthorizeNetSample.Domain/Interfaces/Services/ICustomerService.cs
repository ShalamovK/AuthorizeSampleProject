using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Dtos;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Domain.Interfaces.Services {
    public interface ICustomerService : IService {
        CustomerDto GetCustomerWithCardsAndAddresses(Guid customerId);
        List<CustomerDto> GetCustomersList();
        List<CustomerDto> GetAuthorizeCustomersList();
        bool CreateCustomer(ANetCustomerProfileModelDto customer, CustomerAccountResponseDto profileIds);
    }
}
