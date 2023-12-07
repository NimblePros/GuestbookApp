using Nimble.GuestbookApp.UseCases.Entries.List;
using Nimble.GuestbookApp.UseCases.Entries;
using Microsoft.EntityFrameworkCore;

namespace Nimble.GuestbookApp.Infrastructure.Data.Queries;

public class ListEntriesQueryService : IListEntriesQueryService
{
  private readonly AppDbContext _dbContext;

  public ListEntriesQueryService(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<EntryDTO>> ListAsync()
  {
    var firstGuestbook = await _dbContext.Guestbooks
                    .Include(g => g.Entries)
                    .FirstOrDefaultAsync();

    if (firstGuestbook is null) return new List<EntryDTO>();

    return firstGuestbook.Entries
      .Select(entry =>
      new EntryDTO(entry.Id, entry.EmailAddress, 
        entry.Message, entry.DateTimeCreated))
      .OrderByDescending(entry => entry.DateTimeCreated)
      .ToList();

  }
}
