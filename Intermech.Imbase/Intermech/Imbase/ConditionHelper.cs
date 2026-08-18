// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ConditionHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Imbase;

internal static class ConditionHelper
{
  internal static Condition ConditionFromString(string condStrind)
  {
    switch (condStrind)
    {
      case "":
        return Condition.None;
      case "!*":
        return Condition.NotSubstring;
      case "!=":
        return Condition.NotEqual;
      case "*":
        return Condition.Substring;
      case "<":
        return Condition.Less;
      case "<!x<":
        return Condition.NotBetween;
      case "<=":
        return Condition.LessOrEqual;
      case "<x<":
        return Condition.Between;
      case "=":
        return Condition.Equal;
      case ">":
        return Condition.Greater;
      case ">=":
        return Condition.GreaterOrEqual;
      default:
        throw new Exception(condStrind + " Ошибка в условии.");
    }
  }

  internal static string StringFromCondition(Condition cond)
  {
    switch (cond)
    {
      case Condition.None:
        return "";
      case Condition.Equal:
        return "=";
      case Condition.NotEqual:
        return "!=";
      case Condition.Substring:
        return "*";
      case Condition.Greater:
        return ">";
      case Condition.GreaterOrEqual:
        return ">=";
      case Condition.Less:
        return "<";
      case Condition.LessOrEqual:
        return "<=";
      case Condition.Between:
        return "<x<";
      case Condition.NotBetween:
        return "<!x<";
      case Condition.NotSubstring:
        return "!*";
      default:
        throw new Exception(cond.ToString() + " Ошибка в условии.");
    }
  }

  internal static void FillConditionsMap(DataTable condsMap)
  {
    condsMap.Clear();
    condsMap.Rows.Add((object) Condition.None, (object) "");
    condsMap.Rows.Add((object) Condition.Equal, (object) LocalizationHolder.rm.GetString(sc_7609.ssp_imbase_7610()));
    condsMap.Rows.Add((object) Condition.NotEqual, (object) LocalizationHolder.rm.GetString("Imbase.Client_33"));
    condsMap.Rows.Add((object) Condition.Substring, (object) LocalizationHolder.rm.GetString("Imbase_Substring"));
    condsMap.Rows.Add((object) Condition.NotSubstring, (object) "!*Abc* (не содержит подстроку");
    condsMap.Rows.Add((object) Condition.Greater, (object) LocalizationHolder.rm.GetString("Imbase.Client_34"));
    condsMap.Rows.Add((object) Condition.GreaterOrEqual, (object) LocalizationHolder.rm.GetString("Imbase.Client_35"));
    condsMap.Rows.Add((object) Condition.Less, (object) LocalizationHolder.rm.GetString("Imbase.Client_36"));
    condsMap.Rows.Add((object) Condition.LessOrEqual, (object) LocalizationHolder.rm.GetString("Imbase.Client_37"));
    condsMap.Rows.Add((object) Condition.Between, (object) LocalizationHolder.rm.GetString("Imbase.Client_39"));
    condsMap.Rows.Add((object) Condition.NotBetween, (object) LocalizationHolder.rm.GetString("Imbase.Client_40"));
    condsMap.Rows.Add((object) Condition.InList, (object) LocalizationHolder.rm.GetString("Imbase.Client_42"));
    condsMap.Rows.Add((object) Condition.NotInList, (object) LocalizationHolder.rm.GetString("Imbase.Client_43"));
  }

  internal static string CondsToString(List<ConditionItem> conds)
  {
    if (conds == null || conds.Count == 0)
      return string.Empty;
    StringWriter w = new StringWriter();
    XmlTextWriter writer = new XmlTextWriter((TextWriter) w);
    writer.Formatting = Formatting.None;
    writer.WriteStartDocument();
    writer.WriteStartElement("Items");
    writer.WriteAttributeString("Count", conds.Count.ToString());
    foreach (ConditionItem cond in conds)
      ConditionHelper.WriteCondItem(writer, cond);
    writer.WriteEndElement();
    writer.WriteEndDocument();
    writer.Flush();
    writer.Close();
    return w.ToString();
  }

  internal static List<ConditionItem> StringToConds(string config)
  {
    if (string.IsNullOrEmpty(config))
      return (List<ConditionItem>) null;
    List<ConditionItem> conds = (List<ConditionItem>) null;
    XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(config));
    while (xmlTextReader.Read())
    {
      if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "Items")
      {
        conds = new List<ConditionItem>();
        int.Parse(xmlTextReader.GetAttribute("Count"));
        while (xmlTextReader.Read())
        {
          if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "Item")
            conds.Add(new ConditionItem()
            {
              AttId = int.Parse(xmlTextReader.GetAttribute("AttId")),
              Condition = (Condition) int.Parse(xmlTextReader.GetAttribute("Condition")),
              Data = xmlTextReader.GetAttribute("Data"),
              Data2 = xmlTextReader.GetAttribute("Data2")
            });
        }
      }
    }
    xmlTextReader.Close();
    return conds;
  }

  private static void WriteCondItem(XmlTextWriter writer, ConditionItem item)
  {
    writer.WriteStartElement("Item");
    writer.WriteAttributeString("AttId", item.AttId.ToString());
    writer.WriteAttributeString("Condition", ((int) item.Condition).ToString());
    writer.WriteAttributeString("Data", item.Data);
    writer.WriteAttributeString("Data2", item.Data2);
    writer.WriteEndElement();
  }
}
