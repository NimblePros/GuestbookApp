using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.UseCases.Entries.Create;
public record CreateEntryCommand(string emailAddress, string message)
  : ICommand<Result<int>>;
