// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectNavigationPropertyMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Runtime;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBObjectNavigationPropertyMapping : FreezableObject
{
  private bool isRelationStart;
  private bool isComplex;
  private DBRelationTypeMapping dbRelationType;
  private DataPropertyMappings dbRelationAttributes;
  private DBRelationApplicabilityMappings dbRelationApplicabilities;

  public DBObjectNavigationPropertyMapping(NavigationPropertyDescriptor propertyDescriptor)
  {
    this.PropertyDescriptor = propertyDescriptor;
  }

  public NavigationPropertyDescriptor PropertyDescriptor { get; private set; }

  public bool IsRelationStart
  {
    [DebuggerStepThrough] get => this.isRelationStart;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsRelationStart));
      this.isRelationStart = value;
    }
  }

  public bool IsComplex
  {
    [DebuggerStepThrough] get => this.isComplex;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsComplex));
      this.isComplex = value;
    }
  }

  public DBRelationTypeMapping DBRelationType
  {
    [DebuggerStepThrough] get => this.dbRelationType;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBRelationType));
      this.dbRelationType = value;
    }
  }

  public DataPropertyMappings DBRelationAttributes
  {
    [DebuggerStepThrough] get => this.dbRelationAttributes;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBRelationAttributes));
      this.dbRelationAttributes = value;
    }
  }

  public DBRelationApplicabilityMappings DBRelationApplicabilities
  {
    [DebuggerStepThrough] get => this.dbRelationApplicabilities;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBRelationApplicabilities));
      this.dbRelationApplicabilities = value;
    }
  }

  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.DBRelationType == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DBRelationType");
    if (this.DBRelationAttributes == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DBRelationAttributes");
    if (this.DBRelationApplicabilities == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DBRelationApplicabilities");
  }

  protected override void DoFreeze()
  {
    base.DoFreeze();
    this.DBRelationType.ValidateBeforeFreeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DataPropertyMapping>) this.DBRelationAttributes.AsCollection)
      freezableObject.ValidateBeforeFreeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DBRelationApplicabilityMapping>) this.DBRelationApplicabilities.AsCollection)
      freezableObject.ValidateBeforeFreeze();
    this.DBRelationType.Freeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DataPropertyMapping>) this.DBRelationAttributes.AsCollection)
      freezableObject.Freeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DBRelationApplicabilityMapping>) this.DBRelationApplicabilities.AsCollection)
      freezableObject.Freeze();
  }
}
