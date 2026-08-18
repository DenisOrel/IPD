
// Type: Intermech.Data.DataTypeFilterAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Data
{
    public class DataTypeFilterAction : ValueRecordActionBase, IAction
    {
      private readonly TransferValueRecordAction decodeAction;
      private readonly Type dataTypeFilter;
      private readonly bool canBeNull;

      public DataTypeFilterAction(
        TransferValueRecordAction decodeAction,
        Type dataTypeFilter,
        bool canBeNull)
      {
        if (decodeAction == null)
          throw new ArgumentNullException(nameof (decodeAction));
        if (dataTypeFilter == (Type) null)
          throw new ArgumentNullException(nameof (dataTypeFilter));
        this.decodeAction = decodeAction;
        this.dataTypeFilter = dataTypeFilter;
        this.canBeNull = canBeNull;
      }

      public void Perform()
      {
        ValueRecord valueRecord = this.decodeAction.Source.Find(this.decodeAction.SourceKey);
        if (valueRecord == null)
          return;
        if (!this.dataTypeFilter.IsAssignableFrom(valueRecord.DataType))
          this.ReportBadTypedItem(valueRecord);
        else if (valueRecord.IsNull && !this.canBeNull)
          this.ReportEmptyValuedItem(valueRecord);
        else
          this.decodeAction.Perform();
      }
    }
}
