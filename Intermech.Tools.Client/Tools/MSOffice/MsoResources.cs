// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.MsoResources
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
namespace Intermech.Tools.MSOffice;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class MsoResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal MsoResources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (MsoResources.resourceMan == null)
        MsoResources.resourceMan = new ResourceManager("Intermech.Tools.Client.MSOffice.MsoResources", typeof (MsoResources).Assembly);
      return MsoResources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => MsoResources.resourceCulture;
    set => MsoResources.resourceCulture = value;
  }

  internal static Bitmap IR_DOC_16x16
  {
    get
    {
      return (Bitmap) MsoResources.ResourceManager.GetObject(nameof (IR_DOC_16x16), MsoResources.resourceCulture);
    }
  }

  internal static Bitmap IR_DOC_32x32
  {
    get
    {
      return (Bitmap) MsoResources.ResourceManager.GetObject(nameof (IR_DOC_32x32), MsoResources.resourceCulture);
    }
  }

  internal static Bitmap IR_XLS_16x16
  {
    get
    {
      return (Bitmap) MsoResources.ResourceManager.GetObject(nameof (IR_XLS_16x16), MsoResources.resourceCulture);
    }
  }

  internal static Bitmap IR_XLS_32x32
  {
    get
    {
      return (Bitmap) MsoResources.ResourceManager.GetObject(nameof (IR_XLS_32x32), MsoResources.resourceCulture);
    }
  }
}
