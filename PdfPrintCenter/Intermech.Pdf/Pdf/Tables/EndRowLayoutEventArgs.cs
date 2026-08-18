// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.EndRowLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class EndRowLayoutEventArgs : EventArgs
{
  private bool m_bCancel;
  private bool m_bDrawnCompletely;
  private RectangleF m_bounds;
  private int m_rowIndex;

  internal EndRowLayoutEventArgs(int rowIndex, bool drawnCompletely, RectangleF rowBounds)
  {
    this.m_rowIndex = rowIndex;
    this.m_bDrawnCompletely = drawnCompletely;
    this.m_bounds = rowBounds;
  }

  public RectangleF Bounds => this.m_bounds;

  public bool Cancel
  {
    get => this.m_bCancel;
    set => this.m_bCancel = value;
  }

  public bool LayoutCompleted => this.m_bDrawnCompletely;

  public int RowIndex => this.m_rowIndex;
}
