using System;
using System.Collections.Generic;
using System.Text;
using App.Commons;
using Microsoft.AspNetCore.Identity;

namespace App.Core.Users
{
    public interface IUserRepo:IRepository<User>
    {
        List<int> FindRole(int userid);
    }
}
