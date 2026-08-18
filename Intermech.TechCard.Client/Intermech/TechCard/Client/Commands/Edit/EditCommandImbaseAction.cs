// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.EditCommandImbaseAction
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.TechCard.Client.Imbase;
using Intermech.TechCard.Client.Settings.TechCardParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>
/// 
/// </summary>
/// <param name="actionParam"></param>
internal class EditCommandImbaseAction([NotNull] EditCommandActionParam actionParam) : 
  EditCommandAction(actionParam)
{
  /// <summary>Значение атрибута "Ссылка на объект IMBASE"</summary>
  private long _imbaseObjectId;
  /// <summary>Значение атрибута "Запись таблицы IMBASE"</summary>
  private long _imbaseRecordId;
  /// <summary>Режим создания объекта</summary>
  private ImbaseObjCreateInfo _imbaseCreateInfo;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="relInfoList2Update"></param>
  private void AcceptChangesForCreateNewMode(
    IUserSession session,
    IList<RelInfoItem> relInfoList2Update)
  {
    if (this._objectAttributeStorage.DeltaValues.IsEmpty<AttributeValues>() && this._relationAttributeStorage.DeltaValues.IsEmpty<AttributeValues>())
      return;
    HashSet<long> longSet = new HashSet<long>();
    IAutoSelectionService service = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
    foreach (RelObjInfoItem relObjInfoItem in (IEnumerable<RelInfoItem>) relInfoList2Update)
    {
      if (relObjInfoItem.RelationID != this._targetRelObjInfo.RelationID && longSet.Add(relObjInfoItem.PartInfo.ObjectID))
      {
        if (this._objectAttributeStorage.DeltaValues.Any<AttributeValues>())
          session.GetObject(relObjInfoItem.PartInfo.ObjectID, false)?.SetAttributesValues(this._objectAttributeStorage.DeltaValues.ToArray<AttributeValues>());
        if (this._relationAttributeStorage.DeltaValues.Any<AttributeValues>())
          session.GetRelation(relObjInfoItem.RelationID, false)?.SetAttributesValues(this._relationAttributeStorage.DeltaValues.ToArray<AttributeValues>());
      }
    }
    service?.ExecuteSelection(new AutoSelectionParams(this._targetRelObjInfo.PartInfo.ObjectID, this._targetRelObjInfo.RelationID, AutoSelectionMode.AutoObject));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="relInfoList2Update"></param>
  private void AcceptChangesForUseExistsMode(
    IUserSession session,
    IList<RelInfoItem> relInfoList2Update)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.PartInfo) || !(this._actionParam.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectID == this._targetRelObjInfo.PartInfo.ObjectID)
      return;
    IDBObject dbObject = session.GetObject(this._targetRelObjInfo.PartInfo.ObjectID);
    foreach (RelObjInfoItem relObjInfoItem in (IEnumerable<RelInfoItem>) relInfoList2Update)
    {
      IDBRelation relation = session.GetRelation(relObjInfoItem.RelationID, true);
      relation.ReplacePartObject(dbObject.ObjectID);
      if (this._relationAttributeStorage.DeltaValues.Any<AttributeValues>())
        relation.SetAttributesValues(this._relationAttributeStorage.DeltaValues.ToArray<AttributeValues>(), false, true);
    }
    if (dbObject == null || !dbObject.IsCreationMode)
      return;
    dbObject.CommitCreation(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override void DoLoadObjectAttributes(IDBObject dbObject)
  {
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    this._imbaseObjectId = attributeById1 == null || attributeById1.Value == DBNull.Value ? 0L : Convert.ToInt64(attributeById1.Value);
    this._imbaseRecordId = attributeById2 == null || attributeById2.Value == DBNull.Value ? 0L : Convert.ToInt64(attributeById2.Value);
    base.DoLoadObjectAttributes(dbObject);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool EditObject()
  {
    ImbaseObjectCreatorForm objectCreatorForm1 = new ImbaseObjectCreatorForm(new ImbaseSelectionParam(this._targetRelObjInfo.ProjInfo.ObjectID, (IEnumerable<int>) new int[1]
    {
      this._targetRelObjInfo.PartInfo.ObjTypeID
    }));
    objectCreatorForm1.Text = Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_254");
    objectCreatorForm1.MultiSelect = false;
    ImbaseObjectCreatorForm objectCreatorForm2 = objectCreatorForm1;
    if (this._imbaseObjectId != 0L)
    {
      IDBTypedObjectID itemData = this._actionParam.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      ImbaseObjectCaptionItem[] objectCaptionItemArray = new ImbaseObjectCaptionItem[1]
      {
        new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(this._imbaseObjectId, -1, itemData != null ? itemData.Caption : string.Empty), this._imbaseRecordId)
      };
      objectCreatorForm2.SelectedObjItems = (IEnumerable<ImbaseObjectInfoItem>) objectCaptionItemArray;
    }
    long newImbaseObjectId = this._imbaseObjectId;
    long newImbaseRecordId = this._imbaseRecordId;
    if (objectCreatorForm2.ShowDialog() == DialogResult.OK && (objectCreatorForm2.SelectedObjItems == null ? 0 : (objectCreatorForm2.SelectedObjItems.Any<ImbaseObjectInfoItem>() ? 1 : 0)) != 0)
    {
      ImbaseObjectInfoItem imbaseObjectInfoItem = objectCreatorForm2.SelectedObjItems.FirstOrDefault<ImbaseObjectInfoItem>();
      if (imbaseObjectInfoItem != null)
      {
        ITypedInfoItem objectInfo = imbaseObjectInfoItem.ObjectInfo;
        newImbaseObjectId = objectInfo != null ? objectInfo.ItemID : 0L;
        newImbaseRecordId = imbaseObjectInfoItem.RecordId;
      }
    }
    if ((this._imbaseObjectId != newImbaseObjectId ? 1 : (this._imbaseRecordId != newImbaseRecordId ? 1 : 0)) != 0)
      return this.EditObjectParams(newImbaseObjectId, newImbaseRecordId);
    this._imbaseCreateInfo.CreateMode = ImbaseObjCreateMode.iocmCreateNew;
    return TechCardParamsHelper.TechParams.Common.ShowCard4ImbaseEdit && base.EditObject();
  }

  /// <summary>
  /// Редактирование параметров объекта, созданного из Imbase
  /// </summary>
  private bool EditObjectParams(long newImbaseObjectId, long newImbaseRecordId)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) session, true);
      if (service == null)
        throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_19295.ssp_techcard_19296()), (object) typeof (IImbaseTechObjInfoService)));
      if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_19295.ssp_techcard_19297()), (object) typeof (IImbaseServer)));
      if (!service.GetCreationMode(this._targetRelObjInfo.PartInfo.ObjectID, this._targetRelObjInfo.PartInfo.ObjTypeID, session.SessionGUID, out this._imbaseCreateInfo))
        this._imbaseCreateInfo.CreateMode = ImbaseObjCreateMode.iocmUnknown;
      session.StartLogHistory();
      try
      {
        if (this._imbaseCreateInfo.CreateMode == ImbaseObjCreateMode.iocmUseExists)
        {
          this._targetRelObjInfo.PartInfo.ObjectID = customService.CreateObject(session.SessionGUID, 0L, newImbaseObjectId, newImbaseRecordId, false, this._targetRelObjInfo.PartInfo.ObjTypeID);
          if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.PartInfo))
            return false;
          flag = true;
        }
        else
          customService.FillObjectAttributes(session.SessionGUID, this._targetRelObjInfo.PartInfo.ObjectID, newImbaseObjectId, newImbaseRecordId, true);
        this._modificationsList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) session.GetModificationsHistoryList());
      }
      finally
      {
        session.StopLogHistory();
      }
    }
    return !TechCardParamsHelper.TechParams.Common.ShowCard4ImbaseEdit ? this.CheckModifications() | flag : base.EditObject() | flag;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void AcceptChanges()
  {
    this.CheckObjectsModifications();
    List<RelInfoItem> relInfoList2Update = this._etpRelObjInfoList != null ? new List<RelInfoItem>((IEnumerable<RelInfoItem>) this._etpRelObjInfoList) : new List<RelInfoItem>();
    relInfoList2Update.Add((RelInfoItem) this._targetRelObjInfo);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      session.StartLogHistory();
      IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      try
      {
        customService?.StartTransaction();
        switch (this._imbaseCreateInfo.CreateMode)
        {
          case ImbaseObjCreateMode.iocmUnknown:
          case ImbaseObjCreateMode.iocmCreateNew:
            this.AcceptChangesForCreateNewMode(session, (IList<RelInfoItem>) relInfoList2Update);
            break;
          case ImbaseObjCreateMode.iocmUseExists:
            this.AcceptChangesForUseExistsMode(session, (IList<RelInfoItem>) relInfoList2Update);
            break;
        }
        customService?.Commit();
        this._modificationsList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) session.GetModificationsHistoryList());
      }
      catch
      {
        customService?.Rollback();
        throw;
      }
      finally
      {
        session.StopLogHistory();
      }
    }
  }
}
