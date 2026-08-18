
// Type: Intermech.IO.PathComparer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.IO
{
    public sealed class PathComparer : 
      IComparer<string>,
      IComparer,
      IEqualityComparer<string>,
      IEqualityComparer
    {
      private readonly StringComparer cmp;

      public PathComparer() => this.cmp = StringComparer.CurrentCultureIgnoreCase;

      public int Compare(string x, string y) => this.cmp.Compare(x, y);

      int IComparer.Compare(object x, object y) => this.cmp.Compare((string) x, (string) y);

      public bool Equals(string x, string y) => this.cmp.Equals(x, y);

      bool IEqualityComparer.Equals(object x, object y) => this.cmp.Equals((string) x, (string) y);

      public int GetHashCode(string obj) => this.cmp.GetHashCode(obj);

      int IEqualityComparer.GetHashCode(object obj) => this.cmp.GetHashCode((string) obj);
    }
}
