using System;

namespace WLN.Test.Project.Model
{
    public interface IUnitOfWork : IDisposable
    {
        /// <exception cref="ConcurrencyException"></exception>
        /// <exception cref="RepositoryException"></exception>
        void Commit();

        void Rollback();
    }
}
