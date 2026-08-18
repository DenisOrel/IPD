// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.MaterialFormulaDlg
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class MaterialFormulaDlg : Form
{
  private const string ChectBoxLineTxt = "–––––––––––––––––––––––––––––";
  private const string ChectBoxNotLineTxt = "                              ";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox leftText;
  private TextBox upText;
  private TextBox downText;
  private TextBox rightText;
  private CheckBox cbDrawLine;
  private Button button1;
  private Button button2;
  private Button downArrow;
  private Button upArrow;
  private Button leftArrow;
  private Button rightArrow;
  private ToolTip toolTip1;

  public MaterialFormulaDlg() => this.InitializeComponent();

  private void leftArrow_Click(object sender, EventArgs e)
  {
    TextBox leftText = this.leftText;
    leftText.Text = leftText.Text + this.upText.Text + this.downText.Text + this.rightText.Text;
    this.upText.Text = "";
    this.downText.Text = "";
    this.rightText.Text = "";
  }

  private void upArrow_Click(object sender, EventArgs e)
  {
    bool flag = false;
    if (this.leftText.SelectionLength > 0)
    {
      string selectedText = this.leftText.SelectedText;
      this.leftText.Text = this.leftText.Text.Remove(this.leftText.SelectionStart, this.leftText.SelectionLength);
      this.upText.Text += selectedText;
      flag = true;
    }
    if (this.downText.SelectionLength > 0)
    {
      string selectedText = this.downText.SelectedText;
      this.downText.Text = this.downText.Text.Remove(this.downText.SelectionStart, this.downText.SelectionLength);
      this.upText.Text += selectedText;
      flag = true;
    }
    if (this.rightText.SelectionLength > 0)
    {
      string selectedText = this.rightText.SelectedText;
      this.rightText.Text = this.rightText.Text.Remove(this.rightText.SelectionStart, this.rightText.SelectionLength);
      this.upText.Text += selectedText;
      flag = true;
    }
    if (flag)
      return;
    int num = (int) MessageBox.Show("Выделите текст, который нужно переместить!");
  }

  private void downArrow_Click(object sender, EventArgs e)
  {
    bool flag = false;
    if (this.leftText.SelectionLength > 0)
    {
      string selectedText = this.leftText.SelectedText;
      this.leftText.Text = this.leftText.Text.Remove(this.leftText.SelectionStart, this.leftText.SelectionLength);
      this.downText.Text += selectedText;
      flag = true;
    }
    if (this.upText.SelectionLength > 0)
    {
      string selectedText = this.upText.SelectedText;
      this.upText.Text = this.upText.Text.Remove(this.upText.SelectionStart, this.upText.SelectionLength);
      this.downText.Text += selectedText;
      flag = true;
    }
    if (this.rightText.SelectionLength > 0)
    {
      string selectedText = this.rightText.SelectedText;
      this.rightText.Text = this.rightText.Text.Remove(this.rightText.SelectionStart, this.rightText.SelectionLength);
      this.downText.Text += selectedText;
      flag = true;
    }
    if (flag)
      return;
    int num = (int) MessageBox.Show("Выделите текст, который нужно переместить!");
  }

  private void rightArrow_Click(object sender, EventArgs e)
  {
    bool flag = false;
    if (this.leftText.SelectionLength > 0)
    {
      string selectedText = this.leftText.SelectedText;
      this.leftText.Text = this.leftText.Text.Remove(this.leftText.SelectionStart, this.leftText.SelectionLength);
      this.rightText.Text += selectedText;
      flag = true;
    }
    if (this.upText.SelectionLength > 0)
    {
      string selectedText = this.upText.SelectedText;
      this.upText.Text = this.upText.Text.Remove(this.upText.SelectionStart, this.upText.SelectionLength);
      this.rightText.Text += selectedText;
      flag = true;
    }
    if (this.downText.SelectionLength > 0)
    {
      string selectedText = this.downText.SelectedText;
      this.downText.Text = this.downText.Text.Remove(this.downText.SelectionStart, this.downText.SelectionLength);
      this.rightText.Text += selectedText;
      flag = true;
    }
    if (flag)
      return;
    int num = (int) MessageBox.Show("Выделите текст, который нужно переместить!");
  }

  private void cbDrawLine_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbDrawLine.Checked)
      this.cbDrawLine.Text = "–––––––––––––––––––––––––––––";
    else
      this.cbDrawLine.Text = "                              ";
  }

  public static DialogResult Execute(ref string text, List<string> materialKeyWords)
  {
    return new MaterialFormulaDlg().ExecuteDlg(ref text, materialKeyWords);
  }

  public DialogResult ExecuteDlg(ref string text, List<string> materialKeyWords)
  {
    string str = text;
    int startIndex1 = str.IndexOf("\\S");
    if (startIndex1 != -1 || materialKeyWords == null || materialKeyWords.Count == 0)
    {
      int startIndex2 = startIndex1 != -1 ? startIndex1 : 0;
      int startIndex3 = str.Substring(startIndex2).IndexOf("/");
      if (startIndex3 != -1)
        startIndex2 = (startIndex3 += startIndex2);
      int startIndex4 = str.Substring(startIndex2).IndexOf("^");
      if (startIndex4 != -1)
        startIndex2 = (startIndex4 += startIndex2);
      int num = str.Substring(startIndex2).IndexOf(";");
      if (num != -1)
      {
        int startIndex5 = num + startIndex2;
        if (startIndex5 + 1 < str.Length)
          this.rightText.Text = str.Substring(startIndex5 + 1);
        str = str.Remove(startIndex5);
      }
      if (startIndex3 != -1)
      {
        if (startIndex3 + 1 < str.Length)
          this.downText.Text = str.Substring(startIndex3 + 1);
        str = str.Remove(startIndex3);
        this.cbDrawLine.Checked = true;
      }
      else if (startIndex4 != -1)
      {
        if (startIndex4 + 1 < str.Length)
          this.downText.Text = str.Substring(startIndex4 + 1);
        str = str.Remove(startIndex4);
        this.cbDrawLine.Checked = false;
      }
      if (startIndex1 != -1)
      {
        if (startIndex1 + 2 < str.Length)
          this.upText.Text = str.Substring(startIndex1 + 2);
        str = str.Remove(startIndex1);
      }
      this.leftText.Text = str;
    }
    else
    {
      int length = -1;
      for (int index = 0; index < materialKeyWords.Count; ++index)
      {
        startIndex1 = str.IndexOf(materialKeyWords[index]);
        if (startIndex1 != -1)
        {
          length = startIndex1 + materialKeyWords[index].Length;
          break;
        }
      }
      if (startIndex1 != -1)
      {
        this.leftText.Text = str.Substring(0, length);
        int startIndex6 = length + 1;
        int num1 = str.IndexOf("/");
        if (num1 != -1)
        {
          this.upText.Text = str.Substring(startIndex6, num1 - startIndex6);
          int startIndex7 = num1 + 1;
          int num2 = str.IndexOf('\u000E');
          if (num2 == -1)
            num2 = str.IndexOf(' ');
          if (num2 != -1)
          {
            this.downText.Text = str.Substring(startIndex7, num2 - startIndex7);
            int startIndex8 = num2 + 1;
            this.rightText.Text = str.Substring(startIndex8);
          }
          else
            this.downText.Text = str.Substring(startIndex7);
        }
      }
      else
        this.leftText.Text = str;
    }
    DialogResult dialogResult = this.ShowDialog();
    if (dialogResult == DialogResult.OK)
    {
      if (this.upText.Text == "" && this.downText.Text == "")
        text = this.leftText.Text + this.rightText.Text;
      else
        text = $"{this.leftText.Text}\\S{this.upText.Text}{(this.cbDrawLine.Checked ? "/" : "^")}{this.downText.Text};{this.rightText.Text}";
    }
    return dialogResult;
  }

  private void rightText_Enter(object sender, EventArgs e)
  {
    if (this.leftText != sender)
      this.leftText.SelectionLength = 0;
    if (this.rightText != sender)
      this.rightText.SelectionLength = 0;
    if (this.upText != sender)
      this.upText.SelectionLength = 0;
    if (this.downText == sender)
      return;
    this.downText.SelectionLength = 0;
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
    this.leftText = new TextBox();
    this.upText = new TextBox();
    this.downText = new TextBox();
    this.rightText = new TextBox();
    this.cbDrawLine = new CheckBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.upArrow = new Button();
    this.rightArrow = new Button();
    this.leftArrow = new Button();
    this.downArrow = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.SuspendLayout();
    this.leftText.HideSelection = false;
    this.leftText.Location = new Point(14, 39);
    this.leftText.Name = "leftText";
    this.leftText.Size = new Size(200, 20);
    this.leftText.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.leftText, "Первая часть записи");
    this.leftText.Enter += new EventHandler(this.rightText_Enter);
    this.upText.HideSelection = false;
    this.upText.Location = new Point(213, 20);
    this.upText.Name = "upText";
    this.upText.Size = new Size(200, 20);
    this.upText.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.upText, "Числитель");
    this.upText.Enter += new EventHandler(this.rightText_Enter);
    this.downText.HideSelection = false;
    this.downText.Location = new Point(213, 59);
    this.downText.Name = "downText";
    this.downText.Size = new Size(200, 20);
    this.downText.TabIndex = 3;
    this.toolTip1.SetToolTip((Control) this.downText, "Знаменатель");
    this.downText.Enter += new EventHandler(this.rightText_Enter);
    this.rightText.HideSelection = false;
    this.rightText.Location = new Point(412, 39);
    this.rightText.Name = "rightText";
    this.rightText.Size = new Size(200, 20);
    this.rightText.TabIndex = 4;
    this.toolTip1.SetToolTip((Control) this.rightText, "Последняя часть записи");
    this.rightText.Enter += new EventHandler(this.rightText_Enter);
    this.cbDrawLine.Checked = true;
    this.cbDrawLine.CheckState = CheckState.Checked;
    this.cbDrawLine.FlatStyle = FlatStyle.System;
    this.cbDrawLine.Font = new Font("Tahoma", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.cbDrawLine.Location = new Point(215, 41);
    this.cbDrawLine.Margin = new Padding(0);
    this.cbDrawLine.Name = "cbDrawLine";
    this.cbDrawLine.Size = new Size(198, 18);
    this.cbDrawLine.TabIndex = 2;
    this.cbDrawLine.Text = "–––––––––––––––––––––––––––––";
    this.toolTip1.SetToolTip((Control) this.cbDrawLine, "Рисовать черту");
    this.cbDrawLine.UseCompatibleTextRendering = true;
    this.cbDrawLine.UseVisualStyleBackColor = true;
    this.cbDrawLine.CheckedChanged += new EventHandler(this.cbDrawLine_CheckedChanged);
    this.button1.DialogResult = DialogResult.OK;
    this.button1.FlatStyle = FlatStyle.System;
    this.button1.Location = new Point(187, 105);
    this.button1.Name = "button1";
    this.button1.Size = new Size(121, 27);
    this.button1.TabIndex = 9;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.FlatStyle = FlatStyle.System;
    this.button2.Location = new Point(314, 105);
    this.button2.Name = "button2";
    this.button2.Size = new Size(121, 27);
    this.button2.TabIndex = 10;
    this.button2.Text = "Отмена";
    this.button2.UseVisualStyleBackColor = true;
    this.upArrow.Image = (Image) Resources.ArrowUp;
    this.upArrow.Location = new Point(142, 11);
    this.upArrow.Name = "upArrow";
    this.upArrow.Size = new Size(25, 25);
    this.upArrow.TabIndex = 5;
    this.upArrow.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.upArrow, "Выделенный текст перенести в числитель");
    this.upArrow.UseVisualStyleBackColor = true;
    this.upArrow.Click += new EventHandler(this.upArrow_Click);
    this.rightArrow.Image = (Image) Resources.ArrowRight;
    this.rightArrow.Location = new Point(173, 59);
    this.rightArrow.Name = "rightArrow";
    this.rightArrow.Size = new Size(25, 25);
    this.rightArrow.TabIndex = 7;
    this.rightArrow.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.rightArrow, "Выделенный текст перенести в конец");
    this.rightArrow.UseVisualStyleBackColor = true;
    this.rightArrow.Click += new EventHandler(this.rightArrow_Click);
    this.leftArrow.Image = (Image) Resources.ArrowLeft;
    this.leftArrow.Location = new Point(111, 59);
    this.leftArrow.Name = "leftArrow";
    this.leftArrow.Size = new Size(25, 25);
    this.leftArrow.TabIndex = 8;
    this.leftArrow.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.leftArrow, "Собрать весь текст в одну строку");
    this.leftArrow.UseVisualStyleBackColor = true;
    this.leftArrow.Click += new EventHandler(this.leftArrow_Click);
    this.downArrow.Image = (Image) Resources.ArrowDown;
    this.downArrow.Location = new Point(142, 59);
    this.downArrow.Name = "downArrow";
    this.downArrow.Size = new Size(25, 25);
    this.downArrow.TabIndex = 6;
    this.downArrow.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.toolTip1.SetToolTip((Control) this.downArrow, "Выделенный текст перенести в знаменатель");
    this.downArrow.UseVisualStyleBackColor = true;
    this.downArrow.Click += new EventHandler(this.downArrow_Click);
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.ClientSize = new Size(625, 138);
    this.Controls.Add((Control) this.rightText);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.upArrow);
    this.Controls.Add((Control) this.rightArrow);
    this.Controls.Add((Control) this.leftArrow);
    this.Controls.Add((Control) this.downArrow);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.cbDrawLine);
    this.Controls.Add((Control) this.downText);
    this.Controls.Add((Control) this.upText);
    this.Controls.Add((Control) this.leftText);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (MaterialFormulaDlg);
    this.Text = "Редактирование наименования";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
