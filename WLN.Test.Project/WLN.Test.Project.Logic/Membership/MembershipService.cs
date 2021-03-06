﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Helpers;
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

        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
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

        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
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

        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
        public string[] GetUserRoles(long userId)
        {
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                var user = userRepository.Find(userId);
                return user.Roles.Select(x => x.Name).ToArray();
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }

        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
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
        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
        public User RegisterUser(string name, string password, string roleName = "User")
        {
            ValidateUser(name, password);
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
            if (!user.Roles.Select(x => x.Name).Contains("User")) user.Roles.Add(GetRoleByName("User"));
            var userRepository = RepositoryFactory.UserRepository;
            try
            {
                userRepository.Create(user);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
            return user;
        }

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
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
            UnitOfWork.Commit();
        }

        /// <exception cref="WLN.Test.Project.Model.Exceptions.ServiceException"></exception>
        public void UpdateUser(User user)
        {
            ValidateUser(user);
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

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        private void ValidateUser(string name, string password)
        {
            try
            {
                Expect.ArgumentNotNullOrEmpty(name);
                Expect.ArgumentNotNullOrEmpty(password);
            }
            catch (ArgumentNullException)
            {
                throw new MembershipServiceException(MembershipError.NameOrPasswordMustBeNotNull);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        private void ValidateUser(User user)
        {
            ValidateUser(user.Name, user.Password);
        }
    }
}
