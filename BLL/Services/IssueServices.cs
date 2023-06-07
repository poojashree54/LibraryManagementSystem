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
    public class IssueServices : IIssueServices
    {
        private readonly IIssueRepo _issueRepository;
        //private readonly IBookRepo _bookRepository;
        //private readonly IUserRepo _userRepository;

        public IssueServices(IIssueRepo issueRepo)
        {
            _issueRepository = issueRepo;
            //_bookRepository = bookRepo;
            //_userRepository = userRepo;
        }

        public IEnumerable<IssueDTO> GetIssues()
        {
            return _issueRepository.GetAllIssues().Select(i => (IssueDTO)i);
        }

        public IssueDTO GetIssue(int id)
        {
            var issue = _issueRepository.GetIssueById(id);
            if (issue == null) return null;
            return (IssueDTO)issue;
        }

        public bool AddIssue(IssueDTO issueDto)
        {
            /*var book = _bookRepository.SearchBook(issueDto.BookId);

            if (book == null) throw new ArgumentException($"Book with ID {issueDto.BookId} not found.");
            if (book.IsAvailable == false) throw new InvalidOperationException($"Book with ID {issueDto.BookId} is not available.");

            var member = _userRepository.GetUserById(issueDto.UserId);
            if (member == null) throw new ArgumentException($"Member with ID {issueDto.UserId} not found.");

            var issue = ((Issue)issueDto);
            issue.IssueDate = DateTime.Now;
            issue.ReturnDate = null;
            issue.Book = book;
            issue.User = member;

            if (_issueRepository.AddIssue(issue))
            {
                return true;
            };

            book.IsAvailable = false;
            _bookRepository.UpdateBook(book);

            return false;*/

            var issue = ((Issue)issueDto);
            _issueRepository.AddIssue(issue);
            return true;
        }

        /*public void UpdateIssue(int id, IssueDTO issueDto)
        {
            var existingIssue = _issueRepository.GetIssueById(id);
            if (existingIssue == null) return;

            var book = _bookRepository.SearchBook(issueDto.BookId);
            if (book == null) throw new ArgumentException($"Book with ID {issueDto.BookId} not found.");

            var member = _userRepository.GetUserById(issueDto.UserId);
            if (member == null) throw new ArgumentException($"Member with ID {issueDto.UserId} not found.");

            existingIssue.Book = book;
            existingIssue.User = member;
            var issue=
            _issueRepository.UpdateIssue(existingIssue);
        }*/

        public void DeleteIssue(int id)
        {
            var issue = _issueRepository.GetIssueById(id);
            if (issue == null) return;

            _issueRepository.DeleteIssue(id);

            issue.Book.IsAvailable = true;
            //_bookRepository.UpdateBook(issue.Book);
        }

        /*public void ReturnBook(int id)
        {
            var issue = _issueRepository.GetIssueById(id);
            if (issue == null) return;

            issue.ReturnDate = DateTime.Now;
            _issueRepository.UpdateIssue(issue);

            issue.Book.IsAvailable = true;
            _bookRepository.UpdateBook(issue.Book);
        }*/
    }
}
