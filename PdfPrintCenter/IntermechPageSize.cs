
// Type: Intermech.PdfPrintCenter.IntermechPageSize




using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Intermech.PdfPrintCenter
{
    internal class IntermechPageSize : Rectangle
    {
      private readonly List<int> _pageRange;
      private string _range;

      public IntermechPageSize(Rectangle rect, int pageNumber)
        : base(rect)
      {
        this._range = "";
        this._pageRange = new List<int>();
        if (pageNumber > 0)
          this._pageRange.Add(pageNumber);
        this.MmWidth = (int) Math.Round((double) this.Width / (360.0 / (double) sbyte.MaxValue));
        this.MmHeight = (int) Math.Round((double) this.Height / (360.0 / (double) sbyte.MaxValue));
        this.Name = $"{this.MmWidth} x {this.MmHeight}";
        KnownPaperFormat knownPaperFormat = KnownPaperFormats.Formats.Find((Predicate<KnownPaperFormat>) (format => this.SameSize((double) format.Width, (double) format.Height)));
        if (knownPaperFormat == null)
          return;
        this.Name = knownPaperFormat.BaseName;
      }

      public void AddPage(int pageNumber)
      {
        this._range = "";
        this._pageRange.Add(pageNumber);
      }

      public string Range
      {
        get
        {
          if (this._range != "")
            return this._range;
          int num1 = -1;
          bool flag = false;
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num2 in this._pageRange)
          {
            if (num1 + 1 != num2)
            {
              if (flag)
              {
                stringBuilder.Append("-");
                stringBuilder.Append(num1);
              }
              if (stringBuilder.Length != 0)
                stringBuilder.Append(",");
              stringBuilder.Append(num2);
              flag = false;
            }
            else
              flag = true;
            num1 = num2;
          }
          if (flag)
          {
            stringBuilder.Append("-");
            stringBuilder.Append(num1);
          }
          this._range = stringBuilder.ToString();
          return this._range;
        }
        set
        {
          this._pageRange.Clear();
          string str = value;
          char[] chArray = new char[2]{ ';', ',' };
          foreach (string s in str.Split(chArray))
          {
            try
            {
              this._pageRange.Add(int.Parse(s));
            }
            catch
            {
              string[] strArray = s.Split('-');
              int start = int.Parse(strArray[0]);
              int num = int.Parse(strArray[1]);
              this._pageRange.AddRange(Enumerable.Range(start, num - start));
            }
          }
          this._range = value;
        }
      }

      public bool SameSize(double w, double h)
      {
        return (double) PdfUtils.ToleranceInMM(w) >= Math.Abs((double) this.MmWidth - w) && (double) PdfUtils.ToleranceInMM(h) >= Math.Abs((double) this.MmHeight - h);
      }

      public string Name { get; set; }

      public int MmWidth { get; set; }

      public int MmHeight { get; set; }
    }
}
