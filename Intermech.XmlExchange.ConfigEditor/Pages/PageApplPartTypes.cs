// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageApplPartTypes
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.XmlExchange.ConfigEditor.ExportApplSetting;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageApplPartTypes : UserControl, IPageConfigEditor
{
  private object _selectNode;
  private bool _editData;
  private bool _readOnly;
  private ConfigEditorHelper _helper;
  private TypeObjectConverter _converterType;
  private IContainer components;
  private SplitContainer splitPageApplPartTypes;
  private ListView LVApplPartTypes;
  private PropertyGrid PGApplPartTypes;
  private ContextMenuStrip menuForLVApplPartTypes;
  private ToolStripMenuItem addPartTypeMenu;
  private ToolStripMenuItem deletePartTypeMenu;
  private ToolStripMenuItem addCustomPartTypeMenu;

  public event EventHandler ModifyData;

  public PageApplPartTypes() => this.InitializeComponent();

  public void InitializeCustomComponent()
  {
    this._helper = ConfigEditorHelper.GetHelper();
    if (this._helper != null)
    {
      this.LVApplPartTypes.SmallImageList = this._helper.CategoryIcons.ImageList;
      this.menuForLVApplPartTypes.ImageList = this._helper.CategoryIcons.ImageList;
      this.menuForLVApplPartTypes.ImageScalingSize = new Size(32 /*0x20*/, 16 /*0x10*/);
      this.addPartTypeMenu.ImageIndex = this._helper.IconsIndexOf(4, 0);
      this.addCustomPartTypeMenu.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 0)], this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 1)]);
      this.deletePartTypeMenu.Image = (Image) ConfigEditorHelper.MergeTwoImages(this._helper.ImageIcon("imgDelete"), this._helper.CategoryIcons.ImageList.Images[this._helper.IconsIndexOf(4, 0)]);
    }
    this._converterType = new TypeObjectConverter();
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.PGApplPartTypes.Font = font;
  }

  public bool EditData
  {
    get => this._editData;
    private set
    {
      this._editData = value;
      if (!this._editData)
        return;
      EventHandler modifyData = this.ModifyData;
      if (modifyData == null)
        return;
      modifyData((object) this, (EventArgs) null);
    }
  }

  public string PageName => "Дочерние типы";

  public void LoadData(object selectNode, bool readOnly)
  {
    this._readOnly = readOnly;
    if (readOnly)
      this.menuForLVApplPartTypes.Enabled = false;
    this._selectNode = selectNode;
    this.LVApplPartTypes.Items.Clear();
    this.PGApplPartTypes.SelectedObject = (object) null;
    if (!(this._selectNode is ExportApplRelationType selectNode1))
      return;
    foreach (XmlExchangeExportAppl getApplSetting in selectNode1.GetApplSettings)
    {
      if (selectNode1.TypeId == getApplSetting.RelTypeID && selectNode1.GetProjType.TypeId == getApplSetting.ProjTypeID)
        this.LVApplPartTypes.Items.Add(new ListViewItem(this._converterType.ConvertToListView((object) getApplSetting.PartTypeGuid).ToString())
        {
          ImageIndex = !this._helper.ObjTypeInBase(getApplSetting.PartTypeGuid) ? this._helper.IconsIndexOf(4, 0) : this._helper.IconsIndexOf(4, getApplSetting.PartTypeID),
          Tag = (object) getApplSetting
        });
    }
    this.LVApplPartTypes.Update();
  }

  public void UpdateView() => this.LoadData(this._selectNode, this._readOnly);

  public void SaveData(bool save, bool refresh)
  {
    if (!this.EditData)
      return;
    if (this.PGApplPartTypes.SelectedObject != null)
    {
      if (this.PGApplPartTypes.SelectedObject is IConfigItemProperties selectedObject)
      {
        if (save)
          selectedObject.SaveSettings();
        else
          selectedObject.ResetSettings();
      }
      this.PGApplPartTypes.Refresh();
    }
    this.EditData = false;
  }

  private void AddMenuLVApplPartTypes_Click(object sender, EventArgs e)
  {
    if (!(this._selectNode is ExportApplRelationType selectNode))
      return;
    List<int> objtypes = new List<int>();
    foreach (XmlExchangeExportAppl getApplSetting in selectNode.GetApplSettings)
    {
      if (selectNode.TypeId == getApplSetting.RelTypeID && selectNode.GetProjType.TypeId == getApplSetting.ProjTypeID)
        objtypes.Add(getApplSetting.PartTypeID);
    }
    IMSObjectType imsObjectType = this._helper.DiagSelectObjectType(objtypes);
    if (imsObjectType != null)
    {
      XmlExchangeExportAppl exchangeExportAppl = new XmlExchangeExportAppl();
      exchangeExportAppl.ProjTypeGuid = selectNode.GetProjType.TypeGuid;
      exchangeExportAppl.ProjTypeID = selectNode.GetProjType.TypeId;
      exchangeExportAppl.RelTypeGuid = selectNode.TypeGuid;
      exchangeExportAppl.RelTypeID = selectNode.TypeId;
      exchangeExportAppl.PartTypeGuid = imsObjectType.Guid;
      exchangeExportAppl.PartTypeID = imsObjectType.ObjectTypeID;
      selectNode.GetApplSettings.Add(exchangeExportAppl);
      this.LVApplPartTypes.Items.Add(new ListViewItem(this._converterType.ConvertToListView((object) exchangeExportAppl.PartTypeGuid).ToString())
      {
        ImageIndex = this._helper.IconsIndexOf(4, exchangeExportAppl.PartTypeID),
        Tag = (object) exchangeExportAppl
      });
    }
    this.LVApplPartTypes.Update();
  }

  private void DeleteMenuLVApplPartTypes_Click(object sender, EventArgs e)
  {
    if (this.LVApplPartTypes.SelectedItems.Count == 0 || !(this._selectNode is ExportApplRelationType selectNode))
      return;
    foreach (ListViewItem selectedItem in this.LVApplPartTypes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportAppl tag)
        selectNode.GetApplSettings.Remove(tag);
    }
    this.UpdateView();
  }

  private void menuForLVApplPartTypes_Opening(object sender, CancelEventArgs e)
  {
    Point client = this.LVApplPartTypes.PointToClient(this.menuForLVApplPartTypes.Bounds.Location);
    this.deletePartTypeMenu.Visible = this.LVApplPartTypes.GetItemAt(client.X, client.Y) != null;
  }

  private void LVApplPartTypes_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    this.PGApplPartTypes.SelectedObject = (object) null;
    if (this.LVApplPartTypes.SelectedItems.Count != 1 || !(this.LVApplPartTypes.SelectedItems[0].Tag is XmlExchangeExportAppl tag))
      return;
    this.PGApplPartTypes.SelectedObject = (object) new GridViewSettingsAppl(tag, this._helper.ObjTypeInBase(tag.PartTypeGuid), this._readOnly);
  }

  private void PGApplPartTypes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.EditData = true;
  }

  private void AddCustomMenuLVApplPartTypes_Click(object sender, EventArgs e)
  {
    if (!(this._selectNode is ExportApplRelationType selectNode))
      return;
    XmlExchangeExportAppl exchangeExportAppl = new XmlExchangeExportAppl();
    exchangeExportAppl.ProjTypeGuid = selectNode.GetProjType.TypeGuid;
    exchangeExportAppl.ProjTypeID = selectNode.GetProjType.TypeId;
    exchangeExportAppl.RelTypeGuid = selectNode.TypeGuid;
    exchangeExportAppl.RelTypeID = selectNode.TypeId;
    exchangeExportAppl.PartTypeGuid = Guid.NewGuid();
    exchangeExportAppl.PartTypeID = -1000;
    selectNode.GetApplSettings.Add(exchangeExportAppl);
    this.LVApplPartTypes.Items.Add(new ListViewItem(this._converterType.ConvertToListView((object) exchangeExportAppl.PartTypeGuid).ToString())
    {
      ImageIndex = this._helper.IconsIndexOf(4, exchangeExportAppl.PartTypeID),
      Tag = (object) exchangeExportAppl
    });
    this.LVApplPartTypes.Update();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.splitPageApplPartTypes = new SplitContainer();
    this.LVApplPartTypes = new ListView();
    this.menuForLVApplPartTypes = new ContextMenuStrip(this.components);
    this.addPartTypeMenu = new ToolStripMenuItem();
    this.addCustomPartTypeMenu = new ToolStripMenuItem();
    this.deletePartTypeMenu = new ToolStripMenuItem();
    this.PGApplPartTypes = new PropertyGrid();
    this.splitPageApplPartTypes.BeginInit();
    this.splitPageApplPartTypes.Panel1.SuspendLayout();
    this.splitPageApplPartTypes.Panel2.SuspendLayout();
    this.splitPageApplPartTypes.SuspendLayout();
    this.menuForLVApplPartTypes.SuspendLayout();
    this.SuspendLayout();
    this.splitPageApplPartTypes.Dock = DockStyle.Fill;
    this.splitPageApplPartTypes.Location = new Point(0, 0);
    this.splitPageApplPartTypes.Name = "splitPageApplPartTypes";
    this.splitPageApplPartTypes.Panel1.Controls.Add((Control) this.LVApplPartTypes);
    this.splitPageApplPartTypes.Panel1.RightToLeft = RightToLeft.No;
    this.splitPageApplPartTypes.Panel2.Controls.Add((Control) this.PGApplPartTypes);
    this.splitPageApplPartTypes.Panel2.RightToLeft = RightToLeft.No;
    this.splitPageApplPartTypes.RightToLeft = RightToLeft.No;
    this.splitPageApplPartTypes.Size = new Size(969, 475);
    this.splitPageApplPartTypes.SplitterDistance = 263;
    this.splitPageApplPartTypes.TabIndex = 5;
    this.LVApplPartTypes.ContextMenuStrip = this.menuForLVApplPartTypes;
    this.LVApplPartTypes.Dock = DockStyle.Fill;
    this.LVApplPartTypes.HideSelection = false;
    this.LVApplPartTypes.Location = new Point(0, 0);
    this.LVApplPartTypes.Name = "LVApplPartTypes";
    this.LVApplPartTypes.Size = new Size(263, 475);
    this.LVApplPartTypes.TabIndex = 0;
    this.LVApplPartTypes.UseCompatibleStateImageBehavior = false;
    this.LVApplPartTypes.View = View.List;
    this.LVApplPartTypes.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.LVApplPartTypes_ItemSelectionChanged);
    this.menuForLVApplPartTypes.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.addPartTypeMenu,
      (ToolStripItem) this.addCustomPartTypeMenu,
      (ToolStripItem) this.deletePartTypeMenu
    });
    this.menuForLVApplPartTypes.Name = "menuForLVApplPartTypes";
    this.menuForLVApplPartTypes.Size = new Size(257, 92);
    this.menuForLVApplPartTypes.Opening += new CancelEventHandler(this.menuForLVApplPartTypes_Opening);
    this.addPartTypeMenu.Name = "addPartTypeMenu";
    this.addPartTypeMenu.Size = new Size(256 /*0x0100*/, 22);
    this.addPartTypeMenu.Text = "Добавить тип дочернего объекта";
    this.addPartTypeMenu.Click += new EventHandler(this.AddMenuLVApplPartTypes_Click);
    this.addCustomPartTypeMenu.Name = "addCustomPartTypeMenu";
    this.addCustomPartTypeMenu.Size = new Size(256 /*0x0100*/, 22);
    this.addCustomPartTypeMenu.Text = "Добавить пользовательский тип";
    this.addCustomPartTypeMenu.Click += new EventHandler(this.AddCustomMenuLVApplPartTypes_Click);
    this.deletePartTypeMenu.Name = "deletePartTypeMenu";
    this.deletePartTypeMenu.Size = new Size(256 /*0x0100*/, 22);
    this.deletePartTypeMenu.Text = "Удалить дочерний тип";
    this.deletePartTypeMenu.Click += new EventHandler(this.DeleteMenuLVApplPartTypes_Click);
    this.PGApplPartTypes.Dock = DockStyle.Fill;
    this.PGApplPartTypes.Location = new Point(0, 0);
    this.PGApplPartTypes.Name = "PGApplPartTypes";
    this.PGApplPartTypes.RightToLeft = RightToLeft.No;
    this.PGApplPartTypes.Size = new Size(702, 475);
    this.PGApplPartTypes.TabIndex = 1;
    this.PGApplPartTypes.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PGApplPartTypes_PropertyValueChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitPageApplPartTypes);
    this.Name = nameof (PageApplPartTypes);
    this.Size = new Size(969, 475);
    this.splitPageApplPartTypes.Panel1.ResumeLayout(false);
    this.splitPageApplPartTypes.Panel2.ResumeLayout(false);
    this.splitPageApplPartTypes.EndInit();
    this.splitPageApplPartTypes.ResumeLayout(false);
    this.menuForLVApplPartTypes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
