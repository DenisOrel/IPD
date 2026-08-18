
// Type: Intermech.Data.SectionEntities.SectionEntityPropertyData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Specialized;


namespace Intermech.Data.SectionEntities
{
    internal sealed class SectionEntityPropertyData
    {
      private EventHandler propertyWatcher;
      private INotifyCollectionChanged collection;
      private NotifyCollectionChangedEventHandler collectionWatcher;

      public EventHandler PropertyWatcher
      {
        get => this.propertyWatcher;
        set => this.propertyWatcher = value;
      }

      public INotifyCollectionChanged Collection
      {
        get => this.collection;
        set => this.collection = value;
      }

      public NotifyCollectionChangedEventHandler CollectionWatcher
      {
        get => this.collectionWatcher;
        set => this.collectionWatcher = value;
      }
    }
}
