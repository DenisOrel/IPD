// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.HtmlReportGeneratorExtensions
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal static class HtmlReportGeneratorExtensions
{
  public static void AppendRange(this XmlNode parent, IEnumerable<XmlElement> children)
  {
    foreach (XmlElement child in children)
      parent.AppendChild((XmlNode) child);
  }
}
