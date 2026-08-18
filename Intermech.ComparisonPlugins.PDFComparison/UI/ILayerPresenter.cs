// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.ILayerPresenter
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.ComparisonPlugins.PDFComparison.Common;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

internal interface ILayerPresenter
{
  event EventHandler PageUpdated;

  event EventHandler OnSelectObjectClick;

  void LoadFile(FileDescription comparedFile);

  Image PageImage { get; }
}
