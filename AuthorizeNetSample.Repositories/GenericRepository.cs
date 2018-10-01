using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AuthorizeNetSample.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
	{
		private AuthorizeDbContext _DbContext;
		private DbSet<T> _DbSet;

		public GenericRepository(IContextAdapter adapter)
		{
			_DbContext = adapter.GetContext();
			_DbSet = _DbContext.Set<T>();
		}

		public IQueryable<T> All => _DbSet;

		public void Attach(T entity)
		{
			if (_DbContext.Entry(entity).State != EntityState.Added)
			{
				_DbContext.Entry(entity).State = EntityState.Detached;
			}
		}

		public void Delete(T entity)
		{
			_DbSet.Remove(entity);
		}

		public void Detatch(T entity)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			throw new NotImplementedException();
		}
	}
}
