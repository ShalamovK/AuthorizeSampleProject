using AuthorizeNetSample.Common.Contracts;
using AuthorizeNetSample.Common.Models.Payment;
using System;

namespace AuthorizeNetSample.PaymentSystem.Services
{
	public class PaymentService : IPaymentService
	{
		public PaymentResponse MakeCreditCardPayment(CreditCardPaymentRequest creditCardPayment)
		{
			throw new NotImplementedException();
		}
	}
}
