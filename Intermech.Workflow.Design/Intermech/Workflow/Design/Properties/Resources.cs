// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.Properties.Resources
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow.Design.Properties;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
      if (Intermech.Workflow.Design.Properties.Resources.resourceMan == null)
        Intermech.Workflow.Design.Properties.Resources.resourceMan = new ResourceManager("Intermech.Workflow.Design.Properties.Resources", typeof (Intermech.Workflow.Design.Properties.Resources).Assembly);
      return Intermech.Workflow.Design.Properties.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Workflow.Design.Properties.Resources.resourceCulture;
    set => Intermech.Workflow.Design.Properties.Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized resource of type System.Drawing.Bitmap.
  /// </summary>
  internal static Bitmap refresh
  {
    get
    {
      return (Bitmap) Intermech.Workflow.Design.Properties.Resources.ResourceManager.GetObject(nameof (refresh), Intermech.Workflow.Design.Properties.Resources.resourceCulture);
    }
  }
}
