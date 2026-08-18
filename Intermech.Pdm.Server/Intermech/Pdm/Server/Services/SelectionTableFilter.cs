// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.SelectionTableFilter
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

#nullable disable
namespace Intermech.Pdm.Server.Services;

internal sealed class SelectionTableFilter
{
  private readonly List<int> _addedColsIdxs = new List<int>();
  private ConditionStructure[] _condsSelection;
  private bool _relationalConditionsPresent;
  private readonly List<int> _measureConditions = new List<int>();
  private readonly List<Tuple<int, AttributeSourceTypes, int, Type>> _conditionIndexes = new List<Tuple<int, AttributeSourceTypes, int, Type>>();

  public void BeforeSelectComposition(
    IUserSession session,
    List<ColumnDescriptor> queryColumns,
    long selectionID,
    List<ConditionStructure> filterConditions)
  {
    List<Tuple<IDBAttributeType, AttributeSourceTypes>> tupleList = new List<Tuple<IDBAttributeType, AttributeSourceTypes>>();
    if (selectionID != 0L || filterConditions != null)
    {
      ISelectionsService service = ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService;
      this._condsSelection = filterConditions != null ? filterConditions.ToArray() : service.GetConditionStructures((object) session, selectionID);
      if (this._condsSelection.Length != 0)
      {
        for (int index = 0; index < this._condsSelection.Length; ++index)
        {
          IDBAttributeType t = (IDBAttributeType) null;
          ConditionStructure cs = this._condsSelection[index];
          if (cs.Attribute is Guid)
            t = session.GetAttributeType((Guid) cs.Attribute);
          if (cs.Attribute is int)
            t = session.GetAttributeType((int) cs.Attribute);
          if (t != null)
          {
            cs.Attribute = t.MultipleValued != MultiValueModes.MultiValues && t.MultipleValued != MultiValueModes.MultiValuesFromList ? (object) t.AttributeID : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_53"), (object) t.Name));
            if (!tupleList.Exists((Predicate<Tuple<IDBAttributeType, AttributeSourceTypes>>) (x => x.Item1.AttributeID.Equals(t.AttributeID) && x.Item2.Equals((object) cs.AttributeSource))))
              tupleList.Add(new Tuple<IDBAttributeType, AttributeSourceTypes>(t, cs.AttributeSource));
          }
          else
          {
            if (cs.RelationalOperator != RelationalOperators.ConsistFromType && cs.RelationalOperator != RelationalOperators.NotConsistFromType && cs.RelationalOperator != RelationalOperators.EntersInType && cs.RelationalOperator != RelationalOperators.NotEntersInType && cs.RelationalOperator != RelationalOperators.ConsistFrom && cs.RelationalOperator != RelationalOperators.EntersIn)
              throw new KernelExceptionID(234, (object) EnumDescConverter.GetEnumDescription((Enum) this._condsSelection[index].RelationalOperator));
            this._relationalConditionsPresent = true;
          }
        }
      }
    }
    foreach (Tuple<IDBAttributeType, AttributeSourceTypes> tuple in tupleList)
    {
      Tuple<IDBAttributeType, AttributeSourceTypes> selectionAttribute = tuple;
      int num = -1;
      if (selectionAttribute.Item1.AttributeType == FieldTypes.ftObjectLink)
      {
        int index = queryColumns.FindIndex((Predicate<ColumnDescriptor>) (x => x.AttributeID is int attributeId && attributeId == selectionAttribute.Item1.AttributeID));
        if (index < 0)
          this.AddSpecialColumn(queryColumns, selectionAttribute, ColumnContents.Text, typeof (string));
        else
          this._conditionIndexes.Add(new Tuple<int, AttributeSourceTypes, int, Type>(selectionAttribute.Item1.AttributeID, selectionAttribute.Item2, index, typeof (string)));
        this.AddSpecialColumn(queryColumns, selectionAttribute, ColumnContents.ID, typeof (long));
      }
      else
      {
        num = queryColumns.FindIndex((Predicate<ColumnDescriptor>) (x => x.AttributeID.Equals((object) selectionAttribute.Item1.AttributeID)));
        if (num < 0)
          this.AddSpecialColumn(queryColumns, selectionAttribute, ColumnContents.Text, typeof (string));
      }
      if (num >= 0)
      {
        if (selectionAttribute.Item1.AttributeType == FieldTypes.ftMeasured)
          this._measureConditions.Add(num);
        this._conditionIndexes.Add(new Tuple<int, AttributeSourceTypes, int, Type>(selectionAttribute.Item1.AttributeID, selectionAttribute.Item2, num, AttributesTypeHelper.GetRDBMSTypeOfAttributeValue(selectionAttribute.Item1.AttributeID)));
      }
    }
  }

  private void AddSpecialColumn(
    List<ColumnDescriptor> queryColumns,
    Tuple<IDBAttributeType, AttributeSourceTypes> selectionAttribute,
    ColumnContents contents,
    Type type)
  {
    queryColumns.Add(new ColumnDescriptor((object) selectionAttribute.Item1.AttributeID, AttributeSourceTypes.Object, contents, ColumnNameMapping.Index, SortOrders.NONE, 0));
    int num = queryColumns.Count - 1;
    this._conditionIndexes.Add(new Tuple<int, AttributeSourceTypes, int, Type>(selectionAttribute.Item1.AttributeID, selectionAttribute.Item2, num, type));
    this._addedColsIdxs.Add(num);
  }

  public DataTable FilterTable(
    IUserSession session,
    DataTable resultTable,
    List<ColumnDescriptor> queryColumns,
    List<int> enabledObjectTypes)
  {
    if (this._relationalConditionsPresent)
    {
      List<long> longList1 = new List<long>(resultTable.Rows.Count);
      int index1 = queryColumns.FindIndex((Predicate<ColumnDescriptor>) (x => x.GetAttributeID(false) == -2));
      for (int index2 = 0; index2 < resultTable.Rows.Count; ++index2)
      {
        long int64 = Convert.ToInt64(resultTable.Rows[index2][index1]);
        if (longList1.IndexOf(int64) < 0)
          longList1.Add(int64);
      }
      ConditionStructure[] conditions = ConditionStructure.Join(new ConditionStructure(-2, RelationalOperators.In, (object) longList1.ToArray(), LogicalOperators.AND, 0, false), this._condsSelection);
      List<int> intList = new List<int>();
      if (enabledObjectTypes != null && enabledObjectTypes.Count > 0)
      {
        if (enabledObjectTypes.Count == 1)
        {
          int enabledObjectType = enabledObjectTypes[0];
          if (!MetaDataHelper.IsLocalObjectType(enabledObjectType))
            intList = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(enabledObjectType);
          else
            intList.Add(enabledObjectType);
        }
        else
        {
          for (int index3 = 0; index3 < enabledObjectTypes.Count; ++index3)
          {
            if (MetaDataHelper.IsLocalObjectType(enabledObjectTypes[index3]))
            {
              if (intList.IndexOf(enabledObjectTypes[index3]) < 0)
                intList.Add(enabledObjectTypes[index3]);
            }
            else
            {
              List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(enabledObjectTypes[index3]);
              for (int index4 = 0; index4 < childrenIdRecursive.Count; ++index4)
              {
                if (intList.IndexOf(childrenIdRecursive[index4]) < 0)
                  intList.Add(childrenIdRecursive[index4]);
              }
            }
          }
        }
      }
      List<long> longList2 = new List<long>();
      for (int index5 = 0; index5 < intList.Count; ++index5)
      {
        DataTable dataTable = session.GetObjectCollection(intList[index5]).SelectWithLocalObjects(new DBRecordSetParams(conditions, new object[1]
        {
          (object) -2
        }, new object[1]{ (object) -2 }, new SortOrders[1]
        {
          SortOrders.ASC
        }));
        for (int index6 = 0; index6 < dataTable.Rows.Count; ++index6)
          longList2.Add(Convert.ToInt64(dataTable.Rows[index6][0]));
      }
      DataTable toTable = resultTable.Clone();
      for (int index7 = 0; index7 < resultTable.Rows.Count; ++index7)
      {
        long int64 = Convert.ToInt64(resultTable.Rows[index7][index1]);
        if (longList2.IndexOf(int64) >= 0)
          DataSetProcessor.AddRow(toTable, resultTable.Rows[index7], false);
      }
      toTable.AcceptChanges();
      resultTable = toTable;
    }
    else
    {
      if (this._conditionIndexes.Count == 0)
        return resultTable;
      if (this._measureConditions.Count > 0)
      {
        foreach (int measureCondition in this._measureConditions)
        {
          resultTable.Columns.Add(new DataColumn($"{measureCondition}_BS", typeof (double)));
          this._addedColsIdxs.Add(resultTable.Columns.Count - 1);
          resultTable.Columns.Add(new DataColumn($"{measureCondition}_MU", typeof (long)));
          this._addedColsIdxs.Add(resultTable.Columns.Count - 1);
          foreach (DataRow row in (InternalDataCollectionBase) resultTable.Rows)
          {
            object obj = row[measureCondition];
            if (obj != null && obj != DBNull.Value && obj.ToString() != string.Empty)
            {
              MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(obj.ToString());
              MeasuredValue baseMeasure = MeasureHelper.ConvertToBaseMeasure(measuredValue);
              row[$"{measureCondition}_BS"] = (object) baseMeasure.Value;
              row[$"{measureCondition}_MU"] = (object) measuredValue.MeasureID;
            }
          }
        }
        resultTable.AcceptChanges();
      }
    }
    resultTable = DataSetProcessor.GetRowsByFilter(this.ConstructFilter(session, this._condsSelection), resultTable);
    if (this._addedColsIdxs.Count > 0)
    {
      foreach (string name in this._addedColsIdxs.ConvertAll<string>((Converter<int, string>) (x => resultTable.Columns[x].ColumnName)))
        resultTable.Columns.Remove(name);
    }
    return resultTable;
  }

  private IDBAttributeType GetAttributeType(object attributeID, IUserSession session)
  {
    switch (attributeID)
    {
      case null:
        return (IDBAttributeType) null;
      case string _:
        return session.GetAttributeType((string) attributeID, false);
      case Guid anAttributeGuid:
        return session.GetAttributeType(anAttributeGuid, false);
      default:
        return (IDBAttributeType) null;
    }
  }

  private string ConstructFilter(IUserSession session, ConditionStructure[] conditions)
  {
    if (conditions == null)
      return string.Empty;
    string empty1 = string.Empty;
    for (int index1 = 0; index1 < conditions.Length; ++index1)
    {
      ConditionStructure cond = conditions[index1];
      IDBAttributeType attributeType = (IDBAttributeType) null;
      if (cond.Attribute != null)
      {
        attributeType = this.GetAttributeType(cond.Attribute, session);
        if (attributeType == null)
          throw new KernelExceptionID(231, cond.Attribute);
        DateTime utcNow;
        if (cond.RelationalOperator == RelationalOperators.LastNDays && cond.Value != null)
        {
          cond.RelationalOperator = RelationalOperators.Between;
          ref ConditionStructure local1 = ref cond;
          utcNow = DateTime.UtcNow;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local2 = (System.ValueType) (utcNow.Date + TimeSpan.FromDays((double) (1L - Convert.ToInt64(cond.Value))));
          local1.Value = (object) local2;
          ref ConditionStructure local3 = ref cond;
          utcNow = DateTime.UtcNow;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> date = (System.ValueType) utcNow.Date;
          local3.Value2 = (object) date;
        }
        else if (cond.RelationalOperator == RelationalOperators.LastNDays && cond.Value != null)
        {
          cond.RelationalOperator = RelationalOperators.Between;
          ref ConditionStructure local4 = ref cond;
          utcNow = DateTime.UtcNow;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local5 = (System.ValueType) (utcNow.Date + TimeSpan.FromDays((double) (Convert.ToInt64(cond.Value) - 1L)));
          local4.Value2 = (object) local5;
          ref ConditionStructure local6 = ref cond;
          utcNow = DateTime.UtcNow;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> date = (System.ValueType) utcNow.Date;
          local6.Value = (object) date;
        }
      }
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (cond.GroupID > 0)
      {
        for (int index2 = 0; index2 < cond.GroupID; ++index2)
          empty2 += "(";
      }
      string str1 = empty2 + "(";
      string format = RelationalOperatorsHelper.SQLOperator(cond.RelationalOperator);
      if (attributeType == null || format == "")
      {
        str1 += "1=1";
      }
      else
      {
        FieldTypes fieldType = attributeType.AttributeType;
        if (fieldType == FieldTypes.ftSystem)
          fieldType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID);
        List<Tuple<int, AttributeSourceTypes, int, Type>> all = this._conditionIndexes.FindAll((Predicate<Tuple<int, AttributeSourceTypes, int, Type>>) (x => x.Item1.Equals(attributeType.AttributeID) && x.Item2.Equals((object) cond.AttributeSource)));
        object value = cond.Value is IList list1 ? list1[0] : cond.Value;
        Tuple<int, AttributeSourceTypes, int, Type> tuple = all.Count <= 1 || value == null ? all[0] : all.Find((Predicate<Tuple<int, AttributeSourceTypes, int, Type>>) (x => x.Item4.Equals(value.GetType())));
        string str2 = cond.CaseSensitive || fieldType != FieldTypes.ftString && fieldType != FieldTypes.ftGuid && fieldType != FieldTypes.ftFile ? (fieldType != FieldTypes.ftMeasured ? empty3 + $"[{tuple.Item3}]" : empty3 + $"[{tuple.Item3}_BS]") : empty3 + $"UPPER([{tuple.Item3}])";
        if (cond.RelationalOperator == RelationalOperators.Between)
          format = $" >= {{0}} AND {str2} <= {{1}}";
        else if (cond.RelationalOperator == RelationalOperators.NotBetween)
          format = $" < {{0}} OR {str2} > {{1}}";
        object obj1 = (object) null;
        switch (fieldType)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftDateTime:
          case FieldTypes.ftFile:
          case FieldTypes.ftGuid:
            object obj2;
            if ((cond.RelationalOperator == RelationalOperators.In || cond.RelationalOperator == RelationalOperators.NotIn) && cond.Value is IList list2)
            {
              obj2 = (object) string.Empty;
              bool flag = false;
              for (int index3 = 0; index3 < list2.Count; ++index3)
              {
                object obj3 = list2[index3] != null ? (object) $"'{Convert.ToString(list2[index3])}'" : list2[index3];
                if (!cond.CaseSensitive && obj3 != null)
                  obj3 = (object) ((string) obj3).ToUpper();
                if (obj3 != null)
                {
                  if (flag)
                    obj2 = (object) (obj2.ToString() + ",");
                  obj2 = (object) (obj2.ToString() + (string) obj3);
                }
                flag = true;
              }
            }
            else
            {
              if (cond.Value == null)
              {
                obj2 = (object) null;
                if (cond.RelationalOperator == RelationalOperators.NotEmpty)
                  format = $"{format} AND {str2}<>''";
                else if (cond.RelationalOperator == RelationalOperators.Empty)
                  format = $"{format} OR {str2}=''";
              }
              else
              {
                string str3 = Convert.ToString(cond.Value);
                switch (cond.RelationalOperator)
                {
                  case RelationalOperators.Substring:
                  case RelationalOperators.NotSubstring:
                    str3 = str3 != string.Empty ? $"%{str3}%" : "%";
                    break;
                  case RelationalOperators.StartString:
                  case RelationalOperators.NotStartString:
                    str3 = str3 != string.Empty ? $"{str3}%" : "%";
                    break;
                  case RelationalOperators.EndString:
                  case RelationalOperators.NotEndString:
                    str3 = str3 != string.Empty ? $"%{str3}" : "%";
                    break;
                }
                obj2 = (object) $"'{str3}'";
                if (!cond.CaseSensitive)
                  obj2 = (object) ((string) obj2).ToUpper();
              }
              obj1 = cond.Value2 != null ? (object) $"'{Convert.ToString(cond.Value2)}'" : cond.Value2;
              if (!cond.CaseSensitive && obj1 != null)
                obj1 = (object) ((string) obj1).ToUpper();
            }
            if (cond.RelationalOperator == RelationalOperators.Between)
            {
              str1 += string.Format("{0} >= {1} AND {0} <= {2}", (object) str2, obj2, obj1);
              break;
            }
            string str4 = string.Format(format, obj2, obj1);
            str1 = str1 + str2 + str4;
            break;
          case FieldTypes.ftBoolean:
            object obj4 = cond.Value == null || (!(cond.Value is bool) || !(bool) cond.Value) && (!(cond.Value is int) || (int) cond.Value != 1) && (!(cond.Value is string) || !((string) cond.Value).ToUpper().Equals("TRUE")) ? (object) 0 : (object) 1;
            object obj5 = cond.Value2 == null || (!(cond.Value2 is bool) || !(bool) cond.Value2) && (!(cond.Value2 is int) || (int) cond.Value2 != 1) && (!(cond.Value2 is string) || !((string) cond.Value2).ToUpper().Equals("TRUE")) ? (object) 0 : (object) 1;
            string str5 = string.Format(format, obj4, obj5);
            str1 = str1 + str2 + str5;
            break;
          case FieldTypes.ftMeasured:
            double num1;
            if ((cond.RelationalOperator == RelationalOperators.In || cond.RelationalOperator == RelationalOperators.NotIn) && cond.Value is IList list3)
            {
              object obj6 = (object) string.Empty;
              bool flag = false;
              long physicalQuantityID = -1;
              long num2 = -1;
              for (int index4 = 0; index4 < list3.Count; ++index4)
              {
                object mValue = list3[index4];
                switch (mValue)
                {
                  case null:
                    if (mValue != null)
                    {
                      if (flag)
                        obj6 = (object) (obj6.ToString() + ",");
                      object obj7 = obj6;
                      num1 = ((MeasuredValue) mValue).Value;
                      string str6 = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                      obj6 = (object) (obj7.ToString() + str6);
                    }
                    flag = true;
                    continue;
                  case string _:
                  case MeasuredValue _:
                    if (mValue is string)
                      mValue = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue);
                    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor((MeasuredValue) mValue);
                    if (physicalQuantityID == -1L)
                    {
                      physicalQuantityID = descriptor.PhysicalQuantityID;
                      num2 = MeasureHelper.GetBaseMeasureID(physicalQuantityID);
                    }
                    else if (physicalQuantityID != descriptor.PhysicalQuantityID)
                      throw new KernelExceptionID(232, (object) attributeType.Name);
                    if (((MeasuredValue) mValue).MeasureID != num2)
                    {
                      mValue = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue);
                      goto case null;
                    }
                    goto case null;
                  default:
                    throw new KernelExceptionID(233, (object) mValue.ToString());
                }
              }
              string str7 = string.Format(format, obj6, obj1);
              str1 += $"({str2}{str7}) AND ([{tuple.Item3}_MU] = {num2})";
              break;
            }
            MeasureDescriptor measureDescriptor1 = (MeasureDescriptor) null;
            MeasureDescriptor measureDescriptor2 = (MeasureDescriptor) null;
            object mValue1 = cond.Value;
            switch (mValue1)
            {
              case null:
                object mValue2 = cond.Value2;
                switch (mValue2)
                {
                  case null:
                    if (measureDescriptor1 != null && measureDescriptor2 != null && measureDescriptor1.PhysicalQuantityID != measureDescriptor2.PhysicalQuantityID)
                      throw new KernelExceptionID(232, (object) attributeType.Name);
                    long baseMeasureId = measureDescriptor1 != null ? MeasureHelper.GetBaseMeasureID(measureDescriptor1.PhysicalQuantityID) : 0L;
                    if (mValue1 != null && ((MeasuredValue) mValue1).MeasureID != baseMeasureId)
                      mValue1 = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue1);
                    object obj8;
                    if (mValue1 == null)
                    {
                      obj8 = mValue1;
                    }
                    else
                    {
                      num1 = ((MeasuredValue) mValue1).Value;
                      obj8 = (object) num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                    }
                    object obj9 = obj8;
                    if (mValue2 != null && ((MeasuredValue) mValue2).MeasureID != baseMeasureId)
                      mValue2 = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue2);
                    object obj10;
                    if (mValue2 == null)
                    {
                      obj10 = mValue2;
                    }
                    else
                    {
                      num1 = ((MeasuredValue) mValue2).Value;
                      obj10 = (object) num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                    }
                    object obj11 = obj10;
                    string str8 = string.Format(format, obj9, obj11);
                    str1 = $"{str1}({str2}{str8})";
                    if (baseMeasureId != 0L)
                    {
                      str1 += $" AND ([{tuple.Item3}_MU] = {baseMeasureId})";
                      break;
                    }
                    break;
                  case string _:
                  case MeasuredValue _:
                    if (mValue2 is string)
                      mValue2 = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue2);
                    measureDescriptor2 = MeasureHelper.FindDescriptor((MeasuredValue) mValue2);
                    goto case null;
                  default:
                    throw new KernelExceptionID(233, (object) mValue2.ToString());
                }
                break;
              case string _:
              case MeasuredValue _:
                if (mValue1 is string)
                  mValue1 = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue1);
                measureDescriptor1 = MeasureHelper.FindDescriptor((MeasuredValue) mValue1);
                goto case null;
              default:
                throw new KernelExceptionID(233, (object) mValue1.ToString());
            }
          default:
            object obj12;
            if ((cond.RelationalOperator == RelationalOperators.In || cond.RelationalOperator == RelationalOperators.NotIn) && cond.Value is IList list4)
            {
              obj12 = (object) string.Empty;
              bool flag = false;
              for (int index5 = 0; index5 < list4.Count; ++index5)
              {
                string str9 = this.PrepareDefaultValue(list4[index5], fieldType);
                if (str9 != string.Empty)
                {
                  if (flag)
                    obj12 = (object) (obj12.ToString() + ",");
                  obj12 = (object) (obj12.ToString() + str9);
                }
                flag = true;
              }
            }
            else
            {
              if (cond.Value is ConditionGroupIDReplacer conditionGroupIdReplacer)
              {
                if (!MetaDataHelper.IsObjectTypeChildOf(session.GetObjectInfo(conditionGroupIdReplacer.GroupID).ObjectTypeID, session.IdentHelper.UsersTypeID))
                {
                  DataTable table = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
                  {
                    new ConditionStructure(-7, RelationalOperators.Equal, (object) session.IdentHelper.UsersTypeID, LogicalOperators.NONE, 0, false)
                  }, new object[1]{ (object) -2 }), conditionGroupIdReplacer.GroupID);
                  str1 = table.Rows.Count != 0 ? str1 + str2 + this.GetString4UserIDs(cond.RelationalOperator, table, "ConditionGroupIDReplacer") : str1 + (cond.RelationalOperator == RelationalOperators.NotEqual ? "1=1" : "1=0");
                  break;
                }
                DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545")).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
                {
                  new ConditionStructure(new Guid("cadd9233-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionGroupIdReplacer.GroupID, LogicalOperators.NONE, 0)
                }, new object[1]{ (object) -2 }));
                if (dataTable.Rows.Count == 0)
                {
                  str1 += cond.RelationalOperator == RelationalOperators.NotEqual ? "1=1" : "1=0";
                  break;
                }
                IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID);
                List<long> longList = new List<long>();
                foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
                {
                  foreach (DataRow row2 in (InternalDataCollectionBase) relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
                  {
                    new ConditionStructure(-7, RelationalOperators.Equal, (object) session.IdentHelper.UsersTypeID, LogicalOperators.NONE, 0, false)
                  }, new object[1]{ (object) -2 }), Convert.ToInt64(row1[0])).Rows)
                  {
                    long int64 = Convert.ToInt64(row2[0]);
                    if (!longList.Contains(int64))
                      longList.Add(int64);
                  }
                }
                if (longList.Count == 0)
                {
                  str1 += cond.RelationalOperator == RelationalOperators.NotEqual ? "1=1" : "1=0";
                  break;
                }
                StringBuilder stringBuilder = new StringBuilder();
                foreach (long num3 in longList)
                {
                  if (stringBuilder.Length > 0)
                    stringBuilder.Append(",");
                  stringBuilder.Append(num3);
                }
                if (cond.RelationalOperator == RelationalOperators.NotEqual)
                {
                  str1 = $"{str1}{str2} NOT IN ({stringBuilder.ToString()})";
                  break;
                }
                if (cond.RelationalOperator == RelationalOperators.Equal)
                {
                  str1 = $"{str1}{str2} IN ({stringBuilder.ToString()})";
                  break;
                }
                break;
              }
              if (cond.Value is ConditionRankIDReplacer conditionRankIdReplacer)
              {
                DataTable table = session.GetObjectCollection(session.IdentHelper.UsersTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
                {
                  new ConditionStructure(new Guid("cad00142-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionRankIdReplacer.RankID, LogicalOperators.NONE, 0)
                }, new object[1]{ (object) -2 }));
                if (table.Rows.Count == 0)
                {
                  session.GetObject(conditionRankIdReplacer.RankID, true);
                  str1 += "1=0";
                  break;
                }
                str1 = str1 + str2 + this.GetString4UserIDs(cond.RelationalOperator, table, "ConditionRankIDReplacer");
                break;
              }
              obj12 = (object) this.PrepareDefaultValue(cond.Value, fieldType);
              obj1 = (object) this.PrepareDefaultValue(cond.Value2, fieldType);
            }
            string str10 = string.Format(format, obj12, obj1);
            str1 = str1 + str2 + str10;
            break;
        }
      }
      string str11 = str1 + ")";
      if (cond.GroupID < 0)
      {
        for (int index6 = 0; index6 > cond.GroupID; --index6)
          str11 += ")";
      }
      if (index1 < conditions.Length - 1)
      {
        if (cond.LogicalOperator != LogicalOperators.AND && cond.LogicalOperator != LogicalOperators.OR)
          throw new Exception($"Невозможно сформировать строку фильтрации. Отсутствует логический оператор у условия ({str11}).");
        str11 += $" {cond.LogicalOperator} ";
      }
      empty1 += str11;
    }
    return empty1;
  }

  private string GetString4UserIDs(
    RelationalOperators relationalOperator,
    DataTable table,
    string functionName)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(",");
      stringBuilder.Append(row[0]);
    }
    if (relationalOperator == RelationalOperators.NotEqual)
      return $" NOT IN ({stringBuilder.ToString()})";
    if (relationalOperator == RelationalOperators.Equal)
      return $" IN ({stringBuilder.ToString()})";
    throw new KernelException($"В функцию {functionName} передали неверный оператор: {relationalOperator.ToString()}");
  }

  private string PrepareDefaultValue(object value, FieldTypes fieldType)
  {
    if (value == null)
      return string.Empty;
    return fieldType == FieldTypes.ftObjectLink && value.GetType().Equals(typeof (string)) ? $"'{value}'" : Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
  }
}
