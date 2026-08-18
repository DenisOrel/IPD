
// Type: Intermech.Search.RecentObjects.RecentObjectsChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsChangedEventArgs : NotificationEventArgs
{
  public RecentObjectsChangedEventArgs(long[] addedRecentObjects, long[] removedRecentObjects)
    : base("RecentObjectsChanged")
  {
    if (addedRecentObjects == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) addedRecentObjects))
      throw new ArgumentException();
    if (removedRecentObjects == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) removedRecentObjects))
      throw new ArgumentException();
    this.AddedRecentObjects = addedRecentObjects;
    this.RemovedRecentObjects = removedRecentObjects;
  }

  public long[] AddedRecentObjects { get; private set; }

  public long[] RemovedRecentObjects { get; private set; }
}
