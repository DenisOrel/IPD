// Decompiled with JetBrains decompiler
// Type: Intermech.AbortableBackgroundWorker
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Threading;

#nullable disable
namespace Intermech;

public class AbortableBackgroundWorker : BackgroundWorker
{
  private Thread workerThread;

  protected override void OnDoWork(DoWorkEventArgs e)
  {
    this.workerThread = Thread.CurrentThread;
    try
    {
      base.OnDoWork(e);
    }
    catch (ThreadAbortException ex)
    {
      e.Cancel = true;
      Thread.ResetAbort();
    }
  }

  public void Abort()
  {
    if (this.workerThread == null)
      return;
    this.workerThread.Abort();
    this.workerThread = (Thread) null;
  }
}
