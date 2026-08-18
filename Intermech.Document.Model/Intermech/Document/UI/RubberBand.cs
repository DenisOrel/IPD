// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.RubberBand
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс для рисования линий по XOR</summary>
internal class RubberBand
{
  private static int GetColorRop(Color color, int darkROP, int lightROP)
  {
    return (double) color.GetBrightness() < 0.5 ? darkROP : lightROP;
  }

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern int SetROP2(IntPtr hdc, int enDrawMode);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr CreatePen(int enPenStyle, int nWidth, int crColor);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern bool DeleteObject(IntPtr hObject);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern void Rectangle(IntPtr hdc, int X1, int Y1, int X2, int Y2);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr GetStockObject(int brStyle);

  private static int RGB(int R, int G, int B) => R | G << 8 | B << 16 /*0x10*/;

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern bool MoveToEx(IntPtr hdc, int x, int y, RubberBand.POINT pt);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern bool LineTo(IntPtr hdc, int x, int y);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern int SetBkColor(IntPtr hDC, int clr);

  internal static void DrawXorLine(Graphics g, Point start, Point end, Color backColor)
  {
    int brStyle = 5;
    int lightROP = 7;
    int darkROP = 10;
    int enPenStyle = 2;
    int colorRop = RubberBand.GetColorRop(backColor, darkROP, lightROP);
    Color c = Color.Black;
    if (colorRop == darkROP)
      c = Color.White;
    IntPtr hdc = g.GetHdc();
    try
    {
      IntPtr pen = RubberBand.CreatePen(enPenStyle, 1, ColorTranslator.ToWin32(backColor));
      int enDrawMode = RubberBand.SetROP2(hdc, colorRop);
      IntPtr hObject1 = RubberBand.SelectObject(hdc, pen);
      IntPtr hObject2 = RubberBand.SelectObject(hdc, RubberBand.GetStockObject(brStyle));
      RubberBand.SetBkColor(hdc, ColorTranslator.ToWin32(c));
      RubberBand.MoveToEx(hdc, start.X, start.Y, (RubberBand.POINT) null);
      RubberBand.LineTo(hdc, end.X, end.Y);
      RubberBand.SetROP2(hdc, enDrawMode);
      RubberBand.SelectObject(hdc, hObject2);
      RubberBand.SelectObject(hdc, hObject1);
      RubberBand.DeleteObject(pen);
    }
    finally
    {
      g.ReleaseHdc(hdc);
    }
  }

  internal static void DrawXorRectangle(Graphics g, System.Drawing.Rectangle rect, Color backColor)
  {
    int brStyle = 5;
    int lightROP = 7;
    int darkROP = 10;
    int enPenStyle = 2;
    int colorRop = RubberBand.GetColorRop(backColor, darkROP, lightROP);
    Color c = Color.Black;
    if (colorRop == darkROP)
      c = Color.White;
    IntPtr hdc = g.GetHdc();
    try
    {
      IntPtr pen = RubberBand.CreatePen(enPenStyle, 1, ColorTranslator.ToWin32(backColor));
      int enDrawMode = RubberBand.SetROP2(hdc, colorRop);
      IntPtr hObject1 = RubberBand.SelectObject(hdc, pen);
      IntPtr hObject2 = RubberBand.SelectObject(hdc, RubberBand.GetStockObject(brStyle));
      RubberBand.SetBkColor(hdc, ColorTranslator.ToWin32(c));
      RubberBand.Rectangle(hdc, rect.X, rect.Y, rect.Right, rect.Bottom);
      RubberBand.SetROP2(hdc, enDrawMode);
      RubberBand.SelectObject(hdc, hObject2);
      RubberBand.SelectObject(hdc, hObject1);
      RubberBand.DeleteObject(pen);
    }
    finally
    {
      g.ReleaseHdc(hdc);
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  internal class POINT
  {
    public int x;
    public int y;

    public POINT()
    {
    }

    public POINT(int x, int y)
    {
      this.x = x;
      this.y = y;
    }
  }
}
