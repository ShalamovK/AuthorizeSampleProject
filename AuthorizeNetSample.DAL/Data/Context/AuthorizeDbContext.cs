using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.DAL.Data.Maps;
using System.Data.Entity;

namespace AuthorizeNetSample.DAL.Data.Context {
    public class AuthorizeDbContext : DbContext
	{
		public AuthorizeDbContext() : base("AuthorizeDbConnection")
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<AuthorizeConfig> AuthorizeConfig { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AddressMap());
			modelBuilder.Configurations.Add(new CreditCardMap());
			modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new BillingAddressMap());
            modelBuilder.Configurations.Add(new AuthorizeConfigMap());
        }
    }
}
