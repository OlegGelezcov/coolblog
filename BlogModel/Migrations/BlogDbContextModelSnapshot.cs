﻿// <auto-generated />
using System;
using BlogModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogModel.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogModel.Data.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("BlogId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogModel.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlogId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogModel.Data.ReadedUserPost", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("PostId");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("ReadedPosts");
                });

            modelBuilder.Entity("BlogModel.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("NickName");

                    b.Property<string>("Position");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlogModel.Data.UserBlogSubscription", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BlogId");

                    b.HasKey("UserId", "BlogId");

                    b.HasIndex("BlogId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("BlogModel.Data.Blog", b =>
                {
                    b.HasOne("BlogModel.Data.User", "User")
                        .WithOne("Blog")
                        .HasForeignKey("BlogModel.Data.Blog", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogModel.Data.Post", b =>
                {
                    b.HasOne("BlogModel.Data.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("BlogModel.Data.ReadedUserPost", b =>
                {
                    b.HasOne("BlogModel.Data.Post", "Post")
                        .WithMany("ReadedPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogModel.Data.User", "User")
                        .WithMany("ReadedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogModel.Data.UserBlogSubscription", b =>
                {
                    b.HasOne("BlogModel.Data.Blog", "Blog")
                        .WithMany("Subscriptions")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogModel.Data.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
