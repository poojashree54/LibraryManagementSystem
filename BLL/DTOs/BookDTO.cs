using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BookDTO
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        //public ICollection<Issue> Issues { get; set; }


        public static explicit operator Book(BookDTO dto)
        {
            if (dto == null) return null;
            return new Book
            {
                BookId = dto.BookId,
                Title = dto.Title,
                Author = dto.Author,
                IsAvailable = dto.IsAvailable,
                //Issues = dto.Issues


            };
        }

        public static implicit operator BookDTO(Book book)
        {
            if (book == null) return null;
            return new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                IsAvailable = book.IsAvailable,
                //Issues = book.Issues
            };
        }
    }
}
