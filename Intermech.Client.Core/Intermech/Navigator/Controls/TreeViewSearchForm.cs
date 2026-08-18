
// Type: Intermech.Navigator.Controls.TreeViewSearchForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Форма для поиска по дереву навигатора</summary>
public sealed class TreeViewSearchForm : Form
{
  private const int HistorySize = 25;
  private static TreeViewSearchForm _treeViewSearchForm;
  private NavigatorTreeView _navigatorTreeView;
  private volatile Thread _thread;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBox _comboBox;
  private CheckBox _matchCaseCheckBox;
  private CheckBox _findInNodeCheckBox;
  private CheckBox _findInClassifiersCheckBox;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _findNextButton;
  private TableLayoutPanel tableLayoutPanel2;

  public static void ShowFor(NavigatorTreeView navigatorTreeView)
  {
    if (navigatorTreeView == null)
      throw new ArgumentNullException(nameof (navigatorTreeView));
    if (TreeViewSearchForm._treeViewSearchForm != null)
      TreeViewSearchForm._treeViewSearchForm.Close();
    TreeViewSearchForm._treeViewSearchForm = new TreeViewSearchForm();
    TreeViewSearchForm._treeViewSearchForm.NavigatorTreeView = navigatorTreeView;
    IMainFormUpdate mainFormUpdate = ServiceLocator.Get<IMainFormUpdate>();
    TreeViewSearchForm._treeViewSearchForm.Show((IWin32Window) mainFormUpdate.MainForm);
  }

  private TreeViewSearchForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeView NavigatorTreeView
  {
    get => this._navigatorTreeView;
    set
    {
      if (this._navigatorTreeView == value)
        return;
      if (this._navigatorTreeView != null)
        this._navigatorTreeView.Disposed -= new EventHandler(this.NavigatorTreeView_Disposed);
      this._navigatorTreeView = value;
      if (this._navigatorTreeView != null)
        this._navigatorTreeView.Disposed += new EventHandler(this.NavigatorTreeView_Disposed);
      this.UpdateControls();
    }
  }

  public TreeViewSearchForm.Memento GetMemento()
  {
    return new TreeViewSearchForm.Memento()
    {
      FindInClassifiers = this._findInClassifiersCheckBox.Checked,
      FindInNode = this._findInNodeCheckBox.Checked,
      History = this._comboBox.Items.Cast<string>().ToArray<string>(),
      MatchCase = this._matchCaseCheckBox.Checked
    };
  }

  public void SetMemento(TreeViewSearchForm.Memento memento)
  {
    if (memento == null)
      throw new ArgumentNullException(nameof (memento));
    if (memento.History != null)
    {
      this._comboBox.BeginUpdate();
      try
      {
        this._comboBox.Items.Clear();
        this._comboBox.Items.AddRange((object[]) memento.History);
      }
      finally
      {
        this._comboBox.EndUpdate();
      }
    }
    if (this._comboBox.Items.Count > 0)
      this._comboBox.SelectedIndex = 0;
    this._matchCaseCheckBox.Checked = memento.MatchCase;
    this._findInNodeCheckBox.Checked = memento.FindInNode;
    this._findInClassifiersCheckBox.Checked = memento.FindInClassifiers;
  }

  private void TreeViewSearchForm_Load(object sender, EventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    FormStorage.LoadLayout((Control) this, (IDictionary) hashtable);
    if (!hashtable.ContainsKey((object) "Memento"))
      return;
    this.SetMemento((TreeViewSearchForm.Memento) hashtable[(object) "Memento"]);
  }

  private void TreeViewSearchForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this._thread = (Thread) null;
    FormStorage.SaveLayout((Control) this, (IDictionary) new Hashtable()
    {
      {
        (object) "Memento",
        (object) this.GetMemento()
      }
    });
  }

  private void ComboBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Return && this._findNextButton.Enabled)
      this.FindNext();
    if (e.KeyCode != Keys.Escape)
      return;
    this.Close();
  }

  private void ComboBox_TextChanged(object sender, EventArgs e) => this.UpdateControls();

  private void FindNextButton_Click(object sender, EventArgs e) => this.FindNext();

  private void CancelButton_Click(object sender, EventArgs e) => this.Close();

  private void NavigatorTreeView_Disposed(object sender, EventArgs e) => this.Close();

  private void UpdateControls()
  {
    this._findNextButton.Enabled = !string.IsNullOrEmpty(this._comboBox.Text) && this._navigatorTreeView != null && this._navigatorTreeView.FocusedNode != null && this._thread == null;
  }

  private void FindNext()
  {
    this._comboBox.TextChanged -= new EventHandler(this.ComboBox_TextChanged);
    this._comboBox.BeginUpdate();
    try
    {
      string text = this._comboBox.Text;
      this._comboBox.Items.Remove((object) text);
      this._comboBox.Items.Insert(0, (object) text);
      this._comboBox.Text = text;
      if (this._comboBox.Items.Count > 25)
        this._comboBox.Items.RemoveAt(this._comboBox.Items.Count - 1);
    }
    finally
    {
      this._comboBox.EndUpdate();
      this._comboBox.TextChanged += new EventHandler(this.ComboBox_TextChanged);
    }
    this._thread = new Thread(new ThreadStart(this.ThreadMethod));
    this._thread.IsBackground = true;
    this._thread.Name = "SearchThread_Thread";
    this._thread.Start();
    this.UpdateControls();
  }

  private void ThreadMethod()
  {
    try
    {
      string findWhat = string.Empty;
      bool matchCase = false;
      this.Invoke((Delegate) (() =>
      {
        findWhat = this._comboBox.Text;
        matchCase = this._matchCaseCheckBox.Checked;
      }));
      foreach (NavigatorTreeNode nextNode in this.GetNextNodes(this._navigatorTreeView.FocusedNode, this._findInNodeCheckBox.Checked, this._findInClassifiersCheckBox.Checked))
      {
        if (this._thread == null)
          return;
        if (nextNode.ContainsText(findWhat, matchCase))
        {
          this.ShowNode(nextNode);
          this._thread = (Thread) null;
          this.Invoke((Delegate) (() => this.UpdateControls()));
          return;
        }
      }
      this.Invoke((Delegate) (() =>
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_531"), MessageDialogs.msgInformation, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this._thread = (Thread) null;
        this.UpdateControls();
      }));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private IEnumerable<NavigatorTreeNode> GetNextNodes(
    NavigatorTreeNode node,
    bool findInNode,
    bool findInClassifiers)
  {
    if (findInClassifiers || !findInClassifiers && !this.IsClassifierNode(node))
    {
      foreach (NavigatorTreeNode descendant in node.GetDescendants(true, (Predicate<NavigatorTreeNode>) (o =>
      {
        if (findInClassifiers)
          return true;
        return !findInClassifiers && !this.IsClassifierNode(o);
      })))
        yield return descendant;
    }
    if (!findInNode)
    {
      NavigatorTreeNode ancestorNextSibling = node.GetNextSiblingOrAncestorNextSibling();
      if (ancestorNextSibling != null)
      {
        foreach (NavigatorTreeNode nextNode in ancestorNextSibling.GetAllNextAndSelf(true, (Predicate<NavigatorTreeNode>) (o =>
        {
          if (findInClassifiers)
            return true;
          return !findInClassifiers && !this.IsClassifierNode(o);
        })))
          yield return nextNode;
      }
    }
  }

  private bool IsClassifierNode(NavigatorTreeNode node)
  {
    if (node.NodeID == null)
      return false;
    int num = node.NodeID.CategoryID == Intermech.Navigator.Consts.CategorySelectionsNode ? 1 : (node.NodeID.CategoryID == Intermech.Navigator.Consts.CategoryClassifiersNode ? 1 : 0);
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545");
    bool flag = node.NodeID.TypeID == objectTypeId || MetaDataHelper.IsObjectTypeChildOf(node.NodeID.TypeID, objectTypeId);
    return ((num != 0 ? 1 : (node.NodeID is SelectionNodeID ? 1 : 0)) & (flag ? 1 : 0)) != 0;
  }

  private void ShowNode(NavigatorTreeNode node)
  {
    this._navigatorTreeView.Invoke((Delegate) (() =>
    {
      foreach (NavigatorTreeNode navigatorTreeNode in node.GetAncestors().Reverse<NavigatorTreeNode>())
      {
        navigatorTreeNode.Handle.EnsureVisible();
        navigatorTreeNode.Handle.Expand();
      }
      this._navigatorTreeView.FocusedNode = node;
    }));
  }

  private void TreeViewSearchForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.Close();
  }

  private void TreeViewSearchForm_Shown(object sender, EventArgs e)
  {
    this._comboBox.Focus();
    this._comboBox.SelectAll();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TreeViewSearchForm));
    this._comboBox = new ComboBox();
    this._matchCaseCheckBox = new CheckBox();
    this._findInNodeCheckBox = new CheckBox();
    this._findInClassifiersCheckBox = new CheckBox();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._cancelButton = new Button();
    this._findNextButton = new Button();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._comboBox, "_comboBox");
    this._comboBox.Name = "_comboBox";
    this._comboBox.TextChanged += new EventHandler(this.ComboBox_TextChanged);
    this._comboBox.KeyDown += new KeyEventHandler(this.ComboBox_KeyDown);
    componentResourceManager.ApplyResources((object) this._matchCaseCheckBox, "_matchCaseCheckBox");
    this._matchCaseCheckBox.Name = "_matchCaseCheckBox";
    this._matchCaseCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._findInNodeCheckBox, "_findInNodeCheckBox");
    this._findInNodeCheckBox.Name = "_findInNodeCheckBox";
    this._findInNodeCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._findInClassifiersCheckBox, "_findInClassifiersCheckBox");
    this._findInClassifiersCheckBox.Name = "_findInClassifiersCheckBox";
    this._findInClassifiersCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._findNextButton);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._findNextButton, "_findNextButton");
    this._findNextButton.Name = "_findNextButton";
    this._findNextButton.UseVisualStyleBackColor = true;
    this._findNextButton.Click += new EventHandler(this.FindNextButton_Click);
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel2, "tableLayoutPanel2");
    this.tableLayoutPanel2.Controls.Add((Control) this._comboBox, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this._findInClassifiersCheckBox, 0, 3);
    this.tableLayoutPanel2.Controls.Add((Control) this._matchCaseCheckBox, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this._findInNodeCheckBox, 0, 2);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.AcceptButton = (IButtonControl) this._findNextButton;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this._cancelButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TreeViewSearchForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.TreeViewSearchForm_FormClosed);
    this.Load += new EventHandler(this.TreeViewSearchForm_Load);
    this.Shown += new EventHandler(this.TreeViewSearchForm_Shown);
    this.KeyDown += new KeyEventHandler(this.TreeViewSearchForm_KeyDown);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.ResumeLayout(false);
  }

  [Serializable]
  public sealed class Memento
  {
    public string[] History { get; set; }

    public bool MatchCase { get; set; }

    public bool FindInNode { get; set; }

    public bool FindInClassifiers { get; set; }
  }
}
