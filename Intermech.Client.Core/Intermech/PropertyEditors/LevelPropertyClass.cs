
// Type: Intermech.PropertyEditors.LevelPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Holders;


namespace Intermech.PropertyEditors;

public class LevelPropertyClass
{
  private int level;

  public int Level => this.level;

  public LevelPropertyClass(int aLevelID) => this.level = aLevelID;

  public override string ToString()
  {
    if (this.Level == 0)
      return CoreConsts.AnyLevel;
    return this.Level == -1 ? string.Empty : DataHolders.LevelsHolder.GetNamebyID(this.Level);
  }
}
