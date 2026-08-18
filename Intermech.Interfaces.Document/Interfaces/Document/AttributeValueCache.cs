// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AttributeValueCache
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для хранения кэша значений атрибутов и ссылок на них</summary>
public class AttributeValueCache
{
  /// <summary>Значение атрибута</summary>
  private object value;
  private bool hasValue;
  private long? id = new long?(-1L);
  /// <summary>Список узлов документа ссылающихся на этот атрибут</summary>
  public List<INodeWithReference> ReferenceOwnerList;

  public object Value
  {
    get => this.value;
    set
    {
      this.HasValue = true;
      this.value = value;
    }
  }

  public long? Id
  {
    get => this.id;
    set => this.id = value;
  }

  public bool HasValue
  {
    get => this.hasValue;
    set => this.hasValue = value;
  }

  /// <summary>Конструктор</summary>
  public AttributeValueCache() => this.ReferenceOwnerList = new List<INodeWithReference>();

  /// <summary>Конструктор</summary>
  /// <param name="value">Значение атрибута</param>
  /// <param name="refOwner">Владелец ссылки на атрибут</param>
  public AttributeValueCache(object value, INodeWithReference refOwner)
  {
    this.Value = value;
    this.ReferenceOwnerList = new List<INodeWithReference>();
    this.ReferenceOwnerList.Add(refOwner);
  }

  /// <summary>Конструктор</summary>
  /// <param name="value">Значение атрибута</param>
  /// <param name="refOwner">Владелец ссылки на атрибут</param>
  public AttributeValueCache(INodeWithReference refOwner, long? id)
  {
    this.Id = id;
    this.ReferenceOwnerList = new List<INodeWithReference>();
    this.ReferenceOwnerList.Add(refOwner);
  }
}
