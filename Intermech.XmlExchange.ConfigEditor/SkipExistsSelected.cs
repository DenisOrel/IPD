// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.SkipExistsSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class SkipExistsSelected : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
    if (formsEditorService != null)
    {
      Dictionary<string, SkipExistsMode> dictionary = new Dictionary<string, SkipExistsMode>();
      CheckedListBox checkedListBox = new CheckedListBox();
      checkedListBox.BorderStyle = BorderStyle.None;
      checkedListBox.DrawMode = DrawMode.Normal;
      checkedListBox.IntegralHeight = true;
      checkedListBox.CheckOnClick = true;
      checkedListBox.Tag = (object) formsEditorService;
      SkipExistsMode result = SkipExistsMode.None;
      bool flag1 = value != null && Enum.TryParse<SkipExistsMode>(value.ToString(), out result);
      foreach (SkipExistsMode flag2 in Enum.GetValues(typeof (SkipExistsMode)))
      {
        checkedListBox.Items.Add((object) flag2.GetDescription<SkipExistsMode>(), flag1 && result.HasFlag((Enum) flag2));
        dictionary.Add(flag2.GetDescription<SkipExistsMode>(), flag2);
      }
      if (result != SkipExistsMode.None)
        checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
      formsEditorService.DropDownControl((Control) checkedListBox);
      if (checkedListBox.SelectedItem != null)
      {
        SkipExistsMode skipExistsMode = SkipExistsMode.None;
        for (int index = 0; index < checkedListBox.Items.Count; ++index)
        {
          SkipExistsMode flag3;
          if (dictionary.TryGetValue(checkedListBox.Items[index].ToString(), out flag3))
          {
            if (checkedListBox.GetItemChecked(index))
            {
              if (!skipExistsMode.HasFlag((Enum) flag3))
                skipExistsMode |= flag3;
            }
            else if (skipExistsMode.HasFlag((Enum) flag3))
              skipExistsMode ^= flag3;
          }
        }
        return (object) (int) skipExistsMode;
      }
    }
    return value;
  }
}
