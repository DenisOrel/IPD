
// Type: Intermech.Interfaces.Briefcase.CheckMetadataLogItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Запись в логе по проверке метаданных</summary>
    [Serializable]
    public struct CheckMetadataLogItem
    {
      public string ItemTextCategory;
      public string ItemTextDBObject;
      public string ItemTextDifference;
      public string ItemTextBriefcaseValue;
      public string ItemTextDBValue;
      public CheckMetadataLogItemType Type;

      public CheckMetadataLogItem(
        CheckMetadataLogItemType itemType,
        string itemTextCategory,
        string itemTextDBObject,
        string itemTextDifference,
        string itemTextBriefcaseValue,
        string itemTextDBValue)
      {
        this.Type = itemType;
        this.ItemTextCategory = itemTextCategory;
        this.ItemTextDBObject = itemTextDBObject;
        this.ItemTextDifference = itemTextDifference;
        this.ItemTextBriefcaseValue = itemTextBriefcaseValue;
        this.ItemTextDBValue = itemTextDBValue;
      }

      public CheckMetadataLogItem(
        CheckMetadataLogItemType itemType,
        string itemTextCategory,
        string itemTextDBObject,
        string itemTextDifference)
      {
        this.Type = itemType;
        this.ItemTextCategory = itemTextCategory;
        this.ItemTextDBObject = itemTextDBObject;
        this.ItemTextDifference = itemTextDifference;
        this.ItemTextBriefcaseValue = string.Empty;
        this.ItemTextDBValue = string.Empty;
      }
    }
}
