using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_graphql
{
    public class Query
    {
        [UseDbContext(typeof(DemoDbContext))]
        [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 50, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Blog> Blogs([ScopedService] DemoDbContext context)
        {
            return context.Blogs.AsNoTracking();
        }

        [UseDbContext(typeof(DemoDbContext))]
        [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 50, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> Posts([ScopedService] DemoDbContext context)
        {
            return context.Posts.AsNoTracking();
        }
    }
}
