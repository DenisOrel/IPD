// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.CrossThreadConflictInfo
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class CrossThreadConflictInfo
{
  private Guid conflictId;

  public CrossThreadConflictInfo(Guid conflictId) => this.conflictId = conflictId;

  public Guid ConflictId
  {
    [DebuggerStepThrough] get => this.conflictId;
  }
}
