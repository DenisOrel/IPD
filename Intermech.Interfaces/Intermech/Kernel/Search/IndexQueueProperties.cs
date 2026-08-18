
// Type: Intermech.Kernel.Search.IndexQueueProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;


namespace Intermech.Kernel.Search
{
    /// <summary>Класс для хранения данных на индексацию</summary>
    public class IndexQueueProperties
    {
      /// <summary>Ид. версии объекта</summary>
      public long ObjectID;
      /// <summary>Ид. объекта</summary>
      public long ID;
      /// <summary>Ид. атрибута</summary>
      public int AttributeID;
      /// <summary>Порядковый номер значения атрибута</summary>
      public int InlistID;
      /// <summary>Текст для индексации</summary>
      public string Text;
      /// <summary>Опции атрибута</summary>
      public AttributeOptions Options;
      /// <summary>Типа данных атрибута</summary>
      public FieldTypes DataType;
      /// <summary>Тип действия, которое нужно выполнить с индексом</summary>
      public ActionType Action;

      public IndexQueueProperties(
        long objectID,
        int attrID,
        int inlistID,
        long id,
        string text,
        AttributeOptions options,
        FieldTypes dataType)
      {
        this.ObjectID = objectID;
        this.AttributeID = attrID;
        this.InlistID = inlistID;
        this.ID = id;
        this.Text = text;
        this.Options = options;
        this.DataType = dataType;
        this.Action = ActionType.Write;
      }

      public IndexQueueProperties(string newValue, IDBAttribute attr)
      {
        this.ObjectID = attr.DBObjectID;
        this.AttributeID = attr.AttributeID;
        this.InlistID = attr.Index;
        this.ID = attr.DB_ID;
        this.DataType = attr.DataType;
        this.Text = newValue;
        this.Options = attr.AttributeType.Options;
        this.Action = ActionType.Write;
      }

      public IndexQueueProperties(IndexQueueProperties source)
      {
        this.ObjectID = source.ObjectID;
        this.AttributeID = source.AttributeID;
        this.InlistID = source.InlistID;
        this.ID = source.ID;
        this.Text = source.Text;
        this.Options = source.Options;
        this.DataType = source.DataType;
        this.Action = source.Action;
      }

      public IndexQueueProperties(long objectID, ActionType actType)
      {
        this.ObjectID = objectID;
        this.AttributeID = 0;
        this.InlistID = 0;
        this.ID = 0L;
        this.Text = string.Empty;
        this.Options = AttributeOptions.None;
        this.DataType = FieldTypes.ftUnknown;
        this.Action = actType;
      }
    }
}
