// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationApplicabilityMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBRelationApplicabilityMapping : FreezableObject
{
  private int parentObjectTypeId;
  private int childObjectTypeId;
  private bool isContent;

  public DBRelationApplicabilityMapping()
  {
    this.parentObjectTypeId = -1;
    this.childObjectTypeId = -1;
  }

  public int ParentObjectTypeId
  {
    [DebuggerStepThrough] get => this.parentObjectTypeId;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (ParentObjectTypeId));
      this.parentObjectTypeId = value;
    }
  }

  public int ChildObjectTypeId
  {
    [DebuggerStepThrough] get => this.childObjectTypeId;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (ChildObjectTypeId));
      this.childObjectTypeId = value;
    }
  }

  public bool IsContent
  {
    [DebuggerStepThrough] get => this.isContent;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsContent));
      this.isContent = value;
    }
  }
}
