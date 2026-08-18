// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DbOperationsHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

public static class DbOperationsHelper
{
  /// <summary>
  /// Возвращает значения атрибутов по умолчанию для указанной сущности. Этот метод используется для получения атрибутов объекта/связи, которые еще не
  /// созданы в базе PDM-системы, но будут там созданы.
  /// </summary>
  /// <param name="attributableType">Описатель для атрибутов сущности</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public static ValueBag ReadBlankAttributes(IDBAttributableTypeRef attributableType)
  {
    return attributableType != null ? new ValueBag((ICollection<ValueRecord>) DBAttributeHelper.ReadBlankValues(attributableType, RequiredModes.AutoRequired, RequiredModes.Auto)) : throw new ArgumentNullException(nameof (attributableType));
  }

  /// <summary>
  /// Читает значения указанного объекта из базы данных PDM-системы.
  /// </summary>
  /// <param name="objRef">Ссылка на идентификатор объекта</param>
  /// <param name="attributableType">Описатель для атрибутов объекта</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public static ValueBag ReadObjectAttributes(
    IDBObjectRef objRef,
    IDBAttributableTypeRef attributableType)
  {
    if (objRef == null)
      throw new ArgumentNullException(nameof (objRef));
    if (attributableType == null)
      throw new ArgumentNullException(nameof (attributableType));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeValues[] attributesValues = sessionKeeper.Session.GetObject(objRef.GetObjectId(), true).GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes);
      return new ValueBag((ICollection<ValueRecord>) DBAttributeHelper.ReadEntityValues(attributableType, (ICollection<AttributeValues>) attributesValues));
    }
  }

  /// <summary>
  /// Читает значения указанной связи из базы данных PDM-системы.
  /// </summary>
  /// <param name="objRef">Ссылка на идентификатор связи</param>
  /// <param name="attributableType">Описатель для атрибутов связи</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public static ValueBag ReadRelationAttributes(
    IDBRelationRef relationRef,
    IDBAttributableTypeRef attributableType)
  {
    if (relationRef == null)
      throw new ArgumentNullException(nameof (relationRef));
    if (attributableType == null)
      throw new ArgumentNullException(nameof (attributableType));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeValues[] attributesValues = sessionKeeper.Session.GetRelation(relationRef.GetRelationGuid(), relationRef.GetProjectId(), true).GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes);
      return new ValueBag((ICollection<ValueRecord>) DBAttributeHelper.ReadEntityValues(attributableType, (ICollection<AttributeValues>) attributesValues));
    }
  }
}
