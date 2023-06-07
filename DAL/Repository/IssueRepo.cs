using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class IssueRepo : IIssueRepo
    {
        private readonly AppDbContext _context;
        public IssueRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool AddIssue(Issue issue)
        {
            if ((_context.Issues.Add(issue)) != null)
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteIssue(int issueId)
        {
            var issue = _context.Issues.Find(issueId);
            _context.Issues.Remove(issue);
            _context.SaveChanges();
        }

        public IEnumerable<Issue> GetAllIssues()
        {
            return _context.Issues.Include(i => i.Book).Include(i => i.User).ToList();
        }

        public Issue GetIssueById(int issueId)
        {
            throw new NotImplementedException();
        }

        public void UpdateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }
    }
}
