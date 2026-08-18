
// Type: Intermech.UI.UIReport
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.UI
{
    public static class UIReport
    {
      [ThreadStatic]
      private static LinkedList<UIReportScope> scopeStack;
      [ThreadStatic]
      private static UIReportScopeData activeScopeData;
      private static readonly object syncRoot = new object();
      private static EventHandler<UIReportDisplayArgs> displayReportHandler;

      /// <summary>
      /// Создает новую область видимости только в том случае, если нет активной области.
      /// </summary>
      /// <returns>Созданная область видимости или null</returns>
      public static UIReportScope CreateScope()
      {
        return UIReport.activeScopeData != null ? (UIReportScope) null : UIReport.CreateScopeInternal();
      }

      /// <summary>
      /// Создает новую область видимости независимо от того, имеется ли уже активная область.
      /// </summary>
      /// <returns>Созданная область видимости</returns>
      private static UIReportScope CreateScopeInternal()
      {
        UIReportScopeData scopeData = new UIReportScopeData();
        UIReportScope scopeInternal = new UIReportScope(scopeData);
        if (UIReport.scopeStack == null)
          UIReport.scopeStack = new LinkedList<UIReportScope>();
        UIReport.scopeStack.AddFirst(scopeInternal);
        UIReport.activeScopeData = scopeData;
        return scopeInternal;
      }

      /// <summary>
      /// Создает новую область видимости только в том случае, если уже имеется активная область видимости. Иначе метод вернет null.
      /// Этот метод используется, если часть отчета должна быть отображена как отдельный отчет.
      /// </summary>
      /// <returns>Созданная область видимости или null</returns>
      public static UIReportScope CreateIsolatedScope()
      {
        return UIReport.activeScopeData == null ? (UIReportScope) null : UIReport.CreateScopeInternal();
      }

      /// <summary>
      /// Создает новую область видимости только в том случае, если уже имеется активная область видимости. Иначе метод вернет null.
      /// Этот метод используется, если часть отчета требует специальной обработки перед показом пользователю. С помощью области видимости,
      /// создаваемой этим методом, можно получить эту часть отчета.
      /// </summary>
      /// <returns>Созданная область видимости или null</returns>
      public static UIReportScope CreateChildScope()
      {
        UIReportScope scopeInternal = UIReport.activeScopeData != null ? UIReport.CreateScopeInternal() : (UIReportScope) null;
        if (scopeInternal != null)
          scopeInternal.DisplayReport += new EventHandler<UIReportDisplayArgs>(UIReport.ReportToParent);
        return scopeInternal;
      }

      private static void ReportToParent(object sender, UIReportDisplayArgs e)
      {
        foreach (UIReportItem reportItem in (IEnumerable<UIReportItem>) e.ReportItems)
          UIReport.ReportItem(reportItem);
      }

      public static bool Enabled => UIReport.activeScopeData != null;

      internal static void ReleaseScope(UIReportScope scope)
      {
        if (scope == null)
          throw new ArgumentNullException(nameof (scope));
        if (UIReport.activeScopeData != scope.Data)
          throw new InvalidOperationException("UIReport scopes order failed.");
        UIReport.scopeStack.RemoveFirst();
        UIReport.activeScopeData = UIReport.scopeStack.Count != 0 ? UIReport.scopeStack.First.Value.Data : (UIReportScopeData) null;
      }

      public static void ReportData(object data)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportData(data, TraceLevel.Off);
      }

      public static void ReportData(object data, TraceLevel traceLevel)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportData(data, traceLevel, 0);
      }

      public static void ReportData(object data, TraceLevel traceLevel, int id)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data));
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportData(new object[1]{ data }, traceLevel, id);
      }

      public static void ReportData(object[] data)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportData(data, TraceLevel.Off);
      }

      public static void ReportData(object[] data, TraceLevel traceLevel)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportData(data, traceLevel, 0);
      }

      public static void ReportData(object[] data, TraceLevel traceLevel, int id)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.ReportData(data, traceLevel, id);
      }

      public static void ReportEvent(string text)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportEvent(text, TraceLevel.Info);
      }

      public static void ReportEvent(string text, TraceLevel traceLevel)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.ReportEvent(text, traceLevel, 0);
      }

      public static void ReportEvent(string text, TraceLevel traceLevel, int id)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.ReportEvent(text, traceLevel, id);
      }

      public static void ReportItem(UIReportItem item)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.ReportItem(item);
      }

      public static void Indent()
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.Indent();
      }

      public static void Unindent()
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.Unindent();
      }

      public static IEnumerable<UIReportItem> ScanReport()
      {
        return UIReport.activeScopeData != null ? UIReport.activeScopeData.ScanReport() : (IEnumerable<UIReportItem>) new UIReportItem[0];
      }

      public static ICollection<UIReportItem> ExtractReport()
      {
        return UIReport.activeScopeData != null ? UIReport.activeScopeData.ExtractReport() : (ICollection<UIReportItem>) new UIReportItem[0];
      }

      public static void StartLogicalOperation(object id)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.StartLogicalOperation(id);
      }

      public static void StopLogicalOperation(object id)
      {
        if (UIReport.activeScopeData == null)
          return;
        UIReport.activeScopeData.StopLogicalOperation(id);
      }

      public static UIReportLogicalOperation CreateLogicalOperation(object id)
      {
        return UIReport.activeScopeData == null ? (UIReportLogicalOperation) null : new UIReportLogicalOperation(id);
      }

      public static event EventHandler<UIReportDisplayArgs> DisplayReportHandler
      {
        add
        {
          lock (UIReport.syncRoot)
            UIReport.displayReportHandler += value;
        }
        remove
        {
          lock (UIReport.syncRoot)
            UIReport.displayReportHandler -= value;
        }
      }

      internal static void RaiseDisplayReport(ICollection<UIReportItem> report)
      {
        if (report == null)
          throw new ArgumentNullException(nameof (report));
        if (report.Count == 0)
          return;
        EventHandler<UIReportDisplayArgs> eventHandler = (EventHandler<UIReportDisplayArgs>) null;
        lock (UIReport.syncRoot)
          eventHandler = UIReport.displayReportHandler;
        if (eventHandler == null)
          return;
        eventHandler((object) null, new UIReportDisplayArgs(report));
      }
    }
}
