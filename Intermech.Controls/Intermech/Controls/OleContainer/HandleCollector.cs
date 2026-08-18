
// Type: Intermech.Controls.OleContainer.HandleCollector
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Threading;


namespace Intermech.Controls.OleContainer;

internal sealed class HandleCollector
{
  private static int handleTypeCount;
  private static HandleCollector.HandleType[] handleTypes = (HandleCollector.HandleType[]) null;
  private static object internalSyncObject;
  private static int suspendCount;

  internal static event HandleChangeEventHandler HandleAdded;

  internal static event HandleChangeEventHandler HandleRemoved;

  static HandleCollector()
  {
    HandleCollector.handleTypeCount = 0;
    HandleCollector.suspendCount = 0;
    HandleCollector.internalSyncObject = new object();
  }

  internal static IntPtr Add(IntPtr handle, int type)
  {
    HandleCollector.handleTypes[type - 1].Add(handle);
    return handle;
  }

  internal static int RegisterType(string typeName, int expense, int initialThreshold)
  {
    lock (HandleCollector.internalSyncObject)
    {
      if (HandleCollector.handleTypeCount == 0 || HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length)
      {
        HandleCollector.HandleType[] destinationArray = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
        if (HandleCollector.handleTypes != null)
          Array.Copy((Array) HandleCollector.handleTypes, 0, (Array) destinationArray, 0, HandleCollector.handleTypeCount);
        HandleCollector.handleTypes = destinationArray;
      }
      HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
      return HandleCollector.handleTypeCount;
    }
  }

  internal static IntPtr Remove(IntPtr handle, int type)
  {
    return HandleCollector.handleTypes[type - 1].Remove(handle);
  }

  internal static void ResumeCollect()
  {
    bool flag = false;
    lock (typeof (HandleCollector))
    {
      if (HandleCollector.suspendCount > 0)
        --HandleCollector.suspendCount;
      if (HandleCollector.suspendCount == 0)
      {
        for (int index = 0; index < HandleCollector.handleTypeCount; ++index)
        {
          lock (HandleCollector.handleTypes[index])
          {
            if (HandleCollector.handleTypes[index].NeedCollection())
              flag = true;
          }
        }
      }
    }
    if (!flag)
      return;
    GC.Collect();
  }

  internal static void SuspendCollect()
  {
    lock (typeof (HandleCollector))
      ++HandleCollector.suspendCount;
  }

  private class HandleType
  {
    private readonly int deltaPercent;
    private int handleCount;
    private int initialThreshHold;
    internal readonly string name;
    private int threshHold;

    internal HandleType(string name, int expense, int initialThreshHold)
    {
      this.name = name;
      this.initialThreshHold = initialThreshHold;
      this.threshHold = initialThreshHold;
      this.handleCount = 0;
      this.deltaPercent = 100 - expense;
    }

    internal void Add(IntPtr handle)
    {
      if (!(handle != IntPtr.Zero))
        return;
      bool flag = false;
      int currentHandleCount = 0;
      lock (this)
      {
        ++this.handleCount;
        flag = this.NeedCollection();
        currentHandleCount = this.handleCount;
      }
      lock (HandleCollector.internalSyncObject)
      {
        if (HandleCollector.HandleAdded != null)
          HandleCollector.HandleAdded(this.name, handle, currentHandleCount);
      }
      if (!(flag & flag))
        return;
      GC.Collect();
      Thread.Sleep((100 - this.deltaPercent) / 4);
    }

    internal int GetHandleCount()
    {
      lock (this)
        return this.handleCount;
    }

    internal bool NeedCollection()
    {
      if (HandleCollector.suspendCount <= 0)
      {
        if (this.handleCount > this.threshHold)
        {
          this.threshHold = this.handleCount + this.handleCount * this.deltaPercent / 100;
          return true;
        }
        int num = 100 * this.threshHold / (100 + this.deltaPercent);
        if (num >= this.initialThreshHold && this.handleCount < (int) ((double) num * 0.89999997615814209))
          this.threshHold = num;
      }
      return false;
    }

    internal IntPtr Remove(IntPtr handle)
    {
      if (handle != IntPtr.Zero)
      {
        int currentHandleCount = 0;
        lock (this)
        {
          --this.handleCount;
          if (this.handleCount < 0)
            this.handleCount = 0;
          currentHandleCount = this.handleCount;
        }
        lock (HandleCollector.internalSyncObject)
        {
          if (HandleCollector.HandleRemoved != null)
            HandleCollector.HandleRemoved(this.name, handle, currentHandleCount);
        }
      }
      return handle;
    }
  }
}
