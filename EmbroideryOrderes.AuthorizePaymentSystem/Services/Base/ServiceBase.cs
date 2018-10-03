using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using EmbroideryOrderes.AuthorizePaymentSystem.Common;
using NLog;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Services.Base {
    public class ServiceBase {
        protected readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        protected void Init(AuthorizeEnviromentsEnum enviroment, string appLoginId, string transactionKey) {
            //Setup conection
            switch (enviroment) {
                case AuthorizeEnviromentsEnum.Sandbox:
                    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
                    break;
                case AuthorizeEnviromentsEnum.Production:
                    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
                    break;
            }

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType() {
                name = appLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = transactionKey
            };
        }
    }
}
