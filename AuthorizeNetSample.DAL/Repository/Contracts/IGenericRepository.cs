using AuthorizeNetSample.DAL.Data.Entity.Base;
using System.Data.Entity;

namespace AuthorizeNetSample.DAL.Repository.Contracts
{
	public interface IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		DbSet<TEntity> All { get; }
		TEntity FindById<TKey>(TKey Id) where TKey : struct;
		void Create(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		void Detach(TEntity entity);
		void Reload(TEntity entity);
	}
}
