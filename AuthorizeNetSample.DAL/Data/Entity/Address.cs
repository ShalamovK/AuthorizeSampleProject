using AuthorizeNetSample.DAL.Data.Entity.Base;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Address : BaseEntity<int>
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZIP { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }

		//Navigation
		public int CustomerId { get; set; }
		public int? CreditCardId { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual CreditCard CreditCard { get; set; }
	}
}
