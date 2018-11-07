using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using AuthorizeNetSample.Repositories.Config;
using System;

namespace AuthorizeNetSample.Repositories
{
    public interface IUnitOfWork : IDisposable {
        AuthorizeDbContext Context { get; }
        void RollBack();
        void SaveChanges();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        IAuthorizeConfigRepository AuthorizeConfig { get; }
    }
}
