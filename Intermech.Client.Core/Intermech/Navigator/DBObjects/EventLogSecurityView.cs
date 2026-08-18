
// Type: Intermech.Navigator.DBObjects.EventLogSecurityView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Security;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Summary description for ObjectSecurityView.</summary>
public class EventLogSecurityView : UserControl, IView
{
  private int _imageIndex;
  private long _objID;
  private Button applyBtn;
  private Button cancelBtn;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool _firstEnter;
  private SecurityControl securityControl;
  private bool wasChanged;

  public EventLogSecurityView()
  {
    this.InitializeComponent();
    this._imageIndex = -1;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventLogSecurityView));
    this.applyBtn = new Button();
    this.cancelBtn = new Button();
    this.securityControl = new SecurityControl();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.applyBtn, "applyBtn");
    this.applyBtn.Name = "applyBtn";
    this.applyBtn.Click += new EventHandler(this.applyBtn_Click);
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.Name = "cancelBtn";
    this.cancelBtn.Click += new EventHandler(this.cancelBtn_Click);
    componentResourceManager.ApplyResources((object) this.securityControl, "securityControl");
    this.securityControl.FocusedUserId = (object) null;
    this.securityControl.Name = "securityControl";
    this.securityControl.Readonly = false;
    this.securityControl.SecurityChanged += new SecurityControl.SecurityChangedEventHandler(this.securityControl_SecurityChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.securityControl);
    this.Controls.Add((Control) this.cancelBtn);
    this.Controls.Add((Control) this.applyBtn);
    this.Name = nameof (EventLogSecurityView);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._firstEnter = true;
  }

  public void Activate(IView previousView)
  {
    if (!this._firstEnter)
      return;
    this.FillData((object) null);
    this.SetWasChanged(false);
    this._firstEnter = false;
  }

  public void Deactivate(IView nextView)
  {
    this.Check4Save();
    this.SetWasChanged(false);
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_154");

  public int OrderID => 60;

  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgKeys");
      return this._imageIndex;
    }
  }

  private void securityControl_SecurityChanged(object sender, EventArgs e)
  {
    this.SetWasChanged(true);
  }

  private void RefreshControls()
  {
    this.applyBtn.Enabled = this.wasChanged;
    this.cancelBtn.Enabled = this.wasChanged;
  }

  private void applyBtn_Click(object sender, EventArgs e) => this.SaveData();

  private void cancelBtn_Click(object sender, EventArgs e)
  {
    this.FillData(this.securityControl.FocusedUserId);
    this.SetWasChanged(false);
  }

  private void Check4Save()
  {
    if (!this.wasChanged || MessageBox.Show(MessageDialogs.msgReallySave, MessageDialogs.msgNeedSave, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.SaveData();
  }

  private void FillData(object focusedUserId)
  {
    this.securityControl.LoadSecurity(new object[1]
    {
      (object) this._objID
    }, (ISecurityCallback) new EventLogCallbackClass());
    this.securityControl.FocusedUserId = focusedUserId;
  }

  private void SaveData()
  {
    if (!this.wasChanged)
      return;
    this.securityControl.SaveSecurity();
    this.SetWasChanged(false);
  }

  private void SetWasChanged(bool b)
  {
    this.wasChanged = b;
    this.RefreshControls();
  }
}
