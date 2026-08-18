
// Type: Intermech.PropertyEditors.AttributeValueClassList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

public class AttributeValueClassList : ArrayList
{
  private long id;
  private AttributableElements attributableElement;

  public AttributeValueClassList(long lId, AttributableElements lAttributableElement)
  {
    this.id = lId;
    this.attributableElement = lAttributableElement;
  }

  public override int Add(object value)
  {
    if (value is AttributeValueClass)
      ((AttributeValueClass) value).Owner = this;
    return base.Add(value);
  }

  public override void RemoveAt(int index) => base.RemoveAt(index);

  public int IndexOfbyAttributeID(int lAttributeID)
  {
    int num = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (((AttributeValueClass) this[index]).attributeID == lAttributeID)
      {
        num = index;
        break;
      }
    }
    return num;
  }

  public AttributeValueClass AttributeValueClassByAttributeID(int lAttributeID)
  {
    AttributeValueClass attributeValueClass = (AttributeValueClass) null;
    int index = this.IndexOfbyAttributeID(lAttributeID);
    if (index != -1)
      attributeValueClass = (AttributeValueClass) this[index];
    return attributeValueClass;
  }
}
