// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DurationForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Project.Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class DurationForm : Form
{
  [CanBeNull]
  public Control Control;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private NumericUpDown _upDown;
  private ComboBox _unitsCombo;
  private Panel _panel1;
  private CheckBox _estimationCheckBox;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown UpDown
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._upDown.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox UnitsCombo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._unitsCombo.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel Panel1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel1.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox EstimationCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._estimationCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  public event EventHandler ValueChanged;

  public DurationForm()
  {
    this.InitializeComponent();
    this.UpDown.Maximum = Decimal.MaxValue;
    foreach (KeyValuePair<string, WorkTimeUnit> unit in WorkTimeUnits.Units)
    {
      WorkTimeUnit workTimeUnit;
      unit.Deconstruct<string, WorkTimeUnit>(out string _, out workTimeUnit);
      this.UnitsCombo.Items.Add((object) workTimeUnit);
    }
  }

  private void DurationForm_Deactivate([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.Close();
  }

  [NotNull]
  public string Value
  {
    get
    {
      return !(this.UnitsCombo.SelectedItem is WorkTimeUnit selectedItem) ? "?" : string.Format(Task._DurationFormat, (object) (double) this.UpDown.Value, (object) selectedItem.ShortName, this.EstimationCheckBox.Checked ? (object) Intermech.Project.IMProject.EstimationSymbol : (object) string.Empty);
    }
    set
    {
      WorkTimeValue workTimeValue = WorkTimeUnits.Parse(value, WorkTimeUnits.Hours);
      if (workTimeValue == null)
        return;
      if (workTimeValue.Value - Math.Truncate(workTimeValue.Value) > 0.0)
        this.UpDown.DecimalPlaces = 2;
      this.UpDown.Value = (Decimal) workTimeValue.Value;
      this.UnitsCombo.SelectedItem = (object) workTimeValue.Unit;
      this.EstimationCheckBox.Checked = workTimeValue.Estimation;
    }
  }

  private void UpDown_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.ValueChanged == null)
      return;
    this.ValueChanged((object) this, e);
  }

  private void DurationForm_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
      return;
    if (this.UpDown.Focused)
      this.UnitsCombo.Focus();
    if (e.KeyCode == Keys.Escape)
      this.DialogResult = DialogResult.Cancel;
    this.Close();
  }

  private void DurationForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    int dialogResult = (int) this.DialogResult;
    this.Dispose();
    if (dialogResult == 2 || !(this.Control is DataGridViewTextBoxEditingControl control) || control.EditingControlDataGridView == null)
      return;
    control.EditingControlDataGridView.EndEdit();
  }

  [NotNull]
  public static DurationForm ShowUnder([NotNull] Control c)
  {
    DurationForm durationForm = new DurationForm();
    if (c.Parent != null)
    {
      Point location = c.Location;
      location.Y += c.Height + 1;
      location.X -= 4;
      Point screen = c.Parent.PointToScreen(location);
      durationForm.Location = screen;
    }
    durationForm.Control = c;
    durationForm.Value = c.Text ?? string.Empty;
    durationForm.Show();
    return durationForm;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DurationForm));
    this._upDown = new NumericUpDown();
    this._unitsCombo = new ComboBox();
    this._panel1 = new Panel();
    this._estimationCheckBox = new CheckBox();
    this._upDown.BeginInit();
    this._panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._upDown, "_upDown");
    this._upDown.Name = "_upDown";
    this._upDown.ValueChanged += new EventHandler(this.UpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this._unitsCombo, "_unitsCombo");
    this._unitsCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._unitsCombo.FormattingEnabled = true;
    this._unitsCombo.Name = "_unitsCombo";
    this._unitsCombo.SelectedIndexChanged += new EventHandler(this.UpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this._panel1, "_panel1");
    this._panel1.BackColor = SystemColors.Window;
    this._panel1.Controls.Add((Control) this._estimationCheckBox);
    this._panel1.Controls.Add((Control) this._upDown);
    this._panel1.Controls.Add((Control) this._unitsCombo);
    this._panel1.Name = "_panel1";
    componentResourceManager.ApplyResources((object) this._estimationCheckBox, "_estimationCheckBox");
    this._estimationCheckBox.ImageKey = Resources.False;
    this._estimationCheckBox.Name = "_estimationCheckBox";
    this._estimationCheckBox.UseVisualStyleBackColor = true;
    this._estimationCheckBox.CheckedChanged += new EventHandler(this.UpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ControlDark;
    this.Controls.Add((Control) this._panel1);
    this.FormBorderStyle = FormBorderStyle.None;
    this.KeyPreview = true;
    this.Name = nameof (DurationForm);
    this.ShowInTaskbar = false;
    this.TopMost = true;
    this.Deactivate += new EventHandler(this.DurationForm_Deactivate);
    this.FormClosed += new FormClosedEventHandler(this.DurationForm_FormClosed);
    this.KeyDown += new KeyEventHandler(this.DurationForm_KeyDown);
    this._upDown.EndInit();
    this._panel1.ResumeLayout(false);
    this._panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
