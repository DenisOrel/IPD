// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardObjectCreateAnalyzingService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Сервис для анализа доступности создания, добавления в состав родительских типов
/// </summary>
internal class TechCardObjectCreateAnalyzingService : ITechCardObjectCreateAnalyzingService
{
  private readonly object _syncRoot = new object();
  /// <summary>
  /// 
  /// </summary>
  private TechObjectCreatorArgs _creatorArgs;
  /// <summary>
  /// 
  /// </summary>
  private TechObjectCreatorParams _creatorParams;
  /// <summary>Описание связей (точнее типов)</summary>
  private readonly List<RelObjInfoItem> _relObjInfoList;
  /// <summary>Режимы модификации род. объектов</summary>
  private readonly Dictionary<ObjectModifyModes, List<ObjInfoItem>> _modifyMode2ObjInfoCache;

  /// <summary>Проверка допустимости создания объекта</summary>
  /// <returns></returns>
  private bool DoObjectCheckAccess() => true;

  /// <summary>Проверка допустимости добавления объекта в состав</summary>
  /// <returns></returns>
  private bool DoApplicabilityCheckAccess()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.DoApplicability_GetRelInfoObjList(sessionKeeper.Session);
      this.DoApplicability_GetModificationModes(sessionKeeper.Session);
    }
    return this.DoApplicability_AcceptModification() && this.DoApplicability_ObjectCheckout();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void DoApplicability_GetRelInfoObjList(IUserSession session)
  {
    this._relObjInfoList.Clear();
    TechObjectCreatorParams creatorParams = this._creatorParams;
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
    if (dbTypedObjectId != null)
    {
      RelObjInfoItem relObjInfoItem = new RelObjInfoItem((IDBRelation) null);
      relObjInfoItem.RelTypeID = TechCardConsts.RelTypes.TechRelationID;
      relObjInfoItem.ProjInfo = new ObjInfoItem(dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType);
      this._relObjInfoList.Add(relObjInfoItem);
    }
    else
    {
      if (this._creatorArgs.RelatedObjectIDs == null || this._creatorArgs.RelatedObjectIDs.Length == 0 || this._creatorArgs.RelationTypeIDs == null || this._creatorArgs.RelationTypeIDs.Length == 0)
        return;
      for (int index = 0; index < this._creatorArgs.RelatedObjectIDs.Length; ++index)
      {
        RelObjInfoItem relObjInfoItem = new RelObjInfoItem((IDBRelation) null);
        relObjInfoItem.RelTypeID = this._creatorArgs.RelationTypeIDs[index];
        relObjInfoItem.ProjInfo = new ObjInfoItem(this._creatorArgs.RelatedObjectIDs[index]);
        this._relObjInfoList.Add(relObjInfoItem);
      }
    }
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>(this._relObjInfoList.Count);
    objInfoList.AddRange(this._relObjInfoList.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (relInfo => relInfo.ProjInfo)));
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoList, session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userSession"></param>
  private void DoApplicability_GetModificationModes(IUserSession userSession)
  {
    this._modifyMode2ObjInfoCache.Clear();
    if (this._relObjInfoList == null || this._relObjInfoList.Count == 0 || this._creatorArgs.ObjectTypeIDs == null || this._creatorArgs.ObjectTypeIDs.Length == 0)
      return;
    foreach (RelObjInfoItem relObjInfo in this._relObjInfoList)
    {
      foreach (int objectTypeId in this._creatorArgs.ObjectTypeIDs)
      {
        IMSApplicability applicability = MetaDataHelper.GetApplicability(relObjInfo.ProjInfo.ObjTypeID, objectTypeId, relObjInfo.RelTypeID);
        if (applicability != null && applicability.IsContent)
        {
          IDBObject dbObject = userSession.GetObject(relObjInfo.ProjInfo.ObjectID, false);
          if (dbObject != null && dbObject.ReadOnly)
          {
            ObjectModifyModes objectModifyMode = dbObject.ObjectModifyMode;
            List<ObjInfoItem> objInfoItemList;
            if (!this._modifyMode2ObjInfoCache.TryGetValue(objectModifyMode, out objInfoItemList))
            {
              objInfoItemList = new List<ObjInfoItem>();
              this._modifyMode2ObjInfoCache.Add(objectModifyMode, objInfoItemList);
            }
            objInfoItemList.Add(relObjInfo.ProjInfo);
            relObjInfo.PartInfo = new ObjInfoItem(dbObject.CheckoutBy);
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool DoApplicability_AcceptModification()
  {
    if (this._modifyMode2ObjInfoCache == null || this._modifyMode2ObjInfoCache.Count == 0)
      return true;
    DescriptorCollection descriptors = new DescriptorCollection();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> objInfoList1;
      if (this._modifyMode2ObjInfoCache.TryGetValue(ObjectModifyModes.CantModify, out objInfoList1))
      {
        string caption = LocalizationHolder.rm.GetString(sc_19718.ssp_techcard_19719());
        Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList1);
        DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList1)), caption, objectTypeCache);
        dictDescriptor.ExpandNodes = false;
        descriptors.Add((IDescriptor) dictDescriptor);
      }
      if (this._modifyMode2ObjInfoCache.TryGetValue(ObjectModifyModes.CreateVersion, out objInfoList1))
      {
        string caption = LocalizationHolder.rm.GetString(sc_19718.ssp_techcard_19720());
        Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList1);
        DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList1)), caption, objectTypeCache);
        dictDescriptor.ExpandNodes = false;
        descriptors.Add((IDescriptor) dictDescriptor);
      }
      if (this._modifyMode2ObjInfoCache.TryGetValue(ObjectModifyModes.Checkout, out objInfoList1))
      {
        List<ObjInfoItem> objInfoList2 = new List<ObjInfoItem>(objInfoList1.Count);
        foreach (ObjInfoItem objInfoItem in objInfoList1)
        {
          ObjInfoItem objInfo = objInfoItem;
          RelObjInfoItem relObjInfoItem = this._relObjInfoList.Find((Predicate<RelObjInfoItem>) (item => item.ProjInfo.Equals(objInfo)));
          if (!((TypedInfoItem) relObjInfoItem == (TypedInfoItem) null) && relObjInfoItem.PartInfo.ObjectID != 0L && relObjInfoItem.PartInfo.ObjectID != sessionKeeper.Session.UserID)
            objInfoList2.Add(objInfo);
        }
        if (objInfoList2.Count != 0)
        {
          string caption = LocalizationHolder.rm.GetString(sc_19718.ssp_techcard_19721());
          Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList2);
          DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList2)), caption, objectTypeCache);
          dictDescriptor.ExpandNodes = false;
          descriptors.Add((IDescriptor) dictDescriptor);
        }
      }
    }
    if (descriptors.Count == 0)
      return true;
    TechcardObjectForm techcardObjectForm = new TechcardObjectForm();
    IDescriptor descriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, LocalizationHolder.rm.GetString("TechCard.Client_471"), descriptors);
    string caption1 = LocalizationHolder.rm.GetString("TechCard.Client_476");
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      int index = service.ImageIndex("imgCorruptedRule");
      if (index != -1)
      {
        Image image = service.ImageList.Images[index];
        using (Bitmap bmp = new Bitmap(image, image.Size))
          techcardObjectForm.Icon = ImageHelper.BitmapToIcon(bmp);
      }
    }
    techcardObjectForm.ShowBtnOk = false;
    techcardObjectForm.Name = "TechCardObjectCreateAnalyzingService_ErrorModification";
    techcardObjectForm.LoadData(caption1, descriptor);
    int num = (int) techcardObjectForm.ShowDialog();
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool DoApplicability_ObjectCheckout()
  {
    List<ObjInfoItem> objInfoItemList;
    if (this._modifyMode2ObjInfoCache == null || this._modifyMode2ObjInfoCache.Count == 0 || !this._modifyMode2ObjInfoCache.TryGetValue(ObjectModifyModes.Checkout, out objInfoItemList) || objInfoItemList.Count == 0)
      return true;
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoItemList);
    string caption1 = LocalizationHolder.rm.GetString("TechCard.Client_471");
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoItemList);
    DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoItemList)), caption1, objectTypeCache);
    dictDescriptor.ExpandNodes = false;
    TechcardObjectForm techcardObjectForm = new TechcardObjectForm();
    string caption2 = $"{LocalizationHolder.rm.GetString("TechCard.Client_475")} {LocalizationHolder.rm.GetString("TechCard.Client_470")}";
    INamedImageList service1 = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service1 != null)
    {
      int index = service1.ImageIndex("imgInvalidRule");
      if (index != -1)
      {
        Image image = service1.ImageList.Images[index];
        using (Bitmap bmp = new Bitmap(image, image.Size))
          techcardObjectForm.Icon = ImageHelper.BitmapToIcon(bmp);
      }
    }
    techcardObjectForm.Name = "TechCardObjectCreateAnalyzingService_ConfirmCheckOut";
    techcardObjectForm.ShowBtnOk = true;
    techcardObjectForm.LoadData(caption2, (IDescriptor) dictDescriptor);
    if (techcardObjectForm.ShowDialog() != DialogResult.OK)
      return false;
    IDBRelationID dbRelationId = (IDBRelationID) null;
    IDBObjectID dbObjectId = (IDBObjectID) null;
    if (this._creatorParams?.Items != null)
    {
      dbRelationId = this._creatorParams.Items.GetItemData<IDBRelationID>(0, false);
      dbObjectId = this._creatorParams.Items.GetParentData<IDBObjectID>(0, false);
    }
    NavigatorTreeView service2 = this._creatorParams != null ? ServiceUtils.GetService<NavigatorTreeView>((object) this._creatorParams.ContextServices, false) : (NavigatorTreeView) null;
    NodeIDPath focusedPath = service2?.FocusedPath;
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoItemList).ToArray());
    ServiceContainer viewServices1 = new ServiceContainer(this._creatorParams != null ? this._creatorParams.ContextServices : (System.IServiceProvider) null);
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    viewServices1.AddService(typeof (ObjectCommandsOptionsHolder), (object) new ObjectCommandsOptionsHolder(ObjectCommandsOptions.NoConfirmation));
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("CheckOut", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ObjInfoItem objInfoItem in objInfoItemList)
      {
        IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(objInfoItem.ObjectID, false);
        if (objectActualCopy1 != null)
        {
          if (this._creatorArgs.RelatedObjectIDs != null)
          {
            int index = Array.IndexOf<long>(this._creatorArgs.RelatedObjectIDs, objInfoItem.ObjectID);
            if (index != -1)
              this._creatorArgs.RelatedObjectIDs[index] = objectActualCopy1.ObjectID;
          }
          objInfoItem.ObjectID = objectActualCopy1.ObjectID;
          if (focusedPath == null && dbRelationId != null && dbObjectId != null && dbObjectId.Value == -objectActualCopy1.ObjectID)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(-dbRelationId.Value, false);
            if (relation != null)
            {
              IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(dbRelationId.PartID, false);
              if (objectActualCopy2 != null)
                this._creatorParams.Items = SelectedItemsHelper.CreateSelectedItemsForCompositionPart(relation.RelationID, objectActualCopy2.ObjectID);
            }
          }
        }
      }
    }
    if (focusedPath != null)
      service2.TryBrowse(focusedPath);
    return true;
  }

  /// <summary>Конструктор</summary>
  public TechCardObjectCreateAnalyzingService()
  {
    this._relObjInfoList = new List<RelObjInfoItem>();
    this._modifyMode2ObjInfoCache = new Dictionary<ObjectModifyModes, List<ObjInfoItem>>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="creatorArgs"></param>
  /// <param name="creatorParams"></param>
  /// <returns></returns>
  public bool AllowObjectCreation(
    TechObjectCreatorArgs creatorArgs,
    TechObjectCreatorParams creatorParams)
  {
    if (creatorArgs == null)
      throw new ArgumentNullException(nameof (creatorArgs));
    lock (this._syncRoot)
    {
      this._creatorArgs = creatorArgs;
      this._creatorParams = creatorParams;
      if (!this.DoObjectCheckAccess())
        return false;
      if (!this.DoApplicabilityCheckAccess())
        return false;
    }
    return true;
  }
}
