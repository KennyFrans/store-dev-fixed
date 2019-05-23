using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Users;
using App.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace App.Identity
{
    public class UserStore : IUserPasswordStore<User>,IUserRoleStore<User>
    {
        protected BaseContext Db = new BaseContext();

        //public UserStore(BaseContext db)
        //{
        //    this._db = db;
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db?.Dispose();
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)null);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            using (var context = new BaseContext())
            {
                context.Add(user);

                await context.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(IdentityResult.Success);
            }
           
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(UpdateAsync));
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            Db.Remove(user);

            int i = await Db.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await Db.Users.FindAsync(id);
            }
            else
            {
                return await Task.FromResult((User)null);
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await Db.Users
                           .AsAsyncEnumerable()
                           .SingleOrDefault(p => p.UserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            using (var context = new BaseContext())
            {
                var roleToAdd = context.Roles.FirstOrDefault(r => r.Name == roleName);
                if (roleToAdd != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = roleToAdd.Id
                    };
                    user.UserRoles.Add(userRole);
                }

                return context.SaveChangesAsync(cancellationToken);
            }
            
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            //using (var context = new BaseContext())
            //{
                return Task.Run(() =>
                {
                    var role = Db.Roles.FirstOrDefault(r => r.Name == roleName);
                    return user.UserRoles.Any(x => role != null && x.RoleId == role.Id);
                }, cancellationToken);
                
                
                //if (roleToAdd != null)
                //{
                //    var userRole = new UserRole
                //    {
                //        UserId = user.Id,
                //        RoleId = roleToAdd.Id
                //    };
                //    user.UserRoles.Add(userRole);
                //}

                //return context.SaveChangesAsync(cancellationToken);
            //}
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
