
// Type: Intermech.Search.Data.Filters.FilterInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Data.Filters
{
    public sealed class FilterInfo
    {
      public FilterInfo(Type type)
      {
        this.Type = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
      }

      public Type Type { get; private set; }

      public object Options { get; set; }
    }
}
