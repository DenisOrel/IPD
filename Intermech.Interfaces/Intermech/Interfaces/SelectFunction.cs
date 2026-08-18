
// Type: Intermech.Interfaces.SelectFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Идентификатор функции, которая вызвала метод Select</summary>
    public enum SelectFunction
    {
      /// <summary>Обычный вызов метода Select</summary>
      Default,
      /// <summary>Select вызывается из метода EntersIn</summary>
      EntersIn,
      /// <summary>Select вызывается из метода EntersInVersion</summary>
      EntersInVersion,
      /// <summary>Select вызывается из метода ConsistFrom</summary>
      ConsistFrom,
    }
}
