using AuthorizeNetSample.Common.Models.Payment;

namespace AuthorizeNetSample.Common.Contracts
{
	public interface IPaymentService
	{
		PaymentResponse MakeCreditCardPayment(CreditCardPaymentRequest creditCardPayment);
	}
}
