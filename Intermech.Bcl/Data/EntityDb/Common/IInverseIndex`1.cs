
// Type: Intermech.Data.EntityDb.Common.IInverseIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public interface IInverseIndex<TKey>
    {
      void AddValue(long entityId, TKey indexKey);

      void RemoveValue(long entityId, TKey indexKey);

      void RemoveAllValues(long entityId);

      IEnumerable<TKey> EnumerateKeys(long entityId);
    }
}
