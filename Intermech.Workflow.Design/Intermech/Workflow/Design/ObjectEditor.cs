// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ObjectEditor
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

/// <summary>Выбор объектов определенного типа</summary>
public class ObjectEditor : UITypeEditor
{
  private Guid _objectTypeGuid;
  private int? _objectTypeID;

  public Guid ObjectTypeGuid => this._objectTypeGuid;

  public ObjectEditor(Guid objectTypeGuid) => this._objectTypeGuid = objectTypeGuid;

  private int ObjectTypeID
  {
    get
    {
      if (!this._objectTypeID.HasValue)
        this._objectTypeID = new int?(MetaDataHelper.GetObjectTypeID(this.ObjectTypeGuid));
      return this._objectTypeID.Value;
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
      this.ObjectTypeID
    }, true, true);
    if (dbObjectIdArray != null)
      value = (object) new CalendarPropertyClass(dbObjectIdArray[0].Value);
    return value;
  }
}
