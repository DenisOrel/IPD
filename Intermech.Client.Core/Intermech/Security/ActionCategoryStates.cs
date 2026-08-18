
// Type: Intermech.Security.ActionCategoryStates
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Specialized;


namespace Intermech.Security;

internal class ActionCategoryStates
{
  private HybridDictionary hd = new HybridDictionary(4);

  public ActionCategoryStates()
  {
    this.hd.Add((object) ActionCategory.NotDefined, (object) new bool[2]);
    this.hd.Add((object) ActionCategory.Read, (object) new bool[2]);
    this.hd.Add((object) ActionCategory.Write, (object) new bool[2]);
    this.hd.Add((object) ActionCategory.Admin, (object) new bool[2]);
  }

  public bool GetState(ActionCategory ac, bool enabledColumn)
  {
    int index = enabledColumn ? 0 : 1;
    return ((bool[]) this.hd[(object) ac])[index];
  }

  public void SetState(ActionCategory ac, bool enabledColumn, bool cValue)
  {
    int index = enabledColumn ? 0 : 1;
    ((bool[]) this.hd[(object) ac])[index] = cValue;
  }
}
