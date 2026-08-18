// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.RemotingDispatchTable
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Concurrent;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class RemotingDispatchTable
{
  private ConcurrentDictionary<string, RemotingDispatchTable.MapRecord> mapTable;
  private Func<string, RemotingDispatchTable.MapRecord> mapRecordFactory;

  public RemotingDispatchTable()
  {
    this.mapTable = new ConcurrentDictionary<string, RemotingDispatchTable.MapRecord>();
    this.mapRecordFactory = new Func<string, RemotingDispatchTable.MapRecord>(RemotingDispatchTable.CreateEmptyMapRecord);
  }

  private static RemotingDispatchTable.MapRecord CreateEmptyMapRecord(string clientThreadKey)
  {
    return new RemotingDispatchTable.MapRecord();
  }

  public RemotingDispatchInfo TryMapClientThread(RemotingDispatchInfo dispatchInfo)
  {
    return this.mapTable.GetOrAdd(dispatchInfo.ClientThreadKey, this.mapRecordFactory).TrySetDispatchInfo(dispatchInfo);
  }

  public void UnmapClientThread(RemotingDispatchInfo dispatchInfo)
  {
    RemotingDispatchTable.MapRecord mapRecord;
    if (!this.mapTable.TryGetValue(dispatchInfo.ClientThreadKey, out mapRecord))
      return;
    mapRecord.TryResetDispatchInfo(dispatchInfo);
  }

  private sealed class MapRecord
  {
    private RemotingDispatchInfo dispatchInfo;

    public RemotingDispatchInfo TrySetDispatchInfo(RemotingDispatchInfo dispatchInfo)
    {
      return Interlocked.CompareExchange<RemotingDispatchInfo>(ref this.dispatchInfo, dispatchInfo, (RemotingDispatchInfo) null) ?? dispatchInfo;
    }

    public bool TryResetDispatchInfo(RemotingDispatchInfo dispatchInfo)
    {
      return Interlocked.CompareExchange<RemotingDispatchInfo>(ref this.dispatchInfo, (RemotingDispatchInfo) null, dispatchInfo) == dispatchInfo;
    }
  }
}
