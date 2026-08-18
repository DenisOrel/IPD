// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.TimeTable.TimedEventsShedulerForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DatabaseConfigurator.TimedEventsSheduler;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.DatabaseConfigurator.TimeTable;

public class TimedEventsShedulerForm : Form
{
  private readonly string TASK = nameof (TASK);
  private readonly string TIMED_EVENT_NAME = nameof (TIMED_EVENT_NAME);
  private readonly string EVENT_KIND = nameof (EVENT_KIND);
  private readonly string SCHEDULE = nameof (SCHEDULE);
  private readonly string SERVER_NAME = nameof (SERVER_NAME);
  private readonly string IMMEDIATE_RUN = nameof (IMMEDIATE_RUN);
  private readonly string EVENT_ID = nameof (EVENT_ID);
  private readonly string PREVIOUS_DATE = nameof (PREVIOUS_DATE);
  private readonly string NEXT_DATE = nameof (NEXT_DATE);
  private readonly string ERROR_MESSAGE = nameof (ERROR_MESSAGE);
  private readonly string STATUS = nameof (STATUS);
  private long _currentEventID;
  private Dictionary<Guid, string> eventHandlersInfo = new Dictionary<Guid, string>();
  private BackgroundWorker worker;
  private IContainer components;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem btnAddTask;
  private ButtonItem btnEditTask;
  private ButtonItem btnStartTask;
  private ImageList ilTimedEvent;
  private ButtonItem btnDeleteTask;
  private ToolTip ttTimedEvents;
  private Button btnOK;
  private iGrid igEvents;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private iGCellStyle iGrid1Col2CellStyle;
  private iGColHdrStyle iGrid1Col2ColHdrStyle;
  private iGCellStyle iGrid1Col3CellStyle;
  private iGColHdrStyle iGrid1Col3ColHdrStyle;
  private iGCellStyle iGrid1Col4CellStyle;
  private iGColHdrStyle iGrid1Col4ColHdrStyle;
  private iGCellStyle iGrid1Col5CellStyle;
  private iGColHdrStyle iGrid1Col5ColHdrStyle;
  private iGCellStyle iGrid1Col6CellStyle;
  private iGColHdrStyle iGrid1Col6ColHdrStyle;
  private iGCellStyle iGrid1Col7CellStyle;
  private iGColHdrStyle iGrid1Col7ColHdrStyle;
  private iGCellStyle iGrid1Col8CellStyle;
  private iGColHdrStyle iGrid1Col8ColHdrStyle;
  private iGCellStyle iGrid1Col9CellStyle;
  private iGColHdrStyle iGrid1Col9ColHdrStyle;
  private ButtonItem btnUpdate;
  private Panel panel1;
  private Button btnSetPrimaryServer;
  private ComboBox cbPrimaryQueueServer;
  private Label label1;
  private MenuBar menuBar1;
  private ContextMenuBarItem taskContextMenu;
  private MenuButtonItem miEdit;
  private MenuButtonItem miRun;
  private MenuButtonItem miDelete;
  private MenuButtonItem miRefresh;
  private MenuButtonItem miCreateTask;
  private iGCellStyle igEventsCol10CellStyle;
  private iGColHdrStyle igEventsCol10ColHdrStyle;

  public TimedEventsShedulerForm()
  {
    this.InitializeComponent();
    this.worker = new BackgroundWorker();
    this.worker.WorkerSupportsCancellation = true;
    this.worker.WorkerReportsProgress = true;
    this.worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBar1.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuBar1.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
    this.LoadTimeEvents();
  }

  private void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBar1.Renderer = renderer;
    this.menuBar1.Renderer = renderer;
  }

  private void LoadTimeEvents()
  {
    this.igEvents.Rows.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITimedEventsSheduler customService = (ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler));
      if (customService == null)
        return;
      ScheduledEventHandlerInfo[] scheduledEventHandlers = customService.GetScheduledEventHandlers();
      this.eventHandlersInfo.Clear();
      for (int index = 0; index < scheduledEventHandlers.Length; ++index)
        this.eventHandlersInfo.Add(scheduledEventHandlers[index].ServiceGuid, scheduledEventHandlers[index].EventName);
      DataTable eventsTable = customService.GetEventsTable(sessionKeeper.Session.SessionGUID);
      for (int index = 0; index < eventsTable.Rows.Count; ++index)
      {
        TimedEventProperties properties = new TimedEventProperties(eventsTable.Rows[index]);
        iGRow iRow = this.igEvents.Rows.Add();
        this.FillRow(iRow, properties);
        if (Convert.ToInt64(iRow.Cells[this.EVENT_ID].Value) == this._currentEventID)
          this.igEvents.SetCurRow(index);
      }
      this.igEvents.Sort();
      for (int index = 0; index < this.igEvents.Cols.Count; ++index)
        this.igEvents.Cols[index].AutoWidth(true);
      if (this.worker.IsBusy)
        return;
      this.cbPrimaryQueueServer.Items.Clear();
      string primaryServer = customService.GetPrimaryServer(sessionKeeper.Session.SessionGUID);
      if (primaryServer != string.Empty)
        this.cbPrimaryQueueServer.Items.Add((object) string.Empty);
      this.cbPrimaryQueueServer.Items.Add((object) primaryServer);
      this.cbPrimaryQueueServer.SelectedIndex = this.cbPrimaryQueueServer.Items.Count - 1;
      this.btnSetPrimaryServer.Enabled = false;
      this.worker.RunWorkerAsync((object) primaryServer);
    }
  }

  private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.cbPrimaryQueueServer.Items.Add((object) e.UserState.ToString());
  }

  private void worker_DoWork(object sender, DoWorkEventArgs e)
  {
    foreach (DirectoryEntry child1 in new DirectoryEntry("WinNT:").Children)
    {
      if (this.worker.CancellationPending)
      {
        e.Cancel = true;
        break;
      }
      child1.Children.SchemaFilter.Add("computer");
      foreach (DirectoryEntry child2 in child1.Children)
      {
        if (this.worker.CancellationPending)
        {
          e.Cancel = true;
          break;
        }
        if (child2.Name != "Schema" && e.Argument.ToString() != child2.Name)
          this.worker.ReportProgress(0, (object) child2.Name);
      }
    }
  }

  private void FillRow(iGRow iRow, TimedEventProperties properties)
  {
    iRow.Tag = (object) properties;
    iRow.Cells[this.EVENT_ID].Value = (object) properties.KeyID;
    iRow.Cells[this.TIMED_EVENT_NAME].Value = (object) properties.Name;
    iRow.Cells[this.TASK].Value = this.eventHandlersInfo.ContainsKey(properties.ServiceGuid) ? (object) this.eventHandlersInfo[properties.ServiceGuid] : (object) "Событие не зарегистрировано на сервере";
    iRow.Cells[this.IMMEDIATE_RUN].Value = (object) (bool) (properties.EventKind == TimedEventKinds.Once ? 0 : (properties.ImmediateRun ? 1 : 0));
    iRow.Cells[this.SERVER_NAME].Value = (object) properties.ServerName;
    iRow.Cells[this.EVENT_KIND].Value = (object) EnumDescConverter.GetEnumDescription((Enum) properties.EventKind);
    iRow.Cells[this.SCHEDULE].Value = (object) this.FormSchedule(properties.Schedule, properties.EventKind, properties.StartDate);
    bool flag = properties.PreviousDate != DateTime.MinValue;
    iGCell cell1 = iRow.Cells[this.PREVIOUS_DATE];
    DateTime localTime;
    string str1;
    if (flag)
    {
      localTime = properties.PreviousDate.ToLocalTime();
      str1 = localTime.ToString("f");
    }
    else
      str1 = "Задача не выполнялась";
    cell1.Value = (object) str1;
    iGCell cell2 = iRow.Cells[this.NEXT_DATE];
    string str2;
    if (properties.EventKind != TimedEventKinds.Once || !(properties.StartDate < DateTime.UtcNow))
    {
      localTime = properties.StartDate.ToLocalTime();
      str2 = localTime.ToString("f");
    }
    else
      str2 = "Дата запуска задачи прошла";
    cell2.Value = (object) str2;
    iRow.Cells[this.ERROR_MESSAGE].Value = (object) properties.ErrorMessage;
    iRow.Cells[this.STATUS].Value = (object) properties.Status;
    iRow.AutoHeight();
  }

  private string FormSchedule(string schedule, TimedEventKinds kind, DateTime startDate)
  {
    string str = schedule;
    switch (kind)
    {
      case TimedEventKinds.Once:
        str = startDate.ToLocalTime().ToString("f");
        break;
      case TimedEventKinds.Hourly:
        str = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_265"), (object) schedule);
        break;
      case TimedEventKinds.Daily:
        str = "в " + DateTime.Parse(schedule, (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime().ToString("t");
        break;
      case TimedEventKinds.Weekly:
        string[] strArray1 = schedule.Split(',');
        str = $"в {DateTime.Parse(strArray1[0], (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime().ToString("t")}\n";
        for (int index = 1; index < strArray1.Length; ++index)
          str = $"{str}{DateTimeFormatInfo.CurrentInfo.GetShortestDayName((DayOfWeek) Convert.ToInt32(strArray1[index]))};";
        break;
      case TimedEventKinds.Monthly:
        string[] lst2 = schedule.Split(',');
        str = $"в {DateTime.Parse(lst2[0], (IFormatProvider) CultureInfo.InvariantCulture).ToLocalTime().ToString("t")}\n" + DateStringWithPeriod.ConvertToString(lst2);
        break;
      case TimedEventKinds.Yearly:
        string[] strArray2 = schedule.Split(',');
        DateTime dateTime = DateTime.UtcNow;
        dateTime = new DateTime(dateTime.Year + 1, Convert.ToInt32(strArray2[3]), Convert.ToInt32(strArray2[2]), Convert.ToInt32(strArray2[0]), Convert.ToInt32(strArray2[1]), 0, DateTimeKind.Utc);
        DateTime localTime = dateTime.ToLocalTime();
        str = localTime.ToString("dd MMMM ") + localTime.ToString("t");
        break;
      case TimedEventKinds.Minutely:
        str = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_264"), (object) schedule);
        break;
    }
    return str;
  }

  private void btnAddTask_Click(object sender, EventArgs e) => this.AddTask();

  private void AddTask()
  {
    using (TimedEventPropertiesForm eventPropertiesForm = new TimedEventPropertiesForm(this.eventHandlersInfo))
    {
      if (eventPropertiesForm.ShowDialog() != DialogResult.OK)
        return;
      this.FillRow(this.igEvents.Rows.Add(), eventPropertiesForm.properties);
    }
  }

  private void miCreateTask_Click(object sender, EventArgs e) => this.AddTask();

  private void btnEditTask_Click(object sender, EventArgs e) => this.EditTask();

  private void EditTask()
  {
    iGRow curRow = this.igEvents.CurRow;
    using (TimedEventPropertiesForm eventPropertiesForm = new TimedEventPropertiesForm(FormMode.Edit, (TimedEventProperties) curRow.Tag, this.eventHandlersInfo))
    {
      if (eventPropertiesForm.ShowDialog() != DialogResult.OK)
        return;
      this.FillRow(curRow, eventPropertiesForm.properties);
    }
  }

  private void igEvents_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
  {
    this.EditTask();
  }

  private void miEdit_Click(object sender, EventArgs e) => this.EditTask();

  private void btnDeleteTask_Click(object sender, EventArgs e)
  {
    this.DeleteTask();
    if (this.igEvents.Rows.Count != 0)
      return;
    this._currentEventID = 0L;
  }

  private void DeleteTask()
  {
    iGRow curRow = this.igEvents.CurRow;
    TimedEventProperties tag = (TimedEventProperties) curRow.Tag;
    if (MessageBox.Show("Удалить задачу из списка?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITimedEventsSheduler customService = (ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler));
      if (customService == null)
        return;
      customService.DeleteEvents(sessionKeeper.Session.SessionGUID, new int[1]
      {
        tag.KeyID
      });
      this.igEvents.Rows.RemoveAt(curRow.Index);
    }
  }

  private void miDelete_Click(object sender, EventArgs e) => this.DeleteTask();

  private void btnStartTask_Click(object sender, EventArgs e) => this.RunTask();

  private void RunTask()
  {
    TimedEventProperties tag = (TimedEventProperties) this.igEvents.CurRow.Tag;
    if (MessageBox.Show("Запустить задачу?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler)))?.RunEvent(sessionKeeper.Session.SessionGUID, tag.KeyID);
    this.LoadTimeEvents();
  }

  private void miRun_Click(object sender, EventArgs e) => this.RunTask();

  private void ButtonsEnabled()
  {
    ButtonItem btnDeleteTask = this.btnDeleteTask;
    ButtonItem btnEditTask = this.btnEditTask;
    bool flag1;
    this.btnStartTask.Enabled = flag1 = this.igEvents.Rows.Count > 0 && this.igEvents.CurRow != null;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    btnEditTask.Enabled = num1 != 0;
    int num2 = flag2 ? 1 : 0;
    btnDeleteTask.Enabled = num2 != 0;
  }

  private void igEvents_CurRowChanged(object sender, EventArgs e)
  {
    this.ButtonsEnabled();
    if (this.igEvents.CurRow != null)
      this._currentEventID = Convert.ToInt64(this.igEvents.CurRow.Cells[this.EVENT_ID].Value);
    else
      this._currentEventID = 0L;
  }

  private void TimedEventsShedulerForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void TimedEventsShedulerForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.worker.IsBusy)
      this.worker.CancelAsync();
    FormStorage.SaveLayout((Control) this);
  }

  private void btnUpdate_Click(object sender, EventArgs e) => this.LoadTimeEvents();

  private void miRefresh_Click(object sender, EventArgs e) => this.LoadTimeEvents();

  private void btnSetPrimaryServer_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITimedEventsSheduler customService = (ITimedEventsSheduler) sessionKeeper.Session.GetCustomService(typeof (ITimedEventsSheduler));
      string text = this.cbPrimaryQueueServer.Text;
      bool flag = false;
      foreach (object obj in this.cbPrimaryQueueServer.Items)
      {
        if (text.Equals(obj.ToString()))
        {
          flag = true;
          break;
        }
      }
      if (!flag && MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_266"), LocalizationHolder.rm.GetString("DatabaseConfigurator_246"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        return;
      customService?.SetPrimaryServer(sessionKeeper.Session.SessionGUID, text);
      this.btnSetPrimaryServer.Enabled = false;
    }
  }

  private void cbPrimaryQueueServer_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btnSetPrimaryServer.Enabled = true;
  }

  private void igEvents_MouseClick(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.miDelete.Enabled = this.miEdit.Enabled = this.miRun.Enabled = this.igEvents.CurCell != null;
    this.taskContextMenu.Show((Control) this.igEvents, e.Location);
  }

  private void cbPrimaryQueueServer_TextChanged(object sender, EventArgs e)
  {
    if (string.IsNullOrWhiteSpace(this.cbPrimaryQueueServer.Text))
      this.btnSetPrimaryServer.Enabled = false;
    else
      this.btnSetPrimaryServer.Enabled = true;
  }

  private void igEvents_ColHdrMouseEnter(object sender, iGColHdrMouseEnterLeaveEventArgs e)
  {
    int colIndex = e.ColIndex;
    if (this.igEvents.Cols[colIndex].Text == null)
      return;
    this.ttTimedEvents.SetToolTip((Control) this.igEvents, this.igEvents.Cols[colIndex].Text.ToString());
  }

  private void igEvents_ColHdrMouseLeave(object sender, iGColHdrMouseEnterLeaveEventArgs e)
  {
    this.ttTimedEvents.RemoveAll();
  }

  protected override void Dispose(bool disposing)
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBar1.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuBar1.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TimedEventsShedulerForm));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    iGColPattern iGcolPattern4 = new iGColPattern();
    iGColPattern iGcolPattern5 = new iGColPattern();
    iGColPattern iGcolPattern6 = new iGColPattern();
    iGColPattern iGcolPattern7 = new iGColPattern();
    iGColPattern iGcolPattern8 = new iGColPattern();
    iGColPattern iGcolPattern9 = new iGColPattern();
    iGColPattern iGcolPattern10 = new iGColPattern();
    iGColPattern iGcolPattern11 = new iGColPattern();
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col5CellStyle = new iGCellStyle(true);
    this.iGrid1Col5ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col6CellStyle = new iGCellStyle(true);
    this.iGrid1Col6ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col7CellStyle = new iGCellStyle(true);
    this.iGrid1Col7ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col8CellStyle = new iGCellStyle(true);
    this.iGrid1Col8ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col2CellStyle = new iGCellStyle(true);
    this.iGrid1Col2ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col3CellStyle = new iGCellStyle(true);
    this.iGrid1Col3ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col4CellStyle = new iGCellStyle(true);
    this.iGrid1Col4ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col9CellStyle = new iGCellStyle(true);
    this.iGrid1Col9ColHdrStyle = new iGColHdrStyle(true);
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.ilTimedEvent = new ImageList(this.components);
    this.btnAddTask = new ButtonItem();
    this.btnDeleteTask = new ButtonItem();
    this.btnEditTask = new ButtonItem();
    this.btnStartTask = new ButtonItem();
    this.btnUpdate = new ButtonItem();
    this.ttTimedEvents = new ToolTip(this.components);
    this.btnOK = new Button();
    this.igEvents = new iGrid();
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.btnSetPrimaryServer = new Button();
    this.cbPrimaryQueueServer = new ComboBox();
    this.menuBar1 = new MenuBar();
    this.taskContextMenu = new ContextMenuBarItem();
    this.miCreateTask = new MenuButtonItem();
    this.miEdit = new MenuButtonItem();
    this.miDelete = new MenuButtonItem();
    this.miRun = new MenuButtonItem();
    this.miRefresh = new MenuButtonItem();
    this.igEventsCol10CellStyle = new iGCellStyle(true);
    this.igEventsCol10ColHdrStyle = new iGColHdrStyle(true);
    ((ISupportInitialize) this.igEvents).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.iGrid1Col0CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col1CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col5CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col6CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col7CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col8CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col2CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col3CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col3CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
    this.iGrid1Col3CellStyle.Type = iGCellType.Check;
    this.iGrid1Col4CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col9CellStyle.ReadOnly = iGBool.True;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("42adbd1f-cc14-49d1-8fe7-d73ad9394c8c");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.ilTimedEvent;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btnAddTask,
      (ToolbarItemBase) this.btnDeleteTask,
      (ToolbarItemBase) this.btnEditTask,
      (ToolbarItemBase) this.btnStartTask,
      (ToolbarItemBase) this.btnUpdate
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Name = "toolBar1";
    this.ilTimedEvent.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilTimedEvent.ImageStream");
    this.ilTimedEvent.TransparentColor = Color.Transparent;
    this.ilTimedEvent.Images.SetKeyName(0, "add.png");
    this.ilTimedEvent.Images.SetKeyName(1, "edit.png");
    this.ilTimedEvent.Images.SetKeyName(2, "delete.png");
    this.ilTimedEvent.Images.SetKeyName(3, "check.png");
    this.ilTimedEvent.Images.SetKeyName(4, "refresh.png");
    this.btnAddTask.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnAddTask, "btnAddTask");
    this.btnAddTask.ImageIndex = 0;
    this.btnAddTask.Click += new EventHandler(this.btnAddTask_Click);
    componentResourceManager.ApplyResources((object) this.btnDeleteTask, "btnDeleteTask");
    this.btnDeleteTask.Enabled = false;
    this.btnDeleteTask.ImageIndex = 2;
    this.btnDeleteTask.Click += new EventHandler(this.btnDeleteTask_Click);
    componentResourceManager.ApplyResources((object) this.btnEditTask, "btnEditTask");
    this.btnEditTask.Enabled = false;
    this.btnEditTask.ImageIndex = 1;
    this.btnEditTask.Click += new EventHandler(this.btnEditTask_Click);
    this.btnStartTask.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnStartTask, "btnStartTask");
    this.btnStartTask.Enabled = false;
    this.btnStartTask.ImageIndex = 3;
    this.btnStartTask.Click += new EventHandler(this.btnStartTask_Click);
    this.btnUpdate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnUpdate, "btnUpdate");
    this.btnUpdate.ImageIndex = 4;
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.igEvents, "igEvents");
    this.igEvents.AutoWidthColMode = iGAutoWidthColMode.Cells;
    iGcolPattern1.CellStyle = this.igEventsCol10CellStyle;
    iGcolPattern1.ColHdrStyle = this.igEventsCol10ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern2.AllowGrouping = false;
    iGcolPattern2.CellStyle = this.iGrid1Col0CellStyle;
    iGcolPattern2.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    iGcolPattern3.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern3.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern3, "iGColPattern3");
    iGcolPattern4.CellStyle = this.iGrid1Col5CellStyle;
    iGcolPattern4.ColHdrStyle = this.iGrid1Col5ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern4, "iGColPattern4");
    iGcolPattern5.CellStyle = this.iGrid1Col6CellStyle;
    iGcolPattern5.ColHdrStyle = this.iGrid1Col6ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern5, "iGColPattern5");
    iGcolPattern6.AllowGrouping = false;
    iGcolPattern6.CellStyle = this.iGrid1Col7CellStyle;
    iGcolPattern6.ColHdrStyle = this.iGrid1Col7ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern6, "iGColPattern6");
    iGcolPattern7.AllowGrouping = false;
    iGcolPattern7.CellStyle = this.iGrid1Col8CellStyle;
    iGcolPattern7.ColHdrStyle = this.iGrid1Col8ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern7, "iGColPattern7");
    iGcolPattern8.CellStyle = this.iGrid1Col2CellStyle;
    iGcolPattern8.ColHdrStyle = this.iGrid1Col2ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern8, "iGColPattern8");
    iGcolPattern9.CellStyle = this.iGrid1Col3CellStyle;
    iGcolPattern9.ColHdrStyle = this.iGrid1Col3ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern9, "iGColPattern9");
    iGcolPattern9.SortOrder = iGSortOrder.None;
    iGcolPattern9.SortType = iGSortType.None;
    iGcolPattern10.CellStyle = this.iGrid1Col4CellStyle;
    iGcolPattern10.ColHdrStyle = this.iGrid1Col4ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern10, "iGColPattern10");
    iGcolPattern11.CellStyle = this.iGrid1Col9CellStyle;
    iGcolPattern11.ColHdrStyle = this.iGrid1Col9ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern11, "iGColPattern11");
    this.igEvents.Cols.AddRange(new iGColPattern[11]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3,
      iGcolPattern4,
      iGcolPattern5,
      iGcolPattern6,
      iGcolPattern7,
      iGcolPattern8,
      iGcolPattern9,
      iGcolPattern10,
      iGcolPattern11
    });
    this.igEvents.GroupBox.BackColor = SystemColors.AppWorkspace;
    this.igEvents.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this.igEvents.GroupBox.HintForeColor = SystemColors.ControlText;
    this.igEvents.GroupBox.Text = componentResourceManager.GetString("igEvents.GroupBox.Text");
    this.igEvents.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this.igEvents.Header.Height = (int) componentResourceManager.GetObject("igEvents.Header.Height");
    this.igEvents.HScrollBar.Visibility = iGScrollBarVisibility.Always;
    this.igEvents.LayoutObject.Flags = iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this.igEvents.Name = "igEvents";
    this.igEvents.RowMode = true;
    this.igEvents.RowModeHasCurCell = true;
    this.igEvents.CellDoubleClick += new iGCellDoubleClickEventHandler(this.igEvents_CellDoubleClick);
    this.igEvents.ColHdrMouseEnter += new iGColHdrMouseEnterLeaveEventHandler(this.igEvents_ColHdrMouseEnter);
    this.igEvents.ColHdrMouseLeave += new iGColHdrMouseEnterLeaveEventHandler(this.igEvents_ColHdrMouseLeave);
    this.igEvents.CurRowChanged += new EventHandler(this.igEvents_CurRowChanged);
    this.igEvents.MouseClick += new MouseEventHandler(this.igEvents_MouseClick);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.btnSetPrimaryServer);
    this.panel1.Controls.Add((Control) this.cbPrimaryQueueServer);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btnSetPrimaryServer, "btnSetPrimaryServer");
    this.btnSetPrimaryServer.Name = "btnSetPrimaryServer";
    this.btnSetPrimaryServer.UseVisualStyleBackColor = true;
    this.btnSetPrimaryServer.Click += new EventHandler(this.btnSetPrimaryServer_Click);
    componentResourceManager.ApplyResources((object) this.cbPrimaryQueueServer, "cbPrimaryQueueServer");
    this.cbPrimaryQueueServer.FormattingEnabled = true;
    this.cbPrimaryQueueServer.Name = "cbPrimaryQueueServer";
    this.cbPrimaryQueueServer.SelectedIndexChanged += new EventHandler(this.cbPrimaryQueueServer_SelectedIndexChanged);
    this.cbPrimaryQueueServer.TextChanged += new EventHandler(this.cbPrimaryQueueServer_TextChanged);
    this.menuBar1.Guid = new Guid("5649158c-2aaa-4a90-bbd6-56df2403c666");
    this.menuBar1.Hidden = false;
    this.menuBar1.ImageList = this.ilTimedEvent;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.taskContextMenu
    });
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.taskContextMenu, "taskContextMenu");
    this.taskContextMenu.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.miCreateTask,
      (ToolbarItemBase) this.miEdit,
      (ToolbarItemBase) this.miDelete,
      (ToolbarItemBase) this.miRun,
      (ToolbarItemBase) this.miRefresh
    });
    this.taskContextMenu.MenuImageList = this.ilTimedEvent;
    this.taskContextMenu.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miCreateTask, "miCreateTask");
    this.miCreateTask.ImageIndex = 0;
    this.miCreateTask.ShowText = true;
    this.miCreateTask.Click += new EventHandler(this.miCreateTask_Click);
    componentResourceManager.ApplyResources((object) this.miEdit, "miEdit");
    this.miEdit.ImageIndex = 1;
    this.miEdit.ShowText = true;
    this.miEdit.Click += new EventHandler(this.miEdit_Click);
    componentResourceManager.ApplyResources((object) this.miDelete, "miDelete");
    this.miDelete.ImageIndex = 2;
    this.miDelete.ShowText = true;
    this.miDelete.Click += new EventHandler(this.miDelete_Click);
    this.miRun.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miRun, "miRun");
    this.miRun.ImageIndex = 3;
    this.miRun.ShowText = true;
    this.miRun.Click += new EventHandler(this.miRun_Click);
    this.miRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miRefresh, "miRefresh");
    this.miRefresh.ImageIndex = 4;
    this.miRefresh.ShowText = true;
    this.miRefresh.Click += new EventHandler(this.miRefresh_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnOK;
    this.Controls.Add((Control) this.igEvents);
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.toolBar1);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TimedEventsShedulerForm);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.TimedEventsShedulerForm_FormClosing);
    this.Load += new EventHandler(this.TimedEventsShedulerForm_Load);
    ((ISupportInitialize) this.igEvents).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
