
// Type: Intermech.PropertyEditors.ObjectTypeAttEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ImbaseObjectTypeEditor.</summary>
public class ObjectTypeAttEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_975"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() == DialogResult.Cancel || selectorForm.IDList.Count == 0)
      return value is Guid guid ? (object) new ObjectTypeAttProxy(guid) : value;
    int id = (int) selectorForm.IDList[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (object) new ObjectTypeAttProxy((sessionKeeper.Session.GetObjectType(id) as IDBGuid).GUID);
  }
}
