// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechNumerationRule
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Интерфейс правила нумерации</summary>
public interface ITechNumerationRule
{
  /// <summary>Загрузка параметров правила из объекта</summary>
  /// <param name="obj">объект-источник</param>
  /// <param name="session">сессия</param>
  void Load(IDBObject obj, IUserSession session);

  /// <summary>Загрузка параметров правила из атрибута</summary>
  /// <param name="attrValues">атрибут-источник</param>
  /// <param name="session">сессия</param>
  void Load(AttributeValues attrValues, IUserSession session);

  /// <summary>Сохранение параметров правила нумерации</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  void Save(IDBObject obj, IUserSession session);

  /// <summary>Идентификатор версии объекта</summary>
  long ObjectID { get; }

  /// <summary>Метод нумерации</summary>
  TechNumerationMethods NumerationMethod { get; set; }

  /// <summary>Тип нумерации</summary>
  TechNumerationTypes NumerationType { get; set; }

  /// <summary>Длина номера</summary>
  int NumberLength { get; set; }

  /// <summary>Список символов, для нумерации</summary>
  string CharList { get; set; }

  /// <summary>Первый номер</summary>
  string NumberFirst { get; set; }

  /// <summary>Шаг номеров</summary>
  int NumberStep { get; set; }

  /// <summary>Область нумерации</summary>
  TechNumerationAreas NumerationArea { get; set; }

  /// <summary>Разделитель номера</summary>
  char NumberSeparator { get; set; }

  /// <summary>Типы нумерации вариантов</summary>
  TechNumerationTypes NumerationTypeVariant { get; set; }

  /// <summary>
  /// Использование номера основного объекта, при нумерации вариантов/заменителей
  /// </summary>
  TechNumerationBool UseBaseObjectNumber { get; set; }

  /// <summary>Вызов перенумерации при удалении объекта / связи</summary>
  bool RenumOnDelete { get; set; }
}
