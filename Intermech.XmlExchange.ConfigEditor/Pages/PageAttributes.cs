// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageAttributes
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageAttributes : UserControl, IPageConfigEditor
{
  private XmlExchangeExportAttrList _attrList;
  private List<XmlNode> _oldValue;
  private List<XmlNode> _copyAttrList;
  private object _selectNode;
  private bool _editData;
  private bool _readOnly;
  private bool _typeInBase;
  private string _guidTypeObject;
  private string _guidTypeRelation;
  private ConfigEditorHelper _helper;
  private IContainer components;
  private SplitContainer splitPageAttribytes;
  private ListView lvAttributes;
  private PropertyGrid pgAttribute;
  private ContextMenuStrip menuForLVAttributes;
  private ToolStripMenuItem addMenuLVAttributes;
  private ToolStripMenuItem removeMenuLVAttributes;
  private ToolStripMenuItem allAttributesMenuLVAttributes;
  private ColumnHeader columnHeader1;
  private ToolStripMenuItem addCustomMenuLVAttributes;
  private ToolStripMenuItem copyMenuLVAttributes;
  private ToolStripMenuItem pasteMenuLVAttributes;

  public event EventHandler ModifyData;

  public PageAttributes() => this.InitializeComponent();

  public void InitializeCustomComponent()
  {
    this._helper = ConfigEditorHelper.GetHelper();
    if (this._helper != null && !this.DesignMode)
      this.lvAttributes.SmallImageList = this._helper.CategoryIcons.ImageList;
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.pgAttribute.Font = font;
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

  public string PageName => "Атрибуты";

  public void UpdateView() => this.LvUpdateView();

  private void menuForLVAttributes_Opening(object sender, CancelEventArgs e)
  {
    Point client = this.lvAttributes.PointToClient(this.menuForLVAttributes.Bounds.Location);
    if (this.lvAttributes.GetItemAt(client.X, client.Y) == null)
    {
      this.removeMenuLVAttributes.Visible = false;
      this.copyMenuLVAttributes.Visible = false;
    }
    else
    {
      this.removeMenuLVAttributes.Visible = true;
      this.copyMenuLVAttributes.Visible = true;
    }
    if (!this._typeInBase)
      this.allAttributesMenuLVAttributes.Visible = false;
    else
      this.allAttributesMenuLVAttributes.Visible = true;
    if (this._copyAttrList != null)
      this.pasteMenuLVAttributes.Enabled = true;
    else
      this.pasteMenuLVAttributes.Enabled = false;
  }

  private void AddMenuLVAttributes_Click(object sender, EventArgs e)
  {
    IMSAttributeType imsAttributeType = this._helper.DiagSelectAttributeType(this._attrList.Select<XmlExchangeExportAttr, int>((System.Func<XmlExchangeExportAttr, int>) (a => a.ID)).ToList<int>(), this._guidTypeObject, this._guidTypeRelation);
    if (imsAttributeType == null)
      return;
    XmlExchangeExportAttr type = new XmlExchangeExportAttr(imsAttributeType.AttributeID, imsAttributeType.AttributeGuid, imsAttributeType.Name);
    this._attrList.Add(type);
    ListViewItem listViewItem = new ListViewItem(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
    this.SetFontAttrItem(listViewItem, this._helper.AtrTypeInBase(type.TypeGuid, type.TypeID));
    listViewItem.Tag = (object) type;
    listViewItem.ImageIndex = this._helper.IconsIndexOf(3, -1, (object) imsAttributeType.FieldType);
    this.lvAttributes.Items.Add(listViewItem);
    this.SelectNewItem(listViewItem);
  }

  private void SelectNewItem(ListViewItem item)
  {
    this.lvAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.lvAttributes.SelectedItems.Clear();
    item.Focused = true;
    item.Selected = true;
    this.EditData = true;
  }

  private void LVAttributes_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    this.pgAttribute.SelectedObject = (object) null;
    if (this.lvAttributes.SelectedItems.Count != 1)
      return;
    XmlExchangeExportAttr type = this.lvAttributes.SelectedItems[0].Tag.CastToType<XmlExchangeExportAttr>();
    this.pgAttribute.SelectedObject = (object) new GridViewSettingsExportAttr(type, this._readOnly, this._helper.AtrTypeInBase(type.TypeGuid));
  }

  public void LoadData(object selectNode, bool readOnly)
  {
    this._readOnly = readOnly;
    this._guidTypeObject = (string) null;
    this._guidTypeRelation = (string) null;
    this._typeInBase = false;
    this._oldValue = (List<XmlNode>) null;
    if (this._readOnly)
      this.menuForLVAttributes.Enabled = false;
    this._selectNode = selectNode;
    XmlExchangeExportAttributable type = this._selectNode.CastToType<XmlExchangeExportAttributable>();
    if (type == null)
      return;
    this._attrList = type.AttrList;
    this._oldValue = this.SaveToXmlData(this._attrList);
    this.lvAttributes.Tag = (object) this._attrList;
    this.LvUpdateView();
    if (type is XmlExchangeExportObj && this._helper.ObjTypeInBase(type.TypeGuid))
    {
      this._guidTypeObject = MetaDataHelper.GetObjectTypeGuid(type.TypeID).ToString();
      this._typeInBase = true;
    }
    else
    {
      if (!(type is XmlExchangeExportRel) || !this._helper.RelTypeInBase(type.TypeGuid))
        return;
      this._guidTypeRelation = MetaDataHelper.GetRelationTypeGuid(type.TypeID).ToString();
      this._typeInBase = true;
    }
  }

  private void LvUpdateView()
  {
    this.lvAttributes.Items.Clear();
    this.pgAttribute.SelectedObject = (object) null;
    foreach (XmlExchangeExportAttr type in (IEnumerable<XmlExchangeExportAttr>) this._attrList.OrderBy<XmlExchangeExportAttr, string>((System.Func<XmlExchangeExportAttr, string>) (a => a.CastToType<XmlExchangeExportAttr>().TypeName)))
    {
      ListViewItem listViewItem = new ListViewItem(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
      listViewItem.Tag = (object) type;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(type.TypeID);
      listViewItem.ImageIndex = attributeType == null ? (listViewItem.StateImageIndex = this._helper.IconsIndexOf(3, 0)) : this._helper.IconsIndexOf(3, -1, (object) attributeType.FieldType);
      this.SetFontAttrItem(listViewItem, this._helper.AtrTypeInBase(type.TypeGuid, type.TypeID));
      this.lvAttributes.Items.Add(listViewItem);
    }
    this.lvAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    this.lvAttributes.Update();
  }

  private void UpdateSelectedItemView()
  {
    if (this.lvAttributes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportAttr tag)
        selectedItem.Text = this._helper.ExportTypedName((XmlExchangeExportTypedItem) tag);
    }
    this.lvAttributes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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
      this._attrList.Clear();
      this._attrList.AddRange((IEnumerable<XmlExchangeExportAttr>) this.LoadInXmlData(this._oldValue));
      this.LvUpdateView();
    }
    else
      this._oldValue = this.SaveToXmlData(this._attrList);
    this.EditData = false;
  }

  private List<XmlNode> SaveToXmlData(XmlExchangeExportAttrList attrList)
  {
    XmlDocument xmlDoc = new XmlDocument();
    List<XmlNode> xmlData = new List<XmlNode>();
    foreach (XmlExchangeExportItem attr in (List<XmlExchangeExportAttr>) attrList)
    {
      XmlNode xmlNode = attr.SaveData(xmlDoc);
      if (xmlNode != null)
        xmlData.Add(xmlNode);
    }
    return xmlData;
  }

  private List<XmlExchangeExportAttr> LoadInXmlData(List<XmlNode> xmlNodeList)
  {
    List<XmlExchangeExportAttr> exchangeExportAttrList = new List<XmlExchangeExportAttr>();
    foreach (XmlNode xmlNode in xmlNodeList)
    {
      XmlExchangeExportAttr exchangeExportAttr = new XmlExchangeExportAttr();
      if (exchangeExportAttr.LoadData(xmlNode))
        exchangeExportAttrList.Add(exchangeExportAttr);
    }
    return exchangeExportAttrList;
  }

  private void RemoveMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this.lvAttributes.SelectedItems.Count == 0 || !(this.lvAttributes.Tag is XmlExchangeExportAttrList tag1))
      return;
    foreach (ListViewItem selectedItem in this.lvAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportAttr tag2)
        tag1.Remove(tag2);
    }
    this.EditData = true;
    this.UpdateView();
  }

  private void AllAttributesMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this._selectNode == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return;
      IDBAttributableType attributableType = (IDBAttributableType) null;
      if (this._selectNode is XmlExchangeExportObj selectNode2)
        attributableType = (IDBAttributableType) session.GetObjectType(selectNode2.TypeID);
      else if (this._selectNode is XmlExchangeExportRel selectNode1)
        attributableType = (IDBAttributableType) session.GetRelationType(selectNode1.TypeID);
      if (attributableType == null)
        return;
      DataTable dataTable = attributableType.Attributes.Select((string) null, (object[]) null);
      if (dataTable.Rows.Count == 0)
        return;
      XmlExchangeExportAttrList attrList = ((XmlExchangeExportAttributable) this._selectNode).AttrList;
      List<int> list = attrList.Select<XmlExchangeExportAttr, int>((System.Func<XmlExchangeExportAttr, int>) (a => a.TypeID)).ToList<int>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int result;
        if (int.TryParse(row["F_ATTRIBUTE_ID"].ToString(), out result) && !list.Contains(result))
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(result);
          XmlExchangeExportAttr exchangeExportAttr = new XmlExchangeExportAttr(attributeType.AttributeID, attributeType.AttributeGuid, attributeType.Name);
          attrList.Add(exchangeExportAttr);
        }
      }
    }
    this.EditData = true;
    this.UpdateView();
  }

  private void AddCustomMenuLVAttributes_Click(object sender, EventArgs e)
  {
    XmlExchangeExportAttr type = new XmlExchangeExportAttr(-1);
    type.TypeName = "Новый тип атрибута";
    this._attrList.Add(type);
    ListViewItem listViewItem = new ListViewItem(this._helper.ExportTypedName((XmlExchangeExportTypedItem) type));
    this.SetFontAttrItem(listViewItem, false);
    listViewItem.Tag = (object) type;
    listViewItem.ImageIndex = this._helper.IconsIndexOf(3, 0);
    this.lvAttributes.Items.Add(listViewItem);
    this.SelectNewItem(listViewItem);
  }

  private void PasteMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this._copyAttrList == null)
      return;
    XmlExchangeExportAttrList exchangeExportAttrList = new XmlExchangeExportAttrList();
    exchangeExportAttrList.AddRange((IEnumerable<XmlExchangeExportAttr>) this.LoadInXmlData(this._copyAttrList));
    if (exchangeExportAttrList.Count > 0)
    {
      int count = this._attrList.Count;
      foreach (XmlExchangeExportAttr exchangeExportAttr in (List<XmlExchangeExportAttr>) exchangeExportAttrList)
      {
        XmlExchangeExportAttr pasteAttr = exchangeExportAttr;
        if (this._attrList.Count<XmlExchangeExportAttr>((System.Func<XmlExchangeExportAttr, bool>) (a => a.TypeID == pasteAttr.TypeID)) == 0)
          this._attrList.Add(pasteAttr);
      }
      if (count != this._attrList.Count)
        this.EditData = true;
    }
    this.LvUpdateView();
  }

  private void CopyMenuLVAttributes_Click(object sender, EventArgs e)
  {
    if (this.lvAttributes.SelectedItems.Count == 0)
      return;
    XmlExchangeExportAttrList attrList = new XmlExchangeExportAttrList();
    foreach (ListViewItem selectedItem in this.lvAttributes.SelectedItems)
    {
      if (selectedItem.Tag is XmlExchangeExportAttr tag)
        attrList.Add(tag);
    }
    if (attrList.Count <= 0)
      return;
    this._copyAttrList = this.SaveToXmlData(attrList);
  }

  private void SetFontAttrItem(ListViewItem item, bool inBase)
  {
    Font font;
    if (inBase)
    {
      font = new Font(this.lvAttributes.Font, FontStyle.Regular);
      item.ToolTipText = string.Empty;
    }
    else
    {
      font = new Font(this.lvAttributes.Font, FontStyle.Italic);
      item.ToolTipText = "Тип атрибута отсутствует в базе";
    }
    item.Font = font;
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
    this.lvAttributes = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.menuForLVAttributes = new ContextMenuStrip(this.components);
    this.addMenuLVAttributes = new ToolStripMenuItem();
    this.removeMenuLVAttributes = new ToolStripMenuItem();
    this.allAttributesMenuLVAttributes = new ToolStripMenuItem();
    this.addCustomMenuLVAttributes = new ToolStripMenuItem();
    this.copyMenuLVAttributes = new ToolStripMenuItem();
    this.pasteMenuLVAttributes = new ToolStripMenuItem();
    this.pgAttribute = new PropertyGrid();
    this.splitPageAttribytes.BeginInit();
    this.splitPageAttribytes.Panel1.SuspendLayout();
    this.splitPageAttribytes.Panel2.SuspendLayout();
    this.splitPageAttribytes.SuspendLayout();
    this.menuForLVAttributes.SuspendLayout();
    this.SuspendLayout();
    this.splitPageAttribytes.Dock = DockStyle.Fill;
    this.splitPageAttribytes.Location = new Point(0, 0);
    this.splitPageAttribytes.Name = "splitPageAttribytes";
    this.splitPageAttribytes.Panel1.Controls.Add((Control) this.lvAttributes);
    this.splitPageAttribytes.Panel1.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.Panel2.Controls.Add((Control) this.pgAttribute);
    this.splitPageAttribytes.Panel2.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.RightToLeft = RightToLeft.No;
    this.splitPageAttribytes.Size = new Size(932, 553);
    this.splitPageAttribytes.SplitterDistance = 254;
    this.splitPageAttribytes.TabIndex = 3;
    this.lvAttributes.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvAttributes.ContextMenuStrip = this.menuForLVAttributes;
    this.lvAttributes.Dock = DockStyle.Fill;
    this.lvAttributes.HeaderStyle = ColumnHeaderStyle.None;
    this.lvAttributes.HideSelection = false;
    this.lvAttributes.Location = new Point(0, 0);
    this.lvAttributes.Name = "lvAttributes";
    this.lvAttributes.Size = new Size(254, 553);
    this.lvAttributes.Sorting = SortOrder.Ascending;
    this.lvAttributes.TabIndex = 0;
    this.lvAttributes.UseCompatibleStateImageBehavior = false;
    this.lvAttributes.View = View.Details;
    this.lvAttributes.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.LVAttributes_ItemSelectionChanged);
    this.columnHeader1.Width = 300;
    this.menuForLVAttributes.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.addMenuLVAttributes,
      (ToolStripItem) this.removeMenuLVAttributes,
      (ToolStripItem) this.allAttributesMenuLVAttributes,
      (ToolStripItem) this.addCustomMenuLVAttributes,
      (ToolStripItem) this.copyMenuLVAttributes,
      (ToolStripItem) this.pasteMenuLVAttributes
    });
    this.menuForLVAttributes.Name = "contextLVAttributes";
    this.menuForLVAttributes.Size = new Size(182, 158);
    this.menuForLVAttributes.Opening += new CancelEventHandler(this.menuForLVAttributes_Opening);
    this.addMenuLVAttributes.Name = "addMenuLVAttributes";
    this.addMenuLVAttributes.Size = new Size(181, 22);
    this.addMenuLVAttributes.Text = "Добавить";
    this.addMenuLVAttributes.Click += new EventHandler(this.AddMenuLVAttributes_Click);
    this.removeMenuLVAttributes.Name = "removeMenuLVAttributes";
    this.removeMenuLVAttributes.Size = new Size(181, 22);
    this.removeMenuLVAttributes.Text = "Удалить";
    this.removeMenuLVAttributes.Click += new EventHandler(this.RemoveMenuLVAttributes_Click);
    this.allAttributesMenuLVAttributes.Name = "allAttributesMenuLVAttributes";
    this.allAttributesMenuLVAttributes.Size = new Size(181, 22);
    this.allAttributesMenuLVAttributes.Text = "Все атрибуты типа";
    this.allAttributesMenuLVAttributes.Click += new EventHandler(this.AllAttributesMenuLVAttributes_Click);
    this.addCustomMenuLVAttributes.Name = "addCustomMenuLVAttributes";
    this.addCustomMenuLVAttributes.Size = new Size(181, 22);
    this.addCustomMenuLVAttributes.Text = "Добавить в ручную";
    this.addCustomMenuLVAttributes.Click += new EventHandler(this.AddCustomMenuLVAttributes_Click);
    this.copyMenuLVAttributes.Name = "copyMenuLVAttributes";
    this.copyMenuLVAttributes.Size = new Size(181, 22);
    this.copyMenuLVAttributes.Text = "Копировать";
    this.copyMenuLVAttributes.Click += new EventHandler(this.CopyMenuLVAttributes_Click);
    this.pasteMenuLVAttributes.Name = "pasteMenuLVAttributes";
    this.pasteMenuLVAttributes.Size = new Size(181, 22);
    this.pasteMenuLVAttributes.Text = "Вставить";
    this.pasteMenuLVAttributes.Click += new EventHandler(this.PasteMenuLVAttributes_Click);
    this.pgAttribute.Dock = DockStyle.Fill;
    this.pgAttribute.Location = new Point(0, 0);
    this.pgAttribute.Name = "pgAttribute";
    this.pgAttribute.RightToLeft = RightToLeft.No;
    this.pgAttribute.Size = new Size(674, 553);
    this.pgAttribute.TabIndex = 1;
    this.pgAttribute.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PGAttribute_PropertyValueChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitPageAttribytes);
    this.Name = nameof (PageAttributes);
    this.Size = new Size(932, 553);
    this.splitPageAttribytes.Panel1.ResumeLayout(false);
    this.splitPageAttribytes.Panel2.ResumeLayout(false);
    this.splitPageAttribytes.EndInit();
    this.splitPageAttribytes.ResumeLayout(false);
    this.menuForLVAttributes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
