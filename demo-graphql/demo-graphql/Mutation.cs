using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace demo_graphql
{
    public class Mutation
    {
        [UseDbContext(typeof(DemoDbContext))]
        public async Task<Post> AddPostAsync([ScopedService] DemoDbContext context, Post entity)
        {
            return await AddEntityAsync(context, entity);
        }

        [UseDbContext(typeof(DemoDbContext))]
        public async Task<Post> DeletePostAsync([ScopedService] DemoDbContext context, int postId)
        {
            return await DeleteEntityAsync<Post>(context, postId);
        }

        [UseDbContext(typeof(DemoDbContext))]
        public async Task<Post> UpdatePostAsync([ScopedService] DemoDbContext context, int postId, Post post)
        {
            return await UpdateEntityAsync<Post>(context, postId, post);
        }

        [UseDbContext(typeof(DemoDbContext))]
        public async Task<Blog> AddBlogAsync([ScopedService] DemoDbContext context, Blog blog)
        {
            return await AddEntityAsync(context, blog);
        }

        private static async Task<T> AddEntityAsync<T>(DemoDbContext context, T entity) where T : CrudBase
        {
            entity.UpdateTime = DateTime.UtcNow;
            if (await context.Set<T>().AnyAsync(
                e => e.Name == entity.Name))
            {
                throw new Exception($"{typeof(T).Name} with name exists:{entity.Name}");
            }

            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        private static async Task<T> DeleteEntityAsync<T>(DemoDbContext context, int id) where T : CrudBase
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("cannot delete not exist entity");
            }

            return entity;
        }

        private static async Task<T> UpdateEntityAsync<T>(DemoDbContext context, int id, T entity) where T : CrudBase
        {
            entity.Id = id;
            if (!await context.Set<T>().AnyAsync(e => e.Id == entity.Id))
            {
                throw new Exception("cannot update not exist entity");
            }
            if (await context.Set<T>().AnyAsync(e => e.Name == entity.Name && e.Id != entity.Id))
            {
                throw new Exception($"{typeof(T).Name} with same name exists:{entity.Name}");
            }
            entity.UpdateTime = DateTime.UtcNow;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
