
// Type: OfficePickers.Util.ContextMenuForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace OfficePickers.Util;

/// <summary>
/// Provides a System.Windows.Forms.Form that have a ContextMenu behavior.
/// Use this Form by extending it or by adding the control using the method:
/// <code>SetContainingControl(Control control)</code>
/// </summary>
public class ContextMenuForm : Form
{
  private bool _locked;
  private Control _parentControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelMain;

  /// <summary>
  /// Gets or sets a value indicating that the form is locked.
  /// The form should be locked when opening a Dialog on it.
  /// </summary>
  public bool Locked
  {
    get => this._locked;
    set => this._locked = value;
  }

  /// <summary>
  /// Initialize a new instace of the ContextMenuForm in order to hold a control that
  /// needes to have a ContextMenu behavior.
  /// </summary>
  public ContextMenuForm() => this.InitializeComponent();

  /// <summary>
  /// Shows the form on the specifies parent in the specifies location.
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="startLocation"></param>
  /// <param name="width"></param>
  public void Show(Control parent, Point startLocation, int width)
  {
    this._parentControl = parent;
    this.Location = parent.PointToScreen(startLocation);
    this.Width = width;
    this.Show();
  }

  /// <summary>
  /// Set the control that will populate the ContextMenuForm.
  /// <remarks>
  /// Any scrolling should be implemented in the control it self, the
  /// ContextMenuForm will not support scrolling.
  /// </remarks>
  /// </summary>
  /// <param name="control"></param>
  public void SetContainingControl(Control control)
  {
    this.panelMain.Controls.Clear();
    control.Dock = DockStyle.Fill;
    this.panelMain.Controls.Add(control);
  }

  private void ContextMenuPanel_Deactivate(object sender, EventArgs e)
  {
    if (this.Locked)
      return;
    this.Hide();
  }

  private void ContextMenuPanel_Leave(object sender, EventArgs e)
  {
    if (this.Locked)
      return;
    this.Hide();
  }

  public new void Hide()
  {
    base.Hide();
    if (this._parentControl == null)
      return;
    this._parentControl.FindForm()?.BringToFront();
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextMenuForm));
    this.panelMain = new Panel();
    this.SuspendLayout();
    this.panelMain.BackColor = Color.White;
    this.panelMain.BorderStyle = BorderStyle.FixedSingle;
    this.panelMain.Dock = DockStyle.Fill;
    this.panelMain.Location = new Point(0, 0);
    this.panelMain.Name = "panelMain";
    this.panelMain.Size = new Size(292, 266);
    this.panelMain.TabIndex = 0;
    this.AutoScaleMode = AutoScaleMode.None;
    this.BackColor = Color.White;
    this.ClientSize = new Size(292, 266);
    this.ControlBox = false;
    this.Controls.Add((Control) this.panelMain);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (ContextMenuForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.Manual;
    this.Text = "ContextMenuPanel";
    this.Deactivate += new EventHandler(this.ContextMenuPanel_Deactivate);
    this.Leave += new EventHandler(this.ContextMenuPanel_Leave);
    this.ResumeLayout(false);
  }
}
