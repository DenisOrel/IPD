// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.ConditionSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class ConditionSettingPageControl : UserControl
{
  private ActivitySettings _settings;
  private string _initialConditionText;
  private bool _readOnly;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox CondGroupBox;
  private ButtonEdit ConditionBox;
  private Button ValidateButton;
  private CheckBox useExpertSystemCheckBox;

  public ConditionSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!value)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, (value ? 1 : 0) != 0, new List<Control>((IEnumerable<Control>) new Control[1]
      {
        (Control) this.ValidateButton
      }));
    }
  }

  public bool LoadConditionSettingPageControl(ActivitySettings settings, IDBObject activityObject)
  {
    bool flag1 = false;
    this._settings = settings;
    if (settings.ActivityType == wfConsts.CondTypeID)
    {
      IDBAttribute byId1 = activityObject.Attributes.FindByID(wfConsts.AttrConditionID);
      bool flag2 = false;
      if (byId1 != null)
      {
        settings.ExpertCondition = MiscFunx.FormulaFromAttribute(byId1);
        this.useExpertSystemCheckBox.CheckedChanged -= new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
        this.useExpertSystemCheckBox.Checked = settings.ExtProperties.Ini.ReadBoolean("Props", "useExpertSystem", settings.ExpertCondition != null);
        this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
        if (this.useExpertSystemCheckBox.Checked)
        {
          if (settings.ExpertCondition == null)
          {
            settings.ExpertCondition = new TempFormula();
            settings.ExpertCondition.Init();
          }
          this._initialConditionText = settings.ExpertCondition.ToString();
          flag2 = true;
        }
        else
        {
          IDBAttribute byId2 = activityObject.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
          settings.ExpressionCondition = MiscFunx.GetExpressionFromAttr(byId2);
          this._initialConditionText = settings.ExpressionCondition.ToString();
          flag2 = true;
        }
        this.ConditionBox.Text = this._initialConditionText;
      }
      else
      {
        IDBAttribute byId3 = activityObject.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
        if (byId3 != null)
        {
          settings.ExpressionCondition = MiscFunx.GetExpressionFromAttr(byId3);
          this._initialConditionText = settings.ExpressionCondition.ToString();
          this.ConditionBox.Text = this._initialConditionText;
          flag2 = true;
        }
      }
      if (!flag2)
        flag1 = true;
    }
    else
      flag1 = true;
    return flag1;
  }

  private void ValidateButton_Click(object sender, EventArgs e)
  {
    if (!this.useExpertSystemCheckBox.Checked)
    {
      int num = (int) MessageBox.Show(MiscFunx.VerifyExpressionFormula(this._settings.ExpressionCondition.ToString(), this._settings.ActivityAllAttributeValues.ToArray()), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
      wfFunx.ValidateFormulaDialog(this._settings.ObjectIDwithVars, this._settings.ExpertCondition);
  }

  private void ConditionBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!this.useExpertSystemCheckBox.Checked)
    {
      string exp = this._settings.ExpressionCondition == null ? string.Empty : this._settings.ExpressionCondition.ToString();
      if (this._settings.ExpressionCondition == null)
        this._settings.ExpressionCondition = new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      if (!wfFunx.EditExpression(ref exp, new List<Intermech.Expressions.Variable>(0), this._settings.ActivityExpressionAttributes))
        return;
      this.ConditionBox.Text = exp;
      this._settings.ExpressionCondition.FormulaForLink = exp;
    }
    else
    {
      if (this._settings.ExpertCondition == null)
      {
        this._settings.ExpertCondition = new TempFormula();
        this._settings.ExpertCondition.Init();
      }
      TempFormula expertCondition = this._settings.ExpertCondition;
      if (!wfFunx.EditExpression(ref expertCondition, this._settings.ProcessID))
        return;
      this._settings.ExpertCondition = expertCondition;
      this.ConditionBox.Text = this._settings.ExpertCondition.ToString();
    }
  }

  private void ConditionBox_TextChanged(object sender, EventArgs e)
  {
    this.ValidateButton.Enabled = this.ConditionBox.Text != "";
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    IDBAttribute byId = activityToSave.Attributes.FindByID(wfConsts.AttrConditionID);
    IDBAttribute attr = activityToSave.Attributes.AddAttribute(wfConsts.AttrConditionFormulaID, false);
    if (this.useExpertSystemCheckBox.Checked)
    {
      if (this._settings.ExpertCondition == null)
      {
        this._settings.ExpertCondition = new TempFormula();
        this._settings.ExpertCondition.Init();
      }
      if (!this._settings.ExpertCondition.ToString().Equals(this._initialConditionText) && byId != null)
      {
        modified = true;
        MiscFunx.FormulaToAttribute(this._settings.ExpertCondition, byId);
      }
      attr.Clear();
    }
    else
    {
      if (this._settings.ExpressionCondition == null)
        this._settings.ExpressionCondition = new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      if (!this._settings.ExpressionCondition.ToString().Equals(this._initialConditionText))
      {
        modified = true;
        MiscFunx.ExpressionToAttribute(this._settings.ExpressionCondition, attr);
      }
      byId.Clear();
    }
    this._settings.ExtProperties.Ini.WriteBoolean("Props", "useExpertSystem", this.useExpertSystemCheckBox.Checked);
    return modified;
  }

  private void useExpertSystemCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (MessageBox.Show($"Внимание! {(this.useExpertSystemCheckBox.Checked ? "Включение" : "Отключение")} опции приведёт к удалению уже созданных формул. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      this._settings.ExpertCondition = (TempFormula) null;
      this._settings.ExpressionCondition = (ExpressionInfo) null;
      this.ConditionBox.Text = string.Empty;
      this._initialConditionText = " ";
    }
    else
    {
      this.useExpertSystemCheckBox.CheckedChanged -= new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
      this.useExpertSystemCheckBox.Checked = !this.useExpertSystemCheckBox.Checked;
      this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
    }
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
    this.CondGroupBox = new GroupBox();
    this.ConditionBox = new ButtonEdit();
    this.ValidateButton = new Button();
    this.useExpertSystemCheckBox = new CheckBox();
    this.CondGroupBox.SuspendLayout();
    this.ConditionBox.Properties.BeginInit();
    this.SuspendLayout();
    this.CondGroupBox.BackColor = Color.Transparent;
    this.CondGroupBox.Controls.Add((Control) this.useExpertSystemCheckBox);
    this.CondGroupBox.Controls.Add((Control) this.ConditionBox);
    this.CondGroupBox.Controls.Add((Control) this.ValidateButton);
    this.CondGroupBox.Dock = DockStyle.Top;
    this.CondGroupBox.Location = new Point(0, 0);
    this.CondGroupBox.Name = "CondGroupBox";
    this.CondGroupBox.Size = new Size(756, (int) sbyte.MaxValue);
    this.CondGroupBox.TabIndex = 1;
    this.CondGroupBox.TabStop = false;
    this.CondGroupBox.Text = "Условие";
    this.ConditionBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.ConditionBox.EditValue = (object) "";
    this.ConditionBox.Location = new Point(10, 28);
    this.ConditionBox.Name = "ConditionBox";
    this.ConditionBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.ConditionBox.Properties.ReadOnly = true;
    this.ConditionBox.Size = new Size(735, 22);
    this.ConditionBox.TabIndex = 4;
    this.ConditionBox.ButtonClick += new ButtonPressedEventHandler(this.ConditionBox_ButtonClick);
    this.ConditionBox.TextChanged += new EventHandler(this.ConditionBox_TextChanged);
    this.ValidateButton.BackColor = SystemColors.Control;
    this.ValidateButton.Enabled = false;
    this.ValidateButton.ImeMode = ImeMode.NoControl;
    this.ValidateButton.Location = new Point(10, 63 /*0x3F*/);
    this.ValidateButton.Name = "ValidateButton";
    this.ValidateButton.Size = new Size((int) sbyte.MaxValue, 26);
    this.ValidateButton.TabIndex = 1;
    this.ValidateButton.Text = "Проверить";
    this.ValidateButton.UseVisualStyleBackColor = true;
    this.ValidateButton.Click += new EventHandler(this.ValidateButton_Click);
    this.useExpertSystemCheckBox.AutoSize = true;
    this.useExpertSystemCheckBox.Dock = DockStyle.Bottom;
    this.useExpertSystemCheckBox.Location = new Point(3, 93);
    this.useExpertSystemCheckBox.Name = "useExpertSystemCheckBox";
    this.useExpertSystemCheckBox.Padding = new Padding(5);
    this.useExpertSystemCheckBox.Size = new Size(750, 31 /*0x1F*/);
    this.useExpertSystemCheckBox.TabIndex = 10;
    this.useExpertSystemCheckBox.Text = "Использовать формулы экспертной системы";
    this.useExpertSystemCheckBox.UseVisualStyleBackColor = true;
    this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.CondGroupBox);
    this.Name = nameof (ConditionSettingPageControl);
    this.Size = new Size(756, 130);
    this.CondGroupBox.ResumeLayout(false);
    this.CondGroupBox.PerformLayout();
    this.ConditionBox.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
