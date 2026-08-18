// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourcesDataGridView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
internal class ResourcesDataGridView : 
  DataGridView,
  ISupportInitialize,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  private IContainer _components;
  private DataGridViewComboTextBoxColumn _functionsDataGridViewColumn;
  private DataGridViewTextBoxColumn _nameDataGridViewColumn;
  private DataGridViewTextBoxColumn _notesDataGridViewColumn;
  private DataGridViewTextBoxColumn _overtimeWorkSupplementalHourCostDataGridViewColumn;
  private ToolStripMenuItem _resetToolStripMenuItem;
  private ToolStripSeparator _toolStripSeparator;
  private DataGridViewTextBoxColumn _workHourCostDataGridViewColumn;
  [NotNull]
  private Brush _backgroundColorBrush;
  private bool _duringUpdate;
  private Control _editControl;
  [NotNull]
  private readonly ColumnLayoutInformation[] _initColumnsLayoutInformation;
  private bool _persistLayout;
  [CanBeNull]
  private ResourceCollection _resourceCollection;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal IContainer Components
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._components.CheckInitializedIn<IContainer>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewComboTextBoxColumn FunctionsDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._functionsDataGridViewColumn.CheckInitializedIn<DataGridViewComboTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn NameDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameDataGridViewColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn NotesDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._notesDataGridViewColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn OvertimeWorkSupplementalHourCostDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._overtimeWorkSupplementalHourCostDataGridViewColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ToolStripMenuItem ResetToolStripMenuItem
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._resetToolStripMenuItem.CheckInitializedIn<ToolStripMenuItem>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ToolStripSeparator ToolStripSeparator
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._toolStripSeparator.CheckInitializedIn<ToolStripSeparator>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn WorkHourCostDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._workHourCostDataGridViewColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  private void InitializeComponent()
  {
    this._components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ResourcesDataGridView));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    this.ColumnHeaderContextMenu = new ContextMenuStrip(this._components);
    this._toolStripSeparator = new ToolStripSeparator();
    this._resetToolStripMenuItem = new ToolStripMenuItem();
    this._nameDataGridViewColumn = new DataGridViewTextBoxColumn();
    this._functionsDataGridViewColumn = new DataGridViewComboTextBoxColumn();
    this._workHourCostDataGridViewColumn = new DataGridViewTextBoxColumn();
    this._overtimeWorkSupplementalHourCostDataGridViewColumn = new DataGridViewTextBoxColumn();
    this._notesDataGridViewColumn = new DataGridViewTextBoxColumn();
    this.ColumnHeaderContextMenu.SuspendLayout();
    ((ISupportInitialize) this).BeginInit();
    this.SuspendLayout();
    this.ColumnHeaderContextMenu.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._toolStripSeparator,
      (ToolStripItem) this._resetToolStripMenuItem
    });
    this.ColumnHeaderContextMenu.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.ColumnHeaderContextMenu, "contextMenuStrip");
    this.ColumnHeaderContextMenu.Opening += new CancelEventHandler(this.contextMenuStrip_Opening);
    this._toolStripSeparator.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this._toolStripSeparator, "toolStripSeparator1");
    componentResourceManager.ApplyResources((object) this._resetToolStripMenuItem, "resetToolStripMenuItem");
    this._resetToolStripMenuItem.Name = "resetToolStripMenuItem";
    this._resetToolStripMenuItem.Click += new EventHandler(this.resetToolStripMenuItem_Click);
    this._nameDataGridViewColumn.DataPropertyName = "Name";
    this._nameDataGridViewColumn.FillWeight = 70f;
    this._nameDataGridViewColumn.Frozen = true;
    componentResourceManager.ApplyResources((object) this._nameDataGridViewColumn, "nameDataGridViewColumn");
    this._nameDataGridViewColumn.Name = "nameDataGridViewColumn";
    this._functionsDataGridViewColumn.DataPropertyName = "Functions";
    gridViewCellStyle1.Padding = new Padding(0, 0, 17, 0);
    this._functionsDataGridViewColumn.DefaultCellStyle = gridViewCellStyle1;
    this._functionsDataGridViewColumn.FillWeight = 65f;
    componentResourceManager.ApplyResources((object) this._functionsDataGridViewColumn, "functionsDataGridViewColumn");
    this._functionsDataGridViewColumn.Name = "functionsDataGridViewColumn";
    this._functionsDataGridViewColumn.SortMode = DataGridViewColumnSortMode.Automatic;
    this._workHourCostDataGridViewColumn.DataPropertyName = "WorkHourCost";
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
    gridViewCellStyle2.Format = "N2";
    this._workHourCostDataGridViewColumn.DefaultCellStyle = gridViewCellStyle2;
    this._workHourCostDataGridViewColumn.FillWeight = 60f;
    componentResourceManager.ApplyResources((object) this._workHourCostDataGridViewColumn, "workHourCostDataGridViewColumn");
    this._workHourCostDataGridViewColumn.Name = "workHourCostDataGridViewColumn";
    this._overtimeWorkSupplementalHourCostDataGridViewColumn.DataPropertyName = "OvertimeWorkSupplementalHourCost";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
    gridViewCellStyle3.Format = "N2";
    this._overtimeWorkSupplementalHourCostDataGridViewColumn.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this._overtimeWorkSupplementalHourCostDataGridViewColumn, "overtimeWorkSupplementalHourCostDataGridViewColumn");
    this._overtimeWorkSupplementalHourCostDataGridViewColumn.Name = "overtimeWorkSupplementalHourCostDataGridViewColumn";
    this._notesDataGridViewColumn.DataPropertyName = "NotesString";
    this._notesDataGridViewColumn.FillWeight = 200f;
    componentResourceManager.ApplyResources((object) this._notesDataGridViewColumn, "notesDataGridViewColumn");
    this._notesDataGridViewColumn.Name = "notesDataGridViewColumn";
    this.AllowUserToDeleteRows = true;
    this.AllowUserToOrderColumns = true;
    this.AllowUserToResizeRows = false;
    this.BackgroundColor = SystemColors.Window;
    this.BorderStyle = BorderStyle.None;
    this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.Columns.AddRange((DataGridViewColumn) this._nameDataGridViewColumn, (DataGridViewColumn) this._functionsDataGridViewColumn, (DataGridViewColumn) this._workHourCostDataGridViewColumn, (DataGridViewColumn) this._overtimeWorkSupplementalHourCostDataGridViewColumn, (DataGridViewColumn) this._notesDataGridViewColumn);
    this.EnableHeadersVisualStyles = false;
    this.GridColor = SystemColors.Control;
    this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.ColumnHeaderContextMenu.ResumeLayout(false);
    ((ISupportInitialize) this).EndInit();
    this.ResumeLayout(false);
  }

  public ResourcesDataGridView()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.AutoGenerateColumns = false;
    this._backgroundColorBrush = (Brush) new SolidBrush(this.BackgroundColor);
    this.RefreshBackgroundColorBrush();
    this._initColumnsLayoutInformation = this.GetColumnsLayoutInformation();
  }

  private void b_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(this._editControl is Panel))
      return;
    this.BeginEdit(true);
    if (!(this.EditingControl is DataGridViewComboTextBoxEditingControl editingControl))
      return;
    editingControl.ShowForm();
  }

  private void BeginEditControl()
  {
    this.EndEditControl();
    Control parent;
    try
    {
      parent = this.Parent;
    }
    catch (SecurityException ex)
    {
      return;
    }
    if (this.CurrentCell == null || this.CurrentCell.RowIndex == this.NewRowIndex)
      return;
    if (this.CurrentCell.OwningColumn == this._functionsDataGridViewColumn)
    {
      Rectangle displayRectangle = this.GetCellDisplayRectangle(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex, false);
      if (this.CurrentCell != null && this.GetCellDisplayRectangle(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex, true) == displayRectangle && this.CurrentCell?.OwningColumn?.DefaultCellStyle != null && displayRectangle.Width >= this.CurrentCell.OwningColumn.DefaultCellStyle.Padding.Right)
      {
        Panel panel = new Panel();
        panel.Size = new Size(this.CurrentCell.OwningColumn.DefaultCellStyle.Padding.Right, displayRectangle.Height - 1);
        panel.Location = new Point(displayRectangle.Left + displayRectangle.Width - panel.Width, displayRectangle.Top);
        Button button = new Button();
        button.Name = "b";
        panel.Controls.Add((Control) button);
        button.FlatStyle = FlatStyle.Popup;
        button.Size = panel.Size;
        button.Location = Point.Empty;
        button.Text = "…";
        button.Click += new EventHandler(this.b_Click);
        this._editControl = (Control) panel;
      }
    }
    if (this._editControl == null)
      return;
    parent?.Controls.Add(this._editControl);
    this._editControl.BringToFront();
  }

  private void contextMenuStrip_Opening([CanBeNull] object sender, [NotNull] CancelEventArgs e)
  {
    this.RefreshContextMenu();
  }

  private void contextMenuStripItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EndEditControl();
    if (sender is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.Tag is DataGridViewColumn tag)
      tag.Visible = !tag.Visible;
    this.BeginEditControl();
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

  [NotNull]
  private ColumnLayoutInformation[] GetColumnsLayoutInformation()
  {
    ColumnLayoutInformation[] layoutInformation = new ColumnLayoutInformation[this.Columns.Count];
    for (int index = 0; index < this.Columns.Count; ++index)
      layoutInformation[index] = new ColumnLayoutInformation(this.Columns[index].Name, this.Columns[index].HeaderText, this.Columns[index].DisplayIndex, this.Columns[index].Visible, 0);
    return layoutInformation;
  }

  private void InitCellEdit()
  {
    if (this.CurrentCell == null || this.CurrentCell.RowIndex == this.NewRowIndex || this.CurrentCell.OwningColumn != this._functionsDataGridViewColumn || !(this.EditingControl is DataGridViewComboTextBoxEditingControl editingControl))
      return;
    DataTable source = new DataTable();
    source.Columns.Add("Name");
    source.Columns.Add("Value");
    if (this.ResourceCollection != null)
    {
      foreach (string function in this.ResourceCollection.Functions)
        source.Rows.Add((object) function, (object) function);
    }
    editingControl.SetProperties(this.CurrentCell.OwningColumn.Width, source, "Name", "Value");
  }

  private void LoadLayout()
  {
    try
    {
      using (IsolatedStorageFileStream s = new IsolatedStorageFileStream("ResourcesView.Columns.layout", FileMode.Open))
        this.LoadLayout((Stream) s);
    }
    catch (IOException ex)
    {
    }
    catch (IsolatedStorageException ex)
    {
    }
    catch (SerializationException ex)
    {
    }
    catch (SecurityException ex)
    {
    }
  }

  public void LoadLayout([NotNull] Stream s)
  {
    this.SetColumnsLayoutInformation((ColumnLayoutInformation[]) new BinaryFormatter().Deserialize(s));
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    Rectangle displayRectangle = this.GetRowDisplayRectangle(this.NewRowIndex, false);
    if (this.RowTemplate != null)
      displayRectangle.Y += this.RowTemplate.Height;
    e.Graphics.FillRectangle(this._backgroundColorBrush, displayRectangle);
  }

  protected override void OnBackgroundColorChanged([NotNull] EventArgs e)
  {
    base.OnBackgroundColorChanged(e);
    this.RefreshBackgroundColorBrush();
  }

  protected override void OnDataError(
    bool displayErrorDialogIfNoHandler,
    [NotNull] DataGridViewDataErrorEventArgs e)
  {
    base.OnDataError(displayErrorDialogIfNoHandler, e);
    e.ThrowException = false;
  }

  private void RefreshBackgroundColorBrush()
  {
    this._backgroundColorBrush = (Brush) new SolidBrush(this.BackgroundColor);
  }

  private void RefreshContextMenu()
  {
    if (this.ColumnHeaderContextMenu == null)
      return;
    foreach (ToolStripItem toolStripItem in this.ColumnHeaderContextMenu.Items.Cast<ToolStripItem>().Where<ToolStripItem>((System.Func<ToolStripItem, bool>) (item => item.Tag is DataGridViewColumn)).ToList<ToolStripItem>(this.ColumnHeaderContextMenu.Items.Count))
      this.ColumnHeaderContextMenu.Items.Remove(toolStripItem);
    int num = 0;
    foreach (DataGridViewColumn column in (BaseCollection) this.Columns)
    {
      if (column != this._nameDataGridViewColumn)
      {
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(column.HeaderText, (Image) null, new EventHandler(this.contextMenuStripItem_Click));
        toolStripMenuItem.Tag = (object) column;
        toolStripMenuItem.Checked = column.Visible;
        this.ColumnHeaderContextMenu.Items.Insert(num++, (ToolStripItem) toolStripMenuItem);
      }
      else if (num > 0)
      {
        ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
        toolStripSeparator.Tag = (object) column;
        this.ColumnHeaderContextMenu.Items.Insert(num++, (ToolStripItem) toolStripSeparator);
      }
    }
  }

  private void resetToolStripMenuItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EndEditControl();
    this.SetColumnsLayoutInformation(this._initColumnsLayoutInformation);
    this.BeginEditControl();
  }

  protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
  {
    base.OnCellBeginEdit(e);
    DataGridViewCell currentCell = this.CurrentCell;
    if (currentCell != null && this.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false) != this.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, true) && currentCell.OwningColumn != null && !currentCell.OwningColumn.Frozen)
      this.FirstDisplayedScrollingColumnIndex = currentCell.ColumnIndex;
    if (e.RowIndex == this.NewRowIndex)
      this.FirstDisplayedScrollingRowIndex = e.RowIndex;
    this.BeginInvoke((Delegate) new ThreadStart(this.InitCellEdit));
  }

  protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
  {
    base.OnColumnWidthChanged(e);
    this.BeginEditControl();
  }

  protected override void OnCurrentCellChanged([NotNull] EventArgs e)
  {
    base.OnCurrentCellChanged(e);
    this.BeginEditControl();
  }

  protected override void OnGotFocus([NotNull] EventArgs e)
  {
    base.OnGotFocus(e);
    if (!this.PersistLayout || this.CurrentCell != null && this.CurrentCell.IsInEditMode)
      return;
    this.LoadLayout();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.KeyCode != Keys.Delete)
      return;
    if (this.CurrentCell != null && !this.CurrentCell.ReadOnly && !this.ReadOnly && !this.Columns[this.CurrentCell.ColumnIndex].ReadOnly && !this.Rows[this.CurrentCell.RowIndex].ReadOnly && this.SelectedRows.Count == 0)
    {
      if (this.BeginEdit(false))
      {
        try
        {
          this.CurrentCell.Value = (object) null;
          this.EndEdit();
        }
        catch (NullReferenceException ex)
        {
          this.CancelEdit();
        }
        e.Handled = true;
      }
    }
    if (this.NewRowIndex < 0 || !this.SelectedRows.Contains(this.Rows[this.NewRowIndex]))
      return;
    e.Handled = true;
  }

  protected override void OnLostFocus([NotNull] EventArgs e)
  {
    base.OnLostFocus(e);
    if (!this.PersistLayout || this.CurrentCell != null && this.CurrentCell.IsInEditMode)
      return;
    this.SaveLayout();
  }

  protected override void OnMouseDown([NotNull] MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (!this.UseColumnHeaderContextMenu || this.HitTest(e.X, e.Y).Type != DataGridViewHitTestType.ColumnHeader || e.Button != MouseButtons.Right || this.IsCurrentCellInEditMode && !this.EndEdit())
      return;
    try
    {
      this.Focus();
    }
    catch (SecurityException ex)
    {
    }
    if (this.ColumnHeaderContextMenu == null)
      return;
    this.ColumnHeaderContextMenu.Show((Control) this, e.X, e.Y);
  }

  protected override void OnScroll([NotNull] ScrollEventArgs e)
  {
    base.OnScroll(e);
    this.EndEditControl();
    if (this.CurrentRow != null)
      this.Invalidate(this.GetRowDisplayRectangle(this.CurrentRow.Index, true));
    this.BeginEditControl();
  }

  protected override void OnSizeChanged([NotNull] EventArgs e)
  {
    base.OnSizeChanged(e);
    this.BeginEditControl();
  }

  private void SaveLayout()
  {
    try
    {
      using (IsolatedStorageFileStream s = new IsolatedStorageFileStream("ResourcesView.Columns.layout", FileMode.Create))
        this.SaveLayout((Stream) s);
    }
    catch (IOException ex)
    {
    }
    catch (IsolatedStorageException ex)
    {
    }
    catch (SerializationException ex)
    {
    }
    catch (SecurityException ex)
    {
    }
  }

  public void SaveLayout([NotNull] Stream s)
  {
    ColumnLayoutInformation[] layoutInformation = this.GetColumnsLayoutInformation();
    new BinaryFormatter().Serialize(s, (object) layoutInformation);
  }

  private void SetColumnsLayoutInformation([NotNull] ColumnLayoutInformation[] cc)
  {
    foreach (ColumnLayoutInformation layoutInformation in cc)
    {
      DataGridViewColumn column = this.Columns[layoutInformation.ColumnName];
      if (column != null)
      {
        column.Visible = layoutInformation.Visible;
        try
        {
          column.DisplayIndex = layoutInformation.DisplayIndex;
        }
        catch (InvalidOperationException ex)
        {
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ContextMenuStrip ColumnHeaderContextMenu { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool DuringUpdate
  {
    get => this._duringUpdate;
    set
    {
      if (value == this.DuringUpdate)
        return;
      this._duringUpdate = value;
      if (this.ResourceCollection == null)
        return;
      this.ResourceCollection.RaiseListChangedEvents = !this.DuringUpdate;
      if (this.DuringUpdate)
        return;
      this.ResourceCollection.ResetBindings();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool PersistLayout
  {
    get => this._persistLayout;
    set
    {
      this._persistLayout = value;
      if (!this.PersistLayout)
        return;
      this.LoadLayout();
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(null)]
  public ResourceCollection ResourceCollection
  {
    get => this._resourceCollection;
    set
    {
      if (value == this.ResourceCollection)
        return;
      this._resourceCollection = value;
      this.DataSource = (object) this.ResourceCollection;
    }
  }

  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool UseColumnHeaderContextMenu { get; set; }
}
