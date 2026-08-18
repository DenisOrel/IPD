// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Resources.CompositionTracking_ServerResources
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
internal class CompositionTracking_ServerResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal CompositionTracking_ServerResources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (CompositionTracking_ServerResources.resourceMan == null)
        CompositionTracking_ServerResources.resourceMan = new ResourceManager("Intermech.CompositionTracking.Server.Resources.CompositionTracking.ServerResources", typeof (CompositionTracking_ServerResources).Assembly);
      return CompositionTracking_ServerResources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => CompositionTracking_ServerResources.resourceCulture;
    set => CompositionTracking_ServerResources.resourceCulture = value;
  }

  internal static string CompositionTracking_LcStepAmbiguous
  {
    get
    {
      return CompositionTracking_ServerResources.ResourceManager.GetString("CompositionTracking.LcStepAmbiguous", CompositionTracking_ServerResources.resourceCulture);
    }
  }

  internal static string CompositionTracking_LcStepNoFound
  {
    get
    {
      return CompositionTracking_ServerResources.ResourceManager.GetString("CompositionTracking.LcStepNoFound", CompositionTracking_ServerResources.resourceCulture);
    }
  }

  internal static string CompositionTracking_Server_1
  {
    get
    {
      return CompositionTracking_ServerResources.ResourceManager.GetString("CompositionTracking.Server_1", CompositionTracking_ServerResources.resourceCulture);
    }
  }

  internal static string CompositionTracking_Server_2
  {
    get
    {
      return CompositionTracking_ServerResources.ResourceManager.GetString("CompositionTracking.Server_2", CompositionTracking_ServerResources.resourceCulture);
    }
  }

  internal static string CompositionTracking_Server_3
  {
    get
    {
      return CompositionTracking_ServerResources.ResourceManager.GetString("CompositionTracking.Server_3", CompositionTracking_ServerResources.resourceCulture);
    }
  }
}
