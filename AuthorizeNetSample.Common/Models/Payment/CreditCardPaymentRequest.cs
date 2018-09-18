using System.Collections.Generic;

namespace AuthorizeNetSample.Common.Models.Payment
{
	public class CreditCardPaymentRequest
	{
		public decimal Amount { get; set; }
		public CreditCard Card { get; set; }
		public CustomerAddress BillAddress { get; set; }
		public CustomerAddress ShipAddress { get; set; }
		public List<InvoiceLine> LineItems { get; set; } 
		public PaymentAuthentication Authentication { get; set; }
	}
}
