// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.AttributesComparisonEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Imbase;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class AttributesComparisonEditor : UITypeEditor
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
    using (AttributesComparisonForm attributesComparisonForm = new AttributesComparisonForm(value != null ? (AttributesComparison) value : (AttributesComparison) null))
    {
      if (attributesComparisonForm.ShowDialog() == DialogResult.OK)
        return (object) attributesComparisonForm.Comparison;
    }
    return value;
  }
}
