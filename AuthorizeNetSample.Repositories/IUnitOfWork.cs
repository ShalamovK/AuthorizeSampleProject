using AuthorizeNetSample.DAL.Data.Entity.Base;

namespace AuthorizeNetSample.Repositories
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> GetRepository<T>() where T : class, IEntity;
		void SaveChanges();
		void SaveChangesAsync();
	}
}
