
// Type: Intermech.Search.CoreHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search
{
    public static class CoreHelper
    {
      public static int GetAttributeTypeID(ColumnInfo columnInfo)
      {
        return columnInfo.AttributeID != null ? AttributeTypeHelper.ConvertToAttributeTypeID(columnInfo.AttributeID) : throw new ArgumentException();
      }

      public static AttributeSourceTypes GetAttributeSourceType(ColumnInfo columnInfo)
      {
        int num = columnInfo.AttributeID != null ? AttributeTypeHelper.ConvertToAttributeTypeID(columnInfo.AttributeID) : throw new ArgumentException();
        AttributeSourceTypes attributeSourceType = columnInfo.AttributeSource;
        if (attributeSourceType == AttributeSourceTypes.Auto && AttributeTypeHelper.IsSystemAttributeTypeID(num))
          attributeSourceType = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) num);
        return attributeSourceType;
      }

      public static SeriesDateSettingsHolder GetSeriesAndDatesSettingsHolderFromRecordSetParams(
        DBRecordSetParams recordSetParams)
      {
        return recordSetParams.Tags != null && recordSetParams.Tags.Contains((object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}") ? (SeriesDateSettingsHolder) recordSetParams.Tags[(object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"] : (SeriesDateSettingsHolder) null;
      }

      public static bool GetBlockSeriesAndDatesFromRecordSetParams(DBRecordSetParams recordSetParams)
      {
        return recordSetParams.Tags != null && recordSetParams.Tags.Contains((object) "{02C00D9C-738E-42AB-A905-454BBD0644AD}") && Convert.ToBoolean(recordSetParams.Tags[(object) "{02C00D9C-738E-42AB-A905-454BBD0644AD}"]);
      }

      public static string GetFiltrationOverrideOwnerIDFromRecordSetParams(
        DBRecordSetParams recordSetParams)
      {
        if (recordSetParams.Tags != null && recordSetParams.Tags.Contains((object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"))
        {
          if (recordSetParams.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"] is string tag)
            return tag;
          VersionsRule fromRecordSetParams = CoreHelper.GetVersionsRuleFromRecordSetParams(recordSetParams);
          if (fromRecordSetParams != null)
            return fromRecordSetParams.RuleObjectGuid;
        }
        return (string) null;
      }

      public static VersionsRule GetVersionsRuleFromRecordSetParams(DBRecordSetParams recordSetParams)
      {
        return recordSetParams.Tags != null && recordSetParams.Tags.Contains((object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}") ? recordSetParams.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"] as VersionsRule : (VersionsRule) null;
      }
    }
}
