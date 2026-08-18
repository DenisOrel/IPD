
// Type: Intermech.Security.CycleControlArrayList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Security;

internal class CycleControlArrayList : ArrayList
{
  public CycleControlClass this[int index] => base[index] as CycleControlClass;

  public int Find(CycleControlClass cycleControlClass)
  {
    int num = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].Equals((object) cycleControlClass))
      {
        num = index;
        break;
      }
    }
    return num;
  }
}
