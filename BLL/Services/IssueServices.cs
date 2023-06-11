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
       

        public IssueServices(IIssueRepo issueRepo)
        {
            _issueRepository = issueRepo;
           
        }

        public ICollection<IssueDTO> GetIssues()
        {
            var issues=_issueRepository.GetAllIssues();
            ICollection<IssueDTO> issueDTOs = issues.Select(issue=>(IssueDTO)issues).ToList();
            return issueDTOs;
        }

        public IssueDTO GetIssue(int id)
        {
            var issue = _issueRepository.GetIssueById(id);
            if (issue == null) return null;
            return (IssueDTO)issue;
        }

        public bool AddIssue(IssueDTO issueDto)
        {

            var issue = ((Issue)issueDto);
            _issueRepository.AddIssue(issue);
            return true;
        }



        public void DeleteIssue(int id)
        {
            var issue = _issueRepository.GetIssueById(id);
            if (issue == null) return;

            _issueRepository.DeleteIssue(id);

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
