// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel.Services.PortalServices;

internal class TransferedObjectTypeIDs
{
  public Type Type { get; }

  public int ID { get; }

  public TransferedObjectTypeIDs(Type type, int id)
  {
    this.Type = type;
    this.ID = id;
  }
}
