using NHibernate;
using System;
using System.Data;
using WLN.Test.Project.DAL.Repositories;
using WLN.Test.Project.Model;
using WLN.Test.Project.Model.MemberhshipModels;
using WLN.Test.Project.Model.Exceptions;
using WLN.Test.Project.Model.Repositories;

namespace WLN.Test.Project.DAL
{
    public sealed class UnitOfWork : IRepositoryFactory, IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isDisposed;

        private IRepository<Role, int> _roleRepository;
        private IRepository<User, long> _userRepository;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Auto;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        #region Memebership

        public IRepository<Role, int> RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new Repository<Role, int>(_session)); }
        }

        public IRepository<User, long> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new Repository<User, long>(_session)); }
        }

        #endregion

        /// <exception cref="Auction.Core.Exceptions.ConcurrencyException"></exception>
        /// <exception cref="Auction.Core.Exceptions.RepositoryException"></exception>
        public void Commit()
        {
            if (!_isDisposed && _transaction != null && _transaction.IsActive)
            {
                try
                {
                    _transaction.Commit();
                }
                catch (StaleObjectStateException ex)
                {
                    _transaction.Rollback();
                    throw new ConcurrencyException(ex.Message, ex.EntityName, ex.Identifier);
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw new RepositoryException(ex.Message, ex.InnerException);
                }
            }
        }

        public void Rollback()
        {
            if (!_isDisposed && _transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    try
                    {
                        _transaction.Commit();
                    }
                    catch (StaleObjectStateException ex)
                    {
                        _transaction.Rollback();
                        throw new ConcurrencyException(ex.Message, ex.EntityName, ex.Identifier);
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback();
                        throw new RepositoryException(ex.Message, ex.InnerException);
                    }
                }
                if (_session.IsOpen)
                {
                    _session.Close();
                }
            }
            _isDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
