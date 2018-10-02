using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.DAL.Data.Maps;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;

namespace AuthorizeNetSample.DAL.Data.Context
{
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AddressMap());
			modelBuilder.Configurations.Add(new CreditCardMap());
			modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new BillingAddressMap());
        }

        public override int SaveChanges() {
            try {
                return base.SaveChanges();
            } catch (DbEntityValidationException ex) {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}
