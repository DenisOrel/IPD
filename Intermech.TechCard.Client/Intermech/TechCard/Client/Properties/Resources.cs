// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Properties.Resources
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.TechCard.Client.Properties;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
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

  /// <summary>
  ///   Returns the cached ResourceManager instance used by this class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Intermech.TechCard.Client.Properties.Resources.resourceMan == null)
        Intermech.TechCard.Client.Properties.Resources.resourceMan = new ResourceManager("Intermech.TechCard.Client.Properties.Resources", typeof (Intermech.TechCard.Client.Properties.Resources).Assembly);
      return Intermech.TechCard.Client.Properties.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.TechCard.Client.Properties.Resources.resourceCulture;
    set => Intermech.TechCard.Client.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap GrayEmpty
  {
    get
    {
      return (Bitmap) Intermech.TechCard.Client.Properties.Resources.ResourceManager.GetObject(nameof (GrayEmpty), Intermech.TechCard.Client.Properties.Resources.resourceCulture);
    }
  }
}
