// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorFindForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal class DatabaseConfiguratorFindForm : Form
{
  private static readonly int MaxHistoryDropDownCount = 25;
  private Control ownerControl;
  private bool inProgress;
  private bool stopFlag;
  private SearchOptions searchOptions = new SearchOptions();
  private IContainer components;
  private CheckBox cbInNode;
  private Button bCancel;
  private Button bOK;
  private CheckBox cbEnableRegister;
  private Label label1;
  private ComboBox tbSearchString;

  public bool InProgress => this.inProgress;

  public DatabaseConfiguratorFindForm() => this.InitializeComponent();

  public DatabaseConfiguratorFindForm(Control ownerControl)
    : this()
  {
    this.ownerControl = ownerControl;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.ownerControl == null || this.inProgress)
      return;
    this.CollectSearchOptions();
    bool flag = false;
    TreeNode resNode = (TreeNode) null;
    try
    {
      this.SetInProgress(true);
      this.stopFlag = false;
      flag = this.FindNode(this.searchOptions, this.ownerControl, out resNode);
    }
    catch
    {
    }
    finally
    {
      this.SetInProgress(false);
    }
    if (this.stopFlag)
      return;
    if (flag && resNode != null && this.ownerControl is DatabaseConfiguratorControl)
      ((DatabaseConfiguratorControl) this.ownerControl).DatabaseConfiguratorTreeView.SelectedNode = resNode;
    else
      IMMessageBox.ShowEx(LocalizationHolder.rm.GetString("DatabaseConfigurator_Find"), LocalizationHolder.rm.GetString("DatabaseConfigurator_NotFound"), new IMMessageBoxButton[1]
      {
        new IMMessageBoxButton("OK", DialogResultAdv.OK)
      });
  }

  private bool FindNode(SearchOptions searchOptions, Control ownerControl, out TreeNode resNode)
  {
    resNode = (TreeNode) null;
    if (this.stopFlag)
      return false;
    bool node = false;
    bool checkStartNode = false;
    DatabaseConfiguratorControl configuratorControl = ownerControl is DatabaseConfiguratorControl ? ownerControl as DatabaseConfiguratorControl : throw new Exception("Объект не является объектом конфигуратора БД");
    TreeNode startNode = configuratorControl.DatabaseConfiguratorTreeView.SelectedNode;
    if (startNode == null)
    {
      if (searchOptions.ThisNodeOnly)
        return node;
      startNode = configuratorControl.DatabaseConfiguratorTreeView.Nodes.Count > 0 ? configuratorControl.DatabaseConfiguratorTreeView.Nodes[0] : (TreeNode) null;
      if (startNode == null)
        return node;
      checkStartNode = true;
    }
    TreeNode categoryNode = (TreeNode) null;
    if (searchOptions.ThisNodeOnly)
      categoryNode = this.FindCategoryNodeFor(startNode);
    resNode = this.FindNextNode(startNode, categoryNode, searchOptions, checkStartNode, false);
    if (resNode != null)
      node = true;
    return node;
  }

  private TreeNode FindCategoryNodeFor(TreeNode startNode)
  {
    TreeNode categoryNodeFor = startNode;
    while (categoryNodeFor.Parent != null)
      categoryNodeFor = categoryNodeFor.Parent;
    return categoryNodeFor;
  }

  private TreeNode FindNextNode(
    TreeNode startNode,
    TreeNode categoryNode,
    SearchOptions searchOptions,
    bool checkStartNode,
    bool upDirection)
  {
    if (this.stopFlag)
      return (TreeNode) null;
    if (checkStartNode && this.CheckConditions(startNode, searchOptions))
      return startNode;
    if (ClientConsts.IsFakeNode(startNode))
    {
      startNode.Expand();
      Application.DoEvents();
    }
    for (int index = 0; index < startNode.Nodes.Count; ++index)
    {
      if (this.stopFlag)
        return (TreeNode) null;
      TreeNode nextNode = this.FindNextNode(startNode.Nodes[index], categoryNode, searchOptions, true, true);
      if (nextNode != null)
        return nextNode;
    }
    if (upDirection)
      return (TreeNode) null;
    if (searchOptions.ThisNodeOnly && startNode == categoryNode)
      return (TreeNode) null;
    for (TreeNode nextNode1 = startNode.NextNode; nextNode1 != null; nextNode1 = nextNode1.NextNode)
    {
      if (this.stopFlag)
        return (TreeNode) null;
      TreeNode nextNode2 = this.FindNextNode(nextNode1, categoryNode, searchOptions, true, true);
      if (nextNode2 != null)
        return nextNode2;
    }
    for (TreeNode parent = startNode.Parent; parent != null; parent = parent.Parent)
    {
      if (this.stopFlag)
        return (TreeNode) null;
      if (searchOptions.ThisNodeOnly && parent == categoryNode)
        return (TreeNode) null;
      for (TreeNode nextNode3 = parent.NextNode; nextNode3 != null; nextNode3 = nextNode3.NextNode)
      {
        TreeNode nextNode4 = this.FindNextNode(nextNode3, categoryNode, searchOptions, true, true);
        if (nextNode4 != null)
          return nextNode4;
      }
    }
    return (TreeNode) null;
  }

  private bool CheckConditions(TreeNode node, SearchOptions searchOptions)
  {
    string str1 = node.Text.Replace('Ё', 'Е').Replace('ё', 'е');
    string str2 = searchOptions.Text.Replace('Ё', 'Е').Replace('ё', 'е');
    return !searchOptions.CaseSensitive ? str1.ToLower().Contains(str2.ToLower()) : str1.Contains(str2);
  }

  private void SetInProgress(bool p)
  {
    this.bOK.Enabled = !p;
    this.inProgress = p;
  }

  private void CollectSearchOptions()
  {
    this.searchOptions.CaseSensitive = this.cbEnableRegister.Checked;
    this.searchOptions.ThisNodeOnly = this.cbInNode.Checked;
    this.searchOptions.Text = this.tbSearchString.Text;
    if (this.searchOptions.History.Contains(this.searchOptions.Text))
      return;
    this.searchOptions.History.Insert(0, this.searchOptions.Text);
    if (this.searchOptions.History.Count > DatabaseConfiguratorFindForm.MaxHistoryDropDownCount)
      this.searchOptions.History.RemoveRange(DatabaseConfiguratorFindForm.MaxHistoryDropDownCount, this.searchOptions.History.Count - DatabaseConfiguratorFindForm.MaxHistoryDropDownCount);
    this.tbSearchString.Items.Clear();
    this.tbSearchString.Items.AddRange((object[]) this.searchOptions.History.ToArray());
  }

  public void Show(SearchOptions searchOptions)
  {
    if (this.InProgress)
    {
      this.BringToFront();
    }
    else
    {
      this.searchOptions = searchOptions;
      this.FillControls();
      this.ownerControl.Disposed += new EventHandler(this.OwnerControlDisposed);
      this.Show((IWin32Window) this.ownerControl);
    }
  }

  private void OwnerControlDisposed(object sender, EventArgs e)
  {
    if (this.inProgress)
      this.stopFlag = true;
    this.Close();
  }

  private void FillControls()
  {
    this.cbEnableRegister.Checked = this.searchOptions.CaseSensitive;
    this.cbInNode.Checked = this.searchOptions.ThisNodeOnly;
    this.tbSearchString.Text = this.searchOptions.Text;
    this.tbSearchString.Items.AddRange((object[]) this.searchOptions.History.ToArray());
  }

  private void DatabaseConfiguratorSearchForm_Load(object sender, EventArgs e)
  {
  }

  private void bCancel_Click(object sender, EventArgs e)
  {
    if (this.inProgress)
      this.stopFlag = true;
    else
      this.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DatabaseConfiguratorFindForm));
    this.cbInNode = new CheckBox();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.cbEnableRegister = new CheckBox();
    this.label1 = new Label();
    this.tbSearchString = new ComboBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cbInNode, "cbInNode");
    this.cbInNode.Checked = true;
    this.cbInNode.CheckState = CheckState.Checked;
    this.cbInNode.Name = "cbInNode";
    this.cbInNode.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    componentResourceManager.ApplyResources((object) this.cbEnableRegister, "cbEnableRegister");
    this.cbEnableRegister.Name = "cbEnableRegister";
    this.cbEnableRegister.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.tbSearchString, "tbSearchString");
    this.tbSearchString.Name = "tbSearchString";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.bCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.cbInNode);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.cbEnableRegister);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbSearchString);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DatabaseConfiguratorFindForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.DatabaseConfiguratorSearchForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
