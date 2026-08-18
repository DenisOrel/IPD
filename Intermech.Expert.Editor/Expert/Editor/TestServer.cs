// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TestServer
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.LookAndFeel;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraTab;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for TestServer.</summary>
public class TestServer : Form
{
  private XtraTabControl tabCon;
  private XtraTabPage xtraTabPage1;
  private XtraTabPage xtraTabPage2;
  private Panel panel1;
  private Label label2;
  private Label label1;
  private Button btnSelContext;
  private Button btnStartTask;
  private Button btnTraceInfo;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private IExpertServer iExpServ;
  private ButtonEdit edDocScript;
  private long docScriptId;
  private long complectID;
  private ListBox lb;
  private long[] context;
  private Button btnStopTask;
  private TreeView XmlView;
  private int taskId;
  private XtraTabPage xtraTabPage3;
  private Panel panel2;
  private TreeView treeView1;
  private Thread thread;
  private Button btnDoc;
  private ExpertTask et;
  private TempFormula tf;
  private FormEditor formEdit;
  private long formObjId = -1;
  private int selAttrId = -1;
  private XtraTabPage xtraTabPage4;
  private Label label5;
  private System.Windows.Forms.ComboBox cbType;
  private Button btnChange;
  private ButtonEdit be2;
  private Label label6;
  private Panel panel3;
  private TreeView xView2;
  private Button btnStartCalc;
  private Button button6;
  private RichTextBox memoForm;
  private ButtonEdit be3;
  private Label label7;
  private ButtonEdit be4;
  private Label label8;
  private Button button7;
  private Panel panel4;
  private TreeView xView3;
  private GroupBox groupBox1;
  private CheckBox cbShowExpertObj;
  private CheckBox cbShowObjects;
  private CheckBox cbShowObjConds;
  private CheckBox cbTraceAttrs;
  private CheckBox cbTraceTables;
  private CheckBox cbTraceScripts;
  private CheckBox cbShowContext;
  private CheckBox cbShowAttrChange;
  private CheckBox cbShowScriptConds;
  private CheckBox cbShowObjResults;
  private CheckBox cbTraceObjectSearch;
  private CheckBox cbShowFillDocs;
  private Label label4;
  private SpinEdit seInterval;
  private Label label3;
  private Label label10;
  private SpinEdit seStrInterval;
  private Label label9;
  private ExpertTraceFlags traceFlags = ExpertTraceFlags.ShowExpertObjects | ExpertTraceFlags.TraceTables | ExpertTraceFlags.TraceScripts | ExpertTraceFlags.ShowContext;
  private int infoInterval = 1000;
  private int lastStrInterval = 100;
  private GroupBox groupBox2;
  private Label label11;
  private ButtonEdit beFormula;
  private MemoryStream doc;
  private CheckBox cbShowSettings;
  private XtraTabPage xtraTabPage5;
  private Panel panel5;
  private ButtonEdit beComplectScript;
  private Label label12;
  private ButtonEdit beComplectContext;
  private Label label13;
  private GroupBox groupBox3;
  private RadioButton rbCreateVersion;
  private RadioButton rbRefresh;
  private RadioButton rbCreate;
  private Button button1;
  private Button button2;
  private Label label14;
  private ButtonEdit beComplect;
  private Button button3;
  private Button btnStopCalc;
  private long formulaID = -1;
  private XtraTabPage xtraTabPage6;
  private Panel panel6;
  private TreeView xView6;
  private Button btnStopInner;
  private Button btnInfoInner;
  private Button btnStartInner;
  private ButtonEdit be6;
  private Label label15;
  private int esObjectType = -1;

  public TestServer() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TestServer));
    this.tabCon = new XtraTabControl();
    this.xtraTabPage1 = new XtraTabPage();
    this.XmlView = new TreeView();
    this.panel1 = new Panel();
    this.btnDoc = new Button();
    this.btnStopTask = new Button();
    this.btnTraceInfo = new Button();
    this.btnStartTask = new Button();
    this.btnSelContext = new Button();
    this.lb = new ListBox();
    this.label2 = new Label();
    this.edDocScript = new ButtonEdit();
    this.label1 = new Label();
    this.xtraTabPage5 = new XtraTabPage();
    this.panel5 = new Panel();
    this.label14 = new Label();
    this.beComplect = new ButtonEdit();
    this.button1 = new Button();
    this.button2 = new Button();
    this.groupBox3 = new GroupBox();
    this.rbCreateVersion = new RadioButton();
    this.rbRefresh = new RadioButton();
    this.rbCreate = new RadioButton();
    this.label13 = new Label();
    this.beComplectContext = new ButtonEdit();
    this.beComplectScript = new ButtonEdit();
    this.label12 = new Label();
    this.xtraTabPage2 = new XtraTabPage();
    this.panel4 = new Panel();
    this.xView3 = new TreeView();
    this.button7 = new Button();
    this.be4 = new ButtonEdit();
    this.label8 = new Label();
    this.be3 = new ButtonEdit();
    this.label7 = new Label();
    this.xtraTabPage4 = new XtraTabPage();
    this.btnStopCalc = new Button();
    this.button3 = new Button();
    this.label11 = new Label();
    this.beFormula = new ButtonEdit();
    this.groupBox2 = new GroupBox();
    this.memoForm = new RichTextBox();
    this.button6 = new Button();
    this.label5 = new Label();
    this.cbType = new System.Windows.Forms.ComboBox();
    this.btnStartCalc = new Button();
    this.panel3 = new Panel();
    this.xView2 = new TreeView();
    this.label6 = new Label();
    this.be2 = new ButtonEdit();
    this.btnChange = new Button();
    this.xtraTabPage6 = new XtraTabPage();
    this.btnStopInner = new Button();
    this.btnInfoInner = new Button();
    this.btnStartInner = new Button();
    this.be6 = new ButtonEdit();
    this.label15 = new Label();
    this.panel6 = new Panel();
    this.xView6 = new TreeView();
    this.xtraTabPage3 = new XtraTabPage();
    this.treeView1 = new TreeView();
    this.panel2 = new Panel();
    this.label10 = new Label();
    this.seStrInterval = new SpinEdit();
    this.label9 = new Label();
    this.label4 = new Label();
    this.seInterval = new SpinEdit();
    this.label3 = new Label();
    this.groupBox1 = new GroupBox();
    this.cbShowSettings = new CheckBox();
    this.cbTraceObjectSearch = new CheckBox();
    this.cbShowFillDocs = new CheckBox();
    this.cbShowScriptConds = new CheckBox();
    this.cbShowObjResults = new CheckBox();
    this.cbShowAttrChange = new CheckBox();
    this.cbShowContext = new CheckBox();
    this.cbTraceScripts = new CheckBox();
    this.cbTraceTables = new CheckBox();
    this.cbTraceAttrs = new CheckBox();
    this.cbShowObjConds = new CheckBox();
    this.cbShowObjects = new CheckBox();
    this.cbShowExpertObj = new CheckBox();
    this.tabCon.BeginInit();
    this.tabCon.SuspendLayout();
    this.xtraTabPage1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.edDocScript.Properties.BeginInit();
    this.xtraTabPage5.SuspendLayout();
    this.panel5.SuspendLayout();
    this.beComplect.Properties.BeginInit();
    this.groupBox3.SuspendLayout();
    this.beComplectContext.Properties.BeginInit();
    this.beComplectScript.Properties.BeginInit();
    this.xtraTabPage2.SuspendLayout();
    this.panel4.SuspendLayout();
    this.be4.Properties.BeginInit();
    this.be3.Properties.BeginInit();
    this.xtraTabPage4.SuspendLayout();
    this.beFormula.Properties.BeginInit();
    this.groupBox2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.be2.Properties.BeginInit();
    this.xtraTabPage6.SuspendLayout();
    this.be6.Properties.BeginInit();
    this.panel6.SuspendLayout();
    this.xtraTabPage3.SuspendLayout();
    this.panel2.SuspendLayout();
    this.seStrInterval.Properties.BeginInit();
    this.seInterval.Properties.BeginInit();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.tabCon.Controls.Add((Control) this.xtraTabPage1);
    this.tabCon.Controls.Add((Control) this.xtraTabPage5);
    this.tabCon.Controls.Add((Control) this.xtraTabPage2);
    this.tabCon.Controls.Add((Control) this.xtraTabPage4);
    this.tabCon.Controls.Add((Control) this.xtraTabPage6);
    this.tabCon.Controls.Add((Control) this.xtraTabPage3);
    componentResourceManager.ApplyResources((object) this.tabCon, "tabCon");
    this.tabCon.LookAndFeel.Style = LookAndFeelStyle.UltraFlat;
    this.tabCon.Name = "tabCon";
    this.tabCon.PaintStyleName = "";
    this.tabCon.SelectedTabPage = this.xtraTabPage4;
    this.tabCon.TabPages.AddRange(new XtraTabPage[6]
    {
      this.xtraTabPage1,
      this.xtraTabPage5,
      this.xtraTabPage2,
      this.xtraTabPage4,
      this.xtraTabPage6,
      this.xtraTabPage3
    });
    this.tabCon.Resize += new EventHandler(this.tabCon_Resize);
    this.xtraTabPage1.Controls.Add((Control) this.XmlView);
    this.xtraTabPage1.Controls.Add((Control) this.panel1);
    this.xtraTabPage1.Name = "xtraTabPage1";
    componentResourceManager.ApplyResources((object) this.xtraTabPage1, "xtraTabPage1");
    componentResourceManager.ApplyResources((object) this.XmlView, "XmlView");
    this.XmlView.Name = "XmlView";
    this.panel1.Controls.Add((Control) this.btnDoc);
    this.panel1.Controls.Add((Control) this.btnStopTask);
    this.panel1.Controls.Add((Control) this.btnTraceInfo);
    this.panel1.Controls.Add((Control) this.btnStartTask);
    this.panel1.Controls.Add((Control) this.btnSelContext);
    this.panel1.Controls.Add((Control) this.lb);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.edDocScript);
    this.panel1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnDoc, "btnDoc");
    this.btnDoc.Name = "btnDoc";
    this.btnDoc.Click += new EventHandler(this.btnDoc_Click);
    componentResourceManager.ApplyResources((object) this.btnStopTask, "btnStopTask");
    this.btnStopTask.Name = "btnStopTask";
    this.btnStopTask.Click += new EventHandler(this.btnStopTask_Click);
    componentResourceManager.ApplyResources((object) this.btnTraceInfo, "btnTraceInfo");
    this.btnTraceInfo.Name = "btnTraceInfo";
    this.btnTraceInfo.Click += new EventHandler(this.btnTraceInfo_Click);
    componentResourceManager.ApplyResources((object) this.btnStartTask, "btnStartTask");
    this.btnStartTask.Name = "btnStartTask";
    this.btnStartTask.Click += new EventHandler(this.btnStartTask_Click);
    componentResourceManager.ApplyResources((object) this.btnSelContext, "btnSelContext");
    this.btnSelContext.Name = "btnSelContext";
    this.btnSelContext.Click += new EventHandler(this.btnSelContext_Click);
    componentResourceManager.ApplyResources((object) this.lb, "lb");
    this.lb.Name = "lb";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.edDocScript, "edDocScript");
    this.edDocScript.Name = "edDocScript";
    this.edDocScript.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edDocScript.Properties.ReadOnly = true;
    this.edDocScript.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.xtraTabPage5, "xtraTabPage5");
    this.xtraTabPage5.Controls.Add((Control) this.panel5);
    this.xtraTabPage5.Name = "xtraTabPage5";
    this.panel5.Controls.Add((Control) this.label14);
    this.panel5.Controls.Add((Control) this.beComplect);
    this.panel5.Controls.Add((Control) this.button1);
    this.panel5.Controls.Add((Control) this.button2);
    this.panel5.Controls.Add((Control) this.groupBox3);
    this.panel5.Controls.Add((Control) this.label13);
    this.panel5.Controls.Add((Control) this.beComplectContext);
    this.panel5.Controls.Add((Control) this.beComplectScript);
    this.panel5.Controls.Add((Control) this.label12);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.label14, "label14");
    this.label14.Name = "label14";
    componentResourceManager.ApplyResources((object) this.beComplect, "beComplect");
    this.beComplect.Name = "beComplect";
    this.beComplect.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beComplect.Properties.Enabled = false;
    this.beComplect.Properties.ReadOnly = true;
    this.beComplect.ButtonClick += new ButtonPressedEventHandler(this.beComplect_ButtonClick);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click_1);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.Click += new EventHandler(this.button2_Click_1);
    this.groupBox3.Controls.Add((Control) this.rbCreateVersion);
    this.groupBox3.Controls.Add((Control) this.rbRefresh);
    this.groupBox3.Controls.Add((Control) this.rbCreate);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbCreateVersion, "rbCreateVersion");
    this.rbCreateVersion.Name = "rbCreateVersion";
    this.rbCreateVersion.UseVisualStyleBackColor = true;
    this.rbCreateVersion.CheckedChanged += new EventHandler(this.rbCreate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbRefresh, "rbRefresh");
    this.rbRefresh.Name = "rbRefresh";
    this.rbRefresh.UseVisualStyleBackColor = true;
    this.rbRefresh.CheckedChanged += new EventHandler(this.rbCreate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbCreate, "rbCreate");
    this.rbCreate.Checked = true;
    this.rbCreate.Name = "rbCreate";
    this.rbCreate.TabStop = true;
    this.rbCreate.UseVisualStyleBackColor = true;
    this.rbCreate.CheckedChanged += new EventHandler(this.rbCreate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label13, "label13");
    this.label13.Name = "label13";
    componentResourceManager.ApplyResources((object) this.beComplectContext, "beComplectContext");
    this.beComplectContext.Name = "beComplectContext";
    this.beComplectContext.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beComplectContext.Properties.ReadOnly = true;
    this.beComplectContext.ButtonClick += new ButtonPressedEventHandler(this.beComplectContext_ButtonClick);
    componentResourceManager.ApplyResources((object) this.beComplectScript, "beComplectScript");
    this.beComplectScript.Name = "beComplectScript";
    this.beComplectScript.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beComplectScript.Properties.ReadOnly = true;
    this.beComplectScript.ButtonClick += new ButtonPressedEventHandler(this.beComplectScript_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label12, "label12");
    this.label12.Name = "label12";
    this.xtraTabPage2.Controls.Add((Control) this.panel4);
    this.xtraTabPage2.Controls.Add((Control) this.button7);
    this.xtraTabPage2.Controls.Add((Control) this.be4);
    this.xtraTabPage2.Controls.Add((Control) this.label8);
    this.xtraTabPage2.Controls.Add((Control) this.be3);
    this.xtraTabPage2.Controls.Add((Control) this.label7);
    this.xtraTabPage2.Name = "xtraTabPage2";
    componentResourceManager.ApplyResources((object) this.xtraTabPage2, "xtraTabPage2");
    this.panel4.BorderStyle = BorderStyle.Fixed3D;
    this.panel4.Controls.Add((Control) this.xView3);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.xView3, "xView3");
    this.xView3.Name = "xView3";
    componentResourceManager.ApplyResources((object) this.button7, "button7");
    this.button7.Name = "button7";
    this.button7.UseVisualStyleBackColor = true;
    this.button7.Click += new EventHandler(this.button7_Click);
    componentResourceManager.ApplyResources((object) this.be4, "be4");
    this.be4.Name = "be4";
    this.be4.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.be4.Properties.ReadOnly = true;
    this.be4.Properties.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit4_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.be3, "be3");
    this.be3.Name = "be3";
    this.be3.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.be3.Properties.ReadOnly = true;
    this.be3.Properties.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit3_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    this.xtraTabPage4.Controls.Add((Control) this.btnStopCalc);
    this.xtraTabPage4.Controls.Add((Control) this.button3);
    this.xtraTabPage4.Controls.Add((Control) this.label11);
    this.xtraTabPage4.Controls.Add((Control) this.beFormula);
    this.xtraTabPage4.Controls.Add((Control) this.groupBox2);
    this.xtraTabPage4.Controls.Add((Control) this.btnStartCalc);
    this.xtraTabPage4.Controls.Add((Control) this.panel3);
    this.xtraTabPage4.Controls.Add((Control) this.label6);
    this.xtraTabPage4.Controls.Add((Control) this.be2);
    this.xtraTabPage4.Controls.Add((Control) this.btnChange);
    this.xtraTabPage4.Name = "xtraTabPage4";
    componentResourceManager.ApplyResources((object) this.xtraTabPage4, "xtraTabPage4");
    componentResourceManager.ApplyResources((object) this.btnStopCalc, "btnStopCalc");
    this.btnStopCalc.Name = "btnStopCalc";
    this.btnStopCalc.Click += new EventHandler(this.btnStopCalc_Click);
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click_1);
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.beFormula, "beFormula");
    this.beFormula.Name = "beFormula";
    this.beFormula.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beFormula.Properties.ReadOnly = true;
    this.beFormula.ButtonClick += new ButtonPressedEventHandler(this.beFormula_ButtonClick);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Controls.Add((Control) this.memoForm);
    this.groupBox2.Controls.Add((Control) this.button6);
    this.groupBox2.Controls.Add((Control) this.label5);
    this.groupBox2.Controls.Add((Control) this.cbType);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.memoForm, "memoForm");
    this.memoForm.Name = "memoForm";
    this.memoForm.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button6, "button6");
    this.button6.Name = "button6";
    this.button6.Click += new EventHandler(this.button6_Click);
    this.label5.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbType.Items.AddRange(new object[5]
    {
      (object) componentResourceManager.GetString("cbType.Items"),
      (object) componentResourceManager.GetString("cbType.Items1"),
      (object) componentResourceManager.GetString("cbType.Items2"),
      (object) componentResourceManager.GetString("cbType.Items3"),
      (object) componentResourceManager.GetString("cbType.Items4")
    });
    componentResourceManager.ApplyResources((object) this.cbType, "cbType");
    this.cbType.Name = "cbType";
    componentResourceManager.ApplyResources((object) this.btnStartCalc, "btnStartCalc");
    this.btnStartCalc.Name = "btnStartCalc";
    this.btnStartCalc.Click += new EventHandler(this.button5_Click);
    this.panel3.BorderStyle = BorderStyle.Fixed3D;
    this.panel3.Controls.Add((Control) this.xView2);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.xView2, "xView2");
    this.xView2.Name = "xView2";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.be2, "be2");
    this.be2.Name = "be2";
    this.be2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.be2.Properties.ReadOnly = true;
    this.be2.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.Name = "btnChange";
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.xtraTabPage6.Controls.Add((Control) this.btnStopInner);
    this.xtraTabPage6.Controls.Add((Control) this.btnInfoInner);
    this.xtraTabPage6.Controls.Add((Control) this.btnStartInner);
    this.xtraTabPage6.Controls.Add((Control) this.be6);
    this.xtraTabPage6.Controls.Add((Control) this.label15);
    this.xtraTabPage6.Controls.Add((Control) this.panel6);
    this.xtraTabPage6.Name = "xtraTabPage6";
    componentResourceManager.ApplyResources((object) this.xtraTabPage6, "xtraTabPage6");
    componentResourceManager.ApplyResources((object) this.btnStopInner, "btnStopInner");
    this.btnStopInner.Name = "btnStopInner";
    this.btnStopInner.Click += new EventHandler(this.btnStopInner_Click);
    componentResourceManager.ApplyResources((object) this.btnInfoInner, "btnInfoInner");
    this.btnInfoInner.Name = "btnInfoInner";
    this.btnInfoInner.Click += new EventHandler(this.btnInfoInner_Click);
    componentResourceManager.ApplyResources((object) this.btnStartInner, "btnStartInner");
    this.btnStartInner.Name = "btnStartInner";
    this.btnStartInner.Click += new EventHandler(this.btnStartInner_Click);
    componentResourceManager.ApplyResources((object) this.be6, "be6");
    this.be6.Name = "be6";
    this.be6.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.be6.Properties.ReadOnly = true;
    this.be6.Properties.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit3_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label15, "label15");
    this.label15.Name = "label15";
    this.panel6.BorderStyle = BorderStyle.Fixed3D;
    this.panel6.Controls.Add((Control) this.xView6);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.xView6, "xView6");
    this.xView6.Name = "xView6";
    this.xtraTabPage3.Controls.Add((Control) this.treeView1);
    this.xtraTabPage3.Controls.Add((Control) this.panel2);
    this.xtraTabPage3.Name = "xtraTabPage3";
    componentResourceManager.ApplyResources((object) this.xtraTabPage3, "xtraTabPage3");
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.Name = "treeView1";
    this.panel2.Controls.Add((Control) this.label10);
    this.panel2.Controls.Add((Control) this.seStrInterval);
    this.panel2.Controls.Add((Control) this.label9);
    this.panel2.Controls.Add((Control) this.label4);
    this.panel2.Controls.Add((Control) this.seInterval);
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this.groupBox1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.seStrInterval, "seStrInterval");
    this.seStrInterval.Name = "seStrInterval";
    this.seStrInterval.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.seStrInterval.Properties.Increment = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this.seStrInterval.Properties.IsFloatValue = false;
    this.seStrInterval.Properties.MaxValue = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.seStrInterval.Properties.MinValue = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    this.seStrInterval.Properties.UseCtrlIncrement = false;
    this.seStrInterval.EditValueChanged += new EventHandler(this.seStrInterval_EditValueChanged);
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.seInterval, "seInterval");
    this.seInterval.Name = "seInterval";
    this.seInterval.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.seInterval.Properties.Increment = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    this.seInterval.Properties.IsFloatValue = false;
    this.seInterval.Properties.MaxValue = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.seInterval.Properties.MinValue = new Decimal(new int[4]
    {
      500,
      0,
      0,
      0
    });
    this.seInterval.Properties.UseCtrlIncrement = false;
    this.seInterval.EditValueChanged += new EventHandler(this.seInterval_EditValueChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.groupBox1.Controls.Add((Control) this.cbShowSettings);
    this.groupBox1.Controls.Add((Control) this.cbTraceObjectSearch);
    this.groupBox1.Controls.Add((Control) this.cbShowFillDocs);
    this.groupBox1.Controls.Add((Control) this.cbShowScriptConds);
    this.groupBox1.Controls.Add((Control) this.cbShowObjResults);
    this.groupBox1.Controls.Add((Control) this.cbShowAttrChange);
    this.groupBox1.Controls.Add((Control) this.cbShowContext);
    this.groupBox1.Controls.Add((Control) this.cbTraceScripts);
    this.groupBox1.Controls.Add((Control) this.cbTraceTables);
    this.groupBox1.Controls.Add((Control) this.cbTraceAttrs);
    this.groupBox1.Controls.Add((Control) this.cbShowObjConds);
    this.groupBox1.Controls.Add((Control) this.cbShowObjects);
    this.groupBox1.Controls.Add((Control) this.cbShowExpertObj);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbShowSettings, "cbShowSettings");
    this.cbShowSettings.Name = "cbShowSettings";
    this.cbShowSettings.Tag = (object) "4096";
    this.cbShowSettings.UseVisualStyleBackColor = true;
    this.cbShowSettings.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbTraceObjectSearch, "cbTraceObjectSearch");
    this.cbTraceObjectSearch.Name = "cbTraceObjectSearch";
    this.cbTraceObjectSearch.Tag = (object) "2048";
    this.cbTraceObjectSearch.UseVisualStyleBackColor = true;
    this.cbTraceObjectSearch.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowFillDocs, "cbShowFillDocs");
    this.cbShowFillDocs.Name = "cbShowFillDocs";
    this.cbShowFillDocs.Tag = (object) "1024";
    this.cbShowFillDocs.UseVisualStyleBackColor = true;
    this.cbShowFillDocs.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowScriptConds, "cbShowScriptConds");
    this.cbShowScriptConds.Name = "cbShowScriptConds";
    this.cbShowScriptConds.Tag = (object) "512";
    this.cbShowScriptConds.UseVisualStyleBackColor = true;
    this.cbShowScriptConds.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowObjResults, "cbShowObjResults");
    this.cbShowObjResults.Name = "cbShowObjResults";
    this.cbShowObjResults.Tag = (object) "256";
    this.cbShowObjResults.UseVisualStyleBackColor = true;
    this.cbShowObjResults.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowAttrChange, "cbShowAttrChange");
    this.cbShowAttrChange.Name = "cbShowAttrChange";
    this.cbShowAttrChange.Tag = (object) "128";
    this.cbShowAttrChange.UseVisualStyleBackColor = true;
    this.cbShowAttrChange.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowContext, "cbShowContext");
    this.cbShowContext.Checked = true;
    this.cbShowContext.CheckState = CheckState.Checked;
    this.cbShowContext.Name = "cbShowContext";
    this.cbShowContext.Tag = (object) "64";
    this.cbShowContext.UseVisualStyleBackColor = true;
    this.cbShowContext.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbTraceScripts, "cbTraceScripts");
    this.cbTraceScripts.Checked = true;
    this.cbTraceScripts.CheckState = CheckState.Checked;
    this.cbTraceScripts.Name = "cbTraceScripts";
    this.cbTraceScripts.Tag = (object) "32";
    this.cbTraceScripts.UseVisualStyleBackColor = true;
    this.cbTraceScripts.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbTraceTables, "cbTraceTables");
    this.cbTraceTables.Checked = true;
    this.cbTraceTables.CheckState = CheckState.Checked;
    this.cbTraceTables.Name = "cbTraceTables";
    this.cbTraceTables.Tag = (object) "16";
    this.cbTraceTables.UseVisualStyleBackColor = true;
    this.cbTraceTables.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbTraceAttrs, "cbTraceAttrs");
    this.cbTraceAttrs.Name = "cbTraceAttrs";
    this.cbTraceAttrs.Tag = (object) "8";
    this.cbTraceAttrs.UseVisualStyleBackColor = true;
    this.cbTraceAttrs.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowObjConds, "cbShowObjConds");
    this.cbShowObjConds.Name = "cbShowObjConds";
    this.cbShowObjConds.Tag = (object) "4";
    this.cbShowObjConds.UseVisualStyleBackColor = true;
    this.cbShowObjConds.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowObjects, "cbShowObjects");
    this.cbShowObjects.Name = "cbShowObjects";
    this.cbShowObjects.Tag = (object) "2";
    this.cbShowObjects.UseVisualStyleBackColor = true;
    this.cbShowObjects.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbShowExpertObj, "cbShowExpertObj");
    this.cbShowExpertObj.Checked = true;
    this.cbShowExpertObj.CheckState = CheckState.Checked;
    this.cbShowExpertObj.Name = "cbShowExpertObj";
    this.cbShowExpertObj.Tag = (object) "1";
    this.cbShowExpertObj.UseVisualStyleBackColor = true;
    this.cbShowExpertObj.CheckedChanged += new EventHandler(this.cbShowExpertObj_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tabCon);
    this.Name = nameof (TestServer);
    this.Shown += new EventHandler(this.TestServer_Shown);
    this.tabCon.EndInit();
    this.tabCon.ResumeLayout(false);
    this.xtraTabPage1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.edDocScript.Properties.EndInit();
    this.xtraTabPage5.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.beComplect.Properties.EndInit();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.beComplectContext.Properties.EndInit();
    this.beComplectScript.Properties.EndInit();
    this.xtraTabPage2.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.be4.Properties.EndInit();
    this.be3.Properties.EndInit();
    this.xtraTabPage4.ResumeLayout(false);
    this.beFormula.Properties.EndInit();
    this.groupBox2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.be2.Properties.EndInit();
    this.xtraTabPage6.ResumeLayout(false);
    this.be6.Properties.EndInit();
    this.panel6.ResumeLayout(false);
    this.xtraTabPage3.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.seStrInterval.Properties.EndInit();
    this.seInterval.Properties.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }

  public void Execute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.iExpServ = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
    this.cbType.SelectedIndex = 0;
    int num = (int) this.ShowDialog();
  }

  private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_411"), LocalizationHolder.rm.GetString("Expert.Editor_412"), ExpertConsts.Consts.objDocScript, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.docScriptId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.docScriptId);
      (sender as ButtonEdit).Text = dbObject.Caption;
    }
  }

  private void btnSelContext_Click(object sender, EventArgs e)
  {
    this.context = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_413"), LocalizationHolder.rm.GetString("Expert.Editor_414"), SelectionOptions.Default);
    this.lb.Items.Clear();
    if (this.context == null || this.context.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.context.Length; ++index)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.context[index]);
        if (sender == this.btnSelContext)
          this.lb.Items.Add((object) dbObject.Caption);
      }
    }
  }

  private void btnStartTask_Click(object sender, EventArgs e)
  {
    if (this.docScriptId == 0L)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_415"), LocalizationHolder.rm.GetString("Expert.Editor_416"));
    }
    else if (this.context == null || this.context.Length == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_417"), LocalizationHolder.rm.GetString("Expert.Editor_418"));
    }
    else
    {
      using (FixEditingContext fixEditingContext = new FixEditingContext())
      {
        this.thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(this.ThreadFunc)));
        this.thread.IsBackground = true;
        this.thread.Start();
        this.btnStartTask.Enabled = false;
        this.btnStopTask.Enabled = true;
      }
    }
  }

  private void SetEnabled() => this.btnDoc.Enabled = true;

  private void ThreadFunc()
  {
    if (this.iExpServ == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.taskId = this.iExpServ.StartTask(sessionKeeper.Session.SessionGUID);
      this.iExpServ.SetDateTimeFormat(this.taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
      this.iExpServ.SetNumberFormat(this.taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
      this.iExpServ.SetDebugMode(this.taskId);
      this.iExpServ.SetTrace(this.taskId, true);
      this.iExpServ.SetLog(this.taskId, true);
      try
      {
        this.iExpServ.SetTraceFlags(this.taskId, this.traceFlags);
        byte[] zippedDoc = (byte[]) null;
        try
        {
          int document = (int) this.iExpServ.GenerateDocument(this.taskId, this.docScriptId, this.context, out zippedDoc);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_419") + ex.Message);
          return;
        }
        if (zippedDoc == null)
          return;
        this.doc = this.LoadFromBuffer(zippedDoc);
        this.btnDoc.Invoke((Delegate) new TestServer.EnableCallback(this.SetEnabled));
      }
      finally
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_420"));
      }
    }
  }

  private XmlDocument GetTraceInfo()
  {
    XmlDocument traceInfo = (XmlDocument) null;
    if (this.iExpServ != null)
    {
      try
      {
        InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) new MemoryStream(this.iExpServ.GetTraceInfo(this.taskId)));
        byte[] buffer = new byte[4096 /*0x1000*/];
        MemoryStream inStream = new MemoryStream();
        while (true)
        {
          int count = inflaterInputStream.Read(buffer, 0, 4096 /*0x1000*/);
          if (count > 0)
            inStream.Write(buffer, 0, count);
          else
            break;
        }
        inStream.Position = 0L;
        traceInfo = new XmlDocument();
        traceInfo.Load((Stream) inStream);
      }
      catch
      {
      }
    }
    return traceInfo;
  }

  private void btnTraceInfo_Click(object sender, EventArgs e)
  {
    XmlDocument traceInfo = this.GetTraceInfo();
    if (traceInfo == null)
      return;
    this.ShowXml(traceInfo, this.XmlView);
    this.XmlView.ExpandAll();
  }

  private void btnStopTask_Click(object sender, EventArgs e)
  {
    if (this.iExpServ == null || this.taskId == 0)
      return;
    this.iExpServ.EndTask(this.taskId);
    this.thread.Abort();
    this.taskId = 0;
    this.btnStartTask.Enabled = true;
    this.btnStopTask.Enabled = false;
    this.btnDoc.Enabled = false;
  }

  private void btnDoc_Click(object sender, EventArgs e)
  {
    if (this.doc == null)
      return;
    ShowDoc showDoc = new ShowDoc();
    XmlDocument traceInfo = this.GetTraceInfo();
    MemoryStream doc = this.doc;
    XmlDocument info = traceInfo;
    showDoc.Execute(doc, info);
  }

  private MemoryStream LoadFromBuffer(byte[] inBuf)
  {
    InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) new MemoryStream(inBuf));
    byte[] buffer = new byte[4096 /*0x1000*/];
    MemoryStream memoryStream = new MemoryStream();
    while (true)
    {
      int count = inflaterInputStream.Read(buffer, 0, 4096 /*0x1000*/);
      if (count > 0)
        memoryStream.Write(buffer, 0, count);
      else
        break;
    }
    memoryStream.Position = 0L;
    return memoryStream;
  }

  private void ShowXml(XmlDocument doc, TreeView xView)
  {
    xView.Nodes.Clear();
    this.AddNodeAndChildren((XmlNode) doc.DocumentElement, (TreeNode) null, xView);
  }

  private void AddNodeAndChildren(XmlNode xnode, TreeNode tnode, TreeView xView)
  {
    string attribs = "";
    if (xnode.Attributes != null)
      attribs = this.CollectAttributes(xnode);
    TreeNode tnode1 = this.AddNode(xnode, tnode, attribs, xView);
    if (!xnode.HasChildNodes)
      return;
    foreach (XmlNode childNode in xnode.ChildNodes)
      this.AddNodeAndChildren(childNode, tnode1, xView);
  }

  private TreeNode AddNode(XmlNode xnode, TreeNode tnode, string attribs, TreeView xView)
  {
    TreeNodeCollection treeNodeCollection1 = tnode == null ? xView.Nodes : tnode.Nodes;
    TreeNode treeNode;
    switch (xnode.NodeType)
    {
      case XmlNodeType.Element:
      case XmlNodeType.Document:
        treeNodeCollection1.Add(treeNode = new TreeNode(xnode.Name + attribs, 0, 0));
        break;
      case XmlNodeType.Text:
        string text1 = xnode.Value;
        if (text1.Length > 128 /*0x80*/)
          text1 = text1.Substring(0, 128 /*0x80*/) + "...";
        treeNodeCollection1.Add(treeNode = new TreeNode(text1, 2, 2));
        break;
      case XmlNodeType.CDATA:
        string str = xnode.Value;
        if (str.Length > 128 /*0x80*/)
          str = str.Substring(0, 128 /*0x80*/) + "...";
        string text2 = $"<![CDATA]{str}]]>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text2, 3, 3));
        break;
      case XmlNodeType.EntityReference:
        string text3 = $"&{xnode.Value};";
        treeNodeCollection1.Add(treeNode = new TreeNode(text3, 7, 7));
        break;
      case XmlNodeType.Entity:
        string text4 = $"<!ENTITY {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text4, 6, 6));
        break;
      case XmlNodeType.ProcessingInstruction:
      case XmlNodeType.XmlDeclaration:
        string text5 = $"<?{xnode.Name + attribs} {xnode.Value}?>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text5, 5, 5));
        break;
      case XmlNodeType.Comment:
        string text6 = $"<!--{xnode.Value}-->";
        treeNodeCollection1.Add(treeNode = new TreeNode(text6, 4, 4));
        break;
      case XmlNodeType.DocumentType:
        string text7 = $"<!DOCTYPE {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text7, 8, 8));
        break;
      case XmlNodeType.Notation:
        string text8 = $"<!NOTATION {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text8, 9, 9));
        break;
      default:
        TreeNodeCollection treeNodeCollection2 = treeNodeCollection1;
        XmlNodeType nodeType = xnode.NodeType;
        TreeNode node;
        treeNode = node = new TreeNode(nodeType.ToString(), 1, 1);
        treeNodeCollection2.Add(node);
        break;
    }
    return treeNode;
  }

  private string CollectAttributes(XmlNode xnode)
  {
    string str = "";
    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xnode.Attributes)
      str += $"    {attribute.Name}={attribute.Value}";
    return str;
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (this.docScriptId == 0L)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_421"), LocalizationHolder.rm.GetString("Expert.Editor_422"));
    }
    else if (this.context == null || this.context.Length == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_423"), LocalizationHolder.rm.GetString("Expert.Editor_424"));
    }
    else
    {
      this.et = new ExpertTask();
      this.et.TraceFlags = ExpertTraceFlags.ShowExpertObjects | ExpertTraceFlags.ShowContext;
      this.et.TimerTraceInfo += new GetTraceInfoEventHandler(this.PerformTraceInfo);
      this.et.EndGenerate += new EndGenerateEventHandler(this.DocGenerated);
      this.et.GenerateDocument(this.docScriptId, this.context);
    }
  }

  public void PerformTraceInfo(object sender, GetTraceInfoEventArgs e)
  {
  }

  public long DocGenerated(object sender, EndGenerateEventArgs e)
  {
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_425"));
    return 0;
  }

  private void button2_Click(object sender, EventArgs e)
  {
    XmlDocument traceInfo = this.et.GetTraceInfo();
    if (traceInfo == null)
    {
      this.treeView1.Nodes.Clear();
    }
    else
    {
      this.et.ShowInfo(traceInfo, this.treeView1);
      this.treeView1.ExpandAll();
    }
  }

  private void button1_Click(object sender, EventArgs e) => this.et.Abort();

  private void btnChange_Click(object sender, EventArgs e)
  {
    if (this.tf == null)
    {
      DataType resType = DataType.Boolean;
      switch (this.cbType.SelectedIndex)
      {
        case 0:
          resType = DataType.Boolean;
          break;
        case 1:
          resType = DataType.Integer;
          break;
        case 2:
          resType = DataType.Float;
          break;
        case 3:
          resType = DataType.String;
          break;
        case 4:
          resType = DataType.Packet;
          break;
      }
      this.tf = new TempFormula(resType, true);
    }
    if (this.formEdit == null)
      this.formEdit = new FormEditor();
    if (!this.formEdit.Execute(ref this.tf, LocalizationHolder.rm.GetString("Expert.Editor_426")))
      return;
    this.ShowFormula(this.tf);
  }

  private void button6_Click(object sender, EventArgs e)
  {
    this.tf = (TempFormula) null;
    this.formulaID = -1L;
    this.beFormula.Text = "";
    this.btnChange_Click(sender, e);
  }

  internal void ShowFormula(TempFormula tf)
  {
    this.memoForm.Text = tf.Text;
    for (int index = 0; index < tf.Count; ++index)
      this.PaintCurToken(tf[index]);
  }

  private void PaintCurToken(Token t)
  {
    if (t.type != Intermech.Expert.TokenType.FuncCall)
      this.memoForm.Select(t.StartPos, t.text.Length);
    switch (t.type)
    {
      case Intermech.Expert.TokenType.UnaryOper:
      case Intermech.Expert.TokenType.BinaryOper:
        this.memoForm.SelectionColor = Color.DarkRed;
        break;
      case Intermech.Expert.TokenType.OpeningBrace:
      case Intermech.Expert.TokenType.ClosingBrace:
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.FuncCall:
        this.memoForm.Select(t.StartPos, t.text.Length - 1);
        this.memoForm.SelectionColor = Color.Black;
        this.memoForm.Select(t.StartPos + t.text.Length - 1, 1);
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.Integer:
        this.memoForm.SelectionColor = Color.Indigo;
        break;
      case Intermech.Expert.TokenType.Float:
        this.memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case Intermech.Expert.TokenType.String:
        this.memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case Intermech.Expert.TokenType.Date:
        this.memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case Intermech.Expert.TokenType.ObjectLink:
        this.memoForm.SelectionColor = Color.Red;
        break;
      default:
        this.memoForm.SelectionColor = Color.Black;
        break;
    }
  }

  private void buttonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_427"), LocalizationHolder.rm.GetString("Expert.Editor_428"), SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.formObjId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.formObjId);
      if (dbObject.Caption != "")
        this.be2.Text = dbObject.Caption;
      else
        this.be2.Text = Convert.ToString(this.formObjId);
    }
  }

  private void button5_Click(object sender, EventArgs e)
  {
    if (this.formulaID == -1L && this.tf == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_429"));
    }
    else
    {
      using (FixEditingContext fixEditingContext = new FixEditingContext())
      {
        this.thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(this.CalcThreadFunc)));
        this.thread.IsBackground = true;
        this.thread.Start();
        this.btnStartCalc.Enabled = false;
        this.btnStopCalc.Enabled = true;
      }
    }
  }

  private void CalcThreadFunc()
  {
    if (this.iExpServ == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.taskId = this.iExpServ.StartTask(sessionKeeper.Session.SessionGUID, ExpertTraceFlags.None);
      this.iExpServ.SetDateTimeFormat(this.taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
      this.iExpServ.SetNumberFormat(this.taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
      this.iExpServ.SetDebugMode(this.taskId);
      this.iExpServ.SetTrace(this.taskId, true);
      this.iExpServ.SetLog(this.taskId, true);
      this.iExpServ.SetTraceFlags(this.taskId, this.traceFlags);
      object obj = (object) null;
      if (this.formulaID != -1L)
      {
        int num1 = (int) this.iExpServ.CalcFormula(this.taskId, this.formulaID, this.formObjId, out obj);
      }
      else
      {
        int num2 = (int) this.iExpServ.CalcFormula(this.taskId, (object) this.tf, this.formObjId, out obj);
      }
      InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) new MemoryStream(this.iExpServ.GetTraceInfo(this.taskId)));
      byte[] buffer = new byte[4096 /*0x1000*/];
      using (MemoryStream memoryStream = new MemoryStream())
      {
        while (true)
        {
          int count = inflaterInputStream.Read(buffer, 0, 4096 /*0x1000*/);
          if (count > 0)
            memoryStream.Write(buffer, 0, count);
          else
            break;
        }
      }
      this.button3.Invoke((Delegate) new TestServer.EnableCallback(this.ShowFormInfo));
    }
  }

  private void ShowFormInfo() => this.button3_Click_1((object) this.button3, (EventArgs) null);

  private void buttonEdit3_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_430"), LocalizationHolder.rm.GetString("Expert.Editor_431"), SelectionOptions.Default);
    if (numArray.Length == 0)
      return;
    this.formObjId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.formObjId);
      if (dbObject.Caption != "")
        this.be6.Text = dbObject.Caption;
      else
        this.be6.Text = Convert.ToString(this.formObjId);
    }
  }

  private void buttonEdit4_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    AdvSelectorForm advSelectorForm;
    if (this.selAttrId == -1)
      advSelectorForm = new AdvSelectorForm(AdvSelector.AttributeType, AttributableElements.Object);
    else
      advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, -1, new int[1]
      {
        this.selAttrId
      });
    if (advSelectorForm.ShowDialog() != DialogResult.OK)
      return;
    this.selAttrId = advSelectorForm.AttributeTypes[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.selAttrId);
      if (attributeType.Name != "")
        this.be4.Text = attributeType.Name;
      else
        this.be4.Text = Convert.ToString(this.selAttrId);
    }
  }

  private void button7_Click(object sender, EventArgs e)
  {
    if (this.selAttrId != -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int taskId = this.iExpServ.StartTask(sessionKeeper.Session.SessionGUID, ExpertTraceFlags.None);
        this.iExpServ.SetDateTimeFormat(taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
        this.iExpServ.SetNumberFormat(taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
        this.iExpServ.SetDebugMode(taskId);
        this.iExpServ.SetTrace(taskId, true);
        this.iExpServ.SetLog(taskId, true);
        try
        {
          this.iExpServ.RecalcForAttr(taskId, this.formObjId, this.selAttrId);
          InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream) new MemoryStream(this.iExpServ.GetTraceInfo(taskId)));
          byte[] buffer = new byte[4096 /*0x1000*/];
          MemoryStream inStream = new MemoryStream();
          while (true)
          {
            int count = inflaterInputStream.Read(buffer, 0, 4096 /*0x1000*/);
            if (count > 0)
              inStream.Write(buffer, 0, count);
            else
              break;
          }
          inStream.Position = 0L;
          XmlDocument doc = new XmlDocument();
          doc.Load((Stream) inStream);
          this.ShowXml(doc, this.xView3);
          this.XmlView.ExpandAll();
        }
        finally
        {
          this.iExpServ.EndTask(taskId);
        }
      }
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_432"));
    }
  }

  private void TestServer_Shown(object sender, EventArgs e) => this.tabCon_Resize(sender, e);

  private void tabCon_Resize(object sender, EventArgs e)
  {
    ButtonEdit edDocScript = this.edDocScript;
    Point location1 = this.edDocScript.Location;
    int x1 = location1.X;
    location1 = this.edDocScript.Location;
    int y1 = location1.Y;
    Size size1 = this.tabCon.Size;
    int width1 = size1.Width - 20;
    size1 = this.edDocScript.Size;
    int height1 = size1.Height;
    edDocScript.SetBounds(x1, y1, width1, height1);
    ListBox lb = this.lb;
    Point location2 = this.lb.Location;
    int x2 = location2.X;
    location2 = this.lb.Location;
    int y2 = location2.Y;
    Size size2 = this.tabCon.Size;
    int width2 = size2.Width - 150;
    size2 = this.lb.Size;
    int height2 = size2.Height;
    lb.SetBounds(x2, y2, width2, height2);
    ButtonEdit be3 = this.be3;
    Point location3 = this.be3.Location;
    int x3 = location3.X;
    location3 = this.be3.Location;
    int y3 = location3.Y;
    Size size3 = this.tabCon.Size;
    int width3 = size3.Width - 20;
    size3 = this.be3.Size;
    int height3 = size3.Height;
    be3.SetBounds(x3, y3, width3, height3);
    ButtonEdit be4 = this.be4;
    Point location4 = this.be4.Location;
    int x4 = location4.X;
    location4 = this.be4.Location;
    int y4 = location4.Y;
    Size size4 = this.tabCon.Size;
    int width4 = size4.Width - 20;
    size4 = this.be4.Size;
    int height4 = size4.Height;
    be4.SetBounds(x4, y4, width4, height4);
    RichTextBox memoForm = this.memoForm;
    Point location5 = this.memoForm.Location;
    int x5 = location5.X;
    location5 = this.memoForm.Location;
    int y5 = location5.Y;
    Size size5 = this.tabCon.Size;
    int width5 = size5.Width - 20;
    size5 = this.memoForm.Size;
    int height5 = size5.Height;
    memoForm.SetBounds(x5, y5, width5, height5);
    ButtonEdit be2 = this.be2;
    Point location6 = this.be2.Location;
    int x6 = location6.X;
    location6 = this.be2.Location;
    int y6 = location6.Y;
    Size size6 = this.tabCon.Size;
    int width6 = size6.Width - 20;
    size6 = this.be2.Size;
    int height6 = size6.Height;
    be2.SetBounds(x6, y6, width6, height6);
    ButtonEdit beComplectScript = this.beComplectScript;
    Point location7 = this.beComplectScript.Location;
    int x7 = location7.X;
    location7 = this.beComplectScript.Location;
    int y7 = location7.Y;
    Size size7 = this.tabCon.Size;
    int width7 = size7.Width - 20;
    size7 = this.beComplectScript.Size;
    int height7 = size7.Height;
    beComplectScript.SetBounds(x7, y7, width7, height7);
    ButtonEdit beComplect = this.beComplect;
    Point location8 = this.beComplect.Location;
    int x8 = location8.X;
    location8 = this.beComplect.Location;
    int y8 = location8.Y;
    Size size8 = this.tabCon.Size;
    int width8 = size8.Width - 20;
    size8 = this.beComplect.Size;
    int height8 = size8.Height;
    beComplect.SetBounds(x8, y8, width8, height8);
    ButtonEdit beComplectContext = this.beComplectContext;
    Point location9 = this.beComplectContext.Location;
    int x9 = location9.X;
    location9 = this.beComplectContext.Location;
    int y9 = location9.Y;
    Size size9 = this.tabCon.Size;
    int width9 = size9.Width - 20;
    size9 = this.beComplectContext.Size;
    int height9 = size9.Height;
    beComplectContext.SetBounds(x9, y9, width9, height9);
    ButtonEdit beFormula = this.beFormula;
    Point location10 = this.beFormula.Location;
    int x10 = location10.X;
    location10 = this.beFormula.Location;
    int y10 = location10.Y;
    Size size10 = this.tabCon.Size;
    int width10 = size10.Width - 20;
    size10 = this.beFormula.Size;
    int height10 = size10.Height;
    beFormula.SetBounds(x10, y10, width10, height10);
    ButtonEdit be6 = this.be6;
    Point location11 = this.be6.Location;
    int x11 = location11.X;
    location11 = this.be6.Location;
    int y11 = location11.Y;
    Size size11 = this.tabCon.Size;
    int width11 = size11.Width - 20;
    size11 = this.be6.Size;
    int height11 = size11.Height;
    be6.SetBounds(x11, y11, width11, height11);
  }

  private void cbShowExpertObj_CheckedChanged(object sender, EventArgs e)
  {
    this.traceFlags ^= (ExpertTraceFlags) Convert.ToInt64((sender as CheckBox).Tag);
  }

  private void seInterval_EditValueChanged(object sender, EventArgs e)
  {
    this.infoInterval = (int) Convert.ToInt64((sender as SpinEdit).Value);
  }

  private void seStrInterval_EditValueChanged(object sender, EventArgs e)
  {
    this.lastStrInterval = (int) Convert.ToInt64((sender as SpinEdit).Value);
  }

  private void beFormula_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects("Выбор формулы", "Выберите формулу для расчета", ExpertConsts.Consts.objSimpleFormula, SelectionOptions.SelectObjects);
    if (numArray == null || numArray.Length == 0)
      return;
    this.formulaID = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.formulaID);
      this.esObjectType = dbObject.ObjectType;
      if (dbObject.Caption != "")
        this.beFormula.Text = dbObject.Caption;
      else
        this.beFormula.Text = Convert.ToString(this.formulaID);
    }
  }

  private void beComplectScript_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects("Скрипт генерации комплекта документов", "Выберите скрипт генерации", ExpertConsts.Consts.objComplectTemplate, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.docScriptId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.docScriptId);
      (sender as ButtonEdit).Text = dbObject.Caption;
    }
  }

  private void beComplectContext_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    this.context = SelectionWindow.SelectObjects("Выбор контекста", "Выберите объект, для которого должен создаваться комплект документов", SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (this.context == null || this.context.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.beComplectContext.Text = sessionKeeper.Session.GetObject(this.context[0]).Caption;
  }

  private void beComplect_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects("Выбор комплекта документов", "Выберите комплект документов для обновления либо создания версии", ExpertConsts.Consts.objDocTPComplect, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    this.complectID = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.beComplect.Text = sessionKeeper.Session.GetObject(this.complectID).Caption;
  }

  private void button2_Click_1(object sender, EventArgs e)
  {
    if (this.docScriptId == 0L)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_415"), LocalizationHolder.rm.GetString("Expert.Editor_416"));
    }
    else if (this.context == null || this.context.Length == 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_417"), LocalizationHolder.rm.GetString("Expert.Editor_418"));
    }
    else if (!this.rbCreate.Checked && this.complectID == 0L)
    {
      int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_603"), LocalizationHolder.rm.GetString("Expert.Editor_604"));
    }
    else
    {
      using (FixEditingContext fixEditingContext = new FixEditingContext())
      {
        this.thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(this.CompThreadFunc)));
        this.thread.IsBackground = true;
        this.thread.Start();
        this.button2.Enabled = false;
        this.button1.Enabled = true;
      }
    }
  }

  private void CompThreadFunc()
  {
    if (this.iExpServ == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.taskId = this.iExpServ.StartTask(sessionKeeper.Session.SessionGUID);
      this.iExpServ.SetDateTimeFormat(this.taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
      this.iExpServ.SetNumberFormat(this.taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
      this.iExpServ.SetDebugMode(this.taskId);
      this.iExpServ.SetTrace(this.taskId, true);
      this.iExpServ.SetLog(this.taskId, true);
      try
      {
        this.iExpServ.SetTraceFlags(this.taskId, this.traceFlags);
        try
        {
          List<ChangeInfo> changed = (List<ChangeInfo>) null;
          if (this.rbCreate.Checked)
          {
            int complect = (int) this.iExpServ.GenerateComplect(this.taskId, this.docScriptId, this.context[0], out changed);
          }
          if (this.rbCreateVersion.Checked)
          {
            int complectVersion = (int) this.iExpServ.CreateComplectVersion(this.taskId, this.docScriptId, this.context[0], this.complectID, out changed);
          }
          if (!this.rbRefresh.Checked)
            return;
          int num = (int) this.iExpServ.RefreshComplect(this.taskId, this.docScriptId, this.context[0], this.complectID, out changed);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_419") + ex.Message);
        }
      }
      finally
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_420"));
      }
    }
  }

  private void button1_Click_1(object sender, EventArgs e)
  {
    if (this.iExpServ == null || this.taskId == 0)
      return;
    this.iExpServ.EndTask(this.taskId);
    this.thread.Abort();
    this.taskId = 0;
    this.button2.Enabled = true;
    this.button1.Enabled = false;
  }

  private void rbCreate_CheckedChanged(object sender, EventArgs e)
  {
    this.beComplect.Enabled = !this.rbCreate.Checked;
  }

  private void btnStopCalc_Click(object sender, EventArgs e)
  {
    if (this.iExpServ == null || this.taskId == 0)
      return;
    this.iExpServ.EndTask(this.taskId);
    this.thread.Abort();
    this.taskId = 0;
    this.btnStartCalc.Enabled = true;
    this.btnStopCalc.Enabled = false;
  }

  private void button3_Click_1(object sender, EventArgs e)
  {
    XmlDocument traceInfo = this.GetTraceInfo();
    if (traceInfo == null)
      return;
    this.ShowXml(traceInfo, this.xView2);
    this.xView2.ExpandAll();
  }

  private void btnStartInner_Click(object sender, EventArgs e)
  {
  }

  private void InnerThreadFunc()
  {
    if (this.iExpServ == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.taskId = this.iExpServ.StartTask(sessionKeeper.Session.SessionGUID, ExpertTraceFlags.None);
      this.iExpServ.SetDateTimeFormat(this.taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
      this.iExpServ.SetNumberFormat(this.taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
      this.iExpServ.SetDebugMode(this.taskId);
      this.iExpServ.SetTraceFlags(this.taskId, this.traceFlags);
      this.iExpServ.SetTrace(this.taskId, true);
      this.iExpServ.SetLog(this.taskId, true);
      this.btnInfoInner.Invoke((Delegate) new TestServer.EnableCallback(this.ShowInnerInfo));
    }
  }

  private void ShowInnerInfo()
  {
    this.btnInfoInner_Click((object) this.btnInfoInner, (EventArgs) null);
  }

  private void btnInfoInner_Click(object sender, EventArgs e)
  {
    XmlDocument traceInfo = this.GetTraceInfo();
    if (traceInfo == null)
      return;
    this.ShowXml(traceInfo, this.xView6);
    this.xView6.ExpandAll();
  }

  private void btnStopInner_Click(object sender, EventArgs e)
  {
    if (this.iExpServ == null || this.taskId == 0)
      return;
    this.iExpServ.EndTask(this.taskId);
    this.thread.Abort();
    this.taskId = 0;
    this.btnStartInner.Enabled = true;
    this.btnStopInner.Enabled = false;
  }

  private delegate void EnableCallback();
}
