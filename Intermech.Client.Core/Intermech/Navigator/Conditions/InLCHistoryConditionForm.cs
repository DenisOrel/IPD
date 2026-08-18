
// Type: Intermech.Navigator.Conditions.InLCHistoryConditionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Navigator.Conditions;

public class InLCHistoryConditionForm : ConditionForm
{
  private LC_ConditionParams _params;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private TextBoxButton tbbLevel;
  private Label label2;
  private Panel panel2;
  private Panel panel1;
  private TextBoxButton tbbStep;
  private Label label1;
  private SplitContainer splitContainer1;
  private GroupBox groupBox1;
  private RelationalOperatorsList tvRelationalOperators;
  private GroupBox groupBox2;
  private Panel pValue;

  public InLCHistoryConditionForm() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    this._params = this.conditionStructure.Value != null ? (LC_ConditionParams) this.conditionStructure.Value : new LC_ConditionParams(new int?(), new int?(), 0);
    this.tvRelationalOperators.Initialize(SelectionParameter.InLCHistoryRelationalOperators, this._params.DateOperator);
    this.RefreshControls();
  }

  public override ConditionStructure Result
  {
    get
    {
      this.conditionStructure.RelationalOperator = RelationalOperators.InLCHistory;
      this.conditionStructure.Value = (object) this._params;
      return this.conditionStructure;
    }
  }

  private void SetOKButton()
  {
    this.bOK.Enabled = (this._params.LCStepID.HasValue || this._params.LevelID.HasValue) && (this._params.LastNDays > 0 || this._params.BeginDate != DateTime.MinValue);
  }

  private RelationalOperators SelectedOperator
  {
    get => (RelationalOperators) this.tvRelationalOperators.SelectedNode.Tag;
  }

  private void RelationalOperators_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.ClearPanelValue();
    RelationalOperators tag = (RelationalOperators) e.Node.Tag;
    RelationOperatorValueMode operatorValueMode = ControlsHelper.GetRelationOperatorValueMode(tag);
    if (operatorValueMode == RelationOperatorValueMode.rovmNone)
      return;
    ShowValueMode valueMode = ControlsHelper.GetValueMode(operatorValueMode, SelectionParameterTypes.sptDate, tag, false);
    if (valueMode == ShowValueMode.svmNone)
      return;
    UserControl userControl = (UserControl) null;
    switch (operatorValueMode)
    {
      case RelationOperatorValueMode.rovmOne:
        userControl = (UserControl) new OneValueControl(this.dataProvider);
        break;
      case RelationOperatorValueMode.rovmTwo:
        userControl = (UserControl) new TwoValueControl(this.dataProvider);
        break;
      case RelationOperatorValueMode.rovmMulti:
        userControl = (UserControl) new MultiValueControl(this.dataProvider);
        break;
    }
    this._params.DateOperator = this.SelectedOperator;
    object conditionValue = this._params.DateOperator == RelationalOperators.LastNDays ? (object) this._params.LastNDays : (object) this._params.BeginDate;
    object conditionValue2 = (object) null;
    if (this._params.DateOperator == RelationalOperators.Between)
      conditionValue2 = (object) (this._params.EndDate.HasValue ? this._params.EndDate : new DateTime?(DateTime.Today));
    ((IValueControl) userControl).ValuesChangedEvent += new ValuesChangedEventHandler(this.ValuesChangedEvent);
    ((IValueControl) userControl).Initialize(0, SelectionParameterTypes.sptDate, valueMode, (Dictionary<object, string>) null, new ConditionStructure(0, RelationalOperators.None, conditionValue, conditionValue2, LogicalOperators.NONE, 0, false), (int[]) null, (object) new AdditionalDateTimeControlParameters(DateTimePickerFormat.Short, (string) null, false));
    this.pValue.Controls.Add((Control) userControl);
    userControl.Dock = DockStyle.Fill;
  }

  private void ClearPanelValue()
  {
    if (this.pValue.Controls.Count == 0)
      return;
    ((IValueControl) this.pValue.Controls[0]).ValuesChangedEvent -= new ValuesChangedEventHandler(this.ValuesChangedEvent);
    foreach (Component control in (ArrangedElementCollection) this.pValue.Controls)
      control.Dispose();
  }

  private void ValuesChangedEvent(object sender, ValuesChangedEventArgs e)
  {
    if (this.SelectedOperator == RelationalOperators.LastNDays)
    {
      this._params.BeginDate = DateTime.Today;
      this._params.EndDate = new DateTime?();
      this._params.LastNDays = e.Value1 == null || e.Value1 is DateTime ? 0 : Convert.ToInt32(e.Value1);
    }
    else
    {
      this._params.BeginDate = e.Value1 == null || !(e.Value1 is DateTime) ? DateTime.Today : (DateTime) e.Value1;
      if (e.Value2 != null)
        this._params.EndDate = new DateTime?((DateTime) e.Value2);
      this._params.LastNDays = 0;
    }
  }

  private bool Level_OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    object aObject = (object) -1;
    if (ValueRelationSelector.SelectLifecycleLevel(ref aObject))
    {
      int? levelId = this._params.LevelID;
      int num = (int) aObject;
      if (!(levelId.GetValueOrDefault() == num & levelId.HasValue))
      {
        this._params.LevelID = new int?((int) aObject);
        this._params.LCStepID = new int?();
        this.RefreshControls();
        return true;
      }
    }
    return false;
  }

  private bool Step_OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    object aObject = (object) -1;
    if (ValueRelationSelector.SelectLifeCycleStep(ref aObject))
    {
      int? lcStepId = this._params.LCStepID;
      int num = (int) aObject;
      if (!(lcStepId.GetValueOrDefault() == num & lcStepId.HasValue))
      {
        this._params.LCStepID = new int?((int) aObject);
        this._params.LevelID = new int?();
        this.RefreshControls();
        return true;
      }
    }
    return false;
  }

  public void RefreshControls()
  {
    this.tbbStep.SetText(this._params.LCStepID.HasValue ? this.dataProvider.GetLifecycleStepCaption((object) this._params.LCStepID) : string.Empty);
    this.tbbLevel.SetText(this._params.LevelID.HasValue ? this.dataProvider.GetLifecycleLevelCaption((object) this._params.LevelID) : string.Empty);
    this.SetOKButton();
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
    this.bOK = new Button();
    this.bCancel = new Button();
    this.tbbLevel = new TextBoxButton();
    this.label2 = new Label();
    this.panel2 = new Panel();
    this.panel1 = new Panel();
    this.tbbStep = new TextBoxButton();
    this.label1 = new Label();
    this.splitContainer1 = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.tvRelationalOperators = new RelationalOperatorsList();
    this.groupBox2 = new GroupBox();
    this.pValue = new Panel();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(383, 357);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 6;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(510, 357);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 7;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.tbbLevel.AutoSize = true;
    this.tbbLevel.Dock = DockStyle.Fill;
    this.tbbLevel.Location = new Point(0, 0);
    this.tbbLevel.Margin = new Padding(0);
    this.tbbLevel.Name = "tbbLevel";
    this.tbbLevel.Size = new Size(401, 27);
    this.tbbLevel.TabIndex = 1;
    this.tbbLevel.OnOpenDialog += new OnOpenDialogEventHandler(this.Level_OnOpenDialog);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(35, 25);
    this.label2.Name = "label2";
    this.label2.Size = new Size(193, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Переведён на уровень продвижения";
    this.panel2.Controls.Add((Control) this.tbbLevel);
    this.panel2.Location = new Point(38, 41);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(401, 27);
    this.panel2.TabIndex = 12;
    this.panel1.Controls.Add((Control) this.tbbStep);
    this.panel1.Location = new Point(38, 101);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(401, 27);
    this.panel1.TabIndex = 14;
    this.tbbStep.AutoSize = true;
    this.tbbStep.Dock = DockStyle.Fill;
    this.tbbStep.Location = new Point(0, 0);
    this.tbbStep.Margin = new Padding(0);
    this.tbbStep.Name = "tbbStep";
    this.tbbStep.Size = new Size(401, 27);
    this.tbbStep.TabIndex = 1;
    this.tbbStep.OnOpenDialog += new OnOpenDialogEventHandler(this.Step_OnOpenDialog);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(35, 85);
    this.label1.Name = "label1";
    this.label1.Size = new Size(197, 13);
    this.label1.TabIndex = 13;
    this.label1.Text = "Переведён на шаг жизненного цикла";
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(38, 140);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.groupBox2);
    this.splitContainer1.Size = new Size(593, 211);
    this.splitContainer1.SplitterDistance = 213;
    this.splitContainer1.TabIndex = 15;
    this.groupBox1.Controls.Add((Control) this.tvRelationalOperators);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(213, 211);
    this.groupBox1.TabIndex = 9;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Условие";
    this.tvRelationalOperators.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tvRelationalOperators.BackColor = SystemColors.Control;
    this.tvRelationalOperators.BorderStyle = BorderStyle.None;
    this.tvRelationalOperators.FullRowSelect = true;
    this.tvRelationalOperators.Location = new Point(6, 19);
    this.tvRelationalOperators.Name = "tvRelationalOperators";
    this.tvRelationalOperators.ShowLines = false;
    this.tvRelationalOperators.ShowRootLines = false;
    this.tvRelationalOperators.Size = new Size(194, 186);
    this.tvRelationalOperators.TabIndex = 4;
    this.tvRelationalOperators.AfterSelect += new TreeViewEventHandler(this.RelationalOperators_AfterSelect);
    this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.pValue);
    this.groupBox2.Location = new Point(0, 0);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(376, 211);
    this.groupBox2.TabIndex = 5;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Дата перевода на шаг ЖЦ или уровень продвижения";
    this.pValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pValue.Location = new Point(6, 19);
    this.pValue.Name = "pValue";
    this.pValue.Size = new Size(364, 186);
    this.pValue.TabIndex = 0;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(649, 396);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.MinimumSize = new Size(665, 435);
    this.Name = nameof (InLCHistoryConditionForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Поиск по истории изменения шагов жизненного цикла объектов";
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
