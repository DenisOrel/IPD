
// Type: Intermech.Navigator.Parts.PartCookie
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Parts;

/// <summary>
/// Реализует уникальный идентификатор части в составе
/// элемента навигации.
/// </summary>
public class PartCookie
{
  private int partId;

  public PartCookie()
  {
  }

  public PartCookie(int partId) => this.partId = partId;

  public int PartId
  {
    get => this.partId;
    set => this.partId = value;
  }
}
