// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElementListSettingsForm
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal class ElementListSettingsForm : Form
{
  private bool _isBatchMode;
  /// <summary>Текущая схема</summary>
  private string _currentScheme;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBox1;
  private Panel panel1;
  private Button bCancel;
  private Button bOK;
  private SplitContainer splitContainer1;
  private Button bAddEL;
  private Button bDeleteEL;
  private TreeView tvListElements;
  private TreeView tvSchemes;
  private Splitter splitter1;
  private ListView lvInstances;
  private ColumnHeader columnHeader1;
  private ToolTip toolTip1;

  public ElementListSettingsForm() => this.InitializeComponent();

  /// <summary>
  ///  Список: Обозначение схемы, список исполнений: ид.версии, заголовок, обозначение, наименование
  /// </summary>
  /// <param name="nodes"></param>
  public void LoadData(
    Dictionary<string, List<Tuple<long, string, string, string>>> nodes)
  {
    this.CreatedElementLists = new Dictionary<string, List<CreatedElementList>>();
    this.tvSchemes.Nodes[0].Nodes.Clear();
    foreach (KeyValuePair<string, List<Tuple<long, string, string, string>>> node in nodes)
    {
      this.tvSchemes.Nodes[0].Nodes.Add(node.Key).Tag = (object) node.Value;
      this.CreatedElementLists.Add(node.Key, new List<CreatedElementList>());
    }
    this.tvSchemes.Nodes[0].ExpandAll();
    this.SetNodeData(this.tvSchemes.Nodes[0].Nodes[0]);
    this.RefreshButtons();
  }

  /// <summary>Установка данных в контролы при выборе схемы</summary>
  /// <param name="node"></param>
  private void SetNodeData(TreeNode node)
  {
    this._currentScheme = string.Empty;
    this.lvInstances.Items.Clear();
    this.tvListElements.Nodes[0].Nodes.Clear();
    if (node.Tag == null)
      return;
    this._currentScheme = node.Text;
    foreach (Tuple<long, string, string, string> tuple in (List<Tuple<long, string, string, string>>) node.Tag)
      this.lvInstances.Items.Add(new ListViewItem(tuple.Item2)
      {
        Tag = (object) tuple
      });
    this.tvListElements.Nodes[0].Nodes.Clear();
    foreach (CreatedElementList el in this._eListsOnCurrentScheme)
      this.AddElementListToTree(el, false);
    this.tvListElements.Nodes[0].ExpandAll();
  }

  private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.SetNodeData(e.Node);
  }

  private void treeView2_AfterSelect(object sender, TreeViewEventArgs e) => this.RefreshButtons();

  private void bAddEL_Click(object sender, EventArgs e)
  {
    List<Tuple<long, string, string, string>> asms = new List<Tuple<long, string, string, string>>();
    foreach (ListViewItem listViewItem in this.lvInstances.Items)
    {
      Tuple<long, string, string, string> tag = (Tuple<long, string, string, string>) listViewItem.Tag;
      if (!this._eListsOnCurrentScheme.Exists((Predicate<CreatedElementList>) (x => x.Assemblies.Exists((Predicate<Tuple<long, string, string, string>>) (y => y.Item1 == tag.Item1)))))
        asms.Add(tag);
    }
    if (asms.Count == 0)
    {
      int num = (int) MessageBox.Show("Отсутсвуют исполнения, не задействованные в других перечнях элементов по текущей схеме!");
    }
    else
    {
      Guid elementListType = ElectricalTypesHelper.GetElementListType(ElectricalTypesHelper.GetSuffix(this._currentScheme));
      if (elementListType == Guid.Empty && !ElectricalTypesHelper.SelectElementListType(ref elementListType, (string) null, (string) null))
        return;
      using (ElementListForm elementListForm = new ElementListForm(MetaDataHelper.GetObjectTypeID(elementListType)))
      {
        elementListForm.LoadData(asms);
        if (this._isBatchMode)
          elementListForm.RefreshDesignation();
        if (!this._isBatchMode && elementListForm.ShowDialog() != DialogResult.OK)
          return;
        CreatedElementList elementList = elementListForm.ElementList;
        this._eListsOnCurrentScheme.Add(elementList);
        this.AddElementListToTree(elementList, true);
      }
    }
  }

  private void AddElementListToTree(CreatedElementList el, bool expand)
  {
    TreeNode treeNode = this.tvListElements.Nodes[0].Nodes.Add(el.ToString());
    treeNode.Tag = (object) el.Designation;
    foreach (Tuple<long, string, string, string> assembly in el.Assemblies)
      treeNode.Nodes.Add(assembly.Item2);
    if (!expand)
      return;
    treeNode.Expand();
  }

  private void RefreshButtons()
  {
    this.bDeleteEL.Enabled = this.tvListElements.SelectedNode != null && this.tvListElements.SelectedNode.Tag != null;
  }

  private void ElementListSettingsForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ElementListSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Перечни элементов на текущую схему</summary>
  private List<CreatedElementList> _eListsOnCurrentScheme
  {
    get
    {
      return !string.IsNullOrEmpty(this._currentScheme) ? this.CreatedElementLists[this._currentScheme] : (List<CreatedElementList>) null;
    }
  }

  /// <summary>Список ПЭ</summary>
  public Dictionary<string, List<CreatedElementList>> CreatedElementLists { get; private set; }

  /// <summary>
  /// Установить/снять пакетный режим (без показа диалога пользователю)
  /// </summary>
  public bool IsBatchMode
  {
    get => this._isBatchMode;
    set => this.SetBatchMode(value);
  }

  private void SetBatchMode(bool isBatchModeOn)
  {
    if (this._isBatchMode == isBatchModeOn)
      return;
    this._isBatchMode = isBatchModeOn;
    if (!this._isBatchMode)
      return;
    this.bAddEL_Click((object) this, EventArgs.Empty);
  }

  private void bDeleteEL_Click(object sender, EventArgs e)
  {
    string designation = (string) this.tvListElements.SelectedNode.Tag;
    this._eListsOnCurrentScheme.Remove(this._eListsOnCurrentScheme.Find((Predicate<CreatedElementList>) (x => x.Designation.Equals(designation))));
    this.tvListElements.Nodes[0].Nodes.Remove(this.tvListElements.SelectedNode);
    this.RefreshButtons();
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
    TreeNode treeNode1 = new TreeNode("Создаваемые перечни элементов");
    TreeNode treeNode2 = new TreeNode("Схемы проекта");
    this.groupBox1 = new GroupBox();
    this.lvInstances = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.panel1 = new Panel();
    this.splitContainer1 = new SplitContainer();
    this.tvListElements = new TreeView();
    this.bAddEL = new Button();
    this.bDeleteEL = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.tvSchemes = new TreeView();
    this.splitter1 = new Splitter();
    this.toolTip1 = new ToolTip(this.components);
    this.groupBox1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.lvInstances);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(525, 188);
    this.groupBox1.TabIndex = 2;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Исполнения";
    this.lvInstances.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvInstances.Dock = DockStyle.Fill;
    this.lvInstances.Location = new Point(3, 16 /*0x10*/);
    this.lvInstances.Name = "lvInstances";
    this.lvInstances.Size = new Size(519, 169);
    this.lvInstances.TabIndex = 0;
    this.lvInstances.UseCompatibleStateImageBehavior = false;
    this.lvInstances.View = View.Details;
    this.columnHeader1.Text = "Заголовок";
    this.columnHeader1.Width = 513;
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Controls.Add((Control) this.splitContainer1);
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOK);
    this.panel1.Location = new Point(199, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(532, 485);
    this.panel1.TabIndex = 3;
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(3, 3);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.tvListElements);
    this.splitContainer1.Panel2.Controls.Add((Control) this.bAddEL);
    this.splitContainer1.Panel2.Controls.Add((Control) this.bDeleteEL);
    this.splitContainer1.Size = new Size(525, 422);
    this.splitContainer1.SplitterDistance = 188;
    this.splitContainer1.TabIndex = 5;
    this.tvListElements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tvListElements.Location = new Point(3, 41);
    this.tvListElements.Name = "tvListElements";
    treeNode1.Name = "Node0";
    treeNode1.Text = "Создаваемые перечни элементов";
    this.tvListElements.Nodes.AddRange(new TreeNode[1]
    {
      treeNode1
    });
    this.tvListElements.Size = new Size(519, 186);
    this.tvListElements.TabIndex = 3;
    this.tvListElements.AfterSelect += new TreeViewEventHandler(this.treeView2_AfterSelect);
    this.bAddEL.Image = (Image) Resources.document_add;
    this.bAddEL.Location = new Point(16 /*0x10*/, 8);
    this.bAddEL.Name = "bAddEL";
    this.bAddEL.Size = new Size(27, 27);
    this.bAddEL.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.bAddEL, "Новый перечень элементов");
    this.bAddEL.UseVisualStyleBackColor = true;
    this.bAddEL.Click += new EventHandler(this.bAddEL_Click);
    this.bDeleteEL.Image = (Image) Resources.document_delete;
    this.bDeleteEL.Location = new Point(49, 8);
    this.bDeleteEL.Name = "bDeleteEL";
    this.bDeleteEL.Size = new Size(27, 27);
    this.bDeleteEL.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.bDeleteEL, "Удалить из списка создаваемых перечень элементов");
    this.bDeleteEL.UseVisualStyleBackColor = true;
    this.bDeleteEL.Click += new EventHandler(this.bDeleteEL_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(398, 441);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(271, 441);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 3;
    this.bOK.Text = "Создать";
    this.bOK.UseVisualStyleBackColor = true;
    this.tvSchemes.Dock = DockStyle.Left;
    this.tvSchemes.Location = new Point(0, 0);
    this.tvSchemes.Name = "tvSchemes";
    treeNode2.Name = "Node0";
    treeNode2.Text = "Схемы проекта";
    this.tvSchemes.Nodes.AddRange(new TreeNode[1]
    {
      treeNode2
    });
    this.tvSchemes.Size = new Size(193, 485);
    this.tvSchemes.TabIndex = 4;
    this.tvSchemes.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
    this.splitter1.Location = new Point(193, 0);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(3, 485);
    this.splitter1.TabIndex = 5;
    this.splitter1.TabStop = false;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(730, 485);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.tvSchemes);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(640, 370);
    this.Name = nameof (ElementListSettingsForm);
    this.Text = "Создание перечней элементов на схемы";
    this.FormClosing += new FormClosingEventHandler(this.ElementListSettingsForm_FormClosing);
    this.Load += new EventHandler(this.ElementListSettingsForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
