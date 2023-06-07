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
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IBookRepo> _mockBookRepository;
        private BookServices _bookService;

        [SetUp]
        public void Setup()
        {
            // Create a mock instance of the book repository
            _mockBookRepository = new Mock<IBookRepo>();

            // Create an instance of the book service and pass the mock repository
            _bookService = new BookServices(_mockBookRepository.Object);
        }

        [Test]
        public void GetBook_WithValidId_ReturnsBook()
        {
            // Arrange
            int bookId = 1;
            Book expectedBook = new Book { BookId = bookId, Title = "Sample Book" };
            _mockBookRepository.Setup(repo => repo.SearchBook(bookId)).Returns(expectedBook);

            // Act
            Book actualBook = _bookService.SearchBook(expectedBook.BookId);

            // Assert
            Assert.AreEqual(expectedBook, actualBook);
        }

        [Test]
        public void GetBook_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int bookId = 100;
            _mockBookRepository.Setup(repo => repo.SearchBook(bookId)).Returns((Book)null);

            // Act
            Book actualBook = _bookService.SearchBook(bookId);

            // Assert
            Assert.IsNull(actualBook);
        }

        [Test]
        public void GetAllBooks_ReturnsListOfBooks()
        {
            // Arrange
            var expectedBooks = new List<Book>
{
new Book { BookId = 1, Title = "Book 1" },
new Book { BookId = 2, Title = "Book 2" },
new Book { BookId = 3, Title = "Book 3" }
};
            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(expectedBooks);

            // Act
            var actualBooks = _bookService.GetBooks();

            // Assert
            Assert.That(actualBooks, Is.EqualTo(expectedBooks));
        }

        [Test]
        public void AddBook_ValidBook_ReturnTrue()
        {
            // Arrange
            var bookDto = new BookDTO { Title = "New Book", Author = "Priya" };
            _mockBookRepository.Setup(r => r.SearchBook(bookDto.BookId)).Returns((Book)null);
            _mockBookRepository.Setup(r => r.AddBook(It.IsAny<Book>())).Returns(true);

            // Act
            var result = _bookService.AddBook(bookDto);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateBook_ExistingBook_ReturnsUpdatedBook()
        {
            // Arrange
            var updatedBook = new Book { BookId = 1, Title = "Updated Book" };

            // Act

            _bookService.UpdateBook(1, updatedBook);

            // Assert
            _mockBookRepository.Verify(r => r.UpdateBook(updatedBook), Times.Once);

        }

        [Test]
        public void DeleteBook_ExistingBook_ReturnsDeletedBook()
        {
            // Arrange
            int bookId = 1;
            var deletedBook = new Book { BookId = bookId, Title = "Deleted Book" };
            _bookService.DeleteBook(1);

            // Assert
            _mockBookRepository.Verify(r => r.DeleteBook(1), Times.Once);
        }
    }
}
