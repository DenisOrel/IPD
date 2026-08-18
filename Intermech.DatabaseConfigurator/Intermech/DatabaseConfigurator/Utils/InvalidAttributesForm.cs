// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.InvalidAttributesForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class InvalidAttributesForm : Form
{
  private ICategoryTypeIconService typedIconService;
  private Dictionary<int, object> settings = new Dictionary<int, object>();
  private List<InvalidAttributesClass> listOfInvalidAttributes = new List<InvalidAttributesClass>();
  private IContainer components;
  private TreeList resultTree;
  private Button btnSave;
  private Button btnOK;
  private Panel panel1;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private TreeListColumn treeListColumn3;
  private SaveFileDialog saveFileDialog;

  public List<InvalidAttributesClass> ListOfInvalidAttributes
  {
    get => this.listOfInvalidAttributes;
    set
    {
      this.listOfInvalidAttributes = value;
      this.CreateResultTree();
    }
  }

  private void CreateResultTree()
  {
    if (this.listOfInvalidAttributes == null)
      return;
    try
    {
      this.resultTree.BeginUpdate();
      this.resultTree.BeginSort();
      this.resultTree.ClearNodes();
      foreach (InvalidAttributesClass invalidAttribute in this.listOfInvalidAttributes)
      {
        if (invalidAttribute.TableOfAttributes.Rows.Count > 0)
          this.AddObjectTypeNode(invalidAttribute);
      }
    }
    finally
    {
      this.resultTree.EndSort();
      this.resultTree.EndUpdate();
    }
  }

  private void AddObjectTypeNode(InvalidAttributesClass currentObjectType)
  {
    TreeListNode parentNode = this.resultTree.AppendNode((object) new object[3]
    {
      (object) MetaDataHelper.GetObjectType(currentObjectType.ObjectType).ObjectTypeName,
      null,
      null
    }, (TreeListNode) null);
    parentNode.ImageIndex = parentNode.SelectImageIndex = this.typedIconService.IndexOf(4, currentObjectType.ObjectType);
    parentNode.Tag = (object) currentObjectType.AllObjectsCount;
    foreach (DataRow row in (InternalDataCollectionBase) currentObjectType.TableOfAttributes.Rows)
      this.AddAttributeTypeNode(parentNode, row);
  }

  private void AddAttributeTypeNode(TreeListNode parentNode, DataRow currentAttributeInfo)
  {
    int int32_1 = Convert.ToInt32(currentAttributeInfo[0]);
    int int32_2 = Convert.ToInt32(currentAttributeInfo[1]);
    int int32_3 = Convert.ToInt32(parentNode.Tag);
    double num1 = (double) int32_2 / (double) int32_3;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(int32_1);
    TreeListNode treeListNode = this.resultTree.AppendNode((object) new object[3]
    {
      (object) attributeType.Name,
      (object) int32_2,
      (object) num1
    }, parentNode);
    int num2;
    int num3 = num2 = this.typedIconService.IndexOf(3, -1, (object) attributeType.FieldType);
    treeListNode.SelectImageIndex = num2;
    treeListNode.ImageIndex = num3;
  }

  public InvalidAttributesForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1245);
    this.Init();
  }

  public void Init()
  {
    this.typedIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.resultTree.SelectImageList = this.typedIconService.ImageList;
  }

  private void InvalidAttributesForm_Load(object sender, EventArgs e)
  {
    this.settings.Clear();
    FormStorage.LoadLayout((Control) this, (IDictionary) this.settings);
    this.SetControlState();
  }

  private void InvalidAttributesForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlState();
    FormStorage.SaveLayout((Control) this, (IDictionary) this.settings);
  }

  private void GetControlState()
  {
    this.settings.Clear();
    for (int index = 0; index < this.resultTree.Columns.Count; ++index)
      this.settings.Add(index + 1000, (object) this.resultTree.Columns[index].Width);
  }

  private void SetControlState()
  {
    for (int index = 0; index < this.resultTree.Columns.Count; ++index)
    {
      if (this.settings.ContainsKey(index + 1000))
        this.resultTree.Columns[index].Width = (int) this.settings[index + 1000];
    }
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK || !(this.saveFileDialog.FileName != string.Empty))
      return;
    using (StreamWriter text = File.CreateText(this.saveFileDialog.FileName))
    {
      foreach (TreeListNode node1 in this.resultTree.Nodes)
      {
        text.WriteLine(node1[(object) 0]);
        foreach (TreeListNode node2 in node1.Nodes)
          text.WriteLine($"\t{node2[(object) 0]}\t{node2[(object) 1]}\t{$"{node2[(object) 2]:P2}"}");
      }
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_218"), LocalizationHolder.rm.GetString("DatabaseConfigurator_219"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InvalidAttributesForm));
    this.resultTree = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumn3 = new TreeListColumn();
    this.btnSave = new Button();
    this.btnOK = new Button();
    this.panel1 = new Panel();
    this.saveFileDialog = new SaveFileDialog();
    this.resultTree.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.resultTree, "resultTree");
    this.resultTree.Columns.AddRange(new TreeListColumn[3]
    {
      this.treeListColumn1,
      this.treeListColumn2,
      this.treeListColumn3
    });
    this.resultTree.Name = "resultTree";
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    componentResourceManager.ApplyResources((object) this.treeListColumn3, "treeListColumn3");
    this.treeListColumn3.Format.FormatString = "P2";
    this.treeListColumn3.Format.FormatType = FormatType.Numeric;
    this.treeListColumn3.Name = "treeListColumn3";
    componentResourceManager.ApplyResources((object) this.btnSave, "btnSave");
    this.btnSave.Name = "btnSave";
    this.btnSave.UseVisualStyleBackColor = true;
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnSave);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.saveFileDialog.CreatePrompt = true;
    this.saveFileDialog.DefaultExt = "*.txt";
    this.saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.resultTree);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (InvalidAttributesForm);
    this.Load += new EventHandler(this.InvalidAttributesForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.InvalidAttributesForm_FormClosed);
    this.resultTree.EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
