
// Type: Intermech.Navigator.Snapshots.SnapshotMasterModel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.Snapshots;

public class SnapshotMasterModel
{
  /// <summary>ID версии объекта, для которого создается итерация</summary>
  private readonly long objectID;
  /// <summary>ID типа объекта, для которого создается итерация</summary>
  private readonly int objectTypeID;
  /// <summary>
  /// Список ИД объектов(по модулю) в составе версии объекта, для которого создается итерация
  /// (верхний уровень)
  /// </summary>
  private List<long> absObjectComposition;
  /// <summary>ИД выделенного в данный момент узла</summary>
  private long absCurrentObjectID;
  /// <summary>ID последней восстановленной или сохраненной итерации</summary>
  private long activeSnapshotID;
  /// <summary>Итерация, отображаемая в данный момент на форме</summary>
  private SnapshotInfo displayedSnapshot;

  /// <summary>ID объекта, для которого создается итерация</summary>
  public long ID { get; private set; }

  /// <summary>ID версии объекта, с которой работаем</summary>
  public long ObjectID => this.objectID;

  /// <summary>ИД выделенного в данный момент на форме объекта.</summary>
  public long AbsCurrentObjectID
  {
    get => this.absCurrentObjectID;
    set => this.absCurrentObjectID = value;
  }

  /// <summary>ID текущего пользователя</summary>
  public long UserID { get; private set; }

  /// <summary>Список с информацией о итерациях версии объекта</summary>
  public List<SnapshotInfo> ObjectSnapshotsInfo { get; private set; }

  /// <summary>ID последней восстановленной или сохраненной итерации</summary>
  private long ActiveSnapshotID
  {
    get => this.activeSnapshotID;
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this.objectID).GetAttributeByGuid(new Guid("cadd94ce-306c-11d8-b4e9-00304f19f545")).Value = (object) value;
        this.activeSnapshotID = value;
      }
    }
  }

  /// <summary>Итерация, отображаемая в данный момент на форме</summary>
  public SnapshotInfo DisplayedSnapshot
  {
    get => this.displayedSnapshot;
    set
    {
      this.displayedSnapshot = value;
      this.DisplayedSnapshotComposition = this.GetSnapshotComposition(value);
    }
  }

  /// <summary>
  /// Состав отображаемой итерации
  /// Значения ИД взяты по модулю для возможности работы с взятыми на изменение объектами
  /// </summary>
  public List<long> DisplayedSnapshotComposition { get; private set; }

  /// <summary>
  /// Событие возникает в модели, если в базе произошли изменения
  /// </summary>
  public event EventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  private void RaiseOnChanged()
  {
    if (this.OnChanged == null)
      return;
    this.OnChanged((object) this, new EventArgs());
  }

  public SnapshotMasterModel(IDBTypedObjectID typedObject)
  {
    this.ID = typedObject.ID;
    this.objectID = typedObject.ObjectID;
    this.objectTypeID = typedObject.ObjectType;
    this.UserID = this.GetUserID();
    this.activeSnapshotID = this.GetActiveSnapshotIDFromAttribute();
    this.ObjectSnapshotsInfo = this.GetObjectSnapshotsInfo();
    this.DisplayedSnapshotComposition = new List<long>();
    this.DisplayedSnapshot = this.GetActiveSnapshotInfo();
    this.absObjectComposition = this.GetObjectComposition();
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectChanged));
  }

  /// <summary>
  /// Получает первый уровень состава объекта с версиями объектов по модулю.
  /// </summary>
  /// <returns></returns>
  private List<long> GetObjectComposition()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetRelationCollection(-1).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }), this.ObjectID);
      if (dataTable.Rows.Count > 0)
      {
        this.absObjectComposition = new List<long>(dataTable.Rows.Count);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long num = Math.Abs(Convert.ToInt64(dataTable.Rows[index][0]));
          if (!this.absObjectComposition.Contains(num))
            this.absObjectComposition.Add(num);
        }
      }
      return this.absObjectComposition;
    }
  }

  /// <summary>Событие на изменение объекта в дереве.</summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  private void OnObjectChanged(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs))
      return;
    foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
    {
      if (this.AbsCurrentObjectID == Math.Abs(objectId))
        this.RaiseOnChanged();
    }
  }

  /// <summary>Получает ИД текущего пользователя</summary>
  /// <returns>ИД текущего пользователя</returns>
  private long GetUserID()
  {
    return ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) is CurrentUserAndRole service ? service.UserID : 0L;
  }

  /// <summary>
  /// Составляет список с информацией об итерациях версии объекта
  /// </summary>
  private List<SnapshotInfo> GetObjectSnapshotsInfo()
  {
    List<SnapshotInfo> objectSnapshotsInfo = new List<SnapshotInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetSnapshotCollection().GetObjectVersionSnapshots(Math.Abs(this.objectID), "F_SNAPSHOT_ID").Rows)
        objectSnapshotsInfo.Add(new SnapshotInfo(Convert.ToInt64(row[0]), Convert.ToString(row[1])));
    }
    return objectSnapshotsInfo;
  }

  /// <summary>
  /// Получает инфу о последней использованной (сохраненной или восстановленной) для версии объекта итерации.
  /// </summary>
  /// <returns> Инфо о записанной в атрибуте итерации</returns>
  private SnapshotInfo GetActiveSnapshotInfo()
  {
    foreach (SnapshotInfo activeSnapshotInfo in this.ObjectSnapshotsInfo)
    {
      if (activeSnapshotInfo.ID == this.ActiveSnapshotID)
        return activeSnapshotInfo;
    }
    return (SnapshotInfo) null;
  }

  /// <summary>
  /// Получает ИД последней использованной итерации из атрибута.
  /// </summary>
  /// <returns>ИД последней использованной итерации.</returns>
  private long GetActiveSnapshotIDFromAttribute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.objectID, new Guid("cadd94ce-306c-11d8-b4e9-00304f19f545"));
      return objectAttributeByGuid == null ? 0L : objectAttributeByGuid.AsInteger;
    }
  }

  /// <summary>
  /// Получает состав итерации.
  /// ИД хранятся по модулю для того чтобы взятые на изменение объекты тоже выделялись в дереве объектов, если версия входит в состав итерации.
  /// </summary>
  /// <param name="chosedSnapshot">Выбранная итерация</param>
  /// <returns></returns>
  private List<long> GetSnapshotComposition(SnapshotInfo chosedSnapshot)
  {
    List<long> snapshotComposition = new List<long>();
    if (chosedSnapshot == null)
      return snapshotComposition;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objects in sessionKeeper.Session.GetSnapshot(chosedSnapshot.ID).GetObjectsList())
        snapshotComposition.Add(Math.Abs(objects));
    }
    return snapshotComposition;
  }

  /// <summary>Сохраняет отображаемую итерацию</summary>
  /// <param name="checkedObjects">Список выделенных в дереве объектов итерации</param>
  /// <param name="snapshotInfo">В какую итерацию сохраняем</param>
  /// <param name="snapshotName">Наименование итерации</param>
  public void SaveDisplayedSnapshot(
    List<long> checkedObjects,
    SnapshotInfo snapshotInfo,
    string snapshotName)
  {
    try
    {
      this.SaveCheckedOutObjects();
      if (snapshotInfo == null)
      {
        long newSnapshot = this.CreateNewSnapshot(checkedObjects, snapshotName);
        this.ActiveSnapshotID = newSnapshot;
        this.ObjectSnapshotsInfo.Add(new SnapshotInfo(newSnapshot, snapshotName));
      }
      else
      {
        this.SaveToSnapshot(checkedObjects, snapshotInfo.ID);
        this.ActiveSnapshotID = snapshotInfo.ID;
      }
      this.DisplayedSnapshot = this.GetActiveSnapshotInfo();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Сохраняет выделенные в дереве объекты в итерацию</summary>
  /// <param name="checkedObjects">Выделенные объекты дерева</param>
  /// <param name="snapshotID">ИД итерации, в которую происходит сохранение</param>
  private void SaveToSnapshot(List<long> checkedObjects, long snapshotID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      sessionKeeper.Session.GetSnapshot(snapshotID).SaveToSnapshot(checkedObjects, service.FiltrationServiceOwnerID);
    }
  }

  /// <summary>Создает новую итерацию для текущего объекта</summary>
  /// <param name="checkedObjects">Список ИД объектов, выделенных в дереве </param>
  /// <param name="snapshotName">Наименование итерации</param>
  public long CreateNewSnapshot(List<long> checkedObjects, string snapshotName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      IDBSnapshotCollection snapshotCollection = sessionKeeper.Session.GetSnapshotCollection();
      checkedObjects.Remove(this.ObjectID);
      long objectId = this.ObjectID;
      string snapshotName1 = snapshotName;
      string filtrationServiceOwnerId = service.FiltrationServiceOwnerID;
      long[] array = checkedObjects.ToArray();
      return snapshotCollection.Create(objectId, snapshotName1, filtrationServiceOwnerId, array);
    }
  }

  /// <summary>
  ///  Сохраняет изменения во всех файлах взятых на изменение объектов
  /// (если объекты взяты на изменение пользователем, создающим итерацию)
  /// </summary>
  private void SaveCheckedOutObjects()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObject(this.objectID, false).CheckoutBy == this.UserID)
      {
        ObjectCommand saveChangesCommand = ObjectCommandFactory.CreateSaveChangesCommand(true);
        saveChangesCommand.ObjectId = this.objectID;
        saveChangesCommand.UpdateUI = false;
        saveChangesCommand.Execute();
        RecentObjectsNode.MRUObjects.Add(this.objectID, ObjectAction.SaveChanges, DateTime.UtcNow);
      }
      IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1, service.FiltrationServiceOwnerID);
      relationCollection.LocalTypesMode = true;
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      List<long> objectIDs = new List<long>();
      this.SaveChildrenObjects(relationCollection, applicabilityCollection, this.objectID, this.objectTypeID, objectIDs);
    }
  }

  /// <summary>
  ///  Сохраняет изменения во всех файлах взятых на изменение объектов
  /// (если объекты взяты на изменение пользователем, создающим итерацию)
  /// </summary>
  /// <param name="relColl"></param>
  /// <param name="apps"></param>
  /// <param name="parentObjectID"></param>
  /// <param name="parentObjectType"></param>
  /// <param name="objectIDs">список id версий обработанных объектов (для правильной обработки циклического состава)</param>
  private void SaveChildrenObjects(
    IDBRelationCollection relColl,
    IDBRelationsApplicabilityCollection apps,
    long parentObjectID,
    int parentObjectType,
    List<long> objectIDs)
  {
    if (objectIDs.Contains(parentObjectID))
      return;
    objectIDs.Add(parentObjectID);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[4]
    {
      (object) -2,
      (object) -23,
      (object) -7,
      (object) -6
    });
    DataTable dataTable = relColl.ConsistFrom(paramSet, parentObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64_1 = Convert.ToInt64(dataTable.Rows[index][0]);
      long int64_2 = Convert.ToInt64(dataTable.Rows[index][3]);
      int int32 = Convert.ToInt32(dataTable.Rows[index][2]);
      IDBRelationsApplicability applicability = apps.GetApplicability(Convert.ToInt32(dataTable.Rows[index][1]), int32, parentObjectType);
      if (applicability != null && (applicability.Options & ApplicabilityOptions.CreateSnapshotChild) == ApplicabilityOptions.CreateSnapshotChild && int64_2 == this.UserID)
      {
        ObjectCommand saveChangesCommand = ObjectCommandFactory.CreateSaveChangesCommand(true);
        saveChangesCommand.ObjectId = int64_1;
        saveChangesCommand.UpdateUI = false;
        saveChangesCommand.Execute();
        RecentObjectsNode.MRUObjects.Add(int64_1, ObjectAction.SaveChanges, DateTime.UtcNow);
      }
      this.SaveChildrenObjects(relColl, apps, int64_1, int32, objectIDs);
    }
  }

  /// <summary>Отписка модели от событий.</summary>
  public void Unsubcribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectChanged));
  }
}
