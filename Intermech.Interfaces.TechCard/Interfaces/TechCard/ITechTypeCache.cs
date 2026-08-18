// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechTypeCache
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>
/// Интерфейс для получения ID типов (объектов, связей) по GUID и
/// GUID по ID
/// </summary>
[Obsolete("Interface is deprecated, please use MetaDataHelper instead", true)]
public interface ITechTypeCache
{
  /// <summary>Получение guid типа объекта по его id</summary>
  /// <param name="objTypeId">id типа объекта</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  Guid GetObjTypeGuidByID(int objTypeId, IUserSession userSession);

  /// <summary>Получение id типа объекта по его guid</summary>
  /// <param name="objTypeGuid">guid типа объекта</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  int GetObjTypeIDByGuid(Guid objTypeGuid, IUserSession userSession);

  /// <summary>Получение id типа объекта по его guid</summary>
  /// <param name="relTypeId">id типа связи</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  Guid GetRelTypeGuidByID(int relTypeId, IUserSession userSession);

  /// <summary>Получение id типа объекта по его guid</summary>
  /// <param name="relTypeGuid">guid типа связи</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  int GetRelTypeIDByGuid(Guid relTypeGuid, IUserSession userSession);

  /// <summary>Получение дочерних типов объектов</summary>
  /// <param name="objTypeId">id типа объекта</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  List<int> GetObjectTypeChildList(int objTypeId, IUserSession userSession);

  /// <summary>
  /// Получение применяемости типа объекта (for tech relation only)
  /// </summary>
  /// <param name="objTypeId">id типа объекта</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  List<int> GetObjectTypeAppList(int objTypeId, IUserSession userSession);

  /// <summary>Получение применяемости типа объекта</summary>
  /// <param name="objTypeId">id типа объекта</param>
  /// <param name="relTypeId">id типа связи</param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  [Obsolete("Method is deprecated, please use MetaDataHelper instead", true)]
  List<int> GetObjectTypeAppList(int objTypeId, int relTypeId, IUserSession userSession);
}
