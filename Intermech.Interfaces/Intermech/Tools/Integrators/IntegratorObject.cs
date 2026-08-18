
// Type: Intermech.Tools.Integrators.IntegratorObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools.Integrators
{
    [Serializable]
    public sealed class IntegratorObject
    {
      private Guid id;
      private string displayName;

      public IntegratorObject(Guid id, string displayName)
      {
        if (id == Guid.Empty)
          throw new ArgumentException();
        if (string.IsNullOrEmpty(displayName))
          throw new ArgumentException();
        this.id = id;
        this.displayName = displayName;
      }

      public Guid Id => this.id;

      public string DisplayName => this.displayName;

      public override string ToString() => this.displayName;
    }
}
