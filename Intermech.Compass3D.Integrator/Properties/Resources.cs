// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Properties.Resources
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Compass3D.Integrator.Properties;

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
      if (Intermech.Compass3D.Integrator.Properties.Resources.resourceMan == null)
        Intermech.Compass3D.Integrator.Properties.Resources.resourceMan = new ResourceManager("Intermech.Compass3D.Integrator.Properties.Resources", typeof (Intermech.Compass3D.Integrator.Properties.Resources).Assembly);
      return Intermech.Compass3D.Integrator.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Compass3D.Integrator.Properties.Resources.resourceCulture;
    set => Intermech.Compass3D.Integrator.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap _16x16
  {
    get => (Bitmap) Intermech.Compass3D.Integrator.Properties.Resources.ResourceManager.GetObject(nameof (_16x16), Intermech.Compass3D.Integrator.Properties.Resources.resourceCulture);
  }

  internal static Bitmap _32x32
  {
    get => (Bitmap) Intermech.Compass3D.Integrator.Properties.Resources.ResourceManager.GetObject(nameof (_32x32), Intermech.Compass3D.Integrator.Properties.Resources.resourceCulture);
  }

  internal static string SR_IntegratorDescription
  {
    get
    {
      return Intermech.Compass3D.Integrator.Properties.Resources.ResourceManager.GetString(nameof (SR_IntegratorDescription), Intermech.Compass3D.Integrator.Properties.Resources.resourceCulture);
    }
  }
}
