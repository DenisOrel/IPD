
// Type: Intermech.Client.Core.Organizer.OrganizerCalendarView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using NJFLib.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>Закладка для отображения календаря органайзера.</summary>
[ToolboxItem(false)]
public class OrganizerCalendarView : UserControl, IView, ICanCloseViews, ICanDeactivateView
{
  private System.IServiceProvider _provider;
  private ICalendar _calendarSettings;
  private int _imgIndex = -1;
  private string _caption = string.Empty;
  private INotificationService _notificationService;
  private NotificationEventHandler _notificationHandler;
  private int _taskTypeID = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
  private int _attrIDStartDate = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
  private int _attrIDFinishDate = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
  private int _ownerID = MetaDataHelper.GetAttributeTypeID("cad0002f-306c-11d8-b4e9-00304f19f545");
  private INodeID _nodeID;
  private Dictionary<int, Icon> _images = new Dictionary<int, Icon>();
  private OrganizerCalendarView.NodeType _nodeType = OrganizerCalendarView.NodeType.IsOther;
  private Image _defaultImg;
  private string _defaultText = string.Empty;
  private static Size _embeddedViewsPanelSize = Size.Empty;
  private static bool _savedConfiguration = false;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private NavigationBar _navBar;
  private NavigationBand _calendarBand;
  private CalendarView _calendar;
  private Scheduler _scheduler;
  protected Intermech.Bars.ToolBar _tbViewBar;
  protected ButtonItem _biRefresh;
  protected DropDownMenuItem _biViewNames;
  protected ButtonItem _biHelp;
  private CollapsibleSplitter _splitterH;
  protected PageViewsManager _viewsManager;
  private Splitter _splitterV;
  private ButtonItem _biCreateTask;

  /// <summary>
  /// 
  /// </summary>
  public DateSelectionMode DateSelectionMode => this._calendar.DateSelectionMode;

  /// <summary>Конструктор.</summary>
  public OrganizerCalendarView()
  {
    this.InitializeComponent();
    this._defaultImg = this._biViewNames.Image;
    this._defaultText = this._biViewNames.Text;
    this._caption = LocalizationHolder.rm.GetString("Organizer.CalendarView.Caption");
    this._scheduler.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
    this.LoadConfiguration();
    this.ReadCalendarSettings();
    this.SetHighlights();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_biRefresh_Click(object sender, EventArgs e)
  {
    this.InitializeSchedulerItems();
    this._calendar.Refresh();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_biViewNames_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this._viewsManager.Visible)
      return;
    this.OpenEmbeddedViews();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_biViewNames_Click(object sender, EventArgs e)
  {
    if (!this._viewsManager.Visible)
      this.OpenEmbeddedViews();
    else
      this.CloseEmbeddedViews();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_calendar_SelectionChanged(object sender, EventArgs e)
  {
    if (!this._calendar.IsEndSelection)
      return;
    if (this.DateSelectionMode == DateSelectionMode.WorkWeek)
    {
      this._scheduler.ExcludedDays.Clear();
      List<int> intList1 = this._calendar.GetExcludedDaysForMonth(this._calendar.SelectionBegin);
      DateTime dateTime;
      if (intList1 == null)
      {
        intList1 = new List<int>();
        DateTime day = this._calendar.SelectionBegin;
        int month1 = day.Month;
        dateTime = this._calendar.SelectionEnd;
        int month2 = dateTime.Month;
        bool flag = month1 != month2;
        for (; day < this._calendar.SelectionEnd; day = day.AddDays(1.0))
        {
          if (flag)
          {
            int month3 = day.Month;
            dateTime = this._calendar.SelectionEnd;
            int month4 = dateTime.Month;
            if (month3 == month4)
              break;
          }
          if (this._calendarSettings.GetDayByDate(day).DayType == DayType.Holiday)
            intList1.Add(day.Day);
        }
      }
      Dictionary<int, List<int>> excludedDays1 = this._scheduler.ExcludedDays;
      dateTime = this._calendar.SelectionBegin;
      int month5 = dateTime.Month;
      List<int> intList2 = intList1;
      excludedDays1.Add(month5, intList2);
      Dictionary<int, List<int>> excludedDays2 = this._scheduler.ExcludedDays;
      dateTime = this._calendar.SelectionEnd;
      int month6 = dateTime.Month;
      if (!excludedDays2.ContainsKey(month6))
      {
        List<int> intList3 = this._calendar.GetExcludedDaysForMonth(this._calendar.SelectionEnd);
        if (intList3 == null)
        {
          intList3 = new List<int>();
          for (DateTime day = this._calendar.SelectionEnd; day > this._calendar.SelectionBegin; day = day.AddDays(-1.0))
          {
            int month7 = day.Month;
            dateTime = this._calendar.SelectionBegin;
            int month8 = dateTime.Month;
            if (month7 != month8)
            {
              if (this._calendarSettings.GetDayByDate(day).DayType == DayType.Holiday)
                intList3.Add(day.Day);
            }
            else
              break;
          }
        }
        Dictionary<int, List<int>> excludedDays3 = this._scheduler.ExcludedDays;
        dateTime = this._calendar.SelectionEnd;
        int month9 = dateTime.Month;
        List<int> intList4 = intList3;
        excludedDays3.Add(month9, intList4);
      }
    }
    else
      this._scheduler.ExcludedDays = (Dictionary<int, List<int>>) null;
    this._scheduler.Items.Clear();
    this.SetHighlights();
    this._scheduler.SetViewRange(this._calendar.SelectionBegin, this._calendar.SelectionEnd);
    this.InitializeSchedulerItems();
  }

  /// <summary>Создание объекта.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_CreateItem(object sender, EventArgs e)
  {
    if (this._nodeType == OrganizerCalendarView.NodeType.IsOther)
      return;
    this.CreateObject(this._taskTypeID);
  }

  /// <summary>Нажатие заголовка дня планировщика.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_DayHeaderClick(object sender, SchedulerDayEventArgs e)
  {
    this._calendar.SetSelection(e.SchedulerDay.Date, e.SchedulerDay.Date, false);
    this._calendar.DateChoosedByUser = e.SchedulerDay.Date;
    this._calendar.DateSelectionMode = DateSelectionMode.Days;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  private void On_scheduler_HeaderButtonClick(object sender, int index)
  {
    switch (index)
    {
      case 0:
        this._calendar.DateSelectionMode = DateSelectionMode.Days;
        break;
      case 1:
        this._calendar.DateSelectionMode = DateSelectionMode.WorkWeek;
        break;
      case 2:
        this._calendar.DateSelectionMode = DateSelectionMode.Week;
        break;
      case 3:
        this._calendar.DateSelectionMode = DateSelectionMode.Month;
        break;
    }
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  private void On_scheduler_HeaderRadioButtonClick(object sender, int index)
  {
    if (index != 0)
    {
      if (index != 1)
        return;
      this._calendar.DateSelectionMode = DateSelectionMode.Week;
    }
    else
      this._calendar.DateSelectionMode = DateSelectionMode.WorkWeek;
  }

  /// <summary>Изменение наименования элемента планировщика.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemCaptionEdited(object sender, SchedulerItemCancelEventArgs e)
  {
    if (e == null || e.Item == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.Item.ObjectID, false);
      if (objectActualCopy == null)
        return;
      objectActualCopy.Caption = e.Item.Caption;
    }
    if (this._notificationService == null)
      return;
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", e.Item.ObjectID));
  }

  /// <summary>Изменение интервала времени элемента планировщика.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemDatesChanged(object sender, SchedulerItemEventArgs e)
  {
    if (e == null || e.Item == null)
      return;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.Item.ObjectID, false);
      if (objectActualCopy != null)
      {
        IDBAttribute attributeByGuid1 = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeStart);
        IDBAttribute attributeByGuid2 = objectActualCopy.GetAttributeByGuid(SystemGUIDs.attributeDueDate);
        if (attributeByGuid1 != null && attributeByGuid1.AsDateTime != e.Item.StartDate)
        {
          int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
          IDBRelation relation = sessionKeeper.Session.GetRelation(e.Item.ObjectID, objectInfo.ID, relationTypeId);
          if (relation != null)
          {
            IDBAttribute attributeByGuid3 = relation.GetAttributeByGuid(new Guid("cad015d5-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid3 != null && attributeByGuid3.AsBoolean)
            {
              IDBAttribute attributeByGuid4 = relation.GetAttributeByGuid(new Guid("cad015d4-306c-11d8-b4e9-00304f19f545"));
              if (attributeByGuid4 != null)
              {
                DateTime asDateTime1 = attributeByGuid4.AsDateTime;
                DateTime asDateTime2 = attributeByGuid1.AsDateTime;
                if (e.Item.StartDate < asDateTime1)
                  attributeByGuid4.Value = (object) asDateTime1.Add(e.Item.StartDate - asDateTime2);
              }
            }
          }
          attributeByGuid1.Value = (object) e.Item.StartDate;
          flag = true;
        }
        if (attributeByGuid2 != null)
        {
          if (attributeByGuid2.AsDateTime != e.Item.EndDate)
          {
            attributeByGuid2.Value = (object) e.Item.EndDate;
            flag = true;
          }
        }
      }
    }
    if (this._notificationService != null & flag)
      this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", e.Item.ObjectID));
    this._calendar.Refresh();
  }

  /// <summary>Двойной клик по элементу планировщика.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemDoubleClick(object sender, SchedulerItemEventArgs e)
  {
    if (e == null || e.Item == null)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, e.Item.ObjectID, false);
  }

  /// <summary>Удаление объектов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemsDeleted(object sender, SchedulerItemsEventArgs e)
  {
    if (this._notificationService == null)
      return;
    List<long> objectIDs = new List<long>(e.Items.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (CalendarItem calendarItem in e.Items)
      {
        if (!objectIDs.Contains(calendarItem.ObjectID))
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(calendarItem.ObjectID);
          if (dbObject != null)
          {
            dbObject.Delete(0L);
            objectIDs.Add(calendarItem.ObjectID);
          }
        }
      }
    }
    if (objectIDs.Count == 0)
      return;
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs));
  }

  /// <summary>Процесс удаления элементов планировщика.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemsDeleting(object sender, SchedulerItemsCancelEventArgs e)
  {
    string caption = LocalizationHolder.rm.GetString("Client.Core_132");
    string text = LocalizationHolder.rm.GetString("Client.Core.DeleteSelectedObject");
    e.Cancel = DialogResult.No == MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ItemsSelectionChanged(object sender, SchedulerItemsEventArgs e)
  {
    if (!this._viewsManager.Visible)
      return;
    this.OpenEmbeddedViews();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    List<CalendarItem> selectedItems = this._scheduler.SelectedItems;
    if (selectedItems.Count == 0)
      return;
    List<long> longList = new List<long>(selectedItems.Count);
    if (selectedItems.Count == 1 && !selectedItems[0].BaseItem)
    {
      string caption = LocalizationHolder.rm.GetString("Organizer_Name");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Organizer_RepetitionItem_Msg"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    foreach (CalendarItem calendarItem in selectedItems)
    {
      if (!longList.Contains(calendarItem.ObjectID))
        longList.Add(calendarItem.ObjectID);
    }
    Intermech.Navigator.ContextMenu.Services.GetMenu(ObjectExtensions.GetItems(longList.ToArray()), this._provider).Show((Control) this._scheduler, e.Location);
  }

  /// <summary>Скролирование месяца.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scheduler_ScrollMonth(object sender, SchedulerDatesEventArgs e)
  {
    this._calendar.SetSelection(this._scheduler.ViewStart, this._scheduler.ViewStart.AddDays(34.0), false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_viewsManager_ActiveViewPageChanged(object sender, EventArgs e)
  {
    if (this._viewsManager.ActiveViewPage == null || this._viewsManager.ActiveViewPage.View == null)
    {
      this.CloseEmbeddedViews();
    }
    else
    {
      string caption = this._viewsManager.ActiveViewPage.View.Caption;
      for (int index = 0; index < this._biViewNames.Items.Count; ++index)
        this._biViewNames.Items[index].Checked = this._biViewNames.Items[index].Text == caption;
      this._biViewNames.ImageIndex = this._viewsManager.ActiveViewPage.View.ImageIndex;
      this._biViewNames.Text = this._viewsManager.ActiveViewPage.View.Caption;
      this._biViewNames.Checked = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_viewsManager_Resize(object sender, EventArgs e)
  {
    OrganizerCalendarView._embeddedViewsPanelSize = this._viewsManager.Size;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cfgManager"></param>
  private static void OnConfiguration_BeforeSave(IConfigurationManager cfgManager)
  {
    if (cfgManager == null)
      return;
    cfgManager.Create(nameof (OrganizerCalendarView)).Add("PageViewsManager").SetProperty("PanelSize", (string) TypeDescriptor.GetConverter(typeof (Size)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) OrganizerCalendarView._embeddedViewsPanelSize, typeof (string)));
    OrganizerCalendarView._savedConfiguration = true;
    cfgManager.ConfigurationBeforeSave -= new ConfigurationBeforeSaveEventHandler(OrganizerCalendarView.OnConfiguration_BeforeSave);
  }

  /// <summary>Изменение данных у заготовки объекта.</summary>
  /// <param name="objID">Идентификатор заготовки объекта</param>
  private void Ondlg_ObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
    if (!(this._scheduler.SelectedElementEnd is SchedulerTimeScaleUnit selectedElementEnd))
      return;
    TimeSpan timeSpan = selectedElementEnd != null ? selectedElementEnd.Duration : new TimeSpan(23, 59, 59);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.ObjectID, false);
      if (objectActualCopy == null)
        return;
      IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(this._attrIDStartDate);
      if (attributeById1 != null)
        attributeById1.Value = (object) selectedElementEnd.Date;
      IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(this._attrIDFinishDate);
      if (attributeById2 == null)
        return;
      attributeById2.Value = (object) selectedElementEnd.Date.Add(timeSpan);
    }
  }

  /// <summary>
  /// Активизирует другой дополнительный вид при выборе из выпадающего списка.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnViewNamesItem_Click(object sender, EventArgs e)
  {
    this._viewsManager.ActiveViewPage = this._viewsManager.ViewPages[this._biViewNames.Items.IndexOf((ToolbarItemBase) (sender as MenuButtonItem))];
  }

  /// <summary>Выполняет инициализацию закладки после ее создания.</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    ServiceContainer serviceContainer = new ServiceContainer(provider);
    serviceContainer.AddService(typeof (OrganizerCalendarView), (object) this);
    this._viewsManager.Services = this._provider = (System.IServiceProvider) serviceContainer;
    INode itemData = (INode) items.GetItemData(0, typeof (INode));
    this._nodeID = items.GetItemID(0);
    this._nodeType = this._nodeID.CategoryID != Intermech.Navigator.Consts.OrganizerRootNodeTypeID ? (!(itemData.GetChild(this._nodeID) is OrganizerTaskNode) ? OrganizerCalendarView.NodeType.IsOther : OrganizerCalendarView.NodeType.IsTask) : OrganizerCalendarView.NodeType.IsRoot;
    this._biCreateTask.Enabled = this._nodeType != OrganizerCalendarView.NodeType.IsOther;
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this._imgIndex = service.ImageIndex("imgOrganizerCalendar");
      this._biViewNames.MenuImageList = service.ImageList;
      int index = service.ImageIndex("imgRefresh");
      if (index > -1)
        this._biRefresh.Image = service.ImageList.Images[index];
    }
    this.InitServices();
    this.InitializeSchedulerItems();
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране.
  /// </summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране.
  /// </summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (OrganizerCalendarView._savedConfiguration)
      return;
    this.SaveConfiguration();
  }

  /// <summary>
  /// Название закладки, которое будет отображаться на экране.
  /// </summary>
  public string Caption => this._caption;

  /// <summary>
  /// Индекс иконки, которая будет отображаться на экране, в именованном списке иконок.
  /// </summary>
  public int ImageIndex => this._imgIndex;

  /// <summary>
  /// Индекс расположения закладки среди других закладок при выводе на экран.
  /// </summary>
  public int OrderID => 0;

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanClose(object sender)
  {
    if (!OrganizerCalendarView._savedConfiguration)
      this.SaveConfiguration();
    return true;
  }

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>Событие от глобальной службы уведомлений.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (sender is ICalendar calendar)
    {
      if (this._calendar.CalendarSettings.CalendarID != calendar.CalendarID)
        return;
      this._calendar.CalendarSettings = this._calendarSettings = calendar;
    }
    else
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs))
        return;
      long num = Math.Abs(objectsEventArgs.ObjectIDs[0]);
      switch (num)
      {
        case -1:
          break;
        case 0:
          break;
        default:
          switch (e.EventName)
          {
            case "ObjectsCreated":
            case "ObjectsChanged":
              int objectTypeId = objectsEventArgs.ObjectTypeIDs[0];
              if (objectTypeId == -1)
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                  objectTypeId = sessionKeeper.Session.GetObjectInfo(num).ObjectTypeID;
              }
              if (objectTypeId != this._taskTypeID)
                return;
              this.InitializeSchedulerItems();
              return;
            case "ObjectsRemoved":
              if (sender is OrganizerCalendarView)
                return;
              this._scheduler.Items.Remove(num);
              this._calendar.Refresh();
              return;
            default:
              return;
          }
      }
    }
  }

  /// <summary>Убирает с экрана панель с дополнительными видами.</summary>
  private void CloseEmbeddedViews()
  {
    this._viewsManager.ActiveViewPageChanged -= new EventHandler(this.On_viewsManager_ActiveViewPageChanged);
    this._viewsManager.CloseViews();
    this._viewsManager.Visible = this._splitterH.Visible = false;
    this._biViewNames.Items.Clear();
    this._biViewNames.ImageIndex = -1;
    this._biViewNames.Image = this._defaultImg;
    this._biViewNames.Text = this._defaultText;
    this._biViewNames.Checked = false;
  }

  /// <summary>Создание объекта указанного типа.</summary>
  /// <param name="typeID">Идентификатор типа создаваемого объекта</param>
  /// <returns>Идентификатор созданного объекта</returns>
  private long CreateObject(int typeID)
  {
    if (typeID == -1)
      return 0;
    long num = 0;
    if (this._provider != null && this._provider.GetService(typeof (ProjectObjectID)) is ProjectObjectID service1)
      num = service1.ProjectID;
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service2))
      return 0;
    service2.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.Ondlg_ObjectCreatorDraftCreatedEvent);
    long objectByTypeDialog = service2.CreateObjectByTypeDialog(typeID);
    service2.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.Ondlg_ObjectCreatorDraftCreatedEvent);
    if (objectByTypeDialog == 0L || objectByTypeDialog == -1L)
      return 0;
    if (num != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(objectByTypeDialog).ProjectID = num;
    }
    this._calendar.Refresh();
    if (this._notificationService != null)
      this._notificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
    return objectByTypeDialog;
  }

  /// <summary>Создание элемента планировщика.</summary>
  /// <param name="objID">Идентификатор объекта, данными которого необходимо заполнить элемент планировщика</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="img">Изображение</param>
  private void CreateSchedulerItem(long objID, string caption, Image img)
  {
    if (objID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (objectActualCopy == null)
        return;
      if (string.IsNullOrEmpty(caption))
        caption = objectActualCopy.Caption;
      IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(this._attrIDStartDate);
      if (attributeById1 == null || attributeById1.Value == null || attributeById1.Value == DBNull.Value)
        return;
      IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(this._attrIDFinishDate);
      if (attributeById2 == null || attributeById2.Value == null || attributeById2.Value == DBNull.Value)
        return;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectActualCopy.ObjectID);
      CalendarItem calendarItem = this._scheduler.CreateItem(objectActualCopy.ObjectID, attributeById1.AsDateTime, attributeById2.AsDateTime, caption, img, true);
      if (calendarItem == null)
        return;
      IDBAttribute attributeById3 = objectActualCopy.GetAttributeByID(this._ownerID);
      if (attributeById3 != null)
        calendarItem.ReadOnly = objectInfo.ObjectTypeID != this._taskTypeID || (long) attributeById3.AttributeID != sessionKeeper.Session.UserID;
      else
        calendarItem.ReadOnly = objectInfo.ObjectTypeID != this._taskTypeID;
    }
  }

  /// <summary>Отрисовка элементов выбранного узла.</summary>
  /// <param name="collection">Коллекция объектов</param>
  /// <param name="conditions">Условия выбора объектов</param>
  private void DrawItems(
    IDBRecords collection,
    ConditionStructure[] conditions,
    HybridDictionary tags = null)
  {
    if (collection == null)
      return;
    DataTable dataTable = collection.Select(new DBRecordSetParams(conditions, new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDStartDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._attrIDFinishDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) this._ownerID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0)
    })
    {
      Tags = tags
    });
    if (dataTable.Rows.Count == 0)
      return;
    long userId = collection.Session.UserID;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      object obj1 = row[0];
      if (obj1 != null && obj1 != DBNull.Value)
      {
        long result1 = 0;
        if (long.TryParse(obj1.ToString(), out result1) && result1 != 0L)
        {
          DateTime result2 = DateTime.MinValue;
          DateTime result3 = DateTime.MinValue;
          object obj2 = row[3];
          object obj3 = row[4];
          if (obj2 != null && obj2 != DBNull.Value && obj3 != null && obj3 != DBNull.Value && DateTime.TryParse(obj2.ToString(), out result2) && DateTime.TryParse(obj3.ToString(), out result3))
          {
            object obj4 = row[1];
            int result4 = -1;
            if (obj4 != null && obj4 != DBNull.Value && int.TryParse(obj4.ToString(), out result4) && result4 != -1)
            {
              object obj5 = row[2];
              string caption = obj5 == null || obj5 == DBNull.Value ? string.Empty : obj5.ToString();
              CalendarItem calendarItem = this._scheduler.CreateItem(result1, result2, result3, caption, this.GetImageByTypeID(result4), true);
              if (calendarItem != null)
              {
                object obj6 = row[5];
                long result5 = 0;
                calendarItem.ReadOnly = obj6 == null || obj6 == DBNull.Value || !long.TryParse(obj6.ToString(), out result5) || result5 == 0L || result4 != this._taskTypeID || result5 != userId;
              }
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="collection"></param>
  private void DrawRepeatItems(IDBRecords collection)
  {
    if (collection == null)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad015d3-306c-11d8-b4e9-00304f19f545");
    ConditionStructure[] joinedConditions = new ConditionStructure[2]
    {
      new ConditionStructure(this._attrIDStartDate, RelationalOperators.LessOrEqual, (object) this._calendar.SelectionEnd, LogicalOperators.AND, 1, true),
      new ConditionStructure(attributeTypeId, RelationalOperators.NotEqual, (object) Convert.ToString(0), LogicalOperators.NONE, -1, false)
    };
    object[] columns = new object[6]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION,
      (object) attributeTypeId,
      (object) this._attrIDStartDate,
      (object) this._attrIDFinishDate,
      (object) this._ownerID
    };
    DataTable dataTable = collection.Select(new DBRecordSetParams(ConditionStructure.Join(joinedConditions, OrganizerTaskNode.DefaultConditions), columns)
    {
      Contents = new ColumnContents[6]
      {
        ColumnContents.ID,
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.ID
      }
    });
    if (dataTable.Rows.Count == 0)
      return;
    List<Repetition> repetitionList = new List<Repetition>((IEnumerable<Repetition>) new Repetition[4]
    {
      Repetition.Daily,
      Repetition.Weekly,
      Repetition.Monthly,
      Repetition.Yearly
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (Repetition repetition in repetitionList)
      {
        foreach (DataRow dataRow in dataTable.Select($"[{dataTable.Columns[2].ColumnName}]={(int) repetition}"))
        {
          DateTime result1 = DateTime.MinValue;
          DateTime result2 = DateTime.MinValue;
          if (dataRow[3] != DBNull.Value && dataRow[3] != null && dataRow[4] != DBNull.Value && dataRow[4] != null && DateTime.TryParse(dataRow[3].ToString(), out result1) && DateTime.TryParse(dataRow[4].ToString(), out result2))
          {
            long result3 = 0;
            if (dataRow[0] != DBNull.Value && dataRow[0] != null && long.TryParse(dataRow[0].ToString(), out result3))
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(result3);
              if (!objectInfo.Empty)
              {
                string caption = dataRow[1] == DBNull.Value || dataRow[1] == null ? string.Empty : dataRow[1].ToString();
                CalendarItem calendarItem = this._scheduler.CreateItem(result3, result1, result2, caption, this.GetImageByTypeID(this._taskTypeID), repetition);
                if (calendarItem != null)
                {
                  if (dataRow[5] == DBNull.Value || dataRow[5] == null)
                  {
                    calendarItem.ReadOnly = true;
                  }
                  else
                  {
                    long result4 = 0;
                    calendarItem.ReadOnly = !long.TryParse(dataRow[5].ToString(), out result4) || objectInfo.ObjectTypeID != this._taskTypeID || result4 != sessionKeeper.Session.UserID;
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// Получить изображение для указанного типа объекта.
  /// Если изображение отсутствует в списке, то оно ищется с помощью Statics.IconSrv.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объетка</param>
  /// <returns>Изображение</returns>
  private Image GetImageByTypeID(int objTypeID)
  {
    if (objTypeID == -1)
      return (Image) null;
    if (!this._images.ContainsKey(objTypeID))
    {
      Icon icon = Statics.IconSrv.GetIcon(4, objTypeID);
      if (icon == null)
        return (Image) null;
      this._images.Add(objTypeID, icon);
    }
    return (Image) this._images[objTypeID].ToBitmap();
  }

  /// <summary>Инициялизация элементов планировщика.</summary>
  private void InitializeSchedulerItems()
  {
    this._scheduler.Items.Clear();
    if (this._viewsManager.Visible)
      this.OpenEmbeddedViews();
    DateTime selectionBegin = this._calendar.SelectionBegin;
    ConditionStructure conditionStructure1 = new ConditionStructure(this._attrIDStartDate, RelationalOperators.LessOrEqual, (object) this._calendar.SelectionEnd, LogicalOperators.AND, 1, true);
    ConditionStructure conditionStructure2 = new ConditionStructure(this._attrIDFinishDate, RelationalOperators.GreaterOrEqual, (object) selectionBegin, LogicalOperators.NONE, -1, true);
    conditionStructure1.AttributeSource = AttributeSourceTypes.Object;
    conditionStructure2.AttributeSource = AttributeSourceTypes.Object;
    ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    };
    if (!(ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service) || this._nodeID == null)
      return;
    if (this._nodeID.CategoryID != Intermech.Navigator.Consts.OrganizerRootNodeTypeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._nodeType == OrganizerCalendarView.NodeType.IsOther)
        {
          OrganizerChildNodeDescriptor descriptor = service.GetDescriptor(this._nodeID.CategoryID);
          if (descriptor == null)
            return;
          this.DrawItems(descriptor.GetCollection(sessionKeeper.Session), ConditionStructure.Join(conditionStructureArray, descriptor.Conditions), descriptor.Tags);
        }
        else
        {
          IDBRecords objectCollection = (IDBRecords) sessionKeeper.Session.GetObjectCollection(this._nodeID.TypeID);
          ConditionStructure[] joinedConditions = ConditionStructure.Join(new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad015d3-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) Convert.ToString(0), LogicalOperators.NONE, 0, false), conditionStructureArray);
          this.DrawItems(objectCollection, ConditionStructure.Join(joinedConditions, OrganizerTaskNode.DefaultConditions));
          this.DrawRepeatItems(objectCollection);
        }
      }
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRecords objectCollection = (IDBRecords) sessionKeeper.Session.GetObjectCollection(this._taskTypeID);
        if (objectCollection != null)
        {
          ConditionStructure[] joinedConditions = ConditionStructure.Join(new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad015d3-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) Convert.ToString(0), LogicalOperators.NONE, 0, false), conditionStructureArray);
          this.DrawItems(objectCollection, ConditionStructure.Join(joinedConditions, OrganizerTaskNode.DefaultConditions));
          this.DrawRepeatItems(objectCollection);
        }
        DescriptorCollection descriptors = service.Descriptors;
        for (int index = 0; index < descriptors.Count; ++index)
        {
          OrganizerChildNodeDescriptor childNodeDescriptor = descriptors[index] as OrganizerChildNodeDescriptor;
          INodeID recordNodeId = childNodeDescriptor.GetRecordNodeID();
          if (recordNodeId != null && recordNodeId.TypeID != -1)
            this.DrawItems(childNodeDescriptor.GetCollection(sessionKeeper.Session), ConditionStructure.Join(conditionStructureArray, childNodeDescriptor.Conditions), childNodeDescriptor.Tags);
        }
      }
    }
  }

  /// <summary>
  /// Загрузить сохраненные данные (на данный момент это размер панели дополнительного вида).
  /// </summary>
  private void LoadConfiguration()
  {
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    service.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(OrganizerCalendarView.OnConfiguration_BeforeSave);
    IConfiguration configuration1 = service.Open(nameof (OrganizerCalendarView));
    if (configuration1 == null)
      return;
    IConfiguration configuration2 = configuration1.Open("PageViewsManager");
    if (configuration2 == null || !configuration2.HasProperty("PanelSize"))
      return;
    string property = configuration2.GetProperty("PanelSize");
    if (string.IsNullOrEmpty(property))
      return;
    try
    {
      OrganizerCalendarView._embeddedViewsPanelSize = (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) property);
      this._viewsManager.Height = OrganizerCalendarView._embeddedViewsPanelSize.Height;
    }
    catch
    {
    }
    finally
    {
      OrganizerCalendarView._savedConfiguration = false;
    }
  }

  /// <summary>Выводит на экран панель с дополнительными видами.</summary>
  private void OpenEmbeddedViews()
  {
    List<CalendarItem> selectedItems = this._scheduler.SelectedItems;
    if (selectedItems.Count == 1 && !selectedItems[0].BaseItem)
    {
      string caption = LocalizationHolder.rm.GetString("Organizer_Name");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Organizer_RepetitionItem_Msg"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    this._splitterH.Visible = this._viewsManager.Visible = true;
    this._viewsManager.Height = this._viewsManager.Height == 0 || this._viewsManager.Height > this.Height - 50 ? this.Height / 2 : this._viewsManager.Height;
    this._viewsManager.Height = this._viewsManager.Height < 50 ? 50 : this._viewsManager.Height;
    this._viewsManager.ActiveViewPageChanged -= new EventHandler(this.On_viewsManager_ActiveViewPageChanged);
    try
    {
      List<long> longList;
      if (selectedItems.Count == 0)
        longList = new List<long>() { -1L };
      else
        longList = new List<long>(selectedItems.Count);
      foreach (CalendarItem calendarItem in selectedItems)
      {
        if (!longList.Contains(calendarItem.ObjectID))
          longList.Add(calendarItem.ObjectID);
      }
      this._viewsManager.UpdateViews(ObjectExtensions.GetItems(longList.ToArray()), true);
      string str = this._viewsManager.ActiveViewPage == null || this._viewsManager.ActiveViewPage.View == null ? this._defaultText : this._viewsManager.ActiveViewPage.View.Caption;
      this._biViewNames.Image = (Image) null;
      this._biViewNames.Items.Clear();
      if (this._viewsManager.ViewPages.Count == 0)
        return;
      for (int index = 0; index < this._viewsManager.ViewPages.Count; ++index)
      {
        if (this._viewsManager.ViewPages[index].ViewDescription != null)
        {
          this._biViewNames.Items.Add(this._viewsManager.ViewPages[index].ViewDescription.Caption, new EventHandler(this.OnViewNamesItem_Click));
          this._biViewNames.Items[index].ImageIndex = this._viewsManager.ViewPages[index].ViewDescription.ImageIndex;
        }
        else
        {
          this._biViewNames.Items.Add(this._viewsManager.ViewPages[index].View.Caption, new EventHandler(this.OnViewNamesItem_Click));
          this._biViewNames.Items[index].ImageIndex = this._viewsManager.ViewPages[index].View.ImageIndex;
        }
        this._biViewNames.Items[index].Checked = this._biViewNames.Items[index].Text == str;
      }
      this._biViewNames.ImageIndex = this._viewsManager.ActiveViewPage.View.ImageIndex;
      this._biViewNames.Text = this._viewsManager.ActiveViewPage.View.Caption;
      this._biViewNames.Checked = true;
    }
    finally
    {
      this._viewsManager.ActiveViewPageChanged += new EventHandler(this.On_viewsManager_ActiveViewPageChanged);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ReadCalendarSettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectID = 0;
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(sessionKeeper.Session.UserID, false);
      if (dbObject1 != null)
      {
        IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(SystemGUIDs.attributeCalendar);
        if (attributeByGuid != null && attributeByGuid.AsInteger != 0L)
          objectID = attributeByGuid.AsInteger;
      }
      IDBObject dbObject2 = objectID == 0L ? sessionKeeper.Session.GetObject(new Guid("cad01582-306c-11d8-b4e9-00304f19f545"), false) : sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject2 == null)
        return;
      this._calendarSettings = (ServicesManager.GetService(typeof (ICalendarsService)) as ICalendarsService).GetCalendar(dbObject2.ObjectID, sessionKeeper.Session);
      this._calendar.CalendarSettings = this._calendarSettings;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveConfiguration()
  {
    OrganizerCalendarView.OnConfiguration_BeforeSave(ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager);
  }

  /// <summary>Задание подсветки рабочих дней.</summary>
  private void SetHighlights()
  {
    if (this.DateSelectionMode == DateSelectionMode.Month || this._scheduler.DaysMode != CalendarDaysMode.Expanded)
      return;
    DateTime day = this._calendar.SelectionBegin;
    List<CalendarHighlightRange> calendarHighlightRangeList = new List<CalendarHighlightRange>();
    TimeSpan ts = new TimeSpan(24, 0, 0);
    for (; day < this._calendar.SelectionEnd; day = day.AddDays(1.0))
    {
      ICalendarDay dayByDate = this._calendarSettings.GetDayByDate(day);
      if (dayByDate.DayType != DayType.Holiday && dayByDate.WorkTimePeriods != null)
      {
        foreach (IWorkTimePeriod workTimePeriod in (IEnumerable<IWorkTimePeriod>) dayByDate.WorkTimePeriods)
        {
          TimeSpan startTime = new TimeSpan(workTimePeriod.StartHours, workTimePeriod.StartMinutes < 30 ? 0 : 30, 0);
          TimeSpan endTime = new TimeSpan(workTimePeriod.FinishHours, workTimePeriod.FinishMinutes < 30 ? 0 : 30, 0);
          calendarHighlightRangeList.Add(new CalendarHighlightRange(day.DayOfWeek, startTime, endTime));
          if (!(startTime >= ts))
            ts = startTime;
        }
      }
    }
    this._scheduler.HighlightRanges = calendarHighlightRangeList.ToArray();
    this._scheduler.SetTimeUnit(ts);
  }

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

  public new void Refresh() => this.InitializeSchedulerItems();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    this.ReleaseServices();
    if (this._defaultImg != null)
      this._defaultImg.Dispose();
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerCalendarView));
    CalendarHighlightRange calendarHighlightRange1 = new CalendarHighlightRange();
    CalendarHighlightRange calendarHighlightRange2 = new CalendarHighlightRange();
    CalendarHighlightRange calendarHighlightRange3 = new CalendarHighlightRange();
    CalendarHighlightRange calendarHighlightRange4 = new CalendarHighlightRange();
    CalendarHighlightRange calendarHighlightRange5 = new CalendarHighlightRange();
    this._tbViewBar = new Intermech.Bars.ToolBar();
    this._biRefresh = new ButtonItem();
    this._biViewNames = new DropDownMenuItem();
    this._biCreateTask = new ButtonItem();
    this._biHelp = new ButtonItem();
    this._splitterV = new Splitter();
    this._scheduler = new Scheduler();
    this._navBar = new NavigationBar();
    this._calendarBand = new NavigationBand(this.components);
    this._calendar = new CalendarView();
    this._splitterH = new CollapsibleSplitter();
    this._viewsManager = new PageViewsManager();
    this._navBar.BeginInit();
    this._navBar.SuspendLayout();
    this._calendarBand.SuspendLayout();
    this.SuspendLayout();
    this._tbViewBar.FullMenus = true;
    this._tbViewBar.Guid = new Guid("2337b74f-5d86-4565-809f-c0fa244e17e8");
    this._tbViewBar.Hidden = false;
    this._tbViewBar.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this._biRefresh,
      (ToolbarItemBase) this._biViewNames,
      (ToolbarItemBase) this._biCreateTask,
      (ToolbarItemBase) this._biHelp
    });
    componentResourceManager.ApplyResources((object) this._tbViewBar, "_tbViewBar");
    this._tbViewBar.Name = "_tbViewBar";
    this._tbViewBar.Overflow = ToolBarOverflow.Wrap;
    componentResourceManager.ApplyResources((object) this._biRefresh, "_biRefresh");
    this._biRefresh.Click += new EventHandler(this.On_biRefresh_Click);
    this._biViewNames.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._biViewNames, "_biViewNames");
    this._biViewNames.Image = (Image) componentResourceManager.GetObject("_biViewNames.Image");
    this._biViewNames.ShowText = true;
    this._biViewNames.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.On_biViewNames_BeforePopup);
    this._biViewNames.Click += new EventHandler(this.On_biViewNames_Click);
    this._biCreateTask.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._biCreateTask, "_biCreateTask");
    this._biCreateTask.Enabled = false;
    this._biCreateTask.Icon = (Icon) componentResourceManager.GetObject("_biCreateTask.Icon");
    this._biCreateTask.ShowText = true;
    this._biCreateTask.Click += new EventHandler(this.On_CreateItem);
    componentResourceManager.ApplyResources((object) this._biHelp, "_biHelp");
    this._biHelp.Image = (Image) componentResourceManager.GetObject("_biHelp.Image");
    this._biHelp.Visible = false;
    componentResourceManager.ApplyResources((object) this._splitterV, "_splitterV");
    this._splitterV.Name = "_splitterV";
    this._splitterV.TabStop = false;
    componentResourceManager.ApplyResources((object) this._scheduler, "_scheduler");
    this._scheduler.ExcludedDays = (Dictionary<int, List<int>>) componentResourceManager.GetObject("_scheduler.ExcludedDays");
    calendarHighlightRange1.DayOfWeek = DayOfWeek.Monday;
    calendarHighlightRange1.EndTime = TimeSpan.Parse("17:00:00");
    calendarHighlightRange1.StartTime = TimeSpan.Parse("08:00:00");
    calendarHighlightRange2.DayOfWeek = DayOfWeek.Tuesday;
    calendarHighlightRange2.EndTime = TimeSpan.Parse("17:00:00");
    calendarHighlightRange2.StartTime = TimeSpan.Parse("08:00:00");
    calendarHighlightRange3.DayOfWeek = DayOfWeek.Wednesday;
    calendarHighlightRange3.EndTime = TimeSpan.Parse("17:00:00");
    calendarHighlightRange3.StartTime = TimeSpan.Parse("08:00:00");
    calendarHighlightRange4.DayOfWeek = DayOfWeek.Thursday;
    calendarHighlightRange4.EndTime = TimeSpan.Parse("17:00:00");
    calendarHighlightRange4.StartTime = TimeSpan.Parse("08:00:00");
    calendarHighlightRange5.DayOfWeek = DayOfWeek.Friday;
    calendarHighlightRange5.EndTime = TimeSpan.Parse("17:00:00");
    calendarHighlightRange5.StartTime = TimeSpan.Parse("08:00:00");
    this._scheduler.HighlightRanges = new CalendarHighlightRange[5]
    {
      calendarHighlightRange1,
      calendarHighlightRange2,
      calendarHighlightRange3,
      calendarHighlightRange4,
      calendarHighlightRange5
    };
    this._scheduler.Name = "_scheduler";
    this._scheduler.DayHeaderClick += new SchedulerDayEventHandler(this.On_scheduler_DayHeaderClick);
    this._scheduler.HeaderButtonClick += new Scheduler.CalendarHeaderButtonClickEventHandler(this.On_scheduler_HeaderButtonClick);
    this._scheduler.HeaderRadioButtonClick += new Scheduler.CalendarHeaderButtonClickEventHandler(this.On_scheduler_HeaderRadioButtonClick);
    this._scheduler.ItemCaptionEdited += new SchedulerItemCancelEventHandler(this.On_scheduler_ItemCaptionEdited);
    this._scheduler.ItemDatesChanged += new SchedulerItemEventHandler(this.On_scheduler_ItemDatesChanged);
    this._scheduler.ItemDoubleClick += new SchedulerItemEventHandler(this.On_scheduler_ItemDoubleClick);
    this._scheduler.ItemsDeleted += new SchedulerItemsEventHandler(this.On_scheduler_ItemsDeleted);
    this._scheduler.ItemsDeleting += new SchedulerItemsCancelEventHandler(this.On_scheduler_ItemsDeleting);
    this._scheduler.ItemsSelectionChanged += new SchedulerItemsEventHandler(this.On_scheduler_ItemsSelectionChanged);
    this._scheduler.SchedulerDoubleClick += new EventHandler(this.On_CreateItem);
    this._scheduler.ScrollMonth += new SchedulerDatesEventHandler(this.On_scheduler_ScrollMonth);
    this._scheduler.MouseUp += new MouseEventHandler(this.On_scheduler_MouseUp);
    this._navBar.ActiveBand = this._calendarBand;
    this._navBar.Controls.Add((Control) this._calendarBand);
    componentResourceManager.ApplyResources((object) this._navBar, "_navBar");
    this._navBar.FooterHeight = 0;
    this._navBar.Name = "_navBar";
    this._calendarBand.Controls.Add((Control) this._calendar);
    componentResourceManager.ApplyResources((object) this._calendarBand, "_calendarBand");
    this._calendarBand.Name = "_calendarBand";
    this._calendarBand.Order = 0;
    this._calendarBand.OriginalOrder = 0;
    this._calendar.CalendarSettings = (ICalendar) null;
    componentResourceManager.ApplyResources((object) this._calendar, "_calendar");
    this._calendar.ItemPadding = new Padding(2);
    this._calendar.MaxSelectionCount = 35;
    this._calendar.Name = "_calendar";
    this._calendar.SelectionChanged += new EventHandler(this.On_calendar_SelectionChanged);
    this._splitterH.AnimationDelay = 20;
    this._splitterH.AnimationStep = 20;
    this._splitterH.BorderStyle3D = Border3DStyle.Etched;
    this._splitterH.ControlToHide = (Control) this._viewsManager;
    componentResourceManager.ApplyResources((object) this._splitterH, "_splitterH");
    this._splitterH.ExpandParentForm = false;
    this._splitterH.Name = "spViewsManager";
    this._splitterH.TabStop = false;
    this._splitterH.UseAnimations = false;
    this._splitterH.VisualStyle = VisualStyles.Mozilla;
    this._viewsManager.ActiveViewPage = (IViewPage) null;
    this._viewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this._viewsManager, "_viewsManager");
    this._viewsManager.HeaderAlignment = Intermech.Docking.TabAlignment.Bottom;
    this._viewsManager.Name = "_viewsManager";
    this._viewsManager.ActiveViewPageChanged += new EventHandler(this.On_viewsManager_ActiveViewPageChanged);
    this._viewsManager.Resize += new EventHandler(this.On_viewsManager_Resize);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._scheduler);
    this.Controls.Add((Control) this._splitterV);
    this.Controls.Add((Control) this._navBar);
    this.Controls.Add((Control) this._tbViewBar);
    this.Controls.Add((Control) this._splitterH);
    this.Controls.Add((Control) this._viewsManager);
    this.DoubleBuffered = true;
    this.Name = nameof (OrganizerCalendarView);
    this._navBar.EndInit();
    this._navBar.ResumeLayout(false);
    this._calendarBand.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Тип узла, выделенного в дереве навигатора.</summary>
  private enum NodeType
  {
    IsRoot,
    IsTask,
    IsOther,
  }
}
