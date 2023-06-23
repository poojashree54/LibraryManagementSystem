using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UserDTO
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }


        public static explicit operator User(UserDTO dto)
        {
            if (dto == null) return null;
            return new User
            {
                UserId = dto.UserId,
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email,
                IsAdmin = dto.IsAdmin


            };
        }

        public static implicit operator UserDTO(User user)
        {
            if (user == null) return null;
            return new UserDTO
            {
                UserId=user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                IsAdmin = user.IsAdmin


            };
        }
    }
}
