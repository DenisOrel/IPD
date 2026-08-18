// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.IMainView
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Drawing;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

public interface IMainView
{
  event EventHandler ChangedView;

  ILayerView TopLayerView { get; }

  ILayerView LowLayerView { get; }

  float Angle { get; }

  double Zoom { get; }

  Point Offset { get; }

  int ViewType { get; }

  void SetImage(Image image);

  void UpdateImage(Image image);
}
