
// Type: Intermech.Kernel.Search.SourceType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    public class SourceType : Attribute
    {
      private AttributeSourceTypes _type;

      public SourceType(AttributeSourceTypes type) => this._type = type;

      public AttributeSourceTypes AttributeSourceType
      {
        get => this._type;
        set => this._type = value;
      }
    }
}
