// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PdfGenerator.Converter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Document.Model.PdfGenerator;

public class Converter
{
  private int dpi = 96 /*0x60*/;
  private float zoom = 1f;

  public int Dpi
  {
    get => this.dpi;
    set => this.dpi = value;
  }

  public float Zoom
  {
    get => this.zoom;
    set => this.zoom = value;
  }

  /// <summary>Пукты в дюймы (1 пункт = 1/72 дюйма)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Дюймы</returns>
  public static float PointToInch(float point) => point / 72f;

  public static int MmToTwips(float mm) => Convert.ToInt32(mm * 56.6929131f);

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="pointMm">Миллиметры</param>
  /// <returns>Twips</returns>
  public static Point MmToTwips(PointF pointMm)
  {
    return new Point(Converter.MmToTwips(pointMm.X), Converter.MmToTwips(pointMm.Y));
  }

  /// <summary>Миллиметры в twips (1 twips = 1/20 пункта = 1/1440 дюйма)</summary>
  /// <param name="sizeMm">Миллиметры</param>
  /// <returns>Twips</returns>
  public static Size MmToTwips(SizeF sizeMm)
  {
    return new Size(Converter.MmToTwips(sizeMm.Width), Converter.MmToTwips(sizeMm.Height));
  }

  public static float Round(float point) => (float) Math.Round((double) point, 2);

  public static Decimal RoundD(float point) => (Decimal) Math.Round((double) point, 2);

  public static float Round(float point, int decimals)
  {
    return (float) Math.Round((double) point, decimals);
  }

  /// <summary>Пукты в дюймы (1 пункт = 1/72 дюйма)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Дюймы</returns>
  public static float PointToMm(float point)
  {
    return (float) ((double) point / 72.0 * 25.399999618530273);
  }

  /// <summary>Пукты в дюймы (1 пункт = 1/72 дюйма)</summary>
  /// <param name="point">Пункты</param>
  /// <returns>Дюймы</returns>
  public static float PointToMmRound(float point)
  {
    return (float) Math.Round((double) point / 72.0 * 25.399999618530273, 1);
  }

  /// <summary>Миллиметры в пункты (1/72 дюйма)</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Пункты</returns>
  public static int MmToPoints(float mm) => Convert.ToInt32(mm * 2.83464575f);

  /// <summary>Миллиметры в пункты (1/72 дюйма)</summary>
  /// <param name="mm">Миллиметры</param>
  /// <returns>Пункты</returns>
  public static float MmToPointsF(float mm) => mm * 2.83464575f;

  /// <summary>Преобразовать миллиметры в пиксели</summary>
  /// <param name="mm">Миллиметры</param>
  /// <param name="dpi">Точек на дюйм</param>
  public static float MmToPixels(float mm, float dpi) => Converter.MmToInch(mm) * dpi;

  public static float PixelsToCharacters(float pixels)
  {
    return (float) (Math.Truncate((double) pixels / 7.0 * 100.0 + 0.5) / 100.0);
  }

  public static float CharactersToWidth(float ch)
  {
    return (float) (Math.Truncate(((double) ch * 7.0 + 5.0) / 7.0 * 256.0) / 256.0);
  }

  public static float MmToWidth(float mm, float dpi)
  {
    return Converter.CharactersToWidth(Converter.PixelsToCharacters(Converter.MmToPixels(mm, dpi)));
  }

  public int MmToHundredthsOfInch(float mm) => Convert.ToInt32(Converter.MmToInch(mm) * 100f);

  public float ConvertPixelsToMM(float value) => value * (25.4f / (float) this.dpi) / this.Zoom;

  public float ConvertPixelsToMM(float value, bool unscaled) => value * (25.4f / (float) this.dpi);

  public static float MmToInch(float mm) => mm / 25.4f;

  public int ConvertMMToPixels(float mm)
  {
    return (int) ((double) Convert.ToInt32(Converter.MmToInch(mm) * (float) this.dpi) * (double) this.Zoom);
  }

  public float ConvertMMToPixelsF(float mm)
  {
    return Converter.MmToInch(mm) * (float) this.dpi * this.Zoom;
  }

  public RectangleF ConvertMMToPixelsF(RectangleF mm)
  {
    double pixelsF1 = (double) this.ConvertMMToPixelsF(mm.Left);
    float pixelsF2 = this.ConvertMMToPixelsF(mm.Top);
    float pixelsF3 = this.ConvertMMToPixelsF(mm.Right);
    float pixelsF4 = this.ConvertMMToPixelsF(mm.Bottom);
    double top = (double) pixelsF2;
    double right = (double) pixelsF3;
    double bottom = (double) pixelsF4;
    return RectangleF.FromLTRB((float) pixelsF1, (float) top, (float) right, (float) bottom);
  }

  public Rectangle ConvertMMToPixels(RectangleF mm)
  {
    int pixels1 = this.ConvertMMToPixels(mm.Left);
    int pixels2 = this.ConvertMMToPixels(mm.Top);
    int pixels3 = this.ConvertMMToPixels(mm.Right);
    int pixels4 = this.ConvertMMToPixels(mm.Bottom);
    int top = pixels2;
    int right = pixels3;
    int bottom = pixels4;
    return Rectangle.FromLTRB(pixels1, top, right, bottom);
  }

  public int ConvertMMToPixels(float mm, bool unscaled)
  {
    return Convert.ToInt32(Converter.MmToInch(mm) * (float) this.dpi);
  }

  public Rectangle ConvertMMToPixels(RectangleF mm, bool unscaled)
  {
    int pixels1 = this.ConvertMMToPixels(mm.Left, true);
    int pixels2 = this.ConvertMMToPixels(mm.Top, true);
    int pixels3 = this.ConvertMMToPixels(mm.Right, true);
    int pixels4 = this.ConvertMMToPixels(mm.Bottom, true);
    int top = pixels2;
    int right = pixels3;
    int bottom = pixels4;
    return Rectangle.FromLTRB(pixels1, top, right, bottom);
  }
}
