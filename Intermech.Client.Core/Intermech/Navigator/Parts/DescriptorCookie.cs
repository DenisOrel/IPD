
// Type: Intermech.Navigator.Parts.DescriptorCookie
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Parts;

/// <summary>
/// Реализует уникальный идентификатор дескриптора в составе
/// элемента навигации.
/// </summary>
public class DescriptorCookie : PartCookie
{
  private int descriptorId;

  public DescriptorCookie(int descriptorId) => this.descriptorId = descriptorId;

  public int DescriptorId => this.descriptorId;
}
