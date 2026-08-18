
// Type: Intermech.Interfaces.RelationHashContentClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    public class RelationHashContentClass : List<AttributeHashContentClass>
    {
      private Guid guid = Guid.Empty;

      public Guid Guid
      {
        get => this.guid;
        set => this.guid = value;
      }

      public RelationHashContentClass()
      {
      }

      public RelationHashContentClass(Guid guid) => this.guid = guid;
    }
}
