using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Users
{
    public interface IRoleService
    {

    }
    public class RoleService:IRoleService
    {
        private readonly IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
    }
}
