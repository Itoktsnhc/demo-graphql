using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace demo_graphql
{
    public class CrudBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class Post : CrudBase
    {
        public string Content { get; set; }
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class Blog : CrudBase
    {
        public virtual ICollection<Post> Posts { get; set; }
    }

    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
