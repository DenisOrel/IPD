
// Type: Intermech.PropertyEditors.TabPagesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for TabPages.</summary>
public class TabPagesHolder
{
  private static Hashtable tabPages = new Hashtable();

  public static Intermech.PropertyEditors.TabPages TabPages(Guid instGuid)
  {
    return (Intermech.PropertyEditors.TabPages) TabPagesHolder.tabPages[(object) instGuid];
  }

  public static void RegisterTabPages(Guid instGuid)
  {
    TabPagesHolder.tabPages.Add((object) instGuid, (object) new Intermech.PropertyEditors.TabPages(instGuid));
  }

  public static void UnregisterTabPages(Guid instGuid)
  {
    Intermech.PropertyEditors.TabPages tabPages = TabPagesHolder.TabPages(instGuid);
    TabPagesHolder.tabPages.Remove((object) instGuid);
    tabPages?.Dispose();
  }
}
