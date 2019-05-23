using System.Collections.Generic;
using System.Linq;
using App.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Repo.Users
{
    public class UserRepo:BaseRepo<User>,IUserRepo
    {
        public List<int> FindRole(int userid)
        {
            using (var context = new BaseContext())
            {
                var roleId = context.Roles.Include("UserRoles").Where(r => r.UserRoles.Any(z => z.UserId == userid)).Select(y => y.Id).ToList();
                return roleId;
            }
        }

        public override void Update(User entity)
        {
            using (var context = new BaseContext())
            {
                var oldUserRole = context.Users.Include("UserRoles").FirstOrDefault(x => x.Id == entity.Id);
                var oldRole = oldUserRole.UserRoles.Select(x => x.RoleId).ToList();
                entity.UserRoles = oldUserRole.UserRoles.ToList();

                context.Entry(oldUserRole).State = EntityState.Detached;

                context.Users.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.ChangeTracker.DetectChanges();

                if (entity.SelectedUserRole != null)
                {
                    var newRole = entity.SelectedUserRole.ToList();

                    var toDelRole = oldRole.Except(newRole).ToList();
                    var toAddRole = newRole.Except(oldRole).ToList();

                    foreach (var delRole in toDelRole)
                    {
                        var role = context.Roles.Find(delRole);
                        var userrole = context.UserRoles.FirstOrDefault(x => x.UserId == entity.Id && x.RoleId == role.Id);
                        entity.UserRoles.Remove(userrole);
                    }

                    foreach (var addRole in toAddRole)
                    {
                        var role = context.Roles.Find(addRole);
                        var userrole = new UserRole
                        {
                            UserId = entity.Id,
                            RoleId = role.Id
                        };
                        entity.UserRoles.Add(userrole);
                    }
                }
                

                context.SaveChanges();
            }
        }

        public override List<User> GetAll()
        {
            using (var context = new BaseContext())
            {
                var e = context.Users.Include("UserRoles").ToList();
                return e;
            }
        }

        public override void Save(User entity)
        {
            using (var context = new BaseContext())
            {
                context.Set<User>().Add(entity);

                if (entity.SelectedUserRole != null)
                {
                    foreach (var roleid in entity.SelectedUserRole)
                    {
                        var nRole = context.Roles.Find(roleid);
                        var userrole = new UserRole
                        {
                            UserId = entity.Id,
                            RoleId = nRole.Id
                        };
                        entity.UserRoles.Add(userrole);
                    }
                }
               

                context.SaveChanges();
            }
        }
    }
}
