// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Wizards.TableMixWizard.ImbaseTableMixCreator
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Wizards.TableMixWizard;

internal class ImbaseTableMixCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return -1;
  }

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return false;
  }

  public bool AfterCreate(long newObjectID) => true;

  public IDictionary<ObjectCreatePages, bool> VisiblePages { get; } = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>()
  {
    {
      ObjectCreatePages.Properties,
      true
    },
    {
      ObjectCreatePages.Template,
      true
    }
  };

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    nea.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", newObjectID));
    return true;
  }

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject)
  {
    DataSet dataSet = TableLoadHelper.CreateDataSet();
    DataTable table1 = dataSet.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = dataSet.Tables["IMS_DATA"];
    if (table1 == null || table2 == null)
      return false;
    IDBAttributeType attributeType1 = session.GetAttributeType(Intermech.Imbase.Consts.LinkToCompoundObjectAttID);
    DataRow dataRow1 = table1.NewRow();
    this.SetAttrTypesRowParams(dataRow1, attributeType1.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType1.UniqueMode, attributeType1.DefaultValue, attributeType1.Options, attributeType1.Mask, string.Empty);
    table1.Rows.Add(dataRow1);
    TableLoadHelper.CreateDataColumn(table2, attributeType1);
    IDBAttributeType attributeType2 = session.GetAttributeType(Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttID);
    DataRow dataRow2 = table1.NewRow();
    this.SetAttrTypesRowParams(dataRow2, attributeType2.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType2.UniqueMode, attributeType2.DefaultValue, attributeType2.Options, attributeType2.Mask, string.Empty);
    table1.Rows.Add(dataRow2);
    TableLoadHelper.CreateDataColumn(table2, attributeType2);
    IDBAttributeType attributeType3 = session.GetAttributeType(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"));
    DataRow dataRow3 = table1.NewRow();
    this.SetAttrTypesRowParams(dataRow3, attributeType3.GUID, RequiredModes.AutoRequired, ComputeValueModes.NotComputableValue, (string) null, attributeType3.UniqueMode, attributeType3.DefaultValue, attributeType3.Options, attributeType3.Mask, string.Empty);
    table1.Rows.Add(dataRow3);
    table2.Columns.Add(attributeType3.GUID.ToString(), AttributesTypeHelper.GetTypeOfAttributeValue(FieldTypes.ftMeasured));
    table1.AcceptChanges();
    table2.AcceptChanges();
    TableLoadHelper.StoreData(session, newObject.ObjectID, dataSet, (ITablesIndexer) null);
    return true;
  }

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }

  private void SetAttrTypesRowParams(
    DataRow newRow,
    Guid attrGuid,
    RequiredModes addMode,
    ComputeValueModes computeMode,
    string formula,
    UniqueValueModes uniqueMode,
    object defaultValue,
    AttributeOptions options,
    string mask,
    string measureGuid)
  {
    newRow["F_ATTRIBUTE_GUID"] = (object) attrGuid.ToString();
    newRow["F_REQUIRED"] = (object) (int) addMode;
    newRow["F_COMPUTED"] = (object) (int) computeMode;
    newRow["F_FORMULA"] = (object) formula;
    newRow["F_UNIQUE"] = (object) (int) uniqueMode;
    newRow["F_DEFAULT_VALUE"] = (object) Convert.ToString(defaultValue);
    newRow["F_OPTIONS"] = (object) (int) options;
    newRow["F_MASK"] = (object) mask;
    newRow["F_UNITS"] = (object) measureGuid;
    newRow["F_DISPLAY"] = (object) string.Empty;
  }
}
