using AuthorizeNetSample.BLL.Services.Base;
using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.DAL.Data.Protection;
using AuthorizeNetSample.Domain.Enums;
using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Authorize;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Repositories;
using AutoMapper;
using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Contracts;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;
using System.Linq;
using System.Web.Configuration;

namespace AuthorizeNetSample.BLL.Services {
    public class AuthorizeService : BaseService, IAuthorizeService {
        private readonly IPaymentService _paymentService;

        public AuthorizeService(IServiceHost serviceHost, IUnitOfWork unitOfWork, IPaymentService paymentService)
            : base(serviceHost, unitOfWork) {
            _paymentService = paymentService;
        }

        public AuthorizeConfigDto GetConfig() {
            AuthorizeConfig config = _unitOfWork.AuthorizeConfig.Get();
            return Mapper.Map<AuthorizeConfigDto>(config);
        }

        public void StoreTokens(AuthorizeConfigDto configDto) {
            AuthorizeConfig config = _unitOfWork.AuthorizeConfig.Get();

            config.AccessToken = configDto.AccessToken;
            config.RefreshToken = configDto.RefreshToken;
            config.AccesssTokenExpiresIn = configDto.AccesssTokenExpiresIn;
            config.RefreshTokenExpiresIn = configDto.RefreshTokenExpiresIn;

            _unitOfWork.SaveChanges();
        }

        public AuthorizePaymentResponse ChargeCreditCard(ChargeCustomerDto charge) {
            CustomerDto customer = _serviceHost.GetService<ICustomerService>().GetCustomerWithCardsAndAddresses(charge.CustomerId);

            if (customer == null) return null;

            CreditCardDto card = customer.CreditCards.FirstOrDefault(cc => cc.Id == charge.CreditCardId);

            if (card == null) return null;

            AddressDto cardBillingAddress = card.BillingAddresses.FirstOrDefault();

            if (cardBillingAddress == null) return null;

            ANetAddressModel chargeAddress = new ANetAddressModel {
                Address = cardBillingAddress.Street,
                Zip = cardBillingAddress.ZIP,
                City = cardBillingAddress.City,
                Country = cardBillingAddress.Country
            };

            CreditCardPayment payment = new CreditCardPayment {
                Card = new ANetCardModel {
                    Number = DataEncryptor.Decrypt(card.CardNumHash),
                    ExpirationDate = card.ExpDate
                },
                Amount = charge.Amount,
                BillAddress = chargeAddress,
                ShipAddress = chargeAddress,
                CustomerPO = charge.PaymentNote
            };

            AuthorizeConfigDto authorizeConfig = GetConfig();
            ANetResponse<PaymentResponse> response;

            switch (charge.AuthenticationType) {
                case MerchantAuthenticationType.AccessToken:
                    response = _paymentService.MakePayment(payment, authorizeConfig.AccessToken, AuthorizeEnviromentsEnum.Sandbox);
                    break;
                case MerchantAuthenticationType.TransactionKey:
                    string AppLoginId = WebConfigurationManager.AppSettings["AuthorizeApiLoginId"];
                    string TransactionKey = WebConfigurationManager.AppSettings["AuthorizeTransactionKey"];
                    response = _paymentService.MakePayment(payment, AppLoginId, TransactionKey, AuthorizeEnviromentsEnum.Sandbox);
                    break;
                default: response = null;
                    break;
            }

            if (!response.IsSuccessful) return null;

            return Mapper.Map<AuthorizePaymentResponse>(response.ResponseObject);
        }
    }
}
