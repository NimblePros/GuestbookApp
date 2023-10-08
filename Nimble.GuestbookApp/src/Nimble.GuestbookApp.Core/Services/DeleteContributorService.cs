﻿using Ardalis.Result;
using Nimble.GuestbookApp.Core.ContributorAggregate;
using Nimble.GuestbookApp.Core.ContributorAggregate.Events;
using Nimble.GuestbookApp.Core.Interfaces;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nimble.GuestbookApp.Core.Services;

public class DeleteContributorService : IDeleteContributorService
{
  private readonly IRepository<Contributor> _repository;
  private readonly IMediator _mediator;
  private readonly ILogger<DeleteContributorService> _logger;

  public DeleteContributorService(IRepository<Contributor> repository,
    IMediator mediator,
    ILogger<DeleteContributorService> logger)
  {
    _repository = repository;
    _mediator = mediator;
    _logger = logger;
  }

  public async Task<Result> DeleteContributor(int contributorId)
  {
    _logger.LogInformation("Deleting Contributor {contributorId}", contributorId);
    var aggregateToDelete = await _repository.GetByIdAsync(contributorId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new ContributorDeletedEvent(contributorId);
    await _mediator.Publish(domainEvent);
    return Result.Success();
  }
}
