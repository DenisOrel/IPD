
// Type: Intermech.PropertyEditors.RelationTypeAttEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ImbaseObjectTypeEditor.</summary>
public class RelationTypeAttEditor : UITypeEditor
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
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("Client.Core_983"), typeof (RelationTypeFolder), false);
    if (selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0)
    {
      int id = (int) selectorForm.IDList[0];
      return (object) new RelationTypeAttProxy((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetRelationType(id).GUID);
    }
    return value is Guid guid ? (object) new RelationTypeAttProxy(guid) : (object) new RelationTypeAttProxy(Guid.Empty);
  }
}
