
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.AttributeChangeHistory
{
    [Serializable]
    public sealed class AttributeChangeHistoryRecord
    {
      public AttributeChangeHistoryRecord()
      {
        this.AttributeTypeID = 0;
        this.ObjectTypeID = -1;
        this.RelationTypeID = -1;
        this.ObjectID = 0L;
        this.RelationID = 0L;
        this.UserVersionID = 0L;
      }

      public long Key { get; set; }

      public int AttributeTypeID { get; set; }

      public int ObjectTypeID { get; set; }

      public int RelationTypeID { get; set; }

      public long ObjectID { get; set; }

      public long RelationID { get; set; }

      public long[] ObjectVersionIds { get; set; }

      public string ObjectCaption { get; set; }

      public DateTime Date { get; set; }

      public object Value { get; set; }

      public long UserVersionID { get; set; }

      public string UserName { get; set; }
    }
}
