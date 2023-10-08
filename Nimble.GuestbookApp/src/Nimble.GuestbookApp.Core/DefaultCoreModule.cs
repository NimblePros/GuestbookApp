using Autofac;
using Nimble.GuestbookApp.Core.Interfaces;
using Nimble.GuestbookApp.Core.Services;

namespace Nimble.GuestbookApp.Core;

/// <summary>
/// An Autofac module that is responsible for wiring up services defined in the Core project.
/// </summary>
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<DeleteContributorService>()
        .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
