// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRP2Resources
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class MRP2Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal MRP2Resources()
  {
  }

  /// <summary>
  ///   Returns the cached ResourceManager instance used by this class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static ResourceManager ResourceManager
  {
    get
    {
      if (MRP2Resources.resourceMan == null)
        MRP2Resources.resourceMan = new ResourceManager("Intermech.Interfaces.MRP.MRP2.MRP2Resources", typeof (MRP2Resources).Assembly);
      return MRP2Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static CultureInfo Culture
  {
    get => MRP2Resources.resourceCulture;
    set => MRP2Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  public static Bitmap MRP2Added
  {
    get
    {
      return (Bitmap) MRP2Resources.ResourceManager.GetObject(nameof (MRP2Added), MRP2Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  public static Bitmap MRP2Copied
  {
    get
    {
      return (Bitmap) MRP2Resources.ResourceManager.GetObject(nameof (MRP2Copied), MRP2Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  public static Bitmap MRP2Deleted
  {
    get
    {
      return (Bitmap) MRP2Resources.ResourceManager.GetObject(nameof (MRP2Deleted), MRP2Resources.resourceCulture);
    }
  }
}
