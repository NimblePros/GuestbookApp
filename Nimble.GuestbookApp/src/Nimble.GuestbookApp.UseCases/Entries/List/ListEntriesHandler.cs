using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.UseCases.Entries.List;

public class ListEntriesHandler(IListEntriesQueryService queryService)
  : IQueryHandler<ListEntriesQuery, Result<IEnumerable<EntryDTO>>>
{
  private readonly IListEntriesQueryService _queryService = queryService;

  public async Task<Result<IEnumerable<EntryDTO>>> Handle(ListEntriesQuery request,
    CancellationToken cancellationToken)
  {
    var entries = await _queryService.ListAsync();
    return Result<IEnumerable<EntryDTO>>.Success(entries);
  }
}
