
// Type: Intermech.Extensions.ContentAlignmentConvertor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Drawing;


namespace Intermech.Extensions
{
    public static class ContentAlignmentConvertor
    {
      public static StringAlignment ToStringAlignment(this ContentAlignment contentAlignment, Axis axis)
      {
        if (axis != Axis.Horizontal)
        {
          if (axis != Axis.Vertical)
            throw new Exception("Only horizontal and vertical axis supported");
          switch (contentAlignment)
          {
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
              return StringAlignment.Near;
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleRight:
              return StringAlignment.Center;
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
              return StringAlignment.Far;
            default:
              throw new Exception("Unknown ContentAlignment");
          }
        }
        else
        {
          switch (contentAlignment)
          {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
              return StringAlignment.Near;
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
              return StringAlignment.Center;
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
              return StringAlignment.Far;
            default:
              throw new Exception("Unknown ContentAlignment");
          }
        }
      }
    }
}
