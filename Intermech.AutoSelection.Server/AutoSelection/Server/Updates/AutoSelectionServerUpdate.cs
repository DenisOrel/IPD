// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.Updates.AutoSelectionServerUpdate
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.AutoSelection.Server.Updates;

internal class AutoSelectionServerUpdate : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[1]
    {
      "Intermech.AutoSelection.Server_0.xml"
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
