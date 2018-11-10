using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Payment : BaseEntity<Guid>
	{
		public string TransactionId { get; set; }
		public string AuthKey { get; set; }
		public decimal Amount { get; set; }

		//Navigation
		public Guid CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
