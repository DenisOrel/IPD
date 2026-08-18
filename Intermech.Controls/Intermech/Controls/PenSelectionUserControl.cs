
// Type: Intermech.Controls.PenSelectionUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class PenSelectionUserControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ColorSelectionUserControl _colorSelectionUserControl;
  private ColorButton _btnTransparentPen;
  private Panel _panelTansparentButton;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    this._colorSelectionUserControl = new ColorSelectionUserControl();
    this._panelTansparentButton = new Panel();
    this._btnTransparentPen = new ColorButton();
    this._panelTansparentButton.SuspendLayout();
    this.SuspendLayout();
    this._colorSelectionUserControl.BackColor = Color.White;
    this._colorSelectionUserControl.Color = Color.Red;
    this._colorSelectionUserControl.Dock = DockStyle.Top;
    this._colorSelectionUserControl.Location = new Point(0, 38);
    this._colorSelectionUserControl.Name = "_colorSelectionUserControl";
    this._colorSelectionUserControl.Size = new Size(253, 220);
    this._colorSelectionUserControl.TabIndex = 2;
    this._colorSelectionUserControl.TrackLastFocusedChildControl = true;
    this._colorSelectionUserControl.UpControl = (Control) this._btnTransparentPen;
    this._panelTansparentButton.Controls.Add((Control) this._btnTransparentPen);
    this._panelTansparentButton.Dock = DockStyle.Top;
    this._panelTansparentButton.Location = new Point(0, 0);
    this._panelTansparentButton.Name = "_panelTansparentButton";
    this._panelTansparentButton.Padding = new Padding(3, 3, 3, 5);
    this._panelTansparentButton.Size = new Size(253, 38);
    this._panelTansparentButton.TabIndex = 3;
    this._btnTransparentPen.Color = Color.White;
    this._btnTransparentPen.Dock = DockStyle.Fill;
    this._btnTransparentPen.DownControl = (Control) this._colorSelectionUserControl;
    this._btnTransparentPen.Location = new Point(3, 3);
    this._btnTransparentPen.Name = "_btnTransparentPen";
    this._btnTransparentPen.Size = new Size(247, 30);
    this._btnTransparentPen.TabIndex = 0;
    this._btnTransparentPen.Text = "Не рисовать";
    this._btnTransparentPen.UseVisualStyleBackColor = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._colorSelectionUserControl);
    this.Controls.Add((Control) this._panelTansparentButton);
    this.Name = nameof (PenSelectionUserControl);
    this.Size = new Size(253, 481);
    this._panelTansparentButton.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
