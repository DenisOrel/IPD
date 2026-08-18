// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionTreeSetupForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionRule;
using Intermech.DataFormats;
using Intermech.Extensions.WinForms;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Protection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionTreeSetupForm : Form
{
  private Panel pnlLeft;
  private Panel pInfo;
  private Panel pnlButtons;
  private TabControl tcInfo;
  private IContainer components;
  private ImageList ilSource;
  private ImageList ilSelTree;
  private Panel pnlCClient;
  private ToolStrip tstripMain;
  private ToolStripLabel tslblSelectionTypes;
  private ToolStripComboBox tscbSelectionTypes;
  private ToolStripSeparator toolStripSeparator1;
  private Panel pnlClient;
  private ToolStripLabel tslblSpace;
  private TreeView tvSelectionTypes;
  private Splitter splCLeft;
  private Panel pnlRight;
  private Splitter splCRight;
  private ToolStripDropDownButton toolStripDropDownButton;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton tsbtnProperties;
  private SplitContainer spCntrClient;
  private TreeView tvAutoSelectionItems;
  private TextBox tbxConditions;
  private PropertyGrid pgSelectionItem;
  private ContextMenuStrip cmsSelectionItems;
  private ToolStripMenuItem tsmiRuleEdit;
  private ToolStripSeparator tsmiSep1;
  private ToolStripMenuItem tsmiRuleTest;
  private ToolStripMenuItem tsmiModeAll;
  private ToolStripSeparator tsmiModeSep1;
  private ToolStripMenuItem tsmiModeWithRule;
  private ToolStripMenuItem tsmiModeWithoutRule;
  private ToolStripMenuItem tsmiRuleNew;
  private ToolStripMenuItem tsmiRuleInclude;
  private ToolStripMenuItem tsmiRuleExclude;
  private ToolStripMenuItem tsmiRuleDelete;
  private ToolStripSeparator tsmiRuleSep3;
  private ToolStripMenuItem tsmiTypeDelete;
  private ToolStripMenuItem tsmiRuleMove;
  private ToolStripMenuItem tsmiRuleMoveFirst;
  private ToolStripMenuItem tsmiRuleMoveUp;
  private ToolStripMenuItem tsmiRuleMoveDown;
  private ToolStripMenuItem tsmiRuleMoveLast;
  private ToolStripSeparator tsmiSep2;
  private TreeView tvImbase;
  private ToolStripMenuItem tsmiRuleNewType;
  private ToolStripMenuItem tsmiRuleNewImbase;
  private ContextMenuStrip cmsTypeItems;
  private ToolStripMenuItem tsmiRuleIncludeType;
  private ToolStripMenuItem tsmiRuleIncludeImbase;
  private ToolStripSeparator tsmiTypeSep1;
  private ToolStripMenuItem tsmiTypeUpdate;
  private ToolStripMenuItem tsmiTypeAdd;
  private bool _readOnly = true;
  internal ImbaseFoldersViewMode _viewMode;
  private TreeBuilder _treeBuilder;
  private List<int> _objectTypes;
  private List<int> _imbaseObjectTypes;

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionTreeSetupForm));
    this.spCntrClient = new SplitContainer();
    this.tvAutoSelectionItems = new TreeView();
    this.cmsSelectionItems = new ContextMenuStrip(this.components);
    this.tsmiRuleNew = new ToolStripMenuItem();
    this.tsmiRuleNewType = new ToolStripMenuItem();
    this.tsmiRuleNewImbase = new ToolStripMenuItem();
    this.tsmiRuleInclude = new ToolStripMenuItem();
    this.tsmiRuleIncludeType = new ToolStripMenuItem();
    this.tsmiRuleIncludeImbase = new ToolStripMenuItem();
    this.tsmiRuleEdit = new ToolStripMenuItem();
    this.tsmiSep1 = new ToolStripSeparator();
    this.tsmiRuleMove = new ToolStripMenuItem();
    this.tsmiRuleMoveFirst = new ToolStripMenuItem();
    this.tsmiRuleMoveUp = new ToolStripMenuItem();
    this.tsmiRuleMoveDown = new ToolStripMenuItem();
    this.tsmiRuleMoveLast = new ToolStripMenuItem();
    this.tsmiSep2 = new ToolStripSeparator();
    this.tsmiRuleExclude = new ToolStripMenuItem();
    this.tsmiRuleDelete = new ToolStripMenuItem();
    this.tsmiRuleSep3 = new ToolStripSeparator();
    this.tsmiRuleTest = new ToolStripMenuItem();
    this.ilSelTree = new ImageList(this.components);
    this.tbxConditions = new TextBox();
    this.pnlLeft = new Panel();
    this.tvImbase = new TreeView();
    this.tvSelectionTypes = new TreeView();
    this.cmsTypeItems = new ContextMenuStrip(this.components);
    this.tsmiTypeAdd = new ToolStripMenuItem();
    this.tsmiTypeDelete = new ToolStripMenuItem();
    this.tsmiTypeSep1 = new ToolStripSeparator();
    this.tsmiTypeUpdate = new ToolStripMenuItem();
    this.ilSource = new ImageList(this.components);
    this.pInfo = new Panel();
    this.tcInfo = new TabControl();
    this.pnlButtons = new Panel();
    this.pnlCClient = new Panel();
    this.tstripMain = new ToolStrip();
    this.tslblSpace = new ToolStripLabel();
    this.tslblSelectionTypes = new ToolStripLabel();
    this.tscbSelectionTypes = new ToolStripComboBox();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.toolStripDropDownButton = new ToolStripDropDownButton();
    this.tsmiModeAll = new ToolStripMenuItem();
    this.tsmiModeSep1 = new ToolStripSeparator();
    this.tsmiModeWithRule = new ToolStripMenuItem();
    this.tsmiModeWithoutRule = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.tsbtnProperties = new ToolStripButton();
    this.pnlClient = new Panel();
    this.splCRight = new Splitter();
    this.pnlRight = new Panel();
    this.pgSelectionItem = new PropertyGrid();
    this.splCLeft = new Splitter();
    this.spCntrClient.BeginInit();
    this.spCntrClient.Panel1.SuspendLayout();
    this.spCntrClient.Panel2.SuspendLayout();
    this.spCntrClient.SuspendLayout();
    this.cmsSelectionItems.SuspendLayout();
    this.pnlLeft.SuspendLayout();
    this.cmsTypeItems.SuspendLayout();
    this.pInfo.SuspendLayout();
    this.pnlCClient.SuspendLayout();
    this.tstripMain.SuspendLayout();
    this.pnlClient.SuspendLayout();
    this.pnlRight.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.spCntrClient, "spCntrClient");
    this.spCntrClient.FixedPanel = FixedPanel.Panel2;
    this.spCntrClient.Name = "spCntrClient";
    this.spCntrClient.Panel1.Controls.Add((Control) this.tvAutoSelectionItems);
    this.spCntrClient.Panel2.Controls.Add((Control) this.tbxConditions);
    this.tvAutoSelectionItems.ContextMenuStrip = this.cmsSelectionItems;
    componentResourceManager.ApplyResources((object) this.tvAutoSelectionItems, "tvAutoSelectionItems");
    this.tvAutoSelectionItems.FullRowSelect = true;
    this.tvAutoSelectionItems.HideSelection = false;
    this.tvAutoSelectionItems.ImageList = this.ilSelTree;
    this.tvAutoSelectionItems.Name = "tvAutoSelectionItems";
    this.tvAutoSelectionItems.AfterSelect += new TreeViewEventHandler(this.tvAutoSelectionItems_AfterSelect);
    this.tvAutoSelectionItems.DoubleClick += new EventHandler(this.tvAutoSelectionItems_DoubleClick);
    this.cmsSelectionItems.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this.tsmiRuleNew,
      (ToolStripItem) this.tsmiRuleInclude,
      (ToolStripItem) this.tsmiRuleEdit,
      (ToolStripItem) this.tsmiSep1,
      (ToolStripItem) this.tsmiRuleMove,
      (ToolStripItem) this.tsmiSep2,
      (ToolStripItem) this.tsmiRuleExclude,
      (ToolStripItem) this.tsmiRuleDelete,
      (ToolStripItem) this.tsmiRuleSep3,
      (ToolStripItem) this.tsmiRuleTest
    });
    this.cmsSelectionItems.Name = "cmsSelectionItems";
    componentResourceManager.ApplyResources((object) this.cmsSelectionItems, "cmsSelectionItems");
    this.cmsSelectionItems.Opening += new CancelEventHandler(this.cmsSelectionItems_Opening);
    this.tsmiRuleNew.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiRuleNewType,
      (ToolStripItem) this.tsmiRuleNewImbase
    });
    this.tsmiRuleNew.Name = "tsmiRuleNew";
    componentResourceManager.ApplyResources((object) this.tsmiRuleNew, "tsmiRuleNew");
    this.tsmiRuleNew.Click += new EventHandler(this.tsmiRuleNew_Click);
    this.tsmiRuleNewType.Name = "tsmiRuleNewType";
    componentResourceManager.ApplyResources((object) this.tsmiRuleNewType, "tsmiRuleNewType");
    this.tsmiRuleNewType.Click += new EventHandler(this.tsmiRuleNewType_Click);
    this.tsmiRuleNewImbase.Name = "tsmiRuleNewImbase";
    componentResourceManager.ApplyResources((object) this.tsmiRuleNewImbase, "tsmiRuleNewImbase");
    this.tsmiRuleNewImbase.Click += new EventHandler(this.tsmiRuleNewImbase_Click);
    this.tsmiRuleInclude.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiRuleIncludeType,
      (ToolStripItem) this.tsmiRuleIncludeImbase
    });
    this.tsmiRuleInclude.Name = "tsmiRuleInclude";
    componentResourceManager.ApplyResources((object) this.tsmiRuleInclude, "tsmiRuleInclude");
    this.tsmiRuleInclude.Click += new EventHandler(this.tsmiRuleInclude_Click);
    this.tsmiRuleIncludeType.Name = "tsmiRuleIncludeType";
    componentResourceManager.ApplyResources((object) this.tsmiRuleIncludeType, "tsmiRuleIncludeType");
    this.tsmiRuleIncludeType.Click += new EventHandler(this.tsmiRuleIncludeType_Click);
    this.tsmiRuleIncludeImbase.Name = "tsmiRuleIncludeImbase";
    componentResourceManager.ApplyResources((object) this.tsmiRuleIncludeImbase, "tsmiRuleIncludeImbase");
    this.tsmiRuleIncludeImbase.Click += new EventHandler(this.tsmiRuleIncludeImbase_Click);
    this.tsmiRuleEdit.Name = "tsmiRuleEdit";
    componentResourceManager.ApplyResources((object) this.tsmiRuleEdit, "tsmiRuleEdit");
    this.tsmiRuleEdit.Click += new EventHandler(this.tsmiRuleEdit_Click);
    this.tsmiSep1.Name = "tsmiSep1";
    componentResourceManager.ApplyResources((object) this.tsmiSep1, "tsmiSep1");
    this.tsmiRuleMove.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiRuleMoveFirst,
      (ToolStripItem) this.tsmiRuleMoveUp,
      (ToolStripItem) this.tsmiRuleMoveDown,
      (ToolStripItem) this.tsmiRuleMoveLast
    });
    this.tsmiRuleMove.Name = "tsmiRuleMove";
    componentResourceManager.ApplyResources((object) this.tsmiRuleMove, "tsmiRuleMove");
    this.tsmiRuleMoveFirst.Name = "tsmiRuleMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsmiRuleMoveFirst, "tsmiRuleMoveFirst");
    this.tsmiRuleMoveFirst.Click += new EventHandler(this.tsmiSelItemMoveFirst_Click);
    this.tsmiRuleMoveUp.Name = "tsmiRuleMoveUp";
    componentResourceManager.ApplyResources((object) this.tsmiRuleMoveUp, "tsmiRuleMoveUp");
    this.tsmiRuleMoveUp.Click += new EventHandler(this.tsmiSelItemMoveUp_Click);
    this.tsmiRuleMoveDown.Name = "tsmiRuleMoveDown";
    componentResourceManager.ApplyResources((object) this.tsmiRuleMoveDown, "tsmiRuleMoveDown");
    this.tsmiRuleMoveDown.Click += new EventHandler(this.tsmiSelItemMoveDown_Click);
    this.tsmiRuleMoveLast.Name = "tsmiRuleMoveLast";
    componentResourceManager.ApplyResources((object) this.tsmiRuleMoveLast, "tsmiRuleMoveLast");
    this.tsmiRuleMoveLast.Click += new EventHandler(this.tsmiSelItemMoveLast_Click);
    this.tsmiSep2.Name = "tsmiSep2";
    componentResourceManager.ApplyResources((object) this.tsmiSep2, "tsmiSep2");
    this.tsmiSep2.Click += new EventHandler(this.toolStripMenuItem1_Click);
    this.tsmiRuleExclude.Name = "tsmiRuleExclude";
    componentResourceManager.ApplyResources((object) this.tsmiRuleExclude, "tsmiRuleExclude");
    this.tsmiRuleExclude.Click += new EventHandler(this.tsmiRuleExclude_Click);
    this.tsmiRuleDelete.Name = "tsmiRuleDelete";
    componentResourceManager.ApplyResources((object) this.tsmiRuleDelete, "tsmiRuleDelete");
    this.tsmiRuleDelete.Click += new EventHandler(this.tsmiRuleDelete_Click);
    this.tsmiRuleSep3.Name = "tsmiRuleSep3";
    componentResourceManager.ApplyResources((object) this.tsmiRuleSep3, "tsmiRuleSep3");
    this.tsmiRuleTest.Name = "tsmiRuleTest";
    componentResourceManager.ApplyResources((object) this.tsmiRuleTest, "tsmiRuleTest");
    this.tsmiRuleTest.Click += new EventHandler(this.tsmiRuleTest_Click);
    this.ilSelTree.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilSelTree, "ilSelTree");
    this.ilSelTree.TransparentColor = Color.Transparent;
    this.tbxConditions.BackColor = SystemColors.Window;
    componentResourceManager.ApplyResources((object) this.tbxConditions, "tbxConditions");
    this.tbxConditions.Name = "tbxConditions";
    this.tbxConditions.ReadOnly = true;
    this.tbxConditions.TabStop = false;
    this.pnlLeft.Controls.Add((Control) this.tvImbase);
    this.pnlLeft.Controls.Add((Control) this.tvSelectionTypes);
    componentResourceManager.ApplyResources((object) this.pnlLeft, "pnlLeft");
    this.pnlLeft.Name = "pnlLeft";
    componentResourceManager.ApplyResources((object) this.tvImbase, "tvImbase");
    this.tvImbase.Name = "tvImbase";
    this.tvSelectionTypes.ContextMenuStrip = this.cmsTypeItems;
    componentResourceManager.ApplyResources((object) this.tvSelectionTypes, "tvSelectionTypes");
    this.tvSelectionTypes.FullRowSelect = true;
    this.tvSelectionTypes.HideSelection = false;
    this.tvSelectionTypes.ImageList = this.ilSource;
    this.tvSelectionTypes.Name = "tvSelectionTypes";
    this.tvSelectionTypes.BeforeExpand += new TreeViewCancelEventHandler(this.tvSelectionTypes_BeforeExpand);
    this.tvSelectionTypes.AfterSelect += new TreeViewEventHandler(this.tvSelectionTypes_AfterSelect);
    this.cmsTypeItems.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiTypeAdd,
      (ToolStripItem) this.tsmiTypeDelete,
      (ToolStripItem) this.tsmiTypeSep1,
      (ToolStripItem) this.tsmiTypeUpdate
    });
    this.cmsTypeItems.Name = "cmsTypeItems";
    componentResourceManager.ApplyResources((object) this.cmsTypeItems, "cmsTypeItems");
    this.cmsTypeItems.Opening += new CancelEventHandler(this.cmsTypeItems_Opening);
    this.tsmiTypeAdd.Name = "tsmiTypeAdd";
    componentResourceManager.ApplyResources((object) this.tsmiTypeAdd, "tsmiTypeAdd");
    this.tsmiTypeAdd.Click += new EventHandler(this.tsmiTypeAdd_Click);
    this.tsmiTypeDelete.Name = "tsmiTypeDelete";
    componentResourceManager.ApplyResources((object) this.tsmiTypeDelete, "tsmiTypeDelete");
    this.tsmiTypeDelete.Click += new EventHandler(this.tsmiTypeDelete_Click);
    this.tsmiTypeSep1.Name = "tsmiTypeSep1";
    componentResourceManager.ApplyResources((object) this.tsmiTypeSep1, "tsmiTypeSep1");
    this.tsmiTypeUpdate.Name = "tsmiTypeUpdate";
    componentResourceManager.ApplyResources((object) this.tsmiTypeUpdate, "tsmiTypeUpdate");
    this.tsmiTypeUpdate.Click += new EventHandler(this.tsmiTypeUpdate_Click);
    this.ilSource.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilSource, "ilSource");
    this.ilSource.TransparentColor = Color.Transparent;
    this.pInfo.Controls.Add((Control) this.tcInfo);
    componentResourceManager.ApplyResources((object) this.pInfo, "pInfo");
    this.pInfo.Name = "pInfo";
    componentResourceManager.ApplyResources((object) this.tcInfo, "tcInfo");
    this.tcInfo.Name = "tcInfo";
    this.tcInfo.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    this.pnlCClient.Controls.Add((Control) this.spCntrClient);
    componentResourceManager.ApplyResources((object) this.pnlCClient, "pnlCClient");
    this.pnlCClient.Name = "pnlCClient";
    this.tstripMain.Items.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.tslblSpace,
      (ToolStripItem) this.tslblSelectionTypes,
      (ToolStripItem) this.tscbSelectionTypes,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.toolStripDropDownButton,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.tsbtnProperties
    });
    componentResourceManager.ApplyResources((object) this.tstripMain, "tstripMain");
    this.tstripMain.Name = "tstripMain";
    this.tslblSpace.Name = "tslblSpace";
    componentResourceManager.ApplyResources((object) this.tslblSpace, "tslblSpace");
    this.tslblSelectionTypes.Name = "tslblSelectionTypes";
    componentResourceManager.ApplyResources((object) this.tslblSelectionTypes, "tslblSelectionTypes");
    this.tscbSelectionTypes.DropDownStyle = ComboBoxStyle.DropDownList;
    this.tscbSelectionTypes.Name = "tscbSelectionTypes";
    componentResourceManager.ApplyResources((object) this.tscbSelectionTypes, "tscbSelectionTypes");
    this.tscbSelectionTypes.SelectedIndexChanged += new EventHandler(this.tscbSelectionTypes_SelectedIndexChanged);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiModeAll,
      (ToolStripItem) this.tsmiModeSep1,
      (ToolStripItem) this.tsmiModeWithRule,
      (ToolStripItem) this.tsmiModeWithoutRule
    });
    componentResourceManager.ApplyResources((object) this.toolStripDropDownButton, "toolStripDropDownButton");
    this.toolStripDropDownButton.Name = "toolStripDropDownButton";
    this.tsmiModeAll.Checked = true;
    this.tsmiModeAll.CheckState = CheckState.Checked;
    this.tsmiModeAll.MergeIndex = 0;
    this.tsmiModeAll.Name = "tsmiModeAll";
    componentResourceManager.ApplyResources((object) this.tsmiModeAll, "tsmiModeAll");
    this.tsmiModeAll.Click += new EventHandler(this.tsmiModeAll_Click);
    this.tsmiModeSep1.Name = "tsmiModeSep1";
    componentResourceManager.ApplyResources((object) this.tsmiModeSep1, "tsmiModeSep1");
    this.tsmiModeWithRule.MergeIndex = 0;
    this.tsmiModeWithRule.Name = "tsmiModeWithRule";
    componentResourceManager.ApplyResources((object) this.tsmiModeWithRule, "tsmiModeWithRule");
    this.tsmiModeWithRule.Click += new EventHandler(this.tsmiModeWithRule_Click);
    this.tsmiModeWithoutRule.MergeIndex = 0;
    this.tsmiModeWithoutRule.Name = "tsmiModeWithoutRule";
    componentResourceManager.ApplyResources((object) this.tsmiModeWithoutRule, "tsmiModeWithoutRule");
    this.tsmiModeWithoutRule.Click += new EventHandler(this.tsmiModeWithoutRule_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this.tsbtnProperties.Checked = true;
    this.tsbtnProperties.CheckOnClick = true;
    this.tsbtnProperties.CheckState = CheckState.Checked;
    this.tsbtnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnProperties, "tsbtnProperties");
    this.tsbtnProperties.Name = "tsbtnProperties";
    this.tsbtnProperties.Click += new EventHandler(this.tsbtnProperties_Click);
    this.pnlClient.Controls.Add((Control) this.pnlCClient);
    this.pnlClient.Controls.Add((Control) this.splCRight);
    this.pnlClient.Controls.Add((Control) this.pnlRight);
    this.pnlClient.Controls.Add((Control) this.splCLeft);
    this.pnlClient.Controls.Add((Control) this.pnlLeft);
    this.pnlClient.Controls.Add((Control) this.tstripMain);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    componentResourceManager.ApplyResources((object) this.splCRight, "splCRight");
    this.splCRight.Name = "splCRight";
    this.splCRight.TabStop = false;
    this.pnlRight.Controls.Add((Control) this.pgSelectionItem);
    componentResourceManager.ApplyResources((object) this.pnlRight, "pnlRight");
    this.pnlRight.Name = "pnlRight";
    this.pgSelectionItem.CategoryForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this.pgSelectionItem, "pgSelectionItem");
    this.pgSelectionItem.Name = "pgSelectionItem";
    this.pgSelectionItem.SelectedObject = (object) this.tvAutoSelectionItems;
    this.pgSelectionItem.PropertyValueChanged += new PropertyValueChangedEventHandler(this.pgSelectionItem_PropertyValueChanged);
    this.pgSelectionItem.SelectedObjectsChanged += new EventHandler(this.pgSelectionItem_SelectedObjectsChanged);
    componentResourceManager.ApplyResources((object) this.splCLeft, "splCLeft");
    this.splCLeft.Name = "splCLeft";
    this.splCLeft.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.pInfo);
    this.Controls.Add((Control) this.pnlButtons);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoSelectionTreeSetupForm);
    this.Tag = (object) " ";
    this.Activated += new EventHandler(this.AutoSelectionTreeSetupForm_Activated);
    this.FormClosed += new FormClosedEventHandler(this.AutoSelectionTreeSetupForm_Closed);
    this.Load += new EventHandler(this.AutoSelectionTreeSetupForm_Load);
    this.spCntrClient.Panel1.ResumeLayout(false);
    this.spCntrClient.Panel2.ResumeLayout(false);
    this.spCntrClient.Panel2.PerformLayout();
    this.spCntrClient.EndInit();
    this.spCntrClient.ResumeLayout(false);
    this.cmsSelectionItems.ResumeLayout(false);
    this.pnlLeft.ResumeLayout(false);
    this.cmsTypeItems.ResumeLayout(false);
    this.pInfo.ResumeLayout(false);
    this.pnlCClient.ResumeLayout(false);
    this.tstripMain.ResumeLayout(false);
    this.tstripMain.PerformLayout();
    this.pnlClient.ResumeLayout(false);
    this.pnlClient.PerformLayout();
    this.pnlRight.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void InitializeData()
  {
    this._objectTypes = new List<int>();
    this._imbaseObjectTypes = new List<int>();
    this._treeBuilder = new TreeBuilder(this.components)
    {
      TreeView = this.tvImbase,
      ShowTableReferences = false
    };
    this.DoubleBuffered = true;
    this.tvAutoSelectionItems.Enabled = false;
    this.pgSelectionItem.SelectedObject = (object) null;
    this.tvAutoSelectionItems.TreeViewNodeSorter = (IComparer) new AutoSelectionNodeTreeComparer();
    this.InitializeImageData();
    this.LoadSelectionObjTypes();
    this.LoadImbaseTypesList();
    this.FillSelectionTypesList();
  }

  internal void InitializeAccessInfo()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBSecurity objectType = sessionKeeper.Session.GetObjectType(AutoSelectionConsts.objTypeRuleID) as IDBSecurity;
      try
      {
        this._readOnly = objectType == null || !objectType.CheckAccess(ActionType.CreateChildItem);
      }
      catch (AccessDeniedException ex)
      {
        this._readOnly = true;
      }
    }
  }

  private void InitializeImageData()
  {
    ImageList ilTree = (ImageList) null;
    AutosSelectConsts.Images.LoadImages(ref ilTree);
    this.tvAutoSelectionItems.ImageList = ilTree;
    AutosSelectConsts.Images.LoadBaseImages(this.ilSelTree);
    this.tvSelectionTypes.StateImageList = this.ilSelTree;
    if (this._treeBuilder == null)
      return;
    this.tvSelectionTypes.ImageList = this.tvImbase.ImageList;
  }

  private int GetNodeStateImage(NodeInfo nodeInfo, IUserSession session)
  {
    if (nodeInfo == null)
      return -1;
    IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
    if (autosServerService == null)
      return -1;
    List<long> longList = nodeInfo is ObjectTypeRec ? autosServerService.GetAllRulesByObjectType(nodeInfo.TypeId) : autosServerService.GetRulesByImbaseObjOnly(nodeInfo.ObjectId, session.SessionGUID);
    return longList == null || longList.Count <= 0 ? -1 : 8;
  }

  private void FillNodeStatusImage(TreeNode node, IUserSession session, IList<long> filterList = null)
  {
    if (!(node?.Tag is NodeInfo tag))
      return;
    node.StateImageIndex = filterList != null ? (filterList.Contains(tag.ObjectId) ? 8 : -1) : this.GetNodeStateImage(tag, session);
    foreach (TreeNode node1 in node.Nodes)
      this.FillNodeStatusImage(node1, session, filterList);
  }

  private TreeNode FindSelectionTypeNode(long objectId, int typeId)
  {
    return objectId == 0L && typeId == -1 ? (TreeNode) null : this.FindSelectionTypeNode(this.tvSelectionTypes.Nodes, objectId, typeId);
  }

  private TreeNode FindSelectionTypeNode(TreeNodeCollection nodes, long objectId, int typeId)
  {
    if (objectId == 0L && typeId == -1)
      return (TreeNode) null;
    foreach (TreeNode node in nodes)
    {
      if (node.Tag is NodeInfo tag)
      {
        if (objectId == 0L)
        {
          if (tag.ObjectId == objectId && tag.TypeId == typeId)
            return node;
        }
        else if (tag.ObjectId == objectId)
          return node;
      }
    }
    TreeNode selectionTypeNode = (TreeNode) null;
    foreach (TreeNode node in nodes)
    {
      selectionTypeNode = this.FindSelectionTypeNode(node.Nodes, objectId, typeId);
      if (selectionTypeNode != null)
        break;
    }
    return selectionTypeNode;
  }

  private void UpdateControls() => this.pnlRight.Visible = this.tsbtnProperties.Checked;

  private void LoadSelectionObjTypes()
  {
    this._objectTypes.Clear();
    foreach (int objectType in AutoSelectionUtils.Cache.GetObjectTypes(true))
    {
      if (!this._objectTypes.Contains(objectType))
        this._objectTypes.Add(objectType);
    }
    foreach (int objectTypesWithRule in AutoSelectionUtils.Cache.GetObjectTypesWithRules(true))
    {
      if (!this._objectTypes.Contains(objectTypesWithRule))
        this._objectTypes.Add(objectTypesWithRule);
    }
  }

  private void FillSelectionTypesList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this.tvSelectionTypes.BeginUpdate();
      try
      {
        this.tvSelectionTypes.Nodes.Clear();
        foreach (int objectType in this._objectTypes)
          this.FillSelectionType(objectType, session);
      }
      finally
      {
        this.tvSelectionTypes.EndUpdate();
      }
    }
  }

  private TreeNode FillSelectionType(int objTypeId, IUserSession session)
  {
    IDBObjectType objectType = session.GetObjectType(objTypeId, false);
    if (objectType == null)
      return (TreeNode) null;
    TreeNode node = this.tvSelectionTypes.Nodes.Add(objectType.ObjectTypeName);
    ObjectTypeRec objectTypeRec = new ObjectTypeRec(0L, objTypeId, ((IDBGuid) objectType).GUID, objectType.ObjectTypeName)
    {
      HasImbaseCatalogs = this._imbaseObjectTypes.Contains(objTypeId)
    };
    node.Tag = (object) objectTypeRec;
    int iconIndex = TreeBuilder.GetIconIndex(objTypeId);
    node.ImageIndex = node.SelectedImageIndex = iconIndex;
    node.StateImageIndex = this.GetNodeStateImage((NodeInfo) objectTypeRec, session);
    this._treeBuilder.AddDummyNode(node);
    return node;
  }

  private void FillSelectionTypeNodeList(TreeNode typeNode)
  {
    if (!this._treeBuilder.UnexploredNode(typeNode) || typeNode.Level != 0)
      return;
    NodeInfo tag = (NodeInfo) typeNode.Tag;
    this.tvSelectionTypes.BeginUpdate();
    try
    {
      typeNode.Nodes.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        List<long> filterList = this.CollectFoldersWithRules(tag.TypeId, ImbaseFoldersViewMode.WithRuleOnly, session);
        if (filterList == null || filterList.Count == 0)
          return;
        this.tvImbase.BeginUpdate();
        try
        {
          this._treeBuilder.ShowList(filterList.ToArray());
          foreach (TreeNode node in this.tvImbase.Nodes.Cast<TreeNode>().ToList<TreeNode>())
          {
            if (node != null)
            {
              this.tvImbase.Nodes.Remove(node);
              typeNode.Nodes.Add(node);
              this.FillNodeStatusImage(node, session, (IList<long>) filterList);
            }
          }
        }
        finally
        {
          this.tvImbase.EndUpdate();
        }
      }
    }
    finally
    {
      this.tvSelectionTypes.EndUpdate();
    }
  }

  private void FillSelectionImbaseNodeList(TreeNode imbaseNode)
  {
    if (!this._treeBuilder.UnexploredNode(imbaseNode) || imbaseNode.Level == 0)
      return;
    Dictionary<NodeInfo, TreeNode> dictionary = new Dictionary<NodeInfo, TreeNode>();
    foreach (TreeNode node in imbaseNode.Nodes)
    {
      if (!this._treeBuilder.IsDummyNode(node) && node.Tag is NodeInfo tag)
        dictionary.Add(tag, node);
    }
    imbaseNode.TreeView.BeginUpdate();
    try
    {
      this._treeBuilder.ExploreNode(imbaseNode);
      if (dictionary.Count != 0)
      {
        for (int index = 0; index < imbaseNode.Nodes.Count; ++index)
        {
          if (imbaseNode.Nodes[index].Tag is NodeInfo tag && dictionary.ContainsKey(tag))
          {
            TreeNode treeNode = dictionary[tag];
            if (treeNode.Nodes.Count != 0)
            {
              TreeNode node = imbaseNode.Nodes[index];
              node.Nodes.Clear();
              List<TreeNode> treeNodeList = new List<TreeNode>(treeNode.Nodes.Count);
              treeNodeList.AddRange(treeNode.Nodes.Cast<TreeNode>());
              node.Nodes.AddRange(treeNodeList.ToArray());
              node.Expand();
            }
          }
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.FillNodeStatusImage(imbaseNode, sessionKeeper.Session);
    }
    finally
    {
      imbaseNode.TreeView.EndUpdate();
    }
  }

  private void LoadImbaseTypesList()
  {
    this._imbaseObjectTypes.Clear();
    if (MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseRootObjectTypeID) == null)
      return;
    bool flag = false;
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
    if (childrenIdRecursive != null)
    {
      childrenIdRecursive.Remove(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
      flag = childrenIdRecursive.Count > 0;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      }, new object[3]
      {
        (object) -2,
        (object) Intermech.Imbase.Consts.ObjectSortOrderAttID,
        (object) Intermech.Imbase.Consts.CreatedObjectAttID
      }, (object[]) null, (SortOrders[]) null)
      {
        ColumnNames = new ColumnNameMapping[3]
        {
          ColumnNameMapping.ID,
          ColumnNameMapping.ID,
          ColumnNameMapping.ID
        },
        TableName = "f",
        FailIfNotFound = false
      };
      DataTable dataTable = flag ? objectCollection.SelectWithLocalObjects(paramSet) : objectCollection.Select(paramSet);
      Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string g = Convert.ToString(row[Intermech.Imbase.Consts.CreatedObjectAttID.ToString()]);
        if (!g.Equals(string.Empty))
        {
          Guid key = new Guid(g);
          if (!key.Equals(Guid.Empty) && !dictionary.ContainsKey(key))
            dictionary.Add(key, 0);
        }
      }
      foreach (Guid key in dictionary.Keys)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(key);
        if (objectType != null)
          this._imbaseObjectTypes.Add(objectType.ObjectTypeID);
      }
    }
  }

  private void FillSelectionRules(NodeInfo nodeInfo)
  {
    List<long> ruleIds = new List<long>();
    try
    {
      if (nodeInfo == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
        if (autosServerService == null)
          return;
        if (nodeInfo is ObjectTypeRec)
          ruleIds.AddRange((IEnumerable<long>) autosServerService.GetRulesByObjectType(nodeInfo.TypeId));
        else
          ruleIds.AddRange((IEnumerable<long>) autosServerService.GetRulesByImbaseObjOnly(nodeInfo.ObjectId, sessionKeeper.Session.SessionGUID));
      }
    }
    finally
    {
      this.FillSelectionRules(ruleIds);
    }
  }

  private void FillSelectionRules(List<long> ruleIds)
  {
    this.tvAutoSelectionItems.BeginUpdate();
    try
    {
      this.tvAutoSelectionItems.Nodes.Clear();
      this.tvAutoSelectionItems.Tag = (object) null;
      if (ruleIds == null || ruleIds.Count == 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int num1 = 0;
        IUserSession session = sessionKeeper.Session;
        foreach (long ruleId in ruleIds)
        {
          IDBObject objectActualCopy = session.GetObjectActualCopy(ruleId, false);
          if (objectActualCopy != null && this.FillSelectionRule(objectActualCopy)?.Tag is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule tag)
          {
            int num2 = num1++;
            tag.Order = num2;
          }
        }
      }
    }
    finally
    {
      this.tvAutoSelectionItems.Sort();
      if (this.tvAutoSelectionItems.Nodes.Count > 0)
        this.tvAutoSelectionItems.SelectedNode = this.tvAutoSelectionItems.Nodes[0];
      else
        this.pgSelectionItem.SelectedObject = (object) null;
      this.tvAutoSelectionItems.EndUpdate();
      int num = TableView.LockWindowUpdate(this.Handle);
      this.tvAutoSelectionItems.ExpandAll();
      this.tvAutoSelectionItems.Enabled = true;
      if (num != 0)
        TableView.LockWindowUpdate(IntPtr.Zero);
    }
  }

  private TreeNode FillSelectionRule(IDBObject dbObject)
  {
    if (dbObject == null)
      return (TreeNode) null;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(dbObject);
    return rule == null ? (TreeNode) null : SelectionTreeViewUtils.AddSelectionRule(this.tvAutoSelectionItems, rule);
  }

  private void UpdateSelectionNodeProperties()
  {
    object tag = this.tvAutoSelectionItems.SelectedNode?.Tag;
    this.pgSelectionItem.Enabled = false;
    try
    {
      this.pgSelectionItem.SelectedObject = tag;
    }
    finally
    {
      this.pgSelectionItem.Enabled = tag is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule;
    }
    this.UpdateSelectionNodeCond(tag as AutoSelectionNodeCommon);
  }

  private void UpdateSelectionNodeCond(AutoSelectionNodeCommon selNode)
  {
    if (selNode != null)
    {
      if (selNode.Condition == null || selNode.Condition.Count.Equals(0))
        this.tbxConditions.Text = string.Empty;
      else
        this.tbxConditions.Text = selNode.Condition.ToString();
    }
    else
      this.tbxConditions.Text = string.Empty;
  }

  private NodeInfo GetCurrentImbaseNode() => this.tvSelectionTypes.SelectedNode?.Tag as NodeInfo;

  private TreeNode GetSelectionRuleNode()
  {
    TreeNode selectionRuleNode = this.tvAutoSelectionItems.SelectedNode;
    while (selectionRuleNode?.Parent != null)
      selectionRuleNode = selectionRuleNode.Parent;
    return selectionRuleNode;
  }

  private Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule GetCurrentRule()
  {
    return this.GetSelectionRuleNode()?.Tag as Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule;
  }

  private List<long> GetAvailableRules(Guid objectType, NodeInfo nodeInfo)
  {
    List<long> availableRules = new List<long>();
    if (nodeInfo == null || objectType.Equals(Guid.Empty))
      return availableRules;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return availableRules;
      List<long> excludeRuleList = !(nodeInfo is ObjectTypeRec) ? autosServerService.GetRulesByImbaseObjOnly(nodeInfo.ObjectId, session.SessionGUID) ?? new List<long>() : autosServerService.GetRulesByObjectType(nodeInfo.TypeId) ?? new List<long>();
      if (excludeRuleList.Count == 0)
        excludeRuleList.Add(0L);
      return AutoSelectionUtils.Common.GetAvailabledRules(objectType, excludeRuleList, session);
    }
  }

  private DataTable CollectFoldersDataWithRules(
    int createdObjType,
    ImbaseFoldersViewMode mode,
    IUserSession session)
  {
    List<string> stringList = new List<string>();
    IImbaseServer imbaseServerService = AutoSelectionUtils.ServiceKeeper.GetImbaseServerService(session);
    if (imbaseServerService == null)
      return (DataTable) null;
    if (MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseRootObjectTypeID) == null)
      return (DataTable) null;
    DataTable foldersForCreateType = imbaseServerService.GetFoldersForCreateType(session.SessionGUID, (object) createdObjType, (long[]) null, false, false);
    if (foldersForCreateType.Rows.Count == 0)
      return (DataTable) null;
    DataView dataView = new DataView(foldersForCreateType)
    {
      Sort = "F_PATH ASC"
    };
    int columnIndex = foldersForCreateType.Columns.IndexOf("F_PATH");
    string str1 = string.Empty;
    for (int recordIndex = 0; recordIndex < dataView.Count; ++recordIndex)
    {
      string str2 = dataView[recordIndex].Row[columnIndex].ToString();
      if (str1 == string.Empty || str2 != string.Empty && !str2.StartsWith(str1))
      {
        stringList.Add(str2);
        str1 = str2;
      }
    }
    if (stringList.Count == 0)
      return (DataTable) null;
    bool flag = false;
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
    if (childrenIdRecursive != null)
    {
      childrenIdRecursive.Remove(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
      flag = childrenIdRecursive.Count > 0;
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
    int attributeId = MetaDataHelper.GetAttributeID((object) AutoSelectionConsts.attrTypeRuleLinkGuid.ToString());
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    for (int index = 0; index < stringList.Count; ++index)
    {
      int groupID = 0;
      if (index == 0)
        groupID = 1;
      else if (index == stringList.Count - 1)
        groupID = -1;
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) stringList[index], index != stringList.Count - 1 ? LogicalOperators.OR : LogicalOperators.AND, groupID, true));
    }
    conditionStructureList.Add(mode == ImbaseFoldersViewMode.WithRuleOnly ? new ConditionStructure(attributeId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false) : new ConditionStructure(attributeId, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.NONE, 0, false));
    object[] columns = new object[4]
    {
      (object) -2,
      (object) Intermech.Imbase.Consts.ObjectSortOrderAttID,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) attributeId
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns, (object[]) null, (SortOrders[]) null)
    {
      ColumnNames = new ColumnNameMapping[4]
      {
        ColumnNameMapping.ID,
        ColumnNameMapping.ID,
        ColumnNameMapping.ID,
        ColumnNameMapping.ID
      },
      TableName = "f",
      FailIfNotFound = false
    };
    return flag ? objectCollection.SelectWithLocalObjects(paramSet) : objectCollection.Select(paramSet);
  }

  private List<long> CollectFoldersWithRules(
    int createdObjType,
    ImbaseFoldersViewMode mode,
    IUserSession session)
  {
    List<long> longList = new List<long>();
    IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
    DataTable dataTable = this.CollectFoldersDataWithRules(createdObjType, mode, session);
    if (dataTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long result;
        if (long.TryParse(Convert.ToString(row[-2.ToString()]), out result) && (autosServerService == null || dataTable.Rows.Count > 20 || autosServerService.GetRulesByImbaseObjOnly(result, session.SessionGUID).Count != 0))
          longList.Add(result);
      }
    }
    return longList;
  }

  private ObjectTypeRec GetCurrentType() => this.GetCurrentTypeNode()?.Tag as ObjectTypeRec;

  private TreeNode GetCurrentTypeNode()
  {
    TreeNode currentTypeNode = this.tvSelectionTypes.SelectedNode;
    if (currentTypeNode == null)
      return (TreeNode) null;
    while (currentTypeNode.Parent != null)
      currentTypeNode = currentTypeNode.Parent;
    return currentTypeNode;
  }

  private void UpdateTypeNode(TreeNode typeNode)
  {
    if (typeNode == null)
      return;
    this.tvSelectionTypes.BeginUpdate();
    try
    {
      typeNode.Nodes.Clear();
      this._treeBuilder.AddDummyNode(typeNode);
      this.FillSelectionTypeNodeList(typeNode);
    }
    finally
    {
      this.tvSelectionTypes.EndUpdate();
    }
    typeNode.Expand();
  }

  private void TypeNew()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (this.ReadOnly)
      return;
    List<int> inApplicabilities = MetaDataHelper.GetObjectTypesWithEnterInApplicabilities();
    SelectorForm form = new SelectorForm(typeof (ObjectTypesFolder), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_56"), typeof (ObjectTypeFolder), false);
    form.ClearSelection();
    form.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(inApplicabilities.ToArray(), true, true);
    form.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
    form.ExpandLevelsOnLoad = 1;
    if (form.ShowTopDialog() != DialogResult.OK || form.IDList.Count == 0)
      return;
    int id = (int) form.IDList[0];
    if (id == -1 || this._objectTypes.Contains(id))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(id);
    if (objectType != null && objectType.VersionsMode == ObjectVersionModes.Abstract)
    {
      int num2 = (int) MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_735()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_84"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      this._objectTypes.Add(id);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
        if (autosServerService == null)
          return;
        autosServerService.SetObjectTypes(this._objectTypes, session.SessionGUID);
        AutoSelectionUtils.Cache.Invalidate();
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.FillSelectionType(id, sessionKeeper.Session);
    }
  }

  private void TypeRemove()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return;
      List<long> rulesByObjectType = autosServerService.GetAllRulesByObjectType(currentType.TypeId);
      if (rulesByObjectType.Count != 0)
      {
        if (MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_736()), (object) currentType.TypeName), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_48"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes || MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_737()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_48"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
          return;
        foreach (long objectID in rulesByObjectType)
          sessionKeeper.Session.GetObjectActualCopy(objectID, false)?.Delete(0L);
      }
    }
    this._objectTypes.Remove(currentType.TypeId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return;
      autosServerService.SetObjectTypes(this._objectTypes, session.SessionGUID);
      AutoSelectionUtils.Cache.Invalidate();
    }
    foreach (TreeNode node in this.tvSelectionTypes.Nodes)
    {
      if (node.Tag == currentType)
      {
        node.Remove();
        break;
      }
    }
  }

  private void RuleNewType()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null || this.GetCurrentImbaseNode() == null)
      return;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      rule = new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule(currentType.TypeGuid);
      if (rule.Name.Equals(string.Empty))
      {
        IDBObjectType objectType = session.GetObjectType(currentType.TypeGuid);
        if (objectType == null)
        {
          int num2 = (int) MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_738()), (object) currentType.TypeGuid.ToString()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        rule.Name = objectType.ObjectTypeName;
      }
    }
    if (!this.RuleEdit(ref rule))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (rule.RuleID != 0L)
        return;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(AutoSelectionConsts.objTypeRuleGuid);
      if (objectCollection == null)
        throw new Exception(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_739()), (object) AutoSelectionConsts.objTypeRuleGuid));
      rule.Order = this.tvAutoSelectionItems.Nodes.Count;
      IDBObject dbObject = objectCollection.Create();
      rule.Save(dbObject, sessionKeeper.Session);
      AttributeValues[] valuesList = new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeID((object) "cad001a0-306c-11d8-b4e9-00304f19f545"), (object) currentType.TypeGuid)
      };
      dbObject.SetAttributesValues(valuesList);
      dbObject.CommitCreation(false, true);
      rule.RuleID = dbObject.ObjectID;
      this.Rule_RegisterInNode((NodeInfo) currentType, rule.RuleID);
      SelectionTreeViewUtils.AddSelectionRule(this.tvAutoSelectionItems, rule);
      TreeNode currentTypeNode = this.GetCurrentTypeNode();
      if (currentTypeNode == null)
        return;
      this.tvSelectionTypes.SelectedNode = currentTypeNode;
      this.FillNodeStatusImage(currentTypeNode, sessionKeeper.Session);
    }
  }

  private void RuleNewImbase()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray1 = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray1.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray1;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo nodeInfo = this.GetCurrentImbaseNode();
    if (nodeInfo == null)
      return;
    long objectId = nodeInfo.ObjectId;
    AutoSelectionImbaseObjSelectForm form = new AutoSelectionImbaseObjSelectForm(currentType.TypeId, objectId);
    if (form.ShowTopDialog() != DialogResult.OK || form.ImbaseObjID == 0L)
      return;
    long imbaseObjId = form.ImbaseObjID;
    int[] numArray2 = new int[1]
    {
      MetaDataHelper.GetAttributeTypeID(AutoSelectionConsts.imbaseObjectAttrGuid.ToString())
    };
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      QuickObjectInfo objectInfo = session.GetObjectInfo(imbaseObjId);
      nodeInfo = new NodeInfo(imbaseObjId, objectInfo.ObjectTypeID);
      rule = new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule(currentType.TypeGuid);
      if (rule.Name.Equals(string.Empty))
      {
        IDBObject objectActualCopy = session.GetObjectActualCopy(nodeInfo.ObjectId, false);
        if (objectActualCopy == null)
        {
          int num2 = (int) MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_740()), (object) nodeInfo.ObjectId), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        rule.Name = objectActualCopy.Caption;
      }
    }
    if (!this.RuleEdit(ref rule))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (rule.RuleID != 0L)
        return;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(AutoSelectionConsts.objTypeRuleGuid);
      if (objectCollection == null)
        throw new Exception(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_741()), (object) AutoSelectionConsts.objTypeRuleGuid));
      rule.Order = this.tvAutoSelectionItems.Nodes.Count;
      IDBObject dbObject = objectCollection.Create();
      rule.Save(dbObject, sessionKeeper.Session);
      int attributeId1 = MetaDataHelper.GetAttributeID((object) "cad001a0-306c-11d8-b4e9-00304f19f545");
      int attributeId2 = MetaDataHelper.GetAttributeID((object) "cad001d0-306c-11d8-b4e9-00304f19f545");
      IDBAttributeType attributeType = session.GetAttributeType(numArray2[0]);
      AttributeValues[] valuesList = new AttributeValues[2]
      {
        new AttributeValues(attributeId1, (object) currentType.TypeGuid),
        new AttributeValues(attributeId2, (object) ((IDBGuid) attributeType).GUID)
      };
      dbObject.SetAttributesValues(valuesList);
      dbObject.CommitCreation(false, true);
      rule.RuleID = dbObject.ObjectID;
      rule.AttributeType = ((IDBGuid) attributeType).GUID;
      this.Rule_RegisterInNode(nodeInfo, rule.RuleID);
      TreeNode currentTypeNode = this.GetCurrentTypeNode();
      if (currentTypeNode == null)
        return;
      this.UpdateTypeNode(currentTypeNode);
      TreeNode selectionTypeNode = this.FindSelectionTypeNode(currentTypeNode.Nodes, imbaseObjId, currentType.TypeId);
      if (selectionTypeNode == null)
        return;
      this.tvSelectionTypes.SelectedNode = selectionTypeNode;
    }
  }

  private void RuleIncludeType()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo nodeInfo = (NodeInfo) currentType;
    if (!this.RuleInclude(currentType, nodeInfo))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.FillNodeStatusImage(this.tvSelectionTypes.SelectedNode, sessionKeeper.Session);
    this.FillSelectionRules(nodeInfo);
  }

  private void RuleIncludeImbase()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo nodeInfo = this.GetCurrentImbaseNode();
    if (nodeInfo == null)
      return;
    long objectId = nodeInfo.ObjectId;
    AutoSelectionImbaseObjSelectForm form = new AutoSelectionImbaseObjSelectForm(currentType.TypeId, objectId);
    if (form.ShowTopDialog() != DialogResult.OK || form.ImbaseObjID == 0L)
      return;
    long imbaseObjId = form.ImbaseObjID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(imbaseObjId);
      nodeInfo = new NodeInfo(imbaseObjId, objectInfo.ObjectTypeID);
    }
    if (!this.RuleInclude(currentType, nodeInfo))
      return;
    TreeNode currentTypeNode = this.GetCurrentTypeNode();
    if (currentTypeNode == null)
      return;
    this.UpdateTypeNode(currentTypeNode);
    TreeNode selectionTypeNode = this.FindSelectionTypeNode(currentTypeNode.Nodes, imbaseObjId, currentType.TypeId);
    if (selectionTypeNode == null)
      return;
    this.tvSelectionTypes.SelectedNode = selectionTypeNode;
  }

  private bool RuleInclude(ObjectTypeRec typeRec, NodeInfo nodeInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service1.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (nodeInfo == null || typeRec == null)
      return false;
    List<long> availableRules = this.GetAvailableRules(typeRec.TypeGuid, nodeInfo);
    if (availableRules == null || availableRules.Count == 0)
    {
      string text;
      if (!(nodeInfo is ObjectTypeRec))
      {
        string caption;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          caption = sessionKeeper.Session.GetObjectInfo(nodeInfo.ObjectId).Caption;
        text = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_742()), (object) caption);
      }
      else
        text = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_743()), (object) typeRec.TypeName);
      int num2 = (int) MessageBox.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    int num3 = 0;
    Guid guid = new Guid("{ECF815EB-C53F-4d54-93AB-36050CEBCBD0}");
    IGuidMapper service2 = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true);
    try
    {
      num3 = service2.Register(guid);
      IFactory service3 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, true);
      service3.AddNodeType(num3, typeof (ObjectsListNode));
      service3.AddViewsProvider(num3, (IViewsProvider) new AdvObjectsPropertiesProvider());
      if (!(SelectionWindow.Select(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_45"), (IDescriptor) new ListDescriptor(num3, AutoSelectionConsts.objTypeRuleID, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_72"), (IList) availableRules), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] source))
        return false;
      List<long> list = ((IEnumerable<IDBObjectID>) source).Select<IDBObjectID, long>((System.Func<IDBObjectID, long>) (dbObjId => dbObjId.Value)).ToList<long>();
      this.Rule_RegisterInNode(nodeInfo, list);
    }
    finally
    {
      service2.Unregister(num3);
    }
    return true;
  }

  private void RuleExclude()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo currentImbaseNode = this.GetCurrentImbaseNode();
    if (currentImbaseNode == null)
      return;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule currentRule = this.GetCurrentRule();
    if (currentRule == null || MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_744()), (object) currentRule.Name), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_48"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.Rule_UnregisterInNode(currentImbaseNode, currentRule.RuleID);
    if (this.tvAutoSelectionItems.SelectedNode.Tag == currentRule)
      this.tvAutoSelectionItems.SelectedNode.Remove();
    else
      this.FillSelectionRules(currentImbaseNode);
    TreeNode currentTypeNode = this.GetCurrentTypeNode();
    if (currentTypeNode == null)
      return;
    this.UpdateTypeNode(currentTypeNode);
    TreeNode selectionTypeNode = this.FindSelectionTypeNode(currentTypeNode.Nodes, currentImbaseNode.ObjectId, currentType.TypeId);
    if (selectionTypeNode == null)
      return;
    this.tvSelectionTypes.SelectedNode = selectionTypeNode;
  }

  private void RuleDelete()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo currentImbaseNode = this.GetCurrentImbaseNode();
    if (currentImbaseNode == null)
      return;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule currentRule = this.GetCurrentRule();
    if (currentRule == null || MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_734.ssp_automatch_745()), (object) currentRule.Name), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_48"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(currentRule.RuleID, false);
      if (objectActualCopy != null)
      {
        objectActualCopy.Delete(0L);
        this.Rule_UnregisterInNode(currentImbaseNode, objectActualCopy.ObjectID);
        if (objectActualCopy.CheckoutBy == sessionKeeper.Session.UserID)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(-objectActualCopy.ObjectID, false);
          if (dbObject != null)
          {
            dbObject.Delete(0L);
            this.Rule_UnregisterInNode(currentImbaseNode, dbObject.ObjectID);
          }
        }
      }
      if (this.tvAutoSelectionItems.SelectedNode.Tag == currentRule)
        this.tvAutoSelectionItems.SelectedNode.Remove();
      else
        this.FillSelectionRules(currentImbaseNode);
      TreeNode currentTypeNode = this.GetCurrentTypeNode();
      if (currentTypeNode == null)
        return;
      this.UpdateTypeNode(currentTypeNode);
      TreeNode selectionTypeNode = this.FindSelectionTypeNode(currentTypeNode.Nodes, currentImbaseNode.ObjectId, currentType.TypeId);
      if (selectionTypeNode == null)
        return;
      this.tvSelectionTypes.SelectedNode = selectionTypeNode;
    }
  }

  private void RuleEdit()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule currentRule = this.GetCurrentRule();
    if (currentRule == null || !this.RuleEdit(ref currentRule))
      return;
    this.RuleUpdate(currentRule);
  }

  private bool RuleEdit(ref Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule)
  {
    if (rule == null)
      return false;
    AutoSelectionEditForm form = new AutoSelectionEditForm()
    {
      ReadOnly = false,
      Rule = rule
    };
    int num = form.ShowTopDialog().Equals((object) DialogResult.OK) ? 1 : 0;
    if (num == 0)
      return num != 0;
    rule = form.Rule;
    return num != 0;
  }

  private void RuleUpdate(Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule)
  {
    if (rule == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(rule.RuleID, false);
      if (dbObject == null)
        throw new ObjectNotFoundException(rule.RuleID);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      {
        int num = dbObject.CheckoutBy == 0L ? 1 : 0;
        if (num != 0)
          dbObject = dbObject.CheckOut(false);
        rule.Save(dbObject, sessionKeeper.Session);
        if (num != 0)
          dbObject.CheckIn();
        rule.RuleID = Math.Abs(dbObject.ObjectID);
      }
      else
        rule.Save(dbObject, sessionKeeper.Session);
      TreeNode selectionRuleNode = this.GetSelectionRuleNode();
      if (selectionRuleNode == null)
        return;
      SelectionTreeViewUtils.UpdateSelectionRule(selectionRuleNode, rule, true);
      this.tvAutoSelectionItems.SelectedNode = selectionRuleNode;
    }
  }

  private void RulesTest()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    ObjectTypeRec currentType = this.GetCurrentType();
    if (currentType == null)
      return;
    NodeInfo currentImbaseNode = this.GetCurrentImbaseNode();
    if (currentImbaseNode == null)
      return;
    AutoSelectionNodeTest selectionNodeTest = new AutoSelectionNodeTest(currentType.TypeGuid, currentImbaseNode.ObjectId);
    int num2 = (int) new AutoSelectionTestForm()
    {
      NodeTest = selectionNodeTest
    }.ShowTopDialog();
  }

  private void RuleMoveFirst()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    TreeNode selectionRuleNode = this.GetSelectionRuleNode();
    if (selectionRuleNode == null || selectionRuleNode.Index == 0)
      return;
    this.RuleMove(selectionRuleNode.Index, 0);
  }

  private void RuleMoveUp()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    TreeNode selectionRuleNode = this.GetSelectionRuleNode();
    if (selectionRuleNode == null || selectionRuleNode.Index == 0)
      return;
    this.RuleMove(selectionRuleNode.Index, selectionRuleNode.Index - 1);
  }

  private void RuleMoveDown()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    TreeNode selectionRuleNode = this.GetSelectionRuleNode();
    if (selectionRuleNode == null || selectionRuleNode.Index == this.tvAutoSelectionItems.Nodes.Count - 1)
      return;
    this.RuleMove(selectionRuleNode.Index, selectionRuleNode.Index + 1);
  }

  private void RuleMoveLast()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.ReadOnly)
      return;
    TreeNode selectionRuleNode = this.GetSelectionRuleNode();
    if (selectionRuleNode == null || selectionRuleNode.Index == this.tvAutoSelectionItems.Nodes.Count - 1)
      return;
    this.RuleMove(selectionRuleNode.Index, this.tvAutoSelectionItems.Nodes.Count - 1);
  }

  private void RuleMove(int oldIdx, int newIdx)
  {
    NodeInfo currentImbaseNode = this.GetCurrentImbaseNode();
    if (currentImbaseNode == null || oldIdx == newIdx)
      return;
    List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule> autoSelectionRuleList = new List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule>();
    foreach (TreeNode node in this.tvAutoSelectionItems.Nodes)
    {
      if (node.Tag is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule tag)
        autoSelectionRuleList.Add(tag);
    }
    if (autoSelectionRuleList.Count == 0)
      return;
    int num = Math.Sign(oldIdx - newIdx);
    try
    {
      for (int index = newIdx; index != oldIdx; index += num)
      {
        int order = autoSelectionRuleList[index].Order;
        autoSelectionRuleList[index].Order = autoSelectionRuleList[index + num].Order;
        autoSelectionRuleList[index + num].Order = order;
      }
    }
    finally
    {
      autoSelectionRuleList.Sort((IComparer<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule>) new AutoSelectionRuleComparer());
      List<long> ruleIDs = new List<long>();
      foreach (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule in autoSelectionRuleList)
      {
        if (autoSelectionRule != null)
          ruleIDs.Add(autoSelectionRule.RuleID);
      }
      this.Rule_UpdateInNode(currentImbaseNode, ruleIDs);
      this.tvAutoSelectionItems.Sort();
      this.UpdateSelectionNodeProperties();
    }
  }

  private void Rule_UnregisterInNode(NodeInfo nodeInfo, long ruleId)
  {
    if (nodeInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return;
      List<long> ruleIdList = new List<long>() { ruleId };
      if (nodeInfo is ObjectTypeRec)
        autosServerService.RulesUnregister(ruleIdList, (long) nodeInfo.TypeId, AutoSelectionLinkMode.asotObjectType, sessionKeeper.Session.SessionGUID);
      else
        autosServerService.RulesUnregister(ruleIdList, nodeInfo.ObjectId, AutoSelectionLinkMode.asotImbaseObject, sessionKeeper.Session.SessionGUID);
    }
  }

  private void Rule_RegisterInNode(NodeInfo nodeInfo, List<long> ruleIDs)
  {
    if (nodeInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return;
      if (nodeInfo is ObjectTypeRec)
        autosServerService.RulesRegister(ruleIDs, (long) nodeInfo.TypeId, AutoSelectionLinkMode.asotObjectType, sessionKeeper.Session.SessionGUID);
      else
        autosServerService.RulesRegister(ruleIDs, nodeInfo.ObjectId, AutoSelectionLinkMode.asotImbaseObject, sessionKeeper.Session.SessionGUID);
    }
  }

  private void Rule_RegisterInNode(NodeInfo nodeInfo, long ruleId)
  {
    if (nodeInfo == null)
      return;
    List<long> ruleIDs = new List<long>() { ruleId };
    this.Rule_RegisterInNode(nodeInfo, ruleIDs);
  }

  private void Rule_UpdateInNode(NodeInfo nodeInfo, List<long> ruleIDs)
  {
    if (nodeInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAutoSelectionRuleCacheService autosServerService = AutoSelectionUtils.ServiceKeeper.GetAutosServerService();
      if (autosServerService == null)
        return;
      if (nodeInfo is ObjectTypeRec)
        autosServerService.RulesUpdate(ruleIDs, (long) nodeInfo.TypeId, AutoSelectionLinkMode.asotObjectType, sessionKeeper.Session.SessionGUID);
      else
        autosServerService.RulesUpdate(ruleIDs, nodeInfo.ObjectId, AutoSelectionLinkMode.asotImbaseObject, sessionKeeper.Session.SessionGUID);
    }
  }

  public AutoSelectionTreeSetupForm()
  {
    this.InitializeComponent();
    this.InitializeAccessInfo();
    this.InitializeData();
    this.UpdateControls();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1452);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
      this._treeBuilder?.Dispose();
      this._treeBuilder = (TreeBuilder) null;
    }
    base.Dispose(disposing);
  }

  public bool ReadOnly
  {
    [DebuggerStepThrough] get => this._readOnly;
  }

  private void AutoSelectionTreeSetupForm_Load(object sender, EventArgs e)
  {
    AutoSelectionUtils.Forms.LoadSettings((Form) this);
  }

  private void AutoSelectionTreeSetupForm_Closed(object sender, EventArgs e)
  {
    AutoSelectionUtils.Forms.SaveSettings((Form) this);
  }

  private void tvAutoSelectionItems_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.UpdateSelectionNodeProperties();
  }

  private void tvAutoSelectionItems_DoubleClick(object sender, EventArgs e)
  {
    if (this.ReadOnly)
      return;
    this.RuleEdit();
  }

  private void tvSelectionTypes_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (!this._treeBuilder.UnexploredNode(e.Node))
      return;
    if (e.Node.Level == 0)
      this.FillSelectionTypeNodeList(e.Node);
    else
      this.FillSelectionImbaseNodeList(e.Node);
  }

  private void tvSelectionTypes_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.FillSelectionRules(e.Node?.Tag as NodeInfo);
  }

  private void cmsSelectionItems_Opening(object sender, CancelEventArgs e)
  {
    ObjectTypeRec currentType = this.GetCurrentType();
    NodeInfo currentImbaseNode = this.GetCurrentImbaseNode();
    this.tsmiRuleInclude.Enabled = this.tsmiRuleMove.Enabled = !this.ReadOnly;
    this.tsmiTypeDelete.Enabled = !this.ReadOnly && currentType != null;
    ToolStripMenuItem tsmiRuleNew = this.tsmiRuleNew;
    ToolStripMenuItem tsmiRuleNewType = this.tsmiRuleNewType;
    ToolStripMenuItem tsmiRuleNewImbase = this.tsmiRuleNewImbase;
    ToolStripMenuItem tsmiRuleIncludeType = this.tsmiRuleIncludeType;
    bool flag1;
    this.tsmiRuleIncludeImbase.Enabled = flag1 = !this.ReadOnly && currentImbaseNode != null;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    tsmiRuleIncludeType.Enabled = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    tsmiRuleNewImbase.Enabled = num2 != 0;
    int num3;
    bool flag4 = (num3 = flag3 ? 1 : 0) != 0;
    tsmiRuleNewType.Enabled = num3 != 0;
    int num4 = flag4 ? 1 : 0;
    tsmiRuleNew.Enabled = num4 != 0;
    this.tsmiRuleNewImbase.Enabled = this.tsmiRuleIncludeImbase.Enabled = !this.ReadOnly && this.tsmiRuleNewImbase.Enabled && currentType != null && currentType.HasImbaseCatalogs;
    ToolStripMenuItem tsmiRuleExclude = this.tsmiRuleExclude;
    ToolStripMenuItem tsmiRuleDelete = this.tsmiRuleDelete;
    bool flag5;
    this.tsmiRuleEdit.Enabled = flag5 = !this.ReadOnly && currentImbaseNode != null && this.GetCurrentRule() != null;
    int num5;
    bool flag6 = (num5 = flag5 ? 1 : 0) != 0;
    tsmiRuleDelete.Enabled = num5 != 0;
    int num6 = flag6 ? 1 : 0;
    tsmiRuleExclude.Enabled = num6 != 0;
    this.tsmiRuleTest.Enabled = currentImbaseNode != null && this.tvAutoSelectionItems.Nodes.Count > 0;
    TreeNode selectionRuleNode = this.GetSelectionRuleNode();
    this.tsmiRuleMoveFirst.Enabled = this.tsmiRuleMoveUp.Enabled = !this.ReadOnly && selectionRuleNode != null && selectionRuleNode.Index != 0;
    this.tsmiRuleMoveDown.Enabled = this.tsmiRuleMoveLast.Enabled = !this.ReadOnly && selectionRuleNode != null && selectionRuleNode.Index != this.tvAutoSelectionItems.Nodes.Count - 1;
  }

  private void tsmiRuleNew_Click(object sender, EventArgs e)
  {
  }

  private void tsmiRuleEdit_Click(object sender, EventArgs e) => this.RuleEdit();

  private void tsmiRuleInclude_Click(object sender, EventArgs e)
  {
  }

  private void tsmiRuleExclude_Click(object sender, EventArgs e) => this.RuleExclude();

  private void tsmiRuleDelete_Click(object sender, EventArgs e) => this.RuleDelete();

  private void tsmiRuleTest_Click(object sender, EventArgs e) => this.RulesTest();

  private void tsmiModeAll_Click(object sender, EventArgs e)
  {
  }

  private void tsmiModeWithRule_Click(object sender, EventArgs e)
  {
  }

  private void tsmiModeWithoutRule_Click(object sender, EventArgs e)
  {
  }

  private void tsmiSelItemMoveFirst_Click(object sender, EventArgs e) => this.RuleMoveFirst();

  private void tsmiSelItemMoveUp_Click(object sender, EventArgs e) => this.RuleMoveUp();

  private void tsmiSelItemMoveDown_Click(object sender, EventArgs e) => this.RuleMoveDown();

  private void tsmiSelItemMoveLast_Click(object sender, EventArgs e) => this.RuleMoveLast();

  private void tsmiRuleNewType_Click(object sender, EventArgs e) => this.RuleNewType();

  private void tsmiRuleNewImbase_Click(object sender, EventArgs e) => this.RuleNewImbase();

  private void tsmiRuleIncludeType_Click(object sender, EventArgs e) => this.RuleIncludeType();

  private void tsmiRuleIncludeImbase_Click(object sender, EventArgs e) => this.RuleIncludeImbase();

  private void cmsTypeItems_Opening(object sender, CancelEventArgs e)
  {
    ObjectTypeRec currentType = this.GetCurrentType();
    this.tsmiTypeAdd.Enabled = !this.ReadOnly;
    this.tsmiTypeDelete.Enabled = !this.ReadOnly && currentType != null;
  }

  private void tsmiTypeAdd_Click(object sender, EventArgs e) => this.TypeNew();

  private void tsmiTypeDelete_Click(object sender, EventArgs e) => this.TypeRemove();

  private void tsmiTypeUpdate_Click(object sender, EventArgs e) => this.FillSelectionTypesList();

  private void tscbSelectionTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void tsbtnProperties_Click(object sender, EventArgs e) => this.UpdateControls();

  private void pgSelectionItem_SelectedObjectsChanged(object sender, EventArgs e)
  {
  }

  private void pgSelectionItem_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (!(this.pgSelectionItem.SelectedObject is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule selectedObject) && !this.pgSelectionItem.Enabled)
      return;
    this.RuleUpdate(selectedObject);
  }

  private void toolStripMenuItem1_Click(object sender, EventArgs e)
  {
  }

  private void AutoSelectionTreeSetupForm_Activated(object sender, EventArgs e)
  {
  }
}
