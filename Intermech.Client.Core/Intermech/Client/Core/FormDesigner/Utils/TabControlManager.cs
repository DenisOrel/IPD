
// Type: Intermech.Client.Core.FormDesigner.Utils.TabControlManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Utils;

/// <summary>Класс для хранения данных о закладках на формах.</summary>
public class TabControlManager
{
  private Dictionary<long, Dictionary<string, int>> _cache = new Dictionary<long, Dictionary<string, int>>();

  /// <summary>
  /// Кэш закладок на формах ид.формы - номер закладки (TabControl.SelectedIndex).
  /// </summary>
  public Dictionary<long, Dictionary<string, int>> Cache => this._cache;
}
