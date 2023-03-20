using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public ObservableCollection<PostModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
