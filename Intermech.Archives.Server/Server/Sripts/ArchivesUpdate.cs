// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.Sripts.ArchivesUpdate
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.Archives.Server.Sripts;

internal class ArchivesUpdate : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[3]
    {
      "Intermech.Archives.xml",
      "Intermech.Copies.xml",
      "Intermech.Archives.OTD.xml"
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
