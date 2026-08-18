// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPortalMetadata
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Кэш метананных опубликованных на портале</summary>
public interface IPortalMetadata
{
  /// <summary>Получить дерево типов, дочерних от parentType</summary>
  /// <param name="session">Сессия</param>
  /// <param name="parentType">Родительский тип или -1 если корневые</param>
  /// <param name="recursive">Рекурсивно</param>
  /// <returns></returns>
  PortalObjectType[] GetChildObjectTypes(IUserSession session, int parentType, bool recursive);

  /// <summary>Получить все публикуемые типы объектов</summary>
  /// <param name="session">Сессия</param>
  /// <returns></returns>
  PortalObjectType[] GetPublishObjectTypes(IUserSession session);

  /// <summary>
  /// Получить идентификатор типа публикуемого объекта по его имени
  /// </summary>
  /// <param name="name">Имя</param>
  /// <returns></returns>
  int GetPublishObjectTypeID(string name);

  /// <summary>
  /// Получить имя типа публикуемого объекта по его идентификатору
  /// </summary>
  /// <param name="typeID">Идентификатор</param>
  /// <returns></returns>
  string GetPublishObjectTypeName(int typeID);

  /// <summary>
  /// Получить тип публикуемого объекта по его идентификатору
  /// </summary>
  /// <param name="typeID">Идентификатор</param>
  /// <returns></returns>
  PortalObjectType GetPublishObjectType(int typeID);

  /// <summary>
  /// Получить тип публикуемого объекта по его глобальному идентификатору
  /// </summary>
  /// <param name="typeGuid">Идентификатор</param>
  /// <returns></returns>
  PortalObjectType GetPublishObjectType(Guid typeGuid);

  /// <summary>
  /// Получить атрибуты типа связей "Состав опубликованного объекта" из базы данных портала
  /// </summary>
  /// <returns></returns>
  PortalAttributeType[] GetPublishRelationAttributes();

  /// <summary>Получить атрибут</summary>
  /// <param name="attributeGuid"></param>
  /// <returns></returns>
  PortalAttributeType GetAttribute(Guid attributeGuid);

  /// <summary>Получить атрибут</summary>
  /// <param name="attributeID">Идентифкатор атрибута в базе портала</param>
  /// <returns></returns>
  PortalAttributeType GetAttribute(int attributeID);

  /// <summary>Получить допустимые значения для атрибута</summary>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  Dictionary<object, string> GetPossibleValues(int attributeID);
}
