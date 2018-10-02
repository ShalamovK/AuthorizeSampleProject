using AuthorizeNetSample.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps
{
	public class CreditCardMap : EntityTypeConfiguration<CreditCard>
	{
		public CreditCardMap()
		{
			Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(c => c.LastFourDigits).HasMaxLength(4);
			Property(c => c.ExpDate).HasMaxLength(4);
			HasRequired(c => c.Customer).WithMany(cust => cust.CreditCards);
            HasOptional(c => c.BillingAddress).WithRequired(a => a.CreditCard);
		}
	}
}
