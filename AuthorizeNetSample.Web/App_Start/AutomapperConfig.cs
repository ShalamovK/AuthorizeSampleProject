using AuthorizeNetSample.Domain.Models.Authorize;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Web.Models;
using AuthorizeNetSample.Web.Models.Authorize;
using AutoMapper;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile;

namespace AuthorizeNetSample.Web.App_Start {
    public class AutomapperConfig : Profile {
        public AutomapperConfig() {
            CreateMap<AuthorizeConfigDto, GetRequestTokenPageViewModel>().
                ForMember(trg => trg.OAuthUrl, opt => opt.Ignore()).
                ForMember(trg => trg.Scope, opt => opt.Ignore()).
                ForMember(trg => trg.State, opt => opt.Ignore()).
                ForMember(trg => trg.Sub, opt => opt.Ignore());
            CreateMap<ChargeCustomerViewModel, ChargeCustomerDto>().
                ForMember(trg => trg.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));
            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
            CreateMap<CreditCardDto, CreditCardViewModel>().
                ForMember(trg => trg.CardNum, opt => opt.Ignore()).
                ReverseMap();
            CreateMap<AddressDto, AddressViewModel>().ReverseMap();
            CreateMap<PaymentDto, PaymentViewModel>().ReverseMap();
            CreateMap<EncryptVisaCheckoutDataViewModel, EncryptVisaCheckoutDataDto>();
            CreateMap<DecryptedVisaCheckoutDataDto, DecryptedVisaCheckoutDataViewModel>();
            CreateMap<CardInfoDto, CardInfoViewModel>();
            CreateMap<ShippingInfoDto, ShippingInfoViewModel>();
            CreateMap<BillingInfoDto, BillingInfoViewModel>();
            CreateMap<PaymentDetailsDto, PaymentDetailsViewModel>();
            CreateMap<ANetPaymentProfileInfo, CustomerPaymentProfileInfoViewModel>();
            CreateMap<CustomerAccountResponseViewModel, CustomerAccountResponseDto>();
            CreateMap<ANetCustomerProfileModel, ANetCustomerProfileModelDto>();
            CreateMap<PaymentProfileModel, PaymentProfileModelDto>();
            CreateMap<ANetAddressModel, ANetAddressModelDto>();
            CreateMap<ANetCardModel, ANetCardModelDto>();
        }
    }
}