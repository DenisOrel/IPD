// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ProcChanges
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ProcChanges : Form
{
  internal List<ProcChanges.ChangeInfo> changeList;
  internal List<PendingLink> pendList;
  internal List<ProcChanges.ChangeView> viewList;
  public bool somethingChanged;
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private Button btnDown;
  private ImageList IL;
  private Button btnUp;
  private Button btnSortAll;
  private ToolTip toolTip1;
  private Button btnJoin;
  private Button btnSort;
  private DataGridView dgv;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn colDesign;
  private DataGridViewTextBoxColumn colGoal;

  public ProcChanges() => this.InitializeComponent();

  public List<ProcChanges.ChangeInfo> ChangeList => this.changeList;

  public bool Execute(List<ProcChanges.ChangeInfo> changes, List<PendingLink> pList)
  {
    this.changeList = changes;
    this.pendList = pList;
    this.InitGrid();
    return this.ShowDialog() == DialogResult.OK;
  }

  internal void InitGrid()
  {
    this.viewList = new List<ProcChanges.ChangeView>();
    foreach (ProcChanges.ChangeInfo change in this.changeList)
    {
      ProcChanges.ChangeView changeView = new ProcChanges.ChangeView(change, this.pendList);
      this.viewList.Add(changeView);
      this.dgv.Rows.Add((object) changeView.DesList, (object) changeView.Goal);
    }
  }

  internal void UpdateButtons()
  {
    if (this.dgv.SelectedRows.Count != 1)
      return;
    int index = this.dgv.SelectedRows[0].Index;
    this.btnUp.Enabled = index != 0;
    this.btnDown.Enabled = index < this.dgv.Rows.Count - 1;
    ProcChanges.ChangeInfo change = this.changeList[index];
    this.btnSort.Enabled = !change.Sorted;
    if (index < this.dgv.Rows.Count - 1)
      this.btnJoin.Enabled = this.changeList[index + 1].Goal == change.Goal;
    else
      this.btnJoin.Enabled = false;
  }

  private void dgv_SelectionChanged(object sender, EventArgs e) => this.UpdateButtons();

  private void btnUp_Click(object sender, EventArgs e)
  {
    int index = this.dgv.SelectedRows[0].Index;
    DataGridViewRow row = this.dgv.Rows[index - 1];
    this.dgv.Rows.Remove(row);
    row.Frozen = false;
    this.dgv.Rows.Insert(index, row);
    this.dgv.ClearSelection();
    this.dgv.Rows[index - 1].Selected = true;
    this.somethingChanged = true;
    ProcChanges.ChangeInfo change = this.changeList[index];
    this.changeList.RemoveAt(index);
    this.changeList.Insert(index - 1, change);
    ProcChanges.ChangeView view = this.viewList[index];
    this.viewList.RemoveAt(index);
    this.viewList.Insert(index - 1, view);
  }

  private void btnDown_Click(object sender, EventArgs e)
  {
    int index = this.dgv.SelectedRows[0].Index;
    DataGridViewRow row = this.dgv.Rows[index + 1];
    this.dgv.Rows.Remove(row);
    row.Frozen = false;
    this.dgv.Rows.Insert(index, row);
    this.dgv.ClearSelection();
    this.dgv.Rows[index + 1].Selected = true;
    this.somethingChanged = true;
    ProcChanges.ChangeInfo change = this.changeList[index];
    this.changeList.RemoveAt(index);
    this.changeList.Insert(index + 1, change);
    ProcChanges.ChangeView view = this.viewList[index];
    this.viewList.RemoveAt(index);
    this.viewList.Insert(index + 1, view);
  }

  private void btnJoin_Click(object sender, EventArgs e)
  {
    int index1 = this.dgv.SelectedRows[0].Index;
    ProcChanges.ChangeInfo change1 = this.changeList[index1];
    ProcChanges.ChangeInfo change2 = this.changeList[index1 + 1];
    if (change1.MergedList == null)
      change1.MergedList = new List<TableElement>();
    change1.MergedList.Add(change2.Change);
    if (change2.MergedList != null)
    {
      foreach (TableElement merged in change2.MergedList)
        change1.MergedList.Add(merged);
    }
    for (int index2 = 0; index2 < change2.ObjIDs.Count; ++index2)
      change1.ObjIDs.Add(change2.ObjIDs[index2]);
    change2.State = ProcChanges.ChangeState.Deleted;
    change1.State = ProcChanges.ChangeState.Merged;
    this.changeList.RemoveAt(index1 + 1);
    ProcChanges.ChangeView view1 = this.viewList[index1];
    ProcChanges.ChangeView view2 = this.viewList[index1 + 1];
    view1.DesList = $"{view1.DesList}, {view2.DesList}";
    foreach (string des in view2.DesArray)
      view1.DesArray.Add(des);
    this.viewList.RemoveAt(index1 + 1);
    this.dgv.Rows[index1].Cells[0].Value = (object) view1.DesList;
    this.dgv.Rows.RemoveAt(index1 + 1);
    this.somethingChanged = true;
    this.UpdateButtons();
  }

  private void SortChange(int Index)
  {
    ProcChanges.ChangeInfo change = this.changeList[Index];
    ProcChanges.ChangeView view = this.viewList[Index];
    List<ProcChanges.DesAndId> desAndIdList = new List<ProcChanges.DesAndId>();
    for (int index = 0; index < change.ObjIDs.Count; ++index)
      desAndIdList.Add(new ProcChanges.DesAndId(view.DesArray[index], change.ObjIDs[index]));
    desAndIdList.Sort();
    StringBuilder stringBuilder = new StringBuilder();
    change.ObjIDs.Clear();
    view.DesArray.Clear();
    for (int index = 0; index < desAndIdList.Count; ++index)
    {
      ProcChanges.DesAndId desAndId = desAndIdList[index];
      change.ObjIDs.Add(desAndId.Id);
      view.DesArray.Add(desAndId.Design);
      if (stringBuilder.Length > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(desAndId.Design);
    }
    view.DesList = stringBuilder.ToString();
    change.State = ProcChanges.ChangeState.Sorted;
  }

  private void btnSort_Click(object sender, EventArgs e)
  {
    int index = this.dgv.SelectedRows[0].Index;
    this.SortChange(index);
    this.somethingChanged = true;
    this.dgv.Rows[index].Cells[0].Value = (object) this.viewList[index].DesList;
    this.UpdateButtons();
  }

  private void btnSortAll_Click(object sender, EventArgs e)
  {
    for (int Index = 0; Index < this.changeList.Count; ++Index)
      this.SortChange(Index);
    List<ProcChanges.DesAndId> desAndIdList = new List<ProcChanges.DesAndId>();
    for (int index = 0; index < this.changeList.Count; ++index)
    {
      ProcChanges.ChangeInfo change = this.changeList[index];
      ProcChanges.ChangeView view = this.viewList[index];
      string des = view.DesArray.Count > 0 ? view.DesArray[0] : "";
      desAndIdList.Add(new ProcChanges.DesAndId(des, (long) index));
    }
    desAndIdList.Sort();
    List<ProcChanges.ChangeInfo> changeInfoList = new List<ProcChanges.ChangeInfo>();
    List<ProcChanges.ChangeView> changeViewList = new List<ProcChanges.ChangeView>();
    for (int index = 0; index < desAndIdList.Count; ++index)
    {
      int id = (int) desAndIdList[index].Id;
      changeInfoList.Add(this.changeList[id]);
      changeViewList.Add(this.viewList[id]);
    }
    this.changeList = changeInfoList;
    this.viewList = changeViewList;
    this.dgv.Rows.Clear();
    foreach (ProcChanges.ChangeView view in this.viewList)
      this.dgv.Rows.Add((object) view.DesList, (object) view.Goal);
    this.somethingChanged = true;
    this.UpdateButtons();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcChanges));
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    this.panel1 = new Panel();
    this.btnJoin = new Button();
    this.IL = new ImageList(this.components);
    this.btnSort = new Button();
    this.btnSortAll = new Button();
    this.btnDown = new Button();
    this.btnUp = new Button();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.dgv = new DataGridView();
    this.colDesign = new DataGridViewTextBoxColumn();
    this.colGoal = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.dgv).BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnJoin);
    this.panel1.Controls.Add((Control) this.btnSort);
    this.panel1.Controls.Add((Control) this.btnSortAll);
    this.panel1.Controls.Add((Control) this.btnDown);
    this.panel1.Controls.Add((Control) this.btnUp);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 304);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(854, 34);
    this.panel1.TabIndex = 0;
    this.btnJoin.ImageIndex = 6;
    this.btnJoin.ImageList = this.IL;
    this.btnJoin.Location = new Point(250, 2);
    this.btnJoin.Name = "btnJoin";
    this.btnJoin.Size = new Size(187, 29);
    this.btnJoin.TabIndex = 6;
    this.btnJoin.Text = "Объединить со следующим";
    this.btnJoin.TextAlign = ContentAlignment.MiddleLeft;
    this.btnJoin.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.btnJoin, "Объединить текущее изменение со следующим");
    this.btnJoin.UseVisualStyleBackColor = true;
    this.btnJoin.Click += new EventHandler(this.btnJoin_Click);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "вверх.png");
    this.IL.Images.SetKeyName(1, "вниз.png");
    this.IL.Images.SetKeyName(2, "по_возрастанию.png");
    this.IL.Images.SetKeyName(3, "ECOSort.png");
    this.IL.Images.SetKeyName(4, "ok.png");
    this.IL.Images.SetKeyName(5, "ошибка.png");
    this.IL.Images.SetKeyName(6, "объединить_со_следующим.png");
    this.btnSort.ImageIndex = 3;
    this.btnSort.ImageList = this.IL;
    this.btnSort.Location = new Point(83, 2);
    this.btnSort.Name = "btnSort";
    this.btnSort.Size = new Size(33, 29);
    this.btnSort.TabIndex = 5;
    this.btnSort.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.btnSort, "Сортировать текущее изменение по обозначению");
    this.btnSort.UseVisualStyleBackColor = true;
    this.btnSort.Click += new EventHandler(this.btnSort_Click);
    this.btnSortAll.ImageIndex = 2;
    this.btnSortAll.ImageList = this.IL;
    this.btnSortAll.Location = new Point(118, 2);
    this.btnSortAll.Name = "btnSortAll";
    this.btnSortAll.Size = new Size(126, 29);
    this.btnSortAll.TabIndex = 4;
    this.btnSortAll.Text = "Сортировать всё";
    this.btnSortAll.TextAlign = ContentAlignment.MiddleLeft;
    this.btnSortAll.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.btnSortAll, "Сортировать все изменения и упорядочить их по возрастанию");
    this.btnSortAll.UseVisualStyleBackColor = true;
    this.btnSortAll.Click += new EventHandler(this.btnSortAll_Click);
    this.btnDown.ImageIndex = 1;
    this.btnDown.ImageList = this.IL;
    this.btnDown.Location = new Point(42, 2);
    this.btnDown.Name = "btnDown";
    this.btnDown.Size = new Size(33, 29);
    this.btnDown.TabIndex = 3;
    this.btnDown.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.btnDown, "Переместить изменение вниз");
    this.btnDown.UseVisualStyleBackColor = true;
    this.btnDown.Click += new EventHandler(this.btnDown_Click);
    this.btnUp.ImageIndex = 0;
    this.btnUp.ImageList = this.IL;
    this.btnUp.Location = new Point(8, 2);
    this.btnUp.Name = "btnUp";
    this.btnUp.Size = new Size(33, 29);
    this.btnUp.TabIndex = 2;
    this.btnUp.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.btnUp, "Переместить изменение вверх");
    this.btnUp.UseVisualStyleBackColor = true;
    this.btnUp.Click += new EventHandler(this.btnUp_Click);
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.ImageList = this.IL;
    this.btnOK.Location = new Point(655, 2);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(95, 29);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "ОК";
    this.btnOK.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(753, 2);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(95, 29);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.dgv.AllowUserToAddRows = false;
    this.dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    this.dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgv.Columns.AddRange((DataGridViewColumn) this.colDesign, (DataGridViewColumn) this.colGoal);
    gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle.BackColor = SystemColors.Window;
    gridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle.ForeColor = SystemColors.ControlText;
    gridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle.WrapMode = DataGridViewTriState.True;
    this.dgv.DefaultCellStyle = gridViewCellStyle;
    this.dgv.Dock = DockStyle.Fill;
    this.dgv.Location = new Point(0, 0);
    this.dgv.MultiSelect = false;
    this.dgv.Name = "dgv";
    this.dgv.ReadOnly = true;
    this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgv.Size = new Size(854, 304);
    this.dgv.TabIndex = 2;
    this.dgv.SelectionChanged += new EventHandler(this.dgv_SelectionChanged);
    this.colDesign.HeaderText = "Изменения и обозначения включенных в них объектов";
    this.colDesign.Name = "colDesign";
    this.colDesign.ReadOnly = true;
    this.colDesign.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.colDesign.Width = 600;
    this.colGoal.HeaderText = "Цель включения";
    this.colGoal.Name = "colGoal";
    this.colGoal.ReadOnly = true;
    this.colGoal.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.colGoal.Width = 150;
    this.dataGridViewTextBoxColumn1.HeaderText = "Список обозначений";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.Width = 600;
    this.dataGridViewTextBoxColumn2.HeaderText = "Цель включения";
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.Width = 150;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(854, 338);
    this.Controls.Add((Control) this.dgv);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProcChanges);
    this.Text = "Сортировка и объединение изменений";
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.dgv).EndInit();
    this.ResumeLayout(false);
  }

  public enum ChangeState
  {
    NoChange,
    Merged,
    Sorted,
    Deleted,
  }

  public class ChangeInfo
  {
    private List<long> _objIDs;
    private ECOGoal _goal;
    private TableElement _change;
    private ProcChanges.ChangeState _state;
    internal List<TableElement> _mergedList;

    public ChangeInfo(List<long> objIDs, ECOGoal goal, TableElement change)
    {
      this._objIDs = objIDs;
      this._goal = goal;
      this._change = change;
    }

    public List<long> ObjIDs
    {
      get => this._objIDs;
      set => this._objIDs = value;
    }

    public ECOGoal Goal
    {
      get => this._goal;
      set => this._goal = value;
    }

    public TableElement Change
    {
      get => this._change;
      set => this._change = value;
    }

    public ProcChanges.ChangeState State
    {
      get => this._state;
      set => this._state = value;
    }

    public List<TableElement> MergedList
    {
      get => this._mergedList;
      set => this._mergedList = value;
    }

    public bool Sorted => this._state == ProcChanges.ChangeState.Sorted;
  }

  public class ChangeView
  {
    internal ProcChanges.ChangeInfo _info;
    internal List<PendingLink> _pList;
    private string _desList;
    private string _goal;
    private List<string> _desArray;

    public ChangeView(ProcChanges.ChangeInfo info, List<PendingLink> pendList)
    {
      this._info = info;
      this._pList = pendList;
      this.UpdateDesList();
      this._goal = EnumDescConverter.GetEnumDescription((Enum) this._info.Goal);
    }

    public string DesList
    {
      get => this._desList;
      set => this._desList = value;
    }

    public string Goal
    {
      get => this._goal;
      set => this._goal = value;
    }

    public List<string> DesArray
    {
      get => this._desArray;
      set => this._desArray = value;
    }

    private PendingLink FindPendingLink(long objId)
    {
      foreach (PendingLink p in this._pList)
      {
        if (p.verID == objId)
          return p;
      }
      return (PendingLink) null;
    }

    private void UpdateDesList()
    {
      StringBuilder stringBuilder = new StringBuilder();
      this._desArray = new List<string>();
      foreach (long objId in this._info.ObjIDs)
      {
        PendingLink pendingLink = this.FindPendingLink(objId);
        if (pendingLink != null)
        {
          pendingLink.UpdateDesign();
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(pendingLink.design);
          this._desArray.Add(pendingLink.design);
        }
        else
          this._desArray.Add("");
      }
      if (this._desArray.Count == 0)
      {
        if (!(this._info.Change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive))
          return;
        this._desList = templateRecursive.Text;
      }
      else
        this._desList = stringBuilder.ToString();
    }
  }

  internal class DesAndId : IEquatable<ProcChanges.DesAndId>, IComparable<ProcChanges.DesAndId>
  {
    private string _design;
    private long _Id;

    public DesAndId(string des, long Id)
    {
      this._Id = Id;
      this._design = des;
    }

    public string Design => this._design;

    public long Id => this._Id;

    int IComparable<ProcChanges.DesAndId>.CompareTo(ProcChanges.DesAndId other)
    {
      return string.Compare(this._design, other._design);
    }

    bool IEquatable<ProcChanges.DesAndId>.Equals(ProcChanges.DesAndId other)
    {
      return this._design == other._design;
    }
  }
}
