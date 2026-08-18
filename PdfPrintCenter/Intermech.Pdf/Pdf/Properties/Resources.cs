// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Properties.Resources
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Syncfusion.Pdf.Properties;

[CompilerGenerated]
[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
internal class Resources
{
  private static CultureInfo resourceCulture;
  private static ResourceManager resourceMan;

  internal Resources()
  {
  }

  internal static byte[] cmap
  {
    get => (byte[]) Syncfusion.Pdf.Properties.Resources.ResourceManager.GetObject(nameof (cmap), Syncfusion.Pdf.Properties.Resources.resourceCulture);
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Syncfusion.Pdf.Properties.Resources.resourceCulture;
    set => Syncfusion.Pdf.Properties.Resources.resourceCulture = value;
  }

  internal static byte[] name
  {
    get => (byte[]) Syncfusion.Pdf.Properties.Resources.ResourceManager.GetObject(nameof (name), Syncfusion.Pdf.Properties.Resources.resourceCulture);
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Syncfusion.Pdf.Properties.Resources.resourceMan == null)
        Syncfusion.Pdf.Properties.Resources.resourceMan = new ResourceManager("Syncfusion.Pdf.Properties.Resources", typeof (Syncfusion.Pdf.Properties.Resources).Assembly);
      return Syncfusion.Pdf.Properties.Resources.resourceMan;
    }
  }

  internal static StreamReader standard_encoding
  {
    get
    {
      return (StreamReader) Syncfusion.Pdf.Properties.Resources.ResourceManager.GetObject(nameof (standard_encoding), Syncfusion.Pdf.Properties.Resources.resourceCulture);
    }
  }
}
