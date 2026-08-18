
// Type: Intermech.PropertyEditors.AttributePropertyClassComparator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.PropertyEditors;

public class AttributePropertyClassComparator : IComparer
{
  public int Compare(object x, object y)
  {
    if (x == null && y == null)
      return 0;
    if (x == null)
      return -1;
    return y == null ? 1 : string.Compare(x.ToString(), y.ToString());
  }
}
