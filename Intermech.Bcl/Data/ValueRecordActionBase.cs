
// Type: Intermech.Data.ValueRecordActionBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Text;
using Intermech.UI;
using System;
using System.Diagnostics;


namespace Intermech.Data
{
    public abstract class ValueRecordActionBase
    {
      protected object ScavengeSourceValue(ValueRecord sourceItem)
      {
        if (sourceItem.IsNull)
          return (object) TypedNull.Instance(sourceItem.DataType);
        return sourceItem.DataType == typeof (string) ? (object) TextServices.Trim((string) sourceItem.Value) : sourceItem.Value;
      }

      protected void ReportBadTypedItem(ValueRecord item)
      {
        if (!UIReport.Enabled)
          return;
        UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_805"), (object) item.Key, (object) item.DataType), TraceLevel.Warning);
      }

      protected void ReportBadValuedItem(ValueRecord item, Exception x)
      {
        if (!UIReport.Enabled)
          return;
        UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_806"), (object) item.Key, item.Value, (object) x.Message), TraceLevel.Warning);
      }

      protected void ReportEmptyValuedItem(ValueRecord item)
      {
        if (!UIReport.Enabled)
          return;
        UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_807"), (object) item.Key), TraceLevel.Warning);
      }
    }
}
