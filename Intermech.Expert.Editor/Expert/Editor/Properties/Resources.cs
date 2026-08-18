// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Properties.Resources
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Expert.Editor.Properties;

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
      if (Intermech.Expert.Editor.Properties.Resources.resourceMan == null)
        Intermech.Expert.Editor.Properties.Resources.resourceMan = new ResourceManager("Intermech.Expert.Editor.Properties.Resources", typeof (Intermech.Expert.Editor.Properties.Resources).Assembly);
      return Intermech.Expert.Editor.Properties.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Expert.Editor.Properties.Resources.resourceCulture;
    set => Intermech.Expert.Editor.Properties.Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap _7
  {
    get => (Bitmap) Intermech.Expert.Editor.Properties.Resources.ResourceManager.GetObject("7", Intermech.Expert.Editor.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
  /// </summary>
  internal static Icon ExpObject
  {
    get
    {
      return (Icon) Intermech.Expert.Editor.Properties.Resources.ResourceManager.GetObject(nameof (ExpObject), Intermech.Expert.Editor.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap вставить
  {
    get
    {
      return (Bitmap) Intermech.Expert.Editor.Properties.Resources.ResourceManager.GetObject(nameof (вставить), Intermech.Expert.Editor.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap копировать
  {
    get
    {
      return (Bitmap) Intermech.Expert.Editor.Properties.Resources.ResourceManager.GetObject(nameof (копировать), Intermech.Expert.Editor.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap удалить
  {
    get
    {
      return (Bitmap) Intermech.Expert.Editor.Properties.Resources.ResourceManager.GetObject(nameof (удалить), Intermech.Expert.Editor.Properties.Resources.resourceCulture);
    }
  }
}
