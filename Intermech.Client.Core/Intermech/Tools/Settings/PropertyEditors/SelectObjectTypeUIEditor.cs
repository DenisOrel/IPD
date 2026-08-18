
// Type: Intermech.Tools.Settings.PropertyEditors.SelectObjectTypeUIEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class SelectObjectTypeUIEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return !this.CanEditProperty(context.PropertyDescriptor) ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  private bool CanEditProperty(PropertyDescriptor propDescriptor)
  {
    return propDescriptor.PropertyType == typeof (int) || this.IsNamedIdentifier(propDescriptor);
  }

  private bool IsNamedIdentifier(PropertyDescriptor propDescriptor)
  {
    return propDescriptor.PropertyType == typeof (LocalId<int>) || propDescriptor.PropertyType == typeof (GlobalId<int>);
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object originalValue)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_1608"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count != 1)
      return originalValue;
    object objectType = selectorForm.IDList[0];
    if (this.IsNamedIdentifier(context.PropertyDescriptor))
      objectType = (object) DBHelper.CreateObjectTypeGID((int) objectType);
    if (objectType.Equals(originalValue))
      objectType = originalValue;
    return objectType;
  }
}
