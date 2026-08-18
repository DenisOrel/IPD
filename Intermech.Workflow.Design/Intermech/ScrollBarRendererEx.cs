// Decompiled with JetBrains decompiler
// Type: Intermech.ScrollBarRendererEx
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech;

public class ScrollBarRendererEx
{
  public const int DFC_SCROLL = 3;
  public const int DFCS_SCROLLDOWN = 1;
  public const int DFCS_SCROLLUP = 0;

  [DllImport("user32")]
  public static extern int DrawFrameControl(
    IntPtr hdc,
    ref ScrollBarRendererEx.RECT lpRect,
    int un1,
    int un2);

  private static int ScrollBarArrowButtonStateToInt(ScrollBarArrowButtonState state)
  {
    return state == ScrollBarArrowButtonState.DownNormal ? 1 : 0;
  }

  public static void DrawArrowButton(Graphics g, Rectangle bounds, ScrollBarArrowButtonState state)
  {
    if (ScrollBarRenderer.IsSupported)
    {
      ScrollBarRenderer.DrawArrowButton(g, bounds, state);
    }
    else
    {
      IntPtr hdc = g.GetHdc();
      try
      {
        ScrollBarRendererEx.RECT lpRect = new ScrollBarRendererEx.RECT(0, 0, SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight);
        ScrollBarRendererEx.DrawFrameControl(hdc, ref lpRect, 3, ScrollBarRendererEx.ScrollBarArrowButtonStateToInt(state));
      }
      finally
      {
        g.ReleaseHdc(hdc);
      }
    }
  }

  public struct RECT(int left, int top, int right, int bottom)
  {
    public int Left = left;
    public int Top = top;
    public int Right = right;
    public int Bottom = bottom;
  }
}
