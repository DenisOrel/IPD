// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.FiltersForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Workflow.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class FiltersForm : Form
{
  private bool _modified;
  private bool _readOnly;
  [NotNull]
  private TaskFilters _filters = new TaskFilters();
  [CanBeNull]
  private ClientProject _project;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panel2;
  private Button _cancButton;
  private Button _okButton;
  private Panel _panel1;
  private Button _deleteButton;
  private Button _copyButton;
  private Button _editButton;
  private Button _addButton;
  private EnhListView _filtersBox;
  private ColumnHeader _nameColumn;
  private ColumnHeader _globalColumn;
  private ColumnHeader _viewColumn;

  [NotNull]
  protected Panel Panel2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel2.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  protected Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Panel Panel1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel1.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  protected Button DeleteButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._deleteButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button CopyButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._copyButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button EditButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button AddButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._addButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected EnhListView FiltersBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._filtersBox.CheckInitializedIn<EnhListView>((object) this);
    }
  }

  [NotNull]
  protected ColumnHeader NameColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameColumn.CheckInitializedIn<ColumnHeader>((object) this);
    }
  }

  [NotNull]
  protected ColumnHeader GlobalColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._globalColumn.CheckInitializedIn<ColumnHeader>((object) this);
    }
  }

  [NotNull]
  protected ColumnHeader ViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._viewColumn.CheckInitializedIn<ColumnHeader>((object) this);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      foreach (ListViewItem listViewItem in this.FiltersBox.Items)
      {
        foreach (IDisposable disposable in listViewItem.SubItems.Cast<ListViewItem.ListViewSubItem>().NotNull<ListViewItem.ListViewSubItem>().OfType<IDisposable>())
          disposable.Dispose();
      }
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Возвращает True, если фильтры были изменены</summary>
  public static bool Show([NotNull] ClientProject project)
  {
    using (FiltersForm filtersForm = new FiltersForm())
    {
      filtersForm.Project = project;
      int num = (int) filtersForm.ShowDialog();
      return filtersForm._modified;
    }
  }

  public FiltersForm()
  {
    this.InitializeComponent();
    if (!this.DesignMode)
      Intermech.Client.Core.FormStorage.LoadLayout((Control) this);
    this.NameColumn.Width = this.FiltersBox.ClientSize.Width - this.GlobalColumn.Width - this.ViewColumn.Width;
  }

  [CanBeNull]
  public TaskFilter CurrentFilter
  {
    get
    {
      FilterItem selectedItem = this.FiltersBox.SelectedItems.Count > 0 ? this.FiltersBox.SelectedItems[0] as FilterItem : (FilterItem) null;
      return selectedItem == null || selectedItem.Filter.AllTasks ? (TaskFilter) null : selectedItem.Filter;
    }
  }

  private int SelectedIndex
  {
    get => this.FiltersBox.SelectedItems.Count > 0 ? this.FiltersBox.SelectedIndices[0] : -1;
    set
    {
      if (this.FiltersBox.Items.Count <= value)
        return;
      this.FiltersBox.Items[value].Selected = true;
    }
  }

  private bool EditFilter()
  {
    int num = EditFilterForm.Edit(this.CurrentFilter, this.Project, this.ReadOnly) ? 1 : 0;
    int selectedIndex = this.SelectedIndex;
    if (selectedIndex <= -1)
      return num != 0;
    FiltersForm.UpdateRow(this.FiltersBox.Items[selectedIndex], this.CurrentFilter);
    return num != 0;
  }

  private static void UpdateRow([NotNull] ListViewItem fi, [CanBeNull] TaskFilter tf)
  {
    fi.Text = fi.ToString();
    for (int index = fi.SubItems.Count - 1; index > 0; --index)
      fi.SubItems.RemoveAt(index);
    if (tf != null && tf.HasFlag(FilterFlags.Global))
      fi.SubItems.Add((ListViewItem.ListViewSubItem) new ImageSubItem()
      {
        Image = (Image) Images.BulletImage
      });
    else
      fi.SubItems.Add(string.Empty);
    if (tf != null && tf.IsPaintFilter)
      fi.SubItems.Add((ListViewItem.ListViewSubItem) new BrushSubItem(GraphicFuncs.StringToPen(tf.PenStr), GraphicFuncs.StringToBrush(tf.BrushStr)));
    else
      fi.SubItems.Add(string.Empty);
  }

  [NotNull]
  private FilterItem AddRow([NotNull] TaskFilter tf)
  {
    FilterItem fi = new FilterItem(tf);
    this.FiltersBox.Items.Add((ListViewItem) fi);
    FiltersForm.UpdateRow((ListViewItem) fi, tf);
    return fi;
  }

  private bool AddFilter([CanBeNull] TaskFilter proto)
  {
    TaskFilter taskFilter = new TaskFilter(proto);
    if (this.ReadOnly && CurrentUser.IsAdmin)
      taskFilter.SetFlag(FilterFlags.Global, true);
    if (!EditFilterForm.Edit(taskFilter, this.Project, this.ReadOnly))
      return false;
    this._filters.Add(taskFilter);
    this.AddRow(taskFilter).Selected = true;
    return true;
  }

  private void AddButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AddFilter((TaskFilter) null);
  }

  private void EditButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditFilter();
  }

  private void FillFiltersBox()
  {
    this.FiltersBox.BeginUpdate();
    try
    {
      this.FiltersBox.Items.Clear();
      foreach (TaskFilter filter in (List<TaskFilter>) this._filters)
        this.AddRow(filter);
    }
    finally
    {
      this.FiltersBox.EndUpdate();
    }
    if (this.FiltersBox.Items.Count <= 0)
      return;
    this.FiltersBox.Items[0].Selected = true;
  }

  private void FiltersForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.FillFiltersBox();
    this.FiltersBox.ListViewItemSorter = (IComparer) Comparer.Default;
  }

  private void FiltersBox_SelectedValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditButton.Enabled = this.CurrentFilter != null;
    this.CopyButton.Enabled = this.EditButton.Enabled && !this.TotalReadOnly;
    Button deleteButton = this.DeleteButton;
    int num;
    if (this.EditButton.Enabled)
    {
      if (this.ReadOnly)
      {
        if (CurrentUser.IsAdmin)
        {
          TaskFilter currentFilter = this.CurrentFilter;
          num = currentFilter != null ? (currentFilter.HasFlag(FilterFlags.Global) ? 1 : 0) : 0;
        }
        else
          num = 0;
      }
      else
        num = 1;
    }
    else
      num = 0;
    deleteButton.Enabled = num != 0;
  }

  private void FiltersBox_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.EditButton.Enabled)
      return;
    this.EditButton_Click((object) null, EventArgs.Empty);
  }

  private void CopyButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AddFilter(this.CurrentFilter);
  }

  private void DeleteButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    int selectedIndex = this.SelectedIndex;
    if (this.CurrentFilter != null)
      this._filters.Remove(this.CurrentFilter);
    ListViewItem selectedItem = this.FiltersBox.SelectedItems[0];
    if (selectedItem != null)
      this.FiltersBox.Items.Remove(selectedItem);
    this.SelectedIndex = Math.Min(selectedIndex, this.FiltersBox.Items.Count - 1);
  }

  private void FiltersForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    this._modified = false;
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this);
    if (this.DialogResult != DialogResult.OK || this.TotalReadOnly)
      return;
    List<TaskFilter> taskFilterList = this._filters.Select(FilterFlags.Global);
    if (!TaskFilters.All.Equals((object) taskFilterList))
    {
      this._modified = true;
      TaskFilters.All.Clear();
      foreach (TaskFilter proto in taskFilterList)
        TaskFilters.All.Add(new TaskFilter(proto));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        TaskFilters.Save(sessionKeeper.Session);
    }
    List<TaskFilter> all = this._filters.FindAll((Predicate<TaskFilter>) (tf => (tf.Flags | ~FilterFlags.Global) == ~FilterFlags.Global));
    if (this.Project.DisplayOptions.Filters.Equals((object) all))
      return;
    this._modified = true;
    this.Project.DisplayOptions.Filters.Clear();
    foreach (TaskFilter proto in all)
      this.Project.DisplayOptions.Filters.Add(new TaskFilter(proto));
    this.Project.DisplayOptions.SetModified(true);
  }

  private void FiltersForm_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    if (!CurrentUser.IsAdmin || !e.Control || !e.Alt || !e.Shift || e.KeyCode != Keys.R || MessageBox.Show("Восстановить стандартные фильтры?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
      return;
    TaskFilters taskFilters = new TaskFilters();
    TaskFilters.AddStandardFilters((List<TaskFilter>) taskFilters);
    foreach (TaskFilter filter in (List<TaskFilter>) this._filters)
    {
      TaskFilter tf = filter;
      bool flag = false;
      if (tf.HasFlag(FilterFlags.Global) && taskFilters.Any<TaskFilter>((Func<TaskFilter, bool>) (tft => tf.Name == tft.Name)))
        flag = true;
      if (!flag)
        taskFilters.Add(tf);
    }
    this._filters = taskFilters;
    this.FillFiltersBox();
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      this.AddButton.Enabled = !this.TotalReadOnly;
      this.FiltersBox_SelectedValueChanged((object) null, EventArgs.Empty);
    }
  }

  private bool TotalReadOnly => this.ReadOnly && !CurrentUser.IsAdmin;

  [NotNull]
  public ClientProject Project
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._project;
    set
    {
      this._project = value;
      if (this._project == null)
        return;
      this.ReadOnly = !this._project.EditingMode.Any();
      this._filters.AddRange(TaskFilters.All.Select<TaskFilter, TaskFilter>((Func<TaskFilter, TaskFilter>) (tf => new TaskFilter(tf))));
      this._filters.AddRange(this._project.DisplayOptions.Filters.Select<TaskFilter, TaskFilter>((Func<TaskFilter, TaskFilter>) (tf => new TaskFilter(tf))));
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FiltersForm));
    this._panel2 = new Panel();
    this._cancButton = new Button();
    this._okButton = new Button();
    this._panel1 = new Panel();
    this._deleteButton = new Button();
    this._copyButton = new Button();
    this._editButton = new Button();
    this._addButton = new Button();
    this._filtersBox = new EnhListView();
    this._nameColumn = new ColumnHeader();
    this._globalColumn = new ColumnHeader();
    this._viewColumn = new ColumnHeader();
    this._panel2.SuspendLayout();
    this._panel1.SuspendLayout();
    this.SuspendLayout();
    this._panel2.BackColor = Color.Transparent;
    this._panel2.Controls.Add((Control) this._cancButton);
    this._panel2.Controls.Add((Control) this._okButton);
    componentResourceManager.ApplyResources((object) this._panel2, "_panel2");
    this._panel2.Name = "_panel2";
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.Name = "_cancButton";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    this._panel1.Controls.Add((Control) this._deleteButton);
    this._panel1.Controls.Add((Control) this._copyButton);
    this._panel1.Controls.Add((Control) this._editButton);
    this._panel1.Controls.Add((Control) this._addButton);
    componentResourceManager.ApplyResources((object) this._panel1, "_panel1");
    this._panel1.Name = "_panel1";
    componentResourceManager.ApplyResources((object) this._deleteButton, "_deleteButton");
    this._deleteButton.Name = "_deleteButton";
    this._deleteButton.UseVisualStyleBackColor = true;
    this._deleteButton.Click += new EventHandler(this.DeleteButton_Click);
    componentResourceManager.ApplyResources((object) this._copyButton, "_copyButton");
    this._copyButton.Name = "_copyButton";
    this._copyButton.UseVisualStyleBackColor = true;
    this._copyButton.Click += new EventHandler(this.CopyButton_Click);
    componentResourceManager.ApplyResources((object) this._editButton, "_editButton");
    this._editButton.Name = "_editButton";
    this._editButton.UseVisualStyleBackColor = true;
    this._editButton.Click += new EventHandler(this.EditButton_Click);
    componentResourceManager.ApplyResources((object) this._addButton, "_addButton");
    this._addButton.Name = "_addButton";
    this._addButton.UseVisualStyleBackColor = true;
    this._addButton.Click += new EventHandler(this.AddButton_Click);
    this._filtersBox.AllowManualSorting = true;
    this._filtersBox.Columns.AddRange(new ColumnHeader[3]
    {
      this._nameColumn,
      this._globalColumn,
      this._viewColumn
    });
    componentResourceManager.ApplyResources((object) this._filtersBox, "_filtersBox");
    this._filtersBox.FullRowSelect = true;
    this._filtersBox.HideSelection = false;
    this._filtersBox.MultiSelect = false;
    this._filtersBox.Name = "_filtersBox";
    this._filtersBox.OwnerDraw = true;
    this._filtersBox.RadioGroups = false;
    this._filtersBox.SortColumn = 0;
    this._filtersBox.Sorting = SortOrder.Ascending;
    this._filtersBox.SubitemImages = (ImageList) null;
    this._filtersBox.UseCompatibleStateImageBehavior = false;
    this._filtersBox.View = View.Details;
    this._filtersBox.SelectedIndexChanged += new EventHandler(this.FiltersBox_SelectedValueChanged);
    this._filtersBox.DoubleClick += new EventHandler(this.FiltersBox_DoubleClick);
    componentResourceManager.ApplyResources((object) this._nameColumn, "_nameColumn");
    componentResourceManager.ApplyResources((object) this._globalColumn, "_globalColumn");
    componentResourceManager.ApplyResources((object) this._viewColumn, "_viewColumn");
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.Controls.Add((Control) this._filtersBox);
    this.Controls.Add((Control) this._panel1);
    this.Controls.Add((Control) this._panel2);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FiltersForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.FiltersForm_FormClosed);
    this.Load += new EventHandler(this.FiltersForm_Load);
    this.KeyDown += new KeyEventHandler(this.FiltersForm_KeyDown);
    this._panel2.ResumeLayout(false);
    this._panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
