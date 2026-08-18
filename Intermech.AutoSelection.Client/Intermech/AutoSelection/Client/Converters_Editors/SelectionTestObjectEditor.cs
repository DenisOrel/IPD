// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionTestObjectEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Protection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionTestObjectEditor : UITypeEditor
{
  private object SelectSimpleObject(AutoSelectionNodeTest node, object value)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    return !(SelectionWindow.Select("", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(node.ObjectType.Value)), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0 ? value : (object) new AS_Long(dbObjectIdArray[0].Value);
  }

  private object SelectImbaseObject(AutoSelectionNodeTest node, object value)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    long objectID = node.ImbaseObjectID.Value;
    if (objectID == 0L)
      return value;
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>() { objectID };
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      ColumnDescriptor[] columns = new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objectID, Intermech.Imbase.Consts.ClassifFolderKeyAttId);
      string conditionValue = objectAttributeById == null || objectAttributeById.Value == DBNull.Value ? string.Empty : objectAttributeById.Value.ToString();
      if (conditionValue != string.Empty)
      {
        conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) conditionValue, LogicalOperators.NONE, 0, false));
        DataTable objectData = DataHelper.GetObjectData(Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS, sessionKeeper.Session, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columns);
        if (objectData != null)
          longList.AddRange((IEnumerable<long>) objectData.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[0]))));
      }
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(node.ObjectType.Value);
      int attributeId = MetaDataHelper.GetAttributeID((object) AutosSelectConsts.ImbaseObjectLinkAttrGuid.ToString());
      conditionStructureList.Clear();
      conditionStructureList.Add(new ConditionStructure(attributeId, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID));
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns)
      {
        TableName = "f",
        FailIfNotFound = false
      };
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long result1;
        int result2;
        if (long.TryParse(Convert.ToString(row[-2.ToString()]), out result1) && int.TryParse(Convert.ToString(row[-7.ToString()]), out result2))
          objInfoList.Add(new ObjInfoItem(result1, result2));
      }
    }
    if (objInfoList.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_703.ssp_automatch_704()), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return value;
    }
    Guid guid = new Guid("{5561D10D-ACCC-4262-836E-3563319B9947}");
    IGuidMapper service1 = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true);
    int num1 = service1.Register(guid);
    try
    {
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, true);
      service2.AddNodeType(num1, typeof (ObjectsListNode));
      service2.AddViewsProvider(num1, (IViewsProvider) new AdvObjectsPropertiesProvider());
      List<int> objectTypes = ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList);
      List<long> objectIds = ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList);
      GenericListHelper.MakeUnique<int>(objectTypes);
      int parentObjectTypeId = objectTypes[0];
      if (objectTypes.Count > 1)
      {
        for (int index = 1; index < objectTypes.Count - 1; ++index)
          parentObjectTypeId = MetaDataHelper.GetCommonParentObjectTypeID(parentObjectTypeId, objectTypes[index]);
      }
      return !(SelectionWindow.Select("", (IDescriptor) new ListDescriptor(num1, parentObjectTypeId, LocalizationHolder.rm.GetString(sc_703.ssp_automatch_705()), (IList) objectIds), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray) ? value : (object) new AS_Long(dbObjectIdArray[0].Value);
    }
    finally
    {
      service1.Unregister(num1);
    }
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is AS_Long) || context == null || context.Instance == null)
      return value;
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (!(context.Instance is AutoSelectionNodeTest instance))
      return value;
    if (instance.ObjectType.Value == Guid.Empty)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_703.ssp_automatch_706()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    return instance.ImbaseObjectID.Value == 0L ? this.SelectSimpleObject(instance, value) : this.SelectImbaseObject(instance, value);
  }
}
