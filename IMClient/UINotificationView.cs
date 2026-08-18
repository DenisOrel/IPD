
// Type: IMClient.UINotificationView




using Intermech;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.UI.ExceptionHandling;
using Intermech.UI.Wpf.WinformsInterop;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Forms;


namespace IMClient
{
    public sealed class UINotificationView : DockControl, IUINotificationService
    {
      private static readonly int _notifyIconTimeout = (int) TimeSpan.FromSeconds(30.0).TotalMilliseconds;
      private UINotificationsVM _itemsListVM;
      private ExceptionRecoveryHandler _exceptionRecoveryHandler;
      private System.Timers.Timer _itemsSaveTimer;
      private NotifyIcon _notifyIcon;
      private UINotificationsStorage _storage;
      private IContainer components;
      private Button _clearButton;
      private TableLayoutPanel tableLayoutPanel1;
      private FlowLayoutPanel flowLayoutPanel1;
      private WpfElementHost itemsListHost;
      private UINotificationViewControl itemsListControl;

      public UINotificationView()
      {
        this.InitializeComponent();
        this._itemsListVM = new UINotificationsVM();
        this._itemsListVM.NotificationActionHandler = new EventHandler<UINotificationActionEventArgs>(this.OnNotificationAction);
        this.itemsListControl.DataContext = (object) this._itemsListVM;
        this._exceptionRecoveryHandler = new ExceptionRecoveryHandler();
        this._itemsSaveTimer = new System.Timers.Timer();
        this._itemsSaveTimer.Interval = TimeSpan.FromSeconds(30.0).TotalMilliseconds;
        this._itemsSaveTimer.AutoReset = false;
        this._itemsSaveTimer.SynchronizingObject = (ISynchronizeInvoke) this;
        this._itemsSaveTimer.Elapsed += new ElapsedEventHandler(this.OnItemsSaveTimer);
        this._notifyIcon = new NotifyIcon();
        this._notifyIcon.BalloonTipClicked += new EventHandler(this.NotifyIcon_BalloonTipClicked);
        this._notifyIcon.Click += new EventHandler(this.NotifyIcon_Click);
        this._notifyIcon.Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
        this._notifyIcon.Text = $"{AssemblyAttributes.AssemblyProduct} {AssemblyAttributes.IPSVersion}";
        this._notifyIcon.Visible = false;
      }

      private void DisposeManuallyCreatedComponents(bool disposing)
      {
        if (!disposing)
          return;
        this._itemsSaveTimer.Stop();
        this._itemsSaveTimer.Dispose();
        this._notifyIcon.Visible = false;
        this._notifyIcon.Dispose();
      }

      void IUINotificationService.ShowNotification(UINotification notification)
      {
        if (notification == null)
          throw new ArgumentNullException(nameof (notification));
        IMainFormClientService service = (IMainFormClientService) ApplicationServices.Container.GetService(typeof (IMainFormClientService));
        if (service.MainForm.InvokeRequired)
        {
          Action<UINotification> method = new Action<UINotification>(this.ShowNotificationInUIThread);
          service.MainForm.BeginInvoke((Delegate) method, (object) notification);
        }
        else
          this.ShowNotificationInUIThread(notification);
      }

      private void ShowNotificationInUIThread(UINotification notification)
      {
        if (!this.IsOpen)
          this.TryOpenInDocking();
        UINotificationsItemVM notificationsItemVm = new UINotificationsItemVM(notification);
        this._itemsListVM.Items.Insert(0, notificationsItemVm);
        this._notifyIcon.Visible = true;
        this._notifyIcon.ShowBalloonTip(UINotificationView._notifyIconTimeout, notificationsItemVm.Caption, notificationsItemVm.Message, this.ToToolTipIcon(notificationsItemVm.Icon));
        this._notifyIcon.Tag = (object) notificationsItemVm;
      }

      private void TryOpenInDocking()
      {
        if (this.Manager == null)
        {
          DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
          if (service != null)
            this.Manager = service;
        }
        if (this.Manager == null)
          return;
        this.Open();
      }

      private ToolTipIcon ToToolTipIcon(UINotificationIcon icon)
      {
        switch (icon)
        {
          case UINotificationIcon.None:
            return ToolTipIcon.None;
          case UINotificationIcon.Info:
            return ToolTipIcon.Info;
          case UINotificationIcon.Warning:
            return ToolTipIcon.Warning;
          case UINotificationIcon.Error:
            return ToolTipIcon.Error;
          default:
            throw new NotSupportedEnumException((Enum) icon);
        }
      }

      event EventHandler<UINotificationActionEventArgs> IUINotificationService.NotificationAction
      {
        add
        {
          lock (this)
            this._itemsListVM.NotificationActionHandler += value;
        }
        remove
        {
          lock (this)
            this._itemsListVM.NotificationActionHandler -= value;
        }
      }

      private void NotifyIcon_Click(object sender, EventArgs e) => this.FireLastShownItemAction();

      private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
      {
        this.FireLastShownItemAction();
      }

      private void ClearButton_Click(object sender, EventArgs e) => this._itemsListVM.Items.Clear();

      private void FireLastShownItemAction()
      {
        UINotificationsItemVM tag = (UINotificationsItemVM) this._notifyIcon.Tag;
        this._notifyIcon.Tag = (object) null;
        if (tag != null && this._itemsListVM.Items.Contains(tag))
        {
          Form mainForm = ((IMainFormClientService) ApplicationServices.Container.GetService(typeof (IMainFormClientService))).MainForm;
          if (mainForm.WindowState == FormWindowState.Minimized)
            mainForm.Restore();
          mainForm.Activate();
          if (!this.IsOpen)
            this.TryOpenInDocking();
          this._itemsListVM.FireItemActionCommand.Execute((object) tag);
        }
        else
        {
          if (!this._notifyIcon.Visible)
            return;
          this._notifyIcon.Visible = false;
        }
      }

      private void OnViewLoaded(object sender, EventArgs e)
      {
        IConfigurationOptionRepository service = (IConfigurationOptionRepository) ApplicationServices.Container.GetService(typeof (IConfigurationOptionRepository));
        if (service != null)
        {
          if (service.Find(ConfigurationOptionKeys.UI_GridFont) is Font navigatorGridFont)
            this.SetItemsFontFromNavigatorGridFont(navigatorGridFont);
          service.OptionChanged += new EventHandler<Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs>(this.OnNavigatorGridFontChanged);
        }
        this.RestoreItemsFromUserConfiguration();
        this._itemsListVM.Items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ScheduleSaveItemsToUserConfiguration);
      }

      private void OnViewClosed(object sender, EventArgs e)
      {
        if (!this._itemsSaveTimer.Enabled)
          return;
        this._itemsSaveTimer.Stop();
        this.SaveItemsToUserConfiguration();
      }

      private void OnNavigatorGridFontChanged(object sender, Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs e)
      {
        if (!(e.OptionKey == ConfigurationOptionKeys.UI_GridFont) || !(e.NewValue is Font newValue))
          return;
        this.SetItemsFontFromNavigatorGridFont(newValue);
      }

      private void SetItemsFontFromNavigatorGridFont(Font navigatorGridFont)
      {
        this._itemsListVM.FontSize = (double) navigatorGridFont.SizeInPoints / 72.0 * 96.0;
      }

      private void RestoreItemsFromUserConfiguration()
      {
        try
        {
          foreach (UINotification notification in (IEnumerable<UINotification>) this.Storage.LoadFromUserConfiguration())
            this._itemsListVM.Items.Add(new UINotificationsItemVM(notification));
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, this.GetCurrentMethodName(nameof (RestoreItemsFromUserConfiguration)));
        }
      }

      private void ScheduleSaveItemsToUserConfiguration(
        object sender,
        NotifyCollectionChangedEventArgs e)
      {
        this._itemsSaveTimer.Stop();
        this._itemsSaveTimer.Start();
      }

      private void OnItemsSaveTimer(object sender, ElapsedEventArgs e)
      {
        this.SaveItemsToUserConfiguration();
      }

      private void SaveItemsToUserConfiguration()
      {
        try
        {
          this.Storage.SaveToUserConfiguration((ICollection<UINotification>) this._itemsListVM.Items.Select<UINotificationsItemVM, UINotification>((Func<UINotificationsItemVM, UINotification>) (x => x.Notification)).ToArray<UINotification>());
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, this.GetCurrentMethodName(nameof (SaveItemsToUserConfiguration)));
        }
      }

      private UINotificationsStorage Storage
      {
        [DebuggerStepThrough] get
        {
          if (this._storage == null)
            this._storage = new UINotificationsStorage();
          return this._storage;
        }
      }

      private void OnNotificationAction(object sender, UINotificationActionEventArgs e)
      {
        if (e.Handled)
          return;
        switch (e.Action.Name)
        {
          case "UI.Notifications.Open":
            Uri data1 = e.Action.Data;
            if (!(data1 != (Uri) null))
              break;
            this.OnOpenAction(data1);
            break;
          case "UI.Notifications.RecoverError":
            Uri data2 = e.Action.Data;
            if (!(data2 != (Uri) null))
              break;
            this.OnRecoverErrorAction(data2);
            break;
          case "UI.Notifications.ShowError":
            Exception error = e.Notification.Error;
            if (error == null)
              break;
            this.OnShowErrorAction(error);
            break;
        }
      }

      private void OnOpenAction(Uri uri)
      {
        try
        {
          Process.Start(uri.AbsoluteUri).Dispose();
        }
        catch (Exception ex)
        {
          int num = (int) System.Windows.Forms.MessageBox.Show($"При открытии ссылки '{uri}' произошла ошибка. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }

      private void OnRecoverErrorAction(Uri uri)
      {
        this._exceptionRecoveryHandler.TryInvokeRecoveryAction(uri);
      }

      private void OnShowErrorAction(Exception exception)
      {
        ((IExceptionHandlerService) ApplicationServices.Container.GetService(typeof (IExceptionHandlerService)))?.ShowException(exception);
      }

      protected override void Dispose(bool disposing)
      {
        this.DisposeManuallyCreatedComponents(disposing);
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this._clearButton = new Button();
        this.tableLayoutPanel1 = new TableLayoutPanel();
        this.flowLayoutPanel1 = new FlowLayoutPanel();
        this.itemsListHost = new WpfElementHost();
        this.itemsListControl = new UINotificationViewControl();
        this.tableLayoutPanel1.SuspendLayout();
        this.flowLayoutPanel1.SuspendLayout();
        this.SuspendLayout();
        this._clearButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this._clearButton.Location = new System.Drawing.Point(202, 3);
        this._clearButton.Name = "_clearButton";
        this._clearButton.Size = new System.Drawing.Size(75, 23);
        this._clearButton.TabIndex = 0;
        this._clearButton.Text = "Очистить";
        this._clearButton.UseVisualStyleBackColor = true;
        this._clearButton.Click += new EventHandler(this.ClearButton_Click);
        this.tableLayoutPanel1.ColumnCount = 1;
        this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 0);
        this.tableLayoutPanel1.Controls.Add((Control) this.itemsListHost, 0, 1);
        this.tableLayoutPanel1.Dock = DockStyle.Fill;
        this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount = 2;
        this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
        this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        this.tableLayoutPanel1.Size = new System.Drawing.Size(286, 327);
        this.tableLayoutPanel1.TabIndex = 2;
        this.flowLayoutPanel1.Controls.Add((Control) this._clearButton);
        this.flowLayoutPanel1.Dock = DockStyle.Fill;
        this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
        this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 34);
        this.flowLayoutPanel1.TabIndex = 1;
        this.itemsListHost.Dock = DockStyle.Fill;
        this.itemsListHost.Location = new System.Drawing.Point(3, 43);
        this.itemsListHost.Name = "itemsListHost";
        this.itemsListHost.Size = new System.Drawing.Size(280, 281);
        this.itemsListHost.TabIndex = 2;
        this.itemsListHost.Text = "itemsListHost";
        this.itemsListHost.Child = (UIElement) this.itemsListControl;
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Float;
        this.AutoScaleMode = AutoScaleMode.Inherit;
        this.Controls.Add((Control) this.tableLayoutPanel1);
        this.Guid = new Guid("94c3365b-423e-471c-b18a-b690328da34b");
        this.HideOnClose = true;
        this.Name = "UINotificationsView";
        this.Size = new System.Drawing.Size(286, 327);
        this.Text = "Уведомления";
        this.Closed += new EventHandler(this.OnViewClosed);
        this.Load += new EventHandler(this.OnViewLoaded);
        this.tableLayoutPanel1.ResumeLayout(false);
        this.flowLayoutPanel1.ResumeLayout(false);
        this.ResumeLayout(false);
      }
    }
}
