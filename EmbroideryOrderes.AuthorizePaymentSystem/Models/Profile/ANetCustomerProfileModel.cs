using System.Collections.Generic;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile {
    /// <summary>
    /// Model for create profile request.<br />
    /// <dl>
    ///     <dt>Required properties:</dt>
    ///     <dd>Email</dd>
    /// </dl>
    /// </summary>
    public class ANetCustomerProfileModel {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public List<PaymentProfileModel> PaymentProfiles { get; set; }

        public ANetCustomerProfileModel() {
            PaymentProfiles = new List<PaymentProfileModel>();
        }
    }
}
