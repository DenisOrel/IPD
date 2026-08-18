// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevisionWizardForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class RevisionWizardForm : Form
{
  private List<long> _obj4EcoList;
  private int step;
  private Step1 step1;
  private Step2 step2;
  private Step3 step3;
  private int newObjectTypeID = -1;
  private long newECOObjectID;
  private RequireClass rc;
  private ImDocument documentECO;
  private MemoryStream ms;
  public List<long> ClassifiersToAdd = new List<long>();
  public ObjectsClassifyType _classifyType;
  private bool selType;
  private RevType rt;
  private long selRevId = -1;
  internal long curRevId;
  public bool abortVersion;
  private bool allowExisting;
  private bool blockEdOpening;
  private bool allowSelTemplate = true;
  private RevisionWizardForm.RevTypeChecker rtc;
  public long ECOObjectID = -1;
  private Color DisabledForeColor = Color.FromArgb(120, 120, 120);
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btnOk;
  private Button btnCancel;
  private Button btnBack;
  private Button btnExistingIzv;
  private Button btnNext;
  private Panel panel1;
  private Panel panel2;
  private Panel panel3;
  private Button btnCurrentIzv;
  private ToolTip ttRWF;
  private Button btnAnotherType;

  public ImDocument DocumentECO
  {
    set => this.documentECO = value;
    get => this.documentECO;
  }

  public MemoryStream MS
  {
    set => this.ms = value;
  }

  public RevType RT => this.rt;

  public long SelRevId => this.selRevId;

  public RevisionWizardForm(
    int aObjectTypeID,
    RequireClass rc,
    bool allowExisting,
    List<long> objIds,
    ECOGoal captionGoal,
    string objCaption,
    bool blockEditorOpening = false,
    bool allowSelTemplate = true)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 831);
    this.newObjectTypeID = aObjectTypeID;
    this.rc = rc;
    this.allowExisting = allowExisting;
    this._obj4EcoList = objIds;
    this.allowSelTemplate = allowSelTemplate;
    this.step1 = new Step1(rc);
    if (this.newObjectTypeID == RevHelper.idObj_II || this.newObjectTypeID == RevHelper.idObj_PI || this.newObjectTypeID == RevHelper.idObj_PR)
      this.step1.SetOnlyObjType(this.newObjectTypeID);
    this.LoadControl();
    this.blockEdOpening = blockEditorOpening;
    this.SetFormCaption(captionGoal, objCaption);
    if (objIds == null)
      return;
    this.rtc = new RevisionWizardForm.RevTypeChecker(objIds);
    this.DisableProhibitedRevTypes();
    this.ProcessOnlyRevType();
  }

  public RevisionWizardForm(
    int aObjectTypeID,
    RequireClass rc,
    bool allowExisting,
    List<long> objIds,
    ECOGoal captionGoal,
    QuickObjectInfo qoi,
    bool blockEditorOpening = false,
    bool allowSelTemplate = true)
    : this(aObjectTypeID, rc, allowExisting, objIds, captionGoal, qoi.Caption, blockEditorOpening, allowSelTemplate)
  {
    if (qoi.ObjectTypeID == MetaDataHelper.GetObjectTypeID("cadd9a5c-306c-11d8-b4e9-00304f19f545"))
      this.step1.SetOnlyObjType(RevHelper.idObj_IPV);
    if (objIds == null)
      return;
    this.rtc = new RevisionWizardForm.RevTypeChecker(objIds);
    this.DisableProhibitedRevTypes();
    this.ProcessOnlyRevType();
  }

  public RevisionWizardForm(
    List<int> objTypes,
    RequireClass rc,
    bool allowExisting,
    List<long> objIds,
    ECOGoal captionGoal,
    string objCaption,
    bool blockEditorOpening = false,
    bool allowSelTemplate = true)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 831);
    if (objTypes == null || objTypes.Count == 0)
      return;
    this.newObjectTypeID = objTypes[0];
    this.rc = rc;
    this.allowExisting = allowExisting;
    this._obj4EcoList = objIds;
    this.allowSelTemplate = allowSelTemplate;
    this.step1 = new Step1(rc);
    this.step1.SetObjTypes(objTypes);
    this.LoadControl();
    this.blockEdOpening = blockEditorOpening;
    this.SetFormCaption(captionGoal, objCaption);
    if (objIds == null)
      return;
    this.rtc = new RevisionWizardForm.RevTypeChecker(objIds);
    this.DisableProhibitedRevTypes();
    this.ProcessOnlyRevType();
  }

  public void LoadControl()
  {
    this.panel1.Controls.Clear();
    if (this.step == 0)
    {
      if (this.curRevId == 0L)
        this.curRevId = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).CachedEditingContextID;
      this.btnNext.Enabled = true;
      this.btnBack.Visible = false;
      this.btnExistingIzv.Visible = true;
      this.btnExistingIzv.Enabled = this.allowExisting;
      this.btnOk.Enabled = false;
      this.btnOk.Visible = false;
      this.btnCurrentIzv.Visible = true;
      string errMsg = "";
      if (this.allowExisting)
      {
        if (this.curRevId == 0L)
        {
          errMsg = LocalizationHolder.rm.GetString("ECO.Client_393");
        }
        else
        {
          int num = (int) this.ValidateEcoObjects(this.curRevId, out errMsg);
        }
        if (errMsg != "")
          this.ttRWF.SetToolTip((Control) this.btnCurrentIzv, errMsg);
        this.btnCurrentIzv.Enabled = this.allowExisting;
        if (errMsg != "")
        {
          this.btnCurrentIzv.ForeColor = this.DisabledForeColor;
          this.btnCurrentIzv.BackColor = Color.FromArgb(220, 220, 220);
        }
      }
      this.panel1.Controls.Add((Control) this.step1);
    }
    else if (this.step == 1)
    {
      this.btnNext.Text = LocalizationHolder.rm.GetString("ECO.Client_394");
      this.btnNext.Enabled = this.step2.NextIsAccessible;
      this.btnBack.Visible = true;
      if (!this.selType)
        this.btnBack.Enabled = true;
      else
        this.btnBack.Enabled = false;
      this.btnExistingIzv.Visible = false;
      this.btnCurrentIzv.Visible = false;
      this.btnOk.Visible = false;
      this.btnOk.Enabled = false;
      this.panel1.Controls.Add((Control) this.step2);
    }
    else
    {
      this.btnBack.Visible = true;
      this.btnNext.Enabled = false;
      if (!this.selType || this._classifyType != ObjectsClassifyType.None)
        this.btnBack.Enabled = true;
      else
        this.btnBack.Enabled = false;
      this.btnOk.Visible = true;
      this.btnOk.Enabled = true;
      this.btnExistingIzv.Visible = false;
      this.btnCurrentIzv.Visible = false;
      if (this.step3 == null)
        this.step3 = new Step3(this.newObjectTypeID);
      else
        this.step3.RevTypeID = this.newObjectTypeID;
      this.step3.SetupControls(this.allowSelTemplate);
      this.step3.ClassifyAttributes(this.newECOObjectID);
      this.panel1.Controls.Add((Control) this.step3);
    }
  }

  private void SetFormCaption(ECOGoal goal, string objCaption)
  {
    if (goal == ECOGoal.NoGoal)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_383"));
    switch (goal)
    {
      case ECOGoal.Change:
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_384"));
        break;
      case ECOGoal.Annul:
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_388"));
        break;
      case ECOGoal.Litera:
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_387"));
        break;
      case ECOGoal.Replace:
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_385"));
        break;
      case ECOGoal.Creation:
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_386"));
        break;
      default:
        if (goal != ECOGoal.VersionCreate)
        {
          if (goal == ECOGoal.Stamp)
          {
            stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_390"));
            break;
          }
          break;
        }
        stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_389"));
        break;
    }
    stringBuilder.Append($" '{objCaption}')");
    this.Text = stringBuilder.ToString();
  }

  private RevType ValidateEcoObjects(long ecoObjID, out string errMsg)
  {
    errMsg = "";
    if (ecoObjID == 0L || ecoObjID == -1L || this._obj4EcoList == null || this._obj4EcoList.Count == 0)
      return RevType.Unknown;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(ecoObjID, false);
      if (dbObject == null)
        return RevType.Unknown;
      RevType revType = RevHelper.objType2RevType(dbObject.ObjectType);
      long checkoutBy = dbObject.CheckoutBy;
      if (checkoutBy != 0L && checkoutBy != session.UserID)
        errMsg = LocalizationHolder.rm.GetString("ECO.Client_391");
      switch (dbObject.ObjectModifyMode)
      {
        case ObjectModifyModes.Checkout:
          dbObject = dbObject.CheckOut();
          break;
        case ObjectModifyModes.CantModify:
          errMsg = LocalizationHolder.rm.GetString("ECO.Client_392");
          break;
      }
      if (dbObject.ReadOnly)
        errMsg = LocalizationHolder.rm.GetString("ECO.Client_392");
      if (errMsg == "")
      {
        int maxDocsAllowed = ECOPlugin.plugin.eps.Current.MaxDocsAllowed;
        if (maxDocsAllowed > 0 && ECOPlugin.GetECO_ObjectsCount(ecoObjID) >= maxDocsAllowed)
          errMsg = string.Format(LocalizationHolder.rm.GetString("ECO.Client_431"), (object) maxDocsAllowed);
      }
      if (errMsg == "")
      {
        Dictionary<long, long> dictionary = new Dictionary<long, long>(this._obj4EcoList.Count);
        List<long> list = new List<long>((IEnumerable<long>) this._obj4EcoList);
        GenericListHelper.MakeUnique<long>(list);
        if (list.Count == 0)
          return revType;
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(2);
        columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        DataTable objectData = DataHelper.GetObjectData(-1, session, (IEnumerable<ConditionStructure>) new List<ConditionStructure>(1)
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) list.ToArray(), (object) null, LogicalOperators.NONE, 0, false)
        }.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray());
        if (objectData == null || objectData.Rows.Count == 0)
          return revType;
        int columnIndex1 = objectData.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex2 = objectData.Columns.IndexOf("F_ID");
        foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
        {
          if (row != null)
          {
            long int64_1 = Convert.ToInt64(row[columnIndex1]);
            long int64_2 = Convert.ToInt64(row[columnIndex2]);
            dictionary.Add(int64_1, int64_2);
          }
        }
        if (dictionary.Count == 0)
          return revType;
        columnDescriptorList.Clear();
        columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) -22, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) -5, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptorList.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        DataTable childSostavData = DataHelper.GetChildSostavData(this.selRevId, session, (IEnumerable<int>) new int[1]
        {
          MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545")
        }, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray());
        if (childSostavData == null || childSostavData.Rows.Count == 0)
          return revType;
        List<string> stringList = new List<string>(list.Count);
        int columnIndex3 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex4 = childSostavData.Columns.IndexOf("F_PART_ID");
        int columnIndex5 = childSostavData.Columns.IndexOf("F_VERSION_ID");
        int columnIndex6 = childSostavData.Columns.IndexOf("CAPTION");
        foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
        {
          if (row != null)
          {
            long int64_3 = Convert.ToInt64(row[columnIndex4]);
            if (dictionary.ContainsValue(int64_3))
            {
              long int64_4 = Convert.ToInt64(row[columnIndex3]);
              int int32 = Convert.ToInt32(row[columnIndex5]);
              string str = $"'{row[columnIndex6].ToString()}'[{int32}] ObjID = {int64_4}";
              stringList.Add(str);
            }
          }
        }
        if (stringList.Count == 0)
          return revType;
        errMsg = string.Format(LocalizationHolder.rm.GetString("ECO.Client_259"), (object) string.Join(", ", stringList.ToArray()));
      }
      return revType;
    }
  }

  private void ReportError(string msgText)
  {
    string caption = LocalizationHolder.rm.GetString("ECO.Client_260");
    int num = (int) MessageBox.Show((IWin32Window) this, msgText, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    this.DialogResult = DialogResult.None;
  }

  private void btnNext_Click(object sender, EventArgs e)
  {
    int objectTypeId = this.step1.objectTypeId;
    IObjectCreatorService service = (IObjectCreatorService) ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService));
    long objectID = -1;
    try
    {
      ECOPlugin.RevObjectCreator.allowChooseTemplate = this.allowSelTemplate;
      objectID = this.blockEdOpening ? service.CreateObjectByTypeDialog(objectTypeId, false) : service.CreateObjectByTypeDialog(objectTypeId);
    }
    finally
    {
      ECOPlugin.RevObjectCreator.allowChooseTemplate = true;
    }
    if (objectID == -1L)
      return;
    if (!this.allowSelTemplate)
    {
      this.ECOObjectID = objectID;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject docObject = sessionKeeper.Session.GetObject(objectID);
        this.ECOObjectID = docObject.ObjectID;
        this.documentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, false, true, false);
      }
    }
    this.rt = RevHelper.objType2RevType(objectTypeId);
    this.DialogResult = DialogResult.OK;
  }

  private void btnBack_Click(object sender, EventArgs e)
  {
    if (this.step == 2)
      this.step3.SaveAttr(this.newECOObjectID);
    if (this._classifyType == ObjectsClassifyType.None)
      this.step = 0;
    else
      --this.step;
    this.LoadControl();
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.step3.BeforeCreateObject())
    {
      try
      {
        this.ECOObjectID = this.step3.CreateObject(out this.documentECO, this.ms, this.newECOObjectID, this.ClassifiersToAdd).ObjectID;
      }
      catch (Exception ex)
      {
        this.DialogResult = DialogResult.None;
        throw ex;
      }
    }
    else
      this.DialogResult = DialogResult.None;
  }

  private void step2_EnableChangedEvent(object sender, EventArgs e)
  {
    this.btnNext.Enabled = this.step2.NextIsAccessible;
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.newECOObjectID == -1L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(this.newECOObjectID, false)?.Delete(0L);
  }

  private void btnExistingIzv_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_148"), LocalizationHolder.rm.GetString("ECO.Client_149"), RevHelper.idObjRevision, SelectionOptions.Default);
    if (numArray != null && numArray.Length != 0)
    {
      this.selRevId = numArray[0];
      string errMsg = "";
      this.rt = this.ValidateEcoObjects(this.selRevId, out errMsg);
      if (!(errMsg != ""))
        return;
      this.ReportError(errMsg);
    }
    else
      this.DialogResult = DialogResult.None;
  }

  private void btnCurrentIzv_Click(object sender, EventArgs e)
  {
    if (this.btnCurrentIzv.ForeColor != this.DisabledForeColor)
      this.selRevId = this.curRevId;
    else
      this.DialogResult = DialogResult.None;
  }

  private void btnAnotherType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("ECO.Client_453"), typeof (ObjectTypeFolder), false);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) this.rtc;
    int[] allowableTypes = new int[3]
    {
      RevHelper.idObj_II,
      RevHelper.idObj_PI,
      RevHelper.idObj_PR
    };
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(allowableTypes, true, true);
    selectorForm.AllowRootSelect = false;
    selectorForm.ExpandAll();
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    int num1 = selectorForm.IDList.Count == 0 ? -1 : (int) selectorForm.IDList[0];
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PI);
    List<int> childrenIdRecursive3 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PR);
    int num2 = num1;
    if (!childrenIdRecursive1.Contains(num2) && !childrenIdRecursive2.Contains(num1) && !childrenIdRecursive3.Contains(num1))
    {
      int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_454"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
      this.step1.SetChildType(num1, MetaDataHelper.GetObjectTypeName(num1));
  }

  private void DisableProhibitedRevTypes()
  {
    if (!this.rtc.AllowedRevTypes.Contains(RevHelper.idObj_II))
    {
      this.step1.rbII.Checked = false;
      this.step1.rbII.Enabled = false;
    }
    if (!this.rtc.AllowedRevTypes.Contains(RevHelper.idObj_PI))
    {
      this.step1.rbPI.Checked = false;
      this.step1.rbPI.Enabled = false;
    }
    if (!this.rtc.AllowedRevTypes.Contains(RevHelper.idObj_PR))
    {
      this.step1.rbPR.Checked = false;
      this.step1.rbPR.Enabled = false;
    }
    if (this.rtc.AllowedRevTypes.Contains(RevHelper.idObj_SN))
      return;
    this.step1.rbSN.Checked = false;
    this.step1.rbSN.Enabled = false;
  }

  private void ProcessOnlyRevType()
  {
    if (this.rtc.AllowedRevTypes.Count != 1)
      return;
    int num = this.rtc.AllowedRevTypes.Any<int>() ? this.rtc.AllowedRevTypes[0] : -1;
    RadioButton radioButton = (RadioButton) null;
    if (num == RevHelper.idObj_II)
      radioButton = this.step1.rbII;
    if (num == RevHelper.idObj_PI)
      radioButton = this.step1.rbPI;
    if (num == RevHelper.idObj_PR)
      radioButton = this.step1.rbPR;
    if (num == RevHelper.idObj_SN)
      radioButton = this.step1.rbSN;
    if (radioButton == null)
    {
      radioButton = this.step1.rbChild;
      this.step1.SetChildType(num, MetaDataHelper.GetObjectTypeName(num));
    }
    radioButton.Checked = true;
    this.btnAnotherType.Enabled = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RevisionWizardForm));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnNext = new Button();
    this.panel2 = new Panel();
    this.btnBack = new Button();
    this.btnExistingIzv = new Button();
    this.panel3 = new Panel();
    this.btnCurrentIzv = new Button();
    this.btnOk = new Button();
    this.ttRWF = new ToolTip(this.components);
    this.btnAnotherType = new Button();
    this.tableLayoutPanel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.btnAnotherType, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 4, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnNext, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.panel2, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.panel3, 3, 1);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.panel1, 5);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnNext, "btnNext");
    this.btnNext.Name = "btnNext";
    this.btnNext.UseVisualStyleBackColor = true;
    this.btnNext.Click += new EventHandler(this.btnNext_Click);
    this.panel2.Controls.Add((Control) this.btnBack);
    this.panel2.Controls.Add((Control) this.btnExistingIzv);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.btnBack, "btnBack");
    this.btnBack.Name = "btnBack";
    this.btnBack.UseVisualStyleBackColor = true;
    this.btnBack.Click += new EventHandler(this.btnBack_Click);
    this.btnExistingIzv.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnExistingIzv, "btnExistingIzv");
    this.btnExistingIzv.Name = "btnExistingIzv";
    this.btnExistingIzv.UseVisualStyleBackColor = true;
    this.btnExistingIzv.Click += new EventHandler(this.btnExistingIzv_Click);
    this.panel3.Controls.Add((Control) this.btnCurrentIzv);
    this.panel3.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    this.btnCurrentIzv.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnCurrentIzv, "btnCurrentIzv");
    this.btnCurrentIzv.ForeColor = SystemColors.ControlText;
    this.btnCurrentIzv.Name = "btnCurrentIzv";
    this.btnCurrentIzv.UseVisualStyleBackColor = true;
    this.btnCurrentIzv.Click += new EventHandler(this.btnCurrentIzv_Click);
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnAnotherType, "btnAnotherType");
    this.btnAnotherType.Name = "btnAnotherType";
    this.btnAnotherType.UseVisualStyleBackColor = true;
    this.btnAnotherType.Click += new EventHandler(this.btnAnotherType_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RevisionWizardForm);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public class RevTypeChecker : INodeSelectorFilter
  {
    public Dictionary<long, int> IncludingObjs { get; set; }

    public List<int> AllowedRevTypes { get; set; }

    public RevTypeChecker(List<long> objIds)
    {
      this.IncludingObjs = new Dictionary<long, int>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objId in objIds)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objId);
          if (!objectInfo.Empty)
            this.IncludingObjs.Add(objId, objectInfo.ObjectTypeID);
        }
      }
      this.UpdateAllowedRevTypes();
    }

    public bool IsRevTypeAllowed(int revType)
    {
      foreach (KeyValuePair<long, int> includingObj in this.IncludingObjs)
      {
        if (!RevisionWizardForm.RevTypeChecker.CanBeIncluded(revType, includingObj.Value))
          return false;
      }
      return true;
    }

    public void UpdateAllowedRevTypes()
    {
      this.AllowedRevTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObjRevision).Where<int>(new System.Func<int, bool>(this.IsRevTypeAllowed)).ToList<int>();
    }

    public static bool CanBeIncluded(int revType, int objType)
    {
      HashSet<int> intSet = new HashSet<int>((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(revType, RevHelper.idLinkRevision));
      List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(objType);
      parentsIdReverse.Add(objType);
      List<int> other = parentsIdReverse;
      return intSet.Overlaps((IEnumerable<int>) other);
    }

    public bool CanSelectNode(int category, object id, out string errorMessage)
    {
      int revType = (int) id;
      foreach (KeyValuePair<long, int> includingObj in this.IncludingObjs)
      {
        if (!RevisionWizardForm.RevTypeChecker.CanBeIncluded(revType, includingObj.Value))
        {
          errorMessage = string.Format(LocalizationHolder.rm.GetString("ECO.Client_455"), (object) includingObj.Key);
          return false;
        }
      }
      errorMessage = "";
      return true;
    }
  }
}
