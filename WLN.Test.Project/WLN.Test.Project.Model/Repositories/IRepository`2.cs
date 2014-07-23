using System;
using System.Linq;
using System.Linq.Expressions;
namespace WLN.Test.Project.Model.Repositories
{
    public interface IRepository<TEntity, in TKey> : IRepository where TEntity : Entity
    {
        /// <exception cref="RepositoryException"></exception>
        TEntity Find(TKey id);

        /// <exception cref="System.ArgumentNullException"><paramref name="predicate" /> is null.</exception>
        /// <exception cref="RepositoryException"></exception>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> All();

        /// <exception cref="System.ArgumentNullException"><paramref name="predicate" /> is null.</exception>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        void Create(TEntity entity);

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        void Update(TEntity entity);

        /// <exception cref="System.ArgumentNullException"><paramref name="entity" /> is null.</exception>
        void Delete(TEntity entity);
    }
}