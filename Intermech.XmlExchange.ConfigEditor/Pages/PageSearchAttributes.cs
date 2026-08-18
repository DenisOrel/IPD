// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageSearchAttributes
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageSearchAttributes : UserControl, IPageConfigEditor
{
  private List<XmlExchangeImportAttrTypeBase> _attributes;
  private XmlExchangeImportAttributes _ruleItemAttributes;
  private XDocument _oldValue;
  private List<XDocument> _copyAttrList;
  private object _selectNode;
  private bool _editData;
  private bool _readOnly;
  private string _guidTypeObject;
  private System.Type _typeCopyAttr;
  private ConfigEditorHelper _helper;
  private IContainer components;
  private SplitContainer splitPageAttribytes;
  private ListView lvSearchAttributes;
  private PropertyGrid gvSettingsAttribyte;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem addMenu;
  private ToolStripMenuItem removeMenu;
  private ColumnHeader columnHeader1;
  private ToolStripMenuItem addCustomMenu;
  private ToolStripMenuItem copyMenu;
  private ToolStripMenuItem pasteMenu;
  private ToolStripMenuItem moveMenu;
  private ToolStripMenuItem moveInStartMenu;
  private ToolStripMenuItem moveInUpMenu;
  private ToolStripMenuItem moveInDownMenu;
  private ToolStripMenuItem moveInEndMenu;

  public event EventHandler ModifyData;

  public PageSearchAttributes() => this.InitializeComponent();

  public void InitializeCustomComponent()
  {
    this._helper = ConfigEditorHelper.GetHelper();
    if (this._helper != null && !this.DesignMode)
      this.lvSearchAttributes.SmallImageList = this._helper.CategoryIcons.ImageList;
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.gvSettingsAttribyte.Font = font;
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

  public string PageName => "Атрибуты поиска";

  public void UpdateView() => this.LvUpdateView();

  private void ContextMenu_Opening(object sender, CancelEventArgs e)
  {
    Point client = this.lvSearchAttributes.PointToClient(this._contextMenu.Bounds.Location);
    ListViewItem itemAt = this.lvSearchAttributes.GetItemAt(client.X, client.Y);
    if (itemAt == null)
    {
      this.removeMenu.Visible = false;
      this.copyMenu.Visible = false;
      this.moveMenu.Visible = false;
    }
    else
    {
      this.removeMenu.Visible = true;
      this.copyMenu.Visible = true;
      this.moveMenu.Visible = true;
      foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) this.moveMenu.DropDownItems)
        dropDownItem.Enabled = true;
      if (itemAt.Tag is XmlExchangeImportAttrTypeBase tag)
      {
        int num = this._attributes.IndexOf(tag);
        if (num == 0)
        {
          this.moveInStartMenu.Enabled = false;
          this.moveInUpMenu.Enabled = false;
        }
        if (num == this._attributes.IndexOf(this._attributes.Last<XmlExchangeImportAttrTypeBase>()))
        {
          this.moveInDownMenu.Enabled = false;
          this.moveInEndMenu.Enabled = false;
        }
      }
    }
    if (this._copyAttrList != null)
      this.pasteMenu.Enabled = true;
    else
      this.pasteMenu.Enabled = false;
  }

  private void AddMenuLVAttributes_Click(object sender, EventArgs e)
  {
    IMSAttributeType imsAttributeType = this._helper.DiagSelectAttributeType(this._attributes.Select<XmlExchangeImportAttrTypeBase, int>((Func<XmlExchangeImportAttrTypeBase, int>) (a => MetaDataHelper.GetObjectTypeID(a.Guid))).ToList<int>(), this._guidTypeObject, (string) null);
    if (imsAttributeType == null)
      return;
    XmlExchangeImportAttrTypeBase attrType = this._ruleItemAttributes.CreateAttrType(imsAttributeType.AttributeGuid, imsAttributeType.Name);
    this._attributes.Add(attrType);
    ListViewItem listViewItem = new ListViewItem(attrType.Name);
    listViewItem.Tag = (object) attrType;
    listViewItem.ImageIndex = this._helper.IconsIndexOf(3, -1, (object) imsAttributeType.FieldType);
    this.lvSearchAttributes.Items.Add(listViewItem);
    this.SelectNewItem(listViewItem);
  }

  private void SelectNewItem(ListViewItem item)
  {
    this.lvSearchAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.lvSearchAttributes.SelectedItems.Clear();
    item.Focused = true;
    item.Selected = true;
    this.EditData = true;
  }

  private void LvSearchAttributes_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    this.gvSettingsAttribyte.SelectedObject = (object) null;
    if (this.lvSearchAttributes.SelectedItems.Count != 1)
      return;
    XmlExchangeImportAttrTypeBase type = this.lvSearchAttributes.SelectedItems[0].Tag.CastToType<XmlExchangeImportAttrTypeBase>();
    this.gvSettingsAttribyte.SelectedObject = (object) new GridViewSettingsImportAttrType(type, this._readOnly, this._helper.AtrTypeInBase(type.Guid));
  }

  public void LoadData(object selectNode, bool readOnly)
  {
    this._readOnly = readOnly;
    this._guidTypeObject = (string) null;
    this._oldValue = (XDocument) null;
    if (this._readOnly)
      this._contextMenu.Enabled = false;
    this._selectNode = selectNode;
    this._ruleItemAttributes = this._selectNode.CastToType<XmlExchangeImportAttributes>();
    if (this._ruleItemAttributes == null)
      return;
    this._guidTypeObject = this._ruleItemAttributes.Guid.ToString();
    this._attributes = this._ruleItemAttributes.Attributes;
    this._oldValue = this.SaveToXmlData(this._ruleItemAttributes.ImportItemSetting);
    this.lvSearchAttributes.Tag = (object) this._attributes;
    this.LvUpdateView();
  }

  private void LvUpdateView()
  {
    this.lvSearchAttributes.Items.Clear();
    this.gvSettingsAttribyte.SelectedObject = (object) null;
    foreach (XmlExchangeImportAttrTypeBase attribute in this._attributes)
    {
      ListViewItem listViewItem = new ListViewItem(attribute.Name);
      listViewItem.Tag = (object) attribute;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute.Guid);
      listViewItem.ImageIndex = attributeType == null ? (listViewItem.StateImageIndex = this._helper.IconsIndexOf(3, 0)) : this._helper.IconsIndexOf(3, -1, (object) attributeType.FieldType);
      this.SetFontAttrItem(listViewItem, attributeType != null);
      this.lvSearchAttributes.Items.Add(listViewItem);
    }
    this.lvSearchAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.lvSearchAttributes.Update();
  }

  private void UpdateSelectedItemView()
  {
    if (this.lvSearchAttributes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvSearchAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeImportAttrTypeBase tag)
        selectedItem.Text = tag.Name;
    }
    this.lvSearchAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
  }

  private void PGAttribute_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
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
      this._ruleItemAttributes.ImportItemSetting.Items.Clear();
      this._ruleItemAttributes.ImportItemSetting.Load(this._oldValue.Root);
      this._ruleItemAttributes.LoadData();
      this.LvUpdateView();
    }
    else
    {
      this._ruleItemAttributes.SaveData();
      this._oldValue = this.SaveToXmlData(this._ruleItemAttributes.ImportItemSetting);
    }
    this.EditData = false;
  }

  private XDocument SaveToXmlData(XmlImportBase хmlImportBase)
  {
    XDocument doc = new XDocument();
    XmlConfigEditorExtension.SaveXmlDocument(doc, хmlImportBase, (XElement) null);
    return doc;
  }

  private void RemoveMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this.lvSearchAttributes.SelectedItems.Count == 0 || this._attributes == null)
      return;
    foreach (ListViewItem selectedItem in this.lvSearchAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeImportAttrTypeBase tag && tag.RemoveItemSetting())
        this._attributes.Remove(tag);
    }
    this.EditData = true;
    this.UpdateView();
  }

  private void AddCustomMenuLVAttributes_Click(object sender, EventArgs e)
  {
    XmlExchangeImportAttrTypeBase attrType = this._ruleItemAttributes.CreateAttrType(Guid.Empty, "Новый тип атрибута");
    this._attributes.Add(attrType);
    ListViewItem listViewItem = new ListViewItem(attrType.Name);
    this.SetFontAttrItem(listViewItem, false);
    listViewItem.Tag = (object) attrType;
    listViewItem.ImageIndex = this._helper.IconsIndexOf(3, 0);
    this.lvSearchAttributes.Items.Add(listViewItem);
    this.SelectNewItem(listViewItem);
  }

  private void PasteMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this._copyAttrList == null || !(this._typeCopyAttr != (System.Type) null))
      return;
    int count = this._attributes.Count;
    foreach (XDocument copyAttr in this._copyAttrList)
    {
      XmlImportBase xmlImportBase = new XmlImportBase();
      XmlExchangeImportAttrTypeBase pasteAttrType;
      if ((pasteAttrType = Activator.CreateInstance(this._typeCopyAttr) as XmlExchangeImportAttrTypeBase) != null && xmlImportBase.Load(copyAttr.Root) && pasteAttrType.LoadData(xmlImportBase) && this._attributes.Where<XmlExchangeImportAttrTypeBase>((Func<XmlExchangeImportAttrTypeBase, bool>) (a =>
      {
        Guid guid = a.Guid;
        string str1 = guid.ToString();
        guid = pasteAttrType.Guid;
        string str2 = guid.ToString();
        return str1 == str2;
      })).ToList<XmlExchangeImportAttrTypeBase>().Count == 0)
      {
        pasteAttrType.ImportItemSetting.Owner = this._ruleItemAttributes.ImportItemSetting;
        this._attributes.Add(pasteAttrType);
      }
    }
    if (count != this._attributes.Count)
      this.EditData = true;
    this.LvUpdateView();
  }

  private void CopyMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this.lvSearchAttributes.SelectedItems.Count == 0)
      return;
    List<XmlExchangeImportAttrTypeBase> source = new List<XmlExchangeImportAttrTypeBase>();
    foreach (ListViewItem selectedItem in this.lvSearchAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeImportAttrTypeBase tag)
        source.Add(tag);
    }
    if (source.Count <= 0)
      return;
    this._typeCopyAttr = source.First<XmlExchangeImportAttrTypeBase>().GetType();
    this._copyAttrList = new List<XDocument>();
    foreach (XmlExchangeImportAttrTypeBase importAttrTypeBase in source)
    {
      importAttrTypeBase.SaveData();
      this._copyAttrList.Add(this.SaveToXmlData(importAttrTypeBase.ImportItemSetting));
    }
  }

  private void SetFontAttrItem(ListViewItem item, bool inBase)
  {
    Font font;
    if (inBase)
    {
      font = new Font(this.lvSearchAttributes.Font, FontStyle.Regular);
      item.ToolTipText = string.Empty;
    }
    else
    {
      font = new Font(this.lvSearchAttributes.Font, FontStyle.Italic);
      item.ToolTipText = "Тип атрибута отсутствует в базе";
    }
    item.Font = font;
  }

  private void moveInStartMenu_Click(object sender, EventArgs e)
  {
    ListViewItem listViewItem = this.ItemMenuClick();
    if (listViewItem == null)
      return;
    XmlExchangeImportAttrTypeBase tag = listViewItem.Tag as XmlExchangeImportAttrTypeBase;
    if (this._attributes.IndexOf(tag) == 0 || !this._attributes.Remove(tag))
      return;
    this._attributes.Insert(0, tag);
    this.lvSearchAttributes.Items.Remove(listViewItem);
    this.lvSearchAttributes.Items.Insert(0, listViewItem);
    this.EditData = true;
  }

  private void moveInUpMenu_Click(object sender, EventArgs e)
  {
    ListViewItem listViewItem = this.ItemMenuClick();
    if (listViewItem == null)
      return;
    XmlExchangeImportAttrTypeBase tag = listViewItem.Tag as XmlExchangeImportAttrTypeBase;
    int num = this._attributes.IndexOf(tag);
    if (num <= 0 || !this._attributes.Remove(tag))
      return;
    int index = num - 1;
    this._attributes.Insert(index, tag);
    this.lvSearchAttributes.Items.Remove(listViewItem);
    this.lvSearchAttributes.Items.Insert(index, listViewItem);
    this.EditData = true;
  }

  private void moveInDownMenu_Click(object sender, EventArgs e)
  {
    ListViewItem listViewItem = this.ItemMenuClick();
    if (listViewItem == null)
      return;
    XmlExchangeImportAttrTypeBase tag = listViewItem.Tag as XmlExchangeImportAttrTypeBase;
    int num = this._attributes.IndexOf(tag);
    if (num + 1 >= this._attributes.Count || !this._attributes.Remove(tag))
      return;
    int index = num + 1;
    this._attributes.Insert(index, tag);
    this.lvSearchAttributes.Items.Remove(listViewItem);
    this.lvSearchAttributes.Items.Insert(index, listViewItem);
    this.EditData = true;
  }

  private void moveInEndMenu_Click(object sender, EventArgs e)
  {
    ListViewItem listViewItem = this.ItemMenuClick();
    if (listViewItem == null)
      return;
    XmlExchangeImportAttrTypeBase tag = listViewItem.Tag as XmlExchangeImportAttrTypeBase;
    if (this._attributes.IndexOf(tag) + 1 >= this._attributes.Count || !this._attributes.Remove(tag))
      return;
    this._attributes.Add(tag);
    this.lvSearchAttributes.Items.Remove(listViewItem);
    this.lvSearchAttributes.Items.Add(listViewItem);
    this.EditData = true;
  }

  private ListViewItem ItemMenuClick()
  {
    Point client = this.lvSearchAttributes.PointToClient(this._contextMenu.Bounds.Location);
    return this.lvSearchAttributes.GetItemAt(client.X, client.Y);
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
    this.splitPageAttribytes = new SplitContainer();
    this.lvSearchAttributes = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this._contextMenu = new ContextMenuStrip(this.components);
    this.addMenu = new ToolStripMenuItem();
    this.removeMenu = new ToolStripMenuItem();
    this.addCustomMenu = new ToolStripMenuItem();
    this.copyMenu = new ToolStripMenuItem();
    this.pasteMenu = new ToolStripMenuItem();
    this.moveMenu = new ToolStripMenuItem();
    this.moveInStartMenu = new ToolStripMenuItem();
    this.moveInUpMenu = new ToolStripMenuItem();
    this.moveInDownMenu = new ToolStripMenuItem();
    this.moveInEndMenu = new ToolStripMenuItem();
    this.gvSettingsAttribyte = new PropertyGrid();
    this.splitPageAttribytes.BeginInit();
    this.splitPageAttribytes.Panel1.SuspendLayout();
    this.splitPageAttribytes.Panel2.SuspendLayout();
    this.splitPageAttribytes.SuspendLayout();
    this._contextMenu.SuspendLayout();
    this.SuspendLayout();
    this.splitPageAttribytes.Dock = DockStyle.Fill;
    this.splitPageAttribytes.Location = new Point(0, 0);
    this.splitPageAttribytes.Name = "splitPageAttribytes";
    this.splitPageAttribytes.Panel1.Controls.Add((Control) this.lvSearchAttributes);
    this.splitPageAttribytes.Panel1.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.Panel2.Controls.Add((Control) this.gvSettingsAttribyte);
    this.splitPageAttribytes.Panel2.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.Size = new Size(932, 553);
    this.splitPageAttribytes.SplitterDistance = 254;
    this.splitPageAttribytes.TabIndex = 3;
    this.lvSearchAttributes.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvSearchAttributes.ContextMenuStrip = this._contextMenu;
    this.lvSearchAttributes.Dock = DockStyle.Fill;
    this.lvSearchAttributes.HeaderStyle = ColumnHeaderStyle.None;
    this.lvSearchAttributes.HideSelection = false;
    this.lvSearchAttributes.Location = new Point(0, 0);
    this.lvSearchAttributes.Name = "lvSearchAttributes";
    this.lvSearchAttributes.Size = new Size(254, 553);
    this.lvSearchAttributes.TabIndex = 0;
    this.lvSearchAttributes.UseCompatibleStateImageBehavior = false;
    this.lvSearchAttributes.View = View.Details;
    this.lvSearchAttributes.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.LvSearchAttributes_ItemSelectionChanged);
    this.columnHeader1.Width = 300;
    this._contextMenu.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.addMenu,
      (ToolStripItem) this.removeMenu,
      (ToolStripItem) this.addCustomMenu,
      (ToolStripItem) this.copyMenu,
      (ToolStripItem) this.pasteMenu,
      (ToolStripItem) this.moveMenu
    });
    this._contextMenu.Name = "contextLVAttributes";
    this._contextMenu.Size = new Size(182, 158);
    this._contextMenu.Opening += new CancelEventHandler(this.ContextMenu_Opening);
    this.addMenu.Name = "addMenu";
    this.addMenu.Size = new Size(181, 22);
    this.addMenu.Text = "Добавить";
    this.addMenu.Click += new EventHandler(this.AddMenuLVAttributes_Click);
    this.removeMenu.Name = "removeMenu";
    this.removeMenu.Size = new Size(181, 22);
    this.removeMenu.Text = "Удалить";
    this.removeMenu.Click += new EventHandler(this.RemoveMenuLVAttributes_Click);
    this.addCustomMenu.Name = "addCustomMenu";
    this.addCustomMenu.Size = new Size(181, 22);
    this.addCustomMenu.Text = "Добавить в ручную";
    this.addCustomMenu.Click += new EventHandler(this.AddCustomMenuLVAttributes_Click);
    this.copyMenu.Name = "copyMenu";
    this.copyMenu.Size = new Size(181, 22);
    this.copyMenu.Text = "Копировать";
    this.copyMenu.Click += new EventHandler(this.CopyMenuLVAttributes_Click);
    this.pasteMenu.Name = "pasteMenu";
    this.pasteMenu.Size = new Size(181, 22);
    this.pasteMenu.Text = "Вставить";
    this.pasteMenu.Click += new EventHandler(this.PasteMenuLVAttributes_Click);
    this.moveMenu.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.moveInStartMenu,
      (ToolStripItem) this.moveInUpMenu,
      (ToolStripItem) this.moveInDownMenu,
      (ToolStripItem) this.moveInEndMenu
    });
    this.moveMenu.Name = "moveMenu";
    this.moveMenu.Size = new Size(181, 22);
    this.moveMenu.Text = "Переместить";
    this.moveInStartMenu.Name = "moveInStartMenu";
    this.moveInStartMenu.Size = new Size(200, 22);
    this.moveInStartMenu.Text = "В начало";
    this.moveInStartMenu.Click += new EventHandler(this.moveInStartMenu_Click);
    this.moveInUpMenu.Name = "moveInUpMenu";
    this.moveInUpMenu.Size = new Size(200, 22);
    this.moveInUpMenu.Text = "На один уровень вверх";
    this.moveInUpMenu.Click += new EventHandler(this.moveInUpMenu_Click);
    this.moveInDownMenu.Name = "moveInDownMenu";
    this.moveInDownMenu.Size = new Size(200, 22);
    this.moveInDownMenu.Text = "На один уровень вниз";
    this.moveInDownMenu.Click += new EventHandler(this.moveInDownMenu_Click);
    this.moveInEndMenu.Name = "moveInEndMenu";
    this.moveInEndMenu.Size = new Size(200, 22);
    this.moveInEndMenu.Text = "В конец ";
    this.moveInEndMenu.Click += new EventHandler(this.moveInEndMenu_Click);
    this.gvSettingsAttribyte.Dock = DockStyle.Fill;
    this.gvSettingsAttribyte.Location = new Point(0, 0);
    this.gvSettingsAttribyte.Name = "gvSettingsAttribyte";
    this.gvSettingsAttribyte.RightToLeft = RightToLeft.No;
    this.gvSettingsAttribyte.Size = new Size(674, 553);
    this.gvSettingsAttribyte.TabIndex = 1;
    this.gvSettingsAttribyte.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PGAttribute_PropertyValueChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitPageAttribytes);
    this.Name = nameof (PageSearchAttributes);
    this.Size = new Size(932, 553);
    this.splitPageAttribytes.Panel1.ResumeLayout(false);
    this.splitPageAttribytes.Panel2.ResumeLayout(false);
    this.splitPageAttribytes.EndInit();
    this.splitPageAttribytes.ResumeLayout(false);
    this._contextMenu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
