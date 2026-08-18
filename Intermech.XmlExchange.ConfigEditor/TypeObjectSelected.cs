// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.TypeObjectSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class TypeObjectSelected : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Любой тип объекта", typeof (ObjectTypeFolder), false);
    selectorForm.AllowRootSelect = true;
    return selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0 ? (object) MetaDataHelper.GetObjectTypeGuid((int) selectorForm.IDList[0]) : value;
  }
}
