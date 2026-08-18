
// Type: Intermech.Interfaces.BlobInformation4Import
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура с информацией о блобе/файле для его закачки программами импорта данных непосредственно в файловый шкаф
    /// </summary>
    [Serializable]
    public class BlobInformation4Import
    {
      /// <summary>Реальный размер файла (BLOB-поля)</summary>
      public long RealFileSize;
      /// <summary>Упакованный размер файла (BLOB-поля)</summary>
      public long PackedFileSize;
      /// <summary>
      /// Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд
      /// (чтобы потом правильно работало сравнение)
      /// </summary>
      private DateTime _ModifyDate;
      /// <summary>Имя файла (для BLOB-поля пусто)</summary>
      public string FileName;
      /// <summary>Метод запаковки</summary>
      public ArcMethods ArcMethod;
      /// <summary>
      /// Комментарии (только для тех, что хранятся в файловом хранилище).
      /// </summary>
      public string Note;
      /// <summary>Тип файла</summary>
      public FileTypes FileType;
      /// <summary>Автор файла (ObjectID)</summary>
      public long Author;
      /// <summary>Ид. атрибута</summary>
      public int AttributeID;
      /// <summary>Ид. версии объекта/ид. связи</summary>
      public long ObjectID;
      /// <summary>
      /// Локальные путь и имя запакованного файла. Готовый к закачке файл блоба должен быть на локальном диске сервера.
      /// </summary>
      public string LocalFileName;

      public BlobInformation4Import(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        string fileName,
        ArcMethods arcMethod,
        string note,
        FileTypes file_type,
        long file_author,
        int attributeID,
        long objectID,
        string localFileName)
      {
        this.RealFileSize = realFileSize;
        this.PackedFileSize = packedFileSize;
        this._ModifyDate = new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, modifyDate.Second);
        this.FileName = fileName;
        this.ArcMethod = arcMethod;
        this.Note = note;
        this.FileType = file_type;
        this.Author = file_author;
        this.AttributeID = attributeID;
        this.ObjectID = objectID;
        this.LocalFileName = localFileName;
      }

      /// <summary>
      /// Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд (чтобы потом правильно работало сравнение)
      /// </summary>
      public DateTime ModifyDate => this._ModifyDate;
    }
}
