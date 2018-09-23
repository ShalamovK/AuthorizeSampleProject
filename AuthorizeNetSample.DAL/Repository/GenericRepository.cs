using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using AuthorizeNetSample.DAL.Repository.Contracts;
using System.Data.Entity;

namespace AuthorizeNetSample.DAL.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
	{
		private AuthorizeDbContext _DbContext;
		private DbSet<T> _DbSet;

		public GenericRepository(AuthorizeDbContext context)
		{
			_DbContext = context;
			_DbSet = context.Set<T>();
		}

		public DbSet<T> All { get { return _DbSet; } }

		public void Create(T entity)
		{
			_DbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			_DbSet.Remove(entity);
		}

		public void Detach(T entity)
		{
			throw new System.NotImplementedException();
		}

		public T FindById<TKey>(TKey Id) where TKey : struct
		{
			throw new System.NotImplementedException();
		}

		public void Reload(T entity)
		{
			throw new System.NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
