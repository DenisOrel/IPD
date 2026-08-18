
// Type: Intermech.Collections.ReadOnlyListWrapper`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Collections
{
    public sealed class ReadOnlyListWrapper<T>(IList<T> list) : 
      ReadOnlyCollection<T>(list),
      ICollectionWrapper<T>
    {
      ICollection<T> ICollectionWrapper<T>.Unwrap() => (ICollection<T>) this.Items;
    }
}
