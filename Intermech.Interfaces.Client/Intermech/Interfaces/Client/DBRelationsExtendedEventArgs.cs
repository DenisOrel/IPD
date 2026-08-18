// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBRelationsExtendedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Аргументы события изменения атрибутов связи в базе</summary>
[Serializable]
public class DBRelationsExtendedEventArgs : DBRelationsEventArgs
{
  /// <summary>
  /// 
  /// </summary>
  private AttributeValues[] origAttributeValuesArray;
  /// <summary>
  /// 
  /// </summary>
  private AttributeValues[] attributeValuesArray;
  /// <summary>
  /// 
  /// </summary>
  private int relationType = -1;

  /// <summary>Старые значения атрибутов</summary>
  public AttributeValues[] OrigAttributeValuesArray => this.origAttributeValuesArray;

  /// <summary>Новые значения атрибутов</summary>
  public AttributeValues[] AttributeValuesArray => this.attributeValuesArray;

  /// <summary>Тип связи</summary>
  public int RelationType => this.relationType;

  /// <summary>Конструктор</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="relationID">Ид связи</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="origList">список оригинальных значений</param>
  /// <param name="avArray">список измененных значений</param>
  public DBRelationsExtendedEventArgs(
    string eventName,
    long relationID,
    int relationType,
    AttributeValues[] origList,
    AttributeValues[] avArray)
    : base(eventName, relationID, relationType)
  {
    this.relationType = relationType;
    this.origAttributeValuesArray = origList;
    this.attributeValuesArray = avArray;
  }

  /// <summary>Конструктор агрументов события RelationsChanged</summary>
  /// <param name="relationID">Ид связи</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="oldAttrValues">Старые значения атрибута</param>
  /// <param name="newAttrValues">Новые значения атрибута</param>
  public DBRelationsExtendedEventArgs(
    long relationID,
    int relationType,
    AttributeValues oldAttrValues,
    AttributeValues newAttrValues)
    : base("RelationsChanged", relationID)
  {
    this.relationType = relationType;
    this.origAttributeValuesArray = new AttributeValues[1]
    {
      oldAttrValues
    };
    this.attributeValuesArray = new AttributeValues[1]
    {
      newAttrValues
    };
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj) => false;
}
