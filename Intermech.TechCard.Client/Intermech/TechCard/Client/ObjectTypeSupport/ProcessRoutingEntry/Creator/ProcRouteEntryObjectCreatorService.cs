// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.Creator.ProcRouteEntryObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.Creator;

/// <summary>
/// Реализация создателя объектов типа "Входимость маршрута обработки"
/// </summary>
internal class ProcRouteEntryObjectCreatorService : TechObjectCreatorRiderCustomService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="attributeValueList"></param>
  private IList<long> GetProcRouteEntryWithValues(
    [NotNull] IUserSession session,
    IList<AttributeValues> attributeValueList)
  {
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    List<ConditionStructure> conditions = new List<ConditionStructure>();
    foreach (AttributeValues attributeValue in (IEnumerable<AttributeValues>) attributeValueList)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeValue.AttributeID);
      if (attributeType != null)
      {
        ConditionStructure conditionStructure = new ConditionStructure(attributeValue.AttributeID, RelationalOperators.Equal, attributeValue.Value, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object);
        switch (attributeType.FieldType)
        {
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            conditionStructure.Content = ColumnContents.ID;
            break;
        }
        conditions.Add(conditionStructure);
      }
    }
    conditions.Add(new ConditionStructure(-7, RelationalOperators.In, (object) this._creatorArgs.ObjectTypeIDs, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object));
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2)
    };
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) this._creatorArgs.RelatedObjectIDs), (IEnumerable<int>) this._creatorArgs.ObjectTypeIDs, (IEnumerable<int>) this._creatorArgs.RelationTypeIDs, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) conditions, true, false)
    {
      LoadLevels = 1
    };
    DataTable source = service.LoadComplexCompositions((object) session, loadingParams);
    return source == null ? (IList<long>) new List<long>() : (IList<long>) source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 0, 0L))).ToList<long>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId)
  {
    if (this._creatorArgs == null || this._creatorArgs.IsVersion || this._creatorArgs.TemplateObjectIDs != null && ((IEnumerable<long>) this._creatorArgs.TemplateObjectIDs).Any<long>((System.Func<long, bool>) (item => item != 0L && item != -1L)) || this._creatorArgs?.RelatedObjectIDs == null || this._creatorArgs.RelatedObjectIDs.Length == 0)
      return true;
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) this._creatorArgs.RelatedObjectIDs);
    if (objectInfoList == null || objectInfoList.Count == 0)
      return true;
    ISelectedItems navigatorSelection = SelectedItemsHelper.GetNavigatorSelection();
    if (navigatorSelection == null)
      return true;
    IList<AttributeValues> attributeValueList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!ProcRouteEntryHelper.GetCurrentEntryAttributeValues(sessionKeeper.Session, navigatorSelection, out attributeValueList))
        return true;
      if (this.GetProcRouteEntryWithValues(sessionKeeper.Session, attributeValueList).Any<long>())
        return true;
    }
    if (MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_ProcRouteEntry_FillCurrentApplicabilityParams"), string.Format(LocalizationHolder.rm.GetString("TechCard.Client_CreateNewObject"), (object) MetaDataHelper.GetObjectTypeName(((IEnumerable<int>) this._creatorArgs.ObjectTypeIDs).FirstOrDefault<int>())), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0, false) != DialogResult.Yes)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(newObjectId, true).SetAttributesValues(attributeValueList.ToArray<AttributeValues>());
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    if (this._creatorArgs == null)
      this._creatorArgs = new TechObjectCreatorArgs(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    base.AcceptDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    return false;
  }

  /// <summary>CreateObjectDialog</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    return base.CreateObjectDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      IDictionary<ObjectCreatePages, bool> visiblePages = base.VisiblePages;
      if (!visiblePages.ContainsKey(ObjectCreatePages.Relations))
        visiblePages.Add(ObjectCreatePages.Relations, true);
      else
        visiblePages[ObjectCreatePages.Relations] = true;
      return visiblePages;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public override Dictionary<UserControl, int> AddPages(object createdObject, int propPageIndex)
  {
    if (!(createdObject is CreatedObjectItem createdObject1))
      return (Dictionary<UserControl, int>) null;
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>();
    if ((this._creatorExtraParams == null || !this._creatorExtraParams.RawMode) && !createdObject1.IsVersion)
    {
      List<ObjectRelationLink> objectRelationArray = createdObject1.ObjectRelationArray;
      if (objectRelationArray == null || objectRelationArray.Count == 0)
        return dictionary;
      bool flag1 = false;
      bool flag2 = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
        List<ObjInfoItem> list = objectRelationArray.Select<ObjectRelationLink, ObjInfoItem>((System.Func<ObjectRelationLink, ObjInfoItem>) (a => new ObjInfoItem(a.ObjectID))).ToList<ObjInfoItem>();
        if (list.All<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (a => a.ObjectID == 0L)))
          return dictionary;
        ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) list, session);
        if (list.All<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (a => a.ObjTypeID != TechCardConsts.ObjectTypes.ProcRoutingID)))
          return dictionary;
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) list, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes), (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, RelObjInfoDbScheme<ObjInfoIDItem>.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, false, false, 1, (VersionsRule) null, "cad00601-306c-11d8-b4e9-00304f19f545");
        DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
        List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
        new RelObjInfoDbScheme<ObjInfoIDItem>(false).ParseInfoItems(session, source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) relObjInfoItemList);
        if (relObjInfoItemList.Count == 0)
          return dictionary;
        List<int> objTypesProduction = MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects);
        if (relObjInfoItemList.Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => objTypesProduction.Contains(a.ProjInfo.ObjTypeID))))
          flag1 = true;
        if (relObjInfoItemList.Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => !objTypesProduction.Contains(a.ProjInfo.ObjTypeID))))
          flag2 = true;
      }
      int num = 0;
      if (flag1)
      {
        dictionary.Add((UserControl) new ProcRouteEntryObjectCreatorControl(createdObject1, this._creatorExtraParams), num);
        ++num;
      }
      if (flag2)
        dictionary.Add((UserControl) new ProcRouteEntryObjectForArticleCreatorControl(createdObject1, this._creatorExtraParams), num);
    }
    return dictionary;
  }
}
