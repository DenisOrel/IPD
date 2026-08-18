// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DAnnotation : PdfFileAnnotation
{
  private Pdf3DActivation m_activation;
  private PdfTemplate m_apperance;
  private Pdf3DBase m_u3d;

  public Pdf3DAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
  }

  public Pdf3DAnnotation(RectangleF rectangle, string fileName)
    : base(rectangle)
  {
    this.m_u3d = fileName != null ? new Pdf3DBase(fileName) : throw new ArgumentNullException(nameof (fileName));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("3D"));
  }

  protected override void Save()
  {
    base.Save();
    this.Dictionary.SetProperty("3DD", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_u3d));
    if (this.m_activation != null)
      this.Dictionary["3DA"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_activation);
    if (this.m_apperance == null)
      return;
    this.Dictionary["AP /N"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_apperance);
  }

  public Pdf3DActivation Activation
  {
    get => this.m_activation;
    set => this.m_activation = value;
  }

  public int DefaultView
  {
    get => this.m_u3d.Stream.DefaultView;
    set => this.m_u3d.Stream.DefaultView = value;
  }

  public override string FileName
  {
    get => this.m_u3d.FileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArgumentException("FileName can't be empty");
        default:
          if (!(this.m_u3d.FileName != value))
            break;
          this.m_u3d.FileName = value;
          break;
      }
    }
  }

  public string OnInstantiate
  {
    get => this.m_u3d.Stream.OnInstantiate;
    set => this.m_u3d.Stream.OnInstantiate = value;
  }

  public Pdf3DViewCollection Views => this.m_u3d.Stream.Views;
}
