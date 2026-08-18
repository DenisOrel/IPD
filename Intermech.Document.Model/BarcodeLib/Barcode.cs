// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Barcode
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using BarcodeLib.Symbologies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

#nullable disable
namespace BarcodeLib;

/// <summary>
/// Generates a barcode image of a specified symbology from a string of data.
/// </summary>
public class Barcode : IDisposable
{
  private IBarcode ibarcode = (IBarcode) new Blank();
  private string Raw_Data = "";
  private string Encoded_Value = "";
  private string _Country_Assigning_Manufacturer_Code = "N/A";
  private TYPE Encoded_Type;
  private Image _Encoded_Image;
  private Color _ForeColor = Color.Black;
  private Color _BackColor = Color.White;
  private int _Width = 300;
  private int _Height = 150;
  private string _XML = "";
  private ImageFormat _ImageFormat = ImageFormat.Jpeg;
  private Font _LabelFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
  private LabelPositions _LabelPosition = LabelPositions.BOTTOMCENTER;
  private RotateFlipType _RotateFlipType;

  /// <summary>
  /// Default constructor.  Does not populate the raw data.  MUST be done via the RawData property before encoding.
  /// </summary>
  public Barcode()
  {
  }

  /// <summary>
  /// Constructor. Populates the raw data. No whitespace will be added before or after the barcode.
  /// </summary>
  /// <param name="data">String to be encoded.</param>
  public Barcode(string data) => this.Raw_Data = data;

  public Barcode(string data, TYPE iType)
  {
    this.Raw_Data = data;
    this.Encoded_Type = iType;
  }

  /// <summary>Gets or sets the raw data to encode.</summary>
  public string RawData
  {
    get => this.Raw_Data;
    set => this.Raw_Data = value;
  }

  /// <summary>Gets the encoded value.</summary>
  public string EncodedValue => this.Encoded_Value;

  /// <summary>Gets the Country that assigned the Manufacturer Code.</summary>
  public string Country_Assigning_Manufacturer_Code => this._Country_Assigning_Manufacturer_Code;

  /// <summary>
  /// Gets or sets the Encoded Type (ex. UPC-A, EAN-13 ... etc)
  /// </summary>
  public TYPE EncodedType
  {
    set => this.Encoded_Type = value;
    get => this.Encoded_Type;
  }

  /// <summary>Gets the Image of the generated barcode.</summary>
  public Image EncodedImage => this._Encoded_Image;

  /// <summary>
  /// Gets or sets the color of the bars. (Default is black)
  /// </summary>
  public Color ForeColor
  {
    get => this._ForeColor;
    set => this._ForeColor = value;
  }

  /// <summary>Gets or sets the background color. (Default is white)</summary>
  public Color BackColor
  {
    get => this._BackColor;
    set => this._BackColor = value;
  }

  /// <summary>
  /// Gets or sets the label font. (Default is Microsoft Sans Serif, 10pt, Bold)
  /// </summary>
  public Font LabelFont
  {
    get => this._LabelFont;
    set => this._LabelFont = value;
  }

  /// <summary>
  /// Gets or sets the location of the label in relation to the barcode. (BOTTOMCENTER is default)
  /// </summary>
  public LabelPositions LabelPosition
  {
    get => this._LabelPosition;
    set => this._LabelPosition = value;
  }

  /// <summary>
  /// Gets or sets the degree in which to rotate/flip the image.(No action is default)
  /// </summary>
  public RotateFlipType RotateFlipType
  {
    get => this._RotateFlipType;
    set => this._RotateFlipType = value;
  }

  /// <summary>
  /// Gets or sets the width of the image to be drawn. (Default is 300 pixels)
  /// </summary>
  public int Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  /// <summary>
  /// Gets or sets the height of the image to be drawn. (Default is 150 pixels)
  /// </summary>
  public int Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  /// <summary>
  /// Gets or sets whether a label should be drawn below the image. (Default is false)
  /// </summary>
  public bool IncludeLabel { get; set; }

  /// <summary>
  /// Alternate label to be displayed.  (IncludeLabel must be set to true as well)
  /// </summary>
  public string AlternateLabel { get; set; }

  /// <summary>
  /// Gets or sets the amount of time in milliseconds that it took to encode and draw the barcode.
  /// </summary>
  public double EncodingTime { get; set; }

  /// <summary>
  /// Gets the XML representation of the Barcode data and image.
  /// </summary>
  public string XML => this._XML;

  /// <summary>
  /// Gets or sets the image format to use when encoding and returning images. (Jpeg is default)
  /// </summary>
  public ImageFormat ImageFormat
  {
    get => this._ImageFormat;
    set => this._ImageFormat = value;
  }

  /// <summary>Gets the list of errors encountered.</summary>
  public List<string> Errors => this.ibarcode.Errors;

  /// <summary>
  /// Gets or sets the alignment of the barcode inside the image. (Not for Postnet or ITF-14)
  /// </summary>
  public AlignmentPositions Alignment { get; set; }

  /// <summary>
  /// Gets a byte array representation of the encoded image. (Used for Crystal Reports)
  /// </summary>
  public byte[] Encoded_Image_Bytes
  {
    get
    {
      if (this._Encoded_Image == null)
        return (byte[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        this._Encoded_Image.Save((Stream) memoryStream, this._ImageFormat);
        return memoryStream.ToArray();
      }
    }
  }

  /// <summary>Gets the assembly version information.</summary>
  public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="StringToEncode">Raw data to encode.</param>
  /// <param name="Width">Width of the resulting barcode.(pixels)</param>
  /// <param name="Height">Height of the resulting barcode.(pixels)</param>
  /// <returns>Image representing the barcode.</returns>
  public Image Encode(TYPE iType, string StringToEncode, int Width, int Height)
  {
    this.Width = Width;
    this.Height = Height;
    return this.Encode(iType, StringToEncode);
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="StringToEncode">Raw data to encode.</param>
  /// <param name="DrawColor">Foreground color</param>
  /// <param name="BackColor">Background color</param>
  /// <param name="Width">Width of the resulting barcode.(pixels)</param>
  /// <param name="Height">Height of the resulting barcode.(pixels)</param>
  /// <returns>Image representing the barcode.</returns>
  public Image Encode(
    TYPE iType,
    string StringToEncode,
    Color ForeColor,
    Color BackColor,
    int Width,
    int Height)
  {
    this.Width = Width;
    this.Height = Height;
    return this.Encode(iType, StringToEncode, ForeColor, BackColor);
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="StringToEncode">Raw data to encode.</param>
  /// <param name="DrawColor">Foreground color</param>
  /// <param name="BackColor">Background color</param>
  /// <returns>Image representing the barcode.</returns>
  public Image Encode(TYPE iType, string StringToEncode, Color ForeColor, Color BackColor)
  {
    this.BackColor = BackColor;
    this.ForeColor = ForeColor;
    return this.Encode(iType, StringToEncode);
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="StringToEncode">Raw data to encode.</param>
  /// <returns>Image representing the barcode.</returns>
  public Image Encode(TYPE iType, string StringToEncode)
  {
    this.Raw_Data = StringToEncode;
    return this.Encode(iType);
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  internal Image Encode(TYPE iType)
  {
    this.Encoded_Type = iType;
    return this.Encode();
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.
  /// </summary>
  internal Image Encode()
  {
    this.ibarcode.Errors.Clear();
    DateTime now = DateTime.Now;
    if (this.Raw_Data.Trim() == "")
      throw new Exception("EENCODE-1: Input data not allowed to be blank.");
    if (this.EncodedType == TYPE.UNSPECIFIED)
      throw new Exception("EENCODE-2: Symbology type not allowed to be unspecified.");
    this._Country_Assigning_Manufacturer_Code = "N/A";
    switch (this.Encoded_Type)
    {
      case TYPE.UPCA:
      case TYPE.UCC12:
        this.ibarcode = (IBarcode) new UPCA(this.Raw_Data);
        break;
      case TYPE.UPCE:
        this.ibarcode = (IBarcode) new UPCE(this.Raw_Data);
        break;
      case TYPE.UPC_SUPPLEMENTAL_2DIGIT:
        this.ibarcode = (IBarcode) new UPCSupplement2(this.Raw_Data);
        break;
      case TYPE.UPC_SUPPLEMENTAL_5DIGIT:
        this.ibarcode = (IBarcode) new UPCSupplement5(this.Raw_Data);
        break;
      case TYPE.EAN13:
      case TYPE.UCC13:
        this.ibarcode = (IBarcode) new EAN13(this.Raw_Data);
        break;
      case TYPE.EAN8:
        this.ibarcode = (IBarcode) new EAN8(this.Raw_Data);
        break;
      case TYPE.Interleaved2of5:
        this.ibarcode = (IBarcode) new Interleaved2of5(this.Raw_Data);
        break;
      case TYPE.Standard2of5:
      case TYPE.Industrial2of5:
        this.ibarcode = (IBarcode) new Standard2of5(this.Raw_Data);
        break;
      case TYPE.CODE39:
      case TYPE.LOGMARS:
        this.ibarcode = (IBarcode) new Code39(this.Raw_Data);
        break;
      case TYPE.CODE39Extended:
        this.ibarcode = (IBarcode) new Code39(this.Raw_Data, true);
        break;
      case TYPE.CODE39_Mod43:
        this.ibarcode = (IBarcode) new Code39(this.Raw_Data, false, true);
        break;
      case TYPE.Codabar:
        this.ibarcode = (IBarcode) new Codabar(this.Raw_Data);
        break;
      case TYPE.PostNet:
        this.ibarcode = (IBarcode) new Postnet(this.Raw_Data);
        break;
      case TYPE.BOOKLAND:
      case TYPE.ISBN:
        this.ibarcode = (IBarcode) new ISBN(this.Raw_Data);
        break;
      case TYPE.JAN13:
        this.ibarcode = (IBarcode) new JAN13(this.Raw_Data);
        break;
      case TYPE.MSI_Mod10:
      case TYPE.MSI_2Mod10:
      case TYPE.MSI_Mod11:
      case TYPE.MSI_Mod11_Mod10:
      case TYPE.Modified_Plessey:
        this.ibarcode = (IBarcode) new MSI(this.Raw_Data, this.Encoded_Type);
        break;
      case TYPE.CODE11:
      case TYPE.USD8:
        this.ibarcode = (IBarcode) new Code11(this.Raw_Data);
        break;
      case TYPE.CODE128:
        this.ibarcode = (IBarcode) new Code128(this.Raw_Data);
        break;
      case TYPE.CODE128A:
        this.ibarcode = (IBarcode) new Code128(this.Raw_Data, Code128.TYPES.A);
        break;
      case TYPE.CODE128B:
        this.ibarcode = (IBarcode) new Code128(this.Raw_Data, Code128.TYPES.B);
        break;
      case TYPE.CODE128C:
        this.ibarcode = (IBarcode) new Code128(this.Raw_Data, Code128.TYPES.C);
        break;
      case TYPE.ITF14:
        this.ibarcode = (IBarcode) new ITF14(this.Raw_Data);
        break;
      case TYPE.CODE93:
        this.ibarcode = (IBarcode) new Code93(this.Raw_Data);
        break;
      case TYPE.TELEPEN:
        this.ibarcode = (IBarcode) new Telepen(this.Raw_Data);
        break;
      case TYPE.FIM:
        this.ibarcode = (IBarcode) new FIM(this.Raw_Data);
        break;
      case TYPE.PHARMACODE:
        this.ibarcode = (IBarcode) new Pharmacode(this.Raw_Data);
        break;
      default:
        throw new Exception("EENCODE-2: Unsupported encoding type specified.");
    }
    this.Encoded_Value = this.ibarcode.Encoded_Value;
    this.Raw_Data = this.ibarcode.RawData;
    this._Encoded_Image = (Image) this.Generate_Image();
    this.EncodedImage.RotateFlip(this.RotateFlipType);
    this.EncodingTime = (DateTime.Now - now).TotalMilliseconds;
    return this.EncodedImage;
  }

  /// <summary>Gets a bitmap representation of the encoded data.</summary>
  /// <returns>Bitmap of encoded value.</returns>
  private Bitmap Generate_Image()
  {
    if (this.Encoded_Value == "")
      throw new Exception("EGENERATE_IMAGE-1: Must be encoded first.");
    DateTime now = DateTime.Now;
    Bitmap img;
    if (this.Encoded_Type == TYPE.ITF14)
    {
      img = new Bitmap(this.Width, this.Height);
      int num1 = (int) ((double) img.Width / 12.05);
      int int32 = Convert.ToInt32((double) img.Width * 0.05);
      int width = (img.Width - num1 * 2 - int32 * 2) / this.Encoded_Value.Length;
      int num2 = (img.Width - num1 * 2 - int32 * 2) % this.Encoded_Value.Length / 2;
      if (width <= 0 || int32 <= 0)
        throw new Exception("EGENERATE_IMAGE-3: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel or quiet zone determined to be less than 1 pixel)");
      int index = 0;
      using (Graphics graphics = Graphics.FromImage((Image) img))
      {
        graphics.Clear(this.BackColor);
        using (Pen pen = new Pen(this.ForeColor, (float) width))
        {
          pen.Alignment = PenAlignment.Right;
          for (; index < this.Encoded_Value.Length; ++index)
          {
            if (this.Encoded_Value[index] == '1')
              graphics.DrawLine(pen, new Point(index * width + num2 + num1 + int32, 0), new Point(index * width + num2 + num1 + int32, this.Height));
          }
          pen.Width = (float) img.Height / 8f;
          pen.Color = this.ForeColor;
          pen.Alignment = PenAlignment.Center;
          graphics.DrawLine(pen, new Point(0, 0), new Point(img.Width, 0));
          graphics.DrawLine(pen, new Point(0, img.Height), new Point(img.Width, img.Height));
          graphics.DrawLine(pen, new Point(0, 0), new Point(0, img.Height));
          graphics.DrawLine(pen, new Point(img.Width, 0), new Point(img.Width, img.Height));
        }
      }
      if (this.IncludeLabel)
        this.Label_ITF14((Image) img);
    }
    else
    {
      img = new Bitmap(this.Width, this.Height);
      int num3 = this.Width / this.Encoded_Value.Length;
      int num4 = 1;
      if (this.Encoded_Type == TYPE.PostNet)
        num4 = 2;
      int num5;
      switch (this.Alignment)
      {
        case AlignmentPositions.CENTER:
          num5 = this.Width % this.Encoded_Value.Length / 2;
          break;
        case AlignmentPositions.LEFT:
          num5 = 0;
          break;
        case AlignmentPositions.RIGHT:
          num5 = this.Width % this.Encoded_Value.Length;
          break;
        default:
          num5 = this.Width % this.Encoded_Value.Length / 2;
          break;
      }
      if (num3 <= 0)
        throw new Exception("EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");
      int index = 0;
      int num6 = (int) ((double) num3 * 0.5);
      using (Graphics graphics = Graphics.FromImage((Image) img))
      {
        graphics.Clear(this.BackColor);
        using (new Pen(this.BackColor, (float) (num3 / num4)))
        {
          using (Pen pen = new Pen(this.ForeColor, (float) (num3 / num4)))
          {
            for (; index < this.Encoded_Value.Length; ++index)
            {
              if (this.Encoded_Type == TYPE.PostNet)
              {
                if (this.Encoded_Value[index] == '0')
                  graphics.DrawLine(pen, new Point(index * num3 + num5 + num6, this.Height), new Point(index * num3 + num5 + num6, this.Height / 2));
                else
                  graphics.DrawLine(pen, new Point(index * num3 + num5 + num6, this.Height), new Point(index * num3 + num5 + num6, 0));
              }
              else if (this.Encoded_Value[index] == '1')
                graphics.DrawLine(pen, new Point(index * num3 + num5 + num6, 0), new Point(index * num3 + num5 + num6, this.Height));
            }
          }
        }
      }
      if (this.IncludeLabel)
        this.Label_Generic((Image) img);
    }
    this._Encoded_Image = (Image) img;
    this.EncodingTime += (DateTime.Now - now).TotalMilliseconds;
    return img;
  }

  /// <summary>Gets the bytes that represent the image.</summary>
  /// <param name="savetype">File type to put the data in before returning the bytes.</param>
  /// <returns>Bytes representing the encoded image.</returns>
  public byte[] GetImageData(SaveTypes savetype)
  {
    byte[] imageData = (byte[]) null;
    try
    {
      if (this._Encoded_Image != null)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          this.SaveImage((Stream) memoryStream, savetype);
          imageData = memoryStream.ToArray();
          memoryStream.Flush();
          memoryStream.Close();
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception("EGETIMAGEDATA-1: Could not retrieve image data. " + ex.Message);
    }
    return imageData;
  }

  /// <summary>Saves an encoded image to a specified file and type.</summary>
  /// <param name="Filename">Filename to save to.</param>
  /// <param name="FileType">Format to use.</param>
  public void SaveImage(string Filename, SaveTypes FileType)
  {
    try
    {
      if (this._Encoded_Image == null)
        return;
      ImageFormat format;
      switch (FileType)
      {
        case SaveTypes.JPG:
          format = ImageFormat.Jpeg;
          break;
        case SaveTypes.BMP:
          format = ImageFormat.Bmp;
          break;
        case SaveTypes.PNG:
          format = ImageFormat.Png;
          break;
        case SaveTypes.GIF:
          format = ImageFormat.Gif;
          break;
        case SaveTypes.TIFF:
          format = ImageFormat.Tiff;
          break;
        default:
          format = this.ImageFormat;
          break;
      }
      this._Encoded_Image.Save(Filename, format);
    }
    catch (Exception ex)
    {
      throw new Exception("ESAVEIMAGE-1: Could not save image.\n\n=======================\n\n" + ex.Message);
    }
  }

  /// <summary>Saves an encoded image to a specified stream.</summary>
  /// <param name="stream">Stream to write image to.</param>
  /// <param name="FileType">Format to use.</param>
  public void SaveImage(Stream stream, SaveTypes FileType)
  {
    try
    {
      if (this._Encoded_Image == null)
        return;
      ImageFormat format;
      switch (FileType)
      {
        case SaveTypes.JPG:
          format = ImageFormat.Jpeg;
          break;
        case SaveTypes.BMP:
          format = ImageFormat.Bmp;
          break;
        case SaveTypes.PNG:
          format = ImageFormat.Png;
          break;
        case SaveTypes.GIF:
          format = ImageFormat.Gif;
          break;
        case SaveTypes.TIFF:
          format = ImageFormat.Tiff;
          break;
        default:
          format = this.ImageFormat;
          break;
      }
      this._Encoded_Image.Save(stream, format);
    }
    catch (Exception ex)
    {
      throw new Exception("ESAVEIMAGE-2: Could not save image.\n\n=======================\n\n" + ex.Message);
    }
  }

  /// <summary>
  /// Returns the size of the EncodedImage in real world coordinates (millimeters or inches).
  /// </summary>
  /// <param name="Metric">Millimeters if true, otherwise Inches.</param>
  /// <returns></returns>
  public Barcode.ImageSize GetSizeOfImage(bool Metric)
  {
    double width = 0.0;
    double height = 0.0;
    if (this.EncodedImage != null && this.EncodedImage.Width > 0 && this.EncodedImage.Height > 0)
    {
      double num = 25.4;
      using (Graphics graphics = Graphics.FromImage(this.EncodedImage))
      {
        width = (double) this.EncodedImage.Width / (double) graphics.DpiX;
        height = (double) this.EncodedImage.Height / (double) graphics.DpiY;
        if (Metric)
        {
          width *= num;
          height *= num;
        }
      }
    }
    return new Barcode.ImageSize(width, height, Metric);
  }

  private Image Label_ITF14(Image img)
  {
    try
    {
      Font labelFont = this.LabelFont;
      using (Graphics graphics = Graphics.FromImage(img))
      {
        graphics.DrawImage(img, 0.0f, 0.0f);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), new Rectangle(0, img.Height - (labelFont.Height - 2), img.Width, labelFont.Height));
        graphics.DrawString(this.AlternateLabel == null ? this.RawData : this.AlternateLabel, labelFont, (Brush) new SolidBrush(this.ForeColor), (float) img.Width / 2f, (float) (img.Height - labelFont.Height + 1), new StringFormat()
        {
          Alignment = StringAlignment.Center
        });
        graphics.DrawLine(new Pen(this.ForeColor, (float) img.Height / 16f)
        {
          Alignment = PenAlignment.Inset
        }, new Point(0, img.Height - labelFont.Height - 2), new Point(img.Width, img.Height - labelFont.Height - 2));
        graphics.Save();
      }
      return img;
    }
    catch (Exception ex)
    {
      throw new Exception("ELABEL_ITF14-1: " + ex.Message);
    }
  }

  private Image Label_Generic(Image img)
  {
    try
    {
      Font labelFont = this.LabelFont;
      using (Graphics graphics = Graphics.FromImage(img))
      {
        graphics.DrawImage(img, 0.0f, 0.0f);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Near;
        format.LineAlignment = StringAlignment.Near;
        int y = 0;
        switch (this.LabelPosition)
        {
          case LabelPositions.TOPLEFT:
            y = 0;
            format.Alignment = StringAlignment.Near;
            break;
          case LabelPositions.TOPCENTER:
            y = 0;
            format.Alignment = StringAlignment.Center;
            break;
          case LabelPositions.TOPRIGHT:
            y = 0;
            format.Alignment = StringAlignment.Far;
            break;
          case LabelPositions.BOTTOMLEFT:
            y = img.Height - labelFont.Height;
            format.Alignment = StringAlignment.Near;
            break;
          case LabelPositions.BOTTOMCENTER:
            y = img.Height - labelFont.Height;
            format.Alignment = StringAlignment.Center;
            break;
          case LabelPositions.BOTTOMRIGHT:
            y = img.Height - labelFont.Height;
            format.Alignment = StringAlignment.Far;
            break;
        }
        graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), new RectangleF(0.0f, (float) y, (float) img.Width, (float) labelFont.Height));
        graphics.DrawString(this.AlternateLabel == null ? this.RawData : this.AlternateLabel, labelFont, (Brush) new SolidBrush(this.ForeColor), new RectangleF(0.0f, (float) y, (float) img.Width, (float) labelFont.Height), format);
        graphics.Save();
      }
      return img;
    }
    catch (Exception ex)
    {
      throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
    }
  }

  /// <summary>Draws Label for UPC-A barcodes (NOT COMPLETE)</summary>
  /// <param name="img"></param>
  /// <returns></returns>
  private Image Label_UPCA(Image img)
  {
    try
    {
      int num1 = this.Width / this.Encoded_Value.Length;
      int num2;
      switch (this.Alignment)
      {
        case AlignmentPositions.CENTER:
          num2 = this.Width % this.Encoded_Value.Length / 2;
          break;
        case AlignmentPositions.LEFT:
          num2 = 0;
          break;
        case AlignmentPositions.RIGHT:
          num2 = this.Width % this.Encoded_Value.Length;
          break;
        default:
          num2 = this.Width % this.Encoded_Value.Length / 2;
          break;
      }
      Font font = new Font("OCR A Extended", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      using (Graphics graphics = Graphics.FromImage(img))
      {
        graphics.DrawImage(img, 0.0f, 0.0f);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        RectangleF rectangleF = new RectangleF((float) (num1 * 3 + num2), (float) (this.Height - (int) ((double) this.Height * 0.1)), (float) (num1 * 43), (float) (int) ((double) this.Height * 0.1));
        graphics.FillRectangle((Brush) new SolidBrush(Color.Yellow), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
        graphics.DrawString(this.RawData.Substring(1, 5), font, (Brush) new SolidBrush(this.ForeColor), rectangleF.X, rectangleF.Y);
        graphics.Save();
      }
      return img;
    }
    catch (Exception ex)
    {
      throw new Exception("ELABEL_UPCA-1: " + ex.Message);
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(TYPE iType, string Data)
  {
    using (Barcode barcode = new Barcode())
      return barcode.Encode(iType, Data);
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <param name="XML">XML representation of the data and the image of the barcode.</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(TYPE iType, string Data, ref string XML)
  {
    using (Barcode barcode = new Barcode())
    {
      Image image = barcode.Encode(iType, Data);
      XML = barcode.XML;
      return image;
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <param name="IncludeLabel">Include the label at the bottom of the image with data encoded.</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel)
  {
    using (Barcode barcode = new Barcode())
    {
      barcode.IncludeLabel = IncludeLabel;
      return barcode.Encode(iType, Data);
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="data">Raw data to encode.</param>
  /// <param name="IncludeLabel">Include the label at the bottom of the image with data encoded.</param>
  /// <param name="Width">Width of the resulting barcode.(pixels)</param>
  /// <param name="Height">Height of the resulting barcode.(pixels)</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, int Width, int Height)
  {
    using (Barcode barcode = new Barcode())
    {
      barcode.IncludeLabel = IncludeLabel;
      return barcode.Encode(iType, Data, Width, Height);
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <param name="IncludeLabel">Include the label at the bottom of the image with data encoded.</param>
  /// <param name="DrawColor">Foreground color</param>
  /// <param name="BackColor">Background color</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(
    TYPE iType,
    string Data,
    bool IncludeLabel,
    Color DrawColor,
    Color BackColor)
  {
    using (Barcode barcode = new Barcode())
    {
      barcode.IncludeLabel = IncludeLabel;
      return barcode.Encode(iType, Data, DrawColor, BackColor);
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <param name="IncludeLabel">Include the label at the bottom of the image with data encoded.</param>
  /// <param name="DrawColor">Foreground color</param>
  /// <param name="BackColor">Background color</param>
  /// <param name="Width">Width of the resulting barcode.(pixels)</param>
  /// <param name="Height">Height of the resulting barcode.(pixels)</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(
    TYPE iType,
    string Data,
    bool IncludeLabel,
    Color DrawColor,
    Color BackColor,
    int Width,
    int Height)
  {
    using (Barcode barcode = new Barcode())
    {
      barcode.IncludeLabel = IncludeLabel;
      return barcode.Encode(iType, Data, DrawColor, BackColor, Width, Height);
    }
  }

  /// <summary>
  /// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
  /// </summary>
  /// <param name="iType">Type of encoding to use.</param>
  /// <param name="Data">Raw data to encode.</param>
  /// <param name="IncludeLabel">Include the label at the bottom of the image with data encoded.</param>
  /// <param name="DrawColor">Foreground color</param>
  /// <param name="BackColor">Background color</param>
  /// <param name="Width">Width of the resulting barcode.(pixels)</param>
  /// <param name="Height">Height of the resulting barcode.(pixels)</param>
  /// <param name="XML">XML representation of the data and the image of the barcode.</param>
  /// <returns>Image representing the barcode.</returns>
  public static Image DoEncode(
    TYPE iType,
    string Data,
    bool IncludeLabel,
    Color DrawColor,
    Color BackColor,
    int Width,
    int Height,
    ref string XML)
  {
    using (Barcode barcode = new Barcode())
    {
      barcode.IncludeLabel = IncludeLabel;
      Image image = barcode.Encode(iType, Data, DrawColor, BackColor, Width, Height);
      XML = barcode.XML;
      return image;
    }
  }

  public void Dispose()
  {
  }

  /// <summary>
  /// Represents the size of an image in real world coordinates (millimeters or inches).
  /// </summary>
  public class ImageSize
  {
    public ImageSize(double width, double height, bool metric)
    {
      this.Width = width;
      this.Height = height;
      this.Metric = metric;
    }

    public double Width { get; set; }

    public double Height { get; set; }

    public bool Metric { get; set; }
  }
}
