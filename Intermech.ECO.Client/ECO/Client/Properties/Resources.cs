// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.Properties.Resources
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.ECO.Client.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
      if (Intermech.ECO.Client.Properties.Resources.resourceMan == null)
        Intermech.ECO.Client.Properties.Resources.resourceMan = new ResourceManager("Intermech.ECO.Client.Properties.Resources", typeof (Intermech.ECO.Client.Properties.Resources).Assembly);
      return Intermech.ECO.Client.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.ECO.Client.Properties.Resources.resourceCulture;
    set => Intermech.ECO.Client.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap Roger
  {
    get => (Bitmap) Intermech.ECO.Client.Properties.Resources.ResourceManager.GetObject(nameof (Roger), Intermech.ECO.Client.Properties.Resources.resourceCulture);
  }
}
