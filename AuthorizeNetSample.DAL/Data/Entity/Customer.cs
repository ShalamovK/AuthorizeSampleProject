using AuthorizeNetSample.DAL.Data.Entity.Base;
using System.Collections.Generic;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Customer : BaseEntity<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<Payment> Payments { get; set; } 
	}
}
