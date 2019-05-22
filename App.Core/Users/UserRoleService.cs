using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Users
{
    public interface IUserRoleService
    {

    }
    public class UserRoleService:IUserRoleService
    {
        private readonly IUserRoleRepo _userRoleRepo;

        public UserRoleService(IUserRoleRepo userRoleRepo)
        {
            _userRoleRepo = userRoleRepo;
        }
    }
}
