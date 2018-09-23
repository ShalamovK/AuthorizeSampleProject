using AuthorizeNetSample.DAL.Data.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Customer : BaseEntity<int>
	{
		public Customer()
		{
			Payments = new HashSet<Payment>();
			CreditCards = new HashSet<CreditCard>();
			Addresses = new HashSet<Address>();
		}

		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }

		//Navigation
		public virtual ICollection<Payment> Payments { get; set; }
		public virtual ICollection<CreditCard> CreditCards { get; set; }
		public virtual ICollection<Address> Addresses { get; set; }
	}
}
