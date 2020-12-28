using CascadingSelectList.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        #endregion


        #region DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        #endregion


        #region Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
  

            modelBuilder.Entity<Category>(category =>
            {

                category.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentCategoryId);

                category.HasMany(c => c.Posts)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

                category.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(45);

                category.Property(c => c.Description)
                        .HasMaxLength(150);
                
             });



                

        }
        #endregion






    }
}
