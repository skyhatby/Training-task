using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Logic.Common;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Model;
using WLN.Test.Project.Model.Exceptions;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.Logic.Membership
{
    public class MembershipService : BaseService, IMembershipService
    {
        public MembershipService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, repositoryFactory)
        {
        }

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
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

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public User GetUser(long userId)
        {
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                var user = userRepository.Find(userId);
                return user;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public string[] GetUserRoles(long userId)
        {
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                var user = userRepository.Find(userId);
                return user.Roles.Select(x=>x.Name).ToArray();
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public User GetUserByName(string userName)
        {
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                var user = userRepository.Find(x => x.Name == userName);
                return user;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public User RegisterUser(string name, string password, string roleName)
        {
            var role = GetRoleByName(roleName);
            if (role == null)
            {
                throw new MembershipServiceException(MembershipError.RoleDoesNotExist);
            }
            var user = GetUserByName(name.ToLower());
            if (user != null)
            {
                throw new MembershipServiceException(MembershipError.UserIsAlreadyRegistered);
            }
            user = new User { Name = name };
            user.SetPassword(password);
            user.Roles.Add(role);
            var userRepository = RepositoryFactory.UserRepository;
            userRepository.Create(user);
            return user;
        }

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public void ResetPassword(string name)
        {
            var user = GetUserByName(name);
            if (user == null)
            {
                throw new MembershipServiceException(MembershipError.UserDoesNotExist);
            }
            var newPassword = "111111";
            user.SetPassword(newPassword);
            UpdateUser(user);
        }

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public void UpdateUser(User user)
        {
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                userRepository.Update(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex);
            }
        }
    }
}
