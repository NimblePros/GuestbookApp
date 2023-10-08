using FastEndpoints;
using FluentValidation;
using Nimble.GuestbookApp.Infrastructure.Data.Config;

namespace Nimble.GuestbookApp.Web.Entries;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateEntryValidator : Validator<CreateEntryRequest>
{
  public CreateEntryValidator()
  {
    RuleFor(x => x.EmailAddress)
      .NotEmpty()
      .WithMessage("Email Address is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    RuleFor(x => x.Message)
      .NotEmpty()
      .WithMessage("Message is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
  }
}
