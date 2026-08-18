// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SearchInIndexSubstringGettingModeHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

public static class SearchInIndexSubstringGettingModeHelper
{
  private static bool _inited;
  private static SearchInIndexSubstringGettingMode _mode;

  public static SearchInIndexSubstringGettingMode GetSearchInIndexSubstringGettingMode()
  {
    if (!SearchInIndexSubstringGettingModeHelper._inited)
    {
      if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service)
        SearchInIndexSubstringGettingModeHelper._mode = (SearchInIndexSubstringGettingMode) service.ReadInteger("System", "DatabaseProperties", "SearchInIndSubstrGet", 0L, DBConfigMode.GlobalOnly);
      SearchInIndexSubstringGettingModeHelper._inited = true;
    }
    return SearchInIndexSubstringGettingModeHelper._mode;
  }

  public static void SetSearchInIndexSubstringGettingMode(SearchInIndexSubstringGettingMode mode)
  {
    (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).WriteInteger("System", "DatabaseProperties", "SearchInIndSubstrGet", (long) mode, 0L);
    SearchInIndexSubstringGettingModeHelper._mode = mode;
  }
}
