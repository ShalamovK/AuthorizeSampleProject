using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Contracts;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;
using EmbroideryOrderes.AuthorizePaymentSystem.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Services {
    public class CustomerProfileService : ServiceBase, ICustomerProfileService {
        #region [ Private Methods ]

        private ANetResponse<CustomerProfileResponse> _ProcessCustomerProfileResponse(ANetResponse<CustomerProfileResponse> response,
            createCustomerProfileResponse apiResponse, string loggerMessage) {
            if (apiResponse != null && apiResponse.messages.resultCode == messageTypeEnum.Ok) {
                if (response != null && apiResponse.messages.message != null) {
                    response.IsSuccessful = true;
                    response.Message = "Successfully created customer profile from transaction";
                    response.ResponseObject = new CustomerProfileResponse {
                        CustomerProfileId = apiResponse.customerProfileId,
                        CustomerPaymentProfileIds = apiResponse.customerPaymentProfileIdList.ToList(),
                    };
                }
            } else if (response != null) {
                response.Message = "Error: " + apiResponse.messages.message[0].code + "  " + apiResponse.messages.message[0].text;
            }

            return response;
        }

        private ANetResponse<CustomerProfileResponse> _ProcessCustomerPaymentProfileResponse(ANetResponse<CustomerProfileResponse> response,
            createCustomerPaymentProfileResponse apiResponse, string loggerMessage) {
            if (apiResponse != null && apiResponse.messages.resultCode == messageTypeEnum.Ok) {
                if (response != null && apiResponse.messages.message != null) {
                    response.IsSuccessful = true;
                    response.Message = "Successfully created customer profile from transaction";
                    response.ResponseObject = new CustomerProfileResponse {
                        CustomerProfileId = apiResponse.customerProfileId,
                        CustomerPaymentProfileIds = new List<string> { apiResponse.customerPaymentProfileId }
                    };
                }
            } else if (response != null) {
                response.Message = "Error: " + apiResponse.messages.message[0].code + "  " + apiResponse.messages.message[0].text;
            }

            return response;
        }

        #endregion
        #region [ Public Methods ]

        public ANetResponse<CustomerProfileResponse> CreateCustomerProfile(ANetCustomerProfileModel model, string appLoginId, string transactionKey,
            AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Add customer profile for customer: {model.Email} Result: "; //Init base logger message
            ANetResponse<CustomerProfileResponse> response = new ANetResponse<CustomerProfileResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            var customerProfile = new customerProfileType {
                merchantCustomerId = model.Name,
                email = model.Email,
            };

            if (model.PaymentProfiles.Any()) {
                customerProfile.paymentProfiles = new customerPaymentProfileType[model.PaymentProfiles.Count];

                for (int i = 0; i < model.PaymentProfiles.Count; i++) {
                    PaymentProfileModel profileModel = model.PaymentProfiles[i];

                    customerProfile.paymentProfiles[i] = new customerPaymentProfileType {
                        payment = new paymentType {
                            Item = new creditCardType {
                                cardNumber = profileModel.CreditCard.Number,
                                expirationDate = profileModel.CreditCard.ExpirationDate
                            }
                        },
                        billTo = new customerAddressType {
                            address = profileModel.BillTo.Address,
                            city = profileModel.BillTo.City,
                            company = profileModel.BillTo.Company,
                            country = profileModel.BillTo.Country,
                            email = profileModel.BillTo.Email,
                            zip = profileModel.BillTo.Zip,
                            phoneNumber = profileModel.BillTo.PhoneNumber,
                            state = profileModel.BillTo.State,
                            firstName = profileModel.BillTo.FirstName,
                            lastName = profileModel.BillTo.LastName
                        }
                    };
                }
            }

            var request = new createCustomerProfileRequest {
                profile = customerProfile,
                validationMode = validationModeEnum.none
            };

            var controller = new createCustomerProfileController(request);
            var apiResponse = controller.ExecuteWithApiResponse();

            return _ProcessCustomerProfileResponse(response, apiResponse, loggerMessage);
        }

        public ANetResponse<CustomerProfileResponse> CreateCustomerProfileFromTransaction(string transactionId, ANetCustomerProfileModel profileModel,
            string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Add customer profile from payment: {transactionId} Result: "; //Init base logger message
            ANetResponse<CustomerProfileResponse> response = new ANetResponse<CustomerProfileResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            var customerProfile = new customerProfileBaseType {
                email = profileModel.Email,
            };

            var request = new createCustomerProfileFromTransactionRequest {
                transId = transactionId,
                customer = customerProfile
            };

            var controller = new createCustomerProfileFromTransactionController(request);
            controller.Execute();
            createCustomerProfileResponse apiResponse = controller.GetApiResponse();

            return _ProcessCustomerProfileResponse(response, apiResponse, loggerMessage);
        }

        public ANetResponse<CustomerProfileResponse> CreateCustomerPaymentProfile(ANetCardModel cardModel, ANetAddressModel billAddress, string profileId,
            string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {

            string loggerMessage = $"Add customer payment profile to profile: {profileId} Result: "; //Init base logger message
            ANetResponse<CustomerProfileResponse> response = new ANetResponse<CustomerProfileResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            var creditCard = new creditCardType {
                cardNumber = cardModel.Number,
                expirationDate = cardModel.ExpirationDate
            };

            var billingAddress = new customerAddressType {
                firstName = billAddress.FirstName,
                lastName = billAddress.LastName,
                address = billAddress.Address,
                city = billAddress.City,
                zip = billAddress.Zip,
                company = billAddress.Company,
            };

            var paymentType = new paymentType { Item = creditCard };

            var paymentProfile = new customerPaymentProfileType {
                payment = paymentType,
                billTo = billingAddress
            };

            var request = new createCustomerPaymentProfileRequest {
                customerProfileId = profileId,
                paymentProfile = paymentProfile,
                validationMode = validationModeEnum.none //This should be set to live if we want to use profile for payments.
            };

            var controller = new createCustomerPaymentProfileController(request);
            createCustomerPaymentProfileResponse ApiResponse = controller.ExecuteWithApiResponse();

            return _ProcessCustomerPaymentProfileResponse(response, ApiResponse, loggerMessage);
        }

        #endregion
    }
}
