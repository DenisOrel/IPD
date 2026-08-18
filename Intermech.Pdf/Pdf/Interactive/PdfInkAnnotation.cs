// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfInkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfInkAnnotation : PdfAnnotation
    {
      private PdfDictionary m_borderDic;
      private PdfLineBorderStyle m_borderStyle;
      private int m_borderWidth;
      private int[] m_dashArray;
      private List<float> m_inkList;

      public PdfInkAnnotation(RectangleF rectangle, List<float> linePoints)
        : base(rectangle)
      {
        this.m_borderWidth = 1;
        this.m_borderDic = new PdfDictionary();
        this.InkList = linePoints;
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Ink"));
      }

      protected override void Save()
      {
        base.Save();
        this.m_borderDic.SetProperty("Type", (IPdfPrimitive) new PdfName("Border"));
        this.Dictionary.SetProperty("BS", (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_borderDic));
      }

      public PdfLineBorderStyle BorderStyle
      {
        get => this.m_borderStyle;
        set
        {
          this.m_borderStyle = value;
          if (this.m_borderStyle == PdfLineBorderStyle.Solid)
            this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("S"));
          else if (this.m_borderStyle == PdfLineBorderStyle.Inset)
            this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("I"));
          else if (this.m_borderStyle == PdfLineBorderStyle.Dashed)
            this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("D"));
          else if (this.m_borderStyle == PdfLineBorderStyle.Beveled)
          {
            this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("B"));
          }
          else
          {
            if (this.m_borderStyle != PdfLineBorderStyle.Underline)
              return;
            this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("U"));
          }
        }
      }

      public int BorderWidth
      {
        get => this.m_borderWidth;
        set
        {
          this.m_borderWidth = value;
          this.m_borderDic.SetProperty("W", (IPdfPrimitive) new PdfNumber(this.m_borderWidth));
        }
      }

      public int[] DashArray
      {
        get => this.m_dashArray;
        set
        {
          this.m_dashArray = value;
          this.m_borderDic.SetProperty("D", (IPdfPrimitive) new PdfArray(this.m_dashArray));
        }
      }

      public List<float> InkList
      {
        get => this.m_inkList;
        set
        {
          this.m_inkList = value;
          this.Dictionary.SetProperty(nameof (InkList), (IPdfPrimitive) new PdfArray(new PdfArray()
          {
            (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) new PdfArray(this.m_inkList.ToArray()))
          }));
        }
      }
    }
}
