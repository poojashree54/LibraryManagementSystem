using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IBookRepo
    {
        public ICollection<Book> GetAllBooks();
        public Book SearchBook(int Bookid);

        public bool AddBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(int Bookid);

    }
}
