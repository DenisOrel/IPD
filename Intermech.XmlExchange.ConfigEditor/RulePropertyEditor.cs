// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.RulePropertyEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class RulePropertyEditor : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    VersionRulesSelectionForm rulesSelectionForm = (VersionRulesSelectionForm) null;
    Guid result;
    if (value != null && Guid.TryParse(value.ToString(), out result))
      rulesSelectionForm = new VersionRulesSelectionForm(VersionRulesSelectFilter.vrfNone, false, "", result);
    if (rulesSelectionForm == null)
      rulesSelectionForm = new VersionRulesSelectionForm(VersionRulesSelectFilter.vrfNone, false, "");
    return rulesSelectionForm.ShowDialog() == DialogResult.OK && rulesSelectionForm.SelectedRules.Length != 0 ? (object) rulesSelectionForm.SelectedRules[0].RuleObjectGuid : value;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }
}
