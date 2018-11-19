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
using AuthorizeNetSample.DAL.Data.Protection;

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

        public List<CustomerDto> GetAuthorizeCustomersList() {
            List<Customer> customers = _unitOfWork.GetRepository<Customer>().All.
                Where(c => c.AuthorizeId != null && c.CreditCards.Any(cc => cc.AuthorizeId != null)).ToList();

            return Mapper.Map<List<CustomerDto>>(customers);
        }

        public bool CreateCustomer(ANetCustomerProfileModelDto customer, CustomerAccountResponseDto profileIds) {
            Customer newCustomer = new Customer {
                Id = Guid.NewGuid(),
                AuthorizeId = profileIds.CustomerId,
                FirstName = customer.PaymentProfiles?.FirstOrDefault()?.BillTo?.FirstName,
                LastName = customer.PaymentProfiles?.FirstOrDefault()?.BillTo?.LastName,
                DateAdded = DateTime.Now
            };

            if (customer.Shippings.Any()) {
                ANetAddressModelDto ship = customer.Shippings.First();

                Address shipAddress = new Address {
                    CustomerId = newCustomer.Id,
                    AuthorizeId = profileIds.ShippingProfiles.FirstOrDefault(),
                    DateAdded = DateTime.Now,
                    City = ship.City,
                    Country = ship.Country,
                    State = ship.State,
                    Street = ship.Address,
                    ZIP = ship.Zip,
                    Id = Guid.NewGuid()
                };

                _unitOfWork.GetRepository<Address>().Add(shipAddress);
            }

            if (customer.PaymentProfiles.Any()) {
                PaymentProfileModelDto pp = customer.PaymentProfiles.First();

                CreditCard card = new CreditCard {
                    Id = Guid.NewGuid(),
                    CardNumHash = DataEncryptor.Encrypt(pp.CreditCard.Number),
                    AuthorizeId = profileIds.PaymentProfiles.FirstOrDefault(),
                    ExpDate = pp.CreditCard.ExpirationDate,
                    DateAdded = DateTime.Now,
                    LastFourDigits = pp.CreditCard.Number.Substring(pp.CreditCard.Number.Length - 4),
                    FirstName = pp.BillTo.FirstName,
                    LastName = pp.BillTo.LastName,
                    CustomerId = newCustomer.Id
                };

                Address billAddress = new Address {
                    Id = Guid.NewGuid(),
                    City = pp.BillTo.City,
                    Country = pp.BillTo.Country,
                    DateAdded = DateTime.Now,
                    ZIP = pp.BillTo.Zip,
                    State = pp.BillTo.State,
                    Street = pp.BillTo.Address,
                    CreditCardId = card.Id
                };

                _unitOfWork.GetRepository<Address>().Add(billAddress);
                _unitOfWork.GetRepository<CreditCard>().Add(card);
            }

            _unitOfWork.GetRepository<Customer>().Add(newCustomer);
            _unitOfWork.SaveChanges();

            return true;
        }
    }
}
