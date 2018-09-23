using AuthorizeNetSample.DAL.Data.Entity.Base;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthorizeNetSample.DAL.Repository.Contracts
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> GetRepository<T>() where T : class, IEntity;
		void SaveChanges();
		Task SaveChangesAsync();
	}
}
