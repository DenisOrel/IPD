// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecRowAttributeInfoList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections;

#nullable disable
namespace Intermech.AVS;

/// <summary> Список атрибутов строки спецификации</summary>
public class SpecRowAttributeInfoList : ArrayList
{
  /// <summary> Конструктор </summary>
  public SpecRowAttributeInfoList()
  {
  }

  /// <summary> Конструктор </summary>
  /// <param name="capacity"></param>
  public SpecRowAttributeInfoList(int capacity)
    : base(capacity)
  {
  }

  public override int Add(object value)
  {
    switch (value)
    {
      case null:
      case AvsRowAttributeInfo _:
        return base.Add(value);
      default:
        throw new Exception("SpecRowAttributeInfoList can contains only SpecRowAttributeInfo objects");
    }
  }

  public int Add(AvsRowAttributeInfo value, bool checkContains)
  {
    return !checkContains || !this.Contains((object) value) ? this.Add((object) value) : -1;
  }

  /// <summary> Индексатор </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public AvsRowAttributeInfo this[int index]
  {
    get => (AvsRowAttributeInfo) base[index];
    set => this[index] = (object) value;
  }
}
