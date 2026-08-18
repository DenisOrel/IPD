// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridColumnCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridColumnCollection : IEnumerable
{
  private List<PdfGridColumn> m_columns = new List<PdfGridColumn>();
  private PdfGrid m_grid;
  private float m_previousCellsCount;
  private float m_width = float.MinValue;

  public PdfGridColumnCollection(PdfGrid grid)
  {
    this.m_grid = grid;
    this.m_columns = new List<PdfGridColumn>();
  }

  public PdfGridColumn Add()
  {
    PdfGridColumn pdfGridColumn = new PdfGridColumn(this.m_grid);
    this.m_columns.Add(pdfGridColumn);
    return pdfGridColumn;
  }

  public void Add(PdfGridColumn column)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    this.m_columns.Add(column);
  }

  public void Add(int count)
  {
    for (int index = 0; index < count; ++index)
    {
      this.m_columns.Add(new PdfGridColumn(this.m_grid));
      foreach (PdfGridRow row in (List<PdfGridRow>) this.m_grid.Rows)
        row.Cells.Add(new PdfGridCell()
        {
          Value = (object) ""
        });
    }
  }

  internal void AddColumns(int count)
  {
    if ((double) this.m_previousCellsCount == (double) count)
      return;
    for (int index = count - 1; index < count; ++index)
      this.m_columns.Add(new PdfGridColumn(this.m_grid));
    this.m_previousCellsCount = (float) count;
  }

  internal void Clear() => this.m_columns.Clear();

  internal float[] GetDefaultWidths(float totalWidth)
  {
    float[] defaultWidths = new float[this.Count];
    int count = this.Count;
    for (int index = 0; index < this.Count; ++index)
    {
      defaultWidths[index] = this.m_columns[index].Width;
      if ((double) this.m_columns[index].Width > 0.0)
      {
        totalWidth -= this.m_columns[index].Width;
        --count;
      }
    }
    for (int index = 0; index < this.Count; ++index)
    {
      float num = totalWidth / (float) count;
      if ((double) defaultWidths[index] <= 0.0)
        defaultWidths[index] = num;
    }
    return defaultWidths;
  }

  public IEnumerator GetEnumerator()
  {
    return (IEnumerator) new PdfGridColumnCollection.PdfGridColumnEnumerator(this);
  }

  internal float MeasureColumnsWidth()
  {
    float num = 0.0f;
    this.m_grid.MeasureColumnsWidth();
    int index = 0;
    for (int count = this.m_columns.Count; index < count; ++index)
      num += this.m_columns[index].Width;
    return num;
  }

  public int Count => this.m_columns.Count;

  public PdfGridColumn this[int index]
  {
    get
    {
      return index >= 0 && index < this.Count ? this.m_columns[index] : throw new IndexOutOfRangeException();
    }
  }

  internal float Width
  {
    get
    {
      if ((double) this.m_width == -3.4028234663852886E+38)
        this.m_width = this.MeasureColumnsWidth();
      return this.m_width;
    }
  }

  private struct PdfGridColumnEnumerator : IEnumerator
  {
    private PdfGridColumnCollection m_columnCollection;
    private int m_currentIndex;

    internal PdfGridColumnEnumerator(PdfGridColumnCollection columnCollection)
    {
      this.m_columnCollection = columnCollection != null ? columnCollection : throw new ArgumentNullException(nameof (columnCollection));
      this.m_currentIndex = -1;
    }

    public object Current
    {
      get
      {
        this.CheckIndex();
        return (object) this.m_columnCollection[this.m_currentIndex];
      }
    }

    public bool MoveNext()
    {
      ++this.m_currentIndex;
      return this.m_currentIndex < this.m_columnCollection.Count;
    }

    public void Reset() => this.m_currentIndex = -1;

    private void CheckIndex()
    {
      if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_columnCollection.Count)
        throw new IndexOutOfRangeException();
    }
  }
}
