
// Type: Intermech.Data.ChangeValueRecordAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Data
{
    public abstract class ChangeValueRecordAction : ValueRecordActionBase, IAction
    {
      private readonly ValueBag bag;
      private readonly StringKey valueKey;

      protected ChangeValueRecordAction(ValueBag bag, StringKey valueKey)
      {
        if (bag == null)
          throw new ArgumentNullException(nameof (bag));
        if (valueKey == (StringKey) null)
          throw new ArgumentNullException(nameof (valueKey));
        this.bag = bag;
        this.valueKey = valueKey;
      }

      public ValueBag Bag => this.bag;

      public StringKey ValueKey => this.valueKey;

      public abstract void Perform();
    }
}
