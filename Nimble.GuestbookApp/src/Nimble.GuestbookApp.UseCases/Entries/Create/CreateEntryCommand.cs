using Ardalis.Result;

public record CreateEntryCommand(string emailAddress, string message): Ardalis.SharedKernel.ICommand<Result<int>>;
