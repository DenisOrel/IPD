
// Type: Intermech.Navigator.Conditions.InputOperatorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal class InputOperatorForm : ConditionForm
{
  private object _value1;
  private object _typeID;
  private ConditionStructure[] _nestedConditions;
  private RelationalOperators _previousRelationalOperator = RelationalOperators.None;
  private bool _selfChecked;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private GroupBox groupBox1;
  private RelationalOperatorsList tvRelationalOperators;
  private SplitContainer splitContainer1;
  private CheckBox checkBox1;
  private TextBoxButton tbbRelationType;
  private Button bInnerCondition;
  private Label label2;
  private GroupBox groupBox2;
  private Panel panel1;

  public InputOperatorForm() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    this.tvRelationalOperators.Initialize(SelectionParameter.GetInRelationOperators(), this.conditionStructure.RelationalOperator);
    this._value1 = this.conditionStructure.Value;
    this._typeID = this.conditionStructure.TypeID;
    this._nestedConditions = this.conditionStructure.NestedConditions;
    if (this._typeID != null)
      this.tbbRelationType.Text = this.dataProvider.GetRelationTypeCaption(this._typeID);
    if (this.conditionStructure.Value is int && (int) this.conditionStructure.Value == -1 || this.conditionStructure.Value is long && (long) this.conditionStructure.Value == -1L)
    {
      this._selfChecked = true;
      try
      {
        this.checkBox1.Checked = true;
      }
      finally
      {
        this._selfChecked = false;
      }
    }
    this.SetControls();
  }

  private void SetControls() => this.bOK.Enabled = this._value1 != null || this.checkBox1.Checked;

  private bool ObjectIsEmpty => this._value1 == null || !this.checkBox1.Checked;

  public override ConditionStructure Result
  {
    get
    {
      return this.MakeConditionStructureForMultiValueControl() with
      {
        TypeID = this._typeID,
        NestedConditions = this._nestedConditions
      };
    }
  }

  private ConditionStructure MakeConditionStructureForMultiValueControl()
  {
    return new ConditionStructure((string) null, (RelationalOperators) this.tvRelationalOperators.SelectedNode.Tag, InputOperatorValueConvertHelper.Convert(this._value1, this.IsObjectInputOperator(this.SelectedOperator)), (object) null, this.conditionStructure.LogicalOperator, this.conditionStructure.GroupID, this.conditionStructure.CaseSensitive);
  }

  private RelationalOperators SelectedOperator
  {
    get => (RelationalOperators) this.tvRelationalOperators.SelectedNode.Tag;
  }

  private bool IsObjectInputOperator(RelationalOperators relationalOperator)
  {
    return relationalOperator == RelationalOperators.EntersIn || relationalOperator == RelationalOperators.ConsistFrom || relationalOperator == RelationalOperators.ExistsInVersionContext;
  }

  private void CheckBox1_CheckedChanged(object sender, EventArgs e)
  {
    this.panel1.Enabled = this.tvRelationalOperators.Enabled = !this.checkBox1.Checked;
    if (this._selfChecked)
      return;
    this.RefreshControlPanel(this.IsObjectInputOperator(this.SelectedOperator));
    this.SetControls();
  }

  private void RefreshControlPanel(bool selectedIsObjectInputOperator)
  {
    if (this.panel1.Controls.Count > 0)
      this.panel1.Controls[0].Dispose();
    this.panel1.Controls.Clear();
    this.groupBox2.Text = selectedIsObjectInputOperator ? "Объект" : "Тип объектов";
    this.bInnerCondition.Enabled = !selectedIsObjectInputOperator;
    if (this._previousRelationalOperator != RelationalOperators.None)
      this._value1 = !this.checkBox1.Checked ? (object) null : (object) -1;
    MultiValueControl multiValueControl = new MultiValueControl(this.dataProvider);
    int[] objectTypeIDs = (int[]) null;
    if (this.SelectedOperator == RelationalOperators.ExistsInVersionContext)
      objectTypeIDs = MetaDataHelper.SpecialContextObjectTypes.ToArray();
    multiValueControl.Initialize(0, selectedIsObjectInputOperator ? SelectionParameterTypes.sptObject : SelectionParameterTypes.sptObjectType, ShowValueMode.svmObj, (Dictionary<object, string>) null, this.MakeConditionStructureForMultiValueControl(), objectTypeIDs, (object) null);
    multiValueControl.ValuesChangedEvent += new ValuesChangedEventHandler(this.ObjectValuesChangedEvent);
    multiValueControl.Dock = DockStyle.Fill;
    this.panel1.Controls.Add((Control) multiValueControl);
  }

  private void RelationalOperators_AfterSelect(object sender, TreeViewEventArgs e)
  {
    try
    {
      bool selectedIsObjectInputOperator = this.IsObjectInputOperator(this.SelectedOperator);
      if (this._previousRelationalOperator != RelationalOperators.None && this.IsObjectInputOperator(this._previousRelationalOperator) == selectedIsObjectInputOperator)
        return;
      this.RefreshControlPanel(selectedIsObjectInputOperator);
    }
    finally
    {
      this._previousRelationalOperator = this.SelectedOperator;
      this.SetControls();
    }
  }

  private void ObjectValuesChangedEvent(object sender, ValuesChangedEventArgs e)
  {
    if (e.Value1 != null && e.Value1 is List<object> && ((List<object>) e.Value1).Count == 1)
      this._value1 = ((List<object>) e.Value1)[0];
    this._value1 = e.Value1;
    this.SetControls();
  }

  private bool RelationType_OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    RelationTypeButtonDialog typeButtonDialog = new RelationTypeButtonDialog(this.dataProvider, this._typeID);
    if (!typeButtonDialog.OnOpenDialog(e.Multiselect))
      return false;
    this._typeID = typeButtonDialog.Value;
    this.tbbRelationType.Text = typeButtonDialog.Text;
    return true;
  }

  private void TbbRelationType_OnDeleteKey(object sender, EventArgs e)
  {
    this._typeID = (object) null;
    this.tbbRelationType.Text = string.Empty;
  }

  private void InnerCondition_Click(object sender, EventArgs e)
  {
    SelectionForm selectionForm = new SelectionForm()
    {
      ParentMode = SelectionFormMode.InnerConditionsForm,
      ReadOnly = false
    };
    if ((this.SelectedOperator == RelationalOperators.ConsistFromType || this.SelectedOperator == RelationalOperators.EntersInType) && !this.ObjectIsEmpty)
      selectionForm.ObjectTypeForInnerSelection = ControlsHelper.GetObjectTypeFilterForInnerForm(this._value1);
    selectionForm.SelectionLoad(this.selectionID, new List<long>(), this._nestedConditions);
    if (selectionForm.ShowDialog() != DialogResult.OK)
      return;
    this._nestedConditions = selectionForm.Conditions;
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
    this.groupBox1 = new GroupBox();
    this.tvRelationalOperators = new RelationalOperatorsList();
    this.splitContainer1 = new SplitContainer();
    this.tbbRelationType = new TextBoxButton(true, string.Empty);
    this.label2 = new Label();
    this.groupBox2 = new GroupBox();
    this.panel1 = new Panel();
    this.checkBox1 = new CheckBox();
    this.bInnerCondition = new Button();
    this.groupBox1.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(394, 372);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 15;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(521, 372);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 14;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.groupBox1.Controls.Add((Control) this.tvRelationalOperators);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(227, 354);
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
    this.tvRelationalOperators.Size = new Size(208 /*0xD0*/, 329);
    this.tvRelationalOperators.TabIndex = 4;
    this.tvRelationalOperators.AfterSelect += new TreeViewEventHandler(this.RelationalOperators_AfterSelect);
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(12, 12);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.tbbRelationType);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.groupBox2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.checkBox1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.bInnerCondition);
    this.splitContainer1.Size = new Size(630, 354);
    this.splitContainer1.SplitterDistance = 227;
    this.splitContainer1.TabIndex = 10;
    this.tbbRelationType.Location = new Point(111, 269);
    this.tbbRelationType.Name = "tbbRelationType";
    this.tbbRelationType.Size = new Size(250, 25);
    this.tbbRelationType.TabIndex = 1;
    this.tbbRelationType.OnOpenDialog += new OnOpenDialogEventHandler(this.RelationType_OnOpenDialog);
    this.tbbRelationType.OnDeleteKey += new EventHandler(this.TbbRelationType_OnDeleteKey);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(35, 271);
    this.label2.Name = "label2";
    this.label2.Size = new Size(68, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Тип связей:";
    this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.panel1);
    this.groupBox2.Location = new Point(0, 0);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(399, 231);
    this.groupBox2.TabIndex = 5;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Значение";
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Location = new Point(2, 14);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(394, 211);
    this.panel1.TabIndex = 0;
    this.checkBox1.AutoSize = true;
    this.checkBox1.Location = new Point(38, 237);
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Size = new Size(182, 17);
    this.checkBox1.TabIndex = 0;
    this.checkBox1.Text = "Использовать входной объект";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.CheckBox1_CheckedChanged);
    this.bInnerCondition.Location = new Point(35, 311);
    this.bInnerCondition.Name = "bInnerCondition";
    this.bInnerCondition.Size = new Size(121, 27);
    this.bInnerCondition.TabIndex = 2;
    this.bInnerCondition.Text = "Вложенные условия";
    this.bInnerCondition.UseVisualStyleBackColor = true;
    this.bInnerCondition.Click += new EventHandler(this.InnerCondition_Click);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(654, 411);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.MinimumSize = new Size(670, 450);
    this.Name = nameof (InputOperatorForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Оператор отношений для состава и входимости";
    this.groupBox1.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
