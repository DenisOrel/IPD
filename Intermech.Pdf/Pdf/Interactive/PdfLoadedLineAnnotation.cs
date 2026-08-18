// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedLineAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedLineAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfColor m_backcolor;
      private PdfCrossTable m_crossTable;
      private LineBorder m_lineborder;

      internal PdfLoadedLineAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle,
        string text)
        : base(dictionary, crossTable)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
        this.Text = text;
      }

      private PdfColor GetBackColor()
      {
        PdfColorSpace colorSpace = PdfColorSpace.RGB;
        PdfColor empty = PdfColor.Empty;
        PdfArray pdfArray = !this.Dictionary.ContainsKey("C") ? empty.ToArray(colorSpace) : this.Dictionary["C"] as PdfArray;
        return new PdfColor((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue, (pdfArray[2] as PdfNumber).FloatValue);
      }

      private PdfBorderStyle GetBorderStyle(string bstyle)
      {
        PdfBorderStyle borderStyle = PdfBorderStyle.Solid;
        string str = bstyle;
        switch (str)
        {
          case null:
            return borderStyle;
          case "S":
            return PdfBorderStyle.Solid;
          default:
            if (!(str != "D"))
              return PdfBorderStyle.Dashed;
            switch (str)
            {
              case "B":
                return PdfBorderStyle.Beveled;
              case "I":
                return PdfBorderStyle.Inset;
              default:
                return str != "U" ? borderStyle : PdfBorderStyle.Underline;
            }
        }
      }

      private PdfLineCaptionType GetCaptionType()
      {
        PdfLineCaptionType captionType = PdfLineCaptionType.Inline;
        if (this.Dictionary.ContainsKey("CP"))
          captionType = this.GetCaptionType((this.Dictionary["CP"] as PdfName).Value.ToString());
        return captionType;
      }

      private PdfLineCaptionType GetCaptionType(string cType)
      {
        return cType == "Inline" ? PdfLineCaptionType.Inline : PdfLineCaptionType.Top;
      }

      private int GetLeaderExt()
      {
        int leaderExt = 0;
        if (this.Dictionary.ContainsKey("LLE"))
          leaderExt = (this.Dictionary["LLE"] as PdfNumber).IntValue;
        return leaderExt;
      }

      private int GetLeaderLine()
      {
        int leaderLine = 0;
        if (this.Dictionary.ContainsKey("LL"))
          leaderLine = (this.Dictionary["LL"] as PdfNumber).IntValue;
        return leaderLine;
      }

      private LineBorder GetLineBorder()
      {
        LineBorder lineBorder = new LineBorder();
        if (this.Dictionary.ContainsKey("BS"))
        {
          PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["BS"]) as PdfDictionary;
          if (pdfDictionary.ContainsKey("W"))
            lineBorder.BorderWidth = (pdfDictionary["W"] as PdfNumber).IntValue;
          if (pdfDictionary.ContainsKey("S"))
          {
            PdfName pdfName = pdfDictionary["S"] as PdfName;
            lineBorder.BorderStyle = this.GetBorderStyle(pdfName.Value.ToString());
          }
          if (pdfDictionary.ContainsKey("D"))
          {
            PdfArray pdfArray = pdfDictionary["D"] as PdfArray;
            int intValue = (pdfArray[0] as PdfNumber).IntValue;
            pdfArray.Clear();
            pdfArray.Insert(0, (IPdfPrimitive) new PdfNumber(intValue));
            pdfArray.Insert(1, (IPdfPrimitive) new PdfNumber(intValue));
            lineBorder.DashArray = intValue;
          }
        }
        return lineBorder;
      }

      private bool GetLineCaption()
      {
        bool lineCaption = false;
        if (this.Dictionary.ContainsKey("Cap"))
          lineCaption = (this.Dictionary["Cap"] as PdfBoolean).Value;
        return lineCaption;
      }

      private PdfLineIntent GetLineIntent()
      {
        PdfLineIntent lineIntent = PdfLineIntent.LineArrow;
        if (this.Dictionary.ContainsKey("IT"))
          lineIntent = this.GetLineIntentText((this.m_crossTable.GetObject(this.Dictionary["IT"]) as PdfName).Value.ToString());
        return lineIntent;
      }

      private PdfLineIntent GetLineIntentText(string lintent)
      {
        PdfLineIntent lineIntentText = PdfLineIntent.LineArrow;
        string str = lintent;
        switch (str)
        {
          case null:
            return lineIntentText;
          case "LineArrow":
            return PdfLineIntent.LineArrow;
          default:
            return str != "LineDimension" ? lineIntentText : PdfLineIntent.LineDimension;
        }
      }

      private PdfArray GetLineStyle()
      {
        PdfArray lineStyle = (PdfArray) null;
        if (this.Dictionary.ContainsKey("LE"))
          lineStyle = this.m_crossTable.GetObject(this.Dictionary["LE"]) as PdfArray;
        return lineStyle;
      }

      private PdfLineEndingStyle GetLineStyle(int Ch)
      {
        PdfLineEndingStyle lineStyle1 = PdfLineEndingStyle.Square;
        PdfArray lineStyle2 = this.GetLineStyle();
        if (lineStyle2 != null)
          lineStyle1 = this.GetLineStyle((lineStyle2[Ch] as PdfName).Value);
        return lineStyle1;
      }

      private PdfLineEndingStyle GetLineStyle(string style)
      {
        PdfLineEndingStyle lineStyle = PdfLineEndingStyle.None;
        switch (style)
        {
          case "Butt":
            return PdfLineEndingStyle.Butt;
          case "Circle":
            return PdfLineEndingStyle.Circle;
          case "ClosedArrow":
            return PdfLineEndingStyle.ClosedArrow;
          case "Diamond":
            return PdfLineEndingStyle.Diamond;
          case "None":
            return PdfLineEndingStyle.None;
          case "OpenArrow":
            return PdfLineEndingStyle.OpenArrow;
          case "RClosedArrow":
            return PdfLineEndingStyle.RClosedArrow;
          case "ROpenArrow":
            return PdfLineEndingStyle.ROpenArrow;
          case "Slash":
            return PdfLineEndingStyle.Slash;
          case "Square":
            return PdfLineEndingStyle.Square;
          default:
            return lineStyle;
        }
      }

      public PdfColor BackColor
      {
        get => this.GetBackColor();
        set
        {
          PdfArray primitive = new PdfArray();
          this.m_backcolor = value;
          primitive.Insert(0, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.R / (float) byte.MaxValue));
          primitive.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.G / (float) byte.MaxValue));
          primitive.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.B / (float) byte.MaxValue));
          this.Dictionary.SetProperty("C", (IPdfPrimitive) primitive);
        }
      }

      public PdfLineEndingStyle BeginLineStyle
      {
        get => this.GetLineStyle(0);
        set
        {
          PdfArray lineStyle = this.GetLineStyle();
          if (lineStyle == null)
            lineStyle.Insert(1, (IPdfPrimitive) new PdfName((Enum) PdfLineEndingStyle.Square));
          else
            lineStyle.RemoveAt(0);
          lineStyle.Insert(0, (IPdfPrimitive) new PdfName((Enum) this.GetLineStyle(value.ToString())));
          this.Dictionary.SetProperty("LE", (IPdfPrimitive) lineStyle);
        }
      }

      public PdfLineCaptionType CaptionType
      {
        get => this.GetCaptionType();
        set
        {
          this.Dictionary.SetProperty("CP", (IPdfPrimitive) new PdfName((Enum) this.GetCaptionType(value.ToString())));
        }
      }

      public PdfLineEndingStyle EndLineStyle
      {
        get => this.GetLineStyle(1);
        set
        {
          PdfArray lineStyle = this.GetLineStyle();
          if (lineStyle == null)
            lineStyle.Insert(0, (IPdfPrimitive) new PdfName((Enum) PdfLineEndingStyle.Square));
          else
            lineStyle.RemoveAt(1);
          lineStyle.Insert(1, (IPdfPrimitive) new PdfName((Enum) this.GetLineStyle(value.ToString())));
          this.Dictionary.SetProperty("LE", (IPdfPrimitive) lineStyle);
        }
      }

      public PdfColor InnerLineColor
      {
        get => this.GetBackColor();
        set
        {
          PdfArray primitive = new PdfArray();
          this.m_backcolor = value;
          primitive.Insert(0, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.R / (float) byte.MaxValue));
          primitive.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.G / (float) byte.MaxValue));
          primitive.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_backcolor.B / (float) byte.MaxValue));
          this.Dictionary.SetProperty("IC", (IPdfPrimitive) primitive);
        }
      }

      public int LeaderExt
      {
        get => this.GetLeaderExt();
        set => this.Dictionary.SetNumber("LLE", value);
      }

      public int LeaderLine
      {
        get => this.GetLeaderLine();
        set => this.Dictionary.SetNumber("LL", value);
      }

      public LineBorder LineBorder
      {
        get => this.GetLineBorder();
        set
        {
          this.m_lineborder = value;
          this.Dictionary.SetProperty("BS", (IPdfWrapper) this.m_lineborder);
        }
      }

      public bool LineCaption
      {
        get => this.GetLineCaption();
        set => this.Dictionary.SetBoolean("Cap", value);
      }

      public PdfLineIntent LineIntent
      {
        get => this.GetLineIntent();
        set => this.Dictionary.SetName("IT", value.ToString());
      }
    }
}
