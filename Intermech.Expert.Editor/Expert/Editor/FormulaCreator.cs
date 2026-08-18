// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.FormulaCreator
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator.SelectionView;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Object Creator for expert system formulae and conditions
/// </summary>
public class FormulaCreator : Form
{
  private GroupBox groupAttParms;
  private ImageList IL;
  private IContainer components;
  private FormEditor formEd;
  private TempFormula formTF;
  private TempFormula condTF;
  private string title = "";
  private bool attrChanged;
  private bool formChanged;
  private bool condChanged;
  internal ExpertFormulaType efType;
  private Panel panel2;
  private Button btnCancel;
  private Button btnCreate;
  private GroupBox gbFormula;
  private Label label1;
  private ComboBox cbResType;
  private Button button3;
  private Button button1;
  private RichTextBox richFormula;
  private Panel panelCond;
  private Button btnDeleteCond;
  private Button btnChangeCond;
  private RichTextBox richCond;
  private Label label2;
  private Panel panel1;
  private TextBox editName;
  private Label label3;
  private SelObjAttrControl selObjAttr1;

  public FormulaCreator()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1315);
    this.formEd = new FormEditor();
    this.formTF = new TempFormula(true);
    this.formTF.Cond = false;
    this.condTF = new TempFormula(true);
    this.condTF.resType = DataType.Boolean;
    this.condTF.Cond = true;
  }

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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormulaCreator));
    this.groupAttParms = new GroupBox();
    this.selObjAttr1 = new SelObjAttrControl();
    this.IL = new ImageList(this.components);
    this.panel2 = new Panel();
    this.btnCancel = new Button();
    this.btnCreate = new Button();
    this.gbFormula = new GroupBox();
    this.label1 = new Label();
    this.cbResType = new ComboBox();
    this.button3 = new Button();
    this.button1 = new Button();
    this.richFormula = new RichTextBox();
    this.panelCond = new Panel();
    this.btnDeleteCond = new Button();
    this.btnChangeCond = new Button();
    this.richCond = new RichTextBox();
    this.label2 = new Label();
    this.panel1 = new Panel();
    this.editName = new TextBox();
    this.label3 = new Label();
    this.groupAttParms.SuspendLayout();
    this.panel2.SuspendLayout();
    this.gbFormula.SuspendLayout();
    this.panelCond.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.groupAttParms.Controls.Add((Control) this.selObjAttr1);
    componentResourceManager.ApplyResources((object) this.groupAttParms, "groupAttParms");
    this.groupAttParms.Name = "groupAttParms";
    this.groupAttParms.TabStop = false;
    this.selObjAttr1.attrText = "";
    componentResourceManager.ApplyResources((object) this.selObjAttr1, "selObjAttr1");
    this.selObjAttr1.Name = "selObjAttr1";
    this.selObjAttr1.objTypeText = "";
    this.selObjAttr1.ShowButtons = false;
    this.selObjAttr1.Changed += new EventHandler(this.selObjAttr1_Changed);
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
    this.panel2.Controls.Add((Control) this.btnCancel);
    this.panel2.Controls.Add((Control) this.btnCreate);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnCreate, "btnCreate");
    this.btnCreate.DialogResult = DialogResult.OK;
    this.btnCreate.Name = "btnCreate";
    this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
    this.gbFormula.Controls.Add((Control) this.label1);
    this.gbFormula.Controls.Add((Control) this.cbResType);
    this.gbFormula.Controls.Add((Control) this.button3);
    this.gbFormula.Controls.Add((Control) this.button1);
    this.gbFormula.Controls.Add((Control) this.richFormula);
    componentResourceManager.ApplyResources((object) this.gbFormula, "gbFormula");
    this.gbFormula.Name = "gbFormula";
    this.gbFormula.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.cbResType, "cbResType");
    this.cbResType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbResType.Items.AddRange(new object[7]
    {
      (object) componentResourceManager.GetString("cbResType.Items"),
      (object) componentResourceManager.GetString("cbResType.Items1"),
      (object) componentResourceManager.GetString("cbResType.Items2"),
      (object) componentResourceManager.GetString("cbResType.Items3"),
      (object) componentResourceManager.GetString("cbResType.Items4"),
      (object) componentResourceManager.GetString("cbResType.Items5"),
      (object) componentResourceManager.GetString("cbResType.Items6")
    });
    this.cbResType.Name = "cbResType";
    this.cbResType.SelectedIndexChanged += new EventHandler(this.cbResType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.itemDeleteForm_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.itemChangeForm_Click);
    componentResourceManager.ApplyResources((object) this.richFormula, "richFormula");
    this.richFormula.BackColor = SystemColors.Window;
    this.richFormula.Name = "richFormula";
    this.richFormula.ReadOnly = true;
    this.panelCond.Controls.Add((Control) this.btnDeleteCond);
    this.panelCond.Controls.Add((Control) this.btnChangeCond);
    this.panelCond.Controls.Add((Control) this.richCond);
    this.panelCond.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panelCond, "panelCond");
    this.panelCond.Name = "panelCond";
    componentResourceManager.ApplyResources((object) this.btnDeleteCond, "btnDeleteCond");
    this.btnDeleteCond.Name = "btnDeleteCond";
    this.btnDeleteCond.Click += new EventHandler(this.itemDeleteCond_Click);
    componentResourceManager.ApplyResources((object) this.btnChangeCond, "btnChangeCond");
    this.btnChangeCond.Name = "btnChangeCond";
    this.btnChangeCond.Click += new EventHandler(this.itemChangeCond_Click);
    componentResourceManager.ApplyResources((object) this.richCond, "richCond");
    this.richCond.BackColor = SystemColors.Window;
    this.richCond.Name = "richCond";
    this.richCond.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.panel1.Controls.Add((Control) this.editName);
    this.panel1.Controls.Add((Control) this.label3);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.editName, "editName");
    this.editName.Name = "editName";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.gbFormula);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.groupAttParms);
    this.Controls.Add((Control) this.panelCond);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (FormulaCreator);
    this.Tag = (object) " ";
    this.FormClosed += new FormClosedEventHandler(this.FormulaCreator_FormClosed);
    this.Load += new EventHandler(this.FormulaCreator_Load);
    this.groupAttParms.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.gbFormula.ResumeLayout(false);
    this.panelCond.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }

  private void FormulaCreator_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void FormulaCreator_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Show dialog window to edit new formula/cond</summary>
  /// <param name="efType">Type of formula</param>
  /// <returns>true if user pressed OK</returns>
  public bool Execute(ExpertFormulaType efType)
  {
    this.selObjAttr1.LoadSessionData();
    this.efType = efType;
    this.SetupControls(efType);
    return this.ShowDialog() == DialogResult.OK;
  }

  internal void EnableButtons()
  {
    this.button3.Enabled = this.formTF.Count > 0;
    this.btnCreate.Enabled = this.efType == ExpertFormulaType.ESFolder || this.formTF.Count > 0;
    this.btnDeleteCond.Enabled = this.condTF.Count > 0;
  }

  internal void SetupControls(ExpertFormulaType efType)
  {
    bool flag = efType == ExpertFormulaType.Cond || efType == ExpertFormulaType.ESFolder;
    if (efType != ExpertFormulaType.CommonFormula)
    {
      this.groupAttParms.Visible = false;
      this.panelCond.Visible = false;
      this.Height = this.Size.Height - this.panelCond.Height - this.groupAttParms.Height;
      if (flag)
      {
        this.cbResType.Visible = false;
        this.label1.Visible = false;
      }
    }
    if (flag)
    {
      this.Text = efType == ExpertFormulaType.Cond ? LocalizationHolder.rm.GetString("Expert.Editor_211") : LocalizationHolder.rm.GetString("Expert.Editor_675");
      this.gbFormula.Text = LocalizationHolder.rm.GetString("Expert.Editor_212");
      this.cbResType.SelectedIndex = 3;
    }
    else
      this.cbResType.SelectedIndex = 0;
    this.cbResType.Enabled = efType == ExpertFormulaType.SimpleFormula;
    this.EnableButtons();
  }

  /// <summary>
  /// Actually create object (call after Execute returns true)
  /// </summary>
  /// <returns>ID of new object</returns>
  public long createObject(long protoObjId, bool IsVersion)
  {
    if (this.efType != ExpertFormulaType.ESFolder)
    {
      if (!this.formChanged && !this.condChanged)
        return 0;
      if (this.formTF.Count == 0)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_213"), LocalizationHolder.rm.GetString("Expert.Editor_214"));
        return 0;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      switch (this.efType)
      {
        case ExpertFormulaType.CommonFormula:
          if (!this.condChanged)
          {
            if (!this.formChanged)
              break;
          }
          IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objFormula);
          IExpertFormula expertFormula = !IsVersion ? (protoObjId == -1L ? (IExpertFormula) objectCollection1.Create() : (IExpertFormula) objectCollection1.Create(protoObjId)) : (IExpertFormula) objectCollection1.CreateVersion(protoObjId);
          if (this.condChanged)
            expertFormula.Cond = this.condTF;
          AttribPair attribPair = new AttribPair(this.selObjAttr1.selAttr.ID);
          if (this.selObjAttr1.selObjType != null)
            attribPair.objTypeID = this.selObjAttr1.selObjType.ID;
          expertFormula.Result = attribPair;
          expertFormula.resAttrGuid = this.selObjAttr1.selAttr.GUID;
          expertFormula.resObjTypeGuid = this.selObjAttr1.selObjType == null ? "" : this.selObjAttr1.selObjType.GUID;
          expertFormula.Name = this.editName.Text;
          expertFormula.UpdateObject(this.formTF);
          expertFormula.CommitCreation(true);
          IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
          byte[] traceInfo = (byte[]) null;
          bool flag = false;
          if (customService != null)
            flag = customService.ReflectObjUpdate(sessionKeeper.Session.SessionGUID, expertFormula.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
          if (flag)
          {
            using (RuleUpdateReport ruleUpdateReport = new RuleUpdateReport())
              ruleUpdateReport.Execute(traceInfo);
          }
          return expertFormula.ObjectID;
        case ExpertFormulaType.Cond:
          IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objCond);
          IExpertCond expertCond1 = (!IsVersion ? (protoObjId == -1L ? objectCollection2.Create() : objectCollection2.Create(protoObjId)) : objectCollection2.CreateVersion(protoObjId)) as IExpertCond;
          expertCond1.Name = this.editName.Text;
          expertCond1.UpdateObject(this.formTF);
          expertCond1.CommitCreation(true);
          return expertCond1.ObjectID;
        case ExpertFormulaType.SimpleFormula:
          IDBObjectCollection objectCollection3 = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objSimpleFormula);
          IExpertFormulable expertFormulable = (!IsVersion ? (protoObjId == -1L ? objectCollection3.Create() : objectCollection3.Create(protoObjId)) : objectCollection3.CreateVersion(protoObjId)) as IExpertFormulable;
          expertFormulable.Name = this.editName.Text;
          expertFormulable.UpdateObject(this.formTF);
          expertFormulable.CommitCreation(true);
          return expertFormulable.ObjectID;
        case ExpertFormulaType.ESFolder:
          IDBObjectCollection objectCollection4 = sessionKeeper.Session.GetObjectCollection(ExpertConsts.Consts.objESFolder);
          IExpertCond expertCond2 = (!IsVersion ? (protoObjId == -1L ? objectCollection4.Create() : objectCollection4.Create(protoObjId)) : objectCollection4.CreateVersion(protoObjId)) as IExpertCond;
          expertCond2.Name = this.editName.Text;
          expertCond2.UpdateObject(this.formTF);
          expertCond2.CommitCreation(true);
          return expertCond2.ObjectID;
      }
    }
    return 0;
  }

  private bool ValidateAttr()
  {
    if (this.efType == ExpertFormulaType.CommonFormula)
    {
      if (this.selObjAttr1.selObjType == null && !this.selObjAttr1.NoObjType)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_215"), LocalizationHolder.rm.GetString("Expert.Editor_216"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      if (this.selObjAttr1.selAttr == null)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_217"), LocalizationHolder.rm.GetString("Expert.Editor_218"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
    }
    return true;
  }

  private void UpdateTitle()
  {
    if (!this.attrChanged)
      return;
    StringBuilder stringBuilder = new StringBuilder("\"");
    if (this.selObjAttr1.objTypeText != "")
      stringBuilder.Append(this.selObjAttr1.objTypeText + ".");
    stringBuilder.Append(this.selObjAttr1.attrText + "\" ");
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
    switch (this.efType)
    {
      case ExpertFormulaType.CommonFormula:
        this.formTF.resType = DataTypeConvertor.AttrType2DataType(this.selObjAttr1.attrType);
        break;
      case ExpertFormulaType.Cond:
        this.formTF.resType = DataType.Boolean;
        break;
      case ExpertFormulaType.SimpleFormula:
        switch (this.cbResType.SelectedIndex)
        {
          case 0:
            this.formTF.resType = DataType.String;
            break;
          case 1:
            this.formTF.resType = DataType.Integer;
            break;
          case 2:
            this.formTF.resType = DataType.Float;
            break;
          case 3:
            this.formTF.resType = DataType.Boolean;
            break;
          case 4:
            this.formTF.resType = DataType.Date;
            break;
          case 5:
            this.formTF.resType = DataType.Packet;
            break;
          case 6:
            this.formTF.resType = DataType.Measured;
            break;
        }
        break;
    }
    this.UpdateTitle();
    if (!this.formEd.Execute(ref this.formTF, this.title))
      return;
    this.formChanged = true;
    this.ShowFormula(false);
    this.EnableButtons();
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
    this.EnableButtons();
  }

  private void itemChangeForm_Click(object sender, EventArgs e) => this.ChangeFormula();

  private void itemDeleteForm_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_219"), LocalizationHolder.rm.GetString("Expert.Editor_220"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.formTF.Clear();
    this.richFormula.Clear();
    this.formChanged = true;
    this.EnableButtons();
  }

  private void itemChangeCond_Click(object sender, EventArgs e) => this.ChangeCond();

  private void itemDeleteCond_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_221"), LocalizationHolder.rm.GetString("Expert.Editor_222"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.condTF.Clear();
    this.richCond.Clear();
    this.condChanged = true;
    this.EnableButtons();
  }

  private void cbResType_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.efType != ExpertFormulaType.SimpleFormula)
      return;
    this.formTF.Clear();
    this.richFormula.Clear();
    this.formChanged = true;
  }

  private void selObjAttr1_Changed(object sender, EventArgs e)
  {
    try
    {
      switch (DataTypeConvertor.AttrType2DataType(this.selObjAttr1.attrType))
      {
        case DataType.Integer:
          this.cbResType.SelectedIndex = 1;
          break;
        case DataType.Float:
          this.cbResType.SelectedIndex = 2;
          break;
        case DataType.Measured:
          this.cbResType.SelectedIndex = 6;
          break;
        case DataType.String:
          this.cbResType.SelectedIndex = 0;
          break;
        case DataType.Date:
          this.cbResType.SelectedIndex = 4;
          break;
        case DataType.Boolean:
          this.cbResType.SelectedIndex = 3;
          break;
        case DataType.Packet:
          this.cbResType.SelectedIndex = 5;
          break;
      }
      string str = LocalizationHolder.rm.GetString("Expert.Editor_223");
      if (this.selObjAttr1.objTypeText != "")
        str = $"{str}{this.selObjAttr1.objTypeText}>.<";
      this.editName.Text = $"{str}{this.selObjAttr1.attrText}>";
    }
    catch
    {
    }
  }

  private void btnCreate_Click(object sender, EventArgs e)
  {
    if (this.formTF.Count != 0 || this.efType == ExpertFormulaType.ESFolder)
      return;
    this.DialogResult = DialogResult.None;
  }
}
