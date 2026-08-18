
// Type: Intermech.Data.EntityDb.TraceHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.Data.EntityDb
{
    internal static class TraceHelper
    {
      internal static readonly BooleanSwitch QueryTime = new BooleanSwitch("EntityDb.QueryTime", "", "0");
      internal static readonly TraceSwitch QueryCode = new TraceSwitch("EntityDb.QueryCode", "", "0");

      internal static void TraceConditionCode(EntityQuery query, IQueryCondition condition)
      {
        Trace.Indent();
        Trace.WriteLine($"Executing condition {condition}, recordLimit = {query.RecordLimit}, filter = '{query.Filter}'");
        Trace.Unindent();
      }

      internal static void TraceConditionResult(int resultCount)
      {
        Trace.Indent();
        Trace.WriteLine($"Found {resultCount} entities");
        Trace.Unindent();
      }

      internal static void TraceIndexRangeStart(EntitySet indexKeyEntitites, bool fastScanMode)
      {
        TraceHelper.TraceIndexRangeStart(indexKeyEntitites.Count, fastScanMode);
      }

      internal static void TraceIndexRangeStart(int itemsCount, bool fastScanMode)
      {
        Trace.Indent();
        Trace.WriteLine($"Scanning index range, items count = {itemsCount}, fast scan mode = {fastScanMode}");
        Trace.Unindent();
      }

      internal static void TraceIndexRangeBreak(int addCount)
      {
        Trace.Indent();
        Trace.WriteLine($"The scan stopped after {addCount} items");
        Trace.Unindent();
      }

      internal static void TraceCompoundSetBrackets(
        string operation,
        int subConditions,
        string rewriteRule)
      {
        Trace.Indent();
        Trace.WriteLine($"{operation}: found {subConditions} child conditions. Rewriting by rule: {rewriteRule}");
        Trace.Unindent();
      }

      internal static void TraceCompoundSetOperator(
        string operation,
        IQueryCondition a,
        IQueryCondition b)
      {
        Trace.Indent();
        Trace.WriteLine(operation);
        Trace.Indent();
        Trace.WriteLine(a.ToString());
        Trace.WriteLine(b.ToString());
        Trace.Unindent();
        Trace.Unindent();
      }
    }
}
