
// Type: Intermech.PropertyEditors.AttributePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for AttributeEditor.</summary>
public class AttributePropertyClass
{
  private int attribute;
  private bool firstToString = true;
  private string cachedString = string.Empty;

  public int Attribute => this.attribute;

  public AttributePropertyClass(int aAttribute) => this.attribute = aAttribute;

  public string ToStringPrim(bool fromInnerCache)
  {
    if (this.attribute == 0)
      return string.Empty;
    string stringPrim = string.Empty;
    if (this.firstToString || !fromInnerCache)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.attribute);
      if (attributeType != null)
        stringPrim = attributeType.Name;
      this.cachedString = stringPrim;
      this.firstToString = false;
    }
    else
      stringPrim = this.cachedString;
    return stringPrim;
  }

  public override string ToString() => this.ToStringPrim(false);
}
