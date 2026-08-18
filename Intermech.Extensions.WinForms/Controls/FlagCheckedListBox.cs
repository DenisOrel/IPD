// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.FlagCheckedListBox
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class FlagCheckedListBox : CheckedListBox
{
  private bool _isUpdatingCheckStates;
  [CanBeNull]
  private Type _enumType;
  [CanBeNull]
  private Enum _enumValue;

  public FlagCheckedListBox() => this.CheckOnClick = true;

  [NotNull]
  public FlagCheckedListBoxItem Add(int value, [NotNull] string caption)
  {
    FlagCheckedListBoxItem checkedListBoxItem = new FlagCheckedListBoxItem(value, caption);
    this.Items.Add((object) checkedListBoxItem);
    return checkedListBoxItem;
  }

  [NotNull]
  public FlagCheckedListBoxItem Add([NotNull] FlagCheckedListBoxItem item)
  {
    this.Items.Add((object) item);
    return item;
  }

  protected override void OnItemCheck([NotNull] ItemCheckEventArgs e)
  {
    base.OnItemCheck(e);
    if (this._isUpdatingCheckStates)
      return;
    this.UpdateCheckedItems(this.Items[e.Index] as FlagCheckedListBoxItem, e.NewValue);
  }

  protected void UpdateCheckedItems(int value)
  {
    this._isUpdatingCheckStates = true;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.Items[index] is FlagCheckedListBoxItem checkedListBoxItem)
      {
        if (checkedListBoxItem.Value == 0)
          this.SetItemChecked(index, value == 0);
        else if ((checkedListBoxItem.Value & value) == checkedListBoxItem.Value)
          this.SetItemChecked(index, true);
        else
          this.SetItemChecked(index, false);
      }
    }
    this._isUpdatingCheckStates = false;
  }

  protected void UpdateCheckedItems([NotNull] FlagCheckedListBoxItem composite, CheckState cs)
  {
    if (composite.Value == 0)
      this.UpdateCheckedItems(0);
    int num = this.Items.OfType<FlagCheckedListBoxItem>().Where<FlagCheckedListBoxItem>((Func<FlagCheckedListBoxItem, int, bool>) ((item, i) => this.GetItemChecked(i))).Aggregate<FlagCheckedListBoxItem, int>(0, (Func<int, FlagCheckedListBoxItem, int>) ((current, item) => current | item.Value));
    this.UpdateCheckedItems(cs != CheckState.Unchecked ? num | composite.Value : num & ~composite.Value);
  }

  public int GetCurrentValue()
  {
    return this.Items.OfType<FlagCheckedListBoxItem>().Where<FlagCheckedListBoxItem>((Func<FlagCheckedListBoxItem, int, bool>) ((item, i) => this.GetItemChecked(i))).Aggregate<FlagCheckedListBoxItem, int>(0, (Func<int, FlagCheckedListBoxItem, int>) ((current, item) => current | item.Value));
  }

  private void FillEnumMembers()
  {
    foreach (string name in Enum.GetNames(this._enumType))
    {
      object obj = Enum.Parse(this._enumType, name);
      int num = (int) Convert.ChangeType(obj, typeof (int));
      string caption = name;
      MemberInfo[] member = this._enumType.GetMember(obj.ToString());
      if (member.Length != 0)
      {
        object[] customAttributes = member[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
        if (customAttributes.Length != 0)
          caption = (customAttributes[0] as DescriptionAttribute).Description ?? string.Empty;
      }
      this.Add(num, caption);
    }
  }

  private void ApplyEnumValue()
  {
    this.UpdateCheckedItems((int) Convert.ChangeType((object) this._enumValue, typeof (int)));
  }

  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Enum EnumValue
  {
    get => (Enum) Enum.ToObject(this._enumType, this.GetCurrentValue());
    set
    {
      this.Items.Clear();
      this._enumValue = value;
      this._enumType = value.GetType();
      this.FillEnumMembers();
      this.ApplyEnumValue();
    }
  }
}
