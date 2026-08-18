
// Type: Intermech.Client.Core.ObjectCreator.CreatedObjectItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace Intermech.Client.Core.ObjectCreator;

public class CreatedObjectItem
{
  /// <summary>
  /// Локальная константа для заголовка типа объекта, если он не определен.
  /// </summary>
  private static readonly string _typeNotDefined = LocalizationHolder.rm.GetString("Client.Core_881");
  private int _objTypeID = -1;
  private long[] _relatedObjIDs;
  private bool _isRelationsCreated;
  /// <summary>Файловые атрибуты создаваемого объекта.</summary>
  public FileAttributesClass FileAttrs;
  /// <summary>
  /// Классификатор, по которому надо провести классификацию создаваемого объекта.
  /// </summary>
  public List<long> ClassifiersToAdd = new List<long>();
  /// <summary>
  /// Идентификатор типа связи, которыми надо связать объект с объектами из realatedObectIDs.
  /// </summary>
  public int RelationTypeID = -1;
  /// <summary>Массив линков "тип связи - объект".</summary>
  public List<ObjectRelationLink> ObjectRelationArray = new List<ObjectRelationLink>();
  /// <summary>Дата начала действия связей.</summary>
  public DateTime CreateRelationDate = DateTime.Now;

  internal Intermech.Client.Core.ObjectCreator.ObjectCreator ObjCreator { get; }

  /// <summary>Событие - изменение типа объекта.</summary>
  public event CreatedObjectItem.OnObjectTypeChanged ObjectTypeChanged;

  /// <summary>Событие - коммит.</summary>
  public event CreatedObjectItem.AfterCommitCreation AfterCommitCreationEvent;

  /// <summary>Событие - коммит.</summary>
  public event CreatedObjectItem.BeforeCommitCreation BeforeCommitCreationEvent;

  /// <summary>Событие - коммит.</summary>
  public event CreatedObjectItem.OnCancelCreation OnCancelCreationEvent;

  /// <summary>
  /// Признак, указывающий что создается не объект, а новая версия.
  /// </summary>
  public bool IsVersion { get; private set; }

  /// <summary>Идентификатор объекта-заготовки.</summary>
  public long ObjectID { get; set; } = -1;

  /// <summary>Заголовок выбранного типа объекта.</summary>
  public string ObjectTypeCaption { get; private set; } = CreatedObjectItem._typeNotDefined;

  /// <summary>Идентификатор типа объекта.</summary>
  public int ObjectTypeID
  {
    get => this._objTypeID;
    set
    {
      if (value == this._objTypeID)
        return;
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      this.ObjectTypeCaption = CreatedObjectItem._typeNotDefined;
      int objectTypeID = value;
      IDBObjectTypeInfo objectType = service.GetObjectType(objectTypeID);
      if (objectType == null)
        return;
      this._objTypeID = objectType.ObjectType;
      this.ObjectTypeCaption = objectType.ObjectInstanceName;
      if (objectType.Icon != null && objectType.Icon.Length != 0)
      {
        using (MemoryStream memoryStream = new MemoryStream(objectType.Icon))
          this.ObjectTypeImage = (Image) new Icon((Stream) memoryStream, 64 /*0x40*/, 64 /*0x40*/).ToBitmap();
      }
      else
        this.ObjectTypeImage = (Image) null;
      CreatedObjectItem.OnObjectTypeChanged objectTypeChanged = this.ObjectTypeChanged;
      if (objectTypeChanged == null)
        return;
      objectTypeChanged();
    }
  }

  /// <summary>
  /// Поле содеращее иконку, соответствующую типу создаваемого объекта.
  /// </summary>
  public Image ObjectTypeImage { get; private set; }

  /// <summary>
  /// Идентификатор объекта-прототипа (или версии) по которой создается объект.
  /// Если равно 0, то объект создается не по прототипу (и не по версии).
  /// </summary>
  public long PrototypeID { get; set; } = -1;

  /// <summary>Конструктор.</summary>
  /// <param name="objectCreator"></param>
  public CreatedObjectItem(Intermech.Client.Core.ObjectCreator.ObjectCreator objectCreator)
  {
    this.ObjCreator = objectCreator;
    this.FileAttrs = new FileAttributesClass(this);
  }

  /// <summary>Удаление заготовки (отмена создания).</summary>
  internal void Cancel()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions service1 = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, true);
      List<long> objectIDs = new List<long>()
      {
        this.ObjectID
      };
      if (this._relatedObjIDs != null && this._relatedObjIDs.Length != 0)
        objectIDs.AddRange((IEnumerable<long>) this._relatedObjIDs);
      List<NotificationEventArgs> nea = new List<NotificationEventArgs>();
      if (this.OnCancelCreationEvent != null)
      {
        int num = this.OnCancelCreationEvent(sessionKeeper.Session, this.ObjectID, nea) ? 1 : 0;
      }
      long[] array = objectIDs.ToArray();
      service1.RollBackCreationLog(array);
      nea.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs));
      try
      {
        INotificationService service2 = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
        if (nea.Count <= 0 || service2 == null)
          return;
        foreach (NotificationEventArgs e in nea)
          service2.FireEvent((object) null, e);
      }
      catch
      {
      }
    }
  }

  /// <summary>Завершение создания объекта.</summary>
  /// <returns></returns>
  internal bool Commit(out List<NotificationEventArgs> nea)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      nea = new List<NotificationEventArgs>();
      IDBObject newObject = this.ObjectID != -1L ? sessionKeeper.Session.GetObject(this.ObjectID) : (IDBObject) null;
      if (newObject != null)
      {
        long objectId = this.ObjectID;
        try
        {
          this.ObjCreator.FireBeforeCommitCreationEvent(newObject, this.PrototypeID);
          CreatedObjectItem.BeforeCommitCreation commitCreationEvent1 = this.BeforeCommitCreationEvent;
          if (commitCreationEvent1 != null)
          {
            int num1 = commitCreationEvent1(sessionKeeper.Session, newObject) ? 1 : 0;
          }
          newObject.CommitCreation(false, UISettings.AutoCheckOutNewObjects);
          this.ObjectID = newObject.ObjectID;
          if (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService && this.ClassifiersToAdd.Count > 0)
          {
            foreach (long num2 in this.ClassifiersToAdd)
            {
              // ISSUE: variable of a boxed type
              __Boxed<Guid> sessionGuid = (ValueType) sessionKeeper.Session.SessionGUID;
              long selectionID = num2;
              long[] objectIDs = new long[1]
              {
                this.ObjectID
              };
              customService.IncludeObjects((object) sessionGuid, selectionID, objectIDs);
            }
          }
          CreatedObjectItem.AfterCommitCreation commitCreationEvent2 = this.AfterCommitCreationEvent;
          if (commitCreationEvent2 != null)
          {
            int num3 = commitCreationEvent2(sessionKeeper.Session, this.ObjectID, nea) ? 1 : 0;
          }
          if (this._relatedObjIDs != null && this._relatedObjIDs.Length != 0)
          {
            List<long> longList = new List<long>();
            for (int index = 0; index < this._relatedObjIDs.Length; ++index)
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(this._relatedObjIDs[index], false);
              if (dbObject != null && dbObject.ObjectVerType == -1)
              {
                dbObject.CommitCreation(false);
                longList.Add(dbObject.ObjectID);
              }
            }
            if (longList.Count > 0)
              nea.Add(longList.Count == 1 ? (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", longList[0]) : (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) longList.ToArray()));
          }
          flag = true;
        }
        catch
        {
          flag = false;
          this.ObjectID = objectId;
          throw;
        }
      }
    }
    return flag;
  }

  /// <summary>Ссоздание заготовки объекта.</summary>
  internal void Create() => this.Create(-1L);

  /// <summary>Создание заготовки объекта.</summary>
  /// <param name="prototypeID">Идентификатор прототипа объекта</param>
  internal void Create(long prototypeID) => this.Create(prototypeID, false);

  /// <summary>Ссоздание заготовки объекта.</summary>
  /// <param name="prototypeID">Идентификатор прототипа объекта</param>
  /// <param name="isVersion">Нужно ли создавать версию объекта</param>
  internal void Create(long prototypeID, bool isVersion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.IsVersion = isVersion;
      if (prototypeID != 0L && prototypeID != -1L)
        this.PrototypeID = prototypeID;
      else if (this.ObjCreator != null)
        this.PrototypeID = this.ObjCreator.FireObjectCreatorBeforeDraftCreateEvent(sessionKeeper.Session, this.ObjectTypeID, prototypeID);
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this.ObjectTypeID);
      long[] sourceArray = this.PrototypeID == -1L || this.PrototypeID == 0L ? objectCollection.CreateEx() : (isVersion ? objectCollection.CreateVersionEx(this.PrototypeID) : objectCollection.CreateEx(this.PrototypeID));
      if (sourceArray == null || sourceArray.Length == 0)
        return;
      this.ObjectID = sourceArray[0];
      if (sourceArray.Length > 1)
      {
        this._relatedObjIDs = new long[sourceArray.Length - 1];
        Array.Copy((Array) sourceArray, 1, (Array) this._relatedObjIDs, 0, sourceArray.Length - 1);
      }
      this.CheckVersion(sessionKeeper.Session);
      this.FileAttrs.Initialize(sessionKeeper.Session);
      this.ObjCreator?.FireObjectCreatorDraftCreatedEvent(this._objTypeID, this.ObjectID, prototypeID);
    }
  }

  private void CheckVersion(IUserSession session)
  {
    if (!this.IsVersion || this.PrototypeID == 0L)
      return;
    IDBObject dBObject = session.GetObject(this.ObjectID);
    if (dBObject.ObjectType == this.ObjectTypeID)
      return;
    dBObject.ObjectType = this.ObjectTypeID;
    int fileAttributeId = session.IdentHelper.FileAttributeID;
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this.ObjectTypeID);
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (!(objectType.Attributes.GetAttributeByID(fileAttributeId) is IDBAttributeType4Object))
      return;
    IDBAttribute attributeById = dBObject.GetAttributeByID(fileAttributeId);
    if (attributeById == null)
      return;
    if (!attributeById.IsNull)
      attributeById.ClearValues();
    if (!SetFileAttrPrototype.Execute(attributeById, session, dBObject))
      throw new Exception($"Для создания объекта другого типа необходимо настроить объект-прототип для типа \"{MetaDataHelper.GetObjectTypeName(this.ObjectTypeID)}\"");
  }

  /// <summary>
  /// Включение в состав указанных объектов созданной заготовки.
  /// </summary>
  public void EntersInCreate()
  {
    if (this._isRelationsCreated)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ObjectRelationLink objectRelation in this.ObjectRelationArray)
      {
        IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(objectRelation.RelationTypeID).Create(objectRelation.ObjectID, this.ObjectID, this.CreateRelationDate);
        if (objectRelation.Attributes != null)
        {
          foreach (KeyValuePair<int, object> attribute in objectRelation.Attributes)
            (dbRelation.GetAttributeByID(attribute.Key) ?? dbRelation.Attributes.AddAttribute(attribute.Key, false)).Value = attribute.Value;
        }
        this.ObjCreator.FireEntersInCreatedEvent(new AfterEntersInCreatedEventArgs(this.ObjectTypeID, this.ObjectID, objectRelation.ObjectID, objectRelation.RelationTypeID, dbRelation.RelationID));
        objectRelation.LinkID = dbRelation.RelationID;
      }
    }
    this._isRelationsCreated = true;
  }

  /// <summary>Длегат для события - изменение типа объекта.</summary>
  public delegate void OnObjectTypeChanged();

  /// <summary>Длегат для события - икоммит.</summary>
  /// <param name="session"></param>
  /// <param name="newObjectID"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  public delegate bool AfterCommitCreation(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea);

  /// <summary>Длегат для события - икоммит.</summary>
  public delegate bool BeforeCommitCreation(IUserSession session, IDBObject newObject);

  /// <summary>Длегат для события - икоммит.</summary>
  public delegate bool OnCancelCreation(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea);
}
