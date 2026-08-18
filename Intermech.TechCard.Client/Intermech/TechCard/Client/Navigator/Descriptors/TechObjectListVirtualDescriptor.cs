// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechObjectListVirtualDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.TechCard.Client.Navigator.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>
/// Declare custom TechCard descriptor to display
/// object list (typed or not) with columns customization by type +
/// displaying virtual fields
/// </summary>
public class TechObjectListVirtualDescriptor : TechObjectListDescriptor
{
  /// <summary>Additional / virtual column's data</summary>
  protected TechObjectListVirtualDescriptor.ObjVirtualFields _virtualData;

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public TechObjectListVirtualDescriptor(PersistentState state)
    : base(state)
  {
    this._virtualData = new TechObjectListVirtualDescriptor.ObjVirtualFields();
  }

  /// <summary>Constructor</summary>
  /// <param name="categoryId"></param>
  /// <param name="typeId"></param>
  /// <param name="caption"></param>
  /// <param name="objectIDs"></param>
  public TechObjectListVirtualDescriptor(
    int categoryId,
    int typeId,
    string caption,
    IList objectIDs)
    : base(categoryId, typeId, caption, objectIDs)
  {
    this._virtualData = new TechObjectListVirtualDescriptor.ObjVirtualFields();
  }

  /// <summary>Additional / virtual column's data</summary>
  public TechObjectListVirtualDescriptor.ObjVirtualFields VirtualData
  {
    get => this._virtualData;
    set => this._virtualData = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeId)
  {
    return (INode) new TechObjectListVirtualNode((IDescriptor) this, this._objectIDs, this.TypeID, this.ExpandNodes);
  }

  /// <summary>Значение виртуального поля</summary>
  public class VirtualField
  {
    /// <summary>Ид. типа атрибута</summary>
    protected int _attrTypeID;
    /// <summary>Значение поля</summary>
    protected object _fieldData;

    /// <summary>Конструктор</summary>
    /// <param name="attrTypeId">Ид. типа атрибута</param>
    /// <param name="fieldData">Значение поля</param>
    /// <remarks>Внимание! fieldData должен быть или простым типом, или
    /// MeasuredValue или объектом, реализующий IComparable</remarks>
    public VirtualField(int attrTypeId, object fieldData)
    {
      this._attrTypeID = attrTypeId;
      this._fieldData = fieldData;
    }

    /// <summary>Ид. типа атрибута</summary>
    public int AttrTypeID
    {
      get => this._attrTypeID;
      set => this._attrTypeID = value;
    }

    /// <summary>Значение поля</summary>
    public object FieldData
    {
      get => this._fieldData;
      set => this._fieldData = value;
    }
  }

  /// <summary>Описание объекта с полями</summary>
  public class ObjVirtualField : List<TechObjectListVirtualDescriptor.VirtualField>
  {
    /// <summary>Ид. версии объекта</summary>
    protected long _objectID;

    /// <summary>Конструктор</summary>
    public ObjVirtualField(long objectID) => this._objectID = objectID;

    /// <summary>Конструктор</summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public ObjVirtualField(
      IEnumerable<TechObjectListVirtualDescriptor.VirtualField> collection)
      : base(collection)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public ObjVirtualField(int capacity)
      : base(capacity)
    {
    }

    /// <summary>Получение индекса поля в списке по ид. типа атрибута</summary>
    /// <param name="attrTypeId"></param>
    /// <returns></returns>
    public int GetFieldIndex(int attrTypeId)
    {
      int fieldIndex = -1;
      if (attrTypeId == 0)
        return fieldIndex;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].AttrTypeID == attrTypeId)
        {
          fieldIndex = index;
          break;
        }
      }
      return fieldIndex;
    }

    /// <summary>Добавление новой записи в список</summary>
    /// <param name="field"></param>
    public new void Add(TechObjectListVirtualDescriptor.VirtualField field)
    {
      if (field == null || field.AttrTypeID == 0 || this.GetFieldIndex(field.AttrTypeID) != -1)
        return;
      base.Add(field);
    }

    /// <summary>Добавление новой записи в список</summary>
    /// <param name="attrTypeId"></param>
    /// <param name="fieldData"></param>
    public void Add(int attrTypeId, object fieldData)
    {
      this.Add(new TechObjectListVirtualDescriptor.VirtualField(attrTypeId, fieldData));
    }

    /// <summary>Получим список атрибутов</summary>
    /// <returns></returns>
    public int[] GetFieldAttrIds()
    {
      return this.Select<TechObjectListVirtualDescriptor.VirtualField, int>((Func<TechObjectListVirtualDescriptor.VirtualField, int>) (field => field.AttrTypeID)).ToArray<int>();
    }

    /// <summary>Ид. версии объекта</summary>
    public long ObjectID
    {
      get => this._objectID;
      set => this._objectID = value;
    }
  }

  /// <summary>Список объектов с полями</summary>
  public class ObjVirtualFields : List<TechObjectListVirtualDescriptor.ObjVirtualField>
  {
    /// <summary>Конструктор</summary>
    public ObjVirtualFields()
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public ObjVirtualFields(int capacity)
      : base(capacity)
    {
    }

    /// <summary>Получим список атрибутов</summary>
    /// <returns></returns>
    public int[] GetFieldAttrIds()
    {
      SortedList<int, int> sortedList = new SortedList<int, int>();
      foreach (TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField in (List<TechObjectListVirtualDescriptor.ObjVirtualField>) this)
      {
        foreach (int fieldAttrId in objVirtualField.GetFieldAttrIds())
        {
          if (!sortedList.ContainsKey(fieldAttrId))
            sortedList.Add(fieldAttrId, fieldAttrId);
        }
      }
      List<int> intList = new List<int>();
      intList.AddRange((IEnumerable<int>) sortedList.Keys);
      return intList.ToArray();
    }

    /// <summary>Поискописание объекта по ид. объекта</summary>
    /// <remarks>Для одного и того же объекта может быть несколько описаний</remarks>
    /// <param name="objectID"></param>
    /// <returns></returns>
    public List<TechObjectListVirtualDescriptor.ObjVirtualField> GetFields4Object(long objectID)
    {
      return this.Where<TechObjectListVirtualDescriptor.ObjVirtualField>((Func<TechObjectListVirtualDescriptor.ObjVirtualField, bool>) (objField => objField != null && objField.ObjectID == objectID)).ToList<TechObjectListVirtualDescriptor.ObjVirtualField>();
    }
  }
}
