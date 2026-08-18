// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.NewMailForm
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Controls;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Controls;
using Intermech.Workflow.Design;
using Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

internal class NewMailForm : FormEx
{
  private System.Windows.Forms.Timer _timer;
  private FormWindowState _previousMainFormState;
  private Dictionary<ProcessPriority, long> _lastMailCount;
  private double _secondsFromLastRefresh;
  private DateTime _lastRefreshTime;
  private bool _inUpdate;
  private bool _checkingSkipped;
  private IContainer components;
  private ToolTip toolTip1;
  private GroupBox newMessagesPanel;
  private PictureBox image1;
  private Label unreadLabel;
  private Label allCountLabel;
  private Panel bevel1;
  private Label label33;
  private Label label5;
  private Label label4;
  private PictureBox image3;
  private PictureBox image2;
  private Label label1;
  private Label label2;
  private Label label3;
  private Panel incompletedPanel;
  private Label uncompletedLabel;
  private Label label10;
  private PictureBox image4;
  private Panel panel3;
  private Label label8;
  private Label label9;
  private Label label100;
  private Label label11;
  private Label label12;
  private Label label13;
  private Label label6;
  private Label label14;
  private Label label140;
  private Label label15;
  private Button OkButton;
  private Button button1;

  public NewMailForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 828);
    this.incompletedPanel.Visible = false;
    this.Height -= this.incompletedPanel.Height;
    this._lastMailCount = new Dictionary<ProcessPriority, long>()
    {
      {
        ProcessPriority.Low,
        0L
      },
      {
        ProcessPriority.Normal,
        0L
      },
      {
        ProcessPriority.High,
        0L
      },
      {
        ProcessPriority.Unreal,
        0L
      }
    };
  }

  public System.Windows.Forms.Timer Timer => this._timer;

  public FormWindowState PreviousMainFormState
  {
    get => this._previousMainFormState;
    set => this._previousMainFormState = value;
  }

  public void StartMonitor()
  {
    if (this._timer != null)
    {
      this._timer.Stop();
      this.components?.Remove((IComponent) this._timer);
      this._timer.Dispose();
    }
    if (MailSettings.Cfg.RefreshInterval <= 0 || MailSettings.Cfg.NotifyPriority >= ProcessPriority.Unreal && !(MailSettings.Cfg.SoundFileName != string.Empty) || MailSettings.Cfg.DisableAllNotify)
      return;
    this._timer = new System.Windows.Forms.Timer();
    this.components?.Add((IComponent) this._timer);
    this._timer.Interval = MailSettings.Cfg.RefreshInterval * 1000 * 60;
    this._timer.Tick += new EventHandler(this.Timer_Tick);
    this._timer.Start();
  }

  public Dictionary<ProcessPriority, long> LastMailCount
  {
    get => this._lastMailCount;
    set => this._lastMailCount = value;
  }

  private bool CountMail(MailType mtype, ProcessPriority p, Label l)
  {
    long num1 = MailView.CountMail(mtype, false, true, p);
    l.Text = num1.ToString();
    int num2 = MailSettings.Cfg.NotifyPriority > p ? 0 : (num1 != this._lastMailCount[p] ? 1 : 0);
    this._lastMailCount[p] = num1;
    return num2 != 0;
  }

  public void CountMail() => this.CountMail(true);

  public void GoToMail()
  {
    Form mainForm = ApplicationServices.Container.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service1 ? service1.MainForm : (Form) null;
    if (mainForm != null)
    {
      mainForm.Activate();
      mainForm.BringToFront();
    }
    if (mainForm != null && mainForm.WindowState == FormWindowState.Minimized)
      mainForm.WindowState = this._previousMainFormState;
    bool flag = false;
    if (Holder.LastMailTree == null)
    {
      if (ApplicationServices.Container.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service2)
      {
        service2.OpenWellKnownWindow(wfClientPlugin.MailWindowName);
        for (int index = 1; index < 5 && Holder.LastMailTree == null; ++index)
        {
          Thread.Sleep(300);
          Application.DoEvents();
        }
      }
    }
    else
    {
      Control control = (Control) Holder.LastMailTree;
      while (control.Parent != null && !(control is DockControl))
        control = control.Parent;
      if (control is DockControl dockControl)
        dockControl.Activate();
      flag = control is WellKnownNavWindow wellKnownNavWindow && wellKnownNavWindow.WellKnownName == "mainNavigator";
    }
    if (Holder.LastMailTree == null)
      return;
    Holder.LastMailTree.Browse((flag ? LocalizationHolder.rm.GetString("Workflow.Client_26") : LocalizationHolder.rm.GetString("Workflow.Client_27")) + "*");
  }

  internal void CountMail(bool showForm)
  {
    this._checkingSkipped = false;
    if (this.Visible)
      return;
    long num = MailView.CountMail(MailType.Inbox, false, true);
    MailNode.InboxDescriptor.UnreadCount = num;
    if (num <= 0L || this._lastMailCount[ProcessPriority.Unreal] == num || MailSettings.Cfg.DisableAllNotify)
      return;
    this._lastMailCount[ProcessPriority.Unreal] = num;
    this.allCountLabel.Text = num.ToString();
    if (this._timer != null && MailSettings.Cfg.SoundFileName != string.Empty)
      MediaPlayer.PlaySound(MailSettings.Cfg.SoundFileName);
    NotificationEventArgs e = (NotificationEventArgs) new MailRefreshWithoutCountingEventArgs("MailRefresh");
    BaseHolder.NotificationService.FireEvent((object) null, e);
    if (!showForm)
      return;
    this._timer?.Stop();
    this.CountMail(MailType.Inbox, ProcessPriority.High, this.label1);
    this.CountMail(MailType.Inbox, ProcessPriority.Normal, this.label2);
    this.CountMail(MailType.Inbox, ProcessPriority.Low, this.label3);
    try
    {
      if ((ApplicationServices.Container.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service ? service.MainForm : (Form) null) == null)
        return;
      this.TopMost = true;
      if (this.ShowDialog() != DialogResult.Yes)
        return;
      this.GoToMail();
    }
    finally
    {
      this._timer?.Start();
    }
  }

  private void Timer_Tick(object sender, EventArgs e)
  {
    if (this._lastRefreshTime != DateTime.MinValue)
      this._secondsFromLastRefresh = DateTime.Now.Subtract(this._lastRefreshTime).TotalSeconds;
    this._lastRefreshTime = DateTime.Now;
    if (this._inUpdate)
      this._checkingSkipped = true;
    else
      this.CountMail();
  }

  public void ShowDebug()
  {
    string str = string.Empty;
    foreach (KeyValuePair<ProcessPriority, long> keyValuePair in this._lastMailCount)
      str = $"{str}{keyValuePair.Key.ToString()} = {keyValuePair.Value.ToString()}\r\n";
    int num = (int) MessageBox.Show($"{$"Последний опрос почты: {this._lastRefreshTime},\r\nреальный интервал опроса: {this._secondsFromLastRefresh} секунд"}\r\nНепрочитанных:\r\n{str}");
  }

  public bool InUpdate
  {
    get => this._inUpdate;
    set => this._inUpdate = value;
  }

  public bool CheckingSkipped
  {
    get => this._checkingSkipped;
    set => this._checkingSkipped = value;
  }

  private void NewMailForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(828);
  }

  private void NewMailForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(828);
  }

  protected override void CreateHandle()
  {
    base.CreateHandle();
    this.StartPosition = FormStartPosition.CenterScreen;
  }

  private void NewMailForm_Shown(object sender, EventArgs e) => this.TopMost = false;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewMailForm));
    this.toolTip1 = new ToolTip(this.components);
    this.unreadLabel = new Label();
    this.uncompletedLabel = new Label();
    this.newMessagesPanel = new GroupBox();
    this.image1 = new PictureBox();
    this.allCountLabel = new Label();
    this.bevel1 = new Panel();
    this.label33 = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.image3 = new PictureBox();
    this.image2 = new PictureBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.incompletedPanel = new Panel();
    this.label10 = new Label();
    this.image4 = new PictureBox();
    this.panel3 = new Panel();
    this.label8 = new Label();
    this.label9 = new Label();
    this.label100 = new Label();
    this.label11 = new Label();
    this.label12 = new Label();
    this.label13 = new Label();
    this.label6 = new Label();
    this.label14 = new Label();
    this.label140 = new Label();
    this.label15 = new Label();
    this.OkButton = new Button();
    this.button1 = new Button();
    this.newMessagesPanel.SuspendLayout();
    ((ISupportInitialize) this.image1).BeginInit();
    this.bevel1.SuspendLayout();
    ((ISupportInitialize) this.image3).BeginInit();
    ((ISupportInitialize) this.image2).BeginInit();
    this.incompletedPanel.SuspendLayout();
    ((ISupportInitialize) this.image4).BeginInit();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.unreadLabel, "unreadLabel");
    this.unreadLabel.Name = "unreadLabel";
    this.toolTip1.SetToolTip((Control) this.unreadLabel, componentResourceManager.GetString("unreadLabel.ToolTip"));
    componentResourceManager.ApplyResources((object) this.uncompletedLabel, "uncompletedLabel");
    this.uncompletedLabel.Name = "uncompletedLabel";
    this.toolTip1.SetToolTip((Control) this.uncompletedLabel, componentResourceManager.GetString("uncompletedLabel.ToolTip"));
    this.newMessagesPanel.Controls.Add((Control) this.image1);
    this.newMessagesPanel.Controls.Add((Control) this.unreadLabel);
    this.newMessagesPanel.Controls.Add((Control) this.allCountLabel);
    this.newMessagesPanel.Controls.Add((Control) this.bevel1);
    componentResourceManager.ApplyResources((object) this.newMessagesPanel, "newMessagesPanel");
    this.newMessagesPanel.Name = "newMessagesPanel";
    this.newMessagesPanel.TabStop = false;
    componentResourceManager.ApplyResources((object) this.image1, "image1");
    this.image1.Name = "image1";
    this.image1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.allCountLabel, "allCountLabel");
    this.allCountLabel.Name = "allCountLabel";
    this.bevel1.BorderStyle = BorderStyle.Fixed3D;
    this.bevel1.Controls.Add((Control) this.label33);
    this.bevel1.Controls.Add((Control) this.label5);
    this.bevel1.Controls.Add((Control) this.label4);
    this.bevel1.Controls.Add((Control) this.image3);
    this.bevel1.Controls.Add((Control) this.image2);
    this.bevel1.Controls.Add((Control) this.label1);
    this.bevel1.Controls.Add((Control) this.label2);
    this.bevel1.Controls.Add((Control) this.label3);
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this.label33, "label33");
    this.label33.Name = "label33";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.image3, "image3");
    this.image3.Name = "image3";
    this.image3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.image2, "image2");
    this.image2.Name = "image2";
    this.image2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.incompletedPanel.BorderStyle = BorderStyle.FixedSingle;
    this.incompletedPanel.Controls.Add((Control) this.uncompletedLabel);
    this.incompletedPanel.Controls.Add((Control) this.label10);
    this.incompletedPanel.Controls.Add((Control) this.image4);
    this.incompletedPanel.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.incompletedPanel, "incompletedPanel");
    this.incompletedPanel.Name = "incompletedPanel";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.image4, "image4");
    this.image4.Name = "image4";
    this.image4.TabStop = false;
    this.panel3.Controls.Add((Control) this.label8);
    this.panel3.Controls.Add((Control) this.label9);
    this.panel3.Controls.Add((Control) this.label100);
    this.panel3.Controls.Add((Control) this.label11);
    this.panel3.Controls.Add((Control) this.label12);
    this.panel3.Controls.Add((Control) this.label13);
    this.panel3.Controls.Add((Control) this.label6);
    this.panel3.Controls.Add((Control) this.label14);
    this.panel3.Controls.Add((Control) this.label140);
    this.panel3.Controls.Add((Control) this.label15);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.label100, "label100");
    this.label100.Name = "label100";
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.label12, "label12");
    this.label12.Name = "label12";
    componentResourceManager.ApplyResources((object) this.label13, "label13");
    this.label13.Name = "label13";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label14, "label14");
    this.label14.Name = "label14";
    componentResourceManager.ApplyResources((object) this.label140, "label140");
    this.label140.Name = "label140";
    componentResourceManager.ApplyResources((object) this.label15, "label15");
    this.label15.Name = "label15";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Yes;
    this.button1.Name = "button1";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.OkButton);
    this.Controls.Add((Control) this.newMessagesPanel);
    this.Controls.Add((Control) this.incompletedPanel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NewMailForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.TopMost = true;
    this.Shown += new EventHandler(this.NewMailForm_Shown);
    this.newMessagesPanel.ResumeLayout(false);
    this.newMessagesPanel.PerformLayout();
    ((ISupportInitialize) this.image1).EndInit();
    this.bevel1.ResumeLayout(false);
    this.bevel1.PerformLayout();
    ((ISupportInitialize) this.image3).EndInit();
    ((ISupportInitialize) this.image2).EndInit();
    this.incompletedPanel.ResumeLayout(false);
    this.incompletedPanel.PerformLayout();
    ((ISupportInitialize) this.image4).EndInit();
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.ResumeLayout(false);
  }
}
