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
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;

    private readonly BorrowBookCommandHandler _handler;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    public BorrowBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMemberRepository = new Mock<IMemberRepository>();
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _handler = new BorrowBookCommandHandler(_mockBookRepository.Object, _mockHttpContextAccessor.Object);
    }


    [Fact]
    public async Task BorrowBook_ShouldReturnBook_WhenBookIsSuccessfullyBorrowed()
    {
        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        Guid libraryId = Guid.NewGuid();

        Book book = new(bookId)
        {
            Title = "New Book",
            Author = "Some Author",
            IsBorrowed = false,
            LibraryId = libraryId
        };

        Member member = new(memberId)
        {
            Name = "Hamza",
            Email = "Hamza@gmail.com"
        };

        Library library = new(libraryId)
        {
            Name = "test lib",
            Members = new List<Member> { member }
        };

        BorrowBookCommand command = new();

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
    {
        { "id", bookId.ToString() },
        { "memberId", memberId.ToString() }
    };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync(member);
        _mockLibraryRepository.Setup(repo => repo.GetLibraryById(libraryId)).ReturnsAsync(library);

        Book borrowedBook = new(bookId)
        {
            Title = "New Book",
            Author = "Some Author",
            IsBorrowed = true,
            LibraryId = libraryId,
            BorrowedBy = memberId,
            BorrowedDate = DateTime.UtcNow
        };

        _mockBookRepository.Setup(repo => repo.BorrowBook(bookId, memberId)).ReturnsAsync(borrowedBook);

        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        Assert.True(result.IsBorrowed);
        Assert.Equal(memberId, result.BorrowedBy);
        Assert.True(result.BorrowedDate.HasValue);

        _mockBookRepository.Verify(repo => repo.BorrowBook(bookId, memberId), Times.Once);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnBookNotFound_WhenBookDoesNotExist()
    {


        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new();

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString()},
            { "memberId", memberId.ToString() }
        };
        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);

        _mockBookRepository.Setup(repo => repo.BorrowBook(bookId, memberId))
            .ThrowsAsync(new BookNotFoundException());

        await Assert.ThrowsAsync<BookNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnAlreadyBorrowedMessage_WhenBookIsAlreadyBorrowed()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new();

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Author",
            IsBorrowed = true
        };

        _mockBookRepository.Setup(repo => repo.BorrowBook(bookId, memberId)).ThrowsAsync(new BookAlreadyBorrowedException());

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", bookId.ToString() },
            { "memberId", memberId.ToString() }
        };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);

        Exception exception = await Assert.ThrowsAsync<BookAlreadyBorrowedException>(() =>
            _handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task BorrowBook_ShouldReturnMemberNotFoundException_MemberNotFoundException()
    {


        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new();

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Author",
            IsBorrowed = false
        };

        _mockBookRepository.Setup(repo => repo.BorrowBook(bookId, memberId)).ThrowsAsync(new MemberNotFoundException());


        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
    {
        { "id", bookId.ToString() },
        { "memberId", memberId.ToString() }
    };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);

        Exception exception = await Assert.ThrowsAsync<MemberNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

    }

}
