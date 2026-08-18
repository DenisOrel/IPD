
// Type: Intermech.Interfaces.Data.Queries.DBQueryAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Interfaces.Data.Queries
{
    public class DBQueryAttribute(
      int attributeId,
      AttributeSourceTypes attributeSource,
      ColumnContents attributeContent) : Tuple<int, AttributeSourceTypes, ColumnContents>(attributeId, attributeSource, attributeContent)
    {
    }
}
