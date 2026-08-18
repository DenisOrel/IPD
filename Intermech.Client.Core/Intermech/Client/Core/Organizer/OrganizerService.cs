
// Type: Intermech.Client.Core.Organizer.OrganizerService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Сервис необходим для узлов, которые добавляются в состав узла "Органайзер".
/// </summary>
public class OrganizerService : IOrganizerService
{
  internal const string OBJECT_ID = "OBJECT_ID";
  internal const string OBJECT_TYPE = "OBJECT_TYPE";
  internal const string CAPTION = "CAPTION";
  internal const string OBJECT_TEXT = "OBJECT_TEXT";
  internal const string START_DATE = "START_DATE";
  internal const string FINISH_DATE = "FINISH_DATE";
  internal const string REMINDER_DATE = "REMINDER_DATE";
  private int _attrIDStartDate = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
  private int _attrIDFinishDate = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
  private int _attrIDReminderDate = MetaDataHelper.GetAttributeTypeID("cad015d4-306c-11d8-b4e9-00304f19f545");
  private int _attrIDReminder = MetaDataHelper.GetAttributeTypeID("cad015d5-306c-11d8-b4e9-00304f19f545");
  private int _attrIDRepetition = MetaDataHelper.GetAttributeTypeID("cad015d3-306c-11d8-b4e9-00304f19f545");
  private int _attrIDTaskText = MetaDataHelper.GetAttributeID((object) "cad015d2-306c-11d8-b4e9-00304f19f545");
  private System.IServiceProvider _provider;
  private Dictionary<int, OrganizerService.NodeInfo> _nodesInfo = new Dictionary<int, OrganizerService.NodeInfo>(1);
  private Dictionary<int, OrganizerService.ReminderInfo> _dictReminders = new Dictionary<int, OrganizerService.ReminderInfo>(1);
  private System.Threading.Timer _timerForRequestToServer;
  private System.Windows.Forms.Timer _timerForLocalCollection;
  private int _objTypeIDOrganizerTask = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
  private OrganizerReminderForm _reminderForm;
  private INotificationService _notificationService;
  private NotificationEventHandler _notificationHandler;
  private int _intervalForRequestToServer = 60000;
  private DateTime _nextDate = DateTime.Now;
  private int _timeBeforeReminder = 30;

  /// <summary>
  /// Коллекция всех созданных дескрипторов для подузлов узла "Органайзер".
  /// </summary>
  public DescriptorCollection Descriptors
  {
    get
    {
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (OrganizerService.NodeInfo nodeInfo in this._nodesInfo.Values)
        descriptors.Add((IDescriptor) nodeInfo.descriptor);
      return descriptors;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public Dictionary<int, string> NodesCaption
  {
    get
    {
      Dictionary<int, string> nodesCaption = (Dictionary<int, string>) null;
      if (this._nodesInfo != null)
      {
        nodesCaption = new Dictionary<int, string>(this._nodesInfo.Count);
        foreach (KeyValuePair<int, OrganizerService.NodeInfo> keyValuePair in this._nodesInfo)
          nodesCaption.Add(keyValuePair.Key, keyValuePair.Value.descriptor.Caption);
      }
      return nodesCaption;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="provider"></param>
  public OrganizerService(System.IServiceProvider provider)
  {
    this._provider = provider;
    if (ServicesManager.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service)
    {
      this._timerForRequestToServer = new System.Threading.Timer(new TimerCallback(this.On_timerForRequestToServer_Tick), (object) service.MainForm, -1, -1);
      this._timerForLocalCollection = new System.Windows.Forms.Timer();
      this._timerForLocalCollection.Interval = 60000;
      this._timerForLocalCollection.Tick += new EventHandler(this.On_timerForLocalCollection_Tick);
    }
    ((INotificationService) ApplicationServices.Container.GetService(typeof (INotificationService))).Subscribe("ApplicationClosed", new NotificationEventHandler(this.OnApplicationClosed));
  }

  /// <summary>
  /// Приложение закрывается, таймеры останавливаются, чтобы не продолжать пересчитывать событие обработки списка уведомлений органайзера
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnApplicationClosed(object sender, NotificationEventArgs e)
  {
    this._timerForRequestToServer.Change(-1, -1);
    this._timerForLocalCollection.Enabled = false;
  }

  /// <summary>
  /// Перенос напоминания на указанное время. При нажатии кнопки "Отложить".
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="dict">Информация о выбранных задачах.
  /// int - идентификатор типа объектов
  /// Int64 - идентификатор выбранного объекта
  /// DateTime - время, на которое необходимо отложить напоминание</param>
  private void On_reminderForm_DelayReminderForObjects(
    object sender,
    Dictionary<int, Dictionary<long, DateTime>> dict)
  {
    if (dict == null || dict.Count <= 0)
      return;
    foreach (KeyValuePair<int, Dictionary<long, DateTime>> keyValuePair in dict)
    {
      if (keyValuePair.Key == this._objTypeIDOrganizerTask)
        this.DelayReminderForOrganizerTasks(keyValuePair.Value);
    }
  }

  /// <summary>Закрытие формы напоминания.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_reminderForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this._reminderForm.StopReminderForObjects -= new StopReminderForObjectsHandler(this.On_reminderForm_StopReminderForObjects);
    this._reminderForm.DelayReminderForObjects -= new DelayReminderForObjectsHandler(this.On_reminderForm_DelayReminderForObjects);
    this._reminderForm.FormClosed -= new FormClosedEventHandler(this.On_reminderForm_FormClosed);
    this._reminderForm = (OrganizerReminderForm) null;
  }

  /// <summary>Прекращение напоминаний для выбранных задач.</summary>
  /// <remarks>Если у задачи настроена периодичность повторения, то дата напоминания переносится на указанный период.</remarks>
  /// <param name="sender"></param>
  /// <param name="dict">Информация о выбранных задачах.
  /// int - идентификатор типа объектов
  /// Int64 - идентификатор выбранного объекта</param>
  private void On_reminderForm_StopReminderForObjects(
    object sender,
    Dictionary<int, List<long>> dict)
  {
    if (dict == null || dict.Count <= 0)
      return;
    foreach (KeyValuePair<int, List<long>> keyValuePair in dict)
    {
      if (keyValuePair.Key == this._objTypeIDOrganizerTask)
        this.StopReminderForOrganizerTasks(keyValuePair.Value);
    }
  }

  /// <summary>
  /// Срабатывание локального таймера по истечении заданного интервала времени.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_timerForLocalCollection_Tick(object sender, EventArgs e)
  {
    if (this._dictReminders.Count == 0)
      return;
    Dictionary<int, DataRow[]> objsInfo = new Dictionary<int, DataRow[]>(1);
    foreach (KeyValuePair<int, OrganizerService.ReminderInfo> dictReminder in this._dictReminders)
    {
      DataTable dtObjectsInfo = dictReminder.Value._dtObjectsInfo;
      if (dtObjectsInfo != null)
      {
        List<DataRow> dataRowList = new List<DataRow>(dtObjectsInfo.Rows.Count);
        DateTime now = DateTime.Now;
        foreach (DataRow row in (InternalDataCollectionBase) dtObjectsInfo.Rows)
        {
          if (row["REMINDER_DATE"] is DateTime dateTime && dateTime.CompareTo(now) <= 0)
            dataRowList.Add(row);
        }
        if (dataRowList.Count != 0)
          objsInfo.Add(dictReminder.Key, dataRowList.ToArray());
      }
    }
    if (objsInfo.Count == 0)
      return;
    if (this._reminderForm != null)
    {
      this._reminderForm.Refresh(objsInfo);
    }
    else
    {
      this._reminderForm = new OrganizerReminderForm();
      this._reminderForm.StopReminderForObjects += new StopReminderForObjectsHandler(this.On_reminderForm_StopReminderForObjects);
      this._reminderForm.DelayReminderForObjects += new DelayReminderForObjectsHandler(this.On_reminderForm_DelayReminderForObjects);
      this._reminderForm.FormClosed += new FormClosedEventHandler(this.On_reminderForm_FormClosed);
      this._reminderForm.Refresh(objsInfo);
      (ServicesManager.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service ? service.MainForm : (Form) null)?.BringToFront();
      int num = (int) this._reminderForm.ShowTopDialog();
    }
  }

  /// <summary>
  /// Срабатывание таймера для запросов на сервер по истечении заданного интервала времени.
  /// </summary>
  /// <param name="state"></param>
  private void On_timerForRequestToServer_Tick(object state)
  {
    this._nextDate = DateTime.Now.AddMinutes((double) this._intervalForRequestToServer);
    if (this._dictReminders.Count == 0)
    {
      this._timerForRequestToServer.Change(-1, -1);
      this._timerForLocalCollection.Stop();
    }
    else
    {
      foreach (KeyValuePair<int, OrganizerService.ReminderInfo> dictReminder in this._dictReminders)
      {
        if (this._objTypeIDOrganizerTask == dictReminder.Key)
        {
          OrganizerService.ReminderInfo rInfo = dictReminder.Value;
          ConditionStructure[] conditions = ConditionStructure.Join(new ConditionStructure(this._attrIDReminderDate, RelationalOperators.LessOrEqual, (object) this._nextDate, LogicalOperators.NONE, 0, false), rInfo._conditions);
          try
          {
            this.GetReminderInfoForOrganizerTasks(rInfo, conditions);
          }
          catch (Exception ex)
          {
            if (state is Control control)
            {
              if (control.InvokeRequired)
                control.Invoke((Delegate) new OrganizerService.ExceptionDelegate(this.ShowException), (object) ex);
            }
          }
        }
      }
      if (!(state is Control control1))
        return;
      try
      {
        if (control1.InvokeRequired)
          control1.Invoke((Delegate) new OrganizerService.TimerForLocalCollectionDelegate(this.On_timerForLocalCollection_Tick), new object[2]);
        else
          this.On_timerForLocalCollection_Tick((object) null, (EventArgs) null);
      }
      catch (Exception ex)
      {
        if (control1 == null || control1.Disposing || control1.IsDisposed || !control1.InvokeRequired)
          return;
        control1.Invoke((Delegate) new OrganizerService.ExceptionDelegate(this.ShowException), (object) ex);
      }
    }
  }

  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="nodeGuid">GUID узла</param>
  /// <param name="typeID">Идентификатор типа объектов, которые будут входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="icoIndex">Индекс иконки для узла в ICategoryTypeIconService</param>
  public IDescriptor RegisterNode(
    Guid nodeGuid,
    int typeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex)
  {
    return this.RegisterNode(nodeGuid, typeID, conditions, columns, caption, icoIndex, (Dictionary<string, CommandInfo>) null, (List<string>) null, (Dictionary<string, ViewInfo>) null, (List<string>) null);
  }

  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="nodeGuid">GUID узла</param>
  /// <param name="typeID">Идентификатор типа объектов, которые будут входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="icoIndex">Индекс иконки для узла в ICategoryTypeIconService</param>
  /// <param name="requiredCommans">Команды контекстного меню, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousCommands">Команды контекстного меню, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  /// <param name="requiredViews">Вложенные закладки, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousViews">Вложенные закладки, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  public IDescriptor RegisterNode(
    Guid nodeGuid,
    int typeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex,
    Dictionary<string, CommandInfo> requiredCommans,
    List<string> superfluousCommands,
    Dictionary<string, ViewInfo> requiredViews,
    List<string> superfluousViews)
  {
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.ServiceContainer.GetService(typeof (ICategoryTypeIconService));
    if (service == null)
      return (IDescriptor) null;
    Icon indexIcon = service.GetIndexIcon(icoIndex);
    return this.RegisterNode(nodeGuid, typeID, conditions, columns, caption, indexIcon, requiredCommans, superfluousCommands, requiredViews, superfluousViews);
  }

  /// <summary>Регистрация подузла узла "Органайзер".</summary>
  /// <param name="nodeGuid">GUID узла</param>
  /// <param name="typeID">Идентификатор типа объектов, которые будут входить в данный узел</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="columns">Коллекция отображаемых колонок</param>
  /// <param name="caption">Наименование узла</param>
  /// <param name="ico">Иконка для узла</param>
  /// <param name="requiredCommands">Команды контекстного меню, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousCommands">Команды контекстного меню, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  /// <param name="requiredViews">Вложенные закладки, которые необходимо добавить для элементов подузла узла "Органайзер"</param>
  /// <param name="superfluousViews">Вложенные закладки, которые необходимо убрать для элементов подузла узла "Органайзер"</param>
  public IDescriptor RegisterNode(
    Guid nodeGuid,
    int typeID,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    Icon ico,
    Dictionary<string, CommandInfo> requiredCommands,
    List<string> superfluousCommands,
    Dictionary<string, ViewInfo> requiredViews,
    List<string> superfluousViews)
  {
    return this.RegisterNode(nodeGuid, typeID, 0, (int[]) null, conditions, columns, caption, ico, requiredCommands, superfluousCommands, requiredViews, superfluousViews);
  }

  public IDescriptor RegisterNode(
    Guid nodeGuid,
    int typeID,
    int relTypeID,
    int[] objTypeIDs,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    Icon ico,
    Dictionary<string, CommandInfo> requiredCommands,
    List<string> superfluousCommands,
    Dictionary<string, ViewInfo> requiredViews,
    List<string> superfluousViews)
  {
    if (nodeGuid == Guid.Empty || typeID == -1 || typeID == -1)
      return (IDescriptor) null;
    System.IServiceProvider serviceContainer = (System.IServiceProvider) ServicesManager.ServiceContainer;
    if (serviceContainer == null)
      return (IDescriptor) null;
    IFactory service1 = (IFactory) serviceContainer.GetService(typeof (IFactory));
    if (service1 == null)
      return (IDescriptor) null;
    if (!(serviceContainer.GetService(typeof (IGuidMapper)) is IGuidMapper service2))
      return (IDescriptor) null;
    int num = service2.Register(nodeGuid);
    OrganizerService.NodeInfo nodeInfo = new OrganizerService.NodeInfo();
    service1.AddNodeType(num, typeof (OrganizerChildNode));
    service1.AddViewsProvider(num, (IViewsProvider) new OrganizerViewProvider());
    if (requiredCommands != null && requiredCommands.Count > 0 || superfluousCommands != null && superfluousCommands.Count > 0)
    {
      service1.AddCommandsProvider(1, typeID, (ICommandsProvider) new OrganizerChildCommandProvider());
      nodeInfo.requiredCommands = requiredCommands;
      nodeInfo.superfluousCommands = superfluousCommands;
    }
    if (requiredViews != null && requiredViews.Count > 0 || superfluousViews != null && superfluousViews.Count > 0)
    {
      service1.AddViewsProvider(1, typeID, (IViewsProvider) new OrganizerChildViewProvider());
      nodeInfo.requiredViews = requiredViews;
      nodeInfo.superfluousViews = superfluousViews;
    }
    ConditionStructure conditionStructure1 = new ConditionStructure(this._attrIDStartDate, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false);
    ConditionStructure conditionStructure2 = new ConditionStructure(this._attrIDFinishDate, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false);
    conditionStructure1.AttributeSource = AttributeSourceTypes.Object;
    conditionStructure2.AttributeSource = AttributeSourceTypes.Object;
    ConditionStructure[] conditions1 = ConditionStructure.Join(new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    }, conditions);
    nodeInfo.descriptor = new OrganizerChildNodeDescriptor(nodeGuid, num, typeID, relTypeID, objTypeIDs, columns, conditions1, caption);
    this._nodesInfo.Add(num, nodeInfo);
    if (ico == null)
      return (IDescriptor) null;
    ((ICategoryTypeIconService) serviceContainer.GetService(typeof (ICategoryTypeIconService)))?.AddIcon(ico, num);
    ico.Dispose();
    return (IDescriptor) nodeInfo.descriptor;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeGuid"></param>
  /// <param name="relTypeID"></param>
  /// <param name="objTypeID"></param>
  /// <param name="objTypeIDs"></param>
  /// <param name="conditions"></param>
  /// <param name="columns"></param>
  /// <param name="caption"></param>
  /// <param name="icoIndex"></param>
  public IDescriptor RegisterNode(
    Guid nodeGuid,
    int relTypeID,
    int objTypeID,
    int[] objTypeIDs,
    ConditionStructure[] conditions,
    NodeColumnCollection columns,
    string caption,
    int icoIndex)
  {
    Icon ico = (Icon) null;
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
      ico = service.GetIndexIcon(icoIndex);
    return this.RegisterNode(nodeGuid, objTypeID, relTypeID, objTypeIDs, conditions, columns, caption, ico, (Dictionary<string, CommandInfo>) null, (List<string>) null, (Dictionary<string, ViewInfo>) null, (List<string>) null);
  }

  /// <summary>
  /// Регистрация типа объектов, о которых необходимо напоминать пользователю.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="conditions">Условия, по которым выбираются объекты о которых необходимо напоминать</param>
  public void RegisterTypeForReminder(int objTypeID, ConditionStructure[] conditions)
  {
    if (objTypeID == -1 || conditions == null || conditions.Length == 0 || this._dictReminders.ContainsKey(objTypeID))
      return;
    this._dictReminders.Add(objTypeID, new OrganizerService.ReminderInfo(conditions));
  }

  /// <summary>Запуск таймеров.</summary>
  /// <param name="interval">Интервал времени, через который таймер будет производить запрос на сервер.
  /// Интервал необходимо задавать в минутах</param>
  public void StartTimers(int interval)
  {
    this.InitServices();
    this._intervalForRequestToServer = interval;
    if (this._dictReminders.Count <= 0)
      return;
    this._timerForRequestToServer.Change(0, interval * 1000 * 60);
    this._timerForLocalCollection.Start();
  }

  /// <summary>Остоновка таймеров.</summary>
  internal void StopTimers()
  {
    this.ReleaseServices();
    this._timerForRequestToServer.Change(-1, -1);
    this._timerForLocalCollection.Stop();
    foreach (KeyValuePair<int, OrganizerService.ReminderInfo> dictReminder in this._dictReminders)
      dictReminder.Value._dtObjectsInfo = (DataTable) null;
  }

  /// <summary>
  /// Время до начала напоминания (при инициализации напоминания), 30 мин по умолчанию/
  /// </summary>
  public int TimeBeforeReminder
  {
    get => this._timeBeforeReminder;
    set => this._timeBeforeReminder = value;
  }

  /// <summary>Событие от глобальной службы уведомлений.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (sender is OrganizerService || e == null || !(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count == 0)
      return;
    long objectId = objectsEventArgs.ObjectIDs[0];
    switch (objectId)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        switch (e.EventName)
        {
          case "ObjectsCreated":
            if (!(ServicesManager.ServiceContainer.GetService(typeof (IOrganizerService)) is OrganizerService service1) || !(ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service2) || !service2.ReadBool("CLIENT", "ORGANIZER_REMINDER", "ACTIVATE", true, DBConfigMode.UserAndGlobal))
              return;
            int int32 = Convert.ToInt32(service2.ReadInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_SPACE", 15L, DBConfigMode.UserAndGlobal));
            service1.StartTimers(int32);
            return;
          case "ObjectsChanged":
          case "RelationsChanged":
            this.ChangeReminderObject(Math.Abs(objectId), objectsEventArgs.ObjectTypeIDs[0]);
            return;
          case "ObjectsRemoved":
            this.RemoveReminderObject(Math.Abs(objectId));
            return;
          default:
            return;
        }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="typeID"></param>
  /// <param name="objID"></param>
  private void ChangeReminderForOrganizerTasks(int typeID, long objID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
      IDBRelation relation = sessionKeeper.Session.GetRelation(objID, sessionKeeper.Session.UserID, relationTypeId, true);
      if (relation == null)
        return;
      IDBAttribute attributeById1 = relation.GetAttributeByID(this._attrIDReminder);
      bool flag = attributeById1 != null && attributeById1.AsBoolean;
      IDBAttribute attributeById2 = relation.GetAttributeByID(this._attrIDReminderDate);
      if (attributeById2 == null || attributeById2.Value == null || attributeById2.Value == DBNull.Value)
        attributeById1.Value = (object) (flag = false);
      DataRow[] dataRowArray = this._dictReminders[typeID]._dtObjectsInfo.Select($"{"OBJECT_ID"} = {objID}");
      if (dataRowArray.Length != 0)
      {
        if (flag)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
          if (objectActualCopy == null)
            return;
          dataRowArray[0]["OBJECT_ID"] = (object) objectActualCopy.ObjectID;
          dataRowArray[0]["OBJECT_TYPE"] = (object) objectActualCopy.ObjectType;
          dataRowArray[0]["CAPTION"] = (object) objectActualCopy.Caption;
          IDBAttribute attributeById3 = objectActualCopy.GetAttributeByID(this._attrIDTaskText);
          dataRowArray[0]["OBJECT_TEXT"] = attributeById3 != null ? attributeById3.Value : (object) string.Empty;
          IDBAttribute attributeById4 = objectActualCopy.GetAttributeByID(this._attrIDStartDate);
          if (attributeById4 != null)
            dataRowArray[0]["START_DATE"] = attributeById4.Value;
          IDBAttribute attributeById5 = objectActualCopy.GetAttributeByID(this._attrIDFinishDate);
          if (attributeById5 != null)
            dataRowArray[0]["FINISH_DATE"] = attributeById5.Value;
          dataRowArray[0]["REMINDER_DATE"] = attributeById2.Value;
        }
        else
          this.RemoveReminderObject(objID);
      }
      else
      {
        if (!flag)
          return;
        this._timerForRequestToServer.Change(0, this._intervalForRequestToServer);
      }
    }
  }

  /// <summary>Изменение объекта напоминания.</summary>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="typeID">Тип объекта</param>
  private void ChangeReminderObject(long objID, int typeID)
  {
    if (typeID == -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        typeID = sessionKeeper.Session.GetObjectInfo(objID).ObjectTypeID;
    }
    if (!this._dictReminders.ContainsKey(typeID) || this._dictReminders[typeID]._dtObjectsInfo == null)
      return;
    if (typeID == this._objTypeIDOrganizerTask)
    {
      this.ChangeReminderForOrganizerTasks(typeID, objID);
    }
    else
    {
      DataRow[] dataRowArray = this._dictReminders[typeID]._dtObjectsInfo.Select($"{"OBJECT_ID"} = {objID}");
      DataRow row;
      if (dataRowArray.Length == 0)
      {
        row = this._dictReminders[typeID]._dtObjectsInfo.NewRow();
        this._dictReminders[typeID]._dtObjectsInfo.Rows.Add(row);
        row["OBJECT_ID"] = (object) objID;
        row["OBJECT_TYPE"] = (object) typeID;
      }
      else
        row = dataRowArray[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
        if (objectActualCopy == null)
          return;
        IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(this._attrIDReminder);
        if (attributeById1 == null || attributeById1.Value == null || attributeById1.Value == DBNull.Value)
          return;
        if (!Convert.ToBoolean(attributeById1.Value))
        {
          this.RemoveReminderObject(objID);
        }
        else
        {
          row["CAPTION"] = (object) objectActualCopy.Caption;
          IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(this._attrIDStartDate);
          if (attributeById2 != null)
            row["START_DATE"] = attributeById2.Value;
          IDBAttribute attributeById3 = objectActualCopy.GetAttributeByID(this._attrIDFinishDate);
          if (attributeById3 != null)
            row["FINISH_DATE"] = attributeById3.Value;
          IDBAttribute attributeById4 = objectActualCopy.GetAttributeByID(this._attrIDReminderDate);
          if (attributeById4 != null && attributeById4.Value != null && attributeById4.Value != DBNull.Value)
          {
            DateTime result = DateTime.Now;
            if (DateTime.TryParse(attributeById4.Value.ToString(), out result))
            {
              row["REMINDER_DATE"] = attributeById4.Value;
              if (this._reminderForm == null)
                return;
              if (result <= DateTime.Now)
              {
                this._reminderForm.RefreshElement(objID, row);
                return;
              }
              this._reminderForm.RemoveElement(objID);
              return;
            }
          }
          this.RemoveReminderObject(objID);
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dict"></param>
  private void DelayReminderForOrganizerTasks(Dictionary<long, DateTime> dict)
  {
    if (dict == null || dict.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
      List<long> relationIDs = new List<long>(dict.Count);
      DataTable dataTable = (DataTable) null;
      OrganizerService.ReminderInfo reminderInfo = (OrganizerService.ReminderInfo) null;
      if (this._dictReminders.ContainsKey(this._objTypeIDOrganizerTask))
      {
        reminderInfo = this._dictReminders[this._objTypeIDOrganizerTask];
        dataTable = reminderInfo._dtObjectsInfo != null ? reminderInfo._dtObjectsInfo : new DataTable();
      }
      foreach (KeyValuePair<long, DateTime> keyValuePair in dict)
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(keyValuePair.Key, sessionKeeper.Session.UserID, relationTypeId, true);
        if (relation != null)
        {
          IDBAttribute attributeById = relation.GetAttributeByID(this._attrIDReminderDate);
          if (attributeById != null)
          {
            attributeById.Value = (object) keyValuePair.Value;
            if (!relationIDs.Contains(relation.RelationID))
              relationIDs.Add(relation.RelationID);
          }
        }
        if (keyValuePair.Value < this._nextDate)
        {
          DataRow[] dataRowArray = dataTable.Select($"{"OBJECT_ID"} = '{keyValuePair.Key}'");
          if (dataRowArray.Length != 0)
            dataRowArray[0]["REMINDER_DATE"] = (object) keyValuePair.Value;
        }
        else
          reminderInfo?.RemoveObjectInfo(keyValuePair.Key);
      }
      if (relationIDs.Count <= 0)
        return;
      this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", (IList<long>) relationIDs));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rInfo"></param>
  /// <param name="conditions"></param>
  private void GetReminderInfoForOrganizerTasks(
    OrganizerService.ReminderInfo rInfo,
    ConditionStructure[] conditions)
  {
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
    if (SessionKeeper.CurrentAllocator == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
      relationCollection.ObjectTypeID = this._objTypeIDOrganizerTask;
      relationCollection.LocalTypesMode = true;
      if (relationCollection == null)
        return;
      DataTable dataTable = relationCollection.Select(new DBRecordSetParams(conditions, new object[2]
      {
        (object) -21,
        (object) this._attrIDReminderDate
      }));
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this._objTypeIDOrganizerTask);
      if (objectCollection == null)
        return;
      DataTable infoTable = (DataTable) null;
      int count = dataTable.Rows.Count;
      List<object> taskIDs = new List<object>(count < 501 ? count : 500);
      Dictionary<object, object> dict = new Dictionary<object, object>(count);
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        taskIDs.Add(row[0]);
        dict.Add(row[0], row[1]);
        if (++num >= 500)
        {
          infoTable = this.MargeReminderInfoTable(objectCollection, infoTable, taskIDs, dict);
          taskIDs.Clear();
          num = 0;
        }
      }
      rInfo.AddObjectsInfo(this.MargeReminderInfoTable(objectCollection, infoTable, taskIDs, dict));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objCollection"></param>
  /// <param name="infoTable"></param>
  /// <param name="taskIDs"></param>
  /// <param name="dict"></param>
  /// <returns></returns>
  private DataTable MargeReminderInfoTable(
    IDBObjectCollection objCollection,
    DataTable infoTable,
    List<object> taskIDs,
    Dictionary<object, object> dict)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) taskIDs.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[7]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDTaskText, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDStartDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDFinishDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDReminderDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable table = objCollection.Select(paramSet);
    if (table != null)
    {
      string columnName = this._attrIDReminderDate.ToString();
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        row[columnName] = dict[row[0]];
      if (infoTable != null)
        infoTable.Merge(table);
      else
        infoTable = table;
    }
    return infoTable;
  }

  /// <summary>
  /// Удаление информации об объекте из локального хранилища и с формы напоминания.
  /// </summary>
  /// <param name="objID">Идентификатор объекта</param>
  private void RemoveReminderObject(long objID)
  {
    if (objID == 0L)
      return;
    foreach (KeyValuePair<int, OrganizerService.ReminderInfo> dictReminder in this._dictReminders)
    {
      if (dictReminder.Value.RemoveObjectInfo(objID))
        break;
    }
    if (this._reminderForm == null)
      return;
    this._reminderForm.RemoveElement(objID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="list"></param>
  private void StopReminderForOrganizerTasks(List<long> list)
  {
    if (list == null || list.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
      List<long> relationIDs = new List<long>(list.Count);
      DataTable dataTable = (DataTable) null;
      OrganizerService.ReminderInfo reminderInfo = (OrganizerService.ReminderInfo) null;
      if (this._dictReminders.ContainsKey(this._objTypeIDOrganizerTask))
      {
        reminderInfo = this._dictReminders[this._objTypeIDOrganizerTask];
        dataTable = reminderInfo._dtObjectsInfo != null ? reminderInfo._dtObjectsInfo : new DataTable();
      }
      foreach (long num1 in list)
      {
        long num2 = 0;
        DateTime dateTime = DateTime.MinValue;
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(num1, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeById = objectActualCopy.GetAttributeByID(this._attrIDRepetition);
          if (attributeById != null)
            num2 = attributeById.AsInteger;
        }
        IDBRelation relation = sessionKeeper.Session.GetRelation(num1, sessionKeeper.Session.UserID, relationTypeId, true);
        if (relation != null)
        {
          if (num2 > 0L && num2 <= 4L)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(this._attrIDReminderDate);
            DateTime asDateTime = attributeById.AsDateTime;
            if (attributeById != null)
            {
              DateTime now;
              switch (num2 - 1L)
              {
                case 0:
                  ref DateTime local1 = ref dateTime;
                  now = DateTime.Now;
                  int year1 = now.Year;
                  now = DateTime.Now;
                  int month1 = now.Month;
                  now = DateTime.Now;
                  int day1 = now.Day;
                  int hour1 = asDateTime.Hour;
                  int minute1 = asDateTime.Minute;
                  int second1 = asDateTime.Second;
                  local1 = new DateTime(year1, month1, day1, hour1, minute1, second1);
                  dateTime = dateTime.AddDays(1.0);
                  break;
                case 1:
                  now = DateTime.Now;
                  int num3 = now.DayOfYear - asDateTime.DayOfYear;
                  if (num3 > 7)
                    num3 -= num3 / 7 * 7;
                  int num4 = 7 - num3;
                  ref DateTime local2 = ref dateTime;
                  now = DateTime.Now;
                  int year2 = now.Year;
                  now = DateTime.Now;
                  int month2 = now.Month;
                  now = DateTime.Now;
                  int day2 = now.Day;
                  int hour2 = asDateTime.Hour;
                  int minute2 = asDateTime.Minute;
                  int second2 = asDateTime.Second;
                  local2 = new DateTime(year2, month2, day2, hour2, minute2, second2);
                  dateTime = dateTime.AddDays((double) num4);
                  break;
                case 2:
                  ref DateTime local3 = ref dateTime;
                  now = DateTime.Now;
                  int year3 = now.Year;
                  now = DateTime.Now;
                  int month3 = now.Month;
                  int day3 = asDateTime.Day;
                  int hour3 = asDateTime.Hour;
                  int minute3 = asDateTime.Minute;
                  int second3 = asDateTime.Second;
                  local3 = new DateTime(year3, month3, day3, hour3, minute3, second3);
                  int day4 = asDateTime.Day;
                  now = DateTime.Now;
                  int day5 = now.Day;
                  if (day4 <= day5)
                  {
                    dateTime = dateTime.AddMonths(1);
                    break;
                  }
                  break;
                case 3:
                  ref DateTime local4 = ref dateTime;
                  now = DateTime.Now;
                  int year4 = now.Year;
                  int month4 = asDateTime.Month;
                  int day6 = asDateTime.Day;
                  int hour4 = asDateTime.Hour;
                  int minute4 = asDateTime.Minute;
                  int second4 = asDateTime.Second;
                  local4 = new DateTime(year4, month4, day6, hour4, minute4, second4);
                  dateTime = dateTime.AddYears(1);
                  break;
              }
              attributeById.Value = (object) dateTime;
              if (!relationIDs.Contains(relation.RelationID))
                relationIDs.Add(relation.RelationID);
            }
          }
          if (dateTime == DateTime.MinValue)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(this._attrIDReminder);
            if (attributeById != null)
            {
              attributeById.Value = (object) false;
              if (!relationIDs.Contains(relation.RelationID))
                relationIDs.Add(relation.RelationID);
            }
          }
        }
        if (dateTime != DateTime.MinValue && dateTime < this._nextDate)
        {
          DataRow[] dataRowArray = dataTable.Select($"{"OBJECT_ID"} = '{num1}'");
          if (dataRowArray.Length != 0)
            dataRowArray[0]["REMINDER_DATE"] = (object) dateTime;
        }
        else
          reminderInfo?.RemoveObjectInfo(num1);
      }
      if (relationIDs.Count <= 0)
        return;
      this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", (IList<long>) relationIDs));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  private void ShowException(Exception ex) => ExceptionHelper.ExceptionService.ShowException(ex);

  /// <summary>Выполнить инициализацию сервисов закладки.</summary>
  protected virtual void InitServices()
  {
    if (this._notificationService != null)
      return;
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._notificationHandler != null || this._notificationService == null)
      return;
    this._notificationHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
    this._notificationService.Subscribe(this._notificationHandler);
  }

  /// <summary>Выполнить деинициализацию сервисов закладки.</summary>
  protected virtual void ReleaseServices()
  {
    if (this._notificationService == null)
      return;
    if (this._notificationHandler != null && this._notificationService != null)
      this._notificationService.Unsubscribe(this._notificationHandler);
    this._notificationService = (INotificationService) null;
    this._notificationHandler = (NotificationEventHandler) null;
  }

  /// <summary>Получение дескриптора подузла узла "Органайзер".</summary>
  /// <param name="categoryID">Идентификатор узла</param>
  /// <returns>Дескриптор узла. Null если для узла дескриптора нет</returns>
  public OrganizerChildNodeDescriptor GetDescriptor(int categoryID)
  {
    return !this._nodesInfo.ContainsKey(categoryID) ? (OrganizerChildNodeDescriptor) null : this._nodesInfo[categoryID].descriptor;
  }

  /// <summary>
  /// Получение команд контекстного меню, которые необходимо добавить для элементов подузла узла "Органайзер".
  /// </summary>
  /// <param name="categoryID">Идентификатор узла</param>
  /// <returns>Список команд. Null если список команд отсутствует
  /// string - наименование команды
  /// CommandInfo - описание команды контекстного меню</returns>
  public Dictionary<string, CommandInfo> GetRequiredCommands(int categoryID)
  {
    return !this._nodesInfo.ContainsKey(categoryID) ? (Dictionary<string, CommandInfo>) null : this._nodesInfo[categoryID].requiredCommands;
  }

  /// <summary>
  /// Получение команд контекстного меню, которые необходимо убрать для элементов подузла узла "Органайзер".
  /// </summary>
  /// <param name="categoryID">Идентификатор узла</param>
  /// <returns>Список команд. Null если список команд отсутствует
  /// string - наименование команды</returns>
  public List<string> GetSuperfluousCommands(int categoryID)
  {
    return !this._nodesInfo.ContainsKey(categoryID) ? (List<string>) null : this._nodesInfo[categoryID].superfluousCommands;
  }

  /// <summary>
  /// Получение вложенных закладок, которые необходимо добавить для элементов подузла узла "Органайзер".
  /// </summary>
  /// <param name="categoryID">Идентификатор узла</param>
  /// <returns>Список закладок. Null если список закладок отсутствует
  /// string - наименование закладки
  /// ViewInfo - описание закладки</returns>
  public Dictionary<string, ViewInfo> GetRequiredViews(int categoryID)
  {
    return !this._nodesInfo.ContainsKey(categoryID) ? (Dictionary<string, ViewInfo>) null : this._nodesInfo[categoryID].requiredViews;
  }

  /// <summary>
  /// Получение вложенных закладок, которые необходимо убрать для элементов подузла узла "Органайзер".
  /// </summary>
  /// <param name="categoryID">Идентификатор узла</param>
  /// <returns>Список закладок. Null если список закладок отсутствует
  /// string - наименование закладки</returns>
  public List<string> GetSuperfluousViews(int categoryID)
  {
    return !this._nodesInfo.ContainsKey(categoryID) ? (List<string>) null : this._nodesInfo[categoryID].superfluousViews;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public delegate void TimerForLocalCollectionDelegate(object sender, EventArgs e);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public delegate void ExceptionDelegate(Exception ex);

  /// <summary>
  /// Класс для хранения информации, касающейся подузла узла "Органайзер".
  /// </summary>
  private class NodeInfo
  {
    /// <summary>Дескриптор узла</summary>
    internal OrganizerChildNodeDescriptor descriptor;
    /// <summary>
    /// Коллекция команд, которые необходимо дополнительно добавить в контекстном меню
    /// string - наименование команды
    /// CommandInfo - описание команды контекстного меню
    /// </summary>
    internal Dictionary<string, CommandInfo> requiredCommands;
    /// <summary>
    /// Коллекция команд, которые необходимо подавить в контекстном меню
    /// string - наименование команды
    /// </summary>
    internal List<string> superfluousCommands;
    /// <summary>
    /// Коллекция вьюшек, которые необходимо дополнительно добавить
    /// string - наименование команды
    /// ViewInfo - описание закладки
    /// </summary>
    internal Dictionary<string, ViewInfo> requiredViews;
    /// <summary>
    /// Коллекция вьюшек, которые необходимо подавить
    /// string - наименование закладки
    /// </summary>
    internal List<string> superfluousViews;
  }

  /// <summary>
  /// Класс хранит информацию связанную с напоминанием пользователю о запланированных задачах.
  /// </summary>
  private class ReminderInfo
  {
    /// <summary>
    /// Поле, в котором хранятся условия выбора объектов, о которых необходимо напомнить пользователю
    /// </summary>
    internal ConditionStructure[] _conditions;
    /// <summary>
    /// Поле, в котором хранится коллекции объектов для напоминания
    /// </summary>
    internal DataTable _dtObjectsInfo;

    /// <summary>Конструктор.</summary>
    /// <param name="conditions">Условия выбора объектов, о которых необходимо напомнить пользователю</param>
    internal ReminderInfo(ConditionStructure[] conditions) => this._conditions = conditions;

    /// <summary>Добавление новых объектов к таблице уже существующих.</summary>
    /// <param name="dt">Таблица с объектами</param>
    internal void AddObjectsInfo(DataTable dt)
    {
      if (dt == null)
        return;
      int num = -2;
      string name1 = num.ToString();
      num = -7;
      string name2 = num.ToString();
      num = -50;
      string name3 = num.ToString();
      num = MetaDataHelper.GetAttributeID((object) "cad015d2-306c-11d8-b4e9-00304f19f545");
      string name4 = num.ToString();
      num = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
      string name5 = num.ToString();
      num = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
      string name6 = num.ToString();
      num = MetaDataHelper.GetAttributeTypeID("cad015d4-306c-11d8-b4e9-00304f19f545");
      string name7 = num.ToString();
      if (dt.Columns.Contains(name1))
        dt.Columns[name1].ColumnName = "OBJECT_ID";
      if (dt.Columns.Contains(name2))
        dt.Columns[name2].ColumnName = "OBJECT_TYPE";
      if (dt.Columns.Contains(name3))
        dt.Columns[name3].ColumnName = "CAPTION";
      if (dt.Columns.Contains(name4))
        dt.Columns[name4].ColumnName = "OBJECT_TEXT";
      if (dt.Columns.Contains(name5))
        dt.Columns[name5].ColumnName = "START_DATE";
      if (dt.Columns.Contains(name6))
        dt.Columns[name6].ColumnName = "FINISH_DATE";
      if (dt.Columns.Contains(name7))
        dt.Columns[name7].ColumnName = "REMINDER_DATE";
      this._dtObjectsInfo = dt;
      this._dtObjectsInfo.AcceptChanges();
    }

    /// <summary>Удаление объектов из таблицы.</summary>
    /// <param name="objID">Идентификатор удаляемого объекта</param>
    /// <returns>Результат, был или не был удален объект</returns>
    internal bool RemoveObjectInfo(long objID)
    {
      bool flag = false;
      if (this._dtObjectsInfo != null)
      {
        DataRow[] dataRowArray = this._dtObjectsInfo.Select($"{"OBJECT_ID"} = {objID}");
        if (dataRowArray.Length != 0)
        {
          this._dtObjectsInfo.Rows.Remove(dataRowArray[0]);
          this._dtObjectsInfo.AcceptChanges();
          flag = true;
        }
      }
      return flag;
    }
  }
}
