// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ColumnAttributeData
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Информация</summary>
public class ColumnAttributeData
{
  /// <summary>Идентификатор атрибута</summary>
  public int AttributeID;
  /// <summary>Тип атрибута</summary>
  public FieldTypes AttributeType;
  /// <summary>Принадлежность атрибута</summary>
  public AttributeSourceTypes AttributeSource;
  /// <summary>Задает порядок сортировки данных по этой колонке</summary>
  public SortOrders Sort;
  /// <summary>
  /// Задает приоритет сортировки для данной колонки (ее порядок в операторе ORDER BY)
  /// </summary>
  public int OrderByID;

  public ColumnAttributeData(
    int attributeID,
    FieldTypes attributeType,
    AttributeSourceTypes attributeSource,
    SortOrders sort,
    int orderBy)
  {
    this.AttributeID = attributeID;
    this.AttributeType = attributeType;
    this.AttributeSource = attributeSource;
    this.Sort = sort;
    this.OrderByID = orderBy;
  }

  public ColumnAttributeData(
    int attributeID,
    FieldTypes attributeType,
    AttributeSourceTypes attributeSource)
    : this(attributeID, attributeType, attributeSource, SortOrders.NONE, 0)
  {
  }
}
