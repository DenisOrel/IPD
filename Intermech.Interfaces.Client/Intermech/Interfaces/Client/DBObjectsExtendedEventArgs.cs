// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectsExtendedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Аргументы события изменения атрибутов объекта в базе</summary>
[Serializable]
public class DBObjectsExtendedEventArgs : DBObjectsEventArgs
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
  private int objectType = -1;

  /// <summary>Старые значения атрибутов</summary>
  public AttributeValues[] OrigAttributeValuesArray => this.origAttributeValuesArray;

  /// <summary>Новые значения атрибутов</summary>
  public AttributeValues[] AttributeValuesArray => this.attributeValuesArray;

  /// <summary>Тип объекта</summary>
  public int ObjectType => this.objectType;

  /// <summary>Конструктор</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="objectID">Ид версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="origList">Список оригинальных значений</param>
  /// <param name="avArray">Список изменённых значений</param>
  public DBObjectsExtendedEventArgs(
    string eventName,
    long objectID,
    int objectType,
    AttributeValues[] origList,
    AttributeValues[] avArray)
    : base(eventName, objectID, objectType)
  {
    this.objectType = objectType;
    this.origAttributeValuesArray = origList;
    this.attributeValuesArray = avArray;
  }

  /// <summary>Конструктор агрументов события ObjectsChanged</summary>
  /// <param name="objectID">ID версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="oldAttrValues">Старые значения атрибута</param>
  /// <param name="newAttrValues">Новые значения атрибута</param>
  public DBObjectsExtendedEventArgs(
    long objectID,
    int objectType,
    AttributeValues oldAttrValues,
    AttributeValues newAttrValues)
    : base("ObjectsChanged", objectID, objectType)
  {
    this.objectType = objectType;
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
