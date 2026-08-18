using Intermech.ApplicationModel;
using Intermech.Kernel.Services;
using System;
using System.Diagnostics;

namespace ConsoleServer;

internal abstract class RemoteObjectsCommand : AbstractConsoleCommand
{
  private readonly RemotingInfoService remotingInfoService;

  protected RemoteObjectsCommand(RemotingInfoService remotingInfoService)
  {
    this.remotingInfoService = remotingInfoService != null ? remotingInfoService : throw new ArgumentNullException(nameof (remotingInfoService));
  }

  protected RemotingInfoService RemotingInfoService
  {
    [DebuggerStepThrough] get => this.remotingInfoService;
  }
}
