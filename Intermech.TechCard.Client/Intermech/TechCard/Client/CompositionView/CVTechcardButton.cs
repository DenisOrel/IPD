// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.CompositionView.CVTechcardButton
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.CompositionView;
using Intermech.DataFormats;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.CompositionView;

/// <summary>Techcard composition button</summary>
[Serializable]
internal class CVTechcardButton : CVTechcardButtonBase
{
  /// <summary>Imbase catalog/dictionary guid</summary>
  private Guid _dictionaryGuid = Guid.Empty;
  /// <summary>Is button registered</summary>
  private static bool _isRegister;

  /// <summary>Constructor</summary>
  public CVTechcardButton() => this.ImageName = "imgServerObjects";

  /// <summary>Register button in composition view</summary>
  public static void RegisterButton()
  {
    if (CVTechcardButton._isRegister)
      return;
    CompositionViewButtons service = ServiceUtils.GetService<CompositionViewButtons>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.Add(typeof (CVTechcardButton), LocalizationHolder.rm.GetString("TechCard.Client_83"));
    CVTechcardButton._isRegister = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  public override void ApplyParams(CVButtonBase button)
  {
    if (!(button is CVTechcardButton button1))
      return;
    base.ApplyParams((CVButtonBase) button1);
    this._dictionaryGuid = button1._dictionaryGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override bool Select()
  {
    IDescriptor rootDescriptor = (IDescriptor) new ImbaseRootNodeDescriptor();
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("TechCard.Client_84"), string.Empty, rootDescriptor, SelectionOptions.Default);
    if (numArray != null && numArray.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[0], false);
        if (dbObject != null)
        {
          this._dictionaryGuid = ((IDBGuid) dbObject).GUID;
          this._hint = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_85"), (object) dbObject.Caption);
          return true;
        }
      }
    }
    return base.Select();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDescriptor BuildTree()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._dictionaryGuid, false);
      if (dbObject != null)
        return (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(dbObject.ObjectID);
    }
    return base.BuildTree();
  }

  /// <summary>Проверка на доступность действия</summary>
  /// <param name="args"></param>
  public override CVButtonEnabled Check(CVLocalButton.CVButtonArgs args)
  {
    return CVLocalButton.Check((CVButtonBase) this, args);
  }

  /// <summary>Выполнение действия</summary>
  /// <param name="args"></param>
  public override void Click(CVLocalButton.CVButtonClickArgs args)
  {
    CVLocalButton.Click((CVButtonBase) this, args);
  }

  /// <summary>Load button from XML</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  public static CVTechcardButton Load(XmlNode node)
  {
    if (!node.Name.Equals(typeof (CVTechcardButton).FullName))
      return (CVTechcardButton) null;
    CVTechcardButton cvTechcardButton = new CVTechcardButton();
    XmlAttribute attribute = node.Attributes?["Guid"];
    if (attribute != null)
    {
      try
      {
        cvTechcardButton._dictionaryGuid = new Guid(attribute.Value);
      }
      catch
      {
        cvTechcardButton._dictionaryGuid = Guid.Empty;
      }
    }
    return cvTechcardButton;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xml"></param>
  public override void Save(XmlNode xml)
  {
    XmlNode xmlNode = this.SaveInternal(xml);
    XmlAttribute attribute = xml.OwnerDocument?.CreateAttribute("Guid");
    if (attribute == null)
      return;
    attribute.Value = this._dictionaryGuid.ToString();
    xmlNode.Attributes?.Append(attribute);
  }

  /// <summary>
  /// Приведение типов selectedItems к требуемым,
  /// (т.к. тип создаваемого объекта отличается от текущего)
  /// </summary>
  /// <param name="typedObjectIds">SelectedItems</param>
  /// <returns></returns>
  public override List<IDBTypedObjectID> DoConvertTypes(List<IDBTypedObjectID> typedObjectIds)
  {
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      if (service == null)
        return dbTypedObjectIdList;
      List<long> objects = new List<long>(typedObjectIds.Count);
      foreach (IDBTypedObjectID typedObjectId in typedObjectIds)
      {
        if (typedObjectId != null)
          objects.Add(typedObjectId.ObjectID);
      }
      Dictionary<long, ImbaseObjCreateInfo> objCreateInfo;
      if (!service.GetCreationMode((IList<long>) objects, sessionKeeper.Session.SessionGUID, out objCreateInfo))
        return dbTypedObjectIdList;
      foreach (IDBTypedObjectID typedObjectId in typedObjectIds)
      {
        ImbaseObjCreateInfo imbaseObjCreateInfo;
        if (typedObjectId != null && objCreateInfo.TryGetValue(typedObjectId.ObjectID, out imbaseObjCreateInfo) && imbaseObjCreateInfo.ObjectType != -1)
        {
          if (typedObjectId is CompDBTypedObjectID compDbTypedObjectId)
          {
            compDbTypedObjectId.ObjectType = imbaseObjCreateInfo.ObjectType;
            dbTypedObjectIdList.Add((IDBTypedObjectID) compDbTypedObjectId);
          }
          else
            dbTypedObjectIdList.Add((IDBTypedObjectID) new DBTypedObjectID(imbaseObjCreateInfo.ObjectType, typedObjectId.ObjectID, typedObjectId.ID, typedObjectId.Caption, typedObjectId.Owner, typedObjectId.Version, typedObjectId.BaseVersion, typedObjectId.SiteID, typedObjectId.ModificationID));
        }
      }
    }
    return dbTypedObjectIdList;
  }

  /// <summary>
  /// Получение/создание нового объекта по заданным параметрам
  /// </summary>
  /// <param name="ownerObjId"></param>
  /// <param name="objectId"></param>
  /// <param name="relationHash"></param>
  /// <param name="session"></param>
  /// <param name="throwException"></param>
  /// <param name="errorString"></param>
  /// <returns></returns>
  public override IDBObject DoCreateObject(
    IDBTypedObjectID ownerObjId,
    IDBTypedObjectID objectId,
    Dictionary<int, List<cvRelationInfo>> relationHash,
    IUserSession session,
    bool throwException,
    out string errorString)
  {
    if (session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      return this.PrepareId(ownerObjId, objectId, relationHash, session, customService, false, throwException, out errorString);
    errorString = string.Format(LocalizationHolder.rm.GetString(sc_19306.ssp_techcard_19307()), (object) typeof (IImbaseServer));
    return (IDBObject) null;
  }

  /// <summary>Завершение создания объекта typedObject</summary>
  /// <param name="typedObject"></param>
  /// <param name="session"></param>
  public override void DoCommitObject(IDBObject typedObject, IUserSession session)
  {
    if (typedObject == null || !typedObject.IsCreationMode)
      return;
    typedObject.CommitCreation(true, UISettings.AutoCheckOutNewObjects);
  }

  /// <summary>Get selected items</summary>
  /// <param name="treeView"></param>
  /// <param name="viewsManager"></param>
  /// <returns></returns>
  public override List<IDBTypedObjectID> GetSelectedItems(
    NavigatorTreeView treeView,
    IViewsManager viewsManager)
  {
    List<IDBTypedObjectID> selectedItems1 = new List<IDBTypedObjectID>();
    ISelectedItemsHost control = viewsManager.ActiveViewPage?.Control as ISelectedItemsHost;
    ISelectedItems selectedItems2 = (ISelectedItems) null;
    ISelectedItems selectedItems3 = control?.SelectedItems;
    if (treeView.FocusedNode != null)
      selectedItems2 = treeView.FocusedNode.NodeID.CategoryID.Equals(1) ? treeView.SelectedItems : (ISelectedItems) null;
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
    if (selectedItems3 == null || selectedItems3.Count == 0)
      selectedItems3 = selectedItems2;
    else if (selectedItems2 != null && selectedItems2.Count != 0)
      dbTypedObjectId = selectedItems3.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (selectedItems3 != null && selectedItems3.Count > 0)
    {
      for (int index = 0; index < selectedItems3.Count; ++index)
      {
        if (selectedItems3.GetItemID(index).CategoryID.Equals(1))
        {
          if (selectedItems3.GetItemData(index, typeof (IImbaseTableRecordID)) is IImbaseTableRecordID itemData2 && dbTypedObjectId != null)
            selectedItems1.Add((IDBTypedObjectID) new CompDBTypedObjectID(dbTypedObjectId, (object) itemData2));
          else if (selectedItems3.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1)
            selectedItems1.Add(itemData1);
        }
      }
    }
    return selectedItems1;
  }

  /// <summary>Clone object</summary>
  /// <returns></returns>
  public override CVButtonBase Clone()
  {
    CVTechcardButton cvTechcardButton = new CVTechcardButton();
    cvTechcardButton.ApplyParams((CVButtonBase) this);
    return (CVButtonBase) cvTechcardButton;
  }

  /// <summary>Prepare object for creation</summary>
  /// <param name="targetObjectId"></param>
  /// <param name="sourceObjectId"></param>
  /// <param name="hash"></param>
  /// <param name="session"></param>
  /// <param name="imbServer"></param>
  /// <param name="commitCreation"></param>
  /// <param name="throwException"></param>
  /// <param name="errorString"></param>
  /// <returns></returns>
  private IDBObject PrepareId(
    IDBTypedObjectID targetObjectId,
    IDBTypedObjectID sourceObjectId,
    Dictionary<int, List<cvRelationInfo>> hash,
    IUserSession session,
    IImbaseServer imbServer,
    bool commitCreation,
    bool throwException,
    out string errorString)
  {
    errorString = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_88"), (object) sourceObjectId.Caption);
    if (session == null)
      return (IDBObject) null;
    int num = -1;
    ImbaseObjCreateInfo imbaseObjCreateInfo;
    if (this._imObjectInfoList.TryGetValue(sourceObjectId.ObjectID, out imbaseObjCreateInfo))
      num = imbaseObjCreateInfo.ObjectType;
    if (num.Equals(-1))
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.CreatedObjectAttID);
      errorString += string.Format(LocalizationHolder.rm.GetString(sc_19306.ssp_techcard_19308()), (object) attributeType.Name);
      if (throwException)
        throw new ArgumentException(errorString);
      return (IDBObject) null;
    }
    if (!hash.ContainsKey(num))
    {
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(targetObjectId.ObjectType);
      IMSObjectType objectType2 = MetaDataHelper.GetObjectType(num);
      errorString += string.Format(LocalizationHolder.rm.GetString(sc_19306.ssp_techcard_19309()), (object) objectType2.ObjectTypeName, (object) objectType1.ObjectTypeName);
      if (throwException)
        throw new ArgumentException(errorString);
      return (IDBObject) null;
    }
    if (!CompositionViewHelper.IsRelationTypesInVisibleRelations(new List<IDBTypedObjectID>((IEnumerable<IDBTypedObjectID>) new IDBTypedObjectID[1]
    {
      (IDBTypedObjectID) new DBTypedObjectID(num, -1L, 0L, sourceObjectId.Caption, 0L, 0L, 0L, string.Empty, 0L)
    }), targetObjectId.ObjectType, hash))
    {
      errorString += LocalizationHolder.rm.GetString(sc_19306.ssp_techcard_19310());
      return (IDBObject) null;
    }
    long objectID = !(sourceObjectId is ICompDBTypedObjectID compDbTypedObjectId) || !(compDbTypedObjectId.InfoObject is IImbaseTableRecordID infoObject) ? imbServer.CreateObject(session.SessionGUID, 0L, sourceObjectId.ObjectID, 0L, commitCreation, -1) : imbServer.CreateObject(session.SessionGUID, 0L, sourceObjectId.ObjectID, infoObject.Value, commitCreation, -1);
    return objectID == 0L ? (IDBObject) null : session.GetObject(objectID, false);
  }
}
