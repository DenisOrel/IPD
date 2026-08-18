
// Type: Intermech.Data.TransferValueRecordAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Data
{
    public abstract class TransferValueRecordAction : ValueRecordActionBase, IAction
    {
      private readonly ValueBag source;
      private readonly StringKey sourceKey;
      private readonly ValueBag target;
      private readonly StringKey targetKey;

      protected TransferValueRecordAction(
        ValueBag source,
        StringKey sourceKey,
        ValueBag target,
        StringKey targetKey)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        if (sourceKey == (StringKey) null)
          throw new ArgumentNullException("key");
        if (target == null)
          throw new ArgumentNullException(nameof (target));
        if (targetKey == (StringKey) null)
          throw new ArgumentNullException(nameof (targetKey));
        this.source = source;
        this.sourceKey = sourceKey;
        this.target = target;
        this.targetKey = targetKey;
      }

      public ValueBag Source => this.source;

      public StringKey SourceKey => this.sourceKey;

      public ValueBag Target => this.target;

      public StringKey TargetKey => this.targetKey;

      public abstract void Perform();
    }
}
