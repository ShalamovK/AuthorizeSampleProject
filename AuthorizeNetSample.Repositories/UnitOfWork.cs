using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using AuthorizeNetSample.Repositories.Config;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace AuthorizeNetSample.Repositories
{
    public class UnitOfWork : IUnitOfWork {
        public UnitOfWork() {
            Context = new AuthorizeDbContext();
        }

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<Type, object> _repos = new Dictionary<Type, object>();
        private IAuthorizeConfigRepository _authorizeConfigRepository;

        public AuthorizeDbContext Context { get; private set; }

        public void RollBack() {
            var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified)) {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added)) {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted)) {
                entry.State = EntityState.Unchanged;
            }
        }

        public void SaveChanges() {
            try {
                Context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                _logger.Error("Context.SaveChanges raised an error: {0}", sb);
                // Add the original exception as the innerException
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity {
            if (!_repos.ContainsKey(typeof(TEntity))) {
                _repos.Add(typeof(TEntity), new GenericRepository<TEntity>(Context));
            }
            return _repos[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public void Dispose() {
            Context?.Dispose();
            Context = null;
        }

        public IAuthorizeConfigRepository AuthorizeConfig {
            get { return _authorizeConfigRepository ?? (_authorizeConfigRepository = new AuthorizeConfigRepository(Context)); }
        }
    }
}
