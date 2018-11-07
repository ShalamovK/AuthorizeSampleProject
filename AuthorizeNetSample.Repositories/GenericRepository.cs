using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using AuthorizeNetSample.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace AuthorizeNetSample.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		private AuthorizeDbContext Context;
		private DbSet<TEntity> DbSet;

		public GenericRepository(AuthorizeDbContext dbContext)
		{
            Context = dbContext;
			DbSet = Context.Set<TEntity>();
		}

        public IQueryable<TEntity> All => DbSet;

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties) {
            IQueryable<TEntity> result = DbSet;

            foreach (Expression<Func<TEntity, object>> prop in includeProperties) {
                result = result.Include(prop);
            }

            if (predicate != null) {
                result = result.Where(predicate);
            }

            return result;
        }

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties) {
            var query = All;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates) {
            IQueryable<TEntity> query = All;
            foreach (var predicate in predicates) {
                query = query.Where(predicate);
            }
            return query;
        }

        public void Add(TEntity entity) {
            var baseEntity = entity as BaseEntity<Guid>;
            if (baseEntity != null && baseEntity.Id == Guid.Empty) {
                baseEntity.Id = Guid.NewGuid();
            }

            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached) {
                dbEntityEntry.State = EntityState.Added;
            } else {
                DbSet.Add(entity);
            }
        }

        public void Update(TEntity entity) {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached) {
                DbSet.Attach(entity);
            }
            if (dbEntityEntry.State != EntityState.Modified) {
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity) {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted) {
                dbEntityEntry.State = EntityState.Deleted;
            } else {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            dbEntityEntry.State = EntityState.Deleted;
        }

        public TEntity GetById(Guid id) {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> Get(out int total, string orderBy, int skip, int take = 0, bool descOrder = true,
            IEnumerable<Expression<Func<TEntity, bool>>> conditions = null) {
            var query = All;

            if (conditions != null) {
                foreach (var condition in conditions) {
                    query = query.Where(condition);
                }
            }

            query = query.OrderByField(orderBy, descOrder);
            total = query.Count();
            query = query.Skip(skip);
            if (take != 0) {
                query = query.Take(take);
            }
            return query;
        }

        public void Delete(Guid id) {
            var entity = GetById(id);
            if (entity == null)
                return;

            Delete(entity);
        }

        public bool Exists(Guid id) {
            return DbSet.Find(id) != null;
        }

        public TEntity Duplicate(Guid id, Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties) {
            var query = All;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            var duplicate = query.AsNoTracking().First(where);
            return duplicate;
        }

        public DbEntityEntry<T> GetEntry<T>(T entity) where T : class {
            return Context.Entry(entity);
        }

        public T Attach<T>(T entity) where T : class {
            var entry = Context.Entry(entity);
            var dbSet = Context.Set<T>();

            if (entry.State == EntityState.Unchanged) {
                entry.State = EntityState.Detached;
            }

            return dbSet.Attach(entity);
        }

        public void Detach<T>(T entity) where T : class {
            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
