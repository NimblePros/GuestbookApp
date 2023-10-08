using Ardalis.Result;
using Ardalis.SharedKernel;
using Nimble.GuestbookApp.Core.GuestbookAggregate;

public class CreateEntryHandler : ICommandHandler<CreateEntryCommand, Result<int>>
{
  private readonly IRepository<Guestbook> _guestbookRepo;

  public CreateEntryHandler(IRepository<Guestbook> guestbookRepo)
    {
    _guestbookRepo = guestbookRepo;
  }
  public async Task<Result<int>> Handle(CreateEntryCommand request,
    CancellationToken cancellationToken)
  {
    var guestbook = (await _guestbookRepo.ListAsync()).First();
    var newEntry = new GuestbookEntry()
    {
        EmailAddress = request.emailAddress,
        Message = request.message
    };
    guestbook.AddEntry(newEntry);
    await _guestbookRepo.SaveChangesAsync();

    return newEntry.Id;
  }
}
