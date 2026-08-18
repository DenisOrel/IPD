// Decompiled with JetBrains decompiler
// Type: ConsoleServer.VerboseRemoteObjectsCommand
// Assembly: ConsoleServer, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A2572001-4A8A-44C7-AECE-87B2080D6C9F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\ConsoleServer.exe

using Intermech.ApplicationModel;
using Intermech.Kernel.Services;
using System.Collections.Generic;

#nullable disable
namespace ConsoleServer;

internal sealed class VerboseRemoteObjectsCommand(RemotingInfoService remotingInfoService) : 
  RemoteObjectsCommand(remotingInfoService)
{
  protected override void DoInvoke(IConsoleService consoleService, List<string> commandArgs)
  {
    this.RemotingInfoService.Verbose = !this.RemotingInfoService.Verbose;
  }
}
