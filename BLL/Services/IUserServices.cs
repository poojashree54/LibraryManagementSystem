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
        string Login(string username, string password);
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUserById(int id);
        public bool Register(UserDTO userDto);
        public string Authenticate(string username, string password);
    }
}
