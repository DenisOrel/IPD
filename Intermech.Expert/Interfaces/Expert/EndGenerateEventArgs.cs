// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.EndGenerateEventArgs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

public class EndGenerateEventArgs
{
  public ExpertResult result;
  public object doc;
  /// <summary>
  ///  Информация о трассировке.
  ///  На самом деле запакованный XmlDocument
  /// </summary>
  /// <remarks>Храним в запакованном виде, т.к трассировка не всегда нужна -
  /// с целью экономии памяти для больших документов</remarks>
  public byte[] traceInfo;
  public List<string> report;
  public long scriptID;
  public long[] context;
  public string docName;
  public bool WasCancelled;
  /// <summary>Способ модификации существующего документа</summary>
  public object ModifyMode;
  public HybridTableExp dtObjs;
  public HybridTableExp dtLinks;
  public Exception exception;

  public EndGenerateEventArgs(
    ExpertResult result,
    object doc,
    byte[] traceInfo,
    List<string> report,
    long scriptID,
    long[] cont,
    Exception exc,
    bool cancelled)
  {
    this.result = result;
    this.doc = doc;
    this.traceInfo = traceInfo;
    this.scriptID = scriptID;
    this.report = report;
    this.context = (long[]) cont.Clone();
    this.exception = exc;
    this.WasCancelled = cancelled;
  }
}
