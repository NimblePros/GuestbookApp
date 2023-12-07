using FastEndpoints;
using MediatR;
using Nimble.GuestbookApp.UseCases.Entries.Create;

namespace Nimble.GuestbookApp.Web.Entries;

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

    if (result.IsSuccess)
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
