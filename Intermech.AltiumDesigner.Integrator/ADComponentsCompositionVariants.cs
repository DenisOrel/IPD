// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADComponentsCompositionVariants
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class ADComponentsCompositionVariants : ComponentsCompositionVariants
{
  public ADComponentsCompositionVariants() => this.Initialize(false);

  public ADComponentsCompositionVariants(
    CompositionVariants standart,
    CompositionVariants mechanical,
    CompositionVariants graphical,
    CompositionVariants netTieNoBom,
    CompositionVariants netTieBom,
    CompositionVariants standartNoBom)
  {
    this.Standard = new CompositionVariantsProxy(standart);
    this.Mechanical = new CompositionVariantsProxy(mechanical);
    this.Graphical = new CompositionVariantsProxy(graphical);
    this.NetTie_NoBOM = new CompositionVariantsProxy(netTieNoBom);
    this.NetTie_BOM = new CompositionVariantsProxy(netTieBom);
    this.Standard_NoBOM = new CompositionVariantsProxy(standartNoBom);
  }

  public override void Initialize(bool initializeDefault)
  {
    if (initializeDefault)
    {
      this.Standard = new CompositionVariantsProxy(CompositionVariants.SpecificationAndElementsList);
      this.Mechanical = new CompositionVariantsProxy(CompositionVariants.Specification);
      this.Graphical = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.NetTie_NoBOM = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.NetTie_BOM = new CompositionVariantsProxy(CompositionVariants.SpecificationAndElementsList);
      this.Standard_NoBOM = new CompositionVariantsProxy(CompositionVariants.NoUsed);
    }
    else
    {
      this.Standard = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.Mechanical = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.Graphical = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.NetTie_NoBOM = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.NetTie_BOM = new CompositionVariantsProxy(CompositionVariants.NoUsed);
      this.Standard_NoBOM = new CompositionVariantsProxy(CompositionVariants.NoUsed);
    }
  }

  [DisplayName("Standard")]
  [ComponentKind(ComponentKinds.Standard)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy Standard { get; set; }

  [DisplayName("Mechanical")]
  [ComponentKind(ComponentKinds.Mechanical)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy Mechanical { get; set; }

  [DisplayName("Graphical")]
  [ComponentKind(ComponentKinds.Graphical)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy Graphical { get; set; }

  [DisplayName("Net tie (in BOM)")]
  [ComponentKind(ComponentKinds.NetTie_BOM)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy NetTie_BOM { get; set; }

  [DisplayName("Net tie (no BOM)")]
  [ComponentKind(ComponentKinds.NetTie_NoBOM)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy NetTie_NoBOM { get; set; }

  [DisplayName("Standard (no BOM)")]
  [ComponentKind(ComponentKinds.Standard_NoBOM)]
  [Editor(typeof (CompositionVariantsEditor), typeof (UITypeEditor))]
  public CompositionVariantsProxy Standard_NoBOM { get; set; }

  public override bool Equals(object obj)
  {
    if (!(obj is ADComponentsCompositionVariants compositionVariants))
      return base.Equals(obj);
    return compositionVariants.Graphical.Equals((object) this.Graphical) && compositionVariants.Mechanical.Equals((object) this.Mechanical) && compositionVariants.NetTie_BOM.Equals((object) this.NetTie_BOM) && compositionVariants.NetTie_NoBOM.Equals((object) this.NetTie_NoBOM) && compositionVariants.Standard.Equals((object) this.Standard) && compositionVariants.Standard_NoBOM.Equals((object) this.Standard_NoBOM);
  }

  public override int GetHashCode()
  {
    return this.Standard.GetHashCode() << 24 ^ this.Standard_NoBOM.GetHashCode() << 20 ^ this.NetTie_BOM.GetHashCode() << 16 /*0x10*/ ^ this.NetTie_NoBOM.GetHashCode() << 12 ^ this.Graphical.GetHashCode() << 8 ^ this.Mechanical.GetHashCode();
  }

  public override object Clone()
  {
    return (object) new ADComponentsCompositionVariants(this.Standard.Value, this.Mechanical.Value, this.Graphical.Value, this.NetTie_NoBOM.Value, this.NetTie_BOM.Value, this.Standard_NoBOM.Value);
  }
}
