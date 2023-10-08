using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Nimble.GuestbookApp.UseCases.Entries;

public record ListEntriesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<EntryDTO>>>;
