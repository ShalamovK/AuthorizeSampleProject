using AuthorizeNetSample.BLL.Services.Base;
using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Repositories;
using System;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using System.Collections.Generic;

namespace AuthorizeNetSample.BLL.Services {
    public class CustomerService : BaseService, ICustomerService {
        public CustomerService(IServiceHost serviceHost, IUnitOfWork unitOfWork) 
            : base (serviceHost, unitOfWork) { }

        public CustomerDto GetCustomerWithCardsAndAddresses(Guid customerId) {
            Customer customer = _unitOfWork.GetRepository<Customer>().All.
                Where(c => c.Id == customerId).
                Include(c => c.CreditCards).
                Include(c => c.Addresses).
                FirstOrDefault();

            if (customer == null) return null;

            return Mapper.Map<CustomerDto>(customer);
        }

        public List<CustomerDto> GetCustomersList() {
            List<Customer> customers = _unitOfWork.GetRepository<Customer>().All.ToList();

            return Mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
