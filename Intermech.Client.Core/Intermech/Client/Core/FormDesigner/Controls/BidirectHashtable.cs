
// Type: Intermech.Client.Core.FormDesigner.Controls.BidirectHashtable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Двунаправленный Hashtable.</summary>
public class BidirectHashtable
{
  /// <summary>forward - прямой hash</summary>
  public Hashtable forward = new Hashtable();
  /// <summary>backward - обратный hash</summary>
  public Hashtable backward = new Hashtable();

  /// <summary>Добавление элемента.</summary>
  /// <param name="key">Ключ</param>
  /// <param name="value">Значение</param>
  public void Add(object key, object value)
  {
    this.forward.Add(key, value);
    this.backward.Add(value, key);
  }

  /// <summary>Удаление элемента.</summary>
  /// <param name="key">Ключ</param>
  public void Remove(object key)
  {
    object key1 = this[key];
    if (this.forward.ContainsKey(key))
    {
      this.forward.Remove(key);
      this.backward.Remove(key1);
    }
    else
    {
      this.forward.Remove(key1);
      this.backward.Remove(key);
    }
  }

  /// <summary>Очистить Hashtable.</summary>
  public void Clear()
  {
    this.forward.Clear();
    this.backward.Clear();
  }

  /// <summary>Возвращает значение по ключу</summary>
  public object this[object key]
  {
    get
    {
      object obj = (object) null;
      if (this.forward.ContainsKey(key))
        obj = this.forward[key];
      if (this.backward.ContainsKey(key))
        obj = this.backward[key];
      return obj;
    }
  }
}
