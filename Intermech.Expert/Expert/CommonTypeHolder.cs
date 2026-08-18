// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.CommonTypeHolder
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Описатель ячейки (тип объекта, тип атрибута)</summary>
[Serializable]
public class CommonTypeHolder : ISerializable, ICloneable
{
  private ObjectTypeHolder _objectTypeHolder;
  private AttributeTypeHolder _attributeTypeHolder;

  private CommonTypeHolder()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeHolder">Описатель типа объекта</param>
  /// <param name="attributeTypeHolder">Описатель типа атрибута</param>
  public CommonTypeHolder(
    ObjectTypeHolder objectTypeHolder,
    AttributeTypeHolder attributeTypeHolder)
  {
    this._objectTypeHolder = objectTypeHolder;
    this._attributeTypeHolder = attributeTypeHolder;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="attributeType">Тип атрибута</param>
  /// <param name="session">Юзерская сессия</param>
  public CommonTypeHolder(int objectType, int attributeType, IUserSession session)
  {
    this._objectTypeHolder = ExpertTableCaches.GetObjHolder((long) objectType);
    if (this._objectTypeHolder == null)
    {
      this._objectTypeHolder = new ObjectTypeHolder(objectType, session);
      ExpertTableCaches.AddObjHolder((long) objectType, this._objectTypeHolder);
    }
    this._attributeTypeHolder = ExpertTableCaches.GetAttrHolder((long) attributeType);
    if (this._attributeTypeHolder != null)
      return;
    this._attributeTypeHolder = new AttributeTypeHolder(attributeType, session);
    ExpertTableCaches.AddAttrHolder((long) attributeType, this._attributeTypeHolder);
  }

  public void UnifyHolders()
  {
    if (this._objectTypeHolder != null)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(this._objectTypeHolder.Guid);
      if (objectTypeId != -1)
      {
        ObjectTypeHolder objHolder = ExpertTableCaches.GetObjHolder((long) objectTypeId);
        if (objHolder != null)
          this._objectTypeHolder = objHolder;
        else
          ExpertTableCaches.AddObjHolder((long) objectTypeId, this._objectTypeHolder);
      }
    }
    if (this._attributeTypeHolder == null)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(this._attributeTypeHolder.Guid);
    if (attributeTypeId == 0)
      return;
    AttributeTypeHolder attrHolder = ExpertTableCaches.GetAttrHolder((long) attributeTypeId);
    if (attrHolder != null)
      this._attributeTypeHolder = attrHolder;
    else
      ExpertTableCaches.AddAttrHolder((long) attributeTypeId, this._attributeTypeHolder);
  }

  /// <summary>Возвращает тип объекта</summary>
  public ObjectTypeHolder ObjectType => this._objectTypeHolder;

  /// <summary>Возвращает тип атрибута</summary>
  public AttributeTypeHolder AttributeType => this._attributeTypeHolder;

  /// <summary>Преобразование к строке</summary>
  /// <returns>Строка со значением</returns>
  public override string ToString()
  {
    return $"{this._objectTypeHolder.ToString()}.{this._attributeTypeHolder.ToString()}";
  }

  /// <summary>Проверка равенства</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>True если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (!obj.GetType().Equals(typeof (CommonTypeHolder)))
      return base.Equals(obj);
    CommonTypeHolder commonTypeHolder = obj as CommonTypeHolder;
    return this._objectTypeHolder.Equals((object) commonTypeHolder.ObjectType) && this._attributeTypeHolder.Equals((object) commonTypeHolder.AttributeType);
  }

  /// <summary>Определение hashcode'а объекта</summary>
  /// <returns>Hashcode</returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected CommonTypeHolder(SerializationInfo info, StreamingContext context)
  {
    this._objectTypeHolder = info.GetValue(nameof (ObjectType), typeof (ObjectTypeHolder)) as ObjectTypeHolder;
    this._attributeTypeHolder = info.GetValue(nameof (AttributeType), typeof (AttributeTypeHolder)) as AttributeTypeHolder;
    this.UnifyHolders();
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("ObjectType", (object) this._objectTypeHolder);
    info.AddValue("AttributeType", (object) this._attributeTypeHolder);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new CommonTypeHolder()
    {
      _attributeTypeHolder = (this._attributeTypeHolder.Clone() as AttributeTypeHolder),
      _objectTypeHolder = (this._objectTypeHolder.Clone() as ObjectTypeHolder)
    };
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    if (!this.AttributeType.Guid.Equals(fromAttribute.GUID))
      return false;
    this._attributeTypeHolder = new AttributeTypeHolder(toAttribute);
    return true;
  }
}
