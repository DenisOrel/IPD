// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertUpdate
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertUpdate : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[7]
    {
      "Intermech.Expert.ComplectTemplate.xml",
      "Intermech.Expert.DocGen.xml",
      "Intermech.Expert.THIS.xml",
      "Intermech.Expert.Quantity.xml",
      "Intermech.Expert.Isp.xml",
      "Intermech.Expert.Scenario.xml",
      "Intermech.Expert.Script.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
  }
}
