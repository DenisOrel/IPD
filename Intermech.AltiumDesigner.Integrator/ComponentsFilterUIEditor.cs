// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ComponentsFilterUIEditor
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class ComponentsFilterUIEditor : UITypeEditor
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
    using (ComponentsFilterForm componentsFilterForm = new ComponentsFilterForm())
    {
      componentsFilterForm.Initialize(value as ComponentsFilterSettings<ADComponentsCompositionVariants>);
      return componentsFilterForm.ShowDialog() == DialogResult.OK ? (object) componentsFilterForm.Value : value;
    }
  }
}
