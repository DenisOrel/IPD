
// Type: Intermech.Search.RecentObjects.IRecentObjectsClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.RecentObjects;

public interface IRecentObjectsClientService
{
  void AddToCurrentUserRecentObjects(long[] objectVersionIds);

  void ChangeRecentObjectsAccessSettings();

  void ClearCurrentUserRecentObjects();

  long[] GetCurrentUserRecentObjects();

  void OpenOtherUserRecentObjects();

  void RemoveFromCurrentUserRecentObjects(long[] objectVersionIds);

  RecentObjectsSettings GetCurrentUserRecentObjectsSettings();

  void SetCurrentUserRecentObjectsSettings(RecentObjectsSettings recentObjectsSettings);
}
