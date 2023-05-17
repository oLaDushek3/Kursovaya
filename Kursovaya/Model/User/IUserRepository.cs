using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Net;

namespace Kursovaya.Model.User
{
    internal interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id, ApplicationContext context);
        List<UserModel> GetByAll(ApplicationContext context);
        public UserModel GetByUsername(string username, ApplicationContext context);
        public UserModel GetById(int id, ApplicationContext context);
    }
}
