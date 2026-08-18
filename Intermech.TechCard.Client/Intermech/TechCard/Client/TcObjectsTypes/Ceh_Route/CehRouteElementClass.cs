// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteElementClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Расцеховочный элемент</summary>
/// <summary>Конструктор</summary>
/// <param name="objectId">Идентификатор версии объекта</param>
/// <param name="linkId">Идентификатор связи</param>
/// <param name="type">Ид. типа объекта</param>
public class CehRouteElementClass(long objectId, long linkId, int type) : CustomTechClass(objectId, linkId)
{
  /// <summary>Значение атрибута "Цех"</summary>
  protected internal long _cehAttrID;
  /// <summary>Значение атрибута "Вид работ"</summary>
  protected internal long _workTypeID;
  /// <summary>Статус удаления</summary>
  private bool _deleted;

  /// <summary>Конструктор</summary>
  public CehRouteElementClass()
    : this(0L, 0L, -1)
  {
  }

  /// <summary>Очистить объект</summary>
  public override void Clear()
  {
    base.Clear();
    this.LinkID = 0L;
    this._cehAttrID = 0L;
    this._workTypeID = 0L;
    this.Deleted = false;
  }

  /// <summary>Загрузить параметры из базы</summary>
  public override void LoadData(IUserSession session)
  {
    if (this.ObjectId != 0L)
    {
      IDBObject dbObject = session.GetObject(this.ObjectId);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.CehRouteAttrGUID);
        if (attributeByGuid1 != null)
          this._cehAttrID = attributeByGuid1.AsInteger;
        IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.WorkTypeAttrGuid);
        if (attributeByGuid2 != null)
          this._workTypeID = attributeByGuid2.AsInteger;
      }
    }
    if (this.LinkID == 0L)
      return;
    IDBRelation relation = session.GetRelation(this.LinkID);
    if (relation == null)
      return;
    this.LinkGuid = relation.GUID;
    IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
      return;
    this.OrderID = (long) Convert.ToInt32(attributeByGuid.AsInteger);
  }

  /// <summary>Сохранить параметры в базу</summary>
  public override void SaveData(IUserSession session)
  {
    if (!this.Modified)
      return;
    if (this.Deleted && this.LinkID != 0L)
    {
      IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -21)
      };
      relationCollection.LocalTypesMode = true;
      relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, this.ObjectId);
      if (dataTable != null && dataTable.Rows.Count == 1)
        session.GetObject(Math.Abs(this.ObjectId), false)?.Delete(0L);
      else
        session.GetRelation(this.LinkID)?.Delete(0L);
      this.LinkID = 0L;
    }
    if (this.LinkID == 0L)
      return;
    IDBRelation relation = session.GetRelation(this.LinkID);
    if (relation == null)
      return;
    AttributeValues[] valuesList = new AttributeValues[1]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.SortAttrTypeID, (object) this.OrderID)
    };
    relation.SetAttributesValues(valuesList);
  }

  /// <summary>
  /// 
  /// </summary>
  public Guid LinkGuid { get; set; }

  /// <summary>Значение атрибута "Цех"</summary>
  public long CehAttrID => this._cehAttrID;

  /// <summary>Значение атрибута "Вид работ"</summary>
  public long WorkTypeID => this._workTypeID;

  /// <summary>Идентификатор сортировки</summary>
  public override long OrderID
  {
    get => this._orderID;
    set
    {
      if (this._objID == value)
        return;
      this._orderID = value;
      this.Modified = true;
    }
  }

  /// <summary>Статус удаления объекта</summary>
  public bool Deleted
  {
    get => this._deleted;
    set
    {
      if (this._deleted == value)
        return;
      this._deleted = value;
      this.Modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return (!(obj is CehRouteElementClass routeElementClass) || (this._cehAttrID != routeElementClass.CehAttrID || this._workTypeID != routeElementClass.WorkTypeID ? 0 : (this._deleted == routeElementClass.Deleted ? 1 : 0)) != 0) && base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();
}
