using AuthorizeNetSample.PaymentSystem.Common;

namespace AuthorizeNetSample.PaymentSystem.Contracts.Base
{
	public interface IRequest
	{
		PaymentProcessingEnviroments Enviroment { get; set; }
		string ApiLoginId { get; set; }
		string ApiTransactionKey { get; set; }
	}
}
