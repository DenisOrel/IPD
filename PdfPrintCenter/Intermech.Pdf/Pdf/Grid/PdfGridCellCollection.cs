// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridCellCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridCellCollection : IEnumerable
{
  private List<PdfGridCell> m_cells = new List<PdfGridCell>();
  private PdfGridRow m_row;

  internal PdfGridCellCollection(PdfGridRow row) => this.m_row = row;

  internal PdfGridCell Add()
  {
    PdfGridCell cell = new PdfGridCell();
    cell.Style = this.m_row.Style as PdfGridCellStyle;
    this.Add(cell);
    return cell;
  }

  internal void Add(PdfGridCell cell)
  {
    if (cell.Style == null)
      cell.Style = this.m_row.Style as PdfGridCellStyle;
    cell.Row = this.m_row;
    this.m_cells.Add(cell);
  }

  public IEnumerator GetEnumerator()
  {
    return (IEnumerator) new PdfGridCellCollection.PdfGridCellEnumerator(this);
  }

  public int IndexOf(PdfGridCell cell) => this.m_cells.IndexOf(cell);

  public int Count => this.m_cells.Count;

  public PdfGridCell this[int index]
  {
    get
    {
      return index >= 0 && index < this.Count ? this.m_cells[index] : throw new IndexOutOfRangeException();
    }
  }

  private struct PdfGridCellEnumerator : IEnumerator
  {
    private PdfGridCellCollection m_cellCollection;
    private int m_currentIndex;

    internal PdfGridCellEnumerator(PdfGridCellCollection columnCollection)
    {
      this.m_cellCollection = columnCollection != null ? columnCollection : throw new ArgumentNullException(nameof (columnCollection));
      this.m_currentIndex = -1;
    }

    public object Current
    {
      get
      {
        this.CheckIndex();
        return (object) this.m_cellCollection[this.m_currentIndex];
      }
    }

    public bool MoveNext()
    {
      ++this.m_currentIndex;
      return this.m_currentIndex < this.m_cellCollection.Count;
    }

    public void Reset() => this.m_currentIndex = -1;

    private void CheckIndex()
    {
      if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_cellCollection.Count)
        throw new IndexOutOfRangeException();
    }
  }
}
