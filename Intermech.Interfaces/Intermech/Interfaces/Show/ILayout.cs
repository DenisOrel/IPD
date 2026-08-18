
// Type: Intermech.Interfaces.Show.ILayout
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Syncfusion.Pdf.Graphics;


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с компоновкой</summary>
    public interface ILayout : IDllIndex
    {
      /// <summary>имя компоновки</summary>
      string Name { get; }

      /// <summary>пересчитать границы включённых слоёв для компоновки</summary>
      RectangleD Bounds { get; }

      /// <summary>габариты компоновки при всех слоях</summary>
      RectangleD BoundsAll { get; }

      /// <summary>прочитать штамп (для видимых слоёв)</summary>
      /// <param name="fileCfgName">имя файла конфигурации штампа</param>
      /// <param name="cfgData">данные файла конфигурации штампа</param>
      /// <returns>массив прочитанных данных из штампа; null -нет штампа</returns>
      IStampField[] ScanStamp(string fileCfgName, byte[] cfgData);

      /// <summary>рисовать графику компоновки безграниц GDI+</summary>
      /// <param name="graphics">Graphics для рисования</param>
      void Paint(System.Drawing.Graphics graphics);

      /// <summary>рисовать графику компоновки GDI+</summary>
      /// <param name="graphics">Graphics для рисования</param>
      /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
      /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
      void Paint(System.Drawing.Graphics graphics, RectangleD clipBox, double epsilon);

      /// <summary>рисовать графику компоновки PDF</summary>
      /// <param name="graphics">Graphics для рисования PDF</param>
      /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
      /// <param name="epsilon">погрешность поиска нерисуемой рамки</param>
      void Paint(PdfGraphics graphics, RectangleD clipBox, double epsilon);
    }
}
