// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.FormDesigner.CAD.Classes.ExternalCADAttrParser
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using Intermech.TechCard.Client.FormDesigner.CAD.Navigator;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.FormDesigner.CAD.Classes;

/// <summary>Cad attributes parser</summary>
internal sealed class ExternalCADAttrParser
{
  /// <summary>Parse string</summary>
  /// <param name="elementInfo"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public static Dictionary<int, object> Parse(IElementInfo elementInfo, string data)
  {
    if (data.Equals(string.Empty))
      return (Dictionary<int, object>) null;
    Dictionary<int, object> dictionary = new Dictionary<int, object>();
    string[] attributes1 = ExternalCADAttrParser.ParseAttributes(data);
    if (new ExternalCADAttrForm(attributes1).ShowDialog() != DialogResult.OK)
      return (Dictionary<int, object>) null;
    List<ExternalCADAttrBase> externalCadAttrBaseList = new List<ExternalCADAttrBase>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (string data1 in attributes1)
      {
        ExternalCADAttrBase attribute = ExternalCADAttrParser.ParseAttribute(data1);
        if (attribute != null)
          externalCadAttrBaseList.Add(attribute);
      }
      foreach (ExternalCADAttrBase externalCadAttrBase in externalCadAttrBaseList)
      {
        List<string> stringList = new List<string>();
        foreach (string key in externalCadAttrBase.Params.Keys)
        {
          if (key.Contains("Value"))
          {
            string str = externalCadAttrBase.Params[key];
            if (!str.Equals(string.Empty))
              stringList.Add(str);
          }
          else
          {
            IDBAttributeType attrType = ExternalCADAttrParser.GetAttrType(key, sessionKeeper.Session);
            if (attrType != null)
            {
              string data2 = externalCadAttrBase.Params[key];
              if (!data2.Equals(string.Empty))
                dictionary.Add(attrType.AttributeID, ExternalCADAttrParser.GetAttrValue(attrType, data2));
            }
            else
              continue;
          }
          if (stringList.Count != 0)
          {
            IDBAttributeType attrType = ExternalCADAttrParser.GetAttrType(externalCadAttrBase.Name, sessionKeeper.Session);
            if (attrType != null)
            {
              string[] array = stringList.ToArray();
              switch (attrType.MultipleValued)
              {
                case MultiValueModes.SingleValue:
                  dictionary.Add(attrType.AttributeID, (object) string.Join(",", array));
                  continue;
                case MultiValueModes.MultiValues:
                  dictionary.Add(attrType.AttributeID, (object) array);
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }
    if (dictionary.Count != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributableType attributableType = (IDBAttributableType) null;
        switch (elementInfo.ElementKind)
        {
          case AttributableElements.Object:
            IDBAttributable dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(elementInfo.ElementIdentifier);
            attributableType = (IDBAttributableType) sessionKeeper.Session.GetObjectType(dbAttributable.TypeID);
            break;
          case AttributableElements.Relation:
            IDBAttributable relation = (IDBAttributable) sessionKeeper.Session.GetRelation(elementInfo.ElementIdentifier);
            attributableType = (IDBAttributableType) sessionKeeper.Session.GetRelationType(relation.TypeID);
            break;
        }
        if (!attributableType.AnyAttributes)
        {
          List<int> intList = new List<int>();
          IDBAttribute4TypeCollection attributes2 = attributableType.Attributes;
          foreach (int key in dictionary.Keys)
          {
            if (attributes2.GetAttributeByID(key, false) == null)
              intList.Add(key);
          }
          foreach (int key in intList)
            dictionary.Remove(key);
        }
      }
    }
    return dictionary;
  }

  /// <summary>Parse attributes</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  public static string[] ParseAttributes(string data)
  {
    string[] strArray = data.Split('\a');
    List<string> stringList = new List<string>();
    string str1 = string.Empty;
    foreach (string str2 in strArray)
    {
      if (str2.Contains("AttributeType") && !str1.Equals(string.Empty))
      {
        stringList.Add(str1);
        str1 = string.Empty;
      }
      str1 = $"{str1}{str2}\a";
    }
    if (!str1.Equals(string.Empty))
      stringList.Add(str1);
    return stringList.ToArray();
  }

  /// <summary>Parse attribute</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  public static ExternalCADAttrBase ParseAttribute(string data)
  {
    if (data.Equals(string.Empty) || !data.Contains("AttributeType"))
      return (ExternalCADAttrBase) null;
    string[] strArray1 = data.Split('\a');
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (string str in strArray1)
    {
      char[] chArray = new char[1]{ '=' };
      string[] strArray2 = str.Split(chArray);
      if (strArray2.Length == 2)
        dictionary.Add(strArray2[0], strArray2[1]);
    }
    if (!dictionary.ContainsKey("AttributeType"))
      return (ExternalCADAttrBase) null;
    string name = dictionary["AttributeType"];
    if (name.Equals(string.Empty))
      return (ExternalCADAttrBase) null;
    return new ExternalCADAttrBase(name)
    {
      Params = dictionary
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="paramName"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  internal static IDBAttributeType GetAttrType(string paramName, IUserSession session)
  {
    return session.GetAttributeType(paramName, false);
  }

  /// <summary>Get correct attr value</summary>
  /// <param name="attrType"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  internal static object GetAttrValue(IDBAttributeType attrType, string data)
  {
    return attrType == null ? (object) null : (object) data;
  }
}
