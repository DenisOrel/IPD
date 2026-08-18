// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.Scenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Сценарий</summary>
public class Scenario : IDBScenario
{
  /// <summary>Идентификатор версии объекта</summary>
  protected long scenarioID;
  /// <summary>Текст сценария</summary>
  protected string code;
  /// <summary>Язык сценария</summary>
  protected ScenarioLangs language;
  /// <summary>Сторона выполнения</summary>
  protected ExecSides execSide;
  /// <summary>Глобальный идентификатор скрипта</summary>
  protected Guid guid;
  protected Type instanceType;
  [Obsolete("Not supported more. Use new script format. Will be removed in IPS 8", true)]
  public static string ModuleName = "Expert_Scenario";
  [Obsolete("Not supported more. Use new script format. Will be removed in IPS 8", true)]
  public static string ParametersSectionID = "Parameters";
  [Obsolete("Not supported more. Use new script format. Will be removed in IPS 8", true)]
  public static string ParameterDeveloperMode = "DeveloperMode";

  public Scenario()
  {
  }

  public Scenario(
    long scenarioID,
    string code,
    ScenarioLangs language,
    ExecSides execSide,
    Guid guid,
    Type instanceType)
  {
    this.scenarioID = scenarioID;
    this.code = code;
    this.language = language;
    this.execSide = execSide;
    this.guid = guid;
    this.instanceType = instanceType;
  }

  public long ScenarioID => this.scenarioID;

  public string Code => this.code;

  public ScenarioLangs Language => this.language;

  public ExecSides ExecSide => this.execSide;

  [Obsolete("Not supported more. Use new script format. Will be removed in IPS 8", true)]
  public object CreateInstance(IUserSession session)
  {
    throw new NotSupportedException("Not supported more. Use new script format");
  }

  [Obsolete("Not supported more. Use new script format. Will be removed in IPS 8", true)]
  protected Assembly CompileAssembly(Assembly[] assemblies, long userID)
  {
    throw new NotSupportedException("Not supported more. Use new script format");
  }

  public virtual bool Execute(object session, long[] objectIDs) => false;
}
