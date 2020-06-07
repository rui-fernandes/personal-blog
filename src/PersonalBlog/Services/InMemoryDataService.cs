namespace PersonalBlog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PersonalBlog.Interfaces;
    using PersonalBlog.Models;

    public class InMemoryDataService : IDataService
    {
        private readonly Dictionary<string, Post> posts;

        public InMemoryDataService()
        {
            this.posts = new Dictionary<string, Post>
            {
                {
                    "D0594BF4-BA5C-444E-8A9B-1B92910610B0",
                    new Post
                    {
                        Id = "D0594BF4-BA5C-444E-8A9B-1B92910610B0",
                        Title = "My first post!",
                        Content = "This is my first post.",
                        PostDateTime = DateTime.UtcNow.AddDays(-3)
                    }
                },
                {
                    "7BF8CB02-42DC-41EA-8231-07325D74FD37",
                    new Post
                    {
                        Id = "7BF8CB02-42DC-41EA-8231-07325D74FD37",
                        Title = "My second post!",
                        Content = "This is my second post.",
                        PostDateTime = DateTime.UtcNow.AddDays(-2)
                    }
                },
                {
                    "9088E70E-C3B8-433A-B6F9-B77B662412A5",
                    new Post
                    {
                        Id = "9088E70E-C3B8-433A-B6F9-B77B662412A5",
                        Title = "My third post!",
                        Content = "This is my third post.",
                        PostDateTime = DateTime.UtcNow.AddDays(-1)
                    }
                }
            };
        }

        public void Create(Post post)
        {
            this.posts.Add(post.Id, post);
        }

        public List<Post> GetAll()
        {
            return this.posts.Values.ToList();
        }
    }
}
