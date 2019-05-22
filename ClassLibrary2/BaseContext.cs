using App.Core.Products;
using App.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Repo
{
    public class BaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles
        {
            get;
            set;
        }

        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<UserRole> UserRoles
        {
            get;
            set;
        }

        public BaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=FRANSKE;Initial Catalog=Store;Integrated Security=False;User ID=sa;Password=abc123");
            optionsBuilder.UseSqlServer(@"Server=.;Database=Store ;integrated security=True;");
        }
    }
}
