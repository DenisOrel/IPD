// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImCompositeElement
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Абстрактный интерфейс, содержащий список ключей и значения
/// </summary>
public interface IImCompositeElement : IImBaseElement, IAssignable, ICloneable, IDisplayable
{
  /// <summary>Коллекция атрибутов и их значений</summary>
  IDictionary<string, object> Attributes { get; }

  /// <summary>
  /// Прочитать/установить значение свойства с указанным именем
  /// </summary>
  /// <param name="attrName">Имя атрибута</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  object this[string attrName] { get; set; }

  /// <summary>
  /// Получить строковое представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  string GetAsString(string attr, string defValue);

  /// <summary>Установить значение атрибута как строку</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде строки</param>
  void SetAsString(string attr, string value);

  /// <summary>
  /// Получить объектное представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  object GetAsObject(string attr, object defValue);

  /// <summary>Установить значение атрибута как объект</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде объекта</param>
  void SetAsObject(string attr, object value);

  /// <summary>
  /// Получить представление DateTime атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение DateTime атрибута или значение по умолчанию</returns>
  DateTime GetAsDateTime(string attr, DateTime defValue);

  /// <summary>Установить значение атрибута как DateTime</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде DateTime</param>
  void SetAsDateTime(string attr, DateTime value);

  /// <summary>
  /// Получить представление Int32 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int32 атрибута или значение по умолчанию</returns>
  int GetAsInt32(string attr, int defValue);

  /// <summary>Установить значение атрибута как Int32</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int32</param>
  void SetAsInt32(string attr, int value);

  /// <summary>
  /// Получить представление Int64 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int64 атрибута или значение по умолчанию</returns>
  long GetAsInt64(string attr, long defValue);

  /// <summary>Установить значение атрибута как Int64</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int64</param>
  void SetAsInt64(string attr, long value);

  /// <summary>
  /// Получить представление Double атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Double атрибута или значение по умолчанию</returns>
  double GetAsDouble(string attr, double defValue);

  /// <summary>Установить значение атрибута как Double</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Double</param>
  void SetAsDouble(string attr, double value);

  /// <summary>
  /// Получить представление Guid атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Guid атрибута или значение по умолчанию</returns>
  Guid GetAsGuid(string attr, Guid defValue);

  /// <summary>Установить значение атрибута как Guid</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Guid</param>
  void SetAsGuid(string attr, Guid value);

  /// <summary>
  /// Получить представление Boolean атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Boolean атрибута или значение по умолчанию</returns>
  bool GetAsBoolean(string attr, bool defValue);

  /// <summary>Установить значение атрибута как Boolean</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Boolean</param>
  void SetAsBoolean(string attr, bool value);
}
