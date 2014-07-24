using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Helpers;
using WLN.Test.Project.Model;

namespace WLN.Test.Project.Logic.Common
{
    public class BaseService : IService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IRepositoryFactory RepositoryFactory;

        /// <exception cref="System.ArgumentNullException"><paramref name="unitOfWork" /> is null.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="repositoryFactory" /> is null.</exception>
        protected BaseService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
        {
            Expect.ArgumentNotNull(unitOfWork, "unitOfWork");
            Expect.ArgumentNotNull(repositoryFactory, "repositoryFactory");

            UnitOfWork = unitOfWork;
            RepositoryFactory = repositoryFactory;
        }
    }
}
