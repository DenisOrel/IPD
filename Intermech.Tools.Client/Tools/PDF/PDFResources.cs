// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFResources
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.PDF;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class PDFResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal PDFResources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (PDFResources.resourceMan == null)
        PDFResources.resourceMan = new ResourceManager("Intermech.Tools.Client.PDF.PDFResources", typeof (PDFResources).Assembly);
      return PDFResources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PDFResources.resourceCulture;
    set => PDFResources.resourceCulture = value;
  }

  internal static Bitmap IR_PDF_16x16
  {
    get
    {
      return (Bitmap) PDFResources.ResourceManager.GetObject(nameof (IR_PDF_16x16), PDFResources.resourceCulture);
    }
  }

  internal static Bitmap IR_PDF_32x32
  {
    get
    {
      return (Bitmap) PDFResources.ResourceManager.GetObject(nameof (IR_PDF_32x32), PDFResources.resourceCulture);
    }
  }
}
