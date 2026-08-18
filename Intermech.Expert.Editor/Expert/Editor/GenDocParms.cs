// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.GenDocParms
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.AVS;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class GenDocParms : Form
{
  public Guid objTypeGUID = Guid.Empty;
  public int objTypeId = -1;
  public bool ShowDebugInfo;
  public bool AllNodeObjects;
  public UseZamens UseAllZamens;
  public bool ThisObjectDoc;
  public bool Classify;
  public string objTypeName = "";
  public string docName = "";
  public long scriptId = -1;
  public OpParmSetting ops;
  public List<int> allowedTypes;
  public List<int> objTypes;
  public List<int> relTypes;
  public List<string> attrGUIDs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private ButtonEdit beDocType;
  private Label label2;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private CheckBox cbTraceInfo;
  private TextBox edDocName;
  private Button btnSortSettings;
  private Button btnAddAttr;
  private Button btnAddObjType;
  private ImageList IL;
  private ListBox lbTypes;
  private GroupBox groupBox1;
  private CheckBox cbAllNode;
  private Button btnData;
  private CheckBox cbThisObjectDoc;
  private RadioButton rbActual;
  private RadioButton rbAll;
  private RadioButton rbUseClient;
  private CheckBox cbClassify;
  private Button btnTime;
  private Panel topPanel;

  public GenDocParms() => this.InitializeComponent();

  public GenDocParms(
    Guid otGUID,
    bool showInfo,
    bool allNode,
    UseZamens allZamens,
    string otName,
    string docName,
    bool thisObjectDoc,
    long scrId,
    bool needClassify,
    OpParmSetting o,
    bool commandScript)
  {
    this.objTypeGUID = otGUID;
    this.ShowDebugInfo = showInfo;
    this.AllNodeObjects = allNode;
    this.objTypeName = otName;
    this.docName = docName;
    this.scriptId = scrId;
    this.ThisObjectDoc = thisObjectDoc;
    this.Classify = needClassify;
    this.ops = o;
    this.UseAllZamens = allZamens;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1333);
    this.cbTraceInfo.Checked = this.ShowDebugInfo;
    this.cbAllNode.Checked = this.AllNodeObjects;
    switch (this.UseAllZamens)
    {
      case UseZamens.AsClient:
        this.rbUseClient.Checked = true;
        break;
      case UseZamens.MainVariant:
        this.rbActual.Checked = true;
        break;
      case UseZamens.AllVariants:
        this.rbAll.Checked = true;
        break;
    }
    this.cbThisObjectDoc.Checked = this.ThisObjectDoc;
    this.cbClassify.Checked = needClassify;
    this.beDocType.Text = otName;
    this.edDocName.Text = docName;
    if (!commandScript)
      return;
    this.topPanel.Enabled = false;
    this.cbThisObjectDoc.Enabled = false;
    this.cbClassify.Enabled = false;
  }

  private void beDocType_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_338"), typeof (ObjectTypeFolder), false);
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    if (this.objTypeGUID != Guid.Empty)
    {
      idList.Add((object) this.objTypeId);
      typeList.Add((object) typeof (ObjectTypeFolder));
    }
    selectorForm.InitSelectionAsType(idList, typeList);
    int[] allowableTypes = new int[1]
    {
      ExpertConsts.Consts.objDocRoot
    };
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(allowableTypes, true, true);
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.objTypeId = (int) selectorForm.IDList[0];
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.objTypeId);
      this.beDocType.Text = objectType.ObjectTypeName;
      this.objTypeGUID = objectType.PropertiesStructure.ObjectTypeGuid;
      this.objTypeName = objectType.ObjectTypeName;
    }
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.ShowDebugInfo = this.cbTraceInfo.Checked;
    this.AllNodeObjects = this.cbAllNode.Checked;
    if (this.rbUseClient.Checked)
      this.UseAllZamens = UseZamens.AsClient;
    if (this.rbActual.Checked)
      this.UseAllZamens = UseZamens.MainVariant;
    if (this.rbAll.Checked)
      this.UseAllZamens = UseZamens.AllVariants;
    this.docName = this.edDocName.Text;
    this.Classify = this.cbClassify.Checked;
  }

  private void btnSortSettings_Click(object sender, EventArgs e)
  {
    List<Triple> tripleList = new List<Triple>();
    tripleList.Add(new Triple("0", "0", "По умолчанию"));
    if (this.ops != null && this.ops.listTable != null)
      tripleList.AddRange((IEnumerable<Triple>) this.ops.listTable);
    int num = (int) new FormSetupSorting(this.scriptId, VedomostiSettingsStructure.Instance, tripleList, this.objTypes, this.relTypes).ShowDialog();
  }

  private void btnAddAttr_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK)
      return;
    int anAttributeType = attributesSelectDlg.SelectedAttributesID[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.edDocName.SelectedText = $"{{{sessionKeeper.Session.GetAttributeType(anAttributeType).Name}}}";
  }

  private void btnAddObjType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_338"), typeof (ObjectTypeFolder), true);
    ArrayList idList = new ArrayList();
    ArrayList typeList = new ArrayList();
    for (int index = 0; index < this.allowedTypes.Count; ++index)
    {
      idList.Add((object) this.allowedTypes[index]);
      typeList.Add((object) typeof (ObjectTypeFolder));
    }
    selectorForm.InitSelectionAsType(idList, typeList);
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    this.allowedTypes.Clear();
    for (int index = 0; index < selectorForm.IDList.Count; ++index)
      this.allowedTypes.Add((int) selectorForm.IDList[index]);
    this.ShowAllowedTypes();
  }

  public void ShowAllowedTypes()
  {
    this.lbTypes.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.allowedTypes.Count; ++index)
        this.lbTypes.Items.Add((object) sessionKeeper.Session.GetObjectType(this.allowedTypes[index]).ObjectTypeName);
    }
  }

  private void GenDocParms_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void GenDocParms_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void btnData_Click(object sender, EventArgs e) => this.edDocName.SelectedText = "{DATE}";

  private void cbThisObjectDoc_CheckedChanged(object sender, EventArgs e)
  {
    this.ThisObjectDoc = this.cbThisObjectDoc.Checked;
  }

  private void btnTime_Click(object sender, EventArgs e) => this.edDocName.SelectedText = "{TIME}";

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GenDocParms));
    this.label1 = new Label();
    this.beDocType = new ButtonEdit();
    this.label2 = new Label();
    this.panel1 = new Panel();
    this.btnSortSettings = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.cbTraceInfo = new CheckBox();
    this.edDocName = new TextBox();
    this.btnAddAttr = new Button();
    this.btnAddObjType = new Button();
    this.IL = new ImageList(this.components);
    this.lbTypes = new ListBox();
    this.groupBox1 = new GroupBox();
    this.cbAllNode = new CheckBox();
    this.btnData = new Button();
    this.cbThisObjectDoc = new CheckBox();
    this.rbActual = new RadioButton();
    this.rbAll = new RadioButton();
    this.rbUseClient = new RadioButton();
    this.cbClassify = new CheckBox();
    this.btnTime = new Button();
    this.topPanel = new Panel();
    this.beDocType.Properties.BeginInit();
    this.panel1.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.topPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.beDocType, "beDocType");
    this.beDocType.Name = "beDocType";
    this.beDocType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beDocType.ButtonClick += new ButtonPressedEventHandler(this.beDocType_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.panel1.Controls.Add((Control) this.btnSortSettings);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnSortSettings, "btnSortSettings");
    this.btnSortSettings.Name = "btnSortSettings";
    this.btnSortSettings.UseVisualStyleBackColor = true;
    this.btnSortSettings.Click += new EventHandler(this.btnSortSettings_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.cbTraceInfo, "cbTraceInfo");
    this.cbTraceInfo.Name = "cbTraceInfo";
    this.cbTraceInfo.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.edDocName, "edDocName");
    this.edDocName.Name = "edDocName";
    componentResourceManager.ApplyResources((object) this.btnAddAttr, "btnAddAttr");
    this.btnAddAttr.Name = "btnAddAttr";
    this.btnAddAttr.UseVisualStyleBackColor = true;
    this.btnAddAttr.Click += new EventHandler(this.btnAddAttr_Click);
    componentResourceManager.ApplyResources((object) this.btnAddObjType, "btnAddObjType");
    this.btnAddObjType.ImageList = this.IL;
    this.btnAddObjType.Name = "btnAddObjType";
    this.btnAddObjType.UseVisualStyleBackColor = false;
    this.btnAddObjType.Click += new EventHandler(this.btnAddObjType_Click);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "VVV2.bmp");
    this.IL.Images.SetKeyName(1, "VVV1.bmp");
    this.IL.Images.SetKeyName(2, "VVV3.bmp");
    componentResourceManager.ApplyResources((object) this.lbTypes, "lbTypes");
    this.lbTypes.FormattingEnabled = true;
    this.lbTypes.Name = "lbTypes";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.lbTypes);
    this.groupBox1.Controls.Add((Control) this.btnAddObjType);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbAllNode, "cbAllNode");
    this.cbAllNode.Name = "cbAllNode";
    this.cbAllNode.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnData, "btnData");
    this.btnData.Name = "btnData";
    this.btnData.UseVisualStyleBackColor = true;
    this.btnData.Click += new EventHandler(this.btnData_Click);
    componentResourceManager.ApplyResources((object) this.cbThisObjectDoc, "cbThisObjectDoc");
    this.cbThisObjectDoc.Name = "cbThisObjectDoc";
    this.cbThisObjectDoc.UseVisualStyleBackColor = true;
    this.cbThisObjectDoc.CheckedChanged += new EventHandler(this.cbThisObjectDoc_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbActual, "rbActual");
    this.rbActual.Name = "rbActual";
    this.rbActual.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbAll, "rbAll");
    this.rbAll.Name = "rbAll";
    this.rbAll.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbUseClient, "rbUseClient");
    this.rbUseClient.Checked = true;
    this.rbUseClient.Name = "rbUseClient";
    this.rbUseClient.TabStop = true;
    this.rbUseClient.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbClassify, "cbClassify");
    this.cbClassify.Name = "cbClassify";
    this.cbClassify.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnTime, "btnTime");
    this.btnTime.Name = "btnTime";
    this.btnTime.UseVisualStyleBackColor = true;
    this.btnTime.Click += new EventHandler(this.btnTime_Click);
    this.topPanel.Controls.Add((Control) this.beDocType);
    this.topPanel.Controls.Add((Control) this.btnTime);
    this.topPanel.Controls.Add((Control) this.label1);
    this.topPanel.Controls.Add((Control) this.label2);
    this.topPanel.Controls.Add((Control) this.edDocName);
    this.topPanel.Controls.Add((Control) this.btnAddAttr);
    this.topPanel.Controls.Add((Control) this.btnData);
    componentResourceManager.ApplyResources((object) this.topPanel, "topPanel");
    this.topPanel.Name = "topPanel";
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.topPanel);
    this.Controls.Add((Control) this.cbClassify);
    this.Controls.Add((Control) this.rbUseClient);
    this.Controls.Add((Control) this.rbAll);
    this.Controls.Add((Control) this.rbActual);
    this.Controls.Add((Control) this.cbThisObjectDoc);
    this.Controls.Add((Control) this.cbAllNode);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.cbTraceInfo);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (GenDocParms);
    this.FormClosed += new FormClosedEventHandler(this.GenDocParms_FormClosed);
    this.Load += new EventHandler(this.GenDocParms_Load);
    this.beDocType.Properties.EndInit();
    this.panel1.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.topPanel.ResumeLayout(false);
    this.topPanel.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
