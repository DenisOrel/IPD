// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneProgressSinkAdapter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop.LocalServer;
using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class CloneProgressSinkAdapter : SingleThreadedObject, ICloneProgressSink
{
  private CloneDataProxy cloneData;
  private CloneProgressSink cloneProgressSink;

  public CloneProgressSinkAdapter(CloneDataProxy cloneData, CloneProgressSink cloneProgressSink = null)
  {
    this.cloneData = cloneData;
    this.cloneProgressSink = cloneProgressSink;
  }

  bool ICloneProgressSink.ItemCompleted(CloneDataFile fileRawObject)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<CloneDataFile>("ICloneProgressSink.ItemCompleted()", fileRawObject);
    if (fileRawObject != null && this.cloneProgressSink != null)
    {
      CloneDataFileProxy proxy = this.cloneData.TryMapToProxy(fileRawObject);
      if (proxy != null)
        return this.cloneProgressSink.ItemCompleted(proxy);
    }
    return true;
  }
}
