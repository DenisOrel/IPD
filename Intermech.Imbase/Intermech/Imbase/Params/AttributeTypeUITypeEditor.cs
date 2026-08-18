// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.AttributeTypeUITypeEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Params;

internal class AttributeTypeUITypeEditor : UITypeEditor
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
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      });
      return attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count == 0 ? value : (object) attributesSelectDlg.SelectedAttributesID[0];
    }
  }
}
