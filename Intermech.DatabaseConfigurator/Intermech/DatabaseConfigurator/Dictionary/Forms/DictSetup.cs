// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Dictionary.Forms.DictSetup
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Dictionary;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Dictionary.Forms;

internal class DictSetup : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private Panel pMain;
  private ListBox lbLang;
  private Panel pLang;
  private TreeView tvWords;
  private ListBox lbRules;
  private Label label1;
  private Label label2;
  private Panel pRuleSetup;
  private Label label3;
  private Panel pRuleCommon;
  private Label label4;
  private System.Windows.Forms.ComboBox cbVop;
  private Label label5;
  private CalcEdit ceVop;
  private Label label6;
  private Label label7;
  private CalcEdit ceValue1;
  private CalcEdit ceValue2;
  private Label label8;
  private Panel pOptions;
  private ContextMenu cWord;
  private ContextMenu cRule;
  private MenuItem miAddWord;
  private MenuItem miDeleteWord;
  private MenuItem miEditWord;
  private MenuItem miS1;
  private MenuItem miAddExt;
  private MenuItem miDeleteExt;
  private MenuItem miEditExt;
  private MenuItem miAddRule;
  private MenuItem miDeleteRule;
  private System.Windows.Forms.ComboBox cbRop;
  private Button bPreview;
  private System.ComponentModel.Container components;
  private Panel panel1;
  private Label label9;
  private MenuItem menuItem2;
  private MenuItem miSort;
  private System.IServiceProvider _serviceProvider;
  private int selectedLangIndex = -1;

  public DictSetup(System.IServiceProvider serviceProvider)
  {
    this.InitializeComponent();
    this._serviceProvider = serviceProvider;
    if (this._serviceProvider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service)
      service.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_33"), (IPropertyPage) this);
    this.Cancel();
    foreach (FieldInfo field in typeof (DictVOP).GetFields())
    {
      string caption = EnumTypeHelper.GetCaption((Enum) (DictVOP) field.GetValue((object) DictVOP.Value));
      if (!this.cbVop.Items.Contains((object) caption))
        this.cbVop.Items.Add((object) caption);
    }
    this.cbVop.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) DictVOP.Value);
    foreach (FieldInfo field in typeof (DictROP).GetFields())
    {
      string caption = EnumTypeHelper.GetCaption((Enum) (DictROP) field.GetValue((object) DictROP.Equal));
      if (!this.cbRop.Items.Contains((object) caption))
        this.cbRop.Items.Add((object) caption);
    }
    this.cbRop.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) DictROP.Equal);
  }

  public LangHelper[] Result
  {
    get
    {
      LangHelper[] result = new LangHelper[this.lbLang.Items.Count];
      for (int index = 0; index < this.lbLang.Items.Count; ++index)
        result[index] = this.lbLang.Items[index] as LangHelper;
      return result;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void Advise()
  {
    this.cbVop.SelectedIndexChanged += new EventHandler(this.cbVop_SelectedIndexChanged);
    this.cbRop.SelectedIndexChanged += new EventHandler(this.cbRop_SelectedIndexChanged);
    this.ceVop.TextChanged += new EventHandler(this._TextChanged);
    this.ceValue1.TextChanged += new EventHandler(this._TextChanged);
    this.ceValue2.TextChanged += new EventHandler(this._TextChanged);
  }

  private void UnAdvise()
  {
    this.cbVop.SelectedIndexChanged -= new EventHandler(this.cbVop_SelectedIndexChanged);
    this.cbRop.SelectedIndexChanged -= new EventHandler(this.cbRop_SelectedIndexChanged);
    this.ceVop.TextChanged -= new EventHandler(this._TextChanged);
    this.ceValue1.TextChanged -= new EventHandler(this._TextChanged);
    this.ceValue2.TextChanged -= new EventHandler(this._TextChanged);
  }

  private void FillRule(DictRule rule)
  {
    this.UnAdvise();
    this.cbVop.Text = EnumTypeHelper.GetCaption((Enum) rule.VOP);
    this.ceVop.Value = (Decimal) rule.VOPValue;
    this.ceVop.Enabled = rule.VOP.Equals((object) DictVOP.Div) || rule.VOP.Equals((object) DictVOP.Mod);
    this.cbRop.Text = EnumTypeHelper.GetCaption((Enum) rule.ROP);
    this.ceValue1.Value = (Decimal) rule.ROPValue1;
    this.ceValue2.Value = (Decimal) rule.ROPValue2;
    CalcEdit ceValue2 = this.ceValue2;
    DictROP rop = rule.ROP;
    int num;
    if (!rop.Equals((object) DictROP.In))
    {
      rop = rule.ROP;
      num = rop.Equals((object) DictROP.NotIn) ? 1 : 0;
    }
    else
      num = 1;
    ceValue2.Enabled = num != 0;
    this.Advise();
  }

  private void lbRulesUpdate(DictRule rule)
  {
    this.lbRules.SelectedIndexChanged -= new EventHandler(this.lbRules_SelectedIndexChanged);
    this.lbRules.BeginUpdate();
    try
    {
      int selectedIndex = this.lbRules.SelectedIndex;
      this.lbRules.Items.RemoveAt(selectedIndex);
      this.lbRules.Items.Insert(selectedIndex, (object) rule);
      this.lbRules.SelectedItem = (object) rule;
    }
    finally
    {
      this.lbRules.EndUpdate();
      this.lbRules.SelectedIndexChanged += new EventHandler(this.lbRules_SelectedIndexChanged);
    }
    this.OnChanged();
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DictSetup));
    this.bPreview = new Button();
    this.pMain = new Panel();
    this.pLang = new Panel();
    this.pRuleSetup = new Panel();
    this.pOptions = new Panel();
    this.ceValue2 = new CalcEdit();
    this.label8 = new Label();
    this.ceValue1 = new CalcEdit();
    this.label7 = new Label();
    this.ceVop = new CalcEdit();
    this.label6 = new Label();
    this.pRuleCommon = new Panel();
    this.cbRop = new System.Windows.Forms.ComboBox();
    this.label5 = new Label();
    this.cbVop = new System.Windows.Forms.ComboBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.lbRules = new ListBox();
    this.cRule = new ContextMenu();
    this.miAddRule = new MenuItem();
    this.miDeleteRule = new MenuItem();
    this.label2 = new Label();
    this.tvWords = new TreeView();
    this.cWord = new ContextMenu();
    this.miAddWord = new MenuItem();
    this.miEditWord = new MenuItem();
    this.miDeleteWord = new MenuItem();
    this.miS1 = new MenuItem();
    this.miAddExt = new MenuItem();
    this.miEditExt = new MenuItem();
    this.miDeleteExt = new MenuItem();
    this.menuItem2 = new MenuItem();
    this.miSort = new MenuItem();
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.lbLang = new ListBox();
    this.label9 = new Label();
    this.pMain.SuspendLayout();
    this.pLang.SuspendLayout();
    this.pRuleSetup.SuspendLayout();
    this.pOptions.SuspendLayout();
    this.ceValue2.Properties.BeginInit();
    this.ceValue1.Properties.BeginInit();
    this.ceVop.Properties.BeginInit();
    this.pRuleCommon.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bPreview, "bPreview");
    this.bPreview.Name = "bPreview";
    this.bPreview.Click += new EventHandler(this.bPreview_Click);
    this.pMain.Controls.Add((System.Windows.Forms.Control) this.pLang);
    this.pMain.Controls.Add((System.Windows.Forms.Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.pMain, "pMain");
    this.pMain.Name = "pMain";
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.pRuleSetup);
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.lbRules);
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.tvWords);
    this.pLang.Controls.Add((System.Windows.Forms.Control) this.label1);
    componentResourceManager.ApplyResources((object) this.pLang, "pLang");
    this.pLang.Name = "pLang";
    this.pRuleSetup.Controls.Add((System.Windows.Forms.Control) this.pOptions);
    this.pRuleSetup.Controls.Add((System.Windows.Forms.Control) this.pRuleCommon);
    componentResourceManager.ApplyResources((object) this.pRuleSetup, "pRuleSetup");
    this.pRuleSetup.Name = "pRuleSetup";
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.ceValue2);
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.label8);
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.ceValue1);
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.label7);
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.ceVop);
    this.pOptions.Controls.Add((System.Windows.Forms.Control) this.label6);
    componentResourceManager.ApplyResources((object) this.pOptions, "pOptions");
    this.pOptions.Name = "pOptions";
    componentResourceManager.ApplyResources((object) this.ceValue2, "ceValue2");
    this.ceValue2.Name = "ceValue2";
    this.ceValue2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.ceValue2.TextChanged += new EventHandler(this._TextChanged);
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.ceValue1, "ceValue1");
    this.ceValue1.Name = "ceValue1";
    this.ceValue1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.ceValue1.TextChanged += new EventHandler(this._TextChanged);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.ceVop, "ceVop");
    this.ceVop.Name = "ceVop";
    this.ceVop.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.ceVop.TextChanged += new EventHandler(this._TextChanged);
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    this.pRuleCommon.Controls.Add((System.Windows.Forms.Control) this.cbRop);
    this.pRuleCommon.Controls.Add((System.Windows.Forms.Control) this.label5);
    this.pRuleCommon.Controls.Add((System.Windows.Forms.Control) this.cbVop);
    this.pRuleCommon.Controls.Add((System.Windows.Forms.Control) this.label4);
    componentResourceManager.ApplyResources((object) this.pRuleCommon, "pRuleCommon");
    this.pRuleCommon.Name = "pRuleCommon";
    componentResourceManager.ApplyResources((object) this.cbRop, "cbRop");
    this.cbRop.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRop.Name = "cbRop";
    this.cbRop.SelectedIndexChanged += new EventHandler(this.cbRop_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.cbVop, "cbVop");
    this.cbVop.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbVop.Name = "cbVop";
    this.cbVop.SelectedIndexChanged += new EventHandler(this.cbVop_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.lbRules.ContextMenu = this.cRule;
    componentResourceManager.ApplyResources((object) this.lbRules, "lbRules");
    this.lbRules.Name = "lbRules";
    this.lbRules.SelectedIndexChanged += new EventHandler(this.lbRules_SelectedIndexChanged);
    this.cRule.MenuItems.AddRange(new MenuItem[2]
    {
      this.miAddRule,
      this.miDeleteRule
    });
    this.cRule.Popup += new EventHandler(this.cRule_Popup);
    this.miAddRule.Index = 0;
    componentResourceManager.ApplyResources((object) this.miAddRule, "miAddRule");
    this.miAddRule.Click += new EventHandler(this.miAddRule_Click);
    this.miDeleteRule.Index = 1;
    componentResourceManager.ApplyResources((object) this.miDeleteRule, "miDeleteRule");
    this.miDeleteRule.Click += new EventHandler(this.miDeleteRule_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.tvWords.ContextMenu = this.cWord;
    componentResourceManager.ApplyResources((object) this.tvWords, "tvWords");
    this.tvWords.FullRowSelect = true;
    this.tvWords.HideSelection = false;
    this.tvWords.Name = "tvWords";
    this.tvWords.AfterLabelEdit += new NodeLabelEditEventHandler(this.tvWords_AfterLabelEdit);
    this.tvWords.AfterSelect += new TreeViewEventHandler(this.tvWords_AfterSelect);
    this.tvWords.MouseDown += new MouseEventHandler(this.tvWords_MouseDown);
    this.cWord.MenuItems.AddRange(new MenuItem[9]
    {
      this.miAddWord,
      this.miEditWord,
      this.miDeleteWord,
      this.miS1,
      this.miAddExt,
      this.miEditExt,
      this.miDeleteExt,
      this.menuItem2,
      this.miSort
    });
    this.cWord.Popup += new EventHandler(this.cWord_Popup);
    this.miAddWord.Index = 0;
    componentResourceManager.ApplyResources((object) this.miAddWord, "miAddWord");
    this.miAddWord.Click += new EventHandler(this.miAddWord_Click);
    this.miEditWord.Index = 1;
    componentResourceManager.ApplyResources((object) this.miEditWord, "miEditWord");
    this.miEditWord.Click += new EventHandler(this.miEditWord_Click);
    this.miDeleteWord.Index = 2;
    componentResourceManager.ApplyResources((object) this.miDeleteWord, "miDeleteWord");
    this.miDeleteWord.Click += new EventHandler(this.miDeleteWord_Click);
    this.miS1.Index = 3;
    componentResourceManager.ApplyResources((object) this.miS1, "miS1");
    this.miAddExt.Index = 4;
    componentResourceManager.ApplyResources((object) this.miAddExt, "miAddExt");
    this.miAddExt.Click += new EventHandler(this.miAddExt_Click);
    this.miEditExt.Index = 5;
    componentResourceManager.ApplyResources((object) this.miEditExt, "miEditExt");
    this.miEditExt.Click += new EventHandler(this.miEditExt_Click);
    this.miDeleteExt.Index = 6;
    componentResourceManager.ApplyResources((object) this.miDeleteExt, "miDeleteExt");
    this.miDeleteExt.Click += new EventHandler(this.miDeleteExt_Click);
    this.menuItem2.Index = 7;
    componentResourceManager.ApplyResources((object) this.menuItem2, "menuItem2");
    this.miSort.Index = 8;
    componentResourceManager.ApplyResources((object) this.miSort, "miSort");
    this.miSort.Click += new EventHandler(this.miSort_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.bPreview);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.lbLang);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.label9);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.lbLang, "lbLang");
    this.lbLang.Name = "lbLang";
    this.lbLang.SelectedIndexChanged += new EventHandler(this.lbLang_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    this.Controls.Add((System.Windows.Forms.Control) this.pMain);
    this.Name = nameof (DictSetup);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) " ";
    this.pMain.ResumeLayout(false);
    this.pLang.ResumeLayout(false);
    this.pRuleSetup.ResumeLayout(false);
    this.pOptions.ResumeLayout(false);
    this.ceValue2.Properties.EndInit();
    this.ceValue1.Properties.EndInit();
    this.ceVop.Properties.EndInit();
    this.pRuleCommon.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void cbVop_SelectedIndexChanged(object sender, EventArgs e)
  {
    DictVOP enumValue = (DictVOP) EnumTypeHelper.GetEnumValue(typeof (DictVOP), this.cbVop.Text, (object) DictVOP.Value);
    this.ceVop.Value = 0M;
    this.ceVop.Enabled = enumValue.Equals((object) DictVOP.Div) || enumValue.Equals((object) DictVOP.Mod);
    if (this.lbRules.SelectedIndex < 0)
      return;
    (this.lbRules.SelectedItem as DictRule).VOP = (DictVOP) EnumTypeHelper.GetEnumValue(typeof (DictVOP), this.cbVop.Text, (object) DictVOP.Value);
    this.lbRulesUpdate(this.lbRules.SelectedItem as DictRule);
  }

  private void cbRop_SelectedIndexChanged(object sender, EventArgs e)
  {
    DictROP enumValue = (DictROP) EnumTypeHelper.GetEnumValue(typeof (DictROP), this.cbRop.Text, (object) DictROP.Equal);
    this.ceValue1.Value = 0M;
    this.ceValue2.Value = 0M;
    this.ceValue2.Enabled = enumValue.Equals((object) DictROP.In) || enumValue.Equals((object) DictROP.NotIn);
    if (this.lbRules.SelectedIndex < 0)
      return;
    (this.lbRules.SelectedItem as DictRule).ROP = (DictROP) EnumTypeHelper.GetEnumValue(typeof (DictROP), this.cbRop.Text, (object) DictROP.Equal);
    this.lbRulesUpdate(this.lbRules.SelectedItem as DictRule);
  }

  private void lbLang_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lbLang.SelectedIndex < 0)
      return;
    if (this.selectedLangIndex != this.lbLang.SelectedIndex)
    {
      this.tvWords.Enabled = true;
      this.tvWords.BeginUpdate();
      try
      {
        this.tvWords.Nodes.Clear();
        foreach (DictWord word in (this.lbLang.SelectedItem as LangHelper).Words)
        {
          TreeNode node = new TreeNode(word.ToString());
          node.Tag = (object) word;
          this.tvWords.Nodes.Add(node);
          foreach (DictEnding ending in word.Endings)
            node.Nodes.Add(new TreeNode(ending.ToString())
            {
              Tag = (object) ending
            });
        }
      }
      finally
      {
        this.tvWords.EndUpdate();
      }
      this.tvWords.SelectedNode = (TreeNode) null;
      this.tvWords_AfterSelect((object) null, (TreeViewEventArgs) null);
    }
    this.selectedLangIndex = this.lbLang.SelectedIndex;
  }

  private void tvWords_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.bPreview.Enabled = false;
    this.lbRules.Enabled = false;
    this.lbRules.Items.Clear();
    if (this.tvWords.SelectedNode != null && this.tvWords.SelectedNode.Tag != null)
    {
      if (this.tvWords.SelectedNode.Tag is DictWord)
        this.bPreview.Enabled = true;
      else if (this.tvWords.SelectedNode.Tag is DictEnding)
      {
        this.lbRules.Enabled = true;
        DictEnding tag = this.tvWords.SelectedNode.Tag as DictEnding;
        this.lbRules.BeginUpdate();
        try
        {
          this.lbRules.Items.Clear();
          foreach (object rule in tag.Rules)
            this.lbRules.Items.Add(rule);
          if (this.lbRules.Items.Count > 0)
            this.lbRules.SelectedIndex = 0;
        }
        finally
        {
          this.lbRules.EndUpdate();
        }
      }
    }
    this.lbRules_SelectedIndexChanged((object) null, (EventArgs) null);
  }

  private void lbRules_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lbRules.SelectedIndex >= 0)
    {
      this.pRuleCommon.Enabled = this.ceValue1.Enabled = true;
      this.FillRule(this.lbRules.SelectedItem as DictRule);
    }
    else
    {
      this.pRuleCommon.Enabled = this.ceValue1.Enabled = false;
      this.FillRule(new DictRule());
    }
  }

  private void cWord_Popup(object sender, EventArgs e)
  {
    this.miAddWord.Enabled = true;
    this.miEditWord.Enabled = false;
    this.miDeleteWord.Enabled = false;
    this.miAddExt.Enabled = false;
    this.miEditExt.Enabled = false;
    this.miDeleteExt.Enabled = false;
    this.miSort.Enabled = false;
    if ((this.tvWords.SelectedNode == null ? 0 : (this.tvWords.SelectedNode.Tag != null ? 1 : 0)) == 0)
      return;
    this.miAddExt.Enabled = true;
    this.miDeleteWord.Enabled = true;
    if (this.tvWords.SelectedNode.Tag is DictWord)
    {
      this.miEditWord.Enabled = true;
      if (this.tvWords.SelectedNode.Nodes == null || this.tvWords.SelectedNode.Nodes.Count <= 1)
        return;
      this.miSort.Enabled = true;
    }
    else
    {
      if (!(this.tvWords.SelectedNode.Tag is DictEnding))
        return;
      this.miEditExt.Enabled = true;
      this.miDeleteExt.Enabled = true;
    }
  }

  private void miAddWord_Click(object sender, EventArgs e)
  {
    DictWord dictWord = new DictWord();
    TreeNode node = new TreeNode(dictWord.ToString());
    node.Tag = (object) dictWord;
    this.tvWords.Nodes.Add(node);
    (this.lbLang.SelectedItem as LangHelper).Words.Add(dictWord);
    this.tvWords.SelectedNode = node;
    this.tvWords.LabelEdit = true;
    this.tvWords.SelectedNode.BeginEdit();
    this.OnChanged();
  }

  private void miEditWord_Click(object sender, EventArgs e)
  {
    this.tvWords.LabelEdit = true;
    this.tvWords.SelectedNode.BeginEdit();
    this.OnChanged();
  }

  private void miDeleteWord_Click(object sender, EventArgs e)
  {
    if (this.tvWords.SelectedNode.Tag.GetType().Equals(typeof (DictEnding)))
      this.tvWords.SelectedNode = this.tvWords.SelectedNode.Parent;
    (this.lbLang.SelectedItem as LangHelper).Words.Remove((DictWord) this.tvWords.SelectedNode.Tag);
    this.tvWords.SelectedNode.Remove();
    if (this.tvWords.SelectedNode != null)
      this.bPreview.Enabled = true;
    else
      this.bPreview.Enabled = false;
    this.OnChanged();
  }

  private void miAddExt_Click(object sender, EventArgs e)
  {
    TreeNode treeNode = (TreeNode) null;
    if (this.tvWords.SelectedNode.Tag is DictWord)
      treeNode = this.tvWords.SelectedNode;
    else if (this.tvWords.SelectedNode.Tag is DictEnding)
      treeNode = this.tvWords.SelectedNode.Parent;
    if (treeNode == null)
      return;
    DictEnding dictEnding = new DictEnding();
    TreeNode node = new TreeNode(dictEnding.ToString());
    node.Tag = (object) dictEnding;
    treeNode.Nodes.Add(node);
    (treeNode.Tag as DictWord).Endings.Add(dictEnding);
    this.tvWords.SelectedNode = node;
    this.tvWords.LabelEdit = true;
    this.tvWords.SelectedNode.BeginEdit();
    this.OnChanged();
  }

  private void miEditExt_Click(object sender, EventArgs e)
  {
    this.tvWords.LabelEdit = true;
    this.tvWords.SelectedNode.BeginEdit();
    this.OnChanged();
  }

  private void miDeleteExt_Click(object sender, EventArgs e)
  {
    (this.tvWords.SelectedNode.Parent.Tag as DictWord).Endings.Remove((DictEnding) this.tvWords.SelectedNode.Tag);
    this.tvWords.SelectedNode.Remove();
    this.OnChanged();
  }

  private void cRule_Popup(object sender, EventArgs e)
  {
    this.miDeleteRule.Enabled = this.lbRules.SelectedIndex >= 0;
  }

  private void miAddRule_Click(object sender, EventArgs e)
  {
    DictEnding tag = this.tvWords.SelectedNode.Tag as DictEnding;
    DictRule dictRule = new DictRule();
    this.lbRules.Items.Add((object) dictRule);
    tag.Rules.Add(dictRule);
    this.lbRules.SelectedItem = (object) dictRule;
    this.OnChanged();
  }

  private void miDeleteRule_Click(object sender, EventArgs e)
  {
    (this.tvWords.SelectedNode.Tag as DictEnding).Rules.Remove(this.lbRules.SelectedItem as DictRule);
    this.lbRules.Items.RemoveAt(this.lbRules.SelectedIndex);
    this.OnChanged();
  }

  private void _TextChanged(object sender, EventArgs e)
  {
    if (this.lbRules.SelectedIndex < 0)
      return;
    DictRule selectedItem = this.lbRules.SelectedItem as DictRule;
    if (sender.Equals((object) this.ceVop))
      selectedItem.VOPValue = (long) this.ceVop.Value;
    else if (sender.Equals((object) this.ceValue1))
      selectedItem.ROPValue1 = (long) this.ceValue1.Value;
    else if (sender.Equals((object) this.ceValue2))
      selectedItem.ROPValue2 = (long) this.ceValue2.Value;
    this.lbRulesUpdate(selectedItem);
  }

  private void tvWords_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
  {
    this.tvWords.LabelEdit = false;
    if (e.CancelEdit)
      return;
    if (e.Node.Tag is DictWord)
    {
      if (string.IsNullOrEmpty(e.Label))
        this.miDeleteWord_Click(sender, (EventArgs) e);
      else
        (e.Node.Tag as DictWord).Word = e.Label;
    }
    if (!(e.Node.Tag is DictEnding))
      return;
    (e.Node.Tag as DictEnding).Ending = e.Label == null ? string.Empty : e.Label;
  }

  private void tvWords_MouseDown(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    this.tvWords.SelectedNode = this.tvWords.GetNodeAt(e.X, e.Y);
  }

  private void bPreview_Click(object sender, EventArgs e)
  {
    DictWord tag = this.tvWords.SelectedNode.Tag as DictWord;
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < 26; ++index)
      arrayList.Add((object) $"{Convert.ToString(index)} {ExtFinder.GetString((long) index, tag)}");
    int num = (int) MessageBox.Show(string.Join(", ", arrayList.ToArray(typeof (string)) as string[]), LocalizationHolder.rm.GetString("DatabaseConfigurator_34"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public void Cancel()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDictionaryService)) is IDictionaryService customService))
        return;
      LangHelper[] langHelperArray = customService.LoadLanguages(sessionKeeper.Session.SessionGUID);
      this.lbLang.BeginUpdate();
      try
      {
        this.lbLang.Items.Clear();
        foreach (LangHelper langHelper in langHelperArray)
        {
          this.lbLang.Items.Add((object) langHelper);
          if (langHelper.Default.Equals(1))
            this.lbLang.SelectedItem = (object) langHelper;
        }
      }
      finally
      {
        this.lbLang.EndUpdate();
      }
    }
  }

  public object Control => (object) this;

  public void Apply()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDictionaryService)) is IDictionaryService customService))
        return;
      customService.SaveLanguages(this.Result, sessionKeeper.Session.SessionGUID);
    }
  }

  public PropertyPageType Type => PropertyPageType.Control;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_35");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public event EventHandler Changed;

  public string HelpTopicID => "1665";

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  private void miSort_Click(object sender, EventArgs e)
  {
    if (!(this.tvWords.SelectedNode.Tag is DictWord))
      return;
    DictWord tag = this.tvWords.SelectedNode.Tag as DictWord;
    tag.Endings.Sort();
    this.tvWords.BeginUpdate();
    try
    {
      this.tvWords.SelectedNode.Nodes.Clear();
      foreach (DictEnding ending in tag.Endings)
        this.tvWords.SelectedNode.Nodes.Add(new TreeNode(ending.ToString())
        {
          Tag = (object) ending
        });
    }
    finally
    {
      this.tvWords.EndUpdate();
    }
    this.OnChanged();
  }
}
