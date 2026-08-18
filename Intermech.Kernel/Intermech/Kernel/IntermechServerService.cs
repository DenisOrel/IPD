// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IntermechServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Diagnostics;


namespace Intermech.Kernel;

internal abstract class IntermechServerService : LongLifeObject
{
  private IntermechServer server;

  protected IntermechServerService(IntermechServer server) => this.server = server;

  protected IntermechServer Server
  {
    [DebuggerStepThrough] get => this.server;
  }
}
