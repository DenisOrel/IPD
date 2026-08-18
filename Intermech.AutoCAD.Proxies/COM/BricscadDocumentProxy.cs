// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.BricscadDocumentProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>Создает объект.</summary>
/// <param name="rawDocument">Необернутый COM-объект документа</param>
/// <param name="documentName">Имя документа для сообщений об ошибках</param>
/// <param name="cadSystem">Прокси-объект приложения</param>
internal class BricscadDocumentProxy(object rawDocument, string documentName, CadProxy cadSystem) : 
  CadDocumentProxy(rawDocument, documentName, cadSystem)
{
  /// <summary>Создает .dsd-файл для экспорта чертежа в pdf-документ</summary>
  /// <param name="pdfFileName">Полный путь с именем и расширением, по которому будет сохранен экспортированный pdf-докмуент</param>
  protected override void DoCreateDsdFile(
    StreamWriter dsdWriter,
    string pdfFileName,
    List<string> entries)
  {
    base.DoCreateDsdFile(dsdWriter, pdfFileName, entries);
    dsdWriter.WriteLine("[SheetSet Properties]");
    dsdWriter.WriteLine("NoOfCopies = 1");
    dsdWriter.WriteLine("PlotStampOn = FALSE");
    dsdWriter.WriteLine("PromptForDwfName = FALSE");
    dsdWriter.WriteLine("GenerateDwfName = FALSE");
    dsdWriter.WriteLine("IncludeLayer = FALSE");
    dsdWriter.WriteLine("ViewFile = FALSE");
    dsdWriter.WriteLine("[PdfOptions]");
    dsdWriter.WriteLine("ConvertTextToGeometry = FALSE");
    dsdWriter.WriteLine("LineMerge = FALSE");
    dsdWriter.WriteLine("EmbedTtf = TRUE");
    dsdWriter.WriteLine("ImageAntiAliasing = TRUE");
    dsdWriter.WriteLine("JPEGImageCompression = TRUE");
    dsdWriter.WriteLine("VectorResolution = 2400");
    dsdWriter.WriteLine("RasterResolution = 300");
  }
}
