using System;
using System.Linq;
using System.Threading;
using App.Commons;
using App.Core.OrderDetails;
using App.Core.Orders;
using App.Core.OrderStatuses;
using App.Core.Products;
using App.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Repo
{
    public class BaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
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

        public override int SaveChanges()
        {
            var addEntities = ChangeTracker.Entries<EntityBase>().Where(e => e.State == EntityState.Added).ToList();
            addEntities.ForEach(i =>
            {
                i.Entity.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                i.Entity.CreatedDate = DateTime.Now;
            });

            var updateEntities = ChangeTracker.Entries<EntityBase>().Where(e => e.State == EntityState.Modified).ToList();
            updateEntities.ForEach(i =>
            {
                i.Entity.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                i.Entity.UpdatedDate = DateTime.Now;
            });

            return base.SaveChanges();
        }
    }
}
