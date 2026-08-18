// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ShowPostfix
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Real;
using SourceGrid3.Styles;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for ShowPostfix.</summary>
public class ShowPostfix : Form
{
  private Grid grid;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public ShowPostfix() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Execute(TempFormula tf)
  {
    this.grid.Redim(2, tf.postfixForm.Count);
    for (int index = 0; index < this.grid.ColumnsCount; ++index)
    {
      Cell cell1 = new Cell((object) index);
      this.grid[0, index] = (ICell) cell1;
      Cell cell2 = new Cell((object) tf.postfixForm[index].text);
      cell2.View.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
      this.grid[1, index] = (ICell) cell2;
    }
    this.grid.Columns.AutoSize(false);
    int num = (int) this.ShowDialog();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShowPostfix));
    this.grid = new Grid();
    this.SuspendLayout();
    this.grid.AccessibleDescription = (string) null;
    this.grid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.BackgroundImage = (System.Drawing.Image) null;
    this.grid.Font = (Font) null;
    this.grid.GridToolTipActive = true;
    this.grid.Name = "grid";
    this.grid.SpecialKeys = GridSpecialKeys.Default;
    this.grid.StyleGrid = (StyleGrid) null;
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (System.Drawing.Image) null;
    this.Controls.Add((Control) this.grid);
    this.Font = (Font) null;
    this.FormBorderStyle = FormBorderStyle.Fixed3D;
    this.Icon = (Icon) null;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ShowPostfix);
    this.ResumeLayout(false);
  }
}
