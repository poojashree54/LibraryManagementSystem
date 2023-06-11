using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }


        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }



        public string CreateUser(User user)
        {
            if ((_context.Users.Add(user) != null))
            {
                _context.SaveChanges();
                return user.Token;
            }
            return null;
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(x => x.UserName == username).FirstOrDefault();
        }
    }
}
