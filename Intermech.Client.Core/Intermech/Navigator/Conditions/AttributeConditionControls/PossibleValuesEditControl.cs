
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.PossibleValuesEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class PossibleValuesEditControl : EditControl<ComboBox>
{
  private Dictionary<object, string> _pValues;

  public PossibleValuesEditControl(Dictionary<object, string> pValues, bool firstValue)
    : base(firstValue)
  {
    this._pValues = pValues;
  }

  public override object Value
  {
    get
    {
      return this.control.SelectedItem == null ? (object) null : ((ComboBoxItem) this.control.SelectedItem).Value;
    }
    set
    {
      if (value != null)
      {
        for (int index = 0; index < this.control.Items.Count; ++index)
        {
          if (object.Equals(((ComboBoxItem) this.control.Items[index]).Value, value))
          {
            this.control.SelectedIndex = index;
            break;
          }
        }
      }
      else
        this.control.SelectedIndex = -1;
    }
  }

  protected override object defaultValue => (object) null;

  protected override void InitializeValue(object value) => this.Value = value;

  protected override void OnCreateControl()
  {
    this.control = this.GetControlForDialog() as ComboBox;
    this.control.SelectedIndexChanged += new EventHandler(((EditControl<ComboBox>) this).OnValueChanged);
  }

  protected override Control GetControlForDialog()
  {
    ComboBox controlForDialog = new ComboBox()
    {
      DropDownStyle = ComboBoxStyle.DropDownList
    };
    foreach (KeyValuePair<object, string> pValue in this._pValues)
      controlForDialog.Items.Add((object) new ComboBoxItem(pValue.Key, pValue.Value));
    return (Control) controlForDialog;
  }

  protected override void OnOKControlDialog(Control control)
  {
    this.Value = (control as ComboBox).SelectedItem != null ? ((ComboBoxItem) (control as ComboBox).SelectedItem).Value : (object) null;
  }

  protected override bool ValidValue(object value) => true;
}
