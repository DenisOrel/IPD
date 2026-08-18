
// Type: Intermech.Data.EntityDb.Common.IIndexKeyProvider`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data.EntityDb.Common
{
    public interface IIndexKeyProvider<TProperty, TKey>
    {
      TKey FromEntityValue(TProperty propertyValue);

      TKey FromQueryCondition(object propertyValue);
    }
}
