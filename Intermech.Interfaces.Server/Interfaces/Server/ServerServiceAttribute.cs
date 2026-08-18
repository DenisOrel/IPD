// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ServerServiceAttribute
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Server;

[AttributeUsage(AttributeTargets.Class)]
public class ServerServiceAttribute : Attribute
{
  private bool isClientVisible;

  public bool ClientVisible
  {
    [DebuggerStepThrough] get => this.isClientVisible;
    [DebuggerStepThrough] set => this.isClientVisible = value;
  }
}
