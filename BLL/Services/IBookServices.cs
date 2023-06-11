using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBookServices
    {
        ICollection<Book> GetBooks();
        //BookDTO GetBook(int id);
        bool AddBook(BookDTO bookDto);
        void UpdateBook(int id, BookDTO bookDto);
        void DeleteBook(int id);
        Book SearchBook(int bookid);
    }
}
