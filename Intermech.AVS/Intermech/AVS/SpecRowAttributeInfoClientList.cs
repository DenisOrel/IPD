// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecRowAttributeInfoClientList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces.AVS;
using System;
using System.Collections;

#nullable disable
namespace Intermech.AVS;

/// <summary> Список атрибутов строки спецификации</summary>
public class SpecRowAttributeInfoClientList : SpecRowAttributeInfoList
{
  /// <summary> Конструктор </summary>
  public SpecRowAttributeInfoClientList()
  {
  }

  /// <summary> Конструктор </summary>
  /// <param name="capacity"></param>
  public SpecRowAttributeInfoClientList(int capacity)
    : base(capacity)
  {
  }

  /// <summary> Конструктор </summary>
  /// <param name="attributeDescriptorList"></param>
  public SpecRowAttributeInfoClientList(AttributeDescriptorList attributeDescriptorList)
    : base(attributeDescriptorList != null ? attributeDescriptorList.Count : 0)
  {
    if (attributeDescriptorList == null)
      return;
    foreach (AttributeDescriptor attributeDescriptor in (ArrayList) attributeDescriptorList)
    {
      if (attributeDescriptor != null)
        this.Add((object) new SpecRowAttributeInfoClient(attributeDescriptor));
    }
  }

  public override int Add(object value)
  {
    switch (value)
    {
      case null:
      case AvsRowAttributeInfo _:
        return base.Add(value);
      default:
        throw new Exception("SpecRowAttributeInfoList can contains only SpecRowAttributeInfoClient objects");
    }
  }

  public int Add(SpecRowAttributeInfoClient value, bool checkContains)
  {
    return !checkContains || !this.Contains((object) value) ? this.Add((object) value) : -1;
  }

  /// <summary> Индексатор </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public SpecRowAttributeInfoClient this[int index]
  {
    get => (SpecRowAttributeInfoClient) base[index];
    set => this[index] = (AvsRowAttributeInfo) value;
  }
}
