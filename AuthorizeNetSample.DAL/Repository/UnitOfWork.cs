using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using AuthorizeNetSample.DAL.Repository.Contracts;
using System.Threading.Tasks;

namespace AuthorizeNetSample.DAL.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private AuthorizeDbContext _context;

		public UnitOfWork(AuthorizeDbContext context)
		{
			_context = context;
		}

		public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
		{
			return new GenericRepository<TEntity>(_context);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public Task SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
	}
}
