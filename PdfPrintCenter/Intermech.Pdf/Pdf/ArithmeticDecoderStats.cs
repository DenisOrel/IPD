// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ArithmeticDecoderStats
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf;

internal class ArithmeticDecoderStats
{
  private int[] m_codingContextTable;
  private int m_contextSize;

  internal ArithmeticDecoderStats(int contextSize)
  {
    this.m_contextSize = contextSize;
    this.m_codingContextTable = new int[contextSize];
    this.reset();
  }

  internal ArithmeticDecoderStats copy()
  {
    ArithmeticDecoderStats arithmeticDecoderStats = new ArithmeticDecoderStats(this.m_contextSize);
    Array.Copy((Array) this.m_codingContextTable, 0, (Array) arithmeticDecoderStats.m_codingContextTable, 0, this.m_contextSize);
    return arithmeticDecoderStats;
  }

  internal int getContextCodingTableValue(int index) => this.m_codingContextTable[index];

  internal void overwrite(ArithmeticDecoderStats stats)
  {
    Array.Copy((Array) stats.m_codingContextTable, 0, (Array) this.m_codingContextTable, 0, this.m_contextSize);
  }

  internal void reset()
  {
    for (int index = 0; index < this.m_contextSize; ++index)
      this.m_codingContextTable[index] = 0;
  }

  internal void setContextCodingTableValue(int index, int value)
  {
    this.m_codingContextTable[index] = value;
  }

  internal void setEntry(int codingContext, int i, int moreProbableSymbol)
  {
    this.m_codingContextTable[codingContext] = (i << i) + moreProbableSymbol;
  }

  internal int ContextSize => this.m_contextSize;
}
