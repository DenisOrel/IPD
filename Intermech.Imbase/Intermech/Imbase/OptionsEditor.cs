// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.OptionsEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase;

internal class OptionsEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    OptionsConverter optionsConverter = new OptionsConverter();
    CheckedListBox checkedListBox = new CheckedListBox();
    checkedListBox.BorderStyle = BorderStyle.None;
    checkedListBox.CheckOnClick = true;
    checkedListBox.Items.Add(optionsConverter.Hash.forward[(object) AttributeOptions.ImbaseFlag_CADMECH]);
    checkedListBox.Items.Add(optionsConverter.Hash.forward[(object) AttributeOptions.ImbaseFlag_CADMECH_T]);
    checkedListBox.Items.Add(optionsConverter.Hash.forward[(object) AttributeOptions.ImbaseFlag_AVS]);
    checkedListBox.Items.Add(optionsConverter.Hash.forward[(object) AttributeOptions.ImbaseFlag_SEARCH]);
    checkedListBox.Items.Add(optionsConverter.Hash.forward[(object) AttributeOptions.ImbaseFlag_CADPROPERTY]);
    checkedListBox.Height = checkedListBox.Items.Count * checkedListBox.ItemHeight;
    if (value is int)
    {
      int int32 = Convert.ToInt32(value);
      checkedListBox.SetItemChecked(0, (int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH)) == int32);
      checkedListBox.SetItemChecked(1, (int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH_T)) == int32);
      checkedListBox.SetItemChecked(2, (int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_AVS)) == int32);
      checkedListBox.SetItemChecked(3, (int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_SEARCH)) == int32);
      checkedListBox.SetItemChecked(4, (int32 | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADPROPERTY)) == int32);
    }
    this.svc.DropDownControl((Control) checkedListBox);
    int num = Convert.ToInt32(value) & ~(Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH) | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH_T) | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_AVS) | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_SEARCH) | Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADPROPERTY));
    foreach (int checkedIndex in checkedListBox.CheckedIndices)
    {
      switch (checkedIndex)
      {
        case 0:
          num |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH);
          continue;
        case 1:
          num |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADMECH_T);
          continue;
        case 2:
          num |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_AVS);
          continue;
        case 3:
          num |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_SEARCH);
          continue;
        case 4:
          num |= Convert.ToInt32((object) AttributeOptions.ImbaseFlag_CADPROPERTY);
          continue;
        default:
          continue;
      }
    }
    return (object) num;
  }
}
