
// Type: Intermech.Interfaces.IRedProperty
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Drawing;


namespace Intermech.Interfaces
{
    /// <summary>настройки пометок</summary>
    public interface IRedProperty
    {
      /// <summary>тип работающего MapTool</summary>
      Type TypeTool { get; set; }

      /// <summary>цвет заливки</summary>
      Color BrushColor { get; set; }

      /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
      int BrushAlpha { get; set; }

      /// <summary>цвет заливки с прозрачностью</summary>
      Color BrushColorAlpha { get; }

      /// <summary>цвет кривой</summary>
      Color PenColor { get; set; }

      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      int PenAlpha { get; set; }

      /// <summary>толщина(мм)</summary>
      float PenThickness { get; set; }

      /// <summary>цвет кривой с прозрачностью</summary>
      Color PenColorAlpha { get; }

      /// <summary>цвет текста</summary>
      Color TextColor { get; set; }

      /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
      int TextAlpha { get; set; }

      /// <summary>цвет заливки с прозрачностью</summary>
      Color TextColorAlpha { get; }

      /// <summary>имя фонта</summary>
      string FontName { get; set; }

      /// <summary>высота текста</summary>
      float FontSize { get; set; }

      /// <summary>стиль фаски</summary>
      IRedNoteStyle NoteStyle { get; set; }

      /// <summary>размер фаски</summary>
      float Facet { get; set; }

      /// <summary>стиль стрелки</summary>
      IRedArrowStyle NoteArrow { get; set; }

      /// <summary>размер стрелки</summary>
      float ArrowSize { get; set; }
    }
}
