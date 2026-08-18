
// Type: Intermech.Navigator.Conditions.TwoValueControl
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

internal sealed class TwoValueControl : ValueControl
{
  private IEditControl _control1;
  private IEditControl _control2;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pControl1;
  private Label label1;
  private Panel pControl2;
  private Label label2;
  private CheckBox checkBox1;
  private CheckBox checkBox2;

  public TwoValueControl(IConditionDataProvider dataProvider)
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
    this._control1 = this.GetControl(paramType, attributeID, objectTypeIDs, valueMode, pValues, conditionStructure.Value, true, conditionStructure.RelationalOperator);
    this._control2 = this.GetControl(paramType, attributeID, objectTypeIDs, valueMode, pValues, conditionStructure.Value2, false, conditionStructure.RelationalOperator);
    this.pControl1.Controls.Add(this._control1.Control);
    this.pControl2.Controls.Add(this._control2.Control);
    if (valueMode == ShowValueMode.svmString)
    {
      this.checkBox1.Visible = false;
      this.checkBox2.Visible = true;
      this.checkBox2.Text = LocalizationHolder.rm.GetString("Client.Core_1498");
      this.checkBox2.Checked = conditionStructure.CaseSensitive;
      this.checkBox2.CheckedChanged += new EventHandler(this.Register_CheckedChanged);
      this.Register_CheckedChanged((object) this.checkBox2, new EventArgs());
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
        this.checkBox1.CheckedChanged += new EventHandler(this.DateNow_CheckedChanged1);
        this.DateNow_CheckedChanged1((object) this.checkBox1, new EventArgs());
        this.checkBox2.Visible = true;
        this.checkBox2.Text = LocalizationHolder.rm.GetString("Client.Core_1499");
        this.checkBox2.Checked = conditionStructure.Value2 is string && (string) conditionStructure.Value2 == Intermech.Consts.CurrentDateFunction;
        this.checkBox2.CheckedChanged += new EventHandler(this.DateNow_CheckedChanged2);
        this.DateNow_CheckedChanged2((object) this.checkBox2, new EventArgs());
      }
      else
      {
        this.checkBox1.Visible = false;
        this.checkBox2.Visible = false;
      }
    }
    else
    {
      this.checkBox1.Visible = false;
      this.checkBox2.Visible = false;
    }
    this.OnValueChanged();
  }

  private void Register_CheckedChanged(object sender, EventArgs e)
  {
    this.OnCaseSensitiveChanged(((CheckBox) sender).Checked);
  }

  private void DateNow_CheckedChanged1(object sender, EventArgs e)
  {
    if (((CheckBox) sender).Checked)
    {
      this.value1 = (object) Intermech.Consts.CurrentDateFunction;
      this._control1.Control.Enabled = false;
    }
    else
    {
      this.value1 = this._control1.Value;
      this._control1.Control.Enabled = true;
    }
    this.OnValueChanged();
  }

  private void DateNow_CheckedChanged2(object sender, EventArgs e)
  {
    if (((CheckBox) sender).Checked)
    {
      this.value2 = (object) Intermech.Consts.CurrentDateFunction;
      this._control2.Control.Enabled = false;
    }
    else
    {
      this.value2 = this._control2.Value;
      this._control2.Control.Enabled = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TwoValueControl));
    this.pControl1 = new Panel();
    this.label1 = new Label();
    this.pControl2 = new Panel();
    this.label2 = new Label();
    this.checkBox1 = new CheckBox();
    this.checkBox2 = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pControl1, "pControl1");
    this.pControl1.Name = "pControl1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.pControl2, "pControl2");
    this.pControl2.Name = "pControl2";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
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
    this.Controls.Add((Control) this.pControl2);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.pControl1);
    this.Controls.Add((Control) this.label1);
    this.Name = nameof (TwoValueControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
