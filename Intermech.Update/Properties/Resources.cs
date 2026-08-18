// Decompiled with JetBrains decompiler
// Type: Intermech.Update.Properties.Resources
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Update.Properties;

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
      if (Intermech.Update.Properties.Resources.resourceMan == null)
        Intermech.Update.Properties.Resources.resourceMan = new ResourceManager("Intermech.Update.Properties.Resources", typeof (Intermech.Update.Properties.Resources).Assembly);
      return Intermech.Update.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Update.Properties.Resources.resourceCulture;
    set => Intermech.Update.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap Copy
  {
    get => (Bitmap) Intermech.Update.Properties.Resources.ResourceManager.GetObject(nameof (Copy), Intermech.Update.Properties.Resources.resourceCulture);
  }

  internal static Bitmap ScriptNew
  {
    get
    {
      return (Bitmap) Intermech.Update.Properties.Resources.ResourceManager.GetObject(nameof (ScriptNew), Intermech.Update.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap ScriptOpen
  {
    get
    {
      return (Bitmap) Intermech.Update.Properties.Resources.ResourceManager.GetObject(nameof (ScriptOpen), Intermech.Update.Properties.Resources.resourceCulture);
    }
  }
}
