
// Type: Intermech.PropertyEditors.ColumnSchemeAttEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ImbaseObjectTypeEditor.</summary>
public class ColumnSchemeAttEditor : UITypeEditor
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
    Guid attribute = Guid.Empty;
    int columnWidth = 150;
    AttributeSourceTypes sourceType = AttributeSourceTypes.Object;
    if (value != DBNull.Value && value != null && Convert.ToString(value) != string.Empty && value is ColumnSchemeAttProxy columnSchemeAttProxy)
    {
      string[] strArray = columnSchemeAttProxy.Value.ToString().Split('|');
      if (strArray.Length == 3)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(strArray[0]));
          sourceType = (AttributeSourceTypes) Convert.ToInt32(strArray[1]);
          attribute = (attributeType as IDBGuid).GUID;
          columnWidth = Convert.ToInt32(strArray[2]);
        }
      }
    }
    ColumnSchemeAttrEditForm schemeAttrEditForm = new ColumnSchemeAttrEditForm((List<int>) null, attribute, columnWidth, sourceType);
    return schemeAttrEditForm.ShowDialog() == DialogResult.OK ? (object) new ColumnSchemeAttProxy(schemeAttrEditForm.Attribute, schemeAttrEditForm.AttributeSource, schemeAttrEditForm.ColumnWidth) : value;
  }
}
