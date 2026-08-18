// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.PacketValue
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace Intermech.Expert;

/// <summary>A class used to represent Packets</summary>
[Serializable]
public class PacketValue : ISerializable, ICloneable
{
  private List<ExpertValue> _data = new List<ExpertValue>();

  /// <summary>Создать пустой пакет</summary>
  public PacketValue()
  {
  }

  /// <summary>
  /// Создать пакет из набора значений одного типа (например, значений многозначного атрибута)
  /// </summary>
  /// <param name="Values"></param>
  /// <param name="dt"></param>
  public PacketValue(IEnumerable Values, DataType dt)
  {
    foreach (object obj in Values)
      this.Add(new ExpertValue(dt, obj));
  }

  /// <summary>Значение по индексу</summary>
  /// <param name="index">индекс значения</param>
  /// <returns></returns>
  public ExpertValue this[int index]
  {
    get => this._data[index];
    set => this._data[index] = value;
  }

  /// <summary>Добавить значение</summary>
  /// <param name="expValue">значение</param>
  /// <returns>индекс добавленного значения</returns>
  public void Add(ExpertValue expValue) => this._data.Add(expValue);

  /// <summary>Получить количество элементов</summary>
  public int Count => this._data.Count;

  /// <summary>Удалить значение</summary>
  /// <param name="expValue">значение</param>
  public void Remove(ExpertValue expValue) => this._data.Remove(expValue);

  /// <summary>Очистить список</summary>
  public void Clear() => this._data.Clear();

  /// <summary>В строку</summary>
  /// <returns></returns>
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder("{ ");
    for (int index = 0; index < this._data.Count; ++index)
    {
      ExpertValue expertValue = this[index];
      if (expertValue != null)
      {
        stringBuilder.Append(expertValue.ToString());
        if (index < this._data.Count - 1)
          stringBuilder.Append(",");
      }
    }
    return stringBuilder.Append(" }").ToString();
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected PacketValue(SerializationInfo info, StreamingContext context)
  {
    this._data = info.GetValue("Values", typeof (List<ExpertValue>)) as List<ExpertValue>;
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Values", (object) this._data);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    PacketValue packetValue = new PacketValue();
    foreach (ExpertValue expertValue in this._data)
      packetValue._data.Add(expertValue.Clone() as ExpertValue);
    return (object) packetValue;
  }
}
