// Decompiled with JetBrains decompiler
// Type: Intermech.Paint.GraphicsDrawRtfText
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System.Drawing;

#nullable disable
namespace Intermech.Paint;

public static class GraphicsDrawRtfText
{
  [CanBeNull]
  private static RichTextBoxAdv _rtfDrawer;

  public static void DrawRtfText([NotNull] this Graphics graphics, [NotNull] string rtf, Rectangle layoutArea)
  {
    if (GraphicsDrawRtfText._rtfDrawer == null)
      GraphicsDrawRtfText._rtfDrawer = new RichTextBoxAdv();
    GraphicsDrawRtfText._rtfDrawer.Rtf = rtf;
    GraphicsDrawRtfText._rtfDrawer.Draw(graphics, layoutArea);
  }
}
