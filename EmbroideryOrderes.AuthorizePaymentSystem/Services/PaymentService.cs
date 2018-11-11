using System.Collections.Generic;
using System.Linq;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Contracts;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;
using EmbroideryOrderes.AuthorizePaymentSystem.Services.Base;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Services {
    public class PaymentService : ServiceBase, IPaymentService {
        #region [ Constructor ]

        public PaymentService() {
        }

        #endregion
        #region [ Private Methods ]

        private ANetResponse<PaymentResponse> _ProcessPaymentResponse(ANetResponse<PaymentResponse> response, createTransactionResponse apiResponse, string loggerMessage) {
            if (apiResponse == null) {
                response.Message = "Null Api response";

                _logger.Error(loggerMessage + response.Message);
                return response;
            }

            if (apiResponse.messages.resultCode == messageTypeEnum.Ok) {
                if (apiResponse.messages != null) {
                    response.IsSuccessful = true;
                    response.Message = "Successfully created transaction with Transaction ID: " + apiResponse.transactionResponse.transId;
                    response.ResponseObject = new PaymentResponse {
                        TransactionId = apiResponse.transactionResponse.transId,
                        AuthCode = apiResponse.transactionResponse.authCode,
                        PaddedCardNum = apiResponse.transactionResponse.accountNumber
                    };

                    string loggerAddMessage = response.Message + " "
                                            + "Response Code: " + apiResponse.transactionResponse.responseCode + " "
                                            + "Message Code: " + apiResponse.transactionResponse.messages[0].code + " "
                                            + "Description: " + apiResponse.transactionResponse.messages[0].description + " "
                                            + "Success, Auth Code : " + apiResponse.transactionResponse.authCode;

                    _logger.Info(loggerMessage + loggerAddMessage);
                } else {
                    if (apiResponse.transactionResponse.errors != null) {
                        response.Message = "Error Code: " + apiResponse.transactionResponse.errors[0].errorCode + " "
                                         + "Error message: " + apiResponse.transactionResponse.errors[0].errorText;
                    } else {
                        response.Message = "Transaction failed";
                    }

                    _logger.Error(loggerMessage + response.Message);
                }
            } else {
                if (apiResponse.transactionResponse?.errors != null) {
                    response.Message = "Error Code: " + apiResponse.transactionResponse.errors[0].errorCode + " "
                                         + "Error message: " + apiResponse.transactionResponse.errors[0].errorText;
                } else {
                    response.Message = "Error Code: " + apiResponse.messages.message[0].code + " "
                                         + "Error message: " + apiResponse.messages.message[0].text;
                }

                _logger.Error(loggerMessage + response.Message);
            }

            return response;
        }

        private ANetResponse<PaymentResponse> _ProcessPaymentHelper(CreditCardPayment model, ANetResponse<PaymentResponse> response, string loggerMessage) {
            creditCardType creditCard = new creditCardType {
                cardNumber = model.Card.Number,
                expirationDate = model.Card.ExpirationDate
            };

            //Setup Payment Info
            customerAddressType billingAddr = new customerAddressType {
                firstName = model.BillAddress.FirstName,
                lastName = model.BillAddress.LastName,
                address = model.BillAddress.Address,
                city = model.BillAddress.City,
                zip = model.BillAddress.Zip,
                company = model.BillAddress.Company,
            };

            nameAndAddressType shippingAddr = new nameAndAddressType {
                address = model.ShipAddress.Address,
                city = model.ShipAddress.City,
                zip = model.ShipAddress.Zip,
                company = model.ShipAddress.Company,
                country = model.ShipAddress.Country,
                state = model.ShipAddress.State
            };

            var transactionOrderDetails = new orderType {
                invoiceNumber = model.OrderNum,
                description = "Payment for Invoice"
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),

                amount = model.Amount,
                payment = paymentType,
                billTo = billingAddr,
                shipTo = shippingAddr,
                order = transactionOrderDetails,
                poNumber = model.CustomerPO
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            //Create transaction controller
            var controller = new createTransactionController(request);

            //Execute transaction
            controller.Execute();

            //Get response for transaction
            createTransactionResponse apiResponse = controller.GetApiResponse();

            return _ProcessPaymentResponse(response, apiResponse, loggerMessage);
        }

        #endregion
        #region [ Payment Methods]

        public ANetResponse<PaymentResponse> MakePayment(CreditCardPayment model, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Credit card payment. Customer: {model.BillAddress.FirstName} {model.BillAddress.LastName} Result: "; //Init base logger message
            ANetResponse<PaymentResponse> response = new ANetResponse<PaymentResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            return _ProcessPaymentHelper(model, response, loggerMessage);
        }

        public ANetResponse<PaymentResponse> MakePayment(CreditCardPayment model, string accessToken, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Credit card payment. Customer: {model.BillAddress.FirstName} {model.BillAddress.LastName} Result: "; //Init base logger message
            ANetResponse<PaymentResponse> response = new ANetResponse<PaymentResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, accessToken);

            return _ProcessPaymentHelper(model, response, loggerMessage);
        }

            public ANetResponse<PaymentResponse> MakePaymentByProfile(ProfilePayment payment, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Profile based payment. Customer profile Id: {payment.CustomerProfileModel.Id} Result: "; //Init base logger message
            ANetResponse<PaymentResponse> response = new ANetResponse<PaymentResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            var profileToCharge = new customerProfilePaymentType {
                customerProfileId = payment.CustomerProfileModel.Id,
                paymentProfile = new paymentProfile {
                    paymentProfileId = payment.CustomerProfileModel.PaymentProfiles.First().Id
                }
            };

            nameAndAddressType shippingAddr = new nameAndAddressType {
                address = payment.ShipAddress.Address,
                city = payment.ShipAddress.City,
                zip = payment.ShipAddress.Zip,
                company = payment.ShipAddress.Company,
                country = payment.ShipAddress.Country,
                state = payment.ShipAddress.State
            };

            var order = new orderType {
                invoiceNumber = payment.OrderNum,
                description = "Payment for Invoice"
            };

            var requestType = new transactionRequestType {
                amount = payment.Amount,
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                profile = profileToCharge,
                shipTo = shippingAddr,
                poNumber = payment.CustomerPO,
                order = order
            };

            var request = new createTransactionRequest { transactionRequest = requestType };
            var controller = new createTransactionController(request);
            var apiResponse = controller.ExecuteWithApiResponse();

            return _ProcessPaymentResponse(response, apiResponse, loggerMessage);
        }

        public ANetResponse<PaymentResponse> RefundCreditCard(ANetRefundModel model, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Credit card refund. Customer: {model.BillAddress.FirstName} {model.BillAddress.LastName} Result: "; //Init base logger message
            ANetResponse<PaymentResponse> response = new ANetResponse<PaymentResponse> { IsSuccessful = false, Message = "" };

            Init(enviroment, appLoginId, transactionKey);

            ANetPaymentCharge aNetTransaction = GetCharge(model.TransactionId, appLoginId, transactionKey, enviroment);

            creditCardType creditCard = new creditCardType {
                cardNumber = model.Card.Number,
                expirationDate = model.Card.ExpirationDate
            };

            paymentType paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),
                payment = paymentType,
                amount = model.Amount,
                refTransId = model.TransactionId
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            //Create transaction controller
            var controller = new createTransactionController(request);

            //Execute transaction
            controller.Execute();

            //Get response for transaction
            createTransactionResponse apiResponse = controller.GetApiResponse();

            return _ProcessPaymentResponse(response, apiResponse, loggerMessage);
        }

        public List<ANetPaymentCharge> GetUnsettledPayments(string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Get charges. Result: "; //Init base logger message

            Init(enviroment, appLoginId, transactionKey);
            int requestItemsLimit = 1000;

            var transactionListRequest = new getUnsettledTransactionListRequest {
                paging = new Paging {
                    limit = requestItemsLimit,
                    offset = 1
                },
                sorting = new TransactionListSorting {
                    orderBy = TransactionListOrderFieldEnum.submitTimeUTC,
                    orderDescending = true
                }
            };

            var controller = new getUnsettledTransactionListController(transactionListRequest);

            controller.Execute();

            var response = controller.GetApiResponse();

            if (response == null) {
                _logger.Error(loggerMessage + "Null response");
                return null;
            }

            if (response.messages.resultCode == messageTypeEnum.Ok) {
                if (response.transactions != null) {
                    return response.transactions.
                        Select(t => new ANetPaymentCharge {
                            Id = t.transId,
                            Status = t.transactionStatus,
                            Amount = t.settleAmount,
                            Created = t.submitTimeUTC,
                            CardNumber = t.accountNumber,
                            FirstName = t.firstName,
                            LastName = t.lastName
                        }).ToList();
                } else {
                    return null;
                }
            } else {
                string message = response.messages.message[0]?.text ?? "Error";
                _logger.Error(loggerMessage + message);
                return null;
            }
        }

        public List<ANetPaymentCharge> GetCustomerCharges(string customerId, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment,
            string paymentProfileId = null) {
            string loggerMessage = $"Get customer charges. customerId:{customerId} Result: "; //Init base logger message

            Init(enviroment, appLoginId, transactionKey);

            var request = new getTransactionListForCustomerRequest {
                customerProfileId = customerId,
                customerPaymentProfileId = paymentProfileId ?? null,
                paging = new Paging {
                    limit = 1000,
                    offset = 1
                },
                sorting = new TransactionListSorting {
                    orderBy = TransactionListOrderFieldEnum.submitTimeUTC,
                    orderDescending = true
                }
            };

            var controller = new getTransactionListForCustomerController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            if (response == null) {
                _logger.Error(loggerMessage + "Null response");
                return null;
            }

            if (response.messages.resultCode == messageTypeEnum.Ok) {
                if (response.transactions == null) {
                    return null;
                }

                return response.transactions.
                        Select(t => new ANetPaymentCharge {
                            Id = t.transId,
                            Status = t.transactionStatus,
                            Amount = t.settleAmount,
                            Created = t.submitTimeUTC,
                            CardNumber = t.accountNumber,
                            FirstName = t.firstName,
                            LastName = t.lastName
                        }).ToList();
            } else {
                string message = response.messages.message[0]?.text ?? "Error";
                _logger.Error(loggerMessage + message);
                return null;
            }
        }

        public ANetPaymentCharge GetCharge(string chargeId, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment) {
            string loggerMessage = $"Get charge. Result: "; //Init base logger message

            Init(enviroment, appLoginId, transactionKey);

            var transactionDetailsRequest = new getTransactionDetailsRequest {
                transId = chargeId
            };

            var controller = new getTransactionDetailsController(transactionDetailsRequest);

            controller.Execute();

            var response = controller.GetApiResponse();

            if (response == null) {
                _logger.Error(loggerMessage + "Null response");
                return null;
            }

            if (response.messages.resultCode == messageTypeEnum.Ok) {
                if (response.transaction != null) {
                    return new ANetPaymentCharge {
                        Id = response.transaction.transId,
                        Amount = response.transaction.settleAmount,
                        Created = response.transaction.submitTimeUTC,
                        Status = response.transaction.transactionStatus
                    };
                } else {
                    return null;
                }
            } else {
                string message = response.messages.message[0]?.text ?? "Error";
                _logger.Error(loggerMessage + message);
                return null;
            }
        }

        #endregion
    }
}
