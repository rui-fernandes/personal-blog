namespace PersonalBlog.Interfaces
{
    using System.Collections.Generic;
    using PersonalBlog.Models;

    public interface IDataService
    {
        void Create(Post post);

        List<Post> GetAll();
    }
}
