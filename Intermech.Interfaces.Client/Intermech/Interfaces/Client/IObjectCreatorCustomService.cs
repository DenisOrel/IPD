// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectCreatorCustomService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для реализации заменителей диалога создания объектов.
/// Используется для создания собственного диалога создания объекта заданного типа
/// </summary>
public interface IObjectCreatorCustomService
{
  /// <summary>
  /// Вызов диалога создания нового объекта (по прототипу) c созданием заданных связей с указанными объектами
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <param name="RelationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="RelatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="StartDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  /// <returns>Идентификатор созданного объекта</returns>
  long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion);
}
