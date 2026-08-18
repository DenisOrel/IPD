
// Type: Intermech.PropertyEditors.UserEditor
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

/// <summary>Редактор юзеров</summary>
public class UserEditor : UITypeEditor
{
  private int? usersObjTypeID;

  protected int UsersObjTypeID
  {
    get
    {
      if (!this.usersObjTypeID.HasValue)
        this.usersObjTypeID = new int?(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"));
      return this.usersObjTypeID.Value;
    }
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    if (context == null)
      return base.GetEditStyle(context);
    return context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    long[] objects;
    if (value == null || !(value is UserPropertyClass))
      objects = (long[]) null;
    else
      objects = new long[1]
      {
        ((ObjectPropertyClass) value).ObjectID
      };
    IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
    {
      this.UsersObjTypeID
    }, objects, true, false);
    if (dbObjectIdArray != null)
      value = (object) new UserPropertyClass(dbObjectIdArray[0].Value);
    return value;
  }
}
