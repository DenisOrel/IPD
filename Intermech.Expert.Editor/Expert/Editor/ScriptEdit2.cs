// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ScriptEdit2
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Bars;
using Intermech.ButtonsPanel;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Remoting.Sponsors;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Styles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// This is Script Editor that is used for editing expert system scripts and functions
/// as well as document generation scripts
/// </summary>
public class ScriptEdit2 : Form, IView
{
  private DockManager dockMan;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private DropDownMenuItem addMenu;
  private ButtonItem btnChange;
  private ButtonItem btnApply;
  private DropDownMenuItem clipMenu;
  private MenuButtonItem cmdInsBefore;
  private MenuButtonItem cmdInsAfter;
  private MenuButtonItem cmdInsInto;
  private MenuButtonItem cmdCut;
  private MenuButtonItem cmdCopy;
  private MenuButtonItem cmdPaste;
  private ButtonItem cmdDelete;
  private DockControl dockOpMod;
  private DockControl dockModParms;
  private DockControl dockOpParms;
  private DockControl dockDesc;
  private Panel panModParms1;
  private RichTextBox richTextBox1;
  private Label label9;
  private Panel panModParmsEmpty;
  private Panel panModParms2;
  private Label label14;
  private Panel panModParms3;
  private Label label11;
  private Panel panModParms4;
  private RichTextBox richTextBox2;
  private Label label10;
  private GroupBox gbResType;
  private Panel panModParms5;
  private Panel panOpParmsEmpty;
  private Panel panOpParms1;
  private System.Windows.Forms.Button btnAddObjType;
  private System.Windows.Forms.Button button3;
  private ButtonEdit btnEdExcerpt;
  private RichTextBox richCond;
  private Label label2;
  private Label label1;
  private ImageList IL2;
  private ContextMenuStrip treePopMenu;
  private ToolStripMenuItem menuInsBefore;
  private ToolStripMenuItem menuInsAfter;
  private ToolStripMenuItem menuInsInto;
  private ToolStripMenuItem menuChange;
  private ToolStripMenuItem menuApply;
  private ToolStripMenuItem menuCut;
  private ToolStripMenuItem menuCopy;
  private ToolStripMenuItem menuPaste;
  private ToolStripMenuItem menuDelete;
  private ContextMenuStrip modPopMenu;
  private ToolStripMenuItem menuChangeModForm;
  private ContextMenuStrip groupMenu;
  private ToolStripMenuItem menuItem2;
  private ContextMenuStrip sortMenu;
  private ToolStripMenuItem menuItem1;
  private ContextMenuStrip opPopMenu;
  private ToolStripMenuItem menuChangeOpForm;
  private ToolTip toolTip2;
  private ImageList IL;
  private Panel panOpParms2;
  private RichTextBox richTextCond;
  private Label label3;
  private Panel panOpParms3;
  private Panel panOpParmsTI;
  private Panel panOpParmsStyleB;
  private Panel panOpParmsStyleC;
  private SelObjAttrControl settAttr;
  private Panel panOpParms4;
  private Panel panOpParms5;
  private Label label7;
  private ButtonEdit edSaveNewID;
  private Panel panOpParms6;
  private GroupBox gbIdent;
  private ButtonEdit edSelTemplate;
  private RichTextBox richTextID;
  private Panel panOpParms7;
  private Label label18;
  private RichTextBox richTextBox3;
  private ButtonEdit edObjType;
  private Label label8;
  private Panel panOpParms8;
  private Label label19;
  private RichTextBox richTextBox4;
  private ButtonEdit editSelObject;
  private Label labelObjType;
  private Panel panelApply;
  private System.Windows.Forms.Button btnSave;
  private System.Windows.Forms.Button btnCancel;
  private System.Windows.Forms.Button btnTemplate;
  private RadioButton checkInt;
  private RadioButton checkString;
  private RadioButton checkFloat;
  private RadioButton checkDate;
  private RadioButton checkSelFromTempl;
  private RadioButton checkByFormula;
  private System.Windows.Forms.CheckBox checkMakeCurrent;
  private System.Windows.Forms.CheckBox checkFillDefault;
  private IContainer components;
  private Intermech.Bars.ToolBar TBar_Control;
  private TreeList tree;
  private FormEditor formEd;
  private AttributesSelectDlg ASF;
  private bool lockChanged;
  internal bool _scriptChanged;
  internal bool readOnly;
  private string treeTT = "";
  public string newObjName = "";
  public long scriptID = -1;
  private long templID = -1;
  private ShowTemplate showTemp;
  internal bool needCloseQuery;
  public static readonly string ExpClipFormat = LocalizationHolder.rm.GetString("Expert.Editor_224");
  internal PanelButton modPressed;
  internal PanelButton opPressed;
  public Guid ObjectGUID = Guid.Empty;
  public Guid AttributeGUID = Guid.Empty;
  internal bool SettingAttrChanged;
  internal ModParmData modData;
  internal OpParmData opData;
  internal bool _modDataChanged;
  internal bool _opDataChanged;
  internal TempFormula cond;
  internal bool condChanged;
  internal List<int> allowedTypes = new List<int>();
  internal Control srcCon;
  private System.Windows.Forms.Button btnOK;
  private RepositoryItemTextEdit repositoryItemTextEdit2;
  private System.Windows.Forms.Button btnAddGroupLink;
  private System.Windows.Forms.Button btnAddGroup;
  private System.Windows.Forms.Button btnSortDel;
  private System.Windows.Forms.Button btnAddSortLink;
  private System.Windows.Forms.Button btnAddSort;
  private System.Windows.Forms.Button btnDelAttr;
  private System.Windows.Forms.Button btnAddLink;
  private System.Windows.Forms.Button btnAddObj;
  private Label label22;
  private System.Windows.Forms.Button btnAddDAttr;
  private System.Windows.Forms.Button btnAddOpLink;
  private System.Windows.Forms.Button btnDelDAttr;
  private System.Windows.Forms.Button btnCond;
  private Panel panelScroll;
  private Panel panelControl;
  private Intermech.ButtonsPanel.ButtonsPanel buttonsPanelObj;
  private PanelButton objParent;
  private PanelButton objChild;
  private PanelButton objSibling;
  private PanelButton objLinked;
  private PanelButton objAncestor;
  private PanelButton objDescendant;
  private PanelButton opExit;
  private PanelButton opFolder;
  private PanelButton opSelFolder;
  private PanelButton opSetting;
  private PanelButton docFillText;
  private PanelButton docNewElem;
  private PanelButton docSelectElem;
  private Intermech.ButtonsPanel.ButtonsPanel buttonsPanelType;
  private PanelButton TypeBtn;
  private Intermech.ButtonsPanel.ButtonsPanel buttonsPanelBy;
  private PanelButton ByFormBtn;
  private PanelButton ByTableBtn;
  private PanelButton ByScriptBtn;
  private Intermech.ButtonsPanel.ButtonsPanel buttonsPanelMod;
  private PanelButton modForEach;
  private PanelButton modForFirst;
  private PanelButton modForMin;
  private PanelButton modForMax;
  private PanelButton modIfExists;
  private PanelButton modIfAll;
  private PanelButton modCycle;
  private PanelButton modCycleSort;
  private PanelButton modCycleGroup;
  private PanelButton returnBtn;
  private PanelButton recalcBtn;
  private Panel panOpParms9;
  private GroupBox groupBox1;
  private System.Windows.Forms.Button btnAddRecalcAttr;
  private System.Windows.Forms.Button btnAddRecalcLink;
  private System.Windows.Forms.Button btnDelRecalcAttr;
  private TreeView tvRecalcAttrs;
  private TreeView tvObjAttrs;
  private TreeView tvAttrs;
  private TreeView tvSortGroup;
  private System.Windows.Forms.Button btnAddLinkType;
  private TreeView tvTypes;
  private System.Windows.Forms.Button button1;
  private ToolTipController tipCon;
  private Panel panOpParmsA;
  private ButtonEdit beUserProc;
  private PanelButton userProc;
  private System.Windows.Forms.Button button2;
  private GroupBox groupBox2;
  private ButtonEdit buttonEdit1;
  private System.Windows.Forms.CheckBox checkAddId;
  private ButtonEdit textId;
  private ButtonEdit edAddAttr;
  private Label label6;
  private Label label23;
  private GroupBox groupBox3;
  private ButtonEdit buttonEdit2;
  private Label label24;
  private System.Windows.Forms.CheckBox checkEditAdd;
  private ButtonEdit textBox1;
  private ButtonEdit editAddAttr;
  private Label label4;
  private System.Windows.Forms.CheckBox cbSelectDoc;
  private Label label25;
  private Label label26;
  private ButtonEdit buttonEdit3;
  private GroupBox groupBox4;
  private RadioButton radioButton2;
  private RadioButton radioButton1;
  private Panel panOpParmsB;
  private ButtonEdit edVersionRule;
  private Label label27;
  private PanelButton docPaging;
  private Panel panOpParmsC;
  private Label label28;
  private ButtonEdit beNewList;
  private System.Windows.Forms.Button btnToggleSort;
  private Label label29;
  private ToolStripSeparator menuItem6;
  private ToolStripSeparator menuItem9;
  private ToolStripSeparator menuItem13;
  private System.Windows.Forms.TabControl tabCon;
  private System.Windows.Forms.TabPage tabFormula;
  private RichTextBox richFormula;
  private GroupBox gbSetParms;
  private Label label21;
  private Label label20;
  private System.Windows.Forms.ComboBox comboSelector;
  private System.Windows.Forms.ComboBox comboDivider;
  private System.Windows.Forms.TabPage tabTable;
  private System.Windows.Forms.Button btnDelete;
  private System.Windows.Forms.Button btnAdd;
  private System.Windows.Forms.Button btnTableDown;
  private System.Windows.Forms.Button btnTableUp;
  private Grid gridTable;
  private GroupBox groupBox5;
  private RadioButton rbObject;
  private RadioButton rbDocField;
  private RadioButton cbScript;
  private RadioButton cbProc;
  private RadioButton cbUserProc;
  private System.Windows.Forms.TabControl tabControl1;
  private System.Windows.Forms.TabPage tabObjMain;
  private System.Windows.Forms.TabPage tabObjSecond;
  private GroupBox groupBox7;
  private RadioButton rbGlobalNone;
  private RadioButton rbGlobalPlus;
  private RadioButton rbGlobalMul;
  private GroupBox groupBox8;
  private RadioButton rbSaveNone;
  private RadioButton rbSaveClear;
  private RadioButton rbSaveLocal;
  private RadioButton rbSaveAdd;
  private System.Windows.Forms.CheckBox cbByDefault;
  private System.Windows.Forms.CheckBox cbNoSearch;
  private System.Windows.Forms.CheckBox cbSaveRels;
  private System.Windows.Forms.Button btnSaveXml;
  private SaveFileDialog sd;
  private System.IServiceProvider provider;
  private Label lblDocType;
  private System.Windows.Forms.Button btnDocParms;
  private ButtonEdit btnObjLink;
  private Label label12;
  private System.Windows.Forms.Button btnDelRef;
  internal ExpertScriptType ObjectType;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem copyToolStripMenuItem;
  private ToolStripMenuItem pasteToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem copyOpToolStripMenuItem;
  private ToolStripMenuItem pasteOpToolStripMenuItem;
  private System.Windows.Forms.CheckBox cbSaveContext;
  private PanelButton opCreateDoc;
  private PanelButton opCreateComplect;
  private Panel panOpParmsD;
  private Label label13;
  private RichTextBox richComplectCond;
  private ButtonEdit beComplectType;
  private Label label15;
  private Panel panOpParmsE;
  private ButtonEdit beDocScript;
  private RichTextBox richDocCond;
  private ButtonEdit beTypeForDoc;
  private Label label32;
  private System.Windows.Forms.CheckBox cbCreateComplect;
  private System.Windows.Forms.CheckBox cbNoEmpty;
  private System.Windows.Forms.CheckBox cbSecondPass;
  private TextBox textPrefix;
  private Label label34;
  private Panel panel1;
  private RadioButton checkFor;
  private RichTextBox richForEnd;
  private Label label17;
  private SpinEdit spinEdit1;
  private ButtonEdit btnForAttr;
  private Panel panel2;
  private RadioButton checkDoWhile;
  private RichTextBox richWhileCond;
  private System.Windows.Forms.CheckBox cbActiveLink;
  private ButtonEdit beCompObjType;
  private Label label35;
  private Label label36;
  private RichTextBox richAfterFilter;
  private RichTextBox richGlobalFilter;
  private Label label5;
  private System.Windows.Forms.Button btnLoadXml;
  private OpenFileDialog od;
  private Panel panel4;
  private Label label37;
  private RichTextBox richInnerCond;
  private TreeListColumn Struct;
  private TreeListColumn ModParms;
  private TreeListColumn OperParms;
  private RepositoryItemTextEdit repositoryItemTextEdit1;
  private System.Windows.Forms.TabPage tabObjTable;
  private System.Windows.Forms.CheckBox cbAddThis;
  private GroupBox groupBox6;
  private System.Windows.Forms.CheckBox cbInbuiltSort;
  private System.Windows.Forms.CheckBox checkDups;
  private ButtonEdit beCompareFunc;
  private Label label30;
  private GroupBox gbIspoln;
  private RadioButton rbNoIspolns;
  private RadioButton rbOnlyCommon;
  private RadioButton rbCurrentIsp;
  private RadioButton rbAllIspolnInfo;
  private System.Windows.Forms.CheckBox cbForAllIsps;
  private RadioButton checkMeasured;
  private System.Windows.Forms.CheckBox cbUseByDef;
  private System.Windows.Forms.CheckBox cbInbSort;
  private System.Windows.Forms.TabControl tabControl2;
  private System.Windows.Forms.TabPage tabPage1;
  private GroupBox gbSetValue;
  private RichTextBox richSetValue;
  private RadioButton checkForm;
  private GroupBox gbAttr;
  private SelObjAttrControl setAttr;
  private RadioButton checkAttr;
  private System.Windows.Forms.TabPage tabPage2;
  private RichTextBox richLeftIndent;
  private Label label39;
  private GroupBox gbFont;
  private ButtonEdit beFontName;
  private FontDialog fontDialog1;
  private System.Windows.Forms.Button btnClearFont;
  private System.Windows.Forms.CheckBox cbUnderline;
  private System.Windows.Forms.CheckBox cbItalic;
  private System.Windows.Forms.CheckBox cbBold;
  private SpinEdit seFontSize;
  private Panel panel5;
  private System.Windows.Forms.CheckBox cbUseCurrentIsps;
  private TextBox lblDescr;
  private Label label40;
  private RichTextBox parm2Box;
  private Label label41;
  private RichTextBox parm1Box;
  private System.Windows.Forms.CheckBox cbLinkThisDoc;
  private RadioButton rbScenario;
  private RadioButton rbDocScript;
  private System.Windows.Forms.CheckBox cbAvoidDup;
  private RadioButton cbScenario;
  private Label label33;
  private TextBox tbPostfix;
  private Panel panGlobalTableFolder;
  private System.Windows.Forms.CheckBox checkBox1;
  private System.Windows.Forms.CheckBox checkBox2;
  private Panel panOpGlobalType;
  private System.Windows.Forms.TabControl tabGlobTable;
  private System.Windows.Forms.TabPage tabPage3;
  private ButtonEdit beGlobExcerpt;
  private Label lblGRootSelect;
  private System.Windows.Forms.Button btnGlobExcClear;
  private System.Windows.Forms.Button btnGlobExcCreate;
  private Label label44;
  private System.Windows.Forms.Button globAddObjType;
  private System.Windows.Forms.Button globAddLinkDown;
  private TreeView tvGlobRoot;
  private System.Windows.Forms.TabPage tabPage5;
  private RichTextBox rtbGlobalCond;
  private Label label45;
  private PanelButton btnGlobalFolder;
  private PanelButton btnGlobalRequest;
  private ButtonEdit beReplaceObjType;
  private Label label47;
  private System.Windows.Forms.Button btnDelReplaceType;
  private System.Windows.Forms.Button button6;
  private System.Windows.Forms.Button button10;
  private System.Windows.Forms.Button button12;
  private Label label46;
  private TreeView tvGlobCommonAttrs;
  private GroupBox gbGRIsps;
  private RadioButton radioButton10;
  private RadioButton radioButton12;
  private RadioButton radioButton13;
  private System.Windows.Forms.Button globDelete;
  private System.Windows.Forms.Button globAddLinkUp;
  private System.Windows.Forms.TabControl tabControlGT;
  private System.Windows.Forms.TabPage tabGTParms;
  private CheckedListBox cbForObjTypes;
  private Label label42;
  private System.Windows.Forms.TabPage tabGTObjects;
  private RichTextBox rtGTCond;
  private Label label49;
  private System.Windows.Forms.Button button18;
  private System.Windows.Forms.Button btnAddForObjType;
  private System.Windows.Forms.Button button7;
  private System.Windows.Forms.Button button8;
  private System.Windows.Forms.Button button9;
  private Label label48;
  private TreeView tvGTAttrs;
  private GroupBox gbIspForGT;
  private RadioButton radioButton3;
  private RadioButton radioButton4;
  private RadioButton radioButton5;
  private System.Windows.Forms.Button gtDelete;
  private System.Windows.Forms.Button gtAddLinkUp;
  private ButtonEdit beGTExcerpt;
  private Label lblGTExcerpt;
  private System.Windows.Forms.Button btnGTExcClear;
  private System.Windows.Forms.Button btnGTExcCreate;
  private Label label51;
  private System.Windows.Forms.Button gtAddObjType;
  private System.Windows.Forms.Button gtAddLinkDown;
  private TreeView tvGTSearch;
  private RichTextBox rtbGlobObjFilter;
  private Label label52;
  private System.Windows.Forms.Button button17;
  private System.Windows.Forms.Button button13;
  private System.Windows.Forms.Button button16;
  private System.Windows.Forms.Button button11;
  private System.Windows.Forms.Button button19;
  private ImageList IL_NEW;
  private ContextMenuStrip rashifrMenu;
  private ToolStripMenuItem rashifrItem;
  private GroupBox groupBox10;
  private RadioButton rbGroupAll;
  private RadioButton rbGroupCont;
  private RadioButton rbNoGroup;
  private GroupBox gbComposition;
  private System.Windows.Forms.CheckBox cbConfigOptions;
  private RadioButton rbContentsAll;
  private RadioButton rbContentsNotClosed;
  private RadioButton rbContentsNotClosedRoots;
  private System.Windows.Forms.TabPage tabGTOther;
  private GroupBox gbSostav2;
  private RadioButton rbHideHiddenRoots2;
  private RadioButton rbHideHidden2;
  private RadioButton rbShowHidden2;
  private System.Windows.Forms.CheckBox cbConfigOptions2;
  private System.Windows.Forms.TabPage tabPage8;
  private GroupBox gbSostav1;
  private RadioButton rbHideHiddenRoots1;
  private RadioButton rbHideHidden1;
  private RadioButton rbShowHidden1;
  private System.Windows.Forms.CheckBox cbConfigOptions1;
  private RadioButton rbChangePage;
  private RadioButton rbNewPage;
  private System.Windows.Forms.CheckBox cbCoCreator;
  private System.Windows.Forms.CheckBox cbCoWorkerDoc;
  private System.Windows.Forms.CheckBox cbCoWorkerComp;
  private Label label38;
  private Panel panel7;
  private Panel panel6;
  private TextBox tbConds;
  private System.Windows.Forms.CheckBox cbNoNumber;
  private ImageList IL_50;
  private Panel panSetArray;
  private RichTextBox richY;
  private Label label54;
  private Label label53;
  private System.Windows.Forms.CheckBox cbArray;
  private System.Windows.Forms.CheckBox cbCurrentForever;
  private Label labelChecks;
  private System.Windows.Forms.Button btnCheckTemplate;
  private System.Windows.Forms.Button btnCheckDown;
  private System.Windows.Forms.CheckBox cbCheckout;
  private RichTextBox richX;
  private ContextMenuStrip elMoveMenu;
  private ToolStripMenuItem upToolStripMenuItem;
  private ToolStripMenuItem downToolStripMenuItem;
  private ToolStripMenuItem firstToolStripMenuItem;
  private ToolStripMenuItem lastToolStripMenuItem;
  private System.Windows.Forms.CheckBox cbNoCount;
  private TextBox textBox2;
  private Label label55;
  private TextBox textBox3;
  private Label label56;
  private Label label57;
  private ButtonEdit btnRefAttr;
  private RadioButton checkMulti;
  private System.Windows.Forms.CheckBox cbAddToGlobal;
  private TextBox textBox5;
  private TextBox textBox6;
  private GroupBox gbVisZamens;
  private RadioButton rbClientSubst;
  private RadioButton rbAllSubst;
  private RadioButton rbActualSubst;
  private PanelButton verRule;
  private Label label43;
  private ButtonEdit beSourceType;
  private Label label16;
  private ButtonEdit beCreatingDocType;
  private TreeView tvCopiedAttrs;
  private System.Windows.Forms.Button btnDeleteAttr;
  private System.Windows.Forms.Button btnAddRelAttr;
  private System.Windows.Forms.Button btnAddObjAttr;
  private Label label50;
  private PanelButton opCreateDocLink;
  private PanelButton btnTILink;
  private System.Windows.Forms.CheckBox cbMakeListCurrent;
  private System.Windows.Forms.Button btnTextColor;
  private ColorDialog textColorDlg;
  private System.Windows.Forms.CheckBox cbAuthFile;
  private System.Windows.Forms.CheckBox cbAdditionalComp;
  private Label label58;
  private RadioButton rbTechcard;
  private RadioButton rbRelation;
  private ToolStripMenuItem menuCollapse;
  private ToolStripMenuItem menuExpand;
  private ToolStripSeparator toolStripMenuItem3;
  private Panel panel3;
  private Panel panModParmsVersion;
  private Panel panel10;
  private Panel panel9;
  private Panel panel8;
  private GroupBox groupBox12;
  private RichTextBox richVerCond;
  private GroupBox groupBox11;
  private GroupBox groupBox9;
  private RadioButton rbAllVersions;
  private RadioButton rbFirstVersion;
  private System.Windows.Forms.ComboBox cbSortVersions;
  private System.Windows.Forms.CheckBox cbVerDescending;
  private PanelButton modVersions;
  private Panel panel11;
  private Splitter splitter1;
  private GroupBox groupBox14;
  private GroupBox groupBox13;
  private RichTextBox richDocCondBefore;
  private PanelButton opHardSet;
  private Panel panel12;
  private System.Windows.Forms.Button btnUpdate;
  private Panel panel13;
  public CreateEventArgs create_Args;
  public string scriptCaption = "<new script>";
  public string selAttrGUID = "";
  public string selAttrName = "";
  public static readonly string ExpertNamespace = "http://www.intermech.ru/Checking";
  public XmlDocument checkDoc;
  public XmlNode curNode;
  public ImDocument checkTempl;
  private Guid objTypeGuid = Guid.Empty;
  private bool useTraceInfo;
  private bool allNodeObjects;
  private UseZamens allZamens;
  private bool thisObjectDoc;
  private string objTypeName = "";
  private string docName = "";
  private bool needClassify;
  private long _objID;
  private CustomEvents cc = new CustomEvents();
  private TreeListNode draggingNode;
  private TreeListNode targetNode;

  public event EventHandler Changed;

  public event ScriptEdit2.CreateEventHandler CreateEvent;

  public new event EventHandler Closed;

  /// <summary>Constructor for view/edit</summary>
  public ScriptEdit2()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1327);
    this.TopLevel = false;
    this.FormBorderStyle = FormBorderStyle.None;
    this.formEd = new FormEditor();
    this.showTemp = new ShowTemplate();
    this.modData.Init();
    this.opData.Init();
    this.ASF = new AttributesSelectDlg(false);
    this.setAttr.Changed += new EventHandler(this.opAttrObjType_Changed);
    this.settAttr.Changed += new EventHandler(this.opAttrObjType_Changed);
    this.settAttr.Changed += new EventHandler(this.settAttrObjType_Changed);
    this.InitParmGrid();
  }

  /// <summary>Constructor for object creator</summary>
  /// <param name="ObjectType">=0 for ES script, =1 for ES function, =2 for document generation script
  /// </param>
  public ScriptEdit2(ExpertScriptType ObjectType)
  {
    this.InitializeComponent();
    this.ObjectType = ObjectType;
    this.TopLevel = false;
    this.FormBorderStyle = FormBorderStyle.None;
    this.formEd = new FormEditor();
    this.showTemp = new ShowTemplate();
    this.modData.Init();
    this.opData.Init();
    this.ASF = new AttributesSelectDlg(false);
    this.setAttr.Changed += new EventHandler(this.opAttrObjType_Changed);
    this.settAttr.Changed += new EventHandler(this.opAttrObjType_Changed);
    this.settAttr.Changed += new EventHandler(this.settAttrObjType_Changed);
    this.InitParmGrid();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.formEd != null)
      {
        this.formEd.Dispose();
        this.formEd = (FormEditor) null;
      }
      if (this.showTemp != null)
      {
        this.showTemp.Dispose();
        this.showTemp = (ShowTemplate) null;
      }
      if (this.ASF != null)
      {
        this.ASF.Dispose();
        this.ASF = (AttributesSelectDlg) null;
      }
      this.DisposeControls((Control) this);
      if (this.IL != null)
        this.IL.Dispose();
      if (this.IL2 != null)
        this.IL2.Dispose();
      if (this.treePopMenu != null)
        this.treePopMenu.Dispose();
      if (this.modPopMenu != null)
        this.modPopMenu.Dispose();
      if (this.groupMenu != null)
        this.groupMenu.Dispose();
      if (this.sortMenu != null)
        this.sortMenu.Dispose();
      if (this.opPopMenu != null)
        this.opPopMenu.Dispose();
    }
    base.Dispose(disposing);
  }

  private void DisposeControls(Control root)
  {
    foreach (Control control in (ArrangedElementCollection) root.Controls)
    {
      control.Dispose();
      this.DisposeControls(control);
    }
  }

  public long TemplateID
  {
    get => this.templID;
    set => this.templID = value;
  }

  public bool scriptChanged
  {
    get => this._scriptChanged;
    set
    {
      this._scriptChanged = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, (EventArgs) null);
    }
  }

  /// <summary>Create new object of the passed type</summary>
  /// <param name="templID">Template object ID</param>
  /// <param name="Modal"></param>
  /// <returns>true if user pressed OK</returns>
  public bool ExecuteForCreate(long templID, bool Modal)
  {
    this.TemplateID = templID;
    this.ShowPanelsForNode((TreeListNode) null);
    this.SettingAttrChanged = false;
    this.condChanged = false;
    this.SetupControls();
    if (Modal)
      return this.ShowDialog() == DialogResult.OK;
    this.Show();
    return true;
  }

  public void ExecuteForEdit(long scrID, bool Modal)
  {
    this.scriptID = scrID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.scriptID);
      using (new RemoteLock((object) dbObject))
      {
        IExpertScriptable scr = dbObject as IExpertScriptable;
        scr.Load();
        this.scriptCaption = scr.Caption;
        this.ObjectType = scr.ScriptType;
        if (this.ObjectType == ExpertScriptType.DocScript)
          this.templID = (scr as IExpertDocScript).TemplateId;
        this.LoadFromBuffer(scr.Script);
        this.readOnly = scr.ReadOnly;
        this.UpdateReadOnlyState();
        if (scr is IExpertRules)
        {
          string resAttrGuid = (scr as IExpertRules).resAttrGuid;
          if (resAttrGuid != "")
            this.AttributeGUID = new Guid(resAttrGuid);
          string resObjTypeGuid = (scr as IExpertRules).resObjTypeGuid;
          if (resObjTypeGuid != "")
            this.ObjectGUID = new Guid(resObjTypeGuid);
        }
        if (this.ObjectType == ExpertScriptType.CommonCalc)
          this.cond = scr.Cond;
        this.FixObjAttrIdents(sessionKeeper.Session, scr);
        if (this.ObjectType != ExpertScriptType.DocScript)
        {
          if (this.ObjectType != ExpertScriptType.CommandScript)
            goto label_25;
        }
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid(ExpertAttrGUIDs.attrScriptObjTypes), false);
        if (attributeByGuid != null)
        {
          this.allowedTypes.Clear();
          foreach (object obj in attributeByGuid.Values)
          {
            if (obj != DBNull.Value)
              this.allowedTypes.Add(MetaDataHelper.GetObjectTypeID(new Guid(Convert.ToString(obj))));
          }
        }
      }
    }
label_25:
    this.SetupControls();
    this.UpdateSaveCancelButtons();
    this.SettingAttrChanged = false;
    this.condChanged = false;
    this.lockChanged = false;
    if (this.ObjectType == ExpertScriptType.AttribRule)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.UpdateInnerConds(sessionKeeper.Session);
    }
    this.FocusFirstNode();
    this.needCloseQuery = true;
    if (Modal)
    {
      int num = (int) this.ShowDialog();
    }
    else
      this.Show();
  }

  public void FocusFirstNode()
  {
    if (this.tree.Nodes.Count <= 0)
      return;
    this.tree.SetFocusedNode(this.tree.Nodes[0]);
    this.OnChangeNode(this.tree.FocusedNode);
  }

  internal void FixObjAttrIdents(IUserSession ius, IExpertScriptable scr)
  {
    List<IdGuid> attrs = new List<IdGuid>();
    List<IdGuid> objs = new List<IdGuid>();
    string[] attribGuiDs = scr.attribGUIDs;
    string[] objGuiDs = scr.objGUIDs;
    if (attribGuiDs != null)
    {
      for (int index = 0; index < attribGuiDs.Length; ++index)
      {
        string str = attribGuiDs[index];
        if (str != "" && str != null)
        {
          bool flag = false;
          foreach (IdGuid idGuid in attrs)
          {
            if (idGuid.sGuid == str)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            IDBAttributeType attributeType = ius.GetAttributeType(new Guid(str), false);
            if (attributeType != null)
              attrs.Add(new IdGuid(str, attributeType.AttributeID));
          }
        }
      }
    }
    if (objGuiDs != null)
    {
      for (int index = 0; index < objGuiDs.Length; ++index)
      {
        string str = objGuiDs[index];
        if (str != null && str != "")
        {
          bool flag = false;
          foreach (IdGuid idGuid in objs)
          {
            if (idGuid.sGuid == str)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            IDBObjectType objectType = ius.GetObjectType(new Guid(str), false);
            if (objectType != null)
              objs.Add(new IdGuid(str, objectType.ObjectType));
          }
        }
      }
    }
    bool flag1 = false;
    foreach (TreeListNode node in this.tree.Nodes)
      flag1 = flag1 || this.FixNode(node, attrs, objs);
    if (!flag1)
      return;
    this.scriptChanged = true;
  }

  internal bool FixNode(TreeListNode node, List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    Intermech.Expert.NodeData nodeData = this.data(node);
    if (nodeData != null)
    {
      if (nodeData.mods != null)
        flag = nodeData.mods.FixIdents(attrs, objs);
      if (nodeData.ops != null)
        flag = nodeData.ops.FixIdents(attrs, objs) | flag;
    }
    if (node.Nodes != null)
    {
      foreach (TreeListNode node1 in node.Nodes)
        flag = this.FixNode(node1, attrs, objs) | flag;
    }
    return flag;
  }

  /// <summary>
  /// Used in object creator to set GUIDs and create new object name
  /// </summary>
  /// <returns>New object name</returns>
  public string CreateObjName(Guid attrGUID, Guid objTypeGUID)
  {
    this.AttributeGUID = attrGUID;
    this.ObjectGUID = objTypeGUID;
    switch (this.ObjectType)
    {
      case ExpertScriptType.DocScript:
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.recalcBtn.Visible = false;
        break;
      case ExpertScriptType.AttribRule:
        if (this.AttributeGUID != Guid.Empty)
        {
          IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
          if (this.ObjectGUID != Guid.Empty)
            this.newObjName = $"<{service.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>.";
          this.newObjName = $"{this.newObjName}<{service.GetAttributeType(this.AttributeGUID, true).PropertiesStructure.Name}>";
          break;
        }
        break;
      case ExpertScriptType.ObjectRule:
        IClientMetadataCache service1 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
        this.newObjName = !(this.ObjectGUID != Guid.Empty) ? $"<{LocalizationHolder.rm.GetString("Expert.Editor_568")}>" : $"<{service1.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>";
        if (this.AttributeGUID != Guid.Empty)
        {
          IDBAttributeTypeInfo attributeType = service1.GetAttributeType(this.AttributeGUID, true);
          this.newObjName = $"{this.newObjName}{LocalizationHolder.rm.GetString("Expert.Editor_435")}{attributeType.PropertiesStructure.Name}>";
          break;
        }
        break;
      case ExpertScriptType.RecalcScript:
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        if (this.AttributeGUID != Guid.Empty)
        {
          IClientMetadataCache service2 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
          if (this.ObjectGUID != Guid.Empty)
            this.newObjName = $"<{service2.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>.";
          this.newObjName = $"{this.newObjName}<{service2.GetAttributeType(this.AttributeGUID, true).PropertiesStructure.Name}>";
          break;
        }
        break;
    }
    this.scriptCaption = this.newObjName;
    return this.newObjName;
  }

  /// <summary>Enable, disable, show and hide different controls</summary>
  internal void SetupControls()
  {
    if (this.ObjectType == ExpertScriptType.DocScript)
    {
      this.showTemp.TemplateId = this.templID;
      this.lblDocType.Visible = true;
      this.lblDocType.Text = this.objTypeName;
    }
    else
    {
      this.textId.Properties.Buttons[0].Visible = false;
      this.textBox1.Properties.Buttons[0].Visible = false;
      this.docFillText.Visible = false;
      this.docNewElem.Visible = false;
      this.docSelectElem.Visible = false;
      this.rbDocField.Visible = false;
      this.docPaging.Visible = false;
      this.lblDocType.Visible = false;
    }
    this.buttonsPanelType.Visible = this.ObjectType == ExpertScriptType.AttribRule || this.ObjectType == ExpertScriptType.ObjectRule || this.ObjectType == ExpertScriptType.RecalcScript || this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.ComplectTemplate || this.ObjectType == ExpertScriptType.CommonCalc || this.ObjectType == ExpertScriptType.CommandScript;
    this.buttonsPanelBy.Visible = this.ObjectType == ExpertScriptType.AttribRule;
    this.btnTemplate.Visible = this.ObjectType == ExpertScriptType.DocScript;
    this.btnCond.Visible = this.ObjectType == ExpertScriptType.CommonCalc;
    this.objSibling.Visible = false;
    this.userProc.Visible = this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.CommonCalc || this.ObjectType == ExpertScriptType.FunctionScript || this.ObjectType == ExpertScriptType.ComplectTemplate;
    this.btnDocParms.Visible = this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.AttribRule || this.ObjectType == ExpertScriptType.ComplectTemplate || this.ObjectType == ExpertScriptType.CommandScript;
    this.opCreateDoc.Visible = this.ObjectType == ExpertScriptType.ComplectTemplate;
    this.opCreateComplect.Visible = this.ObjectType == ExpertScriptType.ComplectTemplate;
    this.cbCoCreator.Visible = this.ObjectType == ExpertScriptType.ComplectTemplate;
    this.cbCheckout.Visible = this.ObjectType == ExpertScriptType.ComplectTemplate || this.ObjectType == ExpertScriptType.DocScript;
    this.btnGlobalFolder.Visible = this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.ComplectTemplate || this.ObjectType == ExpertScriptType.CommonCalc || this.ObjectType == ExpertScriptType.CommandScript;
    this.btnGlobalRequest.Visible = this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.ComplectTemplate || this.ObjectType == ExpertScriptType.CommonCalc || this.ObjectType == ExpertScriptType.CommandScript;
    this.TypeBtn.Visible = this.ObjectType != ExpertScriptType.DocScript;
    this.returnBtn.Visible = this.ObjectType != ExpertScriptType.DocScript;
    this.recalcBtn.Visible = this.ObjectType != ExpertScriptType.DocScript;
    this.btnCheckTemplate.Visible = this.ObjectType == ExpertScriptType.DocScript;
    this.opCreateDocLink.Visible = this.ObjectType == ExpertScriptType.ComplectTemplate;
    this.opHardSet.Visible = this.ObjectType == ExpertScriptType.CommandScript;
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service1)
      service1.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectChanged));
    this.btnSaveXml.Visible = true;
    this.btnLoadXml.Visible = true;
    this.ShowOpPanel(-1);
    this.ShowModPanel(-1);
    this.gbVisZamens.Visible = this.ObjectType == ExpertScriptType.VisDataScheme;
    switch (this.ObjectType)
    {
      case ExpertScriptType.CommonCalc:
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.verRule.Visible = false;
        break;
      case ExpertScriptType.AttribRule:
        this.buttonsPanelMod.Visible = false;
        this.objParent.Visible = false;
        this.objChild.Visible = false;
        this.objLinked.Visible = false;
        this.objAncestor.Visible = false;
        this.objDescendant.Visible = false;
        this.verRule.Visible = false;
        this.returnBtn.Visible = false;
        this.recalcBtn.Visible = false;
        this.btnDocParms.Text = LocalizationHolder.rm.GetString("Expert.Editor_570");
        this.opSetting.Visible = false;
        if (this.AttributeGUID != Guid.Empty)
        {
          string newObjName = LocalizationHolder.rm.GetString("Expert.Editor_433");
          IClientMetadataCache service2 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
          if (this.ObjectGUID != Guid.Empty)
          {
            this.newObjName = $"<{service2.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>.";
            newObjName = this.newObjName;
          }
          IDBAttributeTypeInfo attributeType = service2.GetAttributeType(this.AttributeGUID, true);
          string str = $"{newObjName}<{attributeType.PropertiesStructure.Name}>";
          this.newObjName = $"{this.newObjName}<{attributeType.PropertiesStructure.Name}>";
          this.Text = str;
        }
        this.btnDocParms.Left -= 78;
        break;
      case ExpertScriptType.ObjectRule:
        this.opSetting.Visible = false;
        this.recalcBtn.Visible = false;
        this.verRule.Visible = false;
        string str1 = LocalizationHolder.rm.GetString("Expert.Editor_434");
        IClientMetadataCache service3 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
        this.newObjName = !(this.ObjectGUID != Guid.Empty) ? $"<{LocalizationHolder.rm.GetString("Expert.Editor_568")}>" : $"<{service3.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>";
        if (this.AttributeGUID != Guid.Empty)
        {
          IDBAttributeTypeInfo attributeType = service3.GetAttributeType(this.AttributeGUID, true);
          this.newObjName = $"{this.newObjName}{LocalizationHolder.rm.GetString("Expert.Editor_435")}{attributeType.PropertiesStructure.Name}>";
        }
        this.Text = str1 + this.newObjName;
        break;
      case ExpertScriptType.RecalcScript:
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.verRule.Visible = false;
        if (this.AttributeGUID != Guid.Empty)
        {
          string str2 = LocalizationHolder.rm.GetString("Expert.Editor_436");
          IClientMetadataCache service4 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
          if (this.ObjectGUID != Guid.Empty)
          {
            this.newObjName = $"<{service4.GetObjectType(this.ObjectGUID, true).ObjectTypeName}>.";
            str2 = this.newObjName;
          }
          IDBAttributeTypeInfo attributeType = service4.GetAttributeType(this.AttributeGUID, false);
          if (attributeType != null)
          {
            str2 = $"{str2}<{attributeType.PropertiesStructure.Name}>";
            this.newObjName = $"{this.newObjName}<{attributeType.PropertiesStructure.Name}>";
          }
          this.Text = str2;
          break;
        }
        break;
      case ExpertScriptType.ComplectTemplate:
        this.buttonsPanelMod.Visible = false;
        this.objParent.Visible = false;
        this.objChild.Visible = false;
        this.objSibling.Visible = false;
        this.objLinked.Visible = false;
        this.objAncestor.Visible = false;
        this.objDescendant.Visible = true;
        this.objDescendant.Text = LocalizationHolder.rm.GetString("Expert.Editor_562");
        this.btnDocParms.Text = LocalizationHolder.rm.GetString("Expert.Editor_673");
        this.opExit.Visible = false;
        this.opFolder.Visible = false;
        this.opSelFolder.Visible = false;
        this.verRule.Visible = false;
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.recalcBtn.Visible = false;
        this.labelChecks.Visible = false;
        this.btnCheckDown.Visible = false;
        break;
      case ExpertScriptType.VisDataScheme:
        this.buttonsPanelMod.Visible = false;
        this.buttonsPanelObj.Visible = false;
        this.buttonsPanelBy.Visible = false;
        this.buttonsPanelType.Visible = true;
        this.btnTemplate.Visible = false;
        this.btnDocParms.Visible = false;
        this.btnSaveXml.Visible = false;
        this.btnLoadXml.Visible = false;
        this.btnCheckTemplate.Visible = false;
        this.cbCoCreator.Visible = false;
        this.cbCheckout.Visible = false;
        this.btnGlobalFolder.Visible = true;
        this.btnGlobalRequest.Visible = true;
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.recalcBtn.Visible = false;
        this.cbConfigOptions1.Visible = false;
        this.cbConfigOptions2.Visible = false;
        this.rtbGlobalCond.Visible = false;
        this.rtbGlobObjFilter.Visible = false;
        this.label45.Visible = false;
        this.label52.Visible = false;
        this.rtGTCond.Enabled = false;
        this.lblGRootSelect.Visible = false;
        this.beGlobExcerpt.Visible = false;
        this.btnGlobExcCreate.Visible = false;
        this.btnGlobExcClear.Visible = false;
        this.lblGTExcerpt.Visible = false;
        this.beGTExcerpt.Visible = false;
        this.btnGTExcCreate.Visible = false;
        this.btnGTExcClear.Visible = false;
        this.gbGRIsps.Enabled = false;
        this.tabControlGT.TabPages.Remove(this.tabGTObjects);
        this.tabControlGT.TabPages.Remove(this.tabGTOther);
        this.gbIspForGT.Visible = false;
        this.dockModParms.Visible = false;
        this.dockModParms.Close();
        this.cmdInsInto.Visible = false;
        this.menuInsInto.Visible = false;
        break;
      case ExpertScriptType.VisStyles:
        this.buttonsPanelMod.Visible = false;
        this.buttonsPanelObj.Visible = false;
        this.buttonsPanelBy.Visible = false;
        this.buttonsPanelType.Visible = true;
        this.btnTemplate.Visible = false;
        this.btnDocParms.Visible = false;
        this.btnSaveXml.Visible = false;
        this.btnLoadXml.Visible = false;
        this.btnCheckTemplate.Visible = false;
        this.cbCoCreator.Visible = false;
        this.cbCheckout.Visible = false;
        this.verRule.Visible = false;
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        this.recalcBtn.Visible = false;
        this.dockModParms.Close();
        this.cmdInsInto.Visible = false;
        this.menuInsInto.Visible = false;
        this.Struct.Width = 900;
        this.rightDock.Width = 700;
        break;
      case ExpertScriptType.CommandScript:
        this.verRule.Visible = true;
        this.TypeBtn.Visible = false;
        this.returnBtn.Visible = false;
        break;
    }
    if (this.provider != null)
    {
      IViewState service5 = (IViewState) this.provider.GetService(typeof (IViewState));
      if (service5 != null && (service5.ViewState & ViewStateFlags.InDialog) != ViewStateFlags.None)
        this.btnTemplate.Visible = false;
    }
    this.EnableButtons();
  }

  public void ObjectChanged(object sender, NotificationEventArgs e)
  {
    if (!((DBObjectsEventArgs) e).ObjectIDs.Contains(this.templID) || this.showTemp == null)
      return;
    this.showTemp.TemplateId = this.templID;
  }

  /// <summary>Used by object creator to actually create the object</summary>
  /// <returns>ObjectIdIzd</returns>
  public long createObject(long protoObjId, bool IsVersion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = (IDBObjectCollection) null;
      switch (this.ObjectType)
      {
        case ExpertScriptType.CommonCalc:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objScript);
          break;
        case ExpertScriptType.FunctionScript:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objFunction);
          break;
        case ExpertScriptType.DocScript:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript);
          break;
        case ExpertScriptType.AttribRule:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objAttrRules);
          break;
        case ExpertScriptType.ObjectRule:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objObjRules);
          break;
        case ExpertScriptType.RecalcScript:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objRecalcScript);
          break;
        case ExpertScriptType.ComplectTemplate:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objComplectTemplate);
          break;
        case ExpertScriptType.VisDataScheme:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objVisScheme);
          break;
        case ExpertScriptType.VisStyles:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objVisStyles);
          break;
        case ExpertScriptType.CommandScript:
          objectCollection = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objCommandScript);
          break;
      }
      IExpertScriptable version;
      if (IsVersion)
      {
        version = (IExpertScriptable) objectCollection.CreateVersion(protoObjId);
        IDBAttribute byId = version.Attributes.FindByID(ExpertConsts.Consts._attrObjName);
        if (byId != null)
          this.newObjName = Convert.ToString(byId.Value);
      }
      else if (protoObjId != -1L)
      {
        version = (IExpertScriptable) objectCollection.Create(protoObjId);
        if (sessionKeeper.Session.GetObject(protoObjId) is IExpertScriptable expertScriptable)
        {
          expertScriptable.Load();
          this.ObjectType = expertScriptable.ScriptType;
          if (this.ObjectType == ExpertScriptType.DocScript)
          {
            this.templID = (expertScriptable as IExpertDocScript).TemplateId;
            this.showTemp.TemplateId = this.templID;
          }
          this.LoadFromBuffer(expertScriptable.Script);
          if (this.tree.AllNodesCount > 0)
            this.OnChangeNode(this.tree.Nodes[0]);
        }
      }
      else
        version = (IExpertScriptable) objectCollection.Create();
      byte[] buffer = this.SaveToBuffer();
      if (this.ObjectType == ExpertScriptType.DocScript && this.templID != -1L && version is IExpertDocScript)
      {
        (version as IExpertDocScript).TemplateId = this.templID;
        (version as IExpertDocScript).DocTypeGuid = this.objTypeGuid;
      }
      if (version is IExpertRules)
      {
        IExpertRules expertRules = version as IExpertRules;
        AttribPair attribPair = new AttribPair(-1);
        if (this.ObjectGUID != Guid.Empty)
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.ObjectGUID);
          attribPair.objTypeID = objectType.ObjectType;
        }
        else
          attribPair.objTypeID = -1;
        if (this.AttributeGUID != Guid.Empty)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.AttributeGUID);
          attribPair.attribID = attributeType.AttributeID;
        }
        else
          attribPair.attribID = -1;
        expertRules.Result = attribPair;
        expertRules.resAttrGuid = this.AttributeGUID.ToString();
        expertRules.resObjTypeGuid = this.ObjectGUID.ToString();
      }
      if (version is IComplectTemplate)
        (version as IComplectTemplate).ObjTypeGuid = this.ObjectGUID.ToString();
      if (this.condChanged)
        version.Cond = this.cond;
      version.UpdateObject(buffer, this.newObjName);
      version.CommitCreation(true);
      this.ReflectScriptUpdate(sessionKeeper.Session, version);
      this.scriptChanged = false;
      return version.ObjectID;
    }
  }

  internal bool SaveScript()
  {
    if (this.scriptChanged)
    {
      if (this.ValidateScript() != null)
        return false;
      if (this.tree.Nodes.Count > 0)
      {
        TreeListNode treeListNode = this.checkScriptFromNode(this.tree.Nodes[0]);
        if (treeListNode != null && new ShowXml().ExecSaveAbort(this.checkDoc))
        {
          this.tree.FocusedNode = treeListNode;
          return false;
        }
      }
      IExpertScriptable scriptObj = (IExpertScriptable) null;
      byte[] buffer = (byte[]) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        scriptObj = sessionKeeper.Session.GetObject(this.scriptID) as IExpertScriptable;
        scriptObj.Load();
        buffer = this.SaveToBuffer();
        if (this.condChanged)
          scriptObj.Cond = this.cond;
        if (this.ObjectType == ExpertScriptType.DocScript)
        {
          if (scriptObj is IExpertDocScript)
          {
            (scriptObj as IExpertDocScript).DocTypeGuid = this.objTypeGuid;
            (scriptObj as IExpertDocScript).DocTypeName = this.docName;
          }
        }
      }
      string str = "";
      if (this.ObjectType == ExpertScriptType.ComplectTemplate)
      {
        bool flag1 = false;
        bool flag2 = false;
        for (int index = 0; index < this.tree.Nodes.Count; ++index)
        {
          TreeListNode node = this.tree.Nodes[index];
          if (!Convert.ToString(node[(object) 0]).StartsWith("#"))
          {
            Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) node.Tag;
            if (!flag2)
            {
              if (tag.opTag == 14 || tag.opTag == 43 || tag.opTag == 53)
                flag2 = true;
              else
                flag1 = true;
            }
            if (tag.ops is OpCreateComplect && str == "")
              str = (tag.ops as OpCreateComplect).objTypeGUID;
          }
        }
        if (flag1 & flag2)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_674"), LocalizationHolder.rm.GetString("Expert.Editor_378"), MessageBoxButtons.OK);
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if ((this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.CommandScript) && this.allowedTypes != null)
        {
          IDBAttribute dbAttribute = scriptObj.Attributes.AddAttribute(ExpertConsts.Consts.attrScriptObjTypes, false);
          object[] objArray = new object[this.allowedTypes.Count];
          for (int index = 0; index < this.allowedTypes.Count; ++index)
            objArray[index] = (object) MetaDataHelper.GetObjectTypeGuid(this.allowedTypes[index]);
          if (objArray.Length == 0)
            dbAttribute.Values = new object[1]
            {
              (object) DBNull.Value
            };
          else
            dbAttribute.Values = objArray;
        }
        if (str != "")
          ((IComplectTemplate) scriptObj).ObjTypeGuid = str;
        scriptObj.UpdateObject(buffer, scriptObj.Name);
        this.ReflectScriptUpdate(sessionKeeper.Session, scriptObj);
      }
      this.scriptChanged = false;
      this.UpdateSaveCancelButtons();
    }
    return true;
  }

  private void ReflectScriptUpdate(IUserSession ius, IExpertScriptable scriptObj)
  {
    if (this.ObjectType != ExpertScriptType.CommonCalc || !this.SettingAttrChanged)
      return;
    IExpertServer customService = ius.GetCustomService(typeof (IExpertServer)) as IExpertServer;
    byte[] traceInfo = (byte[]) null;
    bool flag = false;
    if (customService != null)
      flag = customService.ReflectObjUpdate(ius.SessionGUID, scriptObj.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
    if (!flag)
      return;
    using (RuleUpdateReport ruleUpdateReport = new RuleUpdateReport())
      ruleUpdateReport.Execute(traceInfo);
  }

  private void ScriptEdit2_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.scriptChanged)
      return;
    if (this.needCloseQuery)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_437"), LocalizationHolder.rm.GetString("Expert.Editor_438"), MessageBoxButtons.YesNoCancel);
      if (num == 6 && !this.SaveScript())
        e.Cancel = true;
      if (num != 2)
        return;
      e.Cancel = true;
    }
    else
    {
      if (this.DialogResult == DialogResult.OK || MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_439"), LocalizationHolder.rm.GetString("Expert.Editor_440"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      e.Cancel = true;
    }
  }

  private void ScriptEdit2_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service1)
    {
      IConfiguration configuration = service1.Open("FormStorage") ?? service1.Create("FormStorage");
      string name = $"{this.GetType().ToString()}_{this.Name}";
      (configuration.Open(name) ?? configuration.Add(name)).SetProperty("DockingLayout", this.dockMan.GetLayout());
    }
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2))
      return;
    service2.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectChanged));
  }

  private void ScriptEdit_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Open("FormStorage");
    if (configuration1 == null)
      return;
    string name = $"{this.GetType().ToString()}_{this.Name}";
    IConfiguration configuration2 = configuration1.Open(name);
    if (configuration2 == null || !configuration2.HasProperty("DockingLayout"))
      return;
    this.dockMan.SetLayout(configuration2.GetProperty("DockingLayout"));
  }

  private void ScriptEdit2_Shown(object sender, EventArgs e)
  {
    this.buttonsPanelObj.ApplyLayout();
    this.buttonsPanelMod.ApplyLayout();
    this.buttonsPanelMod.Height = this.buttonsPanelMod.FitHeight + 10;
    this.buttonsPanelType.ApplyLayout();
    this.buttonsPanelType.Height = this.buttonsPanelType.FitHeight + 2;
    int fitHeight = this.buttonsPanelObj.FitHeight;
    if (this.buttonsPanelMod.Visible)
      fitHeight += this.buttonsPanelMod.Height;
    if (this.buttonsPanelBy.Visible)
      fitHeight += this.buttonsPanelBy.Height;
    if (this.buttonsPanelType.Visible)
      fitHeight += this.buttonsPanelType.Height;
    this.panelScroll.AutoScrollMinSize = new Size(this.panelScroll.Width - 20, fitHeight + 10);
    this.EnableButtons();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScriptEdit2));
    this.dockMan = new DockManager();
    this.leftDock = new DockContainer();
    this.dockOpMod = new DockControl();
    this.panelScroll = new Panel();
    this.panelControl = new Panel();
    this.buttonsPanelObj = new Intermech.ButtonsPanel.ButtonsPanel();
    this.objParent = new PanelButton();
    this.objChild = new PanelButton();
    this.objSibling = new PanelButton();
    this.objLinked = new PanelButton();
    this.objAncestor = new PanelButton();
    this.objDescendant = new PanelButton();
    this.opExit = new PanelButton();
    this.opFolder = new PanelButton();
    this.opSelFolder = new PanelButton();
    this.opSetting = new PanelButton();
    this.docFillText = new PanelButton();
    this.docNewElem = new PanelButton();
    this.docSelectElem = new PanelButton();
    this.docPaging = new PanelButton();
    this.userProc = new PanelButton();
    this.opCreateDoc = new PanelButton();
    this.opCreateComplect = new PanelButton();
    this.opCreateDocLink = new PanelButton();
    this.opHardSet = new PanelButton();
    this.IL_50 = new ImageList(this.components);
    this.buttonsPanelType = new Intermech.ButtonsPanel.ButtonsPanel();
    this.btnGlobalFolder = new PanelButton();
    this.TypeBtn = new PanelButton();
    this.btnGlobalRequest = new PanelButton();
    this.returnBtn = new PanelButton();
    this.recalcBtn = new PanelButton();
    this.verRule = new PanelButton();
    this.buttonsPanelBy = new Intermech.ButtonsPanel.ButtonsPanel();
    this.ByFormBtn = new PanelButton();
    this.ByTableBtn = new PanelButton();
    this.ByScriptBtn = new PanelButton();
    this.buttonsPanelMod = new Intermech.ButtonsPanel.ButtonsPanel();
    this.modForEach = new PanelButton();
    this.modForFirst = new PanelButton();
    this.modForMin = new PanelButton();
    this.modForMax = new PanelButton();
    this.modIfExists = new PanelButton();
    this.modIfAll = new PanelButton();
    this.modCycle = new PanelButton();
    this.modCycleSort = new PanelButton();
    this.modCycleGroup = new PanelButton();
    this.modVersions = new PanelButton();
    this.TBar_Control = new Intermech.Bars.ToolBar();
    this.addMenu = new DropDownMenuItem();
    this.cmdInsBefore = new MenuButtonItem();
    this.cmdInsAfter = new MenuButtonItem();
    this.cmdInsInto = new MenuButtonItem();
    this.btnChange = new ButtonItem();
    this.btnApply = new ButtonItem();
    this.clipMenu = new DropDownMenuItem();
    this.cmdCut = new MenuButtonItem();
    this.cmdCopy = new MenuButtonItem();
    this.cmdPaste = new MenuButtonItem();
    this.IL_NEW = new ImageList(this.components);
    this.cmdDelete = new ButtonItem();
    this.IL = new ImageList(this.components);
    this.rightDock = new DockContainer();
    this.dockModParms = new DockControl();
    this.panModParmsVersion = new Panel();
    this.panel10 = new Panel();
    this.groupBox12 = new GroupBox();
    this.richVerCond = new RichTextBox();
    this.modPopMenu = new ContextMenuStrip(this.components);
    this.menuChangeModForm = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.copyToolStripMenuItem = new ToolStripMenuItem();
    this.pasteToolStripMenuItem = new ToolStripMenuItem();
    this.panel9 = new Panel();
    this.groupBox11 = new GroupBox();
    this.cbVerDescending = new System.Windows.Forms.CheckBox();
    this.cbSortVersions = new System.Windows.Forms.ComboBox();
    this.panel8 = new Panel();
    this.groupBox9 = new GroupBox();
    this.rbAllVersions = new RadioButton();
    this.rbFirstVersion = new RadioButton();
    this.panModParms5 = new Panel();
    this.panel2 = new Panel();
    this.label57 = new Label();
    this.checkDoWhile = new RadioButton();
    this.richWhileCond = new RichTextBox();
    this.btnForAttr = new ButtonEdit();
    this.panel1 = new Panel();
    this.checkMulti = new RadioButton();
    this.btnRefAttr = new ButtonEdit();
    this.checkFor = new RadioButton();
    this.richForEnd = new RichTextBox();
    this.label17 = new Label();
    this.spinEdit1 = new SpinEdit();
    this.panModParms4 = new Panel();
    this.richTextBox2 = new RichTextBox();
    this.label10 = new Label();
    this.gbResType = new GroupBox();
    this.checkMeasured = new RadioButton();
    this.checkDate = new RadioButton();
    this.checkFloat = new RadioButton();
    this.checkString = new RadioButton();
    this.checkInt = new RadioButton();
    this.panModParms3 = new Panel();
    this.cbInbSort = new System.Windows.Forms.CheckBox();
    this.tvAttrs = new TreeView();
    this.btnDelAttr = new System.Windows.Forms.Button();
    this.btnAddLink = new System.Windows.Forms.Button();
    this.btnAddObj = new System.Windows.Forms.Button();
    this.label11 = new Label();
    this.panModParms2 = new Panel();
    this.tvSortGroup = new TreeView();
    this.btnAddGroupLink = new System.Windows.Forms.Button();
    this.btnAddGroup = new System.Windows.Forms.Button();
    this.btnSortDel = new System.Windows.Forms.Button();
    this.btnAddSortLink = new System.Windows.Forms.Button();
    this.btnAddSort = new System.Windows.Forms.Button();
    this.label14 = new Label();
    this.panModParmsEmpty = new Panel();
    this.panModParms1 = new Panel();
    this.cbForAllIsps = new System.Windows.Forms.CheckBox();
    this.cbSaveContext = new System.Windows.Forms.CheckBox();
    this.richTextBox1 = new RichTextBox();
    this.label9 = new Label();
    this.dockOpParms = new DockControl();
    this.panOpParms8 = new Panel();
    this.panel4 = new Panel();
    this.btnUpdate = new System.Windows.Forms.Button();
    this.richInnerCond = new RichTextBox();
    this.rashifrMenu = new ContextMenuStrip(this.components);
    this.rashifrItem = new ToolStripMenuItem();
    this.label37 = new Label();
    this.panel6 = new Panel();
    this.tbConds = new TextBox();
    this.label38 = new Label();
    this.panel7 = new Panel();
    this.richTextBox4 = new RichTextBox();
    this.opPopMenu = new ContextMenuStrip(this.components);
    this.menuChangeOpForm = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.copyOpToolStripMenuItem = new ToolStripMenuItem();
    this.pasteOpToolStripMenuItem = new ToolStripMenuItem();
    this.label19 = new Label();
    this.editSelObject = new ButtonEdit();
    this.labelObjType = new Label();
    this.panOpParmsE = new Panel();
    this.panel11 = new Panel();
    this.groupBox14 = new GroupBox();
    this.richDocCond = new RichTextBox();
    this.splitter1 = new Splitter();
    this.groupBox13 = new GroupBox();
    this.richDocCondBefore = new RichTextBox();
    this.label58 = new Label();
    this.rbTechcard = new RadioButton();
    this.cbNoCount = new System.Windows.Forms.CheckBox();
    this.cbNoNumber = new System.Windows.Forms.CheckBox();
    this.cbCoWorkerDoc = new System.Windows.Forms.CheckBox();
    this.groupBox10 = new GroupBox();
    this.rbGroupAll = new RadioButton();
    this.rbGroupCont = new RadioButton();
    this.rbNoGroup = new RadioButton();
    this.rbScenario = new RadioButton();
    this.rbDocScript = new RadioButton();
    this.label34 = new Label();
    this.textPrefix = new TextBox();
    this.cbSecondPass = new System.Windows.Forms.CheckBox();
    this.cbNoEmpty = new System.Windows.Forms.CheckBox();
    this.beDocScript = new ButtonEdit();
    this.beTypeForDoc = new ButtonEdit();
    this.label32 = new Label();
    this.panOpParmsD = new Panel();
    this.cbAdditionalComp = new System.Windows.Forms.CheckBox();
    this.cbCoWorkerComp = new System.Windows.Forms.CheckBox();
    this.label33 = new Label();
    this.tbPostfix = new TextBox();
    this.label35 = new Label();
    this.beCompObjType = new ButtonEdit();
    this.cbCreateComplect = new System.Windows.Forms.CheckBox();
    this.label13 = new Label();
    this.richComplectCond = new RichTextBox();
    this.beComplectType = new ButtonEdit();
    this.label15 = new Label();
    this.panOpParms7 = new Panel();
    this.label18 = new Label();
    this.richTextBox3 = new RichTextBox();
    this.edObjType = new ButtonEdit();
    this.label8 = new Label();
    this.panOpParms6 = new Panel();
    this.cbByDefault = new System.Windows.Forms.CheckBox();
    this.groupBox4 = new GroupBox();
    this.radioButton2 = new RadioButton();
    this.radioButton1 = new RadioButton();
    this.cbSelectDoc = new System.Windows.Forms.CheckBox();
    this.gbIdent = new GroupBox();
    this.label26 = new Label();
    this.buttonEdit3 = new ButtonEdit();
    this.label25 = new Label();
    this.checkByFormula = new RadioButton();
    this.checkSelFromTempl = new RadioButton();
    this.edSelTemplate = new ButtonEdit();
    this.richTextID = new RichTextBox();
    this.panOpParms5 = new Panel();
    this.textBox2 = new TextBox();
    this.label55 = new Label();
    this.cbCurrentForever = new System.Windows.Forms.CheckBox();
    this.button19 = new System.Windows.Forms.Button();
    this.cbAvoidDup = new System.Windows.Forms.CheckBox();
    this.cbUseByDef = new System.Windows.Forms.CheckBox();
    this.groupBox3 = new GroupBox();
    this.buttonEdit2 = new ButtonEdit();
    this.label24 = new Label();
    this.checkEditAdd = new System.Windows.Forms.CheckBox();
    this.textBox1 = new ButtonEdit();
    this.editAddAttr = new ButtonEdit();
    this.label4 = new Label();
    this.checkFillDefault = new System.Windows.Forms.CheckBox();
    this.checkMakeCurrent = new System.Windows.Forms.CheckBox();
    this.label7 = new Label();
    this.edSaveNewID = new ButtonEdit();
    this.panOpParms4 = new Panel();
    this.tabControl2 = new System.Windows.Forms.TabControl();
    this.tabPage1 = new System.Windows.Forms.TabPage();
    this.gbSetValue = new GroupBox();
    this.richSetValue = new RichTextBox();
    this.checkForm = new RadioButton();
    this.gbAttr = new GroupBox();
    this.setAttr = new SelObjAttrControl();
    this.checkAttr = new RadioButton();
    this.tabPage2 = new System.Windows.Forms.TabPage();
    this.btnTextColor = new System.Windows.Forms.Button();
    this.gbFont = new GroupBox();
    this.seFontSize = new SpinEdit();
    this.cbUnderline = new System.Windows.Forms.CheckBox();
    this.cbItalic = new System.Windows.Forms.CheckBox();
    this.cbBold = new System.Windows.Forms.CheckBox();
    this.btnClearFont = new System.Windows.Forms.Button();
    this.beFontName = new ButtonEdit();
    this.panel12 = new Panel();
    this.richLeftIndent = new RichTextBox();
    this.label39 = new Label();
    this.groupBox2 = new GroupBox();
    this.cbAuthFile = new System.Windows.Forms.CheckBox();
    this.textBox3 = new TextBox();
    this.label56 = new Label();
    this.cbLinkThisDoc = new System.Windows.Forms.CheckBox();
    this.cbActiveLink = new System.Windows.Forms.CheckBox();
    this.buttonEdit1 = new ButtonEdit();
    this.checkAddId = new System.Windows.Forms.CheckBox();
    this.textId = new ButtonEdit();
    this.edAddAttr = new ButtonEdit();
    this.label6 = new Label();
    this.label23 = new Label();
    this.panOpParms3 = new Panel();
    this.tabCon = new System.Windows.Forms.TabControl();
    this.tabFormula = new System.Windows.Forms.TabPage();
    this.richFormula = new RichTextBox();
    this.gbSetParms = new GroupBox();
    this.label21 = new Label();
    this.label20 = new Label();
    this.comboSelector = new System.Windows.Forms.ComboBox();
    this.comboDivider = new System.Windows.Forms.ComboBox();
    this.tabTable = new System.Windows.Forms.TabPage();
    this.gridTable = new Grid();
    this.panel3 = new Panel();
    this.btnAdd = new System.Windows.Forms.Button();
    this.btnTableDown = new System.Windows.Forms.Button();
    this.btnDelete = new System.Windows.Forms.Button();
    this.btnTableUp = new System.Windows.Forms.Button();
    this.panSetArray = new Panel();
    this.richX = new RichTextBox();
    this.richY = new RichTextBox();
    this.label54 = new Label();
    this.label53 = new Label();
    this.groupBox5 = new GroupBox();
    this.rbDocField = new RadioButton();
    this.cbAddToGlobal = new System.Windows.Forms.CheckBox();
    this.rbRelation = new RadioButton();
    this.cbArray = new System.Windows.Forms.CheckBox();
    this.rbObject = new RadioButton();
    this.settAttr = new SelObjAttrControl();
    this.panOpParmsTI = new Panel();
    this.tvCopiedAttrs = new TreeView();
    this.btnDeleteAttr = new System.Windows.Forms.Button();
    this.btnAddRelAttr = new System.Windows.Forms.Button();
    this.btnAddObjAttr = new System.Windows.Forms.Button();
    this.label50 = new Label();
    this.label43 = new Label();
    this.beSourceType = new ButtonEdit();
    this.label16 = new Label();
    this.beCreatingDocType = new ButtonEdit();
    this.panOpParmsStyleB = new Panel();
    this.textBox5 = new TextBox();
    this.panOpParmsStyleC = new Panel();
    this.textBox6 = new TextBox();
    this.panOpParms2 = new Panel();
    this.btnDelRef = new System.Windows.Forms.Button();
    this.label12 = new Label();
    this.btnObjLink = new ButtonEdit();
    this.richTextCond = new RichTextBox();
    this.label3 = new Label();
    this.panOpParms9 = new Panel();
    this.groupBox1 = new GroupBox();
    this.tvRecalcAttrs = new TreeView();
    this.btnAddRecalcAttr = new System.Windows.Forms.Button();
    this.btnAddRecalcLink = new System.Windows.Forms.Button();
    this.btnDelRecalcAttr = new System.Windows.Forms.Button();
    this.panOpParms1 = new Panel();
    this.tabControl1 = new System.Windows.Forms.TabControl();
    this.tabObjMain = new System.Windows.Forms.TabPage();
    this.cbNoSearch = new System.Windows.Forms.CheckBox();
    this.btnEdExcerpt = new ButtonEdit();
    this.label1 = new Label();
    this.button3 = new System.Windows.Forms.Button();
    this.button1 = new System.Windows.Forms.Button();
    this.label22 = new Label();
    this.btnDelDAttr = new System.Windows.Forms.Button();
    this.richCond = new RichTextBox();
    this.label2 = new Label();
    this.btnAddObjType = new System.Windows.Forms.Button();
    this.btnToggleSort = new System.Windows.Forms.Button();
    this.btnAddLinkType = new System.Windows.Forms.Button();
    this.label29 = new Label();
    this.tvTypes = new TreeView();
    this.btnAddOpLink = new System.Windows.Forms.Button();
    this.button2 = new System.Windows.Forms.Button();
    this.btnAddDAttr = new System.Windows.Forms.Button();
    this.tvObjAttrs = new TreeView();
    this.elMoveMenu = new ContextMenuStrip(this.components);
    this.upToolStripMenuItem = new ToolStripMenuItem();
    this.downToolStripMenuItem = new ToolStripMenuItem();
    this.firstToolStripMenuItem = new ToolStripMenuItem();
    this.lastToolStripMenuItem = new ToolStripMenuItem();
    this.tabObjSecond = new System.Windows.Forms.TabPage();
    this.richGlobalFilter = new RichTextBox();
    this.label5 = new Label();
    this.cbAddThis = new System.Windows.Forms.CheckBox();
    this.label36 = new Label();
    this.groupBox8 = new GroupBox();
    this.cbSaveRels = new System.Windows.Forms.CheckBox();
    this.rbSaveAdd = new RadioButton();
    this.rbSaveLocal = new RadioButton();
    this.rbSaveClear = new RadioButton();
    this.rbSaveNone = new RadioButton();
    this.richAfterFilter = new RichTextBox();
    this.groupBox7 = new GroupBox();
    this.rbGlobalMul = new RadioButton();
    this.rbGlobalPlus = new RadioButton();
    this.rbGlobalNone = new RadioButton();
    this.tabObjTable = new System.Windows.Forms.TabPage();
    this.groupBox6 = new GroupBox();
    this.cbInbuiltSort = new System.Windows.Forms.CheckBox();
    this.beCompareFunc = new ButtonEdit();
    this.label30 = new Label();
    this.gbComposition = new GroupBox();
    this.rbContentsNotClosedRoots = new RadioButton();
    this.rbContentsNotClosed = new RadioButton();
    this.rbContentsAll = new RadioButton();
    this.cbConfigOptions = new System.Windows.Forms.CheckBox();
    this.gbIspoln = new GroupBox();
    this.panel5 = new Panel();
    this.cbUseCurrentIsps = new System.Windows.Forms.CheckBox();
    this.rbAllIspolnInfo = new RadioButton();
    this.rbCurrentIsp = new RadioButton();
    this.rbOnlyCommon = new RadioButton();
    this.rbNoIspolns = new RadioButton();
    this.checkDups = new System.Windows.Forms.CheckBox();
    this.panOpParmsA = new Panel();
    this.cbScenario = new RadioButton();
    this.parm2Box = new RichTextBox();
    this.label41 = new Label();
    this.parm1Box = new RichTextBox();
    this.label40 = new Label();
    this.cbScript = new RadioButton();
    this.cbProc = new RadioButton();
    this.cbUserProc = new RadioButton();
    this.beUserProc = new ButtonEdit();
    this.panOpParmsC = new Panel();
    this.cbMakeListCurrent = new System.Windows.Forms.CheckBox();
    this.rbChangePage = new RadioButton();
    this.rbNewPage = new RadioButton();
    this.label28 = new Label();
    this.beNewList = new ButtonEdit();
    this.panOpParmsB = new Panel();
    this.gbVisZamens = new GroupBox();
    this.rbClientSubst = new RadioButton();
    this.rbAllSubst = new RadioButton();
    this.rbActualSubst = new RadioButton();
    this.edVersionRule = new ButtonEdit();
    this.label27 = new Label();
    this.panGlobalTableFolder = new Panel();
    this.tabGlobTable = new System.Windows.Forms.TabControl();
    this.tabPage5 = new System.Windows.Forms.TabPage();
    this.button6 = new System.Windows.Forms.Button();
    this.button10 = new System.Windows.Forms.Button();
    this.button12 = new System.Windows.Forms.Button();
    this.label46 = new Label();
    this.tvGlobCommonAttrs = new TreeView();
    this.beReplaceObjType = new ButtonEdit();
    this.label47 = new Label();
    this.btnDelReplaceType = new System.Windows.Forms.Button();
    this.rtbGlobalCond = new RichTextBox();
    this.label45 = new Label();
    this.tabPage3 = new System.Windows.Forms.TabPage();
    this.rtbGlobObjFilter = new RichTextBox();
    this.label52 = new Label();
    this.globDelete = new System.Windows.Forms.Button();
    this.globAddLinkUp = new System.Windows.Forms.Button();
    this.gbGRIsps = new GroupBox();
    this.radioButton10 = new RadioButton();
    this.radioButton12 = new RadioButton();
    this.radioButton13 = new RadioButton();
    this.beGlobExcerpt = new ButtonEdit();
    this.lblGRootSelect = new Label();
    this.btnGlobExcClear = new System.Windows.Forms.Button();
    this.btnGlobExcCreate = new System.Windows.Forms.Button();
    this.label44 = new Label();
    this.globAddObjType = new System.Windows.Forms.Button();
    this.globAddLinkDown = new System.Windows.Forms.Button();
    this.tvGlobRoot = new TreeView();
    this.tabPage8 = new System.Windows.Forms.TabPage();
    this.gbSostav1 = new GroupBox();
    this.rbHideHiddenRoots1 = new RadioButton();
    this.rbHideHidden1 = new RadioButton();
    this.rbShowHidden1 = new RadioButton();
    this.cbConfigOptions1 = new System.Windows.Forms.CheckBox();
    this.panOpGlobalType = new Panel();
    this.tabControlGT = new System.Windows.Forms.TabControl();
    this.tabGTParms = new System.Windows.Forms.TabPage();
    this.button18 = new System.Windows.Forms.Button();
    this.btnAddForObjType = new System.Windows.Forms.Button();
    this.button7 = new System.Windows.Forms.Button();
    this.button8 = new System.Windows.Forms.Button();
    this.button9 = new System.Windows.Forms.Button();
    this.label48 = new Label();
    this.tvGTAttrs = new TreeView();
    this.gbIspForGT = new GroupBox();
    this.radioButton3 = new RadioButton();
    this.radioButton4 = new RadioButton();
    this.radioButton5 = new RadioButton();
    this.cbForObjTypes = new CheckedListBox();
    this.label42 = new Label();
    this.tabGTObjects = new System.Windows.Forms.TabPage();
    this.gtDelete = new System.Windows.Forms.Button();
    this.gtAddLinkUp = new System.Windows.Forms.Button();
    this.beGTExcerpt = new ButtonEdit();
    this.lblGTExcerpt = new Label();
    this.btnGTExcClear = new System.Windows.Forms.Button();
    this.btnGTExcCreate = new System.Windows.Forms.Button();
    this.label51 = new Label();
    this.gtAddObjType = new System.Windows.Forms.Button();
    this.gtAddLinkDown = new System.Windows.Forms.Button();
    this.tvGTSearch = new TreeView();
    this.rtGTCond = new RichTextBox();
    this.label49 = new Label();
    this.tabGTOther = new System.Windows.Forms.TabPage();
    this.gbSostav2 = new GroupBox();
    this.rbHideHiddenRoots2 = new RadioButton();
    this.rbHideHidden2 = new RadioButton();
    this.rbShowHidden2 = new RadioButton();
    this.cbConfigOptions2 = new System.Windows.Forms.CheckBox();
    this.panOpParmsEmpty = new Panel();
    this.IL2 = new ImageList(this.components);
    this.bottomDock = new DockContainer();
    this.dockDesc = new DockControl();
    this.lblDescr = new TextBox();
    this.topDock = new DockContainer();
    this.treePopMenu = new ContextMenuStrip(this.components);
    this.menuInsBefore = new ToolStripMenuItem();
    this.menuInsAfter = new ToolStripMenuItem();
    this.menuInsInto = new ToolStripMenuItem();
    this.menuItem6 = new ToolStripSeparator();
    this.menuChange = new ToolStripMenuItem();
    this.menuApply = new ToolStripMenuItem();
    this.menuItem9 = new ToolStripSeparator();
    this.menuCut = new ToolStripMenuItem();
    this.menuCopy = new ToolStripMenuItem();
    this.menuPaste = new ToolStripMenuItem();
    this.menuItem13 = new ToolStripSeparator();
    this.menuCollapse = new ToolStripMenuItem();
    this.menuExpand = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripSeparator();
    this.menuDelete = new ToolStripMenuItem();
    this.groupMenu = new ContextMenuStrip(this.components);
    this.menuItem2 = new ToolStripMenuItem();
    this.sortMenu = new ContextMenuStrip(this.components);
    this.menuItem1 = new ToolStripMenuItem();
    this.toolTip2 = new ToolTip(this.components);
    this.button17 = new System.Windows.Forms.Button();
    this.button13 = new System.Windows.Forms.Button();
    this.button16 = new System.Windows.Forms.Button();
    this.button11 = new System.Windows.Forms.Button();
    this.btnCheckTemplate = new System.Windows.Forms.Button();
    this.btnCheckDown = new System.Windows.Forms.Button();
    this.panelApply = new Panel();
    this.cbCheckout = new System.Windows.Forms.CheckBox();
    this.labelChecks = new Label();
    this.cbCoCreator = new System.Windows.Forms.CheckBox();
    this.btnLoadXml = new System.Windows.Forms.Button();
    this.lblDocType = new Label();
    this.btnDocParms = new System.Windows.Forms.Button();
    this.btnSaveXml = new System.Windows.Forms.Button();
    this.btnCancel = new System.Windows.Forms.Button();
    this.btnTemplate = new System.Windows.Forms.Button();
    this.btnCond = new System.Windows.Forms.Button();
    this.btnOK = new System.Windows.Forms.Button();
    this.btnSave = new System.Windows.Forms.Button();
    this.tree = new TreeList();
    this.Struct = new TreeListColumn();
    this.repositoryItemTextEdit2 = new RepositoryItemTextEdit();
    this.ModParms = new TreeListColumn();
    this.OperParms = new TreeListColumn();
    this.repositoryItemTextEdit1 = new RepositoryItemTextEdit();
    this.tipCon = new ToolTipController(this.components);
    this.sd = new SaveFileDialog();
    this.od = new OpenFileDialog();
    this.fontDialog1 = new FontDialog();
    this.checkBox1 = new System.Windows.Forms.CheckBox();
    this.checkBox2 = new System.Windows.Forms.CheckBox();
    this.btnTILink = new PanelButton();
    this.textColorDlg = new ColorDialog();
    this.panel13 = new Panel();
    this.leftDock.SuspendLayout();
    this.dockOpMod.SuspendLayout();
    this.panelScroll.SuspendLayout();
    this.panelControl.SuspendLayout();
    this.rightDock.SuspendLayout();
    this.dockModParms.SuspendLayout();
    this.panModParmsVersion.SuspendLayout();
    this.panel10.SuspendLayout();
    this.groupBox12.SuspendLayout();
    this.modPopMenu.SuspendLayout();
    this.panel9.SuspendLayout();
    this.groupBox11.SuspendLayout();
    this.panel8.SuspendLayout();
    this.groupBox9.SuspendLayout();
    this.panModParms5.SuspendLayout();
    this.panel2.SuspendLayout();
    this.btnForAttr.Properties.BeginInit();
    this.panel1.SuspendLayout();
    this.btnRefAttr.Properties.BeginInit();
    this.spinEdit1.Properties.BeginInit();
    this.panModParms4.SuspendLayout();
    this.gbResType.SuspendLayout();
    this.panModParms3.SuspendLayout();
    this.panModParms2.SuspendLayout();
    this.panModParms1.SuspendLayout();
    this.dockOpParms.SuspendLayout();
    this.panOpParms8.SuspendLayout();
    this.panel4.SuspendLayout();
    this.rashifrMenu.SuspendLayout();
    this.panel6.SuspendLayout();
    this.panel7.SuspendLayout();
    this.opPopMenu.SuspendLayout();
    this.editSelObject.Properties.BeginInit();
    this.panOpParmsE.SuspendLayout();
    this.panel11.SuspendLayout();
    this.groupBox14.SuspendLayout();
    this.groupBox13.SuspendLayout();
    this.groupBox10.SuspendLayout();
    this.beDocScript.Properties.BeginInit();
    this.beTypeForDoc.Properties.BeginInit();
    this.panOpParmsD.SuspendLayout();
    this.beCompObjType.Properties.BeginInit();
    this.beComplectType.Properties.BeginInit();
    this.panOpParms7.SuspendLayout();
    this.edObjType.Properties.BeginInit();
    this.panOpParms6.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.gbIdent.SuspendLayout();
    this.buttonEdit3.Properties.BeginInit();
    this.edSelTemplate.Properties.BeginInit();
    this.panOpParms5.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.buttonEdit2.Properties.BeginInit();
    this.textBox1.Properties.BeginInit();
    this.editAddAttr.Properties.BeginInit();
    this.edSaveNewID.Properties.BeginInit();
    this.panOpParms4.SuspendLayout();
    this.tabControl2.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.gbSetValue.SuspendLayout();
    this.gbAttr.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.gbFont.SuspendLayout();
    this.seFontSize.Properties.BeginInit();
    this.beFontName.Properties.BeginInit();
    this.panel12.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.buttonEdit1.Properties.BeginInit();
    this.textId.Properties.BeginInit();
    this.edAddAttr.Properties.BeginInit();
    this.panOpParms3.SuspendLayout();
    this.tabCon.SuspendLayout();
    this.tabFormula.SuspendLayout();
    this.gbSetParms.SuspendLayout();
    this.tabTable.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panSetArray.SuspendLayout();
    this.groupBox5.SuspendLayout();
    this.panOpParmsTI.SuspendLayout();
    this.beSourceType.Properties.BeginInit();
    this.beCreatingDocType.Properties.BeginInit();
    this.panOpParmsStyleB.SuspendLayout();
    this.panOpParmsStyleC.SuspendLayout();
    this.panOpParms2.SuspendLayout();
    this.btnObjLink.Properties.BeginInit();
    this.panOpParms9.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.panOpParms1.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.tabObjMain.SuspendLayout();
    this.btnEdExcerpt.Properties.BeginInit();
    this.elMoveMenu.SuspendLayout();
    this.tabObjSecond.SuspendLayout();
    this.groupBox8.SuspendLayout();
    this.groupBox7.SuspendLayout();
    this.tabObjTable.SuspendLayout();
    this.groupBox6.SuspendLayout();
    this.beCompareFunc.Properties.BeginInit();
    this.gbComposition.SuspendLayout();
    this.gbIspoln.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panOpParmsA.SuspendLayout();
    this.beUserProc.Properties.BeginInit();
    this.panOpParmsC.SuspendLayout();
    this.beNewList.Properties.BeginInit();
    this.panOpParmsB.SuspendLayout();
    this.gbVisZamens.SuspendLayout();
    this.edVersionRule.Properties.BeginInit();
    this.panGlobalTableFolder.SuspendLayout();
    this.tabGlobTable.SuspendLayout();
    this.tabPage5.SuspendLayout();
    this.beReplaceObjType.Properties.BeginInit();
    this.tabPage3.SuspendLayout();
    this.gbGRIsps.SuspendLayout();
    this.beGlobExcerpt.Properties.BeginInit();
    this.tabPage8.SuspendLayout();
    this.gbSostav1.SuspendLayout();
    this.panOpGlobalType.SuspendLayout();
    this.tabControlGT.SuspendLayout();
    this.tabGTParms.SuspendLayout();
    this.gbIspForGT.SuspendLayout();
    this.tabGTObjects.SuspendLayout();
    this.beGTExcerpt.Properties.BeginInit();
    this.tabGTOther.SuspendLayout();
    this.gbSostav2.SuspendLayout();
    this.bottomDock.SuspendLayout();
    this.dockDesc.SuspendLayout();
    this.treePopMenu.SuspendLayout();
    this.groupMenu.SuspendLayout();
    this.sortMenu.SuspendLayout();
    this.panelApply.SuspendLayout();
    this.tree.BeginInit();
    this.repositoryItemTextEdit2.BeginInit();
    this.repositoryItemTextEdit1.BeginInit();
    this.panel13.SuspendLayout();
    this.SuspendLayout();
    this.dockMan.DockingManager = DockingManager.Whidbey;
    this.dockMan.DocumentContainer = (DocumentContainer) null;
    this.dockMan.OwnerForm = (Form) this;
    this.leftDock.Controls.Add((Control) this.dockOpMod);
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("bb860bdf-02a2-482c-ae76-60b8c75b015e");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(284, 1565, new DockControl[1]
      {
        this.dockOpMod
      }, this.dockOpMod)
    });
    this.leftDock.Manager = this.dockMan;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    this.dockOpMod.Closable = false;
    this.dockOpMod.Controls.Add((Control) this.panelScroll);
    this.dockOpMod.Controls.Add((Control) this.TBar_Control);
    componentResourceManager.ApplyResources((object) this.dockOpMod, "dockOpMod");
    this.dockOpMod.FloatingLocation = new Point(515, 312);
    this.dockOpMod.Guid = new Guid("a579f993-9b31-4c79-9e76-5ea1737ba7c1");
    this.dockOpMod.Name = "dockOpMod";
    componentResourceManager.ApplyResources((object) this.panelScroll, "panelScroll");
    this.panelScroll.Controls.Add((Control) this.panelControl);
    this.panelScroll.Name = "panelScroll";
    this.panelControl.Controls.Add((Control) this.buttonsPanelObj);
    this.panelControl.Controls.Add((Control) this.buttonsPanelType);
    this.panelControl.Controls.Add((Control) this.buttonsPanelBy);
    this.panelControl.Controls.Add((Control) this.buttonsPanelMod);
    componentResourceManager.ApplyResources((object) this.panelControl, "panelControl");
    this.panelControl.Name = "panelControl";
    this.buttonsPanelObj.Buttons.Add(this.objParent);
    this.buttonsPanelObj.Buttons.Add(this.objChild);
    this.buttonsPanelObj.Buttons.Add(this.objSibling);
    this.buttonsPanelObj.Buttons.Add(this.objLinked);
    this.buttonsPanelObj.Buttons.Add(this.objAncestor);
    this.buttonsPanelObj.Buttons.Add(this.objDescendant);
    this.buttonsPanelObj.Buttons.Add(this.opExit);
    this.buttonsPanelObj.Buttons.Add(this.opFolder);
    this.buttonsPanelObj.Buttons.Add(this.opSelFolder);
    this.buttonsPanelObj.Buttons.Add(this.opSetting);
    this.buttonsPanelObj.Buttons.Add(this.docFillText);
    this.buttonsPanelObj.Buttons.Add(this.docNewElem);
    this.buttonsPanelObj.Buttons.Add(this.docSelectElem);
    this.buttonsPanelObj.Buttons.Add(this.docPaging);
    this.buttonsPanelObj.Buttons.Add(this.userProc);
    this.buttonsPanelObj.Buttons.Add(this.opCreateDoc);
    this.buttonsPanelObj.Buttons.Add(this.opCreateComplect);
    this.buttonsPanelObj.Buttons.Add(this.opCreateDocLink);
    this.buttonsPanelObj.Buttons.Add(this.opHardSet);
    this.buttonsPanelObj.ButtonSpacing = 0;
    componentResourceManager.ApplyResources((object) this.buttonsPanelObj, "buttonsPanelObj");
    this.buttonsPanelObj.ImageList = this.IL_50;
    this.buttonsPanelObj.Name = "buttonsPanelObj";
    this.objParent.ImageIndex = 9;
    this.objParent.Tag = (object) "9";
    componentResourceManager.ApplyResources((object) this.objParent, "objParent");
    this.objParent.Click += new EventHandler(this.TBar_ButtonClick);
    this.objChild.ImageIndex = 10;
    this.objChild.Tag = (object) "10";
    componentResourceManager.ApplyResources((object) this.objChild, "objChild");
    this.objChild.Click += new EventHandler(this.TBar_ButtonClick);
    this.objSibling.ImageIndex = 11;
    this.objSibling.Tag = (object) "11";
    componentResourceManager.ApplyResources((object) this.objSibling, "objSibling");
    this.objSibling.Click += new EventHandler(this.TBar_ButtonClick);
    this.objLinked.ImageIndex = 12;
    this.objLinked.Tag = (object) "12";
    componentResourceManager.ApplyResources((object) this.objLinked, "objLinked");
    this.objLinked.Click += new EventHandler(this.TBar_ButtonClick);
    this.objAncestor.ImageIndex = 13;
    this.objAncestor.Tag = (object) "13";
    componentResourceManager.ApplyResources((object) this.objAncestor, "objAncestor");
    this.objAncestor.Click += new EventHandler(this.TBar_ButtonClick);
    this.objDescendant.ImageIndex = 14;
    this.objDescendant.Tag = (object) "14";
    componentResourceManager.ApplyResources((object) this.objDescendant, "objDescendant");
    this.objDescendant.Click += new EventHandler(this.TBar_ButtonClick);
    this.opExit.ImageIndex = 15;
    this.opExit.Tag = (object) "15";
    componentResourceManager.ApplyResources((object) this.opExit, "opExit");
    this.opExit.Click += new EventHandler(this.TBar_ButtonClick);
    this.opFolder.ImageIndex = 16 /*0x10*/;
    this.opFolder.Tag = (object) "16";
    componentResourceManager.ApplyResources((object) this.opFolder, "opFolder");
    this.opFolder.Click += new EventHandler(this.TBar_ButtonClick);
    this.opSelFolder.ImageIndex = 17;
    this.opSelFolder.Tag = (object) "17";
    componentResourceManager.ApplyResources((object) this.opSelFolder, "opSelFolder");
    this.opSelFolder.Click += new EventHandler(this.TBar_ButtonClick);
    this.opSetting.ImageIndex = 18;
    this.opSetting.Tag = (object) "18";
    componentResourceManager.ApplyResources((object) this.opSetting, "opSetting");
    this.opSetting.Click += new EventHandler(this.TBar_ButtonClick);
    this.docFillText.ImageIndex = 19;
    this.docFillText.Tag = (object) "19";
    componentResourceManager.ApplyResources((object) this.docFillText, "docFillText");
    this.docFillText.Click += new EventHandler(this.TBar_ButtonClick);
    this.docNewElem.ImageIndex = 20;
    this.docNewElem.Tag = (object) "20";
    componentResourceManager.ApplyResources((object) this.docNewElem, "docNewElem");
    this.docNewElem.Click += new EventHandler(this.TBar_ButtonClick);
    this.docSelectElem.ImageIndex = 21;
    this.docSelectElem.Tag = (object) "21";
    componentResourceManager.ApplyResources((object) this.docSelectElem, "docSelectElem");
    this.docSelectElem.Click += new EventHandler(this.TBar_ButtonClick);
    this.docPaging.ImageIndex = 32 /*0x20*/;
    this.docPaging.Tag = (object) "32";
    componentResourceManager.ApplyResources((object) this.docPaging, "docPaging");
    this.docPaging.Click += new EventHandler(this.TBar_ButtonClick);
    this.userProc.ImageIndex = 43;
    this.userProc.Tag = (object) "43";
    componentResourceManager.ApplyResources((object) this.userProc, "userProc");
    this.userProc.Click += new EventHandler(this.TBar_ButtonClick);
    this.opCreateDoc.ImageIndex = 49;
    this.opCreateDoc.Tag = (object) "49";
    componentResourceManager.ApplyResources((object) this.opCreateDoc, "opCreateDoc");
    this.opCreateDoc.Click += new EventHandler(this.TBar_ButtonClick);
    this.opCreateComplect.ImageIndex = 50;
    this.opCreateComplect.Tag = (object) "50";
    componentResourceManager.ApplyResources((object) this.opCreateComplect, "opCreateComplect");
    this.opCreateComplect.Click += new EventHandler(this.TBar_ButtonClick);
    this.opCreateDocLink.ImageIndex = 66;
    this.opCreateDocLink.Tag = (object) "66";
    componentResourceManager.ApplyResources((object) this.opCreateDocLink, "opCreateDocLink");
    this.opCreateDocLink.Click += new EventHandler(this.TBar_ButtonClick);
    this.opHardSet.ImageIndex = 67;
    this.opHardSet.Tag = (object) "67";
    componentResourceManager.ApplyResources((object) this.opHardSet, "opHardSet");
    this.opHardSet.Click += new EventHandler(this.TBar_ButtonClick);
    this.IL_50.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL_50.ImageStream");
    this.IL_50.TransparentColor = Color.Magenta;
    this.IL_50.Images.SetKeyName(0, "для_всех.png");
    this.IL_50.Images.SetKeyName(1, "для_первого.png");
    this.IL_50.Images.SetKeyName(2, "для_минимума.png");
    this.IL_50.Images.SetKeyName(3, "для_максимума.png");
    this.IL_50.Images.SetKeyName(4, "если_существует.png");
    this.IL_50.Images.SetKeyName(5, "если_все.png");
    this.IL_50.Images.SetKeyName(6, "цикл.png");
    this.IL_50.Images.SetKeyName(7, "цикл_с_сортировкой.png");
    this.IL_50.Images.SetKeyName(8, "цикл_с_группировкой.png");
    this.IL_50.Images.SetKeyName(9, "родительские_объекты.png");
    this.IL_50.Images.SetKeyName(10, "дочерние_объекты.png");
    this.IL_50.Images.SetKeyName(11, "");
    this.IL_50.Images.SetKeyName(12, "связанные_объекты.png");
    this.IL_50.Images.SetKeyName(13, "содержащие_объекты.png");
    this.IL_50.Images.SetKeyName(14, "вложенные_объекты.png");
    this.IL_50.Images.SetKeyName(15, "выход_из_блока.png");
    this.IL_50.Images.SetKeyName(16 /*0x10*/, "блок.png");
    this.IL_50.Images.SetKeyName(17, "блок_выбора.png");
    this.IL_50.Images.SetKeyName(18, "присваивание.png");
    this.IL_50.Images.SetKeyName(19, "запись_в_поле.png");
    this.IL_50.Images.SetKeyName(20, "создать_элемент.png");
    this.IL_50.Images.SetKeyName(21, "выбор_элемента.png");
    this.IL_50.Images.SetKeyName(22, "вставить_перед.png");
    this.IL_50.Images.SetKeyName(23, "вставить_после.png");
    this.IL_50.Images.SetKeyName(24, "");
    this.IL_50.Images.SetKeyName(25, "считать_по_формуле.bmp");
    this.IL_50.Images.SetKeyName(26, "считать_по_таблице.bmp");
    this.IL_50.Images.SetKeyName(27, "считать_по_скрипту.bmp");
    this.IL_50.Images.SetKeyName(28, "вставить_внутрь.png");
    this.IL_50.Images.SetKeyName(29, "");
    this.IL_50.Images.SetKeyName(30, "");
    this.IL_50.Images.SetKeyName(31 /*0x1F*/, "вырезать.png");
    this.IL_50.Images.SetKeyName(32 /*0x20*/, "управление_документом.png");
    this.IL_50.Images.SetKeyName(33, "вставить.png");
    this.IL_50.Images.SetKeyName(34, "удалить.png");
    this.IL_50.Images.SetKeyName(35, "вверх_.png");
    this.IL_50.Images.SetKeyName(36, "вниз_.png");
    this.IL_50.Images.SetKeyName(37, "атрибуты_объектов.png");
    this.IL_50.Images.SetKeyName(38, "атрибуты_связей.png");
    this.IL_50.Images.SetKeyName(39, "вернуть_объект.bmp");
    this.IL_50.Images.SetKeyName(40, "пересчитать_атрибуты.bmp");
    this.IL_50.Images.SetKeyName(41, "атрибуты_объектов_группиров.png");
    this.IL_50.Images.SetKeyName(42, "атрибуты_связей_группировка.png");
    this.IL_50.Images.SetKeyName(43, "вызов_процедуры.png");
    this.IL_50.Images.SetKeyName(44, "Version_rule.bmp");
    this.IL_50.Images.SetKeyName(45, "сортировка_a_z.png");
    this.IL_50.Images.SetKeyName(46, "сортировка_z_a.png");
    this.IL_50.Images.SetKeyName(47, "вверх_.png");
    this.IL_50.Images.SetKeyName(48 /*0x30*/, "вниз_.png");
    this.IL_50.Images.SetKeyName(49, "создать_документ.png");
    this.IL_50.Images.SetKeyName(50, "создать_комплект.png");
    this.IL_50.Images.SetKeyName(51, "типы_объектов.png");
    this.IL_50.Images.SetKeyName(52, "lTypes.bmp");
    this.IL_50.Images.SetKeyName(53, "глобальная_таблица.png");
    this.IL_50.Images.SetKeyName(54, "глобальная_для_типов.png");
    this.IL_50.Images.SetKeyName(55, "add-upper.bmp");
    this.IL_50.Images.SetKeyName(56, "выборка.png");
    this.IL_50.Images.SetKeyName(57, "копировать.png");
    this.IL_50.Images.SetKeyName(58, "заменить.png");
    this.IL_50.Images.SetKeyName(59, "сохранить_изменения.bmp");
    this.IL_50.Images.SetKeyName(60, "add-lower.bmp");
    this.IL_50.Images.SetKeyName(61, "атрибут.png");
    this.IL_50.Images.SetKeyName(62, "нет_сортировки.png");
    this.IL_50.Images.SetKeyName(63 /*0x3F*/, "Preview.png");
    this.IL_50.Images.SetKeyName(64 /*0x40*/, "AllScheme.png");
    this.IL_50.Images.SetKeyName(65, "Relation.png");
    this.IL_50.Images.SetKeyName(66, "TiLink2.png");
    this.IL_50.Images.SetKeyName(67, "HardSet.png");
    this.IL_50.Images.SetKeyName(68, "LoopVersion2.png");
    this.buttonsPanelType.Buttons.Add(this.btnGlobalFolder);
    this.buttonsPanelType.Buttons.Add(this.TypeBtn);
    this.buttonsPanelType.Buttons.Add(this.btnGlobalRequest);
    this.buttonsPanelType.Buttons.Add(this.returnBtn);
    this.buttonsPanelType.Buttons.Add(this.recalcBtn);
    this.buttonsPanelType.Buttons.Add(this.verRule);
    componentResourceManager.ApplyResources((object) this.buttonsPanelType, "buttonsPanelType");
    this.buttonsPanelType.ImageList = this.IL_50;
    this.buttonsPanelType.Name = "buttonsPanelType";
    this.btnGlobalFolder.ImageIndex = 53;
    this.btnGlobalFolder.Tag = (object) "53";
    componentResourceManager.ApplyResources((object) this.btnGlobalFolder, "btnGlobalFolder");
    this.btnGlobalFolder.Click += new EventHandler(this.TBar_ButtonClick);
    this.TypeBtn.ImageIndex = 51;
    this.TypeBtn.Tag = (object) "51";
    componentResourceManager.ApplyResources((object) this.TypeBtn, "TypeBtn");
    this.TypeBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.btnGlobalRequest.ImageIndex = 54;
    this.btnGlobalRequest.Tag = (object) "54";
    componentResourceManager.ApplyResources((object) this.btnGlobalRequest, "btnGlobalRequest");
    this.btnGlobalRequest.Click += new EventHandler(this.TBar_ButtonClick);
    this.returnBtn.ImageIndex = 39;
    this.returnBtn.Tag = (object) "39";
    componentResourceManager.ApplyResources((object) this.returnBtn, "returnBtn");
    this.returnBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.recalcBtn.ImageIndex = 40;
    this.recalcBtn.Tag = (object) "40";
    componentResourceManager.ApplyResources((object) this.recalcBtn, "recalcBtn");
    this.recalcBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.verRule.ImageIndex = 44;
    this.verRule.Tag = (object) "44";
    componentResourceManager.ApplyResources((object) this.verRule, "verRule");
    this.verRule.Click += new EventHandler(this.TBar_ButtonClick);
    this.buttonsPanelBy.Buttons.Add(this.ByFormBtn);
    this.buttonsPanelBy.Buttons.Add(this.ByTableBtn);
    this.buttonsPanelBy.Buttons.Add(this.ByScriptBtn);
    this.buttonsPanelBy.ButtonSpacing = 0;
    componentResourceManager.ApplyResources((object) this.buttonsPanelBy, "buttonsPanelBy");
    this.buttonsPanelBy.ImageList = this.IL_50;
    this.buttonsPanelBy.Name = "buttonsPanelBy";
    this.ByFormBtn.ImageIndex = 25;
    this.ByFormBtn.Tag = (object) "25";
    componentResourceManager.ApplyResources((object) this.ByFormBtn, "ByFormBtn");
    this.ByFormBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.ByTableBtn.ImageIndex = 26;
    this.ByTableBtn.Tag = (object) "26";
    componentResourceManager.ApplyResources((object) this.ByTableBtn, "ByTableBtn");
    this.ByTableBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.ByScriptBtn.ImageIndex = 27;
    this.ByScriptBtn.Tag = (object) "27";
    componentResourceManager.ApplyResources((object) this.ByScriptBtn, "ByScriptBtn");
    this.ByScriptBtn.Click += new EventHandler(this.TBar_ButtonClick);
    this.buttonsPanelMod.Buttons.Add(this.modForEach);
    this.buttonsPanelMod.Buttons.Add(this.modForFirst);
    this.buttonsPanelMod.Buttons.Add(this.modForMin);
    this.buttonsPanelMod.Buttons.Add(this.modForMax);
    this.buttonsPanelMod.Buttons.Add(this.modIfExists);
    this.buttonsPanelMod.Buttons.Add(this.modIfAll);
    this.buttonsPanelMod.Buttons.Add(this.modCycle);
    this.buttonsPanelMod.Buttons.Add(this.modCycleSort);
    this.buttonsPanelMod.Buttons.Add(this.modCycleGroup);
    this.buttonsPanelMod.Buttons.Add(this.modVersions);
    this.buttonsPanelMod.ButtonSpacing = 0;
    componentResourceManager.ApplyResources((object) this.buttonsPanelMod, "buttonsPanelMod");
    this.buttonsPanelMod.ImageList = this.IL_50;
    this.buttonsPanelMod.Name = "buttonsPanelMod";
    this.modForEach.ImageIndex = 0;
    this.modForEach.Tag = (object) "0";
    componentResourceManager.ApplyResources((object) this.modForEach, "modForEach");
    this.modForEach.Click += new EventHandler(this.TBar_ButtonClick);
    this.modForFirst.ImageIndex = 1;
    this.modForFirst.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.modForFirst, "modForFirst");
    this.modForFirst.Click += new EventHandler(this.TBar_ButtonClick);
    this.modForMin.ImageIndex = 2;
    this.modForMin.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.modForMin, "modForMin");
    this.modForMin.Click += new EventHandler(this.TBar_ButtonClick);
    this.modForMax.ImageIndex = 3;
    this.modForMax.Tag = (object) "3";
    componentResourceManager.ApplyResources((object) this.modForMax, "modForMax");
    this.modForMax.Click += new EventHandler(this.TBar_ButtonClick);
    this.modIfExists.ImageIndex = 4;
    this.modIfExists.Tag = (object) "4";
    componentResourceManager.ApplyResources((object) this.modIfExists, "modIfExists");
    this.modIfExists.Click += new EventHandler(this.TBar_ButtonClick);
    this.modIfAll.ImageIndex = 5;
    this.modIfAll.Tag = (object) "5";
    componentResourceManager.ApplyResources((object) this.modIfAll, "modIfAll");
    this.modIfAll.Click += new EventHandler(this.TBar_ButtonClick);
    this.modCycle.ImageIndex = 6;
    this.modCycle.Tag = (object) "6";
    componentResourceManager.ApplyResources((object) this.modCycle, "modCycle");
    this.modCycle.Click += new EventHandler(this.TBar_ButtonClick);
    this.modCycleSort.ImageIndex = 7;
    this.modCycleSort.Tag = (object) "7";
    componentResourceManager.ApplyResources((object) this.modCycleSort, "modCycleSort");
    this.modCycleSort.Click += new EventHandler(this.TBar_ButtonClick);
    this.modCycleGroup.ImageIndex = 8;
    this.modCycleGroup.Tag = (object) "8";
    componentResourceManager.ApplyResources((object) this.modCycleGroup, "modCycleGroup");
    this.modCycleGroup.Click += new EventHandler(this.TBar_ButtonClick);
    this.modVersions.ImageIndex = 68;
    this.modVersions.Tag = (object) "68";
    componentResourceManager.ApplyResources((object) this.modVersions, "modVersions");
    this.modVersions.Click += new EventHandler(this.TBar_ButtonClick);
    this.TBar_Control.FullMenus = true;
    this.TBar_Control.Guid = new Guid("9c9e6477-5d13-4509-ba19-b98e94e48d89");
    this.TBar_Control.Hidden = false;
    this.TBar_Control.ImageList = this.IL_50;
    this.TBar_Control.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.addMenu,
      (ToolbarItemBase) this.btnChange,
      (ToolbarItemBase) this.btnApply,
      (ToolbarItemBase) this.clipMenu,
      (ToolbarItemBase) this.cmdDelete
    });
    componentResourceManager.ApplyResources((object) this.TBar_Control, "TBar_Control");
    this.TBar_Control.Name = "TBar_Control";
    componentResourceManager.ApplyResources((object) this.addMenu, "addMenu");
    this.addMenu.ImageIndex = 22;
    this.addMenu.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.cmdInsBefore,
      (ToolbarItemBase) this.cmdInsAfter,
      (ToolbarItemBase) this.cmdInsInto
    });
    this.addMenu.MenuImageList = this.IL_50;
    this.addMenu.ShowText = true;
    this.addMenu.Click += new EventHandler(this.addMenu_Click);
    componentResourceManager.ApplyResources((object) this.cmdInsBefore, "cmdInsBefore");
    this.cmdInsBefore.ImageIndex = 22;
    this.cmdInsBefore.ShowText = true;
    this.cmdInsBefore.Tag = (object) "0";
    this.cmdInsBefore.Click += new EventHandler(this.TBar_Control_ButtonClick);
    componentResourceManager.ApplyResources((object) this.cmdInsAfter, "cmdInsAfter");
    this.cmdInsAfter.ImageIndex = 23;
    this.cmdInsAfter.ShowText = true;
    this.cmdInsAfter.Tag = (object) "1";
    this.cmdInsAfter.Click += new EventHandler(this.TBar_Control_ButtonClick);
    componentResourceManager.ApplyResources((object) this.cmdInsInto, "cmdInsInto");
    this.cmdInsInto.ImageIndex = 28;
    this.cmdInsInto.ShowText = true;
    this.cmdInsInto.Tag = (object) "2";
    this.cmdInsInto.Click += new EventHandler(this.TBar_Control_ButtonClick);
    this.btnChange.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.ImageIndex = 58;
    this.btnChange.Tag = (object) "8";
    this.btnChange.Click += new EventHandler(this.TBar_Control_ButtonClick);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.ImageIndex = 59;
    this.btnApply.Tag = (object) "3";
    this.btnApply.Click += new EventHandler(this.TBar_Control_ButtonClick);
    this.clipMenu.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.clipMenu, "clipMenu");
    this.clipMenu.ImageIndex = 57;
    this.clipMenu.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.cmdCut,
      (ToolbarItemBase) this.cmdCopy,
      (ToolbarItemBase) this.cmdPaste
    });
    this.clipMenu.MenuImageList = this.IL_NEW;
    this.clipMenu.ShowText = true;
    componentResourceManager.ApplyResources((object) this.cmdCut, "cmdCut");
    this.cmdCut.ImageIndex = 31 /*0x1F*/;
    this.cmdCut.ShowText = true;
    this.cmdCut.Tag = (object) "4";
    this.cmdCut.Click += new EventHandler(this.TBar_Control_ButtonClick);
    componentResourceManager.ApplyResources((object) this.cmdCopy, "cmdCopy");
    this.cmdCopy.ImageIndex = 32 /*0x20*/;
    this.cmdCopy.ShowText = true;
    this.cmdCopy.Tag = (object) "5";
    this.cmdCopy.Click += new EventHandler(this.TBar_Control_ButtonClick);
    componentResourceManager.ApplyResources((object) this.cmdPaste, "cmdPaste");
    this.cmdPaste.ImageIndex = 33;
    this.cmdPaste.ShowText = true;
    this.cmdPaste.Tag = (object) "6";
    this.cmdPaste.Click += new EventHandler(this.TBar_Control_ButtonClick);
    this.IL_NEW.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL_NEW.ImageStream");
    this.IL_NEW.TransparentColor = Color.Magenta;
    this.IL_NEW.Images.SetKeyName(0, "");
    this.IL_NEW.Images.SetKeyName(1, "");
    this.IL_NEW.Images.SetKeyName(2, "");
    this.IL_NEW.Images.SetKeyName(3, "");
    this.IL_NEW.Images.SetKeyName(4, "");
    this.IL_NEW.Images.SetKeyName(5, "");
    this.IL_NEW.Images.SetKeyName(6, "");
    this.IL_NEW.Images.SetKeyName(7, "");
    this.IL_NEW.Images.SetKeyName(8, "");
    this.IL_NEW.Images.SetKeyName(9, "");
    this.IL_NEW.Images.SetKeyName(10, "");
    this.IL_NEW.Images.SetKeyName(11, "");
    this.IL_NEW.Images.SetKeyName(12, "");
    this.IL_NEW.Images.SetKeyName(13, "");
    this.IL_NEW.Images.SetKeyName(14, "");
    this.IL_NEW.Images.SetKeyName(15, "");
    this.IL_NEW.Images.SetKeyName(16 /*0x10*/, "");
    this.IL_NEW.Images.SetKeyName(17, "");
    this.IL_NEW.Images.SetKeyName(18, "");
    this.IL_NEW.Images.SetKeyName(19, "");
    this.IL_NEW.Images.SetKeyName(20, "создать_элемент.bmp");
    this.IL_NEW.Images.SetKeyName(21, "");
    this.IL_NEW.Images.SetKeyName(22, "");
    this.IL_NEW.Images.SetKeyName(23, "");
    this.IL_NEW.Images.SetKeyName(24, "");
    this.IL_NEW.Images.SetKeyName(25, "считать_по_формуле.bmp");
    this.IL_NEW.Images.SetKeyName(26, "считать_по_таблице.bmp");
    this.IL_NEW.Images.SetKeyName(27, "считать_по_скрипту.bmp");
    this.IL_NEW.Images.SetKeyName(28, "");
    this.IL_NEW.Images.SetKeyName(29, "");
    this.IL_NEW.Images.SetKeyName(30, "");
    this.IL_NEW.Images.SetKeyName(31 /*0x1F*/, "вырезать.bmp");
    this.IL_NEW.Images.SetKeyName(32 /*0x20*/, "копировать.bmp");
    this.IL_NEW.Images.SetKeyName(33, "вставить.bmp");
    this.IL_NEW.Images.SetKeyName(34, "удалить.bmp");
    this.IL_NEW.Images.SetKeyName(35, "");
    this.IL_NEW.Images.SetKeyName(36, "");
    this.IL_NEW.Images.SetKeyName(37, "атрибуты_объектов.bmp");
    this.IL_NEW.Images.SetKeyName(38, "атрибуты_связей.bmp");
    this.IL_NEW.Images.SetKeyName(39, "вернуть_объект.bmp");
    this.IL_NEW.Images.SetKeyName(40, "пересчитать_атрибуты.bmp");
    this.IL_NEW.Images.SetKeyName(41, "VVV4.bmp");
    this.IL_NEW.Images.SetKeyName(42, "VVV5.bmp");
    this.IL_NEW.Images.SetKeyName(43, "процудура_3.bmp");
    this.IL_NEW.Images.SetKeyName(44, "Version_rule.bmp");
    this.IL_NEW.Images.SetKeyName(45, "сортировка.bmp");
    this.IL_NEW.Images.SetKeyName(46, "сортировка2.bmp");
    this.IL_NEW.Images.SetKeyName(47, "TT1.bmp");
    this.IL_NEW.Images.SetKeyName(48 /*0x30*/, "TT2.bmp");
    this.IL_NEW.Images.SetKeyName(49, "создать_документ.bmp");
    this.IL_NEW.Images.SetKeyName(50, "создать_комплект.bmp");
    this.IL_NEW.Images.SetKeyName(51, "типы_объектов.bmp");
    this.IL_NEW.Images.SetKeyName(52, "lTypes.bmp");
    this.IL_NEW.Images.SetKeyName(53, "глобальная_таблица.bmp");
    this.IL_NEW.Images.SetKeyName(54, "глобальная_т_о_1.bmp");
    this.IL_NEW.Images.SetKeyName(55, "add-upper.bmp");
    this.IL_NEW.Images.SetKeyName(56, "выборка.bmp");
    this.IL_NEW.Images.SetKeyName(57, "управление_документом.bmp");
    this.IL_NEW.Images.SetKeyName(58, "замена.bmp");
    this.IL_NEW.Images.SetKeyName(59, "сохранить_изменения.bmp");
    this.IL_NEW.Images.SetKeyName(60, "add-lower.bmp");
    this.IL_NEW.Images.SetKeyName(61, "атрибут.png");
    this.cmdDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cmdDelete, "cmdDelete");
    this.cmdDelete.ImageIndex = 34;
    this.cmdDelete.Tag = (object) "7";
    this.cmdDelete.Click += new EventHandler(this.TBar_Control_ButtonClick);
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
    this.IL.Images.SetKeyName(10, "");
    this.IL.Images.SetKeyName(11, "");
    this.IL.Images.SetKeyName(12, "");
    this.IL.Images.SetKeyName(13, "");
    this.IL.Images.SetKeyName(14, "");
    this.IL.Images.SetKeyName(15, "");
    this.IL.Images.SetKeyName(16 /*0x10*/, "");
    this.IL.Images.SetKeyName(17, "");
    this.IL.Images.SetKeyName(18, "");
    this.IL.Images.SetKeyName(19, "");
    this.IL.Images.SetKeyName(20, "");
    this.IL.Images.SetKeyName(21, "");
    this.IL.Images.SetKeyName(22, "");
    this.IL.Images.SetKeyName(23, "");
    this.IL.Images.SetKeyName(24, "");
    this.IL.Images.SetKeyName(25, "");
    this.IL.Images.SetKeyName(26, "");
    this.IL.Images.SetKeyName(27, "");
    this.IL.Images.SetKeyName(28, "");
    this.IL.Images.SetKeyName(29, "");
    this.IL.Images.SetKeyName(30, "");
    this.IL.Images.SetKeyName(31 /*0x1F*/, "");
    this.IL.Images.SetKeyName(32 /*0x20*/, "");
    this.IL.Images.SetKeyName(33, "");
    this.IL.Images.SetKeyName(34, "");
    this.IL.Images.SetKeyName(35, "");
    this.IL.Images.SetKeyName(36, "");
    this.IL.Images.SetKeyName(37, "VVV2.bmp");
    this.IL.Images.SetKeyName(38, "VVV1.bmp");
    this.IL.Images.SetKeyName(39, "VVV3.bmp");
    this.IL.Images.SetKeyName(40, "SSS_18.bmp");
    this.IL.Images.SetKeyName(41, "VVV4.bmp");
    this.IL.Images.SetKeyName(42, "VVV5.bmp");
    this.IL.Images.SetKeyName(43, "VVV6.bmp");
    this.IL.Images.SetKeyName(44, "Version_rule.bmp");
    this.IL.Images.SetKeyName(45, "sort1.bmp");
    this.IL.Images.SetKeyName(46, "sort2.bmp");
    this.IL.Images.SetKeyName(47, "TT1.bmp");
    this.IL.Images.SetKeyName(48 /*0x30*/, "TT2.bmp");
    this.IL.Images.SetKeyName(49, "docpage.bmp");
    this.IL.Images.SetKeyName(50, "doccomplect.bmp");
    this.IL.Images.SetKeyName(51, "oTypes.bmp");
    this.IL.Images.SetKeyName(52, "lTypes.bmp");
    this.IL.Images.SetKeyName(53, "global-table.bmp");
    this.IL.Images.SetKeyName(54, "global-type.bmp");
    this.IL.Images.SetKeyName(55, "add-upper.bmp");
    this.rightDock.Controls.Add((Control) this.dockModParms);
    this.rightDock.Controls.Add((Control) this.dockOpParms);
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("6e6a26a6-bded-4fce-a58f-adada83878c3");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[2]
    {
      (LayoutSystemBase) new ControlLayoutSystem(572, 710, new DockControl[1]
      {
        this.dockModParms
      }, this.dockModParms),
      (LayoutSystemBase) new ControlLayoutSystem(572, 850, new DockControl[1]
      {
        this.dockOpParms
      }, this.dockOpParms)
    });
    this.rightDock.Manager = this.dockMan;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    this.rightDock.SizeChanged += new EventHandler(this.rightDock_SizeChanged);
    this.dockModParms.Closable = false;
    this.dockModParms.Controls.Add((Control) this.panModParmsVersion);
    this.dockModParms.Controls.Add((Control) this.panModParms5);
    this.dockModParms.Controls.Add((Control) this.panModParms4);
    this.dockModParms.Controls.Add((Control) this.panModParms3);
    this.dockModParms.Controls.Add((Control) this.panModParms2);
    this.dockModParms.Controls.Add((Control) this.panModParmsEmpty);
    this.dockModParms.Controls.Add((Control) this.panModParms1);
    componentResourceManager.ApplyResources((object) this.dockModParms, "dockModParms");
    this.dockModParms.FloatingLocation = new Point(515, 312);
    this.dockModParms.Guid = new Guid("8ded1e30-5d2b-44f6-92e6-9879e6224a0c");
    this.dockModParms.Name = "dockModParms";
    this.panModParmsVersion.Controls.Add((Control) this.panel10);
    this.panModParmsVersion.Controls.Add((Control) this.panel9);
    this.panModParmsVersion.Controls.Add((Control) this.panel8);
    componentResourceManager.ApplyResources((object) this.panModParmsVersion, "panModParmsVersion");
    this.panModParmsVersion.Name = "panModParmsVersion";
    this.panel10.Controls.Add((Control) this.groupBox12);
    componentResourceManager.ApplyResources((object) this.panel10, "panel10");
    this.panel10.Name = "panel10";
    this.groupBox12.Controls.Add((Control) this.richVerCond);
    componentResourceManager.ApplyResources((object) this.groupBox12, "groupBox12");
    this.groupBox12.Name = "groupBox12";
    this.groupBox12.TabStop = false;
    this.richVerCond.ContextMenuStrip = this.modPopMenu;
    componentResourceManager.ApplyResources((object) this.richVerCond, "richVerCond");
    this.richVerCond.Name = "richVerCond";
    this.modPopMenu.ImageScalingSize = new Size(24, 24);
    this.modPopMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.menuChangeModForm,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.copyToolStripMenuItem,
      (ToolStripItem) this.pasteToolStripMenuItem
    });
    this.modPopMenu.Name = "modPopMenu";
    componentResourceManager.ApplyResources((object) this.modPopMenu, "modPopMenu");
    this.modPopMenu.Opening += new CancelEventHandler(this.modPopMenu_Opening);
    this.menuChangeModForm.Name = "menuChangeModForm";
    componentResourceManager.ApplyResources((object) this.menuChangeModForm, "menuChangeModForm");
    this.menuChangeModForm.Click += new EventHandler(this.menuChangeModForm_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.copyToolStripMenuItem.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.копировать;
    this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.copyToolStripMenuItem, "copyToolStripMenuItem");
    this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
    this.pasteToolStripMenuItem.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.вставить;
    this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
    this.pasteToolStripMenuItem.Click += new EventHandler(this.pasteToolStripMenuItem_Click);
    this.panel9.Controls.Add((Control) this.groupBox11);
    componentResourceManager.ApplyResources((object) this.panel9, "panel9");
    this.panel9.Name = "panel9";
    this.groupBox11.Controls.Add((Control) this.cbVerDescending);
    this.groupBox11.Controls.Add((Control) this.cbSortVersions);
    componentResourceManager.ApplyResources((object) this.groupBox11, "groupBox11");
    this.groupBox11.Name = "groupBox11";
    this.groupBox11.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbVerDescending, "cbVerDescending");
    this.cbVerDescending.Name = "cbVerDescending";
    this.cbVerDescending.UseVisualStyleBackColor = true;
    this.cbVerDescending.CheckedChanged += new EventHandler(this.cbVerDescending_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbSortVersions, "cbSortVersions");
    this.cbSortVersions.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbSortVersions.FormattingEnabled = true;
    this.cbSortVersions.Items.AddRange(new object[6]
    {
      (object) componentResourceManager.GetString("cbSortVersions.Items"),
      (object) componentResourceManager.GetString("cbSortVersions.Items1"),
      (object) componentResourceManager.GetString("cbSortVersions.Items2"),
      (object) componentResourceManager.GetString("cbSortVersions.Items3"),
      (object) componentResourceManager.GetString("cbSortVersions.Items4"),
      (object) componentResourceManager.GetString("cbSortVersions.Items5")
    });
    this.cbSortVersions.Name = "cbSortVersions";
    this.cbSortVersions.SelectedIndexChanged += new EventHandler(this.cbSortVersions_SelectedIndexChanged);
    this.panel8.Controls.Add((Control) this.groupBox9);
    componentResourceManager.ApplyResources((object) this.panel8, "panel8");
    this.panel8.Name = "panel8";
    this.groupBox9.Controls.Add((Control) this.rbAllVersions);
    this.groupBox9.Controls.Add((Control) this.rbFirstVersion);
    componentResourceManager.ApplyResources((object) this.groupBox9, "groupBox9");
    this.groupBox9.Name = "groupBox9";
    this.groupBox9.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbAllVersions, "rbAllVersions");
    this.rbAllVersions.Name = "rbAllVersions";
    this.rbAllVersions.TabStop = true;
    this.rbAllVersions.UseVisualStyleBackColor = true;
    this.rbAllVersions.CheckedChanged += new EventHandler(this.rbAllVersions_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbFirstVersion, "rbFirstVersion");
    this.rbFirstVersion.Name = "rbFirstVersion";
    this.rbFirstVersion.TabStop = true;
    this.rbFirstVersion.UseVisualStyleBackColor = true;
    this.rbFirstVersion.CheckedChanged += new EventHandler(this.rbFirstVersions_CheckedChanged);
    this.panModParms5.Controls.Add((Control) this.panel2);
    this.panModParms5.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.panModParms5, "panModParms5");
    this.panModParms5.Name = "panModParms5";
    this.panel2.Controls.Add((Control) this.label57);
    this.panel2.Controls.Add((Control) this.checkDoWhile);
    this.panel2.Controls.Add((Control) this.richWhileCond);
    this.panel2.Controls.Add((Control) this.btnForAttr);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label57, "label57");
    this.label57.Name = "label57";
    this.checkDoWhile.Checked = true;
    componentResourceManager.ApplyResources((object) this.checkDoWhile, "checkDoWhile");
    this.checkDoWhile.Name = "checkDoWhile";
    this.checkDoWhile.TabStop = true;
    this.checkDoWhile.CheckedChanged += new EventHandler(this.checkDoWhile_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.richWhileCond, "richWhileCond");
    this.richWhileCond.BackColor = SystemColors.Window;
    this.richWhileCond.ContextMenuStrip = this.modPopMenu;
    this.richWhileCond.Name = "richWhileCond";
    this.richWhileCond.ReadOnly = true;
    this.richWhileCond.MouseDoubleClick += new MouseEventHandler(this.richWhileCond_MouseDoubleClick);
    this.richWhileCond.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    componentResourceManager.ApplyResources((object) this.btnForAttr, "btnForAttr");
    this.btnForAttr.Name = "btnForAttr";
    this.btnForAttr.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.btnForAttr.Properties.ButtonClick += new ButtonPressedEventHandler(this.btnForAttr_Properties_ButtonClick);
    this.panel1.Controls.Add((Control) this.checkMulti);
    this.panel1.Controls.Add((Control) this.btnRefAttr);
    this.panel1.Controls.Add((Control) this.checkFor);
    this.panel1.Controls.Add((Control) this.richForEnd);
    this.panel1.Controls.Add((Control) this.label17);
    this.panel1.Controls.Add((Control) this.spinEdit1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.checkMulti, "checkMulti");
    this.checkMulti.Name = "checkMulti";
    this.checkMulti.CheckedChanged += new EventHandler(this.checkDoWhile_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnRefAttr, "btnRefAttr");
    this.btnRefAttr.Name = "btnRefAttr";
    this.btnRefAttr.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.btnRefAttr.Properties.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit4_Properties_ButtonClick_1);
    componentResourceManager.ApplyResources((object) this.checkFor, "checkFor");
    this.checkFor.Name = "checkFor";
    this.checkFor.CheckedChanged += new EventHandler(this.checkDoWhile_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.richForEnd, "richForEnd");
    this.richForEnd.BackColor = SystemColors.Window;
    this.richForEnd.ContextMenuStrip = this.modPopMenu;
    this.richForEnd.Name = "richForEnd";
    this.richForEnd.ReadOnly = true;
    this.richForEnd.MouseDoubleClick += new MouseEventHandler(this.richForEnd_MouseDoubleClick);
    this.richForEnd.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    componentResourceManager.ApplyResources((object) this.label17, "label17");
    this.label17.Name = "label17";
    componentResourceManager.ApplyResources((object) this.spinEdit1, "spinEdit1");
    this.spinEdit1.Name = "spinEdit1";
    this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.spinEdit1.Properties.UseCtrlIncrement = false;
    this.spinEdit1.EditValueChanged += new EventHandler(this.spinEdit1_EditValueChanged);
    this.panModParms4.Controls.Add((Control) this.richTextBox2);
    this.panModParms4.Controls.Add((Control) this.label10);
    this.panModParms4.Controls.Add((Control) this.gbResType);
    componentResourceManager.ApplyResources((object) this.panModParms4, "panModParms4");
    this.panModParms4.Name = "panModParms4";
    componentResourceManager.ApplyResources((object) this.richTextBox2, "richTextBox2");
    this.richTextBox2.BackColor = SystemColors.Window;
    this.richTextBox2.ContextMenuStrip = this.modPopMenu;
    this.richTextBox2.Name = "richTextBox2";
    this.richTextBox2.ReadOnly = true;
    this.richTextBox2.MouseDoubleClick += new MouseEventHandler(this.richTextBox2_MouseDoubleClick);
    this.richTextBox2.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextBox2.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.gbResType, "gbResType");
    this.gbResType.Controls.Add((Control) this.checkMeasured);
    this.gbResType.Controls.Add((Control) this.checkDate);
    this.gbResType.Controls.Add((Control) this.checkFloat);
    this.gbResType.Controls.Add((Control) this.checkString);
    this.gbResType.Controls.Add((Control) this.checkInt);
    this.gbResType.Name = "gbResType";
    this.gbResType.TabStop = false;
    componentResourceManager.ApplyResources((object) this.checkMeasured, "checkMeasured");
    this.checkMeasured.Name = "checkMeasured";
    this.checkMeasured.TabStop = true;
    this.checkMeasured.UseVisualStyleBackColor = true;
    this.checkMeasured.CheckedChanged += new EventHandler(this.checkInt_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.checkDate, "checkDate");
    this.checkDate.Name = "checkDate";
    this.checkDate.CheckedChanged += new EventHandler(this.checkInt_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.checkFloat, "checkFloat");
    this.checkFloat.Name = "checkFloat";
    this.checkFloat.CheckedChanged += new EventHandler(this.checkInt_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.checkString, "checkString");
    this.checkString.Name = "checkString";
    this.checkString.CheckedChanged += new EventHandler(this.checkInt_CheckedChanged);
    this.checkInt.Checked = true;
    componentResourceManager.ApplyResources((object) this.checkInt, "checkInt");
    this.checkInt.Name = "checkInt";
    this.checkInt.TabStop = true;
    this.checkInt.CheckedChanged += new EventHandler(this.checkInt_CheckedChanged);
    this.panModParms3.Controls.Add((Control) this.cbInbSort);
    this.panModParms3.Controls.Add((Control) this.tvAttrs);
    this.panModParms3.Controls.Add((Control) this.btnDelAttr);
    this.panModParms3.Controls.Add((Control) this.btnAddLink);
    this.panModParms3.Controls.Add((Control) this.btnAddObj);
    this.panModParms3.Controls.Add((Control) this.label11);
    componentResourceManager.ApplyResources((object) this.panModParms3, "panModParms3");
    this.panModParms3.Name = "panModParms3";
    componentResourceManager.ApplyResources((object) this.cbInbSort, "cbInbSort");
    this.cbInbSort.Name = "cbInbSort";
    this.cbInbSort.UseVisualStyleBackColor = true;
    this.cbInbSort.CheckedChanged += new EventHandler(this.cbInbSort_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.tvAttrs, "tvAttrs");
    this.tvAttrs.ImageList = this.IL_50;
    this.tvAttrs.Name = "tvAttrs";
    this.tvAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvAttrs.Nodes1")
    });
    this.tvAttrs.ShowRootLines = false;
    componentResourceManager.ApplyResources((object) this.btnDelAttr, "btnDelAttr");
    this.btnDelAttr.ImageList = this.IL_50;
    this.btnDelAttr.Name = "btnDelAttr";
    this.toolTip2.SetToolTip((Control) this.btnDelAttr, componentResourceManager.GetString("btnDelAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDelAttr, "Не использовать атрибут");
    this.btnDelAttr.UseVisualStyleBackColor = true;
    this.btnDelAttr.Click += new EventHandler(this.btnDelAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddLink, "btnAddLink");
    this.btnAddLink.ImageList = this.IL_50;
    this.btnAddLink.Name = "btnAddLink";
    this.toolTip2.SetToolTip((Control) this.btnAddLink, componentResourceManager.GetString("btnAddLink.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddLink, "Добавить атрибут связи");
    this.btnAddLink.UseVisualStyleBackColor = true;
    this.btnAddLink.Click += new EventHandler(this.btnAddSortAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddObj, "btnAddObj");
    this.btnAddObj.ImageList = this.IL_50;
    this.btnAddObj.Name = "btnAddObj";
    this.toolTip2.SetToolTip((Control) this.btnAddObj, componentResourceManager.GetString("btnAddObj.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddObj, "Добавить атрибут объекта");
    this.btnAddObj.UseVisualStyleBackColor = true;
    this.btnAddObj.Click += new EventHandler(this.btnAddSortAttr_Click);
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    this.panModParms2.Controls.Add((Control) this.tvSortGroup);
    this.panModParms2.Controls.Add((Control) this.btnAddGroupLink);
    this.panModParms2.Controls.Add((Control) this.btnAddGroup);
    this.panModParms2.Controls.Add((Control) this.btnSortDel);
    this.panModParms2.Controls.Add((Control) this.btnAddSortLink);
    this.panModParms2.Controls.Add((Control) this.btnAddSort);
    this.panModParms2.Controls.Add((Control) this.label14);
    componentResourceManager.ApplyResources((object) this.panModParms2, "panModParms2");
    this.panModParms2.Name = "panModParms2";
    componentResourceManager.ApplyResources((object) this.tvSortGroup, "tvSortGroup");
    this.tvSortGroup.ImageList = this.IL_50;
    this.tvSortGroup.Name = "tvSortGroup";
    this.tvSortGroup.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvSortGroup.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvSortGroup.Nodes1")
    });
    this.tvSortGroup.ShowRootLines = false;
    componentResourceManager.ApplyResources((object) this.btnAddGroupLink, "btnAddGroupLink");
    this.btnAddGroupLink.ImageList = this.IL_50;
    this.btnAddGroupLink.Name = "btnAddGroupLink";
    this.toolTip2.SetToolTip((Control) this.btnAddGroupLink, componentResourceManager.GetString("btnAddGroupLink.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddGroupLink, "Атрибуты связи для группировки");
    this.btnAddGroupLink.UseVisualStyleBackColor = true;
    this.btnAddGroupLink.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.btnAddGroup, "btnAddGroup");
    this.btnAddGroup.ImageList = this.IL_50;
    this.btnAddGroup.Name = "btnAddGroup";
    this.toolTip2.SetToolTip((Control) this.btnAddGroup, componentResourceManager.GetString("btnAddGroup.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddGroup, "Атрибуты объекта для группировки");
    this.btnAddGroup.UseVisualStyleBackColor = true;
    this.btnAddGroup.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.btnSortDel, "btnSortDel");
    this.btnSortDel.ImageList = this.IL_50;
    this.btnSortDel.Name = "btnSortDel";
    this.toolTip2.SetToolTip((Control) this.btnSortDel, componentResourceManager.GetString("btnSortDel.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnSortDel, "Не использовать атрибут");
    this.btnSortDel.UseVisualStyleBackColor = true;
    this.btnSortDel.Click += new EventHandler(this.btnSortDel_Click);
    componentResourceManager.ApplyResources((object) this.btnAddSortLink, "btnAddSortLink");
    this.btnAddSortLink.ImageList = this.IL_50;
    this.btnAddSortLink.Name = "btnAddSortLink";
    this.toolTip2.SetToolTip((Control) this.btnAddSortLink, componentResourceManager.GetString("btnAddSortLink.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddSortLink, "Атрибуты связи для сортировки");
    this.btnAddSortLink.UseVisualStyleBackColor = true;
    this.btnAddSortLink.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.btnAddSort, "btnAddSort");
    this.btnAddSort.ImageList = this.IL_50;
    this.btnAddSort.Name = "btnAddSort";
    this.toolTip2.SetToolTip((Control) this.btnAddSort, componentResourceManager.GetString("btnAddSort.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddSort, "Атрибуты объекта для сортировки");
    this.btnAddSort.UseVisualStyleBackColor = true;
    this.btnAddSort.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.label14, "label14");
    this.label14.Name = "label14";
    componentResourceManager.ApplyResources((object) this.panModParmsEmpty, "panModParmsEmpty");
    this.panModParmsEmpty.Name = "panModParmsEmpty";
    this.panModParms1.Controls.Add((Control) this.cbForAllIsps);
    this.panModParms1.Controls.Add((Control) this.cbSaveContext);
    this.panModParms1.Controls.Add((Control) this.richTextBox1);
    this.panModParms1.Controls.Add((Control) this.label9);
    componentResourceManager.ApplyResources((object) this.panModParms1, "panModParms1");
    this.panModParms1.Name = "panModParms1";
    componentResourceManager.ApplyResources((object) this.cbForAllIsps, "cbForAllIsps");
    this.cbForAllIsps.Name = "cbForAllIsps";
    this.cbForAllIsps.UseVisualStyleBackColor = true;
    this.cbForAllIsps.CheckedChanged += new EventHandler(this.cbForAllIsps_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbSaveContext, "cbSaveContext");
    this.cbSaveContext.Name = "cbSaveContext";
    this.cbSaveContext.UseVisualStyleBackColor = true;
    this.cbSaveContext.CheckedChanged += new EventHandler(this.cbSaveContext_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.richTextBox1, "richTextBox1");
    this.richTextBox1.BackColor = SystemColors.Window;
    this.richTextBox1.ContextMenuStrip = this.modPopMenu;
    this.richTextBox1.Name = "richTextBox1";
    this.richTextBox1.ReadOnly = true;
    this.richTextBox1.DoubleClick += new EventHandler(this.menuChangeModForm_Click);
    this.richTextBox1.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextBox1.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    this.dockOpParms.Closable = false;
    this.dockOpParms.Controls.Add((Control) this.panOpParms8);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsE);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsD);
    this.dockOpParms.Controls.Add((Control) this.panOpParms7);
    this.dockOpParms.Controls.Add((Control) this.panOpParms6);
    this.dockOpParms.Controls.Add((Control) this.panOpParms5);
    this.dockOpParms.Controls.Add((Control) this.panOpParms4);
    this.dockOpParms.Controls.Add((Control) this.panOpParms3);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsTI);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsStyleB);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsStyleC);
    this.dockOpParms.Controls.Add((Control) this.panOpParms2);
    this.dockOpParms.Controls.Add((Control) this.panOpParms9);
    this.dockOpParms.Controls.Add((Control) this.panOpParms1);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsA);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsC);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsB);
    this.dockOpParms.Controls.Add((Control) this.panGlobalTableFolder);
    this.dockOpParms.Controls.Add((Control) this.panOpGlobalType);
    this.dockOpParms.Controls.Add((Control) this.panOpParmsEmpty);
    componentResourceManager.ApplyResources((object) this.dockOpParms, "dockOpParms");
    this.dockOpParms.FloatingLocation = new Point(515, 312);
    this.dockOpParms.Guid = new Guid("1896adfc-b32d-4cff-b029-89e697c851d5");
    this.dockOpParms.Name = "dockOpParms";
    this.dockOpParms.SizeChanged += new EventHandler(this.dockOpParms_SizeChanged);
    this.panOpParms8.Controls.Add((Control) this.panel4);
    this.panOpParms8.Controls.Add((Control) this.panel6);
    this.panOpParms8.Controls.Add((Control) this.panel7);
    componentResourceManager.ApplyResources((object) this.panOpParms8, "panOpParms8");
    this.panOpParms8.Name = "panOpParms8";
    this.panel4.Controls.Add((Control) this.richInnerCond);
    this.panel4.Controls.Add((Control) this.label37);
    this.panel4.Controls.Add((Control) this.panel13);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.btnUpdate, "btnUpdate");
    this.btnUpdate.Name = "btnUpdate";
    this.btnUpdate.UseVisualStyleBackColor = true;
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    this.richInnerCond.ContextMenuStrip = this.rashifrMenu;
    componentResourceManager.ApplyResources((object) this.richInnerCond, "richInnerCond");
    this.richInnerCond.Name = "richInnerCond";
    this.richInnerCond.ReadOnly = true;
    this.rashifrMenu.ImageScalingSize = new Size(24, 24);
    this.rashifrMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.rashifrItem
    });
    this.rashifrMenu.Name = "rashifrMenu";
    componentResourceManager.ApplyResources((object) this.rashifrMenu, "rashifrMenu");
    this.rashifrItem.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources._7;
    this.rashifrItem.Name = "rashifrItem";
    componentResourceManager.ApplyResources((object) this.rashifrItem, "rashifrItem");
    this.rashifrItem.Click += new EventHandler(this.rashifrItem_Click);
    componentResourceManager.ApplyResources((object) this.label37, "label37");
    this.label37.Name = "label37";
    this.panel6.Controls.Add((Control) this.tbConds);
    this.panel6.Controls.Add((Control) this.label38);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.tbConds, "tbConds");
    this.tbConds.Cursor = Cursors.No;
    this.tbConds.Name = "tbConds";
    this.tbConds.ReadOnly = true;
    this.tbConds.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label38, "label38");
    this.label38.Name = "label38";
    this.panel7.Controls.Add((Control) this.richTextBox4);
    this.panel7.Controls.Add((Control) this.label19);
    this.panel7.Controls.Add((Control) this.editSelObject);
    this.panel7.Controls.Add((Control) this.labelObjType);
    componentResourceManager.ApplyResources((object) this.panel7, "panel7");
    this.panel7.Name = "panel7";
    componentResourceManager.ApplyResources((object) this.richTextBox4, "richTextBox4");
    this.richTextBox4.BackColor = SystemColors.Window;
    this.richTextBox4.ContextMenuStrip = this.opPopMenu;
    this.richTextBox4.Name = "richTextBox4";
    this.richTextBox4.ReadOnly = true;
    this.richTextBox4.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richTextBox4.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextBox4.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    this.opPopMenu.ImageScalingSize = new Size(24, 24);
    this.opPopMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.menuChangeOpForm,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.copyOpToolStripMenuItem,
      (ToolStripItem) this.pasteOpToolStripMenuItem
    });
    this.opPopMenu.Name = "opPopMenu";
    componentResourceManager.ApplyResources((object) this.opPopMenu, "opPopMenu");
    this.opPopMenu.Opening += new CancelEventHandler(this.opPopMenu_Opening);
    this.menuChangeOpForm.Name = "menuChangeOpForm";
    componentResourceManager.ApplyResources((object) this.menuChangeOpForm, "menuChangeOpForm");
    this.menuChangeOpForm.Click += new EventHandler(this.menuChangeOpForm_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem2, "toolStripMenuItem2");
    this.copyOpToolStripMenuItem.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.копировать;
    this.copyOpToolStripMenuItem.Name = "copyOpToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.copyOpToolStripMenuItem, "copyOpToolStripMenuItem");
    this.copyOpToolStripMenuItem.Click += new EventHandler(this.copyOpToolStripMenuItem_Click);
    this.pasteOpToolStripMenuItem.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.вставить;
    this.pasteOpToolStripMenuItem.Name = "pasteOpToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.pasteOpToolStripMenuItem, "pasteOpToolStripMenuItem");
    this.pasteOpToolStripMenuItem.Click += new EventHandler(this.pasteOpToolStripMenuItem_Click);
    componentResourceManager.ApplyResources((object) this.label19, "label19");
    this.label19.Name = "label19";
    componentResourceManager.ApplyResources((object) this.editSelObject, "editSelObject");
    this.editSelObject.Name = "editSelObject";
    this.editSelObject.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.editSelObject.Properties.ReadOnly = true;
    this.editSelObject.Properties.ButtonClick += new ButtonPressedEventHandler(this.editSelObject_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.labelObjType, "labelObjType");
    this.labelObjType.Name = "labelObjType";
    this.panOpParmsE.Controls.Add((Control) this.panel11);
    this.panOpParmsE.Controls.Add((Control) this.label58);
    this.panOpParmsE.Controls.Add((Control) this.rbTechcard);
    this.panOpParmsE.Controls.Add((Control) this.cbNoCount);
    this.panOpParmsE.Controls.Add((Control) this.cbNoNumber);
    this.panOpParmsE.Controls.Add((Control) this.cbCoWorkerDoc);
    this.panOpParmsE.Controls.Add((Control) this.groupBox10);
    this.panOpParmsE.Controls.Add((Control) this.rbScenario);
    this.panOpParmsE.Controls.Add((Control) this.rbDocScript);
    this.panOpParmsE.Controls.Add((Control) this.label34);
    this.panOpParmsE.Controls.Add((Control) this.textPrefix);
    this.panOpParmsE.Controls.Add((Control) this.cbSecondPass);
    this.panOpParmsE.Controls.Add((Control) this.cbNoEmpty);
    this.panOpParmsE.Controls.Add((Control) this.beDocScript);
    this.panOpParmsE.Controls.Add((Control) this.beTypeForDoc);
    this.panOpParmsE.Controls.Add((Control) this.label32);
    componentResourceManager.ApplyResources((object) this.panOpParmsE, "panOpParmsE");
    this.panOpParmsE.Name = "panOpParmsE";
    this.panel11.Controls.Add((Control) this.groupBox14);
    this.panel11.Controls.Add((Control) this.splitter1);
    this.panel11.Controls.Add((Control) this.groupBox13);
    componentResourceManager.ApplyResources((object) this.panel11, "panel11");
    this.panel11.Name = "panel11";
    this.groupBox14.Controls.Add((Control) this.richDocCond);
    componentResourceManager.ApplyResources((object) this.groupBox14, "groupBox14");
    this.groupBox14.Name = "groupBox14";
    this.groupBox14.TabStop = false;
    this.richDocCond.BackColor = SystemColors.Window;
    this.richDocCond.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richDocCond, "richDocCond");
    this.richDocCond.Name = "richDocCond";
    this.richDocCond.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.groupBox13.Controls.Add((Control) this.richDocCondBefore);
    componentResourceManager.ApplyResources((object) this.groupBox13, "groupBox13");
    this.groupBox13.Name = "groupBox13";
    this.groupBox13.TabStop = false;
    this.richDocCondBefore.BackColor = SystemColors.Window;
    this.richDocCondBefore.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richDocCondBefore, "richDocCondBefore");
    this.richDocCondBefore.Name = "richDocCondBefore";
    this.richDocCondBefore.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.label58, "label58");
    this.label58.Name = "label58";
    componentResourceManager.ApplyResources((object) this.rbTechcard, "rbTechcard");
    this.rbTechcard.Name = "rbTechcard";
    this.rbTechcard.Tag = (object) "T";
    this.rbTechcard.UseVisualStyleBackColor = true;
    this.rbTechcard.CheckedChanged += new EventHandler(this.rbScenario_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbNoCount, "cbNoCount");
    this.cbNoCount.Name = "cbNoCount";
    this.cbNoCount.UseVisualStyleBackColor = true;
    this.cbNoCount.CheckedChanged += new EventHandler(this.cbNoCount_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbNoNumber, "cbNoNumber");
    this.cbNoNumber.Name = "cbNoNumber";
    this.cbNoNumber.UseVisualStyleBackColor = true;
    this.cbNoNumber.CheckedChanged += new EventHandler(this.cbNoNumber_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbCoWorkerDoc, "cbCoWorkerDoc");
    this.cbCoWorkerDoc.Name = "cbCoWorkerDoc";
    this.cbCoWorkerDoc.UseVisualStyleBackColor = true;
    this.cbCoWorkerDoc.CheckedChanged += new EventHandler(this.cbCoWorkerDoc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.groupBox10, "groupBox10");
    this.groupBox10.Controls.Add((Control) this.rbGroupAll);
    this.groupBox10.Controls.Add((Control) this.rbGroupCont);
    this.groupBox10.Controls.Add((Control) this.rbNoGroup);
    this.groupBox10.Name = "groupBox10";
    this.groupBox10.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbGroupAll, "rbGroupAll");
    this.rbGroupAll.Name = "rbGroupAll";
    this.rbGroupAll.Tag = (object) "2";
    this.rbGroupAll.UseVisualStyleBackColor = true;
    this.rbGroupAll.CheckedChanged += new EventHandler(this.rbNoGroup_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbGroupCont, "rbGroupCont");
    this.rbGroupCont.Name = "rbGroupCont";
    this.rbGroupCont.Tag = (object) "1";
    this.rbGroupCont.UseVisualStyleBackColor = true;
    this.rbGroupCont.CheckedChanged += new EventHandler(this.rbNoGroup_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbNoGroup, "rbNoGroup");
    this.rbNoGroup.Checked = true;
    this.rbNoGroup.Name = "rbNoGroup";
    this.rbNoGroup.TabStop = true;
    this.rbNoGroup.Tag = (object) "0";
    this.rbNoGroup.UseVisualStyleBackColor = true;
    this.rbNoGroup.CheckedChanged += new EventHandler(this.rbNoGroup_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbScenario, "rbScenario");
    this.rbScenario.Name = "rbScenario";
    this.rbScenario.Tag = (object) "Y";
    this.rbScenario.UseVisualStyleBackColor = true;
    this.rbScenario.CheckedChanged += new EventHandler(this.rbScenario_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbDocScript, "rbDocScript");
    this.rbDocScript.Checked = true;
    this.rbDocScript.Name = "rbDocScript";
    this.rbDocScript.TabStop = true;
    this.rbDocScript.Tag = (object) "N";
    this.rbDocScript.UseVisualStyleBackColor = true;
    this.rbDocScript.CheckedChanged += new EventHandler(this.rbScenario_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label34, "label34");
    this.label34.Name = "label34";
    componentResourceManager.ApplyResources((object) this.textPrefix, "textPrefix");
    this.textPrefix.Name = "textPrefix";
    this.textPrefix.TextChanged += new EventHandler(this.textPrefix_TextChanged);
    componentResourceManager.ApplyResources((object) this.cbSecondPass, "cbSecondPass");
    this.cbSecondPass.Name = "cbSecondPass";
    this.cbSecondPass.UseVisualStyleBackColor = true;
    this.cbSecondPass.CheckedChanged += new EventHandler(this.cbSecondPass_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbNoEmpty, "cbNoEmpty");
    this.cbNoEmpty.Name = "cbNoEmpty";
    this.cbNoEmpty.UseVisualStyleBackColor = true;
    this.cbNoEmpty.CheckedChanged += new EventHandler(this.cbNoEmpty_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.beDocScript, "beDocScript");
    this.beDocScript.Name = "beDocScript";
    this.beDocScript.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beDocScript.Properties.ReadOnly = true;
    this.beDocScript.ButtonClick += new ButtonPressedEventHandler(this.beDocScript_ButtonClick);
    componentResourceManager.ApplyResources((object) this.beTypeForDoc, "beTypeForDoc");
    this.beTypeForDoc.Name = "beTypeForDoc";
    this.beTypeForDoc.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beTypeForDoc.Properties.ReadOnly = true;
    this.beTypeForDoc.ButtonClick += new ButtonPressedEventHandler(this.edObjType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label32, "label32");
    this.label32.Name = "label32";
    this.panOpParmsD.Controls.Add((Control) this.cbAdditionalComp);
    this.panOpParmsD.Controls.Add((Control) this.cbCoWorkerComp);
    this.panOpParmsD.Controls.Add((Control) this.label33);
    this.panOpParmsD.Controls.Add((Control) this.tbPostfix);
    this.panOpParmsD.Controls.Add((Control) this.label35);
    this.panOpParmsD.Controls.Add((Control) this.beCompObjType);
    this.panOpParmsD.Controls.Add((Control) this.cbCreateComplect);
    this.panOpParmsD.Controls.Add((Control) this.label13);
    this.panOpParmsD.Controls.Add((Control) this.richComplectCond);
    this.panOpParmsD.Controls.Add((Control) this.beComplectType);
    this.panOpParmsD.Controls.Add((Control) this.label15);
    componentResourceManager.ApplyResources((object) this.panOpParmsD, "panOpParmsD");
    this.panOpParmsD.Name = "panOpParmsD";
    componentResourceManager.ApplyResources((object) this.cbAdditionalComp, "cbAdditionalComp");
    this.cbAdditionalComp.Name = "cbAdditionalComp";
    this.cbAdditionalComp.UseVisualStyleBackColor = true;
    this.cbAdditionalComp.CheckedChanged += new EventHandler(this.cbAdditionalComp_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbCoWorkerComp, "cbCoWorkerComp");
    this.cbCoWorkerComp.Name = "cbCoWorkerComp";
    this.cbCoWorkerComp.UseVisualStyleBackColor = true;
    this.cbCoWorkerComp.CheckedChanged += new EventHandler(this.cbCoWorkerComp_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label33, "label33");
    this.label33.Name = "label33";
    componentResourceManager.ApplyResources((object) this.tbPostfix, "tbPostfix");
    this.tbPostfix.Name = "tbPostfix";
    this.tbPostfix.TextChanged += new EventHandler(this.tbPostfix_TextChanged);
    componentResourceManager.ApplyResources((object) this.label35, "label35");
    this.label35.Name = "label35";
    componentResourceManager.ApplyResources((object) this.beCompObjType, "beCompObjType");
    this.beCompObjType.Name = "beCompObjType";
    this.beCompObjType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beCompObjType.Properties.ReadOnly = true;
    this.beCompObjType.ButtonClick += new ButtonPressedEventHandler(this.beCompObjType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.cbCreateComplect, "cbCreateComplect");
    this.cbCreateComplect.Name = "cbCreateComplect";
    this.cbCreateComplect.UseVisualStyleBackColor = true;
    this.cbCreateComplect.CheckedChanged += new EventHandler(this.cbCreateComplect_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label13, "label13");
    this.label13.Name = "label13";
    componentResourceManager.ApplyResources((object) this.richComplectCond, "richComplectCond");
    this.richComplectCond.BackColor = SystemColors.Window;
    this.richComplectCond.ContextMenuStrip = this.opPopMenu;
    this.richComplectCond.Name = "richComplectCond";
    this.richComplectCond.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.beComplectType, "beComplectType");
    this.beComplectType.Name = "beComplectType";
    this.beComplectType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beComplectType.Properties.ReadOnly = true;
    this.beComplectType.ButtonClick += new ButtonPressedEventHandler(this.edObjType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label15, "label15");
    this.label15.Name = "label15";
    this.panOpParms7.Controls.Add((Control) this.label18);
    this.panOpParms7.Controls.Add((Control) this.richTextBox3);
    this.panOpParms7.Controls.Add((Control) this.edObjType);
    this.panOpParms7.Controls.Add((Control) this.label8);
    componentResourceManager.ApplyResources((object) this.panOpParms7, "panOpParms7");
    this.panOpParms7.Name = "panOpParms7";
    componentResourceManager.ApplyResources((object) this.label18, "label18");
    this.label18.Name = "label18";
    componentResourceManager.ApplyResources((object) this.richTextBox3, "richTextBox3");
    this.richTextBox3.BackColor = SystemColors.Window;
    this.richTextBox3.ContextMenuStrip = this.opPopMenu;
    this.richTextBox3.Name = "richTextBox3";
    this.richTextBox3.ReadOnly = true;
    this.richTextBox3.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richTextBox3.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextBox3.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.edObjType, "edObjType");
    this.edObjType.Name = "edObjType";
    this.edObjType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edObjType.Properties.ReadOnly = true;
    this.edObjType.ButtonClick += new ButtonPressedEventHandler(this.edObjType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    this.panOpParms6.Controls.Add((Control) this.cbByDefault);
    this.panOpParms6.Controls.Add((Control) this.groupBox4);
    this.panOpParms6.Controls.Add((Control) this.cbSelectDoc);
    this.panOpParms6.Controls.Add((Control) this.gbIdent);
    componentResourceManager.ApplyResources((object) this.panOpParms6, "panOpParms6");
    this.panOpParms6.Name = "panOpParms6";
    componentResourceManager.ApplyResources((object) this.cbByDefault, "cbByDefault");
    this.cbByDefault.Name = "cbByDefault";
    this.cbByDefault.UseVisualStyleBackColor = true;
    this.cbByDefault.CheckedChanged += new EventHandler(this.cbByDefault_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.groupBox4, "groupBox4");
    this.groupBox4.Controls.Add((Control) this.radioButton2);
    this.groupBox4.Controls.Add((Control) this.radioButton1);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.TabStop = false;
    componentResourceManager.ApplyResources((object) this.radioButton2, "radioButton2");
    this.radioButton2.Name = "radioButton2";
    this.radioButton2.UseVisualStyleBackColor = true;
    this.radioButton2.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButton1, "radioButton1");
    this.radioButton1.Checked = true;
    this.radioButton1.Name = "radioButton1";
    this.radioButton1.TabStop = true;
    this.radioButton1.UseVisualStyleBackColor = true;
    this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbSelectDoc, "cbSelectDoc");
    this.cbSelectDoc.Name = "cbSelectDoc";
    this.cbSelectDoc.UseVisualStyleBackColor = true;
    this.cbSelectDoc.CheckedChanged += new EventHandler(this.cbSelectDoc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.gbIdent, "gbIdent");
    this.gbIdent.Controls.Add((Control) this.label26);
    this.gbIdent.Controls.Add((Control) this.buttonEdit3);
    this.gbIdent.Controls.Add((Control) this.label25);
    this.gbIdent.Controls.Add((Control) this.checkByFormula);
    this.gbIdent.Controls.Add((Control) this.checkSelFromTempl);
    this.gbIdent.Controls.Add((Control) this.edSelTemplate);
    this.gbIdent.Controls.Add((Control) this.richTextID);
    this.gbIdent.Name = "gbIdent";
    this.gbIdent.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label26, "label26");
    this.label26.Name = "label26";
    componentResourceManager.ApplyResources((object) this.buttonEdit3, "buttonEdit3");
    this.buttonEdit3.Name = "buttonEdit3";
    this.buttonEdit3.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit3.Properties.ReadOnly = true;
    this.buttonEdit3.ButtonPressed += new ButtonPressedEventHandler(this.edSelTemplate_ButtonPressed);
    componentResourceManager.ApplyResources((object) this.label25, "label25");
    this.label25.Name = "label25";
    componentResourceManager.ApplyResources((object) this.checkByFormula, "checkByFormula");
    this.checkByFormula.Name = "checkByFormula";
    this.checkByFormula.CheckedChanged += new EventHandler(this.checkSelFromTempl_CheckedChanged);
    this.checkSelFromTempl.Checked = true;
    componentResourceManager.ApplyResources((object) this.checkSelFromTempl, "checkSelFromTempl");
    this.checkSelFromTempl.Name = "checkSelFromTempl";
    this.checkSelFromTempl.TabStop = true;
    this.checkSelFromTempl.CheckedChanged += new EventHandler(this.checkSelFromTempl_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.edSelTemplate, "edSelTemplate");
    this.edSelTemplate.Name = "edSelTemplate";
    this.edSelTemplate.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edSelTemplate.Properties.ReadOnly = true;
    this.edSelTemplate.ButtonPressed += new ButtonPressedEventHandler(this.edSelTemplate_ButtonPressed);
    componentResourceManager.ApplyResources((object) this.richTextID, "richTextID");
    this.richTextID.BackColor = SystemColors.Window;
    this.richTextID.ContextMenuStrip = this.opPopMenu;
    this.richTextID.Name = "richTextID";
    this.richTextID.ReadOnly = true;
    this.richTextID.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richTextID.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextID.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    this.panOpParms5.Controls.Add((Control) this.textBox2);
    this.panOpParms5.Controls.Add((Control) this.label55);
    this.panOpParms5.Controls.Add((Control) this.cbCurrentForever);
    this.panOpParms5.Controls.Add((Control) this.button19);
    this.panOpParms5.Controls.Add((Control) this.cbAvoidDup);
    this.panOpParms5.Controls.Add((Control) this.cbUseByDef);
    this.panOpParms5.Controls.Add((Control) this.groupBox3);
    this.panOpParms5.Controls.Add((Control) this.checkFillDefault);
    this.panOpParms5.Controls.Add((Control) this.checkMakeCurrent);
    this.panOpParms5.Controls.Add((Control) this.label7);
    this.panOpParms5.Controls.Add((Control) this.edSaveNewID);
    componentResourceManager.ApplyResources((object) this.panOpParms5, "panOpParms5");
    this.panOpParms5.Name = "panOpParms5";
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.Name = "textBox2";
    this.textBox2.Leave += new EventHandler(this.textBox2_Leave);
    componentResourceManager.ApplyResources((object) this.label55, "label55");
    this.label55.Name = "label55";
    componentResourceManager.ApplyResources((object) this.cbCurrentForever, "cbCurrentForever");
    this.cbCurrentForever.Name = "cbCurrentForever";
    this.cbCurrentForever.UseVisualStyleBackColor = true;
    this.cbCurrentForever.CheckedChanged += new EventHandler(this.cbCurrentForever_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.button19, "button19");
    this.button19.ImageList = this.IL_50;
    this.button19.Name = "button19";
    this.toolTip2.SetToolTip((Control) this.button19, componentResourceManager.GetString("button19.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button19, "Не использовать атрибут");
    this.button19.UseVisualStyleBackColor = true;
    this.button19.Click += new EventHandler(this.button19_Click);
    componentResourceManager.ApplyResources((object) this.cbAvoidDup, "cbAvoidDup");
    this.cbAvoidDup.Name = "cbAvoidDup";
    this.cbAvoidDup.CheckedChanged += new EventHandler(this.cbAvoidDup_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbUseByDef, "cbUseByDef");
    this.cbUseByDef.Name = "cbUseByDef";
    this.cbUseByDef.UseVisualStyleBackColor = true;
    this.cbUseByDef.CheckedChanged += new EventHandler(this.cbUseByDef_CheckedChanged);
    this.groupBox3.Controls.Add((Control) this.buttonEdit2);
    this.groupBox3.Controls.Add((Control) this.label24);
    this.groupBox3.Controls.Add((Control) this.checkEditAdd);
    this.groupBox3.Controls.Add((Control) this.textBox1);
    this.groupBox3.Controls.Add((Control) this.editAddAttr);
    this.groupBox3.Controls.Add((Control) this.label4);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.buttonEdit2, "buttonEdit2");
    this.buttonEdit2.Name = "buttonEdit2";
    this.buttonEdit2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit2.Properties.ButtonClick += new ButtonPressedEventHandler(this.textBox1_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label24, "label24");
    this.label24.Name = "label24";
    componentResourceManager.ApplyResources((object) this.checkEditAdd, "checkEditAdd");
    this.checkEditAdd.Name = "checkEditAdd";
    this.checkEditAdd.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    this.textBox1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textBox1.Properties.ButtonClick += new ButtonPressedEventHandler(this.textBox1_ButtonClick);
    componentResourceManager.ApplyResources((object) this.editAddAttr, "editAddAttr");
    this.editAddAttr.Name = "editAddAttr";
    this.editAddAttr.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.editAddAttr.Properties.ReadOnly = true;
    this.editAddAttr.Properties.ButtonClick += new ButtonPressedEventHandler(this.editAddAttr_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.checkFillDefault, "checkFillDefault");
    this.checkFillDefault.Name = "checkFillDefault";
    this.checkFillDefault.CheckedChanged += new EventHandler(this.checkFillDefault_CheckedChanged);
    this.checkMakeCurrent.Checked = true;
    this.checkMakeCurrent.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this.checkMakeCurrent, "checkMakeCurrent");
    this.checkMakeCurrent.Name = "checkMakeCurrent";
    this.checkMakeCurrent.CheckedChanged += new EventHandler(this.checkMakeCurrent_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.edSaveNewID, "edSaveNewID");
    this.edSaveNewID.Name = "edSaveNewID";
    this.edSaveNewID.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edSaveNewID.Properties.ReadOnly = true;
    this.edSaveNewID.Properties.ButtonClick += new ButtonPressedEventHandler(this.edSaveNewID_Properties_ButtonClick);
    this.panOpParms4.Controls.Add((Control) this.tabControl2);
    this.panOpParms4.Controls.Add((Control) this.groupBox2);
    componentResourceManager.ApplyResources((object) this.panOpParms4, "panOpParms4");
    this.panOpParms4.Name = "panOpParms4";
    this.tabControl2.Controls.Add((Control) this.tabPage1);
    this.tabControl2.Controls.Add((Control) this.tabPage2);
    componentResourceManager.ApplyResources((object) this.tabControl2, "tabControl2");
    this.tabControl2.Name = "tabControl2";
    this.tabControl2.SelectedIndex = 0;
    this.tabControl2.SelectedIndexChanged += new EventHandler(this.tabControl2_SelectedIndexChanged);
    this.tabPage1.Controls.Add((Control) this.gbSetValue);
    this.tabPage1.Controls.Add((Control) this.gbAttr);
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.gbSetValue, "gbSetValue");
    this.gbSetValue.Controls.Add((Control) this.richSetValue);
    this.gbSetValue.Controls.Add((Control) this.checkForm);
    this.gbSetValue.Name = "gbSetValue";
    this.gbSetValue.TabStop = false;
    componentResourceManager.ApplyResources((object) this.richSetValue, "richSetValue");
    this.richSetValue.ContextMenuStrip = this.opPopMenu;
    this.richSetValue.Name = "richSetValue";
    this.richSetValue.MouseDoubleClick += new MouseEventHandler(this.richSetValue_MouseDoubleClick);
    componentResourceManager.ApplyResources((object) this.checkForm, "checkForm");
    this.checkForm.Name = "checkForm";
    this.checkForm.CheckedChanged += new EventHandler(this.checkForm_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.gbAttr, "gbAttr");
    this.gbAttr.Controls.Add((Control) this.setAttr);
    this.gbAttr.Controls.Add((Control) this.checkAttr);
    this.gbAttr.Name = "gbAttr";
    this.gbAttr.TabStop = false;
    componentResourceManager.ApplyResources((object) this.setAttr, "setAttr");
    this.setAttr.attrText = "";
    this.setAttr.Name = "setAttr";
    this.setAttr.objTypeText = "";
    this.setAttr.ShowButtons = false;
    this.checkAttr.Checked = true;
    componentResourceManager.ApplyResources((object) this.checkAttr, "checkAttr");
    this.checkAttr.Name = "checkAttr";
    this.checkAttr.TabStop = true;
    this.checkAttr.CheckedChanged += new EventHandler(this.checkAttr_CheckedChanged);
    this.tabPage2.Controls.Add((Control) this.btnTextColor);
    this.tabPage2.Controls.Add((Control) this.gbFont);
    this.tabPage2.Controls.Add((Control) this.panel12);
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnTextColor, "btnTextColor");
    this.btnTextColor.Name = "btnTextColor";
    this.btnTextColor.UseVisualStyleBackColor = true;
    this.btnTextColor.Click += new EventHandler(this.btnTextColor_Click);
    this.gbFont.Controls.Add((Control) this.seFontSize);
    this.gbFont.Controls.Add((Control) this.cbUnderline);
    this.gbFont.Controls.Add((Control) this.cbItalic);
    this.gbFont.Controls.Add((Control) this.cbBold);
    this.gbFont.Controls.Add((Control) this.btnClearFont);
    this.gbFont.Controls.Add((Control) this.beFontName);
    componentResourceManager.ApplyResources((object) this.gbFont, "gbFont");
    this.gbFont.Name = "gbFont";
    this.gbFont.TabStop = false;
    componentResourceManager.ApplyResources((object) this.seFontSize, "seFontSize");
    this.seFontSize.Name = "seFontSize";
    this.seFontSize.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.seFontSize.Properties.MaxValue = new Decimal(new int[4]
    {
      96 /*0x60*/,
      0,
      0,
      0
    });
    this.seFontSize.Properties.UseCtrlIncrement = false;
    this.seFontSize.Properties.EditValueChanged += new EventHandler(this.seFontSize_Properties_EditValueChanged);
    componentResourceManager.ApplyResources((object) this.cbUnderline, "cbUnderline");
    this.cbUnderline.Name = "cbUnderline";
    this.cbUnderline.UseVisualStyleBackColor = true;
    this.cbUnderline.CheckedChanged += new EventHandler(this.cbBold_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbItalic, "cbItalic");
    this.cbItalic.Name = "cbItalic";
    this.cbItalic.UseVisualStyleBackColor = true;
    this.cbItalic.CheckedChanged += new EventHandler(this.cbBold_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbBold, "cbBold");
    this.cbBold.Name = "cbBold";
    this.cbBold.UseVisualStyleBackColor = true;
    this.cbBold.CheckedChanged += new EventHandler(this.cbBold_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnClearFont, "btnClearFont");
    this.btnClearFont.ImageList = this.IL_50;
    this.btnClearFont.Name = "btnClearFont";
    this.btnClearFont.UseVisualStyleBackColor = true;
    this.btnClearFont.Click += new EventHandler(this.btnClearFont_Click);
    componentResourceManager.ApplyResources((object) this.beFontName, "beFontName");
    this.beFontName.Name = "beFontName";
    this.beFontName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beFontName.Properties.ReadOnly = true;
    this.beFontName.Properties.ButtonClick += new ButtonPressedEventHandler(this.textId_ButtonClick);
    this.beFontName.ButtonPressed += new ButtonPressedEventHandler(this.beFontName_ButtonPressed);
    this.panel12.Controls.Add((Control) this.richLeftIndent);
    this.panel12.Controls.Add((Control) this.label39);
    componentResourceManager.ApplyResources((object) this.panel12, "panel12");
    this.panel12.Name = "panel12";
    this.richLeftIndent.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richLeftIndent, "richLeftIndent");
    this.richLeftIndent.Name = "richLeftIndent";
    componentResourceManager.ApplyResources((object) this.label39, "label39");
    this.label39.Name = "label39";
    this.groupBox2.Controls.Add((Control) this.cbAuthFile);
    this.groupBox2.Controls.Add((Control) this.textBox3);
    this.groupBox2.Controls.Add((Control) this.label56);
    this.groupBox2.Controls.Add((Control) this.cbLinkThisDoc);
    this.groupBox2.Controls.Add((Control) this.cbActiveLink);
    this.groupBox2.Controls.Add((Control) this.buttonEdit1);
    this.groupBox2.Controls.Add((Control) this.checkAddId);
    this.groupBox2.Controls.Add((Control) this.textId);
    this.groupBox2.Controls.Add((Control) this.edAddAttr);
    this.groupBox2.Controls.Add((Control) this.label6);
    this.groupBox2.Controls.Add((Control) this.label23);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbAuthFile, "cbAuthFile");
    this.cbAuthFile.Name = "cbAuthFile";
    this.cbAuthFile.UseVisualStyleBackColor = true;
    this.cbAuthFile.CheckedChanged += new EventHandler(this.cbAuthFile_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.textBox3, "textBox3");
    this.textBox3.Name = "textBox3";
    this.textBox3.Leave += new EventHandler(this.textBox3_Leave);
    componentResourceManager.ApplyResources((object) this.label56, "label56");
    this.label56.Name = "label56";
    componentResourceManager.ApplyResources((object) this.cbLinkThisDoc, "cbLinkThisDoc");
    this.cbLinkThisDoc.Name = "cbLinkThisDoc";
    this.cbLinkThisDoc.UseVisualStyleBackColor = true;
    this.cbLinkThisDoc.CheckedChanged += new EventHandler(this.cbLinkThisDoc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbActiveLink, "cbActiveLink");
    this.cbActiveLink.Name = "cbActiveLink";
    this.cbActiveLink.UseVisualStyleBackColor = true;
    this.cbActiveLink.CheckedChanged += new EventHandler(this.cbActiveLink_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.buttonEdit1, "buttonEdit1");
    this.buttonEdit1.Name = "buttonEdit1";
    this.buttonEdit1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit1.Properties.ButtonClick += new ButtonPressedEventHandler(this.textId_ButtonClick);
    componentResourceManager.ApplyResources((object) this.checkAddId, "checkAddId");
    this.checkAddId.Name = "checkAddId";
    this.checkAddId.CheckedChanged += new EventHandler(this.checkAddId_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.textId, "textId");
    this.textId.Name = "textId";
    this.textId.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textId.Properties.ButtonClick += new ButtonPressedEventHandler(this.textId_ButtonClick);
    componentResourceManager.ApplyResources((object) this.edAddAttr, "edAddAttr");
    this.edAddAttr.Name = "edAddAttr";
    this.edAddAttr.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edAddAttr.Properties.ReadOnly = true;
    this.edAddAttr.Properties.ButtonClick += new ButtonPressedEventHandler(this.editAddAttr_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label23, "label23");
    this.label23.Name = "label23";
    this.panOpParms3.Controls.Add((Control) this.tabCon);
    this.panOpParms3.Controls.Add((Control) this.panSetArray);
    this.panOpParms3.Controls.Add((Control) this.groupBox5);
    this.panOpParms3.Controls.Add((Control) this.settAttr);
    componentResourceManager.ApplyResources((object) this.panOpParms3, "panOpParms3");
    this.panOpParms3.Name = "panOpParms3";
    this.tabCon.Controls.Add((Control) this.tabFormula);
    this.tabCon.Controls.Add((Control) this.tabTable);
    componentResourceManager.ApplyResources((object) this.tabCon, "tabCon");
    this.tabCon.Name = "tabCon";
    this.tabCon.SelectedIndex = 0;
    this.tabCon.Selecting += new TabControlCancelEventHandler(this.tabCon_Selecting);
    this.tabFormula.Controls.Add((Control) this.richFormula);
    this.tabFormula.Controls.Add((Control) this.gbSetParms);
    componentResourceManager.ApplyResources((object) this.tabFormula, "tabFormula");
    this.tabFormula.Name = "tabFormula";
    this.tabFormula.UseVisualStyleBackColor = true;
    this.richFormula.BackColor = SystemColors.Window;
    this.richFormula.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richFormula, "richFormula");
    this.richFormula.Name = "richFormula";
    this.richFormula.ReadOnly = true;
    this.richFormula.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richFormula.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richFormula.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    this.gbSetParms.Controls.Add((Control) this.label21);
    this.gbSetParms.Controls.Add((Control) this.label20);
    this.gbSetParms.Controls.Add((Control) this.comboSelector);
    this.gbSetParms.Controls.Add((Control) this.comboDivider);
    componentResourceManager.ApplyResources((object) this.gbSetParms, "gbSetParms");
    this.gbSetParms.Name = "gbSetParms";
    this.gbSetParms.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label21, "label21");
    this.label21.Name = "label21";
    componentResourceManager.ApplyResources((object) this.label20, "label20");
    this.label20.Name = "label20";
    this.comboSelector.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboSelector.Items.AddRange(new object[8]
    {
      (object) componentResourceManager.GetString("comboSelector.Items"),
      (object) componentResourceManager.GetString("comboSelector.Items1"),
      (object) componentResourceManager.GetString("comboSelector.Items2"),
      (object) componentResourceManager.GetString("comboSelector.Items3"),
      (object) componentResourceManager.GetString("comboSelector.Items4"),
      (object) componentResourceManager.GetString("comboSelector.Items5"),
      (object) componentResourceManager.GetString("comboSelector.Items6"),
      (object) componentResourceManager.GetString("comboSelector.Items7")
    });
    componentResourceManager.ApplyResources((object) this.comboSelector, "comboSelector");
    this.comboSelector.Name = "comboSelector";
    this.comboSelector.SelectedIndexChanged += new EventHandler(this.checkVal_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.comboDivider, "comboDivider");
    this.comboDivider.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboDivider.Items.AddRange(new object[5]
    {
      (object) componentResourceManager.GetString("comboDivider.Items"),
      (object) componentResourceManager.GetString("comboDivider.Items1"),
      (object) componentResourceManager.GetString("comboDivider.Items2"),
      (object) componentResourceManager.GetString("comboDivider.Items3"),
      (object) componentResourceManager.GetString("comboDivider.Items4")
    });
    this.comboDivider.Name = "comboDivider";
    this.comboDivider.SelectedIndexChanged += new EventHandler(this.comboDivider_SelectedIndexChanged);
    this.tabTable.Controls.Add((Control) this.gridTable);
    this.tabTable.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.tabTable, "tabTable");
    this.tabTable.Name = "tabTable";
    this.tabTable.UseVisualStyleBackColor = true;
    this.gridTable.BackColor = Color.White;
    this.gridTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.gridTable, "gridTable");
    this.gridTable.GridToolTipActive = true;
    this.gridTable.Name = "gridTable";
    this.gridTable.SpecialKeys = GridSpecialKeys.Default;
    this.gridTable.StyleGrid = (StyleGrid) null;
    this.panel3.Controls.Add((Control) this.btnAdd);
    this.panel3.Controls.Add((Control) this.btnTableDown);
    this.panel3.Controls.Add((Control) this.btnDelete);
    this.panel3.Controls.Add((Control) this.btnTableUp);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.btnTableDown, "btnTableDown");
    this.btnTableDown.ImageList = this.IL_50;
    this.btnTableDown.Name = "btnTableDown";
    this.btnTableDown.UseVisualStyleBackColor = true;
    this.btnTableDown.Click += new EventHandler(this.btnTableDown_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.btnTableUp, "btnTableUp");
    this.btnTableUp.ImageList = this.IL_50;
    this.btnTableUp.Name = "btnTableUp";
    this.btnTableUp.UseVisualStyleBackColor = true;
    this.btnTableUp.Click += new EventHandler(this.btnTableUp_Click);
    this.panSetArray.Controls.Add((Control) this.richX);
    this.panSetArray.Controls.Add((Control) this.richY);
    this.panSetArray.Controls.Add((Control) this.label54);
    this.panSetArray.Controls.Add((Control) this.label53);
    componentResourceManager.ApplyResources((object) this.panSetArray, "panSetArray");
    this.panSetArray.Name = "panSetArray";
    componentResourceManager.ApplyResources((object) this.richX, "richX");
    this.richX.BackColor = SystemColors.HighlightText;
    this.richX.Name = "richX";
    this.richX.ReadOnly = true;
    this.richX.DoubleClick += new EventHandler(this.richX_DoubleClick);
    componentResourceManager.ApplyResources((object) this.richY, "richY");
    this.richY.BackColor = SystemColors.HighlightText;
    this.richY.Name = "richY";
    this.richY.ReadOnly = true;
    this.richY.DoubleClick += new EventHandler(this.richX_DoubleClick);
    componentResourceManager.ApplyResources((object) this.label54, "label54");
    this.label54.Name = "label54";
    componentResourceManager.ApplyResources((object) this.label53, "label53");
    this.label53.Name = "label53";
    this.groupBox5.Controls.Add((Control) this.rbDocField);
    this.groupBox5.Controls.Add((Control) this.cbAddToGlobal);
    this.groupBox5.Controls.Add((Control) this.rbRelation);
    this.groupBox5.Controls.Add((Control) this.cbArray);
    this.groupBox5.Controls.Add((Control) this.rbObject);
    componentResourceManager.ApplyResources((object) this.groupBox5, "groupBox5");
    this.groupBox5.Name = "groupBox5";
    this.groupBox5.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbDocField, "rbDocField");
    this.rbDocField.Name = "rbDocField";
    this.rbDocField.UseVisualStyleBackColor = true;
    this.rbDocField.CheckedChanged += new EventHandler(this.rbObject_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbAddToGlobal, "cbAddToGlobal");
    this.cbAddToGlobal.Name = "cbAddToGlobal";
    this.cbAddToGlobal.UseVisualStyleBackColor = true;
    this.cbAddToGlobal.CheckedChanged += new EventHandler(this.cbAddToGlobal_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbRelation, "rbRelation");
    this.rbRelation.Name = "rbRelation";
    this.rbRelation.TabStop = true;
    this.rbRelation.UseVisualStyleBackColor = true;
    this.rbRelation.CheckedChanged += new EventHandler(this.rbObject_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbArray, "cbArray");
    this.cbArray.Name = "cbArray";
    this.cbArray.UseVisualStyleBackColor = true;
    this.cbArray.CheckedChanged += new EventHandler(this.cbArray_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbObject, "rbObject");
    this.rbObject.Checked = true;
    this.rbObject.Name = "rbObject";
    this.rbObject.TabStop = true;
    this.rbObject.UseVisualStyleBackColor = true;
    this.rbObject.CheckedChanged += new EventHandler(this.rbObject_CheckedChanged);
    this.settAttr.attrText = "";
    componentResourceManager.ApplyResources((object) this.settAttr, "settAttr");
    this.settAttr.Name = "settAttr";
    this.settAttr.objTypeText = "";
    this.settAttr.ShowButtons = false;
    this.settAttr.Changed += new EventHandler(this.setAttr_Changed);
    this.panOpParmsTI.Controls.Add((Control) this.tvCopiedAttrs);
    this.panOpParmsTI.Controls.Add((Control) this.btnDeleteAttr);
    this.panOpParmsTI.Controls.Add((Control) this.btnAddRelAttr);
    this.panOpParmsTI.Controls.Add((Control) this.btnAddObjAttr);
    this.panOpParmsTI.Controls.Add((Control) this.label50);
    this.panOpParmsTI.Controls.Add((Control) this.label43);
    this.panOpParmsTI.Controls.Add((Control) this.beSourceType);
    this.panOpParmsTI.Controls.Add((Control) this.label16);
    this.panOpParmsTI.Controls.Add((Control) this.beCreatingDocType);
    componentResourceManager.ApplyResources((object) this.panOpParmsTI, "panOpParmsTI");
    this.panOpParmsTI.Name = "panOpParmsTI";
    componentResourceManager.ApplyResources((object) this.tvCopiedAttrs, "tvCopiedAttrs");
    this.tvCopiedAttrs.ImageList = this.IL_50;
    this.tvCopiedAttrs.Name = "tvCopiedAttrs";
    this.tvCopiedAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvCopiedAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvCopiedAttrs.Nodes1")
    });
    this.tvCopiedAttrs.ShowRootLines = false;
    componentResourceManager.ApplyResources((object) this.btnDeleteAttr, "btnDeleteAttr");
    this.btnDeleteAttr.ImageList = this.IL_50;
    this.btnDeleteAttr.Name = "btnDeleteAttr";
    this.toolTip2.SetToolTip((Control) this.btnDeleteAttr, componentResourceManager.GetString("btnDeleteAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDeleteAttr, "Не использовать атрибут");
    this.btnDeleteAttr.UseVisualStyleBackColor = true;
    this.btnDeleteAttr.Click += new EventHandler(this.btnDeleteAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddRelAttr, "btnAddRelAttr");
    this.btnAddRelAttr.ImageList = this.IL_50;
    this.btnAddRelAttr.Name = "btnAddRelAttr";
    this.toolTip2.SetToolTip((Control) this.btnAddRelAttr, componentResourceManager.GetString("btnAddRelAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddRelAttr, "Добавить атрибут связи");
    this.btnAddRelAttr.UseVisualStyleBackColor = true;
    this.btnAddRelAttr.Click += new EventHandler(this.btnAddObjAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddObjAttr, "btnAddObjAttr");
    this.btnAddObjAttr.ImageList = this.IL_50;
    this.btnAddObjAttr.Name = "btnAddObjAttr";
    this.toolTip2.SetToolTip((Control) this.btnAddObjAttr, componentResourceManager.GetString("btnAddObjAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddObjAttr, "Добавить атрибут объекта");
    this.btnAddObjAttr.UseVisualStyleBackColor = true;
    this.btnAddObjAttr.Click += new EventHandler(this.btnAddObjAttr_Click);
    componentResourceManager.ApplyResources((object) this.label50, "label50");
    this.label50.Name = "label50";
    componentResourceManager.ApplyResources((object) this.label43, "label43");
    this.label43.Name = "label43";
    componentResourceManager.ApplyResources((object) this.beSourceType, "beSourceType");
    this.beSourceType.Name = "beSourceType";
    this.beSourceType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beSourceType.Properties.ButtonClick += new ButtonPressedEventHandler(this.beDocType_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label16, "label16");
    this.label16.Name = "label16";
    componentResourceManager.ApplyResources((object) this.beCreatingDocType, "beCreatingDocType");
    this.beCreatingDocType.Name = "beCreatingDocType";
    this.beCreatingDocType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beCreatingDocType.Properties.ButtonClick += new ButtonPressedEventHandler(this.beDocType_Properties_ButtonClick);
    this.panOpParmsStyleB.Controls.Add((Control) this.textBox5);
    componentResourceManager.ApplyResources((object) this.panOpParmsStyleB, "panOpParmsStyleB");
    this.panOpParmsStyleB.Name = "panOpParmsStyleB";
    componentResourceManager.ApplyResources((object) this.textBox5, "textBox5");
    this.textBox5.Name = "textBox5";
    this.textBox5.ReadOnly = true;
    this.panOpParmsStyleC.Controls.Add((Control) this.textBox6);
    componentResourceManager.ApplyResources((object) this.panOpParmsStyleC, "panOpParmsStyleC");
    this.panOpParmsStyleC.Name = "panOpParmsStyleC";
    componentResourceManager.ApplyResources((object) this.textBox6, "textBox6");
    this.textBox6.Name = "textBox6";
    this.textBox6.ReadOnly = true;
    this.panOpParms2.Controls.Add((Control) this.btnDelRef);
    this.panOpParms2.Controls.Add((Control) this.label12);
    this.panOpParms2.Controls.Add((Control) this.btnObjLink);
    this.panOpParms2.Controls.Add((Control) this.richTextCond);
    this.panOpParms2.Controls.Add((Control) this.label3);
    componentResourceManager.ApplyResources((object) this.panOpParms2, "panOpParms2");
    this.panOpParms2.Name = "panOpParms2";
    componentResourceManager.ApplyResources((object) this.btnDelRef, "btnDelRef");
    this.btnDelRef.ImageList = this.IL_50;
    this.btnDelRef.Name = "btnDelRef";
    this.toolTip2.SetToolTip((Control) this.btnDelRef, componentResourceManager.GetString("btnDelRef.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDelRef, "Не использовать ссылку");
    this.btnDelRef.Click += new EventHandler(this.btnDelRef_Click);
    componentResourceManager.ApplyResources((object) this.label12, "label12");
    this.label12.Name = "label12";
    componentResourceManager.ApplyResources((object) this.btnObjLink, "btnObjLink");
    this.btnObjLink.Name = "btnObjLink";
    this.btnObjLink.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.btnObjLink.ButtonClick += new ButtonPressedEventHandler(this.btnObjLink_ButtonClick);
    componentResourceManager.ApplyResources((object) this.richTextCond, "richTextCond");
    this.richTextCond.BackColor = SystemColors.Window;
    this.richTextCond.ContextMenuStrip = this.opPopMenu;
    this.richTextCond.Name = "richTextCond";
    this.richTextCond.ReadOnly = true;
    this.richTextCond.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richTextCond.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richTextCond.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.panOpParms9.Controls.Add((Control) this.groupBox1);
    componentResourceManager.ApplyResources((object) this.panOpParms9, "panOpParms9");
    this.panOpParms9.Name = "panOpParms9";
    this.groupBox1.Controls.Add((Control) this.tvRecalcAttrs);
    this.groupBox1.Controls.Add((Control) this.btnAddRecalcAttr);
    this.groupBox1.Controls.Add((Control) this.btnAddRecalcLink);
    this.groupBox1.Controls.Add((Control) this.btnDelRecalcAttr);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tvRecalcAttrs, "tvRecalcAttrs");
    this.tvRecalcAttrs.ImageList = this.IL_50;
    this.tvRecalcAttrs.Name = "tvRecalcAttrs";
    this.tvRecalcAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvRecalcAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvRecalcAttrs.Nodes1")
    });
    this.tvRecalcAttrs.ShowLines = false;
    this.tvRecalcAttrs.ShowPlusMinus = false;
    componentResourceManager.ApplyResources((object) this.btnAddRecalcAttr, "btnAddRecalcAttr");
    this.btnAddRecalcAttr.ImageList = this.IL_50;
    this.btnAddRecalcAttr.Name = "btnAddRecalcAttr";
    this.toolTip2.SetToolTip((Control) this.btnAddRecalcAttr, componentResourceManager.GetString("btnAddRecalcAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddRecalcAttr, "Добавить атрибут объекта");
    this.btnAddRecalcAttr.UseVisualStyleBackColor = true;
    this.btnAddRecalcAttr.Click += new EventHandler(this.btnAddRecalcAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddRecalcLink, "btnAddRecalcLink");
    this.btnAddRecalcLink.ImageList = this.IL_50;
    this.btnAddRecalcLink.Name = "btnAddRecalcLink";
    this.toolTip2.SetToolTip((Control) this.btnAddRecalcLink, componentResourceManager.GetString("btnAddRecalcLink.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddRecalcLink, "Добавить атрибут связи");
    this.btnAddRecalcLink.UseVisualStyleBackColor = true;
    this.btnAddRecalcLink.Click += new EventHandler(this.btnAddRecalcAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnDelRecalcAttr, "btnDelRecalcAttr");
    this.btnDelRecalcAttr.ImageList = this.IL_50;
    this.btnDelRecalcAttr.Name = "btnDelRecalcAttr";
    this.toolTip2.SetToolTip((Control) this.btnDelRecalcAttr, componentResourceManager.GetString("btnDelRecalcAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDelRecalcAttr, "Удалить атрибут");
    this.btnDelRecalcAttr.UseVisualStyleBackColor = true;
    this.btnDelRecalcAttr.Click += new EventHandler(this.btnDelRecalcAttr_Click);
    this.panOpParms1.Controls.Add((Control) this.tabControl1);
    componentResourceManager.ApplyResources((object) this.panOpParms1, "panOpParms1");
    this.panOpParms1.Name = "panOpParms1";
    this.tabControl1.Controls.Add((Control) this.tabObjMain);
    this.tabControl1.Controls.Add((Control) this.tabObjSecond);
    this.tabControl1.Controls.Add((Control) this.tabObjTable);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
    this.tabObjMain.Controls.Add((Control) this.cbNoSearch);
    this.tabObjMain.Controls.Add((Control) this.btnEdExcerpt);
    this.tabObjMain.Controls.Add((Control) this.label1);
    this.tabObjMain.Controls.Add((Control) this.button3);
    this.tabObjMain.Controls.Add((Control) this.button1);
    this.tabObjMain.Controls.Add((Control) this.label22);
    this.tabObjMain.Controls.Add((Control) this.btnDelDAttr);
    this.tabObjMain.Controls.Add((Control) this.richCond);
    this.tabObjMain.Controls.Add((Control) this.label2);
    this.tabObjMain.Controls.Add((Control) this.btnAddObjType);
    this.tabObjMain.Controls.Add((Control) this.btnToggleSort);
    this.tabObjMain.Controls.Add((Control) this.btnAddLinkType);
    this.tabObjMain.Controls.Add((Control) this.label29);
    this.tabObjMain.Controls.Add((Control) this.tvTypes);
    this.tabObjMain.Controls.Add((Control) this.btnAddOpLink);
    this.tabObjMain.Controls.Add((Control) this.button2);
    this.tabObjMain.Controls.Add((Control) this.btnAddDAttr);
    this.tabObjMain.Controls.Add((Control) this.tvObjAttrs);
    componentResourceManager.ApplyResources((object) this.tabObjMain, "tabObjMain");
    this.tabObjMain.Name = "tabObjMain";
    this.tabObjMain.UseVisualStyleBackColor = true;
    this.tabObjMain.Resize += new EventHandler(this.tabObjMain_Resize);
    componentResourceManager.ApplyResources((object) this.cbNoSearch, "cbNoSearch");
    this.cbNoSearch.ForeColor = Color.Black;
    this.cbNoSearch.Name = "cbNoSearch";
    this.cbNoSearch.UseVisualStyleBackColor = true;
    this.cbNoSearch.CheckedChanged += new EventHandler(this.cbNoSearch_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnEdExcerpt, "btnEdExcerpt");
    this.btnEdExcerpt.Name = "btnEdExcerpt";
    this.btnEdExcerpt.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.btnEdExcerpt.Properties.ReadOnly = true;
    this.btnEdExcerpt.Properties.ButtonClick += new ButtonPressedEventHandler(this.btnEdExcerpt_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.ImageList = this.IL_50;
    this.button3.Name = "button3";
    this.toolTip2.SetToolTip((Control) this.button3, componentResourceManager.GetString("button3.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button3, "Не использовать выборку");
    this.button3.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.ImageList = this.IL_50;
    this.button1.Name = "button1";
    this.toolTip2.SetToolTip((Control) this.button1, componentResourceManager.GetString("button1.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button1, "Создать новую выборку");
    this.button1.Click += new EventHandler(this.button1_Click_1);
    componentResourceManager.ApplyResources((object) this.label22, "label22");
    this.label22.Name = "label22";
    componentResourceManager.ApplyResources((object) this.btnDelDAttr, "btnDelDAttr");
    this.btnDelDAttr.ImageList = this.IL_50;
    this.btnDelDAttr.Name = "btnDelDAttr";
    this.toolTip2.SetToolTip((Control) this.btnDelDAttr, componentResourceManager.GetString("btnDelDAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDelDAttr, "Не использовать атрибут");
    this.btnDelDAttr.UseVisualStyleBackColor = true;
    this.btnDelDAttr.Click += new EventHandler(this.btnDelDAttr_Click);
    componentResourceManager.ApplyResources((object) this.richCond, "richCond");
    this.richCond.BackColor = SystemColors.Window;
    this.richCond.ContextMenuStrip = this.opPopMenu;
    this.richCond.Name = "richCond";
    this.richCond.ReadOnly = true;
    this.richCond.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richCond.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richCond.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.btnAddObjType, "btnAddObjType");
    this.btnAddObjType.ImageList = this.IL_50;
    this.btnAddObjType.Name = "btnAddObjType";
    this.toolTip2.SetToolTip((Control) this.btnAddObjType, componentResourceManager.GetString("btnAddObjType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddObjType, "Настроить типы объектов");
    this.btnAddObjType.UseVisualStyleBackColor = false;
    this.btnAddObjType.Click += new EventHandler(this.button4_Click);
    componentResourceManager.ApplyResources((object) this.btnToggleSort, "btnToggleSort");
    this.btnToggleSort.ImageList = this.IL_50;
    this.btnToggleSort.Name = "btnToggleSort";
    this.toolTip2.SetToolTip((Control) this.btnToggleSort, componentResourceManager.GetString("btnToggleSort.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnToggleSort, "Переключить режим сортировки по атрибуту");
    this.btnToggleSort.UseVisualStyleBackColor = true;
    this.btnToggleSort.Click += new EventHandler(this.btnToggleSort_Click);
    componentResourceManager.ApplyResources((object) this.btnAddLinkType, "btnAddLinkType");
    this.btnAddLinkType.ImageList = this.IL_50;
    this.btnAddLinkType.Name = "btnAddLinkType";
    this.toolTip2.SetToolTip((Control) this.btnAddLinkType, componentResourceManager.GetString("btnAddLinkType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddLinkType, "Настроить типы связей");
    this.btnAddLinkType.UseVisualStyleBackColor = false;
    this.btnAddLinkType.Click += new EventHandler(this.btnAddLinkType_Click);
    componentResourceManager.ApplyResources((object) this.label29, "label29");
    this.label29.Name = "label29";
    componentResourceManager.ApplyResources((object) this.tvTypes, "tvTypes");
    this.tvTypes.ImageList = this.IL_50;
    this.tvTypes.Name = "tvTypes";
    this.tvTypes.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvTypes.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvTypes.Nodes1")
    });
    this.tvTypes.ShowRootLines = false;
    componentResourceManager.ApplyResources((object) this.btnAddOpLink, "btnAddOpLink");
    this.btnAddOpLink.ImageList = this.IL_50;
    this.btnAddOpLink.Name = "btnAddOpLink";
    this.toolTip2.SetToolTip((Control) this.btnAddOpLink, componentResourceManager.GetString("btnAddOpLink.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddOpLink, "Добавить атрибут связи");
    this.btnAddOpLink.UseVisualStyleBackColor = true;
    this.btnAddOpLink.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.ImageList = this.IL_50;
    this.button2.Name = "button2";
    this.toolTip2.SetToolTip((Control) this.button2, componentResourceManager.GetString("button2.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button2, "Удалить тип объекта или связи");
    this.button2.UseVisualStyleBackColor = false;
    this.button2.Click += new EventHandler(this.button2_Click_1);
    componentResourceManager.ApplyResources((object) this.btnAddDAttr, "btnAddDAttr");
    this.btnAddDAttr.ImageList = this.IL_50;
    this.btnAddDAttr.Name = "btnAddDAttr";
    this.toolTip2.SetToolTip((Control) this.btnAddDAttr, componentResourceManager.GetString("btnAddDAttr.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddDAttr, "Добавить атрибут объекта");
    this.btnAddDAttr.UseVisualStyleBackColor = true;
    this.btnAddDAttr.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.tvObjAttrs, "tvObjAttrs");
    this.tvObjAttrs.ContextMenuStrip = this.elMoveMenu;
    this.tvObjAttrs.ImageList = this.IL_50;
    this.tvObjAttrs.Name = "tvObjAttrs";
    this.tvObjAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvObjAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvObjAttrs.Nodes1")
    });
    this.tvObjAttrs.ShowRootLines = false;
    this.tvObjAttrs.AfterSelect += new TreeViewEventHandler(this.tvObjAttrs_AfterSelect);
    this.elMoveMenu.ImageScalingSize = new Size(24, 24);
    this.elMoveMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.upToolStripMenuItem,
      (ToolStripItem) this.downToolStripMenuItem,
      (ToolStripItem) this.firstToolStripMenuItem,
      (ToolStripItem) this.lastToolStripMenuItem
    });
    this.elMoveMenu.Name = "elMoveMenu";
    componentResourceManager.ApplyResources((object) this.elMoveMenu, "elMoveMenu");
    this.elMoveMenu.Opening += new CancelEventHandler(this.elMoveMenu_Opening);
    this.upToolStripMenuItem.Name = "upToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.upToolStripMenuItem, "upToolStripMenuItem");
    this.upToolStripMenuItem.Click += new EventHandler(this.upToolStripMenuItem_Click);
    this.downToolStripMenuItem.Name = "downToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.downToolStripMenuItem, "downToolStripMenuItem");
    this.downToolStripMenuItem.Click += new EventHandler(this.upToolStripMenuItem_Click);
    this.firstToolStripMenuItem.Name = "firstToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.firstToolStripMenuItem, "firstToolStripMenuItem");
    this.firstToolStripMenuItem.Click += new EventHandler(this.upToolStripMenuItem_Click);
    this.lastToolStripMenuItem.Name = "lastToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.lastToolStripMenuItem, "lastToolStripMenuItem");
    this.lastToolStripMenuItem.Click += new EventHandler(this.upToolStripMenuItem_Click);
    this.tabObjSecond.Controls.Add((Control) this.richGlobalFilter);
    this.tabObjSecond.Controls.Add((Control) this.label5);
    this.tabObjSecond.Controls.Add((Control) this.cbAddThis);
    this.tabObjSecond.Controls.Add((Control) this.label36);
    this.tabObjSecond.Controls.Add((Control) this.groupBox8);
    this.tabObjSecond.Controls.Add((Control) this.richAfterFilter);
    this.tabObjSecond.Controls.Add((Control) this.groupBox7);
    componentResourceManager.ApplyResources((object) this.tabObjSecond, "tabObjSecond");
    this.tabObjSecond.Name = "tabObjSecond";
    this.tabObjSecond.UseVisualStyleBackColor = true;
    this.richGlobalFilter.BackColor = SystemColors.Window;
    this.richGlobalFilter.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richGlobalFilter, "richGlobalFilter");
    this.richGlobalFilter.Name = "richGlobalFilter";
    this.richGlobalFilter.ReadOnly = true;
    this.richGlobalFilter.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richGlobalFilter.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richGlobalFilter.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.cbAddThis, "cbAddThis");
    this.cbAddThis.Name = "cbAddThis";
    this.cbAddThis.UseVisualStyleBackColor = true;
    this.cbAddThis.CheckedChanged += new EventHandler(this.cbAddThis_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label36, "label36");
    this.label36.Name = "label36";
    this.groupBox8.Controls.Add((Control) this.cbSaveRels);
    this.groupBox8.Controls.Add((Control) this.rbSaveAdd);
    this.groupBox8.Controls.Add((Control) this.rbSaveLocal);
    this.groupBox8.Controls.Add((Control) this.rbSaveClear);
    this.groupBox8.Controls.Add((Control) this.rbSaveNone);
    componentResourceManager.ApplyResources((object) this.groupBox8, "groupBox8");
    this.groupBox8.Name = "groupBox8";
    this.groupBox8.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbSaveRels, "cbSaveRels");
    this.cbSaveRels.Name = "cbSaveRels";
    this.cbSaveRels.UseVisualStyleBackColor = true;
    this.cbSaveRels.CheckedChanged += new EventHandler(this.cbSaveRels_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbSaveAdd, "rbSaveAdd");
    this.rbSaveAdd.Name = "rbSaveAdd";
    this.rbSaveAdd.TabStop = true;
    this.rbSaveAdd.UseVisualStyleBackColor = true;
    this.rbSaveAdd.CheckedChanged += new EventHandler(this.cbKeepData_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbSaveLocal, "rbSaveLocal");
    this.rbSaveLocal.Name = "rbSaveLocal";
    this.rbSaveLocal.UseVisualStyleBackColor = true;
    this.rbSaveLocal.CheckedChanged += new EventHandler(this.cbKeepData_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbSaveClear, "rbSaveClear");
    this.rbSaveClear.Name = "rbSaveClear";
    this.rbSaveClear.UseVisualStyleBackColor = true;
    this.rbSaveClear.CheckedChanged += new EventHandler(this.cbKeepData_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbSaveNone, "rbSaveNone");
    this.rbSaveNone.Checked = true;
    this.rbSaveNone.Name = "rbSaveNone";
    this.rbSaveNone.TabStop = true;
    this.rbSaveNone.UseVisualStyleBackColor = true;
    this.rbSaveNone.CheckedChanged += new EventHandler(this.cbKeepData_CheckedChanged);
    this.richAfterFilter.BackColor = SystemColors.Window;
    this.richAfterFilter.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.richAfterFilter, "richAfterFilter");
    this.richAfterFilter.Name = "richAfterFilter";
    this.richAfterFilter.ReadOnly = true;
    this.richAfterFilter.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.richAfterFilter.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.richAfterFilter.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    this.groupBox7.Controls.Add((Control) this.rbGlobalMul);
    this.groupBox7.Controls.Add((Control) this.rbGlobalPlus);
    this.groupBox7.Controls.Add((Control) this.rbGlobalNone);
    componentResourceManager.ApplyResources((object) this.groupBox7, "groupBox7");
    this.groupBox7.Name = "groupBox7";
    this.groupBox7.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbGlobalMul, "rbGlobalMul");
    this.rbGlobalMul.Name = "rbGlobalMul";
    this.rbGlobalMul.UseVisualStyleBackColor = true;
    this.rbGlobalMul.CheckedChanged += new EventHandler(this.cbOnlyCurrent_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbGlobalPlus, "rbGlobalPlus");
    this.rbGlobalPlus.Name = "rbGlobalPlus";
    this.rbGlobalPlus.UseVisualStyleBackColor = true;
    this.rbGlobalPlus.CheckedChanged += new EventHandler(this.cbOnlyCurrent_CheckedChanged);
    this.rbGlobalNone.Checked = true;
    componentResourceManager.ApplyResources((object) this.rbGlobalNone, "rbGlobalNone");
    this.rbGlobalNone.Name = "rbGlobalNone";
    this.rbGlobalNone.TabStop = true;
    this.rbGlobalNone.UseVisualStyleBackColor = true;
    this.rbGlobalNone.CheckedChanged += new EventHandler(this.cbOnlyCurrent_CheckedChanged);
    this.tabObjTable.Controls.Add((Control) this.groupBox6);
    this.tabObjTable.Controls.Add((Control) this.gbComposition);
    this.tabObjTable.Controls.Add((Control) this.gbIspoln);
    this.tabObjTable.Controls.Add((Control) this.checkDups);
    componentResourceManager.ApplyResources((object) this.tabObjTable, "tabObjTable");
    this.tabObjTable.Name = "tabObjTable";
    this.tabObjTable.UseVisualStyleBackColor = true;
    this.groupBox6.Controls.Add((Control) this.cbInbuiltSort);
    this.groupBox6.Controls.Add((Control) this.beCompareFunc);
    this.groupBox6.Controls.Add((Control) this.label30);
    componentResourceManager.ApplyResources((object) this.groupBox6, "groupBox6");
    this.groupBox6.Name = "groupBox6";
    this.groupBox6.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbInbuiltSort, "cbInbuiltSort");
    this.cbInbuiltSort.Name = "cbInbuiltSort";
    this.cbInbuiltSort.UseVisualStyleBackColor = true;
    this.cbInbuiltSort.CheckedChanged += new EventHandler(this.cbInbuiltSort_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.beCompareFunc, "beCompareFunc");
    this.beCompareFunc.Name = "beCompareFunc";
    this.beCompareFunc.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    componentResourceManager.ApplyResources((object) this.label30, "label30");
    this.label30.Name = "label30";
    this.gbComposition.Controls.Add((Control) this.rbContentsNotClosedRoots);
    this.gbComposition.Controls.Add((Control) this.rbContentsNotClosed);
    this.gbComposition.Controls.Add((Control) this.rbContentsAll);
    this.gbComposition.Controls.Add((Control) this.cbConfigOptions);
    componentResourceManager.ApplyResources((object) this.gbComposition, "gbComposition");
    this.gbComposition.Name = "gbComposition";
    this.gbComposition.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbContentsNotClosedRoots, "rbContentsNotClosedRoots");
    this.rbContentsNotClosedRoots.Name = "rbContentsNotClosedRoots";
    this.rbContentsNotClosedRoots.Tag = (object) "2";
    this.rbContentsNotClosedRoots.UseVisualStyleBackColor = true;
    this.rbContentsNotClosedRoots.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbContentsNotClosed, "rbContentsNotClosed");
    this.rbContentsNotClosed.Name = "rbContentsNotClosed";
    this.rbContentsNotClosed.Tag = (object) "1";
    this.rbContentsNotClosed.UseVisualStyleBackColor = true;
    this.rbContentsNotClosed.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbContentsAll, "rbContentsAll");
    this.rbContentsAll.Checked = true;
    this.rbContentsAll.Name = "rbContentsAll";
    this.rbContentsAll.TabStop = true;
    this.rbContentsAll.Tag = (object) "0";
    this.rbContentsAll.UseVisualStyleBackColor = true;
    this.rbContentsAll.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbConfigOptions, "cbConfigOptions");
    this.cbConfigOptions.Name = "cbConfigOptions";
    this.cbConfigOptions.UseVisualStyleBackColor = true;
    this.cbConfigOptions.CheckedChanged += new EventHandler(this.cbConfigOptions_CheckedChanged);
    this.gbIspoln.Controls.Add((Control) this.panel5);
    this.gbIspoln.Controls.Add((Control) this.rbAllIspolnInfo);
    this.gbIspoln.Controls.Add((Control) this.rbCurrentIsp);
    this.gbIspoln.Controls.Add((Control) this.rbOnlyCommon);
    this.gbIspoln.Controls.Add((Control) this.rbNoIspolns);
    componentResourceManager.ApplyResources((object) this.gbIspoln, "gbIspoln");
    this.gbIspoln.Name = "gbIspoln";
    this.gbIspoln.TabStop = false;
    this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.panel5.Controls.Add((Control) this.cbUseCurrentIsps);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.ForeColor = SystemColors.ControlText;
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.cbUseCurrentIsps, "cbUseCurrentIsps");
    this.cbUseCurrentIsps.Name = "cbUseCurrentIsps";
    this.cbUseCurrentIsps.UseVisualStyleBackColor = true;
    this.cbUseCurrentIsps.CheckedChanged += new EventHandler(this.cbUseCurrentIsps_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAllIspolnInfo, "rbAllIspolnInfo");
    this.rbAllIspolnInfo.Name = "rbAllIspolnInfo";
    this.rbAllIspolnInfo.Tag = (object) "3";
    this.rbAllIspolnInfo.UseVisualStyleBackColor = true;
    this.rbAllIspolnInfo.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbCurrentIsp, "rbCurrentIsp");
    this.rbCurrentIsp.Name = "rbCurrentIsp";
    this.rbCurrentIsp.Tag = (object) "2";
    this.rbCurrentIsp.UseVisualStyleBackColor = true;
    this.rbCurrentIsp.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbOnlyCommon, "rbOnlyCommon");
    this.rbOnlyCommon.Name = "rbOnlyCommon";
    this.rbOnlyCommon.Tag = (object) "1";
    this.rbOnlyCommon.UseVisualStyleBackColor = true;
    this.rbOnlyCommon.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbNoIspolns, "rbNoIspolns");
    this.rbNoIspolns.Checked = true;
    this.rbNoIspolns.Name = "rbNoIspolns";
    this.rbNoIspolns.TabStop = true;
    this.rbNoIspolns.Tag = (object) "0";
    this.rbNoIspolns.UseVisualStyleBackColor = true;
    this.rbNoIspolns.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.checkDups, "checkDups");
    this.checkDups.Name = "checkDups";
    this.checkDups.CheckedChanged += new EventHandler(this.checkDups_CheckedChanged);
    this.panOpParmsA.Controls.Add((Control) this.cbScenario);
    this.panOpParmsA.Controls.Add((Control) this.parm2Box);
    this.panOpParmsA.Controls.Add((Control) this.label41);
    this.panOpParmsA.Controls.Add((Control) this.parm1Box);
    this.panOpParmsA.Controls.Add((Control) this.label40);
    this.panOpParmsA.Controls.Add((Control) this.cbScript);
    this.panOpParmsA.Controls.Add((Control) this.cbProc);
    this.panOpParmsA.Controls.Add((Control) this.cbUserProc);
    this.panOpParmsA.Controls.Add((Control) this.beUserProc);
    componentResourceManager.ApplyResources((object) this.panOpParmsA, "panOpParmsA");
    this.panOpParmsA.Name = "panOpParmsA";
    componentResourceManager.ApplyResources((object) this.cbScenario, "cbScenario");
    this.cbScenario.Name = "cbScenario";
    this.cbScenario.UseVisualStyleBackColor = true;
    this.cbScenario.CheckedChanged += new EventHandler(this.cbProc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.parm2Box, "parm2Box");
    this.parm2Box.BackColor = SystemColors.Window;
    this.parm2Box.ContextMenuStrip = this.opPopMenu;
    this.parm2Box.Name = "parm2Box";
    this.parm2Box.ReadOnly = true;
    this.parm2Box.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.parm2Box.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label41, "label41");
    this.label41.Name = "label41";
    componentResourceManager.ApplyResources((object) this.parm1Box, "parm1Box");
    this.parm1Box.BackColor = SystemColors.Window;
    this.parm1Box.ContextMenuStrip = this.opPopMenu;
    this.parm1Box.Name = "parm1Box";
    this.parm1Box.ReadOnly = true;
    this.parm1Box.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.parm1Box.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label40, "label40");
    this.label40.Name = "label40";
    componentResourceManager.ApplyResources((object) this.cbScript, "cbScript");
    this.cbScript.Name = "cbScript";
    this.cbScript.UseVisualStyleBackColor = true;
    this.cbScript.CheckedChanged += new EventHandler(this.cbProc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbProc, "cbProc");
    this.cbProc.Name = "cbProc";
    this.cbProc.UseVisualStyleBackColor = true;
    this.cbProc.CheckedChanged += new EventHandler(this.cbProc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbUserProc, "cbUserProc");
    this.cbUserProc.Checked = true;
    this.cbUserProc.Name = "cbUserProc";
    this.cbUserProc.TabStop = true;
    this.cbUserProc.UseVisualStyleBackColor = true;
    this.cbUserProc.CheckedChanged += new EventHandler(this.cbProc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.beUserProc, "beUserProc");
    this.beUserProc.Name = "beUserProc";
    this.beUserProc.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beUserProc.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.beUserProc.ButtonClick += new ButtonPressedEventHandler(this.beUserProc_ButtonClick);
    this.panOpParmsC.Controls.Add((Control) this.cbMakeListCurrent);
    this.panOpParmsC.Controls.Add((Control) this.rbChangePage);
    this.panOpParmsC.Controls.Add((Control) this.rbNewPage);
    this.panOpParmsC.Controls.Add((Control) this.label28);
    this.panOpParmsC.Controls.Add((Control) this.beNewList);
    componentResourceManager.ApplyResources((object) this.panOpParmsC, "panOpParmsC");
    this.panOpParmsC.Name = "panOpParmsC";
    componentResourceManager.ApplyResources((object) this.cbMakeListCurrent, "cbMakeListCurrent");
    this.cbMakeListCurrent.Name = "cbMakeListCurrent";
    this.cbMakeListCurrent.UseVisualStyleBackColor = true;
    this.cbMakeListCurrent.CheckedChanged += new EventHandler(this.cbMakeListCurrent_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbChangePage, "rbChangePage");
    this.rbChangePage.Name = "rbChangePage";
    this.rbChangePage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbNewPage, "rbNewPage");
    this.rbNewPage.Checked = true;
    this.rbNewPage.Name = "rbNewPage";
    this.rbNewPage.TabStop = true;
    this.rbNewPage.UseVisualStyleBackColor = true;
    this.rbNewPage.CheckedChanged += new EventHandler(this.cbNewPage_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label28, "label28");
    this.label28.Name = "label28";
    componentResourceManager.ApplyResources((object) this.beNewList, "beNewList");
    this.beNewList.Name = "beNewList";
    this.beNewList.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beNewList.Properties.ReadOnly = true;
    this.beNewList.ButtonClick += new ButtonPressedEventHandler(this.beNewList_ButtonClick);
    this.panOpParmsB.Controls.Add((Control) this.gbVisZamens);
    this.panOpParmsB.Controls.Add((Control) this.edVersionRule);
    this.panOpParmsB.Controls.Add((Control) this.label27);
    componentResourceManager.ApplyResources((object) this.panOpParmsB, "panOpParmsB");
    this.panOpParmsB.Name = "panOpParmsB";
    componentResourceManager.ApplyResources((object) this.gbVisZamens, "gbVisZamens");
    this.gbVisZamens.Controls.Add((Control) this.rbClientSubst);
    this.gbVisZamens.Controls.Add((Control) this.rbAllSubst);
    this.gbVisZamens.Controls.Add((Control) this.rbActualSubst);
    this.gbVisZamens.Name = "gbVisZamens";
    this.gbVisZamens.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbClientSubst, "rbClientSubst");
    this.rbClientSubst.Checked = true;
    this.rbClientSubst.Name = "rbClientSubst";
    this.rbClientSubst.TabStop = true;
    this.rbClientSubst.UseVisualStyleBackColor = true;
    this.rbClientSubst.CheckedChanged += new EventHandler(this.rbActualSubst_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAllSubst, "rbAllSubst");
    this.rbAllSubst.Name = "rbAllSubst";
    this.rbAllSubst.UseVisualStyleBackColor = true;
    this.rbAllSubst.CheckedChanged += new EventHandler(this.rbActualSubst_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbActualSubst, "rbActualSubst");
    this.rbActualSubst.Name = "rbActualSubst";
    this.rbActualSubst.UseVisualStyleBackColor = true;
    this.rbActualSubst.CheckedChanged += new EventHandler(this.rbActualSubst_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.edVersionRule, "edVersionRule");
    this.edVersionRule.Name = "edVersionRule";
    this.edVersionRule.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edVersionRule.ButtonClick += new ButtonPressedEventHandler(this.edVersionRule_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label27, "label27");
    this.label27.Name = "label27";
    this.panGlobalTableFolder.Controls.Add((Control) this.tabGlobTable);
    componentResourceManager.ApplyResources((object) this.panGlobalTableFolder, "panGlobalTableFolder");
    this.panGlobalTableFolder.Name = "panGlobalTableFolder";
    this.tabGlobTable.Controls.Add((Control) this.tabPage5);
    this.tabGlobTable.Controls.Add((Control) this.tabPage3);
    this.tabGlobTable.Controls.Add((Control) this.tabPage8);
    componentResourceManager.ApplyResources((object) this.tabGlobTable, "tabGlobTable");
    this.tabGlobTable.Name = "tabGlobTable";
    this.tabGlobTable.SelectedIndex = 0;
    this.tabPage5.Controls.Add((Control) this.button6);
    this.tabPage5.Controls.Add((Control) this.button10);
    this.tabPage5.Controls.Add((Control) this.button12);
    this.tabPage5.Controls.Add((Control) this.label46);
    this.tabPage5.Controls.Add((Control) this.tvGlobCommonAttrs);
    this.tabPage5.Controls.Add((Control) this.beReplaceObjType);
    this.tabPage5.Controls.Add((Control) this.label47);
    this.tabPage5.Controls.Add((Control) this.btnDelReplaceType);
    this.tabPage5.Controls.Add((Control) this.rtbGlobalCond);
    this.tabPage5.Controls.Add((Control) this.label45);
    componentResourceManager.ApplyResources((object) this.tabPage5, "tabPage5");
    this.tabPage5.Name = "tabPage5";
    this.tabPage5.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button6, "button6");
    this.button6.ImageList = this.IL_50;
    this.button6.Name = "button6";
    this.toolTip2.SetToolTip((Control) this.button6, componentResourceManager.GetString("button6.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button6, "Не использовать атрибут");
    this.button6.UseVisualStyleBackColor = true;
    this.button6.Click += new EventHandler(this.btnDelDAttr_Click);
    componentResourceManager.ApplyResources((object) this.button10, "button10");
    this.button10.ImageList = this.IL_50;
    this.button10.Name = "button10";
    this.toolTip2.SetToolTip((Control) this.button10, componentResourceManager.GetString("button10.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button10, "Добавить атрибут связи");
    this.button10.UseVisualStyleBackColor = true;
    this.button10.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.button12, "button12");
    this.button12.ImageList = this.IL_50;
    this.button12.Name = "button12";
    this.toolTip2.SetToolTip((Control) this.button12, componentResourceManager.GetString("button12.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button12, "Добавить атрибут объекта");
    this.button12.UseVisualStyleBackColor = true;
    this.button12.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.label46, "label46");
    this.label46.Name = "label46";
    componentResourceManager.ApplyResources((object) this.tvGlobCommonAttrs, "tvGlobCommonAttrs");
    this.tvGlobCommonAttrs.ImageList = this.IL_50;
    this.tvGlobCommonAttrs.Name = "tvGlobCommonAttrs";
    this.tvGlobCommonAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvGlobCommonAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvGlobCommonAttrs.Nodes1")
    });
    this.tvGlobCommonAttrs.ShowRootLines = false;
    this.tvGlobCommonAttrs.StateImageList = this.IL_50;
    componentResourceManager.ApplyResources((object) this.beReplaceObjType, "beReplaceObjType");
    this.beReplaceObjType.Name = "beReplaceObjType";
    this.beReplaceObjType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beReplaceObjType.Properties.ReadOnly = true;
    this.beReplaceObjType.ButtonClick += new ButtonPressedEventHandler(this.edObjType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label47, "label47");
    this.label47.Name = "label47";
    componentResourceManager.ApplyResources((object) this.btnDelReplaceType, "btnDelReplaceType");
    this.btnDelReplaceType.ImageList = this.IL_50;
    this.btnDelReplaceType.Name = "btnDelReplaceType";
    this.toolTip2.SetToolTip((Control) this.btnDelReplaceType, componentResourceManager.GetString("btnDelReplaceType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnDelReplaceType, "Не заменять контекст");
    this.btnDelReplaceType.Click += new EventHandler(this.btnDelReplaceType_Click);
    componentResourceManager.ApplyResources((object) this.rtbGlobalCond, "rtbGlobalCond");
    this.rtbGlobalCond.BackColor = SystemColors.Window;
    this.rtbGlobalCond.ContextMenuStrip = this.opPopMenu;
    this.rtbGlobalCond.Name = "rtbGlobalCond";
    this.rtbGlobalCond.ReadOnly = true;
    this.rtbGlobalCond.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.rtbGlobalCond.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.rtbGlobalCond.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label45, "label45");
    this.label45.Name = "label45";
    this.tabPage3.Controls.Add((Control) this.rtbGlobObjFilter);
    this.tabPage3.Controls.Add((Control) this.label52);
    this.tabPage3.Controls.Add((Control) this.globDelete);
    this.tabPage3.Controls.Add((Control) this.globAddLinkUp);
    this.tabPage3.Controls.Add((Control) this.gbGRIsps);
    this.tabPage3.Controls.Add((Control) this.beGlobExcerpt);
    this.tabPage3.Controls.Add((Control) this.lblGRootSelect);
    this.tabPage3.Controls.Add((Control) this.btnGlobExcClear);
    this.tabPage3.Controls.Add((Control) this.btnGlobExcCreate);
    this.tabPage3.Controls.Add((Control) this.label44);
    this.tabPage3.Controls.Add((Control) this.globAddObjType);
    this.tabPage3.Controls.Add((Control) this.globAddLinkDown);
    this.tabPage3.Controls.Add((Control) this.tvGlobRoot);
    componentResourceManager.ApplyResources((object) this.tabPage3, "tabPage3");
    this.tabPage3.Name = "tabPage3";
    this.tabPage3.UseVisualStyleBackColor = true;
    this.rtbGlobObjFilter.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.rtbGlobObjFilter, "rtbGlobObjFilter");
    this.rtbGlobObjFilter.Name = "rtbGlobObjFilter";
    this.rtbGlobObjFilter.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.rtbGlobObjFilter.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.rtbGlobObjFilter.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label52, "label52");
    this.label52.Name = "label52";
    componentResourceManager.ApplyResources((object) this.globDelete, "globDelete");
    this.globDelete.ImageList = this.IL_50;
    this.globDelete.Name = "globDelete";
    this.toolTip2.SetToolTip((Control) this.globDelete, componentResourceManager.GetString("globDelete.ToolTip"));
    this.tipCon.SetToolTip((Control) this.globDelete, "Удалить тип связи или объекта");
    this.globDelete.Click += new EventHandler(this.globDelete_Click);
    componentResourceManager.ApplyResources((object) this.globAddLinkUp, "globAddLinkUp");
    this.globAddLinkUp.ImageList = this.IL_50;
    this.globAddLinkUp.Name = "globAddLinkUp";
    this.toolTip2.SetToolTip((Control) this.globAddLinkUp, componentResourceManager.GetString("globAddLinkUp.ToolTip"));
    this.tipCon.SetToolTip((Control) this.globAddLinkUp, "Искать по связи вверх");
    this.globAddLinkUp.UseVisualStyleBackColor = false;
    this.globAddLinkUp.Click += new EventHandler(this.globAddLinkDown_Click);
    this.gbGRIsps.Controls.Add((Control) this.radioButton10);
    this.gbGRIsps.Controls.Add((Control) this.radioButton12);
    this.gbGRIsps.Controls.Add((Control) this.radioButton13);
    componentResourceManager.ApplyResources((object) this.gbGRIsps, "gbGRIsps");
    this.gbGRIsps.Name = "gbGRIsps";
    this.gbGRIsps.TabStop = false;
    componentResourceManager.ApplyResources((object) this.radioButton10, "radioButton10");
    this.radioButton10.Name = "radioButton10";
    this.radioButton10.Tag = (object) "3";
    this.radioButton10.UseVisualStyleBackColor = true;
    this.radioButton10.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButton12, "radioButton12");
    this.radioButton12.Name = "radioButton12";
    this.radioButton12.Tag = (object) "1";
    this.radioButton12.UseVisualStyleBackColor = true;
    this.radioButton12.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButton13, "radioButton13");
    this.radioButton13.Checked = true;
    this.radioButton13.Name = "radioButton13";
    this.radioButton13.TabStop = true;
    this.radioButton13.Tag = (object) "0";
    this.radioButton13.UseVisualStyleBackColor = true;
    this.radioButton13.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.beGlobExcerpt, "beGlobExcerpt");
    this.beGlobExcerpt.Name = "beGlobExcerpt";
    this.beGlobExcerpt.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beGlobExcerpt.Properties.ReadOnly = true;
    this.beGlobExcerpt.Properties.ButtonClick += new ButtonPressedEventHandler(this.btnEdExcerpt_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.lblGRootSelect, "lblGRootSelect");
    this.lblGRootSelect.Name = "lblGRootSelect";
    componentResourceManager.ApplyResources((object) this.btnGlobExcClear, "btnGlobExcClear");
    this.btnGlobExcClear.ImageList = this.IL_50;
    this.btnGlobExcClear.Name = "btnGlobExcClear";
    this.toolTip2.SetToolTip((Control) this.btnGlobExcClear, componentResourceManager.GetString("btnGlobExcClear.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnGlobExcClear, "Не использовать выборку");
    this.btnGlobExcClear.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.btnGlobExcCreate, "btnGlobExcCreate");
    this.btnGlobExcCreate.ImageList = this.IL_50;
    this.btnGlobExcCreate.Name = "btnGlobExcCreate";
    this.toolTip2.SetToolTip((Control) this.btnGlobExcCreate, componentResourceManager.GetString("btnGlobExcCreate.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnGlobExcCreate, "Создать новую выборку");
    this.btnGlobExcCreate.Click += new EventHandler(this.button1_Click_1);
    componentResourceManager.ApplyResources((object) this.label44, "label44");
    this.label44.Name = "label44";
    componentResourceManager.ApplyResources((object) this.globAddObjType, "globAddObjType");
    this.globAddObjType.ImageList = this.IL_50;
    this.globAddObjType.Name = "globAddObjType";
    this.toolTip2.SetToolTip((Control) this.globAddObjType, componentResourceManager.GetString("globAddObjType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.globAddObjType, "Искать типы объектов для связи");
    this.globAddObjType.UseVisualStyleBackColor = false;
    this.globAddObjType.Click += new EventHandler(this.globAddObjType_Click);
    componentResourceManager.ApplyResources((object) this.globAddLinkDown, "globAddLinkDown");
    this.globAddLinkDown.ImageList = this.IL_50;
    this.globAddLinkDown.Name = "globAddLinkDown";
    this.toolTip2.SetToolTip((Control) this.globAddLinkDown, componentResourceManager.GetString("globAddLinkDown.ToolTip"));
    this.tipCon.SetToolTip((Control) this.globAddLinkDown, "Искать по связи вниз");
    this.globAddLinkDown.UseVisualStyleBackColor = false;
    this.globAddLinkDown.Click += new EventHandler(this.globAddLinkDown_Click);
    componentResourceManager.ApplyResources((object) this.tvGlobRoot, "tvGlobRoot");
    this.tvGlobRoot.ImageList = this.IL_50;
    this.tvGlobRoot.Name = "tvGlobRoot";
    this.tvGlobRoot.Nodes.AddRange(new TreeNode[1]
    {
      (TreeNode) componentResourceManager.GetObject("tvGlobRoot.Nodes")
    });
    this.tvGlobRoot.ShowRootLines = false;
    this.tvGlobRoot.StateImageList = this.IL_50;
    this.tabPage8.Controls.Add((Control) this.gbSostav1);
    componentResourceManager.ApplyResources((object) this.tabPage8, "tabPage8");
    this.tabPage8.Name = "tabPage8";
    this.tabPage8.UseVisualStyleBackColor = true;
    this.gbSostav1.Controls.Add((Control) this.rbHideHiddenRoots1);
    this.gbSostav1.Controls.Add((Control) this.rbHideHidden1);
    this.gbSostav1.Controls.Add((Control) this.rbShowHidden1);
    this.gbSostav1.Controls.Add((Control) this.cbConfigOptions1);
    componentResourceManager.ApplyResources((object) this.gbSostav1, "gbSostav1");
    this.gbSostav1.Name = "gbSostav1";
    this.gbSostav1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbHideHiddenRoots1, "rbHideHiddenRoots1");
    this.rbHideHiddenRoots1.Name = "rbHideHiddenRoots1";
    this.rbHideHiddenRoots1.Tag = (object) "2";
    this.rbHideHiddenRoots1.UseVisualStyleBackColor = true;
    this.rbHideHiddenRoots1.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbHideHidden1, "rbHideHidden1");
    this.rbHideHidden1.Name = "rbHideHidden1";
    this.rbHideHidden1.Tag = (object) "1";
    this.rbHideHidden1.UseVisualStyleBackColor = true;
    this.rbHideHidden1.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbShowHidden1, "rbShowHidden1");
    this.rbShowHidden1.Checked = true;
    this.rbShowHidden1.Name = "rbShowHidden1";
    this.rbShowHidden1.TabStop = true;
    this.rbShowHidden1.Tag = (object) "0";
    this.rbShowHidden1.UseVisualStyleBackColor = true;
    this.rbShowHidden1.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbConfigOptions1, "cbConfigOptions1");
    this.cbConfigOptions1.Name = "cbConfigOptions1";
    this.cbConfigOptions1.UseVisualStyleBackColor = true;
    this.cbConfigOptions1.CheckedChanged += new EventHandler(this.cbConfigOptions_CheckedChanged);
    this.panOpGlobalType.Controls.Add((Control) this.tabControlGT);
    componentResourceManager.ApplyResources((object) this.panOpGlobalType, "panOpGlobalType");
    this.panOpGlobalType.Name = "panOpGlobalType";
    this.tabControlGT.Controls.Add((Control) this.tabGTParms);
    this.tabControlGT.Controls.Add((Control) this.tabGTObjects);
    this.tabControlGT.Controls.Add((Control) this.tabGTOther);
    componentResourceManager.ApplyResources((object) this.tabControlGT, "tabControlGT");
    this.tabControlGT.Name = "tabControlGT";
    this.tabControlGT.SelectedIndex = 0;
    this.tabGTParms.Controls.Add((Control) this.button18);
    this.tabGTParms.Controls.Add((Control) this.btnAddForObjType);
    this.tabGTParms.Controls.Add((Control) this.button7);
    this.tabGTParms.Controls.Add((Control) this.button8);
    this.tabGTParms.Controls.Add((Control) this.button9);
    this.tabGTParms.Controls.Add((Control) this.label48);
    this.tabGTParms.Controls.Add((Control) this.tvGTAttrs);
    this.tabGTParms.Controls.Add((Control) this.gbIspForGT);
    this.tabGTParms.Controls.Add((Control) this.cbForObjTypes);
    this.tabGTParms.Controls.Add((Control) this.label42);
    componentResourceManager.ApplyResources((object) this.tabGTParms, "tabGTParms");
    this.tabGTParms.Name = "tabGTParms";
    this.tabGTParms.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button18, "button18");
    this.button18.ImageList = this.IL_50;
    this.button18.Name = "button18";
    this.toolTip2.SetToolTip((Control) this.button18, componentResourceManager.GetString("button18.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button18, "Удалить тип объекта");
    this.button18.Click += new EventHandler(this.button18_Click);
    componentResourceManager.ApplyResources((object) this.btnAddForObjType, "btnAddForObjType");
    this.btnAddForObjType.ImageList = this.IL_50;
    this.btnAddForObjType.Name = "btnAddForObjType";
    this.toolTip2.SetToolTip((Control) this.btnAddForObjType, componentResourceManager.GetString("btnAddForObjType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnAddForObjType, "Настроить типы объектов");
    this.btnAddForObjType.UseVisualStyleBackColor = false;
    this.btnAddForObjType.Click += new EventHandler(this.btnAddForObjType_Click);
    componentResourceManager.ApplyResources((object) this.button7, "button7");
    this.button7.ImageList = this.IL_50;
    this.button7.Name = "button7";
    this.toolTip2.SetToolTip((Control) this.button7, componentResourceManager.GetString("button7.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button7, "Не использовать атрибут");
    this.button7.UseVisualStyleBackColor = true;
    this.button7.Click += new EventHandler(this.btnDelDAttr_Click);
    componentResourceManager.ApplyResources((object) this.button8, "button8");
    this.button8.ImageList = this.IL_50;
    this.button8.Name = "button8";
    this.toolTip2.SetToolTip((Control) this.button8, componentResourceManager.GetString("button8.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button8, "Добавить атрибут связи");
    this.button8.UseVisualStyleBackColor = true;
    this.button8.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.button9, "button9");
    this.button9.ImageList = this.IL_50;
    this.button9.Name = "button9";
    this.toolTip2.SetToolTip((Control) this.button9, componentResourceManager.GetString("button9.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button9, "Добавить атрибут объекта");
    this.button9.UseVisualStyleBackColor = true;
    this.button9.Click += new EventHandler(this.btnAddDAttr_Click);
    componentResourceManager.ApplyResources((object) this.label48, "label48");
    this.label48.Name = "label48";
    componentResourceManager.ApplyResources((object) this.tvGTAttrs, "tvGTAttrs");
    this.tvGTAttrs.ImageList = this.IL_50;
    this.tvGTAttrs.Name = "tvGTAttrs";
    this.tvGTAttrs.Nodes.AddRange(new TreeNode[2]
    {
      (TreeNode) componentResourceManager.GetObject("tvGTAttrs.Nodes"),
      (TreeNode) componentResourceManager.GetObject("tvGTAttrs.Nodes1")
    });
    this.tvGTAttrs.ShowRootLines = false;
    this.tvGTAttrs.StateImageList = this.IL_50;
    componentResourceManager.ApplyResources((object) this.gbIspForGT, "gbIspForGT");
    this.gbIspForGT.Controls.Add((Control) this.radioButton3);
    this.gbIspForGT.Controls.Add((Control) this.radioButton4);
    this.gbIspForGT.Controls.Add((Control) this.radioButton5);
    this.gbIspForGT.Name = "gbIspForGT";
    this.gbIspForGT.TabStop = false;
    componentResourceManager.ApplyResources((object) this.radioButton3, "radioButton3");
    this.radioButton3.Name = "radioButton3";
    this.radioButton3.Tag = (object) "3";
    this.radioButton3.UseVisualStyleBackColor = true;
    this.radioButton3.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButton4, "radioButton4");
    this.radioButton4.Name = "radioButton4";
    this.radioButton4.Tag = (object) "1";
    this.radioButton4.UseVisualStyleBackColor = true;
    this.radioButton4.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButton5, "radioButton5");
    this.radioButton5.Checked = true;
    this.radioButton5.Name = "radioButton5";
    this.radioButton5.TabStop = true;
    this.radioButton5.Tag = (object) "0";
    this.radioButton5.UseVisualStyleBackColor = true;
    this.radioButton5.CheckedChanged += new EventHandler(this.rbNoIspolns_CheckedChanged);
    this.cbForObjTypes.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbForObjTypes, "cbForObjTypes");
    this.cbForObjTypes.Name = "cbForObjTypes";
    this.cbForObjTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbForObjTypes_ItemCheck);
    componentResourceManager.ApplyResources((object) this.label42, "label42");
    this.label42.Name = "label42";
    this.tabGTObjects.Controls.Add((Control) this.gtDelete);
    this.tabGTObjects.Controls.Add((Control) this.gtAddLinkUp);
    this.tabGTObjects.Controls.Add((Control) this.beGTExcerpt);
    this.tabGTObjects.Controls.Add((Control) this.lblGTExcerpt);
    this.tabGTObjects.Controls.Add((Control) this.btnGTExcClear);
    this.tabGTObjects.Controls.Add((Control) this.btnGTExcCreate);
    this.tabGTObjects.Controls.Add((Control) this.label51);
    this.tabGTObjects.Controls.Add((Control) this.gtAddObjType);
    this.tabGTObjects.Controls.Add((Control) this.gtAddLinkDown);
    this.tabGTObjects.Controls.Add((Control) this.tvGTSearch);
    this.tabGTObjects.Controls.Add((Control) this.rtGTCond);
    this.tabGTObjects.Controls.Add((Control) this.label49);
    componentResourceManager.ApplyResources((object) this.tabGTObjects, "tabGTObjects");
    this.tabGTObjects.Name = "tabGTObjects";
    this.tabGTObjects.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.gtDelete, "gtDelete");
    this.gtDelete.ImageList = this.IL_50;
    this.gtDelete.Name = "gtDelete";
    this.toolTip2.SetToolTip((Control) this.gtDelete, componentResourceManager.GetString("gtDelete.ToolTip"));
    this.tipCon.SetToolTip((Control) this.gtDelete, "Удалить тип объекта или связи");
    this.gtDelete.Click += new EventHandler(this.globDelete_Click);
    componentResourceManager.ApplyResources((object) this.gtAddLinkUp, "gtAddLinkUp");
    this.gtAddLinkUp.ImageList = this.IL_50;
    this.gtAddLinkUp.Name = "gtAddLinkUp";
    this.toolTip2.SetToolTip((Control) this.gtAddLinkUp, componentResourceManager.GetString("gtAddLinkUp.ToolTip"));
    this.tipCon.SetToolTip((Control) this.gtAddLinkUp, "Добавить тип связей вверх");
    this.gtAddLinkUp.UseVisualStyleBackColor = false;
    this.gtAddLinkUp.Click += new EventHandler(this.globAddLinkDown_Click);
    componentResourceManager.ApplyResources((object) this.beGTExcerpt, "beGTExcerpt");
    this.beGTExcerpt.Name = "beGTExcerpt";
    this.beGTExcerpt.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beGTExcerpt.Properties.ReadOnly = true;
    this.beGTExcerpt.Properties.ButtonClick += new ButtonPressedEventHandler(this.btnEdExcerpt_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.lblGTExcerpt, "lblGTExcerpt");
    this.lblGTExcerpt.Name = "lblGTExcerpt";
    componentResourceManager.ApplyResources((object) this.btnGTExcClear, "btnGTExcClear");
    this.btnGTExcClear.ImageList = this.IL_50;
    this.btnGTExcClear.Name = "btnGTExcClear";
    this.toolTip2.SetToolTip((Control) this.btnGTExcClear, componentResourceManager.GetString("btnGTExcClear.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnGTExcClear, "Не использовать выборку");
    this.btnGTExcClear.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.btnGTExcCreate, "btnGTExcCreate");
    this.btnGTExcCreate.ImageList = this.IL_50;
    this.btnGTExcCreate.Name = "btnGTExcCreate";
    this.toolTip2.SetToolTip((Control) this.btnGTExcCreate, componentResourceManager.GetString("btnGTExcCreate.ToolTip"));
    this.tipCon.SetToolTip((Control) this.btnGTExcCreate, "Создать новую выборку");
    this.btnGTExcCreate.Click += new EventHandler(this.button1_Click_1);
    componentResourceManager.ApplyResources((object) this.label51, "label51");
    this.label51.Name = "label51";
    componentResourceManager.ApplyResources((object) this.gtAddObjType, "gtAddObjType");
    this.gtAddObjType.ImageList = this.IL_50;
    this.gtAddObjType.Name = "gtAddObjType";
    this.toolTip2.SetToolTip((Control) this.gtAddObjType, componentResourceManager.GetString("gtAddObjType.ToolTip"));
    this.tipCon.SetToolTip((Control) this.gtAddObjType, "Типы объектов по связям этого типа");
    this.gtAddObjType.UseVisualStyleBackColor = false;
    this.gtAddObjType.Click += new EventHandler(this.globAddObjType_Click);
    componentResourceManager.ApplyResources((object) this.gtAddLinkDown, "gtAddLinkDown");
    this.gtAddLinkDown.ImageList = this.IL_50;
    this.gtAddLinkDown.Name = "gtAddLinkDown";
    this.toolTip2.SetToolTip((Control) this.gtAddLinkDown, componentResourceManager.GetString("gtAddLinkDown.ToolTip"));
    this.tipCon.SetToolTip((Control) this.gtAddLinkDown, "Добавить тип связей вниз");
    this.gtAddLinkDown.UseVisualStyleBackColor = false;
    this.gtAddLinkDown.Click += new EventHandler(this.globAddLinkDown_Click);
    componentResourceManager.ApplyResources((object) this.tvGTSearch, "tvGTSearch");
    this.tvGTSearch.ImageList = this.IL_50;
    this.tvGTSearch.Name = "tvGTSearch";
    this.tvGTSearch.Nodes.AddRange(new TreeNode[1]
    {
      (TreeNode) componentResourceManager.GetObject("tvGTSearch.Nodes")
    });
    this.tvGTSearch.ShowRootLines = false;
    this.tvGTSearch.StateImageList = this.IL_50;
    this.rtGTCond.BackColor = SystemColors.Window;
    this.rtGTCond.ContextMenuStrip = this.opPopMenu;
    componentResourceManager.ApplyResources((object) this.rtGTCond, "rtGTCond");
    this.rtGTCond.Name = "rtGTCond";
    this.rtGTCond.ReadOnly = true;
    this.rtGTCond.DoubleClick += new EventHandler(this.menuChangeOpForm_Click);
    this.rtGTCond.MouseDown += new MouseEventHandler(this.richWhileCond_MouseDown);
    this.rtGTCond.MouseMove += new MouseEventHandler(this.CondEdit_MouseMove);
    componentResourceManager.ApplyResources((object) this.label49, "label49");
    this.label49.Name = "label49";
    this.tabGTOther.Controls.Add((Control) this.gbSostav2);
    componentResourceManager.ApplyResources((object) this.tabGTOther, "tabGTOther");
    this.tabGTOther.Name = "tabGTOther";
    this.tabGTOther.UseVisualStyleBackColor = true;
    this.gbSostav2.Controls.Add((Control) this.rbHideHiddenRoots2);
    this.gbSostav2.Controls.Add((Control) this.rbHideHidden2);
    this.gbSostav2.Controls.Add((Control) this.rbShowHidden2);
    this.gbSostav2.Controls.Add((Control) this.cbConfigOptions2);
    componentResourceManager.ApplyResources((object) this.gbSostav2, "gbSostav2");
    this.gbSostav2.Name = "gbSostav2";
    this.gbSostav2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbHideHiddenRoots2, "rbHideHiddenRoots2");
    this.rbHideHiddenRoots2.Name = "rbHideHiddenRoots2";
    this.rbHideHiddenRoots2.Tag = (object) "2";
    this.rbHideHiddenRoots2.UseVisualStyleBackColor = true;
    this.rbHideHiddenRoots2.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbHideHidden2, "rbHideHidden2");
    this.rbHideHidden2.Name = "rbHideHidden2";
    this.rbHideHidden2.Tag = (object) "1";
    this.rbHideHidden2.UseVisualStyleBackColor = true;
    this.rbHideHidden2.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbShowHidden2, "rbShowHidden2");
    this.rbShowHidden2.Checked = true;
    this.rbShowHidden2.Name = "rbShowHidden2";
    this.rbShowHidden2.TabStop = true;
    this.rbShowHidden2.Tag = (object) "0";
    this.rbShowHidden2.UseVisualStyleBackColor = true;
    this.rbShowHidden2.CheckedChanged += new EventHandler(this.rbContentsAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbConfigOptions2, "cbConfigOptions2");
    this.cbConfigOptions2.Name = "cbConfigOptions2";
    this.cbConfigOptions2.UseVisualStyleBackColor = true;
    this.cbConfigOptions2.CheckedChanged += new EventHandler(this.cbConfigOptions_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.panOpParmsEmpty, "panOpParmsEmpty");
    this.panOpParmsEmpty.Name = "panOpParmsEmpty";
    this.IL2.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL2.ImageStream");
    this.IL2.TransparentColor = Color.Green;
    this.IL2.Images.SetKeyName(0, "");
    this.IL2.Images.SetKeyName(1, "");
    this.bottomDock.Controls.Add((Control) this.dockDesc);
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("72555f96-2233-4e5e-882b-5054287eedc6");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Vertical, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(1111, 191, new DockControl[1]
      {
        this.dockDesc
      }, this.dockDesc)
    });
    this.bottomDock.Manager = this.dockMan;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    this.dockDesc.Closable = false;
    this.dockDesc.Controls.Add((Control) this.lblDescr);
    componentResourceManager.ApplyResources((object) this.dockDesc, "dockDesc");
    this.dockDesc.FloatingLocation = new Point(515, 312);
    this.dockDesc.Guid = new Guid("3adccb0d-dfa6-439e-9905-1f1339a155c4");
    this.dockDesc.Name = "dockDesc";
    componentResourceManager.ApplyResources((object) this.lblDescr, "lblDescr");
    this.lblDescr.Name = "lblDescr";
    this.lblDescr.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("8491ab9b-17db-4138-9521-1a8c56fa3c81");
    this.topDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.topDock.Manager = this.dockMan;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    this.treePopMenu.ImageList = this.IL;
    this.treePopMenu.ImageScalingSize = new Size(24, 24);
    this.treePopMenu.Items.AddRange(new ToolStripItem[15]
    {
      (ToolStripItem) this.menuInsBefore,
      (ToolStripItem) this.menuInsAfter,
      (ToolStripItem) this.menuInsInto,
      (ToolStripItem) this.menuItem6,
      (ToolStripItem) this.menuChange,
      (ToolStripItem) this.menuApply,
      (ToolStripItem) this.menuItem9,
      (ToolStripItem) this.menuCut,
      (ToolStripItem) this.menuCopy,
      (ToolStripItem) this.menuPaste,
      (ToolStripItem) this.menuItem13,
      (ToolStripItem) this.menuCollapse,
      (ToolStripItem) this.menuExpand,
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.menuDelete
    });
    this.treePopMenu.Name = "treePopMenu";
    componentResourceManager.ApplyResources((object) this.treePopMenu, "treePopMenu");
    this.treePopMenu.Opening += new CancelEventHandler(this.treePopMenu_Opening);
    componentResourceManager.ApplyResources((object) this.menuInsBefore, "menuInsBefore");
    this.menuInsBefore.Name = "menuInsBefore";
    this.menuInsBefore.Click += new EventHandler(this.menuItem3_Click);
    componentResourceManager.ApplyResources((object) this.menuInsAfter, "menuInsAfter");
    this.menuInsAfter.Name = "menuInsAfter";
    this.menuInsAfter.Click += new EventHandler(this.menuItem3_Click);
    componentResourceManager.ApplyResources((object) this.menuInsInto, "menuInsInto");
    this.menuInsInto.Name = "menuInsInto";
    this.menuInsInto.Click += new EventHandler(this.menuItem3_Click);
    this.menuItem6.Name = "menuItem6";
    componentResourceManager.ApplyResources((object) this.menuItem6, "menuItem6");
    componentResourceManager.ApplyResources((object) this.menuChange, "menuChange");
    this.menuChange.Name = "menuChange";
    this.menuChange.Click += new EventHandler(this.menuItem3_Click);
    this.menuApply.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.menuApply, "menuApply");
    this.menuApply.Name = "menuApply";
    this.menuApply.Click += new EventHandler(this.menuItem3_Click);
    this.menuItem9.Name = "menuItem9";
    componentResourceManager.ApplyResources((object) this.menuItem9, "menuItem9");
    componentResourceManager.ApplyResources((object) this.menuCut, "menuCut");
    this.menuCut.Name = "menuCut";
    this.menuCut.Click += new EventHandler(this.menuItem3_Click);
    componentResourceManager.ApplyResources((object) this.menuCopy, "menuCopy");
    this.menuCopy.Name = "menuCopy";
    this.menuCopy.Click += new EventHandler(this.menuItem3_Click);
    componentResourceManager.ApplyResources((object) this.menuPaste, "menuPaste");
    this.menuPaste.Name = "menuPaste";
    this.menuPaste.Click += new EventHandler(this.menuItem3_Click);
    this.menuItem13.Name = "menuItem13";
    componentResourceManager.ApplyResources((object) this.menuItem13, "menuItem13");
    this.menuCollapse.Name = "menuCollapse";
    componentResourceManager.ApplyResources((object) this.menuCollapse, "menuCollapse");
    this.menuCollapse.Click += new EventHandler(this.menuCollapse_Click);
    this.menuExpand.Name = "menuExpand";
    componentResourceManager.ApplyResources((object) this.menuExpand, "menuExpand");
    this.menuExpand.Click += new EventHandler(this.menuExpand_Click);
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem3, "toolStripMenuItem3");
    componentResourceManager.ApplyResources((object) this.menuDelete, "menuDelete");
    this.menuDelete.Name = "menuDelete";
    this.menuDelete.Click += new EventHandler(this.menuItem3_Click);
    this.groupMenu.ImageScalingSize = new Size(24, 24);
    this.groupMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.menuItem2
    });
    this.groupMenu.Name = "groupMenu";
    componentResourceManager.ApplyResources((object) this.groupMenu, "groupMenu");
    this.menuItem2.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.удалить;
    this.menuItem2.Name = "menuItem2";
    componentResourceManager.ApplyResources((object) this.menuItem2, "menuItem2");
    this.sortMenu.ImageScalingSize = new Size(24, 24);
    this.sortMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.menuItem1
    });
    this.sortMenu.Name = "sortMenu";
    componentResourceManager.ApplyResources((object) this.sortMenu, "sortMenu");
    this.menuItem1.Image = (System.Drawing.Image) Intermech.Expert.Editor.Properties.Resources.удалить;
    this.menuItem1.Name = "menuItem1";
    componentResourceManager.ApplyResources((object) this.menuItem1, "menuItem1");
    this.toolTip2.AutoPopDelay = 1000;
    this.toolTip2.InitialDelay = 0;
    this.toolTip2.ReshowDelay = 0;
    componentResourceManager.ApplyResources((object) this.button17, "button17");
    this.button17.ImageList = this.IL;
    this.button17.Name = "button17";
    this.toolTip2.SetToolTip((Control) this.button17, componentResourceManager.GetString("button17.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button17, "Добавить тип связей вниз");
    this.button17.UseVisualStyleBackColor = false;
    componentResourceManager.ApplyResources((object) this.button13, "button13");
    this.button13.ImageList = this.IL;
    this.button13.Name = "button13";
    this.toolTip2.SetToolTip((Control) this.button13, componentResourceManager.GetString("button13.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button13, "Добавить тип связей вверх");
    this.button13.UseVisualStyleBackColor = false;
    componentResourceManager.ApplyResources((object) this.button16, "button16");
    this.button16.ImageList = this.IL;
    this.button16.Name = "button16";
    this.toolTip2.SetToolTip((Control) this.button16, componentResourceManager.GetString("button16.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button16, "Типы объектов по связям этого типа");
    this.button16.UseVisualStyleBackColor = false;
    componentResourceManager.ApplyResources((object) this.button11, "button11");
    this.button11.ImageList = this.IL;
    this.button11.Name = "button11";
    this.toolTip2.SetToolTip((Control) this.button11, componentResourceManager.GetString("button11.ToolTip"));
    this.tipCon.SetToolTip((Control) this.button11, "Удалить тип объекта или связи");
    componentResourceManager.ApplyResources((object) this.btnCheckTemplate, "btnCheckTemplate");
    this.btnCheckTemplate.ImageList = this.IL_NEW;
    this.btnCheckTemplate.Name = "btnCheckTemplate";
    this.toolTip2.SetToolTip((Control) this.btnCheckTemplate, componentResourceManager.GetString("btnCheckTemplate.ToolTip"));
    this.btnCheckTemplate.UseVisualStyleBackColor = true;
    this.btnCheckTemplate.Click += new EventHandler(this.btnCheckTemplate_Click);
    componentResourceManager.ApplyResources((object) this.btnCheckDown, "btnCheckDown");
    this.btnCheckDown.ImageList = this.IL_NEW;
    this.btnCheckDown.Name = "btnCheckDown";
    this.toolTip2.SetToolTip((Control) this.btnCheckDown, componentResourceManager.GetString("btnCheckDown.ToolTip"));
    this.btnCheckDown.UseVisualStyleBackColor = true;
    this.btnCheckDown.Click += new EventHandler(this.btnCheckDown_Click);
    this.panelApply.Controls.Add((Control) this.cbCheckout);
    this.panelApply.Controls.Add((Control) this.labelChecks);
    this.panelApply.Controls.Add((Control) this.btnCheckTemplate);
    this.panelApply.Controls.Add((Control) this.btnCheckDown);
    this.panelApply.Controls.Add((Control) this.cbCoCreator);
    this.panelApply.Controls.Add((Control) this.btnLoadXml);
    this.panelApply.Controls.Add((Control) this.lblDocType);
    this.panelApply.Controls.Add((Control) this.btnDocParms);
    this.panelApply.Controls.Add((Control) this.btnSaveXml);
    this.panelApply.Controls.Add((Control) this.btnCancel);
    this.panelApply.Controls.Add((Control) this.btnTemplate);
    this.panelApply.Controls.Add((Control) this.btnCond);
    this.panelApply.Controls.Add((Control) this.btnOK);
    this.panelApply.Controls.Add((Control) this.btnSave);
    componentResourceManager.ApplyResources((object) this.panelApply, "panelApply");
    this.panelApply.Name = "panelApply";
    componentResourceManager.ApplyResources((object) this.cbCheckout, "cbCheckout");
    this.cbCheckout.ForeColor = Color.Red;
    this.cbCheckout.Name = "cbCheckout";
    this.cbCheckout.UseVisualStyleBackColor = true;
    this.cbCheckout.CheckedChanged += new EventHandler(this.cbCoCreator_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.labelChecks, "labelChecks");
    this.labelChecks.Name = "labelChecks";
    componentResourceManager.ApplyResources((object) this.cbCoCreator, "cbCoCreator");
    this.cbCoCreator.ForeColor = Color.Red;
    this.cbCoCreator.Name = "cbCoCreator";
    this.cbCoCreator.UseVisualStyleBackColor = true;
    this.cbCoCreator.CheckedChanged += new EventHandler(this.cbCoCreator_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnLoadXml, "btnLoadXml");
    this.btnLoadXml.Name = "btnLoadXml";
    this.btnLoadXml.UseVisualStyleBackColor = true;
    this.btnLoadXml.Click += new EventHandler(this.btnLoadXml_Click);
    componentResourceManager.ApplyResources((object) this.lblDocType, "lblDocType");
    this.lblDocType.Name = "lblDocType";
    componentResourceManager.ApplyResources((object) this.btnDocParms, "btnDocParms");
    this.btnDocParms.Name = "btnDocParms";
    this.btnDocParms.UseVisualStyleBackColor = true;
    this.btnDocParms.Click += new EventHandler(this.btnDocParms_Click);
    componentResourceManager.ApplyResources((object) this.btnSaveXml, "btnSaveXml");
    this.btnSaveXml.Name = "btnSaveXml";
    this.btnSaveXml.UseVisualStyleBackColor = true;
    this.btnSaveXml.Click += new EventHandler(this.btnSaveXml_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnTemplate, "btnTemplate");
    this.btnTemplate.Name = "btnTemplate";
    this.btnTemplate.Click += new EventHandler(this.btnTemplate_Click);
    componentResourceManager.ApplyResources((object) this.btnCond, "btnCond");
    this.btnCond.Name = "btnCond";
    this.btnCond.UseVisualStyleBackColor = true;
    this.btnCond.Click += new EventHandler(this.btnCond_Click);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnSave, "btnSave");
    this.btnSave.Name = "btnSave";
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.tree.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this.tree, "tree");
    this.tree.Columns.AddRange(new TreeListColumn[3]
    {
      this.Struct,
      this.ModParms,
      this.OperParms
    });
    this.tree.ContextMenuStrip = this.treePopMenu;
    this.tree.Name = "tree";
    this.tree.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.repositoryItemTextEdit1
    });
    this.tree.SelectImageList = this.IL_50;
    this.tree.StateImageList = this.IL_50;
    this.tree.GetPreviewText += new GetPreviewTextEventHandler(this.tree_GetPreviewText);
    this.tree.BeforeDragNode += new BeforeDragNodeEventHandler(this.tree_BeforeDragNode);
    this.tree.BeforeFocusNode += new BeforeFocusNodeEventHandler(this.tree_BeforeFocusNode);
    this.tree.AfterFocusNode += new NodeEventHandler(this.tree_AfterFocusNode);
    this.tree.CellValueChanged += new CellValueChangedEventHandler(this.tree_CellValueChanged);
    this.tree.DragDrop += new DragEventHandler(this.tree_DragDrop);
    this.tree.DragEnter += new DragEventHandler(this.tree_DragEnter);
    this.tree.DragOver += new DragEventHandler(this.tree_DragOver);
    this.tree.MouseMove += new MouseEventHandler(this.tree_MouseMove);
    componentResourceManager.ApplyResources((object) this.Struct, "Struct");
    this.Struct.ColumnEdit = (RepositoryItem) this.repositoryItemTextEdit2;
    this.Struct.Name = "Struct";
    this.repositoryItemTextEdit2.AutoHeight = false;
    this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
    componentResourceManager.ApplyResources((object) this.ModParms, "ModParms");
    this.ModParms.Name = "ModParms";
    componentResourceManager.ApplyResources((object) this.OperParms, "OperParms");
    this.OperParms.Name = "OperParms";
    this.repositoryItemTextEdit1.AutoHeight = false;
    this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
    this.tipCon.Active = false;
    this.tipCon.Style = new ViewStyle("ToolTip style");
    this.sd.DefaultExt = "XML";
    componentResourceManager.ApplyResources((object) this.sd, "sd");
    this.sd.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.od, "od");
    this.od.RestoreDirectory = true;
    this.fontDialog1.Color = SystemColors.ControlText;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Checked = true;
    this.checkBox1.CheckState = CheckState.Checked;
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.checkBox2, "checkBox2");
    this.checkBox2.Checked = true;
    this.checkBox2.CheckState = CheckState.Checked;
    this.checkBox2.Name = "checkBox2";
    this.checkBox2.UseVisualStyleBackColor = true;
    this.btnTILink.ImageIndex = 66;
    this.btnTILink.Tag = (object) "66";
    componentResourceManager.ApplyResources((object) this.btnTILink, "btnTILink");
    this.panel13.Controls.Add((Control) this.btnUpdate);
    componentResourceManager.ApplyResources((object) this.panel13, "panel13");
    this.panel13.Name = "panel13";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tree);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.panelApply);
    this.MinimizeBox = false;
    this.Name = nameof (ScriptEdit2);
    this.FormClosing += new FormClosingEventHandler(this.ScriptEdit2_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ScriptEdit2_FormClosed);
    this.Load += new EventHandler(this.ScriptEdit_Load);
    this.Shown += new EventHandler(this.ScriptEdit2_Shown);
    this.KeyDown += new KeyEventHandler(this.ScriptEdit_KeyDown);
    this.KeyPress += new KeyPressEventHandler(this.ScriptEdit_KeyPress);
    this.leftDock.ResumeLayout(false);
    this.dockOpMod.ResumeLayout(false);
    this.panelScroll.ResumeLayout(false);
    this.panelControl.ResumeLayout(false);
    this.rightDock.ResumeLayout(false);
    this.dockModParms.ResumeLayout(false);
    this.panModParmsVersion.ResumeLayout(false);
    this.panel10.ResumeLayout(false);
    this.groupBox12.ResumeLayout(false);
    this.modPopMenu.ResumeLayout(false);
    this.panel9.ResumeLayout(false);
    this.groupBox11.ResumeLayout(false);
    this.groupBox11.PerformLayout();
    this.panel8.ResumeLayout(false);
    this.groupBox9.ResumeLayout(false);
    this.groupBox9.PerformLayout();
    this.panModParms5.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.btnForAttr.Properties.EndInit();
    this.panel1.ResumeLayout(false);
    this.btnRefAttr.Properties.EndInit();
    this.spinEdit1.Properties.EndInit();
    this.panModParms4.ResumeLayout(false);
    this.gbResType.ResumeLayout(false);
    this.gbResType.PerformLayout();
    this.panModParms3.ResumeLayout(false);
    this.panModParms3.PerformLayout();
    this.panModParms2.ResumeLayout(false);
    this.panModParms2.PerformLayout();
    this.panModParms1.ResumeLayout(false);
    this.panModParms1.PerformLayout();
    this.dockOpParms.ResumeLayout(false);
    this.panOpParms8.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.panel4.PerformLayout();
    this.rashifrMenu.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.panel6.PerformLayout();
    this.panel7.ResumeLayout(false);
    this.panel7.PerformLayout();
    this.opPopMenu.ResumeLayout(false);
    this.editSelObject.Properties.EndInit();
    this.panOpParmsE.ResumeLayout(false);
    this.panOpParmsE.PerformLayout();
    this.panel11.ResumeLayout(false);
    this.groupBox14.ResumeLayout(false);
    this.groupBox13.ResumeLayout(false);
    this.groupBox10.ResumeLayout(false);
    this.groupBox10.PerformLayout();
    this.beDocScript.Properties.EndInit();
    this.beTypeForDoc.Properties.EndInit();
    this.panOpParmsD.ResumeLayout(false);
    this.panOpParmsD.PerformLayout();
    this.beCompObjType.Properties.EndInit();
    this.beComplectType.Properties.EndInit();
    this.panOpParms7.ResumeLayout(false);
    this.edObjType.Properties.EndInit();
    this.panOpParms6.ResumeLayout(false);
    this.panOpParms6.PerformLayout();
    this.groupBox4.ResumeLayout(false);
    this.groupBox4.PerformLayout();
    this.gbIdent.ResumeLayout(false);
    this.gbIdent.PerformLayout();
    this.buttonEdit3.Properties.EndInit();
    this.edSelTemplate.Properties.EndInit();
    this.panOpParms5.ResumeLayout(false);
    this.panOpParms5.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.buttonEdit2.Properties.EndInit();
    this.textBox1.Properties.EndInit();
    this.editAddAttr.Properties.EndInit();
    this.edSaveNewID.Properties.EndInit();
    this.panOpParms4.ResumeLayout(false);
    this.tabControl2.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.gbSetValue.ResumeLayout(false);
    this.gbAttr.ResumeLayout(false);
    this.tabPage2.ResumeLayout(false);
    this.gbFont.ResumeLayout(false);
    this.gbFont.PerformLayout();
    this.seFontSize.Properties.EndInit();
    this.beFontName.Properties.EndInit();
    this.panel12.ResumeLayout(false);
    this.panel12.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.buttonEdit1.Properties.EndInit();
    this.textId.Properties.EndInit();
    this.edAddAttr.Properties.EndInit();
    this.panOpParms3.ResumeLayout(false);
    this.tabCon.ResumeLayout(false);
    this.tabFormula.ResumeLayout(false);
    this.gbSetParms.ResumeLayout(false);
    this.tabTable.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panSetArray.ResumeLayout(false);
    this.panSetArray.PerformLayout();
    this.groupBox5.ResumeLayout(false);
    this.groupBox5.PerformLayout();
    this.panOpParmsTI.ResumeLayout(false);
    this.panOpParmsTI.PerformLayout();
    this.beSourceType.Properties.EndInit();
    this.beCreatingDocType.Properties.EndInit();
    this.panOpParmsStyleB.ResumeLayout(false);
    this.panOpParmsStyleB.PerformLayout();
    this.panOpParmsStyleC.ResumeLayout(false);
    this.panOpParmsStyleC.PerformLayout();
    this.panOpParms2.ResumeLayout(false);
    this.panOpParms2.PerformLayout();
    this.btnObjLink.Properties.EndInit();
    this.panOpParms9.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.panOpParms1.ResumeLayout(false);
    this.tabControl1.ResumeLayout(false);
    this.tabObjMain.ResumeLayout(false);
    this.tabObjMain.PerformLayout();
    this.btnEdExcerpt.Properties.EndInit();
    this.elMoveMenu.ResumeLayout(false);
    this.tabObjSecond.ResumeLayout(false);
    this.tabObjSecond.PerformLayout();
    this.groupBox8.ResumeLayout(false);
    this.groupBox8.PerformLayout();
    this.groupBox7.ResumeLayout(false);
    this.groupBox7.PerformLayout();
    this.tabObjTable.ResumeLayout(false);
    this.tabObjTable.PerformLayout();
    this.groupBox6.ResumeLayout(false);
    this.groupBox6.PerformLayout();
    this.beCompareFunc.Properties.EndInit();
    this.gbComposition.ResumeLayout(false);
    this.gbComposition.PerformLayout();
    this.gbIspoln.ResumeLayout(false);
    this.gbIspoln.PerformLayout();
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.panOpParmsA.ResumeLayout(false);
    this.panOpParmsA.PerformLayout();
    this.beUserProc.Properties.EndInit();
    this.panOpParmsC.ResumeLayout(false);
    this.panOpParmsC.PerformLayout();
    this.beNewList.Properties.EndInit();
    this.panOpParmsB.ResumeLayout(false);
    this.panOpParmsB.PerformLayout();
    this.gbVisZamens.ResumeLayout(false);
    this.gbVisZamens.PerformLayout();
    this.edVersionRule.Properties.EndInit();
    this.panGlobalTableFolder.ResumeLayout(false);
    this.tabGlobTable.ResumeLayout(false);
    this.tabPage5.ResumeLayout(false);
    this.tabPage5.PerformLayout();
    this.beReplaceObjType.Properties.EndInit();
    this.tabPage3.ResumeLayout(false);
    this.tabPage3.PerformLayout();
    this.gbGRIsps.ResumeLayout(false);
    this.gbGRIsps.PerformLayout();
    this.beGlobExcerpt.Properties.EndInit();
    this.tabPage8.ResumeLayout(false);
    this.gbSostav1.ResumeLayout(false);
    this.gbSostav1.PerformLayout();
    this.panOpGlobalType.ResumeLayout(false);
    this.tabControlGT.ResumeLayout(false);
    this.tabGTParms.ResumeLayout(false);
    this.tabGTParms.PerformLayout();
    this.gbIspForGT.ResumeLayout(false);
    this.gbIspForGT.PerformLayout();
    this.tabGTObjects.ResumeLayout(false);
    this.tabGTObjects.PerformLayout();
    this.beGTExcerpt.Properties.EndInit();
    this.tabGTOther.ResumeLayout(false);
    this.gbSostav2.ResumeLayout(false);
    this.gbSostav2.PerformLayout();
    this.bottomDock.ResumeLayout(false);
    this.dockDesc.ResumeLayout(false);
    this.dockDesc.PerformLayout();
    this.treePopMenu.ResumeLayout(false);
    this.groupMenu.ResumeLayout(false);
    this.sortMenu.ResumeLayout(false);
    this.panelApply.ResumeLayout(false);
    this.panelApply.PerformLayout();
    this.tree.EndInit();
    this.repositoryItemTextEdit2.EndInit();
    this.repositoryItemTextEdit1.EndInit();
    this.panel13.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void UpdateOperDescr()
  {
  }

  private string GetModStr(int modTag, out int operForm)
  {
    string modStr = "";
    operForm = -1;
    switch (modTag)
    {
      case 0:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_441");
        operForm = 0;
        break;
      case 1:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_442");
        operForm = 0;
        break;
      case 2:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_443");
        operForm = 0;
        break;
      case 3:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_444");
        operForm = 0;
        break;
      case 4:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_445");
        operForm = 1;
        break;
      case 5:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_446");
        operForm = 3;
        break;
      case 6:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_447");
        operForm = 2;
        break;
      case 7:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_448");
        operForm = 2;
        break;
      case 8:
        modStr = LocalizationHolder.rm.GetString("Expert.Editor_449");
        operForm = 2;
        break;
    }
    return modStr;
  }

  private string GetOpStr(int opTag, int operForm)
  {
    string opStr = "";
    switch (opTag)
    {
      case 0:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_450");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_451");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_452");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_453");
            break;
        }
        break;
      case 9:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_454");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_455");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_456");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_457");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_458");
            break;
        }
        break;
      case 10:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_459");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_460");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_461");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_462");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_463");
            break;
        }
        break;
      case 11:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_464");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_465");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_466");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_467");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_468");
            break;
        }
        break;
      case 12:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_469");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_470");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_471");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_472");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_473");
            break;
        }
        break;
      case 13:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_474");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_475");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_476");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_477");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_478");
            break;
        }
        break;
      case 14:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_479");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_480");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_481");
            break;
          case 3:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_482");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_483");
            break;
        }
        break;
      case 15:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_484");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_485");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_486");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_487");
            break;
        }
        break;
      case 16 /*0x10*/:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_488");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_489");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_490");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_491");
            break;
        }
        break;
      case 17:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_492");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_493");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_494");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_495");
            break;
        }
        break;
      case 18:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_496");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_497");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_498");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_499");
            break;
        }
        break;
      case 19:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_500");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_501");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_502");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_503");
            break;
        }
        break;
      case 20:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_504");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_505");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_506");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_507");
            break;
        }
        break;
      case 21:
        switch (operForm)
        {
          case 0:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_508");
            break;
          case 1:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_509");
            break;
          case 2:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_510");
            break;
          default:
            opStr = LocalizationHolder.rm.GetString("Expert.Editor_511");
            break;
        }
        break;
      case 24:
        opStr = LocalizationHolder.rm.GetString("Expert.Editor_512");
        break;
      case 39:
        opStr = LocalizationHolder.rm.GetString("Expert.Editor_513");
        break;
      case 66:
        opStr = "TEST TEST";
        break;
    }
    return opStr;
  }

  private string GetObjectsStr(int operForm)
  {
    switch (operForm)
    {
      case 0:
        return LocalizationHolder.rm.GetString("Expert.Editor_514");
      case 1:
        return LocalizationHolder.rm.GetString("Expert.Editor_515");
      case 2:
        return LocalizationHolder.rm.GetString("Expert.Editor_516");
      case 3:
        return LocalizationHolder.rm.GetString("Expert.Editor_517");
      default:
        return "";
    }
  }

  private string GetWhichStr(int operForm)
  {
    switch (operForm)
    {
      case 0:
        return LocalizationHolder.rm.GetString("Expert.Editor_518");
      case 1:
      case 2:
        return LocalizationHolder.rm.GetString("Expert.Editor_519");
      default:
        return "";
    }
  }

  private string GetPreModStr(int modTag, int opTag, OpParm ops, out int operForm)
  {
    operForm = -1;
    string modStr;
    if (modTag == 2 || modTag == 3)
    {
      modStr = LocalizationHolder.rm.GetString("Expert.Editor_520");
      operForm = 0;
    }
    else
      modStr = this.GetModStr(modTag, out operForm);
    string preModStr;
    switch (opTag)
    {
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
        preModStr = !this.SelectingObjects(ops) ? modStr + this.GetOpStr(opTag, operForm) : modStr + this.GetOpStr(0, operForm);
        break;
      default:
        string objectsStr = this.GetObjectsStr(operForm);
        switch (operForm)
        {
          case 0:
          case 1:
          case 2:
          case 3:
            preModStr = modStr + objectsStr;
            break;
          default:
            preModStr = LocalizationHolder.rm.GetString("Expert.Editor_521");
            break;
        }
        break;
    }
    return preModStr;
  }

  private string GetModOperStr()
  {
    int operForm = -1;
    string str1 = "";
    string str2 = "";
    if (this.modPressed != null)
      str1 = this.GetModStr(Convert.ToInt32(this.modPressed.Tag), out operForm);
    if (this.opPressed != null)
      str2 = this.GetOpStr(Convert.ToInt32(this.opPressed.Tag), operForm);
    return str1 + str2;
  }

  private bool SelectingObjects(OpParm ops)
  {
    if (ops == null || !(ops is OpParmObject))
      return false;
    return (ops as OpParmObject).excerptID != 0L || (ops as OpParmObject).cond != null || (ops as OpParmObject).linkTypeTexts != null || (ops as OpParmObject).objTypeTexts != null;
  }

  private string GetNodeDescr(Intermech.Expert.NodeData data)
  {
    string nodeDescr = "";
    string str1 = LocalizationHolder.rm.GetString("Expert.Editor_522");
    string str2 = LocalizationHolder.rm.GetString("Expert.Editor_523");
    string str3 = LocalizationHolder.rm.GetString("Expert.Editor_524");
    int operForm = -1;
    if (data.mods != null)
    {
      switch (data.modTag)
      {
        case 0:
        case 1:
          nodeDescr = this.GetPreModStr(data.modTag, data.opTag, data.ops, out operForm);
          if ((data.mods as ModParmFormula).tf != null)
          {
            nodeDescr = $"{nodeDescr}{this.GetWhichStr(operForm)}{(data.mods as ModParmFormula).tf.Text}, ";
            break;
          }
          break;
        case 2:
        case 3:
          if ((data.mods as ModParmFormula).tf != null)
          {
            string preModStr = this.GetPreModStr(data.modTag, data.opTag, data.ops, out operForm);
            nodeDescr = $"{(data.modTag != 2 ? preModStr + LocalizationHolder.rm.GetString("Expert.Editor_526") : preModStr + LocalizationHolder.rm.GetString("Expert.Editor_525"))}{(data.mods as ModParmFormula).tf.Text}, ";
            break;
          }
          nodeDescr = str1 + str3 + LocalizationHolder.rm.GetString("Expert.Editor_527");
          break;
        case 4:
        case 5:
          nodeDescr = this.GetPreModStr(data.modTag, data.opTag, data.ops, out operForm);
          if ((data.mods as ModParmFormula).tf != null)
          {
            nodeDescr = $"{nodeDescr}{(data.modTag == 4 ? this.GetWhichStr(operForm) : " ")}{(data.mods as ModParmFormula).tf.Text}, ";
            break;
          }
          if (data.modTag == 5 || !this.SelectingObjects(data.ops))
          {
            nodeDescr = str1 + str3 + LocalizationHolder.rm.GetString("Expert.Editor_528");
            break;
          }
          break;
        case 6:
          ModParmLoop mods1 = data.mods as ModParmLoop;
          if (mods1.whileLoop)
          {
            nodeDescr = mods1.tf != null || data.ops is OpParmObject ? LocalizationHolder.rm.GetString("Expert.Editor_530") : LocalizationHolder.rm.GetString("Expert.Editor_529");
            if (data.ops is OpParmObject)
            {
              string str4 = nodeDescr + LocalizationHolder.rm.GetString("Expert.Editor_531");
              nodeDescr = !this.SelectingObjects(data.ops) ? str4 + this.GetOpStr(data.opTag, 0) : str4 + LocalizationHolder.rm.GetString("Expert.Editor_532");
              if (mods1.tf != null)
              {
                nodeDescr = nodeDescr + LocalizationHolder.rm.GetString("Expert.Editor_533") + mods1.tf.Text;
                break;
              }
              break;
            }
            if (mods1.tf != null)
            {
              nodeDescr = nodeDescr + LocalizationHolder.rm.GetString("Expert.Editor_534") + mods1.tf.Text;
              break;
            }
            break;
          }
          if (mods1.tf != null)
          {
            nodeDescr = LocalizationHolder.rm.GetString("Expert.Editor_535") + mods1.attrText + LocalizationHolder.rm.GetString("Expert.Editor_536") + Convert.ToString(mods1.startWith) + LocalizationHolder.rm.GetString("Expert.Editor_537") + mods1.tf.Text;
            break;
          }
          break;
        case 7:
        case 8:
          ModParmSort mods2 = data.mods as ModParmSort;
          bool flag = this.SelectingObjects(data.ops);
          string str5;
          if (mods2.sortAttrs != null && mods2.sortAttrs.Count > 0)
          {
            string str6 = LocalizationHolder.rm.GetString("Expert.Editor_538") + (flag ? LocalizationHolder.rm.GetString("Expert.Editor_539") : LocalizationHolder.rm.GetString("Expert.Editor_540")) + LocalizationHolder.rm.GetString("Expert.Editor_541");
            for (int index = 0; index < mods2.sortAttrTexts.Count; ++index)
            {
              if (index > 0)
                str6 += ", ";
              str6 += mods2.sortAttrTexts[index];
            }
            string str7 = str6 + LocalizationHolder.rm.GetString("Expert.Editor_225");
            str5 = data.modTag != 7 ? str7 + LocalizationHolder.rm.GetString("Expert.Editor_227") : str7 + LocalizationHolder.rm.GetString("Expert.Editor_226");
          }
          else
          {
            if (data.modTag == 7)
            {
              nodeDescr = str1 + str3 + LocalizationHolder.rm.GetString("Expert.Editor_228");
              break;
            }
            str5 = LocalizationHolder.rm.GetString("Expert.Editor_229");
          }
          nodeDescr = !(data.ops is OpParmObject) || mods2.sortAttrs != null && mods2.sortAttrs.Count > 0 ? str5 + this.GetObjectsStr(data.modTag == 7 ? 0 : 3) : (!flag ? str5 + this.GetOpStr(data.opTag, data.modTag == 7 ? 0 : 3) : str5 + (data.modTag == 7 ? LocalizationHolder.rm.GetString("Expert.Editor_230") : LocalizationHolder.rm.GetString("Expert.Editor_231")));
          if (mods2.groupAttrs != null && mods2.groupAttrs.Count > 0 && data.modTag == 8)
          {
            string str8 = nodeDescr + LocalizationHolder.rm.GetString("Expert.Editor_232");
            for (int index = 0; index < mods2.groupAttrTexts.Count; ++index)
            {
              if (index > 0)
                str8 += ", ";
              str8 += mods2.groupAttrTexts[index];
            }
            nodeDescr = str8 + "]";
            break;
          }
          if (data.modTag == 8)
          {
            nodeDescr = str1 + str3 + LocalizationHolder.rm.GetString("Expert.Editor_233");
            break;
          }
          break;
      }
    }
    if (data.ops != null)
    {
      string str9 = "";
      switch (data.opTag)
      {
        case 9:
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
          OpParmObject ops1 = data.ops as OpParmObject;
          if (data.mods != null && data.mods is ModParmFormula && (data.mods as ModParmFormula).saveContext)
            nodeDescr = $"{LocalizationHolder.rm.GetString("Expert.Editor_550")};\n\r{nodeDescr}";
          if (!ops1.NoSearch)
          {
            str9 = LocalizationHolder.rm.GetString("Expert.Editor_234") + this.GetOpStr(data.opTag, 1);
            if (ops1.excerptID != 0L || ops1.cond != null || ops1.objTypeTexts != null || ops1.linkTypeTexts != null)
            {
              if (ops1.objTypeTexts != null)
              {
                str9 += LocalizationHolder.rm.GetString("Expert.Editor_235");
                for (int index = 0; index < ops1.objTypeTexts.Count; ++index)
                {
                  string str10 = str9 + (string) ops1.objTypeTexts[index];
                  str9 = index >= ops1.objTypeTexts.Count - 1 ? str10 + "]" : str10 + ", ";
                }
              }
              if (ops1.linkTypeTexts != null)
              {
                str9 += LocalizationHolder.rm.GetString("Expert.Editor_236");
                for (int index = 0; index < ops1.linkTypeTexts.Count; ++index)
                {
                  string str11 = str9 + (string) ops1.linkTypeTexts[index];
                  str9 = index >= ops1.linkTypeTexts.Count - 1 ? str11 + "]" : str11 + ", ";
                }
              }
              if (ops1.excerptID != 0L)
                str9 = $"{str9}{LocalizationHolder.rm.GetString("Expert.Editor_237")}{ops1.excerptName}\"";
              if (ops1.cond != null && ops1.cond.Count > 0)
                str9 = str9 + LocalizationHolder.rm.GetString("Expert.Editor_238") + ops1.cond.TrueText();
              if (ops1.useGlobal == GlobalData.globalMult)
                str9 += LocalizationHolder.rm.GetString("Expert.Editor_239");
              if (ops1.Dups)
                str9 += LocalizationHolder.rm.GetString("Expert.Editor_240");
              if (ops1.useGlobal == GlobalData.globalAdd)
                str9 += LocalizationHolder.rm.GetString("Expert.Editor_241");
            }
          }
          else if (ops1.useGlobal == GlobalData.globalAdd)
            str9 = LocalizationHolder.rm.GetString("Expert.Editor_242");
          if (ops1.useGlobal != GlobalData.globalNone && ops1.filter != null && ops1.filter.Count > 0)
            str9 = str9 + LocalizationHolder.rm.GetString("Expert.Editor_243") + ops1.filter.TrueText();
          string str12 = str9 + "; ";
          if (ops1.dataAttrGUIDs != null && ops1.dataAttrGUIDs.Count > 0)
          {
            string str13 = $"{str12}\n\r\n\r{LocalizationHolder.rm.GetString("Expert.Editor_244")}";
            for (int index = 0; index < ops1.dataAttrTexts.Count; ++index)
            {
              if (index > 0)
                str13 += ", ";
              str13 += ops1.dataAttrTexts[index];
            }
            str12 = str13 + "]" + LocalizationHolder.rm.GetString("Expert.Editor_245");
          }
          if (ops1.saveGlobal != GlobalSave.saveNone)
          {
            switch (ops1.saveGlobal)
            {
              case GlobalSave.saveClear:
                str12 = $"{str12}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_248")}";
                break;
              case GlobalSave.saveAdd:
                str12 = $"{str12}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_246")}";
                break;
              case GlobalSave.saveSet:
                str12 = $"{str12}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_247")}";
                break;
            }
            if (ops1.saveRels)
              str12 += LocalizationHolder.rm.GetString("Expert.Editor_547");
          }
          if (str12 != "")
          {
            nodeDescr = $"{str12}\n\r{nodeDescr}";
            break;
          }
          break;
        case 15:
        case 39:
          OpParmCond ops2 = data.ops as OpParmCond;
          if (ops2.cond != null)
            nodeDescr = LocalizationHolder.rm.GetString("Expert.Editor_249") + ops2.cond.Text + (nodeDescr != "" ? ", \n\r" + nodeDescr : ",");
          nodeDescr = data.opTag != 15 ? $"{nodeDescr}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_251")}" : $"{nodeDescr}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_250")}";
          break;
        case 16 /*0x10*/:
        case 17:
          OpParmCond ops3 = data.ops as OpParmCond;
          if (ops3.cond != null)
            nodeDescr = LocalizationHolder.rm.GetString("Expert.Editor_252") + ops3.cond.Text + (nodeDescr != "" ? ", \n\r" + nodeDescr : ",");
          if (data.opTag == 16 /*0x10*/)
          {
            if (ops3.refAttrName != "")
              nodeDescr += string.Format(LocalizationHolder.rm.GetString("Expert.Editor_546"), (object) ops3.refAttrName);
            nodeDescr = $"{nodeDescr}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_253")}";
            break;
          }
          nodeDescr = $"{nodeDescr}\n\r{LocalizationHolder.rm.GetString("Expert.Editor_254")}";
          break;
        case 18:
          OpParmSetting ops4 = data.ops as OpParmSetting;
          string str14;
          if (ops4.attrGUID == "")
            str14 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_255");
          else if (ops4.tf == null && ops4.setKind != ExpertSettingKind.setKindNumber && ops4.setKind != ExpertSettingKind.setKindByTable)
          {
            str14 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_256");
          }
          else
          {
            string str15 = LocalizationHolder.rm.GetString("Expert.Editor_257") + ops4.objTypeText;
            if (ops4.objTypeText != "")
              str15 += ".";
            str14 = $"{str15}\"{ops4.attrText}\" ";
            if (ops4.tf != null)
            {
              switch (ops4.setKind)
              {
                case ExpertSettingKind.setKindValue:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_258") + ops4.tf.Text;
                  break;
                case ExpertSettingKind.setKindByTable:
                  str14 += LocalizationHolder.rm.GetString("Expert.Editor_266");
                  break;
                case ExpertSettingKind.setKindSum:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_259") + ops4.tf.Text;
                  break;
                case ExpertSettingKind.setKindAverage:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_260") + ops4.tf.Text;
                  break;
                case ExpertSettingKind.setKindNumber:
                  str14 += LocalizationHolder.rm.GetString("Expert.Editor_261");
                  break;
                case ExpertSettingKind.setKindMinimum:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_262") + ops4.tf.Text;
                  break;
                case ExpertSettingKind.setKindMaximum:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_263") + ops4.tf.Text;
                  break;
                case ExpertSettingKind.setKindList:
                  str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_264") + ops4.tf.Text;
                  if (this.comboDivider.Text != "")
                  {
                    str14 = str14 + LocalizationHolder.rm.GetString("Expert.Editor_265") + this.comboDivider.Text;
                    break;
                  }
                  break;
              }
            }
          }
          nodeDescr = !(nodeDescr == "") ? $"{nodeDescr}\n\r{str14}" : str14;
          break;
        case 19:
          if (!(data.ops is OpParmFillFld ops5))
            return "";
          string str16;
          if (ops5.FldID == "")
            str16 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_267");
          else if (ops5.attrGUID == "" && ops5.tf == null)
          {
            str16 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_268");
          }
          else
          {
            string str17 = LocalizationHolder.rm.GetString("Expert.Editor_269");
            if (ops5.FldName != "")
              str17 = $"{str17}\"{ops5.FldName}\" ";
            string str18 = $"{str17}[{ops5.FldID}] ";
            if (ops5.AddAttrText != "")
              str18 = $"{str18}{LocalizationHolder.rm.GetString("Expert.Editor_270")}{ops5.AddAttrText}\" ";
            if (ops5.fillFormula() && ops5.tf != null)
            {
              str16 = $"{str18}{LocalizationHolder.rm.GetString("Expert.Editor_271")}{ops5.tf.Text} ";
            }
            else
            {
              if (ops5.fillAttr())
                str18 += LocalizationHolder.rm.GetString("Expert.Editor_272");
              if (ops5.objTypeText != "")
                str18 = $"{str18}{ops5.objTypeText}.";
              str16 = $"{str18}{ops5.attrText}\" ";
            }
          }
          nodeDescr = !(nodeDescr == "") ? $"{nodeDescr}\n\r{str16}" : str16;
          break;
        case 20:
          OpParmCreateFld ops6 = data.ops as OpParmCreateFld;
          string str19;
          if (ops6.FldID == "")
          {
            str19 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_273");
          }
          else
          {
            string str20 = LocalizationHolder.rm.GetString("Expert.Editor_274");
            if (ops6.FldName != "")
              str20 = $"{str20}\"{ops6.FldName}\" ";
            str19 = $"{str20}[{ops6.FldID}]";
            if (ops6.AddAttrText != "")
              str19 = $"{str19}{LocalizationHolder.rm.GetString("Expert.Editor_275")}{ops6.AddAttrText}\"";
            if (ops6.SaveIDAttrGUID != "")
              str19 = $"{str19}{LocalizationHolder.rm.GetString("Expert.Editor_276")}{ops6.SaveIDAttrText}\"";
            if (ops6.makeNewCurrent)
              str19 = !ops6.fillChildren ? str19 + LocalizationHolder.rm.GetString("Expert.Editor_278") : str19 + LocalizationHolder.rm.GetString("Expert.Editor_277");
            else if (ops6.fillChildren)
              str19 += LocalizationHolder.rm.GetString("Expert.Editor_279");
          }
          nodeDescr = !(nodeDescr == "") ? $"{nodeDescr}\n\r{str19}" : str19;
          break;
        case 21:
          OpParmSelFld ops7 = data.ops as OpParmSelFld;
          string str21;
          if (ops7.tf == null && ops7.FldId == "")
          {
            str21 = str1 + str2 + LocalizationHolder.rm.GetString("Expert.Editor_280");
          }
          else
          {
            string str22 = LocalizationHolder.rm.GetString("Expert.Editor_281");
            if (ops7.FldName != "")
              str22 = $"{str22}\"{ops7.FldName}\" ";
            str21 = ops7.tf == null ? $"{str22}[{ops7.FldId}]" : $"{str22}[{ops7.tf.Text}]";
          }
          nodeDescr = !(nodeDescr == "") ? $"{nodeDescr}\n\r{str21}" : str21;
          break;
        case 24:
        case 51:
          OpParmType ops8 = data.ops as OpParmType;
          nodeDescr = $"{(ops8.cond == null ? "" : $"{LocalizationHolder.rm.GetString("Expert.Editor_282")}{ops8.cond.Text},\n")}{LocalizationHolder.rm.GetString("Expert.Editor_283")}{ops8.objTypeText}\"";
          break;
        case 25:
        case 26:
        case 27:
          OpParmExpObj ops9 = data.ops as OpParmExpObj;
          string str23 = (ops9.cond == null ? "" : $"{LocalizationHolder.rm.GetString("Expert.Editor_284")}{ops9.cond.Text},\n") + LocalizationHolder.rm.GetString("Expert.Editor_285");
          switch (data.opTag)
          {
            case 25:
              str23 += LocalizationHolder.rm.GetString("Expert.Editor_286");
              break;
            case 26:
              str23 += LocalizationHolder.rm.GetString("Expert.Editor_287");
              break;
            case 27:
              str23 += LocalizationHolder.rm.GetString("Expert.Editor_288");
              break;
          }
          nodeDescr = $"{str23}\"{ops9.objTypeText}\"";
          break;
        case 43:
          OpParmUserProc ops10 = data.ops as OpParmUserProc;
          switch (ops10.type)
          {
            case ExpertCalling.callProc:
              nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_289")}{ops10.procName}\"";
              break;
            case ExpertCalling.callUserProc:
              nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_290")}{ops10.procName}\"";
              break;
            case ExpertCalling.callScript:
              nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_291")}{ops10.procName}\"";
              break;
            case ExpertCalling.callScenario:
              nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_620")}{ops10.procName}\" - {LocalizationHolder.rm.GetString("Expert.Editor_621")} {(object) ops10.parm1}({(object) ops10.parm2})";
              break;
          }
          break;
        case 44:
          OpParmVersionRule ops11 = data.ops as OpParmVersionRule;
          nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_292")}{ops11.ruleCapt}\"";
          break;
        case 49:
          OpCreateDoc ops12 = data.ops as OpCreateDoc;
          if (ops12.cond != null)
            nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_249")} {ops12.cond.ToString()}\n\r";
          nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_559")} \"{ops12.scriptText}\"\n\r{LocalizationHolder.rm.GetString("Expert.Editor_560")} \"{ops12.objTypeText}\"";
          break;
        case 50:
          OpCreateComplect ops13 = data.ops as OpCreateComplect;
          if (ops13.cond != null)
            nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_249")} {ops13.cond.ToString()}\n\r";
          nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_561")} \"{ops13.objTypeText}\"";
          break;
        case 53:
          OpParm ops14 = data.ops;
          nodeDescr += LocalizationHolder.rm.GetString("Expert.Editor_627");
          break;
        case 54:
          OpParmGlobForType ops15 = data.ops as OpParmGlobForType;
          nodeDescr = $"{nodeDescr}{LocalizationHolder.rm.GetString("Expert.Editor_628")}{ops15.GetObjTypesStr()})";
          break;
        case 66:
          OpParmTiLink ops16 = data.ops as OpParmTiLink;
          nodeDescr += string.Format(LocalizationHolder.rm.GetString("Expert.Editor_690"), (object) ops16.TiDocTypeName, (object) ops16.NewDocTypeName);
          break;
      }
    }
    return nodeDescr;
  }

  private void TBar_ButtonClick(object sender, EventArgs e)
  {
    PanelButton panelButton = (PanelButton) sender;
    if (panelButton == null)
      return;
    switch (Convert.ToInt32(panelButton.Tag))
    {
      case 0:
      case 1:
      case 2:
      case 3:
      case 4:
      case 5:
      case 6:
      case 7:
      case 8:
      case 68:
        if (this.opPressed != null && this.opPressed == this.TypeBtn)
        {
          this.modPressed = (PanelButton) null;
          break;
        }
        if (this.modPressed != null && this.modPressed == panelButton)
        {
          this.modPressed.Checked = false;
          this.modPressed = (PanelButton) null;
          break;
        }
        if (this.modPressed != null)
          this.modPressed.Checked = false;
        panelButton.Checked = true;
        this.modPressed = panelButton;
        break;
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
      case 15:
      case 16 /*0x10*/:
      case 17:
      case 18:
      case 19:
      case 20:
      case 21:
      case 32 /*0x20*/:
      case 40:
      case 43:
      case 44:
        if (this.opPressed != null && this.opPressed != panelButton)
        {
          this.opPressed.Checked = false;
          this.opPressed = (PanelButton) null;
        }
        panelButton.Checked = true;
        this.opPressed = panelButton;
        break;
      case 22:
        this.SaveToXML("C:\\script.xml");
        return;
      case 23:
        this.LoadFromXML("C:\\Script.xml");
        return;
      case 24:
      case 39:
      case 51:
      case 53:
      case 54:
        if (this.modPressed != null)
        {
          this.modPressed.Checked = false;
          this.modPressed = (PanelButton) null;
        }
        if (this.opPressed != null && this.opPressed != panelButton)
          this.opPressed.Checked = false;
        if (!panelButton.Checked)
          panelButton.Checked = true;
        this.opPressed = panelButton;
        break;
      case 25:
      case 26:
      case 27:
        if (this.modPressed != null)
        {
          this.modPressed.Checked = false;
          this.modPressed = (PanelButton) null;
        }
        if (this.opPressed != null && this.opPressed != panelButton)
          this.opPressed.Checked = false;
        if (!panelButton.Checked)
          panelButton.Checked = true;
        this.opPressed = panelButton;
        break;
      case 49:
      case 50:
      case 57:
      case 63 /*0x3F*/:
      case 64 /*0x40*/:
      case 65:
      case 66:
      case 67:
        if (this.modPressed != null)
        {
          this.modPressed.Checked = false;
          this.modPressed = (PanelButton) null;
        }
        if (this.opPressed != null && this.opPressed != panelButton)
          this.opPressed.Checked = false;
        if (!panelButton.Checked)
          panelButton.Checked = true;
        this.opPressed = panelButton;
        break;
    }
    this.UpdateOperDescr();
  }

  internal int selNodeID() => this.tree.Selection.Count == 0 ? -1 : this.tree.Selection[0].Id;

  internal TreeListNode selNode()
  {
    return this.tree.Selection.Count == 0 ? (TreeListNode) null : this.tree.Selection[0];
  }

  private void TBar_Control_ButtonClick(object sender, EventArgs e)
  {
    Component component = (Component) sender;
    int num = -1;
    if (component.GetType() == typeof (MenuButtonItem))
      num = Convert.ToInt32(((ToolbarItemBase) component).Tag);
    if (component.GetType() == typeof (ButtonItem))
      num = Convert.ToInt32(((ToolbarItemBase) component).Tag);
    bool flag = this.tree.Nodes.Count == 0;
    switch (Convert.ToInt32(num))
    {
      case 0:
        if (!this.CheckFocusNode(this.tree.FocusedNode))
          break;
        TreeListNode node1 = this.InsBeforeAfter(true);
        if (node1 == null)
          break;
        Intermech.Expert.NodeData nd1 = new Intermech.Expert.NodeData();
        this.InitParms(nd1, this);
        node1.Tag = (object) nd1;
        this.UpdateNode(node1, ref nd1);
        this.tree.FocusedNode = node1;
        if (flag)
          this.OnChangeNode(node1);
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        if (this.addMenu.ImageIndex == 22)
          break;
        this.addMenu.ImageIndex = 22;
        break;
      case 1:
        if (!this.CheckFocusNode(this.tree.FocusedNode))
          break;
        TreeListNode node2 = this.InsBeforeAfter(false);
        if (node2 == null)
          break;
        Intermech.Expert.NodeData nd2 = new Intermech.Expert.NodeData();
        this.InitParms(nd2, this);
        node2.Tag = (object) nd2;
        this.UpdateNode(node2, ref nd2);
        this.tree.FocusedNode = node2;
        if (flag)
          this.OnChangeNode(node2);
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        if (this.addMenu.ImageIndex == 23)
          break;
        this.addMenu.ImageIndex = 23;
        break;
      case 2:
        if (!this.CheckFocusNode(this.tree.FocusedNode))
          break;
        TreeListNode node3 = this.AddNode();
        if (node3 == null)
          break;
        Intermech.Expert.NodeData nd3 = new Intermech.Expert.NodeData();
        this.InitParms(nd3, this);
        node3.Tag = (object) nd3;
        this.UpdateNode(node3, ref nd3);
        this.tree.FocusedNode = node3;
        if (flag)
          this.OnChangeNode(node3);
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        if (this.addMenu.ImageIndex == 28)
          break;
        this.addMenu.ImageIndex = 28;
        break;
      case 3:
        if (!this.SaveChangedNodeData() || this.tree.FocusedNode == null)
          break;
        this.SetDescrText(this.GetNodeDescr((Intermech.Expert.NodeData) this.tree.FocusedNode.Tag));
        break;
      case 4:
        if (this.clipMenu.ImageIndex != 31 /*0x1F*/)
          this.clipMenu.ImageIndex = 31 /*0x1F*/;
        if (this.tree.FocusedNode == null)
          break;
        this.ClipCopy();
        this.DeleteNode();
        if (this.tree.Nodes.Count == 0)
          this.OnChangeNode(this.tree.FocusedNode);
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        break;
      case 5:
        if (this.clipMenu.ImageIndex != 32 /*0x20*/)
          this.clipMenu.ImageIndex = 32 /*0x20*/;
        if (this.tree.FocusedNode == null)
          break;
        this.ClipCopy();
        break;
      case 6:
        if (this.clipMenu.ImageIndex != 33)
          this.clipMenu.ImageIndex = 33;
        if (!Clipboard.ContainsData(ScriptEdit2.ExpClipFormat))
          break;
        this.ClipPaste();
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        this.EnableButtons();
        this.EnableBtnDocParms();
        if (this.tree.FocusedNode == null)
          break;
        this.OnChangeNode(this.tree.FocusedNode);
        break;
      case 7:
        this.DeleteNode();
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
        if (this.tree.Nodes.Count != 0)
          break;
        this.OnChangeNode(this.tree.FocusedNode);
        break;
      case 8:
        this.ChangeNodeModOp();
        break;
    }
  }

  internal bool LegalModOp(int mod, int op)
  {
    if (op != 15 || mod < 0 || mod == 4 || mod == 5)
      return true;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_293"), LocalizationHolder.rm.GetString("Expert.Editor_294"));
    return false;
  }

  internal TreeListNode AddNode()
  {
    if (this.opPressed == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_295"), LocalizationHolder.rm.GetString("Expert.Editor_296"));
      return (TreeListNode) null;
    }
    int parentNodeId = -1;
    TreeListNode treeListNode = this.selNode();
    if (treeListNode != null)
      parentNodeId = treeListNode.Id;
    int num1 = -1;
    int int32 = Convert.ToInt32(this.opPressed.Tag);
    if (this.modPressed != null)
      num1 = Convert.ToInt32(this.modPressed.Tag);
    if (!this.LegalModOp(num1, int32))
      return (TreeListNode) null;
    this.tree.AppendNode((object) new object[3]
    {
      (object) "",
      (object) "",
      (object) ""
    }, parentNodeId, num1, num1, int32);
    return treeListNode == null ? this.tree.Nodes[this.tree.Nodes.Count - 1] : treeListNode.Nodes[treeListNode.Nodes.Count - 1];
  }

  internal TreeListNode InsBeforeAfter(bool before)
  {
    if (this.opPressed == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_297"), LocalizationHolder.rm.GetString("Expert.Editor_298"));
      return (TreeListNode) null;
    }
    int parentNodeId1 = -1;
    TreeListNode node1 = this.selNode();
    if (node1 != null)
      parentNodeId1 = node1.Id;
    int num1 = -1;
    int int32 = Convert.ToInt32(this.opPressed.Tag);
    if (this.modPressed != null)
      num1 = Convert.ToInt32(this.modPressed.Tag);
    if (!this.LegalModOp(num1, int32))
      return (TreeListNode) null;
    TreeListNode node2 = (TreeListNode) null;
    if (this.tree.Nodes.Count == 0)
    {
      this.tree.AppendNode((object) new object[3]
      {
        (object) "",
        (object) "",
        (object) ""
      }, parentNodeId1, num1, num1, int32);
      node2 = this.tree.Nodes[this.tree.Nodes.Count - 1];
    }
    else if (node1 != null)
    {
      TreeListNode parentNode = node1.ParentNode;
      int parentNodeId2 = -1;
      int num2;
      if (parentNode != null)
      {
        parentNodeId2 = parentNode.Id;
        num2 = parentNode.Nodes.IndexOf(node1);
      }
      else
        num2 = this.tree.Nodes.IndexOf(node1);
      this.tree.AppendNode((object) new object[3]
      {
        (object) "",
        (object) "",
        (object) ""
      }, parentNodeId2, num1, num1, int32);
      node2 = parentNode == null ? this.tree.Nodes[this.tree.Nodes.Count - 1] : parentNode.Nodes[parentNode.Nodes.Count - 1];
      if (num2 >= 0)
        this.tree.SetNodeIndex(node2, before ? num2 : num2 + 1);
    }
    return node2;
  }

  private void tree_GetPreviewText(object sender, GetPreviewTextEventArgs e)
  {
  }

  internal void DeleteNode()
  {
    TreeListNode node = this.selNode();
    if (node == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_299"), LocalizationHolder.rm.GetString("Expert.Editor_300"));
    }
    else
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_301"), LocalizationHolder.rm.GetString("Expert.Editor_302"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
      TreeListNode parentNode = node.ParentNode;
      if (parentNode == null)
        this.tree.Nodes.Remove(node);
      else
        parentNode.Nodes.Remove(node);
      this.scriptChanged = true;
      this.UpdateSaveCancelButtons();
    }
  }

  internal void ChangeNodeModOp()
  {
    TreeListNode node = this.selNode();
    if (node == null)
      return;
    int mod = -1;
    if (this.opPressed == null)
      return;
    int int32 = Convert.ToInt32(this.opPressed.Tag);
    if (this.modPressed != null)
      mod = Convert.ToInt32(this.modPressed.Tag);
    if (!this.LegalModOp(mod, int32))
      return;
    System.Type modNodeType = this.GetModNodeType();
    System.Type opNodeType = this.GetOpNodeType();
    Intermech.Expert.NodeData nodeData = this.data(node);
    if (nodeData.mods != null && modNodeType != nodeData.mods.GetType() || nodeData.ops != null && opNodeType != nodeData.ops.GetType())
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_303"), LocalizationHolder.rm.GetString("Expert.Editor_304"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
      if (nodeData.mods != null)
      {
        if (modNodeType != nodeData.mods.GetType())
        {
          if (modNodeType != (System.Type) null)
            nodeData.mods = (ModParm) Activator.CreateInstance(modNodeType);
        }
        else
          nodeData.mods.Clear();
      }
      else if (modNodeType != (System.Type) null)
        nodeData.mods = (ModParm) Activator.CreateInstance(modNodeType);
      if (nodeData.ops != null && opNodeType != nodeData.ops.GetType())
        nodeData.ops = (OpParm) Activator.CreateInstance(opNodeType);
      this.scriptChanged = true;
      this.UpdateSaveCancelButtons();
    }
    else
    {
      if (modNodeType != (System.Type) null)
      {
        if (nodeData.mods == null)
          nodeData.mods = (ModParm) Activator.CreateInstance(modNodeType);
        else
          nodeData.mods.Clear();
      }
      else
        nodeData.mods = (ModParm) null;
      if (nodeData.ops == null)
        nodeData.ops = (OpParm) Activator.CreateInstance(opNodeType);
      this.scriptChanged = mod != nodeData.modTag || int32 != nodeData.opTag;
      this.UpdateSaveCancelButtons();
    }
    if (this.modPressed == null)
    {
      node.ImageIndex = -1;
      node.SelectImageIndex = -1;
      nodeData.modTag = -1;
    }
    else
    {
      node.ImageIndex = mod;
      node.SelectImageIndex = node.ImageIndex;
      nodeData.modTag = mod;
    }
    node.StateImageIndex = int32;
    nodeData.opTag = int32;
    this.OnChangeNode(node);
  }

  /// <summary>
  /// Check, whether some parms were changed and ask user to save them
  /// (call this prior to jumping to another node)
  /// </summary>
  /// <param name="curNode">Current Tree Node</param>
  /// <returns></returns>
  private bool CheckFocusNode(TreeListNode curNode)
  {
    if (!this.opDataChanged && !this.modDataChanged)
      return true;
    string str = LocalizationHolder.rm.GetString("Expert.Editor_305");
    switch (MessageBox.Show((!this.opDataChanged ? str + LocalizationHolder.rm.GetString("Expert.Editor_308") : (!this.modDataChanged ? str + LocalizationHolder.rm.GetString("Expert.Editor_307") : str + LocalizationHolder.rm.GetString("Expert.Editor_306"))) + LocalizationHolder.rm.GetString("Expert.Editor_309"), LocalizationHolder.rm.GetString("Expert.Editor_310"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
    {
      case DialogResult.Cancel:
        return false;
      case DialogResult.Yes:
        return this.SaveChangedNodeData();
      case DialogResult.No:
        return true;
      default:
        return false;
    }
  }

  private void tree_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
  {
    e.CanFocus = this.CheckFocusNode(e.OldNode);
    this.EnableButtons();
  }

  private void EnableButtons()
  {
    bool flag = this.tree.FocusedNode != null;
    this.cmdInsBefore.Enabled = this.menuInsBefore.Enabled = !this.readOnly;
    this.cmdInsInto.Enabled = this.menuInsInto.Enabled = !this.readOnly & flag;
    this.btnChange.Enabled = this.menuChange.Enabled = !this.readOnly & flag;
    this.btnApply.Enabled = this.menuApply.Enabled = !this.readOnly && (this.opDataChanged || this.modDataChanged);
    this.cmdCut.Enabled = this.menuCut.Enabled = !this.readOnly & flag;
    this.cmdPaste.Enabled = this.menuPaste.Enabled = !this.readOnly && Clipboard.ContainsData(ScriptEdit2.ExpClipFormat);
    this.cmdCopy.Enabled = this.menuCopy.Enabled = flag;
    this.cmdDelete.Enabled = this.menuDelete.Enabled = !this.readOnly & flag;
    this.menuCollapse.Enabled = !this.readOnly & flag;
    this.menuExpand.Enabled = !this.readOnly & flag;
  }

  private void menuItem3_Click(object sender, EventArgs e)
  {
    object sender1 = (object) null;
    if (sender == this.menuInsBefore)
      sender1 = (object) this.cmdInsBefore;
    if (sender == this.menuInsAfter)
      sender1 = (object) this.cmdInsAfter;
    if (sender == this.menuInsInto)
      sender1 = (object) this.cmdInsInto;
    if (sender == this.menuChange)
      sender1 = (object) this.btnChange;
    if (sender == this.menuApply)
      sender1 = (object) this.btnApply;
    if (sender == this.menuCut)
      sender1 = (object) this.cmdCut;
    if (sender == this.menuCopy)
      sender1 = (object) this.cmdCopy;
    if (sender == this.menuPaste)
      sender1 = (object) this.cmdPaste;
    if (sender == this.menuDelete)
      sender1 = (object) this.cmdDelete;
    if (sender1 == null)
      return;
    this.TBar_Control_ButtonClick(sender1, new EventArgs());
  }

  private bool InvokeCommandForObject(long objectID, string Command)
  {
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objectID);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    if (!commandsTable.Contains(Command))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(Command, commandsTable, (System.IServiceProvider) viewServices1);
    return true;
  }

  private void btnTemplate_Click(object sender, EventArgs e)
  {
    this.InvokeCommandForObject(this.templID, "EditDocument");
  }

  private void addMenu_Click(object sender, EventArgs e)
  {
    switch (this.addMenu.ImageIndex)
    {
      case 22:
        this.TBar_Control_ButtonClick((object) this.cmdInsBefore, e);
        break;
      case 23:
        this.TBar_Control_ButtonClick((object) this.cmdInsAfter, e);
        break;
      case 28:
        this.TBar_Control_ButtonClick((object) this.cmdInsInto, e);
        break;
    }
  }

  /// <summary>Select right panel and fill it from ModParmData</summary>
  /// <param name="modTag">Modifier Tag</param>
  internal void UpdateModPanel(int modTag)
  {
    this.lockChanged = true;
    try
    {
      this.ShowModPanel(modTag);
      switch (modTag)
      {
        case 0:
        case 1:
        case 4:
        case 5:
          if (this.modData.tf != null)
            this.ShowFormula(this.modData.tf, this.richTextBox1);
          else
            this.richTextBox1.Clear();
          this.lockChanged = true;
          try
          {
            this.cbSaveContext.Checked = this.modData.startValue != 0;
            this.cbForAllIsps.Checked = this.modData.ForLoop;
            break;
          }
          finally
          {
            this.lockChanged = false;
          }
        case 2:
        case 3:
          if (this.modData.tf != null)
          {
            this.ShowFormula(this.modData.tf, this.richTextBox2);
            switch (this.modData.tf.resType)
            {
              case DataType.Float:
                this.checkFloat.Checked = true;
                break;
              case DataType.Measured:
                this.checkMeasured.Checked = true;
                break;
              case DataType.String:
                this.checkString.Checked = true;
                break;
              case DataType.Date:
                this.checkDate.Checked = true;
                break;
              default:
                this.checkInt.Checked = true;
                break;
            }
          }
          else
          {
            this.richTextBox1.Clear();
            this.checkInt.Checked = true;
            break;
          }
          break;
        case 6:
          this.btnForAttr.Text = this.modData.ForAttrText;
          this.lockChanged = true;
          try
          {
            if (this.modData.startValue == int.MaxValue)
            {
              this.checkMulti.Checked = true;
              this.checkFor.Checked = false;
              this.checkDoWhile.Checked = false;
              this.btnRefAttr.Text = this.modData.sortTexts[0];
              this.richForEnd.Text = "";
            }
            else if (this.modData.ForLoop)
            {
              this.checkFor.Checked = true;
              this.checkMulti.Checked = false;
              this.checkDoWhile.Checked = false;
              this.ShowFormula(this.modData.tf, this.richForEnd);
              this.btnForAttr.Text = this.modData.ForAttrText;
              this.spinEdit1.Value = (Decimal) this.modData.startValue;
            }
            else
            {
              this.checkDoWhile.Checked = true;
              this.checkMulti.Checked = false;
              this.checkFor.Checked = false;
              this.ShowFormula(this.modData.tf, this.richWhileCond);
              this.richForEnd.Text = "";
            }
            this.EnableDisable(this.panModParms5);
            break;
          }
          finally
          {
            this.lockChanged = false;
          }
        case 7:
          TreeNode node1 = this.tvAttrs.Nodes[0];
          TreeNode node2 = this.tvAttrs.Nodes[1];
          this.lockChanged = true;
          try
          {
            node1.Nodes.Clear();
            node2.Nodes.Clear();
            for (int index = 0; index < this.modData.sortTexts.Count; ++index)
            {
              TreeNode treeNode = !this.modData.sortChecks[index] ? node1.Nodes.Add(this.modData.sortTexts[index]) : node2.Nodes.Add(this.modData.sortTexts[index]);
              treeNode.ImageIndex = 61;
              treeNode.SelectedImageIndex = 61;
            }
            this.cbInbSort.Checked = this.modData.ForLoop;
          }
          finally
          {
            this.lockChanged = false;
          }
          this.tvAttrs.ExpandAll();
          this.EnableDisable(this.panModParms3);
          break;
        case 8:
          TreeNode node3 = this.tvSortGroup.Nodes[0].Nodes[0];
          TreeNode node4 = this.tvSortGroup.Nodes[0].Nodes[1];
          this.lockChanged = true;
          try
          {
            node3.Nodes.Clear();
            node4.Nodes.Clear();
            for (int index = 0; index < this.modData.sortTexts.Count; ++index)
            {
              TreeNode treeNode = !this.modData.sortChecks[index] ? node3.Nodes.Add(this.modData.sortTexts[index]) : node4.Nodes.Add(this.modData.sortTexts[index]);
              treeNode.ImageIndex = 32 /*0x20*/;
              treeNode.SelectedImageIndex = 32 /*0x20*/;
            }
          }
          finally
          {
            this.lockChanged = false;
          }
          TreeNode node5 = this.tvSortGroup.Nodes[1].Nodes[0];
          TreeNode node6 = this.tvSortGroup.Nodes[1].Nodes[1];
          this.lockChanged = true;
          try
          {
            node5.Nodes.Clear();
            node6.Nodes.Clear();
            for (int index = 0; index < this.modData.groupTexts.Count; ++index)
            {
              TreeNode treeNode = !this.modData.groupChecks[index] ? node5.Nodes.Add(this.modData.groupTexts[index]) : node6.Nodes.Add(this.modData.groupTexts[index]);
              treeNode.ImageIndex = 32 /*0x20*/;
              treeNode.SelectedImageIndex = 32 /*0x20*/;
            }
          }
          finally
          {
            this.lockChanged = false;
          }
          this.tvSortGroup.ExpandAll();
          break;
        case 68:
          this.rbFirstVersion.Checked = !this.modData.ForLoop;
          this.rbAllVersions.Checked = this.modData.ForLoop;
          this.cbSortVersions.SelectedIndex = this.modData.startValue;
          this.cbVerDescending.Checked = this.modData.Bool1;
          this.ShowFormula(this.modData.tf, this.richVerCond);
          break;
      }
      this.modDataChanged = false;
    }
    finally
    {
      this.lockChanged = false;
    }
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

  private void ShowFormula(TempFormula tf, RichTextBox memoForm)
  {
    if (tf == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < tf.Count; ++index)
      stringBuilder.Append(tf[index].text);
    tf.UpdateTokenBegs();
    memoForm.Text = stringBuilder.ToString();
    for (int index = 0; index < tf.Count; ++index)
      this.PaintCurToken(tf[index], memoForm);
    if (tf.Count > 0)
    {
      DataType srcConType = this.GetSrcConType(memoForm);
      if (srcConType == DataType.Unknown)
        return;
      this.IndicateNeedChangeFormula(tf, srcConType, memoForm);
    }
    else
      memoForm.BackColor = Color.White;
  }

  /// <summary>
  /// Проверить, будет ли формула верна с НОВЫМ типом результата.
  /// Если нет, покрасить RichTextBox в красный цвет
  /// </summary>
  /// <param name="tf">Формула</param>
  /// <param name="newType">Новый тип</param>
  /// <param name="rtb">Редактор для этой формулы</param>
  /// <returns>true, если цвет был изменен</returns>
  private bool IndicateNeedChangeFormula(TempFormula tf, DataType newType, RichTextBox rtb)
  {
    Color color = Color.White;
    if (tf.resType == DataType.String && (newType == DataType.Float || newType == DataType.Integer || !TempFormula.CanBeConverted(tf.resType, newType)))
      color = Color.Red;
    if (!(color != rtb.BackColor))
      return false;
    rtb.BackColor = color;
    return true;
  }

  /// <summary>
  /// Получить тип данных в зависимости от текущего RichEdit'а
  /// </summary>
  /// <returns>Тип данных</returns>
  private DataType GetSrcConType(RichTextBox srcCon)
  {
    if (srcCon == this.richAfterFilter || srcCon == this.richGlobalFilter || srcCon == this.richCond || srcCon == this.richInnerCond || srcCon == this.richComplectCond || srcCon == this.richDocCond || srcCon == this.richTextCond || srcCon == this.richWhileCond || srcCon == this.rtbGlobObjFilter || srcCon == this.rtbGlobalCond)
      return DataType.Boolean;
    if (srcCon == this.richFormula)
      return this.GetAttrDataType(this.opData.s1);
    return srcCon == this.richLeftIndent ? DataType.Integer : DataType.Unknown;
  }

  public bool GetAttrCheck(int index)
  {
    if (index >= this.opData.dA_Checks.Count)
      return false;
    string dACheck = this.opData.dA_Checks[index];
    return dACheck != "" && dACheck[0] == 'Y';
  }

  public int GetAttrImageIndex(int index)
  {
    if (index < this.opData.dA_Checks.Count)
    {
      string dACheck = this.opData.dA_Checks[index];
      if (dACheck.Length < 2)
        return 61;
      if (dACheck[1] == 'a')
        return 45;
      if (dACheck[1] == 'd')
        return 46;
    }
    return 0;
  }

  /// <summary>Select right panel and fill it from OpParmData</summary>
  /// <param name="opTag">Operator Tag</param>
  internal void UpdateOpPanel(int opTag)
  {
    this.lockChanged = true;
    try
    {
      this.ShowOpPanel(opTag);
      switch (opTag)
      {
        case 9:
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
          this.panOpParms1.BringToFront();
          this.checkDups.Checked = this.opData.b1;
          switch (Convert.ToInt32(this.opData.s2))
          {
            case 0:
              this.rbGlobalNone.Checked = true;
              break;
            case 1:
              this.rbGlobalPlus.Checked = true;
              break;
            case 2:
              this.rbGlobalMul.Checked = true;
              break;
          }
          switch (Convert.ToInt32(this.opData.s4))
          {
            case 0:
              this.rbSaveNone.Checked = true;
              break;
            case 1:
              this.rbSaveClear.Checked = true;
              break;
            case 2:
              this.rbSaveAdd.Checked = true;
              break;
            case 3:
              this.rbSaveLocal.Checked = true;
              break;
          }
          this.cbSaveRels.Checked = this.opData.b2;
          this.cbNoSearch.Checked = !this.opData.b3;
          if (this.cbNoSearch.Checked)
            this.cbNoSearch.ForeColor = Color.Red;
          else
            this.cbNoSearch.ForeColor = Color.Black;
          this.cbAddThis.Checked = this.opData.b4;
          this.cbConfigOptions.Checked = this.opData.b5;
          switch (Convert.ToInt32(this.opData.st4))
          {
            case 0:
              this.rbContentsAll.Checked = true;
              break;
            case 1:
              this.rbContentsNotClosed.Checked = true;
              break;
            case 2:
              this.rbContentsNotClosedRoots.Checked = true;
              break;
          }
          this.ShowFormula(this.opData.tf, this.richCond);
          if (this.opData.tf2 != null)
            this.ShowFormula(this.opData.tf2, this.richGlobalFilter);
          if (this.opData.tf3 != null)
            this.ShowFormula(this.opData.tf3, this.richAfterFilter);
          this.btnToggleSort.Enabled = false;
          this.srcCon = (Control) this.richCond;
          this.srcCon = this.tabControl1.TabIndex != 0 ? (Control) this.richGlobalFilter : (Control) this.richCond;
          TreeNode node1 = this.tvObjAttrs.Nodes[0];
          TreeNode node2 = this.tvObjAttrs.Nodes[1];
          node1.Nodes.Clear();
          node2.Nodes.Clear();
          for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
          {
            TreeNode node3 = new TreeNode(this.opData.dA_Texts[index]);
            if (this.GetAttrCheck(index))
              node2.Nodes.Add(node3);
            else
              node1.Nodes.Add(node3);
            node3.ImageIndex = this.GetAttrImageIndex(index);
            node3.SelectedImageIndex = node3.ImageIndex;
          }
          this.cbInbuiltSort.Checked = this.opData.st2 == "Y";
          switch (this.opData.s3 == "" ? 0 : Convert.ToInt32(this.opData.s3))
          {
            case 0:
              this.rbNoIspolns.Checked = true;
              break;
            case 1:
              this.rbOnlyCommon.Checked = true;
              break;
            case 2:
              this.rbCurrentIsp.Checked = true;
              break;
            case 3:
              this.rbAllIspolnInfo.Checked = true;
              break;
          }
          this.cbUseCurrentIsps.Checked = this.opData.st3 == "Y";
          this.btnEdExcerpt.Text = this.opData.st1;
          this.beCompareFunc.Text = this.opData.s1;
          this.ShowSelLinks();
          this.tvObjAttrs.ExpandAll();
          this.EnableDisable(this.panOpParms1);
          break;
        case 15:
        case 16 /*0x10*/:
        case 17:
        case 39:
          this.panOpParms2.BringToFront();
          this.ShowFormula(this.opData.tf, this.richTextCond);
          this.srcCon = (Control) this.richTextCond;
          this.btnObjLink.Text = this.opData.st3;
          break;
        case 18:
        case 67:
          this.panOpParms3.BringToFront();
          this.settAttr.attrText = this.opData.st1;
          this.settAttr.objTypeText = this.opData.st2;
          this.settAttr.SetAttrAndObjType(this.opData.s1, this.opData.s2);
          DataType attrDataType = this.GetAttrDataType(this.opData.s1);
          if (attrDataType != DataType.Unknown)
            this.IndicateNeedChangeFormula(this.opData.tf, attrDataType, this.richFormula);
          this.ShowFormula(this.opData.tf, this.richFormula);
          this.srcCon = (Control) this.richFormula;
          this.comboSelector.SelectedIndex = this.opData.settingMod;
          this.cbAddToGlobal.Checked = this.opData.b3;
          if (this.opData.settingMod == 7)
          {
            switch (this.opData.s3)
            {
              case ",":
                this.comboDivider.SelectedIndex = 0;
                break;
              case ", ":
                this.comboDivider.SelectedIndex = 1;
                break;
              case ";":
                this.comboDivider.SelectedIndex = 2;
                break;
              case "; ":
                this.comboDivider.SelectedIndex = 3;
                break;
              case " ":
                this.comboDivider.SelectedIndex = 4;
                break;
              default:
                this.comboDivider.SelectedIndex = -1;
                break;
            }
          }
          else
            this.comboDivider.SelectedIndex = -1;
          switch (this.opData.s4)
          {
            case "O":
              this.rbObject.Checked = true;
              break;
            case "R":
              this.rbRelation.Checked = true;
              break;
            case "F":
              this.rbDocField.Checked = true;
              break;
          }
          this.cbArray.Checked = this.opData.tf2 != null || this.opData.tf3 != null;
          this.EnableDisable(this.panOpParms3);
          break;
        case 19:
          this.panOpParms4.BringToFront();
          this.textId.Text = this.opData.s3;
          this.buttonEdit1.Text = this.opData.st4;
          this.beFontName.Text = this.opData.s5;
          this.seFontSize.Value = (Decimal) this.opData.exID;
          this.cbActiveLink.Checked = this.opData.b1;
          this.cbBold.Checked = this.opData.b2;
          this.cbItalic.Checked = this.opData.b3;
          this.cbUnderline.Checked = this.opData.b4;
          this.cbLinkThisDoc.Checked = this.opData.b5;
          this.textBox3.Text = this.opData.s6;
          this.btnTextColor.ForeColor = Color.FromArgb(this.opData.settingMod);
          this.cbAuthFile.Checked = this.opData.b6;
          if (this.opData.st3 != "")
          {
            this.checkAddId.Checked = true;
            this.edAddAttr.Text = this.opData.st3;
          }
          else
            this.checkAddId.Checked = false;
          if (this.opData.tf != null && this.opData.tf.Count > 0)
          {
            this.checkForm.Checked = true;
            this.checkAttr.Checked = false;
            this.ShowFormula(this.opData.tf, this.richSetValue);
          }
          else
          {
            this.checkAttr.Checked = true;
            this.checkForm.Checked = false;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              this.setAttr.selAttr = !(this.opData.s1 != "") ? (SelFormResult) null : new SelFormResult(sessionKeeper.Session, true, this.opData.s1);
              this.setAttr.selObjType = !(this.opData.s2 != "") ? (SelFormResult) null : new SelFormResult(sessionKeeper.Session, false, this.opData.s2);
            }
            this.setAttr.attrText = this.opData.st1;
            this.setAttr.objTypeText = this.opData.st2;
            this.richSetValue.Clear();
          }
          if (this.opData.tf2 != null && this.opData.tf2.Count > 0)
            this.ShowFormula(this.opData.tf2, this.richLeftIndent);
          this.cbActiveLink.Visible = this.checkAttr.Checked;
          this.cbLinkThisDoc.Visible = this.checkAttr.Checked;
          this.srcCon = (Control) this.richSetValue;
          this.tabControl2.SelectedIndex = 0;
          this.EnableDisable(this.panOpParms4);
          break;
        case 20:
          this.panOpParms5.BringToFront();
          this.textBox1.Text = this.opData.s1;
          this.buttonEdit2.Text = this.opData.st4;
          this.editAddAttr.Text = this.opData.st3;
          this.edSaveNewID.Text = this.opData.st2;
          this.checkMakeCurrent.Checked = this.opData.b1;
          this.checkFillDefault.Checked = this.opData.b2;
          this.cbUseByDef.Checked = this.opData.b3;
          this.checkEditAdd.Checked = this.opData.s4 != "";
          this.cbAvoidDup.Checked = this.opData.b4;
          this.cbCurrentForever.Checked = this.opData.b5;
          this.textBox2.Text = this.opData.s5;
          this.EnableDisable(this.panOpParms5);
          break;
        case 21:
          this.panOpParms6.BringToFront();
          this.cbSelectDoc.Checked = this.opData.b1;
          this.radioButton2.Checked = this.opData.b2;
          this.cbByDefault.Checked = this.opData.b3;
          if (this.opData.tf.Count == 0)
          {
            this.checkSelFromTempl.Checked = true;
            this.edSelTemplate.Text = this.opData.s1;
            this.buttonEdit3.Text = this.opData.st3;
            this.richTextID.Clear();
          }
          else
          {
            this.checkByFormula.Checked = true;
            this.ShowFormula(this.opData.tf, this.richTextID);
          }
          this.srcCon = (Control) this.richTextID;
          this.EnableDisable(this.panOpParms6);
          break;
        case 24:
        case 51:
          this.panOpParms7.BringToFront();
          this.edObjType.Text = this.opData.s2;
          this.ShowFormula(this.opData.tf, this.richTextBox3);
          this.srcCon = (Control) this.richTextBox3;
          break;
        case 25:
        case 26:
        case 27:
          this.panOpParms8.BringToFront();
          this.editSelObject.Text = this.opData.s2;
          this.ShowFormula(this.opData.tf, this.richTextBox4);
          this.ShowFormula(this.opData.tf2, this.richInnerCond);
          this.srcCon = (Control) this.richTextBox4;
          switch (opTag - 25)
          {
            case 0:
              this.labelObjType.Text = LocalizationHolder.rm.GetString("Expert.Editor_311");
              break;
            case 1:
              this.labelObjType.Text = LocalizationHolder.rm.GetString("Expert.Editor_312");
              break;
            case 2:
              this.labelObjType.Text = LocalizationHolder.rm.GetString("Expert.Editor_313");
              break;
          }
          if (this.opData.s4 != null)
          {
            this.tbConds.Text = this.opData.s4;
            break;
          }
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (this.opData.s1 != "")
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(this.opData.s1), false);
              if (dbObject != null)
              {
                if (sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) is IExpertServer customService)
                {
                  this.opData.s4 = customService.GetFolderConds(dbObject.ObjectID);
                  this.tbConds.Text = this.opData.s4;
                  break;
                }
                break;
              }
              break;
            }
            break;
          }
        case 32 /*0x20*/:
          this.panOpParmsC.BringToFront();
          if (this.opData.b1)
            this.rbNewPage.Checked = true;
          else
            this.rbChangePage.Checked = true;
          this.beNewList.Text = this.opData.s2 != "" ? this.opData.s2 : this.opData.s1;
          this.cbMakeListCurrent.Checked = this.opData.b2;
          this.EnableDisable(this.panOpParmsC);
          break;
        case 40:
          this.panOpParms9.BringToFront();
          TreeNode node4 = this.tvRecalcAttrs.Nodes[0];
          TreeNode node5 = this.tvRecalcAttrs.Nodes[1];
          node4.Nodes.Clear();
          node5.Nodes.Clear();
          for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
          {
            TreeNode node6 = new TreeNode(this.opData.dA_Texts[index]);
            if (this.GetAttrCheck(index))
              node5.Nodes.Add(node6);
            else
              node4.Nodes.Add(node6);
            node6.ImageIndex = 32 /*0x20*/;
            node6.SelectedImageIndex = 32 /*0x20*/;
          }
          this.tvRecalcAttrs.ExpandAll();
          break;
        case 43:
          this.panOpParmsA.BringToFront();
          this.beUserProc.Text = this.opData.s1;
          ExpertCalling int32 = (ExpertCalling) Convert.ToInt32(this.opData.s2);
          switch (int32)
          {
            case ExpertCalling.callProc:
              this.cbProc.Checked = true;
              break;
            case ExpertCalling.callUserProc:
              this.cbUserProc.Checked = true;
              break;
            case ExpertCalling.callScript:
              this.cbScript.Checked = true;
              break;
            case ExpertCalling.callScenario:
              this.cbScenario.Checked = true;
              break;
          }
          bool flag = int32 == ExpertCalling.callUserProc || int32 == ExpertCalling.callScenario;
          this.parm1Box.Visible = flag;
          this.parm2Box.Visible = flag;
          this.label40.Visible = flag;
          this.label41.Visible = flag;
          if (flag)
          {
            this.ShowFormula(this.opData.tf, this.parm1Box);
            this.ShowFormula(this.opData.tf2, this.parm2Box);
            break;
          }
          break;
        case 44:
          this.panOpParmsB.BringToFront();
          this.edVersionRule.Text = this.opData.s2;
          switch (this.opData.s3)
          {
            case "M":
              this.rbActualSubst.Checked = true;
              break;
            case "A":
              this.rbAllSubst.Checked = true;
              break;
            case "C":
              this.rbClientSubst.Checked = true;
              break;
          }
          break;
        case 49:
          this.panOpParmsE.BringToFront();
          this.lockChanged = true;
          try
          {
            switch (this.opData.st2)
            {
              case "Y":
                this.rbScenario.Checked = true;
                break;
              case "N":
              case "":
                this.rbDocScript.Checked = true;
                break;
              case "T":
                this.rbTechcard.Checked = true;
                break;
            }
            this.beDocScript.Text = this.opData.s4;
            this.beTypeForDoc.Text = this.opData.s2;
            if (this.opData.tf.Count > 0)
              this.ShowFormula(this.opData.tf, this.richDocCond);
            else
              this.richDocCond.Clear();
            if (this.opData.tf2.Count > 0)
              this.ShowFormula(this.opData.tf2, this.richDocCondBefore);
            else
              this.richDocCondBefore.Clear();
            this.cbNoEmpty.Checked = this.opData.b1;
            this.cbSecondPass.Checked = this.opData.b2;
            this.textPrefix.Text = this.opData.st1;
            this.srcCon = (Control) this.richDocCond;
            switch (this.opData.s5)
            {
              case "1":
                this.rbGroupCont.Checked = true;
                break;
              case "2":
                this.rbGroupAll.Checked = true;
                break;
              default:
                this.rbNoGroup.Checked = true;
                break;
            }
            this.EnableDisable(this.panOpParmsE);
            this.cbCoWorkerDoc.Checked = this.opData.b4;
            this.cbNoNumber.Checked = this.opData.b5;
            this.cbNoCount.Checked = this.opData.b6;
            break;
          }
          finally
          {
            this.lockChanged = false;
          }
        case 50:
          this.panOpParmsD.BringToFront();
          this.beComplectType.Text = this.opData.s2;
          this.beCompObjType.Text = this.opData.s4;
          this.cbCreateComplect.Checked = this.opData.b1;
          if (this.opData.tf.Count > 0)
            this.ShowFormula(this.opData.tf, this.richComplectCond);
          else
            this.richComplectCond.Clear();
          this.srcCon = (Control) this.richComplectCond;
          this.tbPostfix.Text = this.opData.st1;
          this.cbAdditionalComp.Checked = this.opData.b3;
          break;
        case 53:
          this.panGlobalTableFolder.BringToFront();
          this.beReplaceObjType.Text = this.opData.s2;
          this.ShowFormula(this.opData.tf2, this.rtbGlobalCond);
          this.tvGlobCommonAttrs.BeginUpdate();
          TreeNode node7 = this.tvGlobCommonAttrs.Nodes[0];
          TreeNode node8 = this.tvGlobCommonAttrs.Nodes[1];
          node7.Nodes.Clear();
          node8.Nodes.Clear();
          for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
          {
            TreeNode node9 = new TreeNode(this.opData.dA_Texts[index]);
            if (this.GetAttrCheck(index))
              node8.Nodes.Add(node9);
            else
              node7.Nodes.Add(node9);
            node9.ImageIndex = this.GetAttrImageIndex(index);
            node9.SelectedImageIndex = node9.ImageIndex;
          }
          this.tvGlobCommonAttrs.ExpandAll();
          this.tvGlobCommonAttrs.EndUpdate();
          this.beGlobExcerpt.Text = this.opData.st1;
          this.tvGlobRoot.BeginUpdate();
          this.tvGlobRoot.Nodes.Clear();
          for (int index = 0; index < this.opData.linkTexts.Count; ++index)
          {
            TreeNode node10 = new TreeNode(this.opData.linkTexts[index]);
            this.tvGlobRoot.Nodes.Add(node10);
            node10.ImageIndex = this.opData.linkIDs[index] <= 90000 ? 10 : 9;
            node10.SelectedImageIndex = node10.ImageIndex;
          }
          for (int index1 = 0; index1 < this.opData.objTexts.Count; ++index1)
          {
            int index2 = this.opData.ltForOT[index1];
            if (index2 >= 0 && index2 < this.tvGlobRoot.Nodes.Count)
            {
              TreeNode node11 = this.tvGlobRoot.Nodes[index2];
              TreeNode node12 = new TreeNode(this.opData.objTexts[index1]);
              node11.Nodes.Add(node12);
              node12.ImageIndex = 51;
              node12.SelectedImageIndex = node12.ImageIndex;
            }
          }
          this.tvGlobRoot.ExpandAll();
          this.tvGlobRoot.EndUpdate();
          switch (this.opData.s3)
          {
            case "1":
              this.radioButton12.Checked = true;
              break;
            case "3":
              this.radioButton10.Checked = true;
              break;
            default:
              this.radioButton13.Checked = true;
              break;
          }
          this.ShowFormula(this.opData.tf, this.rtbGlobObjFilter);
          this.cbConfigOptions1.Checked = this.opData.b5;
          switch (Convert.ToInt32(this.opData.st4))
          {
            case 0:
              this.rbShowHidden1.Checked = true;
              break;
            case 1:
              this.rbHideHidden1.Checked = true;
              break;
            case 2:
              this.rbHideHiddenRoots1.Checked = true;
              break;
          }
          break;
        case 54:
          this.panOpGlobalType.BringToFront();
          this.cbForObjTypes.Items.Clear();
          this.cbForObjTypes.BeginUpdate();
          for (int index = 0; index < this.opData.forTexts.Count; ++index)
            this.cbForObjTypes.Items.Add((object) this.opData.forTexts[index], !this.opData.forOT_Only[index]);
          this.cbForObjTypes.EndUpdate();
          switch (this.opData.s3)
          {
            case "1":
              this.radioButton4.Checked = true;
              break;
            case "3":
              this.radioButton3.Checked = true;
              break;
            default:
              this.radioButton5.Checked = true;
              break;
          }
          this.tvGTAttrs.BeginUpdate();
          TreeNode node13 = this.tvGTAttrs.Nodes[0];
          TreeNode node14 = this.tvGTAttrs.Nodes[1];
          node13.Nodes.Clear();
          node14.Nodes.Clear();
          for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
          {
            TreeNode node15 = new TreeNode(this.opData.dA_Texts[index]);
            if (this.GetAttrCheck(index))
              node14.Nodes.Add(node15);
            else
              node13.Nodes.Add(node15);
            node15.ImageIndex = this.GetAttrImageIndex(index);
            node15.SelectedImageIndex = node15.ImageIndex;
          }
          this.tvGTAttrs.ExpandAll();
          this.tvGTAttrs.EndUpdate();
          this.beGTExcerpt.Text = this.opData.st1;
          this.tvGTSearch.BeginUpdate();
          this.tvGTSearch.Nodes.Clear();
          for (int index = 0; index < this.opData.linkTexts.Count; ++index)
          {
            TreeNode node16 = new TreeNode(this.opData.linkTexts[index]);
            this.tvGTSearch.Nodes.Add(node16);
            node16.ImageIndex = this.opData.linkIDs[index] <= 90000 ? 10 : 9;
            node16.SelectedImageIndex = node16.ImageIndex;
          }
          for (int index3 = 0; index3 < this.opData.objTexts.Count; ++index3)
          {
            int index4 = this.opData.ltForOT[index3];
            if (index4 >= 0 && index4 < this.tvGTSearch.Nodes.Count)
            {
              TreeNode node17 = this.tvGTSearch.Nodes[index4];
              TreeNode node18 = new TreeNode(this.opData.objTexts[index3]);
              node17.Nodes.Add(node18);
              node18.ImageIndex = 51;
              node18.SelectedImageIndex = node18.ImageIndex;
            }
          }
          this.tvGTSearch.ExpandAll();
          this.tvGTSearch.EndUpdate();
          this.ShowFormula(this.opData.tf, this.rtGTCond);
          this.cbConfigOptions2.Checked = this.opData.b5;
          switch (Convert.ToInt32(this.opData.st4))
          {
            case 0:
              this.rbShowHidden2.Checked = true;
              break;
            case 1:
              this.rbHideHidden2.Checked = true;
              break;
            case 2:
              this.rbHideHiddenRoots2.Checked = true;
              break;
          }
          break;
        case 63 /*0x3F*/:
          this.panOpParmsTI.BringToFront();
          break;
        case 64 /*0x40*/:
          this.panOpParmsStyleB.BringToFront();
          break;
        case 65:
          this.panOpParmsStyleC.BringToFront();
          break;
        case 66:
          this.panOpParmsTI.BringToFront();
          this.tvCopiedAttrs.BeginUpdate();
          TreeNode node19 = this.tvCopiedAttrs.Nodes[0];
          TreeNode node20 = this.tvCopiedAttrs.Nodes[1];
          node19.Nodes.Clear();
          node20.Nodes.Clear();
          for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
          {
            TreeNode node21 = new TreeNode(this.opData.dA_Texts[index]);
            if (this.GetAttrCheck(index))
              node20.Nodes.Add(node21);
            else
              node19.Nodes.Add(node21);
            node21.ImageIndex = this.GetAttrImageIndex(index);
            node21.SelectedImageIndex = node21.ImageIndex;
          }
          this.tvCopiedAttrs.ExpandAll();
          this.tvCopiedAttrs.EndUpdate();
          this.beSourceType.Text = this.opData.s2;
          this.beCreatingDocType.Text = this.opData.st2;
          break;
      }
      this.opDataChanged = false;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  internal void ShowModPanel(int modTag)
  {
    switch (modTag)
    {
      case -1:
        this.panModParmsEmpty.BringToFront();
        break;
      case 0:
      case 1:
        this.panModParms1.BringToFront();
        break;
      case 2:
      case 3:
        this.panModParms4.BringToFront();
        break;
      case 4:
      case 5:
        this.panModParms1.BringToFront();
        break;
      case 6:
        this.panModParms5.BringToFront();
        break;
      case 7:
        this.panModParms3.BringToFront();
        break;
      case 8:
        this.panModParms2.BringToFront();
        break;
      case 68:
        this.panModParmsVersion.BringToFront();
        break;
    }
  }

  internal void ShowOpPanel(int opTag)
  {
    switch (opTag)
    {
      case -1:
        this.panOpParmsEmpty.BringToFront();
        break;
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
        this.panOpParms1.BringToFront();
        break;
      case 15:
      case 16 /*0x10*/:
      case 17:
        this.panOpParms2.BringToFront();
        break;
      case 18:
        this.panOpParms3.BringToFront();
        this.FillParmGrid();
        this.gridTable.Selection.FocusRow(1);
        break;
      case 19:
        this.panOpParms4.BringToFront();
        break;
      case 20:
        this.panOpParms5.BringToFront();
        break;
      case 21:
        this.panOpParms6.BringToFront();
        break;
      case 40:
        this.panOpParms9.BringToFront();
        break;
      case 43:
        this.panOpParmsA.BringToFront();
        break;
      case 44:
        this.panOpParmsB.BringToFront();
        break;
      case 49:
        this.panOpParmsE.BringToFront();
        break;
      case 50:
        this.panOpParmsD.BringToFront();
        break;
    }
  }

  internal void ShowParmPanels(int modTag, int opTag)
  {
    this.ShowModPanel(modTag);
    this.ShowOpPanel(opTag);
  }

  /// <summary>
  /// Show Right panels and fill them from opData and modData
  /// (call UpdateModOp first)
  /// </summary>
  /// <param name="node">Tree Node</param>
  internal void ShowPanelsForNode(TreeListNode node)
  {
    int modTag = -1;
    int opTag = -1;
    if (node != null)
    {
      modTag = node.ImageIndex;
      opTag = node.StateImageIndex;
    }
    this.UpdateModPanel(modTag);
    this.UpdateOpPanel(opTag);
  }

  /// <summary>Fill modData and opData from the node</summary>
  /// <param name="node">Tree Node</param>
  private void UpdateModOp(TreeListNode node)
  {
    if (node == null)
    {
      this.modData.Clear();
      this.opData.Clear();
    }
    else
    {
      Intermech.Expert.NodeData nodeData = this.data(node);
      if (nodeData.mods != null)
        nodeData.mods.FillModParmData(ref this.modData);
      else
        this.modData.Clear();
      if (nodeData.ops != null)
        nodeData.ops.FillOpParmData(ref this.opData);
      else
        this.opData.Clear();
    }
  }

  private void SetDescrText(string text)
  {
    List<string> stringList = new List<string>();
    do
    {
      int length = text.IndexOf("\n\r");
      if (length < 0)
      {
        stringList.Add(text);
        break;
      }
      stringList.Add(text.Substring(0, length));
      text = text.Substring(length + 2);
    }
    while (text != "");
    this.lblDescr.Lines = stringList.ToArray();
  }

  private void OnChangeNode(TreeListNode node)
  {
    this.UpdateModOp(node);
    this.ShowPanelsForNode(node);
    if (node != null)
      this.SetDescrText(this.GetNodeDescr(this.data(node)));
    else
      this.lblDescr.Text = "";
    if (node != null)
    {
      Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) node.Tag;
      this.UpdateNode(node, ref tag);
    }
    this.EnableBtnDocParms();
  }

  private void EnableBtnDocParms()
  {
    TreeListNode focusedNode = this.tree.FocusedNode;
    if (this.ObjectType == ExpertScriptType.AttribRule)
    {
      if (focusedNode != null)
      {
        Intermech.Expert.NodeData nodeData = this.data(focusedNode);
        this.btnDocParms.Enabled = nodeData.ops is OpParmExpObj && (nodeData.ops as OpParmExpObj).objTypeGUID != "";
      }
      else
        this.btnDocParms.Enabled = false;
    }
    if (this.ObjectType != ExpertScriptType.ComplectTemplate)
      return;
    if (focusedNode != null)
    {
      Intermech.Expert.NodeData nodeData = this.data(focusedNode);
      this.btnDocParms.Enabled = nodeData.ops is OpCreateDoc && (nodeData.ops as OpCreateDoc).scriptGUID != "";
    }
    else
      this.btnDocParms.Enabled = false;
  }

  private void tree_AfterFocusNode(object sender, DevExpress.IM.XtraTreeList.NodeEventArgs e)
  {
    this.OnChangeNode(e.Node);
  }

  private void tabControl1_Selected(object sender, TabControlEventArgs e)
  {
    if (this.tabControl1.TabIndex == 0)
      this.srcCon = (Control) this.richCond;
    else
      this.srcCon = (Control) this.richGlobalFilter;
  }

  private bool SaveNodeData(ref Intermech.Expert.NodeData nd)
  {
    try
    {
      if (nd.mods != null && nd.mods is ModParmLoop && this.checkFor.Checked && nd.ops != null && nd.ops is OpParmObject)
        throw new AbortException(LocalizationHolder.rm.GetString("Expert.Editor_314"));
      if (nd.opTag == 15 && nd.modTag >= 0 && nd.modTag != 4 && nd.modTag != 5)
        throw new AbortException(LocalizationHolder.rm.GetString("Expert.Editor_315"));
      if (this.modDataChanged)
      {
        if (nd.mods == null)
        {
          System.Type modNodeType = Intermech.Expert.NodeData.GetModNodeType(nd.modTag);
          if (modNodeType != (System.Type) null)
            nd.mods = (ModParm) Activator.CreateInstance(modNodeType);
        }
        nd.mods.SetData(ref this.modData);
      }
      if (this.opDataChanged)
      {
        if (nd.ops != null)
        {
          if (this.ObjectType == ExpertScriptType.CommonCalc && nd.opTag == 18 && (this.opData.s1 != ((OpParmSetting) nd.ops).attrGUID || this.opData.s2 != ((OpParmSetting) nd.ops).attrGUID))
            this.SettingAttrChanged = true;
          nd.ops.SetData(ref this.opData);
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Expert.Editor_316"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this.modDataChanged = false;
    this.opDataChanged = false;
    return true;
  }

  private void UpdateNode(TreeListNode node, ref Intermech.Expert.NodeData nd)
  {
    node[(object) 1] = (object) nd.GetShortMod();
    node[(object) 2] = (object) nd.GetShortOp();
  }

  private bool SaveChangedNodeData()
  {
    if (this.tree.FocusedNode == null || !this.modDataChanged && !this.opDataChanged)
      return true;
    Intermech.Expert.NodeData nd = this.data(this.tree.FocusedNode);
    if (!this.SaveNodeData(ref nd))
      return false;
    this.UpdateNode(this.tree.FocusedNode, ref nd);
    this.scriptChanged = true;
    this.UpdateSaveCancelButtons();
    return true;
  }

  public void InitParms(Intermech.Expert.NodeData nd, ScriptEdit2 editor)
  {
    nd.modTag = editor.modPressed != null ? Convert.ToInt32(editor.modPressed.Tag) : -1;
    nd.opTag = editor.opPressed != null ? Convert.ToInt32(editor.opPressed.Tag) : -1;
    System.Type modNodeType = editor.GetModNodeType();
    if (modNodeType != (System.Type) null)
      nd.mods = (ModParm) Activator.CreateInstance(modNodeType);
    System.Type opNodeType = editor.GetOpNodeType();
    if (!(opNodeType != (System.Type) null))
      return;
    nd.ops = (OpParm) Activator.CreateInstance(opNodeType);
  }

  internal System.Type GetModNodeType()
  {
    if (this.modPressed == null)
      return (System.Type) null;
    if (this.modPressed == this.modForEach || this.modPressed == this.modForFirst || this.modPressed == this.modForMin || this.modPressed == this.modForMax || this.modPressed == this.modIfExists || this.modPressed == this.modIfAll)
      return typeof (ModParmFormula);
    if (this.modPressed == this.modCycle)
      return typeof (ModParmLoop);
    if (this.modPressed == this.modCycleSort || this.modPressed == this.modCycleGroup)
      return typeof (ModParmSort);
    return this.modPressed == this.modVersions ? typeof (ModParmVersion) : (System.Type) null;
  }

  internal System.Type GetOpNodeType()
  {
    if (this.opPressed == null)
      return (System.Type) null;
    if (this.opPressed == this.objParent || this.opPressed == this.objChild || this.opPressed == this.objSibling || this.opPressed == this.objLinked || this.opPressed == this.objAncestor || this.opPressed == this.objDescendant)
      return typeof (OpParmObject);
    if (this.opPressed == this.opExit || this.opPressed == this.returnBtn || this.opPressed == this.opFolder || this.opPressed == this.opSelFolder)
      return typeof (OpParmCond);
    if (this.opPressed == this.opSetting)
      return typeof (OpParmSetting);
    if (this.opPressed == this.docFillText)
      return typeof (OpParmFillFld);
    if (this.opPressed == this.docNewElem)
      return typeof (OpParmCreateFld);
    if (this.opPressed == this.docSelectElem)
      return typeof (OpParmSelFld);
    if (this.opPressed == this.TypeBtn)
      return typeof (OpParmType);
    if (this.opPressed == this.recalcBtn)
      return typeof (OpParmObject);
    if (this.opPressed == this.ByFormBtn || this.opPressed == this.ByTableBtn || this.opPressed == this.ByScriptBtn)
      return typeof (OpParmExpObj);
    if (this.opPressed == this.userProc)
      return typeof (OpParmUserProc);
    if (this.opPressed == this.verRule)
      return typeof (OpParmVersionRule);
    if (this.opPressed == this.docPaging)
      return typeof (OpParmDocControl);
    if (this.opPressed == this.opCreateDoc)
      return typeof (OpCreateDoc);
    if (this.opPressed == this.opCreateComplect)
      return typeof (OpCreateComplect);
    if (this.opPressed == this.btnGlobalFolder)
      return typeof (OpParmGlobRoot);
    if (this.opPressed == this.btnGlobalRequest)
      return typeof (OpParmGlobForType);
    return this.opPressed == this.opCreateDocLink ? typeof (OpParmTiLink) : (System.Type) null;
  }

  internal Intermech.Expert.NodeData data(TreeListNode node) => (Intermech.Expert.NodeData) node.Tag;

  internal void UpdateESCond(Guid objGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objGuid, false);
      if (dbObject == null || !(dbObject is IExpertObject))
        return;
      IExpertObject expertObject = (IExpertObject) dbObject;
      expertObject.Load();
      if (expertObject.Cond != null)
      {
        TempFormula cond = expertObject.Cond;
        cond.BeautifyInfixForm();
        this.opData.tf2 = cond;
        this.ShowFormula(cond, this.richInnerCond);
        this.opData.s4 = (string) null;
      }
      else
        this.richInnerCond.Text = "";
      this.opDataChanged = true;
    }
  }

  internal bool EditFormula(ref TempFormula tf, string caption, RichTextBox memo)
  {
    this.formEd.CanReturnEmpty = true;
    if (!this.formEd.Execute(ref tf, caption))
      return false;
    this.ShowFormula(tf, memo);
    return true;
  }

  private void modPopMenu_Popup(object sender, EventArgs e)
  {
  }

  private void opPopMenu_Popup(object sender, EventArgs e)
  {
  }

  private void menuChangeOpForm_Click(object sender, EventArgs e)
  {
    if (sender is RichTextBox && this.srcCon != sender)
      this.srcCon = (Control) (sender as RichTextBox);
    if (sender is ToolStripItem)
      this.srcCon = ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl;
    if (!(this.srcCon is RichTextBox))
      return;
    string caption1 = "";
    RichTextBox srcCon = (RichTextBox) this.srcCon;
    if (srcCon == this.richGlobalFilter)
    {
      if (this.opData.tf2 == null)
        this.opData.tf2 = new TempFormula();
      this.opData.tf2.resType = DataType.Boolean;
      this.opData.tf2.Cond = true;
      if (!this.EditFormula(ref this.opData.tf2, LocalizationHolder.rm.GetString("Expert.Editor_317"), this.richGlobalFilter))
        return;
      this.opDataChanged = true;
    }
    else if (srcCon == this.richAfterFilter)
    {
      if (this.opData.tf3 == null)
        this.opData.tf3 = new TempFormula();
      this.opData.tf3.resType = DataType.Boolean;
      this.opData.tf3.Cond = true;
      if (!this.EditFormula(ref this.opData.tf3, LocalizationHolder.rm.GetString("Expert.Editor_317"), this.richAfterFilter))
        return;
      this.opDataChanged = true;
    }
    else
    {
      if (srcCon == this.richSetValue)
      {
        caption1 = LocalizationHolder.rm.GetString("Expert.Editor_318");
        this.opData.tf.resType = DataType.String;
        this.opData.tf.Cond = false;
      }
      if (srcCon == this.richFormula)
      {
        caption1 = LocalizationHolder.rm.GetString("Expert.Editor_319");
        FieldTypes attrType = FieldTypes.ftString;
        if (this.opData.s1 == "")
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_99"), LocalizationHolder.rm.GetString("Expert.Editor_55"), MessageBoxButtons.OK);
          return;
        }
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(this.opData.s1), false);
          if (attributeType != null)
          {
            attrType = attributeType.AttributeType;
            this.opData.tf.isArray = attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
          }
        }
        DataType dataType = DataTypeConvertor.AttrType2DataType(attrType);
        if (this.opData.tf.resType != dataType)
        {
          this.opData.tf.resType = dataType;
          this.opDataChanged = true;
          this.IndicateWrongFormula(this.opData.tf, this.richFormula);
        }
        this.opData.tf.Cond = false;
      }
      if (srcCon == this.richTextCond)
      {
        caption1 = "";
        this.opData.tf.resType = DataType.Boolean;
        this.opData.tf.Cond = true;
      }
      if (srcCon == this.richCond)
      {
        caption1 = LocalizationHolder.rm.GetString("Expert.Editor_320");
        this.opData.tf.resType = DataType.Boolean;
        this.opData.tf.Cond = true;
      }
      if (srcCon == this.richTextID)
      {
        caption1 = LocalizationHolder.rm.GetString("Expert.Editor_321");
        this.opData.tf.resType = DataType.String;
        this.opData.tf.Cond = false;
      }
      if (srcCon == this.richTextBox3 || srcCon == this.richTextBox4)
      {
        caption1 = LocalizationHolder.rm.GetString("Expert.Editor_322");
        this.opData.tf.resType = DataType.Boolean;
        this.opData.tf.Cond = true;
      }
      if (srcCon == this.parm1Box)
      {
        caption1 = "";
        this.opData.tf.resType = DataType.String;
        this.opData.tf.Cond = false;
      }
      if (srcCon == this.richLeftIndent)
      {
        string caption2 = LocalizationHolder.rm.GetString("Expert.Editor_593");
        this.opData.tf2.resType = DataType.Integer;
        this.opData.tf2.Cond = false;
        if (!this.EditFormula(ref this.opData.tf2, caption2, srcCon))
          return;
        this.opDataChanged = true;
      }
      else if (srcCon == this.parm2Box)
      {
        string caption3 = "";
        if (this.opData.tf2 == null)
          this.opData.tf2 = new TempFormula();
        this.opData.tf2.resType = DataType.String;
        this.opData.tf2.Cond = false;
        if (!this.EditFormula(ref this.opData.tf2, caption3, srcCon))
          return;
        this.opDataChanged = true;
      }
      else if (srcCon == this.rtGTCond || srcCon == this.rtbGlobObjFilter || srcCon == this.rtbGlobalCond)
      {
        if (srcCon == this.rtbGlobalCond)
        {
          string caption4 = LocalizationHolder.rm.GetString("Expert.Editor_623");
          this.opData.tf2.resType = DataType.Boolean;
          this.opData.tf2.Cond = true;
          if (!this.EditFormula(ref this.opData.tf2, caption4, srcCon))
            return;
          this.opDataChanged = true;
        }
        else
        {
          string caption5 = LocalizationHolder.rm.GetString("Expert.Editor_622");
          this.opData.tf.resType = DataType.Boolean;
          this.opData.tf.Cond = true;
          if (!this.EditFormula(ref this.opData.tf, caption5, srcCon))
            return;
          this.opDataChanged = true;
        }
      }
      else if (srcCon == this.richDocCondBefore)
      {
        if (!this.EditFormula(ref this.opData.tf2, caption1, srcCon))
          return;
        this.opDataChanged = true;
      }
      else
      {
        if (!this.EditFormula(ref this.opData.tf, caption1, srcCon))
          return;
        this.opDataChanged = true;
      }
    }
  }

  private bool IndicateWrongFormula(TempFormula tf, RichTextBox rtb)
  {
    string errorMsg = (string) null;
    Color color = tf.CheckDataTypes(ref errorMsg) == -1 ? Color.White : Color.Red;
    if (!(color != rtb.BackColor))
      return false;
    rtb.BackColor = color;
    return true;
  }

  /// <summary>Получить тип данных для атрибута</summary>
  /// <param name="attrGuid"></param>
  /// <returns></returns>
  private DataType GetAttrDataType(string attrGuid)
  {
    if (attrGuid == "")
      return DataType.Unknown;
    FieldTypes attrType = FieldTypes.ftString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(attrGuid), false);
      if (attributeType != null)
        attrType = attributeType.AttributeType;
    }
    return DataTypeConvertor.AttrType2DataType(attrType);
  }

  private bool MultipleValued(string attrGuid)
  {
    if (attrGuid == "")
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(attrGuid), false);
      if (attributeType != null)
      {
        if (attributeType.MultipleValued != MultiValueModes.MultiValues)
        {
          if (attributeType.MultipleValued != MultiValueModes.MultiValuesFromList)
            goto label_10;
        }
        return true;
      }
    }
label_10:
    return false;
  }

  private void menuChangeModForm_Click(object sender, EventArgs e)
  {
    Control control = (Control) (sender as RichTextBox);
    if (sender is ToolStripItem)
      control = ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl;
    if (!(control is RichTextBox))
      return;
    string caption = "";
    RichTextBox memo = (RichTextBox) control;
    if (memo == this.richWhileCond)
    {
      caption = LocalizationHolder.rm.GetString("Expert.Editor_323");
      this.modData.tf.resType = DataType.Boolean;
      this.modData.tf.Cond = true;
    }
    if (memo == this.richForEnd)
    {
      caption = LocalizationHolder.rm.GetString("Expert.Editor_324");
      this.modData.tf.resType = DataType.Integer;
      this.modData.tf.Cond = false;
    }
    if (memo == this.richTextBox1)
    {
      caption = "";
      this.modData.tf.resType = DataType.Boolean;
      this.modData.tf.Cond = true;
    }
    if (memo == this.richTextBox2)
    {
      caption = LocalizationHolder.rm.GetString("Expert.Editor_325");
      if (this.checkInt.Checked)
        this.modData.tf.resType = DataType.Integer;
      if (this.checkFloat.Checked)
        this.modData.tf.resType = DataType.Float;
      if (this.checkDate.Checked)
        this.modData.tf.resType = DataType.Date;
      if (this.checkString.Checked)
        this.modData.tf.resType = DataType.String;
      if (this.checkMeasured.Checked)
        this.modData.tf.resType = DataType.Measured;
      this.modData.tf.Cond = false;
    }
    if (memo == this.richVerCond)
    {
      caption = LocalizationHolder.rm.GetString("Expert.Editor_707");
      this.modData.tf.resType = DataType.Boolean;
      this.modData.tf.Cond = true;
    }
    if (!this.EditFormula(ref this.modData.tf, caption, memo))
      return;
    this.modDataChanged = true;
  }

  private void richWhileCond_MouseDown(object sender, MouseEventArgs e)
  {
    this.srcCon = sender as Control;
  }

  private void checkInt_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (sender is CheckEdit)
    {
      if (!((CheckEdit) sender).Checked)
        return;
    }
    else if (sender is RadioButton && !(sender as RadioButton).Checked)
      return;
    if (this.modData.tf.Count > 0)
    {
      this.modData.tf.Clear();
      this.richTextBox2.Clear();
    }
    this.modDataChanged = true;
  }

  private void checkDoWhile_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender as RadioButton).Checked)
      return;
    if (sender == this.checkDoWhile)
    {
      this.checkFor.Checked = false;
      this.checkMulti.Checked = false;
    }
    else if (sender == this.checkMulti)
    {
      this.checkDoWhile.Checked = false;
      this.checkFor.Checked = false;
    }
    else
    {
      this.checkDoWhile.Checked = false;
      this.checkMulti.Checked = false;
    }
    if (this.lockChanged)
      return;
    if (this.modData.tf.Count > 0)
    {
      this.modData.tf.Clear();
      this.richWhileCond.Clear();
      this.richForEnd.Clear();
    }
    this.modData.ForLoop = this.checkFor.Checked;
    this.modData.startValue = !this.checkMulti.Checked ? 0 : int.MaxValue;
    this.modDataChanged = true;
    this.EnableDisable(this.panModParms5);
  }

  private void checkAttr_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (this.opData.tf.Count > 0)
    {
      this.opData.tf.Clear();
      this.richSetValue.Clear();
    }
    this.opDataChanged = true;
    this.lockChanged = true;
    try
    {
      this.checkForm.Checked = !this.checkAttr.Checked;
    }
    finally
    {
      this.lockChanged = false;
    }
    this.cbActiveLink.Visible = this.checkAttr.Checked;
    this.cbLinkThisDoc.Visible = this.checkAttr.Checked;
    this.EnableDisable(this.panOpParms4);
  }

  private void checkForm_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (this.opData.tf.Count > 0)
    {
      this.opData.tf.Clear();
      this.richSetValue.Clear();
    }
    this.opDataChanged = true;
    this.lockChanged = true;
    try
    {
      this.checkAttr.Checked = !this.checkForm.Checked;
    }
    finally
    {
      this.lockChanged = false;
    }
    this.cbActiveLink.Visible = this.checkAttr.Checked;
    this.cbLinkThisDoc.Visible = this.checkAttr.Checked;
    this.EnableDisable(this.panOpParms4);
  }

  private bool ChooseAttr(bool mod, ButtonEdit ed)
  {
    this.selAttrGUID = "";
    this.selAttrName = "";
    if (this.ASF.ShowDialog() != DialogResult.OK || this.ASF.SelectedAttributesGuid.Count <= 0)
      return false;
    Guid anAttributeGuid = this.ASF.SelectedAttributesGuid[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
      str = !(attributeType.ShortName != "") ? attributeType.Name : attributeType.ShortName;
    }
    if (mod)
    {
      this.modData.ForAttrText = str;
      this.modData.ForAttrGUID = Convert.ToString((object) anAttributeGuid);
    }
    else
    {
      this.selAttrGUID = Convert.ToString((object) anAttributeGuid);
      this.selAttrName = str;
    }
    if (ed != null)
      ed.Text = str;
    return true;
  }

  private void btnForAttr_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.ASF.ShowDialog() != DialogResult.OK || this.ASF.SelectedAttributesGuid.Count <= 0)
      return;
    Guid anAttributeGuid = this.ASF.SelectedAttributesGuid[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
      str = !(attributeType.ShortName != "") ? attributeType.Name : attributeType.ShortName;
    }
    this.modData.ForAttrText = str;
    this.modData.ForAttrGUID = Convert.ToString((object) anAttributeGuid);
    this.btnForAttr.Text = str;
    this.modDataChanged = true;
  }

  private void textSortAttr_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
  }

  private void textAttr2_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
  }

  private void editAddAttr_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.ASF.ShowDialog() != DialogResult.OK || this.ASF.SelectedAttributesGuid.Count <= 0)
      return;
    Guid anAttributeGuid = this.ASF.SelectedAttributesGuid[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
      str = !(attributeType.ShortName != "") ? attributeType.Name : attributeType.ShortName;
    }
    this.opData.st3 = str;
    this.opData.s4 = Convert.ToString((object) anAttributeGuid);
    (sender as ButtonEdit).Text = str;
    this.opDataChanged = true;
  }

  private void edSaveNewID_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.ASF.ShowDialog() != DialogResult.OK || this.ASF.SelectedAttributesGuid.Count <= 0)
      return;
    Guid anAttributeGuid = this.ASF.SelectedAttributesGuid[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
      str = !(attributeType.ShortName != "") ? attributeType.Name : attributeType.ShortName;
    }
    this.opData.st2 = str;
    this.opData.s3 = Convert.ToString((object) anAttributeGuid);
    this.edSaveNewID.Text = str;
    this.opDataChanged = true;
  }

  private void button19_Click(object sender, EventArgs e)
  {
    if (!(this.opData.s3 != ""))
      return;
    this.opData.st2 = "";
    this.opData.s3 = "";
    this.edSaveNewID.Text = "";
    this.opDataChanged = true;
  }

  private void btnAddSortAttr_Click(object sender, EventArgs e)
  {
    this.modData.ForAttrGUID = "";
    if (!this.ChooseAttr(true, (ButtonEdit) null) || this.modData.ForAttrGUID == "")
      return;
    bool flag = sender == this.btnAddLink;
    for (int index = 0; index < this.modData.sortGUIDs.Count; ++index)
    {
      if (this.modData.sortGUIDs[index].Equals(this.modData.ForAttrGUID))
      {
        bool sortCheck = this.modData.sortChecks[index];
        if (flag & sortCheck || !flag && !sortCheck)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_326"), LocalizationHolder.rm.GetString("Expert.Editor_327"));
          return;
        }
      }
    }
    try
    {
      TreeNode node1 = this.tvAttrs.Nodes[0];
      TreeNode node2 = this.tvAttrs.Nodes[1];
      TreeNode node3 = new TreeNode(this.modData.ForAttrText);
      if (flag)
        node2.Nodes.Add(node3);
      else
        node1.Nodes.Add(node3);
      node3.ImageIndex = 61;
      node3.SelectedImageIndex = 61;
      this.modData.sortTexts.Add(this.modData.ForAttrText);
      this.modData.sortGUIDs.Add(this.modData.ForAttrGUID);
      this.modData.sortChecks.Add(flag);
      this.tvAttrs.ExpandAll();
    }
    finally
    {
      this.lockChanged = false;
    }
    this.modDataChanged = true;
  }

  private void btnDelSortAttr_Click(object sender, EventArgs e)
  {
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.modData.ForAttrGUID = "";
    if (!this.ChooseAttr(true, (ButtonEdit) null) || this.modData.ForAttrGUID == "")
      return;
    if (sender == this.btnAddSort || sender == this.btnAddSortLink)
    {
      bool flag = sender == this.btnAddSortLink;
      for (int index = 0; index < this.modData.sortGUIDs.Count; ++index)
      {
        if (this.modData.sortGUIDs[index] == this.modData.ForAttrGUID)
        {
          bool sortCheck = this.modData.sortChecks[index];
          if (flag & sortCheck || !flag && !sortCheck)
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_328"), LocalizationHolder.rm.GetString("Expert.Editor_329"));
            return;
          }
        }
      }
      try
      {
        TreeNode node1 = this.tvSortGroup.Nodes[0].Nodes[0];
        TreeNode node2 = this.tvSortGroup.Nodes[0].Nodes[1];
        TreeNode node3 = new TreeNode(this.modData.ForAttrText);
        if (flag)
          node2.Nodes.Add(node3);
        else
          node1.Nodes.Add(node3);
        node3.ImageIndex = 32 /*0x20*/;
        node3.SelectedImageIndex = 32 /*0x20*/;
        this.modData.sortTexts.Add(this.modData.ForAttrText);
        this.modData.sortGUIDs.Add(this.modData.ForAttrGUID);
        this.modData.sortChecks.Add(flag);
      }
      finally
      {
        this.lockChanged = false;
      }
    }
    else
    {
      bool flag = sender == this.btnAddGroupLink;
      for (int index = 0; index < this.modData.groupGUIDs.Count; ++index)
      {
        if (this.modData.groupGUIDs[index] == this.modData.ForAttrGUID)
        {
          bool groupCheck = this.modData.groupChecks[index];
          if (flag & groupCheck || !flag && !groupCheck)
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_330"), LocalizationHolder.rm.GetString("Expert.Editor_331"));
            return;
          }
        }
      }
      try
      {
        TreeNode node4 = this.tvSortGroup.Nodes[1].Nodes[0];
        TreeNode node5 = this.tvSortGroup.Nodes[1].Nodes[1];
        TreeNode node6 = new TreeNode(this.modData.ForAttrText);
        if (flag)
          node5.Nodes.Add(node6);
        else
          node4.Nodes.Add(node6);
        node6.ImageIndex = 32 /*0x20*/;
        node6.SelectedImageIndex = 32 /*0x20*/;
        this.modData.groupTexts.Add(this.modData.ForAttrText);
        this.modData.groupGUIDs.Add(this.modData.ForAttrGUID);
        this.modData.groupChecks.Add(flag);
      }
      finally
      {
        this.lockChanged = false;
      }
    }
    this.tvObjAttrs.ExpandAll();
    this.modDataChanged = true;
  }

  private void button1_Click(object sender, EventArgs e)
  {
  }

  private void menuItem1_Click(object sender, EventArgs e)
  {
  }

  private void menuItem2_Click(object sender, EventArgs e)
  {
  }

  private void spinEdit1_EditValueChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.startValue = Convert.ToInt32(this.spinEdit1.Value);
    this.modDataChanged = true;
  }

  private void opAttrObjType_Changed(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    SelObjAttrControl selObjAttrControl = sender as SelObjAttrControl;
    if (selObjAttrControl.selAttr != null)
    {
      this.opData.s1 = selObjAttrControl.selAttr.GUID;
      this.opData.st1 = selObjAttrControl.attrText;
    }
    else
    {
      this.opData.s1 = "";
      this.opData.st1 = "";
    }
    if (selObjAttrControl.selObjType != null && !selObjAttrControl.NoObjType)
    {
      this.opData.s2 = selObjAttrControl.selObjType.GUID;
      this.opData.st2 = selObjAttrControl.objTypeText;
    }
    else
    {
      this.opData.s2 = "";
      this.opData.st2 = "";
    }
    this.opData.attrType = selObjAttrControl.attrType;
    this.opDataChanged = true;
  }

  private void settAttrObjType_Changed(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (this.opData.attrType == FieldTypes.ftUnknown)
    {
      this.richFormula.Clear();
      this.opData.tf.Clear();
      this.richFormula.Enabled = false;
    }
    else
    {
      if (this.opData.tf.Count > 0 && this.opData.tf.resType != DataTypeConvertor.AttrType2DataType(this.opData.attrType))
      {
        this.opData.tf.Clear();
        this.richFormula.Clear();
      }
      this.richFormula.Enabled = true;
    }
  }

  private void checkVal_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.settingMod = this.comboSelector.SelectedIndex;
    this.opDataChanged = true;
    this.EnableDisable(this.panOpParms3);
  }

  private void checkDups_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.checkDups.Checked;
    this.opDataChanged = true;
  }

  private void btnAddDAttr_Click(object sender, EventArgs e)
  {
    if (!this.ChooseAttr(false, (ButtonEdit) null) || this.selAttrName == "")
      return;
    bool flag = sender == this.btnAddOpLink || sender == this.button10 || sender == this.button8;
    for (int index = 0; index < this.opData.dA_GUIDs.Count; ++index)
    {
      if (this.opData.dA_GUIDs[index] == this.selAttrGUID)
      {
        bool attrCheck = this.GetAttrCheck(index);
        if (flag & attrCheck || !flag && !attrCheck)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_332"), LocalizationHolder.rm.GetString("Expert.Editor_333"));
          return;
        }
      }
    }
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(this.selAttrGUID));
    if (this.selAttrGUID != "cad0004b-306c-11d8-b4e9-00304f19f545" && (attributeType.FieldType == FieldTypes.ftFile || attributeType.FieldType == FieldTypes.ftBlob))
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_696"), LocalizationHolder.rm.GetString("Expert.Editor_333"));
    }
    else
    {
      this.lockChanged = true;
      try
      {
        TreeView treeView = this.tvObjAttrs;
        if (sender == this.button9 || sender == this.button8)
          treeView = this.tvGTAttrs;
        if (sender == this.button12 || sender == this.button10)
          treeView = this.tvGlobCommonAttrs;
        TreeNode node1 = treeView.Nodes[0];
        TreeNode node2 = treeView.Nodes[1];
        TreeNode node3 = new TreeNode(this.selAttrName);
        if (flag)
          node2.Nodes.Add(node3);
        else
          node1.Nodes.Add(node3);
        node3.ImageIndex = 61;
        node3.SelectedImageIndex = 61;
        this.opData.dA_Texts.Add(this.selAttrName);
        this.opData.dA_GUIDs.Add(this.selAttrGUID);
        this.opData.dA_Checks.Add(flag ? "Y" : "N");
        treeView.ExpandAll();
      }
      finally
      {
        this.lockChanged = false;
      }
      this.opDataChanged = true;
    }
  }

  private void btnDelDAttr_Click(object sender, EventArgs e)
  {
    TreeView treeView = this.tvObjAttrs;
    if (sender == this.button7)
      treeView = this.tvGTAttrs;
    if (sender == this.button6)
      treeView = this.tvGlobCommonAttrs;
    TreeNode selectedNode = treeView.SelectedNode;
    if (selectedNode == null || selectedNode.Parent == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_334"), LocalizationHolder.rm.GetString("Expert.Editor_335"));
    }
    else
    {
      bool flag = selectedNode.Parent == treeView.Nodes[1];
      for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
      {
        if (this.opData.dA_Texts[index] == selectedNode.Text && this.GetAttrCheck(index) == flag)
        {
          this.opData.dA_GUIDs.RemoveAt(index);
          this.opData.dA_Texts.RemoveAt(index);
          this.opData.dA_Checks.RemoveAt(index);
          treeView.Nodes.Remove(selectedNode);
          break;
        }
      }
      this.opDataChanged = true;
    }
  }

  private void checkEdit1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.EnableDisable(this.panOpParms5);
    if (!this.checkEditAdd.Checked)
    {
      this.opData.s4 = "";
      this.opData.st3 = "";
      this.editAddAttr.Text = "";
    }
    this.opDataChanged = true;
  }

  private void checkAddId_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.EnableDisable(this.panOpParms4);
    if (!this.checkAddId.Checked)
    {
      this.opData.s4 = "";
      this.opData.st3 = "";
      this.edAddAttr.Text = "";
    }
    this.opDataChanged = true;
  }

  private void btnEdExcerpt_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_336"), LocalizationHolder.rm.GetString("Expert.Editor_337"), ExpertConsts.Consts.objESExceprt, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    if (objectID == this.opData.exID)
      return;
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      str = sessionKeeper.Session.GetObject(objectID).Caption;
    this.opData.exID = objectID;
    this.opData.st1 = str;
    ((Control) sender).Text = str;
    this.opDataChanged = true;
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (this.opData.exID != 0L)
    {
      this.opData.exID = 0L;
      this.opData.st1 = "";
      this.opDataChanged = true;
    }
    if (sender == this.btnGlobExcClear)
      this.beGlobExcerpt.Text = "";
    else if (sender == this.btnGTExcClear)
      this.beGTExcerpt.Text = "";
    else
      this.btnEdExcerpt.Text = "";
  }

  private void textId_TextChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.s3 = this.textId.Text;
    this.opDataChanged = true;
  }

  private void textBox1_TextChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.s1 = this.textBox1.Text;
    this.opDataChanged = true;
  }

  private void comboDivider_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    switch (this.comboDivider.SelectedIndex)
    {
      case 0:
        this.opData.s3 = ",";
        break;
      case 1:
        this.opData.s3 = ", ";
        break;
      case 2:
        this.opData.s3 = ";";
        break;
      case 3:
        this.opData.s3 = "; ";
        break;
      case 4:
        this.opData.s3 = " ";
        break;
      default:
        this.opData.s3 = "";
        break;
    }
    this.opDataChanged = true;
  }

  private void textId_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    string str = this.showTemp.Execute(this.opData.s3);
    if (!(str != ""))
      return;
    this.textId.Text = str;
    this.opData.s3 = str;
    this.opData.st4 = this.showTemp.selName;
    this.buttonEdit1.Text = this.opData.st4;
    this.opDataChanged = true;
  }

  private void textBox1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    string str = this.showTemp.Execute(this.opData.s1);
    if (!(str != ""))
      return;
    this.textBox1.Text = str;
    this.opData.s1 = str;
    this.opData.st4 = this.showTemp.selName;
    this.buttonEdit2.Text = this.opData.st4;
    this.opDataChanged = true;
  }

  private void checkFillDefault_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = this.checkFillDefault.Checked;
    this.opDataChanged = true;
  }

  private void checkMakeCurrent_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.checkMakeCurrent.Checked;
    if (!this.opData.b1)
      this.cbCurrentForever.Checked = false;
    this.opDataChanged = true;
    this.EnableDisable(this.panOpParms5);
  }

  private void checkSelFromTempl_CheckedChanged(object sender, EventArgs e)
  {
    this.EnableDisable(this.panOpParms6);
    if (this.checkSelFromTempl.Checked)
    {
      this.opData.s1 = this.edSelTemplate.Text;
      this.opData.tf.Clear();
      this.richTextID.Text = "";
    }
    else
    {
      this.opData.s1 = "";
      this.edSelTemplate.Text = "";
    }
  }

  private void edSelTemplate_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    string str = this.showTemp.Execute(this.opData.s1);
    if (!(str != ""))
      return;
    this.edSelTemplate.Text = str;
    this.buttonEdit3.Text = this.showTemp.selName;
    this.opData.s1 = str;
    this.opData.st3 = this.showTemp.selName;
    this.opDataChanged = true;
  }

  private void lbDataAttrs_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
  {
    if (this.lockChanged)
      return;
    string dAGuiD = this.opData.dA_GUIDs[e.Index];
    for (int index = 0; index < this.opData.dA_GUIDs.Count; ++index)
    {
      if (index != e.Index && this.opData.dA_GUIDs[index] == dAGuiD)
      {
        e.NewValue = e.CurrentValue;
        return;
      }
    }
    this.opData.dA_Checks[e.Index] = e.NewValue == CheckState.Checked ? "Y" : "N";
    this.opDataChanged = true;
  }

  private void lbSort_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
  {
    if (this.lockChanged)
      return;
    string sortGuiD = this.modData.sortGUIDs[e.Index];
    for (int index = 0; index < this.modData.sortGUIDs.Count; ++index)
    {
      if (index != e.Index && this.modData.sortGUIDs[index] == sortGuiD)
      {
        e.NewValue = e.CurrentValue;
        return;
      }
    }
    this.modData.sortChecks[e.Index] = e.NewValue == CheckState.Checked;
    this.modDataChanged = true;
  }

  private void lbGroup_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
  {
    if (this.lockChanged)
      return;
    string groupGuiD = this.modData.groupGUIDs[e.Index];
    for (int index = 0; index < this.modData.groupGUIDs.Count; ++index)
    {
      if (index != e.Index && this.modData.groupGUIDs[index] == groupGuiD)
      {
        e.NewValue = e.CurrentValue;
        return;
      }
    }
    this.modData.groupChecks[e.Index] = e.NewValue == CheckState.Checked;
    this.modDataChanged = true;
  }

  private void button4_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_338"), typeof (ObjectTypeFolder), true);
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    for (int index = 0; index < this.opData.objGUIDs.Count; ++index)
    {
      idList.Add((object) Convert.ToInt32(this.opData.objGUIDs[index]));
      typeList.Add((object) typeof (ObjectTypeFolder));
    }
    selectorForm.SelectFocusedWhenNothingMultiselected = false;
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    this.opData.objGUIDs.Clear();
    this.opData.objTexts.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        int id = (int) selectorForm.IDList[index];
        this.opData.objGUIDs.Add(Convert.ToString(id));
        this.opData.objTexts.Add(sessionKeeper.Session.GetObjectType(id).ObjectTypeName);
      }
    }
    this.ShowSelLinks();
    this.opDataChanged = true;
    this.tvTypes.ExpandAll();
  }

  private void ShowSelLinks()
  {
    TreeNode node1 = this.tvTypes.Nodes[0];
    TreeNode node2 = this.tvTypes.Nodes[1];
    node1.Nodes.Clear();
    node2.Nodes.Clear();
    for (int index = 0; index < this.opData.linkTexts.Count; ++index)
    {
      TreeNode node3 = new TreeNode(this.opData.linkTexts[index]);
      node2.Nodes.Add(node3);
      node3.ImageIndex = 52;
      node3.SelectedImageIndex = 52;
    }
    for (int index = 0; index < this.opData.objTexts.Count; ++index)
    {
      TreeNode node4 = new TreeNode(this.opData.objTexts[index]);
      node1.Nodes.Add(node4);
      node4.ImageIndex = 51;
      node4.SelectedImageIndex = 51;
    }
    this.tvTypes.ExpandAll();
  }

  private void edObjType_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_339"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    int id = (int) selectorForm.IDList[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id);
      this.opData.s1 = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
      this.opData.s2 = objectType.PropertiesStructure.ObjectTypeName;
      if (sender is ButtonEdit)
        (sender as ButtonEdit).Text = this.opData.s2;
    }
    this.opDataChanged = true;
  }

  private void setAttr_Changed(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opDataChanged = true;
  }

  private void btnAddSortLink_Click(object sender, EventArgs e)
  {
  }

  private void btnAddGroupLink_Click(object sender, EventArgs e)
  {
  }

  private void btnSortDel_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvSortGroup.SelectedNode;
    if (selectedNode == null || selectedNode.Parent == null || selectedNode.Parent.Parent == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_340"), LocalizationHolder.rm.GetString("Expert.Editor_341"));
    }
    else
    {
      bool flag = selectedNode.Parent == selectedNode.Parent.Parent.Nodes[1];
      if (selectedNode.Parent.Parent != this.tvSortGroup.Nodes[1])
      {
        for (int index = 0; index < this.modData.sortTexts.Count; ++index)
        {
          if (this.modData.sortTexts[index] == selectedNode.Text && this.modData.sortChecks[index] == flag)
          {
            this.modData.sortGUIDs.RemoveAt(index);
            this.modData.sortTexts.RemoveAt(index);
            this.modData.sortChecks.RemoveAt(index);
            selectedNode.Parent.Nodes.Remove(selectedNode);
            break;
          }
        }
      }
      else
      {
        for (int index = 0; index < this.modData.groupTexts.Count; ++index)
        {
          if (this.modData.groupTexts[index] == selectedNode.Text && this.modData.groupChecks[index] == flag)
          {
            this.modData.groupGUIDs.RemoveAt(index);
            this.modData.groupTexts.RemoveAt(index);
            this.modData.groupChecks.RemoveAt(index);
            selectedNode.Parent.Nodes.Remove(selectedNode);
            break;
          }
        }
      }
      this.modDataChanged = true;
    }
  }

  private void btnDelGroup_Click(object sender, EventArgs e)
  {
  }

  private void lbSort_DrawItem(object sender, DrawItemEventArgs e)
  {
  }

  private void lbGroup_DrawItem(object sender, DrawItemEventArgs e)
  {
  }

  private void lbAttrs_DrawItem(object sender, DrawItemEventArgs e)
  {
  }

  private void btnAddLink_Click(object sender, EventArgs e)
  {
  }

  private void btnDelAttr_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvAttrs.SelectedNode;
    if (selectedNode == null || selectedNode.Parent == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_342"), LocalizationHolder.rm.GetString("Expert.Editor_343"));
    }
    else
    {
      bool flag = selectedNode.Parent == this.tvAttrs.Nodes[1];
      for (int index = 0; index < this.modData.sortTexts.Count; ++index)
      {
        if (this.modData.sortTexts[index] == selectedNode.Text && this.modData.sortChecks[index] == flag)
        {
          this.modData.sortGUIDs.RemoveAt(index);
          this.modData.sortTexts.RemoveAt(index);
          this.modData.sortChecks.RemoveAt(index);
          this.tvAttrs.Nodes.Remove(selectedNode);
          break;
        }
      }
      this.modDataChanged = true;
    }
  }

  private void lbDataAttrs_DrawItem(object sender, DrawItemEventArgs e)
  {
  }

  private void btnCond_Click(object sender, EventArgs e)
  {
    if (this.cond == null)
      this.cond = new TempFormula(DataType.Boolean, true);
    this.formEd.CanReturnEmpty = true;
    if (!this.formEd.Execute(ref this.cond, LocalizationHolder.rm.GetString("Expert.Editor_344")))
      return;
    this.condChanged = this.formEd.Changed;
    if (!this.condChanged)
      return;
    this.scriptChanged = true;
    this.UpdateSaveCancelButtons();
  }

  private void lbRecalcAttrs_DrawItem(object sender, DrawItemEventArgs e)
  {
  }

  private void btnAddRecalcAttr_Click(object sender, EventArgs e)
  {
    if (!this.ChooseAttr(false, (ButtonEdit) null) || this.selAttrName == "")
      return;
    bool flag = sender == this.btnAddRecalcLink;
    for (int index = 0; index < this.opData.dA_GUIDs.Count; ++index)
    {
      if (this.opData.dA_GUIDs[index] == this.selAttrGUID)
      {
        bool attrCheck = this.GetAttrCheck(index);
        if (flag & attrCheck || !flag && !attrCheck)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_345"), LocalizationHolder.rm.GetString("Expert.Editor_346"));
          return;
        }
      }
    }
    this.lockChanged = true;
    try
    {
      TreeNode node1 = this.tvRecalcAttrs.Nodes[0];
      TreeNode node2 = this.tvRecalcAttrs.Nodes[1];
      TreeNode node3 = new TreeNode(this.selAttrName);
      if (flag)
        node2.Nodes.Add(node3);
      else
        node1.Nodes.Add(node3);
      node3.ImageIndex = 32 /*0x20*/;
      node3.SelectedImageIndex = 32 /*0x20*/;
      this.opData.dA_Texts.Add(this.selAttrName);
      this.opData.dA_GUIDs.Add(this.selAttrGUID);
      this.opData.dA_Checks.Add(flag ? "Y" : "N");
      this.tvRecalcAttrs.ExpandAll();
    }
    finally
    {
      this.lockChanged = false;
    }
    this.opDataChanged = true;
  }

  private void btnAddRecalcLink_Click(object sender, EventArgs e)
  {
  }

  private void btnDelRecalcAttr_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvRecalcAttrs.SelectedNode;
    if (selectedNode == null || selectedNode.Parent == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_347"), LocalizationHolder.rm.GetString("Expert.Editor_348"));
    }
    else
    {
      bool flag = selectedNode.Parent == this.tvRecalcAttrs.Nodes[1];
      for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
      {
        if (this.opData.dA_Texts[index] == selectedNode.Text && this.GetAttrCheck(index) == flag)
        {
          this.opData.dA_GUIDs.RemoveAt(index);
          this.opData.dA_Texts.RemoveAt(index);
          this.opData.dA_Checks.RemoveAt(index);
          this.tvRecalcAttrs.Nodes.Remove(selectedNode);
          break;
        }
      }
      this.opDataChanged = true;
    }
  }

  private void buttonEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
  }

  private void buttonEdit1_Properties_ButtonClick_1(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_349"), LocalizationHolder.rm.GetString("Expert.Editor_350"), ExpertConsts.Consts.objExcerpt, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    if (objectID == this.opData.exID)
      return;
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      str = sessionKeeper.Session.GetObject(objectID).Caption;
    this.opData.exID = objectID;
    this.opData.st1 = str;
    this.opDataChanged = true;
  }

  private void btnDelExcerpt_Click(object sender, EventArgs e)
  {
    if (this.opData.exID == 0L)
      return;
    this.opData.exID = 0L;
    this.opData.st1 = "";
    this.opDataChanged = true;
  }

  private void btnAddLinkType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_351"), typeof (RelationTypeFolder), true);
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    for (int index = 0; index < this.opData.linkIDs.Count; ++index)
    {
      idList.Add((object) this.opData.linkIDs[index]);
      typeList.Add((object) typeof (RelationTypeFolder));
    }
    selectorForm.SelectFocusedWhenNothingMultiselected = false;
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    this.opData.linkIDs.Clear();
    this.opData.linkTexts.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectorForm.IDList.Count; ++index)
      {
        int id = (int) selectorForm.IDList[index];
        this.opData.linkIDs.Add(id);
        this.opData.linkTexts.Add(sessionKeeper.Session.GetRelationType(id).Description);
      }
    }
    this.ShowSelLinks();
    this.opDataChanged = true;
    this.tvTypes.ExpandAll();
  }

  private void button1_Click_1(object sender, EventArgs e)
  {
    long objectByTypeDialog = ((IObjectCreatorService) ServicesManager.GetService(typeof (IObjectCreatorService))).CreateObjectByTypeDialog(ExpertConsts.Consts.objESExceprt);
    if (objectByTypeDialog == -1L)
      return;
    using (Form form = new Form())
    {
      Intermech.Navigator.SelectionView.SelectionView selectionView = new Intermech.Navigator.SelectionView.SelectionView();
      form.Controls.Add((Control) selectionView);
      selectionView.Dock = DockStyle.Fill;
      ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objectByTypeDialog);
      selectionView.Initialize(items, (System.IServiceProvider) ServicesManager.ServiceContainer);
      selectionView.Activate((IView) null);
      form.StartPosition = FormStartPosition.CenterScreen;
      form.Size = new Size(600, 400);
      int num = (int) form.ShowDialog();
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.opData.st1 = sessionKeeper.Session.GetObject(objectByTypeDialog).Caption;
      this.opData.exID = objectByTypeDialog;
      if (sender == this.btnGlobExcCreate)
        this.beGlobExcerpt.Text = this.opData.st1;
      else if (sender == this.btnGTExcCreate)
        this.beGTExcerpt.Text = this.opData.st1;
      else
        this.btnEdExcerpt.Text = this.opData.st1;
    }
    this.opDataChanged = true;
  }

  private void beUserProc_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    switch (Convert.ToInt32(this.opData.s2))
    {
      case 0:
        SelectTree selectTree = new SelectTree();
        if (!selectTree.Execute(this.tree.Nodes) || !(this.opData.s1 != selectTree.selLabel))
          break;
        this.beUserProc.Text = selectTree.selLabel;
        this.opData.s1 = selectTree.selLabel;
        TreeListNode nodeById = this.tree.FindNodeByID(selectTree.selNodeId);
        if ((string) nodeById[(object) 0] != selectTree.selLabel)
          nodeById[(object) 0] = (object) selectTree.selLabel;
        this.opDataChanged = true;
        break;
      case 1:
        string str = new SelUserProc().Execute(false);
        if (!(str != ""))
          break;
        this.beUserProc.Text = str;
        if (!(this.opData.s1 != str))
          break;
        this.opData.s1 = str;
        this.opDataChanged = true;
        break;
      case 2:
        long[] numArray1 = this.ObjectType == ExpertScriptType.DocScript ? Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_352"), LocalizationHolder.rm.GetString("Expert.Editor_353"), ExpertConsts.Consts.objDocScript, SelectionOptions.Default) : Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_616"), LocalizationHolder.rm.GetString("Expert.Editor_617"), ExpertConsts.Consts.objScript, SelectionOptions.Default);
        if (numArray1 == null || numArray1.Length == 0)
          break;
        long objectID1 = numArray1[0];
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID1);
          this.beUserProc.Text = dbObject.Caption;
          this.opData.s1 = dbObject.Caption;
        }
        this.opData.exID = (long) this.ObjectType;
        this.opDataChanged = true;
        break;
      case 3:
        long[] numArray2 = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_618"), LocalizationHolder.rm.GetString("Expert.Editor_619"), ExpertConsts.Consts.objExpScenario, SelectionOptions.Default);
        if (numArray2 == null || numArray2.Length == 0)
          break;
        long objectID2 = numArray2[0];
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID2);
          this.beUserProc.Text = dbObject.Caption;
          this.opData.s1 = dbObject.Caption;
        }
        this.opDataChanged = true;
        break;
    }
  }

  private void cbSelectDoc_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.lockChanged = true;
    try
    {
      this.opData.b1 = this.cbSelectDoc.Checked;
      this.opDataChanged = true;
      this.EnableDisable(this.panOpParms6);
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void radioButton1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = this.radioButton2.Checked;
    this.opDataChanged = true;
  }

  private void cbDocField_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.rbDocField.Checked;
    this.opDataChanged = true;
  }

  private void edVersionRule_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = VersionRulesSelectionForm.Execute(VersionRulesSelectFilter.vrfExcludeVariableRules, false, "");
    if (numArray == null)
      return;
    long objectID = numArray[0];
    string str1 = "";
    string str2 = "";
    if (objectID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
        this.edVersionRule.Text = dbObject.Caption;
        str2 = dbObject.Caption;
        str1 = dbObject.GUID.ToString();
      }
    }
    else
    {
      str2 = LocalizationHolder.rm.GetString("Expert.Editor_592");
      this.edVersionRule.Text = str2;
      str1 = Guid.Empty.ToString();
    }
    this.opData.exID = objectID;
    this.opData.s1 = str1;
    this.opData.s2 = str2;
    this.opDataChanged = true;
  }

  private void cbNewPage_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.rbNewPage.Checked;
    this.opDataChanged = true;
    this.EnableDisable(this.panOpParmsC);
  }

  private void beNewList_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    string str = this.showTemp.Execute(this.opData.s1);
    if (!(str != ""))
      return;
    this.edSelTemplate.Text = str;
    this.beNewList.Text = this.showTemp.selName != "" ? this.showTemp.selName : str;
    this.beNewList.Text = this.showTemp.selName;
    this.opData.s1 = str;
    this.opData.s2 = this.showTemp.selName;
    this.opDataChanged = true;
  }

  private void tvObjAttrs_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.btnToggleSort.Enabled = e.Node.Parent != null;
    if (!this.btnToggleSort.Enabled)
      return;
    this.btnToggleSort.ImageIndex = this.NextNodeIndex(e.Node);
    this.UpdateToggleTooltip();
  }

  private char NodeIndex2Code(TreeNode node)
  {
    int imageIndex = node.ImageIndex;
    switch (imageIndex)
    {
      case 45:
        return 'a';
      case 46:
        return 'd';
      default:
        int num = imageIndex - 61;
        return ' ';
    }
  }

  private int NextNodeIndex(TreeNode node)
  {
    switch (node.ImageIndex)
    {
      case -1:
      case 61:
      case 62:
        return 45;
      case 45:
        return 46;
      case 46:
        return 62;
      default:
        return 62;
    }
  }

  private void UpdateToggleTooltip()
  {
    switch (this.btnToggleSort.ImageIndex)
    {
      case -1:
      case 62:
        this.tipCon.SetToolTip((Control) this.btnToggleSort, LocalizationHolder.rm.GetString("Expert.Editor_354"));
        break;
      case 45:
        this.tipCon.SetToolTip((Control) this.btnToggleSort, LocalizationHolder.rm.GetString("Expert.Editor_355"));
        break;
      case 46:
        this.tipCon.SetToolTip((Control) this.btnToggleSort, LocalizationHolder.rm.GetString("Expert.Editor_356"));
        break;
    }
  }

  private void btnToggleSort_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvObjAttrs.SelectedNode;
    if (selectedNode == null)
      return;
    int index = 0;
    bool flag = selectedNode.Parent == this.tvObjAttrs.Nodes[1];
    while (index < this.opData.dA_Texts.Count && (!(selectedNode.Text == this.opData.dA_Texts[index]) || this.GetAttrCheck(index) ^ flag))
      ++index;
    if (index >= this.opData.dA_Texts.Count)
      return;
    int num = this.NextNodeIndex(selectedNode);
    selectedNode.ImageIndex = num == 62 ? 61 : num;
    selectedNode.SelectedImageIndex = num == 62 ? 61 : num;
    this.opData.dA_Checks[index] = this.opData.dA_Checks[index].Substring(0, 1) + this.NodeIndex2Code(selectedNode).ToString();
    this.btnToggleSort.ImageIndex = this.NextNodeIndex(selectedNode);
    this.UpdateToggleTooltip();
    this.opDataChanged = true;
  }

  private void cbOnlyCurrent_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.EnableDisable(this.panOpParms1);
    if (!((RadioButton) sender).Checked)
      return;
    GlobalData globalData = GlobalData.globalNone;
    if (sender == this.rbGlobalPlus)
      globalData = GlobalData.globalAdd;
    if (sender == this.rbGlobalMul)
      globalData = GlobalData.globalMult;
    this.opData.s2 = Convert.ToString((int) globalData);
    this.opDataChanged = true;
  }

  private void cbKeepData_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged || !((RadioButton) sender).Checked)
      return;
    GlobalSave globalSave = GlobalSave.saveNone;
    if (sender == this.rbSaveClear)
      globalSave = GlobalSave.saveClear;
    if (sender == this.rbSaveLocal)
      globalSave = GlobalSave.saveSet;
    if (sender == this.rbSaveAdd)
      globalSave = GlobalSave.saveAdd;
    this.opData.s4 = Convert.ToString((int) globalSave);
    this.opDataChanged = true;
    this.EnableDisable(this.panOpParms1);
  }

  private void cbAddThis_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b4 = this.cbAddThis.Checked;
    this.opDataChanged = true;
  }

  private void SetOpDataChanged(bool newDataChanged)
  {
    this._opDataChanged = newDataChanged;
    string str = LocalizationHolder.rm.GetString("Expert.Editor_357");
    if (this._opDataChanged)
    {
      this.dockOpParms.Text = str + LocalizationHolder.rm.GetString("Expert.Editor_358");
      if (!this.btnSave.Enabled)
      {
        this.scriptChanged = true;
        this.btnSave.Enabled = true;
      }
    }
    else
      this.dockOpParms.Text = str;
    this.EnableButtons();
    this.EnableBtnDocParms();
  }

  private void SetModDataChanged(bool newDataChanged)
  {
    this._modDataChanged = newDataChanged;
    string str = LocalizationHolder.rm.GetString("Expert.Editor_359");
    if (this._modDataChanged)
    {
      this.dockModParms.Text = str + LocalizationHolder.rm.GetString("Expert.Editor_360");
      if (!this.btnSave.Enabled)
      {
        this.scriptChanged = true;
        this.btnSave.Enabled = true;
      }
    }
    else
      this.dockModParms.Text = str;
    this.EnableButtons();
  }

  internal bool opDataChanged
  {
    get => this._opDataChanged;
    set => this.SetOpDataChanged(value);
  }

  internal bool modDataChanged
  {
    get => this._modDataChanged;
    set => this.SetModDataChanged(value);
  }

  private void beCompareFunc_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    string str = new SelUserProc().Execute(true);
    this.beCompareFunc.Text = str;
    if (!(this.opData.s1 != str))
      return;
    this.opData.s1 = str;
    this.opDataChanged = true;
  }

  private void cbProc_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged || !(sender as RadioButton).Checked)
      return;
    ExpertCalling expertCalling = ExpertCalling.callProc;
    if (sender == this.cbUserProc)
      expertCalling = ExpertCalling.callUserProc;
    else if (sender == this.cbScript)
      expertCalling = ExpertCalling.callScript;
    else if (sender == this.cbScenario)
      expertCalling = ExpertCalling.callScenario;
    ExpertCalling int32 = (ExpertCalling) Convert.ToInt32(this.opData.s2);
    if (expertCalling == int32)
      return;
    this.beUserProc.Text = "";
    this.opData.s2 = Convert.ToString((int) expertCalling);
    this.opData.s1 = "";
    this.opData.exID = -1L;
    this.opDataChanged = true;
    this.parm1Box.Visible = expertCalling == ExpertCalling.callUserProc || expertCalling == ExpertCalling.callScenario;
    this.parm2Box.Visible = expertCalling == ExpertCalling.callUserProc || expertCalling == ExpertCalling.callScenario;
    this.label40.Visible = expertCalling == ExpertCalling.callUserProc;
    this.label41.Visible = expertCalling == ExpertCalling.callUserProc;
  }

  private void cbByDefault_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.lockChanged = true;
    try
    {
      this.opData.b3 = this.cbByDefault.Checked;
      this.opDataChanged = true;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void cbNoSearch_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.lockChanged = true;
    try
    {
      this.opData.b3 = !this.cbNoSearch.Checked;
      this.opDataChanged = true;
      this.EnableDisable(this.panOpParms1);
      if (this.cbNoSearch.Checked)
        this.cbNoSearch.ForeColor = Color.Red;
      else
        this.cbNoSearch.ForeColor = Color.Black;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void CondEdit_MouseMove(object sender, MouseEventArgs e)
  {
    TempFormula tempFormula = (TempFormula) null;
    if (sender == this.rtbGlobObjFilter || sender == this.rtGTCond)
      tempFormula = this.opData.tf;
    if (sender == this.rtbGlobalCond)
      tempFormula = this.opData.tf2;
    if (sender == this.richTextBox1 || sender == this.richWhileCond || sender == this.richTextBox2)
      tempFormula = this.modData.tf;
    if (sender == this.richTextBox3 || sender == this.richTextBox4 || sender == this.richTextID || sender == this.richSetValue || sender == this.richFormula || sender == this.richTextCond || sender == this.richCond || sender == this.parm1Box)
      tempFormula = this.opData.tf;
    if (sender == this.richGlobalFilter || sender == this.parm2Box)
      tempFormula = this.opData.tf2;
    if (sender == this.richAfterFilter)
      tempFormula = this.opData.tf3;
    if (tempFormula == null)
      return;
    int indexFromPosition = (sender as RichTextBox).GetCharIndexFromPosition(new Point(e.X, e.Y));
    int tokenByPos = tempFormula.GetTokenByPos(indexFromPosition);
    string caption = "";
    if (tokenByPos >= 0)
    {
      Token token = tempFormula[tokenByPos];
      if (token.type == Intermech.Expert.TokenType.Integer && token.text != token.trueText)
        caption = token.trueText;
    }
    if (!(caption != this.toolTip2.GetToolTip((Control) (sender as RichTextBox))))
      return;
    this.toolTip2.SetToolTip((Control) (sender as RichTextBox), caption);
  }

  private void cbSaveRels_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.lockChanged = true;
    try
    {
      this.opData.b2 = this.cbSaveRels.Checked;
      this.opDataChanged = true;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void btnObjLink_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!this.ChooseAttr(false, this.btnObjLink))
      return;
    this.opData.s3 = this.selAttrGUID;
    this.opData.st3 = this.selAttrName;
    this.opDataChanged = true;
  }

  private void btnDelRef_Click(object sender, EventArgs e)
  {
    this.opData.s3 = "";
    this.opData.st3 = "";
    this.opDataChanged = true;
  }

  private void copyToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.modData.tf == null)
      return;
    FormulaEditPlugin.CopyToClipboard(this.modData.tf);
  }

  private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.srcCon == null || !(this.srcCon is RichTextBox) || !Clipboard.ContainsData(TempFormula.FormulaFormat))
      return;
    if (this.modData.tf == null)
      this.modData.tf = new TempFormula(true);
    FormulaEditPlugin.PasteFromClipboard(this.modData.tf);
    this.modData.tf.UpdateTokenBegs();
    this.ShowFormula(this.modData.tf, (RichTextBox) this.srcCon);
    this.modDataChanged = true;
  }

  private void copyOpToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (sender is ToolStripItem)
      this.srcCon = ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl;
    RichTextBox srcCon = (RichTextBox) this.srcCon;
    if (srcCon == this.richGlobalFilter)
    {
      if (this.opData.tf2 == null)
        return;
      FormulaEditPlugin.CopyToClipboard(this.opData.tf2);
    }
    else if (srcCon == this.richAfterFilter)
    {
      if (this.opData.tf3 == null)
        return;
      FormulaEditPlugin.CopyToClipboard(this.opData.tf3);
    }
    else
    {
      if (this.opData.tf == null)
        return;
      FormulaEditPlugin.CopyToClipboard(this.opData.tf);
    }
  }

  private void pasteOpToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (sender is ToolStripItem)
      this.srcCon = ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl;
    if (this.srcCon == null || !(this.srcCon is RichTextBox) || !Clipboard.ContainsData(TempFormula.FormulaFormat))
      return;
    TempFormula tf = new TempFormula(true);
    FormulaEditPlugin.PasteFromClipboard(tf);
    tf.UpdateTokenBegs();
    this.ShowFormula(tf, (RichTextBox) this.srcCon);
    if (this.srcCon == this.richGlobalFilter || this.srcCon == this.richDocCondBefore)
      this.opData.tf2 = tf;
    else if (this.srcCon == this.richAfterFilter)
      this.opData.tf3 = tf;
    else if (this.srcCon == this.rtbGlobalCond)
      this.opData.tf2 = tf;
    else
      this.opData.tf = tf;
    this.opDataChanged = true;
  }

  private void cbSaveContext_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.startValue = this.cbSaveContext.Checked ? 1 : 0;
    this.modDataChanged = true;
  }

  private void cbCreateComplect_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.cbCreateComplect.Checked;
    this.opDataChanged = true;
  }

  private void cbNoEmpty_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.cbNoEmpty.Checked;
    this.opDataChanged = true;
  }

  private void cbSecondPass_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = this.cbSecondPass.Checked;
    this.opDataChanged = true;
  }

  private void textPrefix_TextChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.st1 = this.textPrefix.Text;
    this.opDataChanged = true;
  }

  private void cbActiveLink_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b1 = this.cbActiveLink.Checked;
    this.opDataChanged = true;
  }

  private void cbInbuiltSort_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.st2 = this.cbInbuiltSort.Checked ? "Y" : "N";
    this.opDataChanged = true;
  }

  private void beCompObjType_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_339"), typeof (ObjectTypeFolder), false);
    selectorForm.InitSelectionAsType(new ArrayList()
    {
      (object) ExpertConsts.Consts.objComplect
    }, (ArrayList) null);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    int id = (int) selectorForm.IDList[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id);
      this.opData.s3 = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
      this.opData.s4 = objectType.PropertiesStructure.ObjectTypeName;
      if (sender is ButtonEdit)
        (sender as ButtonEdit).Text = this.opData.s4;
    }
    this.opDataChanged = true;
  }

  private void rbNoIspolns_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender as RadioButton).Checked || this.lockChanged)
      return;
    this.opData.s3 = Convert.ToString((sender as RadioButton).Tag);
    this.opDataChanged = true;
  }

  private void cbForAllIsps_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.ForLoop = this.cbForAllIsps.Checked;
    this.modDataChanged = true;
  }

  private void cbUseCurrentIsps_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.st3 = this.cbUseCurrentIsps.Checked ? "Y" : "N";
    this.opDataChanged = true;
  }

  private void tree_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
  {
    this.draggingNode = e.Node;
    e.CanDrag = !this.modDataChanged && !this.opDataChanged;
  }

  private void cbUseByDef_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b3 = this.cbUseByDef.Checked;
    this.opDataChanged = true;
  }

  private void cbInbSort_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.ForLoop = this.cbInbSort.Checked;
    this.modDataChanged = true;
    this.EnableDisable(this.panModParms3);
  }

  private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (this.tabControl2.SelectedIndex == 0)
      this.srcCon = (Control) this.richSetValue;
    else
      this.srcCon = (Control) this.richLeftIndent;
  }

  private void beFontName_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    if (this.lockChanged)
      return;
    FontStyle style = FontStyle.Regular;
    if (this.cbBold.Checked)
      style |= FontStyle.Bold;
    if (this.cbItalic.Checked)
      style |= FontStyle.Italic;
    if (this.cbUnderline.Checked)
      style |= FontStyle.Underline;
    long emSize = Convert.ToInt64(this.seFontSize.Value);
    if (emSize == 0L)
      emSize = 10L;
    this.fontDialog1.Font = new Font(this.beFontName.Text, (float) emSize, style);
    if (this.fontDialog1.ShowDialog() != DialogResult.OK)
      return;
    this.lockChanged = true;
    try
    {
      this.beFontName.Text = this.fontDialog1.Font.Name;
      this.seFontSize.Value = (Decimal) Convert.ToInt64(this.fontDialog1.Font.SizeInPoints);
      this.cbUnderline.Checked = this.fontDialog1.Font.Underline;
      this.cbBold.Checked = this.fontDialog1.Font.Bold;
      this.cbItalic.Checked = this.fontDialog1.Font.Italic;
      this.opData.s5 = this.beFontName.Text;
      this.opData.exID = Convert.ToInt64(this.seFontSize.Value);
      this.opData.b2 = this.fontDialog1.Font.Bold;
      this.opData.b3 = this.fontDialog1.Font.Italic;
      this.opData.b4 = this.fontDialog1.Font.Underline;
      this.opDataChanged = true;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void seFontSize_Properties_EditValueChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.exID = Convert.ToInt64(this.seFontSize.Value);
    this.opDataChanged = true;
  }

  private void btnClearFont_Click(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.lockChanged = true;
    try
    {
      this.beFontName.Text = "";
      this.seFontSize.Value = 0M;
      this.cbBold.Checked = false;
      this.cbItalic.Checked = false;
      this.cbUnderline.Checked = false;
      this.opData.s5 = "";
      this.opData.exID = 0L;
      this.opData.b2 = false;
      this.opData.b3 = false;
      this.opData.b4 = false;
      this.opDataChanged = true;
    }
    finally
    {
      this.lockChanged = false;
    }
  }

  private void cbBold_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = this.cbBold.Checked;
    this.opData.b3 = this.cbItalic.Checked;
    this.opData.b4 = this.cbUnderline.Checked;
    this.opDataChanged = true;
  }

  private void richSetValue_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.menuChangeOpForm_Click(sender, (EventArgs) e);
  }

  private void treePopMenu_Opening(object sender, CancelEventArgs e) => this.EnableButtons();

  private void cbLinkThisDoc_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b5 = this.cbLinkThisDoc.Checked;
    this.opDataChanged = true;
  }

  private void cbAvoidDup_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b4 = this.cbAvoidDup.Checked;
    this.opDataChanged = true;
  }

  private void tbPostfix_TextChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    string text = this.tbPostfix.Text;
    if (!(text != this.opData.st1))
      return;
    this.opData.st1 = text;
    this.opDataChanged = true;
  }

  private void btnDelReplaceType_Click(object sender, EventArgs e)
  {
    this.opData.s1 = "";
    this.opData.s2 = "";
    this.beReplaceObjType.Text = "";
    this.opDataChanged = true;
  }

  private void btnAddForObjType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_338"), typeof (ObjectTypeFolder), true);
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    for (int index = 0; index < this.opData.forGUIDs.Count; ++index)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid(this.opData.forGUIDs[index]));
      idList.Add((object) objectType.ObjectTypeID);
      typeList.Add((object) typeof (ObjectTypeFolder));
    }
    selectorForm.SelectFocusedWhenNothingMultiselected = false;
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    List<bool> boolList = new List<bool>();
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.opData.forGUIDs.Count; ++index)
      stringList.Add(this.opData.forGUIDs[index]);
    this.opData.forGUIDs.Clear();
    this.opData.forTexts.Clear();
    using (new SessionKeeper())
    {
      for (int index1 = 0; index1 < selectorForm.IDList.Count; ++index1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType((int) selectorForm.IDList[index1]);
        string str = objectType.Guid.ToString();
        this.opData.forGUIDs.Add(str);
        this.opData.forTexts.Add(objectType.ObjectTypeName);
        int index2 = stringList.IndexOf(str);
        if (index2 < 0)
          boolList.Add(false);
        else
          boolList.Add(this.opData.forOT_Only[index2]);
      }
    }
    this.opData.forOT_Only = boolList;
    this.cbForObjTypes.BeginUpdate();
    this.lockChanged = true;
    try
    {
      this.cbForObjTypes.Items.Clear();
      for (int index = 0; index < this.opData.forTexts.Count; ++index)
      {
        this.cbForObjTypes.Items.Add((object) this.opData.forTexts[index]);
        this.cbForObjTypes.SetItemChecked(index, !boolList[index]);
      }
    }
    finally
    {
      this.cbForObjTypes.EndUpdate();
      this.lockChanged = false;
    }
    this.opDataChanged = true;
  }

  private void cbForObjTypes_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
  {
    if (this.lockChanged || e.Index < 0)
      return;
    bool flag = e.NewValue == CheckState.Unchecked;
    if (flag == this.opData.forOT_Only[e.Index])
      return;
    this.opData.forOT_Only[e.Index] = flag;
    this.opDataChanged = true;
  }

  private void button18_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.cbForObjTypes.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.cbForObjTypes.Items.RemoveAt(selectedIndex);
    this.opData.forGUIDs.RemoveAt(selectedIndex);
    this.opData.forTexts.RemoveAt(selectedIndex);
    this.opData.forOT_Only.RemoveAt(selectedIndex);
    this.opDataChanged = true;
  }

  private void globAddLinkDown_Click(object sender, EventArgs e)
  {
    TreeView treeView = this.tvGlobRoot;
    if (sender != this.globAddLinkDown && sender != this.globAddLinkUp)
      treeView = this.tvGTSearch;
    bool flag = true;
    if (sender != this.globAddLinkDown && sender != this.gtAddLinkDown)
      flag = false;
    string description = LocalizationHolder.rm.GetString("Expert.Editor_624");
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), description, typeof (RelationTypeFolder), false);
    selectorForm.SelectFocusedWhenNothingMultiselected = false;
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    int num1 = -1;
    int aRelationTypeID = -1;
    if (selectorForm.IDList.Count > 0)
    {
      aRelationTypeID = (int) selectorForm.IDList[0];
      num1 = aRelationTypeID;
    }
    if (!flag)
      num1 += 100000;
    if (this.opData.linkIDs.Contains(num1))
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_625"));
    }
    else
    {
      if (selectorForm.IDList.Count > 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          description = sessionKeeper.Session.GetRelationType(aRelationTypeID).Description;
      }
      TreeNode node = new TreeNode(description)
      {
        ImageIndex = !flag ? 9 : 10
      };
      node.SelectedImageIndex = node.ImageIndex;
      treeView.Nodes.Add(node);
      this.opData.linkIDs.Add(num1);
      this.opData.linkTexts.Add(description);
      this.opDataChanged = true;
    }
  }

  private void globAddObjType_Click(object sender, EventArgs e)
  {
    TreeView treeView = this.tvGlobRoot;
    if (sender != this.globAddObjType)
      treeView = this.tvGTSearch;
    int index1 = -1;
    if (treeView.SelectedNode != null)
      index1 = treeView.SelectedNode.Parent == null ? treeView.Nodes.IndexOf(treeView.SelectedNode) : treeView.Nodes.IndexOf(treeView.SelectedNode.Parent);
    if (index1 < 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_626"), LocalizationHolder.rm.GetString("Expert.Editor_63"));
    }
    else
    {
      SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_338"), typeof (ObjectTypeFolder), true);
      ArrayList idList = new ArrayList();
      ArrayList typeList = new ArrayList();
      for (int index2 = 0; index2 < this.opData.objGUIDs.Count; ++index2)
      {
        if (this.opData.ltForOT[index2] == index1)
        {
          idList.Add((object) Convert.ToInt32(this.opData.objGUIDs[index2]));
          typeList.Add((object) typeof (ObjectTypeFolder));
        }
      }
      selectorForm.SelectFocusedWhenNothingMultiselected = false;
      selectorForm.OnUncheckActions = SelectorForm.CheckActions.None;
      selectorForm.OnCheckActions = SelectorForm.CheckActions.None;
      selectorForm.InitSelectionAsType(idList, typeList);
      if (selectorForm.ShowDialog() != DialogResult.OK)
        return;
      for (int index3 = this.opData.objGUIDs.Count - 1; index3 >= 0; --index3)
      {
        if (this.opData.ltForOT[index3] == index1)
        {
          this.opData.objGUIDs.RemoveAt(index3);
          this.opData.ltForOT.RemoveAt(index3);
          this.opData.objTexts.RemoveAt(index3);
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        treeView.BeginUpdate();
        treeView.Nodes[index1].Nodes.Clear();
        for (int index4 = 0; index4 < selectorForm.IDList.Count; ++index4)
        {
          int id = (int) selectorForm.IDList[index4];
          this.opData.objGUIDs.Add(Convert.ToString(id));
          string objectTypeName = sessionKeeper.Session.GetObjectType(id).ObjectTypeName;
          this.opData.objTexts.Add(objectTypeName);
          this.opData.ltForOT.Add(index1);
          TreeNode node = new TreeNode(objectTypeName, 51, 51);
          treeView.Nodes[index1].Nodes.Add(node);
        }
        treeView.EndUpdate();
      }
      this.opDataChanged = true;
      treeView.ExpandAll();
    }
  }

  private void globDelete_Click(object sender, EventArgs e)
  {
    TreeView treeView = this.tvGlobRoot;
    if (sender != this.globDelete)
      treeView = this.tvGTSearch;
    TreeNode selectedNode = treeView.SelectedNode;
    if (selectedNode == null)
      return;
    int index1 = selectedNode.Parent == null ? treeView.Nodes.IndexOf(selectedNode) : treeView.Nodes.IndexOf(selectedNode.Parent);
    if (selectedNode.ImageIndex == 51)
    {
      string text = selectedNode.Text;
      int index2 = -1;
      for (int index3 = 0; index3 < this.opData.objTexts.Count; ++index3)
      {
        if (this.opData.ltForOT[index3] == index1 && this.opData.objTexts[index3] == text)
        {
          index2 = index3;
          break;
        }
      }
      if (index2 >= 0)
      {
        this.opData.ltForOT.RemoveAt(index2);
        this.opData.objGUIDs.RemoveAt(index2);
        this.opData.objTexts.RemoveAt(index2);
        this.opDataChanged = true;
      }
      treeView.Nodes.Remove(selectedNode);
    }
    else
    {
      for (int index4 = this.opData.objGUIDs.Count - 1; index4 >= 0; --index4)
      {
        if (this.opData.ltForOT[index4] == index1)
        {
          this.opData.ltForOT.RemoveAt(index4);
          this.opData.objGUIDs.RemoveAt(index4);
          this.opData.objTexts.RemoveAt(index4);
        }
      }
      this.opData.linkIDs.RemoveAt(index1);
      this.opData.linkTexts.RemoveAt(index1);
      this.opDataChanged = true;
      treeView.Nodes.RemoveAt(index1);
    }
  }

  private void rashifrItem_Click(object sender, EventArgs e)
  {
    if (this.opData.tf2.Count <= 0)
      return;
    new DeshifrForm().Execute(this.opData.tf2);
  }

  private void rbNoGroup_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged || !(sender is RadioButton radioButton) || !radioButton.Checked)
      return;
    this.opData.s5 = Convert.ToString(radioButton.Tag);
    this.opDataChanged = true;
  }

  private void rbContentsAll_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged || !(sender is RadioButton radioButton) || !radioButton.Checked)
      return;
    this.opData.st4 = Convert.ToString(radioButton.Tag);
    this.opDataChanged = true;
  }

  private void cbConfigOptions_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b5 = (sender as System.Windows.Forms.CheckBox).Checked;
    this.opDataChanged = true;
  }

  private void cbCoCreator_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.scriptChanged = true;
    this.btnSave.Enabled = true;
  }

  private void cbCoWorkerDoc_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b4 = this.cbCoWorkerDoc.Checked;
    this.opDataChanged = true;
  }

  private void cbCoWorkerComp_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = this.cbCoWorkerComp.Checked;
    this.opDataChanged = true;
  }

  private void buttonEdit4_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_676"), LocalizationHolder.rm.GetString("Expert.Editor_677"), ExpertConsts.Consts.objESFolder, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    if (objectID == this.opData.exID)
      return;
    string str = "";
    Guid guid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      str = dbObject.Caption;
      guid = dbObject.ObjectGUID;
    }
    this.opData.exID = objectID;
    this.opData.st1 = str;
    ((Control) sender).Text = str;
    this.opData.s3 = guid.ToString();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.opData.s4 = OpParmExpObj.ComposeCondsString(OpParmExpObj.LoadFolderConds(sessionKeeper.Session, this.opData.s3));
      this.tbConds.Text = this.opData.s4;
    }
    this.opDataChanged = true;
  }

  private void cbNoNumber_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b5 = this.cbNoNumber.Checked;
    this.cbNoCount.Visible = this.opData.b5;
    this.opDataChanged = true;
  }

  private void cbArray_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    if (this.cbArray.Checked && !this.MultipleValued(this.opData.s1))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_680"), LocalizationHolder.rm.GetString("Expert.Editor_57"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.cbArray.Checked = false;
    }
    else
    {
      this.opData.b2 = this.cbArray.Checked;
      this.EnableDisable(this.panOpParms3);
      this.opDataChanged = true;
    }
  }

  private void richX_DoubleClick(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    RichTextBox memo = (RichTextBox) sender;
    if (memo == this.richX)
    {
      if (this.opData.tf2 == null)
        this.opData.tf2 = new TempFormula(DataType.Integer);
      this.EditFormula(ref this.opData.tf2, LocalizationHolder.rm.GetString("Expert.Editor_678"), memo);
    }
    if (memo == this.richY)
    {
      if (this.opData.tf3 == null)
        this.opData.tf3 = new TempFormula(DataType.Integer);
      this.EditFormula(ref this.opData.tf3, LocalizationHolder.rm.GetString("Expert.Editor_679"), memo);
    }
    this.opDataChanged = true;
  }

  private void cbCurrentForever_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b5 = this.cbCurrentForever.Checked;
    this.opDataChanged = true;
  }

  private void btnCheckTemplate_Click(object sender, EventArgs e)
  {
    this.checkTempl = DocumentEditorPlugin.LoadDocumentFromDBObject(this.templID, updateDoc: false);
    if (this.checkTempl.DocumentFlows != null && this.checkTempl.DocumentFlows.Count > 0 && ((PageData) this.checkTempl.Nodes[0]).GetFirstFlowElement(this.checkTempl.DocumentFlows[0]) is TableData firstFlowElement)
    {
      this.InitCheckDoc();
      this.CheckDocNode((DocumentTreeNode) firstFlowElement);
      if (this.curNode.ChildNodes.Count > 0)
      {
        new ShowXml().Execute(this.checkDoc);
        return;
      }
    }
    int num = (int) MessageBox.Show("Никаких проблем не обнаружено!", "Проверка завершена", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// Проверить скрипт, вернуть неправильный узел, если он есть
  /// </summary>
  /// <returns></returns>
  private TreeListNode checkScript()
  {
    this.InitCheckDoc();
    if (this._WrongDocType())
      return this.tree.FocusedNode;
    if (this.templID != -1L)
      this.checkTempl = DocumentEditorPlugin.LoadDocumentFromDBObject(this.templID, updateDoc: false);
    if (this.checkTempl == null && this.ObjectType == ExpertScriptType.DocScript)
    {
      int num = (int) MessageBox.Show("Не удалось открыть шаблон документа. Без него генерация документа НЕВОЗМОЖНА!", "ВНИМАНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (TreeListNode) null;
    }
    TreeListNode treeListNode = this.tree.FocusedNode;
    if (treeListNode == null)
      return (TreeListNode) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag;
      do
      {
        flag = this.CheckNode(treeListNode, sessionKeeper.Session);
        if (flag)
        {
          treeListNode = this.GetNextNode(treeListNode);
          if (treeListNode == null)
            break;
        }
      }
      while (flag);
    }
    return this.curNode.ChildNodes.Count <= 0 ? (TreeListNode) null : treeListNode;
  }

  /// <summary>
  /// Проверить скрипт, вернуть неправильный узел, если он есть
  /// </summary>
  /// <returns></returns>
  private TreeListNode checkScriptFromNode(TreeListNode startNode)
  {
    this.InitCheckDoc();
    if (this._WrongDocType())
      return startNode;
    if (this.templID != -1L)
      this.checkTempl = DocumentEditorPlugin.LoadDocumentFromDBObject(this.templID, updateDoc: false);
    if (this.checkTempl == null && this.ObjectType == ExpertScriptType.DocScript)
    {
      int num = (int) MessageBox.Show("Не удалось открыть шаблон документа. Без него генерация документа НЕВОЗМОЖНА!", "ВНИМАНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (TreeListNode) null;
    }
    TreeListNode treeListNode = startNode;
    if (treeListNode == null)
      return (TreeListNode) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag;
      do
      {
        flag = this.CheckNode(treeListNode, sessionKeeper.Session);
        if (flag)
        {
          treeListNode = this.GetNextNode(treeListNode);
          if (treeListNode == null)
            break;
        }
      }
      while (flag);
    }
    return this.curNode.ChildNodes.Count <= 0 ? (TreeListNode) null : treeListNode;
  }

  private bool _WrongDocType()
  {
    if (!(this.objTypeGuid != Guid.Empty) || MetaDataHelper.GetObjectTypeID(this.objTypeGuid) != -1)
      return false;
    this.traceAddText(this.traceAddElement("Wrong_Doc_ObjType"), "В параметрах документа задан неверный тип генерируемого документа");
    return true;
  }

  /// <summary>Enable and disable controls on the panel</summary>
  /// <param name="p">Panel which controls should be enabled/disabled</param>
  private void EnableDisable(Panel p)
  {
    if (p == this.panModParms5)
    {
      this.btnForAttr.Enabled = !this.readOnly;
      if (this.checkDoWhile.Checked)
      {
        this.richWhileCond.Enabled = !this.readOnly;
        this.btnRefAttr.Enabled = false;
        this.btnRefAttr.Text = "";
        this.spinEdit1.Enabled = false;
        this.richForEnd.Enabled = false;
      }
      if (this.checkMulti.Checked)
      {
        this.richWhileCond.Enabled = false;
        this.btnRefAttr.Enabled = !this.readOnly;
        this.spinEdit1.Enabled = false;
        this.richForEnd.Enabled = false;
      }
      if (!this.checkFor.Checked)
        return;
      this.richWhileCond.Enabled = false;
      this.btnRefAttr.Enabled = false;
      this.btnRefAttr.Text = "";
      this.spinEdit1.Enabled = !this.readOnly;
      this.richForEnd.Enabled = !this.readOnly;
    }
    else
    {
      if (p == this.panModParms3)
      {
        this.tvAttrs.Enabled = !this.cbInbSort.Checked;
        this.btnAddObj.Enabled = !this.cbInbSort.Checked;
        this.btnAddLink.Enabled = !this.cbInbSort.Checked;
        this.btnDelAttr.Enabled = !this.cbInbSort.Checked;
      }
      if (p == this.panOpParms3)
      {
        this.comboDivider.Enabled = !this.readOnly && this.comboSelector.SelectedIndex == 7;
        this.richFormula.Enabled = !this.readOnly && this.settAttr.selAttr != null && this.comboSelector.SelectedIndex != 4;
        this.richX.Enabled = !this.readOnly && this.cbArray.Checked;
        this.richY.Enabled = !this.readOnly && this.cbArray.Checked;
        if (this.richX.Enabled && this.opData.tf2 != null)
          this.ShowFormula(this.opData.tf2, this.richX);
        else
          this.richX.Clear();
        if (this.richY.Enabled && this.opData.tf3 != null)
          this.ShowFormula(this.opData.tf3, this.richY);
        else
          this.richY.Clear();
      }
      else if (p == this.panOpParms4)
      {
        if (this.checkAttr.Checked)
        {
          this.setAttr.Enabled = !this.readOnly;
          this.richSetValue.Enabled = false;
        }
        if (this.checkForm.Checked)
        {
          this.setAttr.Enabled = false;
          this.richSetValue.Enabled = !this.readOnly;
        }
        this.edAddAttr.Enabled = !this.readOnly && this.checkAddId.Checked;
        this.cbAuthFile.Visible = this.checkAttr.Checked;
      }
      else
      {
        if (p == this.panOpParms5)
        {
          this.editAddAttr.Enabled = !this.readOnly && this.checkEditAdd.Checked;
          this.cbCurrentForever.Enabled = !this.readOnly && this.checkMakeCurrent.Checked;
        }
        if (p == this.panOpParms6)
        {
          this.groupBox4.Enabled = !this.cbSelectDoc.Checked;
          this.gbIdent.Enabled = !this.readOnly && !this.cbSelectDoc.Checked;
          if (this.gbIdent.Enabled)
          {
            this.edSelTemplate.Enabled = !this.readOnly && this.checkSelFromTempl.Checked;
            this.buttonEdit3.Enabled = !this.readOnly && this.edSelTemplate.Enabled;
            this.richTextID.Enabled = !this.readOnly && !this.checkSelFromTempl.Checked;
          }
        }
        if (p == this.panOpParmsC)
        {
          this.beNewList.Enabled = !this.readOnly;
          this.cbMakeListCurrent.Visible = this.rbNewPage.Checked;
        }
        if (p == this.panOpParms1)
        {
          this.label1.Enabled = !this.cbNoSearch.Checked;
          this.btnEdExcerpt.Enabled = !this.cbNoSearch.Checked && !this.readOnly;
          this.richCond.Enabled = !this.readOnly;
          this.tvTypes.Enabled = !this.readOnly;
          this.btnAddObjType.Enabled = !this.readOnly;
          this.btnAddLinkType.Enabled = !this.readOnly;
          this.label5.Enabled = true;
          this.label36.Enabled = true;
          this.richGlobalFilter.Enabled = !this.readOnly;
          this.richAfterFilter.Enabled = !this.readOnly;
          this.cbSaveRels.Enabled = !this.readOnly && (this.rbSaveLocal.Checked || this.rbSaveAdd.Checked);
          if (!this.cbNoSearch.Checked)
          {
            this.rbGlobalMul.Visible = false;
            if (this.rbGlobalMul.Checked)
              this.rbGlobalNone.Checked = true;
            this.rbGlobalPlus.Text = LocalizationHolder.rm.GetString("Expert.Editor_565");
            this.rbGlobalNone.Text = LocalizationHolder.rm.GetString("Expert.Editor_567");
          }
          else
          {
            this.rbGlobalMul.Visible = true;
            this.rbGlobalPlus.Text = LocalizationHolder.rm.GetString("Expert.Editor_564");
            this.rbGlobalNone.Text = LocalizationHolder.rm.GetString("Expert.Editor_566");
          }
        }
        if (p != this.panOpParmsE)
          return;
        this.cbNoCount.Visible = this.opData.b5;
      }
    }
  }

  private void tree_MouseMove(object sender, MouseEventArgs e)
  {
    string caption = "";
    TreeListHitInfo hitInfo = this.tree.GetHitInfo(new Point(e.X, e.Y));
    if (hitInfo.Node != null && hitInfo.Node.Tag is Intermech.Expert.NodeData)
      caption = this.GetNodeDescr(hitInfo.Node.Tag as Intermech.Expert.NodeData);
    if (!(this.treeTT != caption))
      return;
    this.treeTT = caption;
    this.toolTip2.SetToolTip((Control) this.tree, caption);
  }

  private void ScriptEdit_KeyPress(object sender, KeyPressEventArgs e)
  {
  }

  private void ScriptEdit_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.Control || e.KeyCode != Keys.Return || !this.SaveChangedNodeData())
      return;
    this.SetDescrText(this.GetNodeDescr((Intermech.Expert.NodeData) this.tree.FocusedNode.Tag));
    e.Handled = true;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if ((this.opDataChanged || this.modDataChanged) && !this.SaveChangedNodeData())
      return;
    if (this.scriptID >= 0L)
      this.SaveScript();
    this.btnOK.Visible = false;
    this.btnSave.Enabled = false;
    this.btnSave.Visible = true;
    UserPrompt userPrompt = new UserPrompt();
    if (this.ObjectType == ExpertScriptType.DocScript && this.newObjName == "")
      this.newObjName = userPrompt.Execute(LocalizationHolder.rm.GetString("Expert.Editor_367"), LocalizationHolder.rm.GetString("Expert.Editor_368"));
    if (this.ObjectType == ExpertScriptType.CommonCalc)
      this.newObjName = userPrompt.Execute(LocalizationHolder.rm.GetString("Expert.Editor_369"), LocalizationHolder.rm.GetString("Expert.Editor_370"));
    if (this.newObjName != "")
    {
      this.DialogResult = DialogResult.OK;
      if (this.CreateEvent == null)
        return;
      this.CreateEvent((object) this, this.create_Args);
    }
    else
      this.DialogResult = DialogResult.None;
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if ((this.opDataChanged || this.modDataChanged) && !this.SaveChangedNodeData())
      return;
    this.SaveScript();
    if (this.tree.FocusedNode == null)
      return;
    this.SetDescrText(this.GetNodeDescr((Intermech.Expert.NodeData) this.tree.FocusedNode.Tag));
  }

  internal void UpdateReadOnlyState()
  {
    if (this.readOnly)
      this.Struct.Options &= ~ColumnOptions.CanFocused;
    else
      this.Struct.Options |= ColumnOptions.CanFocused;
    this.TBar_Control.Enabled = !this.readOnly;
    this.EnableDisableChildControls((Control) this.panModParms1, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panModParms2, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panModParms3, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panModParms4, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panModParms5, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms1, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms2, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms3, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms4, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms5, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms6, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms7, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms8, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParms9, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsA, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsB, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsC, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsD, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsE, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsTI, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsStyleB, !this.readOnly);
    this.EnableDisableChildControls((Control) this.panOpParmsStyleC, !this.readOnly);
    this.menuInsBefore.Enabled = !this.readOnly;
    this.menuInsAfter.Enabled = !this.readOnly;
    this.menuInsInto.Enabled = !this.readOnly;
    this.menuChange.Enabled = !this.readOnly;
    this.menuApply.Enabled = !this.readOnly;
    this.menuCut.Enabled = !this.readOnly;
    this.menuPaste.Enabled = !this.readOnly;
    this.menuDelete.Enabled = !this.readOnly;
    this.UpdateSaveCancelButtons();
  }

  private void EnableDisableChildControls(Control RootControl, bool enable)
  {
    foreach (Control control in (ArrangedElementCollection) RootControl.Controls)
    {
      if (control.GetType() == typeof (System.Windows.Forms.Button) || control.GetType() == typeof (System.Windows.Forms.CheckBox) || control.GetType() == typeof (RadioButton) || control.GetType() == typeof (ButtonEdit) || control.GetType() == typeof (PanelButton))
        control.Enabled = enable;
      else
        this.EnableDisableChildControls(control, enable);
    }
  }

  private void UpdateSaveCancelButtons()
  {
    this.btnSave.Enabled = !this.readOnly && this.scriptChanged;
    this.btnCancel.Enabled = !this.readOnly && this.scriptChanged;
  }

  private void tree_CellValueChanged(object sender, CellValueChangedEventArgs e)
  {
    this.scriptChanged = true;
    this.UpdateSaveCancelButtons();
  }

  private void tabObjMain_Resize(object sender, EventArgs e)
  {
    RichTextBox richCond = this.richCond;
    Point location = this.richCond.Location;
    int x = location.X;
    location = this.richCond.Location;
    int y1 = location.Y;
    Size size = this.tabObjMain.Size;
    int width = size.Width - 15;
    size = this.tabObjMain.Size;
    int height1 = size.Height;
    location = this.richCond.Location;
    int y2 = location.Y;
    int height2 = height1 - y2 - 8;
    richCond.SetBounds(x, y1, width, height2);
  }

  private void btnDocParms_Click(object sender, EventArgs e)
  {
    if (this.ObjectType == ExpertScriptType.DocScript || this.ObjectType == ExpertScriptType.CommandScript)
    {
      GenDocParms genDocParms = new GenDocParms(this.objTypeGuid, this.useTraceInfo, this.allNodeObjects, this.allZamens, this.objTypeName, this.docName, this.thisObjectDoc, this.scriptID, this.needClassify, this.GetTableNode(), this.ObjectType == ExpertScriptType.CommandScript);
      List<int> objTypes = (List<int>) null;
      List<int> relTypes = (List<int>) null;
      List<string> attrTypes = (List<string>) null;
      this.CollectAllTypes(ref objTypes, ref relTypes, ref attrTypes);
      genDocParms.objTypes = objTypes;
      genDocParms.relTypes = relTypes;
      genDocParms.attrGUIDs = attrTypes;
      genDocParms.allowedTypes = this.allowedTypes;
      genDocParms.ShowAllowedTypes();
      if (genDocParms.ShowDialog() == DialogResult.OK)
      {
        this.objTypeGuid = genDocParms.objTypeGUID;
        this.useTraceInfo = genDocParms.ShowDebugInfo;
        this.allNodeObjects = genDocParms.AllNodeObjects;
        this.objTypeName = genDocParms.objTypeName;
        this.allZamens = genDocParms.UseAllZamens;
        this.docName = genDocParms.docName;
        this.lblDocType.Text = this.objTypeName;
        this.allowedTypes = genDocParms.allowedTypes;
        this.thisObjectDoc = genDocParms.ThisObjectDoc;
        this.needClassify = genDocParms.Classify;
        this.scriptChanged = true;
        this.UpdateSaveCancelButtons();
      }
    }
    if (this.ObjectType == ExpertScriptType.AttribRule && this.tree.FocusedNode != null)
    {
      long objectID = -1;
      Intermech.Expert.NodeData nodeData = this.data(this.tree.FocusedNode);
      if (nodeData.ops is OpParmExpObj)
      {
        string objTypeGuid = (nodeData.ops as OpParmExpObj).objTypeGUID;
        if (objTypeGuid != "")
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(objTypeGuid));
            if (dbObject != null)
              objectID = dbObject.ObjectID;
          }
          if (objectID != -1L)
            this.InvokeCommandForObject(objectID, "EditDocument");
        }
      }
    }
    if (this.ObjectType != ExpertScriptType.ComplectTemplate || this.tree.FocusedNode == null)
      return;
    long objectID1 = -1;
    Intermech.Expert.NodeData nodeData1 = this.data(this.tree.FocusedNode);
    if (!(nodeData1.ops is OpCreateDoc))
      return;
    string scriptGuid = (nodeData1.ops as OpCreateDoc).scriptGUID;
    if (!(scriptGuid != ""))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(scriptGuid));
      if (dbObject != null)
        objectID1 = dbObject.ObjectID;
    }
    if (objectID1 == -1L)
      return;
    this.InvokeCommandForObject(objectID1, "EditDocument");
  }

  private void panelApply_Resize(object sender, EventArgs e)
  {
    this.lblDocType.Width = this.panelApply.Size.Width - this.lblDocType.Location.X - 200;
  }

  private OpParmSetting GetTableNode(TreeListNode root)
  {
    if (root.Tag is Intermech.Expert.NodeData tag && tag.opTag == 18 && tag.ops is OpParmSetting && (tag.ops as OpParmSetting).listTable != null)
      return tag.ops as OpParmSetting;
    foreach (TreeListNode node in root.Nodes)
    {
      OpParmSetting tableNode = this.GetTableNode(node);
      if (tableNode != null)
        return tableNode;
    }
    return (OpParmSetting) null;
  }

  private OpParmSetting GetTableNode()
  {
    for (int index = 0; index < this.tree.Nodes.Count; ++index)
    {
      OpParmSetting tableNode = this.GetTableNode(this.tree.Nodes[index]);
      if (tableNode != null)
        return tableNode;
    }
    return (OpParmSetting) null;
  }

  private void CollectAllTypes(
    TreeListNode root,
    List<int> objTypes,
    List<int> relTypes,
    List<string> attrTypes)
  {
    if (root.Tag is Intermech.Expert.NodeData tag && (tag.opTag == 13 || tag.opTag == 14) && tag.ops is OpParmObject)
    {
      OpParmObject ops = tag.ops as OpParmObject;
      if (ops.saveGlobal == GlobalSave.saveSet || ops.saveGlobal == GlobalSave.saveAdd)
      {
        if (ops.objTypeIDs != null && ops.objTypeIDs.Count > 0)
        {
          foreach (int objTypeId in ops.objTypeIDs)
          {
            if (objTypes.IndexOf(objTypeId) < 0)
              objTypes.Add(objTypeId);
          }
        }
        if (ops.linkTypeIDs != null && ops.linkTypeIDs.Count > 0)
        {
          foreach (int linkTypeId in ops.linkTypeIDs)
          {
            if (relTypes.IndexOf(linkTypeId) < 0)
              relTypes.Add(linkTypeId);
          }
        }
        if (ops.dataAttrGUIDs != null && ops.dataAttrGUIDs.Count > 0)
        {
          foreach (string dataAttrGuiD in ops.dataAttrGUIDs)
          {
            if (attrTypes.IndexOf(dataAttrGuiD) < 0)
              attrTypes.Add(dataAttrGuiD);
          }
        }
      }
    }
    foreach (TreeListNode node in root.Nodes)
      this.CollectAllTypes(node, objTypes, relTypes, attrTypes);
  }

  private void CollectAllTypes(
    ref List<int> objTypes,
    ref List<int> relTypes,
    ref List<string> attrTypes)
  {
    objTypes = new List<int>();
    relTypes = new List<int>();
    attrTypes = new List<string>();
    for (int index = 0; index < this.tree.Nodes.Count; ++index)
      this.CollectAllTypes(this.tree.Nodes[index], objTypes, relTypes, attrTypes);
  }

  private void UpdateInnerConds(TreeListNode root, IUserSession ius)
  {
    Intermech.Expert.NodeData nodeData = this.data(root);
    if (nodeData.ops is OpParmExpObj)
    {
      OpParmExpObj ops = nodeData.ops as OpParmExpObj;
      if (ops.objTypeGUID != "" && (ops.objCond == null || ops.objCond.Count == 0))
      {
        IDBObject dbObject = ius.GetObject(new Guid(ops.objTypeGUID), false);
        if (dbObject != null && dbObject is IExpertObject)
        {
          (dbObject as IExpertObject).Load();
          TempFormula cond = (dbObject as IExpertObject).Cond;
          if (cond != null)
            ops.objCond = (TempFormula) cond.Clone();
        }
      }
    }
    for (int index = 0; index < root.Nodes.Count; ++index)
      this.UpdateInnerConds(root.Nodes[index], ius);
  }

  private void UpdateInnerConds(IUserSession ius)
  {
    for (int index = 0; index < this.tree.Nodes.Count; ++index)
      this.UpdateInnerConds(this.tree.Nodes[index], ius);
  }

  private void WriteNodeToXML(ref XmlTextWriter writer, TreeListNode node)
  {
    Intermech.Expert.NodeData nodeData = this.data(node);
    writer.WriteStartElement(nameof (node));
    writer.WriteAttributeString("label", Convert.ToString(node[(object) 0]));
    ref XmlTextWriter local = ref writer;
    nodeData.WriteToXML(ref local);
    if (node.HasChildren)
    {
      for (int index = 0; index < node.Nodes.Count; ++index)
        this.WriteNodeToXML(ref writer, node.Nodes[index]);
    }
    writer.WriteEndElement();
  }

  private void SaveToXML(string FileName)
  {
    XmlTextWriter writer = (XmlTextWriter) null;
    try
    {
      writer = new XmlTextWriter(FileName, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("WholeScript");
      writer.WriteStartElement("DocParms");
      writer.WriteElementString("DocType_GUID", (string) null, this.objTypeGuid.ToString());
      writer.WriteElementString("DocType_Name", (string) null, this.objTypeName);
      writer.WriteElementString("DocName", (string) null, this.docName);
      writer.WriteElementString("show_info", (string) null, this.useTraceInfo ? "Y" : "N");
      writer.WriteElementString("all_node", (string) null, this.allNodeObjects ? "Y" : "N");
      string str = "C";
      if (this.allZamens == UseZamens.AllVariants)
        str = "N";
      if (this.allZamens == UseZamens.MainVariant)
        str = "Y";
      writer.WriteElementString("all_zamens", (string) null, str);
      writer.WriteElementString("Classify", (string) null, this.needClassify ? "Y" : "N");
      writer.WriteEndElement();
      writer.WriteStartElement("ExpScript");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
      for (int index = 0; index < this.tree.Nodes.Count; ++index)
        this.WriteNodeToXML(ref writer, this.tree.Nodes[index]);
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
    }
    finally
    {
      writer?.Close();
    }
  }

  private void LoadNodeFromXML(XmlNode xmlRoot, TreeListNode rootNode)
  {
    string str = "";
    int num1 = -1;
    int num2 = -1;
    if (xmlRoot.Attributes != null)
    {
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlRoot.Attributes)
      {
        if (attribute.Name == "label")
          str = attribute.Value;
        else if (attribute.Name == "modTag")
          num1 = Convert.ToInt32(attribute.Value);
        else if (attribute.Name == "opTag")
          num2 = Convert.ToInt32(attribute.Value);
      }
    }
    Intermech.Expert.NodeData nodeData = new Intermech.Expert.NodeData(xmlRoot, num1, num2);
    if (nodeData.ops is OpParmObject)
    {
      OpParmObject ops = (OpParmObject) nodeData.ops;
      if (ops.linkTypeIDs != null)
      {
        if (ops.linkTypeTexts == null)
          ops.linkTypeTexts = new ArrayList();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          for (int index = 0; index < ops.linkTypeIDs.Count; ++index)
          {
            IDBRelationType relationType = sessionKeeper.Session.GetRelationType(ops.linkTypeIDs[index], false);
            if (relationType != null)
              ops.linkTypeTexts.Add((object) relationType.Description);
            else
              ops.linkTypeTexts.Add((object) "<???>");
          }
        }
      }
      if (ops.objTypeIDs != null)
      {
        if (ops.objTypeTexts == null)
          ops.objTypeTexts = new ArrayList();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          int index = 0;
          while (index < ops.objTypeIDs.Count)
          {
            IDBObjectType objectType = sessionKeeper.Session.GetObjectType(ops.objTypeIDs[index], false);
            if (objectType != null)
            {
              ops.objTypeTexts.Add((object) objectType.ObjectTypeName);
              ++index;
            }
            else
              ops.objTypeIDs.RemoveAt(index);
          }
        }
      }
    }
    TreeListNode node;
    if (rootNode == null)
    {
      this.tree.AppendNode((object) new object[3]
      {
        (object) "",
        (object) "",
        (object) ""
      }, -1, num1, num1, num2);
      node = this.tree.Nodes[this.tree.Nodes.Count - 1];
    }
    else
    {
      this.tree.AppendNode((object) new object[3]
      {
        (object) "",
        (object) "",
        (object) ""
      }, rootNode.Id, num1, num1, num2);
      node = rootNode.Nodes[rootNode.Nodes.Count - 1];
    }
    if (node != null)
    {
      node.Tag = (object) nodeData;
      node[(object) 0] = (object) str;
      node[(object) 1] = (object) nodeData.GetShortMod();
      node[(object) 2] = (object) nodeData.GetShortOp();
    }
    if (!xmlRoot.HasChildNodes)
      return;
    foreach (XmlNode childNode in xmlRoot.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "node")
        this.LoadNodeFromXML(childNode, node);
    }
  }

  public void LoadFromXML(string s)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(s);
    XmlElement documentElement = xmlDocument.DocumentElement;
    this.tree.ClearNodes();
    if (!documentElement.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in documentElement.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "DocParms")
      {
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocType_GUID")
            this.objTypeGuid = new Guid(childNode2.InnerText);
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocType_Name")
            this.objTypeName = childNode2.InnerText;
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocName")
            this.docName = childNode2.InnerText;
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "show_info")
            this.useTraceInfo = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "all_node")
            this.allNodeObjects = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "all_zamens")
          {
            switch (childNode2.InnerText)
            {
              case "Y":
                this.allZamens = UseZamens.MainVariant;
                continue;
              case "N":
                this.allZamens = UseZamens.AllVariants;
                continue;
              case "C":
                this.allZamens = UseZamens.AsClient;
                continue;
              default:
                continue;
            }
          }
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "thisObj_Doc")
            this.thisObjectDoc = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "Classify")
            this.needClassify = childNode2.InnerText == "Y";
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ExpScript")
      {
        foreach (XmlNode childNode3 in childNode1.ChildNodes)
          this.LoadNodeFromXML(childNode3, (TreeListNode) null);
      }
      else
        this.LoadNodeFromXML(childNode1, (TreeListNode) null);
    }
  }

  /// <summary>Load script from zipped XML in a buffer</summary>
  /// <param name="inBuf">buffer with zipped XML script</param>
  private void LoadFromBuffer(byte[] inBuf)
  {
    XmlElement documentElement = ZlibHelper.UnpackXmlBuffer(inBuf).DocumentElement;
    this.tree.ClearNodes();
    if (!documentElement.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in documentElement.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "DocParms")
      {
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocType_GUID")
            this.objTypeGuid = new Guid(childNode2.InnerText);
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocType_Name")
            this.objTypeName = childNode2.InnerText;
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "DocName")
            this.docName = childNode2.InnerText;
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "show_info")
            this.useTraceInfo = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "all_node")
            this.allNodeObjects = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "all_zamens")
          {
            switch (childNode2.InnerText)
            {
              case "Y":
                this.allZamens = UseZamens.MainVariant;
                continue;
              case "N":
                this.allZamens = UseZamens.AllVariants;
                continue;
              case "C":
                this.allZamens = UseZamens.AsClient;
                continue;
              default:
                continue;
            }
          }
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "thisObj_Doc")
            this.thisObjectDoc = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "Classify")
            this.needClassify = childNode2.InnerText == "Y";
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "coWorker_Template")
          {
            this.lockChanged = true;
            try
            {
              this.cbCoCreator.Checked = childNode2.InnerText == "Y";
            }
            finally
            {
              this.lockChanged = false;
            }
          }
          else if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "Checkout_Docs")
          {
            this.lockChanged = true;
            try
            {
              this.cbCheckout.Checked = childNode2.InnerText == "Y";
            }
            finally
            {
              this.lockChanged = false;
            }
          }
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ExpScript")
      {
        foreach (XmlNode childNode3 in childNode1.ChildNodes)
          this.LoadNodeFromXML(childNode3, (TreeListNode) null);
      }
      else
        this.LoadNodeFromXML(childNode1, (TreeListNode) null);
    }
  }

  /// <summary>Save script in zipped XML form to a buffer</summary>
  /// <returns></returns>
  private byte[] SaveToBuffer()
  {
    MemoryStream w = new MemoryStream();
    MemoryStream baseOutputStream = new MemoryStream();
    XmlTextWriter writer = (XmlTextWriter) null;
    try
    {
      writer = new XmlTextWriter((Stream) w, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("WholeScript");
      writer.WriteStartElement("DocParms");
      writer.WriteElementString("DocType_GUID", (string) null, this.objTypeGuid.ToString());
      writer.WriteElementString("DocType_Name", (string) null, this.objTypeName);
      writer.WriteElementString("DocName", (string) null, this.docName);
      writer.WriteElementString("show_info", (string) null, this.useTraceInfo ? "Y" : "N");
      writer.WriteElementString("all_node", (string) null, this.allNodeObjects ? "Y" : "N");
      writer.WriteElementString("thisObj_Doc", (string) null, this.thisObjectDoc ? "Y" : "N");
      string str = "C";
      if (this.allZamens == UseZamens.AllVariants)
        str = "N";
      if (this.allZamens == UseZamens.MainVariant)
        str = "Y";
      writer.WriteElementString("all_zamens", (string) null, str);
      writer.WriteElementString("coWorker_Template", (string) null, this.cbCoCreator.Checked ? "Y" : "N");
      writer.WriteElementString("Checkout_Docs", (string) null, this.cbCheckout.Checked ? "Y" : "N");
      writer.WriteElementString("Classify", (string) null, this.needClassify ? "Y" : "N");
      writer.WriteEndElement();
      writer.WriteStartElement("ExpScript");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
      for (int index = 0; index < this.tree.Nodes.Count; ++index)
        this.WriteNodeToXML(ref writer, this.tree.Nodes[index]);
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      w.Position = 0L;
      Deflater deflater = new Deflater(3);
      DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream, deflater);
      deflaterOutputStream.Write(w.GetBuffer(), 0, Convert.ToInt32(w.Length));
      deflaterOutputStream.Flush();
      deflaterOutputStream.Finish();
    }
    finally
    {
      writer?.Close();
    }
    return baseOutputStream.ToArray();
  }

  private void btnSaveXml_Click(object sender, EventArgs e)
  {
    if (this.sd.ShowDialog() != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.tree.Nodes.Count; ++index)
        this.PrepareNode(sessionKeeper.Session, this.tree.Nodes[index]);
    }
    this.SaveToXML(this.sd.FileName);
  }

  private void PrepareNode(IUserSession ius, TreeListNode node)
  {
    Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) node.Tag;
    if (tag.ops != null && tag.ops is OpParmObject)
    {
      OpParmObject ops = (OpParmObject) tag.ops;
      if (ops.linkTypeIDs != null)
      {
        ops.linkTypeGUIDs = new List<string>();
        foreach (int linkTypeId in ops.linkTypeIDs)
        {
          IDBRelationType relationType = ius.GetRelationType(linkTypeId, false);
          if (relationType != null)
            ops.linkTypeGUIDs.Add(relationType.PropertiesStructure.RelationTypeGuid.ToString());
        }
      }
      if (ops.objTypeIDs != null)
      {
        ops.objTypeGUIDs = new List<string>();
        foreach (int objTypeId in ops.objTypeIDs)
        {
          IDBObjectType objectType = ius.GetObjectType(objTypeId, false);
          if (objectType != null)
            ops.objTypeGUIDs.Add(objectType.PropertiesStructure.ObjectTypeGuid.ToString());
        }
      }
    }
    if (node.Nodes == null || node.Nodes.Count <= 0)
      return;
    foreach (TreeListNode node1 in node.Nodes)
      this.PrepareNode(ius, node1);
  }

  private void btnLoadXml_Click(object sender, EventArgs e)
  {
    if (this.tree.Nodes.Count > 0 && MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_159"), LocalizationHolder.rm.GetString("Expert.Editor_222"), MessageBoxButtons.OKCancel) != DialogResult.OK || this.od.ShowDialog() != DialogResult.OK)
      return;
    this.LoadFromXML(this.od.FileName);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<IdGuid> attrs = new List<IdGuid>();
      List<IdGuid> objs = new List<IdGuid>();
      for (int index = 0; index < this.tree.Nodes.Count; ++index)
        this.ConvertNode(session, this.tree.Nodes[index], attrs, objs);
      for (int index = attrs.Count - 1; index >= 0; --index)
      {
        string sGuid = attrs[index].sGuid;
        IDBAttributeType attributeType = session.GetAttributeType(new Guid(sGuid), false);
        if (attributeType != null)
        {
          attrs[index].Id = attributeType.AttributeID;
        }
        else
        {
          attrs.RemoveAt(index);
          objs.RemoveAt(index);
        }
      }
      for (int index = objs.Count - 1; index >= 0; --index)
      {
        string sGuid = objs[index].sGuid;
        if (sGuid != "")
        {
          IDBObjectType objectType = session.GetObjectType(new Guid(sGuid), false);
          if (objectType != null)
            objs[index].Id = objectType.ObjectType;
          else
            objs.RemoveAt(index);
        }
      }
      foreach (TreeListNode node in this.tree.Nodes)
        this.FixNode(node, attrs, objs);
    }
    this.scriptChanged = true;
    this.UpdateSaveCancelButtons();
    this.OnChangeNode(this.tree.FocusedNode);
    this.SettingAttrChanged = true;
  }

  private void ConvertNode(
    IUserSession ius,
    TreeListNode node,
    List<IdGuid> attrs,
    List<IdGuid> objs)
  {
    Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) node.Tag;
    if (tag.ops != null)
    {
      if (tag.ops is OpParmObject)
      {
        OpParmObject ops = (OpParmObject) tag.ops;
        if (ops.objTypeGUIDs != null)
        {
          ops.objTypeIDs.Clear();
          foreach (string objTypeGuiD in ops.objTypeGUIDs)
          {
            Guid anObjectTypeGuid = new Guid(objTypeGuiD);
            IDBObjectType objectType = ius.GetObjectType(anObjectTypeGuid, false);
            if (objectType != null)
              ops.objTypeIDs.Add(objectType.ObjectType);
          }
        }
        if (ops.linkTypeGUIDs != null)
        {
          ops.linkTypeIDs.Clear();
          foreach (string linkTypeGuiD in ops.linkTypeGUIDs)
          {
            Guid relationTypeGUID = new Guid(linkTypeGuiD);
            IDBRelationType relationType = ius.GetRelationType(relationTypeGUID, false);
            if (relationType != null)
              ops.linkTypeIDs.Add(relationType.RelationType);
          }
        }
      }
      tag.ops.CollectGUIDs(attrs, objs);
    }
    if (tag.mods != null)
      tag.mods.CollectGUIDs(attrs, objs);
    if (node.Nodes == null || node.Nodes.Count <= 0)
      return;
    foreach (TreeListNode node1 in node.Nodes)
      this.ConvertNode(ius, node1, attrs, objs);
  }

  private TreeListNode ValidateScript()
  {
    TreeListNode treeListNode = (TreeListNode) null;
    try
    {
      for (int index = 0; index < this.tree.Nodes.Count; ++index)
        this.ValidateNode(this.tree.Nodes[index]);
    }
    catch (ScriptEdit2.ScriptInvalidException ex)
    {
      treeListNode = ex.wrongNode;
      this.tree.FocusedNode = ex.wrongNode;
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Expert.Editor_372"));
    }
    if (this.ObjectType == ExpertScriptType.ComplectTemplate)
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (TreeListNode node in this.tree.Nodes)
        this.CollectLabels(node, dictionary);
      List<string> list = dictionary.Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, int, bool>) ((kvp, _) => kvp.Value > 1)).Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, int, string>) ((kvp, _) => kvp.Key)).Take<string>(10).ToList<string>();
      if (list.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder("Следующие метки операторов создания документа или комплекта встречаются несколько раз: \n");
        foreach (string str in list)
          stringBuilder.AppendLine(str);
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("Пожалуйста, исправьте эти метки, поскольку это может вызвать проблемы при сортировке комплектов!");
        int num = (int) MessageBox.Show(stringBuilder.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
    return treeListNode;
  }

  private void CollectLabels(TreeListNode node, Dictionary<string, int> dict)
  {
    string key = (string) node[(object) 0];
    if (key == "" || key.StartsWith("#"))
      return;
    Intermech.Expert.NodeData nodeData = this.data(node);
    if (nodeData.opTag != 50 && nodeData.opTag != 49)
      return;
    if (dict.ContainsKey(key))
      ++dict[key];
    else
      dict.Add(key, 1);
    foreach (TreeListNode node1 in node.Nodes)
      this.CollectLabels(node1, dict);
  }

  private void ValidateNode(TreeListNode node)
  {
    switch ((ExpertScriptOp) this.data(node).opTag)
    {
      case ExpertScriptOp.opUnknown:
        throw new ScriptEdit2.ScriptInvalidException(LocalizationHolder.rm.GetString("Expert.Editor_374"), node);
      case ExpertScriptOp.opSelFolder:
        if (node.HasChildren)
        {
          for (int index = 0; index < node.Nodes.Count; ++index)
          {
            TreeListNode node1 = node.Nodes[index];
            Intermech.Expert.NodeData nodeData = this.data(node1);
            if (nodeData.modTag != -1 && nodeData.modTag != 4 && nodeData.modTag != 5)
              throw new ScriptEdit2.ScriptInvalidException(LocalizationHolder.rm.GetString("Expert.Editor_373"), node1);
          }
          break;
        }
        break;
      case ExpertScriptOp.opCreateDocument:
      case ExpertScriptOp.opCreateComplect:
        string str = Convert.ToString(node[(object) 0]);
        if (str.Length == 0)
          throw new ScriptEdit2.ScriptInvalidException(LocalizationHolder.rm.GetString("Expert.Editor_694"), node);
        if (str.Length > 150)
          throw new ScriptEdit2.ScriptInvalidException(LocalizationHolder.rm.GetString("Expert.Editor_695"), node);
        break;
    }
    if (!node.HasChildren)
      return;
    for (int index = 0; index < node.Nodes.Count; ++index)
      this.ValidateNode(node.Nodes[index]);
  }

  public int ImageIndex => -1;

  public int OrderID => 0;

  public string Caption => this.Text;

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this.provider = provider;
  }

  public void Activate(IView previousView)
  {
    if (this._objID == this.scriptID)
      return;
    this.scriptChanged = false;
    this.ExecuteForEdit(this._objID, false);
  }

  public void Deactivate(IView nextView)
  {
    if (!this.scriptChanged)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_375"), LocalizationHolder.rm.GetString("Expert.Editor_376"), MessageBoxButtons.YesNo) == DialogResult.Yes && !this.SaveScript())
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_377"), LocalizationHolder.rm.GetString("Expert.Editor_378"));
    }
    this.scriptChanged = false;
  }

  private DataTable GetFormulae(Guid attrGUID, Guid objTypeGuid)
  {
    ConditionStructure[] conditions;
    if (objTypeGuid != Guid.Empty)
      conditions = new ConditionStructure[2]
      {
        new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrGUID, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
        new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) objTypeGuid, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
      };
    else
      conditions = new ConditionStructure[1]
      {
        new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrGUID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
      };
    ColumnDescriptor[] columns = new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrResObjTypeName, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) -10, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable formulae = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objFormula).Select(paramSet);
      if (objTypeGuid == Guid.Empty)
      {
        for (int index = formulae.Rows.Count - 1; index >= 0; --index)
        {
          if (formulae.Rows[index][ExpertConsts.Consts.attrResObjTypeName] != DBNull.Value)
            formulae.Rows.RemoveAt(index);
        }
      }
      return formulae;
    }
  }

  private DataTable GetDocScripts()
  {
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrGenDocType, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) -10, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objDocScript).Select(paramSet);
  }

  private DataTable GetMultiObject(int objTypeId, Guid attrGUID, Guid objTypeGuid)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrAttrGUIDs, RelationalOperators.Equal, (object) attrGUID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) -10, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      DataTable multiObject = session.GetObjectCollection(objTypeId).Select(paramSet);
      if (multiObject != null)
      {
        int index1 = 0;
        while (index1 < multiObject.Rows.Count)
        {
          long int64 = Convert.ToInt64(multiObject.Rows[index1][0]);
          IExpertObject expertObject = (IExpertObject) session.GetObject(int64);
          object[] valuesById1 = expertObject.GetValuesByID(ExpertConsts.Consts.attrAttrGUIDs, false);
          object[] valuesById2 = expertObject.GetValuesByID(ExpertConsts.Consts.attrObjTypeGUIDs, false);
          IList list = (IList) null;
          if (objTypeId == ExpertConsts.Consts.objTable)
          {
            expertObject.Load();
            list = (expertObject as IExpertTable).Roles;
          }
          bool flag = false;
          if (valuesById1 != null)
          {
            for (int index2 = 0; index2 < valuesById1.Length; ++index2)
            {
              string g1 = Convert.ToString(valuesById1[index2]);
              string g2 = Convert.ToString(valuesById2[index2]);
              Guid guid1 = g1 != "" ? new Guid(g1) : Guid.Empty;
              Guid guid2 = g2 != "" ? new Guid(g2) : Guid.Empty;
              if (guid1.Equals(attrGUID) && guid2.Equals(objTypeGuid) && (list == null || (AttributeRoles) list[index2] == AttributeRoles.Result || (AttributeRoles) list[index2] == AttributeRoles.argResult))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            multiObject.Rows.RemoveAt(index1);
          else
            ++index1;
        }
      }
      return multiObject;
    }
  }

  private void editSelObject_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) this.tree.FocusedNode.Tag;
    DataTable dt = (DataTable) null;
    string oName = "";
    switch (tag.opTag)
    {
      case 25:
        dt = this.GetFormulae(this.AttributeGUID, this.ObjectGUID);
        oName = LocalizationHolder.rm.GetString("Expert.Editor_379");
        break;
      case 26:
        dt = this.GetMultiObject(ExpertConsts.Consts.objTable, this.AttributeGUID, this.ObjectGUID);
        oName = LocalizationHolder.rm.GetString("Expert.Editor_380");
        break;
      case 27:
        dt = this.GetMultiObject(ExpertConsts.Consts.objScript, this.AttributeGUID, this.ObjectGUID);
        oName = LocalizationHolder.rm.GetString("Expert.Editor_381");
        break;
    }
    if (dt != null && dt.Rows.Count > 0)
    {
      int index = new SimpleSelector().Execute(dt, oName, this.opData.s1);
      if (index < 0)
        return;
      this.opData.s1 = Convert.ToString(dt.Rows[index][1]);
      this.opData.s2 = Convert.ToString(dt.Rows[index][2]);
      this.editSelObject.Text = this.opData.s2;
      this.opDataChanged = true;
      if (!(this.opData.s1 != ""))
        return;
      Guid objectGUID = new Guid(this.opData.s1);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
        if (dbObject == null || !(dbObject is IExpertObject))
          return;
        IExpertObject expertObject = (IExpertObject) dbObject;
        expertObject.Load();
        if (expertObject.Cond != null)
        {
          TempFormula cond = expertObject.Cond;
          cond.BeautifyInfixForm();
          this.opData.tf2 = cond;
          this.ShowFormula(cond, this.richInnerCond);
          this.opData.s4 = (string) null;
        }
        else
          this.richInnerCond.Text = "";
      }
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_382") + oName + LocalizationHolder.rm.GetString("Expert.Editor_383"), LocalizationHolder.rm.GetString("Expert.Editor_384"));
    }
  }

  private void beDocScript_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.rbDocScript.Checked)
    {
      Intermech.Expert.NodeData tag = (Intermech.Expert.NodeData) this.tree.FocusedNode.Tag;
      DataTable docScripts = this.GetDocScripts();
      if (docScripts != null && docScripts.Rows.Count > 0)
      {
        int index1 = new SimpleSelector().Execute(docScripts);
        if (index1 < 0)
          return;
        string g = Convert.ToString(docScripts.Rows[index1][3]);
        if (g != "")
        {
          Guid guid = new Guid(g);
          if (!guid.Equals(Guid.Empty))
          {
            IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
            if (!service.GetObjectType(guid, true).AnyAttributes)
            {
              bool[] flagArray = new bool[5];
              int[] numArray = new int[5]
              {
                ExpertConsts.Consts.attrListsBefore,
                ExpertConsts.Consts.attrScriptRef,
                ExpertConsts.Consts.attrObjForDoc,
                ExpertConsts.Consts.attrLists,
                ExpertConsts.Consts.attrChecksum
              };
              foreach (IMSAttribute4ObjectType attribute4ObjectType in MetaDataHelper.GetAttribute4ObjectTypeList(guid))
              {
                for (int index2 = 0; index2 < 5; ++index2)
                {
                  if (attribute4ObjectType.AttributeID == numArray[index2])
                    flagArray[index2] = true;
                }
              }
              if (!flagArray[0] || !flagArray[1] || !flagArray[2] || !flagArray[3] || !flagArray[4])
              {
                string format = LocalizationHolder.rm.GetString("Expert.Editor_563");
                string caption = LocalizationHolder.rm.GetString("Expert.Editor_552");
                string str = "";
                for (int index3 = 0; index3 < 5; ++index3)
                {
                  if (!flagArray[index3])
                  {
                    if (str != "")
                      str += ", ";
                    IDBAttributeTypeInfo attributeType = service.GetAttributeType(numArray[index3]);
                    str = $"{str}{{{attributeType.Name}}}";
                  }
                }
                int num = (int) MessageBox.Show(string.Format(format, (object) str), caption, MessageBoxButtons.OK);
              }
            }
          }
        }
        this.opData.s3 = Convert.ToString(docScripts.Rows[index1][1]);
        this.opData.s4 = Convert.ToString(docScripts.Rows[index1][2]);
        this.beDocScript.Text = this.opData.s4;
        this.opDataChanged = true;
      }
      else
      {
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_382") + LocalizationHolder.rm.GetString("Expert.Editor_381"), LocalizationHolder.rm.GetString("Expert.Editor_384"));
      }
    }
    else if (this.rbScenario.Checked)
    {
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_607"), LocalizationHolder.rm.GetString("Expert.Editor_608"), ExpertConsts.Consts.objScenario, SelectionOptions.Default);
      if (numArray == null || numArray.Length == 0)
        return;
      long objectID = numArray[0];
      string str = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
        str = dbObject.Caption;
        this.opData.s3 = dbObject.ObjectGUID.ToString();
      }
      this.opData.s4 = str;
      this.beDocScript.Text = str;
      this.opDataChanged = true;
    }
    else
    {
      if (!this.rbTechcard.Checked)
        return;
      if (ExpertConsts.Consts.objTechDocSettings == -1)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_692"), LocalizationHolder.rm.GetString("Expert.Editor_107"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_691"), LocalizationHolder.rm.GetString("Expert.Editor_692"), ExpertConsts.Consts.objTechDocSettings, SelectionOptions.Default);
        if (numArray == null || numArray.Length == 0)
          return;
        long objectID = numArray[0];
        string str = "";
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
          str = dbObject.Caption;
          this.opData.s3 = dbObject.ObjectGUID.ToString();
        }
        this.opData.s4 = str;
        this.beDocScript.Text = str;
        this.opDataChanged = true;
      }
    }
  }

  private void beScenario_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_607"), LocalizationHolder.rm.GetString("Expert.Editor_608"), ExpertConsts.Consts.objScenario, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      str = dbObject.Caption;
      this.opData.s3 = dbObject.ObjectGUID.ToString();
    }
    this.opData.s4 = str;
    this.opDataChanged = true;
  }

  private void rbScenario_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    RadioButton radioButton = sender as RadioButton;
    if (!radioButton.Checked)
      return;
    this.opData.st2 = (string) radioButton.Tag;
    this.opData.s3 = "";
    this.opData.s4 = "";
    this.beDocScript.Text = "";
    this.EnableDisable(this.panOpParmsE);
    this.opDataChanged = true;
  }

  private void ClipCopy()
  {
    MemoryStream w = new MemoryStream();
    XmlTextWriter writer = (XmlTextWriter) null;
    byte[] data = (byte[]) null;
    try
    {
      writer = new XmlTextWriter((Stream) w, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("ExpScriptPart");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
      this.WriteNodeToXML(ref writer, this.tree.FocusedNode);
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      w.Position = 0L;
      data = w.GetBuffer();
    }
    finally
    {
      writer?.Close();
    }
    Clipboard.SetData(ScriptEdit2.ExpClipFormat, (object) data);
    Clipboard.GetData(ScriptEdit2.ExpClipFormat);
  }

  private TreeListNode ClipAddNode(bool insInto, Intermech.Expert.NodeData d, string label, TreeListNode rootNode)
  {
    int parentNodeId1 = -1;
    if (rootNode != null)
      parentNodeId1 = rootNode.Id;
    TreeListNode node = (TreeListNode) null;
    if (this.tree.Nodes.Count == 0)
    {
      this.tree.AppendNode((object) new object[3]
      {
        (object) label,
        (object) d.GetShortMod(),
        (object) d.GetShortOp()
      }, parentNodeId1, d.modTag, d.modTag, d.opTag);
      node = this.tree.Nodes[this.tree.Nodes.Count - 1];
    }
    else if (rootNode != null)
    {
      if (insInto)
      {
        node = this.tree.AppendNode((object) new object[3]
        {
          (object) label,
          (object) d.GetShortMod(),
          (object) d.GetShortOp()
        }, parentNodeId1, d.modTag, d.modTag, d.opTag);
      }
      else
      {
        TreeListNode parentNode = rootNode.ParentNode;
        int parentNodeId2 = -1;
        int num;
        if (parentNode != null)
        {
          parentNodeId2 = parentNode.Id;
          num = parentNode.Nodes.IndexOf(rootNode);
        }
        else
          num = this.tree.Nodes.IndexOf(rootNode);
        this.tree.AppendNode((object) new object[3]
        {
          (object) label,
          (object) d.GetShortMod(),
          (object) d.GetShortOp()
        }, parentNodeId2, d.modTag, d.modTag, d.opTag);
        node = parentNode == null ? this.tree.Nodes[this.tree.Nodes.Count - 1] : parentNode.Nodes[parentNode.Nodes.Count - 1];
        if (num >= 0)
          this.tree.SetNodeIndex(node, num + 1);
      }
    }
    if (node != null)
      node.Tag = (object) d;
    return node;
  }

  private void ClipLoadNode(XmlNode xmlRoot, out string label, out Intermech.Expert.NodeData d)
  {
    int modTag = -1;
    int opTag = -1;
    label = "";
    if (xmlRoot.Attributes != null)
    {
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlRoot.Attributes)
      {
        if (attribute.Name == nameof (label))
          label = attribute.Value;
        else if (attribute.Name == "modTag")
          modTag = Convert.ToInt32(attribute.Value);
        else if (attribute.Name == "opTag")
          opTag = Convert.ToInt32(attribute.Value);
      }
    }
    d = new Intermech.Expert.NodeData(xmlRoot, modTag, opTag);
    if (!(d.ops is OpParmObject))
      return;
    OpParmObject ops = (OpParmObject) d.ops;
    if (ops.linkTypeIDs != null)
    {
      if (ops.linkTypeTexts == null)
        ops.linkTypeTexts = new ArrayList();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < ops.linkTypeIDs.Count; ++index)
        {
          IDBRelationType relationType = sessionKeeper.Session.GetRelationType(ops.linkTypeIDs[index], false);
          if (relationType != null)
            ops.linkTypeTexts.Add((object) relationType.Description);
          else
            ops.linkTypeTexts.Add((object) "<???>");
        }
      }
    }
    if (ops.objTypeIDs == null)
      return;
    if (ops.objTypeTexts == null)
      ops.objTypeTexts = new ArrayList();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < ops.objTypeIDs.Count; ++index)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(ops.objTypeIDs[index], false);
        if (objectType != null)
          ops.objTypeTexts.Add((object) objectType.ObjectTypeName);
        else
          ops.objTypeTexts.Add((object) "<???>");
      }
    }
  }

  private void ClipLoadChilds(TreeListNode root, XmlNode xmlRoot)
  {
    if (!xmlRoot.HasChildNodes)
      return;
    foreach (XmlNode childNode in xmlRoot.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "node")
      {
        string label = "";
        Intermech.Expert.NodeData d = (Intermech.Expert.NodeData) null;
        this.ClipLoadNode(childNode, out label, out d);
        this.ClipLoadChilds(this.ClipAddNode(true, d, label, root), childNode);
      }
    }
  }

  private void ClipPaste()
  {
    if (!Clipboard.ContainsData(ScriptEdit2.ExpClipFormat))
      return;
    MemoryStream inStream = new MemoryStream((byte[]) Clipboard.GetData(ScriptEdit2.ExpClipFormat));
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((Stream) inStream);
    bool insInto = MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_385"), LocalizationHolder.rm.GetString("Expert.Editor_386"), MessageBoxButtons.YesNo) == DialogResult.Yes;
    string label = "";
    Intermech.Expert.NodeData d = (Intermech.Expert.NodeData) null;
    if (!xmlDocument.DocumentElement.HasChildNodes)
      return;
    TreeListNode treeListNode = (TreeListNode) null;
    foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
    {
      this.ClipLoadNode(childNode, out label, out d);
      TreeListNode root = this.ClipAddNode(insInto, d, label, this.tree.FocusedNode);
      if (treeListNode == null)
        treeListNode = root;
      this.ClipLoadChilds(root, childNode);
    }
    if (treeListNode == null)
      return;
    this.tree.FocusedNode = treeListNode;
  }

  private void InitParmGrid()
  {
    this.gridTable.Redim(this.opData.listTable.Count + 1, 3);
    this.gridTable.FixedColumns = 0;
    this.gridTable.FixedRows = 1;
    SourceGrid3.Cells.Real.ColumnHeader columnHeader1 = new SourceGrid3.Cells.Real.ColumnHeader((object) LocalizationHolder.rm.GetString("Expert.Editor_387"));
    this.gridTable[0, 0] = (ICell) columnHeader1;
    columnHeader1.AutomaticSortEnabled = false;
    SourceGrid3.Cells.Real.ColumnHeader columnHeader2 = new SourceGrid3.Cells.Real.ColumnHeader((object) LocalizationHolder.rm.GetString("Expert.Editor_388"));
    this.gridTable[0, 1] = (ICell) columnHeader2;
    columnHeader2.AutomaticSortEnabled = false;
    SourceGrid3.Cells.Real.ColumnHeader columnHeader3 = new SourceGrid3.Cells.Real.ColumnHeader((object) LocalizationHolder.rm.GetString("Expert.Editor_389"));
    this.gridTable[0, 2] = (ICell) columnHeader3;
    columnHeader3.AutomaticSortEnabled = false;
    this.cc.EditEnded += new EventHandler(this.cc_EditEnded);
    this.cc.EditStarting += new CancelEventHandler(this.cc_EditStarting);
    this.gridTable.Columns.AutoSize(false);
    this.gridTable.Selection.EnableMultiSelection = false;
    this.gridTable.Columns[0].Width = 50;
    this.gridTable.Columns[1].Width = 50;
    this.gridTable.Columns[2].Width = this.gridTable.Width - this.gridTable.Columns[0].Width - this.gridTable.Columns[1].Width - 2;
    this.gridTable.Selection.SelectionMode = GridSelectionMode.Row;
    this.gridTable.Selection.EnableMultiSelection = false;
  }

  private void cc_EditStarting(object sender, CancelEventArgs e)
  {
  }

  private void cc_EditEnded(object sender, EventArgs e)
  {
    SourceGrid3.Cells.Real.Cell cell = ((SourceGrid3.CellContext) sender).Cell as SourceGrid3.Cells.Real.Cell;
    int column = ((SourceGrid3.CellContext) sender).Position.Column;
    int row = ((SourceGrid3.CellContext) sender).Position.Row;
    string str = Convert.ToString(cell.Value);
    bool flag = false;
    switch (column)
    {
      case 0:
        flag = this.opData.listTable[row - 1].From != str;
        if (flag)
        {
          this.opData.listTable[row - 1].From = str;
          break;
        }
        break;
      case 1:
        flag = this.opData.listTable[row - 1].To != str;
        if (flag)
        {
          this.opData.listTable[row - 1].To = str;
          break;
        }
        break;
      case 2:
        flag = this.opData.listTable[row - 1].Result != str;
        if (flag)
        {
          this.opData.listTable[row - 1].Result = str;
          break;
        }
        break;
    }
    if (!flag)
      return;
    this.opDataChanged = true;
  }

  private void tabCon_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.tabCon.SelectedIndex <= 0 || this.comboSelector.SelectedIndex == 1)
      return;
    this.tabCon.SelectedIndex = 0;
  }

  private void tabCon_Selecting(object sender, TabControlCancelEventArgs e)
  {
    if (e.TabPageIndex != 1 || this.comboSelector.SelectedIndex == 1)
      return;
    e.Cancel = true;
  }

  private void FillParmGrid()
  {
    this.gridTable.Redim(this.opData.listTable.Count + 1, 3);
    for (int index = 0; index < this.opData.listTable.Count; ++index)
    {
      Triple triple = this.opData.listTable[index];
      SourceGrid3.Cells.Real.Cell cell1 = new SourceGrid3.Cells.Real.Cell((object) triple.From, typeof (string));
      this.gridTable[index + 1, 0] = (ICell) cell1;
      cell1.AddController((IController) this.cc);
      SourceGrid3.Cells.Real.Cell cell2 = new SourceGrid3.Cells.Real.Cell((object) triple.To, typeof (string));
      this.gridTable[index + 1, 1] = (ICell) cell2;
      cell2.AddController((IController) this.cc);
      SourceGrid3.Cells.Real.Cell cell3 = new SourceGrid3.Cells.Real.Cell((object) triple.Result, typeof (string));
      this.gridTable[index + 1, 2] = (ICell) cell3;
      cell3.AddController((IController) this.cc);
    }
  }

  private void SaveParmGrid()
  {
    bool flag1 = false;
    for (int index = 0; index < this.opData.listTable.Count; ++index)
    {
      string str1 = Convert.ToString(((SourceGrid3.Cells.Real.Cell) this.gridTable[index + 1, 0]).Value);
      bool flag2 = flag1 | this.opData.listTable[index].From != str1;
      if (this.opData.listTable[index].From != str1)
        this.opData.listTable[index].From = str1;
      string str2 = Convert.ToString(((SourceGrid3.Cells.Real.Cell) this.gridTable[index + 1, 1]).Value);
      bool flag3 = flag2 | this.opData.listTable[index].To != str2;
      if (this.opData.listTable[index].To != str2)
        this.opData.listTable[index].To = str2;
      string str3 = Convert.ToString(((SourceGrid3.Cells.Real.Cell) this.gridTable[index + 1, 2]).Value);
      flag1 = flag3 | this.opData.listTable[index].Result != str3;
      if (this.opData.listTable[index].Result != str3)
        this.opData.listTable[index].Result = str3;
    }
    if (!flag1)
      return;
    this.opDataChanged = true;
  }

  private void SetCurIndex(int newIndex) => this.gridTable.Selection.FocusRow(newIndex);

  private int GetListIndex()
  {
    int[] rowsIndex = this.gridTable.Selection.GetRowsIndex();
    return rowsIndex.Length != 0 ? rowsIndex[0] : -1;
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    int listIndex = this.GetListIndex();
    if (listIndex >= 1)
      this.opData.listTable.Insert(listIndex, new Triple());
    else
      this.opData.listTable.Add(new Triple());
    this.FillParmGrid();
    if (listIndex >= 1)
      this.gridTable.Selection.FocusRow(listIndex);
    else
      this.gridTable.Selection.FocusRow(1);
    this.opDataChanged = true;
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    int num = this.GetListIndex();
    if (num < 1)
      return;
    this.opData.listTable.RemoveAt(num - 1);
    this.gridTable.Rows.Remove(num);
    if (num >= this.gridTable.RowsCount)
      num = this.gridTable.RowsCount - 1;
    this.gridTable.Selection.FocusRow(num);
    this.opDataChanged = true;
  }

  private void btnTableUp_Click(object sender, EventArgs e)
  {
    int listIndex = this.GetListIndex();
    if (listIndex <= 1 || this.gridTable.RowsCount <= 2)
      return;
    this.gridTable.Rows.Move(listIndex, listIndex - 1);
    this.gridTable.Selection.FocusRow(listIndex - 1);
    Triple triple = this.opData.listTable[listIndex - 1];
    this.opData.listTable.RemoveAt(listIndex - 1);
    this.opData.listTable.Insert(listIndex - 2, triple);
    this.opDataChanged = true;
  }

  private void btnTableDown_Click(object sender, EventArgs e)
  {
    int listIndex = this.GetListIndex();
    if (listIndex < 1 || listIndex >= this.gridTable.RowsCount - 1 || this.gridTable.RowsCount <= 2)
      return;
    Triple triple = this.opData.listTable[listIndex - 1];
    this.opData.listTable.RemoveAt(listIndex - 1);
    this.opData.listTable.Insert(listIndex, triple);
    this.gridTable.Rows.Move(listIndex, listIndex + 1);
    this.gridTable.Selection.FocusRow(listIndex + 1);
    this.opDataChanged = true;
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.Closed == null)
      return;
    this.Closed(sender, e);
  }

  private void opPopMenu_Opening(object sender, CancelEventArgs e)
  {
    RichTextBox srcCon = (RichTextBox) this.srcCon;
    if (srcCon == this.richGlobalFilter)
      this.copyOpToolStripMenuItem.Enabled = this.opData.tf2 != null && this.opData.tf2.Count > 0;
    else if (srcCon == this.richAfterFilter)
      this.copyOpToolStripMenuItem.Enabled = this.opData.tf3 != null && this.opData.tf3.Count > 0;
    else
      this.copyOpToolStripMenuItem.Enabled = this.opData.tf != null && this.opData.tf.Count > 0;
    this.pasteOpToolStripMenuItem.Enabled = Clipboard.ContainsData(TempFormula.FormulaFormat);
  }

  private void modPopMenu_Opening(object sender, CancelEventArgs e)
  {
    this.copyToolStripMenuItem.Enabled = this.modData.tf != null && this.modData.tf.Count > 0;
    this.pasteToolStripMenuItem.Enabled = Clipboard.ContainsData(TempFormula.FormulaFormat);
  }

  private void richForEnd_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (sender is RichTextBox)
      this.srcCon = (Control) sender;
    this.menuChangeModForm_Click(sender, (EventArgs) e);
  }

  private void richWhileCond_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (sender is RichTextBox)
      this.srcCon = (Control) sender;
    this.menuChangeModForm_Click(sender, (EventArgs) e);
  }

  private void richTextBox2_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (sender is RichTextBox)
      this.srcCon = (Control) sender;
    this.menuChangeModForm_Click(sender, (EventArgs) e);
  }

  private void tree_DragDrop(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    DialogResult dialogResult = DialogResult.No;
    if (this.ObjectType != ExpertScriptType.VisStyles && this.ObjectType != ExpertScriptType.VisDataScheme)
    {
      dialogResult = MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_385"), LocalizationHolder.rm.GetString("Expert.Editor_386"), MessageBoxButtons.YesNoCancel);
      if (dialogResult == DialogResult.Cancel)
        return;
    }
    int num = dialogResult == DialogResult.Yes ? 1 : 0;
    bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
    if (num != 0)
    {
      if (flag)
        this.tree.CopyNode(this.draggingNode, this.targetNode, true);
      else
        this.tree.MoveNode(this.draggingNode, this.targetNode);
      this.scriptChanged = true;
      this.UpdateSaveCancelButtons();
    }
    else
    {
      TreeListNode parentNode = this.targetNode.ParentNode;
      int index = parentNode == null ? this.tree.Nodes.IndexOf(this.targetNode) : parentNode.Nodes.IndexOf(this.targetNode);
      this.tree.MoveNode(this.draggingNode, parentNode);
      this.tree.SetNodeIndex(this.draggingNode, index);
      this.scriptChanged = true;
      this.UpdateSaveCancelButtons();
    }
  }

  private void tree_DragEnter(object sender, DragEventArgs e)
  {
  }

  private void tree_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    this.targetNode = this.tree.GetHitInfo(this.tree.PointToClient(new Point(e.X, e.Y))).Node;
    if (this.targetNode == null || this.targetNode == this.draggingNode || this.targetNode.HasAsParent(this.draggingNode))
      return;
    e.Effect = DragDropEffects.Move;
  }

  private void btnCheckDown_Click(object sender, EventArgs e)
  {
    TreeListNode treeListNode = this.checkScript();
    if (treeListNode != null)
    {
      this.tree.FocusedNode = treeListNode;
      new ShowXml().Execute(this.checkDoc);
    }
    else
    {
      int num = (int) MessageBox.Show("Никаких проблем не обнаружено!", "Проверка завершена", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  internal TreeListNode GetNextNode(TreeListNode curNode)
  {
    List<TreeListNode> treeListNodeList = this.CollectTree();
    for (int index = 0; index < treeListNodeList.Count; ++index)
    {
      if (treeListNodeList[index] == curNode)
        return index < treeListNodeList.Count - 1 ? treeListNodeList[index + 1] : (TreeListNode) null;
    }
    return (TreeListNode) null;
  }

  internal List<TreeListNode> CollectTree()
  {
    List<TreeListNode> nodeList = new List<TreeListNode>();
    foreach (TreeListNode node in this.tree.Nodes)
      this.AddTreeNode(nodeList, node);
    return nodeList;
  }

  internal void AddTreeNode(List<TreeListNode> nodeList, TreeListNode node)
  {
    nodeList.Add(node);
    if (node.Nodes == null || node.Nodes.Count <= 0)
      return;
    foreach (TreeListNode node1 in node.Nodes)
      this.AddTreeNode(nodeList, node1);
  }

  public void InitCheckDoc()
  {
    this.checkDoc = new XmlDocument();
    this.checkDoc.LoadXml($"<?xml version='1.0' encoding='utf-16'?><TraceInfo xmlns='{ScriptEdit2.ExpertNamespace}'></TraceInfo>");
    this.curNode = (XmlNode) this.checkDoc.DocumentElement;
  }

  public XmlNode traceAddElement(string Name)
  {
    Name = Name.Replace(' ', '_');
    XmlNode element = (XmlNode) this.checkDoc.CreateElement(Name, ScriptEdit2.ExpertNamespace);
    if (this.curNode != null)
      this.curNode.AppendChild(element);
    return element;
  }

  public XmlAttribute traceAddAttribute(XmlNode node, string Name, string Value)
  {
    XmlAttribute attribute = this.checkDoc.CreateAttribute(Name);
    attribute.Value = Convert.ToString(Value);
    node.Attributes.Append(attribute);
    return attribute;
  }

  public XmlNode traceAddText(XmlNode node, string Text)
  {
    XmlNode textNode = (XmlNode) this.checkDoc.CreateTextNode(Text);
    node.AppendChild(textNode);
    return textNode;
  }

  internal bool CheckNode(TreeListNode node, IUserSession ius)
  {
    bool flag = true;
    if (((string) node[(object) 0]).StartsWith("#"))
      return flag;
    Intermech.Expert.NodeData nodeData = this.data(node);
    if (nodeData != null)
    {
      if (nodeData.mods != null)
      {
        flag = this.CheckMod(nodeData.mods, ius);
        if (nodeData.modTag == 4 || nodeData.modTag == 5)
        {
          TempFormula tf = (nodeData.mods as ModParmFormula).tf;
          if (tf == null || tf.Count == 0)
          {
            this.traceAddText(this.traceAddElement("No_Condition"), "Условие в модификаторах 'Если существует' и 'Если все' не должно быть пустым!");
            return false;
          }
        }
      }
      if (nodeData.ops != null)
      {
        flag = flag && this.CheckOp(nodeData.ops, ius);
        if (nodeData.ops is OpCreateComplect)
        {
          OpCreateComplect ops = (OpCreateComplect) nodeData.ops;
          flag = flag && this.CheckComplectOp((string) node[(object) 0], ops);
        }
        if (nodeData.ops is OpParmGlobRoot)
        {
          foreach (TreeListNode node1 in node.Nodes)
          {
            if (!(this.data(node1).ops is OpParmGlobForType))
            {
              this.traceAddText(this.traceAddElement("Wrong_Global_Table"), "Внутри оператора глобальной таблицы могут быть только операторы 'Для типа объектов'!");
              return false;
            }
          }
        }
      }
    }
    return flag;
  }

  internal bool CheckMod(ModParm mp, IUserSession ius)
  {
    bool flag = true;
    switch (mp)
    {
      case ModParmFormula _:
        return this.CheckFormula((mp as ModParmFormula).tf, ius);
      case ModParmLoop _ when (mp as ModParmLoop).attrGUID != "":
        return this.CheckAttr((mp as ModParmLoop).attrGUID);
      case ModParmSort _:
        ModParmSort modParmSort = mp as ModParmSort;
        if (modParmSort.sortAttrs == null && modParmSort.groupAttrs == null)
        {
          this.traceAddText(this.traceAddElement("Wrong_Attr"), "Незаполненный модификатор");
          return false;
        }
        if (modParmSort.sortAttrs != null)
        {
          foreach (string sortAttr in modParmSort.sortAttrs)
            flag = this.CheckAttr(sortAttr) & flag;
        }
        if (modParmSort.groupAttrs != null)
        {
          using (List<string>.Enumerator enumerator = modParmSort.groupAttrs.GetEnumerator())
          {
            while (enumerator.MoveNext())
              flag = this.CheckAttr(enumerator.Current) & flag;
            break;
          }
        }
        break;
    }
    return flag;
  }

  internal bool CheckOp(OpParm op, IUserSession ius)
  {
    bool flag1 = true;
    if (op is OpParmObject)
    {
      OpParmObject opParmObject = (OpParmObject) op;
      bool flag2 = this.CheckObject(opParmObject.excerptID, ius) & flag1;
      bool flag3 = this.CheckFormula(opParmObject.cond, ius) & flag2;
      bool flag4 = this.CheckFormula(opParmObject.filter, ius) & flag3;
      bool flag5 = this.CheckFormula(opParmObject.afterFilter, ius) & flag4;
      if (opParmObject.dataAttrGUIDs != null)
      {
        foreach (string dataAttrGuiD in opParmObject.dataAttrGUIDs)
          flag5 = this.CheckAttr(dataAttrGuiD) & flag5;
      }
      flag1 = this.CheckObjType(opParmObject.objTypeForGlobalGUID) & flag5;
      if (opParmObject.objTypeIDs != null)
      {
        foreach (int objTypeId in opParmObject.objTypeIDs)
          flag1 = this.CheckObjType(objTypeId) & flag1;
      }
      if (opParmObject.linkTypeIDs != null)
      {
        foreach (int linkTypeId in opParmObject.linkTypeIDs)
          flag1 = this.CheckRelType(linkTypeId) & flag1;
      }
    }
    if (op is OpParmGlobRoot)
    {
      OpParmGlobRoot opParmGlobRoot = (OpParmGlobRoot) op;
      bool flag6 = this.CheckFormula(opParmGlobRoot.afterFilter, ius) & flag1;
      flag1 = this.CheckFormula(opParmGlobRoot.globalFilter, ius) & flag6;
      if (opParmGlobRoot.dataAttrGUIDs != null)
      {
        foreach (string dataAttrGuiD in opParmGlobRoot.dataAttrGUIDs)
          flag1 = this.CheckAttr(dataAttrGuiD) & flag1;
      }
      if (opParmGlobRoot.objTypeIDs != null)
      {
        foreach (int objTypeId in opParmGlobRoot.objTypeIDs)
          flag1 = this.CheckObjType(objTypeId) & flag1;
      }
      if (opParmGlobRoot.linkTypeIDs != null)
      {
        foreach (int linkTypeId in opParmGlobRoot.linkTypeIDs)
          flag1 = this.CheckRelType(linkTypeId > 100000 ? linkTypeId - 100000 : linkTypeId) & flag1;
      }
    }
    if (op is OpParmGlobForType)
    {
      OpParmGlobForType opParmGlobForType = (OpParmGlobForType) op;
      flag1 = this.CheckFormula(opParmGlobForType.afterFilter, ius) & flag1;
      if (opParmGlobForType.dataAttrGUIDs != null)
      {
        foreach (string dataAttrGuiD in opParmGlobForType.dataAttrGUIDs)
          flag1 = this.CheckAttr(dataAttrGuiD) & flag1;
      }
      if (opParmGlobForType.forObjTypeGUIDs != null)
      {
        foreach (string forObjTypeGuiD in opParmGlobForType.forObjTypeGUIDs)
          flag1 = this.CheckObjType(forObjTypeGuiD) & flag1;
      }
      if (opParmGlobForType.objTypeIDs != null)
      {
        foreach (int objTypeId in opParmGlobForType.objTypeIDs)
          flag1 = this.CheckObjType(objTypeId) & flag1;
      }
      if (opParmGlobForType.linkTypeIDs != null)
      {
        foreach (int linkTypeId in opParmGlobForType.linkTypeIDs)
          flag1 = this.CheckRelType(linkTypeId > 100000 ? linkTypeId - 100000 : linkTypeId) & flag1;
      }
    }
    if (op is OpParmCond)
    {
      OpParmCond opParmCond = (OpParmCond) op;
      bool flag7 = this.CheckFormula(opParmCond.cond, ius) & flag1;
      flag1 = this.CheckAttr(opParmCond.refAttrGuid) & flag7;
    }
    if (op is OpParmSetting)
    {
      OpParmSetting opParmSetting = (OpParmSetting) op;
      if (opParmSetting.attrGUID == "")
      {
        this.traceAddText(this.traceAddElement("Setting_No_Attr"), "Не задан идентификатор присваиваемого атрибута");
        flag1 = false;
      }
      if (opParmSetting.setKind != ExpertSettingKind.setKindByTable && opParmSetting.tf == null)
      {
        this.traceAddText(this.traceAddElement("Setting_No_Formula"), "Не задана формула расчета атрибута");
        flag1 = false;
      }
      if (opParmSetting.setKind == ExpertSettingKind.setKindByTable && (opParmSetting.listTable == null || opParmSetting.listTable.Count == 0))
      {
        this.traceAddText(this.traceAddElement("Setting_No_Table"), "Не задана таблица значений атрибута");
        flag1 = false;
      }
      bool flag8 = this.CheckFormula(opParmSetting.tf, ius) & flag1;
      bool flag9 = this.CheckFormula(opParmSetting.formX, ius) & flag8;
      bool flag10 = this.CheckFormula(opParmSetting.formY, ius) & flag9;
      bool flag11 = this.CheckAttr(opParmSetting.attrGUID) & flag10;
      flag1 = this.CheckObjType(opParmSetting.objTypeGUID) & flag11;
    }
    if (op is OpParmType)
    {
      OpParmType opParmType = (OpParmType) op;
      bool flag12 = this.CheckFormula(opParmType.cond, ius) & flag1;
      flag1 = this.CheckObjType(opParmType.objTypeGUID) & flag12;
    }
    if (op is OpParmExpObj)
    {
      OpParmExpObj opParmExpObj = (OpParmExpObj) op;
      bool flag13 = this.CheckFormula(opParmExpObj.cond, ius) & flag1;
      bool flag14 = this.CheckFormula(opParmExpObj.objCond, ius) & flag13;
      flag1 = this.CheckObject(opParmExpObj.objTypeGUID) & flag14;
    }
    if (op is OpCreateComplect)
    {
      OpCreateComplect opCreateComplect = (OpCreateComplect) op;
      bool flag15 = this.CheckFormula(opCreateComplect.cond, ius) & flag1;
      bool flag16 = this.CheckObjType(opCreateComplect.compObjTypeGUID) & flag15;
      flag1 = this.CheckObjType(opCreateComplect.objTypeGUID) & flag16;
    }
    if (op is OpCreateDoc)
    {
      OpCreateDoc opCreateDoc = (OpCreateDoc) op;
      bool flag17 = this.CheckFormula(opCreateDoc.cond, ius) & flag1;
      flag1 = this.CheckObjType(opCreateDoc.objTypeGUID) & flag17;
      if (opCreateDoc.objTypeGUID == "")
      {
        this.traceAddText(this.traceAddElement("Wrong_ObjType"), "Не задан тип объекта, для которого надо генерировать документы!");
        flag1 = false;
      }
    }
    if (op is OpParmFillFld)
    {
      OpParmFillFld opParmFillFld = (OpParmFillFld) op;
      if (this.showTemp.Template != null)
      {
        if (opParmFillFld.FldID == "")
        {
          flag1 = false;
          this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), "Идентификатор поля пуст");
        }
        else
        {
          DocumentTreeNode node = this.showTemp.Template.FindNode(opParmFillFld.FldID);
          if (node == null)
          {
            flag1 = false;
            this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), $"Поле с идентификатором \"{opParmFillFld.FldID}\" в шаблоне не найдено!");
          }
          if (!(node is TextData) && !(node is ContainerData))
          {
            flag1 = false;
            this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), string.Format("Данные можно записывать только в текстовые поля или контейнеры!", (object) opParmFillFld.FldID));
          }
        }
      }
      bool flag18 = this.CheckFormula(opParmFillFld.tf, ius) & flag1;
      bool flag19 = this.CheckFormula(opParmFillFld._leftInd, ius) & flag18;
      flag1 = this.CheckAttr(opParmFillFld.AddAttrGUID) & flag19;
      if (opParmFillFld.tf == null)
      {
        bool flag20 = this.CheckAttr(opParmFillFld.attrGUID) & flag1;
        flag1 = this.CheckObjType(opParmFillFld.objTypeGUID) & flag20;
        if (opParmFillFld.attrGUID == "")
        {
          flag1 = false;
          this.traceAddText(this.traceAddElement("Empty_Oper_Fld"), string.Format("Незаполненный оператор ввода данных в поле!", (object) opParmFillFld.FldID));
        }
      }
    }
    if (op is OpParmSelFld)
    {
      OpParmSelFld opParmSelFld = (OpParmSelFld) op;
      if (opParmSelFld.FldId == "")
      {
        flag1 = false;
        this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), "Идентификатор поля пуст");
      }
      else if (this.showTemp.Template.FindNode(opParmSelFld.FldId) == null)
      {
        flag1 = false;
        this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), $"Поле с идентификатором \"{opParmSelFld.FldId}\" в шаблоне не найдено!");
      }
      flag1 = this.CheckFormula(opParmSelFld.tf, ius) & flag1;
    }
    if (op is OpParmUserProc)
    {
      OpParmUserProc opParmUserProc = (OpParmUserProc) op;
      bool flag21 = this.CheckFormula(opParmUserProc.parm1, ius) & flag1;
      flag1 = this.CheckFormula(opParmUserProc.parm2, ius) & flag21;
    }
    if (op is OpParmCreateFld)
    {
      OpParmCreateFld opParmCreateFld = (OpParmCreateFld) op;
      bool flag22 = this.CheckAttr(opParmCreateFld.AddAttrGUID) & flag1;
      flag1 = this.CheckAttr(opParmCreateFld.SaveIDAttrGUID) & flag22;
      if (opParmCreateFld.FldID == "")
      {
        flag1 = false;
        this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), "Идентификатор поля пуст");
      }
      else if (this.showTemp.Template.FindNode(opParmCreateFld.FldID) == null)
      {
        flag1 = false;
        this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), $"Поле с идентификатором \"{opParmCreateFld.FldID}\" в шаблоне не найдено!");
      }
      if (!opParmCreateFld.makeNewCurrent)
      {
        flag1 = false;
        this.traceAddText(this.traceAddElement("Creation_Warn"), "В операторе создания элемента документа не установлен переключатель \"Сделать новый элемент текущим\"");
      }
    }
    if (op is OpParmVersionRule && ((OpParmVersionRule) op).ruleGuid == "")
    {
      flag1 = false;
      this.traceAddText(this.traceAddElement("Wrong_Template_Fld"), "Правило подбора не задано");
    }
    OpParmDocControl opParmDocControl = op as OpParmDocControl;
    return flag1;
  }

  internal bool CheckComplectOp(string label, OpCreateComplect occ)
  {
    bool flag = true;
    if (label == "" && occ.additional)
    {
      flag = false;
      this.traceAddText(this.traceAddElement("No_Complect_Tag"), "Дополнительный комплект должен иметь уникальный тег!");
    }
    return flag;
  }

  internal bool CheckFormula(TempFormula tf, IUserSession ius)
  {
    if (tf == null)
      return true;
    bool flag = true;
    foreach (AttribPair usedAttr in tf.usedAttrs)
    {
      if (MetaDataHelper.GetAttributeType(usedAttr.attribID) == null)
      {
        flag = false;
        this.traceAddText(this.traceAddElement("Wrong_Attr"), $"Атрибут \"{usedAttr.attribID.ToString()}\" не найден!");
      }
      if (usedAttr.objTypeID != -1 && MetaDataHelper.GetObjectType(usedAttr.objTypeID) == null)
      {
        flag = false;
        this.traceAddText(this.traceAddElement("Wrong_ObjType"), $"Тип объекта \"{usedAttr.objTypeID.ToString()}\" не найден!");
      }
    }
    foreach (Token token in tf.infixForm)
    {
      if (token.type == Intermech.Expert.TokenType.Integer)
      {
        switch (token.spt)
        {
          case SelectionParameterTypes.sptObject:
            if (ius.GetObject(token.iValue, false) == null)
            {
              this.traceAddText(this.traceAddElement("Missing_Object"), $"В базе данных не найден объект [{token.iValue}]!");
              flag = false;
              continue;
            }
            continue;
          case SelectionParameterTypes.sptObjectType:
            if (ius.GetObjectType((int) token.iValue, false) == null)
            {
              this.traceAddText(this.traceAddElement("Missing_ObjType"), $"В базе данных нет типа объекта [{token.iValue}]!");
              flag = false;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    return flag;
  }

  internal bool CheckAttr(string guidStr)
  {
    if (guidStr == "")
      return true;
    int num = MetaDataHelper.GetAttributeTypeID(guidStr) != -10000 ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_Attr"), $"Атрибут \"{guidStr}\" не найден!");
    return num != 0;
  }

  internal bool CheckAttr(Guid attrGuid)
  {
    if (attrGuid.ToString() == "")
      return true;
    int num = MetaDataHelper.GetAttributeTypeID(attrGuid) != -10000 ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_Attr"), $"Атрибут \"{attrGuid.ToString()}\" не найден!");
    return num != 0;
  }

  internal bool CheckObjType(string guidStr)
  {
    if (guidStr == "")
      return true;
    int num = MetaDataHelper.GetObjectTypeID(guidStr) != -1 ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_ObjType"), $"Тип объекта \"{guidStr}\" не найден!");
    return num != 0;
  }

  internal bool CheckObjType(int objTypeId)
  {
    int num = !MetaDataHelper.GetObjectTypeGuid(objTypeId).Equals(Guid.Empty) ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_ObjType"), $"Тип объекта \"{objTypeId.ToString()}\" не найден!");
    return num != 0;
  }

  internal bool CheckObject(string guidStr)
  {
    if (guidStr == "")
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectInfo(new Guid(guidStr)).Empty)
      {
        this.traceAddText(this.traceAddElement("Wrong_Object"), $"Oбъект \"{guidStr}\" не найден!");
        return false;
      }
    }
    return true;
  }

  internal bool CheckObject(long objID, IUserSession ius)
  {
    if (objID == 0L || !ius.GetObjectInfo(objID).Empty)
      return true;
    this.traceAddText(this.traceAddElement("Wrong_Object"), $"Oбъект \"{objID}\" не найден!");
    return false;
  }

  internal bool CheckRelType(string guidStr)
  {
    if (guidStr == "")
      return true;
    int num = MetaDataHelper.GetRelationTypeID(guidStr) != -1 ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_RelType"), $"Тип связи \"{guidStr}\" не найден!");
    return num != 0;
  }

  internal bool CheckRelType(int relTypeId)
  {
    int num = !MetaDataHelper.GetRelationTypeGuid(relTypeId).Equals(Guid.Empty) ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.traceAddText(this.traceAddElement("Wrong_RelType"), $"Тип связи \"{relTypeId.ToString()}\" не найден!");
    return num != 0;
  }

  internal void CheckDocNode(DocumentTreeNode dtn)
  {
    if (dtn is TableData && (dtn as TableData).IsRow)
    {
      if (dtn.CloneByTemplateWithParent)
        this.traceAddText(this.traceAddElement("Mandatory_String"), $"Строка шаблона [{dtn.Id}] является обязательным элементом");
    }
    else if (!dtn.CloneByTemplateWithParent)
      this.traceAddText(this.traceAddElement("Not_Mandatory_String"), $"Столбец шаблона [{dtn.Id}] не является обязательным элементом");
    if (dtn.Nodes == null)
      return;
    foreach (DocumentTreeNode node in dtn.Nodes)
      this.CheckDocNode(node);
  }

  private void button2_Click_1(object sender, EventArgs e)
  {
    TreeNode node1 = this.tvTypes.Nodes[0];
    TreeNode node2 = this.tvTypes.Nodes[1];
    TreeNode selectedNode = this.tvTypes.SelectedNode;
    if (selectedNode.Parent != node1 && selectedNode.Parent != node2)
      return;
    if (selectedNode.Parent == node1)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < this.opData.objTexts.Count; ++index2)
      {
        if (this.opData.objTexts[index2] == selectedNode.Text)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 >= 0)
      {
        this.opData.objTexts.RemoveAt(index1);
        this.opData.objGUIDs.RemoveAt(index1);
        this.tvTypes.Nodes.Remove(selectedNode);
        this.opDataChanged = true;
      }
    }
    if (selectedNode.Parent != node2)
      return;
    int index3 = -1;
    for (int index4 = 0; index4 < this.opData.linkTexts.Count; ++index4)
    {
      if (this.opData.linkTexts[index4] == selectedNode.Text)
      {
        index3 = index4;
        break;
      }
    }
    if (index3 < 0)
      return;
    this.opData.linkTexts.RemoveAt(index3);
    this.opData.linkIDs.RemoveAt(index3);
    this.tvTypes.Nodes.Remove(selectedNode);
    this.opDataChanged = true;
  }

  private void upToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    TreeNode selectedNode = this.tvObjAttrs.SelectedNode;
    int index = selectedNode.Index;
    if (sender == this.upToolStripMenuItem)
    {
      if (index == 0)
        return;
      this.opData.dA_Texts.SwapBefore(index);
      this.opData.dA_GUIDs.SwapBefore(index);
      this.opData.dA_Checks.SwapBefore(index);
      selectedNode.MoveUp();
    }
    if (sender == this.downToolStripMenuItem)
    {
      if (index == selectedNode.Parent.Nodes.Count - 1)
        return;
      this.opData.dA_Texts.SwapAfter(index);
      this.opData.dA_GUIDs.SwapAfter(index);
      this.opData.dA_Checks.SwapAfter(index);
      selectedNode.MoveDown();
    }
    if (sender == this.firstToolStripMenuItem)
    {
      if (index == 0)
        return;
      this.opData.dA_Texts.MoveFirst(index);
      this.opData.dA_GUIDs.MoveFirst(index);
      this.opData.dA_Checks.MoveFirst(index);
      selectedNode.MoveFirst();
    }
    if (sender == this.lastToolStripMenuItem)
    {
      if (index == selectedNode.Parent.Nodes.Count - 1)
        return;
      this.opData.dA_Texts.MoveLast(index);
      this.opData.dA_GUIDs.MoveLast(index);
      this.opData.dA_Checks.MoveLast(index);
      selectedNode.MoveLast();
    }
    this.opDataChanged = true;
  }

  private void elMoveMenu_Opening(object sender, CancelEventArgs e)
  {
    if (this.tvObjAttrs.SelectedNode.Parent != null)
      return;
    e.Cancel = true;
  }

  private void cbNoCount_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b6 = this.cbNoCount.Checked;
    this.opDataChanged = true;
  }

  private void textBox2_Leave(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.s5 = this.textBox2.Text;
    this.opDataChanged = true;
  }

  private void textBox3_Leave(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.s6 = this.textBox3.Text;
    this.opDataChanged = true;
  }

  private void buttonEdit4_Properties_ButtonClick_1(object sender, ButtonPressedEventArgs e)
  {
    if (this.ASF.ShowDialog() != DialogResult.OK || this.ASF.SelectedAttributesGuid.Count <= 0)
      return;
    Guid anAttributeGuid = this.ASF.SelectedAttributesGuid[0];
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
      if (attributeType.MultipleValued != MultiValueModes.MultiValues && attributeType.MultipleValued != MultiValueModes.MultiValuesFromList)
      {
        int num = (int) MessageBox.Show("Выбранный атрибут должен иметь множество значений!", "Ошибка", MessageBoxButtons.OK);
        return;
      }
      str = !(attributeType.ShortName != "") ? attributeType.Name : attributeType.ShortName;
    }
    this.modData.sortGUIDs.Clear();
    this.modData.sortGUIDs.Add(Convert.ToString((object) anAttributeGuid));
    this.modData.sortTexts.Clear();
    this.modData.sortTexts.Add(str);
    this.btnRefAttr.Text = str;
    this.modDataChanged = true;
  }

  private void cbAddToGlobal_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b3 = this.cbAddToGlobal.Checked;
    this.opDataChanged = true;
  }

  private void rbActualSubst_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.s3 = sender != this.rbActualSubst ? (sender != this.rbAllSubst ? "C" : "A") : "M";
    this.opDataChanged = true;
  }

  private void beDocType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    bool flag = sender == this.beSourceType;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_687"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    int id = (int) selectorForm.IDList[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id);
      if (flag)
      {
        this.opData.s1 = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
        this.opData.s2 = objectType.PropertiesStructure.ObjectTypeName;
      }
      else
      {
        this.opData.st1 = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
        this.opData.st2 = objectType.PropertiesStructure.ObjectTypeName;
      }
      if (sender is ButtonEdit)
        (sender as ButtonEdit).Text = objectType.PropertiesStructure.ObjectTypeName;
    }
    this.opDataChanged = true;
  }

  private void btnAddObjAttr_Click(object sender, EventArgs e)
  {
    if (!this.ChooseAttr(false, (ButtonEdit) null) || this.selAttrName == "")
      return;
    bool flag = sender == this.btnAddRelAttr;
    for (int index = 0; index < this.opData.dA_GUIDs.Count; ++index)
    {
      if (this.opData.dA_GUIDs[index] == this.selAttrGUID)
      {
        bool attrCheck = this.GetAttrCheck(index);
        if (flag & attrCheck || !flag && !attrCheck)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_345"), LocalizationHolder.rm.GetString("Expert.Editor_346"));
          return;
        }
      }
    }
    this.lockChanged = true;
    try
    {
      TreeNode node1 = this.tvCopiedAttrs.Nodes[0];
      TreeNode node2 = this.tvCopiedAttrs.Nodes[1];
      TreeNode node3 = new TreeNode(this.selAttrName);
      if (flag)
        node2.Nodes.Add(node3);
      else
        node1.Nodes.Add(node3);
      node3.ImageIndex = 61;
      node3.SelectedImageIndex = node3.ImageIndex;
      this.opData.dA_Texts.Add(this.selAttrName);
      this.opData.dA_GUIDs.Add(this.selAttrGUID);
      this.opData.dA_Checks.Add(flag ? "Y" : "N");
      this.tvCopiedAttrs.ExpandAll();
    }
    finally
    {
      this.lockChanged = false;
    }
    this.opDataChanged = true;
  }

  private void btnDeleteAttr_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvCopiedAttrs.SelectedNode;
    if (selectedNode == null || selectedNode.Parent == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_347"), LocalizationHolder.rm.GetString("Expert.Editor_348"));
    }
    else
    {
      bool flag = selectedNode.Parent == this.tvCopiedAttrs.Nodes[1];
      for (int index = 0; index < this.opData.dA_Texts.Count; ++index)
      {
        if (this.opData.dA_Texts[index] == selectedNode.Text && this.GetAttrCheck(index) == flag)
        {
          this.opData.dA_GUIDs.RemoveAt(index);
          this.opData.dA_Texts.RemoveAt(index);
          this.opData.dA_Checks.RemoveAt(index);
          this.tvCopiedAttrs.Nodes.Remove(selectedNode);
          break;
        }
      }
      this.opDataChanged = true;
    }
  }

  private void cbMakeListCurrent_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b2 = (sender as System.Windows.Forms.CheckBox).Checked;
    this.opDataChanged = true;
  }

  private void btnTextColor_Click(object sender, EventArgs e)
  {
    if (this.textColorDlg.ShowDialog() != DialogResult.OK)
      return;
    this.btnTextColor.ForeColor = this.textColorDlg.Color;
    this.opData.settingMod = this.textColorDlg.Color.ToArgb();
    this.opDataChanged = true;
  }

  private void cbAuthFile_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b6 = (sender as System.Windows.Forms.CheckBox).Checked;
    this.opDataChanged = true;
  }

  private void cbAdditionalComp_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.opData.b3 = (sender as System.Windows.Forms.CheckBox).Checked;
    this.opDataChanged = true;
  }

  private void rbObject_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged || !(sender is RadioButton radioButton) || !radioButton.Checked)
      return;
    if (sender == this.rbObject)
      this.opData.s4 = "O";
    if (sender == this.rbRelation)
      this.opData.s4 = "R";
    if (sender == this.rbDocField)
      this.opData.s4 = "F";
    this.opDataChanged = true;
  }

  private void rightDock_SizeChanged(object sender, EventArgs e)
  {
    this.groupBox6.Width = this.rightDock.Width - 30;
    this.gbIspoln.Width = this.rightDock.Width - 30;
    this.gbComposition.Width = this.rightDock.Width - 30;
    this.groupBox7.Width = this.rightDock.Width - 30;
    this.richGlobalFilter.Width = this.rightDock.Width - 30;
    this.richAfterFilter.Width = this.rightDock.Width - 30;
    this.groupBox8.Width = this.rightDock.Width - 30;
    this.gbSostav1.Width = this.rightDock.Width - 30;
    this.gbGRIsps.Width = this.rightDock.Width - 35;
    this.rtbGlobObjFilter.Width = this.rightDock.Width - 35;
    this.globAddLinkDown.Left = this.rightDock.Width - 60;
    this.globAddLinkUp.Left = this.rightDock.Width - 60;
    this.globAddObjType.Left = this.rightDock.Width - 60;
    this.globDelete.Left = this.rightDock.Width - 60;
    this.btnGlobExcClear.Left = this.rightDock.Width - 60;
    this.btnGlobExcCreate.Left = this.btnGlobExcClear.Left - 40;
    this.tvGlobRoot.Width = this.globAddLinkDown.Left - 20;
    this.beGlobExcerpt.Width = this.btnGlobExcCreate.Left - 20;
    this.gbSostav2.Width = this.rightDock.Width - 30;
    this.rtGTCond.Width = this.rightDock.Width - 35;
    this.gtAddLinkDown.Left = this.rightDock.Width - 60;
    this.gtAddLinkUp.Left = this.rightDock.Width - 60;
    this.gtAddObjType.Left = this.rightDock.Width - 60;
    this.gtDelete.Left = this.rightDock.Width - 60;
    this.btnGTExcClear.Left = this.rightDock.Width - 60;
    this.btnGTExcCreate.Left = this.btnGTExcClear.Left - 40;
    this.tvGTSearch.Width = this.gtAddLinkDown.Left - 20;
    this.beGTExcerpt.Width = this.btnGTExcCreate.Left - 20;
  }

  private void dockOpParms_SizeChanged(object sender, EventArgs e)
  {
    this.groupBox8.Top = this.dockOpParms.Height - 150;
    int num = this.groupBox8.Top - this.groupBox7.Bottom - this.label36.Height - 60;
    this.richAfterFilter.Height = num / 2;
    this.richGlobalFilter.Height = num / 2;
    this.richAfterFilter.Top = this.groupBox8.Top - num / 2 - 10;
    this.label36.Top = this.richAfterFilter.Top - this.label36.Height - 6;
    this.richGlobalFilter.Top = this.label36.Top - num / 2 - 10;
    this.label5.Top = this.richGlobalFilter.Top - this.label5.Height - 6;
  }

  private void menuCollapse_Click(object sender, EventArgs e)
  {
    if (!this.CheckFocusNode(this.tree.FocusedNode))
      return;
    this.PropagateExpanded(this.tree.FocusedNode, false);
  }

  private void menuExpand_Click(object sender, EventArgs e)
  {
    if (!this.CheckFocusNode(this.tree.FocusedNode))
      return;
    this.PropagateExpanded(this.tree.FocusedNode, true);
  }

  private void PropagateExpanded(TreeListNode node, bool expanded)
  {
    node.Expanded = expanded;
    if (node.Nodes == null)
      return;
    foreach (TreeListNode node1 in node.Nodes)
      this.PropagateExpanded(node1, expanded);
  }

  private void rbFirstVersions_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.ForLoop = !this.rbFirstVersion.Checked;
    this.modDataChanged = true;
  }

  private void rbAllVersions_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.ForLoop = this.rbAllVersions.Checked;
    this.modDataChanged = true;
  }

  private void cbSortVersions_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.startValue = this.cbSortVersions.SelectedIndex;
    this.modDataChanged = true;
  }

  private void cbVerDescending_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.modData.Bool1 = this.cbVerDescending.Checked;
    this.modDataChanged = true;
  }

  private void btnUpdate_Click(object sender, EventArgs e)
  {
    this.UpdateESCond(new Guid(this.opData.s1));
  }

  public delegate void CreateEventHandler(object sender, CreateEventArgs e);

  public class ScriptInvalidException : ApplicationException
  {
    public TreeListNode wrongNode;

    public ScriptInvalidException(string Message, TreeListNode node)
      : base(LocalizationHolder.rm.GetString("Expert.Editor_371") + Message)
    {
      this.wrongNode = node;
    }
  }
}
