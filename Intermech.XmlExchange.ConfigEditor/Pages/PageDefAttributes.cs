// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageDefAttributes
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageDefAttributes : UserControl, IPageConfigEditor
{
  private object _selectNode;
  private XmlExchangeExportDefAttrValueList _defAttrList;
  private List<XmlNode> _oldValue;
  private bool _editData;
  private bool _readOnly;
  private ConfigEditorHelper _helper;
  private IContainer components;
  private SplitContainer splitPageDefAttributes;
  private ListView LVDefAttributes;
  private PropertyGrid PGDefAttribute;
  private ContextMenuStrip MenuForLVDefAttributes;
  private ToolStripMenuItem CreateMenuLVDefAttributes;
  private ToolStripMenuItem DeleteMenuLVDefAttributes;
  private ColumnHeader columnHeader1;

  public event EventHandler ModifyData;

  public PageDefAttributes() => this.InitializeComponent();

  public void InitializeCustomComponent()
  {
    this._helper = ConfigEditorHelper.GetHelper();
    if (this._helper != null && !this.DesignMode)
    {
      this.LVDefAttributes.SmallImageList = this._helper.CategoryIcons.ImageList;
      this.LVDefAttributes.SmallImageList = this._helper.CategoryIcons.ImageList;
    }
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.PGDefAttribute.Font = font;
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

  public string PageName => "Атрибуты по умолчанию";

  public void UpdateView() => this.LvUpdateView();

  private void UpdateSelectedItemView()
  {
    if (this.LVDefAttributes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.LVDefAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportDefAttrValue tag)
      {
        selectedItem.Text = tag.UserName;
        selectedItem.ImageIndex = this._helper.IconsIndexOf(3, -1, (object) (FieldTypes) Convert.ToInt32(tag.UserFldType));
      }
    }
    this.LVDefAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
  }

  public void LoadData(object selectNode, bool readOnly)
  {
    this._readOnly = readOnly;
    if (this._readOnly)
      this.MenuForLVDefAttributes.Enabled = false;
    this._selectNode = selectNode;
    if (!(this._selectNode is XmlExchangeExportAttributable selectNode1))
      return;
    this._defAttrList = selectNode1.DefAttrList;
    this._oldValue = this.SaveToXmlData(this._defAttrList);
    this.LVDefAttributes.Tag = (object) this._defAttrList;
    this.LvUpdateView();
  }

  private void LvUpdateView()
  {
    this.LVDefAttributes.Items.Clear();
    this.PGDefAttribute.SelectedObject = (object) null;
    foreach (XmlExchangeExportDefAttrValue defAttr in (List<XmlExchangeExportDefAttrValue>) this._defAttrList)
      this.LVDefAttributes.Items.Add(new ListViewItem(defAttr.UserName)
      {
        Tag = (object) defAttr,
        ImageIndex = this._helper.IconsIndexOf(3, -1, (object) (FieldTypes) Convert.ToInt32(defAttr.UserFldType))
      });
    if (this.LVDefAttributes.Items.Count > 0)
      this.LVDefAttributes.Sorting = SortOrder.Ascending;
    this.LVDefAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.LVDefAttributes.Update();
  }

  private void DeleteMenuLVDefAttributes_Click(object sender, EventArgs e)
  {
    if (this.LVDefAttributes.SelectedItems.Count == 0 || !(this.LVDefAttributes.Tag is XmlExchangeExportDefAttrValueList tag1))
      return;
    foreach (ListViewItem selectedItem in this.LVDefAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportDefAttrValue tag2)
        tag1.Remove(tag2);
    }
    this.EditData = true;
    this.UpdateView();
  }

  private void CreateMenuLVDefAttributes_Click(object sender, EventArgs e)
  {
    if (!(this.LVDefAttributes.Tag is XmlExchangeExportDefAttrValueList tag))
      return;
    XmlExchangeExportDefAttrValue exportDefAttrValue = new XmlExchangeExportDefAttrValue();
    exportDefAttrValue.UserName = "Новый тип атрибута";
    exportDefAttrValue.UserFldType = (object) 1;
    tag.Add(exportDefAttrValue);
    ListViewItem listViewItem = new ListViewItem(exportDefAttrValue.UserName);
    listViewItem.Tag = (object) exportDefAttrValue;
    listViewItem.ImageIndex = this._helper.IconsIndexOf(3, -1, (object) (FieldTypes) exportDefAttrValue.UserFldType);
    this.LVDefAttributes.Items.Add(listViewItem);
    this.SelectNewItem(listViewItem);
  }

  private void SelectNewItem(ListViewItem item)
  {
    this.LVDefAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.LVDefAttributes.SelectedItems.Clear();
    item.Focused = true;
    item.Selected = true;
    this.EditData = true;
  }

  private void MenuForLVDefAttributes_Opening(object sender, CancelEventArgs e)
  {
    Point client = this.LVDefAttributes.PointToClient(this.MenuForLVDefAttributes.Bounds.Location);
    if (this.LVDefAttributes.GetItemAt(client.X, client.Y) == null)
      this.DeleteMenuLVDefAttributes.Visible = false;
    else
      this.DeleteMenuLVDefAttributes.Visible = true;
  }

  private void LVDefAttributes_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    this.PGDefAttribute.SelectedObject = (object) null;
    if (this.LVDefAttributes.SelectedItems.Count != 1)
      return;
    this.PGDefAttribute.SelectedObject = (object) new GridViewSettingsDefAttrValue(this.LVDefAttributes.SelectedItems[0].Tag as XmlExchangeExportDefAttrValue, this._readOnly);
  }

  private void PGDefAttribute_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.EditData = true;
    this.UpdateSelectedItemView();
  }

  public void SaveData(bool save, bool refresh)
  {
    if (!this.EditData)
      return;
    if (!save)
    {
      this._defAttrList.Clear();
      this._defAttrList.AddRange((IEnumerable<XmlExchangeExportDefAttrValue>) this.LoadInXmlData(this._oldValue));
      this.LvUpdateView();
    }
    else
      this._oldValue = this.SaveToXmlData(this._defAttrList);
    this.EditData = false;
  }

  private List<XmlNode> SaveToXmlData(XmlExchangeExportDefAttrValueList attrList)
  {
    XmlDocument xmlDoc = new XmlDocument();
    List<XmlNode> xmlData = new List<XmlNode>();
    foreach (XmlExchangeExportItem attr in (List<XmlExchangeExportDefAttrValue>) attrList)
    {
      XmlNode xmlNode = attr.SaveData(xmlDoc);
      if (xmlNode != null)
        xmlData.Add(xmlNode);
    }
    return xmlData;
  }

  private List<XmlExchangeExportDefAttrValue> LoadInXmlData(List<XmlNode> xmlNodeList)
  {
    List<XmlExchangeExportDefAttrValue> exportDefAttrValueList = new List<XmlExchangeExportDefAttrValue>();
    foreach (XmlNode xmlNode in xmlNodeList)
    {
      XmlExchangeExportDefAttrValue exportDefAttrValue = new XmlExchangeExportDefAttrValue();
      if (exportDefAttrValue.LoadData(xmlNode))
        exportDefAttrValueList.Add(exportDefAttrValue);
    }
    return exportDefAttrValueList;
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
    this.splitPageDefAttributes = new SplitContainer();
    this.LVDefAttributes = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.MenuForLVDefAttributes = new ContextMenuStrip(this.components);
    this.CreateMenuLVDefAttributes = new ToolStripMenuItem();
    this.DeleteMenuLVDefAttributes = new ToolStripMenuItem();
    this.PGDefAttribute = new PropertyGrid();
    this.splitPageDefAttributes.BeginInit();
    this.splitPageDefAttributes.Panel1.SuspendLayout();
    this.splitPageDefAttributes.Panel2.SuspendLayout();
    this.splitPageDefAttributes.SuspendLayout();
    this.MenuForLVDefAttributes.SuspendLayout();
    this.SuspendLayout();
    this.splitPageDefAttributes.Dock = DockStyle.Fill;
    this.splitPageDefAttributes.Location = new Point(0, 0);
    this.splitPageDefAttributes.Name = "splitPageDefAttributes";
    this.splitPageDefAttributes.Panel1.Controls.Add((Control) this.LVDefAttributes);
    this.splitPageDefAttributes.Panel1.RightToLeft = RightToLeft.No;
    this.splitPageDefAttributes.Panel2.Controls.Add((Control) this.PGDefAttribute);
    this.splitPageDefAttributes.Panel2.RightToLeft = RightToLeft.No;
    this.splitPageDefAttributes.RightToLeft = RightToLeft.No;
    this.splitPageDefAttributes.Size = new Size(933, 436);
    this.splitPageDefAttributes.SplitterDistance = 254;
    this.splitPageDefAttributes.TabIndex = 4;
    this.LVDefAttributes.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.LVDefAttributes.ContextMenuStrip = this.MenuForLVDefAttributes;
    this.LVDefAttributes.Dock = DockStyle.Fill;
    this.LVDefAttributes.HeaderStyle = ColumnHeaderStyle.None;
    this.LVDefAttributes.HideSelection = false;
    this.LVDefAttributes.Location = new Point(0, 0);
    this.LVDefAttributes.Name = "LVDefAttributes";
    this.LVDefAttributes.Size = new Size(254, 436);
    this.LVDefAttributes.TabIndex = 0;
    this.LVDefAttributes.UseCompatibleStateImageBehavior = false;
    this.LVDefAttributes.View = View.Details;
    this.LVDefAttributes.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.LVDefAttributes_ItemSelectionChanged);
    this.columnHeader1.Width = 200;
    this.MenuForLVDefAttributes.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.CreateMenuLVDefAttributes,
      (ToolStripItem) this.DeleteMenuLVDefAttributes
    });
    this.MenuForLVDefAttributes.Name = "MenuForLVDefAttributes";
    this.MenuForLVDefAttributes.Size = new Size(165, 48 /*0x30*/);
    this.MenuForLVDefAttributes.Opening += new CancelEventHandler(this.MenuForLVDefAttributes_Opening);
    this.CreateMenuLVDefAttributes.Name = "CreateMenuLVDefAttributes";
    this.CreateMenuLVDefAttributes.Size = new Size(164, 22);
    this.CreateMenuLVDefAttributes.Text = "Создать атрибут";
    this.CreateMenuLVDefAttributes.Click += new EventHandler(this.CreateMenuLVDefAttributes_Click);
    this.DeleteMenuLVDefAttributes.Name = "DeleteMenuLVDefAttributes";
    this.DeleteMenuLVDefAttributes.Size = new Size(164, 22);
    this.DeleteMenuLVDefAttributes.Text = "Удалить атрибут";
    this.DeleteMenuLVDefAttributes.Click += new EventHandler(this.DeleteMenuLVDefAttributes_Click);
    this.PGDefAttribute.Dock = DockStyle.Fill;
    this.PGDefAttribute.Location = new Point(0, 0);
    this.PGDefAttribute.Name = "PGDefAttribute";
    this.PGDefAttribute.RightToLeft = RightToLeft.No;
    this.PGDefAttribute.Size = new Size(675, 436);
    this.PGDefAttribute.TabIndex = 1;
    this.PGDefAttribute.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PGDefAttribute_PropertyValueChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitPageDefAttributes);
    this.Name = nameof (PageDefAttributes);
    this.Size = new Size(933, 436);
    this.splitPageDefAttributes.Panel1.ResumeLayout(false);
    this.splitPageDefAttributes.Panel2.ResumeLayout(false);
    this.splitPageDefAttributes.EndInit();
    this.splitPageDefAttributes.ResumeLayout(false);
    this.MenuForLVDefAttributes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
