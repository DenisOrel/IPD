// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADCompositionTuningRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal static class ECADCompositionTuningRelation
{
  public static void CreateAdditionalAttributes(
    ECADIntegratorSettings settings,
    ComponentsGroup group,
    CompositionItem relation)
  {
    relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeReplace), (object) true));
    string str1 = Convert.ToString(group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0].GetPropertyValue(settings.ASPosDesignation));
    if (string.IsNullOrEmpty(str1))
    {
      string posDesignation = group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0].PosDesignation;
      int startIndex = posDesignation.IndexOf('*');
      if (startIndex > 0)
      {
        string str2 = posDesignation.Substring(startIndex);
        if (str2.Length > 1)
          str1 = str2;
      }
    }
    if (!string.IsNullOrEmpty(str1))
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeASPosDesignation), (object) str1));
    string str3 = Convert.ToString(group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0].GetPropertyValue(settings.NominalsParameter));
    if (string.IsNullOrEmpty(str3))
      return;
    relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeNominals), (object) str3));
  }
}
