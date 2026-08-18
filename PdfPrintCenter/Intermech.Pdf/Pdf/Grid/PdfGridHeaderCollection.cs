// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridHeaderCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridHeaderCollection : IEnumerable
{
  private PdfGrid m_grid;
  private List<PdfGridRow> m_rows = new List<PdfGridRow>();

  public PdfGridHeaderCollection(PdfGrid grid)
  {
    this.m_grid = grid;
    this.m_rows = new List<PdfGridRow>();
  }

  internal void Add(PdfGridRow row) => this.m_rows.Add(row);

  public PdfGridRow[] Add(int count)
  {
    for (int index1 = 0; index1 < count; ++index1)
    {
      PdfGridRow pdfGridRow = new PdfGridRow(this.m_grid);
      for (int index2 = 0; index2 < this.m_grid.Columns.Count; ++index2)
        pdfGridRow.Cells.Add(new PdfGridCell());
      this.m_rows.Add(pdfGridRow);
    }
    return this.m_rows.ToArray();
  }

  public void ApplyStyle(PdfGridStyleBase style)
  {
    switch (style)
    {
      case PdfGridCellStyle _:
label_1:
        IEnumerator enumerator1 = this.m_grid.Headers.GetEnumerator();
        try
        {
          if (!enumerator1.MoveNext())
            break;
          IEnumerator enumerator2 = ((PdfGridRow) enumerator1.Current).Cells.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
              ((PdfGridCell) enumerator2.Current).Style = style as PdfGridCellStyle;
            goto label_1;
          }
          finally
          {
            if (enumerator2 is IDisposable disposable)
              disposable.Dispose();
          }
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case PdfGridRowStyle _:
        IEnumerator enumerator3 = this.m_grid.Headers.GetEnumerator();
        try
        {
          while (enumerator3.MoveNext())
            ((PdfGridRow) enumerator3.Current).Style = style as PdfGridRowStyle;
          break;
        }
        finally
        {
          if (enumerator3 is IDisposable disposable)
            disposable.Dispose();
        }
    }
  }

  public void Clear() => this.m_rows.Clear();

  public IEnumerator GetEnumerator()
  {
    return (IEnumerator) new PdfGridHeaderCollection.PdfGridHeaderRowEnumerator(this);
  }

  internal int IndexOf(PdfGridRow row) => this.m_rows.IndexOf(row);

  public int Count => this.m_rows.Count;

  public PdfGridRow this[int index]
  {
    get
    {
      return index >= 0 && index < this.Count ? this.m_rows[index] : throw new IndexOutOfRangeException();
    }
  }

  private struct PdfGridHeaderRowEnumerator : IEnumerator
  {
    private PdfGridHeaderCollection m_headerRowCollection;
    private int m_currentIndex;

    internal PdfGridHeaderRowEnumerator(PdfGridHeaderCollection rowCollection)
    {
      this.m_headerRowCollection = rowCollection != null ? rowCollection : throw new ArgumentNullException(nameof (rowCollection));
      this.m_currentIndex = -1;
    }

    public object Current
    {
      get
      {
        this.CheckIndex();
        return (object) this.m_headerRowCollection[this.m_currentIndex];
      }
    }

    public bool MoveNext()
    {
      ++this.m_currentIndex;
      return this.m_currentIndex < this.m_headerRowCollection.Count;
    }

    public void Reset() => this.m_currentIndex = -1;

    private void CheckIndex()
    {
      if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_headerRowCollection.Count)
        throw new IndexOutOfRangeException();
    }
  }
}
