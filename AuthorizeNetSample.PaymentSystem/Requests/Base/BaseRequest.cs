using AuthorizeNetSample.PaymentSystem.Common;
using AuthorizeNetSample.PaymentSystem.Contracts.Base;
using System;

namespace AuthorizeNetSample.PaymentSystem.Requests.Base
{
	public class BaseRequest : IRequest
	{
		public PaymentProcessingEnviroments Enviroment { get; set; }
		public string ApiLoginId { get; set; }
		public string ApiTransactionKey { get; set; }
	}
}
