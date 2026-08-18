
// Type: Intermech.Navigator.Conditions.OneValueControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal sealed class OneValueControl : ValueControl
{
  private IEditControl _control;
  private bool _selfChecked;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pControl;
  private Label label1;
  private CheckBox checkBox1;
  private CheckBox checkBox2;

  public OneValueControl(IConditionDataProvider dataProvider)
    : base(dataProvider)
  {
    this.InitializeComponent();
  }

  public override void Initialize(
    int attributeID,
    SelectionParameterTypes paramType,
    ShowValueMode valueMode,
    Dictionary<object, string> pValues,
    ConditionStructure conditionStructure,
    int[] objectTypeIDs,
    object tag)
  {
    base.Initialize(attributeID, paramType, valueMode, pValues, conditionStructure, objectTypeIDs, tag);
    if (this.labelsForControl != null && !string.IsNullOrEmpty(this.labelsForControl.Label1))
      this.label1.Text = this.labelsForControl.Label1;
    this._control = this.GetControl(paramType, attributeID, objectTypeIDs, valueMode, pValues, conditionStructure.Value, true, conditionStructure.RelationalOperator);
    this.pControl.Controls.Add(this._control.Control);
    this.checkBox2.Visible = false;
    if (valueMode == ShowValueMode.svmString)
    {
      this.checkBox1.Visible = true;
      this.checkBox1.Text = LocalizationHolder.rm.GetString("Client.Core_1498");
      this.checkBox1.Checked = conditionStructure.CaseSensitive;
      this.checkBox1.CheckedChanged += new EventHandler(this.Register_CheckedChanged);
      this.Register_CheckedChanged((object) this.checkBox1, new EventArgs());
    }
    else if (paramType == SelectionParameterTypes.sptDate && valueMode == ShowValueMode.svmDate)
    {
      bool flag = true;
      if (this.tag != null && this.tag is AdditionalDateTimeControlParameters)
        flag = ((AdditionalDateTimeControlParameters) this.tag).CurrentDateEnable;
      if (flag)
      {
        this.checkBox1.Visible = true;
        this.checkBox1.Text = LocalizationHolder.rm.GetString("Client.Core_1499");
        this.checkBox1.Checked = conditionStructure.Value is string && (string) conditionStructure.Value == Intermech.Consts.CurrentDateFunction;
        this.checkBox1.CheckedChanged += new EventHandler(this.DateNow_CheckedChanged);
        this.DateNow_CheckedChanged((object) this.checkBox1, new EventArgs());
      }
      else
        this.checkBox1.Visible = false;
    }
    else if (paramType == SelectionParameterTypes.sptUser || paramType == SelectionParameterTypes.sptCheckOutBy)
    {
      this.checkBox1.Visible = true;
      this.checkBox1.Text = LocalizationHolder.rm.GetString("Client.Core_CurrentUser");
      this.checkBox1.Checked = conditionStructure.Value is string && (string) conditionStructure.Value == Intermech.Consts.CurrentUserFunction;
      this.checkBox1.CheckedChanged += new EventHandler(this.CurrentUser_CheckedChanged);
      this.CurrentUser_CheckedChanged((object) this.checkBox1, new EventArgs());
      if (paramType == SelectionParameterTypes.sptCheckOutBy)
      {
        this.checkBox2.Visible = true;
        this.checkBox2.Text = LocalizationHolder.rm.GetString("Interfaces_113");
        this.checkBox2.Checked = conditionStructure.Value is long && (long) conditionStructure.Value == 0L;
        this.checkBox2.CheckedChanged += new EventHandler(this.NothingCheckOut_CheckedChanged);
        this.NothingCheckOut_CheckedChanged((object) this.checkBox2, new EventArgs());
      }
      if (this.checkBox1.Checked || this.checkBox2.Checked)
        this._control.Value = (object) null;
    }
    else
      this.checkBox1.Visible = false;
    if (!this.checkBox1.Checked && !this.checkBox2.Checked)
      this.value1 = this._control.Value;
    this.value2 = (object) null;
    this.OnValueChanged();
  }

  private void NothingCheckOut_CheckedChanged(object sender, EventArgs e)
  {
    if (this._selfChecked)
      return;
    if (((CheckBox) sender).Checked)
    {
      this.value1 = (object) 0L;
      this._control.Control.Enabled = false;
      this._selfChecked = true;
      try
      {
        this.checkBox1.Checked = !((CheckBox) sender).Checked;
      }
      finally
      {
        this._selfChecked = false;
      }
    }
    else if (!this.checkBox1.Checked)
    {
      this.value1 = this._control.Value;
      this._control.Control.Enabled = true;
    }
    this.OnValueChanged();
  }

  private void CurrentUser_CheckedChanged(object sender, EventArgs eventArgs)
  {
    if (this._selfChecked)
      return;
    if (((CheckBox) sender).Checked)
    {
      this.value1 = (object) Intermech.Consts.CurrentUserFunction;
      this._control.Control.Enabled = false;
      if (this.checkBox2.Visible)
      {
        this._selfChecked = true;
        try
        {
          this.checkBox2.Checked = !((CheckBox) sender).Checked;
        }
        finally
        {
          this._selfChecked = false;
        }
      }
    }
    else if (!this.checkBox2.Checked)
    {
      this.value1 = this._control.Value;
      this._control.Control.Enabled = true;
    }
    this.OnValueChanged();
  }

  private void Register_CheckedChanged(object sender, EventArgs e)
  {
    this.OnCaseSensitiveChanged(((CheckBox) sender).Checked);
  }

  private void DateNow_CheckedChanged(object sender, EventArgs e)
  {
    if (((CheckBox) sender).Checked)
    {
      this.value1 = (object) Intermech.Consts.CurrentDateFunction;
      this._control.Control.Enabled = false;
    }
    else
    {
      this.value1 = this._control.Value;
      this._control.Control.Enabled = true;
    }
    this.OnValueChanged();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OneValueControl));
    this.pControl = new Panel();
    this.label1 = new Label();
    this.checkBox1 = new CheckBox();
    this.checkBox2 = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pControl, "pControl");
    this.pControl.Name = "pControl";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.checkBox2, "checkBox2");
    this.checkBox2.Name = "checkBox2";
    this.checkBox2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.checkBox2);
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.pControl);
    this.Controls.Add((Control) this.label1);
    this.Name = nameof (OneValueControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
