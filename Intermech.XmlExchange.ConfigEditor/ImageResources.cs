// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImageResources
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class ImageResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal ImageResources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (ImageResources.resourceMan == null)
        ImageResources.resourceMan = new ResourceManager("Intermech.XmlExchange.ConfigEditor.ImageResources", typeof (ImageResources).Assembly);
      return ImageResources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => ImageResources.resourceCulture;
    set => ImageResources.resourceCulture = value;
  }

  internal static Icon configEditor
  {
    get
    {
      return (Icon) ImageResources.ResourceManager.GetObject(nameof (configEditor), ImageResources.resourceCulture);
    }
  }

  internal static Icon exportApplSettings
  {
    get
    {
      return (Icon) ImageResources.ResourceManager.GetObject(nameof (exportApplSettings), ImageResources.resourceCulture);
    }
  }

  internal static Icon importImbaseSettings
  {
    get
    {
      return (Icon) ImageResources.ResourceManager.GetObject(nameof (importImbaseSettings), ImageResources.resourceCulture);
    }
  }

  internal static Icon importMatchingTypes
  {
    get
    {
      return (Icon) ImageResources.ResourceManager.GetObject(nameof (importMatchingTypes), ImageResources.resourceCulture);
    }
  }
}
