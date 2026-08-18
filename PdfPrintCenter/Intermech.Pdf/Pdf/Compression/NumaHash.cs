// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.NumaHash
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class NumaHash
{
  private int m_initSize;
  private int m_nBuckets;
  private Dictionary<int, Syncfusion.Pdf.Compression.Numa> m_numa;

  internal NumaHash(int nbuckets, int initsize)
  {
    this.NBuckets = nbuckets;
    this.InitSize = initsize;
    this.Numa = new Dictionary<int, Syncfusion.Pdf.Compression.Numa>();
  }

  internal int InitSize
  {
    get => this.m_initSize;
    set => this.m_initSize = value;
  }

  internal int NBuckets
  {
    get => this.m_nBuckets;
    set => this.m_nBuckets = value;
  }

  internal Dictionary<int, Syncfusion.Pdf.Compression.Numa> Numa
  {
    get => this.m_numa;
    set => this.m_numa = value;
  }
}
