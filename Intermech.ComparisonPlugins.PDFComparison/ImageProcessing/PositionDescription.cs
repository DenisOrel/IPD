// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.PositionDescription
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System.Drawing;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;

public class PositionDescription
{
  public float Angle { get; }

  public double Scale { get; }

  public Point Offset { get; }

  public PositionDescription(float angle, double scale, Point offset)
  {
    this.Angle = angle;
    this.Scale = scale;
    this.Offset = offset;
  }
}
