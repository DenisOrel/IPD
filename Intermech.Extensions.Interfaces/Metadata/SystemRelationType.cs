// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemRelationType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class SystemRelationType : IpsMetadataEntityType
{
  [CanBeNull]
  private readonly IMSRelationType _descriptor;

  private protected SystemRelationType([NotNull] SystemRelationType relationType)
    : base(relationType._id, relationType.Guid, relationType._HolderType, relationType.Obligatory, relationType._IdPropertyName)
  {
    this._descriptor = relationType._descriptor;
    this._Found = new bool?(relationType.Found);
  }

  protected internal SystemRelationType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
    this._Found = new bool?(!Intermech.Check.RelationTypeIdIsEmpty(id));
    if (this.Found)
      this._descriptor = MetaDataHelperService.Instance.GetRelationType(id);
    else if (obligatory)
      throw new RelationTypeNotFoundException(this.Guid, $"{this.FullPropertyName}: Тип связи с Guid={this.Guid} не найден!");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSRelationType GetDescriptor()
  {
    if (!this.Found)
      throw new RelationTypeNotFoundException(this.Guid, $"{this.FullPropertyName}: Тип связи с Guid={this.Guid} не найден!");
    return this._descriptor;
  }

  [NotNull]
  public IMSRelationType Descriptor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor();
  }

  [NotNull]
  [NotWhitespace]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.Description;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public RelationApplicability GetApplicability([NotEmpty] int parentObjectTypeID, [NotEmpty] int nestedObjectTypeID)
  {
    return new RelationApplicability(this, parentObjectTypeID, nestedObjectTypeID, true);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public RelationApplicability GetApplicability(
    [NotNull, NotEmptyGuid] string parentObjectTypeGuid,
    [NotNull, NotEmptyGuid] string nestedObjectTypeGuid)
  {
    return RelationApplicability.Create(this, parentObjectTypeGuid, nestedObjectTypeGuid, true);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public RelationApplicability GetApplicability(
    [NotEmptyGuid] Guid parentObjectTypeGuid,
    [NotEmptyGuid] Guid nestedObjectTypeGuid)
  {
    return RelationApplicability.Create(this, parentObjectTypeGuid, nestedObjectTypeGuid, true);
  }

  public abstract class Attributes
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal static SystemAttribute4RelationType Create(
      [NotNull, NotEmptyGuid] string relationTypeGuid,
      [NotNull] SystemAttribute attribute)
    {
      int relationTypeId = MetaDataHelperService.Instance.GetRelationTypeID(relationTypeGuid);
      return new SystemAttribute4RelationType(attribute, relationTypeId, true);
    }

    [NotEmpty]
    public int F_PRJLINK_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -20;
    }

    [NotEmpty]
    public int F_PROJ_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -21;
    }

    [NotEmpty]
    public int F_PART_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -22;
    }

    [NotEmpty]
    public int F_RELATION_TYPE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -23;
    }

    [NotEmpty]
    public int F_CREATE_DATE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -24;
    }

    [NotEmpty]
    public int F_PRJ_GUID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -26;
    }

    [NotEmpty]
    public int F_REL_CREATOR
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -82;
    }

    [NotNull]
    public static SystemAttribute PrjLinkID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.PrjLinkID;
      }
    }

    [NotNull]
    public static SystemAttribute RelationID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.RelationID;
      }
    }

    [NotNull]
    public static SystemAttribute ProjID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.ProjID;
    }

    [NotNull]
    public static SystemAttribute PartID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.PartID;
    }

    [NotNull]
    public static SystemAttribute Type
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.RelationType;
      }
    }

    [NotNull]
    public static SystemAttribute CreateDate
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.CreateDate;
      }
    }

    [NotNull]
    public static SystemAttribute ActualDate
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.RelationActualDate;
      }
    }

    [NotNull]
    public static SystemAttribute RelationCreator
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Creator;
      }
    }
  }
}
