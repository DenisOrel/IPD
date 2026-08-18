
// Type: Intermech.IO.PathCollection
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using System.Collections.Generic;


namespace Intermech.IO
{
    public class PathCollection : OrderedList<string>
    {
      public PathCollection()
        : this(4)
      {
      }

      public PathCollection(int capacity)
        : base(capacity, (IComparer<string>) new PathComparer())
      {
      }

      public PathCollection(IEnumerable<string> collection)
        : base(collection, (IComparer<string>) new PathComparer())
      {
      }
    }
}
