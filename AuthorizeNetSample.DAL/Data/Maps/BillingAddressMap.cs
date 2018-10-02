using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps {
    public class BillingAddressMap : EntityTypeConfiguration<BillingAddress> {
        public BillingAddressMap() {
            HasRequired(a => a.CreditCard).WithOptional(c => c.BillingAddress);
        }
    }
}