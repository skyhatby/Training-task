using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Helpers;
using WLN.Test.Project.Model;
using WLN.Test.Project.Model.Exceptions;
using WLN.Test.Project.Model.Repositories;

namespace WLN.Test.Project.DAL.Repositories
{
    internal class Repository<TEntity, TKey> : Repository, IRepository<TEntity, TKey> where TEntity : Entity
    {
        /// <exception cref="System.ArgumentNullException"><paramref name="session" /> is null.</exception>
        internal Repository(ISession session)
            : base(session)
        {
        }

        /// <exception cref="Auction.Core.Exceptions.RepositoryException"></exception>
        public TEntity Find(TKey id)
        {
            try
            {
                return Session.Get<TEntity>(id);
            }
            catch (ArgumentNullException ex)
            {
                throw new RepositoryException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        /// <exception cref="System.ArgumentNullException"><paramref name="predicate" /> is null.</exception>
        /// <exception cref="Auction.Core.Exceptions.RepositoryException"></exception>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, "predicate");

            try
            {
                return Session.QueryOver<TEntity>().Where(predicate).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public IQueryable<TEntity> All()
        {
            // unable to catch exceptions here due to IQueryable<T> nature
            return Session.Query<TEntity>();
        }

        /// <exception cref="System.ArgumentNullException"><paramref name="predicate" /> is null.</exception>
        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, "predicate");

            // unable to catch exceptions here due to IQueryable<T> nature
            return Session.Query<TEntity>().Where(predicate).AsQueryable();
        }

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        public void Create(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, "entity");
            using (var transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                Session.Save(entity);
                transaction.Commit();
            }
        }

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        public void Update(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, "entity");

            // as we use transactions, exceptions will be generated during Commit()
            Session.Update(entity);
        }

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        public void Delete(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, "entity");

            // as we use transactions, exceptions will be generated during Commit()
            Session.Delete(entity);
        }
    }
}
