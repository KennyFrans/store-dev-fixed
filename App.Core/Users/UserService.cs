using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Commons;
using App.Core.Products;
using Microsoft.AspNetCore.Identity;

namespace App.Core.Users
{
    public interface IUserService
    {
        List<int> FindRoleById(int userid);
        void Update(User user);
    }
    public class UserService : DomainServiceBase<User>,IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo) : base(userRepo)
        {
            _userRepo = userRepo;
        }

        public List<int> FindRoleById(int userid)
        {
            return _userRepo.FindRole(userid);
        }

        public void Update(User user)
        {
            _userRepo.Update(user);
        }

        public override User CreateNew()
        {
            throw new NotImplementedException();
        }
    }
}
