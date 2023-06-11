using BLL.DTOs;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepo _bookRepo;


        public BookServices(IBookRepo bookRepo)
        {

            _bookRepo = bookRepo;


        }

        public ICollection<Book> GetBooks()
        {
            return _bookRepo.GetAllBooks();
        }

        public Book SearchBook(int bookid)
        {
            return _bookRepo.SearchBook(bookid);


        }

        public bool AddBook(BookDTO bookDto)
        {
            //;
            //_bookRepo.AddBook(book);
            if (_bookRepo.SearchBook(bookDto.BookId) != null)
            {
                // BusNumber already exists, return false or throw an exception
                return false;
            }
            var book = ((Book)bookDto);
            if (_bookRepo.AddBook(book))
            {
                return true;
            }
            return false;
        }


        public void UpdateBook(int id, BookDTO bookDto)
        {
            var existingBook = _bookRepo.SearchBook(id);
            if (existingBook == null) return;

            var updatedBook = ((Book)bookDto);
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.BookId = updatedBook.BookId;
            // update other properties as needed

            _bookRepo.UpdateBook(existingBook);
        }

        public void DeleteBook(int id)
        {
            _bookRepo.DeleteBook(id);
        }


    }
}
