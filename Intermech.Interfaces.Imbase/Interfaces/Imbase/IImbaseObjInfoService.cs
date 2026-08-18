// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseObjInfoService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Интерфейс для получения информации Imbase по объектам справочников / каталогов
/// </summary>
public interface IImbaseObjInfoService
{
  /// <summary>List of all object type that can be created by imbase</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="objTypeIds"></param>
  /// <returns></returns>
  bool GetCreationTypes(Guid sessionGuid, out List<int> objTypeIds);

  /// <summary>Get creation modes by object type</summary>
  /// <param name="objTypeId">Object's type id</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateModes">Creation mode's list</param>
  /// <returns>Result</returns>
  bool GetCreationMode(
    int objTypeId,
    Guid sessionGuid,
    out List<ImbaseObjCreateMode> objCreateModes);

  /// <summary>Get creation modes by object type</summary>
  /// <param name="objTypeId">Object's type id</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateModes">Creation mode's list</param>
  /// <param name="checkApplicability"></param>
  /// <returns>Result</returns>
  bool GetCreationMode(
    int objTypeId,
    Guid sessionGuid,
    out List<ImbaseObjCreateMode> objCreateModes,
    bool checkApplicability);

  /// <summary>Get creation mode by object's version ID</summary>
  /// <remarks>Use second function with defined type - if possible</remarks>
  /// <param name="objectId">Object version Id</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateInfo">Obj creation info</param>
  /// <returns>Result</returns>
  bool GetCreationMode(long objectId, Guid sessionGuid, out ImbaseObjCreateInfo objCreateInfo);

  /// <summary>Get creation mode by object's version ID and type ID</summary>
  /// <remarks></remarks>
  /// <param name="objectId">Object version Id</param>
  /// <param name="objTypeId">Creation object type's Id (Recommended to define for speed up!!)</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateInfo">Obj creation info</param>
  /// <returns>Result</returns>
  bool GetCreationMode(
    long objectId,
    int objTypeId,
    Guid sessionGuid,
    out ImbaseObjCreateInfo objCreateInfo);

  /// <summary>Get creations mode by object version IDS and type IDS</summary>
  /// <remarks>Use second function with defined type - if possible</remarks>
  /// <param name="objects">Object's params</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateInfo">Obj creation info</param>
  /// <returns>Result</returns>
  bool GetCreationMode(
    IList<long> objects,
    Guid sessionGuid,
    out Dictionary<long, ImbaseObjCreateInfo> objCreateInfo);

  /// <summary>Get creations mode by object version IDS and type IDS</summary>
  /// <remarks></remarks>
  /// <param name="objects">Object's params</param>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="objCreateInfo">Obj creation info</param>
  /// <returns>Result</returns>
  bool GetCreationMode(
    IDictionary<long, int> objects,
    Guid sessionGuid,
    out Dictionary<long, ImbaseObjCreateInfo> objCreateInfo);
}
