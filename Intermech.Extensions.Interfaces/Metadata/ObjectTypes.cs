// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.ObjectTypes
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public abstract class ObjectTypes
{
  public const string EntityInstanceNameInGenitiveCase = "типа объекта";
  [NotNull]
  public const int UnknownID = -1;
  [NotNull]
  public static readonly UserObjectType User = UserObjectType.Create(nameof (User));
  [NotNull]
  public static readonly SystemObjectType UserGroup = ObjectTypes.CreateObligatory("cad00003-306c-11d8-b4e9-00304f19f545", nameof (UserGroup));
  [NotNull]
  public static readonly SystemObjectType Rank = ObjectTypes.CreateObligatory("cad00147-306c-11d8-b4e9-00304f19f545", nameof (Rank));
  [NotNull]
  public static readonly SystemObjectType MeasureUnit = ObjectTypes.CreateObligatory("cad0000b-306c-11d8-b4e9-00304f19f545", nameof (MeasureUnit));
  [NotNull]
  public static readonly SystemObjectType Scripts = ObjectTypes.Create("cad0036a-306c-11d8-b4e9-00304f19f545", nameof (Scripts));
  [NotNull]
  public static readonly SystemObjectType Forms = ObjectTypes.Create("cad0011b-306c-11d8-b4e9-00304f19f545", nameof (Forms));
  [NotNull]
  public static readonly SystemObjectType FormDataEditingType = ObjectTypes.Create("cad0011c-306c-11d8-b4e9-00304f19f545", nameof (FormDataEditingType));
  [NotNull]
  public static readonly SystemObjectType NoticesOnChanges = ObjectTypes.Create("cad00627-306c-11d8-b4e9-00304f19f545", nameof (NoticesOnChanges));
  [NotNull]
  public static readonly SystemObjectType PortalObjects = ObjectTypes.Create("cad01489-306c-11d8-b4e9-00304f19f545", nameof (PortalObjects));
  [NotNull]
  public static readonly SystemObjectType Notifications = ObjectTypes.Create("cad00629-306c-11d8-b4e9-00304f19f545", nameof (Notifications));
  [NotNull]
  public static readonly SystemObjectType Signatures = ObjectTypes.Create("cad00137-306c-11d8-b4e9-00304f19f545", nameof (Signatures));
  [NotNull]
  public static readonly SystemObjectType VersionRule = ObjectTypes.Create("cad001b3-306c-11d8-b4e9-00304f19f545", nameof (VersionRule));
  [NotNull]
  public static readonly SystemObjectType VersionRuleCommon = ObjectTypes.Create("cad001b4-306c-11d8-b4e9-00304f19f545", nameof (VersionRuleCommon));
  [NotNull]
  public static readonly SystemObjectType VersionRuleUser = ObjectTypes.Create("cad001b5-306c-11d8-b4e9-00304f19f545", nameof (VersionRuleUser));
  [NotNull]
  public static readonly SystemObjectType VersionRuleSystem = ObjectTypes.Create("cad00278-306c-11d8-b4e9-00304f19f545", nameof (VersionRuleSystem));
  [NotNull]
  public static readonly SystemObjectType EditingContexts = ObjectTypes.Create("cad0146b-306c-11d8-b4e9-00304f19f545", nameof (EditingContexts));
  [NotNull]
  public static readonly SystemObjectType IncompleteObject = ObjectTypes.Create("cadd960d-306c-11d8-b4e9-00304f19f545", nameof (IncompleteObject));
  [NotNull]
  public static readonly SystemObjectType Calendar = ObjectTypes.Create("cad00d87-306c-11d8-b4e9-00304f19f545", nameof (Calendar));
  [NotNull]
  public static readonly SystemObjectType OrganizationUnits = ObjectTypes.Create("cadd9235-306c-11d8-b4e9-00304f19f545", nameof (OrganizationUnits));
  public const string EntityName = "Типы объектов";
  public const string EntityInstanceName = "Тип объекта";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectType Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return ObjectTypes.Create<ObjectTypes>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectType CreateObligatory([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return ObjectTypes.Create<ObjectTypes>(guid, true, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemObjectType Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [CallerMemberName, NotNull, NotWhitespace] string idName = "") where THolder : ObjectTypes
  {
    Guid guid1 = new Guid(guid);
    return new SystemObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid1), guid1, typeof (THolder), false, idName);
  }

  [NotNull]
  [ItemNotEmpty]
  public static IReadOnlyCollection<int> GetRecursiveChildrenIDs(
    [NotEmpty] int parentObjectTypeID,
    bool includeParentObjectTypeID = true)
  {
    List<int> objectTypeChildrenId1 = MetaDataHelperService.Instance.GetObjectTypeChildrenID(parentObjectTypeID);
    if (objectTypeChildrenId1.Count == 0)
    {
      if (!includeParentObjectTypeID)
        return (IReadOnlyCollection<int>) Array.Empty<int>();
      return (IReadOnlyCollection<int>) new int[1]
      {
        parentObjectTypeID
      };
    }
    List<int> recursiveChildrenIds = new List<int>(Math.Max(32 /*0x20*/, objectTypeChildrenId1.Count * 4));
    if (includeParentObjectTypeID)
      recursiveChildrenIds.Add(parentObjectTypeID);
    int index = 0;
    while (index++ < objectTypeChildrenId1.Count)
    {
      int parentTypeID = objectTypeChildrenId1[index];
      recursiveChildrenIds.Add(parentTypeID);
      List<int> objectTypeChildrenId2 = MetaDataHelperService.Instance.GetObjectTypeChildrenID(parentTypeID);
      if (objectTypeChildrenId2.Count > 0)
      {
        foreach (int element in objectTypeChildrenId2)
        {
          if (!objectTypeChildrenId1.ContainsFrom<int>(index + 1, element))
            objectTypeChildrenId1.Add(element);
        }
      }
    }
    return (IReadOnlyCollection<int>) recursiveChildrenIds;
  }

  [NotNull]
  [ItemNotEmpty]
  public static IReadOnlyCollection<Guid> GetRecursiveChildrenGuids(
    [NotEmpty] Guid parentObjectTypeGuid,
    bool includeParentObjectTypeID = true)
  {
    List<Guid> typeChildrenGuid1 = MetaDataHelperService.Instance.GetObjectTypeChildrenGuid(parentObjectTypeGuid);
    if (typeChildrenGuid1.Count == 0)
    {
      if (!includeParentObjectTypeID)
        return (IReadOnlyCollection<Guid>) Array.Empty<Guid>();
      return (IReadOnlyCollection<Guid>) new Guid[1]
      {
        parentObjectTypeGuid
      };
    }
    List<Guid> recursiveChildrenGuids = new List<Guid>(Math.Max(32 /*0x20*/, typeChildrenGuid1.Count * 4));
    if (includeParentObjectTypeID)
      recursiveChildrenGuids.Add(parentObjectTypeGuid);
    int index = 0;
    while (index++ < typeChildrenGuid1.Count)
    {
      Guid parentTypeGuid = typeChildrenGuid1[index];
      recursiveChildrenGuids.Add(parentTypeGuid);
      List<Guid> typeChildrenGuid2 = MetaDataHelperService.Instance.GetObjectTypeChildrenGuid(parentTypeGuid);
      if (typeChildrenGuid2.Count > 0)
      {
        foreach (Guid element in typeChildrenGuid2)
        {
          if (!typeChildrenGuid1.ContainsFrom<Guid>(index + 1, element))
            typeChildrenGuid1.Add(element);
        }
      }
    }
    return (IReadOnlyCollection<Guid>) recursiveChildrenGuids;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Is([NotEmpty] int objectTypeID, [NotNull] SystemObjectType systemObjectType)
  {
    return systemObjectType.IsTypeOrChild(objectTypeID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Is([NotEmpty] Guid objectTypeGuid, [NotNull] SystemObjectType systemObjectType)
  {
    return systemObjectType.IsTypeOrChild(objectTypeGuid);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdFromGuid([NotEmpty] Guid guid, bool throwIfNotFound = true)
  {
    int objectTypeId = MetaDataHelperService.Instance.GetObjectTypeID(guid);
    return !throwIfNotFound || !Intermech.Check.ObjectTypeIdIsEmpty(objectTypeId) ? objectTypeId : throw new ObjectTypeNotFoundException(guid);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdFromGuid([NotNull, NotWhitespace] string guid, bool throwIfNotFound = true)
  {
    Guid guid1 = new Guid(guid);
    int relationTypeId = MetaDataHelperService.Instance.GetRelationTypeID(guid1);
    return !throwIfNotFound || !Intermech.Check.ObjectTypeIdIsEmpty(relationTypeId) ? relationTypeId : throw new ObjectTypeNotFoundException(guid1);
  }

  public abstract class Consts
  {
    public const string UserGuid = "cad00002-306c-11d8-b4e9-00304f19f545";
    public const string UserGroupGuid = "cad00003-306c-11d8-b4e9-00304f19f545";
    public const string RankGuid = "cad00147-306c-11d8-b4e9-00304f19f545";
    public const string MeasureGuid = "cad0000b-306c-11d8-b4e9-00304f19f545";
    public const string ScriptsGuid = "cad0036a-306c-11d8-b4e9-00304f19f545";
    public const string FormsGuid = "cad0011b-306c-11d8-b4e9-00304f19f545";
    public const string FormDataEditingTypeGuid = "cad0011c-306c-11d8-b4e9-00304f19f545";
    public const string NoticesOnChangesGuid = "cad00627-306c-11d8-b4e9-00304f19f545";
    public const string PortalObjectsGuid = "cad01489-306c-11d8-b4e9-00304f19f545";
    public const string NotificationsGuid = "cad00629-306c-11d8-b4e9-00304f19f545";
    public const string SignaturesGuid = "cad00137-306c-11d8-b4e9-00304f19f545";
    public const string VersionRuleGuid = "cad001b3-306c-11d8-b4e9-00304f19f545";
    public const string VersionRuleCommonGuid = "cad001b4-306c-11d8-b4e9-00304f19f545";
    public const string VersionRuleUserGuid = "cad001b5-306c-11d8-b4e9-00304f19f545";
    public const string VersionRuleSystemGuid = "cad00278-306c-11d8-b4e9-00304f19f545";
    public const string EditingContextsGuid = "cad0146b-306c-11d8-b4e9-00304f19f545";
    public const string IncompleteObjectGuid = "cadd960d-306c-11d8-b4e9-00304f19f545";
    public const string CalendarsGuid = "cad00d87-306c-11d8-b4e9-00304f19f545";
    public const string OrganizationUnitsGuid = "cadd9235-306c-11d8-b4e9-00304f19f545";
  }
}
