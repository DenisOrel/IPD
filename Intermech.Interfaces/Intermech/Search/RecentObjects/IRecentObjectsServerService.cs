
// Type: Intermech.Search.RecentObjects.IRecentObjectsServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.RecentObjects
{
    public interface IRecentObjectsServerService
    {
      long[] GetCurrentUserRecentObjects(Guid userSessionGuid);

      long[] GetOtherUserRecentObjects(Guid userSessionGuid, long userVersionID);

      long[] GetRecentObjectsAccessSettings(Guid userSessionGuid);

      void SetRecentObjectsAccessSettings(Guid userSessionGuid, long[] objectVersionIds);

      RecentObjectsSettings GetCurrentUserRecentObjectsSettings(Guid userSessionGuid);

      void SetCurrentUserRecentObjectsSettings(
        Guid userSessionGuid,
        RecentObjectsSettings recentObjectsSettings);

      void SaveCurrentUserRecentObjects(Guid userSessionGuid, long[] objectVersionIds);
    }
}
