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
            issue.ReturnDate = issue.ReturnDate.AddDays(7);
            if ((_context.Issues.Add(issue)) == null)
            {
                return false;
            }
            _context.SaveChanges();
            return true;
        }

        public void DeleteIssue(int issueId)
        {
            var issue = _context.Issues.Where(i => i.Id == issueId).FirstOrDefault();
            _context.Issues.Remove(issue);
            _context.SaveChanges();
        }

        public ICollection<Issue> GetAllIssues()
        {
            return _context.Issues.Include(i => i.Book).Include(i => i.User).ToList();
        }

        public Issue GetIssueById(int issueId)
        {
            return _context.Issues.Where(i => i.Id == issueId).FirstOrDefault();
        }

        
    }
}
