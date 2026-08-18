
// Type: Intermech.UI.UIReportScopeData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.UI
{
    internal sealed class UIReportScopeData
    {
      private LinkedList<UIReportItem> items;
      private readonly LinkedList<object> operationStack;
      private int indentLevel;

      internal UIReportScopeData()
      {
        this.items = new LinkedList<UIReportItem>();
        this.operationStack = new LinkedList<object>();
      }

      public void Indent() => ++this.indentLevel;

      public void Unindent()
      {
        if (this.indentLevel == 0)
          throw new InvalidOperationException("Unbalanced Indent/Unindent calls detected.");
        --this.indentLevel;
      }

      public void ReportData(object[] data, TraceLevel traceLevel, int id)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data));
        UIReportItem uiReportItem = this.AddItem();
        uiReportItem.TraceLevel = traceLevel;
        uiReportItem.Id = id;
        uiReportItem.Data = data;
      }

      public void ReportEvent(string text, TraceLevel traceLevel, int id)
      {
        if (string.IsNullOrEmpty(text) && id == 0 && this.items.Count != 0 && string.IsNullOrEmpty(this.items.Last.Value.Text))
          return;
        UIReportItem uiReportItem = this.AddItem();
        uiReportItem.TraceLevel = traceLevel;
        uiReportItem.Id = id;
        if (string.IsNullOrEmpty(text))
          return;
        uiReportItem.Text = text;
      }

      public void ReportItem(UIReportItem item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        this.AddItem(item);
      }

      private UIReportItem AddItem() => this.AddItem(new UIReportItem());

      private UIReportItem AddItem(UIReportItem item)
      {
        item.IndentLevel += this.indentLevel;
        item.Context = new object[this.operationStack.Count];
        this.operationStack.CopyTo(item.Context, 0);
        this.items.AddLast(item);
        return item;
      }

      public IEnumerable<UIReportItem> ScanReport() => (IEnumerable<UIReportItem>) this.items;

      public ICollection<UIReportItem> ExtractReport()
      {
        LinkedList<UIReportItem> items = this.items;
        this.items = new LinkedList<UIReportItem>();
        return (ICollection<UIReportItem>) items;
      }

      public void StartLogicalOperation(object id)
      {
        if (id == null)
          throw new ArgumentNullException(nameof (id));
        this.operationStack.AddFirst(id);
      }

      public void StopLogicalOperation(object id)
      {
        if (this.operationStack.Count == 0 || !object.Equals(this.operationStack.First.Value, id))
          throw new InvalidOperationException("Unbalanced StartLogicalOperation/StopLogicalOperation calls detected.");
        this.operationStack.RemoveFirst();
      }
    }
}
