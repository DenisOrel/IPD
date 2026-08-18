
// Type: Intermech.Client.Core.ButtonedEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class ButtonedEdit : UserControl
{
  private Image _Image;
  private Image _ButtonImage;
  private string _ButtonText;
  private bool _loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private RichTextBox textBox;
  private ToolTip toolTip;
  private Button _Button;
  private PictureBox _PictureBox;
  private Label _LblCaption;

  public event EventHandler ButtonClick;

  public event EventHandler EditTextChanged;

  public ButtonedEdit() => this.InitializeComponent();

  /// <summary>Картинка слева</summary>
  public Image Image
  {
    get => this._Image;
    set
    {
      this._Image = value;
      if (this._Image != null)
      {
        this.textBox.SelectionIndent = 34;
        this._PictureBox.Visible = true;
        this._PictureBox.Image = this._Image;
      }
      else
      {
        this.textBox.SelectionIndent = 2;
        this._PictureBox.Visible = false;
      }
    }
  }

  /// <summary>Картинка на кнопке</summary>
  public Image ButtonImage
  {
    get => this._ButtonImage;
    set => this._ButtonImage = value;
  }

  /// <summary>Текст на кнопке</summary>
  [DefaultValue("")]
  public string ButtonText
  {
    get => this._ButtonText;
    set => this._ButtonText = value;
  }

  /// <summary>Значение текста в textbox</summary>
  [DefaultValue("")]
  public string Value
  {
    get => this.textBox.Text;
    set => this.textBox.Text = value;
  }

  /// <summary>Только для чтения</summary>
  [DefaultValue(false)]
  public bool ReadOnly
  {
    get => this.textBox.ReadOnly;
    set => this.textBox.ReadOnly = value;
  }

  /// <summary>Подсказка</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this.toolTip.GetToolTip((Control) this.textBox);
    set => this.toolTip.SetToolTip((Control) this.textBox, value);
  }

  /// <summary>Позиция курсора в TextBox</summary>
  public int CaretPosition => this.textBox.SelectionStart;

  /// <summary>Заголовок элемента</summary>
  public string Caption
  {
    get => this._LblCaption.Text;
    set
    {
      if (value.Length != 0)
      {
        this._LblCaption.Visible = true;
        this._LblCaption.Text = value;
        this.Height = 23 + this._LblCaption.Height + 2;
      }
      else
      {
        this._LblCaption.Visible = false;
        this._LblCaption.Text = value;
        this.Height = 23;
      }
    }
  }

  /// <summary>Видимость кнопки</summary>
  [DefaultValue(true)]
  public bool ShowButton
  {
    get => this._Button.Visible;
    set => this._Button.Visible = value;
  }

  /// <summary>Шрифт заголовка</summary>
  public Font CaptionFont
  {
    get => this._LblCaption.Font;
    set => this._LblCaption.Font = value;
  }

  /// <summary>Нажатие на кнопку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void OnButtonClick(object sender, EventArgs e)
  {
    if (this.ButtonClick == null)
      return;
    this.ButtonClick((object) this, e);
  }

  protected virtual void OnEditTextChanged(object sender, EventArgs e)
  {
    if (!this._loaded || this.EditTextChanged == null)
      return;
    this.EditTextChanged((object) this, e);
  }

  protected override void OnLoad(EventArgs e)
  {
    this._PictureBox.Size = new Size(32 /*0x20*/, this.textBox.ClientSize.Height);
    this._PictureBox.Location = new Point(1, 0);
    this._PictureBox.Cursor = Cursors.Default;
    this._PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
    this._PictureBox.BackColor = SystemColors.Window;
    if (this._Image != null)
    {
      this._PictureBox.Image = this._Image;
      this._PictureBox.Visible = true;
      this.textBox.SelectionIndent = 34;
    }
    else
    {
      this._PictureBox.Visible = false;
      this.textBox.SelectionIndent = 2;
    }
    this.textBox.Controls.Add((Control) this._PictureBox);
    this._Button.Size = new Size(25, this.textBox.ClientSize.Height);
    this._Button.Location = new Point(this.textBox.ClientSize.Width - this._Button.Width, -1);
    this._Button.BackColor = SystemColors.Control;
    this._Button.Cursor = Cursors.Default;
    this._Button.FlatStyle = FlatStyle.Popup;
    if (this._ButtonImage != null)
      this._Button.Image = this._ButtonImage;
    if (this._ButtonText != "")
      this._Button.Text = this._ButtonText;
    this.textBox.Controls.Add((Control) this._Button);
    ButtonedEdit.SendMessage(this.textBox.Handle, 211, (IntPtr) 2, (IntPtr) 2097152 /*0x200000*/);
    base.OnLoad(e);
    this._loaded = true;
  }

  [DllImport("user32.dll")]
  private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

  private void textBox_Resize(object sender, EventArgs e)
  {
    this._Button.Location = new Point(this.textBox.ClientSize.Width - this._Button.Width, -1);
  }

  private void textBox_KeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);

  private void textBox_KeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.textBox = new RichTextBox();
    this.toolTip = new ToolTip(this.components);
    this._Button = new Button();
    this._PictureBox = new PictureBox();
    this._LblCaption = new Label();
    ((ISupportInitialize) this._PictureBox).BeginInit();
    this.SuspendLayout();
    this.textBox.Dock = DockStyle.Bottom;
    this.textBox.Location = new Point(0, 0);
    this.textBox.Multiline = false;
    this.textBox.Name = "textBox";
    this.textBox.ScrollBars = RichTextBoxScrollBars.None;
    this.textBox.Size = new Size(100, 23);
    this.textBox.TabIndex = 7;
    this.textBox.Text = "";
    this.textBox.TextChanged += new EventHandler(this.OnEditTextChanged);
    this.textBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
    this.textBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
    this.textBox.Resize += new EventHandler(this.textBox_Resize);
    this._Button.Location = new Point(0, 0);
    this._Button.Name = "_Button";
    this._Button.Size = new Size(0, 0);
    this._Button.TabIndex = 9;
    this._Button.UseVisualStyleBackColor = true;
    this._Button.Click += new EventHandler(this.OnButtonClick);
    this._PictureBox.Location = new Point(0, 0);
    this._PictureBox.Name = "_PictureBox";
    this._PictureBox.Size = new Size(0, 0);
    this._PictureBox.TabIndex = 10;
    this._PictureBox.TabStop = false;
    this._LblCaption.AutoSize = true;
    this._LblCaption.Dock = DockStyle.Left;
    this._LblCaption.Location = new Point(0, 0);
    this._LblCaption.Name = "_LblCaption";
    this._LblCaption.Size = new Size(0, 13);
    this._LblCaption.TabIndex = 11;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._LblCaption);
    this.Controls.Add((Control) this._PictureBox);
    this.Controls.Add((Control) this._Button);
    this.Controls.Add((Control) this.textBox);
    this.MinimumSize = new Size(40, 20);
    this.Name = nameof (ButtonedEdit);
    this.Size = new Size(100, 23);
    ((ISupportInitialize) this._PictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
