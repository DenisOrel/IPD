// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.ImbaseTableWizardForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Imbase.Controls;
using Intermech.Imbase.TableWizard.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

public class ImbaseTableWizardForm : Form
{
  private long _tblsID;
  private long _refsID;
  private string _objsName = string.Empty;
  private IImbaseTableStep _stepCtrl;
  private IImbaseTableStep _stepFinish;
  private int _relsTypesID = -1;
  private long _templateObjsID;
  private DataSet _newDS;
  internal bool _bNextClick = true;
  private IContainer components;
  internal Button _btnFinish;
  internal Button _btnPrev;
  internal Button _btnNext;
  internal Button _btnCancel;
  public Panel _pStep;

  internal ImbaseTableWizardForm(
    int objectTypeID,
    int relationTypeID,
    long parentObjectID,
    long templateObjectID,
    bool isVersion)
  {
    this.InitializeComponent();
    this._templateObjsID = templateObjectID;
    this.ParentObjID = parentObjectID;
    this._relsTypesID = relationTypeID;
    this.ObjectTypeID = objectTypeID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this.ObjectTypeID);
      IDBObject dbObject = templateObjectID == 0L ? objectCollection.Create() : (isVersion ? objectCollection.CreateVersion(templateObjectID) : objectCollection.Create(templateObjectID));
      if (this.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        this.Text = dbObject.Caption = LocalizationHolder.rm.GetString(sc_8003.ssp_imbase_8004());
        this._stepCtrl = Activator.CreateInstance(typeof (Step1)) as IImbaseTableStep;
        this.FinalObjIsTbl = false;
      }
      else
      {
        dbObject.Caption = LocalizationHolder.rm.GetString("Imbase.Client_133");
        this.Text = LocalizationHolder.rm.GetString("Imbase.ImbaseTableWizard.Caption.Table");
        this._stepCtrl = Activator.CreateInstance(typeof (Step2)) as IImbaseTableStep;
        this.FinalObjIsTbl = true;
      }
      this.ObjectID = dbObject.ObjectID;
    }
    this._stepCtrl.WizardForm = this;
    this.SetEnabledButtons();
    this._stepCtrl.Context = new Dictionary<System.Type, object>();
    this._pStep.Controls.Add(this._stepCtrl as Control);
  }

  internal ImbaseTableWizardForm(long parentObjectID, long tableObjectID, bool disableFirstStep)
  {
    this.InitializeComponent();
    this.ParentObjID = parentObjectID;
    this.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
    this._relsTypesID = MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
    this.DisableFirstStep = disableFirstStep;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(tableObjectID);
      if (!objectInfo.Empty)
        this._objsName = objectInfo.Caption;
      IDBObject dbObject = (sessionKeeper.Session.GetObjectCollection(this.ObjectTypeID) ?? throw new NullCollectionException(LocalizationHolder.rm.GetString(sc_8003.ssp_imbase_8005()), $"{LocalizationHolder.rm.GetString("Imbase_NullCollection_Msg")} {this.ObjectTypeID}")).Create();
      this.Text = dbObject.Caption = LocalizationHolder.rm.GetString(sc_8003.ssp_imbase_8006());
      this.ObjectID = dbObject.ObjectID;
      this._stepCtrl = Activator.CreateInstance(typeof (Step4)) as IImbaseTableStep;
      this.FinalObjIsTbl = false;
    }
    this._stepCtrl.WizardForm = this;
    this.SetEnabledButtons();
    this._stepCtrl.Context = new Dictionary<System.Type, object>()
    {
      {
        typeof (Step1),
        (object) new Step1Params(1, tableObjectID)
      }
    };
    this._pStep.Controls.Add(this._stepCtrl as Control);
  }

  internal bool DisableFirstStep { get; private set; }

  internal DataSet DS
  {
    get
    {
      this._newDS = this._newDS ?? TableLoadHelper.CreateDataSet();
      return this._newDS;
    }
    set => this._newDS = value;
  }

  internal bool FinalObjIsTbl { get; private set; }

  internal long ObjectID
  {
    get => this.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID ? this._tblsID : this._refsID;
    set
    {
      if (this.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        this._refsID = value;
      else
        this._tblsID = value;
    }
  }

  internal string ObjectName
  {
    get
    {
      if (string.IsNullOrEmpty(this._objsName))
        this._objsName = this.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableTypeID ? LocalizationHolder.rm.GetString("Imbase.ImbaseTableWizard.Caption.TableRef") : LocalizationHolder.rm.GetString("Imbase.Client_133");
      return this._objsName;
    }
    set => this._objsName = value;
  }

  internal int ObjectTypeID { get; set; }

  internal long ParentObjID { get; set; }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    if (this.DialogResult != DialogResult.Cancel || this.ObjectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
      if (dbObject == null)
        return;
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        dbObject.Delete(0L);
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
      }
    }
  }

  private void ImbaseTableWizardForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ImbaseTableWizardForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if ((sender as Form).DialogResult == DialogResult.None)
      e.Cancel = true;
    else if ((sender as Form).DialogResult == DialogResult.OK)
    {
      FormStorage.SaveLayout((Control) this);
      if (this.ObjectID != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID);
          IDBRelation dbRelation = (IDBRelation) null;
          try
          {
            if (this._relsTypesID != -1 && this.ParentObjID != 0L)
              dbRelation = sessionKeeper.Session.GetRelationCollection(this._relsTypesID).Create(this.ParentObjID, dbObject.ObjectID);
            dbObject.CommitCreation(false);
          }
          catch (Exception ex)
          {
            e.Cancel = true;
            dbRelation?.Delete(0L);
            if (!(this._stepCtrl is Step3))
            {
              this._pStep.Controls.Clear();
              if (this._stepCtrl is IDisposable)
                (this._stepCtrl as IDisposable).Dispose();
              this._stepCtrl = this._stepFinish;
              this._stepFinish = (IImbaseTableStep) null;
              this._pStep.Controls.Add(this._stepCtrl as Control);
              this.SetEnabledButtons();
            }
            throw;
          }
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbObject.ObjectID));
            if (dbRelation != null)
              service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, this.ParentObjID, dbRelation.RelationType));
          }
          this.ObjectID = dbObject.ObjectID;
          if (this._stepFinish != null)
            (this._stepFinish as IDisposable).Dispose();
        }
      }
    }
    this.OnClosing((CancelEventArgs) e);
  }

  private void OnbtnFinish_Click(object sender, EventArgs e)
  {
    this._bNextClick = true;
    bool flag = true;
    try
    {
      while (flag)
      {
        IImbaseTableStep imbaseTableStep = this._stepFinish == null ? this._stepCtrl : this._stepFinish;
        Dictionary<System.Type, object> context = imbaseTableStep.Context;
        System.Type nextStep = imbaseTableStep.NextStep;
        if (nextStep == (System.Type) null)
        {
          if (this.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
          {
            if (this._newDS != null && this._newDS.Tables["IMS_DATA"].Rows.Count == 0)
              this.CheckUniqueIndexes();
            else
              this.CheckUniqueIndexesWithTableData();
          }
          flag = false;
        }
        else
        {
          this._stepFinish = Activator.CreateInstance(nextStep) as IImbaseTableStep;
          this._stepFinish.WizardForm = this;
          this.SetEnabledButtons();
          this._stepFinish.Context = context;
        }
      }
    }
    catch (Exception ex)
    {
      this.DialogResult = DialogResult.None;
      if (this._stepFinish != null)
      {
        this._pStep.Controls.Clear();
        if (this._stepCtrl is IDisposable)
          (this._stepCtrl as IDisposable).Dispose();
        this._stepCtrl = this._stepFinish;
        this._stepFinish = (IImbaseTableStep) null;
        this._pStep.Controls.Add(this._stepCtrl as Control);
        this.SetEnabledButtons();
      }
      throw;
    }
  }

  private void OnbtnNext_Click(object sender, EventArgs e)
  {
    this._bNextClick = true;
    Dictionary<System.Type, object> context = this._stepCtrl.Context;
    System.Type nextStep = this._stepCtrl.NextStep;
    this._pStep.Controls.Clear();
    if (this._stepCtrl is IDisposable)
      (this._stepCtrl as IDisposable).Dispose();
    this._stepCtrl = Activator.CreateInstance(nextStep) as IImbaseTableStep;
    this._pStep.Controls.Add(this._stepCtrl as Control);
    this._stepCtrl.WizardForm = this;
    this.SetEnabledButtons();
    this._stepCtrl.Context = context;
  }

  private void OnbtnPrev_Click(object sender, EventArgs e)
  {
    this._bNextClick = false;
    if (this._stepCtrl is Step3)
      (this._stepCtrl as Step3).CommitData = false;
    Dictionary<System.Type, object> context = this._stepCtrl.Context;
    System.Type prevStep = this._stepCtrl.PrevStep;
    this._pStep.Controls.Clear();
    if (this._stepCtrl is IDisposable)
      (this._stepCtrl as IDisposable).Dispose();
    this._stepCtrl = Activator.CreateInstance(prevStep) as IImbaseTableStep;
    this._pStep.Controls.Add(this._stepCtrl as Control);
    this._stepCtrl.WizardForm = this;
    this.SetEnabledButtons();
    this._stepCtrl.Context = context;
  }

  private void SetEnabledButtons()
  {
    if (this._stepCtrl is Step3)
    {
      this._btnPrev.Enabled = true;
      this._btnNext.Enabled = this.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableTypeID && !this.FinalObjIsTbl;
    }
    else
    {
      if (!(this._stepCtrl is Step4))
        return;
      this.Text = LocalizationHolder.rm.GetString("Imbase.ImbaseTableWizard.Caption.TableRef");
    }
  }

  private void CheckUniqueIndexes()
  {
    string message = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
      {
        long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, this.ParentObjID);
        if (catalogIdByObjectId != 0L)
        {
          IImbaseIndexingService imbaseIndexingService = customService;
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          List<long> catalogIDs = new List<long>();
          catalogIDs.Add(catalogIdByObjectId);
          string[] colsNames = new string[3]
          {
            IndexesField.F_ATTRIBUTE_ID,
            IndexesField.F_ATTRIBUTE_STATE,
            IndexesField.F_FLAG
          };
          DataTable uniqueIndexes = imbaseIndexingService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames);
          IDBObject objectActualCopy = uniqueIndexes != null ? sessionKeeper.Session.GetObjectActualCopy(this.ObjectID, false) : (IDBObject) null;
          if (objectActualCopy != null)
          {
            List<IDBAttribute> list1 = objectActualCopy.Attributes.ToList();
            List<int> lockAttrIDs = uniqueIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
            if (lockAttrIDs.Count > 0)
            {
              List<IDBAttribute> all = list1.FindAll((Predicate<IDBAttribute>) (x => lockAttrIDs.Contains(x.AttributeID)));
              if (all.Count > 0)
              {
                List<string> list2 = all.Select<IDBAttribute, string>((System.Func<IDBAttribute, string>) (x => $"'{x.Name}' (ID = {x.AttributeID})")).ToList<string>();
                message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_LockedAttributes"), (object) string.Join(", ", list2.ToArray()));
              }
            }
            if (string.IsNullOrEmpty(message))
            {
              List<int> uIndexes = uniqueIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) != Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
              Dictionary<int, object> dictionary = list1.Where<IDBAttribute>((System.Func<IDBAttribute, bool>) (x => uIndexes.Contains(x.AttributeID))).ToDictionary<IDBAttribute, int, object>((System.Func<IDBAttribute, int>) (p => p.AttributeID), (System.Func<IDBAttribute, object>) (k => k.Value));
              List<int> source = dictionary.Count > 0 ? customService.CheckUniqueBeforeTableRefCreate(sessionKeeper.Session.SessionGUID, catalogIdByObjectId, dictionary) : (List<int>) null;
              if (source != null)
              {
                if (source.Count == 1)
                {
                  string str = $"{MetaDataHelper.GetAttributeType(source[0]).Name} (ID = {source[0]})";
                  message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttribute"), (object) str);
                }
                else
                {
                  List<string> list3 = source.Select<int, string>((System.Func<int, string>) (x => $"{MetaDataHelper.GetAttributeType(x).Name} (ID = {x})")).ToList<string>();
                  message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttributes"), (object) string.Join(", ", list3.ToArray()));
                }
              }
            }
          }
        }
      }
    }
    if (!string.IsNullOrEmpty(message))
      throw new Exception(message);
  }

  private void CheckUniqueIndexesWithTableData()
  {
    string message = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
      {
        long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, this.ParentObjID);
        if (catalogIdByObjectId != 0L)
        {
          IImbaseIndexingService imbaseIndexingService = customService;
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          List<long> catalogIDs = new List<long>();
          catalogIDs.Add(catalogIdByObjectId);
          string[] colsNames = new string[3]
          {
            IndexesField.F_ATTRIBUTE_ID,
            IndexesField.F_ATTRIBUTE_STATE,
            IndexesField.F_FLAG
          };
          DataTable uniqueIndexes = imbaseIndexingService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames);
          IDBObject objectActualCopy = uniqueIndexes != null ? sessionKeeper.Session.GetObjectActualCopy(this.ObjectID, false) : (IDBObject) null;
          if (objectActualCopy != null)
          {
            List<IDBAttribute> list1 = objectActualCopy.Attributes.ToList();
            List<int> lockAttrIDs = uniqueIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
            if (lockAttrIDs.Count > 0)
            {
              List<IDBAttribute> all = list1.FindAll((Predicate<IDBAttribute>) (x => lockAttrIDs.Contains(x.AttributeID)));
              if (all.Count > 0)
              {
                List<string> list2 = all.Select<IDBAttribute, string>((System.Func<IDBAttribute, string>) (x => $"'{x.Name}' (ID = {x.AttributeID})")).ToList<string>();
                message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_LockedAttributes"), (object) string.Join(", ", list2.ToArray()));
              }
            }
            if (string.IsNullOrEmpty(message))
            {
              List<int> uIndexes = uniqueIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) != Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
              Dictionary<int, object> dictionary = list1.Where<IDBAttribute>((System.Func<IDBAttribute, bool>) (x => uIndexes.Contains(x.AttributeID))).ToDictionary<IDBAttribute, int, object>((System.Func<IDBAttribute, int>) (p => p.AttributeID), (System.Func<IDBAttribute, object>) (k => k.Value));
              List<string> stringList = (List<string>) null;
              DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, this._tblsID, false);
              DataTable attTable = (DataTable) null;
              DataTable recordsTable = (DataTable) null;
              if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA"))
              {
                attTable = tables.Tables["IMS_ATTR_TYPES"];
                recordsTable = tables.Tables["IMS_DATA"];
                if (recordsTable.Rows.Count > 0)
                {
                  stringList = new List<string>(attTable.Rows.Count);
                  string empty = string.Empty;
                  foreach (int attrTypeID in uIndexes)
                  {
                    string str = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
                    if (attTable.Select($"[{"F_ATTRIBUTE_GUID"}]='{str}'").Length != 0)
                      stringList.Add(str);
                  }
                }
              }
              if (stringList != null)
              {
                if (stringList.Count > 0)
                {
                  try
                  {
                    AttributeTypeProperties[] columnsAttributes = (AttributeTypeProperties[]) null;
                    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
                    TableLoadHelper.AssignAttributes(sessionKeeper.Session, this._refsID, this._tblsID, recordsTable, attTable, out columnsAttributes, new List<CalculatedColumn>(), ref keyInfo);
                  }
                  catch (Exception ex)
                  {
                    QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._refsID);
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_AssignAttributes_Error"), (object) objectInfo.Caption, (object) this._refsID), ex);
                  }
                  DataTable table = recordsTable.DefaultView.ToTable(false, stringList.ToArray());
                  List<int> notUniqueColumns = new List<int>(table.Columns.Count);
                  List<int> source = customService.CheckUniqueBeforeTableRefCreate(sessionKeeper.Session.SessionGUID, catalogIdByObjectId, dictionary, table, out notUniqueColumns);
                  if (source != null)
                  {
                    if (source.Count == 1)
                    {
                      message = $"{MetaDataHelper.GetAttributeType(source[0]).Name} (ID = {source[0]})";
                      message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttribute"), (object) message);
                    }
                    else
                    {
                      List<string> list3 = source.Select<int, string>((System.Func<int, string>) (x => $"{MetaDataHelper.GetAttributeType(x).Name} (ID = {x})")).ToList<string>();
                      message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttributes"), (object) string.Join(", ", list3.ToArray()));
                    }
                  }
                  else if (notUniqueColumns.Count > 0)
                  {
                    if (notUniqueColumns.Count == 1)
                    {
                      message = $"{MetaDataHelper.GetAttributeType(notUniqueColumns[0]).Name} (ID = {notUniqueColumns[0]})";
                      message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueColumn"), (object) message);
                    }
                    else
                    {
                      List<string> list4 = notUniqueColumns.Select<int, string>((System.Func<int, string>) (x => $"{MetaDataHelper.GetAttributeType(x).Name} (ID = {x})")).ToList<string>();
                      message = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueColumns"), (object) string.Join(", ", list4.ToArray()));
                    }
                  }
                  if (string.IsNullOrEmpty(message))
                  {
                    message = !this.CheckTableID(this._tblsID) ? LocalizationHolder.rm.GetString("Imbase_Indexing_MoreLinksUseTableID") : string.Empty;
                    goto label_39;
                  }
                  goto label_39;
                }
              }
              if (dictionary.Count > 0)
              {
                List<int> source = customService.CheckUniqueBeforeTableRefCreate(sessionKeeper.Session.SessionGUID, catalogIdByObjectId, dictionary);
                if (source != null)
                {
                  if (source.Count == 1)
                  {
                    string str = $"{MetaDataHelper.GetAttributeType(source[0]).Name} (ID = {source[0]})";
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttribute"), (object) str));
                  }
                  List<string> list5 = source.Select<int, string>((System.Func<int, string>) (x => $"{MetaDataHelper.GetAttributeType(x).Name} (ID = {x})")).ToList<string>();
                  throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueAttributes"), (object) string.Join(", ", list5.ToArray())));
                }
              }
            }
          }
        }
      }
    }
label_39:
    if (!string.IsNullOrEmpty(message))
      throw new Exception(message);
  }

  internal bool CheckTableID(long tableID)
  {
    bool flag = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.DenyFewLinksForSameTable)
        return true;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      if (objectCollection != null)
      {
        if (sessionKeeper.Session.GetObjectActualCopy(this.ParentObjID, false) != null)
        {
          ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) Math.Abs(tableID), LogicalOperators.NONE, 0, false)
          }, new ColumnDescriptor[1]{ columnDescriptor });
          DataTable dataTable = objectCollection.Select(paramSet);
          flag = dataTable == null || dataTable.Rows.Count == 0;
        }
      }
    }
    return flag;
  }

  private void ImbaseTableWizardForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowHelpTopic();
  }

  private void ImbaseTableWizardForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    this.ShowHelpTopic();
  }

  private void ShowHelpTopic()
  {
    HelpProvidersClass.ShowHelpTopic(this._stepCtrl is Step2 ? 885 : (this._stepCtrl is Step3 ? 697 : 1535 /*0x05FF*/));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTableWizardForm));
    this._btnPrev = new Button();
    this._btnNext = new Button();
    this._btnCancel = new Button();
    this._btnFinish = new Button();
    this._pStep = new Panel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnPrev, "_btnPrev");
    this._btnPrev.Name = "_btnPrev";
    this._btnPrev.UseVisualStyleBackColor = true;
    this._btnPrev.Click += new EventHandler(this.OnbtnPrev_Click);
    componentResourceManager.ApplyResources((object) this._btnNext, "_btnNext");
    this._btnNext.Name = "_btnNext";
    this._btnNext.UseVisualStyleBackColor = true;
    this._btnNext.Click += new EventHandler(this.OnbtnNext_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnFinish, "_btnFinish");
    this._btnFinish.DialogResult = DialogResult.OK;
    this._btnFinish.Name = "_btnFinish";
    this._btnFinish.UseVisualStyleBackColor = true;
    this._btnFinish.Click += new EventHandler(this.OnbtnFinish_Click);
    componentResourceManager.ApplyResources((object) this._pStep, "_pStep");
    this._pStep.Name = "_pStep";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._btnPrev);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnNext);
    this.Controls.Add((Control) this._btnFinish);
    this.Controls.Add((Control) this._pStep);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ImbaseTableWizardForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.ImbaseTableWizardForm_Load);
    this.HelpButtonClicked += new CancelEventHandler(this.ImbaseTableWizardForm_HelpButtonClicked);
    this.FormClosing += new FormClosingEventHandler(this.ImbaseTableWizardForm_FormClosing);
    this.HelpRequested += new HelpEventHandler(this.ImbaseTableWizardForm_HelpRequested);
    this.ResumeLayout(false);
  }
}
