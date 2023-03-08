using System.Collections.Generic;
using System.Net;

namespace Kursovaya.Model.User
{
    internal interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        IEnumerable<UserModel> GetByAll();
        //...
    }
}
