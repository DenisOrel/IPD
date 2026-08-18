// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ECOUpdate
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.ECO.Server;

public class ECOUpdate : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[3]
    {
      "Intermech.Eco.Base.xml",
      "Intermech.Eco.RevisionComplect.xml",
      "Intermech.Eco.RevisionIPV.xml"
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
