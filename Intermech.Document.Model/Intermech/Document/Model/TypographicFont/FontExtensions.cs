// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TypographicFont.FontExtensions
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.Document.Model.TypographicFont;

public static class FontExtensions
{
  public static Font With(this Font font, TypographicFontWeight weight)
  {
    return font.With(font.SizeInPoints, weight, font.Italic, font.Underline, font.Strikeout);
  }

  public static Font With(this Font font, float size, TypographicFontWeight weight)
  {
    return font.With(size, weight, font.Italic, font.Underline, font.Strikeout);
  }

  public static Font With(this Font font, float size, TypographicFontWeight weight, bool italic)
  {
    return font.With(size, weight, italic, font.Underline, font.Strikeout);
  }

  public static Font With(
    this Font font,
    float size,
    TypographicFontWeight weight,
    bool italic,
    bool underline)
  {
    return font.With(size, weight, italic, underline, font.Strikeout);
  }

  public static Font With(
    this Font font,
    float size,
    TypographicFontWeight weight,
    bool italic,
    bool underline,
    bool strikeout)
  {
    var data = font.GetTypographicFamily().Fonts.Where<Intermech.Document.Model.TypographicFont.TypographicFont>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ => italic || !_.Italic & underline || !_.Underlined)).SelectMany(_ => !_.Bold ? new \u003C\u003Ef__AnonymousType0<Intermech.Document.Model.TypographicFont.TypographicFont, bool, int>[2]
    {
      new
      {
        font = _,
        simulateBold = false,
        weight = (int) _.Weight
      },
      new
      {
        font = _,
        simulateBold = true,
        weight = (int) _.Weight * 700 / 400
      }
    } : new \u003C\u003Ef__AnonymousType0<Intermech.Document.Model.TypographicFont.TypographicFont, bool, int>[1]
    {
      new
      {
        font = _,
        simulateBold = false,
        weight = (int) _.Weight
      }
    }).OrderBy(_ => Math.Abs((int) (_.weight - weight))).ThenByDescending(_ => _.font.Italic == italic).ThenByDescending(_ => _.font.Underlined == underline).ThenByDescending(_ => _.font.Strikeout == underline).First();
    FontStyle style = FontStyle.Regular;
    if (data.font.Bold || data.simulateBold)
      style |= FontStyle.Bold;
    if (data.font.Italic | italic)
      style |= FontStyle.Italic;
    if (data.font.Underlined | underline)
      style |= FontStyle.Underline;
    if (data.font.Strikeout | strikeout)
      style |= FontStyle.Strikeout;
    return !(data.font.Name == font.Name) || style != font.Style ? new Font(data.font.Name, size, style) : font;
  }

  /// <summary>
  /// Gets the base font that is used. If a style is being simulated in the GDI font, this returns the base font without the simulated style.
  /// </summary>
  public static Intermech.Document.Model.TypographicFont.TypographicFont GetTypographicFont(
    this Font font)
  {
    return TypographicFontFamily.InstalledFamiliesList.SelectMany<TypographicFontFamily, Intermech.Document.Model.TypographicFont.TypographicFont>((Func<TypographicFontFamily, IEnumerable<Intermech.Document.Model.TypographicFont.TypographicFont>>) (_ => (IEnumerable<Intermech.Document.Model.TypographicFont.TypographicFont>) _.Fonts)).Where<Intermech.Document.Model.TypographicFont.TypographicFont>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ =>
    {
      if (!(_.Name == font.Name) || !font.Bold && _.Bold || !font.Italic && _.Italic || !font.Underline && _.Underlined)
        return false;
      return font.Strikeout || !_.Strikeout;
    })).OrderByDescending<Intermech.Document.Model.TypographicFont.TypographicFont, bool>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ => _.Bold == font.Bold)).ThenByDescending<Intermech.Document.Model.TypographicFont.TypographicFont, bool>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ => _.Italic == font.Italic)).ThenByDescending<Intermech.Document.Model.TypographicFont.TypographicFont, bool>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ => _.Underlined == font.Underline)).ThenByDescending<Intermech.Document.Model.TypographicFont.TypographicFont, bool>((Func<Intermech.Document.Model.TypographicFont.TypographicFont, bool>) (_ => _.Strikeout == font.Strikeout)).FirstOrDefault<Intermech.Document.Model.TypographicFont.TypographicFont>();
  }

  /// <summary>
  /// Gets the installed typographic family of the GDI font.
  /// </summary>
  public static TypographicFontFamily GetTypographicFamily(this Font font)
  {
    foreach (TypographicFontFamily installedFamilies in TypographicFontFamily.InstalledFamiliesList)
    {
      foreach (Intermech.Document.Model.TypographicFont.TypographicFont font1 in (IEnumerable<Intermech.Document.Model.TypographicFont.TypographicFont>) installedFamilies.Fonts)
      {
        if (font1.Name == font.Name)
          return installedFamilies;
      }
    }
    return (TypographicFontFamily) null;
  }
}
