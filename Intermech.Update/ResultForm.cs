// Decompiled with JetBrains decompiler
// Type: Intermech.Update.ResultForm
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Update;

public class ResultForm : Form
{
  private IContainer components;
  private Panel panel2;
  private Panel panel1;
  private Button bCancel;
  private Button bOK;
  private ButtonEdit buttonEdit2;
  private Label label2;
  private Label label1;
  private OpenFileDialog openPluginFile;
  private SaveFileDialog saveXMLFile;
  private TextBox textBox1;
  private Label label3;
  private Label label4;

  public ResultForm() => this.InitializeComponent();

  private void buttonEdit2_Click(object sender, EventArgs e)
  {
    if (this.saveXMLFile.ShowDialog() != DialogResult.OK)
      return;
    this.buttonEdit2.Text = this.saveXMLFile.FileName;
  }

  public string VersionPlugin => this.textBox1.Text;

  public string FileXML => this.buttonEdit2.Text;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel2 = new Panel();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.panel1 = new Panel();
    this.textBox1 = new TextBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.buttonEdit2 = new ButtonEdit();
    this.openPluginFile = new OpenFileDialog();
    this.saveXMLFile = new SaveFileDialog();
    this.label3 = new Label();
    this.label4 = new Label();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.buttonEdit2.Properties.BeginInit();
    this.SuspendLayout();
    this.panel2.Controls.Add((Control) this.bOK);
    this.panel2.Controls.Add((Control) this.bCancel);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 134);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(435, 45);
    this.panel2.TabIndex = 1;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(267, 10);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(75, 23);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(348, 10);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 0;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.label4);
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.textBox1);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.buttonEdit2);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(435, 134);
    this.panel1.TabIndex = 2;
    this.textBox1.Location = new Point(19, 27);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(147, 20);
    this.textBox1.TabIndex = 4;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(19, 50);
    this.label2.Name = "label2";
    this.label2.Size = new Size(139, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Имя выходного xml-файла";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(19, 11);
    this.label1.Name = "label1";
    this.label1.Size = new Size(150, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Версия серверного плагина";
    this.buttonEdit2.EditValue = (object) "";
    this.buttonEdit2.Location = new Point(19, 66);
    this.buttonEdit2.Name = "buttonEdit2";
    this.buttonEdit2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit2.Size = new Size(401, 20);
    this.buttonEdit2.TabIndex = 1;
    this.buttonEdit2.Click += new EventHandler(this.buttonEdit2_Click);
    this.openPluginFile.DefaultExt = "dll";
    this.openPluginFile.Filter = "Assembly files (*.dll)|*.dll";
    this.openPluginFile.InitialDirectory = "N:\\\\ConsoleServer\\\\";
    this.openPluginFile.Title = "Укажите файл серверной сборки";
    this.openPluginFile.RestoreDirectory = true;
    this.saveXMLFile.DefaultExt = "xml";
    this.saveXMLFile.FileName = "plugin";
    this.saveXMLFile.Filter = "XML files (*.xml)|*.xml";
    this.saveXMLFile.InitialDirectory = "N:\\\\ConsoleServer\\\\";
    this.saveXMLFile.Title = "Укажите имя xml-файла ";
    this.saveXMLFile.RestoreDirectory = true;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(19, 89);
    this.label3.Name = "label3";
    this.label3.Size = new Size(373, 13);
    this.label3.TabIndex = 5;
    this.label3.Text = "Внимание: в одну папку вместе со сформированным скриптом попадут";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(19, 105);
    this.label4.Name = "label4";
    this.label4.Size = new Size(395, 13);
    this.label4.TabIndex = 6;
    this.label4.Text = "все автоматически сформированные программой файлы (например блобы)";
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(435, 179);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ResultForm);
    this.Text = "Параметры скрипта";
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.buttonEdit2.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
