
// Type: Intermech.PropertyEditors.AttributeValuePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for AttributeValueEditor.</summary>
public class AttributeValuePropertyClass
{
  private AttributeValues attributeValue;

  public AttributeValues AttributeValue => this.attributeValue;

  public AttributeValuePropertyClass(AttributeValues aAttributeValue)
  {
    this.attributeValue = aAttributeValue;
  }

  public override string ToString() => string.Empty;
}
