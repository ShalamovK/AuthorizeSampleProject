using AuthorizeNetSample.PaymentSystem.Common;
using AuthorizeNetSample.PaymentSystem.Models.Payment;
using AuthorizeNetSample.PaymentSystem.Requests.Base;
using System.Collections.Generic;

namespace AuthorizeNetSample.PaymentSystem.Requests
{
	public class CreditCardPaymentRequest : BaseRequest
	{
		public decimal Amount { get; set; }
		public CreditCard Card { get; set; }
		public CustomerAddress BillAddress { get; set; }
		public CustomerAddress ShipAddress { get; set; }
		public List<InvoiceLine> LineItems { get; set; }
	}
}
