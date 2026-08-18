// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.GetTraceInfoEventArgs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.Expert;

public class GetTraceInfoEventArgs
{
  /// <summary>
  ///  Информация о трассировке.
  ///  На самом деле запакованный XmlDocument
  /// </summary>
  /// <remarks>Храним в запакованном виде, т.к трассировка не всегда нужна -
  /// с целью экономии памяти для больших документов</remarks>
  public byte[] traceInfo;

  public GetTraceInfoEventArgs(byte[] traceInfo) => this.traceInfo = traceInfo;
}
