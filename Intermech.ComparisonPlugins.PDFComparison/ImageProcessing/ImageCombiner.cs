// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.ImageCombiner
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;

public class ImageCombiner
{
  private Image _sample1;
  private Image _sample2;
  private readonly Image _imageEmpty = (Image) new Bitmap(1, 1);
  private ImageTransformer _topImage = new ImageTransformer();
  private ImageContainer _lowImage = new ImageContainer();
  private ImageContainer _combinedImage = new ImageContainer();
  private ViewType _viewType;
  private Size _size;

  public event EventHandler PageChanged;

  public event EventHandler ImageChanged;

  public Image Image
  {
    get
    {
      switch (this._viewType)
      {
        case ViewType.TopLayer:
          return this._topImage.Data;
        case ViewType.LowLayer:
          return this._lowImage.Data;
        case ViewType.Combine:
          return this._combinedImage.Data;
        default:
          return this._imageEmpty;
      }
    }
  }

  public ImageCombiner(ViewType viewType) => this._viewType = viewType;

  public void SetLayerSamples(Image image1, Image image2)
  {
    this._sample1 = image1;
    this._sample2 = image2;
    this._size = this.getMaxSize(this._sample1.Size, this._sample2.Size);
    this._topImage.Add(this._sample1, this._size);
    this._lowImage.Data = this.resizeImage(this._sample2, this._size);
    this._combinedImage.Data = (Image) this.getCombinedImage(this._topImage.Data, this._lowImage.Data);
    EventHandler pageChanged = this.PageChanged;
    if (pageChanged == null)
      return;
    pageChanged((object) null, EventArgs.Empty);
  }

  public void SetTransform(PositionDescription position, int viewType)
  {
    this._viewType = (ViewType) viewType;
    this._topImage.SetTransform(position);
    Size maxSize = this.getMaxSize(this._topImage.Data.Size, this._size);
    if (this._lowImage.Data.Size != maxSize)
      this._lowImage.Data = this.resizeImage(this._sample2, maxSize);
    this._combinedImage.Data = (Image) this.getCombinedImage(this._topImage.Data, this._lowImage.Data);
    EventHandler imageChanged = this.ImageChanged;
    if (imageChanged == null)
      return;
    imageChanged((object) null, EventArgs.Empty);
  }

  private unsafe Bitmap getCombinedImage2(Image img1, Image img2)
  {
    if (img1.Size != img2.Size)
      throw new Exception("Изображения не совпадают по ширине и высоте");
    Bitmap combinedImage2 = new Bitmap(img1.Width, img1.Height, PixelFormat.Format32bppArgb);
    Rectangle rect = new Rectangle(Point.Empty, img1.Size);
    BitmapData bitmapdata1 = combinedImage2.LockBits(rect, ImageLockMode.ReadWrite, combinedImage2.PixelFormat);
    BitmapData bitmapdata2 = (img1 as Bitmap).LockBits(rect, ImageLockMode.ReadOnly, img1.PixelFormat);
    BitmapData bitmapdata3 = (img2 as Bitmap).LockBits(rect, ImageLockMode.ReadOnly, img2.PixelFormat);
    try
    {
      int num1 = Image.GetPixelFormatSize(combinedImage2.PixelFormat) / 8;
      int num2 = bitmapdata1.Width * num1;
      int height = bitmapdata1.Height;
      for (int index1 = 0; index1 < height; ++index1)
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) bitmapdata1.Scan0 + index1 * bitmapdata1.Stride);
        byte* numPtr2 = (byte*) ((IntPtr) (void*) bitmapdata2.Scan0 + index1 * bitmapdata2.Stride);
        byte* numPtr3 = (byte*) ((IntPtr) (void*) bitmapdata3.Scan0 + index1 * bitmapdata3.Stride);
        for (int index2 = 0; index2 < num2; index2 += num1)
        {
          numPtr1[index2] = numPtr2[index2];
          numPtr1[index2 + 1] = numPtr2[index2 + 1];
          numPtr1[index2 + 2] = numPtr3[index2 + 2];
          numPtr1[index2 + 3] = byte.MaxValue;
        }
      }
    }
    finally
    {
      combinedImage2.UnlockBits(bitmapdata1);
      (img1 as Bitmap).UnlockBits(bitmapdata2);
      (img2 as Bitmap).UnlockBits(bitmapdata3);
    }
    return combinedImage2;
  }

  private unsafe Bitmap getCombinedImage(Image img1, Image img2)
  {
    if (img1.Size != img2.Size)
      throw new Exception("Изображения не совпадают по ширине и высоте");
    Bitmap combinedImage = (Bitmap) img1.Clone();
    Rectangle rect = new Rectangle(Point.Empty, img1.Size);
    BitmapData bitmapdata1 = combinedImage.LockBits(rect, ImageLockMode.ReadWrite, combinedImage.PixelFormat);
    BitmapData bitmapdata2 = (img2 as Bitmap).LockBits(rect, ImageLockMode.ReadOnly, img2.PixelFormat);
    try
    {
      int num1 = Image.GetPixelFormatSize(combinedImage.PixelFormat) / 8;
      int num2 = bitmapdata1.Width * num1;
      int height = bitmapdata1.Height;
      for (int index1 = 0; index1 < height; ++index1)
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) bitmapdata1.Scan0 + index1 * bitmapdata1.Stride);
        byte* numPtr2 = (byte*) ((IntPtr) (void*) bitmapdata2.Scan0 + index1 * bitmapdata2.Stride);
        for (int index2 = 0; index2 < num2; index2 += num1)
          numPtr1[index2 + 2] = numPtr2[index2 + 2];
      }
    }
    finally
    {
      combinedImage.UnlockBits(bitmapdata1);
      (img2 as Bitmap).UnlockBits(bitmapdata2);
    }
    return combinedImage;
  }

  private Image resizeImage(Image image, Size newSize)
  {
    if (image.Size == newSize)
      return (Image) image.Clone();
    float num1 = (float) newSize.Width / (float) image.Width;
    float num2 = (float) newSize.Height / (float) image.Height;
    int width = image.Width;
    int height = image.Height;
    Image image1 = (Image) new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
    using (Graphics graphics = Graphics.FromImage(image1))
    {
      graphics.Clear(Color.White);
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.DrawImage(image, newSize.Width - image.Width, newSize.Height - image.Height, image.Width, image.Height);
    }
    return image1;
  }

  private Size getMaxSize(Size size1, Size size2)
  {
    return new Size(size1.Width > size2.Width ? size1.Width : size2.Width, size1.Height > size2.Height ? size1.Height : size2.Height);
  }
}
