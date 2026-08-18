
// Type: Intermech.Interfaces.ImportedObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Структура с инфой по импортированному объекту</summary>
    [Serializable]
    public class ImportedObjectInfo : IImportedObjectInfo
    {
      public ImportedObjectInfo(long objectID, long id)
      {
        this.ObjectID = objectID;
        this.ID = id;
      }

      public ImportedObjectInfo(Exception exception)
      {
        this.ObjectID = 0L;
        this.ID = 0L;
        this.ImportMessage = exception;
      }

      public long ObjectID { get; }

      public long ID { get; }

      public Exception ImportMessage { get; }
    }
}
