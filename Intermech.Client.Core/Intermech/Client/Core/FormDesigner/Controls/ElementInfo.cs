
// Type: Intermech.Client.Core.FormDesigner.Controls.ElementInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class ElementInfo : IElementInfo
{
  /// <summary>Конструктор.</summary>
  /// <param name="id"></param>
  /// <param name="kind"></param>
  public ElementInfo(long id, AttributableElements kind)
  {
    this.ElementIdentifier = id;
    this.ElementKind = kind;
  }

  /// <summary>
  /// 
  /// </summary>
  public long ElementIdentifier { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public AttributableElements ElementKind { get; private set; }
}
