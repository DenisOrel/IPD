// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DemoStend
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.SelectionView;
using Intermech.PropertyEditors;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Cells.Views;
using SourceGrid3.Styles;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for DemoStend.</summary>
public class DemoStend : Form
{
  internal SelFormResult selAttr;
  internal SelFormResult selObjType;
  internal FieldTypes attrType;
  internal bool multi;
  internal SelForm sForm;
  internal CalcTestForm calcForm;
  public bool sortByShort = true;
  private DemoStend.FormulaStru[] curForms;
  private DataTable attrData;
  private DataTable objTypeData;
  private FormEditor formEd;
  private TempFormula formTF;
  private TempFormula condTF;
  private string title = "";
  private bool attrChanged;
  private bool formChanged;
  private bool condChanged;
  internal IView redVisModel;
  internal IView defVisModel;
  internal IView greenVisModel;
  private int loadedIndex = -1;
  private long userID;
  private string userName = "";
  private GroupBox groupAttParms;
  private System.Windows.Forms.Button btnClearAttr;
  private Label AttrTypeLbl;
  private CheckEdit checkObjType;
  private System.Windows.Forms.Button btnObjTree;
  private Label AttrNameLbl;
  private ButtonEdit textObjName;
  private ButtonEdit textAttName;
  private Label label1;
  private ImageList IL;
  private GroupBox gbFormula;
  private RichTextBox richFormula;
  private Label label2;
  private RichTextBox richCond;
  private MenuItem menuItem2;
  private MenuItem menuItem3;
  private System.Windows.Forms.ContextMenu FormContextMenu;
  private System.Windows.Forms.ContextMenu CondContextMenu;
  private MenuItem itemChangeForm;
  private MenuItem itemDeleteForm;
  private MenuItem itemChangeCond;
  private MenuItem itemDeleteCond;
  private MenuItem itemTestForm;
  private MenuItem menuItem4;
  private MenuItem itemTestCond;
  private MenuItem menuItem1;
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.Button button2;
  private System.Windows.Forms.Button button3;
  private System.Windows.Forms.Button button4;
  private System.Windows.Forms.Button button5;
  private System.Windows.Forms.Button button6;
  private GroupBox groupBox1;
  private System.Windows.Forms.Button btnLoad;
  private System.Windows.Forms.Button btnSave;
  private Grid grid;
  private System.Windows.Forms.Button btnNew;
  private IContainer components;

  public DemoStend()
  {
    this.InitializeComponent();
    this.sForm = new SelForm();
    this.formEd = new FormEditor();
    this.formTF = new TempFormula(true);
    this.formTF.Cond = false;
    this.condTF = new TempFormula(true);
    this.condTF.resType = DataType.Boolean;
    this.condTF.Cond = true;
    this.calcForm = new CalcTestForm();
    this.curForms = (DemoStend.FormulaStru[]) null;
    this.textObjName.Text = "";
    this.textAttName.Text = "";
    this.AttrTypeLbl.Text = "";
    this.AttrNameLbl.Text = "";
    this.redVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.redVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
    this.defVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.defVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
    this.greenVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.greenVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
    this.redVisModel.BackColor = Color.White;
    this.redVisModel.ForeColor = Color.Red;
    this.greenVisModel.BackColor = Color.White;
    this.greenVisModel.ForeColor = Color.Green;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public bool Execute()
  {
    this.LoadSessionData();
    this.UpdateGrid();
    return this.ShowDialog() == DialogResult.OK;
  }

  private void LoadSessionData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.attrData = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("");
      this.objTypeData = sessionKeeper.Session.GetObjectTypeCollection(-2).Select("");
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DemoStend));
    this.groupAttParms = new GroupBox();
    this.btnClearAttr = new System.Windows.Forms.Button();
    this.IL = new ImageList(this.components);
    this.AttrTypeLbl = new Label();
    this.checkObjType = new CheckEdit();
    this.btnObjTree = new System.Windows.Forms.Button();
    this.AttrNameLbl = new Label();
    this.textObjName = new ButtonEdit();
    this.textAttName = new ButtonEdit();
    this.label1 = new Label();
    this.gbFormula = new GroupBox();
    this.button4 = new System.Windows.Forms.Button();
    this.button5 = new System.Windows.Forms.Button();
    this.button6 = new System.Windows.Forms.Button();
    this.button3 = new System.Windows.Forms.Button();
    this.button2 = new System.Windows.Forms.Button();
    this.button1 = new System.Windows.Forms.Button();
    this.richCond = new RichTextBox();
    this.CondContextMenu = new System.Windows.Forms.ContextMenu();
    this.itemChangeCond = new MenuItem();
    this.menuItem3 = new MenuItem();
    this.itemTestCond = new MenuItem();
    this.menuItem1 = new MenuItem();
    this.itemDeleteCond = new MenuItem();
    this.label2 = new Label();
    this.richFormula = new RichTextBox();
    this.FormContextMenu = new System.Windows.Forms.ContextMenu();
    this.itemChangeForm = new MenuItem();
    this.menuItem2 = new MenuItem();
    this.itemTestForm = new MenuItem();
    this.menuItem4 = new MenuItem();
    this.itemDeleteForm = new MenuItem();
    this.groupBox1 = new GroupBox();
    this.btnNew = new System.Windows.Forms.Button();
    this.grid = new Grid();
    this.btnLoad = new System.Windows.Forms.Button();
    this.btnSave = new System.Windows.Forms.Button();
    this.groupAttParms.SuspendLayout();
    this.checkObjType.Properties.BeginInit();
    this.textObjName.Properties.BeginInit();
    this.textAttName.Properties.BeginInit();
    this.gbFormula.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.groupAttParms.AccessibleDescription = (string) null;
    this.groupAttParms.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupAttParms, "groupAttParms");
    this.groupAttParms.BackgroundImage = (System.Drawing.Image) null;
    this.groupAttParms.Controls.Add((Control) this.btnClearAttr);
    this.groupAttParms.Controls.Add((Control) this.AttrTypeLbl);
    this.groupAttParms.Controls.Add((Control) this.checkObjType);
    this.groupAttParms.Controls.Add((Control) this.btnObjTree);
    this.groupAttParms.Controls.Add((Control) this.AttrNameLbl);
    this.groupAttParms.Controls.Add((Control) this.textObjName);
    this.groupAttParms.Controls.Add((Control) this.textAttName);
    this.groupAttParms.Controls.Add((Control) this.label1);
    this.groupAttParms.Font = (Font) null;
    this.groupAttParms.Name = "groupAttParms";
    this.groupAttParms.TabStop = false;
    this.btnClearAttr.AccessibleDescription = (string) null;
    this.btnClearAttr.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnClearAttr, "btnClearAttr");
    this.btnClearAttr.BackgroundImage = (System.Drawing.Image) null;
    this.btnClearAttr.Font = (Font) null;
    this.btnClearAttr.ImageList = this.IL;
    this.btnClearAttr.Name = "btnClearAttr";
    this.btnClearAttr.Click += new EventHandler(this.btnClearAttr_Click);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.White;
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
    this.IL.Images.SetKeyName(10, "");
    this.IL.Images.SetKeyName(11, "");
    this.IL.Images.SetKeyName(12, "");
    this.IL.Images.SetKeyName(13, "");
    this.IL.Images.SetKeyName(14, "");
    this.IL.Images.SetKeyName(15, "");
    this.IL.Images.SetKeyName(16 /*0x10*/, "");
    this.AttrTypeLbl.AccessibleDescription = (string) null;
    this.AttrTypeLbl.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.AttrTypeLbl, "AttrTypeLbl");
    this.AttrTypeLbl.BorderStyle = BorderStyle.Fixed3D;
    this.AttrTypeLbl.Font = (Font) null;
    this.AttrTypeLbl.Name = "AttrTypeLbl";
    this.checkObjType.AccessibleDescription = (string) null;
    this.checkObjType.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.checkObjType, "checkObjType");
    this.checkObjType.BackgroundImage = (System.Drawing.Image) null;
    this.checkObjType.Name = "checkObjType";
    this.checkObjType.Properties.Caption = componentResourceManager.GetString("checkObjType.Properties.Caption");
    this.checkObjType.Properties.CheckStyle = CheckStyles.Style5;
    this.checkObjType.CheckedChanged += new EventHandler(this.checkObjType_Click);
    this.btnObjTree.AccessibleDescription = (string) null;
    this.btnObjTree.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnObjTree, "btnObjTree");
    this.btnObjTree.BackgroundImage = (System.Drawing.Image) null;
    this.btnObjTree.Font = (Font) null;
    this.btnObjTree.ImageList = this.IL;
    this.btnObjTree.Name = "btnObjTree";
    this.btnObjTree.Click += new EventHandler(this.btnObjTree_Click);
    this.AttrNameLbl.AccessibleDescription = (string) null;
    this.AttrNameLbl.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.AttrNameLbl, "AttrNameLbl");
    this.AttrNameLbl.BorderStyle = BorderStyle.Fixed3D;
    this.AttrNameLbl.Font = (Font) null;
    this.AttrNameLbl.Name = "AttrNameLbl";
    this.textObjName.AccessibleDescription = (string) null;
    this.textObjName.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textObjName, "textObjName");
    this.textObjName.BackgroundImage = (System.Drawing.Image) null;
    this.textObjName.Name = "textObjName";
    this.textObjName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textObjName.ButtonClick += new ButtonPressedEventHandler(this.textObjName_ButtonClick);
    this.textAttName.AccessibleDescription = (string) null;
    this.textAttName.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textAttName, "textAttName");
    this.textAttName.BackgroundImage = (System.Drawing.Image) null;
    this.textAttName.Name = "textAttName";
    this.textAttName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textAttName.ButtonClick += new ButtonPressedEventHandler(this.textAttName_ButtonClick);
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Font = (Font) null;
    this.label1.Name = "label1";
    this.gbFormula.AccessibleDescription = (string) null;
    this.gbFormula.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gbFormula, "gbFormula");
    this.gbFormula.BackgroundImage = (System.Drawing.Image) null;
    this.gbFormula.Controls.Add((Control) this.button4);
    this.gbFormula.Controls.Add((Control) this.button5);
    this.gbFormula.Controls.Add((Control) this.button6);
    this.gbFormula.Controls.Add((Control) this.button3);
    this.gbFormula.Controls.Add((Control) this.button2);
    this.gbFormula.Controls.Add((Control) this.button1);
    this.gbFormula.Controls.Add((Control) this.richCond);
    this.gbFormula.Controls.Add((Control) this.label2);
    this.gbFormula.Controls.Add((Control) this.richFormula);
    this.gbFormula.Font = (Font) null;
    this.gbFormula.Name = "gbFormula";
    this.gbFormula.TabStop = false;
    this.button4.AccessibleDescription = (string) null;
    this.button4.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.BackgroundImage = (System.Drawing.Image) null;
    this.button4.Font = (Font) null;
    this.button4.Name = "button4";
    this.button4.Click += new EventHandler(this.itemDeleteCond_Click);
    this.button5.AccessibleDescription = (string) null;
    this.button5.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button5, "button5");
    this.button5.BackgroundImage = (System.Drawing.Image) null;
    this.button5.Font = (Font) null;
    this.button5.Name = "button5";
    this.button5.Click += new EventHandler(this.itemTestCond_Click);
    this.button6.AccessibleDescription = (string) null;
    this.button6.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button6, "button6");
    this.button6.BackgroundImage = (System.Drawing.Image) null;
    this.button6.Font = (Font) null;
    this.button6.Name = "button6";
    this.button6.Click += new EventHandler(this.itemChangeCond_Click);
    this.button3.AccessibleDescription = (string) null;
    this.button3.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.BackgroundImage = (System.Drawing.Image) null;
    this.button3.Font = (Font) null;
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.itemDeleteForm_Click);
    this.button2.AccessibleDescription = (string) null;
    this.button2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.BackgroundImage = (System.Drawing.Image) null;
    this.button2.Font = (Font) null;
    this.button2.Name = "button2";
    this.button2.Click += new EventHandler(this.itemTestForm_Click);
    this.button1.AccessibleDescription = (string) null;
    this.button1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.BackgroundImage = (System.Drawing.Image) null;
    this.button1.Font = (Font) null;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.itemChangeForm_Click);
    this.richCond.AccessibleDescription = (string) null;
    this.richCond.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.richCond, "richCond");
    this.richCond.BackgroundImage = (System.Drawing.Image) null;
    this.richCond.ContextMenu = this.CondContextMenu;
    this.richCond.Name = "richCond";
    this.richCond.ReadOnly = true;
    this.CondContextMenu.MenuItems.AddRange(new MenuItem[5]
    {
      this.itemChangeCond,
      this.menuItem3,
      this.itemTestCond,
      this.menuItem1,
      this.itemDeleteCond
    });
    componentResourceManager.ApplyResources((object) this.CondContextMenu, "CondContextMenu");
    componentResourceManager.ApplyResources((object) this.itemChangeCond, "itemChangeCond");
    this.itemChangeCond.Index = 0;
    this.itemChangeCond.Click += new EventHandler(this.itemChangeCond_Click);
    componentResourceManager.ApplyResources((object) this.menuItem3, "menuItem3");
    this.menuItem3.Index = 1;
    componentResourceManager.ApplyResources((object) this.itemTestCond, "itemTestCond");
    this.itemTestCond.Index = 2;
    this.itemTestCond.Click += new EventHandler(this.itemTestCond_Click);
    componentResourceManager.ApplyResources((object) this.menuItem1, "menuItem1");
    this.menuItem1.Index = 3;
    componentResourceManager.ApplyResources((object) this.itemDeleteCond, "itemDeleteCond");
    this.itemDeleteCond.Index = 4;
    this.itemDeleteCond.Click += new EventHandler(this.itemDeleteCond_Click);
    this.label2.AccessibleDescription = (string) null;
    this.label2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Font = (Font) null;
    this.label2.Name = "label2";
    this.richFormula.AccessibleDescription = (string) null;
    this.richFormula.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.richFormula, "richFormula");
    this.richFormula.BackgroundImage = (System.Drawing.Image) null;
    this.richFormula.ContextMenu = this.FormContextMenu;
    this.richFormula.Name = "richFormula";
    this.richFormula.ReadOnly = true;
    this.FormContextMenu.MenuItems.AddRange(new MenuItem[5]
    {
      this.itemChangeForm,
      this.menuItem2,
      this.itemTestForm,
      this.menuItem4,
      this.itemDeleteForm
    });
    componentResourceManager.ApplyResources((object) this.FormContextMenu, "FormContextMenu");
    componentResourceManager.ApplyResources((object) this.itemChangeForm, "itemChangeForm");
    this.itemChangeForm.Index = 0;
    this.itemChangeForm.Click += new EventHandler(this.itemChangeForm_Click);
    componentResourceManager.ApplyResources((object) this.menuItem2, "menuItem2");
    this.menuItem2.Index = 1;
    componentResourceManager.ApplyResources((object) this.itemTestForm, "itemTestForm");
    this.itemTestForm.Index = 2;
    this.itemTestForm.Click += new EventHandler(this.itemTestForm_Click);
    componentResourceManager.ApplyResources((object) this.menuItem4, "menuItem4");
    this.menuItem4.Index = 3;
    componentResourceManager.ApplyResources((object) this.itemDeleteForm, "itemDeleteForm");
    this.itemDeleteForm.Index = 4;
    this.itemDeleteForm.Click += new EventHandler(this.itemDeleteForm_Click);
    this.groupBox1.AccessibleDescription = (string) null;
    this.groupBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.BackgroundImage = (System.Drawing.Image) null;
    this.groupBox1.Controls.Add((Control) this.btnNew);
    this.groupBox1.Controls.Add((Control) this.grid);
    this.groupBox1.Controls.Add((Control) this.btnLoad);
    this.groupBox1.Controls.Add((Control) this.btnSave);
    this.groupBox1.Font = (Font) null;
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.btnNew.AccessibleDescription = (string) null;
    this.btnNew.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnNew, "btnNew");
    this.btnNew.BackgroundImage = (System.Drawing.Image) null;
    this.btnNew.Font = (Font) null;
    this.btnNew.Name = "btnNew";
    this.btnNew.Click += new EventHandler(this.btnSave_Click);
    this.grid.AccessibleDescription = (string) null;
    this.grid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.BackColor = Color.White;
    this.grid.BackgroundImage = (System.Drawing.Image) null;
    this.grid.Font = (Font) null;
    this.grid.GridToolTipActive = true;
    this.grid.Name = "grid";
    this.grid.SpecialKeys = GridSpecialKeys.Default;
    this.grid.StyleGrid = (StyleGrid) null;
    this.btnLoad.AccessibleDescription = (string) null;
    this.btnLoad.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnLoad, "btnLoad");
    this.btnLoad.BackgroundImage = (System.Drawing.Image) null;
    this.btnLoad.Font = (Font) null;
    this.btnLoad.Name = "btnLoad";
    this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
    this.btnSave.AccessibleDescription = (string) null;
    this.btnSave.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnSave, "btnSave");
    this.btnSave.BackgroundImage = (System.Drawing.Image) null;
    this.btnSave.Font = (Font) null;
    this.btnSave.Name = "btnSave";
    this.btnSave.Click += new EventHandler(this.btnCheckInClick);
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (System.Drawing.Image) null;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.gbFormula);
    this.Controls.Add((Control) this.groupAttParms);
    this.Font = (Font) null;
    this.Icon = (Icon) null;
    this.Name = nameof (DemoStend);
    this.Tag = (object) " ";
    this.groupAttParms.ResumeLayout(false);
    this.checkObjType.Properties.EndInit();
    this.textObjName.Properties.EndInit();
    this.textAttName.Properties.EndInit();
    this.gbFormula.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private bool ValidateAttr()
  {
    if (this.selObjType == null && this.checkObjType.Checked)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_97"), LocalizationHolder.rm.GetString("Expert.Editor_98"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    if (this.selAttr != null)
      return true;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_99"), LocalizationHolder.rm.GetString("Expert.Editor_100"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    return false;
  }

  private void UpdateTitle()
  {
    if (!this.attrChanged)
      return;
    StringBuilder stringBuilder = new StringBuilder("\"");
    if (this.textObjName.Text.Trim() != "")
      stringBuilder.Append(this.textObjName.Text.Trim() + ".");
    stringBuilder.Append(this.textAttName.Text + "\" ");
    this.title = stringBuilder.ToString();
    this.attrChanged = false;
  }

  private void PaintCurToken(Token t, RichTextBox memoForm)
  {
    if (t.type != Intermech.Expert.TokenType.FuncCall)
      memoForm.Select(t.StartPos, t.text.Length);
    switch (t.type)
    {
      case Intermech.Expert.TokenType.UnaryOper:
      case Intermech.Expert.TokenType.BinaryOper:
        memoForm.SelectionColor = Color.DarkRed;
        break;
      case Intermech.Expert.TokenType.OpeningBrace:
      case Intermech.Expert.TokenType.ClosingBrace:
        memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.FuncCall:
        memoForm.Select(t.StartPos, t.text.Length - 1);
        memoForm.SelectionColor = Color.Black;
        memoForm.Select(t.StartPos + t.text.Length - 1, 1);
        memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.Integer:
        memoForm.SelectionColor = Color.Indigo;
        break;
      case Intermech.Expert.TokenType.Float:
        memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case Intermech.Expert.TokenType.String:
        memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case Intermech.Expert.TokenType.Date:
        memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case Intermech.Expert.TokenType.ObjectLink:
        memoForm.SelectionColor = Color.Red;
        break;
      default:
        memoForm.SelectionColor = Color.Black;
        break;
    }
  }

  public void ShowFormula(bool Cond)
  {
    TempFormula tempFormula;
    RichTextBox memoForm;
    if (Cond)
    {
      tempFormula = this.condTF;
      memoForm = this.richCond;
    }
    else
    {
      tempFormula = this.formTF;
      memoForm = this.richFormula;
    }
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < tempFormula.Count; ++index)
      stringBuilder.Append(tempFormula[index].text);
    memoForm.Text = stringBuilder.ToString();
    for (int index = 0; index < tempFormula.Count; ++index)
      this.PaintCurToken(tempFormula[index], memoForm);
  }

  private void ChangeFormula()
  {
    if (!this.ValidateAttr())
      return;
    this.formTF.resType = DataTypeConvertor.AttrType2DataType(this.attrType);
    this.UpdateTitle();
    if (!this.formEd.Execute(ref this.formTF, this.title))
      return;
    this.formChanged = true;
    this.ShowFormula(false);
  }

  private void ChangeCond()
  {
    if (!this.ValidateAttr())
      return;
    this.UpdateTitle();
    if (!this.formEd.Execute(ref this.condTF, this.title))
      return;
    this.condChanged = true;
    this.ShowFormula(true);
  }

  private void itemChangeForm_Click(object sender, EventArgs e) => this.ChangeFormula();

  private void itemDeleteForm_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_101"), LocalizationHolder.rm.GetString("Expert.Editor_102"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.formTF.Clear();
    this.richFormula.Clear();
    this.formChanged = true;
  }

  private void itemChangeCond_Click(object sender, EventArgs e) => this.ChangeCond();

  private void itemDeleteCond_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_103"), LocalizationHolder.rm.GetString("Expert.Editor_104"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.condTF.Clear();
    this.richCond.Clear();
    this.condChanged = true;
  }

  private void ReflectAttr()
  {
    if (this.selAttr == null)
    {
      this.textAttName.Text = "";
      this.AttrTypeLbl.Text = "";
      this.AttrNameLbl.Text = "";
    }
    else
    {
      if (this.selAttr.shortName != "")
        this.textAttName.Text = this.selAttr.shortName;
      else
        this.textAttName.Text = this.selAttr.longName;
      if (this.selObjType == null)
      {
        this.checkObjType.Checked = false;
        this.textObjName.Text = "";
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.selAttr.ID);
        this.attrType = attributeType.AttributeType;
        string str = PairName.GetShortFTDescr(this.attrType);
        this.multi = attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
        if (this.multi)
          str = $"{{{str}}}";
        this.AttrTypeLbl.Text = str;
        this.AttrNameLbl.Text = attributeType.Name;
      }
    }
  }

  private void ReflectObjType()
  {
    if (!this.checkObjType.Checked || this.selObjType == null)
    {
      this.textObjName.Text = "";
    }
    else
    {
      if (this.selObjType.shortName != "")
        this.textObjName.Text = this.selObjType.shortName;
      else
        this.textObjName.Text = this.selObjType.longName;
      if (this.selAttr == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.selObjType.ID);
        if (objectType == null || !objectType.HasAttribute(this.selAttr.ID))
          return;
        this.selAttr = (SelFormResult) null;
        this.ReflectAttr();
      }
    }
  }

  private void textObjName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelFormResult selFormResult = this.sForm.Execute(this.objTypeData.DefaultView, false, ref this.sortByShort, this.textObjName.Text.Trim(), false);
    if (selFormResult == null)
      return;
    this.selObjType = selFormResult;
    this.attrChanged = true;
    this.ReflectObjType();
    this.ClearFormulae();
    this.UpdateFormulae();
  }

  private void btnObjTree_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_105"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int int32 = Convert.ToInt32(selectorForm.IDList[0]);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
      if (objectType == null)
        return;
      ObjectTypeProperties propertiesStructure = objectType.PropertiesStructure;
      if (this.selObjType == null)
        this.selObjType = new SelFormResult();
      this.selObjType.ID = int32;
      this.selObjType.GUID = propertiesStructure.ObjectTypeGuid.ToString();
      this.selObjType.longName = propertiesStructure.ObjectInstanceName;
      if (this.selObjType.longName == "")
        this.selObjType.longName = propertiesStructure.ObjectTypeName;
      this.selObjType.shortName = propertiesStructure.ObjectTypeShortName;
      this.attrChanged = true;
      this.ClearFormulae();
      this.UpdateFormulae();
    }
  }

  private void btnClearAttr_Click(object sender, EventArgs e)
  {
    this.selAttr = (SelFormResult) null;
    this.selObjType = (SelFormResult) null;
    this.textObjName.Text = "";
    this.textAttName.Text = "";
    this.attrChanged = true;
    this.ClearFormulae();
    this.UpdateFormulae();
  }

  private void textAttName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    DataView dv = (DataView) null;
    if (this.checkObjType.Checked && this.selObjType != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.selObjType.ID);
        if (objectType != null)
        {
          string orderBy = !this.sortByShort ? "F_NAME" : "F_SHORT_NAME";
          dv = objectType.Attributes.Select(orderBy, (object) "ALL_FIELDS").DefaultView;
        }
      }
    }
    else
      dv = this.attrData.DefaultView;
    if (dv == null)
      return;
    string begStr = this.textAttName.Text.Trim();
    SelFormResult selFormResult = this.sForm.Execute(dv, true, ref this.sortByShort, begStr, false);
    if (selFormResult == null)
      return;
    this.selAttr = selFormResult;
    this.attrChanged = true;
    this.ReflectAttr();
    this.ClearFormulae();
    this.UpdateFormulae();
  }

  private void ClearFormulae()
  {
    this.formTF.Clear();
    this.condTF.Clear();
    this.richFormula.Text = "";
    this.richCond.Text = "";
  }

  private void itemTestForm_Click(object sender, EventArgs e)
  {
    this.calcForm.Execute(ref this.formTF);
  }

  private void itemTestCond_Click(object sender, EventArgs e)
  {
    this.calcForm.Execute(ref this.condTF);
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (!this.formChanged && !this.condChanged)
      return;
    if (this.formTF.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_106"), LocalizationHolder.rm.GetString("Expert.Editor_107"));
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!this.condChanged && !this.formChanged)
          return;
        IExpertFormula expertFormula = (IExpertFormula) sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objFormula).Create();
        if (this.condChanged)
          expertFormula.Cond = this.condTF;
        AttribPair attribPair = new AttribPair(this.selAttr.ID);
        if (this.selObjType != null)
          attribPair.objTypeID = this.selObjType.ID;
        expertFormula.Result = attribPair;
        expertFormula.resAttrGuid = this.selAttr.GUID;
        if (this.selObjType != null)
          expertFormula.resObjTypeGuid = this.selObjType.GUID;
        expertFormula.UpdateObject(this.formTF);
        expertFormula.CommitCreation(true);
      }
    }
  }

  private void UpdateGrid()
  {
    int num = 0;
    if (this.curForms != null)
      num = this.curForms.Length;
    SourceGrid3.Cells.Real.Cell cell1 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_108"));
    cell1.AddController((IController) new Unselectable());
    cell1.AddController((IController) Resizable.ResizeWidth);
    this.grid.Redim(num + 1, 3);
    this.grid.FixedRows = 1;
    this.grid.Selection.SelectionMode = GridSelectionMode.Row;
    this.grid.Selection.FocusBackColor = this.grid.Selection.BackColor;
    this.grid[0, 0] = (ICell) cell1;
    SourceGrid3.Cells.Real.Cell cell2 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_109"));
    cell2.AddController((IController) new Unselectable());
    cell2.AddController((IController) Resizable.ResizeWidth);
    this.grid[0, 1] = (ICell) cell2;
    SourceGrid3.Cells.Real.Cell cell3 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_110"));
    cell3.AddController((IController) new Unselectable());
    cell3.AddController((IController) Resizable.ResizeWidth);
    this.grid[0, 2] = (ICell) cell3;
    this.grid.Columns.AutoSize(false);
    this.grid.Selection.EnableMultiSelection = false;
    this.grid.Columns[2].Width = 60;
    this.grid.Columns[0].Width = (this.grid.Width - 60) / 2 - 1;
    this.grid.Columns[1].Width = this.grid.Width - 2 - this.grid.Columns[0].Width - 60;
    if (this.curForms == null)
      return;
    for (int index = 0; index < num; ++index)
    {
      if (this.curForms[index] != null)
      {
        SourceGrid3.Cells.Real.Cell cell4 = new SourceGrid3.Cells.Real.Cell((object) this.curForms[index].formName);
        cell4.View = this.defVisModel;
        this.grid[index + 1, 0] = (ICell) cell4;
        SourceGrid3.Cells.Real.Cell cell5 = new SourceGrid3.Cells.Real.Cell((object) this.curForms[index].condName);
        cell5.View = this.defVisModel;
        this.grid[index + 1, 1] = (ICell) cell5;
        SourceGrid3.Cells.Real.Cell cell6 = new SourceGrid3.Cells.Real.Cell((object) this.curForms[index].ownerName);
        cell6.View = this.defVisModel;
        this.grid[index + 1, 2] = (ICell) cell6;
      }
    }
  }

  private DemoStend.FormulaStru[] GetExistingFormulae()
  {
    DemoStend.FormulaStru[] existingFormulae = (DemoStend.FormulaStru[]) null;
    string conditionValue1 = "";
    string conditionValue2 = "";
    if (this.selObjType != null)
      conditionValue1 = this.selObjType.GUID;
    if (this.selAttr != null)
      conditionValue2 = this.selAttr.GUID;
    if (conditionValue2 == "")
      return existingFormulae;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.userID = sessionKeeper.Session.UserID;
      this.userName = sessionKeeper.Session.UserName;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objFormula);
      ConditionStructure[] conditions;
      if (conditionValue1 == "")
        conditions = new ConditionStructure[1]
        {
          new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) conditionValue2, LogicalOperators.NONE, 0, false)
        };
      else
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) conditionValue2, LogicalOperators.AND, 0, false),
          new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) conditionValue1, LogicalOperators.NONE, 0, false)
        };
      object[] columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) new Guid(ExpertAttrGUIDs.objectName)
      };
      object[] sortColumns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      SortOrders[] orders = new SortOrders[1]
      {
        SortOrders.ASC
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, sortColumns, orders);
      DataRow[] dataRowArray = objectCollection.Select(paramSet).Select();
      existingFormulae = (DemoStend.FormulaStru[]) Array.CreateInstance(typeof (DemoStend.FormulaStru), dataRowArray.Length);
      int index = 0;
      foreach (DataRow dataRow in dataRowArray)
      {
        existingFormulae[index] = new DemoStend.FormulaStru();
        existingFormulae[index].formID = Convert.ToInt64(dataRow[0]);
        existingFormulae[index].formName = Convert.ToString(dataRow[1]);
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(existingFormulae[index].formID);
        existingFormulae[index].ownerID = dbObject1.CheckoutBy;
        if (existingFormulae[index].ownerID != 0L)
          existingFormulae[index].ownerName = sessionKeeper.Session.GetObject(existingFormulae[index].ownerID).Caption;
        existingFormulae[index].condID = dbObject1.GetAttributeByID(ExpertConsts.Consts.attrCondObj).AsInteger;
        if (existingFormulae[index].condID != 0L)
        {
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(existingFormulae[index].condID);
          existingFormulae[index].condName = dbObject2.GetAttributeByID(ExpertConsts.Consts.attrObjectName).AsString;
        }
        ++index;
      }
    }
    return existingFormulae;
  }

  private void UpdateFormulae()
  {
    this.curForms = this.selAttr == null || this.selAttr.ID == 0 ? (DemoStend.FormulaStru[]) null : this.GetExistingFormulae();
    this.UpdateGrid();
  }

  private void checkObjType_Click(object sender, EventArgs e)
  {
    this.textObjName.Enabled = this.checkObjType.Checked;
    this.btnObjTree.Enabled = this.checkObjType.Checked;
    this.ReflectObjType();
    if (this.selObjType == null || this.selObjType.ID == 0)
      return;
    this.UpdateFormulae();
  }

  private void LoadFormula()
  {
    if (this.loadedIndex < 0)
      return;
    long formId = this.curForms[this.loadedIndex].formID;
    long condId = this.curForms[this.loadedIndex].condID;
    this.condTF.Clear();
    this.formTF.Clear();
    this.richFormula.Clear();
    this.richCond.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IExpertFormula expertFormula = (IExpertFormula) sessionKeeper.Session.GetObject(formId);
      expertFormula.Load();
      this.formTF = expertFormula.GetTempFormula();
      this.formTF.UpdateTokenBegs();
      if (this.curForms[this.loadedIndex].ownerID != this.userID)
        expertFormula.CheckOut();
      this.ShowFormula(false);
      if (condId != 0L)
      {
        IExpertCond expertCond = (IExpertCond) sessionKeeper.Session.GetObject(condId);
        expertCond.Load();
        this.condTF = expertCond.GetTempFormula();
        this.condTF.UpdateTokenBegs();
        if (this.curForms[this.loadedIndex].ownerID != this.userID)
          expertCond.CheckOut();
        this.ShowFormula(true);
      }
    }
    if (this.curForms[this.loadedIndex].ownerID == this.userID)
      return;
    this.curForms[this.loadedIndex].ownerID = this.userID;
    this.curForms[this.loadedIndex].ownerName = this.userName;
  }

  private void MarkRow(int Index, int State)
  {
    if (Index < 0)
      return;
    IView view = (IView) null;
    switch (State)
    {
      case 0:
        view = this.defVisModel;
        break;
      case 1:
        view = this.greenVisModel;
        break;
      case 2:
        view = this.redVisModel;
        break;
    }
    for (int col = 0; col < this.grid.ColumnsCount; ++col)
      this.grid[Index + 1, col].View = view;
  }

  private void btnLoad_Click(object sender, EventArgs e)
  {
    int index = this.grid.Selection.GetRowsIndex()[0];
    if (index < 0)
      return;
    if (this.curForms[index].ownerID != 0L && this.curForms[index].ownerID != this.userID)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_111"), LocalizationHolder.rm.GetString("Expert.Editor_112"));
    }
    if (this.loadedIndex == index)
      return;
    this.loadedIndex = index;
    this.LoadFormula();
    this.MarkRow(this.loadedIndex, 1);
  }

  private void btnCheckInClick(object sender, EventArgs e)
  {
    if (this.loadedIndex < 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long formId = this.curForms[this.loadedIndex].formID;
      long condId = this.curForms[this.loadedIndex].condID;
      IExpertFormula expertFormula = (IExpertFormula) sessionKeeper.Session.GetObject(formId);
      expertFormula.UpdateObject(this.formTF);
      IExpertCond expertCond = (IExpertCond) sessionKeeper.Session.GetObject(condId);
      expertCond.UpdateObject(this.condTF);
      expertCond.CheckIn();
      expertFormula.CheckIn();
    }
  }

  private class FormulaStru
  {
    public long formID;
    public long condID;
    public long ownerID;
    public string ownerName = "";
    public string formName = "";
    public string condName = "";
  }
}
