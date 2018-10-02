using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity.Base;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace AuthorizeNetSample.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private Dictionary<Type, IGenericRepository<IEntity>> _repositories;
        private readonly AuthorizeDbContext _authorizeDbContext;
        private readonly Logger _logger;

		public UnitOfWork()
		{
			_repositories = new Dictionary<Type, IGenericRepository<IEntity>>();
            _authorizeDbContext = new AuthorizeDbContext();
            _logger = LogManager.GetCurrentClassLogger();
        }

		public void SaveChanges()
		{
            try {
                _authorizeDbContext.SaveChanges();
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

		public void SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public IGenericRepository<T> GetRepository<T>() where T : class, IEntity
		{
			if (!_repositories.ContainsKey(typeof(T)))
			{
				_repositories.Add(typeof(T), new GenericRepository<T>(_authorizeDbContext) as IGenericRepository<IEntity>);
			}

			return _repositories[typeof(T)] as GenericRepository<T>;
		}
	}
}
