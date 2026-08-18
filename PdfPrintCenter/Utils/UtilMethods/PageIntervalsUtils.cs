
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.PageIntervalsUtils




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class PageIntervalsUtils
    {
      public static int CompareIntervals(string lhs, string rhs)
      {
        return PageIntervalsUtils.CompareIntervals(PageIntervalsUtils.GetPages(lhs), PageIntervalsUtils.GetPages(rhs));
      }

      public static int CompareIntervals(List<PageInterval> lhs, List<PageInterval> rhs)
      {
        int num1 = 0;
        foreach ((PageInterval pageInterval1, PageInterval pageInterval2) in lhs.Zip<PageInterval, PageInterval, (PageInterval, PageInterval)>((IEnumerable<PageInterval>) rhs, (Func<PageInterval, PageInterval, (PageInterval, PageInterval)>) ((first, second) => (first, second))))
        {
          int num2 = pageInterval1.Begin;
          int num3 = num2.CompareTo(pageInterval2.Begin);
          if (num3 != 0)
          {
            num1 = num3;
            break;
          }
          num2 = pageInterval1.End;
          int num4 = num2.CompareTo(pageInterval2.End);
          if (num4 != 0)
          {
            num1 = num4;
            break;
          }
        }
        return num1;
      }

      public static int GetFirstNumber(string str)
      {
        string[] source = Regex.Split(str, "\\D+");
        return ((IEnumerable<string>) source).Count<string>() > 0 ? int.Parse(source[0]) : -1;
      }

      public static int GetNumberOfPagesInIntervals(List<PageInterval> pageIntervals)
      {
        return pageIntervals.Sum<PageInterval>((Func<PageInterval, int>) (pageInterval => pageInterval.End - pageInterval.Begin + 1));
      }

      public static List<PageInterval> GetPages(string pages)
      {
        List<PageInterval> pages1 = new List<PageInterval>();
        foreach (string input in Regex.Split(pages, ","))
        {
          string[] source = Regex.Split(input, "-");
          if (((IEnumerable<string>) source).Count<string>() == 1)
            pages1.Add(new PageInterval(int.Parse(source[0]), int.Parse(source[0])));
          else if (((IEnumerable<string>) source).Count<string>() == 2)
            pages1.Add(new PageInterval(int.Parse(source[0]), int.Parse(source[1])));
        }
        return pages1;
      }

      public static bool IsManyPages(string pages) => int.TryParse(pages, out int _);
    }
}
