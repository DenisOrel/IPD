// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.CompositionObjectInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Kernel.Services.Compositions;

internal class CompositionObjectInfo
{
  protected long _objectID;
  protected CompositionSortingInfoCache<CompositionSortingInfoItem> _compositionInfoCache;
  protected Dictionary<int, Dictionary<int, long>> _sortingCache;

  public CompositionObjectInfo(
    long objectId,
    [NotNull] ICompositionSortingComparer<CompositionSortingInfoItem> comparer)
  {
    this._compositionInfoCache = new CompositionSortingInfoCache<CompositionSortingInfoItem>(comparer);
    this._sortingCache = new Dictionary<int, Dictionary<int, long>>();
    this._objectID = objectId;
  }

  public CompositionSortingInfoCache<CompositionSortingInfoItem> CompositionInfoCache
  {
    [DebuggerStepThrough] get => this._compositionInfoCache;
  }

  public Dictionary<int, Dictionary<int, long>> SortingCache
  {
    [DebuggerStepThrough] get => this._sortingCache;
  }
}
