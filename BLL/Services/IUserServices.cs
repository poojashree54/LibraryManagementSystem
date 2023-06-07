using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserServices
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        bool AddUser(UserDTO userDto);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        //bool Login(string username, string password);

        //public string Authenticate(string username, string password);
        public string Register(UserDTO user);

    }
}
