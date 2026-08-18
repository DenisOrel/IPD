
// Type: Intermech.PropertyEditors.TemplateEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Редактор шаблонов процессов</summary>
public class TemplateEditor : UITypeEditor
{
  private int? templateObjTypeID;

  private int TemplateObjTypeID
  {
    get
    {
      if (!this.templateObjTypeID.HasValue)
        this.templateObjTypeID = new int?(MetaDataHelper.GetObjectTypeID("cad002ac-306c-11d8-b4e9-00304f19f545"));
      return this.templateObjTypeID.Value;
    }
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context != null && context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
    {
      this.TemplateObjTypeID
    }, true, true);
    if (dbObjectIdArray != null)
      value = (object) new TemplatePropertyClass(dbObjectIdArray[0].Value);
    return value;
  }
}
