// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationEntityBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

public class DBRelationEntityBuilder
{
  private Type childOccurenceType;

  public DBRelationEntityBuilder(Type childOccurenceType)
  {
    this.childOccurenceType = !(childOccurenceType == (Type) null) ? childOccurenceType : throw new ArgumentNullException(nameof (childOccurenceType));
  }

  public Type ChildOccurenceType
  {
    [DebuggerStepThrough] get => this.childOccurenceType;
  }
}
