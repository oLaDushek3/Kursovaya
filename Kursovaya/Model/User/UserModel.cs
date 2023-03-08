using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.User
{
    public partial class UserModel
    {
        [Key]
        public int User_Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
