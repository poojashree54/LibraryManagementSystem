using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly AppDbContext _context;
        public BookRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool AddBook(Book book)
        {
            if ((_context.Books.Add(book)) != null)
            {
                _context.SaveChanges();
                return true;
            }
            return false;

        }



        public void DeleteBook(int Bookid)
        {
            var book = _context.Books.Find(Bookid);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public ICollection<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book SearchBook(int Bookid)
        {
            return _context.Books.Find(Bookid);
        }



        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
