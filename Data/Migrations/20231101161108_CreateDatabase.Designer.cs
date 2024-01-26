﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Data;

#nullable disable

namespace ToDoList.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231101161108_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("ToDoList.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdUser")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ToDoList.Data.Models.PasswordRecovery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NewPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PasswordRecoveries");
                });

            modelBuilder.Entity("ToDoList.Data.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Checked")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCategory")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdUser")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdUser");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ToDoList.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .HasColumnType("BLOB");

                    b.Property<int>("IncorrectPasswordAttempts")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPasswordRecovery")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoList.Data.Models.Category", b =>
                {
                    b.HasOne("ToDoList.Data.Models.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Data.Models.Task", b =>
                {
                    b.HasOne("ToDoList.Data.Models.Category", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoList.Data.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Data.Models.Category", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ToDoList.Data.Models.User", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
