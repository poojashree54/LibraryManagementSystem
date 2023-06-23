using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class IssueDTO
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }



        public static explicit operator Issue(IssueDTO dto)
        {
            if (dto == null) return null;
            return new Issue
            {
                Id = dto.Id,
                BookId = dto.BookId,
                UserId = dto.UserId,
                IssueDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),


            };
        }

        public static implicit operator IssueDTO(Issue issue)
        {
            if (issue == null) return null;
            return new IssueDTO
            {
                Id = issue.Id,
                BookId = issue.BookId,
                UserId = issue.UserId,
                IssueDate = issue.IssueDate,
                ReturnDate = (DateTime)issue.ReturnDate,

            };
        }
    }
}
