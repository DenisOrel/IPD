// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpFormView
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Control for formulae ViewProvider</summary>
[ViewDescriptionProvider(typeof (ExpFormView.ExpFormViewDescriptionProvider))]
public class ExpFormView : UserControl, IView
{
  private Button btnApply;
  private Button btnCancel;
  private Panel panel2;
  private GroupBox gbFormula;
  private Splitter splitter1;
  private Panel panel3;
  private GroupBox gbCond;
  private Panel panel4;
  private RichTextBox richFormula;
  private Panel panel5;
  private RichTextBox richCond;
  private Splitter splitter2;
  private Panel panel6;
  private Panel panel7;
  private CheckEdit checkForm;
  private CheckEdit checkCond;
  private RichTextBox richDeshifr;
  private IContainer components;
  private long _objID;
  private long formID;
  private long condID;
  private bool formChanged;
  private bool condChanged;
  private bool readOnly;
  private FormEditor formEd;
  private TempFormula formTF;
  private TempFormula condTF;
  private Button btnChangeForm;
  private Button btnDelCond;
  private Button btnChangeCond;
  private ToolTip toolTipFE;
  private Label label1;
  private string title = "";
  private int formObjType;
  private bool DeshifrFormula = true;

  public ExpFormView()
  {
    this.InitializeComponent();
    this.formEd = new FormEditor();
    this.formTF = new TempFormula(true);
    this.formTF.Cond = false;
    this.condTF = new TempFormula(true);
    this.condTF.resType = DataType.Boolean;
    this.condTF.Cond = true;
  }

  public ExpFormView(long objID)
  {
    this.InitializeComponent();
    this.formEd = new FormEditor();
    this.formTF = new TempFormula(true);
    this.formTF.Cond = false;
    this.condTF = new TempFormula(true);
    this.condTF.resType = DataType.Boolean;
    this.condTF.Cond = true;
    this._objID = objID;
    this.formID = objID;
    this.LoadForms();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpFormView));
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel2 = new Panel();
    this.gbFormula = new GroupBox();
    this.richFormula = new RichTextBox();
    this.panel4 = new Panel();
    this.btnChangeForm = new Button();
    this.splitter1 = new Splitter();
    this.panel3 = new Panel();
    this.gbCond = new GroupBox();
    this.richCond = new RichTextBox();
    this.panel5 = new Panel();
    this.btnDelCond = new Button();
    this.btnChangeCond = new Button();
    this.splitter2 = new Splitter();
    this.panel6 = new Panel();
    this.label1 = new Label();
    this.richDeshifr = new RichTextBox();
    this.panel7 = new Panel();
    this.checkCond = new CheckEdit();
    this.checkForm = new CheckEdit();
    this.toolTipFE = new ToolTip();
    this.panel2.SuspendLayout();
    this.gbFormula.SuspendLayout();
    this.panel4.SuspendLayout();
    this.panel3.SuspendLayout();
    this.gbCond.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panel6.SuspendLayout();
    this.panel7.SuspendLayout();
    this.checkCond.Properties.BeginInit();
    this.checkForm.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panel2.Controls.Add((Control) this.gbFormula);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.gbFormula, "gbFormula");
    this.gbFormula.Controls.Add((Control) this.richFormula);
    this.gbFormula.Controls.Add((Control) this.panel4);
    this.gbFormula.Name = "gbFormula";
    this.gbFormula.TabStop = false;
    componentResourceManager.ApplyResources((object) this.richFormula, "richFormula");
    this.richFormula.Name = "richFormula";
    this.richFormula.ReadOnly = true;
    this.richFormula.MouseMove += new MouseEventHandler(this.richFormula_MouseMove);
    this.panel4.Controls.Add((Control) this.btnChangeForm);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.btnChangeForm, "btnChangeForm");
    this.btnChangeForm.Name = "btnChangeForm";
    this.btnChangeForm.Click += new EventHandler(this.itemChangeForm_Click);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panel3.Controls.Add((Control) this.gbCond);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.gbCond, "gbCond");
    this.gbCond.Controls.Add((Control) this.richCond);
    this.gbCond.Controls.Add((Control) this.panel5);
    this.gbCond.Name = "gbCond";
    this.gbCond.TabStop = false;
    componentResourceManager.ApplyResources((object) this.richCond, "richCond");
    this.richCond.Name = "richCond";
    this.richCond.ReadOnly = true;
    this.richCond.MouseMove += new MouseEventHandler(this.richCond_MouseMove);
    this.panel5.Controls.Add((Control) this.btnDelCond);
    this.panel5.Controls.Add((Control) this.btnChangeCond);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.btnDelCond, "btnDelCond");
    this.btnDelCond.Name = "btnDelCond";
    this.btnDelCond.Click += new EventHandler(this.itemDeleteCond_Click);
    componentResourceManager.ApplyResources((object) this.btnChangeCond, "btnChangeCond");
    this.btnChangeCond.Name = "btnChangeCond";
    this.btnChangeCond.Click += new EventHandler(this.itemChangeCond_Click);
    componentResourceManager.ApplyResources((object) this.splitter2, "splitter2");
    this.splitter2.Name = "splitter2";
    this.splitter2.TabStop = false;
    this.splitter2.SplitterMoved += new SplitterEventHandler(this.splitter2_SplitterMoved);
    this.panel6.Controls.Add((Control) this.label1);
    this.panel6.Controls.Add((Control) this.richDeshifr);
    this.panel6.Controls.Add((Control) this.panel7);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.richDeshifr, "richDeshifr");
    this.richDeshifr.Name = "richDeshifr";
    this.richDeshifr.ReadOnly = true;
    this.panel7.Controls.Add((Control) this.btnCancel);
    this.panel7.Controls.Add((Control) this.checkCond);
    this.panel7.Controls.Add((Control) this.btnApply);
    this.panel7.Controls.Add((Control) this.checkForm);
    componentResourceManager.ApplyResources((object) this.panel7, "panel7");
    this.panel7.Name = "panel7";
    componentResourceManager.ApplyResources((object) this.checkCond, "checkCond");
    this.checkCond.Name = "checkCond";
    this.checkCond.Properties.Caption = componentResourceManager.GetString("checkCond.Properties.Caption");
    this.checkCond.Properties.CheckStyle = CheckStyles.Style10;
    this.checkCond.Properties.RadioGroupIndex = 1;
    this.checkCond.TabStop = false;
    this.checkCond.CheckedChanged += new EventHandler(this.checkCond_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.checkForm, "checkForm");
    this.checkForm.Name = "checkForm";
    this.checkForm.Properties.Caption = componentResourceManager.GetString("checkForm.Properties.Caption");
    this.checkForm.Properties.CheckStyle = CheckStyles.Style10;
    this.checkForm.Properties.RadioGroupIndex = 1;
    this.Controls.Add((Control) this.panel6);
    this.Controls.Add((Control) this.splitter2);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (ExpFormView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "";
    this.panel2.ResumeLayout(false);
    this.gbFormula.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.gbCond.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.panel6.PerformLayout();
    this.panel7.ResumeLayout(false);
    this.checkCond.Properties.EndInit();
    this.checkForm.Properties.EndInit();
    this.ResumeLayout(false);
  }

  public int ImageIndex => -1;

  public int OrderID => 0;

  public string Caption => LocalizationHolder.rm.GetString("Expert.Editor_126");

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
  }

  public void Activate(IView previousView)
  {
    if (this._objID == this.formID)
      return;
    this.formID = this._objID;
    this.LoadForms();
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    if (dictionary.ContainsKey("panel2Hei"))
      this.panel2.Height = dictionary["panel2Hei"];
    if (!dictionary.ContainsKey("panel3Hei"))
      return;
    this.panel3.Height = dictionary["panel3Hei"];
  }

  public void Deactivate(IView nextView)
  {
    if ((this.condChanged || this.formChanged) && MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_127"), LocalizationHolder.rm.GetString("Expert.Editor_128"), MessageBoxButtons.YesNo) == DialogResult.Yes)
      this.SaveForms();
    this.formID = 0L;
    this.condID = 0L;
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, int>()
    {
      {
        "panel2Hei",
        this.panel2.Height
      },
      {
        "panel3Hei",
        this.panel3.Height
      }
    });
  }

  internal void EnableButtons()
  {
    this.btnChangeForm.Enabled = !this.readOnly && this.formTF != null;
    this.btnChangeCond.Enabled = !this.readOnly && this.formTF != null;
    this.btnDelCond.Enabled = !this.readOnly && this.formTF != null && this.condID != 0L;
    this.btnApply.Enabled = this.formTF != null && (this.formChanged || this.condChanged);
    this.btnCancel.Enabled = this.formTF != null && (this.formChanged || this.condChanged);
    this.panel3.Visible = this.formObjType != ExpertConsts.Consts.objESFolder;
    this.checkCond.Visible = this.formObjType != ExpertConsts.Consts.objESFolder;
  }

  internal void LoadForms()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.condTF != null)
        this.condTF.Clear();
      if (this.formTF != null)
        this.formTF.Clear();
      IExpertObject expertObject = (IExpertObject) sessionKeeper.Session.GetObject(this.formID);
      expertObject.Load();
      if (expertObject is IExpertFormulable)
      {
        this.formObjType = expertObject.ObjectType;
        try
        {
          this.formTF = (expertObject as IExpertFormulable).GetTempFormula();
        }
        catch
        {
          this.formTF = (TempFormula) null;
          this.btnApply.Enabled = false;
          this.readOnly = true;
          this.EnableButtons();
          return;
        }
        if (expertObject is IExpertFormula)
        {
          AttribPair result = (expertObject as IExpertFormula).Result;
          if (result != null)
          {
            DataType dataType = DataTypeConvertor.AttrType2DataType(MetaDataHelper.GetAttributeType(result.attribID).RealFieldType);
            if (dataType != this.formTF.resType)
            {
              this.formTF.resType = dataType;
              this.richFormula.BackColor = Color.Red;
            }
          }
        }
        this.formTF.BeautifyInfixForm();
        this.formTF.CheckAllTokens(sessionKeeper.Session);
        this.formTF.UpdateTokenBegs();
        this.ShowFormula(false);
        this.readOnly = expertObject.ReadOnly;
        string str = "";
        if (expertObject is IExpertCond)
        {
          str = LocalizationHolder.rm.GetString("Expert.Editor_129");
          this.title = "";
        }
        else if (expertObject is IExpertFormula)
        {
          this.title = (expertObject as IExpertFormula).resName;
          str = $"{LocalizationHolder.rm.GetString("Expert.Editor_130")}{this.title}\"";
        }
        this.gbFormula.Text = $"{str} [{DataTypeConvertor.DataTypeName(this.formTF.resType)}]";
        this.condTF = (expertObject as IExpertFormulable).Cond;
        if (this.condTF != null)
        {
          this.condTF.BeautifyInfixForm();
          this.condTF.CheckAllTokens(sessionKeeper.Session);
          this.condTF.UpdateTokenBegs();
        }
        this.ShowFormula(true);
        this.formChanged = false;
        this.condChanged = false;
        this.EnableButtons();
        this.UpdateDeshifr();
      }
      IExpertFormula expertFormula = expertObject as IExpertFormula;
    }
  }

  internal void SaveForms()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.formChanged || this.condChanged)
      {
        IExpertFormulable expertFormulable = (IExpertFormulable) sessionKeeper.Session.GetObject(this.formID);
        expertFormulable.Load();
        if (this.condChanged)
        {
          this.condTF.FillObjectLinks();
          expertFormulable.Cond = this.condTF;
        }
        if (this.formChanged)
          this.formTF.FillObjectLinks();
        expertFormulable.UpdateObject(this.formTF);
      }
      this.formChanged = false;
      this.condChanged = false;
      this.EnableButtons();
    }
  }

  private void btnApply_Click(object sender, EventArgs e) => this.SaveForms();

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.LoadForms();
    this.UpdateDeshifr();
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
    switch (t.state)
    {
      case TokenState.ObjNotFound:
        memoForm.SelectionBackColor = Color.Red;
        break;
      case TokenState.ObjCaptionChanged:
        memoForm.SelectionBackColor = Color.Yellow;
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
    if (tempFormula != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < tempFormula.Count; ++index)
        stringBuilder.Append(tempFormula[index].text);
      memoForm.Text = stringBuilder.ToString();
      for (int index = 0; index < tempFormula.Count; ++index)
        this.PaintCurToken(tempFormula[index], memoForm);
    }
    else
      memoForm.Text = "";
  }

  private void ChangeFormula()
  {
    if (!this.formEd.Execute(ref this.formTF, this.title))
      return;
    this.formChanged = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.formTF.BeautifyInfixForm();
      this.formTF.CheckAllTokens(sessionKeeper.Session);
      this.formTF.UpdateTokenBegs();
    }
    this.ShowFormula(false);
    this.btnApply.Enabled = true;
    this.btnCancel.Enabled = true;
    if (!this.DeshifrFormula)
      return;
    this.UpdateDeshifr();
  }

  private void ChangeCond()
  {
    if (this.condTF == null)
      this.condTF = new TempFormula();
    if (!this.formEd.Execute(ref this.condTF, this.title))
      return;
    this.condChanged = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.condTF.BeautifyInfixForm();
      this.condTF.CheckAllTokens(sessionKeeper.Session);
      this.condTF.UpdateTokenBegs();
    }
    this.ShowFormula(true);
    this.btnApply.Enabled = true;
    this.btnCancel.Enabled = true;
    if (this.DeshifrFormula)
      return;
    this.UpdateDeshifr();
  }

  private void itemChangeForm_Click(object sender, EventArgs e) => this.ChangeFormula();

  private void itemDeleteForm_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_131"), LocalizationHolder.rm.GetString("Expert.Editor_132"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.formTF.Clear();
    this.richFormula.Clear();
    this.formChanged = true;
    this.btnApply.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  private void itemChangeCond_Click(object sender, EventArgs e) => this.ChangeCond();

  private void itemDeleteCond_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_133"), LocalizationHolder.rm.GetString("Expert.Editor_134"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.condTF.Clear();
    this.richCond.Clear();
    this.condChanged = true;
    this.EnableButtons();
    if (this.DeshifrFormula)
      return;
    this.UpdateDeshifr();
  }

  private void richCond_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.condTF == null)
      return;
    int tokenByPos = this.condTF.GetTokenByPos(this.richCond.GetCharIndexFromPosition(new Point(e.X, e.Y)));
    string caption = "";
    if (tokenByPos >= 0)
    {
      Token token = this.condTF[tokenByPos];
      if (token.type == Intermech.Expert.TokenType.Integer && token.text != token.trueText)
        caption = token.trueText;
    }
    if (!(caption != this.toolTipFE.GetToolTip((Control) this.richCond)))
      return;
    this.toolTipFE.SetToolTip((Control) this.richCond, caption);
  }

  private void richFormula_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.formTF == null)
      return;
    int tokenByPos = this.formTF.GetTokenByPos(this.richFormula.GetCharIndexFromPosition(new Point(e.X, e.Y)));
    string caption = "";
    if (tokenByPos >= 0)
    {
      Token token = this.formTF[tokenByPos];
      if (token.type == Intermech.Expert.TokenType.Integer && token.text != token.trueText)
        caption = token.trueText;
    }
    if (!(caption != this.toolTipFE.GetToolTip((Control) this.richFormula)))
      return;
    this.toolTipFE.SetToolTip((Control) this.richFormula, caption);
  }

  private void checkCond_CheckedChanged(object sender, EventArgs e)
  {
    this.DeshifrFormula = this.checkForm.Checked;
    this.UpdateDeshifr();
  }

  private void UpdateDeshifr()
  {
    if (this.formTF == null)
      return;
    if (this.DeshifrFormula)
      this.richDeshifr.Text = this.formTF.FullText();
    else
      this.richDeshifr.Text = this.condTF != null ? this.condTF.FullText() : "";
  }

  private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
  {
  }

  private sealed class ExpFormViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Expert.Editor_126"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
