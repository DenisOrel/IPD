// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.L_Stack
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class L_Stack
{
  private List<object> m_array;
  private L_Stack m_auxStack;
  private int m_nalloc;

  internal L_Stack(int arg)
  {
    this.Nalloc = arg;
    this.Array = new List<object>();
  }

  internal List<object> Array
  {
    get => this.m_array;
    set => this.m_array = value;
  }

  internal L_Stack AuxStack
  {
    get => this.m_auxStack;
    set => this.m_auxStack = value;
  }

  internal int Nalloc
  {
    get => this.m_nalloc;
    set => this.m_nalloc = value;
  }
}
