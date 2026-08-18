// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.Common.SamplePage
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.Common;

public class SamplePage : IDisposable
{
  public static readonly SamplePage Empty = new SamplePage(0, (Image) new Bitmap(1, 1, PixelFormat.Format32bppArgb));

  public int Number { get; }

  public Image Image { get; }

  public SamplePage(int number, Image image)
  {
    this.Number = number;
    this.Image = image;
  }

  public void Dispose() => this.Image?.Dispose();
}
