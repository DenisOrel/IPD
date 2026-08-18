// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AutoNotificationControl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class AutoNotificationControl : ObjectCreatorControl
{
  private AutoNotificationSettings _notifSettings;
  private long _objectId;
  private bool _isChanged;
  private IContainer components;
  private TabControl tabControl;
  private TabPage AdresseePage;
  private TabPage ActuationConditionPage;
  private TabPage WayOfNotificationPage;

  public AutoNotificationSettings AutoNotificationSettings => this._notifSettings;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public event EventHandler Modified;

  public AutoNotificationControl(long objID)
  {
    this._objectId = objID;
    this._notifSettings = this.ExtractSettingsFromObject(objID);
    if (this._notifSettings == null)
      return;
    this.InitializeComponent();
    this.InitializeControl();
  }

  public AutoNotificationControl(long protoObjID, long newObjId)
  {
    this._objectId = newObjId;
    this._notifSettings = this.ExtractSettingsFromObject(protoObjID);
    this._notifSettings.AutoNotificationID = Math.Abs(newObjId);
    if (this._notifSettings == null)
      return;
    this.InitializeComponent();
    this.InitializeControl();
  }

  public AutoNotificationControl(AutoNotificationSettings emptyNotifSettings, long objectId)
  {
    this._objectId = objectId;
    this._notifSettings = emptyNotifSettings;
    this.InitializeComponent();
    this.tabControl.Dock = DockStyle.Fill;
    this.InitializeControl();
    this._StepIsReadyCheckRequired = true;
    this._StepIsReady = false;
  }

  private void InitializeControl()
  {
    switch (this._notifSettings.NotifEventType)
    {
      case NotificationEventType.None:
        this.tabControl.TabPages.Clear();
        break;
      case NotificationEventType.AddLink:
      case NotificationEventType.DeleteLink:
        this.CreateNotifSettingsCntrlForRelationChanging();
        break;
      case NotificationEventType.Create:
      case NotificationEventType.CreateVersion:
      case NotificationEventType.Delete:
      case NotificationEventType.Cancel:
      case NotificationEventType.CheckIn:
      case NotificationEventType.CheckOut:
      case NotificationEventType.Restore:
        this.CreateNotifSettingsCntrlForObjectChanging();
        break;
      case NotificationEventType.NextLCStep:
        this.CreateNotifSettingsCntrlForLCStepChanging();
        break;
      case NotificationEventType.NextLCLevel:
        this.CreateNotifSettingsCntrlForLCLevelChanging();
        break;
      case NotificationEventType.Write:
        this.CreateWriteAttrNotifSettingsCntrl();
        break;
      case NotificationEventType.GetAccess:
        this.CreateNotifSettingsCntrlForAccessNotification();
        break;
    }
  }

  private AutoNotificationSettings ExtractSettingsFromObject(long objID)
  {
    AutoNotificationSettings settingsFromObject = (AutoNotificationSettings) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAutoNotificationsService)) is IAutoNotificationsService customService))
        return (AutoNotificationSettings) null;
      try
      {
        settingsFromObject = customService.FormSettingsFromObjectsBlobAttr(objID, sessionKeeper.Session.SessionGUID);
      }
      catch (AutoNotificationSettingsException ex)
      {
        ExceptionHelper.ExceptionService.ShowException((Exception) ex);
      }
    }
    return settingsFromObject;
  }

  private void CreateNotifSettingsCntrlForAccessNotification()
  {
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.AutoScroll = true;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    ActuationConditionForAccessCntrl conditionForAccessCntrl = new ActuationConditionForAccessCntrl(this._notifSettings as AccessDeniedAutoNotificationSettings);
    this.ActuationConditionPage.Controls.Add((Control) conditionForAccessCntrl);
    conditionForAccessCntrl.Dock = DockStyle.Fill;
    conditionForAccessCntrl.AutoScroll = true;
    conditionForAccessCntrl.Visible = true;
    conditionForAccessCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void CreateNotifSettingsCntrlForRelationChanging()
  {
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    ActuationConditionForRelationCntrl forRelationCntrl = new ActuationConditionForRelationCntrl(this._notifSettings as AttributableAutoNotificationSettings);
    this.ActuationConditionPage.Controls.Add((Control) forRelationCntrl);
    forRelationCntrl.Dock = DockStyle.Fill;
    forRelationCntrl.AutoScroll = true;
    forRelationCntrl.Visible = true;
    forRelationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void CreateNotifSettingsCntrlForObjectChanging()
  {
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    ActuationConditionForObjectCntrl conditionForObjectCntrl = new ActuationConditionForObjectCntrl(this._notifSettings as AttributableAutoNotificationSettings);
    this.ActuationConditionPage.Controls.Add((Control) conditionForObjectCntrl);
    conditionForObjectCntrl.Dock = DockStyle.Fill;
    conditionForObjectCntrl.AutoScroll = true;
    conditionForObjectCntrl.Visible = true;
    conditionForObjectCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void CreateNotifSettingsCntrlForLCStepChanging()
  {
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    ActuationConditionForObjectCntrl conditionForObjectCntrl = new ActuationConditionForObjectCntrl((AttributableAutoNotificationSettings) (this._notifSettings as LCStepAutoNotificationSettings));
    this.ActuationConditionPage.Controls.Add((Control) conditionForObjectCntrl);
    conditionForObjectCntrl.Dock = DockStyle.Fill;
    conditionForObjectCntrl.AutoScroll = true;
    conditionForObjectCntrl.Visible = true;
    conditionForObjectCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void CreateNotifSettingsCntrlForLCLevelChanging()
  {
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    ActuationConditionForObjectCntrl conditionForObjectCntrl = new ActuationConditionForObjectCntrl((AttributableAutoNotificationSettings) (this._notifSettings as LCLevelAutoNotificationSettings));
    this.ActuationConditionPage.Controls.Add((Control) conditionForObjectCntrl);
    conditionForObjectCntrl.Dock = DockStyle.Fill;
    conditionForObjectCntrl.AutoScroll = true;
    conditionForObjectCntrl.Visible = true;
    conditionForObjectCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void CreateWriteAttrNotifSettingsCntrl()
  {
    ActuationConditionForObjectCntrl conditionForObjectCntrl = new ActuationConditionForObjectCntrl((AttributableAutoNotificationSettings) (this._notifSettings as AttrChangingAutoNotificationSettings));
    this.ActuationConditionPage.Controls.Add((Control) conditionForObjectCntrl);
    conditionForObjectCntrl.Dock = DockStyle.Fill;
    conditionForObjectCntrl.AutoScroll = true;
    conditionForObjectCntrl.Visible = true;
    conditionForObjectCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    AdresseeCntrl adresseeCntrl = new AdresseeCntrl(this._notifSettings);
    this.AdresseePage.Controls.Add((Control) adresseeCntrl);
    adresseeCntrl.Dock = DockStyle.Fill;
    adresseeCntrl.Visible = true;
    adresseeCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
    WayOfNotificationCntrl notificationCntrl = new WayOfNotificationCntrl(this._notifSettings);
    this.WayOfNotificationPage.Controls.Add((Control) notificationCntrl);
    notificationCntrl.Dock = DockStyle.Fill;
    notificationCntrl.Visible = true;
    notificationCntrl.Modified += new EventHandler(this.OnInnerControlChanged);
  }

  private void OnInnerControlChanged(object sender, EventArgs e) => this.IsChanged = true;

  public override void Refresh()
  {
    foreach (Control tabPage in this.tabControl.TabPages)
    {
      foreach (Control control in (ArrangedElementCollection) tabPage.Controls)
        control.Refresh();
    }
    this._StepIsReady = true;
  }

  public override bool Save(PageSaveArgs args)
  {
    this.Save();
    return base.Save(args);
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    base.Refresh(args);
    this._StepIsReady = true;
    return true;
  }

  public void Save()
  {
    foreach (Control control1 in (ArrangedElementCollection) this.tabControl.Controls)
    {
      foreach (Control control2 in (ArrangedElementCollection) control1.Controls)
      {
        if (control2 is ICanSaveNotifSettings)
          (control2 as ICanSaveNotifSettings).SaveSettings();
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IAutoNotificationsService)) is IAutoNotificationsService customService)
        customService.SaveSettingsToObjectsBlobAttr(this._notifSettings, this._objectId, sessionKeeper.Session.SessionGUID);
    }
    this.IsChanged = false;
  }

  public void ResetChanges()
  {
    this.Refresh();
    this.IsChanged = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tabControl = new TabControl();
    this.ActuationConditionPage = new TabPage();
    this.AdresseePage = new TabPage();
    this.WayOfNotificationPage = new TabPage();
    this.tabControl.SuspendLayout();
    this.SuspendLayout();
    this.tabControl.Controls.Add((Control) this.ActuationConditionPage);
    this.tabControl.Controls.Add((Control) this.AdresseePage);
    this.tabControl.Controls.Add((Control) this.WayOfNotificationPage);
    this.tabControl.Dock = DockStyle.Fill;
    this.tabControl.Location = new Point(0, 0);
    this.tabControl.Name = "tabControl";
    this.tabControl.SelectedIndex = 0;
    this.tabControl.Size = new Size(1460, 668);
    this.tabControl.TabIndex = 3;
    this.ActuationConditionPage.Location = new Point(4, 22);
    this.ActuationConditionPage.Name = "ActuationConditionPage";
    this.ActuationConditionPage.Padding = new Padding(3);
    this.ActuationConditionPage.Size = new Size(1452, 642);
    this.ActuationConditionPage.TabIndex = 1;
    this.ActuationConditionPage.Text = "Условия срабатывания";
    this.ActuationConditionPage.UseVisualStyleBackColor = true;
    this.AdresseePage.Location = new Point(4, 22);
    this.AdresseePage.Name = "AdresseePage";
    this.AdresseePage.Padding = new Padding(3);
    this.AdresseePage.Size = new Size(703, 455);
    this.AdresseePage.TabIndex = 0;
    this.AdresseePage.Text = "Адресат";
    this.AdresseePage.UseVisualStyleBackColor = true;
    this.WayOfNotificationPage.Location = new Point(4, 22);
    this.WayOfNotificationPage.Name = "WayOfNotificationPage";
    this.WayOfNotificationPage.Size = new Size(703, 455);
    this.WayOfNotificationPage.TabIndex = 2;
    this.WayOfNotificationPage.Text = "Способ уведомления";
    this.WayOfNotificationPage.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tabControl);
    this.Name = nameof (AutoNotificationControl);
    this.Size = new Size(1460, 668);
    this.tabControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
