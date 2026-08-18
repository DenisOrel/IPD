
// Type: Intermech.Navigator.Snapshots.ShapshotDescriptorHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;


namespace Intermech.Navigator.Snapshots;

/// <summary>
/// для отображения свойств итерации
/// (DBObjectSnapshot не реализует IDBAttributable =&gt; не могу использовать ObjectPropertyGrid)
/// </summary>
public class ShapshotDescriptorHolder : PropDescriptorHolder, IElementInfoEx, IElementInfo
{
  /// <summary>это атрибуты итерации</summary>
  private DataTable attrTable;
  /// <summary>идентификатор объекта в данной итерации</summary>
  private long objectID;
  private int objectType;
  /// <summary>
  /// элемент навигации.
  /// содержит системные атрибуты для итерации
  /// </summary>
  private SnapshotsNodeID nodeID;
  private List<int> lockedAttrsList;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="attrTable"></param>
  public ShapshotDescriptorHolder(SnapshotsNodeID nodeID, DataTable attrTable)
  {
    this.attrTable = attrTable;
    this.objectID = nodeID.ObjectID;
    this.objectType = nodeID.TypeID;
    this.nodeID = nodeID;
  }

  /// <summary>Создаём дескрипторы найденных атрибутов</summary>
  /// <param name="pdc"></param>
  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    List<int> intList = new List<int>();
    this.attrTable.DefaultView.Sort = "F_ATTRIBUTE_ID";
    DataRow[] dataRowArray;
    for (int index1 = this.attrTable.Rows.Count - 1; index1 >= 0; index1 -= dataRowArray.Length)
    {
      int int32 = Convert.ToInt32(this.attrTable.Rows[index1]["F_ATTRIBUTE_ID"]);
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(int32, false);
      dataRowArray = this.attrTable.Select("F_ATTRIBUTE_ID=" + int32.ToString());
      if (attributeType != null && attributeType.AttributeType != FieldTypes.ftBlob && attributeType.AttributeType != FieldTypes.ftFile && attributeType.AttributeType != FieldTypes.ftShortBlob)
      {
        Intermech.Interfaces.AttributeValues aAttributeValues = new Intermech.Interfaces.AttributeValues(int32, attributeType.AttributeType, attributeType.PropertiesStructure.MultiValueMode, attributeType.Computed);
        aAttributeValues.AttributeAlias = attributeType.Alias;
        aAttributeValues.AttributeGuid = attributeType.PropertiesStructure.AttributeGuid;
        aAttributeValues.AttributeName = attributeType.Name;
        aAttributeValues.ReadOnly = true;
        int[] groupsList = attributeType.GetGroupsList();
        if (groupsList.Length != 0)
        {
          IMSAttributeGroup attributeGroup = MetaDataHelper.GetAttributeGroup(groupsList[0]);
          if (attributeGroup != null)
            aAttributeValues.GroupName = attributeGroup.Name;
        }
        aAttributeValues.Values = new object[dataRowArray.Length];
        aAttributeValues.Descriptions = new object[dataRowArray.Length];
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          DataRow curRow = dataRowArray[index2];
          aAttributeValues.Values[index2] = this.AttributeValues(curRow, attributeType.AttributeType, attributeType.Computed, TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now));
          aAttributeValues.Descriptions[index2] = (object) this.AttributeDescriptions(aAttributeValues.Values[index2], curRow, attributeType.AttributeType);
        }
        this.AddAttribute(aAttributeValues);
      }
    }
    this.AddSystemAttributes(pdc);
  }

  private object AttributeValues(
    DataRow curRow,
    FieldTypes attrType,
    ComputeValueModes computeType,
    TimeSpan offset)
  {
    switch (attrType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftPassword:
      case FieldTypes.ftMeasured:
      case FieldTypes.ftGuid:
        if (curRow["F_STRING_VALUE"] == DBNull.Value)
          return (object) DBNull.Value;
        object obj1 = curRow["F_STRING_VALUE"];
        return obj1 == DBNull.Value || obj1 == null ? (object) string.Empty : (object) obj1.ToString();
      case FieldTypes.ftInteger:
        object obj2 = curRow["F_INTEGER_VALUE"];
        return obj2 == DBNull.Value || obj2 == null ? (object) 0 : (object) Convert.ToInt64(obj2);
      case FieldTypes.ftDouble:
        object obj3 = curRow["F_DOUBLE_VALUE"];
        return obj3 == DBNull.Value || obj3 == null ? (object) 0 : (object) Math.Round(Convert.ToDouble(obj3), Intermech.Consts.MaxPrecision);
      case FieldTypes.ftDateTime:
        object obj4 = curRow["F_DATE_VALUE"];
        if (obj4 == DBNull.Value || obj4 == null)
          return (object) DateTime.MinValue;
        DateTime dateTime = Convert.ToDateTime(obj4);
        if (computeType != ComputeValueModes.JITValue)
        {
          try
          {
            dateTime += offset;
          }
          catch
          {
          }
        }
        return (object) dateTime;
      case FieldTypes.ftExternalLink:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        if (curRow["F_INTEGER_VALUE"] == DBNull.Value || curRow["F_INTEGER_VALUE"] == null)
          return (object) DBNull.Value;
        object obj5 = curRow["F_INTEGER_VALUE"];
        return obj5 == DBNull.Value || obj5 == null ? (object) 0 : (object) Convert.ToInt64(obj5);
      case FieldTypes.ftMemo:
        object obj6 = curRow["F_STRING_VALUE"];
        return obj6 == DBNull.Value || obj6 == null ? (object) string.Empty : (object) obj6.ToString();
      case FieldTypes.ftBoolean:
        object obj7 = curRow["F_INTEGER_VALUE"];
        return (object) (bool) (obj7 == DBNull.Value ? 0 : (Convert.ToBoolean(obj7) ? 1 : 0));
      case FieldTypes.ftAutoInc:
        return (object) Convert.ToInt64(curRow["F_INTEGER_VALUE"]);
      default:
        return (object) null;
    }
  }

  private string AttributeDescriptions(object value, DataRow curRow, FieldTypes attrType)
  {
    if (attrType == FieldTypes.ftObjectLink || attrType == FieldTypes.ftObjectLinkByID)
    {
      object obj = curRow["F_STRING_VALUE"];
      return obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
    }
    return value != null ? value.ToString() : string.Empty;
  }

  /// <summary>
  /// добавляем в грид системные атрибуты
  /// и обязательны параметры итерации
  /// </summary>
  /// <param name="pdc"></param>
  /// <returns></returns>
  private void AddSystemAttributes(PropertyDescriptorCollection pdc)
  {
    Intermech.Interfaces.AttributeValues aAttributeValues = new Intermech.Interfaces.AttributeValues(0);
    aAttributeValues.ReadOnly = true;
    aAttributeValues.GroupName = LocalizationHolder.rm.GetString("Client.Core_1402");
    aAttributeValues.AttributeType = FieldTypes.ftSystem;
    aAttributeValues.AttributeID = -2;
    aAttributeValues.AttributeName = ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_ID);
    aAttributeValues.Values = new object[1]
    {
      (object) this.nodeID.ObjectID
    };
    this.AddAttribute(aAttributeValues);
    aAttributeValues.AttributeID = -3;
    aAttributeValues.AttributeName = ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ID);
    aAttributeValues.Values = new object[1]
    {
      (object) this.nodeID.ID
    };
    this.AddAttribute(aAttributeValues);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.nodeID.userID, false);
      aAttributeValues.GroupName = LocalizationHolder.rm.GetString("Client.Core_1403");
      if (dbObject != null)
      {
        aAttributeValues.AttributeID = -36;
        aAttributeValues.AttributeName = LocalizationHolder.rm.GetString("Client.Core_1404");
        aAttributeValues.Values = new object[1]
        {
          (object) dbObject.Caption
        };
        this.AddAttribute(aAttributeValues);
      }
    }
    aAttributeValues.AttributeID = SnapshotConsts.SNAPSHOT_ID;
    aAttributeValues.AttributeName = DataSetProcessor.GetCaption("F_SNAPSHOT_ID");
    this._pdc.Add((PropertyDescriptor) new PropDescriptor(aAttributeValues.AttributeID, (object) this, aAttributeValues.AttributeName, (object) this.nodeID.SnapshotID, typeof (string), (TypeConverter) null, (object) null, aAttributeValues.GroupName, string.Empty, true, true, true));
    aAttributeValues.AttributeID = SnapshotConsts.SNAPSHOT_DATE;
    aAttributeValues.AttributeName = DataSetProcessor.GetCaption("F_SNAPSHOT_DATE");
    this._pdc.Add((PropertyDescriptor) new PropDescriptor(aAttributeValues.AttributeID, (object) this, aAttributeValues.AttributeName, (object) this.nodeID.snapDate, typeof (string), (TypeConverter) null, (object) null, aAttributeValues.GroupName, string.Empty, true, true, true));
    aAttributeValues.AttributeID = -38;
    aAttributeValues.AttributeName = ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_NOTE);
    aAttributeValues.Values = new object[1]
    {
      (object) this.nodeID.name
    };
    this.AddAttribute(aAttributeValues);
  }

  private void AddAttribute(Intermech.Interfaces.AttributeValues aAttributeValues)
  {
    int id = 0;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    Type type = (Type) null;
    TypeConverter typeConverter = (TypeConverter) null;
    object editor = (object) null;
    bool ro = true;
    bool reset = false;
    string empty4 = string.Empty;
    bool disableManualEdit = true;
    if (!AttributeValuesEditor.GetPDAttributes((object) this, aAttributeValues, ref id, ref empty1, ref empty2, ref empty3, ref type, ref typeConverter, ref editor, ref ro, ref reset, ref empty4, ref disableManualEdit))
      return;
    DataTable possibleValues = (DataTable) null;
    if (MultiValueModesHelper.IsValuedFromList(aAttributeValues.MultipleValued))
      possibleValues = ClientCommons.GetPossibleValues(aAttributeValues.AttributeID);
    PropDescriptor propDescriptor = (PropDescriptor) new SimplePropDescriptor(id, (object) this, empty1, AttributeValuesEditor.GetPDValue(aAttributeValues, 0, 0L, AttributableElements.Snapshot, empty4, possibleValues), type, typeConverter, editor, empty3, empty2, true, true, reset, empty4, true, new AttributeValuesPropertyClass(aAttributeValues));
    if (propDescriptor == null)
      return;
    this._pdc.Add((PropertyDescriptor) propDescriptor);
  }

  /// <summary>
  /// Идентификатор элемента.
  /// У нас это объект - ид версии объекта
  /// </summary>
  public long ElementIdentifier => this.objectID;

  /// <summary>Тип элемента.</summary>
  public AttributableElements ElementKind => AttributableElements.Object;

  public int ElementType => this.objectType;

  public bool CheckAttributeLock(int attrId)
  {
    bool flag = false;
    if (this.lockedAttrsList == null && ServicesManager.ServiceContainer.GetService(typeof (IAttributesLockService)) is IAttributesLockService service)
      this.lockedAttrsList = new List<int>((IEnumerable<int>) service.GetLockedAttributes(this.ElementKind, this.ElementIdentifier, this.ElementType));
    if (this.lockedAttrsList != null)
      flag = this.lockedAttrsList.IndexOf(attrId) != -1;
    return flag;
  }
}
