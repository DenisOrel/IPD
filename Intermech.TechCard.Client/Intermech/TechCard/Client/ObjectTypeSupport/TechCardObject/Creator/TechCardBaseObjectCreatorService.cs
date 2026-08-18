// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechCardBaseObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Extensions;
using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Imbase;
using Intermech.TechCard.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Summary description for TechCardBaseObjectCreatorService.
/// </summary>
internal class TechCardBaseObjectCreatorService : TechCardMultiObjectCreatorRiderCustomService
{
  /// <summary>Кеш режимов создания для типов объектов</summary>
  internal readonly Dictionary<int, TechObjectCreationMode> _creationModeCache;

  /// <summary>
  /// Проверка / валидация родительских объектов / связей на допустимость включения объектов
  /// </summary>
  /// <returns></returns>
  private bool ValidateRelatedObjects()
  {
    this.ValidateCreatorArgs();
    ITechCardObjectCreateAnalyzingService service = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    return service == null || service.AllowObjectCreation(this._creatorArgs, this._creatorExtraParams as TechObjectCreatorParams);
  }

  /// <summary>Создание объектов по прототипу / версий</summary>
  /// <returns></returns>
  private bool DoCreateObjects_ByProto()
  {
    this.ValidateCreatorArgs();
    if (this._creatorArgs.TemplateObjectIDs == null || this._creatorArgs.TemplateObjectIDs.Length == 0 || this._creatorArgs.ObjectTypeIDs == null || this._creatorArgs.ObjectTypeIDs.Length == 0)
      return false;
    bool objectsByProto = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        int num = Math.Min(this._creatorArgs.TemplateObjectIDs.Length, this._creatorArgs.ObjectTypeIDs.Length);
        for (int index = 0; index < num; ++index)
        {
          int objectTypeId = this._creatorArgs.ObjectTypeIDs[index];
          if (objectTypeId != -1)
          {
            long templateObjectId = this._creatorArgs.TemplateObjectIDs[index];
            switch (templateObjectId)
            {
              case -1:
              case 0:
                continue;
              default:
                IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
                if (objectCollection != null)
                {
                  IDBObject dbObject = this._creatorArgs.IsVersion ? objectCollection.CreateVersion(templateObjectId) : objectCollection.Create(templateObjectId);
                  if (dbObject != null)
                    this._objectCreatedInfoList.Add(new Intermech.Interfaces.Client.ObjectCreatedInfo(dbObject.ObjectID, dbObject.ObjectType, templateObjectId, this._creatorArgs.IsVersion));
                  objectsByProto = true;
                  continue;
                }
                continue;
            }
          }
        }
      }
      finally
      {
        this.AppendModificationLog(sessionKeeper.Session.GetModificationsHistoryList());
        sessionKeeper.Session.StopLogHistory();
      }
    }
    return objectsByProto;
  }

  /// <summary>Создание объектов по справочникам Imbase</summary>
  private bool DoCreateObjects_ByImbase()
  {
    this.ValidateCreatorArgs();
    IList<ImbaseObjectInfoItem> imbaseObjectInfoItemList = (IList<ImbaseObjectInfoItem>) new List<ImbaseObjectInfoItem>();
    ISelectedItems items = this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams ? creatorExtraParams.Items : (ISelectedItems) null;
    if (items == null && this._creatorArgs.RelatedObjectIDs != null && this._creatorArgs.RelatedObjectIDs.Length != 0)
      items = Intermech.Navigator.ContextMenu.ObjectExtensions.GetItems(this._creatorArgs.RelatedObjectIDs);
    System.IServiceProvider contextServices = creatorExtraParams != null ? creatorExtraParams.ContextServices ?? (System.IServiceProvider) ApplicationServices.Container : (System.IServiceProvider) ApplicationServices.Container;
    ITechCardImbaseObjectCreatorService service1 = ServiceUtils.GetService<ITechCardImbaseObjectCreatorService>((object) ApplicationServices.Container, true);
    if ((creatorExtraParams == null ? 0 : (creatorExtraParams.AsyncMode ? 1 : 0)) != 0)
    {
      IEnumerable<ImbaseObjectInfoItem> service2 = ServiceUtils.GetService<IEnumerable<ImbaseObjectInfoItem>>((object) ApplicationServices.Container, false);
      if (service2 == null || !service2.Any<ImbaseObjectInfoItem>())
      {
        service1.CreateObjects(this._creatorArgs.ObjectTypeIDs[0], items, contextServices);
        return false;
      }
      ApplicationServices.Container.RemoveService(typeof (IEnumerable<ImbaseObjectInfoItem>), true);
      imbaseObjectInfoItemList.AddRange<ImbaseObjectInfoItem>(service2);
    }
    else
      imbaseObjectInfoItemList.AddRange<ImbaseObjectInfoItem>((IEnumerable<ImbaseObjectInfoItem>) service1.SelectObjects(this._creatorArgs.ObjectTypeIDs[0], items, contextServices));
    if (imbaseObjectInfoItemList.Count == 0)
      return false;
    bool flag = this.NeedUniteObjects(imbaseObjectInfoItemList);
    if (imbaseObjectInfoItemList.Count == 0)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseServer service3 = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true);
      sessionKeeper.Session.StartLogHistory();
      try
      {
        if (flag)
        {
          ObjInfoItem imbaseUnitedObject = this.DoCreateImbaseUnitedObject(sessionKeeper.Session, service3, imbaseObjectInfoItemList);
          this._objectCreatedInfoList.Add(new Intermech.Interfaces.Client.ObjectCreatedInfo(imbaseUnitedObject.ObjectID, imbaseUnitedObject.ObjTypeID));
          return true;
        }
        foreach (ImbaseObjectInfoItem imbaseObjectInfoItem in (IEnumerable<ImbaseObjectInfoItem>) imbaseObjectInfoItemList)
        {
          ObjInfoItem imbaseObject = this.DoCreateImbaseObject(sessionKeeper.Session, service3, imbaseObjectInfoItem.ObjectInfo.ItemID, imbaseObjectInfoItem.RecordId);
          this._objectCreatedInfoList.Add(new Intermech.Interfaces.Client.ObjectCreatedInfo(imbaseObject.ObjectID, imbaseObject.ObjTypeID));
        }
      }
      finally
      {
        this.AppendModificationLog(sessionKeeper.Session.GetModificationsHistoryList());
        sessionKeeper.Session.StopLogHistory();
      }
    }
    return true;
  }

  /// <summary>Определение режима "Объединения объектов"</summary>
  /// <param name="imbaseSelectedItems"></param>
  /// <returns></returns>
  private bool NeedUniteObjects(IList<ImbaseObjectInfoItem> imbaseSelectedItems)
  {
    if (imbaseSelectedItems.Count < 2 || !MetaDataHelper.IsObjectTypeChildOf(this._creatorArgs.ObjectTypeIDs[0], TechCardConsts.ObjectTypes.PerehodID))
      return false;
    switch (MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_519"), LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
    {
      case DialogResult.Cancel:
        imbaseSelectedItems.Clear();
        return false;
      case DialogResult.Yes:
        return true;
      default:
        return false;
    }
  }

  /// <summary>Создание объекта Imbase</summary>
  /// <param name="session"></param>
  /// <param name="imServer"></param>
  /// <param name="baseObjId"></param>
  /// <param name="recordId"></param>
  /// <returns></returns>
  private ObjInfoItem DoCreateImbaseObject(
    IUserSession session,
    IImbaseServer imServer,
    long baseObjId,
    long recordId)
  {
    return new ObjInfoItem(0L, this._creatorArgs.ObjectTypeIDs[0])
    {
      ObjectID = imServer.CreateObject(session.SessionGUID, 0L, baseObjId, recordId, false, this._creatorArgs.ObjectTypeIDs[0])
    };
  }

  /// <summary>Создание объединенного объекта Imbase</summary>
  /// <param name="userSession"></param>
  /// <param name="imServer"></param>
  /// <param name="imbaseSelectedItems"></param>
  /// <returns></returns>
  private ObjInfoItem DoCreateImbaseUnitedObject(
    IUserSession userSession,
    IImbaseServer imServer,
    IList<ImbaseObjectInfoItem> imbaseSelectedItems)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (imServer == null)
      throw new ArgumentNullException(nameof (imServer));
    if (imbaseSelectedItems == null)
      throw new ArgumentNullException(nameof (imbaseSelectedItems));
    ImbaseObjectInfoItem imbaseObjectInfoItem = imbaseSelectedItems.Count != 0 ? imbaseSelectedItems.FirstOrDefault<ImbaseObjectInfoItem>() : throw new ArgumentException(nameof (imbaseSelectedItems));
    ObjInfoItem imbaseObject = this.DoCreateImbaseObject(userSession, imServer, imbaseObjectInfoItem.ObjectInfo.ItemID, imbaseObjectInfoItem.RecordId);
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) imbaseObject) || !MetaDataHelper.IsObjectTypeChildOf(this._creatorArgs.ObjectTypeIDs[0], TechCardConsts.ObjectTypes.PerehodID))
      return imbaseObject;
    StringBuilder perehTextBuilder = (StringBuilder) null;
    StringBuilder perehTextExtraBuilder = (StringBuilder) null;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this._creatorArgs.ObjectTypeIDs[0]);
    if (objectType.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectType.Guid, TechCardConsts.AttributeTypes.PerehTextAttrGuid) != null)
      perehTextBuilder = new StringBuilder();
    if (objectType.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectType.Guid, TechCardConsts.AttributeTypes.PerehTextExtraAttrGuid) != null)
      perehTextExtraBuilder = new StringBuilder();
    if (perehTextBuilder == null && perehTextExtraBuilder == null)
      return imbaseObject;
    IDBObjectCollection objectCollection = userSession.GetObjectCollection(this._creatorArgs.ObjectTypeIDs[0]);
    userSession.DBObjectsCacheStart();
    IDBObject dbObject1 = objectCollection.Create();
    try
    {
      Action<IDBObject> action = (Action<IDBObject>) (dbObject =>
      {
        IDBAttribute attributeByGuid1 = perehTextBuilder != null ? dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.PerehTextAttrGuid) : (IDBAttribute) null;
        if (attributeByGuid1 != null && !string.IsNullOrEmpty(attributeByGuid1.AsString))
        {
          if (perehTextBuilder.Length > 0)
            perehTextBuilder.Append(" ");
          perehTextBuilder.Append(attributeByGuid1.AsString);
        }
        IDBAttribute attributeByGuid2 = perehTextExtraBuilder != null ? dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.PerehTextExtraAttrGuid) : (IDBAttribute) null;
        if (attributeByGuid2 == null || string.IsNullOrEmpty(attributeByGuid2.AsString))
          return;
        if (perehTextExtraBuilder.Length > 0)
          perehTextExtraBuilder.Append(" ");
        perehTextExtraBuilder.Append(attributeByGuid2.AsString);
      });
      foreach (ImbaseObjectInfoItem imbaseSelectedItem in (IEnumerable<ImbaseObjectInfoItem>) imbaseSelectedItems)
      {
        imServer.FillObjectAttributes(userSession.SessionGUID, dbObject1.ObjectID, imbaseSelectedItem.ObjectInfo.ItemID, imbaseSelectedItem.RecordId, false);
        action(dbObject1);
      }
    }
    finally
    {
      userSession.DBObjectsCacheStop();
      dbObject1.Delete(0L);
    }
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (perehTextBuilder != null)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.PerehTextAttrGuid), (object) perehTextBuilder.ToString()));
    if (perehTextExtraBuilder != null)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.PerehTextExtraAttrGuid), (object) perehTextExtraBuilder.ToString()));
    if (attributeValuesList.Count != 0)
      userSession.GetObject(imbaseObject.ObjectID).SetAttributesValues(attributeValuesList.ToArray());
    return imbaseObject;
  }

  /// <summary>Конструктор</summary>
  public TechCardBaseObjectCreatorService()
  {
    this._creationModeCache = new Dictionary<int, TechObjectCreationMode>();
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
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    this._creatorArgs = new TechObjectCreatorArgs(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    if (isVersion || this._creatorExtraParams != null && this._creatorExtraParams.RawMode)
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeId);
    if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract || this.GetObjectCreationMode() == TechObjectCreationMode.Default)
      return false;
    this._checkoutObject = false;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId) => true;

  /// <summary>Получение режима создания объекта</summary>
  /// <returns></returns>
  protected override TechObjectCreationMode GetObjectCreationMode()
  {
    TechObjectCreationMode objectCreationMode = base.GetObjectCreationMode();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      List<int> objTypeIds = (List<int>) null;
      service?.GetCreationTypes(sessionKeeper.Session.SessionGUID, out objTypeIds);
      if (objTypeIds != null)
      {
        if (objTypeIds.Contains(this._creatorArgs.ObjectTypeIDs[0]))
          objectCreationMode = TechObjectCreationMode.Imbase;
      }
    }
    this._creationModeCache[this._creatorArgs.ObjectTypeIDs[0]] = objectCreationMode;
    return objectCreationMode;
  }

  /// <summary>Валидация параметров для создания объектов</summary>
  /// <returns></returns>
  protected override bool ObjectsValidateCreation()
  {
    return base.ObjectsValidateCreation() && this.ValidateRelatedObjects();
  }

  /// <summary>Создание объектов</summary>
  protected override void DoObjectsCreate()
  {
    this.ValidateCreatorArgs();
    if (this.DoCreateObjects_ByProto())
      return;
    switch (this._creationModeCache[this._creatorArgs.ObjectTypeIDs[0]])
    {
      case TechObjectCreationMode.Imbase:
        this.DoCreateObjects_ByImbase();
        break;
    }
  }

  /// <summary>
  /// Возвращает коллекцию страниц (наследованные от ObjectCreatorControl), которые будут присутствовать в мастера создания объекта,
  /// значение в коллекции обозначает отображать ли эту страницу в мастере
  /// </summary>
  public override IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      IDictionary<ObjectCreatePages, bool> visiblePages = base.VisiblePages;
      visiblePages[ObjectCreatePages.FileAttributes] = this._creatorArgs != null && this._creatorArgs.ObjectTypeIDs != null && ((IEnumerable<int>) this._creatorArgs.ObjectTypeIDs).Any<int>((Func<int, bool>) (objTypeId => MetaDataHelper.GetAttribute4ObjectTypeList(objTypeId).Any<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (imsAttrType => imsAttrType.FieldType == FieldTypes.ftFile))));
      visiblePages[ObjectCreatePages.Properties] = true;
      visiblePages[ObjectCreatePages.Relations] = true;
      visiblePages[ObjectCreatePages.Template] = true;
      return visiblePages;
    }
  }
}
