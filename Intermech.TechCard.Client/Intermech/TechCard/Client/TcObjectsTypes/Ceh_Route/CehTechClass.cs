// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehTechClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Цех техпроцесса</summary>
public class CehTechClass : CustomTechClass
{
  /// <summary>Ид. связи с расцеховочным элементом</summary>
  private long _reflinkId;
  /// <summary>Значение атрибута связи reflink "Ид. связи"</summary>
  private Guid _attrlinkGuid = Guid.Empty;
  /// <summary>Значение атрибута "цех"</summary>
  private long _cehAttrId;
  /// <summary>Список цехов техпроцесса</summary>
  private readonly OperTechClassList _operTechList;

  /// <summary>Конструктор</summary>
  public CehTechClass()
    : this(0L, 0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectjId">Идентификатор версии объекта</param>
  /// <param name="linkId">Идентификатор связи</param>
  public CehTechClass(long objectjId, long linkId)
    : base(objectjId, linkId)
  {
    this._operTechList = new OperTechClassList((CustomTechClass) this);
  }

  /// <summary>Очистить объект</summary>
  public override void Clear()
  {
    base.Clear();
    this._reflinkId = 0L;
    this._attrlinkGuid = Guid.Empty;
    this._cehAttrId = 0L;
    this._operTechList.Clear();
  }

  /// <summary>Загрузить из базы</summary>
  public override void LoadData(IUserSession session)
  {
    try
    {
      this.Clear();
      if (this.ObjectId == 0L)
        return;
      IDBObject dbObject = session.GetObject(this._objID);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.CehRouteAttrGUID);
        if (attributeByGuid != null)
          this._cehAttrId = attributeByGuid.AsInteger;
      }
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.ObjectId, session, new int[1]
      {
        TechCardConsts.RelTypes.TechRouteRelationID
      }, false);
      List<TechCardUtils.SostavTreeItem> sostavTreeItemList = new List<TechCardUtils.SostavTreeItem>();
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem.ObjectTypeID, TechCardConsts.ObjectTypes.ElemRouteID))
          sostavTreeItemList.Add(sostavTreeItem);
      }
      if (sostavTreeItemList.Count > 0)
        this.RefLinkID = sostavTreeItemList[0].LinkID;
      if (this.RefLinkID != 0L)
      {
        IDBRelation relation = session.GetRelation(this.RefLinkID);
        if (relation != null)
        {
          IDBAttribute attributeByGuid = relation.GetAttributeByGuid(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid);
          if (attributeByGuid != null)
          {
            Guid guid = Guid.Empty;
            try
            {
              guid = new Guid(attributeByGuid.AsString);
            }
            catch
            {
            }
            this.AttrLinkGuid = !(guid != Guid.Empty) ? Guid.Empty : guid;
          }
        }
      }
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.OperaciyaID);
      conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false));
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(this.ObjectId, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, conditionStructureList.ToArray()))
      {
        if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.OperaciyaID))
        {
          OperTechClass operTechClass = new OperTechClass(sostavSortedTreeItem.PartID, sostavSortedTreeItem.LinkID);
          operTechClass.LoadData(session);
          this.OperTechList.Add(operTechClass);
        }
      }
    }
    finally
    {
      this.Modified = false;
    }
  }

  /// <summary>Сохранить в базу</summary>
  public override void SaveData(IUserSession session)
  {
    if (!this.Modified)
      return;
    try
    {
      foreach (CustomTechClass operTech in (CustomTechClassList<OperTechClass>) this.OperTechList)
        operTech.SaveData(session);
      if (this.RefLinkID != 0L)
      {
        if (this.AttrLinkGuid != Guid.Empty)
        {
          IDBRelation relation1 = session.GetRelation(this.AttrLinkGuid, false);
          IDBRelation relation2 = session.GetRelation(this.RefLinkID);
          if (relation1 != null && relation2 != null && relation1.PartID != relation2.ProjID)
          {
            relation2.Delete(0L);
            this.RefLinkID = 0L;
          }
        }
        else
        {
          session.GetRelation(this._reflinkId)?.Delete(0L);
          this.RefLinkID = 0L;
        }
      }
      if (this.RefLinkID == 0L)
      {
        if (this._attrlinkGuid == Guid.Empty)
          return;
        IFiltrationService service = ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, false);
        if (service == null)
          return;
        IDBRelation relation = session.GetRelation(this._attrlinkGuid, false);
        if (relation != null)
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRouteRelationID);
          IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(relation.PartID, service.Filtration.OwnerID, false);
          if (objectByVersionsRule != null)
          {
            IDBRelation dbRelation = relationCollection.Create(objectByVersionsRule.ObjectID, this.ObjectId);
            if (dbRelation != null)
              this._reflinkId = dbRelation.RelationID;
          }
        }
      }
      int attributeID = TechCardConsts.Utils.AttributeTypeByGuid(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid, session);
      IDBRelation relation3 = session.GetRelation(this._reflinkId);
      if (relation3 == null)
        return;
      AttributeValues[] valuesList = new AttributeValues[1]
      {
        new AttributeValues(attributeID, (object) this._attrlinkGuid)
      };
      relation3.SetAttributesValues(valuesList);
    }
    finally
    {
      this.Modified = false;
    }
  }

  /// <summary>Значение атрибута "цех"</summary>
  public long CehAttrID => this._cehAttrId;

  /// <summary>Ид. связи с расцеховочным элементом</summary>
  public long RefLinkID
  {
    get => this._reflinkId;
    set
    {
      if (this._reflinkId == value)
        return;
      this._reflinkId = value;
      this.Modified = true;
    }
  }

  /// <summary>Значение атрибута связи reflink "Ид. связи"</summary>
  public Guid AttrLinkGuid
  {
    get => this._attrlinkGuid;
    set
    {
      if (!(this._attrlinkGuid != value))
        return;
      this._attrlinkGuid = value;
      this.Modified = true;
    }
  }

  /// <summary>Список цехов техпроцесса</summary>
  public OperTechClassList OperTechList => this._operTechList;
}
