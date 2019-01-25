using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogModel.Data {
    public class BlogDbContext : DbContext {

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserBlogSubscription> Subscriptions { get; set; }
        public DbSet<ReadedUserPost> ReadedPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyBlog;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Console.WriteLine("OnModelCreating!!!!!!!!!!!");
            modelBuilder.Entity<User>().HasOne(u => u.Blog)
                .WithOne(b => b.User)
                .HasForeignKey<Blog>(b => b.UserId);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog);

            modelBuilder.Entity<ReadedUserPost>()
                .HasKey(rup => new { rup.UserId, rup.PostId });
            modelBuilder.Entity<ReadedUserPost>()
                .HasOne(rup => rup.User)
                .WithMany(u => u.ReadedPosts)
                .HasForeignKey(rup => rup.UserId);
            modelBuilder.Entity<ReadedUserPost>()
                .HasOne(rup => rup.Post)
                .WithMany(p => p.ReadedPosts)
                .HasForeignKey(rup => rup.PostId);

            modelBuilder.Entity<UserBlogSubscription>()
                .HasKey(s => new { s.UserId, s.BlogId });
            modelBuilder.Entity<UserBlogSubscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserBlogSubscription>()
                .HasOne(s => s.Blog)
                .WithMany(b => b.Subscriptions)
                .HasForeignKey(s => s.BlogId);


        }
    }
}
