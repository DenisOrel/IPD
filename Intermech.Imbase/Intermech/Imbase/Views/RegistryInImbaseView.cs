// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.RegistryInImbaseView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class RegistryInImbaseView : UserControl, IView, ISelectedItemsHost
{
  private AdvancedServiceContainer _srvProvider = new AdvancedServiceContainer();
  private int _itemType = -1;
  private ISelectedItems _selectedItems;
  private IContainer components;
  private CheckBox _chbNeedDelObj;
  private RadioButton _radioFolderType;
  private RadioButton _radioCatalogRecordType;
  private ImbaseTableView _tblView;
  private Panel _pnlTop;

  public RegistryInImbaseView()
  {
    this.InitializeComponent();
    this._chbNeedDelObj.Checked = false;
    this.ImageIndex = -1;
    this.OrderID = 0;
    this.Caption = LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_ViewCaption");
  }

  public int ImageIndex { get; private set; }

  public int OrderID { get; private set; }

  public string Caption { get; private set; }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._selectedItems = items;
    this._srvProvider.AdvancedProvider = services;
    this._radioFolderType.Checked = true;
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    if (itemData != null)
    {
      this._itemType = itemData.ObjectType;
      if (this._itemType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        this._chbNeedDelObj.Visible = true;
        this._radioFolderType.Visible = this._radioCatalogRecordType.Visible = false;
        this._tblView.Initialize(items, services);
        this._tblView.Visible = true;
      }
      else if (this._itemType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
      {
        this._chbNeedDelObj.Visible = this._radioFolderType.Visible = this._radioCatalogRecordType.Visible = true;
        this._tblView.Visible = false;
      }
      else
        this._chbNeedDelObj.Visible = this._radioFolderType.Visible = this._radioCatalogRecordType.Visible = this._tblView.Visible = false;
    }
    else
      this._chbNeedDelObj.Visible = this._radioFolderType.Visible = this._radioCatalogRecordType.Visible = this._tblView.Visible = false;
  }

  public void Deactivate(IView nextView)
  {
    if (this._srvProvider.GetService(typeof (IRegistryInImbase)) is RegistryInImbaseSrv service)
    {
      service.DelSourceObj = this._chbNeedDelObj.Checked;
      service.DestionationObjTypeID = this._radioFolderType.Checked ? Intermech.Imbase.Consts.ImbaseFolderTypeID : (this._radioCatalogRecordType.Checked ? Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID : -1);
    }
    if (this._itemType != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      return;
    this._tblView.Deactivate(nextView);
    this._tblView.TblView.Grid.DoubleClick -= new EventHandler(this.Grid_DoubleClick);
  }

  public void Activate(IView previousView)
  {
    if (this._itemType != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      return;
    this._tblView.Activate(previousView);
    this._tblView.TblView.Grid.DoubleClick += new EventHandler(this.Grid_DoubleClick);
  }

  private void Grid_DoubleClick(object sender, EventArgs e)
  {
    if (this.SelectedItems.Count <= 0 || this._srvProvider.AdvancedProvider == null || !(this._srvProvider.AdvancedProvider.GetService(typeof (ISelectionWindow)) is ISelectionWindow service))
      return;
    if (service is ICurrentSelectedItemsHost selectedItemsHost)
      selectedItemsHost.ItemsHost = (ISelectedItemsHost) this;
    service.OkButton.PerformClick();
  }

  public ISelectedItems SelectedItems => this._selectedItems;

  public event EventHandler SelectedItemsChanged;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RegistryInImbaseView));
    this._chbNeedDelObj = new CheckBox();
    this._radioFolderType = new RadioButton();
    this._radioCatalogRecordType = new RadioButton();
    this._tblView = new ImbaseTableView();
    this._pnlTop = new Panel();
    this._pnlTop.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._chbNeedDelObj, "_chbNeedDelObj");
    this._chbNeedDelObj.Name = "_chbNeedDelObj";
    this._chbNeedDelObj.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._radioFolderType, "_radioFolderType");
    this._radioFolderType.Name = "_radioFolderType";
    this._radioFolderType.TabStop = true;
    this._radioFolderType.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._radioCatalogRecordType, "_radioCatalogRecordType");
    this._radioCatalogRecordType.Name = "_radioCatalogRecordType";
    this._radioCatalogRecordType.TabStop = true;
    this._radioCatalogRecordType.UseVisualStyleBackColor = true;
    this._tblView.Control = (object) this._tblView;
    componentResourceManager.ApplyResources((object) this._tblView, "_tblView");
    this._tblView.Name = "_tblView";
    this._tblView.Services = (System.IServiceProvider) null;
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._pnlTop.Controls.Add((Control) this._chbNeedDelObj);
    this._pnlTop.Controls.Add((Control) this._radioFolderType);
    this._pnlTop.Controls.Add((Control) this._radioCatalogRecordType);
    this._pnlTop.Name = "_pnlTop";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._tblView);
    this.Controls.Add((Control) this._pnlTop);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(600, 150);
    this.Name = nameof (RegistryInImbaseView);
    this._pnlTop.ResumeLayout(false);
    this._pnlTop.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
