// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.Properties.Resources
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.CADExtensions.Properties;

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
      if (Intermech.Tools.CADExtensions.Properties.Resources.resourceMan == null)
        Intermech.Tools.CADExtensions.Properties.Resources.resourceMan = new ResourceManager("Intermech.Tools.CADExtensions.Properties.Resources", typeof (Intermech.Tools.CADExtensions.Properties.Resources).Assembly);
      return Intermech.Tools.CADExtensions.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Tools.CADExtensions.Properties.Resources.resourceCulture;
    set => Intermech.Tools.CADExtensions.Properties.Resources.resourceCulture = value;
  }

  internal static string ModelHasNoDrawingFile
  {
    get
    {
      return Intermech.Tools.CADExtensions.Properties.Resources.ResourceManager.GetString(nameof (ModelHasNoDrawingFile), Intermech.Tools.CADExtensions.Properties.Resources.resourceCulture);
    }
  }
}
