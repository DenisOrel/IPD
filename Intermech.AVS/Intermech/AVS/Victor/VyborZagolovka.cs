// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VyborZagolovka
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class VyborZagolovka : Form
{
  public Guid _guidTemplateVed;
  public Guid _guidTypeVed;
  public string _documentName;
  public bool podZagolovki;
  public One_Ved_Nastr _one_Ved_Nastr;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelButtons;
  private Button bCancel;
  private Button bOK;
  private ToolTip toolTip1;
  private ImageList imageList1;
  private GroupBox GroupBoxListZagolovkov;
  public ListBox ListZagolovkov;

  public VyborZagolovka() => this.InitializeComponent();

  private void VyborZagolovka_Load(object sender, EventArgs e) => this.ListZagolovkov_draw();

  /// <summary> Отрисовка списка заголовков </summary>
  private void ListZagolovkov_draw()
  {
    Vedomost_VB.Zagolovki_Ved zagolovkiVed = this._one_Ved_Nastr._zagolovki_Ved;
    if (this._one_Ved_Nastr._zagolovki_Ved._userZagolovki)
      this.ListZagolovkov.Items.Add((object) "Пустой (Будет заполнен в редакторе)");
    for (int index = 0; index < zagolovkiVed._list_One_Zagolovok.Count; ++index)
      this.ListZagolovkov.Items.Add((object) zagolovkiVed._list_One_Zagolovok[index]._name);
    this.ListZagolovkov.SelectedIndex = 0;
  }

  private void ListZagolovkov_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VyborZagolovka));
    this.panelButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.imageList1 = new ImageList(this.components);
    this.GroupBoxListZagolovkov = new GroupBox();
    this.ListZagolovkov = new ListBox();
    this.panelButtons.SuspendLayout();
    this.GroupBoxListZagolovkov.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelButtons.Controls.Add((Control) this.bCancel);
    this.panelButtons.Controls.Add((Control) this.bOK);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 355);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(412, 42);
    this.panelButtons.TabIndex = 2;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(273, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(133, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.GroupBoxListZagolovkov.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.GroupBoxListZagolovkov.Controls.Add((Control) this.ListZagolovkov);
    this.GroupBoxListZagolovkov.Location = new Point(12, 12);
    this.GroupBoxListZagolovkov.Name = "GroupBoxListZagolovkov";
    this.GroupBoxListZagolovkov.Size = new Size(384, 337);
    this.GroupBoxListZagolovkov.TabIndex = 3;
    this.GroupBoxListZagolovkov.TabStop = false;
    this.GroupBoxListZagolovkov.Text = "Заголовки";
    this.ListZagolovkov.Dock = DockStyle.Fill;
    this.ListZagolovkov.FormattingEnabled = true;
    this.ListZagolovkov.Location = new Point(3, 16 /*0x10*/);
    this.ListZagolovkov.Name = "ListZagolovkov";
    this.ListZagolovkov.Size = new Size(378, 318);
    this.ListZagolovkov.TabIndex = 0;
    this.ListZagolovkov.MouseDoubleClick += new MouseEventHandler(this.ListZagolovkov_MouseDoubleClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(412, 397);
    this.Controls.Add((Control) this.GroupBoxListZagolovkov);
    this.Controls.Add((Control) this.panelButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VyborZagolovka);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор заголовка";
    this.Load += new EventHandler(this.VyborZagolovka_Load);
    this.panelButtons.ResumeLayout(false);
    this.GroupBoxListZagolovkov.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
