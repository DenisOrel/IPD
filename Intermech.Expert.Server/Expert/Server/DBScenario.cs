// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.DBScenario
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Scenarios;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class DBScenario : DBObject, IDBScenario
{
  public DBScenario(UserSession session)
    : base(session)
  {
  }

  public DBScenario(UserSession session, DataTable objectsTable)
    : base(session, objectsTable)
  {
  }

  public long ScenarioID => this.ObjectID;

  public string Code => (string) this.GetAttributeByGuid(ScenarioGUIDs.attributeScenarioCode).Value;

  public ScenarioLangs Language
  {
    get
    {
      return (ScenarioLangs) this.GetAttributeByGuid(ScenarioGUIDs.attributeScenarioLanguage).AsInteger;
    }
  }

  public ExecSides ExecSide
  {
    get => (ExecSides) this.GetAttributeByGuid(ScenarioGUIDs.attributeExecSide).AsInteger;
  }

  public virtual bool Execute(object session, long[] objectIDs) => false;
}
