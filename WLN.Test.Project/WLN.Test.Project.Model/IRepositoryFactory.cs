using WLN.Test.Project.Model.MemberhshipModels;
using WLN.Test.Project.Model.Repositories;

namespace WLN.Test.Project.Model
{
    public interface IRepositoryFactory
    {
        #region Memebership

        IRepository<Role, int> RoleRepository { get; }
        IRepository<User, long> UserRepository { get; }

        #endregion
    }
}
