// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EnhDataGridView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class EnhDataGridView : 
  DataGridView,
  ISupportInitialize,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  [CanBeNull]
  private Control _editControl;
  private readonly Dictionary<Color, Pen> _pens = new Dictionary<Color, Pen>();
  private readonly Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
  [NotNull]
  public readonly Dictionary<int, List<object>> _Store = new Dictionary<int, List<object>>();

  public EnhDataGridView()
  {
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.ColumnAdded += new DataGridViewColumnEventHandler(EnhDataGridView.EnhDataGridView_ColumnAdded);
    this.DoubleBuffered = true;
  }

  private static void EnhDataGridView_ColumnAdded([CanBeNull] object sender, [NotNull] DataGridViewColumnEventArgs e)
  {
    if (!(e.Column is DataGridViewButtonTextBoxColumn) && !(e.Column is DataGridViewUpDownColumn))
      return;
    e.Column.DefaultCellStyle.Padding = new Padding(0, 0, 17, 0);
  }

  public event EventHandler EditorButtonClicked;

  private void EditorButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(this._editControl is Panel))
      return;
    EventHandler editorButtonClicked = this.EditorButtonClicked;
    if (editorButtonClicked == null)
      return;
    editorButtonClicked((object) this, (EventArgs) null);
  }

  private void BeginEditControl()
  {
    if (this.ReadOnly)
      return;
    this.EndEditControl();
    Control parent = this.Parent;
    if (this.CurrentCell == null)
      return;
    bool flag1 = this.CurrentCell.OwningColumn is DataGridViewButtonTextBoxColumn;
    bool flag2 = this.CurrentCell.OwningColumn is DataGridViewUpDownColumn;
    if (flag1 | flag2)
    {
      Rectangle displayRectangle = this.GetCellDisplayRectangle(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex, false);
      DataGridViewCell currentCell = this.CurrentCell;
      if (currentCell != null && this.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, true) == displayRectangle)
      {
        int width1 = displayRectangle.Width;
        DataGridViewColumn owningColumn1 = currentCell.OwningColumn;
        Padding padding;
        int num;
        if (owningColumn1 == null)
        {
          num = 0;
        }
        else
        {
          padding = owningColumn1.DefaultCellStyle.Padding;
          num = padding.Right;
        }
        if (width1 >= num)
        {
          Panel panel1 = new Panel();
          Panel panel2 = panel1;
          DataGridViewColumn owningColumn2 = currentCell.OwningColumn;
          int width2;
          if (owningColumn2 == null)
          {
            width2 = 0;
          }
          else
          {
            padding = owningColumn2.DefaultCellStyle.Padding;
            width2 = padding.Right;
          }
          int height = displayRectangle.Height - 1;
          Size size = new Size(width2, height);
          panel2.Size = size;
          Point screen = this.PointToScreen(new Point(displayRectangle.Left + displayRectangle.Width - panel1.Width, displayRectangle.Top));
          panel1.Location = parent != null ? parent.PointToClient(screen) : new Point(0, 0);
          if (flag1)
          {
            Button button = new Button();
            button.Name = "b";
            panel1.Controls.Add((Control) button);
            button.FlatStyle = FlatStyle.Popup;
            button.Size = panel1.Size;
            button.Location = Point.Empty;
            button.BackColor = SystemColors.ButtonFace;
            button.Text = "…";
            button.UseCompatibleTextRendering = true;
            button.Click += new EventHandler(this.EditorButton_Click);
          }
          if (flag2)
          {
            NumericUpDown numericUpDown = new NumericUpDown();
            panel1.Controls.Add((Control) numericUpDown);
            numericUpDown.Size = panel1.Size;
            numericUpDown.UpDownAlign = LeftRightAlignment.Left;
          }
          this._editControl = (Control) panel1;
        }
      }
    }
    if (this._editControl == null)
      return;
    parent?.Controls.Add(this._editControl);
    this._editControl.BringToFront();
  }

  protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
  {
    this.BeginEditControl();
    base.OnColumnWidthChanged(e);
  }

  protected override void OnCurrentCellChanged([NotNull] EventArgs e)
  {
    this.BeginEditControl();
    base.OnCurrentCellChanged(e);
  }

  protected override void OnScroll([NotNull] ScrollEventArgs e)
  {
    this.EndEditControl();
    if (this.CurrentRow != null)
      this.Invalidate(this.GetRowDisplayRectangle(this.CurrentRow.Index, true));
    this.BeginEditControl();
    base.OnScroll(e);
  }

  protected override void OnSizeChanged([NotNull] EventArgs e)
  {
    this.BeginEditControl();
    base.OnSizeChanged(e);
  }

  private void EndEditControl()
  {
    if (this._editControl == null)
      return;
    this._editControl.Hide();
    this.Parent?.Controls.Remove(this._editControl);
    this._editControl.Dispose();
    this._editControl = (Control) null;
  }

  protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
  {
    base.OnCellDoubleClick(e);
    if (e.ColumnIndex < 0 || !(this.Columns[e.ColumnIndex] is DataGridViewButtonTextBoxColumn))
      return;
    this.EditorButton_Click((object) this, EventArgs.Empty);
  }

  [CanBeNull]
  internal Pen GetCachedPen(Color color)
  {
    Pen cachedPen = (Pen) null;
    if (this._pens != null && !this._pens.TryGetValue(color, out cachedPen))
    {
      cachedPen = new Pen(color);
      this._pens.Add(color, cachedPen);
    }
    return cachedPen;
  }

  [CanBeNull]
  internal Brush GetCachedBrush(Color color)
  {
    Brush cachedBrush = (Brush) null;
    if (this._brushes != null && !this._brushes.TryGetValue(color, out cachedBrush))
    {
      cachedBrush = (Brush) new SolidBrush(color);
      this._brushes.Add(color, cachedBrush);
    }
    return cachedBrush;
  }

  protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
  {
    Graphics graphics = e.Graphics;
    if (this.CellBorderStyle != DataGridViewCellBorderStyle.None || e.ColumnIndex < 0 || e.RowIndex < 0)
      return;
    e.Paint(e.ClipBounds, e.PaintParts);
    using (Pen pen = new Pen(this.GridColor))
    {
      pen.DashStyle = DashStyle.Dot;
      Rectangle cellBounds = e.CellBounds;
      graphics.DrawRectangle(pen, cellBounds);
    }
    e.Handled = true;
  }

  protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
  {
    base.OnCellValueNeeded(e);
    List<object> objectList;
    if (!this._Store.TryGetValue(e.RowIndex, out objectList))
      return;
    e.Value = objectList[e.ColumnIndex];
  }

  protected override void OnCellValuePushed(DataGridViewCellValueEventArgs e)
  {
    base.OnCellValuePushed(e);
    List<object> objectList;
    if (!this._Store.TryGetValue(e.RowIndex, out objectList))
    {
      objectList = new List<object>();
      for (int index = 0; index < this.Columns.Count; ++index)
        objectList.Add((object) null);
      this._Store.Add(e.RowIndex, objectList);
    }
    objectList[e.ColumnIndex] = e.Value;
  }

  protected override void OnVisibleChanged([NotNull] EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (this.Visible)
      this.BeginEditControl();
    else
      this.EndEditControl();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!this.ReadOnly)
    {
      if (e.KeyCode == Keys.Delete && this.AllowUserToDeleteRows)
      {
        DataGridViewRow currentRow = this.CurrentRow;
        if (currentRow != null)
        {
          currentRow.Tag = (object) null;
          foreach (DataGridViewCell cell in (BaseCollection) currentRow.Cells)
          {
            cell.Value = (object) null;
            cell.Tag = (object) null;
          }
        }
        if (currentRow != null)
        {
          this.Rows.Remove(currentRow);
          this.Rows.Add();
        }
      }
      else if (e.KeyCode == Keys.Insert && this.AllowUserToAddRows)
      {
        int rowIndex = 0;
        if (this.CurrentRow != null)
          rowIndex = this.CurrentRow.Index;
        this.Rows.Insert(rowIndex, 1);
      }
    }
    if (e.KeyCode != Keys.Return || this.EditingControl != null)
      return;
    Form form = this.FindForm();
    if (form == null)
      return;
    form.DialogResult = DialogResult.OK;
  }

  internal void SaveLayout([NotNull] Dictionary<string, object> dic)
  {
    string name = this.Name;
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      DataGridViewColumn column = this.Columns[index];
      string key = $"{name}.w.{index}";
      dic.Add(key, (object) column.Width);
    }
  }

  internal void LoadLayout([NotNull] Dictionary<string, object> dic)
  {
    string name = this.Name;
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      DataGridViewColumn column = this.Columns[index];
      string key = $"{name}.w.{index}";
      object obj;
      if (dic.TryGetValue(key, out obj) && obj != null)
        column.Width = SimpleFuncs.StringToIntDef(obj.ToString(), column.Width);
    }
  }

  protected override bool ProcessKeyPreview(ref Message m)
  {
    if (this.EditingControl != null)
    {
      switch (new KeyEventArgs((Keys) (int) m.WParam | Control.ModifierKeys).KeyCode)
      {
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Right:
          return false;
      }
    }
    return base.ProcessKeyPreview(ref m);
  }
}
