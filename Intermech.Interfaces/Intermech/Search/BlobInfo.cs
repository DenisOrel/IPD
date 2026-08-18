
// Type: Intermech.Search.BlobInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Search
{
    [TypeConverter(typeof (BlobInfoConverter))]
    [Serializable]
    public sealed class BlobInfo
    {
      /// <summary>Идентификатор записи о файле (BLOB-поля)</summary>
      [DisplayName("Идентификатор записи о файле")]
      [Description("Идентификатор записи о файле (BLOB-поля)")]
      public long BlobID { get; set; }

      /// <summary>Реальный размер файла (BLOB-поля)</summary>
      [DisplayName("Реальный размер файла")]
      [Description("Реальный размер файла (BLOB-поля)")]
      public long RealFileSize { get; set; }

      /// <summary>Упакованный размер файла (BLOB-поля)</summary>
      [DisplayName("Упакованный размер файла")]
      [Description("Упакованный размер файла (BLOB-поля)")]
      public long PackedFileSize { get; set; }

      /// <summary>
      /// Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд
      /// (чтобы потом правильно работало сравнение)
      /// </summary>
      [DisplayName("Дата модификации файла")]
      [Description("Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд")]
      public DateTime ModifyDate { get; set; }

      /// <summary>Имя файла (для BLOB-поля пусто)</summary>
      [DisplayName("Имя файла")]
      [Description("Имя файла (для BLOB-поля пусто)")]
      public string FileName { get; set; }

      /// <summary>Метод запаковки</summary>
      [DisplayName("Метод запаковки")]
      [Description("Метод запаковки")]
      public ArcMethods ArcMethod { get; set; }

      /// <summary>
      /// Комментарии (только для тех, что хранятся в файловом хранилище).
      /// </summary>
      [DisplayName("Комментарии")]
      [Description("Комментарии (только для тех, что хранятся в файловом хранилище)")]
      public string Note { get; set; }

      /// <summary>Тип файла</summary>
      [DisplayName("Тип файла")]
      [Description("Тип файла")]
      public FileTypes FileType { get; set; }

      /// <summary>Автор файла (ObjectID)</summary>
      [DisplayName("Автор файла")]
      [Description("Автор файла")]
      [TypeConverter(typeof (ObjectLinkConverter))]
      public long AuthorVersionID { get; set; }
    }
}
