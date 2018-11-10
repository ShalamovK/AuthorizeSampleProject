using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps
{
	public class CreditCardMap : EntityTypeConfiguration<CreditCard>
	{
		public CreditCardMap()
		{
            HasKey(c => c.Id);
			Property(c => c.LastFourDigits).HasMaxLength(4);
			Property(c => c.ExpDate).HasMaxLength(4);
			HasRequired(c => c.Customer).WithMany(cust => cust.CreditCards);
            HasMany(c => c.BillingAddresses).WithOptional(a => a.CreditCard);
		}
	}
}
