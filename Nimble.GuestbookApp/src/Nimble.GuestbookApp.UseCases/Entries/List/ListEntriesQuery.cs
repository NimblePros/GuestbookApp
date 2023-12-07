using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.UseCases.Entries.List;
public record ListEntriesQuery(int? Skip, int? Take)
  : IQuery<Result<IEnumerable<EntryDTO>>>;
