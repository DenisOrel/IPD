
// Type: Intermech.Interfaces.WebPortal.PublishTypeAttProxy
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    public class PublishTypeAttProxy : ICloneable
    {
      public PublishTypeAttProxy(int id, Guid guid, string name)
      {
        this.ID = id;
        this.Guid = guid;
        this.Name = name;
      }

      public int ID { get; }

      public Guid Guid { get; }

      public string Name { get; }

      public override string ToString() => this.Name;

      public override bool Equals(object obj)
      {
        return obj is PublishTypeAttProxy publishTypeAttProxy && publishTypeAttProxy.Guid == this.Guid;
      }

      public override int GetHashCode() => this.Guid.GetHashCode();

      public object Clone() => (object) new PublishTypeAttProxy(this.ID, this.Guid, this.Name);
    }
}
