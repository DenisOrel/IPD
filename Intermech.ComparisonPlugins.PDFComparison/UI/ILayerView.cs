// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.ILayerView
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Drawing;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

public interface ILayerView
{
  event EventHandler ClickOpenButton;

  event EventHandler ClickNextPageButton;

  event EventHandler ClickPrevPageButton;

  event EventHandler ChangedPageNumber;

  int PageNumber { get; }

  void UpdateUI(string fileCaption, int pageNumber, int pageCount);

  void SetColor(Color color);
}
