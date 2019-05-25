﻿// <auto-generated />
using System;
using App.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Repo.Migrations
{
    [DbContext(typeof(BaseContext))]
    [Migration("20190524040439_Order_OrderDetail")]
    partial class Order_OrderDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App.Core.OrderDetails.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("App.Core.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("OrderStatusId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("App.Core.OrderStatuses.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Descrition");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("App.Core.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("UnitPrice");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App.Core.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("App.Core.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("FirstName")
                        .HasMaxLength(32);

                    b.Property<string>("LastName")
                        .HasMaxLength(32);

                    b.Property<string>("MiddleInitial")
                        .HasMaxLength(1);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("App.Core.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("App.Core.OrderDetails.OrderDetail", b =>
                {
                    b.HasOne("App.Core.Products.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Core.Orders.Order", b =>
                {
                    b.HasOne("App.Core.OrderStatuses.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Core.Users.UserRole", b =>
                {
                    b.HasOne("App.Core.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Core.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
