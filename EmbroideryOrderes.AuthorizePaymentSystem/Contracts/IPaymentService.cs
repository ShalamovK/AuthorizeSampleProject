using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses;
using EmbroideryOrderes.AuthorizePaymentSystem.Responses.Base;
using System.Collections.Generic;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Contracts {
    /// <summary>
    /// This interface provides access to payments through Authorize.Net Api
    /// </summary>
    public interface IPaymentService {
        ANetResponse<PaymentResponse> MakePayment(CreditCardPayment model, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);
        ANetResponse<PaymentResponse> MakePayment(CreditCardPayment model, string accessToken, AuthorizeEnviromentsEnum enviroment);
        ANetResponse<PaymentResponse> MakePaymentByProfile(ProfilePayment payment, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);
        ANetResponse<PaymentResponse> RefundCreditCard(ANetRefundModel model, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);
        List<ANetPaymentCharge> GetUnsettledPayments(string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);
        ANetPaymentCharge GetCharge(string chargeId, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment);
        List<ANetPaymentCharge> GetCustomerCharges(string customerId, string appLoginId, string transactionKey, AuthorizeEnviromentsEnum enviroment, string paymentProfileId = null);
    }
}
