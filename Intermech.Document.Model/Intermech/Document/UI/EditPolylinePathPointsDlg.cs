// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.EditPolylinePathPointsDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Controls.Grid;
using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог добавления строки таблицы по шаблону</summary>
public class EditPolylinePathPointsDlg : Form
{
  private Polyline polyline;
  private Button btnOk;
  private Button btnCancel;
  private bool isDirty;
  private bool blockReentrancy;
  /// <summary>Список строк</summary>
  private ListGrid rowList;
  private Intermech.Bars.ToolBar ToolBar;
  private ButtonItem _biEdit;
  private ButtonItem _biDelete;
  private ButtonItem _biInsertBefore;
  private ButtonItem _biInsertAfter;
  private ButtonItem _biAddToAll;
  private ButtonItem _biEnclose;
  private Panel pnlPointPanel;
  private Label lblYcoord;
  private Label lblXcoord;
  private Label lblPointEditorCaption;
  private NumericUpDown numYcoord;
  private NumericUpDown numXcoord;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Конструктор</summary>
  public EditPolylinePathPointsDlg() => this.InitializeComponent();

  /// <summary>Конструктор с параметром - элементом полилинии</summary>
  public EditPolylinePathPointsDlg(Polyline polyline)
  {
    this.InitializeComponent();
    this.polyline = polyline;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditPolylinePathPointsDlg));
    ListColumn listColumn1 = new ListColumn();
    ListColumn listColumn2 = new ListColumn();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.rowList = new ListGrid();
    this.ToolBar = new Intermech.Bars.ToolBar();
    this._biEdit = new ButtonItem();
    this._biDelete = new ButtonItem();
    this._biInsertBefore = new ButtonItem();
    this._biInsertAfter = new ButtonItem();
    this._biAddToAll = new ButtonItem();
    this._biEnclose = new ButtonItem();
    this.pnlPointPanel = new Panel();
    this.numYcoord = new NumericUpDown();
    this.numXcoord = new NumericUpDown();
    this.lblYcoord = new Label();
    this.lblXcoord = new Label();
    this.lblPointEditorCaption = new Label();
    this.pnlPointPanel.SuspendLayout();
    this.numYcoord.BeginInit();
    this.numXcoord.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.rowList.AllowColumnResize = false;
    this.rowList.AlternateBackground = Color.DarkGreen;
    componentResourceManager.ApplyResources((object) this.rowList, "rowList");
    this.rowList.BackColor = SystemColors.Control;
    this.rowList.BorderWidth = 0;
    listColumn1.Name = "Xcoordinate";
    listColumn1.Text = "X";
    listColumn1.Width = 55;
    listColumn2.Name = "Ycoordinate";
    listColumn2.Text = "Y";
    listColumn2.Width = 55;
    this.rowList.Columns.AddRange(new ListColumn[2]
    {
      listColumn1,
      listColumn2
    });
    this.rowList.GridColor = SystemColors.Control;
    this.rowList.HeaderHeight = 22;
    this.rowList.HeaderStyle = HeaderStyle.Flat;
    this.rowList.HotTrackingColor = Color.LightGray;
    this.rowList.ImageList = (ImageList) null;
    this.rowList.ItemHeight = 17;
    this.rowList.ItemWordWrap = true;
    this.rowList.Name = "rowList";
    this.rowList.SelectedTextColor = Color.White;
    this.rowList.SelectionColor = Color.DarkBlue;
    this.rowList.SuperFlatHeaderColor = Color.White;
    this.rowList.SelectedIndexChanged += new ListGrid.ClickedEventHandler(this.RowList_SelectedIndexChanged);
    this.ToolBar.DockLine = 1;
    this.ToolBar.FullMenus = true;
    this.ToolBar.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    this.ToolBar.Hidden = false;
    this.ToolBar.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this._biEdit,
      (ToolbarItemBase) this._biDelete,
      (ToolbarItemBase) this._biInsertBefore,
      (ToolbarItemBase) this._biInsertAfter,
      (ToolbarItemBase) this._biAddToAll,
      (ToolbarItemBase) this._biEnclose
    });
    componentResourceManager.ApplyResources((object) this.ToolBar, "ToolBar");
    this.ToolBar.Name = "ToolBar";
    this.ToolBar.ButtonClick += new Intermech.Bars.ToolBar.ButtonClickEventHandler(this.ToolBar_ButtonClick);
    componentResourceManager.ApplyResources((object) this._biEdit, "_biEdit");
    this._biEdit.Enabled = false;
    this._biEdit.Icon = (Icon) componentResourceManager.GetObject("_biEdit.Icon");
    componentResourceManager.ApplyResources((object) this._biDelete, "_biDelete");
    this._biDelete.Icon = (Icon) componentResourceManager.GetObject("_biDelete.Icon");
    this._biInsertBefore.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._biInsertBefore, "_biInsertBefore");
    this._biInsertBefore.Icon = (Icon) componentResourceManager.GetObject("_biInsertBefore.Icon");
    componentResourceManager.ApplyResources((object) this._biInsertAfter, "_biInsertAfter");
    this._biInsertAfter.Icon = (Icon) componentResourceManager.GetObject("_biInsertAfter.Icon");
    componentResourceManager.ApplyResources((object) this._biAddToAll, "_biAddToAll");
    this._biAddToAll.Icon = (Icon) componentResourceManager.GetObject("_biAddToAll.Icon");
    this._biEnclose.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._biEnclose, "_biEnclose");
    this._biEnclose.Icon = (Icon) componentResourceManager.GetObject("_biEnclose.Icon");
    this.pnlPointPanel.Controls.Add((Control) this.numYcoord);
    this.pnlPointPanel.Controls.Add((Control) this.numXcoord);
    this.pnlPointPanel.Controls.Add((Control) this.lblYcoord);
    this.pnlPointPanel.Controls.Add((Control) this.lblXcoord);
    this.pnlPointPanel.Controls.Add((Control) this.lblPointEditorCaption);
    this.pnlPointPanel.Controls.Add((Control) this.ToolBar);
    componentResourceManager.ApplyResources((object) this.pnlPointPanel, "pnlPointPanel");
    this.pnlPointPanel.Name = "pnlPointPanel";
    componentResourceManager.ApplyResources((object) this.numYcoord, "numYcoord");
    this.numYcoord.Maximum = new Decimal(new int[4]
    {
      int.MaxValue,
      0,
      0,
      0
    });
    this.numYcoord.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      int.MinValue
    });
    this.numYcoord.Name = "numYcoord";
    this.numYcoord.ValueChanged += new EventHandler(this.numUpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this.numXcoord, "numXcoord");
    this.numXcoord.Maximum = new Decimal(new int[4]
    {
      int.MaxValue,
      0,
      0,
      0
    });
    this.numXcoord.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      int.MinValue
    });
    this.numXcoord.Name = "numXcoord";
    this.numXcoord.ValueChanged += new EventHandler(this.numUpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this.lblYcoord, "lblYcoord");
    this.lblYcoord.Name = "lblYcoord";
    componentResourceManager.ApplyResources((object) this.lblXcoord, "lblXcoord");
    this.lblXcoord.Name = "lblXcoord";
    componentResourceManager.ApplyResources((object) this.lblPointEditorCaption, "lblPointEditorCaption");
    this.lblPointEditorCaption.Name = "lblPointEditorCaption";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.pnlPointPanel);
    this.Controls.Add((Control) this.rowList);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditPolylinePathPointsDlg);
    this.ShowInTaskbar = false;
    this.pnlPointPanel.ResumeLayout(false);
    this.pnlPointPanel.PerformLayout();
    this.numYcoord.EndInit();
    this.numXcoord.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Обновить значения своств контролов</summary>
  protected void UpdateView(int itemIndex = -1)
  {
    if (itemIndex >= 0)
    {
      Intermech.Controls.Grid.ListItem listItem = itemIndex >= this.rowList.Count ? this.SelectedRow : this.rowList.Items[itemIndex];
      this.blockReentrancy = true;
      try
      {
        NumericUpDown numXcoord = this.numXcoord;
        PointF tag;
        Decimal num1;
        if (listItem == null)
        {
          num1 = -1M;
        }
        else
        {
          tag = (PointF) listItem.Tag;
          num1 = Convert.ToDecimal(tag.X);
        }
        numXcoord.Value = num1;
        NumericUpDown numYcoord = this.numYcoord;
        Decimal num2;
        if (listItem == null)
        {
          num2 = -1M;
        }
        else
        {
          tag = (PointF) listItem.Tag;
          num2 = Convert.ToDecimal(tag.Y);
        }
        numYcoord.Value = num2;
      }
      finally
      {
        this.blockReentrancy = false;
      }
    }
    this._biEdit.Enabled = this.isDirty;
    this._biDelete.Enabled = this.SelectedRow != null;
    this._biAddToAll.Enabled = this.rowList.Count > 0;
    this._biEnclose.Enabled = this.rowList.Count > 2 && (PointF) this.rowList.Items[0].Tag != (PointF) this.rowList.Items[this.rowList.Count - 1].Tag;
  }

  /// <summary>Выбранная строка</summary>
  public Intermech.Controls.Grid.ListItem SelectedRow
  {
    [DebuggerStepThrough] get
    {
      return this.rowList.SelectedItems.Count <= 0 ? this.rowList.FocusedItem : this.rowList.SelectedItems[0] as Intermech.Controls.Grid.ListItem;
    }
  }

  /// <summary>Выполнить диалог</summary>
  /// <param name="polyline">Элемент полилиния</param>
  public static PointF[] Execute(Polyline polyline)
  {
    if (polyline == null)
      return (PointF[]) null;
    EditPolylinePathPointsDlg polylinePathPointsDlg = new EditPolylinePathPointsDlg(polyline);
    polylinePathPointsDlg.LoadPoints();
    return polylinePathPointsDlg.ShowDialog() != DialogResult.OK ? (PointF[]) null : polylinePathPointsDlg.GetPoints();
  }

  public void LoadPoints()
  {
    if (this.polyline == null)
      return;
    foreach (PointF pathPoint in this.polyline.PathPoints)
    {
      PageData page = this.polyline.Page;
      PointF pointF = page != null ? page.ConvertInternalToUser(pathPoint) : pathPoint;
      Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem()
      {
        Tag = (object) pointF
      };
      ListSubItemCollection subItems1 = listItem.SubItems;
      float num = pointF.X;
      string strItemText1 = num.ToString();
      subItems1.Add(strItemText1);
      ListSubItemCollection subItems2 = listItem.SubItems;
      num = pointF.Y;
      string strItemText2 = num.ToString();
      subItems2.Add(strItemText2);
      this.rowList.Items.Add(listItem);
    }
    this.SetFocusTo(0);
    this.UpdateView(0);
  }

  private void SetFocusTo(int itemIndex)
  {
    if (this.rowList.Count == 0)
      return;
    if (itemIndex < 0)
      itemIndex = 0;
    if (itemIndex >= this.rowList.Count)
      itemIndex = this.rowList.Count - 1;
    this.rowList.FocusedItem = this.rowList.Items[itemIndex];
    if (!this.rowList.FocusedItem.Selected)
      this.rowList.FocusedItem.Selected = true;
    this.rowList.Update();
  }

  public PointF[] GetPoints()
  {
    return this.rowList.Items.Cast<Intermech.Controls.Grid.ListItem>().Select<Intermech.Controls.Grid.ListItem, PointF>((Func<Intermech.Controls.Grid.ListItem, PointF>) (i =>
    {
      PointF tag = (PointF) i.Tag;
      PageData page = this.polyline.Page;
      return page == null ? tag : page.ConvertUserToInternal(tag);
    })).ToArray<PointF>();
  }

  private void RowList_SelectedIndexChanged(object source, ClickEventArgs e)
  {
    this.isDirty = false;
    this.UpdateView(e.ItemIndex);
  }

  private void ToolBar_ButtonClick(object sender, ToolBarItemEventArgs e)
  {
    switch (e.Item.CommandName)
    {
      case "PathPoint.Edit":
        this.EditSelectedPoint();
        this.isDirty = false;
        this.UpdateView();
        break;
      case "PathPoint.Delete":
        int itemIndex1 = this.rowList.Items.FindItemIndex(this.SelectedRow);
        this.rowList.Items.Remove(this.SelectedRow);
        this.SetFocusTo(itemIndex1);
        this.isDirty = false;
        this.UpdateView();
        break;
      case "PathPoint.InsertBefore":
        int itemIndex2 = this.rowList.Items.FindItemIndex(this.SelectedRow);
        this.InsertNewPointBefore(itemIndex2);
        this.SetFocusTo(itemIndex2);
        this.isDirty = false;
        this.UpdateView();
        break;
      case "PathPoint.InsertAfter":
        int itemIndex3 = this.rowList.Items.FindItemIndex(this.SelectedRow);
        this.InsertNewPointAfter(itemIndex3);
        this.SetFocusTo(itemIndex3);
        this.isDirty = false;
        this.UpdateView();
        break;
      case "PathPoint.AddToAll":
        int itemIndex4 = this.rowList.Items.FindItemIndex(this.SelectedRow);
        this.IncreaseAllPointCoordinates();
        this.SetFocusTo(itemIndex4);
        this.isDirty = false;
        this.UpdateView();
        break;
      case "PathPoint.Enclose":
        this.EnclosePointSequence();
        this.UpdateView();
        break;
    }
  }

  private void EnclosePointSequence()
  {
    Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem()
    {
      Tag = this.rowList.Items[0].Tag
    };
    listItem.SubItems[0].Text = this.rowList.Items[0].SubItems[0].Text;
    listItem.SubItems[1].Text = this.rowList.Items[0].SubItems[1].Text;
    this.rowList.Items.Add(listItem);
  }

  private void IncreaseAllPointCoordinates()
  {
    foreach (Intermech.Controls.Grid.ListItem listItem in (CollectionBase) this.rowList.Items)
    {
      PointF tag = (PointF) listItem.Tag;
      listItem.Tag = (object) new PointF(tag.X += (float) this.numXcoord.Value, tag.Y += (float) this.numYcoord.Value);
      listItem.SubItems[0].Text = ((PointF) listItem.Tag).X.ToString();
      listItem.SubItems[1].Text = ((PointF) listItem.Tag).Y.ToString();
    }
  }

  private void InsertNewPointAfter(int idx)
  {
    Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem()
    {
      Tag = (object) new PointF((float) this.numXcoord.Value, (float) this.numYcoord.Value)
    };
    listItem.SubItems[0].Text = this.numXcoord.Value.ToString();
    listItem.SubItems[1].Text = this.numYcoord.Value.ToString();
    if (idx == this.rowList.Count - 1)
      this.rowList.Items.Add(listItem);
    else
      this.rowList.Items.Insert(idx + 1, listItem);
  }

  private void InsertNewPointBefore(int idx)
  {
    Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem()
    {
      Tag = (object) new PointF((float) this.numXcoord.Value, (float) this.numYcoord.Value)
    };
    listItem.SubItems[0].Text = this.numXcoord.Value.ToString();
    listItem.SubItems[1].Text = this.numYcoord.Value.ToString();
    if (idx == -1)
      this.rowList.Items.Add(listItem);
    else
      this.rowList.Items.Insert(idx, listItem);
  }

  private void EditSelectedPoint()
  {
    this.SelectedRow.Tag = (object) new PointF((float) this.numXcoord.Value, (float) this.numYcoord.Value);
    this.SelectedRow.SubItems[0].Text = this.numXcoord.Value.ToString();
    this.SelectedRow.SubItems[1].Text = this.numYcoord.Value.ToString();
  }

  private void numUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (this.blockReentrancy)
      return;
    this.isDirty = true;
    this.UpdateView();
  }
}
