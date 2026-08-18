// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.TraceFileRecord
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public class TraceFileRecord
{
  public string EventStr { get; private set; }

  public string TraceFileName { get; private set; }

  public TraceFileRecord(string eventStr, string traceFileName)
  {
    this.EventStr = eventStr;
    this.TraceFileName = traceFileName;
  }
}
