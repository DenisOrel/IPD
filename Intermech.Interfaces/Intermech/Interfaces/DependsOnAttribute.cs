
// Type: Intermech.Interfaces.DependsOnAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public class DependsOnAttribute : StringAttribute
    {
      public DependsOnAttribute(string name = "")
        : base(name)
      {
        this.stringValue = name;
      }

      public string DependsOnName => this.stringValue;
    }
}
