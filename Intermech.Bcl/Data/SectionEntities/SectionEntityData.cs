
// Type: Intermech.Data.SectionEntities.SectionEntityData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Data.SectionEntities
{
    internal sealed class SectionEntityData
    {
      private readonly SectionEntity entity;
      private readonly Dictionary<SectionProperty, SectionEntityPropertyData> propertyDataStore;
      private NotifyCollectionChangedEventHandler metadataWatcher;

      public SectionEntityData(SectionEntity entity)
      {
        this.entity = entity != null ? entity : throw new ArgumentNullException(nameof (entity));
        this.propertyDataStore = new Dictionary<SectionProperty, SectionEntityPropertyData>();
      }

      public SectionEntity Entity => this.entity;

      public SectionEntityPropertyData this[SectionProperty sectionProperty]
      {
        get
        {
          if (sectionProperty == null)
            throw new ArgumentNullException(nameof (sectionProperty));
          SectionEntityPropertyData entityPropertyData;
          if (!this.propertyDataStore.TryGetValue(sectionProperty, out entityPropertyData))
          {
            entityPropertyData = new SectionEntityPropertyData();
            this.propertyDataStore.Add(sectionProperty, entityPropertyData);
          }
          return entityPropertyData;
        }
      }

      public NotifyCollectionChangedEventHandler MetadataWatcher
      {
        get => this.metadataWatcher;
        set => this.metadataWatcher = value;
      }
    }
}
