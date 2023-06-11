using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUserRepo
    {
        public User GetUserById(int userId);
        public User GetUserByUserName(string username);
        public string CreateUser(User user);

        public void DeleteUser(int userId);
        public ICollection<User> GetAllUsers();
        public void UpdateUser(User user);


    }
}
