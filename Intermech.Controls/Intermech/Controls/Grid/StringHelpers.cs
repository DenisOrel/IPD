
// Type: Intermech.Controls.Grid.StringHelpers
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.IO;


namespace Intermech.Controls.Grid;

internal class StringHelpers
{
  public static string TruncateString(string strText, int nWidth, Graphics subDC, Font font)
  {
    string str = "";
    if ((double) StringHelpers.MeasureMultiLineString(strText, subDC, font).Width < (double) nWidth)
      return strText;
    if ((int) subDC.MeasureString("...", font).Width > nWidth)
      return "";
    StringReader stringReader = new StringReader(strText);
    string text1;
    while ((text1 = stringReader.ReadLine()) != null)
    {
      if ((double) subDC.MeasureString(text1, font).Width < (double) nWidth)
      {
        str = $"{str}{text1}\n";
      }
      else
      {
        for (int length = text1.Length; length != 0; --length)
        {
          string text2 = text1.Substring(0, length) + "...";
          if ((double) subDC.MeasureString(text2, font).Width < (double) nWidth)
          {
            str = $"{str}{text2}\n";
            break;
          }
        }
      }
    }
    if (str.Length > 1)
      str.Remove(str.Length - 1, 1);
    return str;
  }

  public static SizeF MeasureMultiLineString(string strText, Graphics mDC, Font font)
  {
    StringReader stringReader = new StringReader(strText);
    SizeF sizeF1 = new SizeF(0.0f, 0.0f);
    string text;
    while ((text = stringReader.ReadLine()) != null)
    {
      SizeF sizeF2 = mDC.MeasureString(text, font);
      sizeF1.Height += sizeF2.Height;
      if ((double) sizeF1.Width < (double) sizeF2.Width)
        sizeF1.Width = sizeF2.Width;
    }
    return sizeF1;
  }

  public static StringAlignment ConvertContentAlignmentToVerticalStringAlignment(
    ContentAlignment alignment)
  {
    StringAlignment verticalStringAlignment = StringAlignment.Near;
    switch (alignment)
    {
      case ContentAlignment.TopLeft:
      case ContentAlignment.TopCenter:
      case ContentAlignment.TopRight:
        verticalStringAlignment = StringAlignment.Near;
        break;
      case ContentAlignment.MiddleLeft:
      case ContentAlignment.MiddleCenter:
      case ContentAlignment.MiddleRight:
        verticalStringAlignment = StringAlignment.Center;
        break;
      case ContentAlignment.BottomLeft:
      case ContentAlignment.BottomCenter:
      case ContentAlignment.BottomRight:
        verticalStringAlignment = StringAlignment.Far;
        break;
    }
    return verticalStringAlignment;
  }

  public static StringAlignment ConvertContentAlignmentToHorizontalStringAlignment(
    ContentAlignment alignment)
  {
    StringAlignment horizontalStringAlignment = StringAlignment.Near;
    switch (alignment)
    {
      case ContentAlignment.TopLeft:
      case ContentAlignment.MiddleLeft:
      case ContentAlignment.BottomLeft:
        horizontalStringAlignment = StringAlignment.Near;
        break;
      case ContentAlignment.TopCenter:
      case ContentAlignment.MiddleCenter:
      case ContentAlignment.BottomCenter:
        horizontalStringAlignment = StringAlignment.Center;
        break;
      case ContentAlignment.TopRight:
      case ContentAlignment.MiddleRight:
      case ContentAlignment.BottomRight:
        horizontalStringAlignment = StringAlignment.Far;
        break;
    }
    return horizontalStringAlignment;
  }
}
