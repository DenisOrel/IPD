// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.OptionsForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Actions;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public class OptionsForm : Form
{
  public List<Guid> _GuidLst = new List<Guid>();
  private SelectorFilter _selectorFilter = new SelectorFilter();
  private HashSet<int> _objTypes;
  private HashSet<IDBTypedObjectID> _items;
  private IContainer components;
  private GroupBox groupBox1;
  private CheckBox cbCatalogRecProps;
  private CheckBox cbTableDataProps;
  private CheckBox cbTableRefProps;
  private CheckBox cbFolderProps;
  private CheckBox cbCatalogProps;
  private GroupBox groupBox2;
  private TableLayoutPanel _btnsLayoutPnl;
  private Button _btnTopmost;
  private Button _btnBottommost;
  private Button _btnBottom;
  private Button _btnTop;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btnReplace;
  private Button btnRemove;
  private Button btnAdd;
  private Panel panel1;
  private Button btnCancel;
  private Button btnExport;
  private ImageList _imgList;
  private ListView listView;
  private ColumnHeader naimHeader;
  private ActionList actList;
  private Intermech.Actions.Action _actAdd;
  private Intermech.Actions.Action _actRemove;
  private Intermech.Actions.Action _actReplace;
  private Intermech.Actions.Action _actUp;
  private Intermech.Actions.Action _actTop;
  private Intermech.Actions.Action _actDown;
  private Intermech.Actions.Action _actBottom;
  private CheckBox cbNameReferences;

  public UnloadFlags Flags
  {
    get
    {
      UnloadFlags flags = UnloadFlags.None;
      if (this.cbCatalogProps.Checked)
        flags |= UnloadFlags.Catalog;
      if (this.cbFolderProps.Checked)
        flags |= UnloadFlags.Folder;
      if (this.cbTableRefProps.Checked)
        flags |= UnloadFlags.TableRef;
      if (this.cbTableDataProps.Checked)
        flags |= UnloadFlags.TableData;
      if (this.cbCatalogRecProps.Checked)
        flags |= UnloadFlags.CatalogRec;
      if (this.cbNameReferences.Checked)
        flags |= UnloadFlags.NameObjectReferences;
      return flags;
    }
    internal set
    {
      this.cbCatalogProps.Checked = (value & UnloadFlags.Catalog) == UnloadFlags.Catalog;
      this.cbFolderProps.Checked = (value & UnloadFlags.Folder) == UnloadFlags.Folder;
      this.cbTableRefProps.Checked = (value & UnloadFlags.TableRef) == UnloadFlags.TableRef;
      this.cbTableDataProps.Checked = (value & UnloadFlags.TableData) == UnloadFlags.TableData;
      this.cbCatalogRecProps.Checked = (value & UnloadFlags.CatalogRec) == UnloadFlags.CatalogRec;
      this.cbNameReferences.Checked = (value & UnloadFlags.NameObjectReferences) == UnloadFlags.NameObjectReferences;
    }
  }

  public OptionsForm() => this.InitializeComponent();

  public OptionsForm(HashSet<int> objTypes, HashSet<IDBTypedObjectID> items)
    : this()
  {
    this._objTypes = objTypes;
    this._items = items;
    this.listView.SmallImageList = ServiceHolder.CategoryTypeIconService.ImageList;
    if (this._objTypes.Contains(Intermech.Imbase.Consts.ImbaseCatalogTypeID))
      this.cbTableDataProps.Checked = this.cbTableRefProps.Checked = this.cbFolderProps.Checked = this.cbCatalogProps.Checked = true;
    if (this._objTypes.Contains(Intermech.Imbase.Consts.ImbaseFolderTypeID))
      this.cbTableDataProps.Checked = this.cbTableRefProps.Checked = this.cbFolderProps.Checked = true;
    if (this._objTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID))
      this.cbTableDataProps.Checked = this.cbTableRefProps.Checked = true;
    this.cbNameReferences.Checked = this.cbTableDataProps.Checked;
    if (this._items.Count<IDBTypedObjectID>() != 1 || !this._objTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        throw new Exception("Интерфейс серверной части IMBASE не найден!");
      string filter = "";
      AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
      ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
      customService.LoadRecords(sessionKeeper.Session.SessionGUID, this._items.First<IDBTypedObjectID>().ObjectID, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out DataTable _, out columnsAttributes, out keyInfo);
      FieldTypes[] forbidenAttrTypes = new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      };
      this.AddItems(((IEnumerable<AttributeTypeProperties>) ((IEnumerable<AttributeTypeProperties>) columnsAttributes).Where<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (x => !((IEnumerable<FieldTypes>) forbidenAttrTypes).Contains<FieldTypes>(x.FieldType))).ToArray<AttributeTypeProperties>()).Select<AttributeTypeProperties, Guid>((System.Func<AttributeTypeProperties, Guid>) (x => x.AttributeGuid)).ToList<Guid>());
    }
  }

  private void ShowAttrDialog()
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.ShowCreateAttrBtn = true;
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) this._selectorFilter;
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count == 0)
        return;
      this._selectorFilter.IdsList.UnionWith((IEnumerable<int>) attributesSelectDlg.SelectedAttributesID);
      this.AddItems(attributesSelectDlg.SelectedAttributesGuid);
    }
  }

  private void AddItems(List<Guid> list)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (Guid anAttributeGuid in list)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid, false);
        if (attributeType != null)
        {
          this.listView.Items.Add(attributeType.Name, ServiceHolder.CategoryTypeIconService.IndexOf(3, -1, (object) attributeType.AttributeType)).Tag = (object) anAttributeGuid;
          this._GuidLst.Add(anAttributeGuid);
        }
      }
    }
  }

  private void RemoveItems()
  {
    foreach (ListViewItem selectedItem in this.listView.SelectedItems)
    {
      Guid tag = (Guid) selectedItem.Tag;
      int attributeId = MetaDataHelper.GetAttributeID((object) tag);
      this.listView.Items.Remove(selectedItem);
      this._GuidLst.Remove(tag);
      this._selectorFilter.IdsList.Remove(attributeId);
    }
  }

  private void ReplaceItem()
  {
    if (this.listView.SelectedItems.Count != 1)
      return;
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    Guid tag = (Guid) selectedItem.Tag;
    int attributeId = MetaDataHelper.GetAttributeID((object) tag);
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.ShowCreateAttrBtn = true;
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) this._selectorFilter;
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count == 0)
        return;
      Guid anAttributeGuid = attributesSelectDlg.SelectedAttributesGuid[0];
      int num = attributesSelectDlg.SelectedAttributesID[0];
      int index1 = this._GuidLst.IndexOf(tag);
      this._GuidLst.RemoveAt(index1);
      this._GuidLst.Insert(index1, anAttributeGuid);
      this._selectorFilter.IdsList.Remove(attributeId);
      this._selectorFilter.IdsList.Add(num);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.listView.BeginUpdate();
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid, false);
        int index2 = selectedItem.Index;
        this.listView.Items.Remove(selectedItem);
        ListViewItem listViewItem = this.listView.Items.Insert(index2, new ListViewItem(attributeType.Name, ServiceHolder.CategoryTypeIconService.IndexOf(3, -1, (object) attributeType.AttributeType)));
        listViewItem.Tag = (object) anAttributeGuid;
        listViewItem.Selected = true;
        this.listView.EndUpdate();
      }
    }
  }

  private void _actAdd_Execute(object sender, EventArgs e) => this.ShowAttrDialog();

  private void _actRemove_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actRemove.Enabled = this.listView.SelectedItems.Count > 0;
  }

  private void _actReplace_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actReplace.Enabled = this.listView.SelectedItems.Count == 1;
  }

  private void _actRemove_Execute(object sender, EventArgs e) => this.RemoveItems();

  private void _actReplace_Execute(object sender, EventArgs e) => this.ReplaceItem();

  private void _actUp_Execute(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count != 1 || this.listView.SelectedItems[0].Index <= 0)
      return;
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    int index = selectedItem.Index;
    this.listView.Items.RemoveAt(index);
    this.listView.Items.Insert(index - 1, selectedItem);
    this._GuidLst.RemoveAt(index);
    this._GuidLst.Insert(index - 1, (Guid) selectedItem.Tag);
  }

  private void _actUp_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actUp.Enabled = this.listView.SelectedItems.Count == 1 && this.listView.SelectedItems[0].Index > 0;
  }

  private void _actTop_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actTop.Enabled = this.listView.SelectedItems.Count == 1 && this.listView.SelectedItems[0].Index > 0;
  }

  private void _actTop_Execute(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count != 1 || this.listView.SelectedItems[0].Index <= 0)
      return;
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    int index = selectedItem.Index;
    this.listView.Items.RemoveAt(index);
    this.listView.Items.Insert(0, selectedItem);
    this._GuidLst.RemoveAt(index);
    this._GuidLst.Insert(0, (Guid) selectedItem.Tag);
  }

  private void _actDown_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actDown.Enabled = this.listView.SelectedItems.Count == 1 && this.listView.SelectedItems[0].Index < this.listView.Items.Count - 1;
  }

  private void _actDown_Execute(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count != 1 || this.listView.SelectedItems[0].Index >= this.listView.Items.Count - 1)
      return;
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    int index = selectedItem.Index;
    this.listView.Items.RemoveAt(index);
    this.listView.Items.Insert(index + 1, selectedItem);
    this._GuidLst.RemoveAt(index);
    this._GuidLst.Insert(index + 1, (Guid) selectedItem.Tag);
  }

  private void _actBottom_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._actBottom.Enabled = this.listView.SelectedItems.Count == 1 && this.listView.SelectedItems[0].Index < this.listView.Items.Count - 1;
  }

  private void _actBottom_Execute(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count != 1 || this.listView.SelectedItems[0].Index >= this.listView.Items.Count - 1)
      return;
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    int index = selectedItem.Index;
    this.listView.Items.RemoveAt(index);
    this.listView.Items.Add(selectedItem);
    this._GuidLst.RemoveAt(index);
    this._GuidLst.Add((Guid) selectedItem.Tag);
  }

  private void OptionsForm_Load(object sender, EventArgs e)
  {
    if (this._items.Count<IDBTypedObjectID>() == 1 && this._objTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID))
      return;
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    try
    {
      object list;
      if (dictionary.Count == 0 || !dictionary.TryGetValue("guids", out list))
        return;
      this.AddItems((List<Guid>) list);
    }
    catch
    {
    }
  }

  private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, object>()
    {
      {
        "guids",
        (object) this._GuidLst
      }
    });
  }

  private void btnExport_Click(object sender, EventArgs e)
  {
    if (this.Flags == UnloadFlags.None)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_Incorrect_Export_Params"), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
      this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OptionsForm));
    this.groupBox1 = new GroupBox();
    this.cbNameReferences = new CheckBox();
    this.cbCatalogRecProps = new CheckBox();
    this.cbTableDataProps = new CheckBox();
    this.cbTableRefProps = new CheckBox();
    this.cbFolderProps = new CheckBox();
    this.cbCatalogProps = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.listView = new ListView();
    this.naimHeader = new ColumnHeader();
    this._btnsLayoutPnl = new TableLayoutPanel();
    this._btnTopmost = new Button();
    this._imgList = new ImageList(this.components);
    this._btnBottommost = new Button();
    this._btnBottom = new Button();
    this._btnTop = new Button();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.btnReplace = new Button();
    this.btnRemove = new Button();
    this.btnAdd = new Button();
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnExport = new Button();
    this.actList = new ActionList(this.components);
    this._actTop = new Intermech.Actions.Action(this.components);
    this._actBottom = new Intermech.Actions.Action(this.components);
    this._actDown = new Intermech.Actions.Action(this.components);
    this._actUp = new Intermech.Actions.Action(this.components);
    this._actReplace = new Intermech.Actions.Action(this.components);
    this._actRemove = new Intermech.Actions.Action(this.components);
    this._actAdd = new Intermech.Actions.Action(this.components);
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this._btnsLayoutPnl.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.cbNameReferences);
    this.groupBox1.Controls.Add((Control) this.cbCatalogRecProps);
    this.groupBox1.Controls.Add((Control) this.cbTableDataProps);
    this.groupBox1.Controls.Add((Control) this.cbTableRefProps);
    this.groupBox1.Controls.Add((Control) this.cbFolderProps);
    this.groupBox1.Controls.Add((Control) this.cbCatalogProps);
    this.groupBox1.Dock = DockStyle.Top;
    this.groupBox1.Location = new Point(5, 5);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(446, 134);
    this.groupBox1.TabIndex = 34;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Параметры экспорта:";
    this.cbNameReferences.AutoSize = true;
    this.cbNameReferences.Location = new Point(236, 88);
    this.cbNameReferences.Name = "cbNameReferences";
    this.cbNameReferences.Size = new Size(186, 17);
    this.cbNameReferences.TabIndex = 39;
    this.cbNameReferences.Text = "Именовать ссылки на объекты";
    this.cbNameReferences.UseVisualStyleBackColor = true;
    this.cbCatalogRecProps.AutoSize = true;
    this.cbCatalogRecProps.Location = new Point(22, 111);
    this.cbCatalogRecProps.Name = "cbCatalogRecProps";
    this.cbCatalogRecProps.Size = new Size(162, 17);
    this.cbCatalogRecProps.TabIndex = 38;
    this.cbCatalogRecProps.Text = "Свойства записи каталога";
    this.cbCatalogRecProps.UseVisualStyleBackColor = true;
    this.cbTableDataProps.AutoSize = true;
    this.cbTableDataProps.Location = new Point(22, 88);
    this.cbTableDataProps.Name = "cbTableDataProps";
    this.cbTableDataProps.Size = new Size(128 /*0x80*/, 17);
    this.cbTableDataProps.TabIndex = 37;
    this.cbTableDataProps.Text = "Данные из таблицы";
    this.cbTableDataProps.UseVisualStyleBackColor = true;
    this.cbTableRefProps.AutoSize = true;
    this.cbTableRefProps.Location = new Point(22, 65);
    this.cbTableRefProps.Name = "cbTableRefProps";
    this.cbTableRefProps.Size = new Size(115, 17);
    this.cbTableRefProps.TabIndex = 36;
    this.cbTableRefProps.Text = "Свойства ярлыка";
    this.cbTableRefProps.UseVisualStyleBackColor = true;
    this.cbFolderProps.AutoSize = true;
    this.cbFolderProps.Location = new Point(22, 42);
    this.cbFolderProps.Name = "cbFolderProps";
    this.cbFolderProps.Size = new Size(107, 17);
    this.cbFolderProps.TabIndex = 35;
    this.cbFolderProps.Text = "Свойства папки";
    this.cbFolderProps.UseVisualStyleBackColor = true;
    this.cbCatalogProps.AutoSize = true;
    this.cbCatalogProps.Location = new Point(22, 19);
    this.cbCatalogProps.Name = "cbCatalogProps";
    this.cbCatalogProps.Size = new Size(123, 17);
    this.cbCatalogProps.TabIndex = 34;
    this.cbCatalogProps.Text = "Свойства каталога";
    this.cbCatalogProps.UseVisualStyleBackColor = true;
    this.groupBox2.Controls.Add((Control) this.listView);
    this.groupBox2.Controls.Add((Control) this._btnsLayoutPnl);
    this.groupBox2.Controls.Add((Control) this.tableLayoutPanel1);
    this.groupBox2.Dock = DockStyle.Fill;
    this.groupBox2.Location = new Point(5, 139);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Padding = new Padding(10);
    this.groupBox2.Size = new Size(446, 233);
    this.groupBox2.TabIndex = 35;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Перечень атрибутов:";
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.naimHeader
    });
    this.listView.Dock = DockStyle.Fill;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(10, 23);
    this.listView.Name = "listView";
    this.listView.Size = new Size(383, 166);
    this.listView.TabIndex = 32 /*0x20*/;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.naimHeader.Text = "Наименование";
    this.naimHeader.Width = 370;
    this._btnsLayoutPnl.AutoSize = true;
    this._btnsLayoutPnl.ColumnCount = 1;
    this._btnsLayoutPnl.ColumnStyles.Add(new ColumnStyle());
    this._btnsLayoutPnl.Controls.Add((Control) this._btnTopmost, 0, 0);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnBottommost, 0, 3);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnBottom, 0, 2);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnTop, 0, 1);
    this._btnsLayoutPnl.Dock = DockStyle.Right;
    this._btnsLayoutPnl.Location = new Point(393, 23);
    this._btnsLayoutPnl.Name = "_btnsLayoutPnl";
    this._btnsLayoutPnl.Padding = new Padding(5, 0, 0, 0);
    this._btnsLayoutPnl.RowCount = 4;
    this._btnsLayoutPnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this._btnsLayoutPnl.RowStyles.Add(new RowStyle());
    this._btnsLayoutPnl.RowStyles.Add(new RowStyle());
    this._btnsLayoutPnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this._btnsLayoutPnl.Size = new Size(43, 166);
    this._btnsLayoutPnl.TabIndex = 29;
    this.actList.SetAction((Component) this._btnTopmost, this._actTop);
    this._btnTopmost.Dock = DockStyle.Bottom;
    this._btnTopmost.Enabled = false;
    this._btnTopmost.ImageIndex = 0;
    this._btnTopmost.ImageList = this._imgList;
    this._btnTopmost.ImeMode = ImeMode.NoControl;
    this._btnTopmost.Location = new Point(8, 10);
    this._btnTopmost.Name = "_btnTopmost";
    this._btnTopmost.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this._btnTopmost.TabIndex = 23;
    this._btnTopmost.Tag = (object) "0";
    this._btnTopmost.UseVisualStyleBackColor = true;
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "Top.ico");
    this._imgList.Images.SetKeyName(1, "Up.ico");
    this._imgList.Images.SetKeyName(2, "Down.ico");
    this._imgList.Images.SetKeyName(3, "Bottom.ico");
    this.actList.SetAction((Component) this._btnBottommost, this._actBottom);
    this._btnBottommost.Dock = DockStyle.Top;
    this._btnBottommost.Enabled = false;
    this._btnBottommost.ImageIndex = 3;
    this._btnBottommost.ImageList = this._imgList;
    this._btnBottommost.ImeMode = ImeMode.NoControl;
    this._btnBottommost.Location = new Point(8, 124);
    this._btnBottommost.Name = "_btnBottommost";
    this._btnBottommost.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this._btnBottommost.TabIndex = 26;
    this._btnBottommost.Tag = (object) "3";
    this._btnBottommost.UseVisualStyleBackColor = true;
    this.actList.SetAction((Component) this._btnBottom, this._actDown);
    this._btnBottom.Enabled = false;
    this._btnBottom.ImageIndex = 2;
    this._btnBottom.ImageList = this._imgList;
    this._btnBottom.ImeMode = ImeMode.NoControl;
    this._btnBottom.Location = new Point(8, 86);
    this._btnBottom.Name = "_btnBottom";
    this._btnBottom.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this._btnBottom.TabIndex = 25;
    this._btnBottom.Tag = (object) "2";
    this._btnBottom.UseVisualStyleBackColor = true;
    this.actList.SetAction((Component) this._btnTop, this._actUp);
    this._btnTop.Enabled = false;
    this._btnTop.ImageIndex = 1;
    this._btnTop.ImageList = this._imgList;
    this._btnTop.ImeMode = ImeMode.NoControl;
    this._btnTop.Location = new Point(8, 48 /*0x30*/);
    this._btnTop.Name = "_btnTop";
    this._btnTop.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this._btnTop.TabIndex = 24;
    this._btnTop.Tag = (object) "1";
    this._btnTop.UseVisualStyleBackColor = true;
    this.tableLayoutPanel1.AutoSize = true;
    this.tableLayoutPanel1.ColumnCount = 3;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this.btnReplace, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnRemove, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnAdd, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Bottom;
    this.tableLayoutPanel1.Location = new Point(10, 189);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.Padding = new Padding(0, 5, 0, 0);
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(426, 34);
    this.tableLayoutPanel1.TabIndex = 31 /*0x1F*/;
    this.actList.SetAction((Component) this.btnReplace, this._actReplace);
    this.btnReplace.ImageList = this._imgList;
    this.btnReplace.Location = new Point(165, 8);
    this.btnReplace.Name = "btnReplace";
    this.btnReplace.Size = new Size(75, 23);
    this.btnReplace.TabIndex = 4;
    this.btnReplace.Text = "Заменить";
    this.btnReplace.UseVisualStyleBackColor = true;
    this.actList.SetAction((Component) this.btnRemove, this._actRemove);
    this.btnRemove.ImageList = this._imgList;
    this.btnRemove.Location = new Point(84, 8);
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Size = new Size(75, 23);
    this.btnRemove.TabIndex = 3;
    this.btnRemove.Text = "Исключить";
    this.btnRemove.UseVisualStyleBackColor = true;
    this.actList.SetAction((Component) this.btnAdd, this._actAdd);
    this.btnAdd.ImageList = this._imgList;
    this.btnAdd.Location = new Point(3, 8);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(75, 23);
    this.btnAdd.TabIndex = 2;
    this.btnAdd.Text = "Добавить";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnExport);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(5, 372);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(446, 35);
    this.panel1.TabIndex = 36;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(358, 6);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnExport.Location = new Point(277, 6);
    this.btnExport.Name = "btnExport";
    this.btnExport.Size = new Size(75, 23);
    this.btnExport.TabIndex = 0;
    this.btnExport.Text = "Экспорт";
    this.btnExport.UseVisualStyleBackColor = true;
    this.btnExport.Click += new EventHandler(this.btnExport_Click);
    this.actList.Actions.AddRange(new Intermech.Actions.Action[7]
    {
      this._actAdd,
      this._actRemove,
      this._actReplace,
      this._actUp,
      this._actTop,
      this._actDown,
      this._actBottom
    });
    this.actList.ImageList = this._imgList;
    this.actList.ShowTextOnToolBar = false;
    this.actList.Tag = (object) null;
    this._actTop.Hint = (string) null;
    this._actTop.ImageIndex = 0;
    this._actTop.Text = "";
    this._actTop.Execute += new EventHandler(this._actTop_Execute);
    this._actTop.Update += new EventHandler(this._actTop_Update);
    this._actBottom.Hint = (string) null;
    this._actBottom.ImageIndex = 3;
    this._actBottom.Text = "";
    this._actBottom.Execute += new EventHandler(this._actBottom_Execute);
    this._actBottom.Update += new EventHandler(this._actBottom_Update);
    this._actDown.Hint = (string) null;
    this._actDown.ImageIndex = 2;
    this._actDown.Text = "";
    this._actDown.Execute += new EventHandler(this._actDown_Execute);
    this._actDown.Update += new EventHandler(this._actDown_Update);
    this._actUp.Hint = (string) null;
    this._actUp.ImageIndex = 1;
    this._actUp.Text = "";
    this._actUp.Execute += new EventHandler(this._actUp_Execute);
    this._actUp.Update += new EventHandler(this._actUp_Update);
    this._actReplace.Hint = (string) null;
    this._actReplace.Text = "Заменить";
    this._actReplace.Execute += new EventHandler(this._actReplace_Execute);
    this._actReplace.Update += new EventHandler(this._actReplace_Update);
    this._actRemove.Hint = (string) null;
    this._actRemove.Text = "Исключить";
    this._actRemove.Execute += new EventHandler(this._actRemove_Execute);
    this._actRemove.Update += new EventHandler(this._actRemove_Update);
    this._actAdd.Hint = (string) null;
    this._actAdd.Text = "Добавить";
    this._actAdd.Execute += new EventHandler(this._actAdd_Execute);
    this.AcceptButton = (IButtonControl) this.btnExport;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(456, 412);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(400, 450);
    this.Name = nameof (OptionsForm);
    this.Padding = new Padding(5);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Экспорт в MS Excel";
    this.FormClosed += new FormClosedEventHandler(this.OptionsForm_FormClosed);
    this.Load += new EventHandler(this.OptionsForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this._btnsLayoutPnl.ResumeLayout(false);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
