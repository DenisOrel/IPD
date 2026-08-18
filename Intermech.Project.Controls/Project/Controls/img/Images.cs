// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.img.Images
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Controls.img;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Images
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Images()
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
      if (Images.resourceMan == null)
        Images.resourceMan = new ResourceManager("Intermech.Project.Controls.img.Images", typeof (Images).Assembly);
      return Images.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Images.resourceCulture;
    set => Images.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap PrintEntireProject
  {
    get
    {
      return (Bitmap) Images.ResourceManager.GetObject(nameof (PrintEntireProject), Images.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap PrintSelectedDates
  {
    get
    {
      return (Bitmap) Images.ResourceManager.GetObject(nameof (PrintSelectedDates), Images.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap PrintSelectedPages
  {
    get
    {
      return (Bitmap) Images.ResourceManager.GetObject(nameof (PrintSelectedPages), Images.resourceCulture);
    }
  }
}
