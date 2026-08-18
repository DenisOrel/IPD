// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.BeginRowLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Tables
{
    public class BeginRowLayoutEventArgs : EventArgs
    {
      private bool m_bCancel;
      private bool m_bSkip;
      private PdfCellStyle m_cellStyle;
      private bool m_ignoreColumnFormat;
      private float m_minHeight;
      private int m_rowIndex;
      private int[] m_spanMap;

      internal BeginRowLayoutEventArgs(int rowIndex, PdfCellStyle cellStyle)
      {
        this.m_rowIndex = rowIndex;
        this.m_cellStyle = cellStyle;
      }

      public bool Cancel
      {
        get => this.m_bCancel;
        set => this.m_bCancel = value;
      }

      public PdfCellStyle CellStyle
      {
        get => this.m_cellStyle;
        set
        {
          this.m_cellStyle = value != null ? value : throw new ArgumentNullException(nameof (CellStyle));
        }
      }

      public int[] ColumnSpanMap
      {
        get => this.m_spanMap;
        set => this.m_spanMap = value;
      }

      public bool IgnoreColumnFormat
      {
        get => this.m_ignoreColumnFormat;
        set => this.m_ignoreColumnFormat = value;
      }

      public float MinimalHeight
      {
        get => this.m_minHeight;
        set
        {
          this.m_minHeight = (double) value >= 0.0 ? value : throw new ArgumentOutOfRangeException(nameof (MinimalHeight), "The value can't be less then zero.");
        }
      }

      public int RowIndex => this.m_rowIndex;

      public bool Skip
      {
        get => this.m_bSkip;
        set => this.m_bSkip = value;
      }
    }
}
