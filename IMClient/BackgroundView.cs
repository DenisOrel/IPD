
// Type: IMClient.BackgroundView




using Intermech;
using Intermech.Bars;
using Intermech.Controls;
using Intermech.Controls.Grid;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace IMClient
{
    public class BackgroundView : DockControl, IBackgroundTaskView
    {
      private System.IServiceProvider _serviceProvider;
      private Intermech.Bars.ToolBar _toolBar;
      private ButtonItem btResume;
      private ButtonItem btPause;
      private ButtonItem btStop;
      private ListGrid _grid;
      private ButtonItem btCancel;
      private System.ComponentModel.Container components;
      private DockManager _dockManager;
      private bool _needHide;
      private NotificationEventHandler _globalNotifyHandler;

      private void InitializeData()
      {
        this.InitializeCustomServices();
        INamedImageList service1 = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        if (service1 != null)
        {
          this._grid.ImageList = service1.ImageList;
          this._toolBar.ImageList = service1.ImageList;
          this.TabImageIndex = service1.ImageIndex("imgBackground");
          this.btPause.ImageIndex = service1.ImageIndex("imgPause");
          this.btStop.ImageIndex = service1.ImageIndex("imgStop2");
          this.btResume.ImageIndex = service1.ImageIndex("imgStart");
        }
        IConfigurationManager service2 = (IConfigurationManager) this._serviceProvider.GetService(typeof (IConfigurationManager));
        if (service2 != null)
        {
          this.LoadConfiguration(service2);
          service2.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.SaveConfiguration);
        }
        this._dockManager = this._serviceProvider.GetService(typeof (DockManager)) as DockManager;
        this._grid.Items.Changed += new ChangedEventHandler(this.Items_Changed);
      }

      private void InitializeCustomServices()
      {
        if (this._globalNotifyHandler != null || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
          return;
        this._globalNotifyHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
        service.Subscribe(this._globalNotifyHandler);
      }

      private void ReleaseCustomServices()
      {
        if (this._globalNotifyHandler == null || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
          return;
        service.Unsubscribe(this._globalNotifyHandler);
        this._globalNotifyHandler = (NotificationEventHandler) null;
      }

      public BackgroundView(System.IServiceProvider provider)
      {
        this._serviceProvider = provider;
        this.InitializeComponent();
        this.InitializeData();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this.ReleaseCustomServices();
          if (this.components != null)
            this.components.Dispose();
        }
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BackgroundView));
        ListColumn listColumn1 = new ListColumn();
        ListColumn listColumn2 = new ListColumn();
        ListColumn listColumn3 = new ListColumn();
        ListColumn listColumn4 = new ListColumn();
        ListColumn listColumn5 = new ListColumn();
        ListColumn listColumn6 = new ListColumn();
        ListColumn listColumn7 = new ListColumn();
        this._toolBar = new Intermech.Bars.ToolBar();
        this.btResume = new ButtonItem();
        this.btPause = new ButtonItem();
        this.btStop = new ButtonItem();
        this.btCancel = new ButtonItem();
        this._grid = new ListGrid();
        this.SuspendLayout();
        this._toolBar.AccessibleDescription = (string) null;
        this._toolBar.AccessibleName = (string) null;
        componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
        this._toolBar.BackgroundImage = (Image) null;
        this._toolBar.Font = (Font) null;
        this._toolBar.FullMenus = true;
        this._toolBar.Guid = new Guid("d9118ca2-613a-40d7-ad6d-f86321d9e56a");
        this._toolBar.Items.AddRange(new ToolbarItemBase[4]
        {
          (ToolbarItemBase) this.btResume,
          (ToolbarItemBase) this.btPause,
          (ToolbarItemBase) this.btStop,
          (ToolbarItemBase) this.btCancel
        });
        this._toolBar.Name = "_toolBar";
        componentResourceManager.ApplyResources((object) this.btResume, "btResume");
        this.btResume.Enabled = false;
        this.btResume.ShowText = true;
        this.btResume.Click += new EventHandler(this.btResume_Click);
        componentResourceManager.ApplyResources((object) this.btPause, "btPause");
        this.btPause.Enabled = false;
        this.btPause.ShowText = true;
        this.btPause.Click += new EventHandler(this.btPause_Click);
        componentResourceManager.ApplyResources((object) this.btStop, "btStop");
        this.btStop.Enabled = false;
        this.btStop.ShowText = true;
        this.btStop.Click += new EventHandler(this.btStop_Click);
        componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
        this.btCancel.ShowText = true;
        this.btCancel.Visible = false;
        this._grid.AccessibleDescription = (string) null;
        this._grid.AccessibleName = (string) null;
        this._grid.AlternateBackground = Color.PowderBlue;
        componentResourceManager.ApplyResources((object) this._grid, "_grid");
        this._grid.AutoHeight = false;
        this._grid.BackColor = SystemColors.Control;
        this._grid.BackgroundImage = (Image) null;
        this._grid.BorderWidth = 4;
        listColumn1.Name = "_icon";
        listColumn1.Text = "";
        listColumn1.Width = 18;
        listColumn2.Name = "_name";
        listColumn2.Text = "Задача";
        listColumn2.Width = 160 /*0xA0*/;
        listColumn3.Name = "_state";
        listColumn3.Text = "Состояние";
        listColumn3.Width = 80 /*0x50*/;
        listColumn4.Name = "_progress";
        listColumn4.NumericSort = true;
        listColumn4.Text = "Выполнено";
        listColumn4.Width = 170;
        listColumn5.Name = "_elapsed";
        listColumn5.NumericSort = true;
        listColumn5.Text = "Прошло времени";
        listColumn5.Width = 120;
        listColumn6.Name = "_endTime";
        listColumn6.NumericSort = true;
        listColumn6.Text = "Осталось";
        listColumn7.Name = "_result";
        listColumn7.NumericSort = true;
        listColumn7.Text = "Результат";
        this._grid.Columns.AddRange(new ListColumn[7]
        {
          listColumn1,
          listColumn2,
          listColumn3,
          listColumn4,
          listColumn5,
          listColumn6,
          listColumn7
        });
        this._grid.Font = (Font) null;
        this._grid.ForeColor = SystemColors.ControlText;
        this._grid.GridColor = Color.Silver;
        this._grid.HeaderHeight = 22;
        this._grid.HeaderStyle = HeaderStyle.Flat;
        this._grid.HotItemTracking = true;
        this._grid.HotTrackingColor = Color.DeepSkyBlue;
        this._grid.ImageList = (ImageList) null;
        this._grid.ItemHeight = 19;
        this._grid.Name = "_grid";
        this._grid.SelectedTextColor = Color.White;
        this._grid.SelectionColor = Color.DarkBlue;
        this._grid.SuperFlatHeaderColor = Color.White;
        this._grid.SelectedIndexChanged += new ListGrid.ClickedEventHandler(this.Grid_SelectedIndexChanged);
        this.AccessibleDescription = (string) null;
        this.AccessibleName = (string) null;
        this.AllowedStates = DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.BackgroundImage = (Image) null;
        this.Controls.Add((Control) this._grid);
        this.Controls.Add((Control) this._toolBar);
        this.Guid = ViewGuids.BackgroundView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (BackgroundView);
        this.ShowHint = DockState.DockBottomAutoHide;
        this.ResumeLayout(false);
      }

      private void Items_Changed(object source, ChangedEventArgs e)
      {
        if (e.ChangedType == ChangedType.SelectionChanged)
          this.UpdateToolbar();
        this._grid.Invalidate();
      }

      private Intermech.Controls.Grid.ListItem GetTaskListItem(IBackgroundTask task)
      {
        lock (this._grid)
        {
          foreach (Intermech.Controls.Grid.ListItem taskListItem in (CollectionBase) this._grid.Items)
          {
            if (taskListItem.Tag == task)
              return taskListItem;
          }
          return (Intermech.Controls.Grid.ListItem) null;
        }
      }

      public void DeleteTask(IBackgroundTask task)
      {
        lock (this._grid)
        {
          Intermech.Controls.Grid.ListItem taskListItem = this.GetTaskListItem(task);
          if (taskListItem != null)
            this._grid.Items.Remove(taskListItem);
          task.Changed -= new BackgroundTaskChangedEventHandler(this.Task_Changed);
        }
      }

      public void AddTask(IBackgroundTask task)
      {
        Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem(this._grid);
        listItem.Tag = (object) task;
        listItem.SubItems[0].ImageIndex = task.ImageIndex;
        listItem.SubItems[1].Text = task.Name;
        listItem.SubItems[2].Text = EnumDescConverter.GetEnumDescription((Enum) task.State);
        RemaindTimePanel remaindTimePanel = (RemaindTimePanel) null;
        ColorProgressBar progressBar = (ColorProgressBar) null;
        if (task.ShowMode == BackgroundTaskShowMode.Progress || task.ShowMode == BackgroundTaskShowMode.TimedProgress)
        {
          if (task.ShowMode == BackgroundTaskShowMode.TimedProgress)
            remaindTimePanel = new RemaindTimePanel();
          progressBar = new ColorProgressBar();
          progressBar.Maximum = task.MaximumValue;
          progressBar.Minimum = task.MinimumValue;
          progressBar.Value = (int) task.Value;
          progressBar.ShowPercent = true;
          progressBar.GradientMode = ColorProgressBar.GradientModes.Vertical;
          listItem.SubItems[3].Control = (Control) progressBar;
        }
        else
          listItem.SubItems[3].Text = (string) task.Value;
        ElapsedTimePanel elapsedPanel = new ElapsedTimePanel();
        elapsedPanel.Enabled = false;
        listItem.SubItems[4].Control = (Control) elapsedPanel;
        if (task.State == BackgroundTaskState.Running)
          elapsedPanel.Enabled = true;
        if (remaindTimePanel != null)
        {
          listItem.SubItems[5].Control = (Control) remaindTimePanel;
          remaindTimePanel.Attach(progressBar, elapsedPanel);
        }
        lock (this._grid)
          this._grid.Items.Insert(0, listItem);
        task.Changed += new BackgroundTaskChangedEventHandler(this.Task_Changed);
        this.ShowView();
      }

      public bool CheckClosing()
      {
        ArrayList arrayList = new ArrayList();
        lock (this._grid)
        {
          foreach (Intermech.Controls.Grid.ListItem listItem in (CollectionBase) this._grid.Items)
          {
            if (listItem.Tag is IBackgroundTask tag && tag.Active)
              arrayList.Add((object) tag.Name);
          }
        }
        if (arrayList.Count > 0)
        {
          int num = 1;
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string str in arrayList)
            stringBuilder.Append($"{num++}) \"{str}\"\n");
          this.ShowView();
          Application.DoEvents();
          if (MessageBox.Show(LocalizationHolder.rm.GetString("IMClient_17") + stringBuilder.ToString() + LocalizationHolder.rm.GetString("IMClient_18"), LocalizationHolder.rm.GetString("IMClient_19"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            return false;
        }
        return true;
      }

      protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
      {
        if (!(e.EventName == "ApplicationClosed"))
          return;
        this.ApplicationClosed();
      }

      private void ShowView()
      {
        this._needHide = !this.IsOpen;
        if (this.Manager != null)
          this.Open();
        else
          this.Show(this._dockManager);
      }

      private void ApplicationClosed()
      {
        lock (this._grid)
        {
          List<IBackgroundTask> backgroundTaskList = new List<IBackgroundTask>(this._grid.Items.Count);
          foreach (Intermech.Controls.Grid.ListItem listItem in (CollectionBase) this._grid.Items)
          {
            if (listItem.Tag is IBackgroundTask tag && tag.Active && tag.CanTerminate())
              backgroundTaskList.Add(tag);
          }
          foreach (IBackgroundTask backgroundTask in backgroundTaskList)
            backgroundTask.Terminate();
        }
      }

      private void Task_Changed(object sender, BackgroundTaskChangedType type)
      {
        if (this._grid.InvokeRequired)
        {
          this._grid.Invoke((Delegate) new BackgroundTaskChangedEventHandler(this.Task_Changed), sender, (object) type);
        }
        else
        {
          if (!(sender is IBackgroundTask task))
            return;
          Intermech.Controls.Grid.ListItem taskListItem = this.GetTaskListItem(task);
          if (taskListItem == null)
            return;
          switch (type)
          {
            case BackgroundTaskChangedType.Text:
              taskListItem.SubItems[1].Text = task.Name;
              break;
            case BackgroundTaskChangedType.ImageIndex:
              taskListItem.SubItems[0].ImageIndex = task.ImageIndex;
              break;
            case BackgroundTaskChangedType.Value:
              if (task.ShowMode == BackgroundTaskShowMode.Progress)
              {
                if (!(taskListItem.SubItems[3].Control is ColorProgressBar control))
                  break;
                control.Maximum = task.MaximumValue;
                control.Minimum = task.MinimumValue;
                control.Value = (int) task.Value;
                break;
              }
              taskListItem.SubItems[3].Text = (string) task.Value;
              break;
            case BackgroundTaskChangedType.State:
              taskListItem.SubItems[2].Text = EnumDescConverter.GetEnumDescription((Enum) task.State);
              if (!(taskListItem.SubItems[4].Control is ElapsedTimePanel control1))
                break;
              control1.Enabled = task.State == BackgroundTaskState.Running;
              break;
            case BackgroundTaskChangedType.Result:
              object result = task.Result;
              string empty = string.Empty;
              if (result != null)
                empty = result.ToString();
              taskListItem.SubItems[6].Text = empty;
              break;
            case BackgroundTaskChangedType.Dispose:
              task.Changed -= new BackgroundTaskChangedEventHandler(this.Task_Changed);
              this._grid.Items.Remove(taskListItem);
              if (this._grid.Items.Count != 0 || !this._needHide)
                break;
              this._needHide = false;
              this.Close();
              break;
          }
        }
      }

      private IBackgroundTask GetSelectedTask()
      {
        Intermech.Controls.Grid.ListItem focusedItem = this._grid.FocusedItem;
        return focusedItem == null ? (IBackgroundTask) null : focusedItem.Tag as IBackgroundTask;
      }

      private void UpdateToolbar()
      {
        this.btResume.Enabled = false;
        this.btStop.Enabled = false;
        this.btPause.Enabled = false;
        this.btPause.Checked = false;
        IBackgroundTask selectedTask = this.GetSelectedTask();
        if (selectedTask == null)
          return;
        this.btStop.Enabled = selectedTask.CanStop();
        this.btResume.Enabled = selectedTask.CanResume();
        this.btPause.Enabled = selectedTask.CanPause();
        this.btPause.Checked = selectedTask.State == BackgroundTaskState.Paused;
      }

      private void Grid_SelectedIndexChanged(object source, ClickEventArgs e) => this.UpdateToolbar();

      private void btStop_Click(object sender, EventArgs e)
      {
        IBackgroundTask selectedTask = this.GetSelectedTask();
        if (selectedTask == null)
          return;
        selectedTask.Stop();
        this.btPause.Checked = selectedTask.State == BackgroundTaskState.Paused;
      }

      private void btResume_Click(object sender, EventArgs e)
      {
        IBackgroundTask selectedTask = this.GetSelectedTask();
        if (selectedTask == null)
          return;
        selectedTask.Resume();
        this.btPause.Checked = selectedTask.State == BackgroundTaskState.Paused;
        this.UpdateToolbar();
      }

      private void btPause_Click(object sender, EventArgs e)
      {
        IBackgroundTask selectedTask = this.GetSelectedTask();
        if (selectedTask == null)
          return;
        selectedTask.Pause();
        this.btPause.Checked = selectedTask.State == BackgroundTaskState.Paused;
        this.UpdateToolbar();
      }

      private void LoadConfiguration(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = configurationManager.Open("BackgroundGrid");
        if (configuration == null)
          return;
        foreach (ListColumn column in (CollectionBase) this._grid.Columns)
        {
          if (configuration.HasProperty(column.Name))
            column.Width = int.Parse(configuration.GetProperty(column.Name));
        }
      }

      private void SaveConfiguration(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = configurationManager.Create("BackgroundGrid");
        foreach (ListColumn column in (CollectionBase) this._grid.Columns)
          configuration.SetProperty(column.Name, column.Width.ToString());
      }
    }
}
