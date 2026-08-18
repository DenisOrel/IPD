// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.QueryNextRowEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class QueryNextRowEventArgs : EventArgs
{
  private int m_columnCount;
  private string[] m_rowData;
  private int m_rowIndex;

  internal QueryNextRowEventArgs(int columnCount, int rowIndex)
  {
    this.m_columnCount = columnCount >= 0 ? columnCount : throw new ArgumentOutOfRangeException(nameof (columnCount));
    this.m_rowIndex = rowIndex;
  }

  public int ColumnCount => this.m_columnCount;

  public string[] RowData
  {
    get => this.m_rowData;
    set
    {
      if (this.m_columnCount != 0 && value != null && value.Length != this.m_columnCount)
        throw new ArgumentException("The data array is not of the proper length.", nameof (RowData));
      this.m_rowData = value;
    }
  }

  public int RowIndex => this.m_rowIndex;
}
