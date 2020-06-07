namespace PersonalBlog.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using PersonalBlog.Interfaces;
    using PersonalBlog.Models;

    public class InMemoryDataServiceV2 : IDataService
    {
        private readonly ConcurrentDictionary<string, Post> posts;

        public InMemoryDataServiceV2()
        {
            this.posts = new ConcurrentDictionary<string, Post>();

            this.posts.TryAdd("D0594BF4-BA5C-444E-8A9B-1B92910610B0", new Post
            {
                Id = "D0594BF4-BA5C-444E-8A9B-1B92910610B0",
                Title = "My first post!",
                Content = "This is my first post.",
                PostDateTime = DateTime.UtcNow.AddDays(-3)
            });

            this.posts.TryAdd("7BF8CB02-42DC-41EA-8231-07325D74FD37", new Post
            {
                Id = "7BF8CB02-42DC-41EA-8231-07325D74FD37",
                Title = "My second post!",
                Content = "This is my second post.",
                PostDateTime = DateTime.UtcNow.AddDays(-2)
            });

            this.posts.TryAdd("7BF8CB02-42DC-41EA-8231-07325D74FD37", new Post
            {
                Id = "7BF8CB02-42DC-41EA-8231-07325D74FD37",
                Title = "My second post!",
                Content = "This is my second post.",
                PostDateTime = DateTime.UtcNow.AddDays(-2)
            });
        }

        public void Create(Post post)
        {
            this.posts.TryAdd(post.Id, post);
        }

        public List<Post> GetAll()
        {
            return this.posts.Values.ToList();
        }
    }
}
