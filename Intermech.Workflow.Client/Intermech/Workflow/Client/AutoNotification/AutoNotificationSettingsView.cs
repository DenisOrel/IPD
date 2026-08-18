// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AutoNotificationSettingsView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

[ViewDescriptionProvider(typeof (AutoNotificationSettingsView.AutoNotificationSettingsViewDescriptionProvider))]
public class AutoNotificationSettingsView : UserControl, IView, ICanCloseViews, ICanDeactivateView
{
  private readonly string _caption;
  private readonly int _imgIndex;
  private readonly string _saveDialogCaption;
  private readonly string _saveDialogMessage;
  private AutoNotificationControl _anControl;
  private long _objID;
  protected bool _reinitialize;
  protected bool _isActivating;
  private System.IServiceProvider _servicesProvider;
  private INotificationService _notificationService;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;
  private Panel _pnl;

  public AutoNotificationSettingsView()
  {
    this.InitializeComponent();
    this._caption = LocalizationHolder.rm.GetString("Workflow.Client_103");
    this._saveDialogCaption = LocalizationHolder.rm.GetString("Workflow.Client_104");
    this._saveDialogMessage = LocalizationHolder.rm.GetString("Workflow.Client_105");
    if (!(ApplicationServices.Container.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this._imgIndex = service.ImageIndex("imgCard");
    this._reinitialize = false;
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider servicesProvider)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    if (this._objID != itemData.ObjectID)
      this._isActivating = false;
    this._objID = itemData.ObjectID;
    this._reinitialize = true;
    this._servicesProvider = servicesProvider;
  }

  public void Activate(IView previousView)
  {
    if (this._isActivating)
      return;
    this._isActivating = true;
    if (this._reinitialize)
    {
      this.InitServices();
      this.UpdateDataOnControl();
      this._pnlBottom.Enabled = false;
      this._reinitialize = false;
    }
    this._isActivating = false;
  }

  public void Deactivate(IView nextView)
  {
    this.ReleaseServices();
    if (this._anControl == null || !this._anControl.IsChanged)
      return;
    if (MessageBox.Show(this._saveDialogMessage, this._saveDialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
    {
      this._anControl.ResetChanges();
      this._pnlBottom.Enabled = false;
    }
    else
      this.Save();
  }

  public string Caption => this._caption;

  public int ImageIndex => this._imgIndex;

  public int OrderID => 0;

  private void On_ctrl_Modified(object sender, EventArgs e) => this._pnlBottom.Enabled = true;

  private void _btnOK_Click(object sender, EventArgs e) => this.Save();

  private void _btnCancel_Click(object sender, EventArgs e)
  {
    this.UpdateDataOnControl();
    this._pnlBottom.Enabled = false;
  }

  public bool CanClose(object sender)
  {
    if (this._anControl.IsChanged)
    {
      switch (MessageBox.Show(this._saveDialogMessage, this._saveDialogCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Cancel:
          return false;
        case DialogResult.No:
          this._anControl.ResetChanges();
          this._pnlBottom.Enabled = false;
          return true;
        default:
          this.Save();
          break;
      }
    }
    return true;
  }

  public bool CanDeactivate(object sender) => this.CanClose(sender);

  private void Save()
  {
    this._anControl.Save();
    this._pnlBottom.Enabled = false;
    if (this._notificationService == null)
      return;
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._objID));
  }

  private void UpdateDataOnControl()
  {
    if (this._anControl != null)
      this._pnl.Controls.Remove((Control) this._anControl);
    this._anControl = new AutoNotificationControl(this._objID);
    this._anControl.Dock = DockStyle.Fill;
    this._anControl.Visible = true;
    this._anControl.Modified += new EventHandler(this.On_ctrl_Modified);
    this._pnl.Controls.Add((Control) this._anControl);
  }

  private void InitServices()
  {
    if (this._notificationService != null)
      return;
    this._notificationService = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
  }

  protected virtual void ReleaseServices()
  {
    this._notificationService = (INotificationService) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._pnl = new Panel();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    this._pnlBottom.Dock = DockStyle.Bottom;
    this._pnlBottom.Location = new Point(0, 145);
    this._pnlBottom.Name = "_pnlBottom";
    this._pnlBottom.Size = new Size(374, 40);
    this._pnlBottom.TabIndex = 9;
    this._btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnCancel.ImeMode = ImeMode.NoControl;
    this._btnCancel.Location = new Point(250, 5);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(121, 27);
    this._btnCancel.TabIndex = 1;
    this._btnCancel.Text = "Отмена";
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    this._btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnOK.ImeMode = ImeMode.NoControl;
    this._btnOK.Location = new Point(123, 5);
    this._btnOK.Name = "_btnOK";
    this._btnOK.Size = new Size(121, 27);
    this._btnOK.TabIndex = 0;
    this._btnOK.Text = "Применить";
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    this._pnl.Dock = DockStyle.Fill;
    this._pnl.Location = new Point(0, 0);
    this._pnl.Name = "_pnl";
    this._pnl.Size = new Size(374, 145);
    this._pnl.TabIndex = 10;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnl);
    this.Controls.Add((Control) this._pnlBottom);
    this.Name = nameof (AutoNotificationSettingsView);
    this.Size = new Size(374, 185);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class AutoNotificationSettingsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Workflow.Client_103"),
        ImageIndex = namedImageList.ImageIndex("imgCard"),
        OrderID = 0
      };
    }
  }
}
