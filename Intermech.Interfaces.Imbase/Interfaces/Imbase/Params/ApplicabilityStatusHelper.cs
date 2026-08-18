// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.ApplicabilityStatusHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Imbase.Server;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

public class ApplicabilityStatusHelper
{
  private static Dictionary<string, ApplicabilityStatusEnum> _applStatusDict = new Dictionary<string, ApplicabilityStatusEnum>();

  public static ApplicabilityStatusEnum GetStatus(string statusStr)
  {
    if (ApplicabilityStatusHelper._applStatusDict.Count == 0)
      ApplicabilityStatusHelper.FillStatusDictionary();
    ApplicabilityStatusEnum applicabilityStatusEnum;
    return !ApplicabilityStatusHelper._applStatusDict.TryGetValue(statusStr, out applicabilityStatusEnum) ? ApplicabilityStatusEnum.None : applicabilityStatusEnum;
  }

  private static void FillStatusDictionary()
  {
    foreach (object obj in Enum.GetValues(typeof (ApplicabilityStatusEnum)))
    {
      Type type = obj.GetType();
      string enumName = type.GetEnumName(obj);
      if (!string.IsNullOrEmpty(enumName))
      {
        MemberInfo[] member = type.GetMember(enumName);
        if (member.Length != 0)
        {
          object[] customAttributes = member[0].GetCustomAttributes(typeof (ApplicabilityValue), false);
          if (customAttributes.Length != 0)
            ApplicabilityStatusHelper._applStatusDict[Convert.ToString(customAttributes[0])] = (ApplicabilityStatusEnum) obj;
        }
      }
    }
  }
}
