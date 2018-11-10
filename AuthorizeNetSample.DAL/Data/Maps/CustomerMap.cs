using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps
{
	public class CustomerMap : EntityTypeConfiguration<Customer>
	{
		public CustomerMap()
		{
            HasKey(c => c.Id);
			Property(c => c.FirstName).IsRequired();
			Property(c => c.LastName).IsRequired();
            HasMany(c => c.Addresses).WithOptional(a => a.Customer);
		}
	}
}
