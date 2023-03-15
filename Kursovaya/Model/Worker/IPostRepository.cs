using System.Collections.Generic;

namespace Kursovaya.Model.Worker
{
    public interface IPostRepository
    {
        void Add(PostModel postModel);
        void Edit(PostModel postModel);
        void Remove(int id);
        IEnumerable<PostModel> GetByAll();
    }
}
