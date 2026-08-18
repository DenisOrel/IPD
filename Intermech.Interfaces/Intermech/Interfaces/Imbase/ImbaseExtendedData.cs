
// Type: Intermech.Interfaces.Imbase.ImbaseExtendedData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Imbase
{
    [Serializable]
    public class ImbaseExtendedData
    {
      public IDictionary<int, ImbaseExtendedObjectTypeInfo> ObjectTypeData { get; } = (IDictionary<int, ImbaseExtendedObjectTypeInfo>) new Dictionary<int, ImbaseExtendedObjectTypeInfo>();
    }
}
