// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDBObjectExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDBObjectExtensions
{
  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TInterface CastToInterface<TInterface>([NotNull] this IDBObject dbObject) where TInterface : class, IDBObject
  {
    return dbObject.CastInterfaceToOtherInterface<IDBObject, TInterface>();
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TInterface CastToInterface<TInterface, TInvalidCastException>(
    [NotNull] this IDBObject dbObject)
    where TInterface : class, IDBObject
    where TInvalidCastException : InvalidCastException
  {
    return dbObject.CastInterfaceToOtherInterface<IDBObject, TInterface, TInvalidCastException>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsTypeOrSubtype([NotNull] this IDBObject iDbObject, [NotEmpty] int objectTypeID)
  {
    return iDbObject.ObjectType == objectTypeID || MetaDataHelperService.Instance.IsObjectTypeChildOf(iDbObject.ObjectType, objectTypeID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void CheckIsTypeOrSubtype(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] int objectTypeID,
    [CanBeNull] string exceptionMessage = null)
  {
    if (!iDbObject.IsTypeOrSubtype(objectTypeID))
    {
      string message = exceptionMessage;
      if (message == null)
        message = $"Object \"{iDbObject.Caption}\" with type \"{MetaDataHelperService.Instance.GetObjectTypeName(iDbObject.TypeID)}\" isn`t type or subtype of type \"{MetaDataHelperService.Instance.GetObjectTypeName(objectTypeID)}\"";
      throw new Exception(message);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ConsistOf(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long projectID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).ConsistOf(projectID, conditions, columns, recursive, actualDate);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ConsistOf(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long projectID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).ConsistOf(projectID, conditions, columns, recursive, actualDate);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectComposition(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [CanBeEmpty] int objectTypeID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    ICompositionLoadService customService = iDbObject.Session.GetCustomService<ICompositionLoadService>();
    if (objectTypeID == -1)
      objectTypeID = iDbObject.TypeID;
    if (searchRelationTypes == null || searchObjectTypes == null)
    {
      List<IMSApplicability> typeApplicabilities = MetaDataHelperService.Instance.GetObjectTypeApplicabilities(objectTypeID);
      if (typeApplicabilities.Count == 0)
      {
        searchRelationTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
      }
      else
      {
        searchRelationTypes = (IReadOnlyCollection<int>) typeApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.RelationTypeID)).Distinct<int>().ToList<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) typeApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.ChildObjectTypeID)).Distinct<int>().ToList<int>();
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<Guid> sessionGuid = (System.ValueType) iDbObject.Session.SessionGUID;
    long objectId = objectVersionID;
    int objectType = objectTypeID;
    IReadOnlyCollection<int> searchRelationTypes1 = searchRelationTypes;
    IReadOnlyCollection<int> searchObjectTypes1 = searchObjectTypes;
    IReadOnlyCollection<ColumnDescriptor> columns1 = columns;
    int num = grouping ? 1 : 0;
    VersionsRule rule1 = rule;
    IReadOnlyCollection<ConditionStructure> conditions1 = conditions;
    string filtrationOwnerId = filtrationOwnerID;
    int loadLevels1 = loadLevels;
    IReadOnlyCollection<int> expandObjectTypes1 = expandObjectTypes;
    return customService.LoadComposition((object) sessionGuid, objectId, objectType, (IEnumerable<int>) searchRelationTypes1, (IEnumerable<int>) searchObjectTypes1, (IEnumerable<ColumnDescriptor>) columns1, true, num != 0, rule1, (IEnumerable<ConditionStructure>) conditions1, filtrationOwnerId, (HybridDictionary) null, loadLevels1, (IEnumerable<int>) expandObjectTypes1);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectComposition(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    return iDbObject.GetObjectComposition(objectVersionID, -1, columns, conditions, searchRelationTypes, searchObjectTypes, expandObjectTypes, grouping, rule, filtrationOwnerID, loadLevels);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersIn(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + iDbObject.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersIn(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + iDbObject.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersInVersion(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + iDbObject.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersInVersion(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return iDbObject.Session.GetRelationCollection(relationTypeID).EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + iDbObject.Session.TimeZoneOffset);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetApplicability(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [CanBeEmpty] int objectTypeID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    ICompositionLoadService customService = iDbObject.Session.GetCustomService<ICompositionLoadService>();
    if (objectTypeID == -1)
      objectTypeID = iDbObject.TypeID;
    if (searchRelationTypes == null || searchObjectTypes == null)
    {
      List<IMSApplicability> parentApplicabilities = MetaDataHelperService.Instance.GetObjectTypeParentApplicabilities(objectTypeID);
      if (parentApplicabilities.Count == 0)
      {
        searchRelationTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
      }
      else
      {
        searchRelationTypes = (IReadOnlyCollection<int>) parentApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.RelationTypeID)).Distinct<int>().ToList<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) parentApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.InObjectType)).Distinct<int>().ToList<int>();
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<Guid> sessionGuid = (System.ValueType) iDbObject.Session.SessionGUID;
    long objectId = objectVersionID;
    int objectType = objectTypeID;
    IReadOnlyCollection<int> searchRelationTypes1 = searchRelationTypes;
    IReadOnlyCollection<int> searchObjectTypes1 = searchObjectTypes;
    IReadOnlyCollection<ColumnDescriptor> columns1 = columns;
    int num = grouping ? 1 : 0;
    VersionsRule rule1 = rule;
    IReadOnlyCollection<ConditionStructure> conditions1 = conditions;
    string filtrationOwnerId = filtrationOwnerID;
    int loadLevels1 = loadLevels;
    IReadOnlyCollection<int> expandObjectTypes1 = expandObjectTypes;
    return customService.LoadComposition((object) sessionGuid, objectId, objectType, (IEnumerable<int>) searchRelationTypes1, (IEnumerable<int>) searchObjectTypes1, (IEnumerable<ColumnDescriptor>) columns1, false, num != 0, rule1, (IEnumerable<ConditionStructure>) conditions1, filtrationOwnerId, (HybridDictionary) null, loadLevels1, (IEnumerable<int>) expandObjectTypes1);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetApplicability(
    [NotNull] this IDBObject iDbObject,
    [NotEmpty] long objectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    return iDbObject.GetApplicability(objectVersionID, -1, columns, conditions, searchRelationTypes, searchObjectTypes, expandObjectTypes, grouping, rule, filtrationOwnerID, loadLevels);
  }
}
