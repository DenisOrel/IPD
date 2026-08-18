// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.HtmlReportGenerator
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class HtmlReportGenerator
{
  private XmlDocument html;

  public HtmlReportGenerator() => this.html = new XmlDocument();

  public string CreateReport(
    DateTime creationTime,
    string reportName,
    List<ScriptCheckResult> resultList)
  {
    if (reportName == null)
      throw new ArgumentNullException("reportTitle");
    if (resultList == null)
      throw new ArgumentNullException(nameof (resultList));
    try
    {
      this.EmitReportInternal(creationTime, reportName, resultList);
      return this.html.OuterXml;
    }
    finally
    {
      this.html.RemoveAll();
    }
  }

  private void EmitReportInternal(
    DateTime creationTime,
    string reportName,
    List<ScriptCheckResult> resultList)
  {
    this.html.AppendChild((XmlNode) this.html.CreateElement("html"));
    XmlNode xmlNode = this.html.DocumentElement.AppendChild((XmlNode) this.html.CreateElement("head"));
    XmlNode parent = this.html.DocumentElement.AppendChild((XmlNode) this.html.CreateElement("body"));
    xmlNode.AppendChild((XmlNode) this.EmitPageTitle(reportName));
    xmlNode.AppendChild((XmlNode) this.EmitStyle());
    parent.AppendChild((XmlNode) this.EmitHeader(reportName));
    parent.AppendChild((XmlNode) this.EmitCreationDateTime(creationTime));
    parent.AppendChild((XmlNode) this.EmitSubHeader("Важные изменения в IPS"));
    parent.AppendChild((XmlNode) this.EmitParagraph(ScriptCheckerConsts.BreakingChangesWarning));
    parent.AppendChild((XmlNode) this.EmitParagraph(ScriptCheckerConsts.ConversionWarning));
    parent.AppendChild((XmlNode) this.EmitSubHeader("Результаты проверки"));
    parent.AppendRange(this.EmitResultListDetails(resultList));
  }

  private XmlElement EmitPageTitle(string reportName)
  {
    XmlElement element = this.html.CreateElement("title");
    element.AppendChild((XmlNode) this.html.CreateTextNode(reportName));
    return element;
  }

  private XmlElement EmitStyle()
  {
    XmlText textNode = this.html.CreateTextNode("table, th, td { border: 1px solid black; }");
    XmlElement element = this.html.CreateElement("style");
    element.AppendChild((XmlNode) textNode);
    return element;
  }

  private XmlElement EmitHeader(string reportName)
  {
    XmlElement element = this.html.CreateElement("h1");
    element.AppendChild((XmlNode) this.html.CreateTextNode(reportName));
    return element;
  }

  private XmlElement EmitSubHeader(string text)
  {
    XmlElement element = this.html.CreateElement("h2");
    element.AppendChild((XmlNode) this.html.CreateTextNode(text));
    return element;
  }

  private XmlElement EmitCreationDateTime(DateTime creationTime)
  {
    XmlElement element = this.html.CreateElement("p");
    element.AppendChild((XmlNode) this.html.CreateTextNode($"Дата создания: {creationTime:f}"));
    return element;
  }

  private IEnumerable<XmlElement> EmitResultListDetails(List<ScriptCheckResult> resultList)
  {
    List<ScriptCheckResult> all = resultList.FindAll((Predicate<ScriptCheckResult>) (item => !item.IsValid));
    if (all.Count == 0)
    {
      yield return this.EmitParagraph("Все сценарии в системе успешно прошли проверку. Никаких действий не требуется.");
    }
    else
    {
      yield return this.EmitParagraph($"В результате проверки было найдено {all.Count} сценариев, которые требуют преобразования.");
      XmlElement element = this.html.CreateElement("table");
      element.AppendChild((XmlNode) this.EmitTableRow(HtmlReportGenerator.TableRowKind.Header, "ID версии", "Заголовок сценария", "Комментарий проверки"));
      foreach (ScriptCheckResult result in resultList)
        element.AppendChild((XmlNode) this.EmitTableRow(result));
      yield return element;
    }
  }

  private XmlElement EmitTableRow(ScriptCheckResult checkResult)
  {
    return this.EmitTableRow(HtmlReportGenerator.TableRowKind.Data, checkResult.ScriptInfo.ObjectId.ToString(), checkResult.ScriptInfo.Caption, checkResult.RequiredAction);
  }

  private XmlElement EmitTableRow(HtmlReportGenerator.TableRowKind rowKind, params string[] values)
  {
    string name = rowKind == HtmlReportGenerator.TableRowKind.Header ? "th" : "td";
    XmlElement element1 = this.html.CreateElement("tr");
    foreach (string text in values)
    {
      XmlElement element2 = this.html.CreateElement(name);
      element2.AppendChild((XmlNode) this.html.CreateTextNode(text));
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  private XmlElement EmitParagraph(string text)
  {
    XmlElement element = this.html.CreateElement("p");
    element.AppendChild((XmlNode) this.html.CreateTextNode(text));
    return element;
  }

  private XmlElement EmitParagraph(string text, XmlNode childNode)
  {
    XmlElement element = this.html.CreateElement("p");
    element.AppendChild((XmlNode) this.html.CreateTextNode(text));
    element.AppendChild((XmlNode) this.html.CreateElement("br"));
    element.AppendChild(childNode);
    return element;
  }

  private enum TableRowKind
  {
    Header,
    Data,
  }
}
