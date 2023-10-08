using Nimble.GuestbookApp.Core.GuestbookAggregate;
using Ardalis.SharedKernel;
using FastEndpoints;
using MediatR;

namespace Nimble.GuestbookApp.Web.Entries;

/// <summary>
/// Create a new Entry
/// </summary>
/// <remarks>
/// Creates a new Entry given a name.
/// </remarks>
public class Create : Endpoint<CreateEntryRequest, CreateEntryResponse>
{
  private readonly IMediator _mediator;

  public Create(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post(CreateEntryRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    CreateEntryRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateEntryCommand(request.EmailAddress!, request.Message!));

    if(result.IsSuccess)
    {
      Response = new CreateEntryResponse()
      {
        Id = result.Value,
        EmailAddress = request.EmailAddress!,
        Message = request.Message!
      };
    }
  }
}
