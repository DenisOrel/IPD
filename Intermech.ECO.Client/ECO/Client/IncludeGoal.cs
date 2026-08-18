// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.IncludeGoal
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class IncludeGoal : Form
{
  public bool AutoMove = true;
  public bool BlockCreate;
  public bool BlockSeparate;
  public RevType revType;
  public ECOGoal goal;
  public string litera = "";
  public int schemaId = -1;
  public bool separateChanges;
  internal DataTable dt;
  internal DataTable levels;
  internal List<int> lcstepIds = new List<int>();
  public int selLCStepId = -1;
  internal List<long> noDesObjects;
  internal List<long> objIds;
  internal List<long> addObjs;
  internal List<bool> allowLitera;
  internal List<bool> allowAddLitera;
  internal ECOPlugin plugin;
  internal bool _lockCheck;
  internal List<long> verList;
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private GroupBox gbGoal;
  private ComboBox cbLitera;
  private RadioButton rbReplace;
  private RadioButton rbLitera;
  private RadioButton rbAnnul;
  private RadioButton rbChange;
  private Label lblNoSchema;
  private ComboBox cb;
  private Label label1;
  private Label lblNoDes;
  private Label label2;
  private Label lblNoMove;
  private RadioButton rbCreate;
  private ListView objectsLV;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private CheckBox cbSeparateChanges;
  private Button btnSelVersion;

  public IncludeGoal()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 842);
  }

  public void CheckMovingObjects()
  {
    if (this.plugin == null)
      return;
    this.AutoMove = this.plugin.eps.Current.AutoMoveObjects;
  }

  protected void BlockLCChange()
  {
    this.plugin = ECOPlugin.FindPlugin();
    this.cb.Enabled = this.plugin.IsLCChangeAllowed;
  }

  public bool ExecuteForCJ(long objId, string lit, bool forceChange = false)
  {
    List<long> objIDs = new List<long>();
    objIDs.Add(objId);
    this.BlockSeparate = true;
    if (forceChange)
    {
      this.rbAnnul.Enabled = false;
      this.rbLitera.Enabled = false;
      this.rbReplace.Enabled = false;
      this.rbCreate.Enabled = false;
    }
    this.BlockLCChange();
    return this.Execute(objIDs, lit, (List<long>) null, (List<long>) null, RevType.CJ);
  }

  public bool Execute(
    List<long> objIDs,
    string lit,
    List<long> noDObjs,
    List<long> addObjects,
    RevType rt,
    ECOGoal force = ECOGoal.NoGoal,
    int schemaId = -1)
  {
    this.litera = lit;
    this.cbLitera.Text = this.litera;
    this.noDesObjects = noDObjs;
    this.objIds = objIDs;
    this.addObjs = addObjects;
    this.revType = rt;
    this.plugin = ECOPlugin.FindPlugin();
    this.CheckMovingObjects();
    if (this.BlockCreate)
      this.rbCreate.Enabled = false;
    this.GetAllowLiteras();
    this.ShowObjects();
    if (this.objectsLV.Items.Count == 0)
      return false;
    this.schemaId = schemaId == -1 ? this.GetCurSchemeId() : schemaId;
    this.UpdateNoDes();
    this.UpdateLifecycles();
    switch (force)
    {
      case ECOGoal.Change:
        this.rbAnnul.Enabled = false;
        this.rbLitera.Enabled = false;
        this.rbReplace.Enabled = false;
        this.rbCreate.Enabled = false;
        break;
      case ECOGoal.Creation:
        this.rbChange.Enabled = false;
        this.rbAnnul.Enabled = false;
        this.rbReplace.Enabled = false;
        this.rbLitera.Enabled = false;
        this.rbCreate.Enabled = true;
        this.rbCreate.Checked = true;
        break;
    }
    this.PerformCheck(1000000);
    this.cbSeparateChanges.Visible = !this.BlockSeparate && objIDs.Count > 1;
    this.BlockLCChange();
    return this.ShowDialog() == DialogResult.OK;
  }

  public bool Execute(
    ECOGoal goal,
    List<long> objIDs,
    string lit,
    List<long> noDObjs,
    List<long> addObjects,
    RevType rt)
  {
    if (goal == ECOGoal.NoGoal)
      return false;
    int num = (int) goal;
    this.revType = rt;
    this.plugin = ECOPlugin.FindPlugin();
    this.CheckMovingObjects();
    if (this.BlockCreate)
      this.rbCreate.Enabled = false;
    this.rbChange.Checked = num == 0;
    this.rbAnnul.Checked = num == 1;
    this.rbLitera.Checked = num == 2;
    this.rbReplace.Checked = num == 3;
    this.rbCreate.Checked = num == 4;
    this.rbChange.Enabled = this.rbChange.Checked;
    this.rbAnnul.Enabled = this.rbAnnul.Checked;
    this.rbLitera.Enabled = this.rbLitera.Checked;
    this.rbReplace.Enabled = this.rbReplace.Checked;
    this.rbCreate.Enabled = this.rbCreate.Checked;
    this.litera = lit;
    this.cbLitera.Text = this.litera;
    this.objIds = objIDs;
    this.addObjs = addObjects;
    this.GetAllowLiteras();
    this.ShowObjects();
    if (this.objectsLV.Items.Count == 0)
      return false;
    this.schemaId = this.GetCurSchemeId();
    this.noDesObjects = noDObjs;
    this.UpdateNoDes();
    this.UpdateLifecycles();
    this.PerformCheck(1000000);
    this.cbSeparateChanges.Visible = !this.BlockSeparate && objIDs.Count > 1;
    this.BlockLCChange();
    return this.ShowDialog() == DialogResult.OK;
  }

  public bool Execute(List<long> objIDs, string lit, HashSet<ECOGoal> allowedGoals)
  {
    this.litera = lit;
    this.cbLitera.Text = this.litera;
    this.objIds = objIDs;
    this.plugin = ECOPlugin.FindPlugin();
    this.CheckMovingObjects();
    if (this.BlockCreate)
      this.rbCreate.Enabled = false;
    this.GetAllowLiteras();
    this.ShowObjects();
    if (this.objectsLV.Items.Count == 0)
      return false;
    this.schemaId = this.schemaId == -1 ? this.GetCurSchemeId() : this.schemaId;
    this.UpdateNoDes();
    this.UpdateLifecycles();
    this.rbChange.Enabled = allowedGoals.Contains(ECOGoal.Change);
    this.rbCreate.Enabled = allowedGoals.Contains(ECOGoal.Creation);
    this.rbAnnul.Enabled = allowedGoals.Contains(ECOGoal.Annul);
    this.rbLitera.Enabled = allowedGoals.Contains(ECOGoal.Litera);
    this.rbReplace.Enabled = allowedGoals.Contains(ECOGoal.Replace);
    this.PerformCheck(1000000);
    this.cbSeparateChanges.Visible = false;
    this.BlockLCChange();
    return this.ShowDialog() == DialogResult.OK;
  }

  internal void ShowObjects()
  {
    this._lockCheck = true;
    try
    {
      this.objectsLV.Items.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objId in this.objIds)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
          if (dbObject != null)
          {
            string objectTypeName = MetaDataHelper.GetObjectTypeName(dbObject.ObjectType);
            this.objectsLV.Items.Add(new ListViewItem(new string[2]
            {
              dbObject.Caption,
              objectTypeName
            })
            {
              Tag = (object) objId
            });
          }
        }
      }
      this.objIds.Clear();
      foreach (ListViewItem listViewItem in this.objectsLV.Items)
        this.objIds.Add((long) listViewItem.Tag);
    }
    finally
    {
      this._lockCheck = false;
    }
  }

  public List<long> GetFinalObjectList()
  {
    List<long> finalObjectList = new List<long>();
    for (int index = 0; index < this.objIds.Count; ++index)
      finalObjectList.Add(this.objIds[index]);
    return finalObjectList;
  }

  public List<long> GetFinalObjectList(int Index)
  {
    List<long> finalObjectList = new List<long>();
    for (int index = 0; index < this.objIds.Count; ++index)
    {
      if (index == Math.Abs(Index))
      {
        if (Index >= 0)
          finalObjectList.Add(this.objIds[index]);
      }
      else
        finalObjectList.Add(this.objIds[index]);
    }
    return finalObjectList;
  }

  public long GetFinalObject() => this.objIds.Count > 0 ? this.objIds[0] : 0L;

  internal void GetAllowLiteras()
  {
    int maxLiteraIndex = this.plugin.GetMaxLiteraIndex((IEnumerable<long>) this.objIds);
    this.cbLitera.Items.Clear();
    for (int index = maxLiteraIndex + 1; index < this.plugin.PossibleLiteras.Count; ++index)
      this.cbLitera.Items.Add((object) this.plugin.PossibleLiteras[index]);
    this.allowLitera = new List<bool>();
    foreach (long objId in this.objIds)
      this.allowLitera.Add(this.plugin.AllowLitera(objId));
    if (this.addObjs == null)
      return;
    this.allowAddLitera = new List<bool>();
    foreach (long addObj in this.addObjs)
      this.allowAddLitera.Add(this.plugin.AllowLitera(addObj));
  }

  internal void PerformCheck(int Index)
  {
    int curSchemeId = this.GetCurSchemeId(this.GetFinalObjectList(Index));
    if (this.rbLitera.Enabled)
    {
      bool flag1 = true;
      foreach (bool flag2 in this.allowLitera)
      {
        if (!flag2)
        {
          flag1 = false;
          break;
        }
      }
      if (flag1 && this.allowAddLitera != null)
      {
        foreach (bool flag3 in this.allowAddLitera)
        {
          if (!flag3)
          {
            flag1 = false;
            break;
          }
        }
      }
      this.rbLitera.Enabled = flag1;
      this.cbLitera.Enabled = flag1 && this.rbLitera.Checked;
    }
    if (curSchemeId == this.schemaId || curSchemeId == -1)
      return;
    this.schemaId = curSchemeId;
    this.UpdateLifecycles();
    this.SetDefaultStep();
  }

  internal int GetCurSchemeId()
  {
    List<long> finalObjectList = this.GetFinalObjectList();
    if (this.addObjs != null)
    {
      foreach (long addObj in this.addObjs)
      {
        if (!finalObjectList.Contains(addObj))
          finalObjectList.Add(addObj);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ECOPlugin.GetSchemeID(sessionKeeper.Session, finalObjectList);
  }

  internal int GetCurSchemeId(List<long> res)
  {
    if (this.addObjs != null)
    {
      foreach (long addObj in this.addObjs)
      {
        if (!res.Contains(addObj))
          res.Add(addObj);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ECOPlugin.GetSchemeID(sessionKeeper.Session, res);
  }

  private void rbChange_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender as RadioButton).Checked)
      return;
    this.goal = (ECOGoal) Convert.ToInt32((sender as RadioButton).Tag);
    this.cbLitera.Enabled = this.goal == ECOGoal.Litera;
    this.SetDefaultStep();
  }

  private void cbLitera_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.litera == "" && this.cbLitera.Text != "")
      this.litera = this.cbLitera.Text;
    if (this.goal == ECOGoal.Litera && this.litera == "")
    {
      this.DialogResult = DialogResult.None;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_116"), LocalizationHolder.rm.GetString("ECO.Client_117"));
    }
    if (this.GetFinalObjectList().Count == 0)
    {
      this.DialogResult = DialogResult.None;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_239"), LocalizationHolder.rm.GetString("ECO.Client_117"));
    }
    this.separateChanges = this.cbSeparateChanges.Checked;
  }

  private void cbLitera_TextChanged(object sender, EventArgs e)
  {
    this.litera = this.cbLitera.Text;
    this.SetDefaultStep();
  }

  public void UpdateLifecycles()
  {
    if (this.schemaId == -1 || !this.AutoMove)
    {
      this.label1.Enabled = false;
      this.cb.Enabled = false;
      if (!this.AutoMove)
        this.lblNoMove.Visible = true;
      else
        this.lblNoMove.Visible = false;
    }
    else
    {
      if (!this.cb.Enabled)
      {
        this.label1.Enabled = true;
        this.cb.Enabled = true;
      }
      ECOPlugin.GetSchemeData(this.schemaId, out this.dt, out this.levels);
      this.lcstepIds.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        this.cb.Items.Add(row["F_LC_NAME"]);
        this.lcstepIds.Add(Convert.ToInt32(row["F_LC_STEP"]));
      }
    }
  }

  public void SetDefaultStep()
  {
    if (this.dt == null && this.schemaId != -1)
      ECOPlugin.GetSchemeData(this.schemaId, out this.dt, out this.levels);
    DataRow[] dataRowArray = (DataRow[]) null;
    if (this.dt == null)
      return;
    if (this.rbChange.Checked || this.rbReplace.Checked || this.rbCreate.Checked)
    {
      switch (this.revType)
      {
        case RevType.II:
          dataRowArray = this.dt.Select("F_GUID = 'cad003c4-306c-11d8-b4e9-00304f19f545'");
          break;
        case RevType.PI:
          dataRowArray = this.dt.Select("F_GUID = 'cadd970b-306c-11d8-b4e9-00304f19f545'");
          break;
      }
      if (dataRowArray == null || dataRowArray.Length == 0)
        dataRowArray = this.dt.Select("F_LEVEL_ID = " + Convert.ToString(RevHelper.idLevelManufacturing));
    }
    if (this.rbAnnul.Checked)
    {
      dataRowArray = this.dt.Select("F_GUID = 'cad003c6-306c-11d8-b4e9-00304f19f545'");
      if (dataRowArray == null || dataRowArray.Length == 0)
        dataRowArray = this.dt.Select("F_LEVEL_ID = " + Convert.ToString(RevHelper.idLevelAnnuled));
    }
    if (this.rbLitera.Checked)
    {
      if (this.litera == "")
      {
        this.cb.SelectedIndex = -1;
      }
      else
      {
        dataRowArray = this.levels.Select($"F_LITERA = '{this.litera}'");
        if (dataRowArray.Length != 0)
          dataRowArray = this.dt.Select("F_LEVEL_ID = " + Convert.ToString(Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"])));
        else
          this.cb.SelectedIndex = -1;
      }
    }
    if (dataRowArray == null || dataRowArray.Length == 0)
      return;
    this.selLCStepId = Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]);
    this.cb.SelectedIndex = this.lcstepIds.IndexOf(this.selLCStepId);
  }

  private void cb_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.cb.SelectedIndex == -1)
      this.selLCStepId = -1;
    else
      this.selLCStepId = this.lcstepIds[this.cb.SelectedIndex];
  }

  private void IncludeGoal_Shown(object sender, EventArgs e)
  {
    if (this.dt == null)
      return;
    this.SetDefaultStep();
  }

  private void UpdateNoDes()
  {
    if (this.noDesObjects == null || this.noDesObjects.Count <= 0)
      return;
    this.lblNoDes.Text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_228"), (object) this.noDesObjects.Count);
  }

  private void IncludeGoal_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(842);
  }

  private void IncludeGoal_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(842);
  }

  private void objectsLV_SelectedIndexChanged(object sender, EventArgs e)
  {
    int index = -1;
    if (this.objectsLV.SelectedIndices.Count > 0)
      index = this.objectsLV.SelectedIndices[0];
    this.verList = (List<long>) null;
    if (index >= 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.verList = sessionKeeper.Session.GetAllObjectVersionsList(this.objIds[index], false, false, false);
    }
    this.btnSelVersion.Enabled = this.verList != null && this.verList.Count > 1;
  }

  private void btnSelVersion_Click(object sender, EventArgs e)
  {
    if (this.verList == null)
      return;
    int index = -1;
    if (this.objectsLV.SelectedIndices.Count > 0)
      index = this.objectsLV.SelectedIndices[0];
    if (index < 0)
      return;
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (long ver in this.verList)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(ver));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("ECO.Client_399"), descriptors);
    IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) SelectionWindow.Select(LocalizationHolder.rm.GetString("ECO.Client_400"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect);
    if (dbTypedObjectIdArray == null || dbTypedObjectIdArray.Length == 0)
      return;
    this.objIds[index] = dbTypedObjectIdArray[0].ObjectID;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      if (this.dt != null)
        this.dt.Dispose();
      if (this.levels != null)
        this.levels.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IncludeGoal));
    this.panel1 = new Panel();
    this.btnSelVersion = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.gbGoal = new GroupBox();
    this.rbCreate = new RadioButton();
    this.cbLitera = new ComboBox();
    this.rbReplace = new RadioButton();
    this.rbLitera = new RadioButton();
    this.rbAnnul = new RadioButton();
    this.rbChange = new RadioButton();
    this.lblNoSchema = new Label();
    this.cb = new ComboBox();
    this.label1 = new Label();
    this.lblNoDes = new Label();
    this.label2 = new Label();
    this.lblNoMove = new Label();
    this.objectsLV = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.cbSeparateChanges = new CheckBox();
    this.panel1.SuspendLayout();
    this.gbGoal.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnSelVersion);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnSelVersion, "btnSelVersion");
    this.btnSelVersion.Name = "btnSelVersion";
    this.btnSelVersion.UseVisualStyleBackColor = true;
    this.btnSelVersion.Click += new EventHandler(this.btnSelVersion_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.gbGoal, "gbGoal");
    this.gbGoal.Controls.Add((Control) this.rbCreate);
    this.gbGoal.Controls.Add((Control) this.cbLitera);
    this.gbGoal.Controls.Add((Control) this.rbReplace);
    this.gbGoal.Controls.Add((Control) this.rbLitera);
    this.gbGoal.Controls.Add((Control) this.rbAnnul);
    this.gbGoal.Controls.Add((Control) this.rbChange);
    this.gbGoal.Name = "gbGoal";
    this.gbGoal.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbCreate, "rbCreate");
    this.rbCreate.Name = "rbCreate";
    this.rbCreate.TabStop = true;
    this.rbCreate.Tag = (object) "4";
    this.rbCreate.UseVisualStyleBackColor = true;
    this.rbCreate.CheckedChanged += new EventHandler(this.rbChange_CheckedChanged);
    this.cbLitera.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbLitera, "cbLitera");
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
    this.cbLitera.Name = "cbLitera";
    this.cbLitera.SelectedIndexChanged += new EventHandler(this.cbLitera_SelectedIndexChanged);
    this.cbLitera.TextChanged += new EventHandler(this.cbLitera_TextChanged);
    componentResourceManager.ApplyResources((object) this.rbReplace, "rbReplace");
    this.rbReplace.Name = "rbReplace";
    this.rbReplace.TabStop = true;
    this.rbReplace.Tag = (object) "3";
    this.rbReplace.UseVisualStyleBackColor = true;
    this.rbReplace.CheckedChanged += new EventHandler(this.rbChange_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbLitera, "rbLitera");
    this.rbLitera.Name = "rbLitera";
    this.rbLitera.TabStop = true;
    this.rbLitera.Tag = (object) "2";
    this.rbLitera.UseVisualStyleBackColor = true;
    this.rbLitera.CheckedChanged += new EventHandler(this.rbChange_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAnnul, "rbAnnul");
    this.rbAnnul.Name = "rbAnnul";
    this.rbAnnul.TabStop = true;
    this.rbAnnul.Tag = (object) "1";
    this.rbAnnul.UseVisualStyleBackColor = true;
    this.rbAnnul.CheckedChanged += new EventHandler(this.rbChange_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbChange, "rbChange");
    this.rbChange.Checked = true;
    this.rbChange.Name = "rbChange";
    this.rbChange.TabStop = true;
    this.rbChange.Tag = (object) "0";
    this.rbChange.UseVisualStyleBackColor = true;
    this.rbChange.CheckedChanged += new EventHandler(this.rbChange_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.lblNoSchema, "lblNoSchema");
    this.lblNoSchema.ForeColor = Color.Red;
    this.lblNoSchema.Name = "lblNoSchema";
    componentResourceManager.ApplyResources((object) this.cb, "cb");
    this.cb.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cb.FormattingEnabled = true;
    this.cb.Name = "cb";
    this.cb.SelectedIndexChanged += new EventHandler(this.cb_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.lblNoDes, "lblNoDes");
    this.lblNoDes.Name = "lblNoDes";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.lblNoMove, "lblNoMove");
    this.lblNoMove.ForeColor = Color.Red;
    this.lblNoMove.Name = "lblNoMove";
    componentResourceManager.ApplyResources((object) this.objectsLV, "objectsLV");
    this.objectsLV.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.objectsLV.FullRowSelect = true;
    this.objectsLV.GridLines = true;
    this.objectsLV.HeaderStyle = ColumnHeaderStyle.None;
    this.objectsLV.MultiSelect = false;
    this.objectsLV.Name = "objectsLV";
    this.objectsLV.UseCompatibleStateImageBehavior = false;
    this.objectsLV.View = View.Details;
    this.objectsLV.SelectedIndexChanged += new EventHandler(this.objectsLV_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.cbSeparateChanges, "cbSeparateChanges");
    this.cbSeparateChanges.Name = "cbSeparateChanges";
    this.cbSeparateChanges.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.cbSeparateChanges);
    this.Controls.Add((Control) this.objectsLV);
    this.Controls.Add((Control) this.lblNoMove);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.lblNoDes);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.cb);
    this.Controls.Add((Control) this.lblNoSchema);
    this.Controls.Add((Control) this.gbGoal);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (IncludeGoal);
    this.ShowInTaskbar = false;
    this.HelpButtonClicked += new CancelEventHandler(this.IncludeGoal_HelpButtonClicked);
    this.Shown += new EventHandler(this.IncludeGoal_Shown);
    this.HelpRequested += new HelpEventHandler(this.IncludeGoal_HelpRequested);
    this.panel1.ResumeLayout(false);
    this.gbGoal.ResumeLayout(false);
    this.gbGoal.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal class StringTag
  {
    public string Str;
    public long Tag;

    public StringTag(string S, long T)
    {
      this.Str = S;
      this.Tag = T;
    }

    public override string ToString() => this.Str;

    public override int GetHashCode() => Convert.ToInt32(this.Tag);
  }
}
