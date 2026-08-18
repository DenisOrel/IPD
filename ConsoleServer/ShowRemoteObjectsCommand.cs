// Decompiled with JetBrains decompiler
// Type: ConsoleServer.ShowRemoteObjectsCommand
// Assembly: ConsoleServer, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A2572001-4A8A-44C7-AECE-87B2080D6C9F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\ConsoleServer.exe

using Intermech.ApplicationModel;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;

#nullable disable
namespace ConsoleServer;

internal sealed class ShowRemoteObjectsCommand(RemotingInfoService remotingInfoService) : 
  RemoteObjectsCommand(remotingInfoService)
{
  protected override void DoInvoke(IConsoleService consoleService, List<string> commandArgs)
  {
    List<Tuple<string, int>> objectsStatistics = this.RemotingInfoService.GetMarshalledObjectsStatistics();
    objectsStatistics.Sort((Comparison<Tuple<string, int>>) ((x, y) => y.Item2.CompareTo(x.Item2)));
    foreach (Tuple<string, int> tuple in objectsStatistics)
      consoleService.WriteLine($"{tuple.Item2,6} : {tuple.Item1}");
  }
}
