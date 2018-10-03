using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Contracts {
    public interface ICustomerProfileService {
        ANetResponse<CustomerProfileResponse> CreateCustomerProfileFromTransaction(string transactionId,
            ANetCustomerProfileModel profileModel, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);

        ANetResponse<CustomerProfileResponse> CreateCustomerPaymentProfile(ANetCardModel cardModel, ANetAddressModel billAddress, string profileId,
            string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);

        ANetResponse<CustomerProfileResponse> CreateCustomerProfile(ANetCustomerProfileModel model, string appLoginId, string transactionKey,
            AuthorizeEnviromentsEnum enviroment);
    }
}
