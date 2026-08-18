// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.MainPresenter
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;
using System;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

internal class MainPresenter
{
  private IMainView view { get; }

  private ComparisonProvider comparisonProvider { get; }

  private ILayerPresenter topLayerPresenter { get; }

  private ILayerPresenter lowLayerPresenter { get; }

  private ImageCombiner imageCombiner { get; }

  public MainPresenter(IMainView view, ComparisonProvider comparisonProvider)
  {
    this.view = view;
    this.comparisonProvider = comparisonProvider;
    view.ChangedView += new EventHandler(this.View_ChangedView);
    this.imageCombiner = new ImageCombiner((ViewType) view.ViewType);
    this.imageCombiner.PageChanged += new EventHandler(this.ImageCombiner_PageChanged);
    this.imageCombiner.ImageChanged += new EventHandler(this.ImageCombiner_ImageChanged);
    this.topLayerPresenter = (ILayerPresenter) new LayerPresenter(view.TopLayerView);
    this.lowLayerPresenter = (ILayerPresenter) new LayerPresenter(view.LowLayerView);
    this.topLayerPresenter.PageUpdated += new EventHandler(this.LayerPresenter_PageUpdated);
    this.topLayerPresenter.OnSelectObjectClick += new EventHandler(this.LayerPresenter_OnSelectObjectClick);
    this.topLayerPresenter.LoadFile(comparisonProvider.SelectFirstComparedFile());
    this.lowLayerPresenter.PageUpdated += new EventHandler(this.LayerPresenter_PageUpdated);
    this.lowLayerPresenter.OnSelectObjectClick += new EventHandler(this.LayerPresenter_OnSelectObjectClick);
    this.lowLayerPresenter.LoadFile(comparisonProvider.SelectSecondComparedFile());
  }

  private void LayerPresenter_OnSelectObjectClick(object sender, EventArgs e)
  {
    (sender as ILayerPresenter).LoadFile(this.comparisonProvider.SelectComparedVersion());
  }

  private void ImageCombiner_PageChanged(object sender, EventArgs e)
  {
    this.view.SetImage(this.imageCombiner.Image);
  }

  private void ImageCombiner_ImageChanged(object sender, EventArgs e)
  {
    this.view.UpdateImage(this.imageCombiner.Image);
  }

  private void LayerPresenter_PageUpdated(object sender, EventArgs e)
  {
    this.imageCombiner.SetLayerSamples(this.topLayerPresenter.PageImage, this.lowLayerPresenter.PageImage);
  }

  private void View_ChangedView(object sender, EventArgs e)
  {
    this.imageCombiner.SetTransform(new PositionDescription(this.view.Angle, this.view.Zoom, this.view.Offset), this.view.ViewType);
  }
}
