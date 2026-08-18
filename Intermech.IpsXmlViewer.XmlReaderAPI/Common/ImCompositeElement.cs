// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImCompositeElement
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.Collections;
using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>
/// Абстрактный базовый класс, содержащий список ключей и значения (используется для атрибутов)
/// </summary>
public abstract class ImCompositeElement : 
  ImBaseElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  /// <summary>Размерность по умолчанию</summary>
  protected const int DefaultCapacity = 0;
  /// <summary>Коллекция атрибутов и их значений</summary>
  protected IDictionary<string, object> _attributes = (IDictionary<string, object>) new Dictionary<string, object>();

  /// <summary>Коллекция атрибутов и их значений</summary>
  public IDictionary<string, object> Attributes => this._attributes;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="capacity"></param>
  protected ImCompositeElement(int capacity = 0)
  {
    this._attributes = (IDictionary<string, object>) new Dictionary<string, object>(capacity);
  }

  /// <summary>
  /// Прочитать/установить значение свойства с указанным именем
  /// </summary>
  /// <param name="attrName">Имя атрибута</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  public virtual object this[string attrName]
  {
    get => this.GetAsObject(attrName, (object) null);
    set => this.SetAsObject(attrName, value);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear() => this._attributes.Clear();

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is ImCompositeElement compositeElement))
      return;
    this._attributes = CloneHelper.Clone((object) compositeElement._attributes) as IDictionary<string, object>;
  }

  /// <summary>
  /// Получить строковое представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  public virtual string GetAsString(string attr, string defValue)
  {
    object obj;
    return !this._attributes.TryGetValue(attr, out obj) ? defValue : Convert.ToString(obj);
  }

  /// <summary>Установить значение атрибута как строку</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде строки</param>
  public virtual void SetAsString(string attr, string value)
  {
    this._attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить объектное представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  public virtual object GetAsObject(string attr, object defValue)
  {
    object obj;
    return !this._attributes.TryGetValue(attr, out obj) ? defValue : obj;
  }

  /// <summary>Установить значение атрибута как объект</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде объекта</param>
  public virtual void SetAsObject(string attr, object value)
  {
    if (value == DBNull.Value)
      this._attributes.Remove(attr);
    else
      this._attributes[attr] = value;
  }

  /// <summary>
  /// Получить представление DateTime атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение DateTime атрибута или значение по умолчанию</returns>
  public virtual DateTime GetAsDateTime(string attr, DateTime defValue)
  {
    DateTime result = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj) && (obj == null || obj == DBNull.Value || !DateTime.TryParse(obj.ToString(), out result)))
      result = defValue;
    return result;
  }

  /// <summary>Установить значение атрибута как DateTime</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде DateTime</param>
  public virtual void SetAsDateTime(string attr, DateTime value)
  {
    this._attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить представление Int32 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int32 атрибута или значение по умолчанию</returns>
  public virtual int GetAsInt32(string attr, int defValue)
  {
    int result = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj) && (obj == null || obj == DBNull.Value || !int.TryParse(obj.ToString(), out result)))
      result = defValue;
    return result;
  }

  /// <summary>Установить значение атрибута как Int32</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int32</param>
  public virtual void SetAsInt32(string attr, int value) => this._attributes[attr] = (object) value;

  /// <summary>
  /// Получить представление Int64 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int64 атрибута или значение по умолчанию</returns>
  public virtual long GetAsInt64(string attr, long defValue)
  {
    long result = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj) && (obj == null || obj == DBNull.Value || !long.TryParse(obj.ToString(), out result)))
      result = defValue;
    return result;
  }

  /// <summary>Установить значение атрибута как Int64</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int64</param>
  public virtual void SetAsInt64(string attr, long value)
  {
    this._attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить представление Double атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Double атрибута или значение по умолчанию</returns>
  public virtual double GetAsDouble(string attr, double defValue)
  {
    double result = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj) && (obj == null || obj == DBNull.Value || !double.TryParse(obj.ToString(), out result)))
      result = defValue;
    return result;
  }

  /// <summary>Установить значение атрибута как Double</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Double</param>
  public virtual void SetAsDouble(string attr, double value)
  {
    this._attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить представление Guid атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Guid атрибута или значение по умолчанию</returns>
  public virtual Guid GetAsGuid(string attr, Guid defValue)
  {
    Guid asGuid = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj))
      asGuid = obj == null || obj == DBNull.Value || !GuidHelper.IsGuid(obj.ToString()) ? defValue : new Guid(obj.ToString());
    return asGuid;
  }

  /// <summary>Установить значение атрибута как Guid</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Guid</param>
  public virtual void SetAsGuid(string attr, Guid value) => this._attributes[attr] = (object) value;

  /// <summary>
  /// Получить представление Boolean атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Boolean атрибута или значение по умолчанию</returns>
  public virtual bool GetAsBoolean(string attr, bool defValue)
  {
    bool result = defValue;
    object obj;
    if (this._attributes.TryGetValue(attr, out obj) && (obj == null || obj == DBNull.Value || !bool.TryParse(obj.ToString(), out result)))
      result = defValue;
    return result;
  }

  /// <summary>Установить значение атрибута как Boolean</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Boolean</param>
  public virtual void SetAsBoolean(string attr, bool value)
  {
    this._attributes[attr] = (object) value;
  }
}
