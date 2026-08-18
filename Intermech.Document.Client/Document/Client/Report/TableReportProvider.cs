// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReportProvider
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Провайдер редактора табличных отчетов</summary>
internal class TableReportProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("TableReportView", new ViewInfo(0, 851, typeof (TableReportView)));
    return views;
  }
}
