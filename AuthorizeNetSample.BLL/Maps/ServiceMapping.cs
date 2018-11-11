using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.Domain.Models.Authorize;
using AuthorizeNetSample.Domain.Models.Dtos;
using AutoMapper;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;

namespace AuthorizeNetSample.BLL.Maps {
    public class ServiceMapping : Profile {
        public ServiceMapping() {
            CreateMap<AuthorizeConfig, AuthorizeConfigDto>();
            CreateMap<Customer, CustomerDto>().
                ForMember(trg => trg.Addresses, opt => opt.MapFrom(src => src.Addresses)).
                ForMember(trg => trg.CreditCards, opt => opt.MapFrom(src => src.CreditCards)).
                ForMember(trg => trg.Payments, opt => opt.MapFrom(src => src.Payments)).
                ReverseMap();
            CreateMap<CreditCard, CreditCardDto>().
                ForMember(trg => trg.Customer, opt => opt.MapFrom(src => src.Customer)).
                ForMember(trg => trg.BillingAddresses, opt => opt.MapFrom(src => src.BillingAddresses)).
                ReverseMap();
            CreateMap<Address, AddressDto>().
                ForMember(trg => trg.Customer, opt => opt.MapFrom(src => src.Customer)).
                ForMember(trg => trg.CreditCard, opt => opt.MapFrom(src => src.CreditCard)).
                ReverseMap();
            CreateMap<Payment, PaymentDto>().
                 ForMember(trg => trg.Customer, opt => opt.MapFrom(src => src.Customer)).
                 ReverseMap();
            CreateMap<PaymentResponse, AuthorizePaymentResponse>();
        }
    }
}
