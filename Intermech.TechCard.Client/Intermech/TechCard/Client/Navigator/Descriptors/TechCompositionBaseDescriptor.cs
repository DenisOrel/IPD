// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechCompositionBaseDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Navigator.Nodes;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>TechCard composition's base descriptor</summary>
public abstract class TechCompositionBaseDescriptor : 
  HiveDescriptor,
  ISupportedColumns,
  IDBObjectTypeSelectionID
{
  /// <summary>Composition object type id - to customize fields</summary>
  /// <remarks>Используется только для кастомизации полей - не тип текущего объекта !</remarks>
  private int _compObjTypeID;
  /// <summary>
  /// Composition relation's type id - для получения состава / кастомизации полей
  /// </summary>
  private IEnumerable<int> _compRelTypeIDs;
  /// <summary>Object's role (состав / применяемость)</summary>
  private RelatedObjectsRole _objectRole;
  /// <summary>Composition's filter</summary>
  private ITechCompositionFilter _compositionFilter;

  /// <summary>Constructor</summary>
  /// <param name="categoryId"></param>
  /// <param name="typeId"></param>
  /// <param name="compObjTypeId">Composition object type id - to customize fields</param>
  /// <param name="compRelTypeId">Composition relation's type id - для получения состава / кастомизации полей</param>
  /// <param name="caption">Descriptor / root node caption</param>
  /// <param name="role">Object's role (состав / применяемость)</param>
  /// <param name="compositionFilter">Composition's list</param>
  protected TechCompositionBaseDescriptor(
    int categoryId,
    int typeId,
    int compObjTypeId,
    int compRelTypeId,
    string caption,
    RelatedObjectsRole role,
    ITechCompositionFilter compositionFilter)
    : this(categoryId, typeId, compObjTypeId, (IEnumerable<int>) new int[1]
    {
      compRelTypeId
    }, caption, role, compositionFilter)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="categoryId">Категория</param>
  /// <param name="typeId">Тип</param>
  /// <param name="compObjTypeId">Composition object type id - to customize fields</param>
  /// <param name="compRelTypeIDs">Composition relation's type ids - для получения состава / кастомизации полей</param>
  /// <param name="caption">Descriptor / root node caption</param>
  /// <param name="role">Object's role (состав / применяемость)</param>
  /// <param name="compositionFilter">Composition's list</param>
  protected TechCompositionBaseDescriptor(
    int categoryId,
    int typeId,
    int compObjTypeId,
    IEnumerable<int> compRelTypeIDs,
    string caption,
    RelatedObjectsRole role,
    ITechCompositionFilter compositionFilter)
    : base(categoryId, typeId, caption)
  {
    this._compObjTypeID = compObjTypeId;
    this._compRelTypeIDs = compRelTypeIDs;
    this._caption = caption;
    this._objectRole = role;
    this._compositionFilter = compositionFilter;
  }

  /// <summary>Composition object's type id ( Can be empty )</summary>
  /// <remarks>Используется только для кастомизации полей - не тип текущего объекта !</remarks>
  public int CompObjTypeID
  {
    [DebuggerStepThrough] get => this._compObjTypeID;
    set => this._compObjTypeID = value;
  }

  /// <summary>Composition relation's type id</summary>
  /// <remarks>Используется для получения состава / кастомизации полей</remarks>
  public IEnumerable<int> CompRelTypeIDs
  {
    [DebuggerStepThrough] get => this._compRelTypeIDs;
    set => this._compRelTypeIDs = value;
  }

  /// <summary>Objects role (состав / применяемость)</summary>
  public RelatedObjectsRole ObjectsRole
  {
    [DebuggerStepThrough] get => this._objectRole;
    set
    {
      if (this._objectRole == value)
        return;
      this._objectRole = value;
      if (this.CompositionFilter == null)
        return;
      this.CompositionFilter.UpdateRelatedObjectsRole(value);
    }
  }

  /// <summary>Composition's filter</summary>
  public ITechCompositionFilter CompositionFilter
  {
    [DebuggerStepThrough] get => this._compositionFilter;
    set => this._compositionFilter = value;
  }

  /// <summary>Get node's address</summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override string GetAddress(INodeID nodeID)
  {
    return !(nodeID is NodeID nodeId) ? Helper.GetAddress(nodeID) : nodeId.Caption;
  }

  /// <summary>Get node by address</summary>
  /// <param name="address"></param>
  /// <returns></returns>
  public override INodeID ParseAddress(string address) => base.ParseAddress(address);

  /// <summary>Cериализовать описание узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Сериализованное представление узла</returns>
  [DebuggerStepThrough]
  public override PersistentState Serialize(INodeID nodeID) => (PersistentState) null;

  /// <summary>Десериализовать описание узла</summary>
  /// <param name="persistNodeID">Сериализованное представление узла</param>
  /// <returns>Описание узла</returns>
  [DebuggerStepThrough]
  public override INodeID Deserialize(PersistentState persistNodeID) => (INodeID) null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is TechCompositionBaseDescriptor compositionBaseDescriptor))
      return base.Equals(obj);
    return this._categoryID == compositionBaseDescriptor._categoryID && this._compObjTypeID == compositionBaseDescriptor._compObjTypeID && this._compRelTypeIDs == compositionBaseDescriptor._compRelTypeIDs && this._objectRole == compositionBaseDescriptor.ObjectsRole && this._compositionFilter == compositionBaseDescriptor.CompositionFilter;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>Отобразить колонку в поле</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Поле</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_STATUSES") ? (object) new NodeColumnID((object) -77, AttributeSourceTypes.Object) : Helper.MapColumnToFieldName(column);
  }

  /// <summary>Get root node's description</summary>
  /// <remarks>Abstract method - override in child class </remarks>
  /// <returns></returns>
  public override INodeID GetRecordNodeID() => throw new NotImplementedException();

  /// <summary>Get data by description</summary>
  /// <param name="nodeID">Node's description</param>
  /// <param name="dataFormat">Requested data type</param>
  /// <returns></returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    if (nodeID is TechCompositionNodeID compositionNodeId)
    {
      if (dataFormat == typeof (IDBTypedObjectID))
        return (object) new DBTypedObjectID(compositionNodeId.ObjectTypeID, compositionNodeId.ObjectID, compositionNodeId.ID, compositionNodeId.Caption, compositionNodeId.Owner, compositionNodeId.Version, compositionNodeId.BaseVersion, compositionNodeId.SiteID, compositionNodeId.ModificationID);
      if (dataFormat == typeof (IDBObjectID))
        return (object) new DBObjectID(compositionNodeId.ObjectID, compositionNodeId.ID, compositionNodeId.Caption, compositionNodeId.Owner);
      if (dataFormat == typeof (IDBRelationID))
        return (object) new DBRelationID(compositionNodeId.PrjLinkID, compositionNodeId.ObjectID, compositionNodeId.RelationTypeID, compositionNodeId.Sorting, compositionNodeId.RelGuid, compositionNodeId.ProjID);
      if (dataFormat == typeof (IDBObjectTypeID))
        return (object) new DBObjectTypeID(compositionNodeId.ObjectTypeID);
      if (dataFormat == typeof (IDBCheckedOutByID))
        return (object) new DBCheckedOutByID(compositionNodeId.ObjectID, compositionNodeId.CheckedOutBy, compositionNodeId.Owner);
    }
    return dataFormat == typeof (IDBObjectTypeSelectionID) ? (object) this : base.GetData(nodeID, dataFormat);
  }

  /// <summary>Get field's values</summary>
  /// <param name="nodeID">Node's description</param>
  /// <param name="fields">Fields</param>
  /// <returns></returns>
  public override object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    try
    {
      object[] recordValues = new object[fields.Length];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(((NodeID) nodeID).ObjectID);
        for (int index = 0; index < recordValues.Length; ++index)
        {
          try
          {
            if (fields[index] is string field)
              recordValues[index] = dbObject.GetAttributeByName(field).Value;
            if (fields[index] is int || fields[index] is ObligatoryObjectAttributes)
            {
              object[] valuesById = dbObject.GetValuesByID((int) fields[index], false);
              recordValues[index] = valuesById == null || valuesById.Length == 0 ? (object) null : valuesById[0];
            }
            recordValues[index] = recordValues[index] == DBNull.Value ? (object) null : recordValues[index];
          }
          catch
          {
          }
        }
      }
      return recordValues;
    }
    catch (ObjectNotFoundException ex)
    {
      return (object[]) null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public NodeColumnCollection GetSupportedColumns()
  {
    return TechCardNavTreeViewUtils.GetObjAndRelSupportedColumns(this._compObjTypeID, this._compRelTypeIDs == null || this._compRelTypeIDs.Count<int>() != 1 ? -1 : this._compRelTypeIDs.First<int>());
  }

  /// <summary>
  /// 
  /// </summary>
  int IDBObjectTypeSelectionID.BindedObjectTypeID => this._compObjTypeID;
}
