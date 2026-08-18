// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteElementContainer
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>
/// 
/// </summary>
public class CehRouteElementContainer : CustomTechClass
{
  /// <summary>
  /// 
  /// </summary>
  private readonly CehRouteElementList _routeElementList;

  internal CehRouteElementContainer(long objectId, long linkId = 0)
    : base(objectId, linkId)
  {
    this._routeElementList = new CehRouteElementList((CustomTechClass) this);
  }

  /// <summary>Очистить объект</summary>
  public override void Clear()
  {
    base.Clear();
    this._routeElementList?.Clear();
  }

  /// <summary>Загрузка данных из базы</summary>
  public override void LoadData(IUserSession session)
  {
    this._routeElementList?.Clear();
    ColumnDescriptor[] columns1 = new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.CehRouteAttrGUID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.WorkTypeAttrGuid, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -26, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    };
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ElemRouteID).ToArray(), (object) null, LogicalOperators.NONE, 0, false)
    };
    DataTable childSostavData = DataHelper.GetChildSostavData(this.ObjectId, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns1);
    if (childSostavData == null)
    {
      this.Modified = false;
    }
    else
    {
      int columnIndex1 = childSostavData.Columns.IndexOf("F_PRJLINK_ID");
      int columnIndex2 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex3 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
      DataColumnCollection columns2 = childSostavData.Columns;
      Guid guid = TechCardConsts.AttributeTypes.CehRouteAttrGUID;
      string columnName1 = guid.ToString();
      int columnIndex4 = columns2.IndexOf(columnName1);
      DataColumnCollection columns3 = childSostavData.Columns;
      guid = TechCardConsts.AttributeTypes.WorkTypeAttrGuid;
      string columnName2 = guid.ToString();
      int columnIndex5 = columns3.IndexOf(columnName2);
      int columnIndex6 = childSostavData.Columns.IndexOf("F_PRJ_GUID");
      DataColumnCollection columns4 = childSostavData.Columns;
      guid = TechCardConsts.AttributeTypes.SortAttrTypeGuid;
      string columnName3 = guid.ToString();
      int columnIndex7 = columns4.IndexOf(columnName3);
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        CehRouteElementClass routeElementClass1 = new CehRouteElementClass(DataSetProcessor.GetInt64Value(row, columnIndex2, 0L), DataSetProcessor.GetInt64Value(row, columnIndex1, 0L), DataSetProcessor.GetInt32Value(row, columnIndex3, -1));
        routeElementClass1._cehAttrID = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
        routeElementClass1._workTypeID = DataSetProcessor.GetInt64Value(row, columnIndex5, 0L);
        routeElementClass1.OrderID = DataSetProcessor.GetInt64Value(row, columnIndex7, 0L);
        CehRouteElementClass routeElementClass2 = routeElementClass1;
        string stringValue = DataSetProcessor.GetStringValue(row, columnIndex6, string.Empty);
        if (GuidHelper.IsGuid(stringValue))
          routeElementClass2.LinkGuid = new Guid(stringValue);
        routeElementClass2.Modified = false;
        this.RouteElementList.Add(routeElementClass2);
      }
      int num = 0;
      foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this.RouteElementList)
      {
        if (routeElement.OrderID <= 0L)
          routeElement.OrderID = (long) (1000 * (this.RouteElementList.Count + num++));
      }
      this.Modified = false;
    }
  }

  /// <summary>Сохранение данных в базу</summary>
  public override void SaveData(IUserSession session)
  {
    TechcardClientUtils.StartCreateRelations(this.ObjectId, session);
    try
    {
      foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this.RouteElementList)
      {
        if (routeElement.LinkID != 0L)
        {
          routeElement.SaveData(session);
        }
        else
        {
          IDBObject dbObject = session.GetObject(routeElement.ObjectId);
          if (dbObject != null)
          {
            List<IDBRelation> relations = TechcardClientUtils.CreateRelations(session, dbObject.ObjectID, new int[1]
            {
              TechCardConsts.RelTypes.TechRelationID
            }, new long[1]{ this.ObjectId }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
            if (relations.Count > 0)
            {
              routeElement.LinkID = relations[0].RelationID;
              routeElement.SaveData(session);
            }
            if (dbObject.IsCreationMode)
            {
              dbObject.CommitCreation(false, true);
              routeElement.ObjectId = dbObject.ObjectID;
            }
          }
        }
      }
    }
    finally
    {
      TechcardClientUtils.StopCreateRelations(session);
    }
    this.Modified = false;
  }

  /// <summary>Отменить изменения</summary>
  public void CancelChanges()
  {
    foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) this.RouteElementList)
    {
      if (routeElement.LinkID == 0L)
        this.RouteElementList.RemoveFromBase(routeElement);
    }
  }

  /// <summary>Список расцеховочных элементов</summary>
  public CehRouteElementList RouteElementList => this._routeElementList;
}
