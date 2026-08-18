// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.MessageForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class MessageForm : Form
{
  private IContainer components;
  private RichTextBox _rtb;
  private Panel _pnl;
  private Button _btnOK;
  private ImageList _img;

  public MessageForm(string msg, string caption, MessageBoxIcon iconType)
  {
    this.InitializeComponent();
    this._rtb.Text = msg;
    this.Text = caption;
    this.SetIcon(iconType);
  }

  private void SetIcon(MessageBoxIcon iconType)
  {
    Bitmap bmp = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    switch (iconType)
    {
      case MessageBoxIcon.Hand:
        bmp = this._img.Images[0] as Bitmap;
        break;
      case MessageBoxIcon.Exclamation:
        bmp = this._img.Images[1] as Bitmap;
        break;
    }
    this.Icon = ImageHelper.BitmapToIcon(bmp);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MessageForm));
    this._rtb = new RichTextBox();
    this._pnl = new Panel();
    this._btnOK = new Button();
    this._img = new ImageList(this.components);
    this._pnl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._rtb, "_rtb");
    this._rtb.BackColor = SystemColors.Control;
    this._rtb.BorderStyle = BorderStyle.None;
    this._rtb.Name = "_rtb";
    componentResourceManager.ApplyResources((object) this._pnl, "_pnl");
    this._pnl.BorderStyle = BorderStyle.FixedSingle;
    this._pnl.Controls.Add((Control) this._rtb);
    this._pnl.Name = "_pnl";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._img.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_img.ImageStream");
    this._img.TransparentColor = Color.Transparent;
    this._img.Images.SetKeyName(0, "Error.ico");
    this._img.Images.SetKeyName(1, "Exclamation.ico");
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._pnl);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (MessageForm);
    this.ShowInTaskbar = false;
    this._pnl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
