using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps
{
	public class AddressMap : EntityTypeConfiguration<Address>
	{
		public AddressMap()
		{
            HasKey(a => a.Id);
			Property(a => a.Street).IsRequired();
			Property(a => a.ZIP).HasMaxLength(6).IsRequired();
		}
	}
}

