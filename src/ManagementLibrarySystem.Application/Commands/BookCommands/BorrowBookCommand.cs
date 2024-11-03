using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class BorrowBookCommand() : IRequest<Book> { }
