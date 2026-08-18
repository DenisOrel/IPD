// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Properties.Resources
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.DatabaseConfigurator.Properties;

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
      if (Intermech.DatabaseConfigurator.Properties.Resources.resourceMan == null)
        Intermech.DatabaseConfigurator.Properties.Resources.resourceMan = new ResourceManager("Intermech.DatabaseConfigurator.Properties.Resources", typeof (Intermech.DatabaseConfigurator.Properties.Resources).Assembly);
      return Intermech.DatabaseConfigurator.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.DatabaseConfigurator.Properties.Resources.resourceCulture;
    set => Intermech.DatabaseConfigurator.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap file_delete
  {
    get
    {
      return (Bitmap) Intermech.DatabaseConfigurator.Properties.Resources.ResourceManager.GetObject(nameof (file_delete), Intermech.DatabaseConfigurator.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap search1
  {
    get
    {
      return (Bitmap) Intermech.DatabaseConfigurator.Properties.Resources.ResourceManager.GetObject(nameof (search1), Intermech.DatabaseConfigurator.Properties.Resources.resourceCulture);
    }
  }
}
