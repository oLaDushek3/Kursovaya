using Kursovaya.Model.User;
using Kursovaya.Model.Worker;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Kursovaya.Repositories
{
    public class UserRepository : ApplicationContext, IUserRepository
    {
        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (ApplicationContext context = new ApplicationContext())
            {
                UserModel? user = context.User.FirstOrDefault(u => u.Login == credential.UserName && u.Password == credential.Password);
                validUser = user == null? false : true;

                return validUser;
            }
        }

        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            ApplicationContext context = new ApplicationContext();
            UserModel? _user = context.User.FirstOrDefault(u => u.Login == username);

            return _user;
        }
    }
}
