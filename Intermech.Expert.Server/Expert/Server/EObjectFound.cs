// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.EObjectFound
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using System;

#nullable disable
namespace Intermech.Expert.Server;

public class EObjectFound : Exception
{
  public long objId;

  public EObjectFound(long res)
    : base("")
  {
    this.objId = res;
  }
}
