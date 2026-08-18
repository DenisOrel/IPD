// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.RelationApplicability
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class RelationApplicability : SystemRelationType
{
  [CanBeEmpty]
  protected int _ParentObjectTypeID;
  [CanBeEmpty]
  protected int _NestedObjectTypeID;
  [CanBeNull]
  private readonly IMSApplicability _applicability;

  public RelationApplicability(
    [NotNull] SystemRelationType relationType,
    [CanBeEmpty] int parentObjectTypeID,
    [CanBeEmpty] int nestedObjectTypeID,
    bool obligatory)
    : base(relationType)
  {
    this._ParentObjectTypeID = parentObjectTypeID;
    this._NestedObjectTypeID = nestedObjectTypeID;
    this._Found = new bool?(relationType.Found && !Intermech.Check.ObjectTypeIdIsEmpty(this._ParentObjectTypeID) && !Intermech.Check.ObjectTypeIdIsEmpty(this._NestedObjectTypeID));
    if (this.Found)
    {
      IMSApplicability applicability = MetaDataHelperService.Instance.GetApplicability(this._ParentObjectTypeID, this._NestedObjectTypeID, relationType.ID);
      this._Found = new bool?(applicability != null);
      if (this.Found)
        this._applicability = applicability;
    }
    if (!obligatory || this.Found)
      return;
    string relationTypeName = MetaDataHelperService.Instance.GetRelationTypeName(this.Guid);
    if (!string.IsNullOrWhiteSpace(relationTypeName))
      throw new RelationTypeNotFoundException(relationTypeName, $"{this.FullPropertyName}: Связь \"{relationTypeName}\" не применима от объекта типа \"{MetaDataHelperService.Instance.GetObjectTypeName(parentObjectTypeID)}\" к объекту типа типа \"{MetaDataHelperService.Instance.GetObjectTypeName(nestedObjectTypeID)}\"!");
    throw new RelationTypeNotFoundException(this.Guid, $"{this.FullPropertyName}: Связь с Guid={this.Guid} не применима от объекта типа \"{MetaDataHelperService.Instance.GetObjectTypeName(parentObjectTypeID)}\" к объекту типа типа \"{MetaDataHelperService.Instance.GetObjectTypeName(nestedObjectTypeID)}\"!");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RelationApplicability Create(
    [NotNull] SystemRelationType relationType,
    [NotNull, NotEmptyGuid] string parentObjectTypeGuid,
    [NotNull, NotEmptyGuid] string nestedObjectTypeGuid,
    bool obligatory)
  {
    int objectTypeId1 = MetaDataHelperService.Instance.GetObjectTypeID(parentObjectTypeGuid);
    int objectTypeId2 = MetaDataHelperService.Instance.GetObjectTypeID(nestedObjectTypeGuid);
    return new RelationApplicability(relationType, objectTypeId1, objectTypeId2, obligatory);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RelationApplicability Create(
    [NotNull] SystemRelationType relationType,
    [NotEmptyGuid] Guid parentObjectTypeGuid,
    [NotEmptyGuid] Guid nestedObjectTypeGuid,
    bool obligatory)
  {
    int objectTypeId1 = MetaDataHelperService.Instance.GetObjectTypeID(parentObjectTypeGuid);
    int objectTypeId2 = MetaDataHelperService.Instance.GetObjectTypeID(nestedObjectTypeGuid);
    return new RelationApplicability(relationType, objectTypeId1, objectTypeId2, obligatory);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSApplicability GetApplicability()
  {
    if (!this.Found)
      throw new RelationTypeNotFoundException(this.Guid, $"{this.FullPropertyName}: Тип связи с Guid={this.Guid} не найден!");
    return this._applicability;
  }

  [NotNull]
  public IMSApplicability Applicability
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetApplicability();
  }
}
