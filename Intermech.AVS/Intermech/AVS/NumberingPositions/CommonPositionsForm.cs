// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NumberingPositions.CommonPositionsForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.AVS.Properties;
using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.AVS.NumberingPositions;

public class CommonPositionsForm : Form
{
  private AVSDocument avsDocument;
  private AVSRow selRow;
  private AVSRow parentRow;
  private List<AVSRow> selectedRows;
  private TreeListNode parentChapterNode;
  private TreeListNode selectedChapterNode;
  private int colCount = 4;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeList treeAll;
  private TreeListColumn colPosition;
  private TreeListColumn colName;
  private TreeListColumn colDesc;
  private ImageList imageList;
  private TreeList treeSelected;
  private TreeListColumn colSelectedPos;
  private TreeListColumn colSelectedName;
  private TreeListColumn colSelectedDescription;
  private System.Windows.Forms.Button btnAdd;
  private System.Windows.Forms.Button btnRemove;
  private System.Windows.Forms.Button btnClear;
  private System.Windows.Forms.Button _BtnCancel;
  private System.Windows.Forms.Button _BtnOK;
  private TreeListColumn colComment;
  private TreeListColumn colSelectedComment;
  private Label label1;
  private System.Windows.Forms.TextBox tbCommonPosition;

  public AVSRow ParentRow => this.parentRow;

  public List<AVSRow> SelectedRows => this.selectedRows;

  public string CommonPosition
  {
    get => this.tbCommonPosition.Text;
    set => this.tbCommonPosition.Text = value;
  }

  public CommonPositionsForm(
    AVSDocument avsDocument,
    string commonPosition,
    AVSRow row,
    AVSRow parentRow)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2896);
    this.Size = new Size(900, 500);
    Rectangle bounds = Screen.PrimaryScreen.Bounds;
    FormStorage.LoadLayout((Control) this);
    if (bounds.Width > 1200 && this.Size.Width < 1200)
      this.Size = new Size(1200, this.Size.Height);
    if (bounds.Width > 1400 && this.Size.Width < 1400)
      this.Size = new Size(1400, this.Size.Height);
    if (bounds.Width > 1600 && this.Size.Width < 1600)
      this.Size = new Size(1600, this.Size.Height);
    this.CommonPosition = commonPosition;
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.colPosition.Options |= ColumnOptions.FixedWidth;
    this.colPosition.Width = 90;
    this.colPosition.Options &= ~ColumnOptions.CanSorted;
    this.colName.Options &= ~ColumnOptions.CanSorted;
    this.colDesc.Options &= ~ColumnOptions.CanSorted;
    this.colComment.Options &= ~ColumnOptions.CanSorted;
    this.colSelectedPos.Options |= ColumnOptions.FixedWidth;
    this.colSelectedPos.Width = 90;
    this.colSelectedPos.Options &= ~ColumnOptions.CanSorted;
    this.colSelectedName.Options &= ~ColumnOptions.CanSorted;
    this.colSelectedDescription.Options &= ~ColumnOptions.CanSorted;
    this.colSelectedComment.Options &= ~ColumnOptions.CanSorted;
    this.avsDocument = avsDocument;
    this.selectedRows = new List<AVSRow>();
    List<AVSRow> commonPositionRows = row.GetCommonPositionRows();
    if (commonPositionRows != null)
      this.selectedRows.AddRange((IEnumerable<AVSRow>) commonPositionRows);
    else
      this.selectedRows.Add(row);
    this.selRow = row;
    this.parentRow = parentRow;
    object[] nodeData1 = new object[this.colCount];
    if (parentRow != null)
    {
      this.parentChapterNode = this.treeSelected.AppendNode((object) nodeData1, (TreeListNode) null);
      this.parentChapterNode.Tag = nodeData1[0];
      this.CreateRowNode(parentRow, this.treeSelected, this.parentChapterNode);
    }
    object[] nodeData2 = new object[this.colCount];
    nodeData2[0] = (object) "Элементы с условными позициями";
    this.selectedChapterNode = this.treeSelected.AppendNode((object) nodeData2, (TreeListNode) null);
    this.selectedChapterNode.Tag = nodeData2[0];
    this.UpdateTree(avsDocument.commonDataChapter, (TreeListNode) null);
    this.UpdateTree((Chapter) avsDocument.variableDataChapter_FormA, (TreeListNode) null);
    this.UpdateTree((Chapter) avsDocument.variableDataChapter_FormV, (TreeListNode) null);
    this.treeAll.FullExpand();
    this.treeSelected.FullExpand();
    Image image1 = (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    CheckBoxRenderer.DrawCheckBox(Graphics.FromImage(image1), new Point(0, 0), CheckBoxState.UncheckedNormal);
    this.imageList.Images.Add(image1);
    Image image2 = (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    CheckBoxRenderer.DrawCheckBox(Graphics.FromImage(image2), new Point(0, 0), CheckBoxState.CheckedNormal);
    this.imageList.Images.Add(image2);
  }

  private void UpdateTree(Chapter chapter, TreeListNode parentNode)
  {
    if (chapter == null)
      return;
    object[] nodeData = new object[this.colCount];
    nodeData[0] = (object) chapter.Caption;
    TreeListNode parentNode1 = this.treeAll.AppendNode((object) nodeData, parentNode);
    parentNode1.Tag = (object) chapter;
    parentNode1.CheckState = CheckState.Indeterminate;
    foreach (Chapter chapter1 in chapter.Chapters)
      this.UpdateTree(chapter1, parentNode1);
    if (!(chapter is SpecificationSection))
      return;
    foreach (AVSRow row in (chapter as SpecificationSection).Rows)
    {
      this.CreateRowNode(row, this.treeAll, parentNode1);
      if (this.SelectedRows.Contains(row))
      {
        this.CreateRowNode(row, this.treeSelected, this.selectedChapterNode);
        if (!this.SelectedRows.Contains(row))
          this.SelectedRows.Add(row);
      }
    }
  }

  private void CreateRowNode(AVSRow row, TreeList list, TreeListNode parentNode)
  {
    object[] nodeData = new object[this.colCount];
    nodeData[0] = row.GetFieldValue(row.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, true, false);
    nodeData[1] = row.GetFieldValue(row.Field_Name, 0, -1, (List<RelationAttributeValuesCache>) null, true, false);
    nodeData[2] = row.GetFieldValue(row.Field_Designation, 0, -1, (List<RelationAttributeValuesCache>) null, true, false);
    nodeData[3] = row.GetFieldValue(row.Field_Note, 0, -1, (List<RelationAttributeValuesCache>) null, true, false);
    list.AppendNode((object) nodeData, parentNode).Tag = (object) row;
    if (parentNode == null)
      return;
    parentNode.Expanded = true;
  }

  private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
    if (e == null || e.Node == null || e.Node.Tag == null || !(e.Node.Tag is Chapter))
      return;
    e.Handled = true;
    TreeListHitInfo hitInfo = this.treeAll.GetHitInfo(new Point(2, 2));
    if (hitInfo == null || hitInfo.HitInfoType != HitInfoType.Column || hitInfo.Column == null)
      return;
    TreeListColumn column = hitInfo.Column;
    if (column != e.Column)
      return;
    int num1 = 0;
    int num2 = 0;
    for (int visibleIndex = 0; visibleIndex < this.treeAll.Columns.Count; ++visibleIndex)
    {
      TreeListColumn columnByVisibleIndex = this.treeAll.GetColumnByVisibleIndex(visibleIndex);
      if (columnByVisibleIndex == null)
        return;
      if (columnByVisibleIndex.VisibleIndex < column.VisibleIndex)
        num1 += columnByVisibleIndex.VisibleWidth;
      num2 += columnByVisibleIndex.VisibleWidth;
    }
    int x = num1 - this.treeAll.Left;
    int val1 = num2 - this.treeAll.Left;
    if (e.Bounds.Left > x)
      x = e.Bounds.Left;
    if (x < 0)
      x = 0;
    Brush brush1;
    Brush brush2;
    if (e.Node != (sender as TreeList).FocusedNode)
    {
      brush1 = (Brush) new SolidBrush(SystemColors.Control);
      brush2 = (Brush) new SolidBrush(Color.Black);
    }
    else
    {
      brush1 = (Brush) new SolidBrush(Color.DarkGray);
      brush2 = (Brush) new SolidBrush(Color.White);
    }
    Rectangle rectangle = new Rectangle(x, e.Bounds.Top, Math.Min(val1, this.treeAll.ClientRectangle.Width) - 20, e.Bounds.Bottom);
    e.Graphics.FillRectangle(brush1, rectangle);
    StringFormat format = new StringFormat();
    format.Alignment = StringAlignment.Center;
    format.LineAlignment = StringAlignment.Near;
    string caption = ((Chapter) e.Node.Tag).Caption;
    e.Graphics.DrawString(caption, e.Style.Font, brush2, (RectangleF) rectangle, format);
    brush1.Dispose();
    brush2.Dispose();
  }

  private void treeList1_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
    TreeList treeList = e.Node.TreeList;
    if (!AVSDocument.IsSectionTreeListNode(e.Node))
      return;
    e.Style = treeList.Styles["SectionHeader"];
  }

  private void treeList1_CalcNodeHeight(object sender, CalcNodeHeightEventArgs e)
  {
    if (e == null || e.Node == null || e.Node.Tag == null || !(e.Node.Tag is Chapter))
      return;
    TreeListHitInfo hitInfo = this.treeAll.GetHitInfo(new Point(2, 2));
    if (hitInfo == null || hitInfo.HitInfoType != HitInfoType.Column || hitInfo.Column == null)
      return;
    TreeListColumn column = hitInfo.Column;
    int num1 = 0;
    int num2 = 0;
    for (int visibleIndex = 0; visibleIndex < this.treeAll.Columns.Count; ++visibleIndex)
    {
      TreeListColumn columnByVisibleIndex = this.treeAll.GetColumnByVisibleIndex(visibleIndex);
      if (columnByVisibleIndex == null)
        return;
      if (columnByVisibleIndex.VisibleIndex < column.VisibleIndex)
        num1 += columnByVisibleIndex.VisibleWidth;
      num2 += columnByVisibleIndex.VisibleWidth;
    }
    int num3 = num1 - this.treeAll.Left;
    int val1 = num2 - this.treeAll.Left;
    if (num3 < 0)
      num3 = 0;
    string caption = ((Chapter) e.Node.Tag).Caption;
    SizeF sizeF;
    using (Graphics graphics = this.treeAll.CreateGraphics())
      sizeF = graphics.MeasureString(caption, this.treeAll.Font, Math.Min(val1, this.treeAll.ClientRectangle.Width) - 20 - num3);
    e.NodeHeight = Convert.ToInt32(sizeF.Height) + 5;
  }

  private void AddRow(AVSRow row)
  {
    if (row == null || this.SelectedRows.Contains(row))
      return;
    List<long> productIds1 = row.ProductIDs;
    foreach (AVSRow selectedRow in this.SelectedRows)
    {
      List<long> productIds2 = selectedRow.ProductIDs;
      if (row.ProductID != -1L && selectedRow.ProductID == row.ProductID)
      {
        if (MessageBox.Show($"Среди выбранных записей запись '{selectedRow.ObjCaption}' имеет тоже исполнение, что и запись которую вы добавляете. Добавить запись?", "Добавление записи", MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        break;
      }
      if (productIds2.Intersect<long>((IEnumerable<long>) productIds1).Any<long>())
      {
        if (MessageBox.Show($"Среди выбранных записей запись '{selectedRow.ObjCaption}' имеет тоже исполнение, что и запись которую вы добавляете. Добавить запись?", "Добавление записи", MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        break;
      }
    }
    this.CreateRowNode(row, this.treeSelected, this.selectedChapterNode);
    if (this.SelectedRows.Contains(row))
      return;
    this.SelectedRows.Add(row);
  }

  private void RemoveRow(AVSRow row)
  {
    if (row == null)
      return;
    this.treeSelected.DeleteNode(this.treeSelected.Selection[0]);
    if (row != this.parentRow)
      this.SelectedRows.Remove(row);
    else
      this.parentRow = (AVSRow) null;
  }

  private void treeList1_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (!(e.Node.Tag is Chapter))
      return;
    e.NewValue = CheckState.Indeterminate;
  }

  private void treeAll_DoubleClick(object sender, EventArgs e)
  {
    if (this.treeAll.Selection.Count <= 0 || !(this.treeAll.Selection[0].Tag is AVSRow))
      return;
    this.AddRow(this.treeAll.Selection[0].Tag as AVSRow);
  }

  private void treeSelected_DoubleClick(object sender, EventArgs e)
  {
    if (this.treeSelected.Selection.Count <= 0 || !(this.treeSelected.Selection[0].Tag is AVSRow))
      return;
    this.RemoveRow(this.treeSelected.Selection[0].Tag as AVSRow);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.treeAll.Selection.Count <= 0 || !(this.treeAll.Selection[0].Tag is AVSRow))
      return;
    this.AddRow(this.treeAll.Selection[0].Tag as AVSRow);
  }

  private void button2_Click(object sender, EventArgs e)
  {
    if (this.treeSelected.Selection.Count <= 0)
      return;
    this.RemoveRow(this.treeSelected.Selection[0].Tag as AVSRow);
  }

  private void button3_Click(object sender, EventArgs e)
  {
    this.selectedChapterNode.Nodes.Clear();
    this.SelectedRows.Clear();
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    FormStorage.SaveLayout((Control) this);
  }

  protected override void OnResize(EventArgs e) => base.OnResize(e);

  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    this.SuspendLayout();
    int num1 = 5;
    int num2 = 60;
    Size size = this.Size;
    int width1 = size.Width;
    size = this.btnAdd.Size;
    int width2 = size.Width;
    int width3 = (width1 - width2 - num1 * 2) / 2;
    int height = this.ClientSize.Height - num2;
    this.treeAll.Size = new Size(width3, height);
    this.btnAdd.Location = new Point(width3 + num1, this.btnAdd.Location.Y);
    this.btnClear.Location = new Point(width3 + num1, this.btnClear.Location.Y);
    this.btnRemove.Location = new Point(width3 + num1, this.btnRemove.Location.Y);
    this.label1.Top = this.treeAll.Bottom + 10;
    this.tbCommonPosition.Top = this.label1.Top - 3;
    this.treeSelected.Size = new Size(width3, height);
    this.treeSelected.Location = new Point(this.Size.Width - width3, this.treeSelected.Location.Y);
    this.ResumeLayout(true);
  }

  private void treeSelected_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
    if (e == null || e.Node == null || !(e.Node.Tag is string))
      return;
    e.Handled = true;
    TreeListHitInfo hitInfo = this.treeSelected.GetHitInfo(new Point(2, 2));
    if ((object) hitInfo == null || hitInfo.HitInfoType != HitInfoType.Column || hitInfo.Column == null)
      return;
    TreeListColumn column = hitInfo.Column;
    if (column != e.Column)
      return;
    int num1 = 0;
    int num2 = 0;
    for (int visibleIndex = 0; visibleIndex < this.treeSelected.Columns.Count; ++visibleIndex)
    {
      TreeListColumn columnByVisibleIndex = this.treeSelected.GetColumnByVisibleIndex(visibleIndex);
      if (columnByVisibleIndex == null)
        return;
      if (columnByVisibleIndex.VisibleIndex < column.VisibleIndex)
        num1 += columnByVisibleIndex.VisibleWidth;
      num2 += columnByVisibleIndex.VisibleWidth;
    }
    if (e.Bounds.Left > num1)
      num1 = e.Bounds.Left;
    if (num1 < 0)
      num1 = 0;
    Brush brush1;
    Brush brush2;
    if (e.Node != (sender as TreeList).FocusedNode)
    {
      brush1 = (Brush) new SolidBrush(SystemColors.Control);
      brush2 = (Brush) new SolidBrush(Color.Black);
    }
    else
    {
      brush1 = (Brush) new SolidBrush(Color.DarkGray);
      brush2 = (Brush) new SolidBrush(Color.White);
    }
    Rectangle rectangle1;
    ref Rectangle local = ref rectangle1;
    int x = num1;
    int top = e.Bounds.Top;
    int val1 = num2;
    Rectangle rectangle2 = this.treeSelected.ClientRectangle;
    int width1 = rectangle2.Width;
    int width2 = Math.Min(val1, width1) - 20;
    rectangle2 = e.Bounds;
    int bottom = rectangle2.Bottom;
    local = new Rectangle(x, top, width2, bottom);
    e.Graphics.FillRectangle(brush1, rectangle1);
    StringFormat format = new StringFormat();
    format.Alignment = StringAlignment.Center;
    format.LineAlignment = StringAlignment.Near;
    string tag = e.Node.Tag as string;
    e.Graphics.DrawString(tag, e.Style.Font, brush2, (RectangleF) rectangle1, format);
    brush1.Dispose();
    brush2.Dispose();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.treeAll = new TreeList();
    this.colPosition = new TreeListColumn();
    this.colName = new TreeListColumn();
    this.colDesc = new TreeListColumn();
    this.colComment = new TreeListColumn();
    this.imageList = new ImageList(this.components);
    this.treeSelected = new TreeList();
    this.colSelectedPos = new TreeListColumn();
    this.colSelectedName = new TreeListColumn();
    this.colSelectedDescription = new TreeListColumn();
    this.colSelectedComment = new TreeListColumn();
    this.btnAdd = new System.Windows.Forms.Button();
    this.btnRemove = new System.Windows.Forms.Button();
    this.btnClear = new System.Windows.Forms.Button();
    this._BtnCancel = new System.Windows.Forms.Button();
    this._BtnOK = new System.Windows.Forms.Button();
    this.label1 = new Label();
    this.tbCommonPosition = new System.Windows.Forms.TextBox();
    this.treeAll.BeginInit();
    this.treeSelected.BeginInit();
    this.SuspendLayout();
    this.treeAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this.treeAll.BehaviorOptions = BehaviorOptionsFlags.MoveOnEdit | BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.treeAll.CheckBoxes = CheckBoxesStyle.TwoState;
    this.treeAll.Columns.AddRange(new TreeListColumn[4]
    {
      this.colPosition,
      this.colName,
      this.colDesc,
      this.colComment
    });
    this.treeAll.Location = new Point(0, 1);
    this.treeAll.MenuOptions = MenuOptionsFlags.None;
    this.treeAll.Name = "treeAll";
    this.treeAll.Size = new Size(322, 374);
    this.treeAll.Styles.AddReplace("SectionHeader", (object) new ViewStyle("SectionHeader", "", new Font("Tahoma", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, Color.Gainsboro, SystemColors.WindowText));
    this.treeAll.TabIndex = 0;
    this.treeAll.Text = "treeList1";
    this.treeAll.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.treeAll.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.treeList1_GetCustomNodeCellStyle);
    this.treeAll.CheckStateChanging += new CheckStateChangingEventHandler(this.treeList1_CheckStateChanging);
    this.treeAll.CalcNodeHeight += new CalcNodeHeightEventHandler(this.treeList1_CalcNodeHeight);
    this.treeAll.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.treeList1_CustomDrawNodeCell);
    this.treeAll.DoubleClick += new EventHandler(this.treeAll_DoubleClick);
    this.colPosition.Caption = "Позиция";
    this.colPosition.FieldName = "treeListColumn1";
    this.colPosition.Name = "colPosition";
    this.colPosition.VisibleIndex = 0;
    this.colPosition.Width = 100;
    this.colName.Caption = "Наименование";
    this.colName.FieldName = "treeListColumn2";
    this.colName.Name = "colName";
    this.colName.VisibleIndex = 2;
    this.colName.Width = 100;
    this.colDesc.Caption = "Обозначение";
    this.colDesc.FieldName = "treeListColumn3";
    this.colDesc.Name = "colDesc";
    this.colDesc.VisibleIndex = 1;
    this.colComment.Caption = "Примечание";
    this.colComment.FieldName = "treeListColumn1";
    this.colComment.Name = "colComment";
    this.colComment.VisibleIndex = 3;
    this.imageList.ColorDepth = ColorDepth.Depth32Bit;
    this.imageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.imageList.TransparentColor = Color.Transparent;
    this.treeSelected.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeSelected.BehaviorOptions = BehaviorOptionsFlags.MoveOnEdit | BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.treeSelected.CheckBoxes = CheckBoxesStyle.TwoState;
    this.treeSelected.Columns.AddRange(new TreeListColumn[4]
    {
      this.colSelectedPos,
      this.colSelectedName,
      this.colSelectedDescription,
      this.colSelectedComment
    });
    this.treeSelected.Location = new Point(358, 1);
    this.treeSelected.MenuOptions = MenuOptionsFlags.None;
    this.treeSelected.Name = "treeSelected";
    this.treeSelected.Size = new Size(291, 374);
    this.treeSelected.Styles.AddReplace("SectionHeader", (object) new ViewStyle("SectionHeader", "", new Font("Tahoma", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, Color.Gainsboro, SystemColors.WindowText));
    this.treeSelected.TabIndex = 1;
    this.treeSelected.Text = "treeList1";
    this.treeSelected.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.treeSelected.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.treeSelected_CustomDrawNodeCell);
    this.treeSelected.DoubleClick += new EventHandler(this.treeSelected_DoubleClick);
    this.colSelectedPos.Caption = "Позиция";
    this.colSelectedPos.FieldName = "treeListColumn1";
    this.colSelectedPos.Name = "colSelectedPos";
    this.colSelectedPos.VisibleIndex = 0;
    this.colSelectedPos.Width = 100;
    this.colSelectedName.Caption = "Наименование";
    this.colSelectedName.FieldName = "treeListColumn2";
    this.colSelectedName.Name = "colSelectedName";
    this.colSelectedName.VisibleIndex = 2;
    this.colSelectedName.Width = 100;
    this.colSelectedDescription.Caption = "Обозначение";
    this.colSelectedDescription.FieldName = "treeListColumn3";
    this.colSelectedDescription.Name = "colSelectedDescription";
    this.colSelectedDescription.VisibleIndex = 1;
    this.colSelectedComment.Caption = "Примечание";
    this.colSelectedComment.FieldName = "treeListColumn1";
    this.colSelectedComment.Name = "colSelectedComment";
    this.colSelectedComment.VisibleIndex = 3;
    this.btnAdd.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.btnAdd.Image = (Image) Resources.arrow_right_blueStandart;
    this.btnAdd.Location = new Point(328, 47);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(23, 23);
    this.btnAdd.TabIndex = 2;
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.button1_Click);
    this.btnRemove.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.btnRemove.Image = (Image) Resources.arrow_left_blueStandart;
    this.btnRemove.Location = new Point(328, 86);
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Size = new Size(23, 23);
    this.btnRemove.TabIndex = 3;
    this.btnRemove.UseVisualStyleBackColor = true;
    this.btnRemove.Click += new EventHandler(this.button2_Click);
    this.btnClear.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.btnClear.Image = (Image) Resources.arrow_allStandart;
    this.btnClear.Location = new Point(328, 126);
    this.btnClear.Name = "btnClear";
    this.btnClear.Size = new Size(23, 23);
    this.btnClear.TabIndex = 4;
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.button3_Click);
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(527, 408);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 6;
    this._BtnCancel.Text = "Отмена";
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(400, 408);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 5;
    this._BtnOK.Text = "ОК";
    this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 384);
    this.label1.Name = "label1";
    this.label1.Size = new Size(102, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Условная позиция";
    this.tbCommonPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.tbCommonPosition.Location = new Point(120, 381);
    this.tbCommonPosition.Name = "tbCommonPosition";
    this.tbCommonPosition.Size = new Size(108, 20);
    this.tbCommonPosition.TabIndex = 8;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(663, 447);
    this.Controls.Add((Control) this.tbCommonPosition);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this.btnClear);
    this.Controls.Add((Control) this.btnRemove);
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.treeSelected);
    this.Controls.Add((Control) this.treeAll);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(671, 485);
    this.Name = nameof (CommonPositionsForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор совместных позиций";
    this.treeAll.EndInit();
    this.treeSelected.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
