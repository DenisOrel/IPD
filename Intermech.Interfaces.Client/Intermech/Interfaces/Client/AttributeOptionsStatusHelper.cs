// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AttributeOptionsStatusHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс-кэш, хранящий состояние опций для атрибута, принадлежащего объекту-связи:
/// сначала определяется применительно к типу объекта-связи (если такое переназначение есть),
/// если не найден, то определяется непосредственно у самого типа атрибута.
/// </summary>
public class AttributeOptionsStatusHelper
{
  private Dictionary<int, AttributeOptions> cache = new Dictionary<int, AttributeOptions>();
  private AttributableElements attributableElements;
  private int elementType;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aAttributableElements">Объект-связь</param>
  /// <param name="aElementType">Идентификатор объекта-связи, для которых строить кэш опций атрибутов</param>
  public AttributeOptionsStatusHelper(AttributableElements aAttributableElements, int aElementType)
  {
    this.Init(aAttributableElements, aElementType);
  }

  /// <summary>
  /// Вернуть состояние опции для атрибута объекта/связи (инициализируется в конструкторе)
  /// </summary>
  /// <param name="aAttributeId"></param>
  /// <param name="option2check"></param>
  /// <returns></returns>
  public bool CheckOptionStatus(int aAttributeId, AttributeOptions option2check)
  {
    if (this.cache.ContainsKey(aAttributeId))
      return (this.cache[aAttributeId] & option2check) != 0;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(aAttributeId);
      if (attributeType != null)
      {
        this.cache[aAttributeId] = attributeType.Options;
        flag = (attributeType.Options & option2check) != 0;
      }
    }
    return flag;
  }

  /// <summary>Очистить</summary>
  public void Clear() => this.Clear(this.attributableElements, this.elementType);

  /// <summary>Очистить с переинициализацией новым объектом-связью</summary>
  /// <param name="aAttributableElements"></param>
  /// <param name="aElementType"></param>
  public void Clear(AttributableElements aAttributableElements, int aElementType)
  {
    this.Init(aAttributableElements, aElementType);
  }

  private void Init(AttributableElements aAttributableElements, int aElementType)
  {
    this.attributableElements = aAttributableElements;
    this.elementType = aElementType;
    this.cache.Clear();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributableTypeInfo attributableTypeInfo = (IDBAttributableTypeInfo) null;
    switch (aAttributableElements)
    {
      case AttributableElements.Object:
        attributableTypeInfo = (IDBAttributableTypeInfo) service.GetObjectType(aElementType);
        break;
      case AttributableElements.Relation:
        attributableTypeInfo = (IDBAttributableTypeInfo) service.GetRelationType(aElementType);
        break;
    }
    foreach (DataRow row in (InternalDataCollectionBase) attributableTypeInfo.Attributes.Select(string.Empty, (object) "F_ATTRIBUTE_ID").Rows)
    {
      IDBAttributeTypeInfo4 attributeById = attributableTypeInfo.Attributes.GetAttributeByID(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      if (attributeById != null)
        this.cache[attributeById.AttributeID] = attributeById.Options;
    }
  }
}
