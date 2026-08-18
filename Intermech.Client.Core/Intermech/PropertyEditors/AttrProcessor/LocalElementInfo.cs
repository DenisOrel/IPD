
// Type: Intermech.PropertyEditors.AttrProcessor.LocalElementInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors.AttrProcessor;

internal class LocalElementInfo : IElementInfo
{
  private AttributableElements attributableElements;
  private long id;

  /// <summary>Конструктор.</summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElements"></param>
  public LocalElementInfo(long aId, AttributableElements aAttributableElements)
  {
    this.id = aId;
    this.attributableElements = aAttributableElements;
  }

  /// <summary>
  /// 
  /// </summary>
  public AttributableElements ElementKind => this.attributableElements;

  /// <summary>
  /// 
  /// </summary>
  public long ElementIdentifier => this.id;
}
