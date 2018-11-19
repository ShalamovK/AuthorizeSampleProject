namespace AuthorizeNetSample.DAL.Migrations
{
	using AuthorizeNetSample.DAL.Data.Context;
	using AuthorizeNetSample.DAL.Data.Entity;
	using AuthorizeNetSample.DAL.Data.Protection;
	using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<AuthorizeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(AuthorizeDbContext context)
        {
			_SeedCustomers(context);
            _SeedAuthorizeConfig(context);
        }

		private void _SeedCustomers(AuthorizeDbContext context)
		{
			if (context.Customers.Any()) return;

			string newCardNumber = "4929399657543118";
			string lastFourDigits = newCardNumber.Substring(newCardNumber.Length - 4);
			string cardNumHash = DataEncryptor.Encrypt(newCardNumber);

            Address address = new Address {
                Id = Guid.NewGuid(),
                Street = "12th Jason ave",
                City = "Orange park",
                State = "FL",
                Phone = "23094587",
                ZIP = "33312",
                Country = "United States",
            };

            CreditCard card = new CreditCard {
                Id = Guid.NewGuid(),
                LastFourDigits = lastFourDigits,
                CardNumHash = cardNumHash,
                BillingAddresses = new List<Address> { address },
                ExpDate = "0822",
                FirstName = "John",
                LastName = "Doe",
            };

            Customer customer = new Customer {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateAdded = DateTime.Now,
                CreditCards = new List<CreditCard> { card },
                Addresses = new List<Address> { address }
            };

            context.Customers.Add(customer);
            context.SaveChanges();
        }

        private void _SeedAuthorizeConfig(AuthorizeDbContext context) {
            if (context.AuthorizeConfig.Any()) return;

            AuthorizeConfig config = new AuthorizeConfig {
                Id = Guid.NewGuid(),
                ClientId = "4dp5b7gRqk",
                ClientSecret = "fa3a5b16753d09b24bb44243605a4a98",
                RedirectUri = "https://developer.authorize.net/api/reference/index.html"
            };

            context.AuthorizeConfig.Add(config);
            context.SaveChanges();
        }
    }
}
