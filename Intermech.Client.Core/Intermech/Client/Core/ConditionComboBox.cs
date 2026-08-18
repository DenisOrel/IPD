
// Type: Intermech.Client.Core.ConditionComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Summary description for ConditionComboBox.</summary>
public class ConditionComboBox : ComboBox
{
  public ConditionComboBox()
  {
    this.Sorted = true;
    this.DropDownStyle = ComboBoxStyle.DropDownList;
  }

  [Browsable(false)]
  public FlagsConditions SelectedCondition
  {
    get
    {
      return this.SelectedItem == null ? FlagsConditions.NONE : ((ConditionClass) this.SelectedItem).FlagCondition;
    }
    set
    {
      this.SelectedItem = (object) null;
      if (value == FlagsConditions.NONE)
        return;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (((ConditionClass) this.Items[index]).FlagCondition == value)
        {
          this.SelectedItem = this.Items[index];
          break;
        }
      }
    }
  }

  private void AddConditionClass(FlagsConditions afc)
  {
    this.Items.Add((object) new ConditionClass(afc));
  }

  public void AssignItems(FlagsConditions fc)
  {
    this.Items.Clear();
    if ((fc & FlagsConditions.EQUAL) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.EQUAL);
    if ((fc & FlagsConditions.NOTEQUAL) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.NOTEQUAL);
    if ((fc & FlagsConditions.LESS) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.LESS);
    if ((fc & FlagsConditions.LESSEQUAL) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.LESSEQUAL);
    if ((fc & FlagsConditions.GREATER) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.GREATER);
    if ((fc & FlagsConditions.GREATEREQUAL) != FlagsConditions.NONE)
      this.AddConditionClass(FlagsConditions.GREATEREQUAL);
    if ((fc & FlagsConditions.SUBSTR) == FlagsConditions.NONE)
      return;
    this.AddConditionClass(FlagsConditions.SUBSTR);
  }
}
