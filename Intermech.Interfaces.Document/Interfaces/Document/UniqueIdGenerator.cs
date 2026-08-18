// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UniqueIdGenerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Генератор уникальных идентификаторов</summary>
[Serializable]
public class UniqueIdGenerator : IUniqueIdService, IDisposable
{
  /// <summary>Формат идентификатора</summary>
  public string IdFormat = "#{0}";
  private Dictionary<string, int> freeIndex = new Dictionary<string, int>();
  /// <summary>Словарь идентификаторов</summary>
  private Hashtable idCollection = new Hashtable();

  /// <summary>Конструктор</summary>
  public UniqueIdGenerator() => this.freeIndex[string.Empty] = 0;

  /// <summary>Конструктор</summary>
  /// <param name="idFormat">Формат идентификатора</param>
  public UniqueIdGenerator(string idFormat)
  {
    this.freeIndex[string.Empty] = 0;
    this.IdFormat = idFormat;
  }

  /// <summary>Возвращает объект по идентификатору</summary>
  public object this[object id]
  {
    [DebuggerStepThrough] get => this.idCollection[id];
  }

  /// <summary>Сгеренировать уникальный идентификатор (сам факт генерации не резервирует ид)</summary>
  /// <returns>Уникальный идентификатор</returns>
  public object GenerateUniqueId()
  {
    string uniqueId = (string) null;
    for (int index = this.freeIndex[string.Empty]; index < int.MaxValue; ++index)
    {
      string id = index.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (!this.ContainsId((object) id))
      {
        uniqueId = id;
        this.freeIndex[string.Empty] = index + 1;
        break;
      }
    }
    return (object) uniqueId;
  }

  /// <summary>Сгеренировать уникальный идентификатор (сам факт генерации не резервирует ид)</summary>
  /// <param name="prototypeID">Заготовка идентификатора</param>
  /// <returns>Уникальный идентификатор</returns>
  public object GenerateUniqueId(object prototypeID)
  {
    if (prototypeID == null)
      return this.GenerateUniqueId();
    if (!this.ContainsId(prototypeID))
      return prototypeID;
    if (!(prototypeID is string s) || s == "")
      return this.GenerateUniqueId();
    int startIndex = s.LastIndexOf('#');
    int result = 1;
    string key;
    if (startIndex == -1)
    {
      if (int.TryParse(s, out result))
      {
        key = "";
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(s);
        stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, " #");
        key = stringBuilder.ToString();
        result = 1;
      }
    }
    else
    {
      key = s.Substring(0, startIndex + 1);
      if (startIndex < s.Length - 1 && !int.TryParse(s.Substring(startIndex), out result))
        result = 1;
    }
    if (this.freeIndex.ContainsKey(key))
      result = this.freeIndex[key] - 1;
    string uniqueId = (string) null;
    for (int index = result + 1; index < int.MaxValue; ++index)
    {
      StringBuilder stringBuilder = new StringBuilder(key);
      stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, index.ToString());
      string id = stringBuilder.ToString();
      if (!this.ContainsId((object) id))
      {
        uniqueId = id;
        this.freeIndex[key] = index + 1;
        break;
      }
    }
    return (object) uniqueId;
  }

  /// <summary>Используется ли заданный идентификатор</summary>
  /// <param name="id">Идентификатор</param>
  /// <returns>Идентификатор уже используется</returns>
  public bool ContainsId(object id) => this.idCollection.ContainsKey(id);

  /// <summary>Добавить (зарезервировать) объект с идентификатором</summary>
  /// <param name="id">Идентификатор</param>
  /// <param name="value">Объект, которому принадлежит идентификатор</param>
  public void AddId(object id, object value) => this.idCollection.Add(id, value);

  /// <summary>Удалить (освободить) идентификатор</summary>
  /// <param name="id">Идентификатор</param>
  public void RemoveId(object id) => this.idCollection.Remove(id);

  void IDisposable.Dispose()
  {
    if (this.idCollection == null)
      return;
    this.idCollection.Clear();
    this.idCollection = (Hashtable) null;
  }
}
