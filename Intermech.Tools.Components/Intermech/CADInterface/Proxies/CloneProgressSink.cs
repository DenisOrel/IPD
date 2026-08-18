// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneProgressSink
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class CloneProgressSink
{
  public bool ItemCompleted(CloneDataFileProxy file)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CloneDataFileProxy>("CloneProgressSink.ItemCompleted()", file);
    return file != null ? this.DoItemCompleted(file) : throw new ArgumentNullException(nameof (file));
  }

  protected virtual bool DoItemCompleted(CloneDataFileProxy file)
  {
    if (this.OnItemCompleted == null)
      return true;
    CloneDataFileCompletedEventArgs e = new CloneDataFileCompletedEventArgs(file);
    this.OnItemCompleted((object) this, e);
    return e.Result;
  }

  public event EventHandler<CloneDataFileCompletedEventArgs> OnItemCompleted;
}
