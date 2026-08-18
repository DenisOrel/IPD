// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.CatalogSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class CatalogSelected : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    QuickObjectInfo quickObjectInfo = SelectObject.SelectObjectOfType(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    return quickObjectInfo.VersionGuid != Guid.Empty ? (object) quickObjectInfo.VersionGuid : value;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }
}
