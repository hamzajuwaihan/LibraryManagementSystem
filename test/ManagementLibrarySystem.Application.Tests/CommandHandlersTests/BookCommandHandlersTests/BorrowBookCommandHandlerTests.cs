using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class BorrowBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly BorrowBookCommandHandler _handler;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    public BorrowBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMemberRepository = new Mock<IMemberRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _handler = new BorrowBookCommandHandler(_mockBookRepository.Object, _mockMemberRepository.Object, _mockHttpContextAccessor.Object);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnSuccessMessage_WhenBookIsSuccessfullyBorrowed()
    {
        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(memberId);

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Authoer"
        };

        Member member = new(memberId)
        {
            Name = "Hamza",
            Email = "Hamza@gmail.com"
        };

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString() }
        };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);


        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync(member);
        _mockBookRepository.Setup(repo => repo.UpdateBookById(bookId, It.IsAny<Book>())).ReturnsAsync(book);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Book borrowed successfully.", result);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(bookId, It.Is<Book>(b => b.IsBorrowed)), Times.Once);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnBookNotFound_WhenBookDoesNotExist()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(memberId);

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString() }
        };
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);

        await Assert.ThrowsAsync<BookNotFoundException>(() =>
                 _handler.Handle(command, CancellationToken.None));

        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }
    [Fact]
    public async Task BorrowBook_ShouldReturnAlreadyBorrowedMessage_WhenBookIsAlreadyBorrowed()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(memberId);

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Authoer",
            IsBorrowed = true
        };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);
        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString() }
        };
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);


        Exception exception = await Assert.ThrowsAsync<BookAlreadyBorrowedException>(() =>
         _handler.Handle(command, CancellationToken.None));

        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnMemberNotFound_WhenMemberDoesNotExist()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(memberId);

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Authoer",
            IsBorrowed = false
        };

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync((Member?)null);
        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString() }
        };
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);


        Exception exception = await Assert.ThrowsAsync<MemberNotFoundException>(() =>
        _handler.Handle(command, CancellationToken.None));
        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }

}
