
// Type: Intermech.Interfaces.IFormDesignerActionParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Заглушка на параметры.</summary>
    [TypeConverter(typeof (TypeConverter))]
    public interface IFormDesignerActionParams
    {
      /// <summary>
      /// 
      /// </summary>
      object Component { get; set; }
    }
}
