// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechCompositionDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Navigator.Nodes;
using Intermech.TechCard.Client.Navigator.Params;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>TechCard composition's descriptor</summary>
public class TechCompositionDescriptor : TechCompositionBaseDescriptor
{
  /// <summary>Описание корневого объекта</summary>
  internal CreateTechNodeParams _params;

  /// <summary>Constructor</summary>
  /// <param name="categoryId">Категория</param>
  /// <param name="typeId">Тип</param>
  /// <param name="objId">Идентификатор версии корневого объекта</param>
  /// <param name="compObjTypeId">Composition object type id - to customize field</param>
  /// <param name="compRelTypeId">Composition relation's type id - для получения состава / кастомизации полей</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="role">Роль объектов</param>
  /// <param name="compositionFilter">Доп. фильтр состава</param>
  public TechCompositionDescriptor(
    int categoryId,
    int typeId,
    long objId,
    int compObjTypeId,
    int compRelTypeId,
    string caption,
    RelatedObjectsRole role,
    ITechCompositionFilter compositionFilter)
    : this(categoryId, typeId, objId, compObjTypeId, compRelTypeId, caption, role, compositionFilter, (IEnumerable<NodeColumnID>) null)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="categoryId">Категория</param>
  /// <param name="typeId">Тип</param>
  /// <param name="objId">Идентификатор версии корневого объекта</param>
  /// <param name="compObjTypeId">Composition object type id - to customize field</param>
  /// <param name="compRelTypeId">Composition relation type id - для получения состава / кастомизации полей</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="role">Роль объектов</param>
  /// <param name="compositionFilter">Доп. фильтр состава</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  public TechCompositionDescriptor(
    int categoryId,
    int typeId,
    long objId,
    int compObjTypeId,
    int compRelTypeId,
    string caption,
    RelatedObjectsRole role,
    ITechCompositionFilter compositionFilter,
    IEnumerable<NodeColumnID> attributes)
    : this(categoryId, typeId, objId, compObjTypeId, (IEnumerable<int>) new int[1]
    {
      compRelTypeId
    }, caption, role, compositionFilter, attributes)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="categoryId">Категория</param>
  /// <param name="typeId">Тип</param>
  /// <param name="objId">Идентификатор версии корневого объекта</param>
  /// <param name="compObjTypeId">Composition object type id - to customize field</param>
  /// <param name="compRelTypeIDs">Composition relation's type ids - для получения состава / кастомизации полей</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="role">Роль объектов</param>
  /// <param name="compositionFilter">Доп. фильтр состава</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  public TechCompositionDescriptor(
    int categoryId,
    int typeId,
    long objId,
    int compObjTypeId,
    IEnumerable<int> compRelTypeIDs,
    string caption,
    RelatedObjectsRole role,
    ITechCompositionFilter compositionFilter,
    IEnumerable<NodeColumnID> attributes)
    : base(categoryId, typeId, compObjTypeId, compRelTypeIDs, caption, role, compositionFilter)
  {
    this._params = new CreateTechNodeParams();
    this.ObjID = objId;
    if (attributes == null)
      return;
    this._params.Attributes = attributes.ToList<NodeColumnID>();
  }

  /// <summary>Root node object version id</summary>
  private long ObjID
  {
    [DebuggerStepThrough] get => this._params.ObjectID;
    set
    {
      if (this._params.ObjectID == value)
        return;
      this._params.ObjectID = value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._params.ObjectID);
        if (dbObject == null)
          return;
        this._params.ObjectTypeID = dbObject.ObjectType;
        this._params.ID = dbObject.ID;
        this._params.CheckedOutBy = dbObject.CheckoutBy;
        this._caption = this._params.Caption = dbObject.Caption;
        this._params.Owner = dbObject.OwnerID;
        this._params.SiteID = dbObject.SiteID;
        this._params.Version = (long) dbObject.VersionID;
        this._params.BaseVersion = Convert.ToInt64(dbObject.IsBaseVersion);
        if (this._params.Attributes == null)
          return;
        foreach (NodeColumnID attribute in this._params.Attributes)
        {
          IDBAttribute byId = dbObject.Attributes.FindByID((int) attribute.ID);
          this[(int) attribute.ID] = byId?.Value;
        }
      }
    }
  }

  /// <summary>Attribute values</summary>
  /// <param name="attributeId">Attribute id</param>
  /// <returns>Attr value or null if not found</returns>
  private object this[int attributeId]
  {
    get
    {
      for (int index = 0; index < this._params.Attributes.Count; ++index)
      {
        if (this._params.Attributes[index].ID.Equals((object) attributeId))
          return this._params.Values[index];
      }
      return (object) null;
    }
    set
    {
      for (int index = 0; index < this._params.Attributes.Count; ++index)
      {
        if (this._params.Attributes[index].ID.Equals((object) attributeId))
          this._params.Values[index] = value;
      }
    }
  }

  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public string FiltrationOwnerId
  {
    [DebuggerStepThrough] get => this._params.FiltrationOwnerID;
    set
    {
      this._params.FiltrationOwnerID = value != string.Empty ? value : "cad001e2-306c-11d8-b4e9-00304f19f545";
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is TechCompositionDescriptor compositionDescriptor))
      return base.Equals(obj);
    return this._params.ObjectID == compositionDescriptor._params.ObjectID && base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>Make compiler happy</remarks>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>Get root node's description</summary>
  /// <returns></returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new TechCompositionNodeID((CreateObjectNodeParams) this._params);
  }

  /// <summary>Get data by description</summary>
  /// <param name="nodeId">Node's description</param>
  /// <param name="dataFormat">Requested data type</param>
  /// <returns></returns>
  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new Intermech.Navigator.DBObjects.Descriptor(this._params.ObjectID);
    return dataFormat == typeof (INode) ? (object) this.GetChild(nodeId) : base.GetData(nodeId, dataFormat);
  }

  /// <summary>Get child node</summary>
  /// <param name="nodeId">Node's description</param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeId)
  {
    return !(nodeId is TechCompositionNodeID compositionNodeId) ? base.GetChild(nodeId) : (INode) new TechCompositionNode((IDescriptor) this, compositionNodeId.Params);
  }
}
