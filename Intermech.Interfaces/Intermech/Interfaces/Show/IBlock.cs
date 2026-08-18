
// Type: Intermech.Interfaces.Show.IBlock
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Syncfusion.Pdf.Graphics;


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с блоком</summary>
    public interface IBlock : IDllIndex
    {
      /// <summary>имя блока</summary>
      string Name { get; }

      /// <summary>пересчитать границы включённых слоёв для блока</summary>
      RectangleD Bounds { get; }

      /// <summary>габариты блока при всех слоях</summary>
      RectangleD BoundsAll { get; }

      /// <summary>рисовать графику блока GDI+</summary>
      /// <param name="graphics">Graphics для рисования</param>
      /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
      /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
      void Paint(System.Drawing.Graphics graphics, RectangleD clipBox, double epsilon);

      /// <summary>рисовать графику блока PDF</summary>
      /// <param name="graphics">Graphics для рисования PDF</param>
      /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
      /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
      void Paint(PdfGraphics graphics, RectangleD clipBox, double epsilon);
    }
}
