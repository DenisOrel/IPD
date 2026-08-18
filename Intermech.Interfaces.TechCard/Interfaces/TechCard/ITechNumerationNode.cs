// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechNumerationNode
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Интерфейс элементов правил нумерации</summary>
public interface ITechNumerationNode
{
  /// <summary>Загрузка параметров из объекта</summary>
  /// <param name="obj">Объект-источник</param>
  /// <param name="session">Сессия</param>
  void Load(IDBObject obj, IUserSession session);

  /// <summary>Загрузка параметров из атрибута</summary>
  /// <param name="attrValues">Атрибут-источник</param>
  /// <param name="session">Сессия</param>
  void Load(AttributeValues attrValues, IUserSession session);

  /// <summary>Сохранение параметров</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  void Save(IDBObject obj, IUserSession session);

  /// <summary>Идентификатор объекта</summary>
  long ObjectID { get; }

  /// <summary>Идентификатор правила нумерации</summary>
  long NumRuleID { get; set; }

  /// <summary>Тип нумеруемого объекта</summary>
  Guid ObjectTypeGuid { get; set; }

  /// <summary>Нумеруемый атрибут</summary>
  Guid AttributeTypeGuid { get; set; }

  /// <summary>
  /// Список родительских элементов, куда входит нумеруемый объект
  /// </summary>
  List<Guid> ParentObjectTypeGuids { get; }

  /// <summary>
  /// Список типов связей, по которым требуется получать состав / применяемость
  /// </summary>
  List<Guid> RelationTypeGuids { get; }

  /// <summary>Режим нумерации</summary>
  TechNumerationMode NumerationMode { get; set; }

  /// <summary>Скрипт C# нумерации объектов</summary>
  string ScriptData { get; set; }
}
