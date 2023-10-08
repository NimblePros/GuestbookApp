using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.UseCases.Entries;

public class ListEntriesHandler : 
  IQueryHandler<ListEntriesQuery, Result<IEnumerable<EntryDTO>>>
{
  private readonly IListEntriesQueryService _query;

  public ListEntriesHandler(IListEntriesQueryService query)
  {
    _query = query;
  }

  public async Task<Result<IEnumerable<EntryDTO>>> Handle(ListEntriesQuery request,
    CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
