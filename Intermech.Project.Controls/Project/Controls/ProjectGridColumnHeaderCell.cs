// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectGridColumnHeaderCell
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>A project grid column header cell.</summary>
public class ProjectGridColumnHeaderCell : DataGridViewColumnHeaderCell, ICloneable, IDisposable
{
  [NotNull]
  private static readonly DataGridViewAdvancedBorderStyle _emptyBorders = new DataGridViewAdvancedBorderStyle();

  static ProjectGridColumnHeaderCell()
  {
    ProjectGridColumnHeaderCell._emptyBorders.All = DataGridViewAdvancedCellBorderStyle.None;
  }

  public void PaintOnGraphics(
    [NotNull] Graphics graphics,
    Rectangle rect,
    DataGridViewPaintParts paintParts = DataGridViewPaintParts.All,
    [CanBeNull] DataGridViewAdvancedBorderStyle borders = null)
  {
    new DataGridViewAdvancedBorderStyle().All = DataGridViewAdvancedCellBorderStyle.None;
    this.Paint(graphics, rect, rect, this.RowIndex, this.State, this.Value, this.FormattedValue, this.ErrorText, this.Style, borders ?? ProjectGridColumnHeaderCell._emptyBorders, paintParts);
  }
}
