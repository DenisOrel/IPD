// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.SymbolSelectChBox_Ctrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Imbase.Templates;

public class SymbolSelectChBox_Ctrl : UserControl
{
  private DialogResult _dlgRes;
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Panel _pnlBottom;
  private Panel _pnl;

  public SymbolSelectChBox_Ctrl() => this.InitializeComponent();

  public string Data
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/);
      foreach (Control control in (ArrangedElementCollection) this._pnl.Controls)
      {
        if (control is CheckBox checkBox && checkBox.Checked)
          stringBuilder.Append((char) checkBox.Tag);
      }
      return stringBuilder.ToString();
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        return;
      foreach (Control control in (ArrangedElementCollection) this._pnl.Controls)
      {
        if (control is CheckBox checkBox)
          checkBox.Checked = value.IndexOf((char) checkBox.Tag) != -1;
      }
    }
  }

  public DialogResult DlgRes => this._dlgRes;

  public event EventHandler BtnClickEvent;

  private void _btnOK_Click(object sender, EventArgs e)
  {
    this._dlgRes = Convert.ToInt16((sender as Button).Tag) == (short) 0 ? DialogResult.OK : DialogResult.Cancel;
    EventHandler btnClickEvent = this.BtnClickEvent;
    if (btnClickEvent == null)
      return;
    btnClickEvent(sender, e);
  }

  public void BuildLayout(string value)
  {
    string[] strArray = value.Replace(Environment.NewLine, "\n").Split('\n');
    int length = strArray.Length;
    using (Graphics graphics = Graphics.FromHwnd(this.Handle))
    {
      int num1 = (int) ((double) graphics.MeasureString("Wg", this._pnl.Font).Height + 4.0);
      int val1 = 0;
      int num2 = 0;
      Font font = new Font(this._pnl.Font, FontStyle.Bold);
      for (int index = 0; index < length; ++index)
      {
        string str1 = strArray[index];
        SizeF sizeF;
        if (!string.IsNullOrEmpty(str1))
        {
          if (str1[0] != '\t')
          {
            Label label1 = new Label();
            label1.Text = str1;
            label1.Font = font;
            Label label2 = label1;
            sizeF = graphics.MeasureString(label1.Text, font);
            int num3 = (int) ((double) sizeF.Width + 16.0);
            label2.Width = num3;
            label1.Parent = (Control) this._pnl;
            label1.Left = 0;
            label1.Top = num2;
            label1.Height = num1;
            val1 = Math.Max(val1, label1.Width);
          }
          else
          {
            string str2 = str1.Substring(5);
            char ch = str1[2];
            CheckBox checkBox1 = new CheckBox();
            checkBox1.Text = str2;
            checkBox1.Parent = (Control) this._pnl;
            checkBox1.Tag = (object) ch;
            checkBox1.Left = 8;
            CheckBox checkBox2 = checkBox1;
            sizeF = graphics.MeasureString(checkBox1.Text, this._pnl.Font);
            int num4 = (int) ((double) sizeF.Width + 24.0);
            checkBox2.Width = num4;
            checkBox1.Top = num2;
            checkBox1.Height = num1 + 3;
            num2 += checkBox1.Height;
            val1 = Math.Max(val1, checkBox1.Width);
          }
        }
      }
      int num5 = this.Height + (num2 - this._pnl.Height);
      if (num5 > this.Height)
        this.Height = num5;
      int num6 = this.Width + (val1 - this._pnl.Width);
      if (num6 <= this.Width)
        return;
      this.Width = num6;
    }
  }

  internal void ShowForm(string layout)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SymbolSelectChBox_Ctrl));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._pnlBottom = new Panel();
    this._pnl = new Panel();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Tag = (object) "1";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this._btnOK_Click);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.Tag = (object) "0";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._pnl, "_pnl");
    this._pnl.Name = "_pnl";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnl);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (SymbolSelectChBox_Ctrl);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
