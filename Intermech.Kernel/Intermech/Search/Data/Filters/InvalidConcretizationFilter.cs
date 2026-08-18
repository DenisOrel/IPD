// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.InvalidConcretizationFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Data.Filters;

public sealed class InvalidConcretizationFilter : FilterBase
{
  public InvalidConcretizationFilter(
    IUserSession userSession,
    ConcretizationType concretizationType)
    : base(userSession)
  {
    this.Type = concretizationType;
  }

  public ConcretizationType Type { get; private set; }

  public override bool Apply(CompositionPart compositionPart)
  {
    throw new InvalidOperationException();
  }

  public override bool Apply(Applicability applicability) => throw new InvalidOperationException();

  public override IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities)
  {
    return this.ApplyInternal((IEnumerable<RelationObjectBase>) applicabilities).Cast<Applicability>();
  }

  public override IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition)
  {
    return this.ApplyInternal((IEnumerable<RelationObjectBase>) composition).Cast<CompositionPart>();
  }

  public override List<ColumnDescriptor> Columns
  {
    get
    {
      List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor()
        {
          AttributeID = (object) Constants.VersionIDInCompositionAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
          AttributeSource = AttributeSourceTypes.Relation
        }
      };
      if (this.IsRevisionInstantiationModeSupported())
        columns.Add(new ColumnDescriptor()
        {
          AttributeID = (object) Constants.RevisionInstantiationModeAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Relation
        });
      return columns;
    }
  }

  private IEnumerable<RelationObjectBase> ApplyInternal(
    IEnumerable<RelationObjectBase> relationObjects)
  {
    if (relationObjects.Count<RelationObjectBase>() == 0 || !this.CheckEnabled(relationObjects.ElementAt<RelationObjectBase>(0)))
      return (IEnumerable<RelationObjectBase>) new List<RelationObjectBase>(0);
    IEnumerable<RelationObjectBase> source = relationObjects.Where<RelationObjectBase>((Func<RelationObjectBase, bool>) (o => !ObjectHelper.IsUnknownObjectVersionID(o.Relation.ExplicitPartVersionID) && Math.Abs(o.Relation.ExplicitPartVersionID) != Math.Abs(o.Object.VersionID)));
    if (this.Type == ConcretizationType.Soft)
      source = source.Where<RelationObjectBase>((Func<RelationObjectBase, bool>) (o => this.GetRevisionInstantiationMode(o) != RevisionInstantiationMode.Hard));
    List<RelationObjectBase> relationObjectBaseList = new List<RelationObjectBase>();
    foreach (RelationObjectBase relationObjectBase in source)
    {
      this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObjectBase.Relation, (short) 1);
      relationObjectBaseList.Add(relationObjectBase);
    }
    return (IEnumerable<RelationObjectBase>) relationObjectBaseList;
  }

  private RevisionInstantiationMode GetRevisionInstantiationMode(RelationObjectBase relationObject)
  {
    return (RevisionInstantiationMode) (long) (relationObject.Relation.Attributes.GetAttributeValue(Constants.RevisionInstantiationModeAttributeTypeID) ?? (object) 0L);
  }

  private bool CheckEnabled(RelationObjectBase relationObject)
  {
    if (this.GetRevisionInstantiationMode(relationObject) == RevisionInstantiationMode.Hard)
      return this.Type == ConcretizationType.Hard;
    if (relationObject.Object.TypeID != -1 && this.Options.RelationTypeID != -1 && (this.Options.ProjectVersionID != 0L || this.Options.PartVersionID != 0L))
    {
      IMSApplicability imsApplicability = (IMSApplicability) null;
      if (this.Options.ProjectVersionID != 0L)
        imsApplicability = this.GetRelationApplicability(relationObject.Relation.TypeID, relationObject.Object.TypeID, this.Options.ProjectTypeID);
      else if (this.Options.PartVersionID != 0L)
        imsApplicability = this.GetRelationApplicability(relationObject.Relation.TypeID, this.Options.PartTypeID, relationObject.Object.TypeID);
      if (imsApplicability != null)
      {
        if (this.Type == ConcretizationType.Hard && !imsApplicability.Options.HasFlag((Enum) ApplicabilityOptions.SoftInstantiation))
          return true;
        return this.Type == ConcretizationType.Soft && imsApplicability.Options.HasFlag((Enum) ApplicabilityOptions.SoftInstantiation);
      }
    }
    return false;
  }

  private IMSApplicability GetRelationApplicability(
    int relationTypeID,
    int partTypeID,
    int projectTypeID)
  {
    return MetaDataHelper.GetApplicability(projectTypeID, partTypeID, relationTypeID);
  }

  private bool IsRevisionInstantiationModeSupported()
  {
    return MetaDataHelper.GetAttribute4RelationType(this.Options.RelationTypeID, Constants.RevisionInstantiationModeAttributeTypeID) != null;
  }
}
