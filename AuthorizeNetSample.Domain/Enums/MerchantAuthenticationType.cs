using System.ComponentModel;

namespace AuthorizeNetSample.Domain.Enums {
    public enum MerchantAuthenticationType {
        [Description("Transaction Key")]
        TransactionKey,
        [Description("Access Token")]
        AccessToken
    }
}
