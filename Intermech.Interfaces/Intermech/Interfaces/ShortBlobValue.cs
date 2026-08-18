
// Type: Intermech.Interfaces.ShortBlobValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура с содержимым атрибута типа ftShortBlob (включает само тело блоба)
    /// </summary>
    [Serializable]
    public class ShortBlobValue : ShortBlobInfo
    {
      /// <summary>Массив с содержимым блоба</summary>
      public byte[] Value;

      /// <summary>
      /// Создает класс с пустыми значениями для последующего заполнения вручную
      /// </summary>
      /// <param name="blobID">Ид. блоба</param>
      public ShortBlobValue(long blobID)
        : base(0L, 0L, DateTime.Now, ArcMethods.NotPacked, string.Empty, blobID)
      {
        this.Value = (byte[]) null;
      }

      /// <summary>Конструктор</summary>
      /// <param name="realFileSize">Реальный размер файла (BLOB-поля)</param>
      /// <param name="packedFileSize">Упакованный размер файла (BLOB-поля)</param>
      /// <param name="modifyDate">Дата модификации файла (BLOB-поля) в локальном времени с округлением до секунд</param>
      /// <param name="arcMethod">Метод запаковки</param>
      /// <param name="note">Комментарии</param>
      public ShortBlobValue(
        long realFileSize,
        long packedFileSize,
        DateTime modifyDate,
        ArcMethods arcMethod,
        string note,
        long blobID)
        : base(realFileSize, packedFileSize, modifyDate, arcMethod, note)
      {
      }

      /// <summary>
      /// Возвращает true, если класс не инициализирован данными
      /// </summary>
      public bool Empty => this.Value == null;
    }
}
