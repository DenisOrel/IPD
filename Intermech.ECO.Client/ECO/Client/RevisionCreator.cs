// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevisionCreator
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core.History;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class RevisionCreator : ObjectCreatorControl
{
  private List<string> reasonIds;
  private List<long> templIds;
  private string reason = "-1";
  private int reason_index = -1;
  private long template = -1;
  private string design = "";
  private string revTypeGuid = "";
  public string litera = "";
  public long selRevId = -1;
  public RevType rt;
  private IContainer components;
  private GroupBox gbKind;
  private RadioButton rbII;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private RadioButton rbPR;
  private RadioButton rbPI;
  private GroupBox gb;
  private ListBox lbTemplates;
  private Label label2;
  private ButtonEdit beDesign;
  private Label lbl33;
  private System.Windows.Forms.ComboBox comboReason;
  private Panel hintPanel;
  private Label hintLabel;
  private Label label5;
  private ImageList imageList1;
  private Label label4;
  private Label label3;
  private Button btnCurrent;
  private Label label1;
  private System.Windows.Forms.ComboBox cbLitera;

  public RevisionCreator() => this.InitializeComponent();

  public void EnableLitera()
  {
    this.cbLitera.Visible = true;
    this.label1.Visible = true;
  }

  internal void SetupControls(RevCreateMode rcm)
  {
    switch (rcm)
    {
      case RevCreateMode.ByUser:
        this.hintPanel.Visible = false;
        this.Height -= this.hintPanel.Height;
        break;
      case RevCreateMode.ByVerSuggest:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("ECO.Client_141");
        this.btnCurrent.Visible = true;
        this.Text = LocalizationHolder.rm.GetString("ECO.Client_142");
        break;
      case RevCreateMode.ByVerForce:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("ECO.Client_139");
        this.btnCurrent.Visible = true;
        this.Text = LocalizationHolder.rm.GetString("ECO.Client_140");
        break;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataRow[] possibleValuesRows = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevReason)).GetPossibleValuesRows();
      this.reasonIds = new List<string>(possibleValuesRows.Length);
      foreach (DataRow dataRow in possibleValuesRows)
      {
        string str1 = Convert.ToString(dataRow["F_STRING_VALUE"]);
        string str2 = Convert.ToString(dataRow["F_DESCRIPTION"]);
        this.reasonIds.Add(str1);
        this.comboReason.Items.Add((object) str2);
      }
      this.comboReason.SelectedIndex = 1;
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(RevHelper.idObjTypeRevTemplate).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_GUID,
        (object) ObligatoryObjectAttributes.CAPTION
      }));
      this.templIds = new List<long>(dataTable.Rows.Count);
      this.lbTemplates.Items.Clear();
      DataRow dataRow1 = (DataRow) null;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToString(row[1]) == RevHelper.guidDefRevTemplate)
        {
          this.templIds.Add(Convert.ToInt64(row[0]));
          this.lbTemplates.Items.Add((object) LocalizationHolder.rm.GetString("ECO.Client_143"));
          dataRow1 = row;
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != dataRow1)
        {
          this.templIds.Add(Convert.ToInt64(row[0]));
          this.lbTemplates.Items.Add((object) Convert.ToString(row[2]));
        }
      }
      this.lbTemplates.SelectedIndex = 0;
    }
  }

  private void lbTemplates_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    Font font = e.Font;
    if (e.Index == 0)
      font = new Font(font, FontStyle.Bold);
    using (SolidBrush solidBrush = new SolidBrush(e.ForeColor))
      e.Graphics.DrawString(this.lbTemplates.Items[e.Index].ToString(), font, (Brush) solidBrush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    e.DrawFocusRectangle();
  }

  private void button2_Click(object sender, EventArgs e)
  {
    if (this.beDesign.Text == "")
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_146"), LocalizationHolder.rm.GetString("ECO.Client_147"));
    }
    else
    {
      this.design = this.beDesign.Text;
      if (this.rbII.Checked)
      {
        this.revTypeGuid = RevHelper.guidObj_II;
        this.rt = RevType.II;
      }
      if (this.rbPI.Checked)
      {
        this.revTypeGuid = RevHelper.guidObj_PI;
        this.rt = RevType.PI;
      }
      if (this.rbPR.Checked)
      {
        this.revTypeGuid = RevHelper.guidObj_PR;
        this.rt = RevType.PR;
      }
      this.reason = this.reasonIds[this.comboReason.SelectedIndex];
      this.reason_index = this.comboReason.SelectedIndex;
      this.template = this.templIds[this.lbTemplates.SelectedIndex];
      if (!this.cbLitera.Visible)
        return;
      this.litera = this.cbLitera.Text;
    }
  }

  private void btnCurrent_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_148"), LocalizationHolder.rm.GetString("ECO.Client_149"), RevHelper.idObjRevision, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.selRevId = numArray[0];
  }

  private void beDesign_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    using (ObjectsHistory objectsHistory = new ObjectsHistory(!this.rbII.Checked ? (!this.rbPI.Checked ? (object) RevHelper.idObj_PR : (object) RevHelper.idObj_PI) : (object) RevHelper.idObj_II, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545")))
    {
      objectsHistory.SelectedValue = (object) this.beDesign.Text.Trim();
      if (objectsHistory.ShowDialog() != DialogResult.OK)
        return;
      this.beDesign.Text = (string) objectsHistory.SelectedValue;
    }
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    args.Error = (Exception) null;
    return true;
  }

  public override bool Save(PageSaveArgs args)
  {
    args.Error = (Exception) null;
    return true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RevisionCreator));
    this.gbKind = new GroupBox();
    this.label5 = new Label();
    this.imageList1 = new ImageList(this.components);
    this.label4 = new Label();
    this.label3 = new Label();
    this.rbPR = new RadioButton();
    this.rbPI = new RadioButton();
    this.rbII = new RadioButton();
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.cbLitera = new System.Windows.Forms.ComboBox();
    this.btnCurrent = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.gb = new GroupBox();
    this.lbTemplates = new ListBox();
    this.label2 = new Label();
    this.beDesign = new ButtonEdit();
    this.lbl33 = new Label();
    this.comboReason = new System.Windows.Forms.ComboBox();
    this.hintPanel = new Panel();
    this.hintLabel = new Label();
    this.gbKind.SuspendLayout();
    this.panel1.SuspendLayout();
    this.gb.SuspendLayout();
    this.beDesign.Properties.BeginInit();
    this.hintPanel.SuspendLayout();
    this.SuspendLayout();
    this.gbKind.Controls.Add((Control) this.label5);
    this.gbKind.Controls.Add((Control) this.label4);
    this.gbKind.Controls.Add((Control) this.label3);
    this.gbKind.Controls.Add((Control) this.rbPR);
    this.gbKind.Controls.Add((Control) this.rbPI);
    this.gbKind.Controls.Add((Control) this.rbII);
    componentResourceManager.ApplyResources((object) this.gbKind, "gbKind");
    this.gbKind.Name = "gbKind";
    this.gbKind.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.ImageList = this.imageList1;
    this.label5.Name = "label5";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "r1.bmp");
    this.imageList1.Images.SetKeyName(1, "r2.bmp");
    this.imageList1.Images.SetKeyName(2, "r3.bmp");
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.ImageList = this.imageList1;
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ImageList = this.imageList1;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.rbPR, "rbPR");
    this.rbPR.Name = "rbPR";
    this.rbPR.Tag = (object) "2";
    this.rbPR.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbPI, "rbPI");
    this.rbPI.Name = "rbPI";
    this.rbPI.Tag = (object) "1";
    this.rbPI.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbII, "rbII");
    this.rbII.Checked = true;
    this.rbII.Name = "rbII";
    this.rbII.TabStop = true;
    this.rbII.Tag = (object) "0";
    this.rbII.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.cbLitera);
    this.panel1.Controls.Add((Control) this.btnCurrent);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.cbLitera.FormattingEnabled = true;
    this.cbLitera.Items.AddRange(new object[8]
    {
      (object) componentResourceManager.GetString("cbLitera.Items"),
      (object) componentResourceManager.GetString("cbLitera.Items1"),
      (object) componentResourceManager.GetString("cbLitera.Items2"),
      (object) componentResourceManager.GetString("cbLitera.Items3"),
      (object) componentResourceManager.GetString("cbLitera.Items4"),
      (object) componentResourceManager.GetString("cbLitera.Items5"),
      (object) componentResourceManager.GetString("cbLitera.Items6"),
      (object) componentResourceManager.GetString("cbLitera.Items7")
    });
    componentResourceManager.ApplyResources((object) this.cbLitera, "cbLitera");
    this.cbLitera.Name = "cbLitera";
    componentResourceManager.ApplyResources((object) this.btnCurrent, "btnCurrent");
    this.btnCurrent.DialogResult = DialogResult.OK;
    this.btnCurrent.Name = "btnCurrent";
    this.btnCurrent.UseVisualStyleBackColor = true;
    this.btnCurrent.Click += new EventHandler(this.btnCurrent_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.OK;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.gb, "gb");
    this.gb.Controls.Add((Control) this.lbTemplates);
    this.gb.Name = "gb";
    this.gb.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lbTemplates, "lbTemplates");
    this.lbTemplates.DrawMode = DrawMode.OwnerDrawFixed;
    this.lbTemplates.FormattingEnabled = true;
    this.lbTemplates.Name = "lbTemplates";
    this.lbTemplates.DrawItem += new DrawItemEventHandler(this.lbTemplates_DrawItem);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.beDesign, "beDesign");
    this.beDesign.Name = "beDesign";
    this.beDesign.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "История значений атрибута")
    });
    this.beDesign.ButtonClick += new ButtonPressedEventHandler(this.beDesign_ButtonClick);
    componentResourceManager.ApplyResources((object) this.lbl33, "lbl33");
    this.lbl33.Name = "lbl33";
    this.comboReason.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboReason.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.comboReason, "comboReason");
    this.comboReason.Name = "comboReason";
    this.hintPanel.Controls.Add((Control) this.hintLabel);
    componentResourceManager.ApplyResources((object) this.hintPanel, "hintPanel");
    this.hintPanel.Name = "hintPanel";
    componentResourceManager.ApplyResources((object) this.hintLabel, "hintLabel");
    this.hintLabel.ForeColor = Color.Purple;
    this.hintLabel.Name = "hintLabel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.hintPanel);
    this.Controls.Add((Control) this.comboReason);
    this.Controls.Add((Control) this.lbl33);
    this.Controls.Add((Control) this.beDesign);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.gb);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.gbKind);
    this.Name = nameof (RevisionCreator);
    this.gbKind.ResumeLayout(false);
    this.gbKind.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.gb.ResumeLayout(false);
    this.beDesign.Properties.EndInit();
    this.hintPanel.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
