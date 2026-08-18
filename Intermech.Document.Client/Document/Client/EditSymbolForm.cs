// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.EditSymbolForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Document.Client;

public class EditSymbolForm : Form
{
  private bool lockGrid;
  private int editingRow = -1;
  private string openMessage = "";
  internal FormList formulae;
  internal int curFormIndex = -1;
  internal Formula curFormula;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelRight;
  private Splitter splitter1;
  private Panel panelLeft;
  private TreeView tree;
  private Splitter splitter2;
  private Panel panel1;
  private Panel panel2;
  private Button btnOK;
  private Button btnCancel;
  private Panel panel3;
  private Button btnAdd;
  private Button btnReplace;
  private Button btnDelete;
  private PictureBox pictCurrent;
  private Splitter splitter3;
  private Panel panel4;
  private PictureBox pictTotal;
  private Panel panel5;
  private Label label1;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private ImageList IL;
  private Panel panel6;
  private Label label2;
  private Timer timer;
  private Label label3;
  private Splitter splitter4;
  private iGrid grid;
  private Panel panel7;
  private ListBox lbSequence;
  private Button btnMoveFirst;
  private Button btnMoveLast;
  private Button btnMoveNext;
  private Button btnMovePrev;
  private ToolTip toolTip1;
  private iGCellStyle gridCol2CellStyle;
  private iGColHdrStyle gridCol2ColHdrStyle;

  public EditSymbolForm()
  {
    this.InitializeComponent();
    this.curFormIndex = -1;
    this.formulae = (FormList) null;
    this.curFormula = (Formula) null;
  }

  public void FillTree()
  {
    List<Page> docTemplates = TemplateHolderBase.Instance.docTemplates;
    List<DocumentSection> groups = TemplateHolderBase.Instance.groups;
    if (docTemplates.Count > 0)
    {
      TreeNode node1 = new TreeNode(LocalizationHolder.rm.GetString("Document.Client_129"));
      this.tree.Nodes.Add(node1);
      node1.ImageIndex = 1;
      node1.SelectedImageIndex = 1;
      node1.Tag = (object) null;
      for (int index = 0; index < docTemplates.Count; ++index)
      {
        Page page = docTemplates[index];
        if (page != null)
        {
          TreeNode node2 = new TreeNode($"{page.Id.ToUpper()} \"{page.Name}\"");
          node1.Nodes.Add(node2);
          node2.ImageIndex = 10;
          node2.SelectedImageIndex = 10;
          node2.Tag = (object) page;
        }
      }
    }
    EditSymbolForm.SameData data = new EditSymbolForm.SameData();
    List<TreeNode> treeNodeList = new List<TreeNode>();
    for (int index = 0; index < groups.Count; ++index)
    {
      TreeNode treeNode = new TreeNode(groups[index].Name);
      treeNodeList.Add(treeNode);
      this.tree.Nodes.Add(treeNode);
      treeNode.ImageIndex = 1;
      treeNode.SelectedImageIndex = 1;
      treeNode.Tag = (object) null;
      this.AddGroupChilds(treeNode, groups[index], data);
    }
    TreeNode node3 = new TreeNode(LocalizationHolder.rm.GetString("Document.Client_167"));
    this.tree.Nodes.Insert(0, node3);
    node3.ImageIndex = 1;
    node3.SelectedImageIndex = 1;
    node3.Tag = (object) null;
    foreach (SpecSymbol recentSpecSymbol in ImDocumentEditorConfig.RecentSpecSymbols)
    {
      if (this.FindNode(new Formula(recentSpecSymbol.Id))?.Tag is Page tag)
      {
        TreeNode node4 = new TreeNode($"{tag.Id.ToUpper()} \"{tag.Name}\"");
        node3.Nodes.Add(node4);
        node4.ImageIndex = 10;
        node4.SelectedImageIndex = 10;
        node4.Tag = (object) tag;
      }
    }
    this.tree.ExpandAll();
    if (data.SameIds.Count <= 0)
      return;
    string str = $"{"Найдены повторяющиеся идентификаторы формул " + Environment.NewLine + string.Join(",", (IEnumerable<string>) data.SameIds)}{Environment.NewLine}в библиотеках {Environment.NewLine}";
    List<string> values = new List<string>();
    foreach (string sameId in data.SameIds)
    {
      foreach (TreeNode root in treeNodeList)
      {
        TreeNode inChildNode = this.FindInChildNode(root, sameId);
        if (inChildNode != null)
        {
          inChildNode.BackColor = Color.Red;
          if (!values.Contains(root.Text))
            values.Add(root.Text);
        }
      }
    }
    this.openMessage = str + string.Join(",", (IEnumerable<string>) values);
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    if (!(this.openMessage != ""))
      return;
    int num = (int) MessageBox.Show(this.openMessage, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  public void AddGroupChilds(TreeNode root, DocumentSection s, EditSymbolForm.SameData data)
  {
    foreach (DocumentTreeNode node1 in s.Nodes)
    {
      if (node1 is Page page)
      {
        string upper = page.Id.ToUpper();
        if (data.Ids.Contains(upper))
          data.SameIds.Add(upper);
        else
          data.Ids.Add(upper);
        TreeNode node2 = new TreeNode($"{upper} \"{page.Name}\"");
        root.Nodes.Add(node2);
        node2.ImageIndex = 10;
        node2.SelectedImageIndex = 10;
        node2.Tag = (object) page;
      }
      else if (node1 is DocumentSection s1)
      {
        TreeNode treeNode = new TreeNode(s1.Name);
        root.Nodes.Add(treeNode);
        treeNode.ImageIndex = 1;
        treeNode.SelectedImageIndex = 1;
        treeNode.Tag = (object) null;
        this.AddGroupChilds(treeNode, s1, data);
      }
    }
  }

  private void InitRows()
  {
    this.curFormula.GetEditFields();
    this.grid.Rows.Count = this.curFormula.fields.Count;
    for (int index = 0; index < this.curFormula.fields.Count; ++index)
    {
      this.grid.Cells[index, 0].Value = (object) this.curFormula.fields[index].Name;
      if (index < this.curFormula.Parms.Count)
        this.grid.Cells[index, 1].Value = (object) this.curFormula.Parms[index];
      if (this.curFormula.fields[index].node is TextBoxElement node)
        this.grid.Cells[index, 2].Value = (object) TextStyleDlg.GetStyleDescription(node.CharFormat);
    }
  }

  private void tree_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this.lockGrid || !this.UpdateCurFormula(true))
      return;
    this.InitRows();
    this.DrawCurFormula();
    this.curFormIndex = -1;
  }

  private TreeNode FindInChildNode(Formula form, TreeNode root)
  {
    for (int index = 0; index < root.Nodes.Count; ++index)
    {
      TreeNode node = root.Nodes[index];
      if (node.Tag is Page tag && tag.Id.ToUpper() == form.Id.ToUpper())
        return node;
      if (node.Nodes.Count > 0)
      {
        TreeNode inChildNode = this.FindInChildNode(form, node);
        if (inChildNode != null)
          return inChildNode;
      }
    }
    return (TreeNode) null;
  }

  private TreeNode FindNode(Formula form)
  {
    for (int index = 0; index < this.tree.Nodes.Count; ++index)
    {
      if (this.tree.Nodes[index].Tag is Page tag && tag.Id.ToUpper() == form.Id.ToUpper())
        return this.tree.Nodes[index];
      if (this.tree.Nodes[index].Nodes.Count > 0)
      {
        TreeNode inChildNode = this.FindInChildNode(form, this.tree.Nodes[index]);
        if (inChildNode != null)
          return inChildNode;
      }
    }
    return (TreeNode) null;
  }

  private TreeNode FindInChildNode(TreeNode root, string id)
  {
    for (int index = 0; index < root.Nodes.Count; ++index)
    {
      TreeNode node = root.Nodes[index];
      if (node.Tag is Page tag && tag.Id.ToUpper() == id.ToUpper())
        return node;
      if (node.Nodes.Count > 0)
      {
        TreeNode inChildNode = this.FindInChildNode(node, id);
        if (inChildNode != null)
          return inChildNode;
      }
    }
    return (TreeNode) null;
  }

  private TreeNode FindInChildNode(TreeNode root)
  {
    for (int index = 0; index < root.Nodes.Count; ++index)
    {
      TreeNode node = root.Nodes[index];
      if (node.Tag is Page tag && tag.Id.ToUpper() == this.curFormula.Id.ToUpper())
        return node;
      if (node.Nodes.Count > 0)
      {
        TreeNode inChildNode = this.FindInChildNode(node);
        if (inChildNode != null)
          return inChildNode;
      }
    }
    return (TreeNode) null;
  }

  private TreeNode FindNodeForCurFormula()
  {
    if (this.curFormula.page == null)
      this.grid.Rows.Clear();
    for (int index = 0; index < this.tree.Nodes.Count; ++index)
    {
      if (this.tree.Nodes[index].Tag is Page tag && tag.Id.ToUpper() == this.curFormula.Id.ToUpper())
        return this.tree.Nodes[index];
      if (this.tree.Nodes[index].Nodes.Count > 0)
      {
        TreeNode inChildNode = this.FindInChildNode(this.tree.Nodes[index]);
        if (inChildNode != null)
          return inChildNode;
      }
    }
    return (TreeNode) null;
  }

  private Formula GetCurFormulaFromGrid()
  {
    if (this.tree.SelectedNode == null || this.tree.SelectedNode.Tag == null)
      return (Formula) null;
    if (!(this.tree.SelectedNode.Tag is Page tag))
      return (Formula) null;
    List<string> aParms = new List<string>();
    for (int rowIndex = 0; rowIndex < this.grid.Rows.Count; ++rowIndex)
    {
      if (this.grid.IsEditing && rowIndex == this.editingRow && this.grid.TextBox != null)
        aParms.Add(this.grid.TextBox.Text);
      else
        aParms.Add(this.grid.Cells[rowIndex, 1].Text);
    }
    return new Formula(tag.Id, aParms) { page = tag };
  }

  private Formula GetCurFormulaFromPage()
  {
    if (this.tree.SelectedNode == null || this.tree.SelectedNode.Tag == null)
      return (Formula) null;
    return !(this.tree.SelectedNode.Tag is Page tag) ? (Formula) null : new Formula(tag);
  }

  private bool UpdateCurFormula(bool fromPage)
  {
    Formula formula = fromPage ? this.GetCurFormulaFromPage() : this.GetCurFormulaFromGrid();
    if (formula == null || formula.Equals((object) this.curFormula))
      return false;
    this.curFormula = formula;
    return true;
  }

  private void grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    this.editingRow = -1;
    if (!this.UpdateCurFormula(false))
      return;
    this.DrawCurFormula();
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    if (this.curFormula == null)
      return;
    string str = this.curFormula.ToString();
    if (this.lbSequence.SelectedIndex < 0)
    {
      this.formulae.List.Add(this.curFormula);
      this.lbSequence.Items.Add((object) str);
    }
    else
    {
      this.formulae.List.Insert(this.lbSequence.SelectedIndex + 1, this.curFormula);
      this.lbSequence.Items.Insert(this.lbSequence.SelectedIndex + 1, (object) str);
    }
    this.AddToRecentUsedSymbols(new SpecSymbol(this.curFormula.Id));
    this.DrawFormList();
    this.EnableButtons();
    this.UpdateMoveButtons();
  }

  private void btnReplace_Click(object sender, EventArgs e)
  {
    if (this.lbSequence.SelectedIndex < 0)
      return;
    this.formulae[this.lbSequence.SelectedIndex] = this.curFormula;
    this.lbSequence.Items[this.lbSequence.SelectedIndex] = (object) this.curFormula.ToString();
    this.AddToRecentUsedSymbols(new SpecSymbol(this.curFormula.Id));
    this.DrawFormList();
    this.UpdateMoveButtons();
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.formulae.List.RemoveAt(selectedIndex);
    this.lbSequence.Items.RemoveAt(selectedIndex);
    if (selectedIndex < this.lbSequence.Items.Count)
      this.lbSequence.SelectedIndex = selectedIndex;
    else
      this.lbSequence.SelectedIndex = this.lbSequence.Items.Count - 1;
    this.DrawFormList();
    this.UpdateMoveButtons();
  }

  private void lbSequence_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex == this.curFormIndex)
      return;
    this.curFormIndex = selectedIndex;
    if (this.curFormIndex < 0)
      return;
    this.curFormula = new Formula(this.formulae[selectedIndex]);
    this.UpdateParms();
    this.DrawCurFormula();
    this.EnableButtons();
  }

  private void UpdateParms()
  {
    TreeNode nodeForCurFormula = this.FindNodeForCurFormula();
    this.lockGrid = true;
    try
    {
      if (nodeForCurFormula != null)
        this.tree.SelectedNode = nodeForCurFormula;
      this.InitRows();
      for (int index = 0; index < this.curFormula.Parms.Count; ++index)
      {
        if (index < this.grid.Rows.Count)
          this.grid.Cells[index, 1].Value = (object) this.curFormula.Parms[index];
      }
    }
    finally
    {
      this.lockGrid = false;
    }
  }

  private void EnableButtons()
  {
    this.btnDelete.Enabled = this.lbSequence.SelectedIndex >= 0;
    this.btnReplace.Enabled = this.lbSequence.SelectedIndex >= 0;
  }

  private void button2_Click(object sender, EventArgs e)
  {
    if (this.curFormula == null || !(this.curFormula.Id != ""))
      return;
    if (this.formulae.Count == 0)
      this.formulae.List.Add(this.curFormula);
    this.AddToRecentUsedSymbols(new SpecSymbol(this.curFormula.Id));
  }

  [DllImport("gdi32.dll")]
  private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

  [DllImport("gdi32.dll")]
  private static extern int DeleteEnhMetaFile(IntPtr hemf);

  private void DrawCurFormula()
  {
    this.curFormula.SetFormulaParms();
    this.curFormula.PerformHorzAligns((DocumentTreeNode) this.curFormula.page);
    this.curFormula.CalcCoords();
    this.curFormula.AdjustCoordsTo(new PointF(0.0f, 0.0f));
    this.pictCurrent.Image = (Image) TemplateHolderBase.Instance.GetMetafile(this.curFormula);
  }

  private void DrawFormList()
  {
    this.formulae.PerformCoords();
    this.pictTotal.Image = (Image) TemplateHolderBase.Instance.GetMetafile(this.formulae);
  }

  /// <summary>Primary execution method</summary>
  /// <param name="doc">Document which templates must be used</param>
  /// <param name="specText">string representation of formula</param>
  /// <param name="formList">list of Pages used in the resulting formula (to save)</param>
  public bool Execute(ImDocument doc, ref string specText, out List<Page> formList)
  {
    if (!(TemplateHolderBase.Instance is TemplateHolder))
      TemplateHolderBase.Instance = (TemplateHolderBase) new TemplateHolder();
    TemplateHolderBase.Instance.ReloadTemplates();
    if (doc != null)
      TemplateHolderBase.Instance.SetTemplatesForDoc(doc);
    formList = new List<Page>();
    bool flag = this.Execute(doc, new FormList(specText));
    if (flag)
    {
      specText = this.formulae.ToString();
      foreach (Formula formula in this.formulae)
        formList.Add(formula.page);
    }
    return flag;
  }

  /// <summary>Primary execution method</summary>
  /// <param name="doc">Document which templates must be used</param>
  /// <param name="formList">Блок формул</param>
  public bool Execute(ImDocument doc, FormList formList)
  {
    if (!(TemplateHolderBase.Instance is TemplateHolder))
      TemplateHolderBase.Instance = (TemplateHolderBase) new TemplateHolder();
    TemplateHolderBase.Instance.ReloadTemplates();
    if (doc != null)
      TemplateHolderBase.Instance.SetTemplatesForDoc(doc);
    this.FillTree();
    this.formulae = formList;
    this.formulae.UpdatePages(TemplateHolderBase.Instance.templates);
    foreach (Formula formula in this.formulae)
      this.lbSequence.Items.Add((object) formula.ToString());
    if (this.lbSequence.Items.Count > 0)
    {
      this.lbSequence.SelectedIndex = 0;
      this.curFormIndex = 0;
    }
    for (int index = this.formulae.Count - 1; index >= 0; --index)
    {
      TreeNode node = this.FindNode(this.formulae[index]);
      if (node == null)
        this.formulae.List.RemoveAt(index);
      else if (this.formulae[index].page == null)
        this.formulae[index].page = node.Tag as Page;
    }
    if (this.formulae.Count > 0)
    {
      this.curFormula = new Formula(this.formulae[this.curFormIndex]);
      this.UpdateParms();
      this.DrawCurFormula();
      this.DrawFormList();
    }
    else
      this.curFormIndex = -1;
    INamedImageList service = (INamedImageList) DocumentEditorPlugin.Instance.ServiceProvider.GetService(typeof (INamedImageList));
    if (service != null)
    {
      int index1 = service.ImageIndex("imgMoveFirst");
      if (index1 >= 0)
        this.btnMoveFirst.Image = service.ImageList.Images[index1];
      int index2 = service.ImageIndex("imgMoveUp");
      if (index2 >= 0)
        this.btnMovePrev.Image = service.ImageList.Images[index2];
      int index3 = service.ImageIndex("imgMoveDown");
      if (index3 >= 0)
        this.btnMoveNext.Image = service.ImageList.Images[index3];
      int index4 = service.ImageIndex("imgMoveLast");
      if (index4 >= 0)
        this.btnMoveLast.Image = service.ImageList.Images[index4];
    }
    this.EnableButtons();
    this.UpdateMoveButtons();
    return this.ShowDialog() == DialogResult.OK;
  }

  /// <summary>получить коллекцию формула -- изображение формулы</summary>
  /// <param name="text">формула</param>
  /// <param name="font">используемый шрифт</param>
  /// <param name="textColor">цвет формулы</param>
  /// <param name="backgroundColor">цвет подложки</param>
  /// <param name="totalSize"></param>
  /// <returns>есть ли формулы</returns>
  public Image CreateFormulaImages(
    string text,
    Font font,
    Color textColor,
    Color backgroundColor,
    out SizeF totalSize)
  {
    if (!(TemplateHolderBase.Instance is TemplateHolder))
      TemplateHolderBase.Instance = (TemplateHolderBase) new TemplateHolder();
    TemplateHolderBase.Instance.ReloadTemplates();
    this.FillTree();
    this.formulae = new FormList(text);
    this.formulae.UpdatePages(TemplateHolderBase.Instance.templates);
    foreach (Formula formula in this.formulae)
      this.lbSequence.Items.Add((object) formula.ToString());
    if (this.lbSequence.Items.Count > 0)
    {
      this.lbSequence.SelectedIndex = 0;
      this.curFormIndex = 0;
    }
    for (int index = this.formulae.Count - 1; index >= 0; --index)
    {
      TreeNode node = this.FindNode(this.formulae[index]);
      if (node == null)
        this.formulae.List.RemoveAt(index);
      else if (this.formulae[index].page == null)
        this.formulae[index].page = node.Tag as Page;
    }
    for (int index = this.formulae.Count - 1; index >= 0; --index)
      this.formulae[index].page.AutoSize = true;
    this.formulae.PerformCoords();
    totalSize = this.formulae.totalSize;
    return (Image) TemplateHolderBase.Instance.GetMetafileRedNote(this.formulae, out totalSize);
  }

  private void grid_TextBoxTextChanged(object sender, iGTextBoxTextChangedEventArgs e)
  {
    if (e.ColIndex > 1)
      return;
    if (this.UpdateCurFormula(false))
      this.DrawCurFormula();
    if (this.curFormIndex >= this.lbSequence.Items.Count)
      this.curFormIndex = this.lbSequence.Items.Count - 1;
    if (this.curFormIndex < 0)
      return;
    this.lbSequence.Items[this.curFormIndex] = (object) this.curFormula.ToString();
    this.formulae.List[this.curFormIndex] = this.curFormula;
    this.DrawFormList();
  }

  private void timer_Tick(object sender, EventArgs e)
  {
  }

  private void grid_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    this.editingRow = e.RowIndex;
  }

  private void grid_CancelEdit(object sender, iGCancelEditEventArgs e) => this.editingRow = -1;

  /// <summary>Добавить спецсимвол в список недавно использованных.</summary>
  /// <param name="specSymbol">Спецсимвол.</param>
  private void AddToRecentUsedSymbols(SpecSymbol specSymbol)
  {
    if (ImDocumentEditorConfig.RecentSpecSymbols.Contains(specSymbol))
    {
      ImDocumentEditorConfig.RecentSpecSymbols.Remove(specSymbol);
      ImDocumentEditorConfig.RecentSpecSymbols.Insert(0, specSymbol);
    }
    else
    {
      if (ImDocumentEditorConfig.RecentSpecSymbols.Count == ImDocumentEditorConfig.RecentSymbolsMaxCount)
        ImDocumentEditorConfig.RecentSpecSymbols.RemoveAt(ImDocumentEditorConfig.RecentSymbolsMaxCount - 1);
      ImDocumentEditorConfig.RecentSpecSymbols.Insert(0, specSymbol);
    }
  }

  private void EditSymbolForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    if (this.formulae.Count <= 0 || this.lbSequence.Items.Count <= 0)
      return;
    this.lbSequence.SelectedIndex = 0;
    this.curFormIndex = 0;
    this.curFormula = new Formula(this.formulae[this.curFormIndex]);
    this.UpdateParms();
    this.DrawCurFormula();
    this.DrawFormList();
  }

  private void EditSymbolForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void btnMoveFirst_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex < 0)
      return;
    Formula formula = this.formulae.List[selectedIndex];
    string str = Convert.ToString(this.lbSequence.Items[selectedIndex]);
    this.lbSequence.SuspendLayout();
    try
    {
      this.formulae.List.RemoveAt(selectedIndex);
      this.lbSequence.Items.RemoveAt(selectedIndex);
      this.formulae.List.Insert(0, formula);
      this.lbSequence.Items.Insert(0, (object) str);
    }
    finally
    {
      this.lbSequence.ResumeLayout();
    }
    this.lbSequence.SelectedIndex = 0;
  }

  private void btnMovePrev_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex < 0)
      return;
    Formula formula = this.formulae.List[selectedIndex];
    string str = Convert.ToString(this.lbSequence.Items[selectedIndex]);
    this.lbSequence.SuspendLayout();
    try
    {
      this.formulae.List.RemoveAt(selectedIndex);
      this.lbSequence.Items.RemoveAt(selectedIndex);
      this.formulae.List.Insert(selectedIndex - 1, formula);
      this.lbSequence.Items.Insert(selectedIndex - 1, (object) str);
    }
    finally
    {
      this.lbSequence.ResumeLayout();
    }
    this.lbSequence.SelectedIndex = selectedIndex - 1;
  }

  private void btnMoveNext_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex < 0)
      return;
    Formula formula = this.formulae.List[selectedIndex];
    string str = Convert.ToString(this.lbSequence.Items[selectedIndex]);
    this.lbSequence.SuspendLayout();
    try
    {
      this.formulae.List.RemoveAt(selectedIndex);
      this.lbSequence.Items.RemoveAt(selectedIndex);
      this.formulae.List.Insert(selectedIndex + 1, formula);
      this.lbSequence.Items.Insert(selectedIndex + 1, (object) str);
    }
    finally
    {
      this.lbSequence.ResumeLayout();
    }
    this.lbSequence.SelectedIndex = selectedIndex + 1;
  }

  private void btnMoveLast_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    if (selectedIndex < 0)
      return;
    Formula formula = this.formulae.List[selectedIndex];
    string str = Convert.ToString(this.lbSequence.Items[selectedIndex]);
    this.lbSequence.SuspendLayout();
    try
    {
      this.formulae.List.RemoveAt(selectedIndex);
      this.lbSequence.Items.RemoveAt(selectedIndex);
      this.formulae.List.Add(formula);
      this.lbSequence.Items.Add((object) str);
    }
    finally
    {
      this.lbSequence.ResumeLayout();
    }
    this.lbSequence.SelectedIndex = this.formulae.List.Count - 1;
  }

  private void UpdateMoveButtons()
  {
    int selectedIndex = this.lbSequence.SelectedIndex;
    int count = this.lbSequence.Items.Count;
    if (this.curFormula == null || count <= 1)
    {
      this.btnMoveFirst.Enabled = false;
      this.btnMovePrev.Enabled = false;
      this.btnMoveNext.Enabled = false;
      this.btnMoveLast.Enabled = false;
    }
    else
    {
      this.btnMoveFirst.Enabled = selectedIndex > 0;
      this.btnMovePrev.Enabled = selectedIndex > 0;
      this.btnMoveNext.Enabled = selectedIndex < count - 1;
      this.btnMoveLast.Enabled = selectedIndex < count - 1;
    }
  }

  private void lbSequence_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateMoveButtons();
  }

  private void grid_EllipsisBtnClick(object sender, iGEllipsisBtnClickEventArgs e)
  {
    if (!(this.curFormula.fields[e.RowIndex].node is TextData node))
      return;
    CharFormat charFormat1 = node.CharFormat;
    CharFormat charFormat2 = charFormat1?.Clone();
    if (charFormat2 == null || TextStyleDlg.Execute((Form) this, charFormat2) != DialogResult.OK)
      return;
    if (!object.Equals((object) charFormat1, (object) charFormat2))
    {
      if (!Formula.CellsCharFormat.ContainsKey(node))
        Formula.CellsCharFormat.Add(node, node.CharFormat);
      CharFormat charFormat3 = node.CharFormat.Clone();
      charFormat3.BoldItalic = charFormat2.BoldItalic;
      charFormat3.CharStyle = charFormat2.CharStyle;
      node.CharFormat = charFormat3;
      this.grid.Cells[e.RowIndex, 2].Value = (object) TextStyleDlg.GetStyleDescription(charFormat2);
      this.DrawCurFormula();
    }
    this.DrawCurFormula();
  }

  private void grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    if (e.ColIndex != 2)
      return;
    e.NewValue = this.grid.Cells[e.RowIndex, e.ColIndex].Value;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditSymbolForm));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.gridCol2CellStyle = new iGCellStyle(true);
    this.gridCol2ColHdrStyle = new iGColHdrStyle(true);
    this.panelRight = new Panel();
    this.pictCurrent = new PictureBox();
    this.splitter3 = new Splitter();
    this.panel4 = new Panel();
    this.pictTotal = new PictureBox();
    this.panel5 = new Panel();
    this.label1 = new Label();
    this.panel2 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.panel6 = new Panel();
    this.label2 = new Label();
    this.splitter1 = new Splitter();
    this.panelLeft = new Panel();
    this.tree = new TreeView();
    this.IL = new ImageList();
    this.splitter2 = new Splitter();
    this.panel1 = new Panel();
    this.grid = new iGrid();
    this.panel3 = new Panel();
    this.label3 = new Label();
    this.btnAdd = new Button();
    this.btnReplace = new Button();
    this.btnDelete = new Button();
    this.splitter4 = new Splitter();
    this.panel7 = new Panel();
    this.btnMoveLast = new Button();
    this.btnMoveNext = new Button();
    this.btnMovePrev = new Button();
    this.btnMoveFirst = new Button();
    this.lbSequence = new ListBox();
    this.timer = new Timer();
    this.toolTip1 = new ToolTip();
    this.panelRight.SuspendLayout();
    ((ISupportInitialize) this.pictCurrent).BeginInit();
    this.panel4.SuspendLayout();
    ((ISupportInitialize) this.pictTotal).BeginInit();
    this.panel5.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel6.SuspendLayout();
    this.panelLeft.SuspendLayout();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.grid).BeginInit();
    this.panel3.SuspendLayout();
    this.panel7.SuspendLayout();
    this.SuspendLayout();
    this.iGrid1Col0CellStyle.BackColor = SystemColors.ButtonFace;
    this.iGrid1Col0CellStyle.ReadOnly = iGBool.True;
    this.iGrid1Col1CellStyle.SingleClickEdit = iGBool.True;
    this.iGrid1Col1CellStyle.ValueType = typeof (string);
    this.gridCol2CellStyle.TypeFlags = iGCellTypeFlags.HasEllipsisBtn;
    this.panelRight.Controls.Add((Control) this.pictCurrent);
    this.panelRight.Controls.Add((Control) this.splitter3);
    this.panelRight.Controls.Add((Control) this.panel4);
    this.panelRight.Controls.Add((Control) this.panel2);
    this.panelRight.Controls.Add((Control) this.panel6);
    componentResourceManager.ApplyResources((object) this.panelRight, "panelRight");
    this.panelRight.Name = "panelRight";
    this.pictCurrent.BackColor = SystemColors.Window;
    this.pictCurrent.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.pictCurrent, "pictCurrent");
    this.pictCurrent.Name = "pictCurrent";
    this.pictCurrent.TabStop = false;
    componentResourceManager.ApplyResources((object) this.splitter3, "splitter3");
    this.splitter3.Name = "splitter3";
    this.splitter3.TabStop = false;
    this.panel4.Controls.Add((Control) this.pictTotal);
    this.panel4.Controls.Add((Control) this.panel5);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    this.pictTotal.BackColor = SystemColors.Window;
    this.pictTotal.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.pictTotal, "pictTotal");
    this.pictTotal.Name = "pictTotal";
    this.pictTotal.TabStop = false;
    this.panel5.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel2.Controls.Add((Control) this.btnOK);
    this.panel2.Controls.Add((Control) this.btnCancel);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.panel6.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panelLeft.Controls.Add((Control) this.tree);
    this.panelLeft.Controls.Add((Control) this.splitter2);
    this.panelLeft.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.panelLeft, "panelLeft");
    this.panelLeft.Name = "panelLeft";
    this.tree.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.tree, "tree");
    this.tree.HideSelection = false;
    this.tree.ImageList = this.IL;
    this.tree.Name = "tree";
    this.tree.AfterSelect += new TreeViewEventHandler(this.tree_AfterSelect);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "");
    this.IL.Images.SetKeyName(1, "");
    this.IL.Images.SetKeyName(2, "");
    this.IL.Images.SetKeyName(3, "");
    this.IL.Images.SetKeyName(4, "");
    this.IL.Images.SetKeyName(5, "");
    this.IL.Images.SetKeyName(6, "");
    this.IL.Images.SetKeyName(7, "");
    this.IL.Images.SetKeyName(8, "");
    this.IL.Images.SetKeyName(9, "");
    this.IL.Images.SetKeyName(10, "VVV3.bmp");
    this.IL.Images.SetKeyName(11, "sort1.bmp");
    this.IL.Images.SetKeyName(12, "sort2.bmp");
    componentResourceManager.ApplyResources((object) this.splitter2, "splitter2");
    this.splitter2.Name = "splitter2";
    this.splitter2.TabStop = false;
    this.panel1.Controls.Add((Control) this.grid);
    this.panel1.Controls.Add((Control) this.panel3);
    this.panel1.Controls.Add((Control) this.splitter4);
    this.panel1.Controls.Add((Control) this.panel7);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.grid.BackColor = SystemColors.Info;
    iGcolPattern1.AllowGrouping = false;
    iGcolPattern1.AllowMoving = false;
    iGcolPattern1.CellStyle = this.iGrid1Col0CellStyle;
    iGcolPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
    iGcolPattern1.IncludeInSelect = false;
    iGcolPattern1.SortOrder = iGSortOrder.None;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern2.AllowGrouping = false;
    iGcolPattern2.AllowMoving = false;
    iGcolPattern2.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern2.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    iGcolPattern2.IncludeInSelect = false;
    iGcolPattern2.SortOrder = iGSortOrder.None;
    iGcolPattern2.SortType = iGSortType.None;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    iGcolPattern3.CellStyle = this.gridCol2CellStyle;
    iGcolPattern3.ColHdrStyle = this.gridCol2ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern3, "iGColPattern3");
    this.grid.Cols.AddRange(new iGColPattern[3]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3
    });
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this.grid.Name = "grid";
    this.grid.EllipsisBtnClick += new iGEllipsisBtnClickEventHandler(this.grid_EllipsisBtnClick);
    this.grid.RequestEdit += new iGRequestEditEventHandler(this.grid_RequestEdit);
    this.grid.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.grid_BeforeCommitEdit);
    this.grid.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.grid_AfterCommitEdit);
    this.grid.CancelEdit += new iGCancelEditEventHandler(this.grid_CancelEdit);
    this.grid.TextBoxTextChanged += new iGTextBoxTextChangedEventHandler(this.grid_TextBoxTextChanged);
    this.panel3.Controls.Add((Control) this.label3);
    this.panel3.Controls.Add((Control) this.btnAdd);
    this.panel3.Controls.Add((Control) this.btnReplace);
    this.panel3.Controls.Add((Control) this.btnDelete);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.btnReplace, "btnReplace");
    this.btnReplace.Name = "btnReplace";
    this.btnReplace.UseVisualStyleBackColor = true;
    this.btnReplace.Click += new EventHandler(this.btnReplace_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.splitter4, "splitter4");
    this.splitter4.Name = "splitter4";
    this.splitter4.TabStop = false;
    this.panel7.Controls.Add((Control) this.btnMoveLast);
    this.panel7.Controls.Add((Control) this.btnMoveNext);
    this.panel7.Controls.Add((Control) this.btnMovePrev);
    this.panel7.Controls.Add((Control) this.btnMoveFirst);
    this.panel7.Controls.Add((Control) this.lbSequence);
    componentResourceManager.ApplyResources((object) this.panel7, "panel7");
    this.panel7.Name = "panel7";
    componentResourceManager.ApplyResources((object) this.btnMoveLast, "btnMoveLast");
    this.btnMoveLast.Name = "btnMoveLast";
    this.toolTip1.SetToolTip((Control) this.btnMoveLast, componentResourceManager.GetString("btnMoveLast.ToolTip"));
    this.btnMoveLast.UseVisualStyleBackColor = true;
    this.btnMoveLast.Click += new EventHandler(this.btnMoveLast_Click);
    componentResourceManager.ApplyResources((object) this.btnMoveNext, "btnMoveNext");
    this.btnMoveNext.Name = "btnMoveNext";
    this.toolTip1.SetToolTip((Control) this.btnMoveNext, componentResourceManager.GetString("btnMoveNext.ToolTip"));
    this.btnMoveNext.UseVisualStyleBackColor = true;
    this.btnMoveNext.Click += new EventHandler(this.btnMoveNext_Click);
    componentResourceManager.ApplyResources((object) this.btnMovePrev, "btnMovePrev");
    this.btnMovePrev.Name = "btnMovePrev";
    this.toolTip1.SetToolTip((Control) this.btnMovePrev, componentResourceManager.GetString("btnMovePrev.ToolTip"));
    this.btnMovePrev.UseVisualStyleBackColor = true;
    this.btnMovePrev.Click += new EventHandler(this.btnMovePrev_Click);
    componentResourceManager.ApplyResources((object) this.btnMoveFirst, "btnMoveFirst");
    this.btnMoveFirst.Name = "btnMoveFirst";
    this.toolTip1.SetToolTip((Control) this.btnMoveFirst, componentResourceManager.GetString("btnMoveFirst.ToolTip"));
    this.btnMoveFirst.UseVisualStyleBackColor = true;
    this.btnMoveFirst.Click += new EventHandler(this.btnMoveFirst_Click);
    componentResourceManager.ApplyResources((object) this.lbSequence, "lbSequence");
    this.lbSequence.FormattingEnabled = true;
    this.lbSequence.Name = "lbSequence";
    this.lbSequence.Click += new EventHandler(this.lbSequence_Click);
    this.lbSequence.SelectedIndexChanged += new EventHandler(this.lbSequence_SelectedIndexChanged);
    this.timer.Interval = 500;
    this.timer.Tick += new EventHandler(this.timer_Tick);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.panelLeft);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panelRight);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditSymbolForm);
    this.FormClosing += new FormClosingEventHandler(this.EditSymbolForm_FormClosing);
    this.Load += new EventHandler(this.EditSymbolForm_Load);
    this.panelRight.ResumeLayout(false);
    ((ISupportInitialize) this.pictCurrent).EndInit();
    this.panel4.ResumeLayout(false);
    ((ISupportInitialize) this.pictTotal).EndInit();
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.panel6.PerformLayout();
    this.panelLeft.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.grid).EndInit();
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.panel7.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public class SameData
  {
    private List<string> ids = new List<string>();
    private List<string> sameIds = new List<string>();

    public List<string> Ids
    {
      get => this.ids;
      set => this.ids = value;
    }

    public List<string> SameIds
    {
      get => this.sameIds;
      set => this.sameIds = value;
    }
  }
}
