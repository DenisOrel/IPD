// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.StartResolveBaseVersionConflictEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class StartResolveBaseVersionConflictEventArgs
{
  public Guid SessionGuid { get; private set; }

  public long TemplateID { get; private set; }

  public long ConflictedObjectID { get; private set; }

  public StartResolveBaseVersionConflictEventArgs(
    Guid sessionGuid,
    long templateID,
    long conflictedObjectID)
  {
    this.SessionGuid = sessionGuid;
    this.TemplateID = templateID;
    this.ConflictedObjectID = conflictedObjectID;
  }
}
