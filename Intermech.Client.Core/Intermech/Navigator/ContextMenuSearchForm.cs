
// Type: Intermech.Navigator.ContextMenuSearchForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Форма для поиска по дереву навигатора</summary>
internal class ContextMenuSearchForm : Form
{
  /// <summary>Ссылка на дерево по которому производим поиск</summary>
  private TreeList contextMenuTree;
  /// <summary>имя комнады, которую ищем</summary>
  private string searchCommandName = string.Empty;
  /// <summary>искать с учётом регистра?</summary>
  private bool enableRegister;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private CheckBox cbEnableRegister;
  private Button bOK;
  private Button bCancel;
  private TextBox tbSearchString;
  private ComboBox cbDirection;
  private Label label2;

  /// <summary>Конструктор</summary>
  public ContextMenuSearchForm() => this.InitializeComponent();

  /// <summary>Метод для отображения формы на экране</summary>
  /// <param name="contextMenuTree">Ссылка на дерево, по которому производим поиск</param>
  public void ShowForm(TreeList contextMenuTree)
  {
    this.contextMenuTree = contextMenuTree;
    this.Show();
  }

  /// <summary>Нажали кнопку "Отмена"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bCancel_Click(object sender, EventArgs e)
  {
    this.SetEnabled(true);
    this.Close();
    this.Dispose();
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    this.SetEnabled(false);
    this.searchCommandName = this.tbSearchString.Text;
    this.enableRegister = this.cbEnableRegister.Checked;
    try
    {
      if (this.searchCommandName == string.Empty)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_530"), MessageDialogs.msgInformation, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.SetEnabled(true);
      }
      else
      {
        if (this.contextMenuTree.FocusedNode == null)
          return;
        TreeListNode currentNode = this.contextMenuTree.FocusedNode.Level == 0 ? this.contextMenuTree.FocusedNode.Nodes.FirstNode : this.contextMenuTree.FocusedNode;
        int id = currentNode.Id;
        TreeListNode nextNode;
        while (true)
        {
          nextNode = this.GetNextNode(currentNode, true);
          if (nextNode != null && id != nextNode.Id)
          {
            if (!this.CompareNode(nextNode, this.searchCommandName, this.enableRegister))
              currentNode = nextNode;
            else
              goto label_8;
          }
          else
            break;
        }
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_531"), MessageDialogs.msgInformation, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.SetEnabled(true);
        return;
label_8:
        this.contextMenuTree.FocusedNode = nextNode;
        this.SetEnabled(true);
      }
    }
    catch (Exception ex)
    {
      this.SetEnabled(true);
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>найти следующий узел</summary>
  /// <param name="currentNode"></param>
  /// <param name="checkChildren"></param>
  /// <returns></returns>
  private TreeListNode GetNextNode(TreeListNode currentNode, bool checkChildren)
  {
    if (checkChildren && currentNode.HasChildren)
    {
      currentNode.Expanded = true;
      return currentNode.Nodes[0];
    }
    TreeListNode parentNode = currentNode.ParentNode;
    int num = parentNode.Nodes.IndexOf(currentNode);
    if (parentNode.Nodes.LastNode != currentNode)
      return parentNode.Nodes[num + 1];
    return parentNode.Level == 0 ? parentNode.Nodes.FirstNode : this.GetNextNode(parentNode, false);
  }

  /// <summary>выделить узел в дереве</summary>
  /// <param name="node"></param>
  private void SelectNode(TreeListNode node) => this.contextMenuTree.FocusedNode = node;

  /// <summary>Находиться ли text в тексте node</summary>
  /// <param name="node">Узел</param>
  /// <param name="text">Искомый текст</param>
  /// <param name="enableRegister">Учитывать регистр</param>
  /// <returns>Да или нет</returns>
  private bool CompareNode(TreeListNode node, string text, bool enableRegister)
  {
    string lower = node[(object) 0].ToString();
    if (!enableRegister)
    {
      lower = lower.ToLower();
      text = text.ToLower();
    }
    return lower.IndexOf(text) >= 0;
  }

  private void SetEnabled(bool enable)
  {
    this.tbSearchString.Enabled = this.cbEnableRegister.Enabled = this.bOK.Enabled = enable;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextMenuSearchForm));
    this.label1 = new Label();
    this.cbEnableRegister = new CheckBox();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.tbSearchString = new TextBox();
    this.cbDirection = new ComboBox();
    this.label2 = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.cbEnableRegister, "cbEnableRegister");
    this.cbEnableRegister.Name = "cbEnableRegister";
    this.cbEnableRegister.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    componentResourceManager.ApplyResources((object) this.tbSearchString, "tbSearchString");
    this.tbSearchString.Name = "tbSearchString";
    componentResourceManager.ApplyResources((object) this.cbDirection, "cbDirection");
    this.cbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbDirection.FormattingEnabled = true;
    this.cbDirection.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("cbDirection.Items"),
      (object) componentResourceManager.GetString("cbDirection.Items1")
    });
    this.cbDirection.Name = "cbDirection";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.cbDirection);
    this.Controls.Add((Control) this.tbSearchString);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.cbEnableRegister);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ContextMenuSearchForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
