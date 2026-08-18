
// Type: Intermech.UI.UIReportDisplayModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.UI;

internal sealed class UIReportDisplayModule : InitializerModule
{
  private Lazy<IOutputView> outputView;
  private string viewCategory;

  public UIReportDisplayModule(Lazy<IOutputView> outputView)
  {
    this.outputView = outputView != null ? outputView : throw new ArgumentNullException(nameof (outputView));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.viewCategory = LocalizationHolder.rm.GetString("Client.Core_1614");
    UIReport.DisplayReportHandler += new EventHandler<UIReportDisplayArgs>(this.OnDisplayReport);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    UIReport.DisplayReportHandler -= new EventHandler<UIReportDisplayArgs>(this.OnDisplayReport);
    this.viewCategory = (string) null;
  }

  private void OnDisplayReport(object sender, UIReportDisplayArgs e)
  {
    if (e.ReportItems.Count == 0)
      return;
    List<string> stringList = new List<string>(e.ReportItems.Count);
    int num1 = 0;
    int num2 = 0;
    foreach (UIReportItem reportItem in (IEnumerable<UIReportItem>) e.ReportItems)
    {
      if (reportItem.TraceLevel != TraceLevel.Off)
      {
        if (reportItem.IndentLevel < num2)
          stringList.Add(string.Empty);
        num2 = reportItem.IndentLevel;
        string reportItemText = this.GetReportItemText(reportItem);
        stringList.Add(reportItemText);
        num1 += reportItemText.Length;
      }
    }
    int num3 = num1 + stringList.Count * 2;
    StringBuilder stringBuilder = new StringBuilder(num3, num3);
    foreach (string str in stringList)
      stringBuilder.AppendLine(str);
    this.outputView.Value.WriteString(this.viewCategory, stringBuilder.ToString());
  }

  private string GetReportItemText(UIReportItem item)
  {
    StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/ + item.Header.Length + item.IndentLevel * 4 + item.Text.Length + item.Data.Length * 32 /*0x20*/);
    if (!string.IsNullOrEmpty(item.Header))
    {
      stringBuilder.Append(item.Header);
      stringBuilder.Append(':');
      stringBuilder.Append(' ');
    }
    if (!string.IsNullOrEmpty(item.Text) || item.Data.Length != 0)
    {
      if (item.IndentLevel > 0)
        stringBuilder.Append(' ', item.IndentLevel * 4);
      if (item.TraceLevel == TraceLevel.Error)
      {
        stringBuilder.Append(LocalizationHolder.rm.GetString("Client.Core_171"));
        stringBuilder.Append(':');
        stringBuilder.Append(' ');
      }
      if (item.TraceLevel == TraceLevel.Warning)
      {
        stringBuilder.Append(LocalizationHolder.rm.GetString("Client.Core_377"));
        stringBuilder.Append(':');
        stringBuilder.Append(' ');
      }
      if (!string.IsNullOrEmpty(item.Text))
        stringBuilder.Append(item.Text);
      if (item.Data.Length != 0)
      {
        if (stringBuilder.Length != 0 && stringBuilder[stringBuilder.Length - 1] != ' ')
          stringBuilder.Append(' ');
        stringBuilder.Append('[');
        stringBuilder.Append(item.Data[0].ToString());
        for (int index = 1; index < item.Data.Length; ++index)
        {
          stringBuilder.Append(',');
          stringBuilder.Append(' ');
          stringBuilder.Append(item.Data[index].ToString());
        }
        stringBuilder.Append(']');
      }
    }
    return stringBuilder.ToString();
  }
}
