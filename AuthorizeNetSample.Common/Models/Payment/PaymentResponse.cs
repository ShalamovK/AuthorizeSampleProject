namespace AuthorizeNetSample.Common.Models.Payment
{
	public class PaymentResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public string TransactionId { get; set; }
		public string AuthKey { get; set; }
	}
}
