using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using ToDoList.Data.Models;

namespace ToDoList.Data
{
    public class AppDbContext : DbContext
	{
		public DbSet<Models.Task> Tasks { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }

		public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }

		public DbSet<TEntity> Generico<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasOne(c => c.User)
				.WithMany(u => u.Categories)
				.HasForeignKey(c => c.IdUser);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("DataSource=app.db;Cache=Shared");
	}
}
