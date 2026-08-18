// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.FindByIdForm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

internal class FindByIdForm : Form
{
  private static FindByIdForm instance;
  private DocumentControl docControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal ComboBox _comboBoxFindText;
  public Button _btnClose;
  internal Button _btnFindNext;
  private Label _labelFindWhat;
  protected CheckBox _checkBoxWholeWord;
  protected CheckBox _checkBoxMathCase;

  protected override void OnGotFocus(EventArgs e) => base.OnGotFocus(e);

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible)
      return;
    if (this.ParentForm != null)
    {
      this.ParentForm.CancelButton = (IButtonControl) this._btnClose;
      this.ParentForm.AcceptButton = (IButtonControl) this._btnFindNext;
    }
    else
    {
      this.CancelButton = (IButtonControl) this._btnClose;
      this.AcceptButton = (IButtonControl) this._btnFindNext;
    }
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = base.ProcessCmdKey(ref msg, keyData);
    if (keyData == Keys.Escape)
    {
      this.Close();
      flag = true;
    }
    return flag;
  }

  public static FindByIdForm Execute(DocumentControl docControl)
  {
    if (FindByIdForm.instance != null)
      FindByIdForm.instance.Close();
    FindByIdForm findByIdForm;
    FindByIdForm.instance = findByIdForm = new FindByIdForm(docControl);
    findByIdForm.Show();
    return findByIdForm;
  }

  protected override void OnClosed(EventArgs e)
  {
    if (this.DocumentControl != null)
      this.DocumentControl.DocumentManager.CommandManager.ActiveTarget = (ICommandTarget) this.DocumentControl.DocumentEditorForm;
    this.DocumentControl = (DocumentControl) null;
    base.OnClosed(e);
    DocumentMenuHelper.DockManager.DockControlActivated -= new DockControlEventHandler(this.DockManager_DockControlActivated);
  }

  public FindByIdForm(DocumentControl docControl)
  {
    this.InitializeComponent();
    this.TopMost = true;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(645, 180);
    this.DocumentControl = docControl;
    DocumentMenuHelper.DockManager.DockControlActivated += new DockControlEventHandler(this.DockManager_DockControlActivated);
  }

  private void DockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
      return;
    this.DocumentControl = DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl;
  }

  public DocumentControl DocumentControl
  {
    get => this.docControl;
    set
    {
      if (this.DocumentControl != null)
        this.DocumentControl.DocumentEditorForm.Closing -= new CancelEventHandler(this.DocumentEditorForm_Closing);
      this.docControl = value;
      if (this.DocumentControl == null)
        return;
      this.DocumentControl.DocumentEditorForm.Closing += new CancelEventHandler(this.DocumentEditorForm_Closing);
    }
  }

  private void DocumentEditorForm_Closing(object sender, CancelEventArgs e)
  {
    if (e.Cancel)
      return;
    this.Close();
  }

  private void DocumentEditorForm_Closed(object sender, EventArgs e)
  {
  }

  private void _btnClose_Click(object sender, EventArgs e) => this.Close();

  private void _btnFindNext_Click(object sender, EventArgs e) => this.Find();

  private void Find()
  {
    string str1 = this._comboBoxFindText.Text;
    if (!this._comboBoxFindText.Items.Contains((object) str1))
      this._comboBoxFindText.Items.Add((object) str1);
    DocumentTreeNode node = this.docControl.SelectedNode;
    DocumentTreeNode selection = (DocumentTreeNode) null;
    if (node == null)
      node = (DocumentTreeNode) this.docControl.Document;
    for (; node != null; node = this.GetNextNode((DocumentTreeNode) this.docControl.Document, node, true))
    {
      string str2 = node.Id;
      if (!this._checkBoxMathCase.Checked)
      {
        str2 = str2.ToUpper();
        str1 = str1.ToUpper();
      }
      if (this._checkBoxWholeWord.Checked)
      {
        if (str1 == str2)
        {
          selection = node;
          break;
        }
      }
      else if (str2.Contains(str1))
      {
        selection = node;
        break;
      }
    }
    if (selection != null)
    {
      this.docControl.SetSelection(selection, true, true);
    }
    else
    {
      int num = (int) MessageBox.Show("Элемент не найден");
    }
  }

  /// <summary>Получение след узла</summary>
  /// <param name="parentNode"></param>
  /// <param name="node"></param>
  /// <param name="getChild"></param>
  /// <returns></returns>
  private DocumentTreeNode GetNextNode(
    DocumentTreeNode parentNode,
    DocumentTreeNode node,
    bool getChild)
  {
    return node.Parent == parentNode.Parent && (!getChild || node.NodesCount == 0) ? (DocumentTreeNode) null : (!(node.NodesCount > 0 & getChild) ? (node.Parent.NodesCount <= node.Index + 1 ? this.GetNextNode(parentNode, node.Parent, false) : node.Parent.Nodes[node.Index + 1]) : node.Nodes[0]);
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
    this._comboBoxFindText = new ComboBox();
    this._btnClose = new Button();
    this._btnFindNext = new Button();
    this._labelFindWhat = new Label();
    this._checkBoxWholeWord = new CheckBox();
    this._checkBoxMathCase = new CheckBox();
    this.SuspendLayout();
    this._comboBoxFindText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxFindText.FormattingEnabled = true;
    this._comboBoxFindText.Location = new Point(12, 28);
    this._comboBoxFindText.Name = "_comboBoxFindText";
    this._comboBoxFindText.Size = new Size(412, 21);
    this._comboBoxFindText.TabIndex = 1;
    this._btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnClose.DialogResult = DialogResult.Cancel;
    this._btnClose.ImeMode = ImeMode.NoControl;
    this._btnClose.Location = new Point(303, 61);
    this._btnClose.Name = "_btnClose";
    this._btnClose.Size = new Size(121, 27);
    this._btnClose.TabIndex = 5;
    this._btnClose.Text = "Закрыть";
    this._btnClose.UseVisualStyleBackColor = true;
    this._btnClose.Click += new EventHandler(this._btnClose_Click);
    this._btnFindNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnFindNext.ImeMode = ImeMode.NoControl;
    this._btnFindNext.Location = new Point(176 /*0xB0*/, 61);
    this._btnFindNext.Name = "_btnFindNext";
    this._btnFindNext.Size = new Size(121, 27);
    this._btnFindNext.TabIndex = 4;
    this._btnFindNext.Text = "На&йти далее";
    this._btnFindNext.UseVisualStyleBackColor = true;
    this._btnFindNext.Click += new EventHandler(this._btnFindNext_Click);
    this._labelFindWhat.AutoSize = true;
    this._labelFindWhat.FlatStyle = FlatStyle.System;
    this._labelFindWhat.ImeMode = ImeMode.NoControl;
    this._labelFindWhat.Location = new Point(12, 12);
    this._labelFindWhat.Name = "_labelFindWhat";
    this._labelFindWhat.Size = new Size(41, 13);
    this._labelFindWhat.TabIndex = 103;
    this._labelFindWhat.Text = "Найт&и:";
    this._checkBoxWholeWord.AutoSize = true;
    this._checkBoxWholeWord.Checked = true;
    this._checkBoxWholeWord.CheckState = CheckState.Checked;
    this._checkBoxWholeWord.FlatStyle = FlatStyle.System;
    this._checkBoxWholeWord.ImeMode = ImeMode.NoControl;
    this._checkBoxWholeWord.Location = new Point(12, 79);
    this._checkBoxWholeWord.Name = "_checkBoxWholeWord";
    this._checkBoxWholeWord.Size = new Size(149, 18);
    this._checkBoxWholeWord.TabIndex = 3;
    this._checkBoxWholeWord.Text = "Только слово целиком";
    this._checkBoxWholeWord.UseVisualStyleBackColor = true;
    this._checkBoxMathCase.AutoSize = true;
    this._checkBoxMathCase.Checked = true;
    this._checkBoxMathCase.CheckState = CheckState.Checked;
    this._checkBoxMathCase.FlatStyle = FlatStyle.System;
    this._checkBoxMathCase.ImeMode = ImeMode.NoControl;
    this._checkBoxMathCase.Location = new Point(12, 55);
    this._checkBoxMathCase.Name = "_checkBoxMathCase";
    this._checkBoxMathCase.Size = new Size(130, 18);
    this._checkBoxMathCase.TabIndex = 2;
    this._checkBoxMathCase.Text = "Учитывать регистр";
    this._checkBoxMathCase.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(436, 103);
    this.Controls.Add((Control) this._comboBoxFindText);
    this.Controls.Add((Control) this._btnClose);
    this.Controls.Add((Control) this._btnFindNext);
    this.Controls.Add((Control) this._labelFindWhat);
    this.Controls.Add((Control) this._checkBoxWholeWord);
    this.Controls.Add((Control) this._checkBoxMathCase);
    this.MinimumSize = new Size(452, 142);
    this.Name = nameof (FindByIdForm);
    this.Text = "Поиск по идентификатору";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
