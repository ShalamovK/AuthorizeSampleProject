using System.ComponentModel;

namespace AuthorizeNetSample.PaymentSystem.Errors
{
	public enum PaymentErrorsEnum
	{
		[Description("Credit card not found")]
		CreditCardNotFound,
		[Description("Null response")]
		NullResponse,
		[Description("Transaction failed")]
		TransactionFailed
	}
}
