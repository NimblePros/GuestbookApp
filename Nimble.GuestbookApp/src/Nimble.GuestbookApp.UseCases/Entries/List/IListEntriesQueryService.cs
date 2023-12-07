namespace Nimble.GuestbookApp.UseCases.Entries.List;

public interface IListEntriesQueryService
{
  Task<IEnumerable<EntryDTO>> ListAsync();
}
