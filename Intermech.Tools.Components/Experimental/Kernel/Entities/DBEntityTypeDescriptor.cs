// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>Реализация является thread safe.</summary>
internal abstract class DBEntityTypeDescriptor : 
  EntityTypeDescriptor,
  IDBEntityTypeDescriptor,
  IEntityTypeDescriptor
{
  private DBEntityKind entityKind;

  protected DBEntityTypeDescriptor(DBEntityKind entityKind, Type entityType)
    : base(entityType)
  {
    this.entityKind = entityKind;
  }

  public DBEntityKind EntityKind => this.entityKind;

  public abstract IDBObjectEntityTypeDescriptor AsDBObjectDescriptor();

  public abstract IDBRelationEntityTypeDescriptor AsDBRelationDescriptor();
}
