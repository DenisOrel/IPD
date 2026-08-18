// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.AttributeCategoriesHelper
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Interfaces;

public class AttributeCategoriesHelper
{
  public static Dictionary<int, string> GetAttributeCategoriesDictionary(IDBObject obj)
  {
    Dictionary<int, string> categoriesDictionary = new Dictionary<int, string>();
    foreach (string str in Array.ConvertAll<object, string>(obj.Attributes.FindByID(Const.ClassAttrTypeAttributeTypeId).Values, (Converter<object, string>) (x => x.ToString())))
    {
      char[] chArray = new char[1]{ '=' };
      string[] strArray = str.Split(chArray);
      int result;
      if (strArray.Length.Equals(2) && int.TryParse(strArray[0], out result) && !categoriesDictionary.ContainsKey(result))
        categoriesDictionary.Add(result, strArray[1]);
    }
    return categoriesDictionary;
  }

  public static void SetAttributeCategoriesDictionary(
    IDBObject obj,
    Dictionary<int, string> attrCatDict)
  {
    obj.Attributes.FindByID(Const.ClassAttrTypeAttributeTypeId).Values = Array.ConvertAll<string, object>(attrCatDict.Select<KeyValuePair<int, string>, string>((Func<KeyValuePair<int, string>, string>) (pair => $"{pair.Key}={pair.Value}")).ToArray<string>(), (Converter<string, object>) (x => (object) x));
  }
}
