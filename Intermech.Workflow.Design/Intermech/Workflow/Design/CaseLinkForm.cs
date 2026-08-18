// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CaseLinkForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for CaseLinkForm.</summary>
public class CaseLinkForm : Form
{
  private Button CancButton;
  private Button OkButton;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private TempFormula _expertFormula;
  private ExpressionInfo _expressionInfo;
  private GroupBox groupBox1;
  private RadioButton ElseRadioButton;
  private Panel CondPanel;
  private RadioButton CondRadioButton;
  private ButtonEdit ConditionBox;
  private Button ValidateButton;
  private long _processID;
  private List<Intermech.Expressions.Variable> _variables;
  private List<Intermech.Expressions.Variable> _activityVariable;
  protected bool ShowExtendedAttributes;

  public CaseLinkForm(long procID)
  {
    this._processID = procID;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1281);
  }

  public TempFormula ExpertFormula
  {
    get => this._expertFormula;
    set
    {
      this._expertFormula = value;
      if (this._expertFormula != null)
        this.ConditionBox.Text = this._expertFormula.ToString();
      else
        this.ElseRadioButton.Checked = true;
    }
  }

  public ExpressionInfo ExpressionInfo
  {
    get => this._expressionInfo;
    set
    {
      this._expressionInfo = value;
      if (this._expressionInfo != null)
        this.ConditionBox.Text = this._expressionInfo.FormulaForLink;
      else
        this.ElseRadioButton.Checked = true;
    }
  }

  public List<Intermech.Expressions.Variable> Variables
  {
    get => this._variables;
    set => this._variables = value;
  }

  public List<Intermech.Expressions.Variable> ActivityVariable
  {
    get => this._activityVariable;
    set => this._activityVariable = value;
  }

  public AttributeValues[] ActivityAttributeValueses { get; set; }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public static LinkKind QueryLinkKind(
    ref TempFormula tf,
    long ProcessID,
    bool ShowExtendedAttributes)
  {
    using (CaseLinkForm caseLinkForm = new CaseLinkForm(ProcessID))
    {
      caseLinkForm.ExpertFormula = tf;
      caseLinkForm.ShowExtendedAttributes = ShowExtendedAttributes;
      if (caseLinkForm.ShowDialog() != DialogResult.OK)
        return LinkKind.Backward;
      if (caseLinkForm.CondRadioButton.Checked)
      {
        tf = caseLinkForm.ExpertFormula;
        return LinkKind.True;
      }
      tf = (TempFormula) null;
      return LinkKind.False;
    }
  }

  public static LinkKind QueryLinkKind(
    ref ExpressionInfo expression,
    List<Intermech.Expressions.Variable> variables,
    List<Intermech.Expressions.Variable> activityVariable,
    AttributeValues[] activityAttributeValueses)
  {
    if (expression == null)
      throw new ArgumentNullException(nameof (expression));
    if (variables == null)
      throw new ArgumentNullException(nameof (variables));
    using (CaseLinkForm caseLinkForm = new CaseLinkForm(-1L))
    {
      caseLinkForm.ExpressionInfo = expression;
      caseLinkForm.ShowExtendedAttributes = false;
      caseLinkForm.ActivityVariable = activityVariable;
      caseLinkForm.Variables = variables;
      caseLinkForm.ActivityAttributeValueses = activityAttributeValueses;
      if (caseLinkForm.ShowDialog() != DialogResult.OK)
        return LinkKind.Backward;
      if (caseLinkForm.CondRadioButton.Checked)
      {
        caseLinkForm.ExpressionInfo.ElseLink = false;
        expression = caseLinkForm.ExpressionInfo;
        return LinkKind.True;
      }
      caseLinkForm.ExpressionInfo.ElseLink = true;
      expression = caseLinkForm.ExpressionInfo;
      return LinkKind.False;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CaseLinkForm));
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.groupBox1 = new GroupBox();
    this.ElseRadioButton = new RadioButton();
    this.CondPanel = new Panel();
    this.ConditionBox = new ButtonEdit();
    this.ValidateButton = new Button();
    this.CondRadioButton = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.CondPanel.SuspendLayout();
    this.ConditionBox.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.groupBox1.Controls.Add((Control) this.ElseRadioButton);
    this.groupBox1.Controls.Add((Control) this.CondPanel);
    this.groupBox1.Controls.Add((Control) this.CondRadioButton);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ElseRadioButton, "ElseRadioButton");
    this.ElseRadioButton.Name = "ElseRadioButton";
    this.ElseRadioButton.UseVisualStyleBackColor = true;
    this.ElseRadioButton.CheckedChanged += new EventHandler(this.CondRadioButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.CondPanel, "CondPanel");
    this.CondPanel.Controls.Add((Control) this.ConditionBox);
    this.CondPanel.Controls.Add((Control) this.ValidateButton);
    this.CondPanel.Name = "CondPanel";
    componentResourceManager.ApplyResources((object) this.ConditionBox, "ConditionBox");
    this.ConditionBox.Name = "ConditionBox";
    this.ConditionBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.ConditionBox.Properties.ReadOnly = true;
    this.ConditionBox.ButtonClick += new ButtonPressedEventHandler(this.ConditionBox_ButtonClick);
    componentResourceManager.ApplyResources((object) this.ValidateButton, "ValidateButton");
    this.ValidateButton.Name = "ValidateButton";
    this.ValidateButton.Click += new EventHandler(this.ValidateButton_Click);
    componentResourceManager.ApplyResources((object) this.CondRadioButton, "CondRadioButton");
    this.CondRadioButton.Checked = true;
    this.CondRadioButton.Name = "CondRadioButton";
    this.CondRadioButton.TabStop = true;
    this.CondRadioButton.UseVisualStyleBackColor = true;
    this.CondRadioButton.CheckedChanged += new EventHandler(this.CondRadioButton_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.CancButton);
    this.Controls.Add((Control) this.OkButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CaseLinkForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.CondPanel.ResumeLayout(false);
    this.ConditionBox.Properties.EndInit();
    this.ResumeLayout(false);
  }

  private void ConditionBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this._expertFormula == null)
    {
      string formulaForLink = this.ExpressionInfo.FormulaForLink;
      if (!wfFunx.EditExpression(ref formulaForLink, this.Variables, this.ActivityVariable))
        return;
      this.ConditionBox.Text = formulaForLink;
      this.ExpressionInfo.FormulaForLink = formulaForLink;
    }
    else
    {
      if (!wfFunx.EditExpression(ref this._expertFormula, this._processID, this.ShowExtendedAttributes))
        return;
      this.ConditionBox.Text = this.ExpertFormula.ToString();
    }
  }

  private void ValidateButton_Click(object sender, EventArgs e)
  {
    if (this._expertFormula == null)
    {
      int num = (int) MessageBox.Show(MiscFunx.VerifyExpressionFormula(this.ExpressionInfo.FormulaForLink, this.ActivityAttributeValueses), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
      wfFunx.ValidateFormulaDialog(this._processID, this.ExpertFormula);
  }

  private void CondRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    this.CondPanel.Enabled = this.CondRadioButton.Checked;
  }
}
