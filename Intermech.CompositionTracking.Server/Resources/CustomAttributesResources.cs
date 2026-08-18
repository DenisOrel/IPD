// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Resources.CustomAttributesResources
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.CompositionTracking.Server.Resources;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class CustomAttributesResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal CustomAttributesResources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (CustomAttributesResources.resourceMan == null)
        CustomAttributesResources.resourceMan = new ResourceManager("Intermech.CompositionTracking.Server.Resources.CustomAttributesResources", typeof (CustomAttributesResources).Assembly);
      return CustomAttributesResources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => CustomAttributesResources.resourceCulture;
    set => CustomAttributesResources.resourceCulture = value;
  }
}
