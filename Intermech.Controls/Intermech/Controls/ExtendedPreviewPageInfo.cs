
// Type: Intermech.Controls.ExtendedPreviewPageInfo
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;


namespace Intermech.Controls;

/// <summary>Расширенный класс PreviewPageInfo</summary>
public class ExtendedPreviewPageInfo
{
  /// <summary>Базовая информация</summary>
  private readonly PreviewPageInfo PreviewPageInfo;
  /// <summary>Координаты области бумаги, в которую есть техническая возможность вывода данных</summary>
  public readonly Rectangle PrintableRect;
  /// <summary>Поля страницы, заданные вручную</summary>
  public readonly Margins Margins;

  /// <summary>Initializes a new instance of the System.Drawing.Printing.PreviewPageInfo class</summary>
  /// <param name="image">The image of the printed page</param>
  /// <param name="physicalSize">The size of the printed page, in hundredths of an inch</param>
  /// <param name="margins">Поля страницы, заданные вручную</param>
  /// <param name="printableRect">Координаты области бумаги, в которую есть техническая возможность вывода данных,
  /// если передать Rectangle.Empty область печати будет соответствовать всей странице</param>
  public ExtendedPreviewPageInfo(
    Image image,
    Size physicalSize,
    Margins margins,
    Rectangle printableRect)
  {
    this.PreviewPageInfo = new PreviewPageInfo(image, physicalSize);
    this.Margins = margins;
    this.PrintableRect = printableRect != Rectangle.Empty ? printableRect : new Rectangle(Point.Empty, physicalSize);
  }

  /// <summary>Initializes a new instance of the System.Drawing.Printing.PreviewPageInfo class</summary>
  /// <param name="image">The image of the printed page</param>
  /// <param name="physicalSize">The size of the printed page, in hundredths of an inch</param>
  /// <param name="margins">Поля страницы, заданные вручную</param>
  /// <param name="printableRect">Координаты области бумаги, в которую есть техническая возможность вывода данных</param>
  public ExtendedPreviewPageInfo(Image image, Size physicalSize, Margins margins)
    : this(image, physicalSize, margins, Rectangle.Empty)
  {
  }

  /// <summary>System.Drawing.Image representing the printed page.</summary>
  public Image Image
  {
    [DebuggerStepThrough] get => this.PreviewPageInfo.Image;
  }

  /// <summary>System.Drawing.Size that specifies the size of the printed page, in hundredths of an inch.</summary>
  public Size PhysicalSize
  {
    [DebuggerStepThrough] get => this.PreviewPageInfo.PhysicalSize;
  }

  /// <summary>Implicit cast that converts the given ExtendedPreviewPageInfo to a PreviewPageInfo</summary>
  /// <param name="extendedPreviewPageInfo"></param>
  /// <returns>The result of the operation</returns>
  public static implicit operator PreviewPageInfo(ExtendedPreviewPageInfo extendedPreviewPageInfo)
  {
    return extendedPreviewPageInfo.PreviewPageInfo;
  }
}
