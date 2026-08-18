// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.RemoveBadDesignationSuffixAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public class RemoveBadDesignationSuffixAction(ValueBag bag, StringKey valueKey, int documentType) : 
  ChangeDesignationSuffixAction(bag, valueKey, documentType)
{
  protected override string ChangeSuffix(string designation)
  {
    foreach (string legacyDocCode in DocumentDesignationHelper.GetLegacyDocCodes())
    {
      if (designation.EndsWith(legacyDocCode, StringComparison.CurrentCultureIgnoreCase))
      {
        designation = designation.Remove(designation.Length - legacyDocCode.Length, legacyDocCode.Length);
        break;
      }
    }
    return designation;
  }
}
