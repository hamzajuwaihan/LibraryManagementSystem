using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class BorrowBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly BorrowBookCommandHandler _handler;

    public BorrowBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new BorrowBookCommandHandler(_mockBookRepository.Object, _mockMemberRepository.Object);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnSuccessMessage_WhenBookIsSuccessfullyBorrowed()
    {
        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(bookId, memberId);

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
        BorrowBookCommand command = new(bookId, memberId);

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);

        string result = await _handler.Handle(command, CancellationToken.None);


        Assert.Equal("Book not found.", result);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }
    [Fact]
    public async Task BorrowBook_ShouldReturnAlreadyBorrowedMessage_WhenBookIsAlreadyBorrowed()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(bookId, memberId);

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Authoer",
            IsBorrowed = true
        };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Book is already borrowed.", result);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task BorrowBook_ShouldReturnMemberNotFound_WhenMemberDoesNotExist()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        BorrowBookCommand command = new(bookId, memberId);

        Book book = new(bookId)
        {
            Title = "New book",
            Author = "Some Authoer",
            IsBorrowed = false
        };

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync((Member?)null);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Member not found.", result);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Never);
    }

}
