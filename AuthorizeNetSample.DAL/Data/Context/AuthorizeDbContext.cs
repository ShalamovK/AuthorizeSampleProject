using AuthorizeNetSample.DAL.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNetSample.DAL.Data.Context
{
	public class AuthorizeDbContext : DbContext
	{
		public AuthorizeDbContext() : base("AuthorizeDbConnection")
		{
			
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Payment> Payments { get; set; }
	}
}
