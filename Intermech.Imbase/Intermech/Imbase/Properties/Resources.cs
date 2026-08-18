// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Properties.Resources
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Imbase.Properties;

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
      if (Intermech.Imbase.Properties.Resources.resourceMan == null)
        Intermech.Imbase.Properties.Resources.resourceMan = new ResourceManager("Intermech.Imbase.Properties.Resources", typeof (Intermech.Imbase.Properties.Resources).Assembly);
      return Intermech.Imbase.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Imbase.Properties.Resources.resourceCulture;
    set => Intermech.Imbase.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap Apply
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (Apply), Intermech.Imbase.Properties.Resources.resourceCulture);
  }

  internal static Bitmap Cancel
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (Cancel), Intermech.Imbase.Properties.Resources.resourceCulture);
  }

  internal static Bitmap clean
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (clean), Intermech.Imbase.Properties.Resources.resourceCulture);
  }

  internal static Bitmap min
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (min), Intermech.Imbase.Properties.Resources.resourceCulture);
  }

  internal static Bitmap normacs
  {
    get
    {
      return (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (normacs), Intermech.Imbase.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap plus
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (plus), Intermech.Imbase.Properties.Resources.resourceCulture);
  }

  internal static Bitmap Synch
  {
    get => (Bitmap) Intermech.Imbase.Properties.Resources.ResourceManager.GetObject(nameof (Synch), Intermech.Imbase.Properties.Resources.resourceCulture);
  }
}
