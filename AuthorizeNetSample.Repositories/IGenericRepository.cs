using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AuthorizeNetSample.Repositories
{
	public interface IGenericRepository<T> where T : class, IEntity
	{
		IQueryable<T> All { get; }
		IQueryable<T> Where(Expression<Func<T, bool>> expression);
		void Update(T entity);
		void Delete(T entity);
		void Detatch(T entity);
		void Attach(T entity);
	}
}
