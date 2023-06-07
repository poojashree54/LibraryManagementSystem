using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IIssueRepo
    {
        public IEnumerable<Issue> GetAllIssues();
        public Issue GetIssueById(int issueId);
        public bool AddIssue(Issue issue);
        public void UpdateIssue(Issue issue);
        public void DeleteIssue(int issueId);


    }
}
