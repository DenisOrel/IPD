
// Type: Intermech.Client.Core.AdvArrayList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Client.Core;

/// <summary>Summary description for AdvArrayList.</summary>
public class AdvArrayList : ArrayList
{
  public void AddList(ArrayList al)
  {
    for (int index = 0; index < al.Count; ++index)
    {
      if (this.IndexOf(al[index]) == -1)
        this.Add(al[index]);
    }
  }

  public void RemoveList(ArrayList al)
  {
    for (int index = 0; index < al.Count; ++index)
      this.Remove(al[index]);
  }
}
