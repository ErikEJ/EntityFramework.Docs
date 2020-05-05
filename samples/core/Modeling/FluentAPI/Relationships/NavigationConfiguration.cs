﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFModeling.FluentAPI.Relationships.NavigationConfiguration
{
    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        #region NavigationConfiguration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne();

            modelBuilder.Entity<Blog>()
                .Navigation(b => b.Posts)
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
        #endregion
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}