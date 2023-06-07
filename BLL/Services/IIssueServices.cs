using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IIssueServices
    {
        IEnumerable<IssueDTO> GetIssues();
        IssueDTO GetIssue(int id);
        bool AddIssue(IssueDTO issueDto);
        //void UpdateIssue(int id, IssueDTO issueDto);
        void DeleteIssue(int id);
        //void ReturnBook(int id);
    }
}
