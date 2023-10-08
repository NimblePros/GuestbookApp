using System.Data;
using Microsoft.EntityFrameworkCore;
using Nimble.GuestbookApp.UseCases.Entries;

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

    if(firstGuestbook is null) return new List<EntryDTO>();
    
    return firstGuestbook.Entries
      .OrderByDescending(e => e.DateTimeCreated)
      .Select(entry =>
      new EntryDTO{
        Id = entry.Id,
        EmailAddress = entry.EmailAddress,
        Message = entry.Message,
        DateTimeCreated = entry.DateTimeCreated
      })
      .ToList();

  }
}


