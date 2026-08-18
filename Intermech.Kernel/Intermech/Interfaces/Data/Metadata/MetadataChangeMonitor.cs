// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Data.Metadata.MetadataChangeMonitor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Server;
using Intermech.Memoization;
using System;
using System.Threading;


namespace Intermech.Interfaces.Data.Metadata;

public sealed class MetadataChangeMonitor : IMetadataChangeMonitor, IStateMonitor
{
  private IOptionalService<ICacheDataset> serverCacheService;
  private long writerSeqNum;

  public MetadataChangeMonitor(IOptionalService<ICacheDataset> serverCacheService)
  {
    this.serverCacheService = serverCacheService != null ? serverCacheService : throw new ArgumentNullException(nameof (serverCacheService));
  }

  public object WriterSeqNum => (object) this.CheckServerCache();

  public bool AnyWritersSince(object seqNum)
  {
    if (seqNum == null)
      return true;
    long num = this.CheckServerCache();
    return (long) seqNum < num;
  }

  private long CheckServerCache()
  {
    long num1 = Interlocked.Read(ref this.writerSeqNum);
    ICacheDataset cacheDataset = this.serverCacheService.TryGet();
    long num2 = cacheDataset != null ? cacheDataset.ModifyDate.Ticks : num1;
    if (num1 == num2)
      return num1;
    Interlocked.Exchange(ref this.writerSeqNum, num2);
    return num2;
  }
}
