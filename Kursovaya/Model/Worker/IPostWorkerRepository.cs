using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Worker
{
    public interface IPostWorkerRepository
    {
        void Add(PostModel postModel);
        void Edit(PostModel postModel);
        void Remove(int id);
        IEnumerable<PostModel> GetByAll();
    }
}
