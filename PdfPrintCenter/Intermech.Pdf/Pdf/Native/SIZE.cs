// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.SIZE
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct SIZE
{
  public int cx;
  public int cy;

  public static implicit operator SizeF(SIZE rect) => new SizeF((float) rect.cx, (float) rect.cy);
}
