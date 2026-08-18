// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.Properties.Resources
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceMan == null)
        Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceMan = new ResourceManager("Intermech.AltiumDesigner.Integrator.Properties.Resources", typeof (Intermech.AltiumDesigner.Integrator.Properties.Resources).Assembly);
      return Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceCulture;
    set => Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap ad16x16
  {
    get
    {
      return (Bitmap) Intermech.AltiumDesigner.Integrator.Properties.Resources.ResourceManager.GetObject(nameof (ad16x16), Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap ad32x32
  {
    get
    {
      return (Bitmap) Intermech.AltiumDesigner.Integrator.Properties.Resources.ResourceManager.GetObject(nameof (ad32x32), Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceCulture);
    }
  }

  internal static string Integrator_template
  {
    get
    {
      return Intermech.AltiumDesigner.Integrator.Properties.Resources.ResourceManager.GetString(nameof (Integrator_template), Intermech.AltiumDesigner.Integrator.Properties.Resources.resourceCulture);
    }
  }
}
