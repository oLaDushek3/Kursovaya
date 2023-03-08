using Kursovaya.Model.User;
using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Kursovaya.Repositories
{
    public class UserRepository : ApplicationContext, IUserRepository
    {
        bool IUserRepository.AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (ApplicationContext context = new ApplicationContext())
            {
                UserModel? user = context.User.FirstOrDefault(u => u.Login == credential.UserName && u.Password == credential.Password);
                if (user == null)
                    validUser = false;
                else
                    validUser = true;

                return validUser;
            }
        }

        void IUserRepository.Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.Remove(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<UserModel> IUserRepository.GetByAll()
        {
            throw new NotImplementedException();
        }
    }
}
