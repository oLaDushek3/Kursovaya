using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Worker
{
    public interface IPostRepository
    {
        void Add(PostModel postModel);
        void Edit(PostModel postModel);
        void Remove(int id, ApplicationContext context);
        PostModel? GetById(int id, ApplicationContext context);
        List<PostModel> GetByAll(ApplicationContext context);
    }
}
