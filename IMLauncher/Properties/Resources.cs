// Decompiled with JetBrains decompiler
// Type: IMLauncher.Properties.Resources
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace IMLauncher.Properties;

[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
      if (IMLauncher.Properties.Resources.resourceMan == null)
        IMLauncher.Properties.Resources.resourceMan = new ResourceManager("IMLauncher.Properties.Resources", typeof (IMLauncher.Properties.Resources).Assembly);
      return IMLauncher.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => IMLauncher.Properties.Resources.resourceCulture;
    set => IMLauncher.Properties.Resources.resourceCulture = value;
  }
}
