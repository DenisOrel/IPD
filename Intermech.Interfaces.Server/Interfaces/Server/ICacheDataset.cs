// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ICacheDataset
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface ICacheDataset
{
  void LoadTables(IDbManager db);

  void ReloadTables(IUserSession uSession, IDbManager db, params string[] tablesList);

  DataTable GetTable(string tableName);

  void ChangeTableValue(
    string filterStr,
    string tableName,
    string fieldName,
    object newValue,
    IUserSession uSession);

  int DeleteRecords(string tableName, string condition, IUserSession uSession);

  void AddRow(string toTableName, DataRow row, IUserSession uSession);

  DataTable GetObjectAttsEmptyRow(int attributeID, long objectID, int inListID);

  DataTable GetRelationAttsEmptyRow(int attributeID, long relationID, int inListID);

  void AddObjectInfo(QuickObjectInfo info);

  void UpdateObjectInfo(QuickObjectInfo info);

  void DeleteObjectInfo(long objectID, Guid versionGuid);

  QuickObjectInfo GetObjectInfo(IDbManager db, long objectID);

  QuickObjectInfo GetObjectInfo(IDbManager db, Guid objectGUID);

  DateTime ModifyDate { get; }

  int GetObjectTypeParentID(int objectTypeID);

  bool IsInhertitedFrom(int childTypeID, int parTypeID);

  bool IsProduct(int objType);

  bool IsArticle(int objType);

  bool IsDocument(int objType);

  bool IsSpecification(int objType);

  Guid GetObjectTypeGuid(int objectTypeID, bool throwIfNotFound);

  OptimizationModes GetOptimizationMode(Attribute4ID attrStruct);

  OptimizationModes GetOptimizationMode(int attributeID);

  OptimizationModes GetOptimizationMode(int attributeID, int objectTypeID, int relationTypeID);

  string[] GetUpdateTables(int attributeID, int objectTypeID, int relationTypeID);

  AttributeOptions GetAttributeOptions(int attributeID, int objectTypeID, int relationTypeID);

  bool ReloadOldTables(IDbManager db);

  void LoadFilePrototypes(IUserSession session, int objectTypeID);

  long[] GetFilePrototype(int attributeID, int objectTypeID, long userID);

  void DeleteFilePrototype(long prototypeID);

  int[] GetDecodingAttributes(int objectTypeID);

  List<string> GetObjectAttrsTables();

  string GetAttributesTableName(int objectTypeID);

  int[] GetFormulasID(int attributeID, int typeID, int mode, bool isObject);

  string GetAttributeGroupName(int attrID);

  string GetAccessCaption(int accessLevel);

  bool AccessLevelExists(int accessLevel);

  bool IsSyncParentObjectType(int objTypeID);

  bool IsSyncCheckInParentObjectType(int objTypeID);

  void InitPossibleValuesCache(IUserSession session);

  string GetDescription(int attrID, object val);

  void ReloadPossibleValuesCache(IUserSession session);

  int ProductTypeID { get; }

  int ArticleTypeID { get; }

  int DocumentTypeID { get; }

  void ClearUsersCache();

  Tuple<long, Guid, string>[] GetUsersCache();

  void AddUserToCache(IDBObject userObject);

  void EnterReadLocker();

  void ExitReadLocker();
}
