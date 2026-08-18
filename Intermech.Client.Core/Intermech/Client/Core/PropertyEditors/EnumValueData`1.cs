
// Type: Intermech.Client.Core.PropertyEditors.EnumValueData`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.PropertyEditors;

/// <summary>Класс хелпер для Enum типов</summary>
/// <typeparam name="T"></typeparam>
public class EnumValueData<T>
{
  /// <summary>
  /// 
  /// </summary>
  private T _data;
  /// <summary>Разделитель для элементов</summary>
  private string _valueSeparator = "; ";

  /// <summary>Constructor</summary>
  /// <param name="data"></param>
  public EnumValueData(T data) => this._data = data;

  /// <summary>Constructor</summary>
  /// <param name="data"></param>
  public EnumValueData(object data)
  {
    if (data is T obj)
      this._data = obj;
    else
      this._data = (T) Enum.ToObject(typeof (T), data);
  }

  /// <summary>Get string value</summary>
  /// <returns></returns>
  public override string ToString()
  {
    if (!((object) this._data is Enum))
      return this._data.ToString();
    string str = string.Empty;
    int int32 = Convert.ToInt32((object) this._data);
    Array values = Enum.GetValues(typeof (T));
    int num = 0;
    for (int index = 0; index < values.Length; ++index)
    {
      T obj = (T) values.GetValue(index);
      if (Convert.ToInt32((object) obj) != 0 && (int32 | Convert.ToInt32((object) obj)) == int32)
      {
        if (num != 0)
          str += this._valueSeparator;
        str += $"{EnumDescConverter.GetEnumDescription(typeof (T), Enum.GetName(typeof (T), (object) obj))}";
        ++num;
      }
    }
    if (int32 == 0)
      str = EnumDescConverter.GetEnumDescription(typeof (T), Enum.GetName(typeof (T), (object) 0));
    return str;
  }

  /// <summary>
  /// 
  /// </summary>
  public T Data => this._data;
}
