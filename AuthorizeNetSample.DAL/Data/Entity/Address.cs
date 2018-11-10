using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Address : BaseEntity<Guid>
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZIP { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }

		//Navigation
		public Guid? CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
        public Guid? CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; }
    }
}
