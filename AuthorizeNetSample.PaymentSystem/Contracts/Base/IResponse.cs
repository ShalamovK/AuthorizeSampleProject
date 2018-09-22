namespace AuthorizeNetSample.PaymentSystem.Contracts.Base
{
	public interface IResponse
	{
		bool Success { get; set; }
		string Message { get; set; }
		string TransactionId { get; set; }
		string AuthKey { get; set; }
	}
}
