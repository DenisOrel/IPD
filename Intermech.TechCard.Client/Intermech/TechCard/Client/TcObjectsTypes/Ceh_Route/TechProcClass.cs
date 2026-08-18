// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.TechProcClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Техпроцесс</summary>
public class TechProcClass : CustomTechClass
{
  /// <summary>Ид. связи с РМ</summary>
  private long _refLinkId;
  /// <summary>Ид. версии РМ</summary>
  private long _refObjId;
  /// <summary>Тип техпроцесса по отношению к РМ</summary>
  private Tp2RouteBaseType _tpRouteType;
  /// <summary>
  /// 
  /// </summary>
  private readonly CehTechClassList _cehTechList;

  /// <summary>Констуктор</summary>
  public TechProcClass(long objectId)
    : this(objectId, 0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId"> Ид. версии объекта</param>
  /// <param name="linkId">Ид. версии связи с родительским объектом</param>
  public TechProcClass(long objectId, long linkId)
    : base(objectId, linkId)
  {
    this._cehTechList = new CehTechClassList((CustomTechClass) this);
  }

  /// <summary>Очистить объект</summary>
  public override void Clear()
  {
    this._refLinkId = 0L;
    this._refObjId = 0L;
    this._tpRouteType = Tp2RouteBaseType.Main;
    base.Clear();
    if (this._cehTechList == null)
      return;
    this._cehTechList.Clear();
  }

  /// <summary>Загрузить данные из базы</summary>
  public override void LoadData(IUserSession session)
  {
    this.Clear();
    List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.ObjectId, session, new int[1]
    {
      TechCardConsts.RelTypes.TechRouteRelationID
    }, false);
    List<TechCardUtils.SostavTreeItem> sostavTreeItemList = new List<TechCardUtils.SostavTreeItem>();
    foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
    {
      if (sostavTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem.ObjectTypeID, TechCardConsts.ObjectTypes.CehRouteID))
        sostavTreeItemList.Add(sostavTreeItem);
    }
    if (sostavTreeItemList.Count > 0)
    {
      TechCardUtils.SostavTreeItem sostavTreeItem = sostavTreeItemList[0];
      this.RefObjID = sostavTreeItem.ProjID;
      this.RefLinkID = sostavTreeItem.LinkID;
      IDBRelation relation = session.GetRelation(sostavTreeItem.LinkID);
      if (relation != null)
      {
        IDBAttribute attributeByGuid = relation.GetAttributeByGuid(TechCardConsts.AttributeTypes.TP2RouteTypeAttrGuid);
        if (attributeByGuid != null)
          this._tpRouteType = (Tp2RouteBaseType) EnumTypeHelper.GetEnumValue(typeof (Tp2RouteBaseType), Convert.ToString(attributeByGuid.AsString), (object) Tp2RouteBaseType.Variant);
      }
    }
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehZahodObjectID);
    conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false));
    long objectId = this.ObjectId;
    IUserSession userSession = session;
    int[] relations = new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    };
    ConditionStructure[] array = conditionStructureList.ToArray();
    foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(objectId, userSession, (IEnumerable<int>) relations, false, array))
    {
      if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.CehZahodObjectID))
      {
        CehTechClass cehTechClass = new CehTechClass(sostavSortedTreeItem.PartID, sostavSortedTreeItem.LinkID);
        cehTechClass.LoadData(session);
        cehTechClass.OrderID = sostavSortedTreeItem.SortIdx;
        cehTechClass.Modified = false;
        this._cehTechList.Add(cehTechClass);
      }
    }
    this.Modified = false;
  }

  /// <summary>Сохранить данные в базу</summary>
  public override void SaveData(IUserSession session)
  {
    if (this.RefLinkID != 0L)
    {
      if (this.RefObjID == 0L)
      {
        session.GetRelation(this.RefLinkID)?.Delete(0L);
        this.RefLinkID = 0L;
      }
      else
      {
        IDBRelation relation = session.GetRelation(this.RefLinkID);
        if (relation != null)
        {
          if (relation.ProjID != this.RefObjID)
          {
            relation.Delete(0L);
            this.RefLinkID = 0L;
          }
        }
        else
          this.RefLinkID = 0L;
      }
    }
    if (this.RefObjID != 0L)
    {
      IDBRelation relation;
      if (this.RefLinkID == 0L)
      {
        relation = session.GetRelationCollection(TechCardConsts.RelTypes.TechRouteRelationID).Create(this.RefObjID, this.ObjectId);
        if (relation != null)
          this.RefLinkID = relation.RelationID;
      }
      else
        relation = session.GetRelation(this.RefLinkID);
      if (relation != null)
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(TechCardConsts.Utils.AttributeTypeByGuid(TechCardConsts.AttributeTypes.TP2RouteTypeAttrGuid, session), (object) EnumTypeHelper.GetCaption((Enum) this.TpRouteType))
        };
        relation.SetAttributesValues(valuesList);
      }
    }
    foreach (CehTechClass cehTech in (CustomTechClassList<CehTechClass>) this._cehTechList)
    {
      if (cehTech.LinkID != 0L)
        cehTech.SaveData(session);
    }
  }

  /// <summary>Список цехов-участков</summary>
  public CehTechClassList CehTechList => this._cehTechList;

  /// <summary>Ид. связи с РМ</summary>
  public long RefLinkID
  {
    get => this._refLinkId;
    set
    {
      if (this._refLinkId == value)
        return;
      this._refLinkId = value;
      this.Modified = true;
    }
  }

  /// <summary>Ид. версии РМ</summary>
  public long RefObjID
  {
    get => this._refObjId;
    set
    {
      if (this._refObjId == value)
        return;
      this._refObjId = value;
      this.Modified = true;
    }
  }

  /// <summary>Тип техпроцесса по отношению к РМ</summary>
  public Tp2RouteBaseType TpRouteType
  {
    get => this._tpRouteType;
    set
    {
      if (this._tpRouteType == value)
        return;
      this._tpRouteType = value;
      this.Modified = true;
    }
  }
}
