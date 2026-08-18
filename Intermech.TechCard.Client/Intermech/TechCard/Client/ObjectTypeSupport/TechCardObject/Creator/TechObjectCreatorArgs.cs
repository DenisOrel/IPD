// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorArgs
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>Параметры / аргументы вызова создания объекта</summary>
public class TechObjectCreatorArgs
{
  /// <summary>Конструктор</summary>
  /// <param name="objectTypeId">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateObjectId">Идентификатор объекта-прототипа</param>
  /// <param name="relationTypeIDs"> массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  public TechObjectCreatorArgs(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
    : this(new int[1]{ objectTypeId }, new long[1]
    {
      templateObjectId
    }, relationTypeIDs, relatedObjectIDs, startDate, isVersion)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeIDs">Идентификаторы типов создаваемых объектов</param>
  /// <param name="templateObjectIDs">Идентификаторы объектов-прототипов</param>
  /// <param name="relationTypeIDs"> массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  public TechObjectCreatorArgs(
    int[] objectTypeIDs,
    long[] templateObjectIDs,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    this.ObjectTypeIDs = objectTypeIDs;
    this.TemplateObjectIDs = templateObjectIDs;
    this.RelationTypeIDs = relationTypeIDs;
    this.RelatedObjectIDs = relatedObjectIDs;
    this.StartDate = startDate;
    this.IsVersion = isVersion;
  }

  /// <summary>Идентификаторы типов создаваемого объекта</summary>
  public int[] ObjectTypeIDs { get; }

  /// <summary>Идентификатор объекта-прототипа</summary>
  public long[] TemplateObjectIDs { get; }

  /// <summary>
  /// массив идентификаторов связей которые необходимо создавать
  /// </summary>
  public int[] RelationTypeIDs { get; protected internal set; }

  /// <summary>
  /// массив идентификаторов объектов с которыми надо связать созданный объект
  /// </summary>
  public long[] RelatedObjectIDs { get; protected internal set; }

  /// <summary>
  /// время с которого начинают действовать связи (если они были созданы)
  /// </summary>
  public DateTime StartDate { get; }

  /// <summary>признак, нужно ли создавать версию объекта</summary>
  public bool IsVersion { get; }
}
