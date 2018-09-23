using AuthorizeNetSample.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps
{
	public class AddressMap : EntityTypeConfiguration<Address>
	{
		public AddressMap()
		{
			Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(a => a.Street).IsRequired();
			Property(a => a.ZIP).HasMaxLength(6).IsRequired();
		}
	}
}

