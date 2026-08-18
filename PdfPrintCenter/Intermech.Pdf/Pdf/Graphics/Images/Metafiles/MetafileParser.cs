// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.MetafileParser
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal abstract class MetafileParser
{
  protected static readonly int FloatSize = Marshal.SizeOf(typeof (float));
  protected static readonly int IntSize = Marshal.SizeOf(typeof (int));
  private object m_context;
  private System.Drawing.Graphics.EnumerateMetafileProc m_enumerateHandler;
  private object m_imageContext;
  private Metafile m_metaFile;
  private float m_pageScale;
  private GraphicsUnit m_pageUnit;
  private PdfEmfRenderer m_renderer;
  protected const byte PointNumber = 2;
  protected const byte RectNumber = 4;
  protected static readonly int ShortSize = Marshal.SizeOf(typeof (short));

  public MetafileParser()
  {
  }

  public MetafileParser(PdfEmfRenderer renderer)
  {
    this.m_renderer = renderer != null ? renderer : throw new ArgumentNullException(nameof (renderer));
  }

  protected internal static void CheckResult(bool result)
  {
    if (result)
      return;
    int lastError = (int) KernelApi.GetLastError();
  }

  protected abstract System.Drawing.Graphics.EnumerateMetafileProc CreateParsingHandler();

  public virtual void Dispose()
  {
    this.m_enumerateHandler = (System.Drawing.Graphics.EnumerateMetafileProc) null;
    this.m_renderer = (PdfEmfRenderer) null;
  }

  protected float ReadNumber(byte[] data, int index, int step)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    float num = 0.0f;
    if (step == MetafileParser.ShortSize)
      return (float) BitConverter.ToInt16(data, index);
    if (step == MetafileParser.FloatSize)
      num = BitConverter.ToSingle(data, index);
    return num;
  }

  public object Context
  {
    get => this.m_context;
    set
    {
      if (this.m_context == value)
        return;
      this.m_context = value;
    }
  }

  public object ImageContext
  {
    get => this.m_imageContext;
    set
    {
      if (this.m_imageContext == value)
        return;
      this.m_imageContext = value;
    }
  }

  public Metafile Metafile
  {
    get => this.m_metaFile;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Metafile));
      if (this.m_metaFile == value)
        return;
      this.m_metaFile = value;
    }
  }

  public float PageScale
  {
    get => this.m_pageScale;
    set => this.m_pageScale = value;
  }

  public GraphicsUnit PageUnit
  {
    get => this.m_pageUnit;
    set => this.m_pageUnit = value;
  }

  public System.Drawing.Graphics.EnumerateMetafileProc ParsingHandler
  {
    get
    {
      if (this.m_enumerateHandler == null)
        this.m_enumerateHandler = this.CreateParsingHandler();
      return this.m_enumerateHandler;
    }
  }

  public PdfEmfRenderer Renderer
  {
    get => this.m_renderer;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Renderer));
      if (this.m_renderer == value)
        return;
      this.m_renderer = value;
    }
  }

  public abstract MetafileType Type { get; }
}
