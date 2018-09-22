using System;

namespace AuthorizeNetSample.DAL.Data.Entity.Base
{
	public class BaseEntity<Type> where Type : struct
	{
		public Type Id { get; set; }
		public DateTime? DateAdded { get; set; } 
	}
}
