using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace AuthorizeNetSample.Repositories
{
	public interface IGenericRepository<T> where T : class, IEntity
	{
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(Guid id);
        IQueryable<T> Get(out int total, string orderBy, int skip, int take = 0,
            bool descOrder = true, IEnumerable<Expression<Func<T, bool>>> conditions = null);
        void Delete(Guid id);
        bool Exists(Guid id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        T Duplicate(Guid id, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        DbEntityEntry<T> GetEntry<T>(T entity) where T : class;
        T Attach<T>(T entity) where T : class;
        void Detach<T>(T entity) where T : class;
    }
}
