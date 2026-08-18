// Decompiled with JetBrains decompiler
// Type: Intermech.Update.ErrorListForm
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Update;

public class ErrorListForm : Form
{
  private IContainer components;
  private Panel panel2;
  private Panel panel1;
  private Button bCancel;
  private Button bOK;
  private OpenFileDialog openPluginFile;
  private SaveFileDialog saveXMLFile;
  private TextBox txtErrors;

  public ErrorListForm() => this.InitializeComponent();

  public List<string> Errors
  {
    get => ((IEnumerable<string>) this.txtErrors.Lines).ToList<string>();
    set => this.txtErrors.Lines = value.ToArray();
  }

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
    this.txtErrors = new TextBox();
    this.openPluginFile = new OpenFileDialog();
    this.saveXMLFile = new SaveFileDialog();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel2.Controls.Add((Control) this.bOK);
    this.panel2.Controls.Add((Control) this.bCancel);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 311);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(637, 45);
    this.panel2.TabIndex = 1;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(469, 10);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(75, 23);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Visible = false;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(550, 10);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 0;
    this.bCancel.Text = "Закрыть";
    this.bCancel.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.txtErrors);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(637, 311);
    this.panel1.TabIndex = 2;
    this.txtErrors.AcceptsReturn = true;
    this.txtErrors.Dock = DockStyle.Fill;
    this.txtErrors.Location = new Point(0, 0);
    this.txtErrors.Multiline = true;
    this.txtErrors.Name = "txtErrors";
    this.txtErrors.ReadOnly = true;
    this.txtErrors.ScrollBars = ScrollBars.Both;
    this.txtErrors.Size = new Size(637, 311);
    this.txtErrors.TabIndex = 4;
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
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(637, 356);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ErrorListForm);
    this.Text = "Список ошибок";
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
