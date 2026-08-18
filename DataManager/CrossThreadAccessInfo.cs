// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.CrossThreadAccessInfo
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

#nullable disable
namespace Intermech.Server.Data;

internal sealed class CrossThreadAccessInfo
{
  public CrossThreadAccessInfo(int threadId, string stackTrace)
  {
    this.ThreadId = threadId;
    this.StackTrace = stackTrace;
  }

  public int ThreadId { get; private set; }

  public string StackTrace { get; private set; }
}
