
// Type: Intermech.Client.Core.Show.Net.ShowSetting
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Drawing;


namespace Intermech.Client.Core.Show.Net;

/// <summary> установки по умолчанию</summary>
public class ShowSetting
{
  private static Hashtable _settings = new Hashtable();

  static ShowSetting()
  {
    ShowSetting._settings.Add((object) "DefaultUnits", (object) GraphicsUnit.Millimeter);
    ShowSetting._settings.Add((object) "DefaultWeight", (object) 0.1f);
  }

  /// <summary> Таблица установок по умолчанию </summary>
  public static IDictionary Settings => (IDictionary) ShowSetting._settings;
}
