
// Type: Intermech.Search.SearchHistory.SearchHistoryItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.SearchHistory;

public sealed class SearchHistoryItem
{
  public string SearchString { get; set; }

  public DateTime SearchDateTime { get; set; }

  public long UserVersionID { get; set; }

  public int SecurityLevel { get; set; }
}
