
// Type: Intermech.Interfaces.ShortBlobInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура с информацией о значении атрибута типа ftShortBlob (НЕ включает тело блоба)
    /// </summary>
    [Serializable]
    public class ShortBlobInfo
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
      /// <summary>Метод запаковки</summary>
      public ArcMethods ArcMethod;
      /// <summary>Комментарии</summary>
      public string Note;

      /// <summary>Конструктор</summary>
      /// <param name="realFileSize">Реальный размер файла (BLOB-поля)</param>
      /// <param name="packedFileSize">Упакованный размер файла (BLOB-поля)</param>
      /// <param name="modifyDate">Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд</param>
      /// <param name="arcMethod">Метод запаковки</param>
      /// <param name="note">Комментарии</param>
      public ShortBlobInfo(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        ArcMethods arcMethod,
        string note)
      {
        this.BlobID = 0L;
        this.RealFileSize = realFileSize;
        this.PackedFileSize = packedFileSize;
        this._ModifyDate = new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, modifyDate.Second);
        this.ArcMethod = arcMethod;
        this.Note = note;
      }

      /// <summary>Конструктор</summary>
      /// <param name="realFileSize">Реальный размер файла (BLOB-поля)</param>
      /// <param name="packedFileSize">Упакованный размер файла (BLOB-поля)</param>
      /// <param name="modifyDate">Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд</param>
      /// <param name="arcMethod">Метод запаковки</param>
      /// <param name="note">Комментарии</param>
      /// <param name="blobID">Ид. блоба</param>
      public ShortBlobInfo(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        ArcMethods arcMethod,
        string note,
        long blobID)
      {
        this.BlobID = blobID;
        this.RealFileSize = realFileSize;
        this.PackedFileSize = packedFileSize;
        this._ModifyDate = new DateTime(modifyDate.Year, modifyDate.Month, modifyDate.Day, modifyDate.Hour, modifyDate.Minute, modifyDate.Second);
        this.ArcMethod = arcMethod;
        this.Note = note;
      }

      /// <summary>
      /// Дата модификации BLOB-поля в локальном времени с округлением до секунд (чтобы потом правильно работало сравнение)
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
    }
}
