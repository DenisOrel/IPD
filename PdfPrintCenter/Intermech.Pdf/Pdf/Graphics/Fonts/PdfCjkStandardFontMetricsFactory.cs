// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.PdfCjkStandardFontMetricsFactory
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal sealed class PdfCjkStandardFontMetricsFactory
{
  private const float c_subSuperScriptFactor = 1.52f;

  private PdfCjkStandardFontMetricsFactory()
  {
  }

  private static PdfFontMetrics GetHanyangSystemsGothicMediumMetrix(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics gothicMediumMetrix = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    gothicMediumMetrix.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(8094, 8190, 500));
    gothicMediumMetrix.Ascent = 880f;
    gothicMediumMetrix.Descent = -120f;
    gothicMediumMetrix.Size = size;
    gothicMediumMetrix.Height = gothicMediumMetrix.Ascent - gothicMediumMetrix.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      gothicMediumMetrix.PostScriptName = "HYGoThic-Medium,BoldItalic";
      return gothicMediumMetrix;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      gothicMediumMetrix.PostScriptName = "HYGoThic-Medium,Bold";
      return gothicMediumMetrix;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      gothicMediumMetrix.PostScriptName = "HYGoThic-Medium,Italic";
      return gothicMediumMetrix;
    }
    gothicMediumMetrix.PostScriptName = "HYGoThic-Medium";
    return gothicMediumMetrix;
  }

  private static PdfFontMetrics GetHanyangSystemsShinMyeongJoMediumMetrix(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics myeongJoMediumMetrix = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    myeongJoMediumMetrix.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(8094, 8190, 500));
    myeongJoMediumMetrix.Ascent = 880f;
    myeongJoMediumMetrix.Descent = -120f;
    myeongJoMediumMetrix.Size = size;
    myeongJoMediumMetrix.Height = myeongJoMediumMetrix.Ascent - myeongJoMediumMetrix.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      myeongJoMediumMetrix.PostScriptName = "HYSMyeongJo-Medium,BoldItalic";
      return myeongJoMediumMetrix;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      myeongJoMediumMetrix.PostScriptName = "HYSMyeongJo-Medium,Bold";
      return myeongJoMediumMetrix;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      myeongJoMediumMetrix.PostScriptName = "HYSMyeongJo-Medium,Italic";
      return myeongJoMediumMetrix;
    }
    myeongJoMediumMetrix.PostScriptName = "HYSMyeongJo-Medium";
    return myeongJoMediumMetrix;
  }

  private static PdfFontMetrics GetHeiseiKakuGothicW5Metrix(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics kakuGothicW5Metrix = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    kakuGothicW5Metrix.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(231, 632, 500));
    kakuGothicW5Metrix.Ascent = 857f;
    kakuGothicW5Metrix.Descent = -125f;
    kakuGothicW5Metrix.Size = size;
    kakuGothicW5Metrix.Height = kakuGothicW5Metrix.Ascent - kakuGothicW5Metrix.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      kakuGothicW5Metrix.PostScriptName = "HeiseiKakuGo-W5,BoldItalic";
      return kakuGothicW5Metrix;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      kakuGothicW5Metrix.PostScriptName = "HeiseiKakuGo-W5,Bold";
      return kakuGothicW5Metrix;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      kakuGothicW5Metrix.PostScriptName = "HeiseiKakuGo-W5,Italic";
      return kakuGothicW5Metrix;
    }
    kakuGothicW5Metrix.PostScriptName = "HeiseiKakuGo-W5";
    return kakuGothicW5Metrix;
  }

  private static PdfFontMetrics GetHeiseiMinchoW3(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics heiseiMinchoW3 = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    heiseiMinchoW3.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(231, 632, 500));
    heiseiMinchoW3.Ascent = 857f;
    heiseiMinchoW3.Descent = -143f;
    heiseiMinchoW3.Size = size;
    heiseiMinchoW3.Height = heiseiMinchoW3.Ascent - heiseiMinchoW3.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      heiseiMinchoW3.PostScriptName = "HeiseiMin-W3,BoldItalic";
      return heiseiMinchoW3;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      heiseiMinchoW3.PostScriptName = "HeiseiMin-W3,Bold";
      return heiseiMinchoW3;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      heiseiMinchoW3.PostScriptName = "HeiseiMin-W3,Italic";
      return heiseiMinchoW3;
    }
    heiseiMinchoW3.PostScriptName = "HeiseiMin-W3";
    return heiseiMinchoW3;
  }

  public static PdfFontMetrics GetMetrics(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics metrics;
    switch (fontFamily)
    {
      case PdfCjkFontFamily.HeiseiKakuGothicW5:
        metrics = PdfCjkStandardFontMetricsFactory.GetHeiseiKakuGothicW5Metrix(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.HeiseiMinchoW3:
        metrics = PdfCjkStandardFontMetricsFactory.GetHeiseiMinchoW3(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.HanyangSystemsGothicMedium:
        metrics = PdfCjkStandardFontMetricsFactory.GetHanyangSystemsGothicMediumMetrix(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.HanyangSystemsShinMyeongJoMedium:
        metrics = PdfCjkStandardFontMetricsFactory.GetHanyangSystemsShinMyeongJoMediumMetrix(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.MonotypeHeiMedium:
        metrics = PdfCjkStandardFontMetricsFactory.GetMonotypeHeiMedium(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.MonotypeSungLight:
        metrics = PdfCjkStandardFontMetricsFactory.GetMonotypeSungLightMetrix(fontFamily, fontStyle, size);
        break;
      case PdfCjkFontFamily.SinoTypeSongLight:
        metrics = PdfCjkStandardFontMetricsFactory.GetSinoTypeSongLight(fontFamily, fontStyle, size);
        break;
      default:
        throw new ArgumentException("Unsupported font family", nameof (fontFamily));
    }
    metrics.Name = fontFamily.ToString();
    metrics.SubScriptSizeFactor = 1.52f;
    metrics.SuperscriptSizeFactor = 1.52f;
    return metrics;
  }

  private static PdfFontMetrics GetMonotypeHeiMedium(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics monotypeHeiMedium = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    monotypeHeiMedium.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(13648, 13742, 500));
    monotypeHeiMedium.Ascent = 880f;
    monotypeHeiMedium.Descent = -120f;
    monotypeHeiMedium.Size = size;
    monotypeHeiMedium.Height = monotypeHeiMedium.Ascent - monotypeHeiMedium.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      monotypeHeiMedium.PostScriptName = "MHei-Medium,BoldItalic";
      return monotypeHeiMedium;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      monotypeHeiMedium.PostScriptName = "MHei-Medium,Bold";
      return monotypeHeiMedium;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      monotypeHeiMedium.PostScriptName = "MHei-Medium,Italic";
      return monotypeHeiMedium;
    }
    monotypeHeiMedium.PostScriptName = "MHei-Medium";
    return monotypeHeiMedium;
  }

  private static PdfFontMetrics GetMonotypeSungLightMetrix(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics monotypeSungLightMetrix = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    monotypeSungLightMetrix.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(13648, 13742, 500));
    monotypeSungLightMetrix.Ascent = 880f;
    monotypeSungLightMetrix.Descent = -120f;
    monotypeSungLightMetrix.Size = size;
    monotypeSungLightMetrix.Height = monotypeSungLightMetrix.Ascent - monotypeSungLightMetrix.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      monotypeSungLightMetrix.PostScriptName = "MSung-Light,BoldItalic";
      return monotypeSungLightMetrix;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      monotypeSungLightMetrix.PostScriptName = "MSung-Light,Bold";
      return monotypeSungLightMetrix;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      monotypeSungLightMetrix.PostScriptName = "MSung-Light,Italic";
      return monotypeSungLightMetrix;
    }
    monotypeSungLightMetrix.PostScriptName = "MSung-Light";
    return monotypeSungLightMetrix;
  }

  private static PdfFontMetrics GetSinoTypeSongLight(
    PdfCjkFontFamily fontFamily,
    PdfFontStyle fontStyle,
    float size)
  {
    PdfFontMetrics sinoTypeSongLight = new PdfFontMetrics();
    CjkWidthTable cjkWidthTable = new CjkWidthTable(1000);
    sinoTypeSongLight.WidthTable = (WidthTable) cjkWidthTable;
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(1, 95, 500));
    cjkWidthTable.Add((CjkWidth) new CjkSameWidth(814, 939, 500));
    cjkWidthTable.Add((CjkWidth) new CjkDifferentWidth(7712, new int[1]
    {
      500
    }));
    cjkWidthTable.Add((CjkWidth) new CjkDifferentWidth(7716, new int[1]
    {
      500
    }));
    sinoTypeSongLight.Ascent = 880f;
    sinoTypeSongLight.Descent = -120f;
    sinoTypeSongLight.Size = size;
    sinoTypeSongLight.Height = sinoTypeSongLight.Ascent - sinoTypeSongLight.Descent;
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular && (fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      sinoTypeSongLight.PostScriptName = "STSong-Light,BoldItalic";
      return sinoTypeSongLight;
    }
    if ((fontStyle & PdfFontStyle.Bold) != PdfFontStyle.Regular)
    {
      sinoTypeSongLight.PostScriptName = "STSong-Light,Bold";
      return sinoTypeSongLight;
    }
    if ((fontStyle & PdfFontStyle.Italic) != PdfFontStyle.Regular)
    {
      sinoTypeSongLight.PostScriptName = "STSong-Light,Italic";
      return sinoTypeSongLight;
    }
    sinoTypeSongLight.PostScriptName = "STSong-Light";
    return sinoTypeSongLight;
  }
}
