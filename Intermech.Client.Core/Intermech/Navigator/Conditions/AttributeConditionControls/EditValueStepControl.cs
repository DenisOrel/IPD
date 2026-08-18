
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.EditValueStepControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class EditValueStepControl : UserControl, IEditValueStepControl
{
  /// <summary>Идентификатор выбранного атрибута</summary>
  private ConditionAttributeInfo _attribute;
  private Dictionary<object, string> _possibleValues;
  protected IConditionDataProvider dataProvider;
  private SelectionParameterTypes _paramType;
  private object _value1;
  private object _value2;
  private ConditionStructure _initializedConditionStructure;
  private bool _caseSensitive;
  private int[] _objectTypeIDs;
  private bool _privateChanges;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer1;
  private GroupBox groupBox1;
  private RelationalOperatorsList tvRelationalOperators;
  private GroupBox gbValue;
  private Panel pValue;

  public event StepControlStateChangedHandler StepControlStateChanged;

  public EditValueStepControl(IConditionDataProvider dataProvider, int[] objectTypeIDs)
  {
    this.InitializeComponent();
    this.dataProvider = dataProvider;
    this._objectTypeIDs = objectTypeIDs;
  }

  protected void SetValueGroupBoxText(string text) => this.gbValue.Text = text;

  /// <summary>
  /// Метод вызывается при отображении контрола на форме (при нажатии кнопки Вперед на предыдущем шаге).
  /// </summary>
  /// <param name="attribute">Выбранный атрибут</param>
  public virtual void OnActivate(
    ConditionAttributeInfo attribute,
    ConditionStructure conditionStructure)
  {
    if (attribute.Equals((object) this._attribute))
    {
      if (this.pValue.Controls.Count != 1)
        return;
      this.ValuesChanged((object) this.pValue.Controls[0]);
    }
    else
    {
      this._attribute = attribute;
      this._possibleValues = this.dataProvider.GetPossibleValues(this._attribute.Id);
      this._value1 = conditionStructure.Value;
      this._value2 = conditionStructure.Value2;
      this._caseSensitive = conditionStructure.CaseSensitive;
      this._initializedConditionStructure = conditionStructure;
      this.tvRelationalOperators.Nodes.Clear();
      RelationalOperators[] enabledOperators;
      this.GetControlData(out enabledOperators, out this._paramType);
      this.tvRelationalOperators.Initialize(enabledOperators, conditionStructure.RelationalOperator);
    }
  }

  /// <summary>
  /// Определяем свойства контрола, такие как список допустимых операторов и тип данных
  /// </summary>
  protected virtual void GetControlData(
    out RelationalOperators[] enabledOperators,
    out SelectionParameterTypes paramType)
  {
    enabledOperators = (RelationalOperators[]) null;
    IConditionEditorAttribute handler = ServicesManager.GetService<IConditionEditorAttributeService>().GetHandler(this.dataProvider.GetAttributeGuid(this._attribute.Id));
    if (handler == null)
    {
      int attributeId = this.dataProvider.GetAttributeID(this._attribute.Id);
      paramType = SelectionParameter.GetNodeValueType(attributeId, this._attribute.FieldType);
      enabledOperators = this.dataProvider.GetEnableRelationalOperators(this._attribute.FieldType, attributeId);
      if (paramType != SelectionParameterTypes.sptObject)
        return;
      List<RelationalOperators> relationalOperatorsList = new List<RelationalOperators>((IEnumerable<RelationalOperators>) enabledOperators);
      relationalOperatorsList.AddRange((IEnumerable<RelationalOperators>) SelectionParameter.StringOperators);
      enabledOperators = relationalOperatorsList.ToArray();
    }
    else
    {
      paramType = handler.NodeValueType;
      enabledOperators = handler.Operators;
    }
  }

  public ConditionStructure ConditionStructure
  {
    get
    {
      RelationalOperators tag = (RelationalOperators) this.tvRelationalOperators.SelectedNode.Tag;
      if (!this._privateChanges && SelectionParameter.IsNoneValueOpr(tag))
      {
        this._value1 = (object) null;
        this._value2 = (object) null;
      }
      ConditionStructure conditionStructure = this._initializedConditionStructure.Clone() with
      {
        Attribute = (object) this.dataProvider.GetAttributeID(this._attribute.Id),
        RelationalOperator = tag,
        Value = this._value1,
        Value2 = this._value2,
        CaseSensitive = this._caseSensitive
      };
      if (this._paramType == SelectionParameterTypes.sptObject && this._value1 is string)
        conditionStructure.Content = ColumnContents.String;
      return conditionStructure;
    }
  }

  public UserControl Control => (UserControl) this;

  private void RelationalOperators_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.ClearPanelValue();
    RelationalOperators tag = (RelationalOperators) e.Node.Tag;
    RelationOperatorValueMode operatorValueMode = ControlsHelper.GetRelationOperatorValueMode(tag);
    if (operatorValueMode != RelationOperatorValueMode.rovmNone)
    {
      ShowValueMode valueMode = this.GetValueMode(operatorValueMode, this._paramType, tag, this._possibleValues != null && this._possibleValues.Count > 0);
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
      ((IValueControl) userControl).ValuesChangedEvent += new ValuesChangedEventHandler(this.ValuesChangedEvent);
      ((IValueControl) userControl).CaseSensitiveChangedEvent += new CaseSensitiveChangedEventHandler(this.CaseSensitiveChangedEvent);
      ((IValueControl) userControl).OnGetLabelEvent += new OnGetLabelEventHandler(this.OnGetLabelEvent);
      this._privateChanges = true;
      try
      {
        ((IValueControl) userControl).Initialize(this.dataProvider.GetAttributeID(this._attribute.Id), this._paramType, valueMode, this._possibleValues, this.ConditionStructure, this._objectTypeIDs, (object) null);
      }
      finally
      {
        this._privateChanges = false;
      }
      this.pValue.Controls.Add((System.Windows.Forms.Control) userControl);
      userControl.Dock = DockStyle.Fill;
    }
    else
    {
      if (this.StepControlStateChanged == null)
        return;
      this.StepControlStateChanged((object) this, new StepControlStateChangedEventArgs(true));
    }
  }

  private void OnGetLabelEvent(object sender, OnGetLabelEventArgs e)
  {
    if (!SelectionParameter.IsLinkRelationOpr(e.RelationalOperator))
      return;
    e.LabelsForControl.Label1 = "В объектах типа:";
    e.Handled = true;
  }

  private void ValuesChanged(object sender)
  {
    bool nextEnable = !(sender is TwoValueControl) ? this._value1 != null : this._value1 != null && this._value2 != null;
    if (this.StepControlStateChanged == null)
      return;
    this.StepControlStateChanged((object) this, new StepControlStateChangedEventArgs(nextEnable));
  }

  private void ClearPanelValue()
  {
    if (this.pValue.Controls.Count == 0)
      return;
    ((IValueControl) this.pValue.Controls[0]).ValuesChangedEvent -= new ValuesChangedEventHandler(this.ValuesChangedEvent);
    ((IValueControl) this.pValue.Controls[0]).CaseSensitiveChangedEvent -= new CaseSensitiveChangedEventHandler(this.CaseSensitiveChangedEvent);
    ((IValueControl) this.pValue.Controls[0]).OnGetLabelEvent -= new OnGetLabelEventHandler(this.OnGetLabelEvent);
    foreach (Component control in (ArrangedElementCollection) this.pValue.Controls)
      control.Dispose();
  }

  private void CaseSensitiveChangedEvent(object sender, CaseSensitiveChangedEventArgs e)
  {
    this._caseSensitive = e.CaseSensitive;
  }

  private void ValuesChangedEvent(object sender, ValuesChangedEventArgs e)
  {
    this._value1 = e.Value1;
    this._value2 = e.Value2;
    this.ValuesChanged(sender);
  }

  /// <summary>
  /// установка флажков определяющих видимость элементов управления
  /// </summary>
  /// <param name="rovm"></param>
  /// <returns></returns>
  protected virtual ShowValueMode GetValueMode(
    RelationOperatorValueMode rovm,
    SelectionParameterTypes paramType,
    RelationalOperators currentOperator,
    bool possibleValuesPresent)
  {
    return ControlsHelper.GetValueMode(rovm, paramType, currentOperator, possibleValuesPresent);
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
    this.splitContainer1 = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.gbValue = new GroupBox();
    this.pValue = new Panel();
    this.tvRelationalOperators = new RelationalOperatorsList();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.gbValue.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.groupBox1);
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.gbValue);
    this.splitContainer1.Size = new Size(646, 281);
    this.splitContainer1.SplitterDistance = 281;
    this.splitContainer1.TabIndex = 11;
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.tvRelationalOperators);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(281, 281);
    this.groupBox1.TabIndex = 8;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Условие";
    this.gbValue.Controls.Add((System.Windows.Forms.Control) this.pValue);
    this.gbValue.Dock = DockStyle.Fill;
    this.gbValue.Location = new Point(0, 0);
    this.gbValue.Name = "gbValue";
    this.gbValue.Size = new Size(361, 281);
    this.gbValue.TabIndex = 9;
    this.gbValue.TabStop = false;
    this.gbValue.Text = "Значение";
    this.pValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pValue.AutoScroll = true;
    this.pValue.Location = new Point(3, 16 /*0x10*/);
    this.pValue.Name = "pValue";
    this.pValue.Size = new Size(352, 259);
    this.pValue.TabIndex = 0;
    this.tvRelationalOperators.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tvRelationalOperators.BackColor = SystemColors.Control;
    this.tvRelationalOperators.BorderStyle = BorderStyle.None;
    this.tvRelationalOperators.FullRowSelect = true;
    this.tvRelationalOperators.Location = new Point(6, 19);
    this.tvRelationalOperators.Name = "tvRelationalOperators";
    this.tvRelationalOperators.ShowLines = false;
    this.tvRelationalOperators.ShowRootLines = false;
    this.tvRelationalOperators.Size = new Size(269, 256 /*0x0100*/);
    this.tvRelationalOperators.TabIndex = 4;
    this.tvRelationalOperators.AfterSelect += new TreeViewEventHandler(this.RelationalOperators_AfterSelect);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.Name = nameof (EditValueStepControl);
    this.Size = new Size(646, 281);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.gbValue.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
