
// Type: Intermech.Client.Core.QuestionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class QuestionForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button button1;
  private Button button2;
  private Button button3;
  private QuestionFormResult questionResult = QuestionFormResult.Break;
  private Label label2;
  private TextBox textBox1;
  private TableLayoutPanel tableLayoutPanel1;

  public QuestionForm(string text, string caption)
  {
    this.InitializeComponent();
    this.textBox1.Text = text;
    this.Text = caption;
  }

  /// <summary>Результат выполнения</summary>
  public QuestionFormResult QuestionResult => this.questionResult;

  /// <summary>Статическое отображение формы с возвратом результата</summary>
  /// <param name="text">Текст сообщения об ошибке</param>
  /// <param name="caption">Заголовок формы</param>
  /// <returns>Результат выполнения</returns>
  public static QuestionFormResult Show(string text, string caption)
  {
    using (QuestionForm questionForm = new QuestionForm(text, caption))
    {
      int num = (int) questionForm.ShowDialog();
      return questionForm.QuestionResult;
    }
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.questionResult = QuestionFormResult.Skip;
    this.Close();
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.questionResult = QuestionFormResult.SkipAll;
    this.Close();
  }

  private void button3_Click(object sender, EventArgs e)
  {
    this.questionResult = QuestionFormResult.Break;
    this.Close();
  }

  private void QuestionForm2_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void QuestionForm2_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (QuestionForm));
    this.button3 = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.label2 = new Label();
    this.textBox1 = new TextBox();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.DialogResult = DialogResult.Cancel;
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.label2, 4);
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.textBox1, 4);
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.textBox1, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.button3, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.button2, 3, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.button1, 2, 4);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.AcceptButton = (IButtonControl) this.button2;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button3;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (QuestionForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.QuestionForm2_FormClosed);
    this.Load += new EventHandler(this.QuestionForm2_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
