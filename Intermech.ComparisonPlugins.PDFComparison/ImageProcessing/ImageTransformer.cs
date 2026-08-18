// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.ImageTransformer
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;

public class ImageTransformer
{
  private ImageContainer _sampleImage = new ImageContainer();
  private ImageContainer _scaledImage = new ImageContainer();
  private ImageContainer _rotatedImage = new ImageContainer();
  private ImageContainer _offsettedImage = new ImageContainer();
  private float _angle;
  private double _scale;
  private Point _offset;
  private Size _canvasSize;
  private ImageTransformer.RenderingStage _renderingStage = ImageTransformer.RenderingStage.Unknown;

  public Image Data => this._offsettedImage.Data;

  private ImageTransformer.RenderingStage renderingStage
  {
    get => this._renderingStage;
    set
    {
      if (this._renderingStage != ImageTransformer.RenderingStage.Unknown && value != ImageTransformer.RenderingStage.Unknown)
        return;
      this._renderingStage = value;
    }
  }

  private double scale
  {
    get => this._scale;
    set
    {
      if (this._scale == value)
        return;
      this._scale = value;
      this.renderingStage = ImageTransformer.RenderingStage.Scale;
    }
  }

  private float angle
  {
    get => this._angle;
    set
    {
      if ((double) this._angle == (double) value)
        return;
      this._angle = value;
      this.renderingStage = ImageTransformer.RenderingStage.Angle;
    }
  }

  private Point offset
  {
    get => this._offset;
    set
    {
      if (this._offset.Equals((object) value))
        return;
      this._offset = value;
      this.renderingStage = ImageTransformer.RenderingStage.Offset;
    }
  }

  public Image Add(Image image, Size canvasSize)
  {
    this.angle = 0.0f;
    this.scale = 100.0;
    this.offset = Point.Empty;
    this._canvasSize = canvasSize;
    this._sampleImage.Data = (Image) image.Clone();
    this._scaledImage.Data = (Image) image.Clone();
    this._rotatedImage.Data = (Image) image.Clone();
    this._offsettedImage.Data = this.ImageOffset(image, (PointF) this.offset, canvasSize);
    return this.Data;
  }

  private void pipelineRndering(ImageTransformer.RenderingStage renderingStage)
  {
    switch (renderingStage)
    {
      case ImageTransformer.RenderingStage.Scale:
        this._scaledImage.Data = this.ImageTransform(this._sampleImage.Data, this.scale, 0.0f);
        goto case ImageTransformer.RenderingStage.Angle;
      case ImageTransformer.RenderingStage.Angle:
        this._rotatedImage.Data = this.ImageTransform(this._scaledImage.Data, 100.0, this.angle);
        goto case ImageTransformer.RenderingStage.Offset;
      case ImageTransformer.RenderingStage.Offset:
        this._offsettedImage.Data = this.ImageOffset(this._rotatedImage.Data, (PointF) this.offset, this._canvasSize);
        this.renderingStage = ImageTransformer.RenderingStage.Unknown;
        break;
    }
  }

  public Image SetTransform(PositionDescription position)
  {
    this.angle = position.Angle;
    this.scale = position.Scale;
    this.offset = position.Offset;
    this.pipelineRndering(this.renderingStage);
    return this._offsettedImage.Data;
  }

  private Image ImageTransform(Image image, double scale, float angle, PointF offset)
  {
    if ((double) angle == 0.0 && offset.IsEmpty && scale == 100.0)
      return (Image) image.Clone();
    float num1 = (float) scale / 100f;
    PointF pointF = new PointF((float) image.Width / 2f, (float) image.Height / 2f);
    double num2 = (double) angle * Math.PI / 180.0;
    double num3 = Math.Abs(Math.Cos(num2));
    double num4 = Math.Abs(Math.Sin(num2));
    int width = (int) ((double) image.Width * num3 + (double) image.Height * num4);
    int height = (int) ((double) image.Width * num4 + (double) image.Height * num3);
    Image image1 = (Image) new Bitmap(width, height, PixelFormat.Format32bppArgb);
    using (Graphics graphics = Graphics.FromImage(image1))
    {
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.Clear(Color.White);
      graphics.TranslateTransform((float) (((double) width - (double) image.Width * (double) num1) / 2.0), (float) (((double) height - (double) image.Height * (double) num1) / 2.0));
      graphics.TranslateTransform(offset.X, offset.Y);
      graphics.TranslateTransform(pointF.X, pointF.Y);
      graphics.RotateTransform(angle);
      graphics.DrawImage(image, -pointF.X, -pointF.Y, (float) image.Width * num1, (float) image.Height * num1);
    }
    return image1;
  }

  private Image ImageTransform(Image image, double scale, float angle)
  {
    if ((double) angle == 0.0 && this.offset.IsEmpty && scale == 100.0)
      return (Image) image.Clone();
    float num1 = (float) scale / 100f;
    PointF pointF = new PointF((float) image.Width / 2f, (float) image.Height / 2f);
    double num2 = (double) angle * Math.PI / 180.0;
    double num3 = Math.Abs(Math.Cos(num2));
    double num4 = Math.Abs(Math.Sin(num2));
    int width = (int) ((double) image.Width * num3 + (double) image.Height * num4);
    int height = (int) ((double) image.Width * num4 + (double) image.Height * num3);
    Image image1 = (Image) new Bitmap(width, height, PixelFormat.Format32bppArgb);
    using (Graphics graphics = Graphics.FromImage(image1))
    {
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.Clear(Color.White);
      graphics.TranslateTransform((float) (((double) width - (double) image.Width * (double) num1) / 2.0), (float) (((double) height - (double) image.Height * (double) num1) / 2.0));
      graphics.TranslateTransform(pointF.X, pointF.Y);
      graphics.RotateTransform(angle);
      graphics.DrawImage(image, -pointF.X, -pointF.Y, (float) image.Width * num1, (float) image.Height * num1);
    }
    return image1;
  }

  private Image ImageOffset(Image image, PointF offset, Size canvasSize)
  {
    int width = image.Width > canvasSize.Width ? image.Width : canvasSize.Width;
    int height = image.Height > canvasSize.Height ? image.Height : canvasSize.Height;
    PointF pointF = new PointF((float) (width - image.Width), (float) (height - image.Height));
    Image image1 = (Image) new Bitmap(width, height, PixelFormat.Format32bppArgb);
    using (Graphics graphics = Graphics.FromImage(image1))
    {
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.Clear(Color.White);
      graphics.TranslateTransform(pointF.X, pointF.Y);
      graphics.DrawImage(image, offset.X, offset.Y, (float) image.Width, (float) image.Height);
    }
    return image1;
  }

  private enum RenderingStage
  {
    Scale,
    Angle,
    Offset,
    Unknown,
  }
}
