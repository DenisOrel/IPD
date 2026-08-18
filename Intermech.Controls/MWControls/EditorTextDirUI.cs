
// Type: MWControls.EditorTextDirUI
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using MWCommon;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace MWControls;

/// <summary>
/// EditorTextDirUI is used in conjunction with the EditorTextDir UITypeEditor.
/// </summary>
public class EditorTextDirUI : UserControl
{
  private ITypeDescriptorContext itdc;
  private IWindowsFormsEditorService iwfes;
  private TextDir tdTextDir;
  private MWLabel mwlblTDN;
  private MWLabel mwlblTDL;
  private MWLabel mwlblTDR;
  private MWLabel mwlblTDU;
  private Label lblDescription;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Standard constructor.</summary>
  public EditorTextDirUI() => this.InitializeComponent();

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
    this.mwlblTDN = new MWLabel();
    this.mwlblTDL = new MWLabel();
    this.mwlblTDR = new MWLabel();
    this.mwlblTDU = new MWLabel();
    this.lblDescription = new Label();
    this.SuspendLayout();
    this.mwlblTDN.BackColor = SystemColors.ActiveBorder;
    this.mwlblTDN.BorderStyle = BorderStyle.FixedSingle;
    this.mwlblTDN.ForeColor = SystemColors.HotTrack;
    this.mwlblTDN.Location = new Point(24, 0);
    this.mwlblTDN.Name = "mwlblTDN";
    this.mwlblTDN.Size = new Size(80 /*0x50*/, 24);
    this.mwlblTDN.TabIndex = 0;
    this.mwlblTDN.Text = "Text";
    this.mwlblTDN.TextAlign = ContentAlignment.MiddleCenter;
    this.mwlblTDN.MouseUp += new MouseEventHandler(this.mwlblTDN_MouseUp);
    this.mwlblTDL.BackColor = SystemColors.ActiveBorder;
    this.mwlblTDL.BorderStyle = BorderStyle.FixedSingle;
    this.mwlblTDL.ForeColor = SystemColors.ControlText;
    this.mwlblTDL.Location = new Point(0, 24);
    this.mwlblTDL.Name = "mwlblTDL";
    this.mwlblTDL.Size = new Size(24, 80 /*0x50*/);
    this.mwlblTDL.TabIndex = 1;
    this.mwlblTDL.Text = "Text";
    this.mwlblTDL.TextAlign = ContentAlignment.MiddleCenter;
    this.mwlblTDL.TextDir = TextDir.Left;
    this.mwlblTDL.MouseUp += new MouseEventHandler(this.mwlblTDL_MouseUp);
    this.mwlblTDR.BackColor = SystemColors.ActiveBorder;
    this.mwlblTDR.BorderStyle = BorderStyle.FixedSingle;
    this.mwlblTDR.ForeColor = SystemColors.ControlText;
    this.mwlblTDR.Location = new Point(104, 24);
    this.mwlblTDR.Name = "mwlblTDR";
    this.mwlblTDR.Size = new Size(24, 80 /*0x50*/);
    this.mwlblTDR.TabIndex = 2;
    this.mwlblTDR.Text = "Text";
    this.mwlblTDR.TextAlign = ContentAlignment.MiddleCenter;
    this.mwlblTDR.TextDir = TextDir.Right;
    this.mwlblTDR.MouseUp += new MouseEventHandler(this.mwlblTDR_MouseUp);
    this.mwlblTDU.BackColor = SystemColors.ActiveBorder;
    this.mwlblTDU.BorderStyle = BorderStyle.FixedSingle;
    this.mwlblTDU.ForeColor = SystemColors.ControlText;
    this.mwlblTDU.Location = new Point(24, 104);
    this.mwlblTDU.Name = "mwlblTDU";
    this.mwlblTDU.Size = new Size(80 /*0x50*/, 24);
    this.mwlblTDU.TabIndex = 3;
    this.mwlblTDU.Text = "Text";
    this.mwlblTDU.TextAlign = ContentAlignment.MiddleCenter;
    this.mwlblTDU.TextDir = TextDir.UpsideDown;
    this.mwlblTDU.MouseUp += new MouseEventHandler(this.mwlblTDU_MouseUp);
    this.lblDescription.Location = new Point(24, 24);
    this.lblDescription.Name = "lblDescription";
    this.lblDescription.Size = new Size(80 /*0x50*/, 80 /*0x50*/);
    this.lblDescription.TabIndex = 4;
    this.lblDescription.Text = "Text\nDirection:";
    this.lblDescription.TextAlign = ContentAlignment.MiddleCenter;
    this.lblDescription.MouseUp += new MouseEventHandler(this.lblDescription_MouseUp);
    this.BackColor = SystemColors.ActiveBorder;
    this.Controls.AddRange(new Control[5]
    {
      (Control) this.lblDescription,
      (Control) this.mwlblTDN,
      (Control) this.mwlblTDU,
      (Control) this.mwlblTDR,
      (Control) this.mwlblTDL
    });
    this.Name = nameof (EditorTextDirUI);
    this.Size = new Size(128 /*0x80*/, 128 /*0x80*/);
    this.Resize += new EventHandler(this.EditorTextDirUI_Resize);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Changes the TextDir and sets the new value for this ITDC.
  /// </summary>
  [Browsable(false)]
  [Category("Appearance")]
  [Description("Direction of the Text.")]
  [DefaultValue(TextDir.Normal)]
  [Editor(typeof (EditorTextDir), typeof (UITypeEditor))]
  public TextDir TextDir
  {
    get => this.tdTextDir;
    set
    {
      TextDir tdTextDir = this.tdTextDir;
      this.lblDescription.Text = "Text\nDirection: " + value.ToString();
      if (this.tdTextDir == value)
        return;
      this.tdTextDir = value;
      switch (this.tdTextDir)
      {
        case TextDir.Normal:
          this.mwlblTDN.ForeColor = SystemColors.HotTrack;
          this.mwlblTDL.ForeColor = SystemColors.ControlText;
          this.mwlblTDR.ForeColor = SystemColors.ControlText;
          this.mwlblTDU.ForeColor = SystemColors.ControlText;
          break;
        case TextDir.UpsideDown:
          this.mwlblTDN.ForeColor = SystemColors.ControlText;
          this.mwlblTDL.ForeColor = SystemColors.ControlText;
          this.mwlblTDR.ForeColor = SystemColors.ControlText;
          this.mwlblTDU.ForeColor = SystemColors.HotTrack;
          break;
        case TextDir.Left:
          this.mwlblTDN.ForeColor = SystemColors.ControlText;
          this.mwlblTDL.ForeColor = SystemColors.HotTrack;
          this.mwlblTDR.ForeColor = SystemColors.ControlText;
          this.mwlblTDU.ForeColor = SystemColors.ControlText;
          break;
        case TextDir.Right:
          this.mwlblTDN.ForeColor = SystemColors.ControlText;
          this.mwlblTDL.ForeColor = SystemColors.ControlText;
          this.mwlblTDR.ForeColor = SystemColors.HotTrack;
          this.mwlblTDU.ForeColor = SystemColors.ControlText;
          break;
      }
      this.OnTextDirChanged(new TextDirEventArgs(tdTextDir, this.tdTextDir));
      this.ITDC.PropertyDescriptor.SetValue(this.ITDC.Instance, (object) this.tdTextDir);
    }
  }

  /// <summary>Occurs when the TextDir property changes.</summary>
  [Browsable(false)]
  [Category("Appearance")]
  [Description("Occurs when the TextDir property changes.")]
  public event EditorTextDirUI.TextDirEventHandler TextDirChanged;

  /// <summary>Raises the TextDirChanged Event.</summary>
  /// <param name="e">Standard EventArgs object.</param>
  protected virtual void OnTextDirChanged(TextDirEventArgs e)
  {
    if (this.TextDirChanged == null)
      return;
    this.TextDirChanged((object) this, e);
  }

  /// <summary>
  /// The ITypeDescriptorContext of this Control.
  /// Used at design time.
  /// </summary>
  [Browsable(false)]
  [Category("Design Time")]
  [Description("ITypeDescriptorContext of this Control.")]
  [DefaultValue(null)]
  public ITypeDescriptorContext ITDC
  {
    get => this.itdc;
    set => this.itdc = value;
  }

  /// <summary>
  /// The IWindowsFormsEditorService of this Control.
  /// Used at design time.
  /// </summary>
  [Browsable(false)]
  [Category("Design Time")]
  [Description("IWindowsFormsEditorService of this Control.")]
  [DefaultValue(null)]
  public IWindowsFormsEditorService IWFES
  {
    get => this.iwfes;
    set => this.iwfes = value;
  }

  /// <summary>Always display as same size.</summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard EventArgs object.</param>
  private void EditorTextDirUI_Resize(object sender, EventArgs e)
  {
    this.Size = new Size(128 /*0x80*/, 128 /*0x80*/);
  }

  /// <summary>
  /// Select TextDir.Normal if this Control is clicked.
  /// If it is clicked with the Left MouseButton also close it.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard MouseEventArgs object.</param>
  private void mwlblTDN_MouseUp(object sender, MouseEventArgs e)
  {
    this.TextDir = TextDir.Normal;
    this.Refresh();
    if (e.Button != MouseButtons.Left)
      return;
    this.IWFES.CloseDropDown();
  }

  /// <summary>
  /// Select TextDir.UpsideDown if this Control is clicked.
  /// If it is clicked with the Left MouseButton also close it.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard MouseEventArgs object.</param>
  private void mwlblTDU_MouseUp(object sender, MouseEventArgs e)
  {
    this.TextDir = TextDir.UpsideDown;
    this.Refresh();
    if (e.Button != MouseButtons.Left)
      return;
    this.IWFES.CloseDropDown();
  }

  /// <summary>
  /// Select TextDir.Left if this Control is clicked.
  /// If it is clicked with the Left MouseButton also close it.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard MouseEventArgs object.</param>
  private void mwlblTDL_MouseUp(object sender, MouseEventArgs e)
  {
    this.TextDir = TextDir.Left;
    this.Refresh();
    if (e.Button != MouseButtons.Left)
      return;
    this.IWFES.CloseDropDown();
  }

  /// <summary>
  /// Select TextDir.Right if this Control is clicked.
  /// If it is clicked with the Left MouseButton also close it.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard MouseEventArgs object.</param>
  private void mwlblTDR_MouseUp(object sender, MouseEventArgs e)
  {
    this.TextDir = TextDir.Right;
    this.Refresh();
    if (e.Button != MouseButtons.Left)
      return;
    this.IWFES.CloseDropDown();
  }

  /// <summary>
  /// Close this Control if the Right MouseButton is clicked.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard MouseEventArgs object.</param>
  private void lblDescription_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.IWFES.CloseDropDown();
  }

  /// <summary>A delegate for event TextDirEventHandler.</summary>
  public delegate void TextDirEventHandler(object sender, TextDirEventArgs e);
}
