// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectTypeMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBObjectTypeMapping : FreezableObject
{
  private Guid guid;
  private int id;
  private string name;
  private bool isLocalType;
  private bool Type;

  public DBObjectTypeMapping(Guid guid)
  {
    this.guid = guid;
    this.id = -1;
  }

  public Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  public int Id
  {
    [DebuggerStepThrough] get => this.id;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Id));
      this.id = value;
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Name));
      this.name = value;
    }
  }

  public bool IsLocalType
  {
    [DebuggerStepThrough] get => this.isLocalType;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsLocalType));
      this.isLocalType = value;
    }
  }

  /// <summary>
  /// Возвращает или задает признак, что у типа объектов нет производных типов.
  /// </summary>
  public bool IsLeafType
  {
    [DebuggerStepThrough] get => this.Type;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsLeafType));
      this.Type = value;
    }
  }
}
