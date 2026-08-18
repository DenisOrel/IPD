// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EditFilterForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Project.Evaluator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class EditFilterForm : Form
{
  private List<object> _possibleValues;
  [NotNull]
  private readonly ClientProject _project;
  [NotNull]
  private TaskFilter _filter;
  private bool _readOnly;
  private Pen _pen;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panelDialogButtons;
  private Button _buttonCancel;
  private Button _buttonOk;
  private Label _labelName;
  private TextBox _textboxName;
  private CheckBox _checkShowSum;
  private CheckBox _checkGlobal;
  private EnhDataGridView _view;
  private DataGridViewComboBoxColumn _columnGroup;
  private DataGridViewComboBoxColumn _columnField;
  private DataGridViewComboBoxColumn _columnOperation;
  private DataGridViewDropDownColumn _columnValue;
  private ComboBox _comboMode;
  private Label _labelMode;
  private Button _buttonSelectColor;
  private ComboBox _comboBrushes;
  private ToolTip _toolTip;

  [NotNull]
  protected Panel PanelDialogButtons
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelDialogButtons.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  protected Button ButtonCancel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCancel.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button ButtonOk
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonOk.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Label LabelName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelName.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  protected TextBox TextboxName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textboxName.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  protected CheckBox CheckShowSum
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkShowSum.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  protected CheckBox CheckGlobal
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkGlobal.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  protected EnhDataGridView View
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._view.CheckInitializedIn<EnhDataGridView>((object) this);
    }
  }

  [NotNull]
  protected DataGridViewComboBoxColumn ColumnGroup
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._columnGroup.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  protected DataGridViewComboBoxColumn ColumnField
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._columnField.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  protected DataGridViewComboBoxColumn ColumnOperation
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._columnOperation.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  internal DataGridViewDropDownColumn ColumnValue
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._columnValue.CheckInitializedIn<DataGridViewDropDownColumn>((object) this);
    }
  }

  [NotNull]
  protected ComboBox ComboMode
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboMode.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  protected Label LabelMode
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMode.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  protected Button ButtonSelectColor
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSelectColor.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected ComboBox ComboBrushes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBrushes.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  protected ToolTip ToolTip
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._toolTip.CheckInitializedIn<ToolTip>((object) this);
    }
  }

  public static bool Edit([NotNull] TaskFilter filter, [NotNull] ClientProject project, bool readOnly)
  {
    using (EditFilterForm editFilterForm = new EditFilterForm(filter, project, readOnly))
      return editFilterForm.ShowDialog() == DialogResult.OK && !editFilterForm.ReadOnly;
  }

  public EditFilterForm([NotNull] TaskFilter filter, [NotNull] ClientProject project, bool readOnly)
  {
    this._project = project;
    this._filter = (TaskFilter) null;
    this.InitializeComponent();
    this.View.Rows.AddCopies(0, 50);
    this.ColumnField.ValueType = typeof (PropInfo);
    this.ColumnField.Items.AddRange((object[]) PropInfos.All.ToArray());
    this.ColumnOperation.ValueType = typeof (Operation);
    this.ColumnOperation.Items.AddRange((object[]) Operations.All.ToArray());
    List<Brush> brushList = new List<Brush>();
    brushList.Add((Brush) new SolidBrush(Color.White));
    brushList.Add((Brush) new SolidBrush(Color.Black));
    IReadOnlyList<HatchStyle> source = EnumHelper.PossibleValues<HatchStyle>();
    brushList.AddRange((IEnumerable<Brush>) source.Select<HatchStyle, HatchBrush>((Func<HatchStyle, HatchBrush>) (v => new HatchBrush(v, Color.Black, this.ComboBrushes.BackColor))));
    this.ComboBrushes.Items.AddRange((object[]) brushList.ToArray());
    if (!this.DesignMode)
      Intermech.Client.Core.FormStorage.LoadLayout((Control) this);
    this.Filter = filter;
    this.ReadOnly = readOnly;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      foreach (Brush brush in this.ComboBrushes.Items.OfType<Brush>())
        brush.Dispose();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  protected void SaveTo([NotNull] TaskFilter filter)
  {
    TaskFilter filter1;
    try
    {
      filter1 = this.Filter;
    }
    catch (EditFilterForm.DataErrorException ex)
    {
      if (ex.Control != null)
        ex.Control.Focus();
      else
        this.View.CurrentCell = this.View.Rows[ex.RowIndex].Cells[ex.ColIndex];
      throw new NotificationException(Localization.GetString("ErrIncorrectFieldValue"));
    }
    if (filter == filter1)
      return;
    filter.Assign(filter1);
  }

  private void View_DataError([CanBeNull] object sender, [NotNull] DataGridViewDataErrorEventArgs e)
  {
  }

  private void View_CellParsing([CanBeNull] object sender, [NotNull] DataGridViewCellParsingEventArgs e)
  {
    if (this.View.Columns[e.ColumnIndex] == this.ColumnValue)
    {
      if (this._possibleValues != null && e.Value != null)
      {
        foreach (object possibleValue in this._possibleValues)
        {
          if (possibleValue.ToString() == e.Value.ToString())
          {
            e.Value = possibleValue;
            break;
          }
        }
      }
    }
    else
      e.Value = this.View.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
    e.ParsingApplied = true;
  }

  private void View_EditingControlShowing(
    [CanBeNull] object sender,
    [NotNull] DataGridViewEditingControlShowingEventArgs e)
  {
    if (!(e.Control is ComboBox control))
      return;
    control.SelectedIndexChanged -= new EventHandler(this.View_ComboSelectedIndexChanged);
    control.SelectedIndexChanged += new EventHandler(this.View_ComboSelectedIndexChanged);
    control.TextChanged -= new EventHandler(this.View_ComboTextChanged);
    control.TextChanged += new EventHandler(this.View_ComboTextChanged);
    control.DropDown -= new EventHandler(EditFilterForm.View_Combo_DropDown);
    control.DropDown += new EventHandler(EditFilterForm.View_Combo_DropDown);
    DataGridViewCell currentCell = this.View.CurrentCell;
    if (currentCell == null)
      return;
    if (currentCell.OwningColumn == this.ColumnValue)
    {
      control.DropDownStyle = ComboBoxStyle.DropDown;
      this._possibleValues = new List<object>();
      if (this.View.Rows[currentCell.RowIndex].Cells[1].Value is PropInfo propInfo)
      {
        PossibleValues possibleValues = propInfo.PossibleValues;
        if (possibleValues != null)
        {
          foreach (object obj in (List<PossibleValue>) possibleValues)
            this._possibleValues.Add(obj);
        }
        if (this._possibleValues.Count > 0)
          this._possibleValues.Add((object) new PossibleValue(string.Empty.PadRight(250, '—'), (object) string.Empty));
      }
      this._possibleValues.AddRange((IEnumerable<object>) PropInfos.All.ToList<PropInfo>());
      control.Items.AddRange(this._possibleValues.ToArray());
      if (currentCell.Value == null)
        return;
      control.Text = currentCell.Value.ToString();
    }
    else
    {
      if (currentCell.OwningColumn != this.ColumnOperation || !(this.View.Rows[currentCell.RowIndex].Cells[1].Value is PropInfo pi))
        return;
      control.Items.Clear();
      control.Items.AddRange((object[]) Operations.All.Filter(pi).ToArray());
      if (currentCell.Value == null)
        return;
      control.Text = currentCell.Value.ToString();
    }
  }

  private static void View_Combo_DropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (sender == null)
      return;
    ((Control) sender).BackColor = SystemColors.Window;
  }

  private void View_ComboTextChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.View.NotifyCurrentCellDirty(true);
  }

  private void View_ComboSelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    DataGridViewCell currentCell = this.View.CurrentCell;
    if (currentCell == null)
      return;
    currentCell.Value = ((ComboBox) sender)?.SelectedItem;
  }

  [NotNull]
  protected TaskFilter Filter
  {
    get
    {
      if (this.TextboxName.Text.Trim() == string.Empty)
        throw new EditFilterForm.DataErrorException((Control) this.TextboxName);
      TaskFilter filter = new TaskFilter(this.TextboxName.Text);
      filter.SetFlag(FilterFlags.Global, this.CheckGlobal.Checked);
      filter.SetFlag(FilterFlags.ShowSummaryTasks, this.CheckShowSum.Checked);
      for (int index = 0; index < this.View.Rows.Count; ++index)
      {
        DataGridViewRow row = this.View.Rows[index];
        PropInfo property = row.Cells[1].Value as PropInfo;
        Operation operation = row.Cells[2].Value as Operation;
        object obj = row.Cells[3].Value;
        if (operation != null || property != null || obj != null && !(obj.ToString() == string.Empty))
        {
          if (operation != null && property == null)
            throw new EditFilterForm.DataErrorException(index, 1);
          if (property != null && operation == null)
            throw new EditFilterForm.DataErrorException(index, 2);
          if (property != null)
          {
            Expression expression = new Expression(property, operation, obj);
            filter.Expressions.Add(expression);
            if (row.Cells[0].Value == this.ColumnGroup.Items[1])
              expression.GroupOperation = GroupOperation.Or;
          }
        }
      }
      filter.PenStr = GraphicFuncs.PenToString(this.Pen);
      filter.BrushStr = GraphicFuncs.BrushToString(this.Brush);
      if (!filter.RequiresInput)
      {
        try
        {
          Intermech.Project.Evaluator.Evaluator.Eval((Task) this._project, filter);
        }
        catch (Exception ex)
        {
          throw new NotificationException(ex.Message);
        }
      }
      return filter;
    }
    set
    {
      this._filter = value;
      this.TextboxName.Text = value.Name;
      this.CheckShowSum.Checked = value.HasFlag(FilterFlags.ShowSummaryTasks);
      for (int index = 0; index < value.Expressions.Count; ++index)
      {
        Expression expression = value.Expressions[index];
        DataGridViewRow row = this.View.Rows[index];
        if (index > 0)
          row.Cells[0].Value = this.ColumnGroup.Items[(int) expression.GroupOperation];
        row.Cells[1].Value = (object) expression.Property;
        row.Cells[2].Value = (object) expression.Operation;
        row.Cells[3].Value = expression.Value;
      }
      this.FilterMode = true;
      this.Pen = GraphicFuncs.StringToPen(value.PenStr);
      this.Brush = GraphicFuncs.StringToBrush(value.BrushStr);
    }
  }

  private void EditFilterForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this);
    if (this.DialogResult != DialogResult.OK)
      return;
    this.SaveTo(this._filter);
  }

  private void View_CellEnter([CanBeNull] object sender, [NotNull] DataGridViewCellEventArgs e)
  {
    this._possibleValues = (List<object>) null;
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (this._filter.HasFlag(FilterFlags.Global))
        this._readOnly = !CurrentUser.IsAdmin;
      this.TextboxName.ReadOnly = this._readOnly;
      this.View.ReadOnly = this._readOnly;
      if (this._readOnly)
      {
        foreach (DataGridViewComboBoxColumn viewComboBoxColumn in this.View.Columns.OfType<DataGridViewComboBoxColumn>())
          viewComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
      }
      if (!CurrentUser.IsAdmin)
        this.CheckGlobal.Enabled = false;
      if (!this._readOnly)
        return;
      this.CheckShowSum.Enabled = false;
      this.ComboMode.Enabled = false;
      this.ComboBrushes.Enabled = false;
      this.View.ReadOnly = true;
      this.TextboxName.ReadOnly = true;
      this.ButtonOk.Enabled = false;
      if (!this.ComboBrushes.Visible)
        return;
      this.ComboBrushes.Enabled = false;
    }
  }

  public bool FilterMode
  {
    get => this.ComboMode.SelectedIndex == 0;
    set => this.ComboMode.SelectedIndex = value ? 0 : 1;
  }

  private void ModeBox_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    bool filterMode = this.FilterMode;
    this.CheckShowSum.Visible = filterMode;
    this.ButtonSelectColor.Visible = !filterMode;
    this.ComboBrushes.Visible = !filterMode;
    if (filterMode || this.ButtonSelectColor.Tag != null)
      return;
    this.Color = IMProject.DefaultTaskColor;
    this.Brush = IMProject.DefaultTaskBrush;
  }

  private Color Color
  {
    get => this.ButtonSelectColor.Tag is Color tag ? tag : Color.Empty;
    set
    {
      this._pen = (Pen) null;
      Bitmap bitmap = new Bitmap(11, 11);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        using (SolidBrush solidBrush = new SolidBrush(value))
          graphics.FillRectangle((Brush) solidBrush, 0, 0, bitmap.Width, bitmap.Height);
      }
      this.ButtonSelectColor.Image = (Image) bitmap;
      this.ButtonSelectColor.Tag = (object) value;
      this.ComboBrushes.Invalidate();
    }
  }

  private void ColorButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (ColorDialog colorDialog = new ColorDialog())
    {
      if (colorDialog.ShowDialog() != DialogResult.OK)
        return;
      this.Color = colorDialog.Color;
    }
  }

  private void BrushesBox_DrawItem([CanBeNull] object sender, [NotNull] DrawItemEventArgs e)
  {
    if (e.Index < 0)
      return;
    if (GraphicFuncs.AreColorsSimilar(this.ComboBrushes.BackColor, this.Color))
      this.ComboBrushes.BackColor = GraphicFuncs.AreColorsSimilar(this.Color, Color.White, 50) ? Color.Silver : SystemColors.Window;
    Rectangle bounds = e.Bounds;
    bounds.Inflate(-3, -3);
    Brush brush = this.ComboBrushes.Items[e.Index] as Brush;
    if (brush is HatchBrush hatchBrush)
    {
      if (hatchBrush.ForegroundColor != this.Color)
      {
        brush = (Brush) new HatchBrush(hatchBrush.HatchStyle, this.Color, this.ComboBrushes.BackColor);
        this.ComboBrushes.Items[e.Index] = (object) brush;
      }
    }
    else if (e.Index > 0 && brush is SolidBrush solidBrush && solidBrush.Color != this.Color)
    {
      brush = (Brush) new SolidBrush(this.Color);
      this.ComboBrushes.Items[e.Index] = (object) brush;
    }
    e.Graphics.FillRectangle(brush, bounds);
    Pen pen = this.Pen;
    e.Graphics.DrawRectangle(pen, bounds);
  }

  [CanBeNull]
  public Brush Brush
  {
    get => !this.FilterMode ? this.ComboBrushes.SelectedItem as Brush : (Brush) null;
    set
    {
      if (value == null)
        return;
      this.FilterMode = false;
      for (int index = 0; index < this.ComboBrushes.Items.Count - 1; ++index)
      {
        switch (value)
        {
          case HatchBrush hatchBrush2 when this.ComboBrushes.Items[index] is HatchBrush hatchBrush1 && hatchBrush1.HatchStyle == hatchBrush2.HatchStyle:
            this.ComboBrushes.SelectedIndex = index;
            return;
          case SolidBrush solidBrush:
            this.ComboBrushes.SelectedIndex = solidBrush.Color == Color.White ? 0 : 1;
            break;
        }
      }
    }
  }

  [CanBeNull]
  public Pen Pen
  {
    get => this.FilterMode ? (Pen) null : this._pen ?? (this._pen = new Pen(this.Color));
    set
    {
      if (value == null)
        return;
      this.Color = value.Color;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditFilterForm));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    this._panelDialogButtons = new Panel();
    this._buttonCancel = new Button();
    this._buttonOk = new Button();
    this._labelName = new Label();
    this._textboxName = new TextBox();
    this._checkShowSum = new CheckBox();
    this._checkGlobal = new CheckBox();
    this._comboMode = new ComboBox();
    this._labelMode = new Label();
    this._buttonSelectColor = new Button();
    this._comboBrushes = new ComboBox();
    this._view = new EnhDataGridView();
    this._columnGroup = new DataGridViewComboBoxColumn();
    this._columnField = new DataGridViewComboBoxColumn();
    this._columnOperation = new DataGridViewComboBoxColumn();
    this._columnValue = new DataGridViewDropDownColumn();
    this._toolTip = new ToolTip(this.components);
    this._panelDialogButtons.SuspendLayout();
    ((ISupportInitialize) this._view).BeginInit();
    this.SuspendLayout();
    this._panelDialogButtons.BackColor = Color.Transparent;
    this._panelDialogButtons.Controls.Add((Control) this._buttonCancel);
    this._panelDialogButtons.Controls.Add((Control) this._buttonOk);
    componentResourceManager.ApplyResources((object) this._panelDialogButtons, "_panelDialogButtons");
    this._panelDialogButtons.Name = "_panelDialogButtons";
    componentResourceManager.ApplyResources((object) this._buttonCancel, "_buttonCancel");
    this._buttonCancel.DialogResult = DialogResult.Cancel;
    this._buttonCancel.Name = "_buttonCancel";
    componentResourceManager.ApplyResources((object) this._buttonOk, "_buttonOk");
    this._buttonOk.DialogResult = DialogResult.OK;
    this._buttonOk.Name = "_buttonOk";
    componentResourceManager.ApplyResources((object) this._labelName, "_labelName");
    this._labelName.Name = "_labelName";
    componentResourceManager.ApplyResources((object) this._textboxName, "_textboxName");
    this._textboxName.Name = "_textboxName";
    componentResourceManager.ApplyResources((object) this._checkShowSum, "_checkShowSum");
    this._checkShowSum.Name = "_checkShowSum";
    this._checkShowSum.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._checkGlobal, "_checkGlobal");
    this._checkGlobal.Name = "_checkGlobal";
    this._checkGlobal.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._comboMode, "_comboMode");
    this._comboMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboMode.FormattingEnabled = true;
    this._comboMode.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("_comboMode.Items"),
      (object) componentResourceManager.GetString("_comboMode.Items1")
    });
    this._comboMode.Name = "_comboMode";
    this._comboMode.SelectedIndexChanged += new EventHandler(this.ModeBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._labelMode, "_labelMode");
    this._labelMode.FlatStyle = FlatStyle.System;
    this._labelMode.Name = "_labelMode";
    componentResourceManager.ApplyResources((object) this._buttonSelectColor, "_buttonSelectColor");
    this._buttonSelectColor.Name = "_buttonSelectColor";
    this._buttonSelectColor.UseVisualStyleBackColor = true;
    this._buttonSelectColor.Click += new EventHandler(this.ColorButton_Click);
    componentResourceManager.ApplyResources((object) this._comboBrushes, "_comboBrushes");
    this._comboBrushes.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboBrushes.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBrushes.FormattingEnabled = true;
    this._comboBrushes.Name = "_comboBrushes";
    this._comboBrushes.DrawItem += new DrawItemEventHandler(this.BrushesBox_DrawItem);
    componentResourceManager.ApplyResources((object) this._view, "_view");
    this._view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
    this._view.BackgroundColor = SystemColors.Window;
    this._view.BorderStyle = BorderStyle.Fixed3D;
    this._view.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._view.Columns.AddRange((DataGridViewColumn) this._columnGroup, (DataGridViewColumn) this._columnField, (DataGridViewColumn) this._columnOperation, (DataGridViewColumn) this._columnValue);
    this._view.EnableHeadersVisualStyles = false;
    this._view.Name = "_view";
    this._view.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    this._view.CellEnter += new DataGridViewCellEventHandler(this.View_CellEnter);
    this._view.CellParsing += new DataGridViewCellParsingEventHandler(this.View_CellParsing);
    this._view.DataError += new DataGridViewDataErrorEventHandler(this.View_DataError);
    this._view.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.View_EditingControlShowing);
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
    this._columnGroup.DefaultCellStyle = gridViewCellStyle1;
    this._columnGroup.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._columnGroup.DisplayStyleForCurrentCellOnly = true;
    this._columnGroup.FlatStyle = FlatStyle.Popup;
    componentResourceManager.ApplyResources((object) this._columnGroup, "_columnGroup");
    this._columnGroup.Items.AddRange((object) "И", (object) "Или");
    this._columnGroup.Name = "_columnGroup";
    this._columnGroup.Resizable = DataGridViewTriState.True;
    this._columnGroup.SortMode = DataGridViewColumnSortMode.Automatic;
    this._columnField.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._columnField.DisplayStyleForCurrentCellOnly = true;
    this._columnField.FlatStyle = FlatStyle.Popup;
    componentResourceManager.ApplyResources((object) this._columnField, "_columnField");
    this._columnField.Name = "_columnField";
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
    this._columnOperation.DefaultCellStyle = gridViewCellStyle2;
    this._columnOperation.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._columnOperation.DisplayStyleForCurrentCellOnly = true;
    this._columnOperation.FlatStyle = FlatStyle.Popup;
    componentResourceManager.ApplyResources((object) this._columnOperation, "_columnOperation");
    this._columnOperation.Name = "_columnOperation";
    this._columnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this._columnValue.DisplayStyleForCurrentCellOnly = true;
    this._columnValue.FlatStyle = FlatStyle.Popup;
    componentResourceManager.ApplyResources((object) this._columnValue, "_columnValue");
    this._columnValue.Name = "_columnValue";
    this.AcceptButton = (IButtonControl) this._buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._buttonCancel;
    this.Controls.Add((Control) this._comboBrushes);
    this.Controls.Add((Control) this._buttonSelectColor);
    this.Controls.Add((Control) this._labelMode);
    this.Controls.Add((Control) this._comboMode);
    this.Controls.Add((Control) this._view);
    this.Controls.Add((Control) this._checkGlobal);
    this.Controls.Add((Control) this._checkShowSum);
    this.Controls.Add((Control) this._textboxName);
    this.Controls.Add((Control) this._labelName);
    this.Controls.Add((Control) this._panelDialogButtons);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditFilterForm);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.EditFilterForm_FormClosing);
    this._panelDialogButtons.ResumeLayout(false);
    ((ISupportInitialize) this._view).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal class DataErrorException : Exception
  {
    public readonly int RowIndex;
    public readonly int ColIndex;
    [CanBeNull]
    public readonly Control Control;

    public DataErrorException(int rowIndex, int colIndex)
    {
      this.RowIndex = rowIndex;
      this.ColIndex = colIndex;
    }

    public DataErrorException([CanBeNull] Control control) => this.Control = control;
  }
}
