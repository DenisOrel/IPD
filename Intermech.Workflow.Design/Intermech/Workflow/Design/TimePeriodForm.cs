// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.TimePeriodForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for TimePeriodForm.</summary>
public class TimePeriodForm : FormEx
{
  private GroupBox mainTimerGroupBox;
  private Panel timer1GroupBox;
  private ComboBox unitsComboBox;
  private Panel timer2GroupBox;
  private Label label9;
  private ComboBox dateComboBox;
  private Panel timPanel;
  private RadioButton varsRadioButton;
  private RadioButton periodRadioButton;
  private Label SeparatorLabel;
  private Panel BottomPanel;
  private Button CancButton;
  private Button OkButton;
  public TimeUnits Units;
  public int UnitsCount;
  private NumericUpDown UnitsEdit;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private RadioButton notSetRadioButton;
  private long _processID;
  private VarList _vars;

  public TimePeriodForm(long processID)
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.unitsComboBox.Items.Clear();
    foreach (TimeUnits timeUnits in Enum.GetValues(typeof (TimeUnits)))
      this.unitsComboBox.Items.Add((object) SimpleFuncs.GetEnumDescription((Enum) timeUnits));
    if (this.unitsComboBox.Items.Count > 0)
      this.unitsComboBox.SelectedIndex = 0;
    this._processID = processID;
    this.FillVariables();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TimePeriodForm));
    this.mainTimerGroupBox = new GroupBox();
    this.timer2GroupBox = new Panel();
    this.label9 = new Label();
    this.dateComboBox = new ComboBox();
    this.timer1GroupBox = new Panel();
    this.unitsComboBox = new ComboBox();
    this.UnitsEdit = new NumericUpDown();
    this.timPanel = new Panel();
    this.SeparatorLabel = new Label();
    this.varsRadioButton = new RadioButton();
    this.periodRadioButton = new RadioButton();
    this.notSetRadioButton = new RadioButton();
    this.BottomPanel = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.mainTimerGroupBox.SuspendLayout();
    this.timer2GroupBox.SuspendLayout();
    this.timer1GroupBox.SuspendLayout();
    this.UnitsEdit.BeginInit();
    this.timPanel.SuspendLayout();
    this.BottomPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.mainTimerGroupBox, "mainTimerGroupBox");
    this.mainTimerGroupBox.BackColor = Color.Transparent;
    this.mainTimerGroupBox.Controls.Add((Control) this.timer2GroupBox);
    this.mainTimerGroupBox.Controls.Add((Control) this.timer1GroupBox);
    this.mainTimerGroupBox.Controls.Add((Control) this.timPanel);
    this.mainTimerGroupBox.Name = "mainTimerGroupBox";
    this.mainTimerGroupBox.TabStop = false;
    this.timer2GroupBox.Controls.Add((Control) this.label9);
    this.timer2GroupBox.Controls.Add((Control) this.dateComboBox);
    componentResourceManager.ApplyResources((object) this.timer2GroupBox, "timer2GroupBox");
    this.timer2GroupBox.Name = "timer2GroupBox";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.dateComboBox, "dateComboBox");
    this.dateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.dateComboBox.Name = "dateComboBox";
    this.timer1GroupBox.Controls.Add((Control) this.unitsComboBox);
    this.timer1GroupBox.Controls.Add((Control) this.UnitsEdit);
    componentResourceManager.ApplyResources((object) this.timer1GroupBox, "timer1GroupBox");
    this.timer1GroupBox.Name = "timer1GroupBox";
    componentResourceManager.ApplyResources((object) this.unitsComboBox, "unitsComboBox");
    this.unitsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.unitsComboBox.Name = "unitsComboBox";
    componentResourceManager.ApplyResources((object) this.UnitsEdit, "UnitsEdit");
    this.UnitsEdit.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.UnitsEdit.Name = "UnitsEdit";
    this.UnitsEdit.Value = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.timPanel, "timPanel");
    this.timPanel.Controls.Add((Control) this.SeparatorLabel);
    this.timPanel.Controls.Add((Control) this.varsRadioButton);
    this.timPanel.Controls.Add((Control) this.periodRadioButton);
    this.timPanel.Controls.Add((Control) this.notSetRadioButton);
    this.timPanel.Name = "timPanel";
    this.SeparatorLabel.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.SeparatorLabel, "SeparatorLabel");
    this.SeparatorLabel.Name = "SeparatorLabel";
    componentResourceManager.ApplyResources((object) this.varsRadioButton, "varsRadioButton");
    this.varsRadioButton.Name = "varsRadioButton";
    this.varsRadioButton.Tag = (object) "2";
    this.varsRadioButton.Click += new EventHandler(this.RadioButtonsClick);
    componentResourceManager.ApplyResources((object) this.periodRadioButton, "periodRadioButton");
    this.periodRadioButton.Checked = true;
    this.periodRadioButton.Name = "periodRadioButton";
    this.periodRadioButton.TabStop = true;
    this.periodRadioButton.Tag = (object) "1";
    this.periodRadioButton.Click += new EventHandler(this.RadioButtonsClick);
    componentResourceManager.ApplyResources((object) this.notSetRadioButton, "notSetRadioButton");
    this.notSetRadioButton.Name = "notSetRadioButton";
    this.notSetRadioButton.Tag = (object) "0";
    this.notSetRadioButton.Click += new EventHandler(this.RadioButtonsClick);
    this.BottomPanel.Controls.Add((Control) this.CancButton);
    this.BottomPanel.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.BottomPanel, "BottomPanel");
    this.BottomPanel.Name = "BottomPanel";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.BottomPanel);
    this.Controls.Add((Control) this.mainTimerGroupBox);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.KeyPreview = true;
    this.Name = nameof (TimePeriodForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.VisibleChanged += new EventHandler(this.TimePeriodForm_VisibleChanged);
    this.mainTimerGroupBox.ResumeLayout(false);
    this.mainTimerGroupBox.PerformLayout();
    this.timer2GroupBox.ResumeLayout(false);
    this.timer2GroupBox.PerformLayout();
    this.timer1GroupBox.ResumeLayout(false);
    this.UnitsEdit.EndInit();
    this.timPanel.ResumeLayout(false);
    this.timPanel.PerformLayout();
    this.BottomPanel.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void RadioButtonsClick(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32((sender as Control).Tag);
    this.SeparatorLabel.Visible = int32 != 0;
    this.timer2GroupBox.Visible = int32 == 2;
    this.timer1GroupBox.Visible = int32 == 1;
    this.UpdateHeight();
  }

  public bool Embedded
  {
    get => !this.TopLevel;
    set
    {
      this.BottomPanel.Visible = !value;
      if (value)
      {
        this.BackColor = Color.Transparent;
        this.FormBorderStyle = FormBorderStyle.None;
        this.UpdateHeight();
        this.CancelButton = (IButtonControl) null;
      }
      this.TopLevel = !value;
    }
  }

  private void FillVariables()
  {
    if (this._vars != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._vars = new VarList(sessionKeeper.Session.GetObject(this._processID), false, false);
    this.dateComboBox.Items.Clear();
    this.dateComboBox.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_91"));
    List<Variable> variableList = new List<Variable>();
    foreach (Variable var in this._vars)
    {
      if (var.VarType == VarType.DateTime)
        variableList.Add(var);
    }
    variableList.Sort();
    this.dateComboBox.Items.AddRange((object[]) variableList.ToArray());
    this.dateComboBox.SelectedIndex = 0;
  }

  public void SetPeriodInformation(PeriodInformation pi)
  {
    if (pi == null)
    {
      this.notSetRadioButton.Checked = true;
      this.RadioButtonsClick((object) this.notSetRadioButton, (EventArgs) null);
    }
    else
    {
      this.Units = pi.Units;
      this.UnitsCount = pi.UnitsCount;
      if ((Decimal) this.UnitsCount < this.UnitsEdit.Minimum)
        this.UnitsCount = Convert.ToInt32(this.UnitsEdit.Minimum);
      if ((Decimal) this.UnitsCount > this.UnitsEdit.Maximum)
        this.UnitsCount = Convert.ToInt32(this.UnitsEdit.Maximum);
      this.UnitsEdit.Value = (Decimal) this.UnitsCount;
      this.unitsComboBox.SelectedIndex = (int) this.Units;
      if (pi.VarTypeID != 0)
      {
        foreach (object obj in this.dateComboBox.Items)
        {
          if (obj is Variable variable && variable.AttrTypeID == pi.VarTypeID)
          {
            this.dateComboBox.SelectedItem = (object) variable;
            this.varsRadioButton.Checked = true;
            this.RadioButtonsClick((object) this.varsRadioButton, (EventArgs) null);
            break;
          }
        }
      }
      this.periodRadioButton.Checked = !this.varsRadioButton.Checked;
      if (this.periodRadioButton.Checked)
        this.RadioButtonsClick((object) this.periodRadioButton, (EventArgs) null);
    }
    if (!this.Embedded)
      return;
    this.ClientSize = this.mainTimerGroupBox.Size;
  }

  public void FillPeriodInformation(ref PeriodInformation pi, IUserSession session)
  {
    this.timer2GroupBox.Top = 0;
    if (this.notSetRadioButton.Checked)
    {
      pi = (PeriodInformation) null;
    }
    else
    {
      this.UnitsCount = Convert.ToInt32(this.UnitsEdit.Value);
      this.Units = (TimeUnits) this.unitsComboBox.SelectedIndex;
      if (pi == null)
        pi = new PeriodInformation(session);
      pi.Units = this.Units;
      pi.UnitsCount = this.UnitsCount;
      if (this.varsRadioButton.Checked && this.dateComboBox.SelectedIndex != -1 && this.dateComboBox.Items[this.dateComboBox.SelectedIndex] is Variable)
        pi.VarTypeID = (this.dateComboBox.Items[this.dateComboBox.SelectedIndex] as Variable).AttrTypeID;
      else
        pi.VarTypeID = 0;
    }
  }

  public bool EditPeriod(ref PeriodInformation pi, IUserSession session)
  {
    this.SetPeriodInformation(pi);
    int num = this.ShowDialog() == DialogResult.OK ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.FillPeriodInformation(ref pi, session);
    return num != 0;
  }

  internal static bool Edit(long processid, Term term)
  {
    using (TimePeriodForm timePeriodForm = new TimePeriodForm(processid))
    {
      timePeriodForm.CanResetPeriod = true;
      PeriodInformation period = term.Period;
      int num = timePeriodForm.EditPeriod(ref period, (IUserSession) null) ? 1 : 0;
      if (num != 0)
        term.Period = period;
      return num != 0;
    }
  }

  public bool CanResetPeriod
  {
    get => this.notSetRadioButton.Visible;
    set
    {
      this.notSetRadioButton.Visible = value;
      this.notSetRadioButton.Checked = value;
      this.UpdateHeight();
    }
  }

  protected void UpdateHeight()
  {
    Padding padding = this.Padding;
    int num1 = padding.Top + this.mainTimerGroupBox.Height;
    padding = this.Padding;
    int bottom = padding.Bottom;
    int num2 = num1 + bottom;
    if (this.FormBorderStyle != FormBorderStyle.None)
      num2 += SystemInformation.CaptionHeight;
    if (this.BottomPanel.Visible)
      num2 += this.BottomPanel.Height;
    this.Height = num2;
  }

  private void TimePeriodForm_VisibleChanged(object sender, EventArgs e)
  {
    if (!this.Visible)
      return;
    this.UpdateHeight();
  }
}
