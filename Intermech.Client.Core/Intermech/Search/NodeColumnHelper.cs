
// Type: Intermech.Search.NodeColumnHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search;

public static class NodeColumnHelper
{
  public static AttributeSourceTypes GetAttributeSourceType(NodeColumn nodeColumn)
  {
    if (nodeColumn == null)
      throw new ArgumentNullException(nameof (nodeColumn));
    return nodeColumn.AttrSource == AttributeSourceTypes.Auto && nodeColumn.Attribute != null && ObligatoryObjectAttributesHelper.IsObligatoryAttribute(nodeColumn.Attribute.AttributeID) ? ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) nodeColumn.Attribute.AttributeID) : nodeColumn.AttrSource;
  }
}
