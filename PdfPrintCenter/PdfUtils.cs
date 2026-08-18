
// Type: Intermech.PdfPrintCenter.PdfUtils




using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter
{
    internal class PdfUtils
    {
      public const double PunktInMM = 2.834645669291;
      public const double MMInInch = 25.4;

      public static bool IsSameSize(float w1, float h1, float w2, float h2)
      {
        return (double) Math.Abs(w1 - w2) <= (double) PdfUtils.ToleranceInPkt((double) w1) && (double) Math.Abs(h1 - h2) <= (double) PdfUtils.ToleranceInPkt((double) h1);
      }

      public static bool IsSameSize(float w1, float h1, float w2, float h2, ref bool rotate)
      {
        if (PdfUtils.IsSameSize(w1, h1, w2, h2))
        {
          rotate = false;
          return true;
        }
        if (!PdfUtils.IsSameSize(w1, h1, h2, w2))
          return false;
        rotate = true;
        return true;
      }

      public static float Tolerance(int mm)
      {
        return Convert.ToSingle((double) PdfUtils.ToleranceInMM(mm) * (360.0 / (double) sbyte.MaxValue));
      }

      public static float ToleranceInMM(double mm)
      {
        if (mm <= 150.0)
          return 1.5f;
        return mm <= 600.0 ? 2f : 3f;
      }

      public static float ToleranceInMM(int mm) => PdfUtils.ToleranceInMM(Convert.ToDouble(mm));

      public static float ToleranceInPkt(double sz)
      {
        double num = sz / (360.0 / (double) sbyte.MaxValue);
        return Convert.ToSingle((double) PdfUtils.ToleranceInMM(sz) * (360.0 / (double) sbyte.MaxValue));
      }

      public static List<IntermechPageSize> GetPDFSizes(string fileName)
      {
        List<IntermechPageSize> pdfSizes = new List<IntermechPageSize>();
        using (PdfReader pdfReader = new PdfReader(fileName))
        {
          for (int index = 1; index <= pdfReader.NumberOfPages; ++index)
          {
            Rectangle ps = pdfReader.GetPageSizeWithRotation(index);
            IntermechPageSize intermechPageSize = pdfSizes.Find((Predicate<IntermechPageSize>) (x => PdfUtils.IsSameSize(x.Width, x.Height, ps.Width, ps.Height)));
            if (intermechPageSize == null)
              pdfSizes.Add(new IntermechPageSize(ps, index));
            else
              intermechPageSize.AddPage(index);
          }
        }
        pdfSizes.Sort((Comparison<IntermechPageSize>) ((x, y) => (double) Math.Abs(x.Width - y.Width) <= 0.01 ? x.Height.CompareTo(y.Height) : x.Width.CompareTo(y.Width)));
        return pdfSizes;
      }

      public static List<int> RangeToList(string range)
      {
        List<int> list = new List<int>();
        if (range != string.Empty)
        {
          string str = range;
          char[] chArray = new char[1]{ ',' };
          foreach (string source in str.Split(chArray))
          {
            if (source.Contains<char>('-'))
            {
              string[] strArray = source.Split('-');
              int int32_1 = Convert.ToInt32(strArray[0]);
              int int32_2 = Convert.ToInt32(strArray[1]);
              for (int index = int32_1; index <= int32_2; ++index)
                list.Add(index);
            }
            else
              list.Add(Convert.ToInt32(source));
          }
        }
        return list;
      }
    }
}
