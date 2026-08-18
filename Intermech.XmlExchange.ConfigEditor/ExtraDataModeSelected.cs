// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ExtraDataModeSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ExtraDataModeSelected : UITypeEditor
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
      Dictionary<string, XmlExportExtraDataMode> dictionary = new Dictionary<string, XmlExportExtraDataMode>();
      CheckedListBox checkedListBox = new CheckedListBox();
      checkedListBox.BorderStyle = BorderStyle.None;
      checkedListBox.DrawMode = DrawMode.Normal;
      checkedListBox.IntegralHeight = true;
      checkedListBox.CheckOnClick = true;
      checkedListBox.Tag = (object) formsEditorService;
      XmlExportExtraDataMode result = XmlExportExtraDataMode.None;
      bool flag1 = value != null && Enum.TryParse<XmlExportExtraDataMode>(value.ToString(), out result);
      foreach (XmlExportExtraDataMode flag2 in Enum.GetValues(typeof (XmlExportExtraDataMode)))
      {
        switch (flag2)
        {
          case XmlExportExtraDataMode.Rights:
          case XmlExportExtraDataMode.LcStep:
          case XmlExportExtraDataMode.LcStepHist:
            continue;
          default:
            checkedListBox.Items.Add((object) flag2.GetDescription<XmlExportExtraDataMode>(), flag1 && result.HasFlag((Enum) flag2));
            dictionary.Add(flag2.GetDescription<XmlExportExtraDataMode>(), flag2);
            continue;
        }
      }
      if (result != XmlExportExtraDataMode.None)
        checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
      formsEditorService.DropDownControl((Control) checkedListBox);
      if (checkedListBox.SelectedItem != null)
      {
        XmlExportExtraDataMode exportExtraDataMode = XmlExportExtraDataMode.None;
        for (int index = 0; index < checkedListBox.Items.Count; ++index)
        {
          XmlExportExtraDataMode flag3;
          if (dictionary.TryGetValue(checkedListBox.Items[index].ToString(), out flag3))
          {
            if (checkedListBox.GetItemChecked(index))
            {
              if (!exportExtraDataMode.HasFlag((Enum) flag3))
                exportExtraDataMode |= flag3;
            }
            else if (exportExtraDataMode.HasFlag((Enum) flag3))
              exportExtraDataMode ^= flag3;
          }
        }
        return (object) (int) exportExtraDataMode;
      }
    }
    return value;
  }
}
