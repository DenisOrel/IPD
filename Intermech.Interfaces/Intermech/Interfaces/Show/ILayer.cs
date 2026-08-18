
// Type: Intermech.Interfaces.Show.ILayer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы со слоем</summary>
    public interface ILayer : IDllIndex
    {
      /// <summary>имя слоя</summary>
      string Name { get; }

      /// <summary>состояние слоя : true - включена видимость</summary>
      bool Visible { get; set; }

      /// <summary>габариты слоя</summary>
      RectangleD Bound { get; }
    }
}
