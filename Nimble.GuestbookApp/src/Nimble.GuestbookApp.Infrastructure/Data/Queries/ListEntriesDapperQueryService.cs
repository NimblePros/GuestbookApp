using System.Data;
using Dapper;
using Nimble.GuestbookApp.UseCases.Entries;

namespace Nimble.GuestbookApp.Infrastructure.Data.Queries;

public class ListEntriesDapperQueryService : IListEntriesQueryService
{
  private readonly DapperContext _dapper;

  public ListEntriesDapperQueryService(DapperContext dapper)
  {
    _dapper = dapper;
  }

  public async Task<IEnumerable<EntryDTO>> ListAsync()
  {
    // configure Dapper types
    SqlMapper.ResetTypeHandlers();
    SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
    SqlMapper.AddTypeHandler(new GuidHandler());
    SqlMapper.AddTypeHandler(new TimeSpanHandler());
    
    var query = "SELECT Id, EmailAddress, Message, DateTimeCreated FROM GuestbookEntry";
    using (var connection = _dapper.CreateConnection())
    {
        var entries = await connection.QueryAsync<EntryDTO>(query);
        return entries;
    }
  }
}

abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    // Parameters are converted by Microsoft.Data.Sqlite
    public override void SetValue(IDbDataParameter parameter, T? value)
        => parameter.Value = value;
}

class DateTimeOffsetHandler : SqliteTypeHandler<DateTimeOffset>
{
    public override DateTimeOffset Parse(object value)
        => DateTimeOffset.Parse((string)value);
}

class GuidHandler : SqliteTypeHandler<Guid>
{
    public override Guid Parse(object value)
        => Guid.Parse((string)value);
}

class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
        => TimeSpan.Parse((string)value);
}
