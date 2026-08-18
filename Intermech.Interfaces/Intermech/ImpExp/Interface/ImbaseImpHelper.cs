
// Type: Intermech.ImpExp.Interface.ImbaseImpHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.ImpExp.Interface
{
    public class ImbaseImpHelper
    {
      public static string CheckSpecialNames(string name, FieldTypes ftField)
      {
        string upper = name.ToUpper();
        if (upper == "НАИМЕНОВАНИЕ")
          return "Размеры и параметры";
        if (ftField == FieldTypes.ftString)
        {
          switch (upper)
          {
            case "СОРТИРОВКА":
              return "Сортировка AVS";
            case "МАТЕРИАЛ":
              return "Код материала";
          }
        }
        return name;
      }

      public static string GetDoubleName(string name, string shortName, bool withBrackets)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < shortName.Length; ++index)
        {
          char c = shortName[index];
          if (char.IsUpper(c))
            stringBuilder.Append('^');
          stringBuilder.Append(c);
        }
        return !withBrackets ? $"{name} {stringBuilder}" : $"{name} ({stringBuilder})";
      }

      /// <summary>
      /// Формирует флаги для атрибута в таблице IMS_ATTR_TYPES
      /// Флаги должны расставляться след. образом:
      /// 1. Если поле не вычисляемое - В поле required должно содержать значение 2, Computed - 0
      /// 2. Если поле вычисляемое (т.е. присутствует формула) required - 0, Computed - 2
      /// 3. Если поле вычисляемое как у владельца (стоит что вычисляемое но формула отсутствует) required - 0, Computed - 0
      /// </summary>
      /// <param name="enterMode"></param>
      /// <param name="formulaPresent"></param>
      /// <param name="addMode"></param>
      /// <param name="computeMode"></param>
      public static void FormingComputedFlags(
        ImEnterMode enterMode,
        bool formulaPresent,
        ref RequiredModes addMode,
        ref ComputeValueModes computeMode)
      {
        addMode = RequiredModes.Manual;
        computeMode = ComputeValueModes.NotComputableValue;
        if (enterMode == ImEnterMode.IEM_ASPARENT)
          return;
        if (!formulaPresent)
        {
          addMode = RequiredModes.AutoRequired;
          computeMode = ComputeValueModes.NotComputableValue;
        }
        else
        {
          addMode = RequiredModes.Manual;
          computeMode = ComputeValueModes.JITValue;
        }
      }

      public static AttributeOptions SetOptionsForAttribute(
        IUserSession session,
        Guid attributeGuid,
        ImEnterMode imEnterMode)
      {
        return ImbaseImpHelper.SetOptionsForAttribute(session.GetAttributeType(attributeGuid, false), imEnterMode, true);
      }

      public static AttributeOptions SetOptionsForAttribute(
        IDBAttributeType attribute,
        ImEnterMode imEnterMode,
        bool setFreeFlags = false)
      {
        AttributeOptions attributeOptions = AttributeOptions.None;
        if (attribute == null || attribute.AttributeType == FieldTypes.ftSystem)
          return attributeOptions;
        if (imEnterMode == ImEnterMode.IEM_RECORD && (attribute.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.None)
        {
          attribute.Options |= AttributeOptions.ImbaseFlag_TableRecordRef;
          attributeOptions |= AttributeOptions.ImbaseFlag_TableRecordRef;
        }
        if (setFreeFlags)
        {
          if (imEnterMode == ImEnterMode.IEM_SEARCH_OBJECT)
          {
            if ((attribute.Options & AttributeOptions.FreeFlag1) == AttributeOptions.None)
              attribute.Options |= AttributeOptions.FreeFlag1;
            attributeOptions |= AttributeOptions.FreeFlag1;
          }
          if (imEnterMode == ImEnterMode.IEM_SEARCH_DOCUMENT)
          {
            if ((attribute.Options & AttributeOptions.FreeFlag2) == AttributeOptions.None)
              attribute.Options |= AttributeOptions.FreeFlag2;
            attributeOptions |= AttributeOptions.FreeFlag2;
          }
        }
        return attributeOptions;
      }

      public static TableAttribute CheckNameColumn(List<Guid> fieldNames)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        for (int index = 0; index < fieldNames.Count; ++index)
        {
          switch (fieldNames[index].ToString().ToLower())
          {
            case "cad008d8-306c-11d8-b4e9-00304f19f545":
              flag1 = true;
              break;
            case "cad003de-306c-11d8-b4e9-00304f19f545":
              flag2 = true;
              break;
            case "cad00211-306c-11d8-b4e9-00304f19f545":
              flag3 = true;
              break;
          }
        }
        return flag3 ? new TableAttribute(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), RequiredModes.AutoRequired, ComputeValueModes.JITValue, OptimizationModes.Read, AttributeOptions.None, $"{(flag1 ? "[cad008d8-306c-11d8-b4e9-00304f19f545]+' '+" : string.Empty)}[cad00211-306c-11d8-b4e9-00304f19f545]{(flag2 ? "+' '+[cad003de-306c-11d8-b4e9-00304f19f545]" : string.Empty)}", string.Empty, Guid.Empty, string.Empty, ImEnterMode.IEM_SIMPLE) : (TableAttribute) null;
      }

      public static DataTable GetTableData()
      {
        DataTable tableData = new DataTable("IMS_DATA");
        tableData.Columns.Add(new DataColumn("F_GUID", typeof (Guid)));
        tableData.Columns.Add(new DataColumn("F_KEY", typeof (int))
        {
          AutoIncrement = true
        });
        tableData.AcceptChanges();
        return tableData;
      }

      public static DataTable GetTableAttributes()
      {
        DataTable tableAttributes = new DataTable("IMS_ATTR_TYPES");
        DataColumn column = new DataColumn("F_ATTRIBUTE_GUID", typeof (string));
        tableAttributes.Columns.Add(column);
        tableAttributes.Columns.Add(new DataColumn("F_REQUIRED", typeof (int)));
        tableAttributes.Columns.Add(new DataColumn("F_COMPUTED", typeof (int)));
        tableAttributes.Columns.Add(new DataColumn("F_FORMULA", typeof (string)));
        tableAttributes.Columns.Add(new DataColumn("F_UNIQUE", typeof (int)));
        tableAttributes.Columns.Add(new DataColumn("F_DEFAULT_VALUE", typeof (string)));
        tableAttributes.Columns.Add(new DataColumn("F_OPTIONS", typeof (int)));
        tableAttributes.Columns.Add(new DataColumn("F_MASK", typeof (string)));
        tableAttributes.Columns.Add(new DataColumn("F_UNITS", typeof (string)));
        tableAttributes.Columns.Add(new DataColumn("F_DISPLAY", typeof (string)));
        tableAttributes.PrimaryKey = new DataColumn[1]{ column };
        tableAttributes.AcceptChanges();
        return tableAttributes;
      }
    }
}
