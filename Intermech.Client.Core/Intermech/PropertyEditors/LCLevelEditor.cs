
// Type: Intermech.PropertyEditors.LCLevelEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class LCLevelEditor : UITypeEditor
{
  protected SelectorForm selectorForm;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    if (context == null)
      return base.GetEditStyle(context);
    return context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    IWindowsFormsEditorService service = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    this.CheckSelectorForm();
    this.selectorForm.ClearSelection();
    if (value is LCLevelPropertyClass)
      this.selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
      {
        ((LCLevelPropertyClass) value).LCLevel
      }), new ArrayList((ICollection) new System.Type[1]
      {
        typeof (LevelFolder)
      }));
    if (this.selectorForm.ShowDialog() != DialogResult.OK || this.selectorForm.IDList.Count <= 0)
      return value;
    return (System.Type) this.selectorForm.TypeList[0] == typeof (LevelFolder) ? (object) new LCLevelPropertyClass(0, (string) null) : (object) new LCLevelPropertyClass((int) this.selectorForm.IDList[0], this.selectorForm.NameList[0].ToString());
  }

  protected void CheckSelectorForm() => this.CheckSelectorForm(false);

  protected void CheckSelectorForm(bool multiselect)
  {
    if (this.selectorForm != null)
      return;
    this.selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_118"), new System.Type[2]
    {
      typeof (ObjectTypesFolder),
      typeof (ObjectTypeFolder)
    }, (multiselect ? 1 : 0) != 0);
  }
}
