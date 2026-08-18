
// Type: Intermech.Navigator.Conditions.ValueControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Conditions.AttributeConditionControls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>Базовый для контролов изменения значений</summary>
internal class ValueControl : UserControl, IValueControl
{
  protected IConditionDataProvider dataProvider;
  protected ConditionStructure conditionStructure;
  private int _attributeID;
  protected LabelsForControl labelsForControl;
  protected object tag;
  /// <summary>Типы объектов, для которых назначена выборка</summary>
  protected int[] objectTypeIDs;
  protected object value1;
  protected object value2;

  public ValueControl()
  {
  }

  public ValueControl(IConditionDataProvider dataProvider) => this.dataProvider = dataProvider;

  public virtual void Initialize(
    int attributeID,
    SelectionParameterTypes paramType,
    ShowValueMode valueMode,
    Dictionary<object, string> pValues,
    ConditionStructure conditionStructure,
    int[] objectTypeIDs,
    object tag)
  {
    this._attributeID = attributeID;
    this.conditionStructure = conditionStructure;
    this.objectTypeIDs = objectTypeIDs;
    this.tag = tag;
    OnGetLabelEventArgs e = new OnGetLabelEventArgs(paramType, valueMode, conditionStructure.RelationalOperator);
    OnGetLabelEventHandler onGetLabelEvent = this.OnGetLabelEvent;
    if (onGetLabelEvent != null)
      onGetLabelEvent((object) this, e);
    if (!e.Handled)
      return;
    this.labelsForControl = e.LabelsForControl;
  }

  public event ValuesChangedEventHandler ValuesChangedEvent;

  public event CaseSensitiveChangedEventHandler CaseSensitiveChangedEvent;

  public event OnGetLabelEventHandler OnGetLabelEvent;

  /// <summary>Вернуть контрол для редактирования значения</summary>
  protected IEditControl GetControl(
    SelectionParameterTypes paramType,
    int attributeID,
    int[] objectTypeIDs,
    ShowValueMode valueMode,
    Dictionary<object, string> pValues,
    object value,
    bool firstValue,
    RelationalOperators relationalOperator)
  {
    IEditControl control = (IEditControl) null;
    if ((valueMode & ShowValueMode.svmBool) == ShowValueMode.svmBool)
      control = (IEditControl) new BooleanMultiValueEditControl(firstValue);
    else if ((valueMode & ShowValueMode.svmDate) == ShowValueMode.svmDate)
    {
      if (this.tag != null && this.tag is AdditionalDateTimeControlParameters)
      {
        AdditionalDateTimeControlParameters tag = (AdditionalDateTimeControlParameters) this.tag;
        control = (IEditControl) new DateTimeEditControl(firstValue, this.dataProvider, attributeID, objectTypeIDs, tag.Format, tag.FormatString);
      }
      else
        control = (IEditControl) new DateTimeEditControl(firstValue, this.dataProvider, attributeID, objectTypeIDs);
    }
    else if ((valueMode & ShowValueMode.svmNumber) == ShowValueMode.svmNumber)
      control = (IEditControl) new NumberEditControl(firstValue);
    else if ((valueMode & ShowValueMode.svmString) == ShowValueMode.svmString)
      control = (IEditControl) new TextEditControl(firstValue);
    else if ((valueMode & ShowValueMode.svmObj) == ShowValueMode.svmObj)
      control = paramType != SelectionParameterTypes.sptObject || !Array.Exists<RelationalOperators>(SelectionParameter.StringOperators, (Predicate<RelationalOperators>) (x => x.Equals((object) this.conditionStructure.RelationalOperator))) ? ObjectEditControl.GetEditControl(this.dataProvider, attributeID, this.conditionStructure.RelationalOperator, paramType, pValues, objectTypeIDs, firstValue) : (IEditControl) new TextEditControl(firstValue);
    else if ((valueMode & ShowValueMode.svmList) == ShowValueMode.svmList || (valueMode & ShowValueMode.svmListMulti) == ShowValueMode.svmListMulti)
      control = (IEditControl) new PossibleValuesEditControl(pValues, firstValue);
    else if ((valueMode & ShowValueMode.svmInputObjectAttribute) == ShowValueMode.svmInputObjectAttribute)
      control = (IEditControl) new InputObjectAttributeEditControl(this.dataProvider, firstValue, relationalOperator);
    if (control != null)
    {
      control.ValueChangedEvent += new ValueChangedEventHandler(this.Control_ValueChangedEvent);
      control.CreateControl(valueMode, value);
      control.Control.Dock = DockStyle.Fill;
    }
    return control;
  }

  protected virtual void Control_ValueChangedEvent(object sender, ValueChangedEventArgs e)
  {
    if (e.IsFirstValue)
    {
      this.value1 = e.Value;
      if (this.value2 == null)
        this.value2 = this.conditionStructure.Value2;
    }
    else
    {
      this.value2 = e.Value;
      if (this.value1 == null)
        this.value1 = this.conditionStructure.Value;
    }
    this.OnValueChanged();
  }

  protected void OnValueChanged()
  {
    ValuesChangedEventHandler valuesChangedEvent = this.ValuesChangedEvent;
    if (valuesChangedEvent == null)
      return;
    valuesChangedEvent((object) this, new ValuesChangedEventArgs(this.value1, this.value2));
  }

  protected void OnCaseSensitiveChanged(bool caseSensitive)
  {
    CaseSensitiveChangedEventHandler sensitiveChangedEvent = this.CaseSensitiveChangedEvent;
    if (sensitiveChangedEvent == null)
      return;
    sensitiveChangedEvent((object) this, new CaseSensitiveChangedEventArgs(caseSensitive));
  }
}
