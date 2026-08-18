// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.QueryRowCountEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class QueryRowCountEventArgs : EventArgs
{
  private int m_rowCount;

  internal QueryRowCountEventArgs()
  {
  }

  public int RowCount
  {
    get => this.m_rowCount;
    set => this.m_rowCount = value > 0 ? value : throw new ArgumentOutOfRangeException("RowNumber");
  }
}
