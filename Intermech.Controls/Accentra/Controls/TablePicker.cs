
// Type: Accentra.Controls.TablePicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Accentra.Controls;

/// <summary>A FrontPage style table dimensions picker.</summary>
public class TablePicker : Form
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private Pen BeigePen = new Pen(Color.Beige, 1f);
  private Brush BeigeBrush = Brushes.Beige;
  private Brush GrayBrush = Brushes.Gray;
  private Brush BlackBrush = Brushes.Black;
  private Brush WhiteBrush = Brushes.White;
  private Pen BorderPen = new Pen(SystemColors.ControlDark);
  private Pen BluePen = new Pen(Color.SlateGray, 1f);
  private string DispText = nameof (Cancel);
  private int DispHeight = 20;
  private Font DispFont = new Font("Tahoma", 8.25f);
  private int SquareX = 20;
  private int SquareY = 20;
  private int SquareQX = 3;
  private int SquareQY = 3;
  private int SelQX = 1;
  private int SelQY = 1;
  private bool bHiding;
  private bool bCancel = true;

  public TablePicker()
  {
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.InitializeComponent();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.AutoScaleBaseSize = new Size(5, 13);
    this.BackColor = Color.WhiteSmoke;
    this.ClientSize = new Size(304, 256 /*0x0100*/);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (TablePicker);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.Manual;
    this.Text = nameof (TablePicker);
    this.Click += new EventHandler(this.TablePicker_Click);
    this.Paint += new PaintEventHandler(this.TablePicker_Paint);
    this.MouseMove += new MouseEventHandler(this.TablePicker_MouseMove);
    this.MouseEnter += new EventHandler(this.TablePicker_MouseEnter);
    this.MouseLeave += new EventHandler(this.TablePicker_MouseLeave);
    this.Deactivate += new EventHandler(this.TablePicker_Deactivate);
  }

  /// <summary>
  /// Similar to <code><see cref="T:System.Windows.Forms.DialogResult" />
  /// == <see cref="F:System.Windows.Forms.DialogResult.Cancel" /></code>,
  /// but is used as a state value before the form
  /// is hidden and cancellation is finalized.
  /// </summary>
  public bool Cancel => this.bCancel;

  /// <summary>
  /// Returns the number of columns, or the horizontal / X count,
  /// of the selection.
  /// </summary>
  public int SelectedColumns => this.SelQX;

  /// <summary>
  /// Returns the number of rows, or the vertical / Y count,
  /// of the selection.
  /// </summary>
  public int SelectedRows => this.SelQY;

  private void TablePicker_Paint(object sender, PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    if (this.SelQX > this.SquareQX - 1)
      this.SquareQX = this.SelQX + 1;
    if (this.SelQY > this.SquareQY - 1)
      this.SquareQY = this.SelQY + 1;
    this.Width = this.SquareX * this.SquareQX + 5;
    this.Height = this.SquareY * this.SquareQY + 6 + this.DispHeight;
    graphics.DrawRectangle(this.BorderPen, 0, 0, this.Width - 1, this.Height - 1);
    int num = (this.SquareY - 1) * this.SquareQY + this.SquareQY + 4;
    this.DispText = !this.Cancel ? $"{this.SelQX.ToString()} by {this.SelQY.ToString()} Table" : "Cancel";
    graphics.DrawString(this.DispText, this.DispFont, this.BlackBrush, 3f, (float) (num + 2));
    for (int index1 = 0; index1 < this.SquareQX; ++index1)
    {
      for (int index2 = 0; index2 < this.SquareQY; ++index2)
      {
        graphics.FillRectangle(this.WhiteBrush, index1 * this.SquareX + 3, index2 * this.SquareY + 3, this.SquareX - 2, this.SquareY - 2);
        graphics.DrawRectangle(this.BorderPen, index1 * this.SquareX + 3, index2 * this.SquareY + 3, this.SquareX - 2, this.SquareY - 2);
      }
    }
    for (int index3 = 0; index3 < this.SelQX; ++index3)
    {
      for (int index4 = 0; index4 < this.SelQY; ++index4)
      {
        graphics.FillRectangle(this.BeigeBrush, index3 * this.SquareX + 3, index4 * this.SquareY + 3, this.SquareX - 2, this.SquareY - 2);
        graphics.DrawRectangle(this.BluePen, index3 * this.SquareX + 3, index4 * this.SquareY + 3, this.SquareX - 2, this.SquareY - 2);
      }
    }
  }

  /// <summary>Detect termination. Hides form.</summary>
  private void TablePicker_Deactivate(object sender, EventArgs e) => this.Hide();

  /// <summary>
  /// Detects mouse movement. Tracks table dimensions selection.
  /// </summary>
  private void TablePicker_MouseMove(object sender, MouseEventArgs e)
  {
    int num1 = e.X / this.SquareX + 1;
    int num2 = e.Y / this.SquareY + 1;
    bool flag = false;
    if (num1 != this.SelQX)
    {
      flag = true;
      this.SelQX = num1;
    }
    if (num2 != this.SelQY)
    {
      flag = true;
      this.SelQY = num2;
    }
    if (!flag)
      return;
    this.Invalidate();
  }

  /// <summary>
  /// Detects mouse sudden exit from the form to indicate
  /// escaped (canceling) state.
  /// </summary>
  private void TablePicker_MouseLeave(object sender, EventArgs e)
  {
    if (!this.bHiding)
      this.bCancel = true;
    this.DialogResult = DialogResult.Cancel;
    this.Invalidate();
  }

  /// <summary>Cancels the prior cancellation caused by MouseLeave.</summary>
  private void TablePicker_MouseEnter(object sender, EventArgs e)
  {
    this.bHiding = false;
    this.bCancel = false;
    this.DialogResult = DialogResult.OK;
    this.Invalidate();
  }

  /// <summary>Detects that the user made a selection by clicking.</summary>
  private void TablePicker_Click(object sender, EventArgs e)
  {
    this.bHiding = true;
    this.Hide();
  }
}
