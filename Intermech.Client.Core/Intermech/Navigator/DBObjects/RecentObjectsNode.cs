
// Type: Intermech.Navigator.DBObjects.RecentObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный узел, реализующий элемент навигации для недавних объектов
/// </summary>
public sealed class RecentObjectsNode
{
  /// <summary>Объект для синхронизации доступа к статическим полям</summary>
  private static object SyncRoot = new object();
  /// <summary>Ссылка на службу недавних объектов</summary>
  private static IRecentObjectsService _recentObjectsService;

  /// <summary>Ссылка на службу недавних объектов</summary>
  public static IRecentObjectsService MRUObjects
  {
    get
    {
      lock (RecentObjectsNode.SyncRoot)
      {
        if (RecentObjectsNode._recentObjectsService != null)
          return RecentObjectsNode._recentObjectsService;
        RecentObjectsNode._recentObjectsService = ServicesManager.GetService(typeof (IRecentObjectsService)) as IRecentObjectsService;
        if (RecentObjectsNode._recentObjectsService == null)
          RecentObjectsService.StartService();
        RecentObjectsNode._recentObjectsService = ServicesManager.GetService(typeof (IRecentObjectsService)) as IRecentObjectsService;
        return RecentObjectsNode._recentObjectsService;
      }
    }
  }
}
