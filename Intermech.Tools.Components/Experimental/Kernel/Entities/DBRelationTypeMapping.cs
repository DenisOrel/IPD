// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationTypeMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBRelationTypeMapping : FreezableObject
{
  private int id;
  private string name;

  public DBRelationTypeMapping(Guid guid)
  {
    this.Guid = guid;
    this.Id = -1;
  }

  public Guid Guid { get; private set; }

  public int Id
  {
    [DebuggerStepThrough] get => this.id;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Id));
      this.id = value;
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Name));
      this.name = value;
    }
  }
}
