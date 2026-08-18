// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.AcadDocumentProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

internal class AcadDocumentProxy(object rawDocument, string documentName, CadProxy cadSystem) : 
  CadDocumentProxy(rawDocument, documentName, cadSystem)
{
  protected override void DoCreateDsdFile(
    StreamWriter dsdWriter,
    string pdfFileName,
    List<string> entries)
  {
    base.DoCreateDsdFile(dsdWriter, pdfFileName, entries);
    dsdWriter.WriteLine("[SheetSet Properties]");
    dsdWriter.WriteLine("ViewFile = FALSE");
  }
}
