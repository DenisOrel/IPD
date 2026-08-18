// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionCache.IAutoSelectionRuleCacheService
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AutoSelection.AutoSelectionCache;

/// <summary>Интерфейс службы кэша правил подбора</summary>
public interface IAutoSelectionRuleCacheService
{
  /// <summary>
  /// Получение списка типов объектов,
  /// в том числе тех, у которых нету правил автоподбора,
  /// </summary>
  /// <param name="sessionGuid">Гл. ид. пользовательской сессии</param>
  /// <returns></returns>
  List<int> GetObjectTypes(Guid sessionGuid);

  /// <summary>
  /// Сохранение списка ид. типов объектов,
  /// в том числе тех, у которых нету правил автоподбора
  /// </summary>
  /// <param name="objectTypeIDs"></param>
  /// <param name="sessionGuid">Гл. ид. пользовательской сессии</param>
  void SetObjectTypes(List<int> objectTypeIDs, Guid sessionGuid);

  /// <summary>
  /// Get all object type's ids that have autoselection rules
  /// </summary>
  /// <returns></returns>
  List<int> GetAllRulesObjTypes();

  /// <summary>Get selection rules by object type id</summary>
  /// <param name="objectTypeId"></param>
  /// <returns></returns>
  List<long> GetAllRulesByObjectType(int objectTypeId);

  /// <summary>Get linked selection rules for object type id</summary>
  /// <param name="objectTypeId"></param>
  /// <returns></returns>
  List<long> GetRulesByObjectType(int objectTypeId);

  /// <summary>
  /// Получение правил подбора для объекта согласно настройкам для его типа
  /// </summary>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="sessionGuid">Сессия</param>
  /// <returns>Список ид. версий объектов правил подбора</returns>
  List<long> GetRulesByObject(long objectId, Guid sessionGuid);

  /// <summary>
  /// Получение правил подбора по ид. версии объекта Imbase с проверкой по родительским узлам
  /// </summary>
  /// <param name="imbaseObjectId">Ид. версии объекта Imbase</param>
  /// <param name="imbaseCatalogId"></param>
  /// <param name="sessionGuid">Сессия</param>
  /// <returns>Список ид. версий объектов правил подбора</returns>
  List<long> GetRulesByImbaseObj(long imbaseObjectId, long imbaseCatalogId, Guid sessionGuid);

  /// <summary>
  /// Получение правил подбора только по ид. версии объекта Imbase, без проверки вверх по дереву
  /// </summary>
  /// <param name="imbaseObjectId">Ид. версии объекта Imbase</param>
  /// <param name="sessionGuid">Сессия</param>
  /// <returns>Список ид. версий объектов правил подбора</returns>
  List<long> GetRulesByImbaseObjOnly(long imbaseObjectId, Guid sessionGuid);

  /// <summary>Register selection rule for specific object</summary>
  /// <param name="ruleIdList">Rule's list</param>
  /// <param name="objectId">Imbase object's id</param>
  /// <param name="linkMode"></param>
  /// <param name="sessonGuid"></param>
  void RulesRegister(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessonGuid);

  /// <summary>Unregister selection rule for specific object</summary>
  /// <param name="ruleIdList"></param>
  /// <param name="objectId">Imbase object's id</param>
  /// <param name="linkMode"></param>
  /// <param name="sessonGuid"></param>
  void RulesUnregister(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessonGuid);

  /// <summary>Update selection rules list for specific object</summary>
  /// <param name="ruleIdList">Rule's list</param>
  /// <param name="objectId">Imbase object's id</param>
  /// <param name="linkMode"></param>
  /// <param name="sessonGuid"></param>
  void RulesUpdate(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessonGuid);

  /// <summary>Удаление всех данных их кэша</summary>
  void ClearCache();
}
