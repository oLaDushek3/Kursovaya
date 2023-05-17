using Kursovaya.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Kursovaya.Repositories
{
    public class UserRepository : ApplicationContext, IUserRepository
    {

        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (ApplicationContext context = new ApplicationContext())
            {
                UserModel? user = context.Users.FirstOrDefault(u => u.Login == credential.UserName && u.Password == credential.Password);
                validUser = user == null ? false : true;

                return validUser;
            }
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetByAll(ApplicationContext context)
        {
            return context.Users.ToList();
        }

        public UserModel GetById(int id, ApplicationContext context)
        {
            UserModel? user = context.Users.Where(u => u.UserId == id).First();
            return user;
        }

        public UserModel GetByUsername(string username, ApplicationContext context)
        {
            UserModel? user = context.Users.FirstOrDefault(u => u.Login == username);

            return user;
        }
    }
}
