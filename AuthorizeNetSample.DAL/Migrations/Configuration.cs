namespace AuthorizeNetSample.DAL.Migrations
{
	using AuthorizeNetSample.DAL.Data.Context;
	using AuthorizeNetSample.DAL.Data.Entity;
	using AuthorizeNetSample.DAL.Data.Protection;
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<AuthorizeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		private bool saveChanges = false;
		private AuthorizeDbContext dbContext;

		protected override void Seed(AuthorizeDbContext context)
        {
			dbContext = context;
			_SeedCustomers();
			_SaveChanges();
        }

		private void _SeedCustomers()
		{
			if (dbContext.Customers.Any()) return;

			string newCardNumber = "4929399657543118";
			string lastFourDigits = newCardNumber.Substring(newCardNumber.Length - 5);
			string cardNumHash = DataEncryptor.Encrypt(newCardNumber);

			CreditCard card = new CreditCard
			{
				LastFourDigits = lastFourDigits,
				CardNumHash = cardNumHash,
				ExpDate = "0822",
				FirstName = "John",
				LastName = "Doe",
			};

			Address address = new Address
			{
				Street = "12th Jason ave",
				City = "Orange park",
				State = "FL",
				Phone = "23094587",
				ZIP = "33312",
				Country = "United States"
			};

			Customer customer = new Customer
			{
				FirstName = "John",
				LastName = "Doe",
				DateAdded = DateTime.Now,
			};

			customer.Addresses.Add(address);
			customer.CreditCards.Add(card);

			dbContext.Customers.Add(customer);

			saveChanges = true;
		}

		private void _SaveChanges()
		{
			if (saveChanges) dbContext.SaveChanges();
		}
	}
}
