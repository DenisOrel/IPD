// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormDesignerUpdate
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class FormDesignerUpdate : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[1]{ "Intermech.FormDesigner.xml" };
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
