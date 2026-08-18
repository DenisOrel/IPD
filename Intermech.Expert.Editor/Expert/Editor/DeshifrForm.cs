// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DeshifrForm
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class DeshifrForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button1;
  private RichTextBox memoForm;

  public DeshifrForm() => this.InitializeComponent();

  public void Execute(TempFormula tf)
  {
    this.memoForm.Text = tf.FullText();
    int pos = 0;
    for (int index = 0; index < tf.Count; ++index)
    {
      Token t = tf[index];
      string str = t.fullText(tf);
      this.PaintCurToken(pos, t, str.Length);
      pos += str.Length;
    }
    int num = (int) this.ShowDialog();
  }

  private void PaintCurToken(int pos, Token t, int len)
  {
    if (t.type != TokenType.FuncCall)
      this.memoForm.Select(pos, len);
    switch (t.type)
    {
      case TokenType.UnaryOper:
      case TokenType.BinaryOper:
        this.memoForm.SelectionColor = Color.DarkRed;
        break;
      case TokenType.OpeningBrace:
      case TokenType.ClosingBrace:
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case TokenType.FuncCall:
        this.memoForm.Select(pos, t.trueText.Length - 1);
        this.memoForm.SelectionColor = Color.Black;
        this.memoForm.Select(pos + t.trueText.Length - 1, 1);
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case TokenType.Integer:
        this.memoForm.SelectionColor = Color.Indigo;
        break;
      case TokenType.Float:
        this.memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case TokenType.String:
        this.memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case TokenType.Date:
        this.memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case TokenType.ObjectLink:
        this.memoForm.SelectionColor = Color.Red;
        break;
      default:
        this.memoForm.SelectionColor = Color.Black;
        break;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeshifrForm));
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.memoForm = new RichTextBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.button1.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.memoForm.BackColor = SystemColors.Window;
    componentResourceManager.ApplyResources((object) this.memoForm, "memoForm");
    this.memoForm.HideSelection = false;
    this.memoForm.Name = "memoForm";
    this.memoForm.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.memoForm);
    this.Controls.Add((Control) this.panel1);
    this.MinimizeBox = false;
    this.Name = nameof (DeshifrForm);
    this.Tag = (object) "";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
