using AuthorizeNetSample.DAL.Data.Entity.Base;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Payment : BaseEntity<int>
	{
		public string TransactionId { get; set; }
		public string AuthKey { get; set; }
		public decimal Amount { get; set; }

		//Navigation
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
