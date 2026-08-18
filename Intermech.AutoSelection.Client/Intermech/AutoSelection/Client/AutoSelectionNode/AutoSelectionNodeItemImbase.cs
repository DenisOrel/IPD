// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeItemImbase
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.AutoSelection.Client.Forms;
using Intermech.Expert.User;
using Intermech.Extensions.WinForms;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

[TypeConverter(typeof (AutoSelectionNodeItemImbaseConverter))]
public class AutoSelectionNodeItemImbase : AutoSelectionNodeItemCommon
{
  private AS_Long _imbaseCatalogId;
  private AS_Long _imbaseObjectId;
  private string _imbaseObjectCaption = string.Empty;
  private AutoSelectionTableInfo _tableInfo;

  private void InitializeData() => this._type = AutoSelectionNodeType.ItemImbase;

  protected virtual List<long> ExecuteDataTable(AutoSelectionSession asSession)
  {
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    List<long> longList = new List<long>();
    if (this.ImbaseObjectID.Value == -1L || this.ImbaseObjectID.Value == 0L)
      return longList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      DataTable recordsTable;
      AutoSelectionUtils.ServiceKeeper.GetImbaseServerService(session).LoadRecords(session.SessionGUID, this.ImbaseObjectID.Value, string.Empty, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out AttributeTypeProperties[] _, out ImbaseKeyInfo _);
      if (recordsTable == null || recordsTable.Rows.Count == 0)
        return longList;
      List<DataRow> dataRowList1 = new List<DataRow>();
      int columnIndex1 = recordsTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttID.ToString());
      if (columnIndex1 == -1)
      {
        dataRowList1.AddRange((IEnumerable<DataRow>) recordsTable.Select());
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) recordsTable.Rows)
        {
          object obj = row[columnIndex1];
          if (obj == DBNull.Value)
          {
            dataRowList1.Add(row);
          }
          else
          {
            string str = Convert.ToString(obj);
            if (string.IsNullOrEmpty(str) || str[0] != '-')
              dataRowList1.Add(row);
          }
        }
      }
      if (dataRowList1.Count == 0)
        return longList;
      IExpertUser expertUserService = AutoSelectionUtils.ServiceKeeper.GetExpertUserService();
      IExpertServer expertServerService = AutoSelectionUtils.ServiceKeeper.GetExpertServerService(session);
      int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(AutoSelectionConsts.etoDoubleExpertAttrGuid);
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(AutoSelectionConsts.etoMeasuredExpertAttrGuid);
      int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID(AutoSelectionConsts.etoStringExpertAttrGuid);
      Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
      foreach (DataColumn column in (InternalDataCollectionBase) recordsTable.Columns)
      {
        int result;
        if (int.TryParse(column.ColumnName, out result))
        {
          Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(result);
          if (!(attributeTypeGuid == Guid.Empty))
            dictionary[attributeTypeGuid] = column.Ordinal;
        }
      }
      foreach (AutoSelectionNodeCondition cond in (List<AutoSelectionNodeCondition>) this.TableInfo.CondList)
      {
        int num;
        if (dictionary.TryGetValue(cond.AttributeGUID, out num) && cond.Condition != null && cond.Condition.Count != 0)
        {
          DataColumn dataColumn = (DataColumn) null;
          if (num != -1)
            dataColumn = recordsTable.Columns[num];
          int attributeId = MetaDataHelper.GetAttributeID((object) cond.AttributeGUID.ToString());
          int taskId = expertServerService.StartTask(sessionKeeper.Session.SessionGUID, ExpertTraceFlags.None);
          try
          {
            expertServerService.SetDateTimeFormat(taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
            expertServerService.SetNumberFormat(taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
            expertServerService.SetTrace(taskId, expertUserService.ShowTraceWindow);
            expertServerService.SetLog(taskId, expertUserService.ReportLog);
            expertServerService.SetTraceFlags(taskId, ExpertTask.GetConfTraceFlags());
            int index = 0;
            while (index < dataRowList1.Count)
            {
              DataRow dataRow = dataRowList1[index];
              object obj1 = dataRow[num];
              if (dataColumn != null && dataColumn.ExtendedProperties.Contains((object) "F_MEASURE"))
              {
                long int64 = Convert.ToInt64(dataColumn.ExtendedProperties[(object) "F_MEASURE"]);
                MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(int64);
                MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(obj1.ToString(), descriptor, true);
                if (measuredValue.MeasureID != int64)
                  measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
                obj1 = (object) measuredValue;
              }
              expertServerService.DeleteParmValue(taskId, asSession.Params.ObjectID, attributeId);
              expertServerService.SetParmValue(taskId, asSession.Params.ObjectID, attributeId, obj1);
              switch (obj1)
              {
                case MeasuredValue _:
                  expertServerService.DeleteParmValue(taskId, asSession.Params.ObjectID, attributeTypeId2);
                  expertServerService.SetParmValue(taskId, asSession.Params.ObjectID, attributeTypeId2, obj1);
                  break;
                case string _:
                  expertServerService.DeleteParmValue(taskId, asSession.Params.ObjectID, attributeTypeId3);
                  expertServerService.SetParmValue(taskId, asSession.Params.ObjectID, attributeTypeId3, obj1);
                  break;
                default:
                  expertServerService.DeleteParmValue(taskId, asSession.Params.ObjectID, attributeTypeId1);
                  expertServerService.SetParmValue(taskId, asSession.Params.ObjectID, attributeTypeId1, obj1);
                  break;
              }
              object obj2;
              if (expertServerService.CalcFormula(taskId, (object) cond.Condition, asSession.ContextInfo.ObjectIds.ToArray<long>(), out obj2, asSession.ContextInfo.RelationIds.FirstOrDefault<long>()).Equals((object) ExpertResult.OK))
              {
                if ((!(obj2 is bool) ? 0 : (Convert.ToBoolean(obj2) ? 1 : 0)) == 0)
                  dataRowList1.Remove(dataRow);
                else
                  ++index;
              }
              else
                dataRowList1.Remove(dataRow);
            }
          }
          finally
          {
            if (expertUserService.ShowTraceWindow)
              ExpertUser.rur.Execute(expertServerService.GetTraceInfo(taskId), true);
            expertServerService.EndTask(taskId);
          }
        }
      }
      if (dataRowList1.Count == 0)
        return longList;
      string str1 = -2.ToString();
      if (this.TableInfo.RowList.Count > 0 && recordsTable.Columns.Contains(str1))
      {
        List<DataRow> dataRowList2 = new List<DataRow>();
        foreach (DataRow dataRow in dataRowList1)
        {
          long result;
          if (dataRow != null && long.TryParse(dataRow[str1].ToString(), out result) && result != 0L && this.TableInfo.RowList.GetRow(result) != null)
            dataRowList2.Add(dataRow);
        }
        if (dataRowList2.Count != 0)
          dataRowList1 = dataRowList2;
      }
      if (dataRowList1.Count == 0)
        return longList;
      foreach (AutoSelectionNodeCondition cond in (List<AutoSelectionNodeCondition>) this.TableInfo.CondList)
      {
        int columnIndex2;
        if (dictionary.TryGetValue(cond.AttributeGUID, out columnIndex2) && (cond.Addon == AutoSelectionNodeCondRule.Max || cond.Addon == AutoSelectionNodeCondRule.Min))
        {
          object obj3 = (object) null;
          object obj4 = (object) null;
          DataRow dataRow1 = (DataRow) null;
          foreach (DataRow dataRow2 in dataRowList1)
          {
            switch (cond.Addon)
            {
              case AutoSelectionNodeCondRule.Min:
                if (obj3 is IComparable comparable1)
                {
                  if (comparable1.CompareTo(dataRow2[columnIndex2]) > 0)
                  {
                    obj3 = dataRow2[columnIndex2];
                    dataRow1 = dataRow2;
                    continue;
                  }
                  continue;
                }
                obj3 = dataRow2[columnIndex2];
                dataRow1 = dataRow2;
                continue;
              case AutoSelectionNodeCondRule.Max:
                if (obj4 is IComparable comparable2)
                {
                  if (comparable2.CompareTo(dataRow2[columnIndex2]) < 0)
                  {
                    obj4 = dataRow2[columnIndex2];
                    dataRow1 = dataRow2;
                    continue;
                  }
                  continue;
                }
                obj4 = dataRow2[columnIndex2];
                dataRow1 = dataRow2;
                continue;
              default:
                continue;
            }
          }
          if (dataRow1 != null)
          {
            List<DataRow> dataRowList3 = new List<DataRow>();
            foreach (DataRow dataRow3 in dataRowList1)
            {
              if (dataRow3 != null && (!(dataRow3[columnIndex2] is IComparable comparable) || comparable.CompareTo(dataRow1[columnIndex2]) == 0))
                dataRowList3.Add(dataRow3);
            }
            dataRowList1 = dataRowList3;
          }
        }
      }
      if (dataRowList1.Count > 1)
      {
        List<string> values = new List<string>();
        foreach (DataRow dataRow in dataRowList1)
        {
          long result;
          if (long.TryParse(dataRow[str1].ToString(), out result))
            values.Add(result.ToString());
        }
        string str2 = $"[{str1}] IN ({string.Join(",", (IEnumerable<string>) values)})";
        AutoSelectionRowSelectForm form = new AutoSelectionRowSelectForm()
        {
          ObjectID = asSession.Params.ObjectID
        };
        form.TableView.ObjectId = this.ImbaseObjectID.Value;
        form.TableView.Filter = str2;
        CheckedRecords.Active = true;
        try
        {
          if (form.ShowTopDialog() != DialogResult.OK)
            dataRowList1.Clear();
          else
            dataRowList1 = form.SelectedRows;
        }
        finally
        {
          CheckedRecords.Active = false;
        }
      }
      if (dataRowList1.Count == 0)
        return longList;
      foreach (DataRow dataRow in dataRowList1)
      {
        long result;
        if (long.TryParse(dataRow[str1].ToString(), out result))
          longList.Add(result);
      }
      return longList;
    }
  }

  public AutoSelectionNodeItemImbase(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._imbaseCatalogId = new AS_Long();
    this._imbaseObjectId = new AS_Long();
    this._tableInfo = new AutoSelectionTableInfo();
    this.InitializeData();
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_36")]
  [TypeConverter(typeof (SelectionLongObjectConverter))]
  [ReadOnly(true)]
  public AS_Long ImbaseCatalogID
  {
    get => this._imbaseCatalogId;
    set => this._imbaseCatalogId = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_37")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_38")]
  [TypeConverter(typeof (SelectionLongObjectConverter))]
  [Editor(typeof (SelectionImbaseFolder), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public AS_Long ImbaseObjectID
  {
    get => this._imbaseObjectId;
    set => this.SetImbaseObjectID(value, true);
  }

  [Browsable(false)]
  public AutoSelectionTableInfo TableInfo => this._tableInfo;

  protected internal override void CollectLinks(
    Dictionary<long, int> id2Types,
    Dictionary<Guid, int> objGuid2Types)
  {
    if (this.ImbaseObjectID == null || this.ImbaseObjectID.Value == 0L || id2Types.ContainsKey(this.ImbaseObjectID.Value))
      return;
    id2Types.Add(this.ImbaseObjectID.Value, MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseRootObjectTypeGUID));
  }

  protected internal override void UpdateLinks(
    Dictionary<long, string> id2Caption,
    Dictionary<Guid, string> guid2Caption)
  {
    if (this.ImbaseObjectID == null || !id2Caption.ContainsKey(this.ImbaseObjectID.Value))
      return;
    this._imbaseObjectCaption = id2Caption[this.ImbaseObjectID.Value];
  }

  protected override string GetShortInfo()
  {
    return this._imbaseObjectCaption != string.Empty ? $"{this.Name}:{this._imbaseObjectCaption}" : base.GetShortInfo();
  }

  public override string ToString()
  {
    if (!(this._imbaseObjectCaption != string.Empty))
      return base.ToString();
    return $"{EnumDescConverter.GetEnumDescription((Enum) this.Type)}({this.Name}:{this._imbaseObjectCaption})";
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    XmlAttribute attribute1 = doc.CreateAttribute("ImbaseCatalogID");
    attribute1.Value = this._imbaseCatalogId.ToString();
    xmlNode.Attributes.Append(attribute1);
    XmlAttribute attribute2 = doc.CreateAttribute("ImbaseObjectID");
    attribute2.Value = this._imbaseObjectId.ToString();
    xmlNode.Attributes.Append(attribute2);
    XmlAttribute attribute3 = doc.CreateAttribute("ImbaseObjectCaption");
    attribute3.Value = this._imbaseObjectCaption;
    xmlNode.Attributes.Append(attribute3);
    if (this._tableInfo != null)
      xmlNode.AppendChild(this._tableInfo.SaveToXml(doc));
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node == null || base.LoadData(node) == null || node.Attributes == null)
      return (AutoSelectionNodeCommon) null;
    this._imbaseCatalogId = new AS_Long(Convert.ToInt64(node.Attributes["ImbaseCatalogID"].Value));
    this._imbaseObjectId = new AS_Long(Convert.ToInt64(node.Attributes["ImbaseObjectID"].Value));
    XmlAttribute attribute = node.Attributes["ImbaseObjectCaption"];
    if (attribute != null)
      this._imbaseObjectCaption = attribute.Value;
    this._tableInfo = AutoSelectionTableInfo.LoadFromXml(node);
    return (AutoSelectionNodeCommon) this;
  }

  protected internal override IList<AutoSelectionObject> CreateObject(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    if (selectionObject == null)
      throw new ArgumentNullException(nameof (selectionObject));
    if (!(selectionObject.Value is AutoSelectionImbaseLink selectionImbaseLink))
      return (IList<AutoSelectionObject>) null;
    if (selectionImbaseLink.FolderID == 0L)
      return (IList<AutoSelectionObject>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseServer imbaseServerService = AutoSelectionUtils.ServiceKeeper.GetImbaseServerService(session);
      ImbaseObjCreateInfo objectCreationInfo = asSession.GetImbaseObjectCreationInfo(selectionImbaseLink.FolderID, session);
      if (session.GetObjectInfo(selectionImbaseLink.FolderID).Empty)
        return (IList<AutoSelectionObject>) null;
      long objectID = imbaseServerService.CreateObject(session.SessionGUID, selectionImbaseLink.CatalogID, selectionImbaseLink.FolderID, selectionImbaseLink.TableRecID, false, objectCreationInfo.ObjectType);
      if (objectID == 0L)
        return (IList<AutoSelectionObject>) null;
      IDBObject dbObject = session.GetObject(objectID, false);
      if (dbObject == null)
        return (IList<AutoSelectionObject>) null;
      this.AttributesObjectSetDefault(asSession, dbObject, this.DefObjAttrList);
      AutoSelectionObject autoSelectionObject = (AutoSelectionObject) selectionObject.Clone();
      autoSelectionObject.CreatedObjInfo = new ObjInfoItem(dbObject);
      this.AttributesCalc(asSession, (IDBAttributable) dbObject, this.CalcObjectAttrList);
      return (IList<AutoSelectionObject>) new AutoSelectionObject[1]
      {
        autoSelectionObject
      };
    }
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied)
      return selExecuteStatus;
    if (this.ImbaseObjectID.Value == 0L)
    {
      string data = Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_90");
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
      return selExecuteStatus;
    }
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(this.ImbaseObjectID.Value);
    if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableTypeID || objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      List<long> longList = this.ExecuteDataTable(asSession);
      if (longList.Count > 0)
      {
        foreach (long tableRecId in longList)
        {
          AutoSelectionObject prototypeSelectionObject = new AutoSelectionObject((AutoSelectionNodeCommon) this, (object) new AutoSelectionImbaseLink(-1L, this.ImbaseObjectID.Value, tableRecId));
          if (this.AnalyzeObject(asSession, logRec))
          {
            if (asSession.TestMode)
            {
              asSession.CreatedObjectList.Add(prototypeSelectionObject);
              selExecuteStatus = AutoSelExecuteStatus.Applied;
            }
            else
            {
              IList<AutoSelectionObject> createdSelectionObjects;
              if (this.CreateSelectionObject(asSession, prototypeSelectionObject, out createdSelectionObjects) && createdSelectionObjects != null)
              {
                foreach (AutoSelectionObject asObject in (IEnumerable<AutoSelectionObject>) createdSelectionObjects)
                {
                  this.CreatedSelectionObject_Edit(asSession, asObject);
                  asSession.CreatedObjectList.Add(asObject);
                  this.CreatedSelectionObject_RunAutoSelection(asSession, asObject);
                }
                selExecuteStatus = AutoSelExecuteStatus.Applied;
              }
            }
          }
        }
      }
      else
        selExecuteStatus = this.MandatoryMode == AutoSelectionMandatoryMode.Mandatory ? AutoSelExecuteStatus.SkipOwnerLevel : AutoSelExecuteStatus.Skipped;
      string data = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_78"), (object) objectInfo.Caption, (object) longList.Count);
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
    }
    else
    {
      AutoSelectionObject prototypeSelectionObject = new AutoSelectionObject((AutoSelectionNodeCommon) this, (object) new AutoSelectionImbaseLink(-1L, this.ImbaseObjectID.Value, -1L));
      if (!this.AnalyzeObject(asSession, logRec))
        return selExecuteStatus;
      if (asSession.TestMode)
      {
        asSession.CreatedObjectList.Add(prototypeSelectionObject);
        return AutoSelExecuteStatus.Applied;
      }
      IList<AutoSelectionObject> createdSelectionObjects;
      if (!this.CreateSelectionObject(asSession, prototypeSelectionObject, out createdSelectionObjects) || createdSelectionObjects == null)
        return selExecuteStatus;
      foreach (AutoSelectionObject asObject in (IEnumerable<AutoSelectionObject>) createdSelectionObjects)
      {
        this.CreatedSelectionObject_Edit(asSession, asObject);
        asSession.CreatedObjectList.Add(asObject);
        this.CreatedSelectionObject_RunAutoSelection(asSession, asObject);
      }
      selExecuteStatus = AutoSelExecuteStatus.Applied;
    }
    return selExecuteStatus;
  }

  public void SetImbaseObjectID(AS_Long value, bool updateLinkMode)
  {
    if (object.Equals((object) this._imbaseObjectId, (object) value))
      return;
    this._imbaseObjectId = value;
    this._tableInfo.Clear();
    if (!updateLinkMode)
      return;
    AutoSelectionUtils.Common.UpdateNodesLinkCaptions(new List<AutoSelectionNodeBase>(1)
    {
      (AutoSelectionNodeBase) this
    });
  }
}
