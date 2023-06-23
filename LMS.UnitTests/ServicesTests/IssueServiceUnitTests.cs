using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using DAL.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.UnitTests.ServicesTests
{
    public class IssueServiceUnitTests
    {
        private Mock<IIssueRepo> issueRepositoryMock;
        private IssueServices issueService;

        [SetUp]
        public void Setup()
        {
            // Create a mock instance of the book repository
            issueRepositoryMock = new Mock<IIssueRepo>();

            // Create an instance of the book service and pass the mock repository
            issueService = new IssueServices(issueRepositoryMock.Object);
        }

        [Test]
        public void GetIssueById_ReturnIssue()
        {
            // Arrange
            int issueId = 1;
            var expectedIssue = new IssueDTO { Id = 1, BookId = 1, UserId = 1, IssueDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7) };
            issueRepositoryMock.Setup(repo => repo.GetIssueById(issueId)).Returns((Issue)expectedIssue);

            // Act
            var result = issueService.GetIssue(issueId);

            // Assert
            Assert.AreEqual(expectedIssue, result);

        }

        /*[Test]
        public void GetIssue_ExistingIssueId_ReturnsIssue()
        {
            // Arrange
            
            int issueId = 1;
            var expectedIssue = new IssueDTO { Id = 1, BookId = 1, UserId = 1, IssueDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7) };
            issueRepositoryMock.Setup(repo => repo.GetIssueById(issueId)).Returns((Issue)expectedIssue);

            // Act
            var result = issueService.GetIssue(issueId);

            // Assert
            Assert.AreEqual(expectedIssue, result);
        }*/

        [Test]
        public void AddIssue_ValidIssue_ReturnsTrue()
        {
            // Arrange
            
            var issueDto = new IssueDTO { Id = 1, BookId = 1, UserId = 1, IssueDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7) };
            issueRepositoryMock.Setup(repo => repo.AddIssue((Issue)issueDto)).Returns(true);

            // Act
            var result = issueService.AddIssue(issueDto);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteIssue_ExistingIssueId_ReturnsVoid()
        {
            // Arrange
           
            int issueId = 1;

            // Act
            issueService.DeleteIssue(issueId);

            // Assert
            issueRepositoryMock.Verify(repo => repo.DeleteIssue(issueId), Times.Never);
        }
    }
}
