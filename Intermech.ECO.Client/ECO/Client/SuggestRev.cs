// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SuggestRev
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SuggestRev : Form
{
  private List<SuggestRev.RevInfo> revs = new List<SuggestRev.RevInfo>();
  private RevType showKind;
  private bool onlyMine;
  private bool onlyMyChecked;
  private long userId = -1;
  private List<int> indList = new List<int>();
  internal long res;
  private IContainer components;
  private ImageList imageList1;
  private Panel panel1;
  private Button btnCancel;
  private Button btnOK;
  private Panel panel2;
  private CheckBox cbOnlyChecked;
  private CheckBox cbOnlyMine;
  private Label label5;
  private RadioButton rbPR;
  private Label label4;
  private RadioButton rbPI;
  private Label label3;
  private RadioButton rbII;
  private ListView lv;
  private ColumnHeader IdCol;
  private ColumnHeader DesCol;
  private ColumnHeader CaptCol;
  private Button btnCreate;

  public SuggestRev() => this.InitializeComponent();

  private void GetRevisions()
  {
    this.revs.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this.userId = session.UserID;
      foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(new Guid(RevHelper.guidObjRevision)).Select(new DBRecordSetParams(new ConditionStructure[0], new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      })).Rows)
      {
        if (row[0] != DBNull.Value)
        {
          long int64 = Convert.ToInt64(row[0]);
          IDBObject dbObject = session.GetObject(int64);
          if (dbObject != null)
          {
            RevType _kind = RevType.II;
            if (dbObject.isParentType(new Guid(RevHelper.guidObj_PI)))
              _kind = RevType.PI;
            if (dbObject.isParentType(new Guid(RevHelper.guidObj_PR)))
              _kind = RevType.PR;
            bool _owned = dbObject.OwnerID == session.UserID;
            string asString = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString;
            this.revs.Add(new SuggestRev.RevInfo(_kind, int64, asString, dbObject.Caption, dbObject.CheckoutBy, _owned));
          }
        }
      }
    }
  }

  private void Update_LV()
  {
    this.lv.Items.Clear();
    this.indList.Clear();
    for (int index = 0; index < this.revs.Count; ++index)
    {
      SuggestRev.RevInfo rev = this.revs[index];
      if (rev.Kind == this.showKind && (!this.onlyMine || rev.owned) && (!this.onlyMyChecked || rev.checkedID == this.userId))
      {
        this.lv.Items.Add(new ListViewItem(new string[3]
        {
          Convert.ToString(rev.objId),
          rev.Design,
          rev.Caption
        }));
        this.indList.Add(index);
      }
    }
  }

  public long Execute()
  {
    this.GetRevisions();
    this.Update_LV();
    return this.ShowDialog() == DialogResult.OK ? this.res : 0L;
  }

  private void btnCreate_Click(object sender, EventArgs e) => this.res = -1L;

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.lv.SelectedIndices.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_175"), LocalizationHolder.rm.GetString("ECO.Client_176"));
      this.DialogResult = DialogResult.None;
    }
    this.res = this.revs[this.indList[this.lv.SelectedIndices[0]]].objId;
  }

  private void rbII_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbII.Checked)
      this.showKind = RevType.II;
    if (this.rbPI.Checked)
      this.showKind = RevType.PI;
    if (this.rbPR.Checked)
      this.showKind = RevType.PR;
    this.Update_LV();
  }

  private void cbOnlyMine_CheckedChanged(object sender, EventArgs e)
  {
    this.onlyMine = this.cbOnlyMine.Checked;
    this.Update_LV();
  }

  private void cbOnlyChecked_CheckedChanged(object sender, EventArgs e)
  {
    this.onlyMyChecked = this.cbOnlyChecked.Checked;
    this.Update_LV();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SuggestRev));
    this.imageList1 = new ImageList(this.components);
    this.panel1 = new Panel();
    this.btnCreate = new Button();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.panel2 = new Panel();
    this.cbOnlyChecked = new CheckBox();
    this.cbOnlyMine = new CheckBox();
    this.label5 = new Label();
    this.rbPR = new RadioButton();
    this.label4 = new Label();
    this.rbPI = new RadioButton();
    this.label3 = new Label();
    this.rbII = new RadioButton();
    this.lv = new ListView();
    this.IdCol = new ColumnHeader();
    this.DesCol = new ColumnHeader();
    this.CaptCol = new ColumnHeader();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "r1.bmp");
    this.imageList1.Images.SetKeyName(1, "r2.bmp");
    this.imageList1.Images.SetKeyName(2, "r3.bmp");
    this.panel1.Controls.Add((Control) this.btnCreate);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnCreate, "btnCreate");
    this.btnCreate.DialogResult = DialogResult.OK;
    this.btnCreate.Name = "btnCreate";
    this.btnCreate.UseVisualStyleBackColor = true;
    this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.panel2.Controls.Add((Control) this.cbOnlyChecked);
    this.panel2.Controls.Add((Control) this.cbOnlyMine);
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.rbPR);
    this.panel2.Controls.Add((Control) this.label4);
    this.panel2.Controls.Add((Control) this.rbPI);
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this.rbII);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.cbOnlyChecked, "cbOnlyChecked");
    this.cbOnlyChecked.Name = "cbOnlyChecked";
    this.cbOnlyChecked.UseVisualStyleBackColor = true;
    this.cbOnlyChecked.CheckedChanged += new EventHandler(this.cbOnlyChecked_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbOnlyMine, "cbOnlyMine");
    this.cbOnlyMine.Name = "cbOnlyMine";
    this.cbOnlyMine.UseVisualStyleBackColor = true;
    this.cbOnlyMine.CheckedChanged += new EventHandler(this.cbOnlyMine_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.ImageList = this.imageList1;
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.rbPR, "rbPR");
    this.rbPR.Name = "rbPR";
    this.rbPR.Tag = (object) "2";
    this.rbPR.UseVisualStyleBackColor = true;
    this.rbPR.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.ImageList = this.imageList1;
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.rbPI, "rbPI");
    this.rbPI.Name = "rbPI";
    this.rbPI.Tag = (object) "1";
    this.rbPI.UseVisualStyleBackColor = true;
    this.rbPI.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ImageList = this.imageList1;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.rbII, "rbII");
    this.rbII.Checked = true;
    this.rbII.Name = "rbII";
    this.rbII.TabStop = true;
    this.rbII.Tag = (object) "0";
    this.rbII.UseVisualStyleBackColor = true;
    this.rbII.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    this.lv.Columns.AddRange(new ColumnHeader[3]
    {
      this.IdCol,
      this.DesCol,
      this.CaptCol
    });
    componentResourceManager.ApplyResources((object) this.lv, "lv");
    this.lv.FullRowSelect = true;
    this.lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lv.HideSelection = false;
    this.lv.MultiSelect = false;
    this.lv.Name = "lv";
    this.lv.UseCompatibleStateImageBehavior = false;
    this.lv.View = View.Details;
    componentResourceManager.ApplyResources((object) this.IdCol, "IdCol");
    componentResourceManager.ApplyResources((object) this.DesCol, "DesCol");
    componentResourceManager.ApplyResources((object) this.CaptCol, "CaptCol");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lv);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SuggestRev);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }

  internal class RevInfo
  {
    internal RevType Kind;
    internal long objId = -1;
    internal string Design = "";
    internal string Caption = "";
    internal long checkedID = -1;
    internal bool owned;

    internal RevInfo(
      RevType _kind,
      long _objId,
      string _Design,
      string _Caption,
      long _checkedId,
      bool _owned)
    {
      this.Kind = _kind;
      this.objId = _objId;
      this.Design = _Design;
      this.Caption = _Caption;
      this.checkedID = _checkedId;
      this.owned = _owned;
    }
  }
}
