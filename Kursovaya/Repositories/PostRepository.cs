using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kursovaya.Repositories
{
    public class PostRepository : ApplicationContext, IPostRepository
    {
        public void Add(PostModel postModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(PostModel postModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public PostModel? GetById(int id, ApplicationContext context)
        {
            PostModel? postModel = context.Posts.Where(p => p.PostId == id).FirstOrDefault();

            return postModel;
        }

        public List<PostModel> GetByAll(ApplicationContext context)
        {
            List<PostModel> postModels = context.Posts.ToList();

            return postModels;
        }
    }
}
