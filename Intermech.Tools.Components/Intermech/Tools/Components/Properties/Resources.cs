// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Components.Properties.Resources
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Components.Properties;

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
      if (Intermech.Tools.Components.Properties.Resources.resourceMan == null)
        Intermech.Tools.Components.Properties.Resources.resourceMan = new ResourceManager("Intermech.Tools.Components.Properties.Resources", typeof (Intermech.Tools.Components.Properties.Resources).Assembly);
      return Intermech.Tools.Components.Properties.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Tools.Components.Properties.Resources.resourceCulture;
    set => Intermech.Tools.Components.Properties.Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap document_add
  {
    get
    {
      return (Bitmap) Intermech.Tools.Components.Properties.Resources.ResourceManager.GetObject(nameof (document_add), Intermech.Tools.Components.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap document_delete
  {
    get
    {
      return (Bitmap) Intermech.Tools.Components.Properties.Resources.ResourceManager.GetObject(nameof (document_delete), Intermech.Tools.Components.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap refresh
  {
    get
    {
      return (Bitmap) Intermech.Tools.Components.Properties.Resources.ResourceManager.GetObject(nameof (refresh), Intermech.Tools.Components.Properties.Resources.resourceCulture);
    }
  }
}
