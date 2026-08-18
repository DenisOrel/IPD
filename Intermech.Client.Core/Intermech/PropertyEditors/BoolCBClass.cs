
// Type: Intermech.PropertyEditors.BoolCBClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.PropertyEditors;

internal class BoolCBClass
{
  private bool flag;

  public bool Flag => this.flag;

  public BoolCBClass(bool b) => this.flag = b;

  public override string ToString()
  {
    return this.flag ? LocalizationHolder.rm.GetString("Client.Core_89") : LocalizationHolder.rm.GetString("Client.Core_90");
  }
}
