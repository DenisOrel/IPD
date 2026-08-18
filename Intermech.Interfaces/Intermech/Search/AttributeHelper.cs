
// Type: Intermech.Search.AttributeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search
{
    public static class AttributeHelper
    {
      public static object[] GetAttributeValues(AttributeValues attributeValues)
      {
        if (attributeValues == null)
          throw new ArgumentNullException(nameof (attributeValues));
        return attributeValues.Values != null ? ((IEnumerable<object>) attributeValues.Values).Select<object, object>((Func<object, object>) (o => AttributeHelper.ConvertAttributeValue(o, attributeValues.AttributeType, AttributeTypeHelper.AllowEmpty(attributeValues.AttributeID)))).ToArray<object>() : (object[]) null;
      }

      private static object ConvertAttributeValue(object value, FieldTypes fieldType, bool allowNulls)
      {
        if (fieldType == FieldTypes.ftGuid)
        {
          switch (value)
          {
            case Guid _:
              return value;
            case string _:
              string input = (string) value;
              if (!string.IsNullOrEmpty(input))
                return (object) Guid.Parse(input);
              return allowNulls ? (object) null : (object) Guid.Empty;
            case DBNull _:
            case null:
              return allowNulls ? (object) null : (object) Guid.Empty;
            default:
              throw new Exception();
          }
        }
        else
        {
          switch (value)
          {
            case DBNull _:
            case null:
              return (object) null;
            default:
              return value;
          }
        }
      }
    }
}
