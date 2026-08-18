// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelationsComparerHelper
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server;

public static class RelationsComparerHelper
{
  private static List<FieldTypes> _supportedFieldTypes = new List<FieldTypes>();

  static RelationsComparerHelper()
  {
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftAutoInc);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftBoolean);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftDateTime);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftDouble);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftGuid);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftInteger);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftMeasured);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftObjectLink);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftPassword);
    RelationsComparerHelper._supportedFieldTypes.Add(FieldTypes.ftString);
  }

  public static bool EqualValues(object attr1Value, object attr2Value, FieldTypes attrFieldType)
  {
    if (RelationsComparerHelper._supportedFieldTypes.IndexOf(attrFieldType) < 0 || attr1Value == null || attr2Value == null)
      return false;
    if (attr1Value == DBNull.Value && attr2Value == DBNull.Value)
      return true;
    switch (attrFieldType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftAutoInc:
        long result1;
        long result2;
        return long.TryParse(attr1Value.ToString(), out result1) && long.TryParse(attr2Value.ToString(), out result2) && result1 == result2;
      case FieldTypes.ftDouble:
        double result3;
        double result4;
        return double.TryParse(attr1Value.ToString(), out result3) && double.TryParse(attr2Value.ToString(), out result4) && result3 == result4;
      case FieldTypes.ftDateTime:
        DateTime result5;
        DateTime result6;
        return DateTime.TryParse(attr1Value.ToString(), out result5) && DateTime.TryParse(attr2Value.ToString(), out result6) && result5 == result6;
      case FieldTypes.ftBoolean:
        bool result7;
        bool result8;
        return bool.TryParse(attr1Value.ToString(), out result7) && bool.TryParse(attr2Value.ToString(), out result8) && result7 == result8;
      case FieldTypes.ftMeasured:
        return RelationsComparerHelper.InternalEqualMeasuredValues(attr1Value, attr2Value);
      case FieldTypes.ftGuid:
        return RelationsComparerHelper.InternalEqualGuidValues(attr1Value, attr2Value);
      default:
        return attr1Value != DBNull.Value && attr2Value != DBNull.Value && attr1Value.ToString() == attr2Value.ToString();
    }
  }

  private static bool InternalEqualMeasuredValues(object attr1Value, object attr2Value)
  {
    return MeasureHelper.Compare(MeasureHelper.ConvertToMeasuredValue(attr1Value.ToString()), MeasureHelper.ConvertToMeasuredValue(attr2Value.ToString())) == CompareResult.Equal;
  }

  private static bool InternalEqualGuidValues(object attr1Value, object attr2Value)
  {
    if (attr1Value == DBNull.Value || attr2Value == DBNull.Value)
      return false;
    string str1 = attr1Value.ToString();
    string str2 = attr2Value.ToString();
    return GuidHelper.IsGuid(str1) && GuidHelper.IsGuid(str2) && new Guid(str1).Equals(new Guid(str2));
  }
}
