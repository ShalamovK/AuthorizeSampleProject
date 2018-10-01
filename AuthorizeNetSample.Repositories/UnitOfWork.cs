using AuthorizeNetSample.DAL.Data.Entity.Base;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private Dictionary<Type, IGenericRepository<IEntity>> _repositories;

		public UnitOfWork()
		{
			_repositories = new Dictionary<Type, IGenericRepository<IEntity>>();
		}

		public void SaveChanges()
		{
			throw new NotImplementedException();
		}

		public void SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public IGenericRepository<T> GetRepository<T>() where T : class, IEntity
		{
			if (!_repositories.ContainsKey(typeof(T)))
			{
				_repositories.Add(typeof(T), new GenericRepository<T>(new ContextAdapter()) as IGenericRepository<IEntity>);
			}

			return _repositories[typeof(T)] as GenericRepository<T>;
		}
	}
}
