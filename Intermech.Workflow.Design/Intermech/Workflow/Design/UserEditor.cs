// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UserEditor
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Выбор пользователей</summary>
public class UserEditor : UITypeEditor
{
  private int? usersObjTypeID;

  private int UsersObjTypeID
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
    return context?.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
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
    }, objects, true, true);
    if (dbObjectIdArray != null)
      value = (object) new UserPropertyClass(dbObjectIdArray[0].Value);
    return value;
  }
}
