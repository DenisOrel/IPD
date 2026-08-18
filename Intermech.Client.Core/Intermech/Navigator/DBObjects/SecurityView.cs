
// Type: Intermech.Navigator.DBObjects.SecurityView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Summary description for ObjectSecurityView.</summary>
[ViewDescriptionProvider(typeof (SecurityView.SecurityViewDescriptionProvider))]
public class SecurityView : UserControl, IView
{
  private int _imageIndex;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  protected List<object> _objIDlist = new List<object>();
  protected List<int> _objTypeIDlist = new List<int>();
  private bool _firstEnter;
  private Panel panelButtons;
  private Button cancelBtn;
  private Button applyBtn;
  private Panel panel1;
  private SecurityControl securityControl;
  private bool wasChanged;
  private object _focusedUserId;

  public SecurityView()
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityView));
    this.panelButtons = new Panel();
    this.cancelBtn = new Button();
    this.applyBtn = new Button();
    this.panel1 = new Panel();
    this.securityControl = new SecurityControl();
    this.panelButtons.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.Controls.Add((Control) this.cancelBtn);
    this.panelButtons.Controls.Add((Control) this.applyBtn);
    componentResourceManager.ApplyResources((object) this.panelButtons, "panelButtons");
    this.panelButtons.Name = "panelButtons";
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.Name = "cancelBtn";
    this.cancelBtn.Click += new EventHandler(this.cancelBtn_Click);
    componentResourceManager.ApplyResources((object) this.applyBtn, "applyBtn");
    this.applyBtn.Name = "applyBtn";
    this.applyBtn.Click += new EventHandler(this.applyBtn_Click);
    this.panel1.Controls.Add((Control) this.securityControl);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.securityControl, "securityControl");
    this.securityControl.FocusedUserId = (object) null;
    this.securityControl.Name = "securityControl";
    this.securityControl.Readonly = false;
    this.securityControl.SecurityChanged += new SecurityControl.SecurityChangedEventHandler(this.securityControl_SecurityChanged);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panelButtons);
    this.Name = nameof (SecurityView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "       ";
    this.panelButtons.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._objIDlist.Clear();
    this._objTypeIDlist.Clear();
    this.InitData(items);
    this._firstEnter = true;
  }

  public void Activate(IView previousView)
  {
    if (!this._firstEnter)
      return;
    this.FillData(this._focusedUserId);
    this.SetWasChanged(false);
    this._firstEnter = false;
  }

  public void Deactivate(IView nextView)
  {
    this._focusedUserId = this.securityControl.FocusedUserId;
    this.Check4Save();
    this.SetWasChanged(false);
  }

  public virtual string Caption => LocalizationHolder.rm.GetString("Client.Core_154");

  public virtual int OrderID => 60;

  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgKeys");
      return this._imageIndex;
    }
  }

  public virtual void InitData(ISelectedItems items)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        this._objIDlist.Add((object) itemData.ObjectID);
        this._objTypeIDlist.Add(itemData.ObjectType);
      }
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
    this.FillData(this.securityControl.FocusedUserId);
  }

  private void FillData(object focusedUserId)
  {
    this.securityControl.LoadSecurity(this._objIDlist.ToArray(), (ISecurityCallback) new SecurityCallbackClass());
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

  private sealed class SecurityViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_154"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgKeys"),
        OrderID = 60
      };
    }
  }
}
