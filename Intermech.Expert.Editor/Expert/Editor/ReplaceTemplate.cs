// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ReplaceTemplate
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class ReplaceTemplate : Form
{
  public string newName = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Label label1;
  private Button btnOK;
  private Button btnCancel;
  private Label lblCoreText;
  private RadioButton rbReplace;
  private RadioButton rbUseCurrent;
  private RadioButton rbCreateNew;
  private TextBox tbNewName;

  public ReplaceTemplate() => this.InitializeComponent();

  public bool Execute(
    IWin32Window owner,
    bool template,
    string name,
    out ReplaceTemplate.ReplaceAction ra)
  {
    ra = ReplaceTemplate.ReplaceAction.raReplace;
    string str = LocalizationHolder.rm.GetString(template ? "Expert.Editor_663" : "Expert.Editor_664");
    this.Text = str + LocalizationHolder.rm.GetString("Expert.Editor_665");
    this.lblCoreText.Text = str + string.Format(LocalizationHolder.rm.GetString("Expert.Editor_666"), (object) name);
    string lower = str.ToLower();
    this.rbCreateNew.Text = string.Format(this.rbCreateNew.Text, (object) lower);
    this.rbReplace.Text = string.Format(this.rbReplace.Text, (object) lower);
    this.rbUseCurrent.Text = string.Format(this.rbUseCurrent.Text, (object) lower);
    if (this.ShowDialog(owner) != DialogResult.OK)
      return false;
    if (this.rbUseCurrent.Checked)
      ra = ReplaceTemplate.ReplaceAction.raUseCurrent;
    if (this.rbCreateNew.Checked)
    {
      ra = ReplaceTemplate.ReplaceAction.raCreateNew;
      this.newName = this.tbNewName.Text;
    }
    return true;
  }

  private void rbReplace_CheckedChanged(object sender, EventArgs e)
  {
    this.tbNewName.Enabled = this.rbCreateNew.Checked;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (!this.rbCreateNew.Checked)
      return;
    if (this.tbNewName.Text == "")
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_667"), LocalizationHolder.rm.GetString("Expert.Editor_59"));
      this.DialogResult = DialogResult.None;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objTemplate).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(ExpertConsts.Consts.attrCaption, RelationalOperators.Equal, (object) this.tbNewName.Text, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        })).Rows.Count <= 0)
          return;
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_668"), LocalizationHolder.rm.GetString("Expert.Editor_59"));
        this.DialogResult = DialogResult.None;
        this.tbNewName.Focus();
      }
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
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.lblCoreText = new Label();
    this.rbReplace = new RadioButton();
    this.rbUseCurrent = new RadioButton();
    this.rbCreateNew = new RadioButton();
    this.tbNewName = new TextBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 136);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(612, 30);
    this.panel1.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(338, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Нажатие кнопки \"Отмена\" приведет к отмене импорта скриптов";
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(448, 4);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(529, 4);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.lblCoreText.AutoSize = true;
    this.lblCoreText.Location = new Point(12, 9);
    this.lblCoreText.Name = "lblCoreText";
    this.lblCoreText.Size = new Size(35, 13);
    this.lblCoreText.TabIndex = 2;
    this.lblCoreText.Text = "label2";
    this.rbReplace.AutoSize = true;
    this.rbReplace.Checked = true;
    this.rbReplace.Location = new Point(15, 33);
    this.rbReplace.Name = "rbReplace";
    this.rbReplace.Size = new Size(288, 17);
    this.rbReplace.TabIndex = 3;
    this.rbReplace.TabStop = true;
    this.rbReplace.Text = "Заменить найденный в базе {0} импортированным ";
    this.rbReplace.UseVisualStyleBackColor = true;
    this.rbReplace.CheckedChanged += new EventHandler(this.rbReplace_CheckedChanged);
    this.rbUseCurrent.AutoSize = true;
    this.rbUseCurrent.Location = new Point(15, 56);
    this.rbUseCurrent.Name = "rbUseCurrent";
    this.rbUseCurrent.Size = new Size(406, 17);
    this.rbUseCurrent.TabIndex = 4;
    this.rbUseCurrent.Text = "Использовать существующий в базе {0} (игнорировать импортированный)";
    this.rbUseCurrent.UseVisualStyleBackColor = true;
    this.rbUseCurrent.CheckedChanged += new EventHandler(this.rbReplace_CheckedChanged);
    this.rbCreateNew.AutoSize = true;
    this.rbCreateNew.Location = new Point(15, 79);
    this.rbCreateNew.Name = "rbCreateNew";
    this.rbCreateNew.Size = new Size(222, 17);
    this.rbCreateNew.TabIndex = 5;
    this.rbCreateNew.Text = "Создать новый {0} под другим именем";
    this.rbCreateNew.UseVisualStyleBackColor = true;
    this.rbCreateNew.CheckedChanged += new EventHandler(this.rbReplace_CheckedChanged);
    this.tbNewName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbNewName.Enabled = false;
    this.tbNewName.Location = new Point(31 /*0x1F*/, 100);
    this.tbNewName.Name = "tbNewName";
    this.tbNewName.Size = new Size(573, 20);
    this.tbNewName.TabIndex = 6;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(612, 166);
    this.Controls.Add((Control) this.tbNewName);
    this.Controls.Add((Control) this.rbCreateNew);
    this.Controls.Add((Control) this.rbUseCurrent);
    this.Controls.Add((Control) this.rbReplace);
    this.Controls.Add((Control) this.lblCoreText);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReplaceTemplate);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Объект уже существует";
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public enum ReplaceAction
  {
    raReplace,
    raUseCurrent,
    raCreateNew,
  }
}
