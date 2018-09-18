using AuthorizeNetSample.Common.Models.Payment;

namespace AuthorizeNetSample.Common.Contracts
{
	public interface IPaymentService
	{
		PaymentResponse ProcessCreditCardPayment(CreditCardPaymentRequest creditCardPayment);
	}
}
