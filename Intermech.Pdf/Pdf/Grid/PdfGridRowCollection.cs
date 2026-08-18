// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridRowCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Syncfusion.Pdf.Grid
{
    public class PdfGridRowCollection : List<PdfGridRow>
    {
      private PdfGrid m_grid;

      internal PdfGridRowCollection(PdfGrid grid) => this.m_grid = grid;

      public PdfGridRow Add()
      {
        PdfGridRow row = new PdfGridRow(this.m_grid);
        this.Add(row);
        return row;
      }

      public new void Add(PdfGridRow row)
      {
        if (row.Cells.Count == 0)
        {
          for (int index = 0; index < this.m_grid.Columns.Count; ++index)
            row.Cells.Add(new PdfGridCell());
        }
        base.Add(row);
      }

      public void ApplyStyle(PdfGridStyleBase style)
      {
        switch (style)
        {
          case PdfGridCellStyle _:
    label_1:
            using (List<PdfGridRow>.Enumerator enumerator1 = this.m_grid.Rows.GetEnumerator())
            {
              if (!enumerator1.MoveNext())
                break;
              IEnumerator enumerator2 = enumerator1.Current.Cells.GetEnumerator();
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
          case PdfGridRowStyle _:
            using (List<PdfGridRow>.Enumerator enumerator = this.m_grid.Rows.GetEnumerator())
            {
              while (enumerator.MoveNext())
                enumerator.Current.Style = style as PdfGridRowStyle;
              break;
            }
        }
      }

      public void SetSpan(int rowIndex, int cellIndex, int rowSpan, int colSpan)
      {
        if (rowIndex > this.m_grid.Rows.Count)
          throw new IndexOutOfRangeException(nameof (rowIndex));
        if (cellIndex > this.m_grid.Columns.Count)
          throw new IndexOutOfRangeException(nameof (cellIndex));
        this.m_grid.Rows[rowIndex].Cells[cellIndex].RowSpan = rowSpan;
        this.m_grid.Rows[rowIndex].Cells[cellIndex].ColumnSpan = colSpan;
      }
    }
}
