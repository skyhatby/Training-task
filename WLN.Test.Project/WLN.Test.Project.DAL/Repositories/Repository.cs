using NHibernate;
using WLN.Test.Project.Helpers;
using WLN.Test.Project.Model.Repositories;

namespace WLN.Test.Project.DAL.Repositories
{
    internal class Repository : IRepository
    {
        protected readonly ISession Session;

        /// <exception cref="System.ArgumentNullException"><paramref name="session" /> is null.</exception>
        internal Repository(ISession session)
        {
            Expect.ArgumentNotNull(session, "session");

            Session = session;
        }
    }
}
