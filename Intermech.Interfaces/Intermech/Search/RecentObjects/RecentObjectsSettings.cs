
// Type: Intermech.Search.RecentObjects.RecentObjectsSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.RecentObjects
{
    [Serializable]
    public sealed class RecentObjectsSettings
    {
      public static readonly RecentObjectsSettings Default = new RecentObjectsSettings(10, RecentObjectAction.All);

      public RecentObjectsSettings(
        int recentObjectsMaxCount,
        RecentObjectAction allowableRecentObjectActions)
      {
        this.RecentObjectsMaxCount = recentObjectsMaxCount >= 0 ? recentObjectsMaxCount : throw new ArgumentException();
        this.AllowableRecentObjectActions = allowableRecentObjectActions;
      }

      public int RecentObjectsMaxCount { get; private set; }

      public RecentObjectAction AllowableRecentObjectActions { get; private set; }
    }
}
