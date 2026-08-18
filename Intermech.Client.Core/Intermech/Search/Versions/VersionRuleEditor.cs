
// Type: Intermech.Search.Versions.VersionRuleEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.Versions;

public sealed class VersionRuleEditor : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is long))
      throw new ArgumentException();
    BindingList<VersionsRule> bindingList = new BindingList<VersionsRule>((IList<VersionsRule>) ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).GetEditingRules().OrderBy<VersionsRule, string>((Func<VersionsRule, string>) (o => o.RuleObjectCaption)).ToList<VersionsRule>());
    using (VersionRuleSelectForm versionRuleSelectForm = new VersionRuleSelectForm())
    {
      versionRuleSelectForm.Location = Cursor.Position;
      versionRuleSelectForm.DataSource = bindingList;
      if (versionRuleSelectForm.ShowDialog() == DialogResult.OK)
        return versionRuleSelectForm.SelectedVersionRule != null ? (object) versionRuleSelectForm.SelectedVersionRule.RuleObjectID : value;
    }
    return value;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }
}
