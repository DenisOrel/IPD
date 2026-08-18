
// Type: Intermech.Navigator.Classifiers.CalcFormulaRules
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Intermech.Navigator.Classifiers;

public abstract class CalcFormulaRules
{
  public static ArrayList possibleTypes = new ArrayList((ICollection) new FieldTypes[8]
  {
    FieldTypes.ftString,
    FieldTypes.ftInteger,
    FieldTypes.ftDouble,
    FieldTypes.ftDateTime,
    FieldTypes.ftObjectLink,
    FieldTypes.ftObjectLinkByID,
    FieldTypes.ftBoolean,
    FieldTypes.ftAutoInc
  });

  public static FormulaRecord GetAttributeAndFormula(string attributeValue)
  {
    return new FormulaRecord(attributeValue);
  }

  public static MyElement GetFormula(
    IUserSession session,
    FieldTypes fieldType,
    object formula,
    bool fromBase)
  {
    MyElement formula1 = new MyElement();
    CultureInfo provider = fromBase ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture;
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftString:
          formula1 = new MyElement((object) Convert.ToString(formula), Convert.ToString(formula), (object) null);
          break;
        case FieldTypes.ftInteger:
          formula1 = new MyElement((object) Convert.ToInt64(formula), Convert.ToString(formula), (object) null);
          break;
        case FieldTypes.ftDouble:
          double num = Convert.ToDouble(formula, (IFormatProvider) provider);
          formula1 = new MyElement((object) num, Convert.ToString(num, (IFormatProvider) CultureInfo.CurrentCulture), (object) null);
          break;
        case FieldTypes.ftDateTime:
          DateTime dateTime = Convert.ToDateTime(formula, (IFormatProvider) provider);
          string shortDateString = Convert.ToString(dateTime, (IFormatProvider) CultureInfo.CurrentCulture);
          if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0)
            shortDateString = dateTime.ToShortDateString();
          formula1 = new MyElement((object) dateTime, shortDateString, (object) null);
          break;
        case FieldTypes.ftObjectLink:
          object obj = formula;
          if (formula.ToString().Substring(0, 1) == "<")
          {
            string str = Convert.ToString(formula);
            obj = (object) str.Substring(str.IndexOf("\"") + 1, str.Length - str.LastIndexOf("\"") + 2);
          }
          QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(obj));
          formula1 = new MyElement((object) objectInfo.ObjectID, objectInfo.Caption, (object) null);
          break;
        case FieldTypes.ftBoolean:
          string caption = Convert.ToString(formula) == "1" ? Intermech.Consts.TrueValue : Intermech.Consts.FalseValue;
          formula1 = new MyElement((object) Convert.ToInt16(formula), caption, (object) null);
          break;
        case FieldTypes.ftAutoInc:
          formula1 = new MyElement((object) Convert.ToInt64(formula), Convert.ToString(formula), (object) null);
          break;
      }
    }
    catch
    {
      formula1 = new MyElement();
    }
    return formula1;
  }

  public static DataTable RefreshDataTable(ClassifierCalcFormula calcFormula, ArrayList parent)
  {
    DataTable dataTable = CalcFormulaRules.FormingTableHeader();
    ClassifierAttribute[] attributes = calcFormula.Attributes;
    for (int index = 0; index < attributes.Length; ++index)
    {
      if (attributes[index] != null && attributes[index].AttributeValue != null && attributes[index].Action != ClassifierAttributesAction.Delete)
      {
        DataRow row = dataTable.NewRow();
        row["ATTRIBUTE"] = (object) attributes[index].AttributeValue.AttrName;
        if (attributes[index].Formula.Caption == string.Empty && attributes[index].Formula.Value != null && Convert.ToString(attributes[index].Formula.Value) != string.Empty)
        {
          if (attributes[index].AttributeValue.AttrType == FieldTypes.ftObjectLink)
            row["FORMULA"] = (object) $"{LocalizationHolder.rm.GetString("Client.Core_261")}{Convert.ToString(attributes[index].Formula.Value)}\" >";
        }
        else
          row["FORMULA"] = (object) attributes[index].Formula.Caption;
        row["TAG"] = (object) index;
        row["PARENT"] = parent == null || parent.Count <= 0 ? (object) 0 : (object) (parent.BinarySearch((object) attributes[index].AttributeValue.AttrGUID) >= 0 ? 1 : 0);
        dataTable.Rows.Add(row);
      }
    }
    dataTable.AcceptChanges();
    return dataTable;
  }

  private static DataTable FormingTableHeader()
  {
    DataTable dataTable = new DataTable(LocalizationHolder.rm.GetString("Client.Core_262"));
    DataColumn dataColumn1 = new DataColumn("ATTRIBUTE")
    {
      Caption = LocalizationHolder.rm.GetString("Client.Core_220")
    };
    DataColumn dataColumn2 = new DataColumn("FORMULA")
    {
      Caption = LocalizationHolder.rm.GetString("Client.Core_263")
    };
    DataColumn dataColumn3 = new DataColumn("TAG");
    DataColumn dataColumn4 = new DataColumn("PARENT");
    dataTable.Columns.AddRange(new DataColumn[4]
    {
      dataColumn1,
      dataColumn2,
      dataColumn3,
      dataColumn4
    });
    return dataTable;
  }

  /// <summary>Формирование DataTable для грида</summary>
  /// <returns></returns>
  public static DataTable FormingDataTable(ClassifierCalcFormula calcFormula, ArrayList parent)
  {
    object[] calcFormulaValue = calcFormula.CalcFormulaValue;
    DataTable dataTable = CalcFormulaRules.FormingTableHeader();
    if (calcFormulaValue != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < calcFormulaValue.Length; ++index)
        {
          FormulaRecord attributeAndFormula = CalcFormulaRules.GetAttributeAndFormula(Convert.ToString(calcFormulaValue[index]));
          if (attributeAndFormula.AttributeGuid != string.Empty)
          {
            CalcFormulaAttribute attributeValue = new CalcFormulaAttribute(sessionKeeper.Session, attributeAndFormula.AttributeGuid);
            if (attributeValue != null && !(attributeValue.AttrGUID == string.Empty))
            {
              MyElement formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, attributeValue.AttrType, (object) attributeAndFormula.Formula, true);
              if (attributeValue.AttrPossibleValues.Count > 0)
              {
                bool flag = false;
                foreach (MyElement attrPossibleValue in attributeValue.AttrPossibleValues)
                {
                  if (Convert.ToString(attrPossibleValue.Value) == Convert.ToString(formula.Value))
                  {
                    formula = attrPossibleValue;
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  formula = new MyElement();
              }
              DataRow row = dataTable.NewRow();
              calcFormula.Attributes[index] = new ClassifierAttribute(attributeValue, formula, attributeAndFormula.SizeControl, attributeAndFormula.UseMissed);
              row["ATTRIBUTE"] = (object) attributeValue.AttrName;
              if (formula.Caption == string.Empty && formula.Value != null && Convert.ToString(formula.Value) != string.Empty)
              {
                if (attributeValue.AttrType == FieldTypes.ftObjectLink)
                  row["FORMULA"] = (object) $"{LocalizationHolder.rm.GetString("Client.Core_261")}{Convert.ToString(formula.Value)}\" >";
              }
              else
                row["FORMULA"] = (object) formula.Caption;
              row["TAG"] = (object) index;
              row["PARENT"] = (object) (parent == null || parent.Count <= 0 ? 0 : (parent.BinarySearch((object) attributeValue.AttrGUID) >= 0 ? 1 : 0));
              dataTable.Rows.Add(row);
            }
          }
        }
      }
    }
    dataTable.AcceptChanges();
    return dataTable;
  }

  /// <summary>Проверка шаблона</summary>
  public static bool CheckFormula(string formula)
  {
    if (formula.IndexOf("%") < 0)
      return true;
    string[] strArray = new string[3]
    {
      "%(9)+:\\d+:\\d+%",
      "%(9)+:\\d+%",
      "%(9)+%"
    };
    bool flag = false;
    foreach (string pattern in strArray)
    {
      if (new Regex(pattern).Matches(formula).Count == 1)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }
}
