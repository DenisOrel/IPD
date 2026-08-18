// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.EndCommandEventArgs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

public class EndCommandEventArgs
{
  public Exception Exc;
  public bool WasCancelled;
  public byte[] traceInfo;
  public List<string> report;
  public long scriptID;
  public HybridTableExp dtObjs;
  public HybridTableExp dtLinks;

  public EndCommandEventArgs(
    long scriptId,
    byte[] traceInfo,
    List<string> report,
    Exception e,
    bool cancelled)
  {
    this.Exc = e;
    this.WasCancelled = cancelled;
    this.traceInfo = traceInfo;
    this.report = report;
    this.scriptID = scriptId;
  }
}
