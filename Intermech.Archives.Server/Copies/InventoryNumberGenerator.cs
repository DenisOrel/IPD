// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.InventoryNumberGenerator
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Server;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Archives.Copies;

public class InventoryNumberGenerator : LongLifeObject, IInventoryNumberGenerator
{
  public Dictionary<string, long> GenerateNumber(long objectID, int objectType, out string formula)
  {
    formula = string.Empty;
    if (!((ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (ICopiesService)) is ICopiesService service))
      return new Dictionary<string, long>();
    int num = objectType;
    int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(ConstsHolder.DocTypeID);
    object obj = (object) null;
    for (; num != objectTypeParentId; num = MetaDataHelper.GetObjectTypeParentID(num))
    {
      obj = service.GetFormula(num);
      if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
        break;
    }
    formula = obj == null ? string.Empty : obj.ToString();
    return this.ParseFormula(ref formula, objectID, (long) num);
  }

  public Dictionary<string, long> ParseFormula(
    ref string formula,
    long objectID,
    long parentTypeID)
  {
    string str = formula;
    Dictionary<string, long> formula1 = new Dictionary<string, long>();
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("InventoryNumberGenerator.ParseFormula");
    try
    {
      DateTime dateTime = DateTime.UtcNow + sessionTemporaryClone.TimeZoneOffset;
      string pattern1 = "{[(dMy)+(\\.\\/:\\s\\,)*]+}";
      MatchCollection matchCollection1 = Regex.Matches(formula, pattern1);
      for (int i = 0; i < matchCollection1.Count; ++i)
      {
        string oldValue = matchCollection1[i].Value;
        string format = oldValue.Trim('{', '}');
        str = str.Replace(oldValue, dateTime.ToString(format));
      }
      formula = str;
      IDBObject dbObject = sessionTemporaryClone.GetObject(objectID, false);
      if (dbObject != null)
      {
        List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
        string pattern2 = "\\{[^}]+\\}";
        MatchCollection matchCollection2 = Regex.Matches(formula, pattern2);
        for (int i = 0; i < matchCollection2.Count; ++i)
        {
          string oldValue = matchCollection2[i].Value;
          string attrName = oldValue.Trim('{', '}');
          IMSAttributeType imsAttributeType = attributeTypesList.Find((Predicate<IMSAttributeType>) (currentType => currentType.Name == attrName || currentType.ShortName == attrName));
          if (imsAttributeType != null)
          {
            string empty = string.Empty;
            object[] valuesById = dbObject.GetValuesByID(imsAttributeType.AttributeID, false);
            if (valuesById != null)
              empty = valuesById[0].ToString();
            str = str.Replace(oldValue, empty);
          }
        }
        formula = str;
        string pattern3 = "{9+}";
        MatchCollection matchCollection3 = Regex.Matches(formula, pattern3);
        if (matchCollection3.Count > 0)
        {
          string s = sessionTemporaryClone.Configurations.ReadStringNoCache("Archive", "Counters", "Counter", true);
          long num1 = 0;
          ref long local = ref num1;
          long.TryParse(s, out local);
          long num2 = num1 + 1L;
          formula1.Add("Counter", num2);
          for (int i = 0; i < matchCollection3.Count; ++i)
          {
            string oldValue = matchCollection3[i].Value;
            str = str.Replace(oldValue, num2.ToString("D" + (oldValue.Length - 2).ToString()));
          }
          sessionTemporaryClone.Configurations.WriteInteger("Archive", "Counters", "Counter", num2, 0L);
        }
        formula = str;
        string pattern4 = "{T9+}";
        MatchCollection matchCollection4 = Regex.Matches(formula, pattern4);
        if (matchCollection4.Count > 0)
        {
          string s = sessionTemporaryClone.Configurations.ReadStringNoCache("Archive", "Counters", $"T{parentTypeID}", true);
          long num3 = 0;
          ref long local = ref num3;
          long.TryParse(s, out local);
          long num4 = num3 + 1L;
          formula1.Add($"T{parentTypeID}", num4);
          for (int i = 0; i < matchCollection4.Count; ++i)
          {
            string oldValue = matchCollection4[i].Value;
            str = str.Replace(oldValue, num4.ToString("D" + (oldValue.Length - 3).ToString()));
          }
          sessionTemporaryClone.Configurations.WriteInteger("Archive", "Counters", $"T{parentTypeID}", num4, 0L);
        }
        formula = str;
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("InventoryNumberGenerator.ParseFormula");
    }
    return formula1;
  }

  public void RestoreCounters(Dictionary<string, long> counters)
  {
    if (counters == null || counters.Count == 0)
      return;
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("InventoryNumberGenerator.RestoreCounters");
    try
    {
      foreach (string key in counters.Keys)
      {
        string s = sessionTemporaryClone.Configurations.ReadStringNoCache("Archive", "Counters", key, true);
        long num1 = 0;
        ref long local = ref num1;
        long.TryParse(s, out local);
        if (num1 == counters[key])
        {
          long num2;
          sessionTemporaryClone.Configurations.WriteInteger("Archive", "Counters", key, num2 = num1 - 1L, 0L);
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("InventoryNumberGenerator.RestoreCounters");
    }
  }
}
