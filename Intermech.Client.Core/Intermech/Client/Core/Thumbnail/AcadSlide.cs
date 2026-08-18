
// Type: Intermech.Client.Core.Thumbnail.AcadSlide
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;


namespace Intermech.Client.Core.Thumbnail;

public class AcadSlide : IThumbImage, IThumbImageProvider
{
  private bool _swap;
  private Point _min;
  private Point _max;
  private short _lastX;
  private short _lastY;
  private ArrayList _list;
  private Rectangle _bounds;
  private Color _color;
  private Color _backColor;
  private Metafile _metafile;
  private static float[] _brightfac = new float[5]
  {
    1f,
    0.65f,
    0.5f,
    0.3f,
    0.15f
  };

  private void RaiseLoadingError()
  {
    throw new ArgumentException("Ошибка загрузки слайда из потока");
  }

  public AcadSlide(Stream data)
    : this(data, AcadSlide.InvertColor(SystemColors.Window))
  {
  }

  public AcadSlide(Stream data, Color backColor)
  {
    this._swap = false;
    this._min = new Point(100000, 100000);
    this._max = new Point(0, 0);
    this._lastX = (short) 0;
    this._lastY = (short) 0;
    this._list = new ArrayList();
    this._color = Color.Transparent;
    this._backColor = backColor;
    this.ReadSlide(data);
    this.Optimize();
  }

  public static Color InvertColor(Color color)
  {
    return Color.FromArgb((int) color.A, (int) ~color.R & (int) byte.MaxValue, (int) ~color.G & (int) byte.MaxValue, (int) ~color.B & (int) byte.MaxValue);
  }

  private bool ReadHeader(Stream data)
  {
    byte[] buffer = new byte[31 /*0x1F*/];
    if (data.Read(buffer, 0, 23) != 23)
      return false;
    if (buffer[18] == (byte) 1)
    {
      data.Position += 11L;
    }
    else
    {
      if (data.Read(buffer, 23, 8) != 8)
        return false;
      this._swap = buffer[29] != (byte) 52 && buffer[30] != (byte) 18;
    }
    return true;
  }

  private void ReadSlide(Stream data)
  {
    byte[] numArray = new byte[8];
    if (!this.ReadHeader(data))
    {
      this.RaiseLoadingError();
    }
    else
    {
      bool flag = false;
      while (!flag)
      {
        if (data.Read(numArray, 0, 2) != 2)
        {
          this.RaiseLoadingError();
          return;
        }
        if (this._swap)
          this.Swap(numArray);
        switch (numArray[1])
        {
          case 251:
            if (data.Read(numArray, 2, 3) != 3)
            {
              this.RaiseLoadingError();
              return;
            }
            this.ProcessOffsetVector(numArray);
            continue;
          case 252:
            flag = true;
            continue;
          case 253:
            if (data.Read(numArray, 2, 4) != 4)
            {
              this.RaiseLoadingError();
              return;
            }
            this.ProcessSolidFill(numArray, data);
            continue;
          case 254:
            if (data.Read(numArray, 2, 1) != 1)
            {
              this.RaiseLoadingError();
              return;
            }
            this.ProcessEndpoint(numArray);
            continue;
          case byte.MaxValue:
            this.ProcessColor(numArray[0]);
            continue;
          default:
            if (numArray[1] > (byte) 128 /*0x80*/)
            {
              flag = true;
              continue;
            }
            if (data.Read(numArray, 2, 6) != 6)
            {
              this.RaiseLoadingError();
              return;
            }
            this.ProcessLine(numArray);
            continue;
        }
      }
      int x = this._min.X;
      int y1 = this._min.Y;
      this._max.X -= x;
      this._max.Y -= y1;
      int y2 = this._max.Y;
      int count = this._list.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this._list[index] is AcadObject acadObject)
          acadObject.Patch(x, y1, y2);
      }
      this._bounds = new Rectangle(0, 0, this._max.X, this._max.Y);
    }
  }

  public Rectangle Bounds => this._bounds;

  private void ProcessColor(byte p)
  {
    Color color = p != (byte) 7 ? this.FromAcadIndex((int) p) : this._backColor;
    if (!(color != this._color))
      return;
    this._list.Add((object) color);
    this._color = color;
  }

  private void ProcessLine(byte[] buf)
  {
    this._lastX = this.GetShort(buf[0], buf[1]);
    this._lastY = this.GetShort(buf[2], buf[3], this._swap);
    this.ProcessLine((int) this._lastX, (int) this._lastY, (int) this.GetShort(buf[4], buf[5], this._swap), (int) this.GetShort(buf[6], buf[7], this._swap));
  }

  private void ProcessOffsetVector(byte[] buf)
  {
    this.ProcessLine((int) this._lastX + (int) (sbyte) buf[0], (int) this._lastY + (int) (sbyte) buf[2], (int) this._lastX + (int) (sbyte) buf[3], (int) this._lastY + (int) (sbyte) buf[4]);
    this._lastX += (short) (sbyte) buf[0];
    this._lastY += (short) (sbyte) buf[2];
  }

  private void ProcessSolidFill(byte[] buf, Stream data)
  {
    int num1 = (int) this.GetShort(buf[2], buf[3], this._swap);
    int num2 = (int) this.GetShort(buf[4], buf[5], this._swap);
    int length = num1;
    int num3 = 0;
    Point[] points = new Point[length];
    while (length-- > 0)
    {
      data.Read(buf, 0, 6);
      points[num3++] = new Point((int) this.GetShort(buf[2], buf[3], this._swap), (int) Math.Abs(this.GetShort(buf[4], buf[5], this._swap)));
      this.CheckPoint(points[num3 - 1]);
    }
    this._list.Add((object) new AcadFill(points));
  }

  private void Swap(byte[] buf)
  {
    byte num = buf[0];
    buf[0] = buf[1];
    buf[1] = num;
  }

  private void ProcessEndpoint(byte[] buf)
  {
    this.ProcessLine((int) this._lastX, (int) this._lastY, (int) this._lastX + (int) (sbyte) buf[0], (int) this._lastY + (int) (sbyte) buf[2]);
    this._lastX += (short) (sbyte) buf[0];
    this._lastY += (short) (sbyte) buf[2];
  }

  private short GetShort(byte b1, byte b2)
  {
    return (short) ((int) b1 & (int) byte.MaxValue | (int) b2 << 8);
  }

  private short GetShort(byte b1, byte b2, bool swap)
  {
    return swap ? (short) ((int) b2 & (int) byte.MaxValue | (int) b1 << 8) : (short) ((int) b1 & (int) byte.MaxValue | (int) b2 << 8);
  }

  private void ProcessLine(int x1, int y1, int x2, int y2)
  {
    this.CheckPoint(x1, y1);
    this.CheckPoint(x2, y2);
    this._list.Add((object) new AcadLine(x1, y1, x2, y2));
  }

  private Color FromAcadIndex(int colorIndex)
  {
    float num1 = 0.5f;
    float num2 = 1f;
    float num3 = 0.0f;
    float num4 = 0.0f;
    float num5 = 0.0f;
    float num6;
    switch (colorIndex)
    {
      case 0:
        num3 = 0.0f;
        num4 = 0.0f;
        num5 = 0.0f;
        num6 = 0.0f;
        break;
      case 1:
        num3 = num2;
        num5 = 0.0f;
        num4 = 0.0f;
        num6 = 1f;
        break;
      case 2:
        num3 = num2;
        num5 = num2;
        num4 = 0.0f;
        num6 = 1f;
        break;
      case 3:
        num3 = 0.0f;
        num5 = num2;
        num4 = 0.0f;
        num6 = 1f;
        break;
      case 4:
        num3 = 0.0f;
        num5 = num2;
        num4 = num2;
        num6 = 1f;
        break;
      case 5:
        num3 = 0.0f;
        num5 = 0.0f;
        num4 = num2;
        num6 = 1f;
        break;
      case 6:
        num3 = num2;
        num5 = 0.0f;
        num4 = num2;
        num6 = 1f;
        break;
      case 7:
        num3 = num2;
        num5 = num2;
        num4 = num2;
        num6 = 1f;
        break;
      case 8:
        num3 = num1;
        num5 = num1;
        num4 = num1;
        num6 = 1f;
        break;
      case 9:
        num3 = 0.75f;
        num5 = 0.75f;
        num4 = 0.75f;
        num6 = 1f;
        break;
      default:
        if (colorIndex > 9 && colorIndex < 250)
        {
          int num7 = (colorIndex - 10) / 10;
          if (num7 >= 24)
            num7 -= 24;
          int num8 = colorIndex % 10;
          int num9;
          float num10 = (float) (num9 = (int) ((double) num7 / 4.0)) - (float) num9;
          num6 = AcadSlide._brightfac[num8 >> 1];
          float num11 = (num8 & 1) == 1 ? num1 : 1f;
          switch (num9)
          {
            case 0:
              num3 = 1f;
              num5 = (float) (1.0 - (double) num11 * (1.0 - (double) num10));
              num4 = 1f - num11;
              break;
            case 1:
              num3 = (float) (1.0 - (double) num11 * (double) num10);
              num5 = 1f;
              num4 = 1f - num11;
              break;
            case 2:
              num3 = 1f - num11;
              num5 = 1f;
              num4 = (float) (1.0 - (double) num11 * (1.0 - (double) num10));
              break;
            case 3:
              num3 = 1f - num11;
              num5 = (float) (1.0 - (double) num11 * (double) num10);
              num4 = 1f;
              break;
            case 4:
              num3 = (float) (1.0 - (double) num11 * (1.0 - (double) num10));
              num5 = 1f - num11;
              num4 = 1f;
              break;
            case 5:
              num3 = 1f;
              num5 = 1f - num11;
              num4 = (float) (1.0 - (double) num11 * (double) num10);
              break;
          }
        }
        else
        {
          num6 = (float) (0.33000001311302185 + (double) (colorIndex - 250) * 0.13400000333786011);
          num3 = 1f;
          num5 = 1f;
          num4 = 1f;
          break;
        }
        break;
    }
    return Color.FromArgb((int) ((double) (num3 * num6) * (double) byte.MaxValue), (int) ((double) (num5 * num6) * (double) byte.MaxValue), (int) ((double) (num4 * num6) * (double) byte.MaxValue));
  }

  private void CheckPoint(Point point) => this.CheckPoint(point.X, point.Y);

  private void CheckPoint(int x, int y)
  {
    this._max.X = Math.Max(this._max.X, x);
    this._max.Y = Math.Max(this._max.Y, y);
    this._min.X = Math.Min(this._min.X, x);
    this._min.Y = Math.Min(this._min.Y, y);
  }

  internal void Draw(Graphics graphics)
  {
    Color color1 = Color.Black;
    Pen p = new Pen(Color.Black);
    SolidBrush b = new SolidBrush(Color.Black);
    int count = this._list.Count;
    for (int index = 0; index < count; ++index)
    {
      object obj = this._list[index];
      if (obj is AcadObject acadObject)
      {
        acadObject.Draw(graphics, (Brush) b, p);
      }
      else
      {
        Color color2 = (Color) obj;
        if (color2 != color1)
        {
          p.Color = color2;
          b.Color = color2;
          color1 = color2;
        }
      }
    }
    p.Dispose();
    b.Dispose();
  }

  private void Optimize()
  {
  }

  public int Width => this._bounds.Width;

  public int Height => this._bounds.Height;

  public void PaintTo(Graphics g, Rectangle bounds, Rectangle stretchBounds)
  {
    if (this._metafile == null)
    {
      IntPtr hdc = g.GetHdc();
      this._metafile = new Metafile(hdc, EmfType.EmfOnly);
      g.ReleaseHdc(hdc);
      using (Graphics graphics = Graphics.FromImage((Image) this._metafile))
        this.Draw(graphics);
    }
    g.DrawImage((Image) this._metafile, stretchBounds);
  }

  public Image Image
  {
    get
    {
      if (this._metafile == null)
      {
        using (Bitmap bitmap = new Bitmap(this._bounds.Width, this._bounds.Height))
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          {
            this._metafile = new Metafile(graphics.GetHdc(), this._bounds, MetafileFrameUnit.Pixel);
            graphics.ReleaseHdc();
          }
        }
        using (Graphics graphics = Graphics.FromImage((Image) this._metafile))
        {
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          this.Draw(graphics);
        }
      }
      return (Image) this._metafile;
    }
  }
}
