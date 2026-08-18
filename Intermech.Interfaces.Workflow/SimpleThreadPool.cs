// Decompiled with JetBrains decompiler
// Type: Intermech.SimpleThreadPool
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech;

public class SimpleThreadPool
{
  public readonly int MaxThreads;
  private object _lock = new object();
  private int _activeThreads;
  private LinkedList<Action> _queue = new LinkedList<Action>();

  public event EventHandler AllCompleted;

  public SimpleThreadPool(int MaxThreads) => this.MaxThreads = MaxThreads;

  public void Enqueue(Action action)
  {
    lock (this._lock)
    {
      if (this._activeThreads < this.MaxThreads)
        this.Start(action);
      else
        this._queue.AddLast(action);
    }
  }

  private void Start(Action action)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
    backgroundWorker.DoWork += (DoWorkEventHandler) ((o, args) => action());
    ++this._activeThreads;
    backgroundWorker.RunWorkerAsync();
  }

  private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    lock (this._lock)
    {
      --this._activeThreads;
      if (this._activeThreads < this.MaxThreads && this._queue.Count > 0)
      {
        Action action = this._queue.First.Value;
        this._queue.RemoveFirst();
        this.Start(action);
      }
      else
      {
        if (this._activeThreads != 0 || this._queue.Count != 0)
          return;
        EventHandler allCompleted = this.AllCompleted;
        if (allCompleted == null)
          return;
        allCompleted((object) this, (EventArgs) null);
      }
    }
  }
}
