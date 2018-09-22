using AuthorizeNet;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNetSample.PaymentSystem.Common;
using AuthorizeNetSample.PaymentSystem.Contracts.Base;

namespace AuthorizeNetSample.PaymentSystem.Services.Base
{
	public class BaseService
	{
		internal void Init(IRequest request)
		{
			Environment AuthorizeEnviroment;

			switch (request.Enviroment)
			{
				case PaymentProcessingEnviroments.SANDBOX:
					AuthorizeEnviroment = Environment.SANDBOX;
					break;
				case PaymentProcessingEnviroments.PRODUCTION:
					AuthorizeEnviroment = Environment.PRODUCTION;
					break;
				default:
					AuthorizeEnviroment = Environment.SANDBOX;
					break;
			}

			var Authentication = new merchantAuthenticationType
			{
				name = request.ApiLoginId,
				Item = request.ApiTransactionKey,
				ItemElementName = ItemChoiceType.transactionKey
			};

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeEnviroment;
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = Authentication;
		}
	}
}
