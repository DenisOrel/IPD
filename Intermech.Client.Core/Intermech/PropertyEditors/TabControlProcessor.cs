
// Type: Intermech.PropertyEditors.TabControlProcessor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for TabControlProcessor.</summary>
public class TabControlProcessor
{
  private static bool _block;

  public static bool BlockTabPageChangedEvent
  {
    get => TabControlProcessor._block;
    set => TabControlProcessor._block = value;
  }

  public static void Clear(TabControl tabControl)
  {
    while (tabControl.TabCount > 0)
      tabControl.TabPages.RemoveAt(tabControl.TabPages.Count - 1);
  }

  public static void AssignTabPages(TabControl tabControl, params object[] args)
  {
    TabControlProcessor._block = true;
    try
    {
      if (args.Length == 0)
      {
        TabControlProcessor.Clear(tabControl);
      }
      else
      {
        TabControlProcessor.Clear(tabControl);
        ArrayList arrayList = new ArrayList();
        for (int index = 0; index < args.Length; ++index)
          arrayList.Add(args[index]);
        for (int index = 0; index < arrayList.Count; ++index)
        {
          if (tabControl.TabPages.IndexOf((TabPage) arrayList[index]) == -1)
            tabControl.TabPages.Add((TabPage) arrayList[index]);
        }
        int index1 = 0;
        while (index1 < tabControl.TabCount)
        {
          if (arrayList.IndexOf((object) tabControl.TabPages[index1]) == -1)
            tabControl.TabPages.Remove(tabControl.TabPages[index1]);
          else
            ++index1;
        }
        tabControl.Refresh();
      }
    }
    finally
    {
      TabControlProcessor._block = false;
    }
  }

  public static void AddTabPages(TabControl tabControl, params object[] args)
  {
    TabControlProcessor._block = true;
    try
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < args.Length; ++index)
        arrayList.Add(args[index]);
      for (int index = 0; index < arrayList.Count; ++index)
      {
        if (tabControl.TabPages.IndexOf((TabPage) arrayList[index]) == -1)
          tabControl.TabPages.Add((TabPage) arrayList[index]);
      }
      tabControl.Refresh();
    }
    finally
    {
      TabControlProcessor._block = false;
    }
  }
}
