// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertFunction
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertFunction : ExpertScriptable
{
  public ExpertFunction(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.objType = ExpertScriptType.FunctionScript;
  }
}
