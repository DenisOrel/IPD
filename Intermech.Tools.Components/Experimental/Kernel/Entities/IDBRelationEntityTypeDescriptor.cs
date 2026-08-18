// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.IDBRelationEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

internal interface IDBRelationEntityTypeDescriptor : IDBEntityTypeDescriptor, IEntityTypeDescriptor
{
  DataPropertyDescriptor KeyProperty { get; }

  DataPropertyDescriptor GuidProperty { get; }

  NavigationPropertyDescriptor RelationStartProperty { get; }

  NavigationPropertyDescriptor RelationEndProperty { get; }

  long GetKey(object relationEntity);

  void SetKey(object relationEntity, long newKey);

  Guid GetGuid(object relationEntity);

  void SetGuid(object relationEntity, Guid newGuid);

  object GetRelationStart(object relationEntity);

  void SetRelationStart(object relationEntity, object parentEntity);

  object GetRelationEnd(object relationEntity);

  void SetRelationEnd(object relationEntity, object childEntity);

  object CreateInstance(object parentEntity, object childEntity);
}
