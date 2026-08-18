
// Type: Intermech.Data.EntityDb.Common.EmptyIndexKeyProvider`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class EmptyIndexKeyProvider<T> : IIndexKeyProvider<T, T>
    {
      public T FromEntityValue(T propertyValue)
      {
        if ((object) propertyValue == null)
          propertyValue = this.CreateNullValue();
        return propertyValue;
      }

      public T FromQueryCondition(object propertyValue) => this.FromEntityValue((T) propertyValue);

      private T CreateNullValue() => throw new NotImplementedException();
    }
}
