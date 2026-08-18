// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.IImbaseAPIRem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Data;

#nullable disable
namespace Intermech.Imbase.API;

internal interface IImbaseAPIRem
{
  int Version { get; }

  int SelectFromTable(
    string catalogDef,
    string objectDef,
    string filter,
    string showFields,
    string sortOrder,
    int recordsCount,
    string comment,
    ref DataTable records,
    ref FieldInfo[] fields,
    ref ContextInfo context);

  int CreateObject(long recordId, long linkId, ref string objectGuid);

  int CreateObjectFromTempKey(string tempKey, ref string objectGuid);

  int ShowPropertyWindow(string guids);

  int MaterialEntry(string command, ref string fileData);

  int GetKeyInfo(
    string key,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList);

  int ShowTables(
    int showFlags,
    string fieldNames,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList);

  int SelectTable(
    long catalogId,
    string prompt,
    ref long tableId,
    ref string fullList,
    ref long recordKey);

  int SelectFolder(long catalogId, string prompt, ref long folderId, ref string fullList);
}
