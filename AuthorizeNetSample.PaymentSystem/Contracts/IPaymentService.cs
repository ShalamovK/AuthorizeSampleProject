using AuthorizeNetSample.PaymentSystem.Models.Payment;
using AuthorizeNetSample.PaymentSystem.Requests;

namespace AuthorizeNetSample.PaymentSystem.Contracts
{
	public interface IPaymentService
	{
		PaymentResponse ProcessCreditCardPayment(CreditCardPaymentRequest creditCardPayment);
	}
}
