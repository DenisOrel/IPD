// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.TechCardCreateVersionSession
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion;

internal class TechCardCreateVersionSession
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IUserSession _session;
  /// <summary>
  /// 
  /// </summary>
  private readonly TechCardCreateVersionParams _param;
  /// <summary>
  /// Список объектов для текущего контекста вида ID объекта -&gt; ID версии
  /// </summary>
  private readonly IDictionary<long, EditingContextsObjectVersion> _contextObjectCache = (IDictionary<long, EditingContextsObjectVersion>) new Dictionary<long, EditingContextsObjectVersion>();
  /// <summary>
  /// Кэш вида "описание объекта" =&gt; "описание созданной версии"
  /// </summary>
  private readonly IDictionary<ObjInfoItem, ObjInfoIDItem> _objInfo2CreatedVersionItems = (IDictionary<ObjInfoItem, ObjInfoIDItem>) new Dictionary<ObjInfoItem, ObjInfoIDItem>();
  /// <summary>Список созданных связей с версиями</summary>
  private readonly IList<RelObjInfoItem> _createdRelInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="param"></param>
  private bool ValidateParams()
  {
    if (this._param.SignedObjInfoItems == null)
      throw new ArgumentNullException("param.SignedObjInfoItems");
    if (this._param.CompositionRelInfoItems == null)
      throw new ArgumentNullException("param.CompositionRelInfoItems");
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._param.EcoObjectInfo))
      return false;
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) this._param.SignedObjInfoItems, this._session);
    return true;
  }

  /// <summary>Получение контекста / его объектов</summary>
  private void LoadContextObjects()
  {
    foreach (EditingContextsObjectVersion contextsObjectVersion in ServiceUtils.GetService<IDBEditingContextsService>((object) this._session, true).GetEditingContextsObject((object) this._session.SessionGUID, this._param.EcoObjectInfo.ObjectID, false, false).Objects)
      this._contextObjectCache[contextsObjectVersion.F_ID] = contextsObjectVersion;
  }

  private bool CheckContextVersionInfo(ObjInfoIDItem objInfoItem, out ObjInfoIDItem versionInfoItem)
  {
    versionInfoItem = (ObjInfoIDItem) null;
    EditingContextsObjectVersion contextsObjectVersion;
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem) || !this._contextObjectCache.TryGetValue(objInfoItem.ID, out contextsObjectVersion))
      return false;
    versionInfoItem = new ObjInfoIDItem(contextsObjectVersion.F_OBJECT_ID)
    {
      ID = contextsObjectVersion.F_ID
    };
    if (versionInfoItem.ObjectID > 0L)
    {
      IDBObject objectActualCopy = this._session.GetObjectActualCopy(versionInfoItem.ObjectID, true);
      versionInfoItem.ObjectID = objectActualCopy.ObjectID;
      versionInfoItem.ObjTypeID = objectActualCopy.ObjectType;
    }
    return true;
  }

  private bool CreateVersionsForComposition(
    ObjInfoIDItem projVersionObjInfo,
    ICollection<ObjInfoIDItem> createdVersionObjItems)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) projVersionObjInfo))
      return false;
    if (projVersionObjInfo.ObjectID > 0L)
    {
      IDBObject dbObject1 = this._session.GetObject(projVersionObjInfo.ObjectID, true);
      if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject1.CheckoutBy == 0L)
      {
        IDBObject dbObject2 = dbObject1.CheckOut(true);
        projVersionObjInfo.ObjectID = dbObject2.ObjectID;
      }
    }
    List<RelObjInfoItem> source1 = new List<RelObjInfoItem>();
    foreach (RelObjInfoItem relObjInfoItem in this._param.CompositionRelInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => item.ProjInfo is ObjInfoIDItem projInfo && projInfo.ID == projVersionObjInfo.ID)))
    {
      ObjInfoIDItem partObjInfoIdItem = relObjInfoItem.PartInfo as ObjInfoIDItem;
      ObjInfoIDItem versionInfoItem;
      if (!((TypedInfoItem) partObjInfoIdItem == (TypedInfoItem) null) && (!this._objInfo2CreatedVersionItems.TryGetValue((ObjInfoItem) partObjInfoIdItem, out versionInfoItem) || !this._param.RelObjInfoItems.All<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.PartInfo != (TypedInfoItem) partObjInfoIdItem))))
      {
        if (this.CheckContextVersionInfo(partObjInfoIdItem, out versionInfoItem))
        {
          createdVersionObjItems.Add(versionInfoItem);
          this.CreateVersionsForComposition(versionInfoItem, createdVersionObjItems);
        }
        else
          source1.Add(relObjInfoItem);
      }
    }
    if (!source1.Any<RelObjInfoItem>())
      return true;
    IEnumerable<RelObjInfoItem> createdVersionRelObjItems;
    if (!this.LoadObjectVersionComposition(projVersionObjInfo, out createdVersionRelObjItems))
      return false;
    foreach (RelObjInfoItem relObjInfoItem in source1)
    {
      RelObjInfoItem needVersionRelInfoItem = relObjInfoItem;
      IEnumerable<RelObjInfoItem> source2 = createdVersionRelObjItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => item.PartInfo.Equals(needVersionRelInfoItem.PartInfo)));
      if (!source2.Any<RelObjInfoItem>())
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_462"), (object) TechCardConsts.Utils.GetObjectString(needVersionRelInfoItem.PartInfo.ObjectID, this._session), (object) needVersionRelInfoItem.PartInfo.ObjectID, (object) needVersionRelInfoItem.PartInfo.ObjectID, (object) TechCardConsts.Utils.GetObjectString(projVersionObjInfo.ObjectID, this._session), (object) projVersionObjInfo.ObjectID));
      ObjInfoIDItem projVersionObjInfo1;
      if (this._objInfo2CreatedVersionItems.TryGetValue(needVersionRelInfoItem.PartInfo, out projVersionObjInfo1))
      {
        if (this._param.RelObjInfoItems.All<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.PartInfo != (TypedInfoItem) needVersionRelInfoItem.PartInfo)))
          continue;
      }
      else
      {
        IDBObject version = this._session.GetObjectCollection(needVersionRelInfoItem.PartInfo.ObjTypeID).CreateVersion(needVersionRelInfoItem.PartInfo.ObjectID);
        TechCardClientConst.MarkObjectAsModified(version.ObjectID, this._session);
        if (version.IsCreationMode)
          version.CommitCreation(true, true);
        projVersionObjInfo1 = new ObjInfoIDItem(version);
        createdVersionObjItems.Add(projVersionObjInfo1);
        this._objInfo2CreatedVersionItems[needVersionRelInfoItem.PartInfo] = projVersionObjInfo1;
      }
      foreach (RelInfoItem relInfoItem in source2)
      {
        IDBRelation relation = this._session.GetRelation(relInfoItem.RelationID);
        if (relation != null)
        {
          AttributeValues[] valuesList = new AttributeValues[1]
          {
            new AttributeValues(TechCardConsts.AttributeTypes.ContextVersionID, (object) Math.Abs(projVersionObjInfo1.ObjectID))
          };
          relation.SetAttributesValues(valuesList);
          CreatedVersionRelationItem versionRelationItem = new CreatedVersionRelationItem(relation);
          versionRelationItem.PrototypeRelationItem = needVersionRelInfoItem;
          versionRelationItem.PartInfo = (ObjInfoItem) projVersionObjInfo1;
          versionRelationItem.ProjInfo = (ObjInfoItem) projVersionObjInfo;
          this._createdRelInfoItems.Add((RelObjInfoItem) versionRelationItem);
        }
      }
      this.CreateVersionsForComposition(projVersionObjInfo1, createdVersionObjItems);
    }
    return createdVersionRelObjItems.Any<RelObjInfoItem>();
  }

  private bool LoadObjectVersionComposition(
    ObjInfoIDItem createdVersionObjInfo,
    out IEnumerable<RelObjInfoItem> createdVersionRelObjItems)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[7]
    {
      new ColumnDescriptor((object) -20, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -23, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -21, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -22, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.Equal, (object) createdVersionObjInfo.ObjectID, LogicalOperators.NONE, 0, false)
    };
    DataTable dataTable = (DataTable) null;
    foreach (int relationType in this.RelationTypes)
    {
      IDBRelationCollection relationCollection = this._session.GetRelationCollection(relationType);
      relationCollection.LocalTypesMode = true;
      relationCollection.FiltrationOwnerID = "cad001e3-306c-11d8-b4e9-00304f19f545";
      DataTable fromTable = relationCollection.Select(new DBRecordSetParams(conditions, columns));
      if (dataTable != null)
        DataSetProcessor.AddTable(dataTable, fromTable, false);
      else
        dataTable = fromTable;
    }
    if (dataTable == null)
    {
      createdVersionRelObjItems = (IEnumerable<RelObjInfoItem>) null;
      return false;
    }
    TechRelObjInfoItemsFromDataTableProvider<RelObjInfoItem> dataTableProvider = new TechRelObjInfoItemsFromDataTableProvider<RelObjInfoItem>(dataTable);
    createdVersionRelObjItems = dataTableProvider.Execute();
    return createdVersionRelObjItems.Any<RelObjInfoItem>();
  }

  private void RegisterEcoAuxObjects(
    ObjInfoIDItem signedObjInfo,
    ICollection<ObjInfoIDItem> createdVersionObjItems)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) signedObjInfo) || !createdVersionObjItems.Any<ObjInfoIDItem>())
      return;
    IDBRelation relation = this._session.GetRelation(this._param.EcoObjectInfo.ObjectID, signedObjInfo.ObjectID, MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"), true);
    if (relation == null)
      return;
    HashSet<long> source = new HashSet<long>(createdVersionObjItems.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => Math.Abs(item.ObjectID))));
    IDBAttribute attributeByGuid = relation.GetAttributeByGuid(TechCardConsts.AttributeTypes.EcoAuxObjAttrGuid);
    if (attributeByGuid?.Values != null)
    {
      foreach (object obj in attributeByGuid.Values)
      {
        if (obj != DBNull.Value)
          source.Add(Convert.ToInt64(obj));
      }
    }
    IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545"), TechCardConsts.AttributeTypes.EcoAuxObjAttrGuid);
    bool flag = false;
    IDBObject dbObject = (IDBObject) null;
    if (attribute4RelationType != null && (attribute4RelationType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
    {
      dbObject = this._session.GetObjectActualCopy(this._param.EcoObjectInfo.ObjectID, false);
      if (dbObject != null && dbObject.CheckoutBy == 0L)
      {
        dbObject = dbObject.CheckOut(true);
        flag = true;
      }
    }
    relation.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.EcoAuxObjAttrGuid), (object) source.Select<long, object>((System.Func<long, object>) (item => (object) item)).ToArray<object>())
    });
    if (!flag)
      return;
    dbObject.CheckIn();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="param"></param>
  public TechCardCreateVersionSession([NotNull] IUserSession session, [NotNull] TechCardCreateVersionParams param)
  {
    this._session = session;
    this._param = param;
  }

  public bool Execute(
    out IEnumerable<RelObjInfoItem> createdRelInfoItems)
  {
    createdRelInfoItems = (IEnumerable<RelObjInfoItem>) null;
    if (!this.ValidateParams())
      return false;
    this.LoadContextObjects();
    bool flag = false;
    IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) this._session, false);
    try
    {
      service?.StartTransaction();
      this._session.EditingContextID = this._param.EcoObjectInfo.ObjectID;
      foreach (ObjInfoIDItem signedObjInfoItem in this._param.SignedObjInfoItems)
      {
        ICollection<ObjInfoIDItem> objInfoIdItems = (ICollection<ObjInfoIDItem>) new HashSet<ObjInfoIDItem>();
        if (!this.CreateVersionsForComposition(signedObjInfoItem, objInfoIdItems))
          return false;
        if (objInfoIdItems.Any<ObjInfoIDItem>())
        {
          foreach (ObjInfoItem objInfoItem in (IEnumerable<ObjInfoIDItem>) objInfoIdItems)
            TechCardClientConst.MarkObjectAsModified(objInfoItem.ObjectID, this._session);
          TechCardClientConst.MarkObjectAsModified(signedObjInfoItem.ObjectID, this._session);
          this.RegisterEcoAuxObjects(signedObjInfoItem, objInfoIdItems);
        }
      }
    }
    catch
    {
      flag = true;
      throw;
    }
    finally
    {
      if (service != null)
      {
        if (flag)
          service.Rollback();
        else
          service.Commit();
      }
    }
    createdRelInfoItems = (IEnumerable<RelObjInfoItem>) this._createdRelInfoItems;
    return true;
  }

  /// <summary>Список обрабатываемых типов связей</summary>
  public IEnumerable<int> RelationTypes { get; set; } = (IEnumerable<int>) TechCardConsts.RelTypes.TechAllRelationTypes;
}
