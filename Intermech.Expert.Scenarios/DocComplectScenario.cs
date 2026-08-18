// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.DocComplectScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Expert.Scenarios;

public class DocComplectScenario : Scenario, IDBDocComplectScenario, IDBScenario
{
  /// <summary>Шаблон  комплекта документов</summary>
  private long _complectTemplateID;

  public DocComplectScenario()
  {
  }

  public DocComplectScenario(
    long scenarioID,
    string code,
    long complectTemplateID,
    ScenarioLangs language,
    ExecSides execSide,
    Guid guid)
    : base(scenarioID, code, language, execSide, guid, typeof (ICustomDocComplectScenario))
  {
    this._complectTemplateID = complectTemplateID;
  }

  public override bool Execute(object session, long[] objectIDs)
  {
    if (this.code == string.Empty)
      throw new Exception("Отсутствует код сценария!");
    return (bool) ApplicationServices.Container.GetService<ICSharpScriptExecutor>().Execute(this.code, CSharpScriptInvocationOptions.Default, (object) (IUserSession) session, (object) this.ComplectTemplateID, (object) objectIDs);
  }

  public long ComplectTemplateID => this._complectTemplateID;
}
