
// Type: Intermech.Navigator.SelectionView.GuidSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>Summary description for GuidSelector.</summary>
public class GuidSelector : Form
{
  private Guid resultGuid;
  private Panel panel1;
  private Button buttonCancel;
  private Button buttonOk;
  private GroupBox groupBox1;
  private TextBox textBox1;
  private Label label1;
  private TextBox textBox2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public Guid ResultGuid => this.resultGuid;

  public GuidSelector(Guid aGuid)
  {
    this.InitializeComponent();
    this.resultGuid = aGuid;
    this.textBox1.Text = this.resultGuid.ToString();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GuidSelector));
    this.panel1 = new Panel();
    this.buttonCancel = new Button();
    this.buttonOk = new Button();
    this.groupBox1 = new GroupBox();
    this.textBox1 = new TextBox();
    this.label1 = new Label();
    this.textBox2 = new TextBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.buttonCancel);
    this.panel1.Controls.Add((Control) this.buttonOk);
    this.panel1.Controls.Add((Control) this.groupBox1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.ForeColor = SystemColors.WindowText;
    this.textBox1.Name = "textBox1";
    this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.BackColor = SystemColors.Control;
    this.textBox2.Name = "textBox2";
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.textBox2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.Name = nameof (GuidSelector);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void textBox1_TextChanged(object sender, EventArgs e)
  {
    bool flag = false;
    try
    {
      this.resultGuid = new Guid(((Control) sender).Text);
      flag = true;
    }
    catch
    {
      this.resultGuid = Guid.Empty;
    }
    finally
    {
      this.buttonOk.Enabled = flag;
      ((Control) sender).ForeColor = flag ? SystemColors.WindowText : Color.Red;
    }
  }
}
