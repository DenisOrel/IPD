
// Type: Intermech.Data.EntityDb.Common.IDirectIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data.EntityDb.Common
{
    public interface IDirectIndex<TKey> : IIndexKeyScanner<TKey>
    {
      void AddValue(IEntity entity, TKey indexKey);

      void RemoveValue(IEntity entity, TKey indexKey);
    }
}
