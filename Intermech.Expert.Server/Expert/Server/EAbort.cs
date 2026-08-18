// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.EAbort
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Expert;
using System;

#nullable disable
namespace Intermech.Expert.Server;

public class EAbort : Exception
{
  public ExpertResult res = ExpertResult.OK;

  public EAbort(ExpertResult res)
    : base("")
  {
    this.res = res;
  }

  public EAbort(ExpertResult res, string Message)
    : base(Message)
  {
    this.res = res;
  }
}
