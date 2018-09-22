using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNetSample.DAL.Data.Entity
{
	public class Payment : BaseEntity<int>
	{
		public string TransactionId { get; set; }
		public string AuthKey { get; set; }
		public decimal Amount { get; set; }
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
