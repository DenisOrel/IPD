// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemObjectType
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class SystemObjectType : IpsMetadataEntityType
{
  [CanBeNull]
  private readonly IMSObjectType _descriptor;

  protected internal SystemObjectType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
    this._Found = new bool?(!Intermech.Check.ObjectTypeIdIsEmpty(id));
    if (this.Found)
      this._descriptor = MetaDataHelperService.Instance.GetObjectType(id);
    else if (obligatory)
      throw new Intermech.Interfaces.ObjectTypeNotFoundException(this.Guid);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IMSObjectType GetDescriptor()
  {
    if (!this.Found)
      throw this.ObjectTypeNotFoundException(this.Guid);
    return this._descriptor;
  }

  [NotNull]
  private Intermech.Interfaces.ObjectTypeNotFoundException ObjectTypeNotFoundException([NotEmpty] Guid guid)
  {
    return new Intermech.Interfaces.ObjectTypeNotFoundException(this.Guid, $"{this.FullPropertyName}: Тип объекта с Guid={this.Guid} не найден!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool IsTypeOrChild([NotEmpty] int objTypeID)
  {
    if (!this.Found)
      throw this.ObjectTypeNotFoundException(this.Guid);
    return objTypeID == this.ID || MetaDataHelperService.Instance.IsObjectTypeChildOf(objTypeID, this.ID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool IsTypeOrChild([NotEmpty] Guid objTypeGuid)
  {
    if (!this.Found)
      throw this.ObjectTypeNotFoundException(this.Guid);
    return objTypeGuid == this.Guid || MetaDataHelperService.Instance.IsObjectTypeChildOf(objTypeGuid, this.Guid);
  }

  [NotNull]
  public IMSObjectType Descriptor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDescriptor();
  }

  [NotNull]
  [NotWhitespace]
  public string ObjectTypeName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.ObjectTypeName;
  }

  [NotNull]
  [NotWhitespace]
  public string ObjectName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.ObjectName;
  }

  [NotWhitespace]
  public bool IsLocalType
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Descriptor.IsLocalType;
  }

  public abstract class Attributes
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal static SystemAttribute4ObjectType Create(
      [NotNull, NotEmptyGuid] string objectTypeGuid,
      [NotNull] SystemAttribute attribute)
    {
      int objectTypeId = MetaDataHelperService.Instance.GetObjectTypeID(objectTypeGuid);
      return new SystemAttribute4ObjectType(attribute, objectTypeId, true);
    }

    [NotEmpty]
    public int F_OBJECT_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -2;
    }

    [NotEmpty]
    public int F_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -3;
    }

    [NotEmpty]
    public int F_LC_STEP
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -4;
    }

    [NotEmpty]
    public int F_VERSION_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -5;
    }

    [NotEmpty]
    public int F_CHKOUT_BY
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -6;
    }

    [NotEmpty]
    public int F_OBJECT_TYPE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -7;
    }

    [NotEmpty]
    public int F_OWNER_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -8;
    }

    [NotEmpty]
    public int F_LEVEL_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -9;
    }

    [NotEmpty]
    public int F_MODIFY_DATE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -10;
    }

    [NotEmpty]
    public int F_GUID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -12;
    }

    [NotEmpty]
    public int F_OBJ_CREATE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -13;
    }

    [NotEmpty]
    public int F_PROJECT_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -14;
    }

    [NotEmpty]
    public int F_MODIFICATION_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -15;
    }

    [NotEmpty]
    public int F_BASE_VERSION
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -16;
    }

    [NotEmpty]
    public int F_SITE_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -17;
    }

    [NotEmpty]
    public int F_OBJ_GUID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -18;
    }

    [NotEmpty]
    public int F_OBJECT_VER_TYPE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -19;
    }

    [NotEmpty]
    public int CAPTION
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -50;
    }

    [NotEmpty]
    public int F_ACCESS
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -80;
    }

    [NotEmpty]
    public int F_CREATOR_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -81;
    }

    [NotEmpty]
    public int F_PARENT_OBJECT_ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -83;
    }

    [NotEmpty]
    public int F_VERSIONS_COUNT
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -84;
    }

    [NotEmpty]
    public int F_REFERENCE_COUNT
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -85;
    }

    [NotEmpty]
    public int F_RELATIONS_COUNT
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -86;
    }

    [NotEmpty]
    public int F_LCSTEP_DATE
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => -87;
    }

    [NotNull]
    public static SystemAttribute VersionID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ObjectVersionID;
      }
    }

    [NotNull]
    public static SystemAttribute ID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.ID;
    }

    [NotNull]
    public static SystemAttribute ObjectID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ObjectID;
      }
    }

    [NotNull]
    public static SystemAttribute VersionGuid
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ObjectVersionGuid;
      }
    }

    [NotNull]
    public static SystemAttribute Guid
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.Guid;
    }

    [NotNull]
    public static SystemAttribute ObjectGuid
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ObjectGuid;
      }
    }

    [NotNull]
    public static SystemAttribute VersionNum
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.VersionNum;
      }
    }

    [NotNull]
    public static SystemAttribute VersionType
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.VersionType;
      }
    }

    [NotNull]
    public static SystemAttribute IsBaseVersion
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.IsBaseVersion;
      }
    }

    [NotNull]
    public static SystemAttribute ParentVersionID
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ParentVersionID;
      }
    }

    [NotNull]
    public static SystemAttribute VersionsCount
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.VersionsCount;
      }
    }

    [NotNull]
    public static SystemAttribute ObjectType
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ObjectType;
      }
    }

    [NotNull]
    public static SystemAttribute Name
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.Name;
    }

    [NotNull]
    public static SystemAttribute Designation
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Metadata.Attributes.Designation;
    }

    [NotNull]
    public static SystemAttribute Caption
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Caption;
      }
    }

    [NotNull]
    public static SystemAttribute Note
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Metadata.Attributes.Note;
    }

    [NotNull]
    public static SystemAttribute LcStep
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.LcStep;
    }

    [NotNull]
    public static SystemAttribute LcChanged
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.LcChanged;
      }
    }

    [NotNull]
    public static SystemAttribute Level
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.Level;
    }

    [NotNull]
    public static SystemAttribute Project
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Project;
      }
    }

    [NotNull]
    public static SystemAttribute Site
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.Site;
    }

    [NotNull]
    public static SystemAttribute AccessLevel
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.AccessLevel;
      }
    }

    [NotNull]
    public static SystemAttribute Creator
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Creator;
      }
    }

    [NotNull]
    public static SystemAttribute Owner
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (SystemAttribute) Intermech.Metadata.Attributes.Owner;
    }

    [NotNull]
    public static SystemAttribute CheckedOutBy
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.CheckedOutBy;
      }
    }

    [NotNull]
    public static SystemAttribute Author
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Metadata.Attributes.Author;
    }

    [NotNull]
    public static SystemAttribute CheckedBy
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Metadata.Attributes.CheckedBy;
    }

    [NotNull]
    public static SystemAttribute ConfirmBy
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Metadata.Attributes.ConfirmBy;
    }

    [NotNull]
    public static SystemAttribute Modified
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Modified;
      }
    }

    [NotNull]
    public static SystemAttribute Created
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Created;
      }
    }

    [NotNull]
    public static SystemAttribute Modification
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.Modification;
      }
    }

    [NotNull]
    public static SystemAttribute ReferencesCount
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.ReferencesCount;
      }
    }

    [NotNull]
    public static SystemAttribute UsageCount
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemAttribute) Intermech.Metadata.Attributes.UsageCount;
      }
    }
  }

  public abstract class ConsistOf
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return RelationApplicability.Create(relationType, parentTypeGuid, nestedTypeGuid, true);
    }
  }

  public abstract class UsedIn
  {
    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return RelationApplicability.Create(relationType, parentTypeGuid, nestedTypeGuid, true);
    }
  }
}
