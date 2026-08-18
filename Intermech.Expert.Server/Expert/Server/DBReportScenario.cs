// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.DBReportScenario
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Scenarios;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.Expert.Server;

public class DBReportScenario : DBScenario, IDBReportScenario, IDBScenario
{
  private Stream _document;
  private Dictionary<Guid, string> _documentAttributes;

  public DBReportScenario(UserSession session)
    : base(session)
  {
  }

  public DBReportScenario(UserSession session, DataTable objectsTable)
    : base(session, objectsTable)
  {
  }

  public long DocTemplateID
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeTemplate);
      return attributeByGuid == null ? 0L : attributeByGuid.AsInteger;
    }
  }

  public int CreateObjectType
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeCreateType);
      return attributeByGuid == null || !GuidHelper.IsGuid(attributeByGuid.AsString) ? -1 : MetaDataHelper.GetObjectTypeID(attributeByGuid.AsString);
    }
  }

  public int CompositionRelType
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeСompositionRelType);
      return attributeByGuid == null || !GuidHelper.IsGuid(attributeByGuid.AsString) ? -1 : MetaDataHelper.GetRelationTypeID(attributeByGuid.AsString);
    }
  }

  public Stream Document => this._document;

  public override bool Execute(object session, long[] objectIDs)
  {
    ReportScenario reportScenario = new ReportScenario(this.ScenarioID, ScenarioHelper.ReadCodeFromAttribute((IDBObject) this), this.DocTemplateID, this.CreateObjectType, this.CompositionRelType, this.Language, this.ExecSide, this.ObjectGUID, this.CreateInViewer, this.OneDocument);
    int num = reportScenario.Execute((object) UserSession.GetSessionByID((Guid) session), objectIDs) ? 1 : 0;
    this._document = reportScenario.Document;
    this._documentAttributes = reportScenario.DocumentAttributes;
    return num != 0;
  }

  public bool CreateInViewer
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeCreateInView);
      return attributeByGuid == null || attributeByGuid.AsBoolean;
    }
  }

  internal bool ExecuteCustomReport(ImDocumentData document, long[] objectIDs)
  {
    bool flag = false;
    if (ApplicationServices.Container.GetService<ICSharpScriptExecutor>().Execute(this.Code, CSharpScriptInvocationOptions.Default, (object) this.Session, (object) document, (object) objectIDs) is ScriptResult scriptResult)
    {
      document = scriptResult.DocumentData;
      flag = scriptResult.Result;
    }
    return flag;
  }

  private string ConvertScriptArgumentToString(object argument)
  {
    if (argument == null)
      return "<null>";
    return RemotingServices.IsTransparentProxy(argument) ? "<transparent proxy>" : Convert.ToString(argument);
  }

  public Dictionary<Guid, string> DocumentAttributes => this._documentAttributes;

  public bool OneDocument
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeOneDocument);
      return attributeByGuid != null && attributeByGuid.AsBoolean;
    }
  }
}
