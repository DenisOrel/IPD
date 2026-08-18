// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.CopyRecords
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Clipboard;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class CopyRecords : Form
{
  private TableEditor _editor;
  private TableData _sourceData;
  private List<AttributeInfo> _sourceInfo;
  private List<AttributeInfo> _destInfo;
  private List<AttributeInfoPair> _pairs;
  private Dictionary<long, long> _needUpdateObjects;
  private static ImageList _imageList;
  internal static ICategoryTypeIconService _iconService;
  private IContainer components;
  private Label _lbSourceTable;
  private ListBox _lbSource;
  private ListBox _lbList;
  private ListBox _lbDest;
  private Label _lbDestTable;
  private Button _btnJoin;
  private ToolTip toolTip1;
  private Button _btnUnjoin;
  private Button _btnClear;
  private Button _btnOk;
  private Button _btnCancel;
  private Button _btnAuto;
  private CheckBox _cbSkipSame;
  private Label _lbCaption;
  private SplitContainer _splVContainer;
  private TableLayoutPanel _tableLayoutPanel;
  private Panel _pnlButtons;
  private SplitContainer _splHContainer;

  static CopyRecords()
  {
    if (!(ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service))
      return;
    CopyRecords._iconService = service;
    CopyRecords._imageList = service.ImageList;
  }

  internal static bool CopyTableRecords(
    TableEditor editor,
    TableData tableData,
    Dictionary<long, long> needUpdateObjects)
  {
    CopyRecords copyRecords = new CopyRecords();
    copyRecords.SetData(editor, tableData, needUpdateObjects);
    return copyRecords.ShowDialog() != DialogResult.Cancel;
  }

  public CopyRecords()
  {
    this._destInfo = new List<AttributeInfo>();
    this._sourceInfo = new List<AttributeInfo>();
    this._pairs = new List<AttributeInfoPair>();
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 894);
  }

  private void OnAutoJoin(object sender, EventArgs e) => this.AutoJoin();

  private void OnClear(object sender, EventArgs e) => this.InitializeControls();

  private void OnCopyRecordsClick(object sender, EventArgs e)
  {
    int count1 = this._lbList.Items.Count;
    if (count1 > 0)
    {
      DataTable table = this._sourceData.DataSet.Tables["IMS_DATA"];
      DataTable proxyTable = this._editor._proxyTable;
      Dictionary<DataColumn, DataColumn> dictionary = new Dictionary<DataColumn, DataColumn>(count1);
      for (int index = 0; index < count1; ++index)
      {
        if (this._lbList.Items[index] is AttributeInfoPair attributeInfoPair)
          dictionary.Add(attributeInfoPair._source._dataColumn, attributeInfoPair._dest._dataColumn);
      }
      int count2 = dictionary.Count;
      bool isCut = this._sourceData.IsCut;
      if (count2 > 0)
      {
        DataRowCollection rows = table.Rows;
        int count3 = rows.Count;
        for (int index = 0; index < count3; ++index)
        {
          DataRow row = proxyTable.NewRow();
          DataRow dataRow = rows[index];
          foreach (DataColumn key in dictionary.Keys)
          {
            try
            {
              row[dictionary[key]] = dataRow[key];
            }
            catch (Exception ex)
            {
              string columnName = dictionary[key].ColumnName;
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(Convert.ToInt32(columnName));
                throw new Exception(ex.Message.Replace($" {columnName}.", $" [{attributeType.Name}]."), ex);
              }
            }
          }
          if (!this._cbSkipSame.Checked || !CopyRecords.RowExists(proxyTable, row.ItemArray))
          {
            proxyTable.Rows.Add(row);
            row["-12"] = !isCut ? (object) Guid.NewGuid() : dataRow["F_GUID"];
            if (isCut)
            {
              long int64_1 = Convert.ToInt64(dataRow["F_KEY"]);
              if (this._sourceData.usedKeys.Contains(int64_1))
              {
                long createdObjectId = TableEditor.GetCreatedObjectId(int64_1, this._sourceData.usedKeys, this._sourceData.createdObjects);
                if (createdObjectId != -1L)
                {
                  long int64_2 = Convert.ToInt64(row["-2"]);
                  if (!this._needUpdateObjects.ContainsKey(int64_2))
                    this._needUpdateObjects.Add(int64_2, createdObjectId);
                }
              }
            }
          }
        }
      }
    }
    this._sourceData.IsCut = false;
    this.DialogResult = DialogResult.OK;
  }

  private void OnJoin(object sender, EventArgs e)
  {
    int selectedIndex1 = this._lbSource.SelectedIndex;
    int selectedIndex2 = this._lbDest.SelectedIndex;
    if (selectedIndex1 == -1 || selectedIndex2 == -1)
      return;
    this.JoinItems(selectedIndex1, selectedIndex2);
  }

  private void OnList_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1)
      return;
    ListBox listBox = sender as ListBox;
    AttributeInfoPair attributeInfoPair = listBox.Items[e.Index] as AttributeInfoPair;
    Rectangle rectangle = new Rectangle(e.Bounds.Left, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    int num = this._lbList.Width / 2;
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    CopyRecords._imageList.Draw(e.Graphics, rectangle.Left + num + 4, rectangle.Top, attributeInfoPair._dest.ImageId);
    e.Graphics.DrawString(attributeInfoPair._dest._caption, listBox.Font, brush, (float) (rectangle.Left + 18 + num), (float) (rectangle.Top + 2));
    CopyRecords._imageList.Draw(e.Graphics, rectangle.Left + num - 22, rectangle.Top, attributeInfoPair._source.ImageId);
    SizeF sizeF = e.Graphics.MeasureString(attributeInfoPair._source._caption, e.Font);
    e.Graphics.DrawString(attributeInfoPair._source._caption, listBox.Font, brush, (float) (rectangle.Left + num - 24) - sizeF.Width, (float) (rectangle.Top + 2));
  }

  private void OnListBox_MeasureItem(object sender, MeasureItemEventArgs e)
  {
    ListBox listBox = sender as ListBox;
    SizeF sizeF = e.Graphics.MeasureString("Wg", listBox.Font);
    e.ItemHeight = (int) sizeF.Height + 3;
  }

  private void OnSingleDrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1)
      return;
    ListBox listBox = sender as ListBox;
    AttributeInfo attributeInfo = listBox.Items[e.Index] as AttributeInfo;
    Rectangle rectangle = new Rectangle(e.Bounds.Left, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    if (attributeInfo.ImageId != -1 && CopyRecords._imageList != null)
      CopyRecords._imageList.Draw(e.Graphics, rectangle.X, rectangle.Y, attributeInfo.ImageId);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    e.Graphics.DrawString(attributeInfo._caption, listBox.Font, brush, (float) (rectangle.Left + 18), (float) (rectangle.Top + 2));
  }

  private void OnUnJoin(object sender, EventArgs e)
  {
    int selectedIndex = this._lbList.SelectedIndex;
    if (selectedIndex == -1 || !(this._lbList.Items[selectedIndex] is AttributeInfoPair attributeInfoPair))
      return;
    this._lbSource.Items.Add((object) attributeInfoPair._source);
    this._lbDest.Items.Add((object) attributeInfoPair._dest);
    this._lbList.Items.RemoveAt(selectedIndex);
  }

  internal bool SkipSameRecords
  {
    get => this._cbSkipSame.Checked;
    set => this._cbSkipSame.Checked = value;
  }

  private void AutoJoin()
  {
    int count = this._destInfo.Count;
    for (int index = 0; index < count; ++index)
    {
      AttributeInfo attributeInfo = this._destInfo[index];
      int itemIndex1 = this.FindItemIndex(this._lbDest, attributeInfo.Id);
      if (itemIndex1 != -1)
      {
        int itemIndex2 = this.FindItemIndex(this._lbSource, attributeInfo.Id);
        if (itemIndex2 != -1)
          this.JoinItems(itemIndex2, itemIndex1);
      }
    }
  }

  private int FindItemIndex(ListBox lb, int attId)
  {
    int count = lb.Items.Count;
    for (int index = 0; index < count; ++index)
    {
      if (lb.Items[index] is AttributeInfo attributeInfo && attributeInfo.Id == attId)
        return index;
    }
    return -1;
  }

  private void JoinItems(int srcIndex, int destIndex)
  {
    AttributeInfo source = this._lbSource.Items[srcIndex] as AttributeInfo;
    AttributeInfo dest = this._lbDest.Items[destIndex] as AttributeInfo;
    this._lbSource.Items.RemoveAt(srcIndex);
    this._lbDest.Items.RemoveAt(destIndex);
    this._lbList.Items.Add((object) new AttributeInfoPair(source, dest));
  }

  private void InitializeControls()
  {
    this._lbSource.Items.Clear();
    this._lbSource.Items.AddRange((object[]) this._sourceInfo.ToArray());
    this._lbDest.Items.Clear();
    this._lbDest.Items.AddRange((object[]) this._destInfo.ToArray());
    this._lbList.Items.Clear();
  }

  private static bool RowExists(DataTable table, object[] values)
  {
    DataRowCollection rows = table.Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      if (TableEditor.SameRows(values, rows[index].ItemArray))
        return true;
    }
    return false;
  }

  private void SetData(
    TableEditor editor,
    TableData tableData,
    Dictionary<long, long> needUpdateObjects)
  {
    this._editor = editor;
    this._sourceData = tableData;
    this._needUpdateObjects = needUpdateObjects;
    DataColumnCollection columns = this._editor._proxyTable.Columns;
    int count1 = columns.Count;
    this._lbSourceTable.Text = string.Format(this._lbSourceTable.Text, (object) tableData.ToString());
    this._lbDestTable.Text = string.Format(this._lbDestTable.Text, (object) editor.TableInfo.Caption);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      for (int index = 0; index < count1; ++index)
      {
        int result = 0;
        if (int.TryParse(columns[index].ColumnName, out result) && result > 0 && this._editor.ProxyToDataColumn(columns[index]) != null && (tableData.IsCut || !columns[index].ExtendedProperties.ContainsKey((object) "F_DONTCOPY")))
          this._destInfo.Add(new AttributeInfo(result, columns[index], session));
      }
      DataTable table = this._sourceData.DataSet.Tables["IMS_DATA"];
      DataRowCollection rows = this._sourceData.DataSet.Tables["IMS_ATTR_TYPES"].Rows;
      int count2 = rows.Count;
      for (int index = 0; index < count2; ++index)
      {
        Guid anAttributeGuid = new Guid(Convert.ToString(rows[index]["F_ATTRIBUTE_GUID"]));
        IDBAttributeType attributeType = session.GetAttributeType(anAttributeGuid);
        DataColumn column = table.Columns[anAttributeGuid.ToString()];
        if (column != null)
          this._sourceInfo.Add(new AttributeInfo(attributeType.AttributeID, column, session));
      }
    }
    this.InitializeControls();
    this.AutoJoin();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CopyRecords));
    this._splVContainer = new SplitContainer();
    this._lbSource = new ListBox();
    this._lbSourceTable = new Label();
    this._lbDest = new ListBox();
    this._lbDestTable = new Label();
    this._tableLayoutPanel = new TableLayoutPanel();
    this._btnAuto = new Button();
    this._btnJoin = new Button();
    this._btnUnjoin = new Button();
    this._splHContainer = new SplitContainer();
    this._lbList = new ListBox();
    this.toolTip1 = new ToolTip(this.components);
    this._btnClear = new Button();
    this._cbSkipSame = new CheckBox();
    this._btnOk = new Button();
    this._btnCancel = new Button();
    this._lbCaption = new Label();
    this._pnlButtons = new Panel();
    this._splVContainer.Panel1.SuspendLayout();
    this._splVContainer.Panel2.SuspendLayout();
    this._splVContainer.SuspendLayout();
    this._tableLayoutPanel.SuspendLayout();
    this._splHContainer.Panel1.SuspendLayout();
    this._splHContainer.Panel2.SuspendLayout();
    this._splHContainer.SuspendLayout();
    this._pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splVContainer, "_splVContainer");
    this._splVContainer.Name = "_splVContainer";
    this._splVContainer.Panel1.Controls.Add((Control) this._lbSource);
    this._splVContainer.Panel1.Controls.Add((Control) this._lbSourceTable);
    this._splVContainer.Panel2.Controls.Add((Control) this._lbDest);
    this._splVContainer.Panel2.Controls.Add((Control) this._lbDestTable);
    this._splVContainer.Panel2.Controls.Add((Control) this._tableLayoutPanel);
    componentResourceManager.ApplyResources((object) this._lbSource, "_lbSource");
    this._lbSource.DrawMode = DrawMode.OwnerDrawFixed;
    this._lbSource.FormattingEnabled = true;
    this._lbSource.Name = "_lbSource";
    this._lbSource.DrawItem += new DrawItemEventHandler(this.OnSingleDrawItem);
    this._lbSource.MeasureItem += new MeasureItemEventHandler(this.OnListBox_MeasureItem);
    this._lbSourceTable.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this._lbSourceTable, "_lbSourceTable");
    this._lbSourceTable.Name = "_lbSourceTable";
    componentResourceManager.ApplyResources((object) this._lbDest, "_lbDest");
    this._lbDest.DrawMode = DrawMode.OwnerDrawFixed;
    this._lbDest.FormattingEnabled = true;
    this._lbDest.Name = "_lbDest";
    this._lbDest.DrawItem += new DrawItemEventHandler(this.OnSingleDrawItem);
    this._lbDest.MeasureItem += new MeasureItemEventHandler(this.OnListBox_MeasureItem);
    this._lbDestTable.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this._lbDestTable, "_lbDestTable");
    this._lbDestTable.Name = "_lbDestTable";
    componentResourceManager.ApplyResources((object) this._tableLayoutPanel, "_tableLayoutPanel");
    this._tableLayoutPanel.Controls.Add((Control) this._btnAuto, 0, 1);
    this._tableLayoutPanel.Controls.Add((Control) this._btnJoin, 0, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._btnUnjoin, 0, 3);
    this._tableLayoutPanel.Name = "_tableLayoutPanel";
    componentResourceManager.ApplyResources((object) this._btnAuto, "_btnAuto");
    this._btnAuto.Name = "_btnAuto";
    this.toolTip1.SetToolTip((Control) this._btnAuto, componentResourceManager.GetString("_btnAuto.ToolTip"));
    this._btnAuto.UseVisualStyleBackColor = true;
    this._btnAuto.Click += new EventHandler(this.OnAutoJoin);
    componentResourceManager.ApplyResources((object) this._btnJoin, "_btnJoin");
    this._btnJoin.Name = "_btnJoin";
    this.toolTip1.SetToolTip((Control) this._btnJoin, componentResourceManager.GetString("_btnJoin.ToolTip"));
    this._btnJoin.UseVisualStyleBackColor = true;
    this._btnJoin.Click += new EventHandler(this.OnJoin);
    componentResourceManager.ApplyResources((object) this._btnUnjoin, "_btnUnjoin");
    this._btnUnjoin.Name = "_btnUnjoin";
    this.toolTip1.SetToolTip((Control) this._btnUnjoin, componentResourceManager.GetString("_btnUnjoin.ToolTip"));
    this._btnUnjoin.UseVisualStyleBackColor = true;
    this._btnUnjoin.Click += new EventHandler(this.OnUnJoin);
    componentResourceManager.ApplyResources((object) this._splHContainer, "_splHContainer");
    this._splHContainer.Name = "_splHContainer";
    this._splHContainer.Panel1.Controls.Add((Control) this._splVContainer);
    this._splHContainer.Panel2.Controls.Add((Control) this._lbList);
    componentResourceManager.ApplyResources((object) this._lbList, "_lbList");
    this._lbList.DrawMode = DrawMode.OwnerDrawFixed;
    this._lbList.FormattingEnabled = true;
    this._lbList.Name = "_lbList";
    this._lbList.DrawItem += new DrawItemEventHandler(this.OnList_DrawItem);
    this._lbList.MeasureItem += new MeasureItemEventHandler(this.OnListBox_MeasureItem);
    componentResourceManager.ApplyResources((object) this._btnClear, "_btnClear");
    this._btnClear.Name = "_btnClear";
    this.toolTip1.SetToolTip((Control) this._btnClear, componentResourceManager.GetString("_btnClear.ToolTip"));
    this._btnClear.UseVisualStyleBackColor = true;
    this._btnClear.Click += new EventHandler(this.OnClear);
    componentResourceManager.ApplyResources((object) this._cbSkipSame, "_cbSkipSame");
    this._cbSkipSame.Name = "_cbSkipSame";
    this.toolTip1.SetToolTip((Control) this._cbSkipSame, componentResourceManager.GetString("_cbSkipSame.ToolTip"));
    this._cbSkipSame.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    this._btnOk.Click += new EventHandler(this.OnCopyRecordsClick);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._lbCaption, "_lbCaption");
    this._lbCaption.Name = "_lbCaption";
    this._pnlButtons.Controls.Add((Control) this._cbSkipSame);
    this._pnlButtons.Controls.Add((Control) this._btnOk);
    this._pnlButtons.Controls.Add((Control) this._btnCancel);
    this._pnlButtons.Controls.Add((Control) this._btnClear);
    componentResourceManager.ApplyResources((object) this._pnlButtons, "_pnlButtons");
    this._pnlButtons.Name = "_pnlButtons";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._splHContainer);
    this.Controls.Add((Control) this._pnlButtons);
    this.Controls.Add((Control) this._lbCaption);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CopyRecords);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._splVContainer.Panel1.ResumeLayout(false);
    this._splVContainer.Panel2.ResumeLayout(false);
    this._splVContainer.ResumeLayout(false);
    this._tableLayoutPanel.ResumeLayout(false);
    this._splHContainer.Panel1.ResumeLayout(false);
    this._splHContainer.Panel2.ResumeLayout(false);
    this._splHContainer.ResumeLayout(false);
    this._pnlButtons.ResumeLayout(false);
    this._pnlButtons.PerformLayout();
    this.ResumeLayout(false);
  }
}
