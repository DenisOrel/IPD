
// Type: Intermech.Interfaces.Show.IShowDwg
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с Show</summary>
    public interface IShowDwg
    {
      /// <summary>массив блоков</summary>
      IBlockTable Blocks { get; }

      /// <summary>массив компоновок</summary>
      ILayoutTable Layouts { get; }

      /// <summary>массив слоёв</summary>
      ILayerTable Layers { get; }

      /// <summary>установить границу прорисовки(мм)</summary>
      /// <param name="box">граница прорисовки, Empty - рисовать всё</param>
      void SetClip(RectangleD box);
    }
}
