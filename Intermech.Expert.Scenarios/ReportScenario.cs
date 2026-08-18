// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ReportScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Expert.Scenarios;

public class ReportScenario : Scenario, IDBReportScenario, IDBScenario
{
  public ReportScenario()
  {
  }

  public ReportScenario(
    long scenarioID,
    string code,
    long docTemplateID,
    int createObjectType,
    int compositionRelType,
    ScenarioLangs language,
    ExecSides execSide,
    Guid guid,
    bool createInViewer,
    bool oneDocument)
    : base(scenarioID, code, language, execSide, guid, typeof (ICustomReportScenario))
  {
    this.DocTemplateID = docTemplateID;
    this.CreateObjectType = createObjectType;
    this.CompositionRelType = compositionRelType;
    this.CreateInViewer = createInViewer;
    this.OneDocument = oneDocument;
  }

  public long DocTemplateID { get; }

  public int CreateObjectType { get; }

  public int CompositionRelType { get; }

  public Stream Document { get; private set; }

  public bool CreateInViewer { get; }

  public Dictionary<Guid, string> DocumentAttributes { get; private set; }

  public bool OneDocument { get; }

  protected Stream LoadXMLFromObject(IDBObject templateObject, int fileAttributeID)
  {
    if (ScenarioTrace.General.TraceVerbose)
      Trace.WriteLine("Start ReportScenario.LoadXMLFromObject");
    IDBAttribute attributeById = templateObject.GetAttributeByID(fileAttributeID);
    if (attributeById != null)
    {
      ImChunkedStream outStream = new ImChunkedStream();
      IBlobReader blobReader = attributeById as IBlobReader;
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      try
      {
        if (blobInformation.RealFileSize > 0L)
        {
          if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
          {
            if (ScenarioTrace.General.TraceVerbose)
              Trace.WriteLine("ReportScenario.LoadXMLFromObject: unpack to stream");
            IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
            using (ImChunkedStream inStream = new ImChunkedStream())
            {
              inStream.Write(blobReader.ReadDataBlock(), 0, Convert.ToInt32(blobInformation.PackedFileSize));
              inStream.Position = 0L;
              service.UnpackStream((Stream) outStream, (Stream) inStream);
            }
          }
          else
          {
            if (ScenarioTrace.General.TraceVerbose)
              Trace.WriteLine("ReportScenario.LoadXMLFromObject: write to stream");
            outStream.Write(blobReader.ReadDataBlock(), 0, Convert.ToInt32(blobInformation.PackedFileSize));
          }
          outStream.Position = 0L;
          if (ScenarioTrace.General.TraceVerbose)
            Trace.WriteLine("End ReportScenario.LoadXMLFromObject");
          return (Stream) outStream;
        }
      }
      finally
      {
        blobReader.CloseBlob();
      }
    }
    return (Stream) null;
  }

  public override bool Execute(object session, long[] objectIDs)
  {
    if (ScenarioTrace.General.TraceVerbose)
      Trace.WriteLine("Start ReportScenario.Execute");
    if (this.code == string.Empty)
      throw new Exception("Отсутствует код сценария!");
    IUserSession userSession = (IUserSession) session;
    IDBObject templateObject = userSession.GetObject(this.DocTemplateID, true);
    ImDocumentData imDocumentData1 = (ImDocumentData) null;
    Stream stream = this.LoadXMLFromObject(templateObject, userSession.IdentHelper.FileAttributeID);
    if (stream != null)
    {
      if (ScenarioTrace.General.TraceVerbose)
        Trace.WriteLine("ReportScenario.Execute: start ImDocumentData.LoadFromXml");
      imDocumentData1 = ImDocumentData.LoadFromXml(stream);
      if (ScenarioTrace.General.TraceVerbose)
        Trace.WriteLine("ReportScenario.Execute: end ImDocumentData.LoadFromXml");
    }
    ImDocumentData imDocumentData2 = imDocumentData1.CloneFromTemplate(true, true) as ImDocumentData;
    if (!(ApplicationServices.Container.GetService<ICSharpScriptExecutor>().Execute(this.code, CSharpScriptInvocationOptions.Default, (object) (IUserSession) session, (object) imDocumentData2, (object) objectIDs) is ScriptResult scriptResult))
      throw new Exception("Результатом выполнения скрипта должен быть ScriptResult!");
    ImDocumentData documentData = scriptResult.DocumentData;
    bool result = scriptResult.Result;
    this.Document = (Stream) new ImChunkedStream();
    if (ScenarioTrace.General.TraceVerbose)
      Trace.WriteLine("ReportScenario.Execute: start ImDocumentData.SaveToXml");
    documentData.SaveToXml(this.Document);
    this.DocumentAttributes = new Dictionary<Guid, string>()
    {
      {
        new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"),
        documentData.Designation
      },
      {
        new Guid("cad00020-306c-11d8-b4e9-00304f19f545"),
        documentData.Name
      }
    };
    foreach (string attributeName in documentData.GetAttributeNames(false))
    {
      if (attributeName.StartsWith("@"))
      {
        string attributeValue = documentData.GetAttributeValue(attributeName, false);
        string str = attributeName.Remove(0, 1);
        if (GuidHelper.IsGuid(str) && !this.DocumentAttributes.ContainsKey(new Guid(str)))
          this.DocumentAttributes.Add(new Guid(str), attributeValue);
      }
    }
    if (ScenarioTrace.General.TraceVerbose)
      Trace.WriteLine("ReportScenario.Execute: end ImDocumentData.SaveToXml");
    return result;
  }
}
