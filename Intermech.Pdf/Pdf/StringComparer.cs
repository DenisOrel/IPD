// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.StringComparer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections;
using System.Collections.Generic;


namespace Syncfusion.Pdf
{
    internal class StringComparer : IComparer<string>, IComparer
    {
      public int Compare(object x, object y)
      {
        string strA = x as string;
        string strB = y as string;
        return strA != null && strB != null ? string.CompareOrdinal(strA, strB) : 0;
      }

      public int Compare(string x, string y)
      {
        return x != null && y != null ? string.CompareOrdinal(x, y) : 0;
      }
    }
}
