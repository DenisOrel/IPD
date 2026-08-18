// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechCardMultiObjectCreatorRiderCustomService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.Remoting.Sponsors;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Base techcard class for implementation interface IObjectCreatorRiderCustomService
/// </summary>
internal abstract class TechCardMultiObjectCreatorRiderCustomService : 
  TechObjectCreatorRiderCustomService,
  ICreatorCheckoutHandler,
  ICreatorMultiObjectHandler
{
  /// <summary>Список "завершенных" объектов</summary>
  private readonly List<Intermech.Interfaces.Client.ObjectCreatedInfo> _committedObjInfoList = new List<Intermech.Interfaces.Client.ObjectCreatedInfo>();
  /// <summary>Лог изменений для отправки уведомлений</summary>
  private readonly List<CategoryValue> _modificationLog = new List<CategoryValue>();
  /// <summary>Режимы создания объектов</summary>
  private Dictionary<long, ImbaseObjCreateInfo> _objCreateInfoList;
  /// <summary>
  /// 
  /// </summary>
  protected bool _checkoutObject;
  /// <summary>Список созданных объектов</summary>
  /// <remarks>В порядке, указанными пользователем</remarks>
  protected readonly List<Intermech.Interfaces.Client.ObjectCreatedInfo> _objectCreatedInfoList = new List<Intermech.Interfaces.Client.ObjectCreatedInfo>();

  /// <summary>
  /// 
  /// </summary>
  protected void ValidateCreatorArgs()
  {
    if (this._creatorArgs == null)
      throw new ArgumentNullException("_creatorArgs");
  }

  /// <summary>Завершение создания объектов</summary>
  private void ObjectsCommit()
  {
    this.DoObjectsCommit();
    this.DoObjectsAfterCommit();
  }

  /// <summary>Завершение создания объектов</summary>
  private void DoObjectsCommit()
  {
    this.ValidateCreatorArgs();
    if (this._objectCreatedInfoList == null || this._objectCreatedInfoList.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        List<Tuple<Intermech.Interfaces.Client.ObjectCreatedInfo, IDBObject>> tupleList = new List<Tuple<Intermech.Interfaces.Client.ObjectCreatedInfo, IDBObject>>(this._objectCreatedInfoList.Count);
        for (int index = 0; index < this._objectCreatedInfoList.Count; ++index)
        {
          Intermech.Interfaces.Client.ObjectCreatedInfo objectCreatedInfo = this._objectCreatedInfoList[index];
          if (objectCreatedInfo.ObjectId != -1L && objectCreatedInfo.ObjectId != 0L)
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectCreatedInfo.ObjectId, false);
            if (objectActualCopy != null)
            {
              try
              {
                if (objectActualCopy.IsCreationMode)
                {
                  objectActualCopy.CommitCreation(false);
                  tupleList.Add(new Tuple<Intermech.Interfaces.Client.ObjectCreatedInfo, IDBObject>(objectCreatedInfo, objectActualCopy));
                  this._committedObjInfoList.Add(objectCreatedInfo);
                }
              }
              finally
              {
                objectCreatedInfo.ObjectId = objectActualCopy.ObjectID;
              }
            }
          }
        }
        IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
        if (service != null)
        {
          Dictionary<long, int> dictionary = this._objectCreatedInfoList.GroupBy<Intermech.Interfaces.Client.ObjectCreatedInfo, long>((Func<Intermech.Interfaces.Client.ObjectCreatedInfo, long>) (item => item.ObjectId)).ToDictionary<IGrouping<long, Intermech.Interfaces.Client.ObjectCreatedInfo>, long, int>((Func<IGrouping<long, Intermech.Interfaces.Client.ObjectCreatedInfo>, long>) (item => item.Key), (Func<IGrouping<long, Intermech.Interfaces.Client.ObjectCreatedInfo>, int>) (item => item.First<Intermech.Interfaces.Client.ObjectCreatedInfo>().ObjectTypeId));
          service.GetCreationMode((IDictionary<long, int>) dictionary, sessionKeeper.Session.SessionGUID, out this._objCreateInfoList);
        }
        foreach (Tuple<Intermech.Interfaces.Client.ObjectCreatedInfo, IDBObject> tuple in tupleList)
        {
          IDBObject dbObject = tuple.Item2;
          this.DoObjectAfterCommit(ref dbObject);
          tuple.Item1.ObjectId = dbObject.ObjectID;
        }
      }
      finally
      {
        List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
        if (modificationsHistoryList != null)
          this._modificationLog.AddRange((IEnumerable<CategoryValue>) modificationsHistoryList);
        sessionKeeper.Session.StopLogHistory();
      }
    }
  }

  /// <summary>Действие после завершения создания объекта</summary>
  /// <param name="dbObject"></param>
  private void DoObjectAfterCommit(ref IDBObject dbObject)
  {
    if (dbObject == null || dbObject.ObjectModifyMode != ObjectModifyModes.Checkout && dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion || dbObject.CheckoutBy != 0L || !((IDBSecurity) dbObject).CheckAccess(ActionType.Edit, true, false))
      return;
    ImbaseObjCreateInfo imbaseObjCreateInfo;
    imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmUnknown;
    if (this._objCreateInfoList == null || !this._objCreateInfoList.TryGetValue(dbObject.ObjectID, out imbaseObjCreateInfo) && !this._objCreateInfoList.TryGetValue(-dbObject.ObjectID, out imbaseObjCreateInfo))
      imbaseObjCreateInfo.CreateMode = ImbaseObjCreateMode.iocmUnknown;
    if (imbaseObjCreateInfo.CreateMode == ImbaseObjCreateMode.iocmUseExists)
      return;
    dbObject = dbObject.CheckOut(false);
  }

  /// <summary>Действия после завершением создания объектов</summary>
  private void DoObjectsAfterCommit()
  {
    this.ValidateCreatorArgs();
    if (this._creatorArgs.IsVersion || !this._objectCreatedInfoList.Any<Intermech.Interfaces.Client.ObjectCreatedInfo>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechNumerationService service = ServiceUtils.GetService<ITechNumerationService>((object) sessionKeeper.Session, true);
      ITechNumerationSession session = service?.CreateSession(sessionKeeper.Session.SessionGUID);
      using (new RemoteLock((object) session))
      {
        sessionKeeper.Session.StartLogHistory();
        try
        {
          List<long> longList = new List<long>();
          foreach (Intermech.Interfaces.Client.ObjectCreatedInfo objectCreatedInfo in this._objectCreatedInfoList)
          {
            if (objectCreatedInfo.RelationLinks != null && objectCreatedInfo.RelationLinks.Length != 0)
            {
              longList = ((IEnumerable<ObjectRelationLink>) objectCreatedInfo.RelationLinks).Select<ObjectRelationLink, long>((Func<ObjectRelationLink, long>) (item => item.LinkID)).ToList<long>();
              break;
            }
          }
          if (session == null || longList.Count == 0)
            return;
          session.PartObjToSuppress.RemoveItems((IEnumerable<long>) this._objectCreatedInfoList.Select<Intermech.Interfaces.Client.ObjectCreatedInfo, long>((Func<Intermech.Interfaces.Client.ObjectCreatedInfo, long>) (item => item.ObjectId)).ToList<long>());
          foreach (long relationId in longList)
            session.NumerateObject(relationId, TechNumerationObjectModes.CurrentObj, sessionKeeper.Session.SessionGUID);
        }
        finally
        {
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          if (modificationsHistoryList != null)
            this._modificationLog.AddRange((IEnumerable<CategoryValue>) modificationsHistoryList);
          sessionKeeper.Session.StopLogHistory();
          if (session != null)
          {
            ITechNumerationLog numerationLog = session.GetNumerationLog();
            if (numerationLog != null)
            {
              foreach (long aCategoryID in (IEnumerable<long>) numerationLog.ObjectsLog)
                this._modificationLog.Add(new CategoryValue(1, aCategoryID, ActionType.EditProperties));
              foreach (long aCategoryID in (IEnumerable<long>) numerationLog.RelationsLog)
                this._modificationLog.Add(new CategoryValue(5, aCategoryID, ActionType.EditProperties));
            }
            service.DisposeSession(sessionKeeper.Session.SessionGUID);
          }
        }
      }
    }
  }

  /// <summary>Создание связей</summary>
  private void RelationsCreate() => this.DoRelationsCreate();

  /// <summary>Создание связей</summary>
  private void DoRelationsCreate()
  {
    this.ValidateCreatorArgs();
    if (this._objectCreatedInfoList == null || this._objectCreatedInfoList.Count == 0 || this._creatorArgs.RelatedObjectIDs == null || this._creatorArgs.RelatedObjectIDs.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechNumerationService service1 = ServiceUtils.GetService<ITechNumerationService>((object) sessionKeeper.Session, true);
      ITechNumerationSession session1 = service1?.CreateSession(sessionKeeper.Session.SessionGUID);
      using (new RemoteLock((object) session1))
      {
        sessionKeeper.Session.StartLogHistory();
        try
        {
          session1?.PartObjToSuppress.AddItems((IEnumerable<long>) this._objectCreatedInfoList.Select<Intermech.Interfaces.Client.ObjectCreatedInfo, long>((Func<Intermech.Interfaces.Client.ObjectCreatedInfo, long>) (item => item.ObjectId)).ToList<long>());
          List<IDBRelation> source = new List<IDBRelation>(this._objectCreatedInfoList.Count);
          for (int index1 = 0; index1 < this._objectCreatedInfoList.Count; ++index1)
          {
            Intermech.Interfaces.Client.ObjectCreatedInfo objectCreatedInfo = this._objectCreatedInfoList[index1];
            if (objectCreatedInfo.ObjectId != -1L && objectCreatedInfo.ObjectId != 0L)
            {
              int num = Math.Min(this._creatorArgs.RelationTypeIDs.Length, this._creatorArgs.RelatedObjectIDs.Length);
              List<IDBRelation> collection = new List<IDBRelation>();
              for (int index2 = 0; index2 < num; ++index2)
              {
                IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this._creatorArgs.RelationTypeIDs[index2]);
                if (relationCollection != null)
                {
                  IDBRelation dbRelation = relationCollection.Create(this._creatorArgs.RelatedObjectIDs[index2], objectCreatedInfo.ObjectId, this._creatorArgs.StartDate);
                  if (dbRelation != null)
                    collection.Add(dbRelation);
                }
              }
              if (collection.Count != 0)
              {
                List<ObjectRelationLink> objectRelationLinkList = new List<ObjectRelationLink>();
                if (objectCreatedInfo.RelationLinks != null)
                  objectRelationLinkList.AddRange((IEnumerable<ObjectRelationLink>) objectCreatedInfo.RelationLinks);
                foreach (IDBRelation dbRelation in collection)
                  objectRelationLinkList.Add(new ObjectRelationLink(dbRelation.ProjID, dbRelation.RelationType, dbRelation.RelationID));
                objectCreatedInfo.RelationLinks = objectRelationLinkList.ToArray();
                source.AddRange((IEnumerable<IDBRelation>) collection);
              }
            }
          }
          if (source.Count <= 0)
            return;
          ICompositionsAutomaticSortingService service2 = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) sessionKeeper.Session, true);
          ICompositionsAutomaticSortingSession session2 = service2.CreateSession((object) sessionKeeper.Session.SessionGUID);
          try
          {
            session2.PrefetchObjectComposition((IEnumerable<long>) this._creatorArgs.RelatedObjectIDs, (object) sessionKeeper.Session.SessionGUID);
            IDBRelationID itemData = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || creatorExtraParams.Items == null ? (IDBRelationID) null : creatorExtraParams.Items.GetItemData<IDBRelationID>(0, false);
            session2.ProceedRelation((IEnumerable<long>) source.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.RelationID)).ToArray<long>(), creatorExtraParams != null ? creatorExtraParams.RelationMode : CompositionTargetMode.Add, itemData != null ? itemData.Value : 0L, (object) sessionKeeper.Session.SessionGUID);
          }
          finally
          {
            service2.DisposeSession((object) sessionKeeper.Session.SessionGUID);
          }
        }
        finally
        {
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          if (modificationsHistoryList != null)
            this._modificationLog.AddRange((IEnumerable<CategoryValue>) modificationsHistoryList);
          sessionKeeper.Session.StopLogHistory();
          if (session1 != null)
          {
            ITechNumerationLog numerationLog = session1.GetNumerationLog();
            if (numerationLog != null)
            {
              foreach (long aCategoryID in (IEnumerable<long>) numerationLog.ObjectsLog)
                this._modificationLog.Add(new CategoryValue(1, aCategoryID, ActionType.EditProperties));
              foreach (long aCategoryID in (IEnumerable<long>) numerationLog.RelationsLog)
                this._modificationLog.Add(new CategoryValue(5, aCategoryID, ActionType.EditProperties));
            }
            service1.DisposeSession(sessionKeeper.Session.SessionGUID);
          }
        }
      }
    }
  }

  /// <summary>Получение режима создания объекта (объектов)</summary>
  /// <returns></returns>
  protected virtual TechObjectCreationMode GetObjectCreationMode()
  {
    this.ValidateCreatorArgs();
    return TechObjectCreationMode.Default;
  }

  /// <summary>Проверка параметров для создания объектов</summary>
  /// <returns></returns>
  protected virtual bool ObjectsValidateCreation()
  {
    this.ValidateCreatorArgs();
    if (this._creatorArgs.ObjectTypeIDs == null || this._creatorArgs.ObjectTypeIDs.Length == 0)
      return false;
    foreach (int objectTypeId in this._creatorArgs.ObjectTypeIDs)
    {
      if (objectTypeId == -1)
        return false;
    }
    return true;
  }

  /// <summary>Создание объектов</summary>
  protected virtual void ObjectsCreate() => this.DoObjectsCreate();

  /// <summary>Создание объектов</summary>
  /// <returns></returns>
  protected abstract void DoObjectsCreate();

  /// <summary>Пополнение лога</summary>
  /// <param name="log"></param>
  protected void AppendModificationLog(List<CategoryValue> log)
  {
    if (log == null)
      return;
    this._modificationLog.AddRange((IEnumerable<CategoryValue>) log);
  }

  /// <summary>Рассылка уведомлений в навигатор</summary>
  private void DoFireNotification()
  {
    if (this._modificationLog == null)
      return;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    TechcardClientControlsUtils.FireNotificationEvents(service, (IEnumerable<CategoryValue>) this._modificationLog, (object) null);
  }

  /// <summary>
  /// Вызывать собственный диалог ?
  /// Если здесь вернуть true, то вызовется диалог создания объектов реализованный в функции CreateObjectDialog подписчика
  /// на конкретный тип объектов, если же вернуть false, то вызовется стандартный диалог создания объекта
  /// с изменениями, реализованными подписчиком (см. функции интерфейса)
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateObjectId">Идентификатор объекта-прототипа</param>
  /// <param name="relationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
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
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    int num2 = isVersion ? 0 : (this._creatorExtraParams == null ? 1 : (!this._creatorExtraParams.RawMode ? 1 : 0));
    if (num2 == 0)
      return num2 != 0;
    this._checkoutObject = false;
    return num2 != 0;
  }

  /// <summary>Метод вызывается по нажатию на кнопку готово</summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  public override bool OnCommitAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    if (this._creatorArgs?.ObjectTypeIDs == null || this._creatorArgs.ObjectTypeIDs.Length == 0)
      return true;
    IDBObject objectActualCopy = session.GetObjectActualCopy(newObjectId, false);
    if (objectActualCopy == null || objectActualCopy.IsCreationMode)
      return true;
    Intermech.Interfaces.Client.ObjectCreatedInfo objectCreatedInfo = new Intermech.Interfaces.Client.ObjectCreatedInfo(objectActualCopy.ObjectID, objectActualCopy.ObjectType, this._creatorArgs.TemplateObjectIDs[0], this._creatorArgs.IsVersion);
    this._objectCreatedInfoList.Add(objectCreatedInfo);
    this._committedObjInfoList.Add(objectCreatedInfo);
    this.DoObjectAfterCommit(ref objectActualCopy);
    objectCreatedInfo.ObjectId = objectActualCopy.ObjectID;
    this.DoObjectsAfterCommit();
    nea.AddRange(TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) this._modificationLog));
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
  public override long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    long objectDialog = 0;
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
    if (!this.ObjectsValidateCreation())
      return objectDialog;
    this.ObjectsCreate();
    this.RelationsCreate();
    this.ObjectsCommit();
    this.DoFireNotification();
    return this._objectCreatedInfoList.Count == 0 ? 0L : this._objectCreatedInfoList[0].ObjectId;
  }

  /// <summary>
  /// Флаг управления взятием на редактирование объекта после его создания
  /// </summary>
  public bool CheckoutObject
  {
    get => this._checkoutObject;
    set => this._checkoutObject = value;
  }

  /// <summary>Перечень созданных объектов</summary>
  public IEnumerable<Intermech.Interfaces.Client.ObjectCreatedInfo> ObjectCreatedInfo
  {
    get => (IEnumerable<Intermech.Interfaces.Client.ObjectCreatedInfo>) this._objectCreatedInfoList;
  }

  /// <summary>
  /// Делегат для события, возникающего при успешном включении в какой-либо состав создаваемого объекта в стандартном создателе объектов
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args">Параметры</param>
  public static void DoEntersInCreatedEvent(object sender, AfterEntersInCreatedEventArgs args)
  {
    if (sender == null || args == null)
      return;
    ObjInfoItem objInfoItem = new ObjInfoItem(args.ObjectID, args.ObjectType);
    if (!TechCardConsts.RelTypes.TechAllRelationTypes.Contains<int>(args.RelationType) && !TechCardConsts.Utils.IsTechcardObjectType((object) objInfoItem.ObjTypeID))
      return;
    RelObjInfoItem relObjInfoItem = new RelObjInfoItem(new RelInfoItem(args.PrjLinkID, args.RelationType), new ObjInfoItem(args.ProjectID), new ObjInfoItem(args.ObjectID, args.ObjectType));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session1 = sessionKeeper.Session;
      ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session1, false);
      if (service == null)
        return;
      ICompositionsAutomaticSortingSession session2 = service.CreateSession((object) session1.SessionGUID);
      try
      {
        session2.ProceedRelation(new CompositionSortingProjInfo(relObjInfoItem.RelationID, relObjInfoItem.RelTypeID, relObjInfoItem.ProjInfo.ObjectID, relObjInfoItem.ProjInfo.ObjTypeID, relObjInfoItem.PartInfo.ObjTypeID, 0L), (object) session1.SessionGUID);
      }
      finally
      {
        service.DisposeSession((object) session1.SessionGUID);
      }
    }
  }

  /// <summary>Делегат события после завершения создания объектов</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  public static void DoObjectCreatorCompletedEvent(object sender, AfterObjectCreatedEventArgs args)
  {
  }

  /// <summary>Делегат на создание объекта-заготовки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public static void DoObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
  }
}
