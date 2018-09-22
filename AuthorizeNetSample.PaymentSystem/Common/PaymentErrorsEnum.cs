using System.ComponentModel;

namespace AuthorizeNetSample.PaymentSystem.Common
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
