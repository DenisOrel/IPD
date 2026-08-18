
// Type: Intermech.Navigator.Controls.GlobalIndexSearchValueClientHelpers
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

public static class GlobalIndexSearchValueClientHelpers
{
  public static GlobalIndexSearchValue GetDefaultGlobalIndexSearchValue()
  {
    return new GlobalIndexSearchValue(string.Empty, GlobalIndexSearchValueClientHelpers.GetDefaultGlobalIndexSearchOptions(), new List<string>());
  }

  private static GlobalIndexSearchOptions GetDefaultGlobalIndexSearchOptions()
  {
    return !GlobalIndexSearchValueClientHelpers.AllowSubstringSearch() ? GlobalIndexSearchOptions.None : GlobalIndexSearchOptions.SubstringSearch;
  }

  private static bool AllowSubstringSearch()
  {
    switch (SearchInIndexSubstringGettingModeHelper.GetSearchInIndexSubstringGettingMode())
    {
      case SearchInIndexSubstringGettingMode.No:
        return false;
      case SearchInIndexSubstringGettingMode.KeepUsersChoice:
        return UISettings.SearchInIndexSubstring;
      default:
        return true;
    }
  }
}
