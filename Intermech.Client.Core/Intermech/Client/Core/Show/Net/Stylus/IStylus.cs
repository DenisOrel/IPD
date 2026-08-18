
// Type: Intermech.Client.Core.Show.Net.Stylus.IStylus
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Intermech.Client.Core.Show.Net.Stylus;

/// <summary>список перьев для линий(по цвету ACAD) </summary>
internal interface IStylus
{
  /// <summary>цвет ACAD указанный в чертеже</summary>
  DwgColor ColorDwg { get; }

  /// <summary>цвет которым рисовать</summary>
  Color ColorPen { get; set; }

  /// <summary>дополнительная толщина пера(мм)</summary>
  double Weight { get; set; }

  /// <summary>перо GDI+</summary>
  Pen Pen { get; }

  /// <summary>заливка GDI+</summary>
  SolidBrush SolidBrush { get; }

  /// <summary>перо PDF</summary>
  PdfPen PdfPen { get; }

  /// <summary>заливка PDF</summary>
  PdfBrush PdfBrush { get; }
}
