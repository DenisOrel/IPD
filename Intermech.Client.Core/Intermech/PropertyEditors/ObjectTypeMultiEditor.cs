
// Type: Intermech.PropertyEditors.ObjectTypeMultiEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class ObjectTypeMultiEditor : ObjectTypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    IWindowsFormsEditorService service = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    this.CheckSelectorForm(true);
    this.selectorForm.ClearSelection();
    if (value is ObjectTypeMultiPropertyClass)
    {
      bool flag = false;
      for (int index = 0; index < ((ObjectTypeMultiPropertyClass) value).ObjectTypePropertyClassList.Count; ++index)
      {
        if (((ObjectTypeMultiPropertyClass) value).ObjectTypePropertyClassList[index].ObjectType == -1)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        this.selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
        {
          -1
        }), new ArrayList((ICollection) new System.Type[1]
        {
          typeof (ObjectTypesFolder)
        }));
      else
        this.selectorForm.InitSelectionAsType(new ArrayList((ICollection) ((ObjectTypeMultiPropertyClass) value).ObjectTypeList), new ArrayList((ICollection) new System.Type[1]
        {
          typeof (ObjectTypeFolder)
        }));
    }
    if (this.selectorForm.ShowDialog() != DialogResult.OK || this.selectorForm.IDList.Count <= 0)
      return value;
    if (!((System.Type) this.selectorForm.TypeList[0] == typeof (ObjectTypesFolder)))
      return (object) new ObjectTypeMultiPropertyClass(new List<int>((IEnumerable<int>) this.selectorForm.IDList.ToArray(typeof (int))));
    return (object) new ObjectTypeMultiPropertyClass(new List<int>((IEnumerable<int>) new int[1]
    {
      -1
    }));
  }
}
