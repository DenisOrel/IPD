// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UnitsConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Преобразование единиц измерения</summary>
public class UnitsConverter
{
  /// <summary>Миллиметры в пункты (1/72 дюйма)</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Пункты</returns>
  public static int MmToPoints(float mm) => Convert.ToInt32(mm * 2.83464575f);

  /// <summary>Миллиметры в пункты (1/72 дюйма)</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Пункты</returns>
  public static float MmToPointsF(float mm) => mm * 2.83464575f;

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Twips</returns>
  public static int MmToTwips(float mm) => Convert.ToInt32(mm * 56.6929131f);

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="pointMm">Миллиметры</param>
  /// <returns>Twips</returns>
  public static Point MmToTwips(PointF pointMm)
  {
    return new Point(UnitsConverter.MmToTwips(pointMm.X), UnitsConverter.MmToTwips(pointMm.Y));
  }

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="sizeMm">Миллиметры</param>
  /// <returns>Twips</returns>
  public static Size MmToTwips(SizeF sizeMm)
  {
    return new Size(UnitsConverter.MmToTwips(sizeMm.Width), UnitsConverter.MmToTwips(sizeMm.Height));
  }

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="rec">Прямоугольник в миллиметрах</param>
  /// <returns>Twips</returns>
  public static Rectangle MmToTwips(RectangleF rec)
  {
    return Rectangle.FromLTRB(UnitsConverter.MmToTwips(rec.X), UnitsConverter.MmToTwips(rec.Y), UnitsConverter.MmToTwips(rec.Right), UnitsConverter.MmToTwips(rec.Bottom));
  }

  /// <summary>Пукты в дюймы (1 пункт = 1/72 дюйма)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Дюймы</returns>
  public static float PointToInch(float point) => point / 72f;

  /// <summary>Пукты в дюймы (1 пункт = 1/72 дюйма)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Дюймы</returns>
  public static float PointToMm(float point)
  {
    return (float) ((double) point / 72.0 * 25.399999618530273);
  }

  /// <summary>Пункты в twips (1 пункт = 20 twips)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Twips</returns>
  public static int PointToTwips(float point) => Convert.ToInt32(point * 20f);

  /// <summary>Twips в миллиметры (1 twips = 1/1440 дюйма)</summary>
  /// <param name="twips">Twips</param>
  /// <returns>Миллиметры</returns>
  public static float TwipsToMm(float twips) => twips * 0.0176388882f;

  /// <summary>Twips в дюймы (1 twips = 1/1440 дюйма)</summary>
  /// <param name="twips">Twips</param>
  /// <returns>Дюймы</returns>
  public static float TwipsToInch(float twips) => twips / 1440f;

  /// <summary>Преобразовать миллиметры в сотые доли дюйма</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Сотые доли дюйма</returns>
  public static int MmToHundredthsOfInch(float mm)
  {
    return Convert.ToInt32(UnitsConverter.MmToInch(mm) * 100f);
  }

  /// <summary>Преобразовать миллиметры в дюймы</summary>
  /// <param name="mm">Миллиметры</param>
  public static float MmToInch(float mm) => mm / 25.4f;

  /// <summary>Преобразовать миллиметры в дюймы</summary>
  /// <param name="mm">Миллиметры</param>
  public static SizeF MmToInch(SizeF mm) => new SizeF(mm.Width / 25.4f, mm.Height / 25.4f);

  /// <summary>Преобразовать дюймы в миллиметры</summary>
  /// <param name="Inch">Дюймы</param>
  public static float InchToMm(float Inch) => Inch * 25.4f;

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static int MmToPixels(float mm, float dpi)
  {
    return Convert.ToInt32(UnitsConverter.MmToInch(mm) * dpi);
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static int MmToPixelsR(float mm, float dpi)
  {
    return (int) Math.Round((double) UnitsConverter.MmToInch(mm) * (double) dpi);
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <param name="roundUp">Округлять вверх</param>
  /// <returns></returns>
  public static int MmToPixels(float mm, float dpi, bool roundUp)
  {
    float num = UnitsConverter.MmToInch(mm) * dpi;
    return roundUp ? Convert.ToInt32(Math.Ceiling((double) num)) : Convert.ToInt32(Math.Floor((double) num));
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static float MmToPixelsF(float mm, float dpi) => UnitsConverter.MmToInch(mm) * dpi;

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static PointF MmToPixelsF(PointF mm, PointF dpi)
  {
    return new PointF(mm.X / 25.4f * dpi.X, mm.Y / 25.4f * dpi.Y);
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static RectangleF MmToPixelsF(RectangleF mm, PointF dpi)
  {
    return new RectangleF(mm.X / 25.4f * dpi.X, mm.Y / 25.4f * dpi.Y, mm.Width / 25.4f * dpi.X, mm.Height / 25.4f * dpi.Y);
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static SizeF MmToPixelsF(SizeF mm, PointF dpi)
  {
    return new SizeF(mm.Width / 25.4f * dpi.X, mm.Height / 25.4f * dpi.Y);
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Точка с координатами в миллиметрах</param>
  /// <param name="dpi">Точек на дюйм по X и Y</param>
  /// <returns>Точка в пикселях</returns>
  public static Point MmToPixels(PointF mm, PointF dpi)
  {
    return new Point(Convert.ToInt32(mm.X / 25.4f * dpi.X), Convert.ToInt32(mm.Y / 25.4f * dpi.Y));
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Точка с координатами в миллиметрах</param>
  /// <param name="dpi">Точек на дюйм по X и Y</param>
  /// <returns>Точка в пикселях</returns>
  public static Point MmToPixelsR(PointF mm, PointF dpi)
  {
    return new Point((int) Math.Round((double) mm.X / 25.399999618530273 * (double) dpi.X), (int) Math.Round((double) mm.Y / 25.399999618530273 * (double) dpi.Y));
  }

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Точка с координатами в миллиметрах</param>
  /// <param name="dpi">Точек на дюйм по X и Y</param>
  /// <returns>Точка в пикселях</returns>
  public static Rectangle MmToPixels(RectangleF mm, PointF dpi)
  {
    return Rectangle.FromLTRB(Convert.ToInt32(mm.Left / 25.4f * dpi.X), Convert.ToInt32(mm.Top / 25.4f * dpi.Y), Convert.ToInt32(mm.Right / 25.4f * dpi.X), Convert.ToInt32(mm.Bottom / 25.4f * dpi.Y));
  }

  public static Rectangle MmToPixelsR(RectangleF mm, PointF dpi)
  {
    return Rectangle.FromLTRB((int) Math.Round((double) mm.Left / 25.399999618530273 * (double) dpi.X), (int) Math.Round((double) mm.Top / 25.399999618530273 * (double) dpi.Y), (int) Math.Round((double) mm.Right / 25.399999618530273 * (double) dpi.X), (int) Math.Round((double) mm.Bottom / 25.399999618530273 * (double) dpi.Y));
  }

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Пикселы</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static float PixelsToMm(int pixels, float dpi) => (float) pixels * (25.4f / dpi);

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Точка в пикселях</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <returns>Точка в миллиметрах</returns>
  public static PointF PixelsToMm(Point pixels, PointF dpi)
  {
    return new PointF((float) pixels.X * (25.4f / dpi.X), (float) pixels.Y * (25.4f / dpi.Y));
  }

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Точка в пикселях</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <returns>Точка в миллиметрах</returns>
  public static SizeF PixelsToMm(Size pixels, PointF dpi)
  {
    return new SizeF((float) pixels.Width * (25.4f / dpi.X), (float) pixels.Height * (25.4f / dpi.Y));
  }

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Точка в пикселях</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <returns>Точка в миллиметрах</returns>
  public static SizeF PixelsToMm(SizeF pixels, PointF dpi)
  {
    return new SizeF(pixels.Width * (25.4f / dpi.X), pixels.Height * (25.4f / dpi.Y));
  }

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Прямоугольник в пикселях</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <returns>Точка в миллиметрах</returns>
  public static RectangleF PixelsToMm(Rectangle pixels, PointF dpi)
  {
    return new RectangleF((float) pixels.X * (25.4f / dpi.X), (float) pixels.Y * (25.4f / dpi.Y), (float) pixels.Width * (25.4f / dpi.X), (float) pixels.Height * (25.4f / dpi.Y));
  }

  /// <summary>Преобразовать пиксели в миллиметры</summary>
  /// <param name="pixels">Прямоугольник в пикселях</param>
  /// <param name="dpi">Точек на дюйм</param>
  /// <returns>Точка в миллиметрах</returns>
  public static RectangleF PixelsToMm(RectangleF pixels, PointF dpi)
  {
    return new RectangleF(pixels.X * (25.4f / dpi.X), pixels.Y * (25.4f / dpi.Y), pixels.Width * (25.4f / dpi.X), pixels.Height * (25.4f / dpi.Y));
  }

  public static SizeF RoundSize(SizeF point, int decimals)
  {
    return new SizeF((float) Math.Round((double) point.Width, decimals), (float) Math.Round((double) point.Height, decimals));
  }

  public static PointF RoundPoint(PointF point, int decimals)
  {
    return new PointF((float) Math.Round((double) point.X, decimals), (float) Math.Round((double) point.Y, decimals));
  }

  public static RectangleF RoundPectangle(RectangleF rect, int decimals)
  {
    return new RectangleF((float) Math.Round((double) rect.X, decimals), (float) Math.Round((double) rect.Y, decimals), (float) Math.Round((double) rect.Width, decimals), (float) Math.Round((double) rect.Height, decimals));
  }

  /// <summary>Вычислить длину линии между двумя точками</summary>
  /// <param name="p0">Точка 0</param>
  /// <param name="p1">Точка 1</param>
  /// <returns>Длина линии от точки 0 до точки 1</returns>
  public static float LineLength(PointF p0, PointF p1)
  {
    return (float) Math.Sqrt(Math.Pow((double) p1.X - (double) p0.X, 2.0) + Math.Pow((double) p1.Y - (double) p0.Y, 2.0));
  }

  /// <summary>Получить точку на прямой ближайшую к заданной точке</summary>
  /// <param name="point">Точка</param>
  /// <param name="linePoint1">Точка прямой 1</param>
  /// <param name="linePoint2">Точка прямой 2</param>
  /// <returns></returns>
  public static PointF GetNearestLinePoint(PointF point, PointF linePoint1, PointF linePoint2)
  {
    PointF pointF = new PointF(linePoint1.X - linePoint2.X, linePoint1.Y - linePoint2.Y);
    if ((double) pointF.X == 0.0 && (double) pointF.Y == 0.0)
      return linePoint1;
    if ((double) pointF.X == 0.0)
      return new PointF(linePoint1.X, point.Y);
    if ((double) pointF.Y == 0.0)
      return new PointF(point.X, linePoint1.Y);
    pointF = new PointF(-pointF.Y, pointF.X);
    float num1 = (float) Math.Sqrt((double) pointF.X * (double) pointF.X + (double) pointF.Y * (double) pointF.Y);
    if ((double) num1 == 0.0)
      return linePoint1;
    pointF = new PointF(pointF.X / num1, pointF.Y / num1);
    float num2 = UnitsConverter.DistanceFromLine(point, linePoint1, linePoint2);
    PointF p0_1 = new PointF(point.X + pointF.X * num2, point.Y + pointF.Y * num2);
    float num3 = -num2;
    PointF p0_2 = new PointF(point.X + pointF.X * num3, point.Y + pointF.Y * num3);
    return (double) UnitsConverter.LineLength(p0_1, linePoint1) < (double) UnitsConverter.LineLength(p0_2, linePoint1) ? p0_1 : p0_2;
  }

  public static float DistanceFromLine(PointF point, PointF linePoint1, PointF linePoint2)
  {
    double num1 = (double) linePoint2.X - (double) linePoint1.X;
    float num2 = linePoint2.Y - linePoint1.Y;
    float num3 = (float) (num1 * num1 + (double) num2 * (double) num2);
    if ((double) num3 == 0.0)
      return UnitsConverter.LineLength(point, linePoint1);
    double num4 = (double) point.X - (double) linePoint1.X;
    float num5 = point.Y - linePoint1.Y;
    double num6 = num4 * num4 + (double) num5 * (double) num5;
    double num7 = ((double) linePoint2.X - (double) linePoint1.X) * ((double) point.X - (double) linePoint1.X);
    float num8 = (float) (((double) linePoint2.Y - (double) linePoint1.Y) * ((double) point.Y - (double) linePoint1.Y));
    float num9 = (float) (num7 * num7 + (double) num8 * (double) num8);
    double num10 = (double) num9 * (double) num9 / (double) num3;
    float d = (float) (num6 - num10);
    return (double) d > 0.0 ? (float) Math.Sqrt((double) d) : (float) Math.Sqrt(-(double) d);
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="point">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public static PointF ConvertPixelToWorld(Point point, Matrix matrix, PointF dpi)
  {
    Matrix matrix1 = matrix.Clone();
    matrix1.Invert();
    return MatrixWrapper.TransformPoint(matrix1.Elements, UnitsConverter.PixelsToMm(point, dpi));
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="rectangle">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public static RectangleF ConvertPixelToWorld(Rectangle rectangle, Matrix matrix, PointF dpi)
  {
    Matrix matrix1 = matrix.Clone();
    matrix1.Invert();
    float[] elements = matrix1.Elements;
    PointF pointF1 = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(rectangle.Location, dpi));
    PointF pointF2 = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(new Point(rectangle.Right, rectangle.Bottom), dpi));
    return RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
  }

  /// <summary>Перевести пиксели в мировые координаты</summary>
  /// <param name="points">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public static PointF[] ConvertPixelFToWorld(PointF[] points, Matrix matrix, PointF dpi)
  {
    PointF[] pointFArray = (PointF[]) points.Clone();
    Matrix matrix1 = matrix.Clone();
    matrix1.Invert();
    float[] elements = matrix1.Elements;
    for (int index = 0; index < pointFArray.Length; ++index)
      pointFArray[index] = MatrixWrapper.TransformPoint(elements, UnitsConverter.PixelsToMm(Point.Round(pointFArray[index]), dpi));
    return pointFArray;
  }

  /// <summary>Преобразовать мировую координату X в пиксели</summary>
  /// <param name="x">x</param>
  /// <returns>Координата x в пикселях</returns>
  public static int ConvertWorldXToPixel(float x, Matrix matrix, PointF dpi)
  {
    return Convert.ToInt32(UnitsConverter.LineLength(UnitsConverter.ConvertWorldToPixelF(new PointF(0.0f, 0.0f), matrix, dpi), UnitsConverter.ConvertWorldToPixelF(new PointF(x, 0.0f), matrix, dpi)));
  }

  /// <summary>Преобразовать мировую координату Y в пиксели</summary>
  /// <param name="y">y</param>
  /// <returns>Координата Y в пикселях</returns>
  public static int ConvertWorldYToPixel(float y, Matrix matrix, PointF dpi)
  {
    return Convert.ToInt32(UnitsConverter.LineLength(UnitsConverter.ConvertWorldToPixelF(new PointF(0.0f, 0.0f), matrix, dpi), UnitsConverter.ConvertWorldToPixelF(new PointF(0.0f, y), matrix, dpi)));
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="point">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public static Point ConvertWorldToPixel(PointF point, Matrix matrix, PointF dpi)
  {
    return UnitsConverter.MmToPixels(MatrixWrapper.TransformPoint(matrix.Elements, point), dpi);
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public static Rectangle ConvertWorldToPixel(RectangleF rectangle, Matrix matrix, PointF dpi)
  {
    PointF mm1 = MatrixWrapper.TransformPoint(matrix.Elements, rectangle.Location);
    PointF mm2 = MatrixWrapper.TransformPoint(matrix.Elements, new PointF(rectangle.Right, rectangle.Bottom));
    Point pixels1 = UnitsConverter.MmToPixels(mm1, dpi);
    PointF dpi1 = dpi;
    Point pixels2 = UnitsConverter.MmToPixels(mm2, dpi1);
    return Rectangle.FromLTRB(pixels1.X, pixels1.Y, pixels2.X, pixels2.Y);
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public static RectangleF ConvertWorldToPixelF(RectangleF rectangle, Matrix matrix, PointF dpi)
  {
    PointF mm1 = MatrixWrapper.TransformPoint(matrix.Elements, rectangle.Location);
    PointF mm2 = MatrixWrapper.TransformPoint(matrix.Elements, new PointF(rectangle.Right, rectangle.Bottom));
    PointF pixelsF1 = UnitsConverter.MmToPixelsF(mm1, dpi);
    PointF dpi1 = dpi;
    PointF pixelsF2 = UnitsConverter.MmToPixelsF(mm2, dpi1);
    return RectangleF.FromLTRB(pixelsF1.X, pixelsF1.Y, pixelsF2.X, pixelsF2.Y);
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="points">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public static PointF[] ConvertWorldToPixelF(PointF[] points, Matrix matrix, PointF dpi)
  {
    PointF[] pixelF = (PointF[]) points.Clone();
    for (int index = 0; index < pixelF.Length; ++index)
    {
      pixelF[index] = MatrixWrapper.TransformPoint(matrix.Elements, pixelF[index]);
      pixelF[index] = UnitsConverter.MmToPixelsF(pixelF[index], dpi);
    }
    return pixelF;
  }

  /// <summary>Преобразовать мировые координаты в пиксели</summary>
  /// <param name="point">Точка в мировых координатах</param>
  /// <returns>Точка в пикселях</returns>
  public static PointF ConvertWorldToPixelF(PointF point, Matrix matrix, PointF dpi)
  {
    point = MatrixWrapper.TransformPoint(matrix.Elements, point);
    return UnitsConverter.MmToPixelsF(point, dpi);
  }
}
