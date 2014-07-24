using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Logic.Common;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Model.Exceptions;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.Logic.Membership
{
    public class MembershipService : BaseService, IMembershipService
    {
        /// <exception cref="Auction.Services.Common.ServiceException"></exception>
        public Role GetRoleByName(string roleName)
        {
            var roleRepository = RepositoryFactory.RoleRepository;
            try
            {
                var role = roleRepository.Find(x => x.Name == roleName);
                return role;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }
    }
}
