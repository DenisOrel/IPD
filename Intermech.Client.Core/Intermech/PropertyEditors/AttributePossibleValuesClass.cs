
// Type: Intermech.PropertyEditors.AttributePossibleValuesClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectVersionModeEditor.</summary>
public class AttributePossibleValuesClass
{
  private object _attributeValue = (object) -1;
  private string _attributeValueCaption = string.Empty;

  public object AttributeValue => this._attributeValue;

  public AttributePossibleValuesClass(object Value, string Caption)
  {
    if (Value == null)
      return;
    this._attributeValue = Value;
    this._attributeValueCaption = Caption == string.Empty ? this._attributeValue.ToString() : Caption;
  }

  public override string ToString() => this._attributeValueCaption;
}
