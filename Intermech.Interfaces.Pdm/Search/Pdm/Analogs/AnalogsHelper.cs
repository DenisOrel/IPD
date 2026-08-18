// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

public static class AnalogsHelper
{
  public static AnalogSelectionMode GetAnalogSelectionModeFromRecordSetParams(
    DBRecordSetParams recordSetParams)
  {
    return recordSetParams.Tags != null ? AnalogsHelper.GetAnalogSelectionModeFromRecordSetParamsTags(recordSetParams.Tags) : AnalogSelectionMode.None;
  }

  public static AnalogSelectionMode GetAnalogSelectionModeFromRecordSetParamsTags(
    HybridDictionary hybridDictionary)
  {
    if (hybridDictionary == null)
      throw new ArgumentNullException(nameof (hybridDictionary));
    return hybridDictionary.Contains((object) "B6002FDD-2998-4EE8-986C-66728CBBFBD7") ? (AnalogSelectionMode) Convert.ToInt32(hybridDictionary[(object) "B6002FDD-2998-4EE8-986C-66728CBBFBD7"]) : AnalogSelectionMode.None;
  }

  public static void SetAnalogSelectionModeToRecordSetParamsTags(
    HybridDictionary hybridDictionary,
    AnalogSelectionMode analogSelectionMode)
  {
    if (hybridDictionary == null)
      throw new ArgumentNullException(nameof (hybridDictionary));
    hybridDictionary[(object) "B6002FDD-2998-4EE8-986C-66728CBBFBD7"] = (object) analogSelectionMode;
  }

  public static bool IsObjectTypeSupportedAnalogs(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return MetaDataHelper.GetObjectTypeApplicabilities(objectTypeID).Any<IMSApplicability>((Func<IMSApplicability, bool>) (o => o.RelationTypeID == AnalogsConstants.AnalogsRelationTypeID));
  }
}
