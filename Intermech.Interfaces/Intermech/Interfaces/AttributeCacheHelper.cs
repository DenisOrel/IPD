
// Type: Intermech.Interfaces.AttributeCacheHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Общие методы классов CAttributeTypeCollection и DBAttributeTypeCollection
    /// </summary>
    public class AttributeCacheHelper
    {
      public static RelationalOperators[] GetMultiValuesRelationalOperators(bool stringType)
      {
        return stringType ? new RelationalOperators[14]
        {
          RelationalOperators.Empty,
          RelationalOperators.NotExistsOrEmpty,
          RelationalOperators.NotEmpty,
          RelationalOperators.Equal,
          RelationalOperators.NotEqual,
          RelationalOperators.StringTemplate,
          RelationalOperators.Substring,
          RelationalOperators.StartString,
          RelationalOperators.EndString,
          RelationalOperators.NotSubstring,
          RelationalOperators.NotStartString,
          RelationalOperators.NotEndString,
          RelationalOperators.In,
          RelationalOperators.AttributeExists
        } : new RelationalOperators[7]
        {
          RelationalOperators.Empty,
          RelationalOperators.NotExistsOrEmpty,
          RelationalOperators.NotEmpty,
          RelationalOperators.Equal,
          RelationalOperators.NotEqual,
          RelationalOperators.In,
          RelationalOperators.AttributeExists
        };
      }

      /// <summary>
      /// Метод возвращает допустимые операторы сравнения для случаев, когда в ConditionStructure подают в качестве значения ConditionFormula
      /// </summary>
      /// <param name="stringType">true если тип данных атрибута слева - строковый</param>
      /// <returns>Массив операторов сравнения</returns>
      public static RelationalOperators[] GetFormulaRelationalOperators(bool stringType)
      {
        return stringType ? new RelationalOperators[15]
        {
          RelationalOperators.Equal,
          RelationalOperators.Greater,
          RelationalOperators.GreaterOrEqual,
          RelationalOperators.In,
          RelationalOperators.Less,
          RelationalOperators.LessOrEqual,
          RelationalOperators.NotEqual,
          RelationalOperators.NotIn,
          RelationalOperators.StringTemplate,
          RelationalOperators.Substring,
          RelationalOperators.StartString,
          RelationalOperators.EndString,
          RelationalOperators.NotSubstring,
          RelationalOperators.NotStartString,
          RelationalOperators.NotEndString
        } : new RelationalOperators[8]
        {
          RelationalOperators.Equal,
          RelationalOperators.Greater,
          RelationalOperators.GreaterOrEqual,
          RelationalOperators.In,
          RelationalOperators.Less,
          RelationalOperators.LessOrEqual,
          RelationalOperators.NotEqual,
          RelationalOperators.NotIn
        };
      }

      /// <summary>
      /// Возвращает SQL-условие, отсеивающее только объекты, входящие в состав parentID
      /// </summary>
      public static string GetAttributesForParentSQL(DataTable table, object parentID)
      {
        if (Convert.ToInt32(parentID) <= 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        DataRow[] dataRowArray = table.Select("F_GROUP_ID = " + parentID.ToString());
        if (dataRowArray.Length != 0)
        {
          stringBuilder.Append("(F_ATTRIBUTE_ID IN (");
          int columnIndex = table.Columns.IndexOf("F_ATTRIBUTE_ID");
          foreach (DataRow dataRow in dataRowArray)
            stringBuilder.Append(dataRow[columnIndex].ToString() + ",");
          --stringBuilder.Length;
          stringBuilder.Append("))");
        }
        else
          stringBuilder.Append("(F_ATTRIBUTE_ID = -1)");
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Получить валидатор для заполнения параметров атрибута ,добавляемого к типу объектов.
      /// </summary>
      public static AttributeTypePropertiesValidator GetValidatorForObjectType(
        IDBAttributeType attrType,
        IDBAttributeTypeCollection attrTypeList)
      {
        AttributeTypePropertiesValidator validator = attrTypeList.GetValidator(attrType.AttributeType);
        if (validator.Computed[0] != attrType.Computed)
        {
          for (int index = 1; index < validator.Computed.Length; ++index)
          {
            if (validator.Computed[index] == attrType.Computed)
            {
              ComputeValueModes computeValueModes = validator.Computed[0];
              validator.Computed[0] = validator.Computed[index];
              validator.Computed[index] = computeValueModes;
              break;
            }
          }
        }
        if (validator.DefaultValue != null)
        {
          validator.DefaultValue = attrType.DefaultValue;
          if (validator.DefaultValue == null)
            validator.DefaultValue = (object) string.Empty;
        }
        if (validator.Formula != null)
          validator.Formula = (object) attrType.Formula;
        validator.LevelID = attrType.LevelID;
        validator.IsContent = attrType.IsContent;
        validator.Options = attrType.Options;
        if (validator.Mask != null)
          validator.Mask = attrType.Mask;
        switch (attrType.AttributeType)
        {
          case FieldTypes.ftShortBlob:
            validator.OptimizationMode = new OptimizationModes[3]
            {
              OptimizationModes.Write,
              OptimizationModes.Read,
              OptimizationModes.Seek
            };
            break;
          case FieldTypes.ftPassword:
            validator.OptimizationMode = new OptimizationModes[1];
            break;
          case FieldTypes.ftMemo:
            validator.OptimizationMode = new OptimizationModes[3]
            {
              OptimizationModes.Write,
              OptimizationModes.Read,
              OptimizationModes.Seek
            };
            break;
          case FieldTypes.ftBlob:
            validator.OptimizationMode = new OptimizationModes[3]
            {
              OptimizationModes.Write,
              OptimizationModes.Read,
              OptimizationModes.Seek
            };
            break;
          default:
            validator.OptimizationMode = new OptimizationModes[3]
            {
              OptimizationModes.Read,
              OptimizationModes.Seek,
              OptimizationModes.Write
            };
            break;
        }
        if (validator.Unique.Length != 0)
        {
          for (int index = 1; index < validator.Unique.Length; ++index)
          {
            if (validator.Unique[index] == attrType.UniqueMode)
            {
              UniqueValueModes uniqueValueModes = validator.Unique[index];
              validator.Unique[index] = validator.Unique[0];
              validator.Unique[0] = uniqueValueModes;
              break;
            }
          }
        }
        return validator;
      }

      /// <summary>Добавляет addInfo к таблице</summary>
      /// <param name="tbl"></param>
      /// <param name="addInfo"></param>
      /// <param name="session"></param>
      /// <returns></returns>
      public static DataTable AddInfoToTable(DataTable tbl, object[] addInfo, IUserSession session)
      {
        foreach (object obj in addInfo)
        {
          switch (obj)
          {
            case AttibuteTypesSelectParams.AddSizeTypeDescription:
              tbl.Columns.Add("F_TYPE_DESCRIPTION", Type.GetType("System.String"));
              tbl.Columns.Add("F_DEFAULT_DESCRIPT", Type.GetType("System.String"));
              foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
              {
                IDBAttributeType attributeType = session.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
                row["F_TYPE_DESCRIPTION"] = (object) attributeType.SizeTypeDescription;
                row["F_DEFAULT_DESCRIPT"] = (object) attributeType.DefaultValueDescription;
              }
              tbl.AcceptChanges();
              break;
            case FieldTypes _:
              int columnIndex = tbl.Columns.IndexOf("F_ATTRIBUTE_TYPE");
              for (int index = tbl.Rows.Count - 1; index >= 0; --index)
              {
                FieldTypes fieldTypes = columnIndex >= 0 ? (FieldTypes) Convert.ToInt32(tbl.Rows[index][columnIndex]) : session.GetAttributeType(Convert.ToInt32(tbl.Rows[index]["F_ATTRIBUTE_ID"])).AttributeType;
                if ((FieldTypes) obj != fieldTypes)
                  tbl.Rows.RemoveAt(index);
              }
              tbl.AcceptChanges();
              break;
          }
        }
        return tbl;
      }

      /// <summary>Возвращает ID атрибута</summary>
      public static int GetAttributeID(object objID, DataTable attributeTable, bool failIfNotFound)
      {
        int attributeId = 0;
        switch (objID)
        {
          case int _:
          case ObligatoryObjectAttributes _:
            attributeId = (int) objID;
            break;
          case Guid attrTypeGuid:
            attributeId = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
            if (attributeId == -10000)
            {
              DataRow[] dataRowArray = attributeTable.Select("F_GUID = " + DataSetProcessor.QString(objID.ToString()));
              if (dataRowArray.Length == 0)
              {
                if (failIfNotFound)
                  throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("Interfaces_119"), objID));
                break;
              }
              attributeId = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
              break;
            }
            break;
          case string _:
            attributeId = MetaDataHelper.GetAttributeByTypeNameID((string) objID);
            if (attributeId == -10000)
            {
              DataRow[] dataRowArray = attributeTable.Select("F_NAME = " + DataSetProcessor.QString(objID.ToString()));
              if (dataRowArray.Length == 0)
              {
                if (failIfNotFound)
                  throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("Interfaces_120"), objID));
                break;
              }
              attributeId = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
              break;
            }
            break;
          case AttributeAlias _:
            DataRow[] dataRowArray1 = attributeTable.Select("F_ALIAS = " + DataSetProcessor.QString(objID.ToString()));
            if (dataRowArray1.Length == 0)
            {
              if (failIfNotFound)
                throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("Interfaces_121"), objID));
              break;
            }
            attributeId = Convert.ToInt32(dataRowArray1[0]["F_ATTRIBUTE_ID"]);
            break;
        }
        return attributeId;
      }

      /// <summary>
      /// Заполняет валидатор исходя из fldtype, возвращает valuesFieldName
      /// </summary>
      public static string FillValidator(
        ref AttributeTypePropertiesValidator validator,
        FieldTypes fldtype,
        string areaID,
        DataTable attributesTable)
      {
        int num = 1;
        do
        {
          validator.Name = LocalizationHolder.rm.GetString("Interfaces_122") + num++.ToString();
        }
        while (attributesTable.Select("F_NAME = " + DataSetProcessor.QString(validator.Name)).Length != 0);
        string str = string.Empty;
        validator.AreaID = areaID;
        validator.ShortName = "";
        validator.Alias = "";
        validator.Note = "";
        validator.FieldType = fldtype;
        validator.LanguageID = "";
        validator.LevelID = 0;
        validator.Formula = (object) null;
        validator.AttributeGuid = Guid.NewGuid();
        validator.OptimizationMode = new OptimizationModes[3]
        {
          OptimizationModes.Write,
          OptimizationModes.Read,
          OptimizationModes.Seek
        };
        validator.InheritMode = new InheritModes[2]
        {
          InheritModes.Public,
          InheritModes.Private
        };
        validator.RequiredMode = new RequiredModes[3]
        {
          RequiredModes.Manual,
          RequiredModes.AutoRequired,
          RequiredModes.Auto
        };
        validator.IsContent = true;
        validator.Options = AttributeOptions.SaveInLog;
        validator.Mask = (string) null;
        validator.MasterAttributeID = 0;
        validator.SourceAttributeID = 0;
        switch (fldtype)
        {
          case FieldTypes.ftUnknown:
            throw new KernelException("Type ftUnknown not supported.");
          case FieldTypes.ftString:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[2]
            {
              (long) Consts.DefaultStringSize,
              (long) Consts.MaxStringSize
            };
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[4]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue,
              ComputeValueModes.IndexValue
            };
            validator.Formula = (object) string.Empty;
            validator.DefaultValue = (object) string.Empty;
            str = "F_STRING_VALUE";
            validator.Mask = string.Empty;
            validator.Options = AttributeOptions.SaveInLog | AttributeOptions.SavePrivateHistory;
            break;
          case FieldTypes.ftInteger:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[3]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue
            };
            validator.Formula = (object) string.Empty;
            validator.DefaultValue = (object) string.Empty;
            str = "F_INTEGER_VALUE";
            break;
          case FieldTypes.ftDouble:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[3]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue
            };
            validator.Formula = (object) string.Empty;
            validator.DefaultValue = (object) string.Empty;
            str = "F_DOUBLE_VALUE";
            break;
          case FieldTypes.ftDateTime:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[3]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue
            };
            validator.Formula = (object) string.Empty;
            validator.DefaultValue = (object) string.Empty;
            str = "F_DATE_VALUE";
            validator.Mask = string.Empty;
            break;
          case FieldTypes.ftShortBlob:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[2]
            {
              (long) Consts.DefaultShortBlobSize,
              (long) Consts.MaxShortBlobSize
            };
            validator.MultiValueMode = new MultiValueModes[2]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues
            };
            validator.Computed = new ComputeValueModes[1];
            validator.OptimizationMode = new OptimizationModes[1];
            validator.Formula = (object) "";
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftFile:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[2]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues
            };
            validator.Computed = new ComputeValueModes[1];
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftExternalLink:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[2]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues
            };
            validator.Computed = new ComputeValueModes[1];
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[1]{ -1L };
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[1];
            validator.DefaultValue = (object) string.Empty;
            validator.Options = AttributeOptions.SaveInLog | AttributeOptions.SavePrivateHistory;
            str = "F_INTEGER_VALUE";
            break;
          case FieldTypes.ftPassword:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[2]
            {
              (long) Consts.DefaultPasswordSize,
              (long) Consts.MaxPasswordSize
            };
            validator.MultiValueMode = new MultiValueModes[1];
            validator.Computed = new ComputeValueModes[1];
            validator.OptimizationMode = new OptimizationModes[1];
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftMemo:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[2]
            {
              (long) (Consts.MaxMemoSize / 2),
              (long) Consts.MaxMemoSize
            };
            validator.MultiValueMode = new MultiValueModes[2]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues
            };
            validator.Computed = new ComputeValueModes[2]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue
            };
            validator.Formula = (object) string.Empty;
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftBlob:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[2]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues
            };
            validator.Computed = new ComputeValueModes[1];
            validator.OptimizationMode = new OptimizationModes[1];
            validator.DefaultValue = (object) null;
            validator.Formula = (object) "";
            break;
          case FieldTypes.ftBoolean:
            validator.Unique = new UniqueValueModes[1];
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[1];
            validator.Computed = new ComputeValueModes[3]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue
            };
            validator.DefaultValue = (object) false;
            validator.Formula = (object) "";
            break;
          case FieldTypes.ftMeasured:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[1]{ -1L };
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.MultiValuesFromList,
              MultiValueModes.SingleValueFromList
            };
            validator.Computed = new ComputeValueModes[3]
            {
              ComputeValueModes.NotComputableValue,
              ComputeValueModes.StoredValue,
              ComputeValueModes.JITValue
            };
            validator.Formula = (object) null;
            validator.Options = AttributeOptions.SaveInLog | AttributeOptions.SavePrivateHistory;
            validator.DefaultValue = (object) string.Empty;
            str = "F_STRING_VALUE";
            break;
          case FieldTypes.ftAutoInc:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly,
              UniqueValueModes.NotUnique
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[1];
            validator.Computed = new ComputeValueModes[1]
            {
              ComputeValueModes.StoredValue
            };
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftSystem:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[1];
            validator.Computed = new ComputeValueModes[1];
            validator.OptimizationMode = new OptimizationModes[1]
            {
              OptimizationModes.Seek
            };
            validator.DefaultValue = (object) null;
            break;
          case FieldTypes.ftGuid:
            validator.Unique = new UniqueValueModes[4]
            {
              UniqueValueModes.NotUnique,
              UniqueValueModes.AllVerTypes,
              UniqueValueModes.TypeOnly,
              UniqueValueModes.VerTypeOnly
            };
            validator.SizeType = new long[0];
            validator.MultiValueMode = new MultiValueModes[4]
            {
              MultiValueModes.SingleValue,
              MultiValueModes.MultiValues,
              MultiValueModes.SingleValueFromList,
              MultiValueModes.MultiValuesFromList
            };
            validator.Computed = new ComputeValueModes[1];
            validator.DefaultValue = (object) string.Empty;
            str = "F_STRING_VALUE";
            break;
        }
        return str;
      }

      /// <summary>
      /// По типу данных атрибута, а также его идентификатору получить имя поля,
      /// в котором хранятся значения атрибута
      /// </summary>
      /// <param name="ft">Тип данных атрибута</param>
      /// <param name="aAttributeID">Идентификатор типа атрибута</param>
      /// <returns>Имя поля, в котором хранятся значения атрибута</returns>
      public static string GetAttributeValueFieldName(FieldTypes ft, int aAttributeID)
      {
        switch (ft)
        {
          case FieldTypes.ftString:
            return "F_STRING_VALUE";
          case FieldTypes.ftInteger:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftDouble:
            return "F_DOUBLE_VALUE";
          case FieldTypes.ftDateTime:
            return "F_DATE_VALUE";
          case FieldTypes.ftShortBlob:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftFile:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftExternalLink:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftPassword:
            return "F_STRING_VALUE";
          case FieldTypes.ftMemo:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftBlob:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftBoolean:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftMeasured:
            return "F_DOUBLE_VALUE";
          case FieldTypes.ftAutoInc:
            return "F_INTEGER_VALUE";
          case FieldTypes.ftSystem:
            int num = aAttributeID < 0 ? (int) ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) aAttributeID) : throw new KernelExceptionID(30, (object) aAttributeID);
            return ObligatoryObjectAttributesHelper.FieldName((ObligatoryObjectAttributes) aAttributeID);
          case FieldTypes.ftGuid:
            return "F_STRING_VALUE";
          default:
            return "F_STRING_VALUE";
        }
      }

      public static string[] GetAtributeFieldNames(FieldTypes ft, int attributeID)
      {
        switch (ft)
        {
          case FieldTypes.ftShortBlob:
          case FieldTypes.ftFile:
          case FieldTypes.ftBlob:
            return new string[4]
            {
              "F" + attributeID.ToString(),
              $"F{attributeID.ToString()}ID",
              $"F{attributeID.ToString()}ID2",
              $"F{attributeID.ToString()}ID3"
            };
          case FieldTypes.ftExternalLink:
            return new string[3]
            {
              "F" + attributeID.ToString(),
              $"F{attributeID.ToString()}ID",
              $"F{attributeID.ToString()}ID2"
            };
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            return new string[2]
            {
              "F" + attributeID.ToString(),
              $"F{attributeID.ToString()}ID"
            };
          case FieldTypes.ftPassword:
            return (string[]) null;
          case FieldTypes.ftMemo:
            return new string[3]
            {
              "F" + attributeID.ToString(),
              $"F{attributeID.ToString()}ID",
              $"F{attributeID.ToString()}ID3"
            };
          case FieldTypes.ftMeasured:
            return new string[3]
            {
              "F" + attributeID.ToString(),
              $"F{attributeID.ToString()}ID",
              $"F{attributeID.ToString()}ID2"
            };
          default:
            return new string[1]{ "F" + attributeID.ToString() };
        }
      }

      public static void GetAttributeTypeValues(
        FieldTypes ft,
        int aAttributeID,
        ref string valueFieldName,
        ref string textFieldName,
        ref List<FieldTypes> convertList,
        ref RelationalOperators[] enabledOperators,
        ref bool computableAttribute,
        ref string possibleValueFieldName)
      {
        switch (ft)
        {
          case FieldTypes.ftString:
            enabledOperators = new RelationalOperators[19]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.EndString,
              RelationalOperators.StartString,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotEndString,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.AttributeExists,
              RelationalOperators.NotStartString,
              RelationalOperators.NotSubstring,
              RelationalOperators.In,
              RelationalOperators.NotIn
            };
            convertList.Add(FieldTypes.ftBoolean);
            convertList.Add(FieldTypes.ftDateTime);
            convertList.Add(FieldTypes.ftDouble);
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftGuid);
            convertList.Add(FieldTypes.ftMemo);
            convertList.Add(FieldTypes.ftPassword);
            valueFieldName = "F_STRING_VALUE";
            textFieldName = "F_STRING_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftInteger:
            enabledOperators = new RelationalOperators[14]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.Between,
              RelationalOperators.NotBetween,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftAutoInc);
            convertList.Add(FieldTypes.ftDouble);
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftGuid);
            convertList.Add(FieldTypes.ftBoolean);
            convertList.Add(FieldTypes.ftObjectLink);
            convertList.Add(FieldTypes.ftObjectLinkByID);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_INTEGER_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftDouble:
            enabledOperators = new RelationalOperators[14]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.Between,
              RelationalOperators.NotBetween,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftMeasured);
            convertList.Add(FieldTypes.ftBoolean);
            valueFieldName = "F_DOUBLE_VALUE";
            textFieldName = "F_DOUBLE_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftDateTime:
            enabledOperators = new RelationalOperators[14]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.LastNDays,
              RelationalOperators.NextNDays,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.Between,
              RelationalOperators.NotBetween,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_DATE_VALUE";
            textFieldName = "F_DATE_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftShortBlob:
            enabledOperators = new RelationalOperators[19]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.EndString,
              RelationalOperators.StartString,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotEndString,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.AttributeExists,
              RelationalOperators.NotStartString,
              RelationalOperators.NotSubstring,
              RelationalOperators.In,
              RelationalOperators.NotIn
            };
            convertList.Add(FieldTypes.ftBlob);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_STRING_VALUE";
            break;
          case FieldTypes.ftFile:
            enabledOperators = new RelationalOperators[19]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.EndString,
              RelationalOperators.StartString,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotEndString,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.AttributeExists,
              RelationalOperators.NotStartString,
              RelationalOperators.NotSubstring,
              RelationalOperators.In,
              RelationalOperators.NotIn
            };
            convertList.Add(FieldTypes.ftBlob);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_STRING_VALUE";
            break;
          case FieldTypes.ftExternalLink:
            enabledOperators = new RelationalOperators[13]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.EndString,
              RelationalOperators.StartString,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotEndString,
              RelationalOperators.NotStartString,
              RelationalOperators.NotSubstring,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_INTEGER_VALUE";
            break;
          case FieldTypes.ftObjectLink:
            enabledOperators = new RelationalOperators[10]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists,
              RelationalOperators.Linked,
              RelationalOperators.NotLinked
            };
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftObjectLinkByID);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_STRING_VALUE";
            break;
          case FieldTypes.ftPassword:
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_STRING_VALUE";
            break;
          case FieldTypes.ftMemo:
            enabledOperators = new RelationalOperators[9]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotSubstring,
              RelationalOperators.NotEmpty,
              RelationalOperators.StartString,
              RelationalOperators.NotStartString,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftShortBlob);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_STRING_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftBlob:
            valueFieldName = "F_INTEGER_VALUE";
            enabledOperators = new RelationalOperators[19]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.EndString,
              RelationalOperators.StartString,
              RelationalOperators.Substring,
              RelationalOperators.StringTemplate,
              RelationalOperators.NotEndString,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.AttributeExists,
              RelationalOperators.NotStartString,
              RelationalOperators.NotSubstring,
              RelationalOperators.In,
              RelationalOperators.NotIn
            };
            textFieldName = "F_STRING_VALUE";
            convertList.Add(FieldTypes.ftShortBlob);
            break;
          case FieldTypes.ftBoolean:
            enabledOperators = new RelationalOperators[6]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftDouble);
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_INTEGER_VALUE";
            break;
          case FieldTypes.ftMeasured:
            enabledOperators = new RelationalOperators[12]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.AttributeExists,
              RelationalOperators.Between,
              RelationalOperators.NotBetween
            };
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftDouble);
            convertList.Add(FieldTypes.ftInteger);
            valueFieldName = "F_DOUBLE_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftAutoInc:
            enabledOperators = new RelationalOperators[14]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.Greater,
              RelationalOperators.GreaterOrEqual,
              RelationalOperators.Less,
              RelationalOperators.LessOrEqual,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.Between,
              RelationalOperators.NotBetween,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftDouble);
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_INTEGER_VALUE";
            computableAttribute = true;
            break;
          case FieldTypes.ftSystem:
            FieldTypes fieldTypes = aAttributeID < 0 ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) aAttributeID) : throw new KernelExceptionID(30, (object) aAttributeID);
            valueFieldName = ObligatoryObjectAttributesHelper.FieldName((ObligatoryObjectAttributes) aAttributeID);
            textFieldName = valueFieldName;
            switch (fieldTypes)
            {
              case FieldTypes.ftString:
                enabledOperators = new RelationalOperators[17]
                {
                  RelationalOperators.Empty,
                  RelationalOperators.Equal,
                  RelationalOperators.NotEmpty,
                  RelationalOperators.NotEqual,
                  RelationalOperators.EndString,
                  RelationalOperators.StartString,
                  RelationalOperators.Substring,
                  RelationalOperators.StringTemplate,
                  RelationalOperators.NotEndString,
                  RelationalOperators.Greater,
                  RelationalOperators.GreaterOrEqual,
                  RelationalOperators.Less,
                  RelationalOperators.LessOrEqual,
                  RelationalOperators.NotStartString,
                  RelationalOperators.NotSubstring,
                  RelationalOperators.In,
                  RelationalOperators.NotIn
                };
                break;
              case FieldTypes.ftDateTime:
                enabledOperators = new RelationalOperators[12]
                {
                  RelationalOperators.Empty,
                  RelationalOperators.Equal,
                  RelationalOperators.LastNDays,
                  RelationalOperators.Greater,
                  RelationalOperators.GreaterOrEqual,
                  RelationalOperators.Less,
                  RelationalOperators.LessOrEqual,
                  RelationalOperators.NotEmpty,
                  RelationalOperators.NotEqual,
                  RelationalOperators.Between,
                  RelationalOperators.In,
                  RelationalOperators.NotIn
                };
                break;
              default:
                enabledOperators = new RelationalOperators[11]
                {
                  RelationalOperators.Empty,
                  RelationalOperators.Equal,
                  RelationalOperators.Greater,
                  RelationalOperators.GreaterOrEqual,
                  RelationalOperators.Less,
                  RelationalOperators.LessOrEqual,
                  RelationalOperators.NotEmpty,
                  RelationalOperators.NotEqual,
                  RelationalOperators.Between,
                  RelationalOperators.In,
                  RelationalOperators.NotIn
                };
                break;
            }
            break;
          case FieldTypes.ftGuid:
            enabledOperators = new RelationalOperators[8]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists
            };
            convertList.Add(FieldTypes.ftString);
            valueFieldName = "F_STRING_VALUE";
            textFieldName = "F_STRING_VALUE";
            break;
          case FieldTypes.ftObjectLinkByID:
            enabledOperators = new RelationalOperators[10]
            {
              RelationalOperators.Empty,
              RelationalOperators.NotExistsOrEmpty,
              RelationalOperators.Equal,
              RelationalOperators.NotEmpty,
              RelationalOperators.NotEqual,
              RelationalOperators.In,
              RelationalOperators.NotIn,
              RelationalOperators.AttributeExists,
              RelationalOperators.Linked,
              RelationalOperators.NotLinked
            };
            convertList.Add(FieldTypes.ftString);
            convertList.Add(FieldTypes.ftInteger);
            convertList.Add(FieldTypes.ftObjectLink);
            valueFieldName = "F_INTEGER_VALUE";
            textFieldName = "F_STRING_VALUE";
            break;
          default:
            enabledOperators = (RelationalOperators[]) null;
            valueFieldName = "F_STRING_VALUE";
            textFieldName = "F_STRING_VALUE";
            computableAttribute = false;
            break;
        }
        if (ft == FieldTypes.ftMeasured)
          possibleValueFieldName = "F_STRING_VALUE";
        else
          possibleValueFieldName = valueFieldName;
      }

      /// <summary>
      /// Функция проверяет безопасно ли менять тип данных атрибута at на тип newType с размером newSize.
      /// Если преобразование вообще недопустимо - генерирует исключение.
      /// </summary>
      public static bool IsSafeConvert(IDBAttributeType at, FieldTypes newType, long newSize)
      {
        if (newType != at.AttributeType && !at.IsCompatibleType(newType))
          throw new KernelExceptionID(284);
        bool flag = true;
        if ((newType == FieldTypes.ftMemo || newType == FieldTypes.ftString || newType == FieldTypes.ftShortBlob || newType == FieldTypes.ftPassword) && (at.AttributeType == FieldTypes.ftMemo || at.AttributeType == FieldTypes.ftString || at.AttributeType == FieldTypes.ftShortBlob || at.AttributeType == FieldTypes.ftPassword) && newSize < at.SizeType)
          flag = false;
        if (newType == FieldTypes.ftString && newSize < 50L && (at.AttributeType == FieldTypes.ftInteger || at.AttributeType == FieldTypes.ftDouble || at.AttributeType == FieldTypes.ftDateTime || at.AttributeType == FieldTypes.ftBoolean))
          flag = false;
        if ((newType == FieldTypes.ftObjectLink || newType == FieldTypes.ftObjectLinkByID) && at.AttributeType == FieldTypes.ftInteger)
          flag = false;
        if (newType == FieldTypes.ftBoolean)
          flag = false;
        return flag;
      }

      public static Attribute4ObjectTypeProperties GetDefaultProperties(
        IDBAttributeTypeCollection attrs,
        int attributeID,
        int objTypeID)
      {
        AttributeTypePropertiesValidator validatorForObjectType = attrs.GetValidatorForObjectType(attributeID);
        return new Attribute4ObjectTypeProperties()
        {
          AttributeID = attributeID,
          ComputeValueMode = validatorForObjectType.Computed == null ? ComputeValueModes.StoredValue : validatorForObjectType.Computed[0],
          DefaultValue = validatorForObjectType.DefaultValue,
          FieldType = validatorForObjectType.FieldType,
          Formula = validatorForObjectType.Formula != null ? validatorForObjectType.Formula.ToString() : string.Empty,
          InheritMode = InheritModes.Private,
          IsContent = validatorForObjectType.IsContent,
          LevelID = validatorForObjectType.LevelID,
          Mask = validatorForObjectType.Mask,
          MasterAttributeID = validatorForObjectType.MasterAttributeID,
          ObjectType = Convert.ToInt32(objTypeID),
          OptimizationMode = validatorForObjectType.OptimizationMode != null ? validatorForObjectType.OptimizationMode[0] : OptimizationModes.Write,
          Options = validatorForObjectType.Options,
          RequiredMode = validatorForObjectType.RequiredMode == null ? RequiredModes.Manual : validatorForObjectType.RequiredMode[0],
          SourceAttributeID = validatorForObjectType.SourceAttributeID,
          UniqueValueMode = validatorForObjectType.Unique != null ? validatorForObjectType.Unique[0] : UniqueValueModes.NotUnique,
          ValidationRule = string.Empty
        };
      }

      /// <summary>
      /// Добавить в таблицу с запросом по атрибутам типа объектов дополнительную инфу по атрибутам
      /// </summary>
      /// <param name="attributesTable">Таблица атрибутов IMS_ATTRIBUTES</param>
      /// <param name="destinationTable">Таблица с запросом</param>
      public static void AddFieldsForAttribute(DataTable attributesTable, DataTable destinationTable)
      {
        destinationTable.Columns.Add("F_NAME", attributesTable.Columns["F_NAME"].DataType);
        destinationTable.Columns.Add("F_SHORT_NAME", attributesTable.Columns["F_SHORT_NAME"].DataType);
        destinationTable.Columns.Add("F_ALIAS", attributesTable.Columns["F_ALIAS"].DataType);
        destinationTable.Columns.Add("F_NOTE", attributesTable.Columns["F_NOTE"].DataType);
        destinationTable.Columns.Add("F_MULTIPLE_VALUED", attributesTable.Columns["F_MULTIPLE_VALUED"].DataType);
        destinationTable.Columns.Add("F_SIZE_TYPE", attributesTable.Columns["F_SIZE_TYPE"].DataType);
        destinationTable.Columns.Add("F_GUID", attributesTable.Columns["F_GUID"].DataType);
        int columnIndex = attributesTable.Columns.IndexOf("F_ATTRIBUTE_ID");
        for (int index = 0; index < destinationTable.Rows.Count; ++index)
        {
          DataRow row = destinationTable.Rows[index];
          DataRow dataRow = attributesTable.Rows.Find(row[columnIndex]);
          row["F_NAME"] = dataRow["F_NAME"];
          row["F_SHORT_NAME"] = dataRow["F_SHORT_NAME"];
          row["F_ALIAS"] = dataRow["F_ALIAS"];
          row["F_NOTE"] = dataRow["F_NOTE"];
          row["F_MULTIPLE_VALUED"] = dataRow["F_MULTIPLE_VALUED"];
          row["F_SIZE_TYPE"] = dataRow["F_SIZE_TYPE"];
          row["F_GUID"] = dataRow["F_GUID"];
        }
        destinationTable.AcceptChanges();
      }

      /// <summary>
      /// Получить тип данных атрибута по указанному идентификатору системного атрибута
      /// </summary>
      /// <param name="columnID">Идентификатор системного атрибута</param>
      /// <returns>Тип данных атрибута</returns>
      public static FieldTypes GetColumnAttrType(ObligatoryObjectAttributes columnID)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType((int) columnID);
        if (attributeType != null && attributeType.RealFieldType != FieldTypes.ftUnknown)
          return attributeType.RealFieldType;
        switch (columnID)
        {
          case ObligatoryObjectAttributes.F_REL_CREATOR:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_CREATOR_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_ACCESS:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_ELEMENT_STATUSES:
            return FieldTypes.ftSystem;
          case ObligatoryObjectAttributes.F_OBJECTLINK_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_ZIPSIZE:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_FILEDATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_FILESIZE:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_FILENAME:
            return FieldTypes.ftString;
          case ObligatoryObjectAttributes.F_FILE_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.CAPTION:
            return FieldTypes.ftString;
          case ObligatoryObjectAttributes.F_NOTE:
            return FieldTypes.ftString;
          case ObligatoryObjectAttributes.F_OBJECT_NAME:
            return FieldTypes.ftString;
          case ObligatoryObjectAttributes.F_DELETE_DATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_CREATE_DATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_RELATION_TYPE:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_PART_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_PROJ_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_PRJLINK_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_MODIFICATION_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_PROJECT_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_OBJ_CREATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_GUID:
            return FieldTypes.ftGuid;
          case ObligatoryObjectAttributes.F_MODIFY_DATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_LEVEL_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_OWNER_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_OBJECT_TYPE:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_CHKOUT_BY:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_VERSION_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_LC_STEP:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_ID:
            return FieldTypes.ftInteger;
          case ObligatoryObjectAttributes.F_OBJECT_ID:
            return FieldTypes.ftInteger;
          default:
            return FieldTypes.ftUnknown;
        }
      }

      /// <summary>
      /// Получить массив атрибутов, которые можно добавлять какому-то типу объектов или связей
      /// </summary>
      /// <param name="attributesTbl">Таблица IMS_ATTRIBUTES</param>
      /// <param name="attrs4typeTbl">Таблица принадлежности атрибутов типу</param>
      /// <param name="filterStr">Условие фильтрации атрибутов типа (если пусто, то все атрибуты)</param>
      /// <param name="attributeSource">Что за тип - объект или связь. Если Auto, то системные атрибуты не добавляем.</param>
      /// <returns></returns>
      public static BasicAttributeProperties[] GetEnabledAttributes(
        DataTable attributesTbl,
        DataTable attrs4typeTbl,
        string filterStr,
        AttributeSourceTypes attributeSource)
      {
        List<BasicAttributeProperties> attributePropertiesList;
        if (filterStr != string.Empty)
        {
          DataRow[] dataRowArray = attrs4typeTbl.Select(filterStr);
          attributePropertiesList = new List<BasicAttributeProperties>(dataRowArray.Length);
          foreach (DataRow dataRow1 in dataRowArray)
          {
            int int32 = Convert.ToInt32(dataRow1["F_ATTRIBUTE_ID"]);
            DataRow dataRow2 = attributesTbl.Rows.Find((object) int32);
            attributePropertiesList.Add(new BasicAttributeProperties(int32, dataRow2["F_NAME"].ToString(), (FieldTypes) Convert.ToInt32(dataRow2["F_ATTRIBUTE_TYPE"])));
          }
          if (attributeSource != AttributeSourceTypes.Auto)
          {
            foreach (DataRow dataRow in attributesTbl.Select("F_ATTRIBUTE_ID < 0"))
            {
              int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
              ObligatoryObjectAttributes objectAttributes = (ObligatoryObjectAttributes) int32;
              if (ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes) == attributeSource)
                attributePropertiesList.Add(new BasicAttributeProperties(int32, dataRow["F_NAME"].ToString(), AttributeCacheHelper.GetColumnAttrType(objectAttributes)));
            }
          }
        }
        else
        {
          attributePropertiesList = new List<BasicAttributeProperties>(attributesTbl.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) attributesTbl.Rows)
          {
            int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
            if (int32 < 0)
            {
              if (attributeSource != AttributeSourceTypes.Auto)
              {
                ObligatoryObjectAttributes objectAttributes = (ObligatoryObjectAttributes) int32;
                if (ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes) == attributeSource)
                  attributePropertiesList.Add(new BasicAttributeProperties(int32, row["F_NAME"].ToString(), AttributeCacheHelper.GetColumnAttrType(objectAttributes)));
              }
            }
            else
              attributePropertiesList.Add(new BasicAttributeProperties(int32, row["F_NAME"].ToString(), (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"])));
          }
        }
        return attributePropertiesList.ToArray();
      }

      /// <summary>
      /// Определяет можно ли типу объектов objectTypeID присваивать атрибут attributeID
      /// </summary>
      /// <param name="attributeID">Ид атрибута</param>
      /// <param name="objectTypeID">Ид типа объектов</param>
      /// <returns></returns>
      public static bool IsEnabledObjectTypeAttribute(int attributeID, int objectTypeID)
      {
        if (attributeID < 0)
          return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Object;
        return MetaDataHelper.GetObjectType(objectTypeID).AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectTypeID, attributeID) != null;
      }

      /// <summary>
      /// Определяет можно ли типу связей relationTypeID присваивать атрибут attributeID
      /// </summary>
      /// <param name="attributeID">Ид атрибута</param>
      /// <param name="relationTypeID">Ид типа связей</param>
      /// <returns></returns>
      public static bool IsEnabledRelationTypeAttribute(int attributeID, int relationTypeID)
      {
        if (attributeID < 0)
          return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Relation;
        return MetaDataHelper.GetRelationType(relationTypeID).AnyAttributes || MetaDataHelper.GetAttribute4RelationType(relationTypeID, attributeID) != null;
      }
    }
}
