namespace AuthorizeNetSample.Common.Models.Payment
{
	public class PaymentAuthentication
	{
		public PaymentAuthentication() { }
		public PaymentAuthentication(string apiLoginId, string apiTransactionKey)
		{
			ApiLoginId = apiLoginId;
			ApiTransactionKey = apiTransactionKey;
		}

		public readonly string ApiLoginId;
		public readonly string ApiTransactionKey;
	}
}
