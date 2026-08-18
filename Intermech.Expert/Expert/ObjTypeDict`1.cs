// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ObjTypeDict`1
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Словарь чего-нибудь по типам объектов. Обеспечивает поиск не только по самому ИД объекта,
/// но и по ИД всех родительских объектов, а если не нашли ничего - то для всех объектов (-1)
/// </summary>
/// <typeparam name="T">Параметризуемый тип хранимых значений</typeparam>
public class ObjTypeDict<T> where T : class
{
  /// <summary>Внутренний словарь</summary>
  private Dictionary<int, T> _innerDict;

  public ObjTypeDict(int capacity = 0) => this._innerDict = new Dictionary<int, T>(capacity);

  public int Count => this._innerDict.Count;

  public T this[int key]
  {
    get => this._GetValue(key);
    set
    {
      if (this._innerDict.ContainsKey(key))
        this._innerDict[key] = value;
      else
        this._innerDict.Add(key, value);
    }
  }

  /// <summary>Получение значения сначала для самого</summary>
  /// <param name="objTypeId">ИД типа объекта</param>
  /// <returns>Значение Value для этого типа или для одного из родительских типов, или null, если нет</returns>
  private T _GetValue(int objTypeId)
  {
    if (this._innerDict.ContainsKey(objTypeId))
      return this._innerDict[objTypeId];
    foreach (int key in MetaDataHelper.GetObjectTypeParentsID(objTypeId))
    {
      if (this._innerDict.ContainsKey(key))
        return this._innerDict[key];
    }
    return this._innerDict.ContainsKey(-1) ? this._innerDict[-1] : default (T);
  }
}
