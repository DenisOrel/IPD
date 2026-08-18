
// Type: Intermech.Controls.SearchDialog
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class SearchDialog : Form
{
  private readonly ISearchableBrowser _browser;
  private static string _last;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox matchCase;
  private CheckBox matchWholeWord;
  private GroupBox groupBox1;
  private RadioButton downButton;
  private RadioButton upButton;
  private Button cancelButton;
  private Button findButton;
  private TextBox searchString;
  private Label label1;

  public SearchDialog(ISearchableBrowser browser)
  {
    this._browser = browser;
    this.InitializeComponent();
    this.downButton.Checked = true;
    this.searchString.Text = SearchDialog._last;
    this.findButton.Enabled = this.searchString.Text.Length > 0;
    this.Disposed += new EventHandler(this.SearchDialog_Disposed);
    this.searchString.TextChanged += new EventHandler(this.searchString_TextChanged);
  }

  private void searchString_TextChanged(object sender, EventArgs e)
  {
    this.findButton.Enabled = this.searchString.Text.Length > 0;
  }

  private void SearchDialog_Disposed(object sender, EventArgs e)
  {
    SearchDialog._last = this.searchString.Text;
  }

  private void findButton_Click(object sender, EventArgs e)
  {
    if (this._browser.Search(this.searchString.Text, this.downButton.Checked, this.matchWholeWord.Checked, this.matchCase.Checked))
      return;
    int num = (int) MessageBox.Show((IWin32Window) this, "Поиск по документу завершен.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  private void cancelButton_Click(object sender, EventArgs e) => this.Close();

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
    this.matchCase = new CheckBox();
    this.matchWholeWord = new CheckBox();
    this.groupBox1 = new GroupBox();
    this.downButton = new RadioButton();
    this.upButton = new RadioButton();
    this.cancelButton = new Button();
    this.findButton = new Button();
    this.searchString = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.matchCase.AutoSize = true;
    this.matchCase.Location = new Point(14, 73);
    this.matchCase.Name = "matchCase";
    this.matchCase.Size = new Size(120, 17);
    this.matchCase.TabIndex = 13;
    this.matchCase.Text = "С у&четом регистра";
    this.matchCase.UseVisualStyleBackColor = true;
    this.matchWholeWord.AutoSize = true;
    this.matchWholeWord.Location = new Point(14, 47);
    this.matchWholeWord.Name = "matchWholeWord";
    this.matchWholeWord.Size = new Size(104, 17);
    this.matchWholeWord.TabIndex = 12;
    this.matchWholeWord.Text = "&Слово целиком";
    this.matchWholeWord.UseVisualStyleBackColor = true;
    this.groupBox1.Controls.Add((Control) this.downButton);
    this.groupBox1.Controls.Add((Control) this.upButton);
    this.groupBox1.Location = new Point(155, 43);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(115, 47);
    this.groupBox1.TabIndex = 11;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Направление";
    this.downButton.AutoSize = true;
    this.downButton.Location = new Point(62, 19);
    this.downButton.Name = "downButton";
    this.downButton.Size = new Size(50, 17);
    this.downButton.TabIndex = 1;
    this.downButton.TabStop = true;
    this.downButton.Text = "Вн&из";
    this.downButton.UseVisualStyleBackColor = true;
    this.upButton.AutoSize = true;
    this.upButton.Location = new Point(6, 19);
    this.upButton.Name = "upButton";
    this.upButton.Size = new Size(55, 17);
    this.upButton.TabIndex = 0;
    this.upButton.TabStop = true;
    this.upButton.Text = "В&верх";
    this.upButton.UseVisualStyleBackColor = true;
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Location = new Point(282, 43);
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.Size = new Size(79, 23);
    this.cancelButton.TabIndex = 10;
    this.cancelButton.Text = "Отмена";
    this.cancelButton.UseVisualStyleBackColor = true;
    this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
    this.findButton.Location = new Point(282, 13);
    this.findButton.Name = "findButton";
    this.findButton.Size = new Size(79, 23);
    this.findButton.TabIndex = 9;
    this.findButton.Text = "&Найти далее";
    this.findButton.UseVisualStyleBackColor = true;
    this.findButton.Click += new EventHandler(this.findButton_Click);
    this.searchString.Location = new Point(46, 14);
    this.searchString.Name = "searchString";
    this.searchString.Size = new Size(224 /*0xE0*/, 20);
    this.searchString.TabIndex = 8;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(11, 14);
    this.label1.Name = "label1";
    this.label1.Size = new Size(29, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Чт&о:";
    this.AcceptButton = (IButtonControl) this.findButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.ClientSize = new Size(368, 103);
    this.Controls.Add((Control) this.matchCase);
    this.Controls.Add((Control) this.matchWholeWord);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.findButton);
    this.Controls.Add((Control) this.searchString);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (SearchDialog);
    this.Text = "Найти";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
