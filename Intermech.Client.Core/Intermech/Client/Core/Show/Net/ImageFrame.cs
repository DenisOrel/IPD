
// Type: Intermech.Client.Core.Show.Net.ImageFrame
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace Intermech.Client.Core.Show.Net;

/// <summary>интерфейс работы с блоком</summary>
[DebuggerDisplay("{Name} {NameId}")]
internal class ImageFrame : IDisposable
{
  private const float MmScale = 0.264583319f;
  private readonly FrameDimension _frame;
  private readonly int _frameIndex;
  private Image _image;

  /// <summary>имя блока</summary>
  public override string ToString() => this.Name;

  /// <summary>имя блока</summary>
  internal string Name { get; private set; }

  /// <summary>Id блока</summary>
  internal string NameId { get; private set; }

  /// <summary>пересчитать границы для блока</summary>
  internal RectangleF Bounds { get; private set; }

  internal ImageFrame(
    string name,
    string nameId,
    Image image,
    FrameDimension frame,
    int frameIndex)
  {
    this.Name = name;
    this.NameId = nameId;
    this._frame = frame;
    this._frameIndex = frameIndex;
    this._image = image;
    Size size = this.Image.Size;
    this.Bounds = new RectangleF(0.0f, 0.0f, (float) size.Width * 0.264583319f, (float) size.Height * 0.264583319f);
  }

  internal Image Image
  {
    get
    {
      lock (this)
      {
        if (this._image != null)
        {
          if (this._frame != null)
            this._image.SelectActiveFrame(this._frame, this._frameIndex);
        }
      }
      return this._image;
    }
  }

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~ImageFrame() => this.Dispose(false);

  private void Dispose(bool disposing) => this._image = (Image) null;
}
