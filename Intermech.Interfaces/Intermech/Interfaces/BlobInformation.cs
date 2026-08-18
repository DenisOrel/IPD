
// Type: Intermech.Interfaces.BlobInformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
    [Serializable]
    public struct BlobInformation : ICloneable
    {
      /// <summary>Идентификатор записи о файле (BLOB-поля)</summary>
      public long BlobID;
      /// <summary>Реальный размер файла (BLOB-поля)</summary>
      public long RealFileSize;
      /// <summary>Упакованный размер файла (BLOB-поля)</summary>
      public long PackedFileSize;
      /// <summary>
      /// Дата модификации файла (BLOB-поля) в локальном времени: автоматически округляется до секунд
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

      /// <summary>Создать экземпляр структуры</summary>
      /// <param name="realFileSize">Реальный размер файла (BLOB-поля)</param>
      /// <param name="packedFileSize">Упакованный размер файла (BLOB-поля)</param>
      /// <param name="modifyDate">Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд</param>
      /// <param name="fileName">Имя файла (для BLOB-поля пусто)</param>
      /// <param name="arcMethod">Метод запаковки</param>
      /// <param name="note">Комментарии (только для тех, что хранятся в файловом хранилище)</param>
      public BlobInformation(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        string fileName,
        ArcMethods arcMethod,
        string note)
      {
        this.BlobID = 0L;
        this.RealFileSize = realFileSize;
        this.PackedFileSize = packedFileSize;
        this._ModifyDate = new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, modifyDate.Second);
        this.FileName = fileName;
        this.ArcMethod = arcMethod;
        this.Note = note;
        this.FileType = FileTypes.ftNormal;
        this.Author = 0L;
      }

      /// <summary>Конструктор</summary>
      /// <param name="realFileSize">Реальный размер файла (BLOB-поля)</param>
      /// <param name="packedFileSize">Упакованный размер файла (BLOB-поля)</param>
      /// <param name="modifyDate">Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд</param>
      /// <param name="fileName">Имя файла (для BLOB-поля пусто)</param>
      /// <param name="arcMethod">Метод запаковки</param>
      /// <param name="note">Комментарии (только для тех, что хранятся в файловом хранилище)</param>
      /// <param name="file_type">Тип файла</param>
      /// <param name="file_author">ObjectID автора файла</param>
      public BlobInformation(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        string fileName,
        ArcMethods arcMethod,
        string note,
        FileTypes file_type,
        long file_author)
      {
        this.BlobID = 0L;
        this.RealFileSize = realFileSize;
        this.PackedFileSize = packedFileSize;
        this._ModifyDate = new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, modifyDate.Second);
        this.FileName = fileName;
        this.ArcMethod = arcMethod;
        this.Note = note;
        this.FileType = file_type;
        this.Author = file_author;
      }

      public BlobInformation(ShortBlobInfo shortBlob)
      {
        this.BlobID = shortBlob.BlobID;
        this.RealFileSize = shortBlob.RealFileSize;
        this.PackedFileSize = shortBlob.PackedFileSize;
        this._ModifyDate = shortBlob.ModifyDate;
        this.FileName = string.Empty;
        this.ArcMethod = shortBlob.ArcMethod;
        this.Note = shortBlob.Note;
        this.FileType = FileTypes.ftNormal;
        this.Author = 0L;
      }

      /// <summary>
      /// Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд (чтобы потом правильно работало сравнение)
      /// </summary>
      public DateTime ModifyDate
      {
        get => this._ModifyDate;
        set
        {
          value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
          this._ModifyDate = value;
        }
      }

      /// <summary>Создать пустую структуру с информацией о BLOB-поле</summary>
      /// <returns>Пустая структура с информацией о BLOB-поле</returns>
      public static BlobInformation EmptyBlobInformation()
      {
        return new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty);
      }

      /// <summary>Создать копию экземпляра структуры</summary>
      /// <returns>Копия экземпляра структуры</returns>
      public BlobInformation Clone()
      {
        return new BlobInformation(this.RealFileSize, this.PackedFileSize, this.ModifyDate, this.FileName, this.ArcMethod, this.Note)
        {
          BlobID = this.BlobID
        };
      }

      object ICloneable.Clone()
      {
        return (object) new BlobInformation(this.RealFileSize, this.PackedFileSize, this.ModifyDate, this.FileName, this.ArcMethod, this.Note, this.FileType, this.Author)
        {
          BlobID = this.BlobID
        };
      }
    }
}
