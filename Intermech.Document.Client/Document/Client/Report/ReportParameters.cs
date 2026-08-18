// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ReportParameters
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Document.Client.Report;

internal struct ReportParameters
{
  public long ReportID;
  public bool SelectedItemsOnly;
  public ISelectedItems SelectedItems;
  public INodeQuery Query;
  public IServiceProvider ViewServices;

  public ReportParameters(
    long reportID,
    ISelectedItems selectedItems,
    INodeQuery nodeQuery,
    IServiceProvider viewServices)
  {
    this.SelectedItemsOnly = true;
    this.ReportID = reportID;
    this.SelectedItems = selectedItems;
    this.Query = nodeQuery;
    this.ViewServices = viewServices;
  }

  public ReportParameters(long reportID, INodeQuery nodeQuery, IServiceProvider viewServices)
  {
    this.SelectedItemsOnly = false;
    this.ReportID = reportID;
    this.SelectedItems = (ISelectedItems) null;
    this.Query = nodeQuery;
    this.ViewServices = viewServices;
  }
}
