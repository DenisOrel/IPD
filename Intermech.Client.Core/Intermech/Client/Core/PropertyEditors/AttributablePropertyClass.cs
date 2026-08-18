
// Type: Intermech.Client.Core.PropertyEditors.AttributablePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;


namespace Intermech.Client.Core.PropertyEditors;

public class AttributablePropertyClass : ObjectPropertyClass
{
  /// <summary>
  /// 
  /// </summary>
  public IElementInfo ElementInfo;
  /// <summary>
  /// 
  /// </summary>
  public int AttributeId;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="elementInfo"></param>
  /// <param name="attributeId"></param>
  /// <param name="objectId"></param>
  /// <param name="caption"></param>
  public AttributablePropertyClass(
    IElementInfo elementInfo,
    int attributeId,
    long objectId,
    string caption = null)
    : base(objectId, caption)
  {
    this.ElementInfo = elementInfo;
    this.AttributeId = attributeId;
  }
}
