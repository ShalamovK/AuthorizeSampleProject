using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Authorize;
using AuthorizeNetSample.Domain.Models.Dtos;

namespace AuthorizeNetSample.Domain.Interfaces.Services {
    public interface IAuthorizeService : IService {
        AuthorizeConfigDto GetConfig();
        void StoreTokens(AuthorizeConfigDto configDto);
        AuthorizePaymentResponse ChargeCreditCard(ChargeCustomerDto charge);
    }
}
