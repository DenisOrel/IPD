// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsResource
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class AnalogsResource
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal AnalogsResource()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (AnalogsResource.resourceMan == null)
        AnalogsResource.resourceMan = new ResourceManager("Intermech.Pdm.Server.Intermech.Search.Pdm.Analogs.AnalogsResource", typeof (AnalogsResource).Assembly);
      return AnalogsResource.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => AnalogsResource.resourceCulture;
    set => AnalogsResource.resourceCulture = value;
  }

  internal static Bitmap ActingAnalog
  {
    get
    {
      return (Bitmap) AnalogsResource.ResourceManager.GetObject(nameof (ActingAnalog), AnalogsResource.resourceCulture);
    }
  }

  internal static Bitmap Analog
  {
    get
    {
      return (Bitmap) AnalogsResource.ResourceManager.GetObject(nameof (Analog), AnalogsResource.resourceCulture);
    }
  }

  internal static Bitmap AnalogsExist
  {
    get
    {
      return (Bitmap) AnalogsResource.ResourceManager.GetObject(nameof (AnalogsExist), AnalogsResource.resourceCulture);
    }
  }

  internal static Bitmap PriorityAnalog
  {
    get
    {
      return (Bitmap) AnalogsResource.ResourceManager.GetObject(nameof (PriorityAnalog), AnalogsResource.resourceCulture);
    }
  }
}
