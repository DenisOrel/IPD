// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.BrushSubItem
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Workflow.Design;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class BrushSubItem : OwnerdrawListViewSubitem, IDisposable
{
  [CanBeNull]
  public readonly Pen Pen;
  [CanBeNull]
  public readonly Brush Brush;

  public BrushSubItem([CanBeNull] Pen pen, [CanBeNull] Brush brush)
  {
    this.Pen = pen;
    this.Brush = brush;
  }

  public override void Draw(DrawInfo di, [NotNull] DrawListViewSubItemEventArgs e)
  {
    base.Draw(di, e);
    Rectangle bounds = e.Bounds;
    bounds.Inflate(-10, -3);
    Pen pen = this.Pen ?? IMProject.DefaultTaskPen;
    Brush brush = this.Brush ?? IMProject.DefaultTaskBrush;
    e.Graphics.FillRectangle(brush, bounds);
    e.Graphics.DrawRectangle(pen, bounds);
  }

  public void Dispose()
  {
    if (this.Pen != null)
      this.Pen.Dispose();
    if (this.Brush == null)
      return;
    this.Brush.Dispose();
  }
}
