using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizeNetSample.DAL.Data.Entity.Base
{
	public class BaseEntity<Type> : IEntity<Type> where Type : struct
	{
		[Key]
		public Type Id { get; set; }
		public DateTime? DateAdded { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? LastModified { get; set; }

	}

	public interface IEntity { }

	public interface IEntity<Type> : IEntity where Type : struct
	{
	}
}
