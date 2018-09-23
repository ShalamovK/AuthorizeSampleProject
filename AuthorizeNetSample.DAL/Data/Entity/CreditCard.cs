using AuthorizeNetSample.DAL.Data.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class CreditCard : BaseEntity<int>
	{
		[Required]
		public string LastFourDigits { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string CardNumHash { get; set; }
		[Required]
		public string ExpDate { get; set; }

		//Navigation
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual ICollection<Address> BillingAddress { get; set; }
	}
}
