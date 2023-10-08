namespace Nimble.GuestbookApp.UseCases.Entries;

public interface IListEntriesQueryService
{
    Task<IEnumerable<EntryDTO>> ListAsync();
}
