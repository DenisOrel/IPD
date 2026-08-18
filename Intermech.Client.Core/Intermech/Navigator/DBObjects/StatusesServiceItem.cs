
// Type: Intermech.Navigator.DBObjects.StatusesServiceItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

internal class StatusesServiceItem
{
  private Dictionary<int, string> descriptions = new Dictionary<int, string>();
  private Dictionary<int, Image> images = new Dictionary<int, Image>();
  private Dictionary<int, Font> fonts = new Dictionary<int, Font>();

  public void Add(int stateValue, Image image) => this.images.Add(stateValue, image);

  public void Add(int stateValue, string description)
  {
    this.descriptions.Add(stateValue, description);
  }

  public void Add(int stateValue, Font font) => this.fonts.Add(stateValue, font);

  public Image GetIcon(int stateValue)
  {
    Image icon;
    this.images.TryGetValue(stateValue, out icon);
    return icon;
  }

  public string GetDescription(int stateValue)
  {
    string str;
    this.descriptions.TryGetValue(stateValue, out str);
    return str ?? string.Empty;
  }

  public Font GetFont(int stateValue)
  {
    Font font;
    this.fonts.TryGetValue(stateValue, out font);
    return font;
  }
}
