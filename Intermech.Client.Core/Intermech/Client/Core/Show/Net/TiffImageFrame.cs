
// Type: Intermech.Client.Core.Show.Net.TiffImageFrame
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using BitMiracle.LibTiff.Classic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Show.Net;

/// <summary>интерфейс работы с блоком</summary>
[DebuggerDisplay("{Name} {NameId}")]
internal class TiffImageFrame : IDisposable
{
  private const float MM_Scale = 0.264583319f;
  /// <summary>имя блока</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string _name;
  /// <summary>Id блока</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string _nameId;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private RectangleF _bounds;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Image _image;
  private Tiff _tiff;
  private int _frameIndex;

  /// <summary>имя блока</summary>
  public override string ToString() => this._name;

  /// <summary>имя блока</summary>
  internal string Name => this._name;

  /// <summary>Id блока</summary>
  internal string NameId => this._nameId;

  internal TiffImageFrame(string name, string nameId, Tiff tiff, int frameIndex)
  {
    this._name = name;
    this._nameId = nameId;
    this._tiff = tiff;
    this._image = (Image) null;
    this._frameIndex = frameIndex;
    this.SelectActiveFrame();
    this._bounds = new RectangleF(0.0f, 0.0f, (float) this._image.Width, (float) this._image.Height);
    this._bounds.Height *= 0.264583319f;
    this._bounds.Width *= 0.264583319f;
  }

  /// <summary>пересчитать границы для блока</summary>
  internal RectangleF Bounds => this._bounds;

  internal Image Image
  {
    get
    {
      if (this._image == null)
        this._image = this.CreateImage();
      return this._image;
    }
  }

  private Image CreateImage()
  {
    this._tiff.SetDirectory((short) this._frameIndex);
    int height = this._tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
    int width = this._tiff.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
    FieldValue[] field1 = this._tiff.GetField(TiffTag.BITSPERSAMPLE);
    short num1 = 0;
    if (field1 != null && field1.Length != 0)
      num1 = field1[0].ToShort();
    if (num1 == (short) 1)
    {
      FieldValue[] field2 = this._tiff.GetField(TiffTag.SAMPLESPERPIXEL);
      short num2 = 1;
      if (field2 != null && field2.Length != 0)
        num2 = field2[0].ToShort();
      if (num2 == (short) 1)
      {
        Photometric photometric = (Photometric) this._tiff.GetField(TiffTag.PHOTOMETRIC)[0].ToInt();
        switch (photometric)
        {
          case Photometric.MINISWHITE:
          case Photometric.MINISBLACK:
            int length = this._tiff.ScanlineSize();
            Bitmap image = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            bool flag = photometric == Photometric.MINISWHITE;
            ColorPalette palette = image.Palette;
            palette.Entries[0] = flag ? Color.White : Color.Black;
            palette.Entries[1] = flag ? Color.Black : Color.White;
            image.Palette = palette;
            for (int index = 0; index < height; ++index)
            {
              Rectangle rect = new Rectangle(0, index, width, 1);
              BitmapData bitmapdata = image.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
              byte[] numArray = new byte[length];
              this._tiff.ReadScanline(numArray, index);
              Marshal.Copy(numArray, 0, bitmapdata.Scan0, numArray.Length);
              image.UnlockBits(bitmapdata);
            }
            if (width > 2048 /*0x0800*/)
            {
              Bitmap bitmap = new Bitmap(width / 3, height / 3, PixelFormat.Format24bppRgb);
              using (Graphics graphics = Graphics.FromImage((Image) bitmap))
              {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image) image, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                image.Dispose();
                image = bitmap;
              }
            }
            return (Image) image;
        }
      }
    }
    int[] raster = new int[height * width];
    if (!this._tiff.ReadRGBAImage(width, height, raster))
    {
      int num3 = (int) MessageBox.Show("Could not read image");
      return (Image) null;
    }
    Bitmap image1 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
    Rectangle rect1 = new Rectangle(0, 0, image1.Width, image1.Height);
    BitmapData bitmapdata1 = image1.LockBits(rect1, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
    byte[] source = new byte[bitmapdata1.Stride * bitmapdata1.Height];
    for (int index1 = 0; index1 < image1.Height; ++index1)
    {
      int num4 = index1 * image1.Width;
      int num5 = (image1.Height - index1 - 1) * bitmapdata1.Stride;
      for (int index2 = 0; index2 < image1.Width; ++index2)
      {
        int num6 = raster[num4++];
        byte[] numArray1 = source;
        int index3 = num5;
        int num7 = index3 + 1;
        int num8 = (int) (byte) (num6 >> 16 /*0x10*/ & (int) byte.MaxValue);
        numArray1[index3] = (byte) num8;
        byte[] numArray2 = source;
        int index4 = num7;
        int num9 = index4 + 1;
        int num10 = (int) (byte) (num6 >> 8 & (int) byte.MaxValue);
        numArray2[index4] = (byte) num10;
        byte[] numArray3 = source;
        int index5 = num9;
        int num11 = index5 + 1;
        int num12 = (int) (byte) (num6 & (int) byte.MaxValue);
        numArray3[index5] = (byte) num12;
        byte[] numArray4 = source;
        int index6 = num11;
        num5 = index6 + 1;
        int num13 = (int) (byte) (num6 >> 24 & (int) byte.MaxValue);
        numArray4[index6] = (byte) num13;
      }
    }
    Marshal.Copy(source, 0, bitmapdata1.Scan0, source.Length);
    image1.UnlockBits(bitmapdata1);
    return (Image) image1;
  }

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~TiffImageFrame() => this.Dispose(false);

  private void Dispose(bool disposing)
  {
    if (this._image != null)
      this._image.Dispose();
    this._image = (Image) null;
    this._name = (string) null;
    this._nameId = (string) null;
  }

  internal void SelectActiveFrame()
  {
    lock (this)
    {
      if (this._image != null)
        return;
      this._image = this.CreateImage();
    }
  }
}
