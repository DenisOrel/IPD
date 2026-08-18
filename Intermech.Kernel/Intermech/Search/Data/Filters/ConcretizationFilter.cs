// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.ConcretizationFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public class ConcretizationFilter : FilterBase<ConcretizationFilter.ConcretizationFilterOptions>
{
  public ConcretizationFilter(IUserSession userSession, ConcretizationType type)
    : base(userSession)
  {
    this.Type = type;
  }

  public ConcretizationType Type { get; set; }

  public override bool Apply(Applicability applicability)
  {
    if (applicability == null)
      throw new ArgumentNullException(nameof (applicability));
    if (!this.CheckEnabled((RelationObjectBase) applicability) || Math.Abs(this.Options.PartVersionID) != Math.Abs(applicability.Relation.ExplicitPartVersionID) && (!this.Options.UseStoredExplicitPartVersionID || Math.Abs(this.Options.PartVersionID) != Math.Abs(this.GetStoredExplicitPartVersionID((RelationObjectBase) applicability))))
      return false;
    this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", applicability.Relation, (short) this.GetFiltrationState());
    return true;
  }

  public override bool Apply(CompositionPart compositionPart)
  {
    if (compositionPart == null)
      throw new ArgumentNullException(nameof (compositionPart));
    if (!this.CheckEnabled((RelationObjectBase) compositionPart) || Math.Abs(compositionPart.Object.VersionID) != Math.Abs(compositionPart.Relation.ExplicitPartVersionID) && (!this.Options.UseStoredExplicitPartVersionID || Math.Abs(compositionPart.Object.VersionID) != Math.Abs(this.GetStoredExplicitPartVersionID((RelationObjectBase) compositionPart))))
      return false;
    this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", compositionPart.Relation, (short) this.GetFiltrationState());
    return true;
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
      ColumnDescriptor columnDescriptor1;
      if (this.IsRevisionInstantiationModeSupported())
      {
        List<ColumnDescriptor> columnDescriptorList = columns;
        columnDescriptor1 = new ColumnDescriptor();
        columnDescriptor1.AttributeID = (object) Constants.RevisionInstantiationModeAttributeTypeID;
        columnDescriptor1.AttributeSource = AttributeSourceTypes.Relation;
        ColumnDescriptor columnDescriptor2 = columnDescriptor1;
        columnDescriptorList.Add(columnDescriptor2);
      }
      if (this.Options.UseStoredExplicitPartVersionID && this.IsStoredExplicitPartVersionIDSupported())
      {
        List<ColumnDescriptor> columnDescriptorList = columns;
        columnDescriptor1 = new ColumnDescriptor();
        columnDescriptor1.AttributeID = (object) Constants.StoredExplicitPartVersionIDAttributeTypeID;
        columnDescriptor1.AttributeSource = AttributeSourceTypes.Relation;
        ColumnDescriptor columnDescriptor3 = columnDescriptor1;
        columnDescriptorList.Add(columnDescriptor3);
      }
      return columns;
    }
  }

  protected override void CheckOptions(
    ConcretizationFilter.ConcretizationFilterOptions options)
  {
  }

  private RevisionInstantiationMode GetRevisionInstantiationMode(RelationObjectBase relationObject)
  {
    return (RevisionInstantiationMode) (long) (relationObject.Relation.Attributes.GetAttributeValue(Constants.RevisionInstantiationModeAttributeTypeID) ?? (object) 0L);
  }

  private long GetStoredExplicitPartVersionID(RelationObjectBase relationObject)
  {
    return !(relationObject.Relation.Attributes.GetAttributeValue(Constants.StoredExplicitPartVersionIDAttributeTypeID) is long attributeValue) ? 0L : attributeValue;
  }

  private ObjectFiltrationState GetFiltrationState()
  {
    return this.Type != ConcretizationType.Hard ? ObjectFiltrationState.fsSoftConcretised : ObjectFiltrationState.fsCompositeVersion;
  }

  private bool CheckEnabled(RelationObjectBase relationObject)
  {
    if (this.GetRevisionInstantiationMode(relationObject) == RevisionInstantiationMode.Hard)
      return this.Type == ConcretizationType.Hard;
    if (relationObject.Object.TypeID != -1 && (this.Options.ProjectVersionID != 0L || this.Options.PartVersionID != 0L))
    {
      IMSApplicability imsApplicability = (IMSApplicability) null;
      if (this.Options.ProjectTypeID != -1)
        imsApplicability = this.GetRelationApplicability(relationObject.Relation.TypeID, relationObject.Object.TypeID, this.Options.ProjectTypeID);
      else if (this.Options.PartTypeID != -1)
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

  private bool IsStoredExplicitPartVersionIDSupported()
  {
    return MetaDataHelper.GetAttribute4RelationType(this.Options.RelationTypeID, Constants.StoredExplicitPartVersionIDAttributeTypeID) != null;
  }

  public sealed class ConcretizationFilterOptions : FilterOptions
  {
    public bool UseStoredExplicitPartVersionID { get; set; }
  }
}
