
// Type: Intermech.Security.SecurityHolderList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Security;

internal class SecurityHolderList : ArrayList
{
  public SecurityHolderClass this[int index] => base[index] as SecurityHolderClass;

  public bool IsChanged
  {
    get
    {
      bool isChanged = false;
      for (int index = 0; index < this.Count; ++index)
      {
        isChanged = isChanged || this[index].IsChangedFlag;
        if (isChanged)
          break;
      }
      return isChanged;
    }
    set
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].IsChangedFlag = value;
    }
  }
}
