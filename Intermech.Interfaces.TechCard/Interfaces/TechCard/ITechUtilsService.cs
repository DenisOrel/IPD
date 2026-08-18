// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechUtilsService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>
/// Интерфейс сервиса утилит для TechCard на серверной стороне
/// </summary>
/// <remarks>Часть методов вынесена сюда для ускорения работы</remarks>
public interface ITechUtilsService
{
  /// <summary>Создание объектов с заполнением атрибутов</summary>
  /// <param name="objectTypeId">Тип создаваемого объекта</param>
  /// <param name="prototypeObjId">Ид. версии объекта атрибуты которого копируем</param>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <returns></returns>
  IDBObject CreateObject(int objectTypeId, long prototypeObjId, Guid sessionGuid);

  /// <summary>Создание связей с заполнением атрибутов</summary>
  /// <param name="relTypeId">Тип создаваемой связи</param>
  /// <param name="projObjId">Ид. версии родительского объекта</param>
  /// <param name="partObjId">Ид. версии дочернего объекта</param>
  /// <param name="prototypeRelationId">Ид. версии связи атрибуты которой копируем</param>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <returns></returns>
  IDBRelation CreateRelation(
    int relationTypeId,
    long projObjId,
    long partObjId,
    long prototypeRelationId,
    Guid sessionGuid);

  /// <summary>Копирование атрибутов объектов</summary>
  /// <param name="sourceRelationId">Ид. версии связи - источника</param>
  /// <param name="targetRelationId">Ид. версии связи - приемника</param>
  /// <param name="attrTypeIds">Список ид. копируемых атрибутов</param>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <returns></returns>
  bool CopyRelationAttributes(
    long sourceRelationId,
    long targetRelationId,
    int[] attrTypeIds,
    Guid sessionGuid);

  /// <summary>Копирование атрибутов объектов</summary>
  /// <param name="sourceObjectId">Ид. версии объекта - источника</param>
  /// <param name="targetObjectId">Ид. версии объекта - приемника</param>
  /// <param name="attrTypeIds">Список ид. копируемых атрибутов</param>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <returns></returns>
  bool CopyObjectAttributes(
    long sourceObjectId,
    long targetObjectId,
    int[] attrTypeIds,
    Guid sessionGuid);

  /// <summary>Копирование состава объекта</summary>
  /// <param name="sourceId">Ид. версии объекта - источника</param>
  /// <param name="targetId">Ид. версии объекта - приемника</param>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <param name="copyRelationList">Список идентификаторов связей требующих копирования, null - значение параметра игнорируется, копируются все связи</param>
  /// <param name="excludeRelationList">Список идентификаторов связей исключения из копирования, null - значение параметра игнорируется, копируются все связи</param>
  /// <returns></returns>
  bool CreateObjectComposition(
    long sourceId,
    long targetId,
    Guid sessionGuid,
    List<long> copyRelationList = null,
    List<long> excludeRelationList = null);
}
