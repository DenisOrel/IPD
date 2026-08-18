// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageLabel
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf
{
    public class PdfPageLabel : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();

      public PdfPageLabel()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("PageLabel"));
        this.m_dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("D"));
      }

      private static PdfNumberStyle FromStringToStyle(string name)
      {
        PdfNumberStyle style = PdfNumberStyle.None;
        if (name == null || !(name != string.Empty))
          return style;
        switch (name)
        {
          case "D":
            return PdfNumberStyle.Numeric;
          case "A":
            return PdfNumberStyle.UpperLatin;
          case "a":
            return PdfNumberStyle.LowerLatin;
          case "R":
            return PdfNumberStyle.UpperRoman;
          case "r":
            return PdfNumberStyle.LowerRoman;
          default:
            throw new ArgumentException("Unsupported style name.", nameof (name));
        }
      }

      private static string FromStyleToString(PdfNumberStyle style)
      {
        string str = (string) null;
        switch (style)
        {
          case PdfNumberStyle.None:
            return str;
          case PdfNumberStyle.Numeric:
            return "D";
          case PdfNumberStyle.LowerLatin:
            return "a";
          case PdfNumberStyle.LowerRoman:
            return "r";
          case PdfNumberStyle.UpperLatin:
            return "A";
          case PdfNumberStyle.UpperRoman:
            return "R";
          default:
            throw new ArgumentException("Unsupported style.", nameof (style));
        }
      }

      public PdfNumberStyle NumberStyle
      {
        get
        {
          PdfNumberStyle numberStyle = PdfNumberStyle.None;
          PdfName pdfName = this.m_dictionary["S"] as PdfName;
          if (pdfName != (PdfName) null)
            numberStyle = PdfPageLabel.FromStringToStyle(pdfName.Value);
          return numberStyle;
        }
        set
        {
          string name = PdfPageLabel.FromStyleToString(value);
          if (name == null || name != null && name.Length == 0)
            this.m_dictionary.Remove("S");
          else
            this.m_dictionary.SetName("S", name);
        }
      }

      public string Prefix
      {
        get
        {
          string prefix = (string) null;
          if (this.m_dictionary["P"] is PdfString pdfString)
            prefix = pdfString.Value;
          return prefix;
        }
        set
        {
          if (value == null || value == string.Empty)
            this.m_dictionary.Remove("P");
          else
            this.m_dictionary.SetString("P", value);
        }
      }

      public int StartNumber
      {
        get
        {
          int startNumber = -1;
          if (this.m_dictionary["St"] is PdfNumber pdfNumber)
            startNumber = pdfNumber.IntValue;
          return startNumber;
        }
        set
        {
          if (value < 0)
            this.m_dictionary.Remove("St");
          else
            this.m_dictionary.SetNumber("St", value);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
